namespace Aisino.Fwkp.DataMigrationTool.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SQLite;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class TaxcodeChangeForm : BaseForm
    {
        private AisinoBTN btn_Close;
        private AisinoBTN btn_Migration;
        private AisinoBTN btn_Select_DB;
        private CheckBox chkBoxBM;
        private CheckBox chkBoxDJ;
        private CheckBox chkBoxSQD;
        private CheckBox chkBoxXXFP;
        private CheckBox chkBoxYH;
        private ComboBox comboBoxOldTaxcode;
        private IContainer components;
        private List<string> configuration = new List<string> { "BM", "XXFP", "XSDJ", "YHXX", "SQD" };
        private GroupBox groupBox1;
        private bool isOldTaxcodeDatabase = true;
        private Label label3;
        private Label lblOldDBPath;
        private RadioButton radioBtnOldDBPath;
        private RadioButton radioBtnOldDBSel;
        private Dictionary<string, string> tables;
        private Dictionary<string, string[]> tablesPrimaryKey;

        public TaxcodeChangeForm()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("BM_KH", "Delete");
            dictionary.Add("BM_SP", "Delete");
            dictionary.Add("BM_SFHR", "Delete");
            dictionary.Add("BM_FYXM", "Delete");
            dictionary.Add("BM_GHDW", "Delete");
            dictionary.Add("BM_CL", "Delete");
            dictionary.Add("BM_XHDW", "Delete");
            dictionary.Add("XXFP", "Merge");
            dictionary.Add("XXFP_MX", "Merge");
            dictionary.Add("XXFP_XHQD", "Merge");
            dictionary.Add("XSDJ", "Merge");
            dictionary.Add("XSDJ_HY", "Merge");
            dictionary.Add("XSDJ_MX", "Merge");
            dictionary.Add("XSDJ_MXYL", "Merge");
            dictionary.Add("XSDJ_MX_HY", "Merge");
            dictionary.Add("XSDJ_YL", "Merge");
            dictionary.Add("QX_JSXX", "Merge");
            dictionary.Add("QX_JSXX_GNXX", "Merge");
            dictionary.Add("QX_YHXX", "Merge");
            dictionary.Add("QX_YHXX_JSXX", "Merge");
            dictionary.Add("HZFPHY_SQD", "Merge");
            dictionary.Add("HZFPHY_SQD_MX", "Merge");
            dictionary.Add("HZFP_SQD", "Merge");
            dictionary.Add("HZFP_SQD_MX", "Merge");
            this.tables = dictionary;
            Dictionary<string, string[]> dictionary2 = new Dictionary<string, string[]>();
            dictionary2.Add("BM_KH", new string[] { "BM" });
            dictionary2.Add("BM_SP", new string[] { "BM" });
            dictionary2.Add("BM_SFHR", new string[] { "BM" });
            dictionary2.Add("BM_FYXM", new string[] { "BM" });
            dictionary2.Add("BM_GHDW", new string[] { "BM" });
            dictionary2.Add("BM_CL", new string[] { "BM" });
            dictionary2.Add("BM_XHDW", new string[] { "BM" });
            dictionary2.Add("XXFP", new string[] { "FPZL", "FPDM", "FPHM" });
            dictionary2.Add("XXFP_MX", new string[] { "FPZL", "FPDM", "FPHM", "FPMXXH" });
            dictionary2.Add("XXFP_XHQD", new string[] { "FPZL", "FPDM", "FPHM", "FPMXXH" });
            dictionary2.Add("XSDJ", new string[] { "BH" });
            dictionary2.Add("XSDJ_HY", new string[] { "BH" });
            dictionary2.Add("XSDJ_MX", new string[] { "XSDJBH", "XH" });
            dictionary2.Add("XSDJ_MXYL", new string[] { "XSDJBH", "XH" });
            dictionary2.Add("XSDJ_MX_HY", new string[] { "XSDJBH", "XH" });
            dictionary2.Add("XSDJ_YL", new string[] { "BH" });
            dictionary2.Add("QX_JSXX", new string[] { "DM" });
            dictionary2.Add("QX_JSXX_GNXX", new string[] { "JSXX_DM", "GNXX_DM" });
            dictionary2.Add("QX_YHXX", new string[] { "DM" });
            dictionary2.Add("QX_YHXX_JSXX", new string[] { "YHXX_DM", "JSXX_DM" });
            dictionary2.Add("HZFPHY_SQD", new string[] { "SQDH" });
            dictionary2.Add("HZFPHY_SQD_MX", new string[] { "SQDH", "MXXH" });
            dictionary2.Add("HZFP_SQD", new string[] { "SQDH" });
            dictionary2.Add("HZFP_SQD_MX", new string[] { "SQDH", "MXXH" });
            this.tablesPrimaryKey = dictionary2;
            this.InitializeComponent();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btn_Migration_Click(object sender, EventArgs e)
        {
            if ((!this.chkBoxBM.Checked && !this.chkBoxXXFP.Checked) && (!this.chkBoxDJ.Checked && !this.chkBoxYH.Checked))
            {
                MessageBox.Show("请选择需要迁移的表", this.Text);
            }
            else
            {
                string path = string.Empty;
                if (this.radioBtnOldDBSel.Checked)
                {
                    path = this.comboBoxOldTaxcode.Text.EndsWith(@"\") ? (this.comboBoxOldTaxcode.Text + @"Bin\cc3268.dll") : (this.comboBoxOldTaxcode.Text + @"\Bin\cc3268.dll");
                    if (!File.Exists(path))
                    {
                        MessageBox.Show("请选择旧税号软件安装路径！", this.Text);
                        return;
                    }
                }
                else if (!string.IsNullOrEmpty(this.lblOldDBPath.Text.Trim()))
                {
                    path = Path.Combine(this.lblOldDBPath.Text, @"Bin\cc3268.dll");
                }
                if (string.IsNullOrEmpty(path))
                {
                    MessageBox.Show("请选择旧税号软件安装路径！", this.Text);
                }
                else if (DialogResult.OK == MessageBox.Show("此操作将使用所选数据替换客户、商品等编码表，合并发票、文本单据、用户表，请注意保存数据。\n是否继续？", this.Text, MessageBoxButtons.OKCancel))
                {
                    Dictionary<string, DataTable> oldDataTables = this.GetOldDataTables(path, this.tables);
                    path = Path.Combine(Path.GetDirectoryName(Application.StartupPath), @"Bin\cc3268.dll");
                    if (this.DealTable(path, oldDataTables))
                    {
                        MessageBox.Show("迁移成功!", this.Text);
                    }
                    else
                    {
                        MessageBox.Show("迁移失败!", this.Text);
                    }
                }
            }
        }

        private void btn_Select_DB_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog {
                Description = "选择以旧税号命名的升级版软件安装目录"
            };
            if (DialogResult.OK == dialog.ShowDialog())
            {
                if (File.Exists(Path.Combine(dialog.SelectedPath, @"Bin\cc3268.dll")))
                {
                    this.lblOldDBPath.Text = dialog.SelectedPath;
                }
                else
                {
                    MessageBox.Show("所选路径未发现旧数据!", this.Text);
                }
            }
        }

        private void chkBoxBM_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkBoxBM.Checked)
            {
                if (!this.configuration.Contains("BM"))
                {
                    this.configuration.Add("BM");
                    this.tables.Add("BM_KH", "Delete");
                    this.tables.Add("BM_SP", "Delete");
                    this.tables.Add("BM_SFHR", "Delete");
                    this.tables.Add("BM_FYXM", "Delete");
                    this.tables.Add("BM_GHDW", "Delete");
                    this.tables.Add("BM_CL", "Delete");
                    this.tables.Add("BM_XHDW", "Delete");
                    this.tablesPrimaryKey.Add("BM_KH", new string[] { "BM" });
                    this.tablesPrimaryKey.Add("BM_SP", new string[] { "BM" });
                    this.tablesPrimaryKey.Add("BM_SFHR", new string[] { "BM" });
                    this.tablesPrimaryKey.Add("BM_FYXM", new string[] { "BM" });
                    this.tablesPrimaryKey.Add("BM_GHDW", new string[] { "BM" });
                    this.tablesPrimaryKey.Add("BM_CL", new string[] { "BM" });
                    this.tablesPrimaryKey.Add("BM_XHDW", new string[] { "BM" });
                }
            }
            else if (this.configuration.Contains("BM"))
            {
                this.configuration.Remove("BM");
                this.tables.Remove("BM_KH");
                this.tables.Remove("BM_SP");
                this.tables.Remove("BM_SFHR");
                this.tables.Remove("BM_FYXM");
                this.tables.Remove("BM_GHDW");
                this.tables.Remove("BM_CL");
                this.tables.Remove("BM_XHDW");
                this.tablesPrimaryKey.Remove("BM_KH");
                this.tablesPrimaryKey.Remove("BM_SP");
                this.tablesPrimaryKey.Remove("BM_SFHR");
                this.tablesPrimaryKey.Remove("BM_FYXM");
                this.tablesPrimaryKey.Remove("BM_GHDW");
                this.tablesPrimaryKey.Remove("BM_CL");
                this.tablesPrimaryKey.Remove("BM_XHDW");
            }
        }

        private void chkBoxDJ_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkBoxDJ.Checked)
            {
                if (!this.configuration.Contains("XSDJ"))
                {
                    this.configuration.Add("XSDJ");
                    this.tables.Add("XSDJ", "Merge");
                    this.tables.Add("XSDJ_HY", "Merge");
                    this.tables.Add("XSDJ_MX", "Merge");
                    this.tables.Add("XSDJ_MXYL", "Merge");
                    this.tables.Add("XSDJ_MX_HY", "Merge");
                    this.tables.Add("XSDJ_YL", "Merge");
                    this.tablesPrimaryKey.Add("XSDJ", new string[] { "BH" });
                    this.tablesPrimaryKey.Add("XSDJ_HY", new string[] { "BH" });
                    this.tablesPrimaryKey.Add("XSDJ_MX", new string[] { "XSDJBH", "XH" });
                    this.tablesPrimaryKey.Add("XSDJ_MXYL", new string[] { "XSDJBH", "XH" });
                    this.tablesPrimaryKey.Add("XSDJ_MX_HY", new string[] { "XSDJBH", "XH" });
                    this.tablesPrimaryKey.Add("XSDJ_YL", new string[] { "BH" });
                }
            }
            else if (this.configuration.Contains("XSDJ"))
            {
                this.configuration.Remove("XSDJ");
                this.tables.Remove("XSDJ");
                this.tables.Remove("XSDJ_HY");
                this.tables.Remove("XSDJ_MX");
                this.tables.Remove("XSDJ_MXYL");
                this.tables.Remove("XSDJ_MX_HY");
                this.tables.Remove("XSDJ_YL");
                this.tablesPrimaryKey.Remove("XSDJ");
                this.tablesPrimaryKey.Remove("XSDJ_HY");
                this.tablesPrimaryKey.Remove("XSDJ_MX");
                this.tablesPrimaryKey.Remove("XSDJ_MXYL");
                this.tablesPrimaryKey.Remove("XSDJ_MX_HY");
                this.tablesPrimaryKey.Remove("XSDJ_YL");
            }
        }

        private void chkBoxSQD_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkBoxSQD.Checked)
            {
                if (!this.configuration.Contains("SQD"))
                {
                    this.configuration.Add("SQD");
                    this.tables.Add("HZFPHY_SQD", "Merge");
                    this.tables.Add("HZFPHY_SQD_MX", "Merge");
                    this.tables.Add("HZFP_SQD", "Merge");
                    this.tables.Add("HZFP_SQD_MX", "Merge");
                    this.tablesPrimaryKey.Add("HZFPHY_SQD", new string[] { "SQDH" });
                    this.tablesPrimaryKey.Add("HZFPHY_SQD_MX", new string[] { "SQDH", "MXXH" });
                    this.tablesPrimaryKey.Add("HZFP_SQD", new string[] { "SQDH" });
                    this.tablesPrimaryKey.Add("HZFP_SQD_MX", new string[] { "SQDH", "MXXH" });
                }
            }
            else if (this.configuration.Contains("SQD"))
            {
                this.configuration.Remove("SQD");
                this.tables.Remove("HZFPHY_SQD");
                this.tables.Remove("HZFPHY_SQD_MX");
                this.tables.Remove("HZFP_SQD");
                this.tables.Remove("HZFP_SQD_MX");
                this.tablesPrimaryKey.Remove("HZFPHY_SQD");
                this.tablesPrimaryKey.Remove("HZFPHY_SQD_MX");
                this.tablesPrimaryKey.Remove("HZFP_SQD");
                this.tablesPrimaryKey.Remove("HZFP_SQD_MX");
            }
        }

        private void chkBoxXXFP_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkBoxXXFP.Checked)
            {
                if (!this.configuration.Contains("XXFP"))
                {
                    this.configuration.Add("XXFP");
                    this.tables.Add("XXFP", "Merge");
                    this.tables.Add("XXFP_MX", "Merge");
                    this.tables.Add("XXFP_XHQD", "Merge");
                    this.tablesPrimaryKey.Add("XXFP", new string[] { "FPZL", "FPDM", "FPHM" });
                    this.tablesPrimaryKey.Add("XXFP_MX", new string[] { "FPZL", "FPDM", "FPHM", "FPMXXH" });
                    this.tablesPrimaryKey.Add("XXFP_XHQD", new string[] { "FPZL", "FPDM", "FPHM", "FPMXXH" });
                }
            }
            else if (this.configuration.Contains("XXFP"))
            {
                this.configuration.Remove("XXFP");
                this.tables.Remove("XXFP");
                this.tables.Remove("XXFP_MX");
                this.tables.Remove("XXFP_XHQD");
                this.tablesPrimaryKey.Remove("XXFP");
                this.tablesPrimaryKey.Remove("XXFP_MX");
                this.tablesPrimaryKey.Remove("XXFP_XHQD");
            }
        }

        private void chkBoxYH_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkBoxYH.Checked)
            {
                if (!this.configuration.Contains("YHXX"))
                {
                    this.configuration.Add("YHXX");
                    this.tables.Add("QX_JSXX", "Merge");
                    this.tables.Add("QX_JSXX_GNXX", "Merge");
                    this.tables.Add("QX_YHXX", "Merge");
                    this.tables.Add("QX_YHXX_JSXX", "Merge");
                    this.tablesPrimaryKey.Add("QX_JSXX", new string[] { "DM" });
                    this.tablesPrimaryKey.Add("QX_JSXX_GNXX", new string[] { "JSXX_DM", "GNXX_DM" });
                    this.tablesPrimaryKey.Add("QX_YHXX", new string[] { "DM" });
                    this.tablesPrimaryKey.Add("QX_YHXX_JSXX", new string[] { "YHXX_DM", "JSXX_DM" });
                }
            }
            else if (this.configuration.Contains("YHXX"))
            {
                this.configuration.Remove("YHXX");
                this.tables.Remove("QX_JSXX");
                this.tables.Remove("QX_JSXX_GNXX");
                this.tables.Remove("QX_YHXX");
                this.tables.Remove("QX_YHXX_JSXX");
                this.tablesPrimaryKey.Remove("QX_JSXX");
                this.tablesPrimaryKey.Remove("QX_JSXX_GNXX");
                this.tablesPrimaryKey.Remove("QX_YHXX");
                this.tablesPrimaryKey.Remove("QX_YHXX_JSXX");
            }
        }

        private void comboBoxOldTaxcode_KeyDown(object sender, KeyEventArgs e)
        {
            if ((((e.KeyData == Keys.LButton) || (e.KeyData == Keys.RButton)) || ((e.KeyData == Keys.MButton) || (e.KeyData == Keys.Up))) || (((e.KeyData == Keys.Down) || (e.KeyData == Keys.Left)) || (e.KeyData == Keys.Right)))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void comboBoxOldTaxcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private bool DealTable(string relativePath, Dictionary<string, DataTable> oldDataTableDic)
        {
            bool flag;
            using (SQLiteConnection connection = new SQLiteConnection(this.GetSQLiteConnString(relativePath)))
            {
                connection.Open();
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        SQLiteCommand command = new SQLiteCommand(connection);
                        command.set_Transaction(transaction);
                        foreach (string str in oldDataTableDic.Keys)
                        {
                            DataTable dataTable = new DataTable();
                            if ("Delete" == this.tables[str])
                            {
                                dataTable = oldDataTableDic[str];
                            }
                            else if ("Merge" == this.tables[str])
                            {
                                SQLiteCommand command2 = new SQLiteCommand("SELECT * FROM " + str, connection);
                                new SQLiteDataAdapter(command2).Fill(dataTable);
                                dataTable.Merge(oldDataTableDic[str], true);
                            }
                            command.CommandText = "DELETE FROM " + str;
                            command.ExecuteNonQuery();
                            string str2 = "insert into " + str + "(";
                            foreach (DataColumn column in dataTable.Columns)
                            {
                                str2 = str2 + column + ",";
                            }
                            str2 = str2.Substring(0, str2.Length - 1) + ") values(";
                            foreach (DataColumn column2 in dataTable.Columns)
                            {
                                object obj3 = str2;
                                str2 = string.Concat(new object[] { obj3, "@", column2, "," });
                            }
                            str2 = str2.Substring(0, str2.Length - 1) + ")";
                            SQLiteParameter[] parameterArray = new SQLiteParameter[dataTable.Columns.Count];
                            foreach (DataRow row in dataTable.Rows)
                            {
                                for (int i = 0; i < dataTable.Columns.Count; i++)
                                {
                                    object obj2 = row[i];
                                    parameterArray[i] = new SQLiteParameter(dataTable.Columns[i].ColumnName, obj2);
                                }
                                command.CommandText = str2;
                                command.get_Parameters().AddRange(parameterArray);
                                command.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                        flag = true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        flag = false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return flag;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Dictionary<string, DataTable> GetOldDataTables(string relativePath, Dictionary<string, string> tables)
        {
            try
            {
                Dictionary<string, DataTable> dictionary = new Dictionary<string, DataTable>();
                using (SQLiteConnection connection = new SQLiteConnection(this.GetSQLiteConnString(relativePath)))
                {
                    connection.Open();
                    foreach (string str in tables.Keys)
                    {
                        SQLiteCommand command = new SQLiteCommand("SELECT * FROM " + str, connection);
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (((str == "XXFP") && dataTable.Columns.Contains("BSZT")) && (dataTable.Columns.Contains("BSRZ") && (base.TaxCardInstance.get_OldTaxCode() != "")))
                        {
                            foreach (DataRow row in dataTable.Rows)
                            {
                                if (row["XFSH"].ToString() == base.TaxCardInstance.get_OldTaxCode())
                                {
                                    row["BSZT"] = "1";
                                    row["BSRZ"] = "已做过一体化变更，状态置为已报送。";
                                    dataTable.AcceptChanges();
                                }
                            }
                        }
                        if ("Merge" == tables[str])
                        {
                            DataColumn[] columnArray = new DataColumn[this.tablesPrimaryKey[str].Length];
                            int num = 0;
                            foreach (string str3 in this.tablesPrimaryKey[str])
                            {
                                columnArray[num++] = dataTable.Columns[str3];
                            }
                            dataTable.PrimaryKey = columnArray;
                        }
                        dictionary[str] = dataTable;
                    }
                    connection.Close();
                }
                return dictionary;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetSQLiteConnString(string dataPath)
        {
            try
            {
                return string.Format("Data Source={0};Pooling=true;Password=LoveR1314;", dataPath);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(TaxcodeChangeForm));
            this.btn_Select_DB = new AisinoBTN();
            this.comboBoxOldTaxcode = new ComboBox();
            this.radioBtnOldDBSel = new RadioButton();
            this.btn_Migration = new AisinoBTN();
            this.radioBtnOldDBPath = new RadioButton();
            this.btn_Close = new AisinoBTN();
            this.label3 = new Label();
            this.lblOldDBPath = new Label();
            this.chkBoxBM = new CheckBox();
            this.chkBoxXXFP = new CheckBox();
            this.chkBoxDJ = new CheckBox();
            this.chkBoxYH = new CheckBox();
            this.groupBox1 = new GroupBox();
            this.chkBoxSQD = new CheckBox();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.btn_Select_DB.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.btn_Select_DB.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.btn_Select_DB.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.btn_Select_DB.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btn_Select_DB.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.btn_Select_DB.ForeColor = Color.White;
            this.btn_Select_DB.ImageAlign = ContentAlignment.MiddleLeft;
            this.btn_Select_DB.Location = new Point(0x142, 0x53);
            this.btn_Select_DB.Name = "btn_Select_DB";
            this.btn_Select_DB.Size = new Size(0x60, 0x1c);
            this.btn_Select_DB.TabIndex = 0x53;
            this.btn_Select_DB.Tag = "自定义数据库选择";
            this.btn_Select_DB.Text = "数据库选择";
            this.btn_Select_DB.UseVisualStyleBackColor = true;
            this.btn_Select_DB.Click += new EventHandler(this.btn_Select_DB_Click);
            this.comboBoxOldTaxcode.Location = new Point(140, 0x38);
            this.comboBoxOldTaxcode.Name = "comboBoxOldTaxcode";
            this.comboBoxOldTaxcode.Size = new Size(0x116, 20);
            this.comboBoxOldTaxcode.TabIndex = 0x56;
            this.comboBoxOldTaxcode.KeyDown += new KeyEventHandler(this.comboBoxOldTaxcode_KeyDown);
            this.comboBoxOldTaxcode.KeyPress += new KeyPressEventHandler(this.comboBoxOldTaxcode_KeyPress);
            this.radioBtnOldDBSel.AutoSize = true;
            this.radioBtnOldDBSel.Checked = true;
            this.radioBtnOldDBSel.Font = new Font("宋体", 10.5f);
            this.radioBtnOldDBSel.Location = new Point(0x20, 0x39);
            this.radioBtnOldDBSel.Name = "radioBtnOldDBSel";
            this.radioBtnOldDBSel.Size = new Size(0x6d, 0x12);
            this.radioBtnOldDBSel.TabIndex = 0x54;
            this.radioBtnOldDBSel.TabStop = true;
            this.radioBtnOldDBSel.Text = "已存在税号：";
            this.radioBtnOldDBSel.UseVisualStyleBackColor = true;
            this.radioBtnOldDBSel.Click += new EventHandler(this.radioBtnOldDB_Click);
            this.btn_Migration.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.btn_Migration.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.btn_Migration.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.btn_Migration.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btn_Migration.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.btn_Migration.ForeColor = Color.White;
            this.btn_Migration.ImageAlign = ContentAlignment.MiddleLeft;
            this.btn_Migration.Location = new Point(0xda, 0x108);
            this.btn_Migration.Name = "btn_Migration";
            this.btn_Migration.Size = new Size(0x60, 0x1c);
            this.btn_Migration.TabIndex = 0x57;
            this.btn_Migration.Tag = "自定义数据库选择";
            this.btn_Migration.Text = "迁移";
            this.btn_Migration.UseVisualStyleBackColor = true;
            this.btn_Migration.Click += new EventHandler(this.btn_Migration_Click);
            this.radioBtnOldDBPath.AutoSize = true;
            this.radioBtnOldDBPath.Checked = true;
            this.radioBtnOldDBPath.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.radioBtnOldDBPath.Location = new Point(0x20, 0x58);
            this.radioBtnOldDBPath.Name = "radioBtnOldDBPath";
            this.radioBtnOldDBPath.Size = new Size(0x6d, 0x12);
            this.radioBtnOldDBPath.TabIndex = 0x54;
            this.radioBtnOldDBPath.TabStop = true;
            this.radioBtnOldDBPath.Text = "数据库路径：";
            this.radioBtnOldDBPath.UseVisualStyleBackColor = true;
            this.radioBtnOldDBPath.Click += new EventHandler(this.radioBtnOldDB_Click);
            this.btn_Close.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.btn_Close.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.btn_Close.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.btn_Close.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btn_Close.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.btn_Close.ForeColor = Color.White;
            this.btn_Close.ImageAlign = ContentAlignment.MiddleLeft;
            this.btn_Close.Location = new Point(0x142, 0x108);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new Size(0x60, 0x1c);
            this.btn_Close.TabIndex = 0x57;
            this.btn_Close.Tag = "自定义数据库选择";
            this.btn_Close.Text = "取消";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new EventHandler(this.btn_Close_Click);
            this.label3.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label3.Location = new Point(0x1c, 0x13);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x83, 0x13);
            this.label3.TabIndex = 0x55;
            this.label3.Text = "旧税号数据源选择：";
            this.lblOldDBPath.Font = new Font("宋体", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lblOldDBPath.Location = new Point(0x33, 0x79);
            this.lblOldDBPath.Name = "lblOldDBPath";
            this.lblOldDBPath.Size = new Size(0x16f, 0x24);
            this.lblOldDBPath.TabIndex = 0x58;
            this.chkBoxBM.AutoSize = true;
            this.chkBoxBM.Checked = true;
            this.chkBoxBM.CheckState = CheckState.Checked;
            this.chkBoxBM.Location = new Point(14, 0x1b);
            this.chkBoxBM.Name = "chkBoxBM";
            this.chkBoxBM.Size = new Size(60, 0x10);
            this.chkBoxBM.TabIndex = 0x59;
            this.chkBoxBM.Text = "编码表";
            this.chkBoxBM.UseVisualStyleBackColor = true;
            this.chkBoxBM.CheckedChanged += new EventHandler(this.chkBoxBM_CheckedChanged);
            this.chkBoxXXFP.AutoSize = true;
            this.chkBoxXXFP.Checked = true;
            this.chkBoxXXFP.CheckState = CheckState.Checked;
            this.chkBoxXXFP.Location = new Point(0x59, 0x1b);
            this.chkBoxXXFP.Name = "chkBoxXXFP";
            this.chkBoxXXFP.Size = new Size(0x54, 0x10);
            this.chkBoxXXFP.TabIndex = 0x59;
            this.chkBoxXXFP.Text = "销项发票表";
            this.chkBoxXXFP.UseVisualStyleBackColor = true;
            this.chkBoxXXFP.CheckedChanged += new EventHandler(this.chkBoxXXFP_CheckedChanged);
            this.chkBoxDJ.AutoSize = true;
            this.chkBoxDJ.Checked = true;
            this.chkBoxDJ.CheckState = CheckState.Checked;
            this.chkBoxDJ.Location = new Point(0xbc, 0x1b);
            this.chkBoxDJ.Name = "chkBoxDJ";
            this.chkBoxDJ.Size = new Size(0x54, 0x10);
            this.chkBoxDJ.TabIndex = 0x59;
            this.chkBoxDJ.Text = "销售单据表";
            this.chkBoxDJ.UseVisualStyleBackColor = true;
            this.chkBoxDJ.CheckedChanged += new EventHandler(this.chkBoxDJ_CheckedChanged);
            this.chkBoxYH.AutoSize = true;
            this.chkBoxYH.Checked = true;
            this.chkBoxYH.CheckState = CheckState.Checked;
            this.chkBoxYH.Location = new Point(0x11f, 0x1b);
            this.chkBoxYH.Name = "chkBoxYH";
            this.chkBoxYH.Size = new Size(0x54, 0x10);
            this.chkBoxYH.TabIndex = 0x59;
            this.chkBoxYH.Text = "用户信息表";
            this.chkBoxYH.UseVisualStyleBackColor = true;
            this.chkBoxYH.CheckedChanged += new EventHandler(this.chkBoxYH_CheckedChanged);
            this.groupBox1.Controls.Add(this.chkBoxSQD);
            this.groupBox1.Controls.Add(this.chkBoxBM);
            this.groupBox1.Controls.Add(this.chkBoxYH);
            this.groupBox1.Controls.Add(this.chkBoxXXFP);
            this.groupBox1.Controls.Add(this.chkBoxDJ);
            this.groupBox1.Location = new Point(0x20, 0xa5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x182, 0x52);
            this.groupBox1.TabIndex = 90;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "需要迁移的表：";
            this.chkBoxSQD.AutoSize = true;
            this.chkBoxSQD.Checked = true;
            this.chkBoxSQD.CheckState = CheckState.Checked;
            this.chkBoxSQD.Location = new Point(14, 0x37);
            this.chkBoxSQD.Name = "chkBoxSQD";
            this.chkBoxSQD.Size = new Size(0x6c, 0x10);
            this.chkBoxSQD.TabIndex = 90;
            this.chkBoxSQD.Text = "红字发票信息表";
            this.chkBoxSQD.UseVisualStyleBackColor = true;
            this.chkBoxSQD.CheckedChanged += new EventHandler(this.chkBoxSQD_CheckedChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.ClientSize = new Size(0x1c6, 0x138);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.lblOldDBPath);
            base.Controls.Add(this.btn_Close);
            base.Controls.Add(this.btn_Migration);
            base.Controls.Add(this.comboBoxOldTaxcode);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.radioBtnOldDBPath);
            base.Controls.Add(this.radioBtnOldDBSel);
            base.Controls.Add(this.btn_Select_DB);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "TaxcodeChangeForm";
            this.Text = "税号变更";
            base.Load += new EventHandler(this.TaxcodeChangeForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void radioBtnOldDB_Click(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Name == "radioBtnOldDBSel")
            {
                this.radioBtnOldDBPath.Checked = false;
                this.comboBoxOldTaxcode.Enabled = true;
                this.btn_Select_DB.Enabled = false;
            }
            else
            {
                this.radioBtnOldDBSel.Checked = false;
                this.comboBoxOldTaxcode.Enabled = false;
                this.btn_Select_DB.Enabled = true;
            }
        }

        private void TaxcodeChangeForm_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (DirectoryInfo info2 in new DirectoryInfo(Application.StartupPath).Parent.Parent.GetDirectories())
                {
                    if (!info2.FullName.Contains(base.TaxCardInstance.get_TaxCode()) && File.Exists(Path.Combine(info2.FullName, @"Bin\cc3268.dll")))
                    {
                        this.comboBoxOldTaxcode.Items.Add(info2.FullName);
                    }
                }
                if (this.comboBoxOldTaxcode.Items.Count > 0)
                {
                    this.comboBoxOldTaxcode.SelectedIndex = 0;
                    this.radioBtnOldDB_Click(this.radioBtnOldDBSel, new EventArgs());
                }
                else
                {
                    this.radioBtnOldDB_Click(this.radioBtnOldDBPath, new EventArgs());
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

