namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class ExcelForm : BaseForm
    {
        private DataGridViewTextBoxColumn BiXuanShuXin;
        private AisinoBTN btnConfigSave;
        private AisinoBTN btnExit;
        private AisinoBTN btnPreview;
        private AisinoCMB com_sheet_1;
        private AisinoCMB com_sheet_2;
        private AisinoCMB combo_1;
        private AisinoCMB combo_2;
        private AisinoCMB combobox = new AisinoCMB();
        private IContainer components = null;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private FileControl file_1;
        private FileControl file_2;
        private AisinoGRP groupBox1;
        private AisinoGRP groupBox2;
        private AisinoGRP groupBox3;
        private AisinoGRP groupBox4;
        private DataGridViewTextBoxColumn id;
        private InvType invtype;
        private DataGridViewTextBoxColumn key;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoLBL label4;
        private AisinoLBL label5;
        private AisinoLBL label6;
        private AisinoLBL label7;
        private DataGridViewTextBoxColumn MoRen;
        private AisinoPNL panel_file;
        private AisinoPNL panel_Key;
        private AisinoPNL panel_row;
        private AisinoRDO radioButton1;
        private AisinoRDO radioButton2;
        private DataGridViewTextBoxColumn ShuJuXiang;
        private AisinoTXT txt_1;
        private AisinoTXT txt_2;
        private DataGridViewTextBoxColumn WenJianLie;
        private XmlComponentLoader xmlComponentLoader1;

        public ExcelForm(InvType type)
        {
            this.invtype = type;
            this.Initialize();
            this.combobox.Visible = false;
            this.dataGridView1.DataSource = WenBenItem.Items();
            this.file_1.add_onClickEnd(new FileControl.OnClickEnd(this, (IntPtr) this.file_1_onClickEnd));
            this.file_2.add_onClickEnd(new FileControl.OnClickEnd(this, (IntPtr) this.file_2_onClickEnd));
            this.dataGridView1.Controls.Add(this.combobox);
            this.combobox.SelectedIndexChanged += new EventHandler(this.combobox_SelectedIndexChanged);
            this.combobox.Validating += new CancelEventHandler(this.combobox_Validating);
            this.combobox.KeyUp += new KeyEventHandler(this.combobox_KeyUp);
            this.CheckInTableOneAndTwo(this.radioButton2.Checked);
        }

        private void btnConfigSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.DataCheck())
                {
                    if (!File.Exists(IniRead.path))
                    {
                        File.Create(IniRead.path);
                    }
                    IniRead.WritePrivateProfileString("File", "File1Path", this.file_1.get_TextBoxFile().Text);
                    IniRead.WritePrivateProfileString("File", "File2Path", this.file_2.get_TextBoxFile().Text);
                    IniRead.WritePrivateProfileString("File", "TableInFile1", this.com_sheet_1.Text);
                    IniRead.WritePrivateProfileString("File", "TableInFile2", this.com_sheet_2.Text);
                    IniRead.WritePrivateProfileString("FieldCon", "FileNumber", this.radioButton1.Checked ? "1" : "2");
                    foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
                    {
                        string val = Convert.ToString(row.Cells["WenJianLie"].Value);
                        int result = 0;
                        int num2 = 0;
                        if (val == string.Empty)
                        {
                            val = "0.0";
                        }
                        else
                        {
                            if (val.Length <= 2)
                            {
                                MessageManager.ShowMsgBox("INP-271205");
                                return;
                            }
                            if (val[1] != '.')
                            {
                                MessageManager.ShowMsgBox("INP-271205");
                                return;
                            }
                            if (!(int.TryParse(val.Substring(0, val.IndexOf('.')), out result) && int.TryParse(val.Substring(val.LastIndexOf('.') + 1), out num2)))
                            {
                                MessageManager.ShowMsgBox("INP-271205");
                                return;
                            }
                        }
                        IniRead.WritePrivateProfileString("FieldCon", row.Cells["key"].Value.ToString(), val);
                    }
                    IniRead.WritePrivateProfileString("FieldCon", "DefaultFuHeRen", Convert.ToString(this.dataGridView1.Rows[6].Cells["MoRen"].Value));
                    IniRead.WritePrivateProfileString("FieldCon", "DefaultShouKuanRen", Convert.ToString(this.dataGridView1.Rows[7].Cells["MoRen"].Value));
                    IniRead.WritePrivateProfileString("FieldCon", "DefaultShuiLv", Convert.ToString(this.dataGridView1.Rows[0x13].Cells["MoRen"].Value));
                    IniRead.WritePrivateProfileString("FieldCon", "Invtype", this.invtype.ToString());
                    IniRead.WritePrivateProfileString("FieldCon", "IsSeted", "1");
                    IniRead.WritePrivateProfileString("TableCon", "MainTableField", Regex.Match(this.combo_1.Text, @"^\[\d+\]").Value.Trim(new char[] { '[', ']' }));
                    IniRead.WritePrivateProfileString("TableCon", "AssistantTableField", Regex.Match(this.combo_2.Text, @"^\[\d+\]").Value.Trim(new char[] { '[', ']' }));
                    IniRead.WritePrivateProfileString("TableCon", "MainTableIgnoreRow", this.txt_1.Text);
                    IniRead.WritePrivateProfileString("TableCon", "AssistantTableIgnoreRow", this.txt_2.Text);
                    MessageBoxHelper.Show("保存完成", "保存", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    base.Close();
                }
            }
            catch (Exception exception)
            {
                MessageBoxHelper.Show(exception.ToString(), "异常");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(this.file_1.get_TextBoxFile().Text.Trim()))
                {
                    MessageBoxHelper.Show(this.file_1.get_TextBoxFile().Text + "主表路径文件不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (this.radioButton2.Checked && !File.Exists(this.file_2.get_TextBoxFile().Text.Trim()))
                {
                    MessageBoxHelper.Show(this.file_2.get_TextBoxFile().Text + "副表路径文件不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    List<ExcelMappingItem.Relation> yingShe = new List<ExcelMappingItem.Relation>();
                    foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
                    {
                        string str = Convert.ToString(row.Cells["WenJianLie"].Value);
                        if ((str != string.Empty) && ((str.Length > 2) && (str[1] == '.')))
                        {
                            ExcelMappingItem.Relation item = new ExcelMappingItem.Relation {
                                Key = row.Cells["key"].Value.ToString()
                            };
                            int result = 0;
                            if (int.TryParse(str.Substring(0, str.IndexOf('.')), out result))
                            {
                                item.TableFlag = result;
                                result = 0;
                                if (int.TryParse(str.Substring(str.LastIndexOf('.') + 1), out result))
                                {
                                    item.ColumnName = result;
                                    yingShe.Add(item);
                                }
                            }
                        }
                    }
                    string defaultFuHeRen = Convert.ToString(this.dataGridView1.Rows[6].Cells["MoRen"].Value);
                    string defaultShouKuanRen = Convert.ToString(this.dataGridView1.Rows[7].Cells["MoRen"].Value);
                    string defaultShuiLv = Convert.ToString(this.dataGridView1.Rows[0x13].Cells["MoRen"].Value);
                    ResolverExcel excel = new ResolverExcel();
                    if (this.radioButton1.Checked)
                    {
                        this.dataGridView2.DataSource = excel.GetFileData(this.file_1.get_TextBoxFile().Text, this.com_sheet_1.Text, Convert.ToInt16(this.txt_1.Text), yingShe, defaultFuHeRen, defaultShouKuanRen, defaultShuiLv);
                    }
                    else
                    {
                        this.dataGridView2.DataSource = ResolverExcel.GetFileData(this.file_1.get_TextBoxFile().Text, this.file_2.get_TextBoxFile().Text, this.com_sheet_1.Text, this.com_sheet_2.Text, Convert.ToInt16(this.txt_1.Text), Convert.ToInt16(this.txt_2.Text), Convert.ToInt16(Regex.Match(this.combo_1.Text, @"^\[\d+\]").Value.Trim(new char[] { '[', ']' })), Convert.ToInt16(Regex.Match(this.combo_2.Text, @"^\[\d+\]").Value.Trim(new char[] { '[', ']' })), yingShe, defaultFuHeRen, defaultShouKuanRen, defaultShuiLv);
                    }
                }
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void CheckInTableOneAndTwo(bool Checked)
        {
            this.panel_file.Visible = Checked;
            this.panel_Key.Visible = Checked;
            this.panel_row.Visible = Checked;
        }

        private void CheckLastSetType()
        {
            if (File.Exists(IniRead.path))
            {
                string privateProfileString = IniRead.GetPrivateProfileString("FieldCon", "Invtype");
                if ((this.invtype == InvType.Common) || (this.invtype == InvType.Special))
                {
                    string str2 = this.invtype.ToString();
                    if ((privateProfileString.CompareTo("Common") != 0) && (privateProfileString.CompareTo("Special") != 0))
                    {
                        CreateIniFile.Create();
                    }
                }
                else if (this.invtype.ToString().CompareTo(privateProfileString) != 0)
                {
                    CreateIniFile.Create();
                }
            }
        }

        private void combo_1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.combo_1.Items.Clear();
            }
            catch (Exception exception)
            {
                MessageBoxHelper.Show(exception.ToString(), "异常");
            }
        }

        private void combo_2_Click(object sender, EventArgs e)
        {
            try
            {
                this.combo_2.Items.Clear();
            }
            catch (Exception exception)
            {
                MessageBoxHelper.Show(exception.ToString(), "异常");
            }
        }

        private void combobox_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Decimal) && !this.combobox.Text.Contains("."))
            {
                this.combobox.Text = this.combobox.Text + ".";
                this.combobox.Select(this.combobox.Text.Length, 1);
            }
        }

        private void combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.combobox.Text.Trim().StartsWith("----") || (this.combobox.Text.Trim() == string.Empty))
            {
                this.dataGridView1.CurrentCell.Value = string.Empty;
                this.combobox.Visible = false;
            }
            else
            {
                if (this.dataGridView1.CurrentCell.Value == null)
                {
                    this.dataGridView1.CurrentCell.Value = "";
                }
                if (!this.dataGridView1.CurrentCell.Value.Equals(this.combobox.Text.Trim()))
                {
                    this.dataGridView1.CurrentCell.Value = this.combobox.Text;
                    this.combobox.Visible = false;
                }
            }
        }

        private void combobox_Validating(object sender, CancelEventArgs e)
        {
            string input = this.combobox.Text.Trim();
            bool flag = false;
            if (input.Length > 0)
            {
                flag = this.RegexMatch(input);
            }
            else
            {
                flag = true;
            }
            if (flag)
            {
                this.dataGridView1.CurrentCell.Value = input;
                this.combobox.Visible = false;
            }
        }

        private void comboboxBind()
        {
            this.combobox.Items.Clear();
            int result = 0;
            try
            {
                int num2;
                int.TryParse(this.txt_1.Text, out result);
                result--;
                if (result < 0)
                {
                    result = 0;
                }
                DataTable table = new DataTable();
                if (File.Exists(this.file_1.get_TextBoxFile().Text))
                {
                    table = ExcelRead.ExcelToDataTable(this.file_1.get_TextBoxFile().Text, this.com_sheet_1.Text.Trim() + "$", 0);
                    for (num2 = 1; num2 <= table.Columns.Count; num2++)
                    {
                        this.combobox.Items.Add("1." + num2);
                    }
                    this.combobox.Items.Add("----------------------");
                    result = 0;
                    int.TryParse(this.txt_2.Text, out result);
                    result--;
                    if (result < 0)
                    {
                        result = 0;
                    }
                }
                if (this.radioButton2.Checked && File.Exists(this.file_2.get_TextBoxFile().Text))
                {
                    DataTable table2 = ExcelRead.ExcelToDataTable(this.file_2.get_TextBoxFile().Text, this.com_sheet_2.Text.Trim() + "$", 0);
                    for (num2 = 1; num2 <= table2.Columns.Count; num2++)
                    {
                        this.combobox.Items.Add("2." + num2);
                    }
                }
            }
            catch
            {
            }
        }

        private void ComboboxSave()
        {
            XmlRead read = new XmlRead();
            int result = 0;
            try
            {
                int num2;
                read.Delete(FileName.File1);
                int.TryParse(this.txt_1.Text, out result);
                result--;
                if (result < 0)
                {
                    result = 0;
                }
                DataTable table = ExcelRead.ExcelToDataTable(this.file_1.get_TextBoxFile().Text, this.com_sheet_1.Text.Trim() + "$", 0);
                for (num2 = 0; num2 < table.Columns.Count; num2++)
                {
                    int num3 = num2 + 1;
                    read.File1AppendChild("1." + num3.ToString(), table.Rows[Convert.ToInt32(result)][num2].ToString());
                }
                read.Delete(FileName.File2);
                int.TryParse(this.txt_2.Text, out result);
                result--;
                if (result < 0)
                {
                    result = 0;
                }
                if (File.Exists(this.file_2.get_TextBoxFile().Text.Trim()))
                {
                    DataTable table2 = ExcelRead.ExcelToDataTable(this.file_2.get_TextBoxFile().Text, this.com_sheet_2.Text.Trim() + "$", 0);
                    for (num2 = 0; num2 < table2.Columns.Count; num2++)
                    {
                        read.File2AppendChild("2." + ((num2 + 1)).ToString(), table.Rows[Convert.ToInt32(result)][num2].ToString());
                    }
                }
                Regex regex = new Regex(@"^\[\d+\]");
                read.SetKey1(regex.Replace(this.combo_1.Text.Trim(), string.Empty));
                read.SetKey2(regex.Replace(this.combo_2.Text.Trim(), string.Empty));
            }
            catch (Exception exception)
            {
                MessageBoxHelper.Show(exception.ToString(), "异常");
            }
        }

        private bool DataCheck()
        {
            int num2;
            if ((this.invtype == InvType.Common) || (this.invtype == InvType.Special))
            {
                int num = 0;
                foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
                {
                    if (((num < 0x1b) && (Convert.ToString(row.Cells["BiXuanShuXin"].Value) == "必选")) && (string.Empty == Convert.ToString(row.Cells["WenJianLie"].Value)))
                    {
                        this.dataGridView1.CurrentCell = row.Cells["WenJianLie"];
                        MessageBoxHelper.Show(Convert.ToString(row.Cells["ShuJuXiang"].Value) + "为必须项！");
                        return false;
                    }
                    num++;
                }
                string str = Convert.ToString(this.dataGridView1.Rows[0x12].Cells["WenJianLie"].Value);
                string str2 = Convert.ToString(this.dataGridView1.Rows[0x13].Cells["WenJianLie"].Value);
                string str3 = Convert.ToString(this.dataGridView1.Rows[0x16].Cells["WenJianLie"].Value);
                num2 = 0;
                if (str != string.Empty)
                {
                    num2++;
                }
                if (str2 != string.Empty)
                {
                    num2++;
                }
                if (str3 != string.Empty)
                {
                    num2++;
                }
                if (num2 < 2)
                {
                    MessageBoxHelper.Show("金额、税率、税额三项必须要有两项! ");
                    if (str3 == string.Empty)
                    {
                        this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0x16].Cells["WenJianLie"];
                    }
                    if (str2 == string.Empty)
                    {
                        this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0x13].Cells["WenJianLie"];
                    }
                    if (str == string.Empty)
                    {
                        this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0x12].Cells["WenJianLie"];
                    }
                    return false;
                }
            }
            else if (this.invtype == InvType.transportation)
            {
                num2 = 0;
                foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
                {
                    if ((((num2 == 0) || ((num2 >= 0x1b) && (num2 < 0x2c))) && (Convert.ToString(row.Cells["BiXuanShuXin"].Value) == "必选")) && (string.Empty == Convert.ToString(row.Cells["WenJianLie"].Value)))
                    {
                        this.dataGridView1.CurrentCell = row.Cells["WenJianLie"];
                        MessageBoxHelper.Show(Convert.ToString(row.Cells["ShuJuXiang"].Value) + "为必须项！");
                        return false;
                    }
                    num2++;
                }
            }
            else if (this.invtype == InvType.vehiclesales)
            {
                num2 = 0;
                foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
                {
                    if ((((num2 == 0) || (num2 >= 0x2c)) && (Convert.ToString(row.Cells["BiXuanShuXin"].Value) == "必选")) && (string.Empty == Convert.ToString(row.Cells["WenJianLie"].Value)))
                    {
                        this.dataGridView1.CurrentCell = row.Cells["WenJianLie"];
                        MessageBoxHelper.Show(Convert.ToString(row.Cells["ShuJuXiang"].Value) + "为必须项！");
                        return false;
                    }
                    num2++;
                }
            }
            return true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.CurrentCell.OwningColumn.Name == "WenJianLie")
            {
                Rectangle rectangle = this.dataGridView1.GetCellDisplayRectangle(this.dataGridView1.CurrentCell.ColumnIndex, this.dataGridView1.CurrentCell.RowIndex, false);
                this.combobox.Left = rectangle.Left;
                this.combobox.Top = rectangle.Top;
                this.combobox.Width = rectangle.Width;
                this.combobox.Height = rectangle.Height;
                this.combobox.Visible = true;
                this.combobox.Text = Convert.ToString(this.dataGridView1.CurrentCell.Value);
            }
            else
            {
                this.combobox.Visible = false;
            }
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.combobox.Visible = false;
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            this.combobox.Visible = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void ExcelForm_Load(object sender, EventArgs e)
        {
            this.ExcelFormShow();
            this.CheckLastSetType();
            this.MoRenStyle();
            this.YuLanColumnsBind();
            this.LoadFile();
            this.comboboxBind();
            this.radioButton2.Visible = false;
        }

        private void ExcelFormPreviewShow()
        {
            int num;
            if ((this.invtype == InvType.Common) || (this.invtype == InvType.Special))
            {
                for (num = 1; num < 0x42; num++)
                {
                    if (num < 0x1b)
                    {
                        this.dataGridView2.Columns[num].Visible = true;
                    }
                    else
                    {
                        this.dataGridView2.Columns[num].Visible = false;
                    }
                }
            }
            else if (this.invtype == InvType.transportation)
            {
                for (num = 1; num < 0x42; num++)
                {
                    if (num < 0x1b)
                    {
                        this.dataGridView2.Columns[num].Visible = false;
                    }
                    else if (num < 0x2c)
                    {
                        this.dataGridView2.Columns[num].Visible = true;
                    }
                    else
                    {
                        this.dataGridView1.Rows[num].Visible = false;
                    }
                }
            }
            else if (this.invtype == InvType.vehiclesales)
            {
                for (num = 1; num < 0x42; num++)
                {
                    if (num < 0x2c)
                    {
                        this.dataGridView2.Columns[num].Visible = false;
                    }
                    else
                    {
                        this.dataGridView2.Columns[num].Visible = true;
                    }
                }
            }
        }

        private void ExcelFormShow()
        {
            int num;
            this.dataGridView1.DataSource = WenBenItem.Items();
            if ((this.invtype == InvType.Common) || (this.invtype == InvType.Special))
            {
                for (num = 1; num < 0x42; num++)
                {
                    if (num < 0x1b)
                    {
                        this.dataGridView1.Rows[num].Visible = true;
                    }
                    else
                    {
                        this.dataGridView1.Rows[num].Visible = false;
                    }
                }
            }
            else if (this.invtype == InvType.transportation)
            {
                for (num = 1; num < 0x42; num++)
                {
                    if (num < 0x1b)
                    {
                        this.dataGridView1.Rows[num].Visible = false;
                    }
                    else if (num < 0x2c)
                    {
                        this.dataGridView1.Rows[num].Visible = true;
                    }
                    else
                    {
                        this.dataGridView1.Rows[num].Visible = false;
                    }
                }
            }
            else if (this.invtype == InvType.vehiclesales)
            {
                for (num = 1; num < 0x42; num++)
                {
                    if (num < 0x2c)
                    {
                        this.dataGridView1.Rows[num].Visible = false;
                    }
                    else
                    {
                        this.dataGridView1.Rows[num].Visible = true;
                    }
                }
            }
        }

        private void ExcelSheetBindComBoBoxItems(AisinoCMB bobox, string path)
        {
            try
            {
                bobox.Items.Clear();
                if (!File.Exists(path))
                {
                    MessageBoxHelper.Show(path + "文件不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DataTable excelTableNames = ExcelRead.GetExcelTableNames(path);
                    bobox.Text = excelTableNames.Rows[0][2].ToString().Trim(new char[] { '$' });
                    for (int i = 0; i < excelTableNames.Rows.Count; i++)
                    {
                        string str = excelTableNames.Rows[i][2].ToString().Trim();
                        int num2 = 0;
                        if (str[str.Length - 1] != '$')
                        {
                            num2 = 1;
                        }
                        for (int j = 0; j < bobox.Items.Count; j++)
                        {
                            if (bobox.Items[j].ToString() == str)
                            {
                                num2 = 1;
                            }
                        }
                        if (num2 == 0)
                        {
                            bobox.Items.Add(str.Trim(new char[] { '$' }));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBoxHelper.Show(exception.ToString(), "异常");
            }
        }

        private void file_1_onClickEnd(object sender, EventArgs e)
        {
            try
            {
                this.ExcelSheetBindComBoBoxItems(this.com_sheet_1, this.file_1.get_TextBoxFile().Text);
                this.comboboxBind();
            }
            catch (Exception exception)
            {
                MessageBoxHelper.Show(exception.ToString(), "异常");
            }
        }

        private void file_2_onClickEnd(object sender, EventArgs e)
        {
            try
            {
                this.ExcelSheetBindComBoBoxItems(this.com_sheet_2, this.file_2.get_TextBoxFile().Text);
                this.comboboxBind();
            }
            catch (Exception exception)
            {
                MessageBoxHelper.Show(exception.ToString(), "异常");
            }
        }

        private string GetDictValue(string key, Dictionary<string, string> dict)
        {
            if (dict.ContainsKey(key))
            {
                return ("[" + key + "]" + dict[key]);
            }
            return string.Empty;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.btnExit = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnExit");
            this.btnConfigSave = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnConfigSave");
            this.groupBox4 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox4");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.file_1 = this.xmlComponentLoader1.GetControlByName<FileControl>("file_1");
            this.com_sheet_1 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("com_sheet_1");
            this.panel_file = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel_file");
            this.file_2 = this.xmlComponentLoader1.GetControlByName<FileControl>("file_2");
            this.label3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label3");
            this.com_sheet_2 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("com_sheet_2");
            this.btnPreview = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnPreview");
            this.dataGridView2 = this.xmlComponentLoader1.GetControlByName<DataGridView>("dataGridView2");
            this.groupBox3 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox3");
            this.label4 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label4");
            this.txt_1 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_1");
            this.panel_row = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel_row");
            this.label5 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label5");
            this.txt_2 = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_2");
            this.groupBox2 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox2");
            this.radioButton1 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("radioButton1");
            this.radioButton2 = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("radioButton2");
            this.panel_Key = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel_Key");
            this.label7 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label7");
            this.combo_2 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("combo_2");
            this.label6 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label6");
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.dataGridView1 = this.xmlComponentLoader1.GetControlByName<DataGridView>("dataGridView1");
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.combo_1 = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("combo_1");
            this.id = new DataGridViewTextBoxColumn();
            this.key = new DataGridViewTextBoxColumn();
            this.ShuJuXiang = new DataGridViewTextBoxColumn();
            this.BiXuanShuXin = new DataGridViewTextBoxColumn();
            this.WenJianLie = new DataGridViewTextBoxColumn();
            this.MoRen = new DataGridViewTextBoxColumn();
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "编号";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            this.id.Width = 0x7d;
            this.key.DataPropertyName = "key";
            this.key.HeaderText = "key";
            this.key.Name = "key";
            this.key.Visible = false;
            this.ShuJuXiang.DataPropertyName = "ShuJuXiang";
            this.ShuJuXiang.HeaderText = "数据项";
            this.ShuJuXiang.Name = "ShuJuXiang";
            this.ShuJuXiang.ReadOnly = true;
            this.ShuJuXiang.Width = 0x7d;
            this.BiXuanShuXin.DataPropertyName = "BiXuanShuXin";
            this.BiXuanShuXin.HeaderText = "必选属性";
            this.BiXuanShuXin.Name = "BiXuanShuXin";
            this.BiXuanShuXin.ReadOnly = true;
            this.BiXuanShuXin.Width = 70;
            this.WenJianLie.DataPropertyName = "WenJianLie";
            this.WenJianLie.HeaderText = "数据项对应Excel文件的列";
            this.WenJianLie.Name = "WenJianLie";
            this.WenJianLie.ReadOnly = true;
            this.WenJianLie.Width = 150;
            this.MoRen.DataPropertyName = "MoRen";
            this.MoRen.HeaderText = "默认值";
            this.MoRen.Name = "MoRen";
            this.MoRen.Width = 60;
            this.com_sheet_1.Visible = false;
            this.com_sheet_2.Visible = false;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToAddRows = false;
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.btnConfigSave.Click += new EventHandler(this.btnConfigSave_Click);
            this.btnPreview.Click += new EventHandler(this.btnPreview_Click);
            this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
            this.radioButton2.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
            this.combo_2.Click += new EventHandler(this.combo_2_Click);
            this.dataGridView1.Scroll += new ScrollEventHandler(this.dataGridView1_Scroll);
            this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.dataGridView1_ColumnWidthChanged);
            this.combo_1.MouseClick += new MouseEventHandler(this.combo_1_MouseClick);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ExcelForm));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x2e7, 0x1fc);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "导入Excel配置";
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.ExcelForm\Aisino.Fwkp.Wbjk.ExcelForm.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2e7, 0x1fc);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "ExcelForm";
            this.Text = "导入Excel配置";
            base.Load += new EventHandler(this.ExcelForm_Load);
            base.ResumeLayout(false);
        }

        private void LoadFile()
        {
            try
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                XmlRead read = new XmlRead();
                Dictionary<string, string> item = read.GetItem(FileName.File1);
                foreach (KeyValuePair<string, string> pair in item)
                {
                    this.combobox.Items.Add("[" + pair.Key + "]" + pair.Value);
                    dictionary.Add(pair.Key, pair.Value);
                }
                this.combobox.Items.Add("----------------------");
                Dictionary<string, string> dictionary3 = read.GetItem(FileName.File2);
                foreach (KeyValuePair<string, string> pair in dictionary3)
                {
                    this.combobox.Items.Add("[" + pair.Key + "]" + pair.Value);
                    dictionary.Add(pair.Key, pair.Value);
                }
                this.file_1.set_FileFilter("Excel文件(*.xls)|*.xls");
                this.file_2.set_FileFilter("Excel文件(*.xls)|*.xls");
                this.file_1.get_TextBoxFile().Text = IniRead.GetPrivateProfileString("File", "File1Path");
                this.file_2.get_TextBoxFile().Text = IniRead.GetPrivateProfileString("File", "File2Path");
                this.com_sheet_1.Text = IniRead.GetPrivateProfileString("File", "TableInFile1");
                this.com_sheet_2.Text = IniRead.GetPrivateProfileString("File", "TableInFile2");
                string privateProfileString = IniRead.GetPrivateProfileString("FieldCon", "FileNumber");
                int result = 1;
                int.TryParse(privateProfileString, out result);
                this.radioButton1.Checked = result == 1;
                this.radioButton2.Checked = result == 2;
                DataGridView view = this.dataGridView1;
                DataTable table = WenBenItem.Items();
                foreach (DataRow row in table.Rows)
                {
                    string str2 = IniRead.GetPrivateProfileString("FieldCon", row["key"].ToString());
                    if (str2 != "0.0")
                    {
                        view.Rows[Convert.ToInt32(row["id"]) - 1].Cells["WenJianLie"].Value = str2;
                    }
                }
                view.Rows[6].Cells["MoRen"].Value = IniRead.GetPrivateProfileString("FieldCon", "DefaultFuHeRen");
                view.Rows[7].Cells["MoRen"].Value = IniRead.GetPrivateProfileString("FieldCon", "DefaultShouKuanRen");
                view.Rows[0x13].Cells["MoRen"].Value = IniRead.GetPrivateProfileString("FieldCon", "DefaultShuiLv");
                this.combo_1.Text = "[" + IniRead.GetPrivateProfileString("TableCon", "MainTableField") + "]" + read.GetKey1();
                this.combo_2.Text = "[" + IniRead.GetPrivateProfileString("TableCon", "AssistantTableField") + "]" + read.GetKey2();
                this.txt_1.Text = IniRead.GetPrivateProfileString("TableCon", "MainTableIgnoreRow");
                this.txt_2.Text = IniRead.GetPrivateProfileString("TableCon", "AssistantTableIgnoreRow");
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void MoRenStyle()
        {
            try
            {
                foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
                {
                    row.Cells["ShuJuXiang"].ReadOnly = true;
                    row.Cells["ShuJuXiang"].Style.BackColor = Color.LightCyan;
                    row.Cells["ShuJuXiang"].Style.ForeColor = Color.Black;
                    row.Cells["BiXuanShuXin"].ReadOnly = true;
                    row.Cells["BiXuanShuXin"].Style.BackColor = Color.LightCyan;
                    if (row.Cells["BiXuanShuXin"].Value.Equals("非必选"))
                    {
                        row.Cells["BiXuanShuXin"].Style.ForeColor = Color.Black;
                    }
                    row.Cells["MoRen"].ReadOnly = true;
                    row.Cells["MoRen"].Style.BackColor = Color.LightCyan;
                }
                this.dataGridView1.Rows[6].Cells["MoRen"].ReadOnly = false;
                this.dataGridView1.Rows[6].Cells["MoRen"].Style.BackColor = Color.White;
                this.dataGridView1.Rows[7].Cells["MoRen"].ReadOnly = false;
                this.dataGridView1.Rows[7].Cells["MoRen"].Style.BackColor = Color.White;
                this.dataGridView1.Rows[0x13].Cells["MoRen"].ReadOnly = false;
                this.dataGridView1.Rows[0x13].Cells["MoRen"].Style.BackColor = Color.White;
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.CheckInTableOneAndTwo(this.radioButton2.Checked);
            if (this.file_1.get_TextBoxFile().Text.Trim().Length > 0)
            {
                this.ExcelSheetBindComBoBoxItems(this.com_sheet_1, this.file_1.get_TextBoxFile().Text);
                this.comboboxBind();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.CheckInTableOneAndTwo(this.radioButton2.Checked);
            if ((this.file_1.get_TextBoxFile().Text.Trim().Length > 0) && (this.file_2.get_TextBoxFile().Text.Trim().Length > 0))
            {
                this.ExcelSheetBindComBoBoxItems(this.com_sheet_1, this.file_1.get_TextBoxFile().Text);
                this.ExcelSheetBindComBoBoxItems(this.com_sheet_2, this.file_2.get_TextBoxFile().Text);
                this.comboboxBind();
            }
        }

        private void ReadExcelTableHead(AisinoCMB combox, string path, string sheetName, int HeadRow)
        {
            try
            {
                HeadRow--;
                if (HeadRow < 0)
                {
                    HeadRow = 0;
                }
                combox.Items.Clear();
                DataTable table = ExcelRead.ExcelToDataTable(path, sheetName + "$", 0);
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    int num2 = i + 1;
                    combox.Items.Add("[" + num2.ToString() + "]" + table.Rows[HeadRow][i].ToString());
                }
            }
            catch (Exception exception)
            {
                MessageBoxHelper.Show(exception.ToString(), "异常");
            }
        }

        private bool RegexMatch(object Input)
        {
            bool flag = false;
            try
            {
                if (Input != null)
                {
                    flag = Regex.IsMatch(Input.ToString(), @"^[1-2]\.\d+$");
                }
                return flag;
            }
            catch (Exception)
            {
                return flag;
            }
        }

        private void YuLanColumnsBind()
        {
            try
            {
                DataTable table = WenBenItem.Items();
                foreach (DataRow row in table.Rows)
                {
                    DataGridViewColumn dataGridViewColumn = new DataGridViewTextBoxColumn {
                        Name = row["key"].ToString(),
                        HeaderText = row["ShuJuXiang"].ToString(),
                        DataPropertyName = row["key"].ToString()
                    };
                    this.dataGridView2.Columns.Add(dataGridViewColumn);
                }
                this.ExcelFormPreviewShow();
            }
            catch (Exception exception)
            {
                HandleException.HandleError(exception);
            }
        }
    }
}

