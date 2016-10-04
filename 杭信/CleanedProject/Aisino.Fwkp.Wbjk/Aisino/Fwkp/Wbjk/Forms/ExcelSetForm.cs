namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class ExcelSetForm : BaseForm
    {
        private AisinoBTN btnAddBlankColumn_Main;
        private AisinoBTN btnAddBlankColumn_Vice;
        private AisinoBTN btnClearYS;
        private AisinoBTN btnConfigSave;
        private AisinoBTN btnloadColName;
        private AisinoBTN btnPreview;
        private AisinoBTN button1;
        private AisinoBTN button2;
        private Dictionary<string, string> ColDic = new Dictionary<string, string>();
        private List<AisinoLBL> ColNamelist = new List<AisinoLBL>();
        private AisinoCMB com_sheet_1;
        private AisinoCMB com_sheet_2;
        private IContainer components = null;
        private int dgvCellMouseDownColIndex = 0;
        private DataGridView dgvMainTable;
        private CustomStyleDataGrid dgvPreView;
        private DataGridView dgvViceTable;
        private int DJHMColMain = 0;
        private int DJHMColSub = 0;
        private FileControl file_1;
        private FileControl file_2;
        private AisinoGRP groupBox5;
        private AisinoGRP groupBoxColNames;
        private InvType invtype;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoLBL label4;
        private Label label5;
        private AisinoLBL label8;
        private AisinoLBL labExplain;
        private AisinoLBL labFHR;
        private AisinoLBL labSKR;
        private AisinoLBL labSLV;
        private AisinoNUD numUpDownMainHeadLine;
        private AisinoNUD numUpDownSubHeadLine;
        private AisinoRDO rdbtnDoubletb;
        private AisinoRDO rdbtnSingletb;
        private SplitContainer splitContainer1;
        private ToolTip toolTip1;
        private AisinoTXT txtFHR;
        private AisinoTXT txtSKR;
        private AisinoTXT txtSLV;

        public ExcelSetForm(InvType type)
        {
            this.invtype = type;
            this.InitializeComponent();
            this.txtFHR.Validating += new CancelEventHandler(this.txtFHR_Validating);
            this.txtSKR.Validating += new CancelEventHandler(this.txtSKR_Validating);
            this.txtSLV.Validated += new EventHandler(this.txtSLV_TextChanged);
            this.file_1.add_onClickEnd(new FileControl.OnClickEnd(this, (IntPtr) this.file_1_onClickEnd));
            this.file_2.add_onClickEnd(new FileControl.OnClickEnd(this, (IntPtr) this.file_2_onClickEnd));
            this.dgvMainTable.DragDrop += new DragEventHandler(this.dgvTable_DragDrop);
            this.dgvMainTable.DragEnter += new DragEventHandler(this.dgvTable_DragEnter);
            this.dgvMainTable.CellMouseMove += new DataGridViewCellMouseEventHandler(this.dgvTable_CellMouseMove);
            this.dgvViceTable.DragDrop += new DragEventHandler(this.dgvTable_DragDrop);
            this.dgvViceTable.DragEnter += new DragEventHandler(this.dgvTable_DragEnter);
            this.dgvViceTable.CellMouseMove += new DataGridViewCellMouseEventHandler(this.dgvTable_CellMouseMove);
            this.CheckInTableOneAndTwo(this.rdbtnDoubletb.Checked);
            if (type == InvType.vehiclesales)
            {
                this.groupBoxColNames.Width = 0x39b;
            }
            this.com_sheet_1.Visible = false;
            this.com_sheet_2.Visible = false;
        }

        private bool AddBlankColumns(DataGridView dgvTable, int count)
        {
            try
            {
                if (dgvTable.DataSource == null)
                {
                    return false;
                }
                using (DataTable table = (DataTable) dgvTable.DataSource)
                {
                    int num3;
                    int num = table.Columns.Count;
                    int num2 = table.Columns.Count + count;
                    for (num3 = num; num3 < num2; num3++)
                    {
                        DataColumn column = new DataColumn(num3.ToString(), System.Type.GetType("System.String"));
                        table.Columns.Add(column);
                    }
                    dgvTable.DataSource = table;
                    for (num3 = num; num3 < num2; num3++)
                    {
                        dgvTable.Columns[num3].HeaderText = string.Format("--- 列{0} ---", num3 + 1);
                        dgvTable.Columns[num3].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("增加新列错误！\n详细原因：" + exception.Message);
                return false;
            }
        }

        private void btnAddBlankColumn_Main_Click(object sender, EventArgs e)
        {
            if (this.AddBlankColumns(this.dgvMainTable, 1))
            {
                this.dgvMainTable.FirstDisplayedScrollingColumnIndex = this.dgvMainTable.Columns.Count - 1;
            }
        }

        private void btnAddBlankColumn_Vice_Click(object sender, EventArgs e)
        {
            if (this.AddBlankColumns(this.dgvViceTable, 1))
            {
                this.dgvViceTable.FirstDisplayedScrollingColumnIndex = this.dgvViceTable.Columns.Count - 1;
            }
        }

        private void btnClearYS_Click(object sender, EventArgs e)
        {
            int num;
            for (num = 0; num < this.dgvMainTable.ColumnCount; num++)
            {
                this.SetColumnHead(this.dgvMainTable, num, string.Format("--- 列{0} ---", num + 1));
            }
            for (num = 0; num < this.dgvViceTable.ColumnCount; num++)
            {
                this.SetColumnHead(this.dgvViceTable, num, string.Format("--- 列{0} ---", num + 1));
            }
            foreach (Control control in this.groupBoxColNames.Controls)
            {
                control.Visible = true;
            }
        }

        private void btnConfigSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.DataCheck())
                {
                    int num;
                    string headerText;
                    string str2;
                    string str3;
                    if (this.invtype == InvType.Common)
                    {
                        if (!File.Exists(IniRead.path))
                        {
                            File.Create(IniRead.path);
                        }
                        IniRead.type = "c";
                    }
                    else if (this.invtype == InvType.Special)
                    {
                        if (!File.Exists(IniRead.path))
                        {
                            File.Create(IniRead.path);
                        }
                        IniRead.type = "s";
                    }
                    else if (this.invtype == InvType.transportation)
                    {
                        if (!File.Exists(IniRead.path_2))
                        {
                            File.Create(IniRead.path_2);
                        }
                        IniRead.type = "f";
                    }
                    else if (this.invtype == InvType.vehiclesales)
                    {
                        if (!File.Exists(IniRead.path_3))
                        {
                            File.Create(IniRead.path_3);
                        }
                        IniRead.type = "j";
                    }
                    IniRead.WritePrivateProfileString("File", "File1Path", this.file_1.get_TextBoxFile().Text);
                    IniRead.WritePrivateProfileString("File", "File2Path", this.file_2.get_TextBoxFile().Text);
                    IniRead.WritePrivateProfileString("File", "TableInFile1", this.com_sheet_1.Text);
                    IniRead.WritePrivateProfileString("File", "TableInFile2", this.com_sheet_2.Text);
                    IniRead.WritePrivateProfileString("FieldCon", "FileNumber", this.rdbtnSingletb.Checked ? "1" : "2");
                    foreach (KeyValuePair<string, string> pair in this.ColDic)
                    {
                        IniRead.WritePrivateProfileString("FieldCon", pair.Value, "0.0");
                    }
                    for (num = 0; num < this.dgvMainTable.ColumnCount; num++)
                    {
                        headerText = this.dgvMainTable.Columns[num].HeaderText;
                        if (!headerText.StartsWith("---") && this.ColDic.ContainsKey(headerText))
                        {
                            str2 = this.ColDic[headerText];
                            if (this.rdbtnDoubletb.Checked && (str2 == "DanJuHaoMa"))
                            {
                                int num2 = num + 1;
                                IniRead.WritePrivateProfileString("TableCon", "MainTableField", num2.ToString());
                            }
                            str3 = string.Format("1.{0}", num + 1);
                            IniRead.WritePrivateProfileString("FieldCon", str2, str3);
                        }
                    }
                    if (this.rdbtnDoubletb.Checked)
                    {
                        for (num = 0; num < this.dgvViceTable.ColumnCount; num++)
                        {
                            headerText = this.dgvViceTable.Columns[num].HeaderText;
                            if (!headerText.StartsWith("---") && this.ColDic.ContainsKey(headerText))
                            {
                                str2 = this.ColDic[headerText];
                                if (str2 == "DanJuHaoMa")
                                {
                                    IniRead.WritePrivateProfileString("TableCon", "AssistantTableField", (num + 1).ToString());
                                }
                                else
                                {
                                    str3 = string.Format("2.{0}", num + 1);
                                    IniRead.WritePrivateProfileString("FieldCon", str2, str3);
                                }
                            }
                        }
                    }
                    IniRead.WritePrivateProfileString("FieldCon", "DefaultFuHeRen", this.txtFHR.Text.Trim());
                    IniRead.WritePrivateProfileString("FieldCon", "DefaultShouKuanRen", this.txtSKR.Text.Trim());
                    IniRead.WritePrivateProfileString("FieldCon", "DefaultShuiLv", this.txtSLV.Text.Trim());
                    IniRead.WritePrivateProfileString("FieldCon", "Invtype", this.invtype.ToString());
                    IniRead.WritePrivateProfileString("FieldCon", "IsSeted", "1");
                    string text = this.numUpDownMainHeadLine.Text;
                    if (text.CompareTo("0") == 0)
                    {
                    }
                    IniRead.WritePrivateProfileString("TableCon", "MainTableIgnoreRow", text);
                    IniRead.WritePrivateProfileString("TableCon", "AssistantTableIgnoreRow", this.numUpDownSubHeadLine.Text);
                    MessageBoxHelper.Show("保存完成", "保存", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        private void btnloadColName_Click(object sender, EventArgs e)
        {
            if ((this.rdbtnSingletb.Checked && (this.dgvMainTable.DataSource == null)) || (this.rdbtnDoubletb.Checked && ((this.dgvMainTable.DataSource == null) || (this.dgvViceTable.DataSource == null))))
            {
                MessageManager.ShowMsgBox("请先加载Excel文件！");
            }
            else
            {
                string str3;
                int num;
                if (this.invtype == InvType.Common)
                {
                    IniRead.type = "c";
                }
                else if (this.invtype == InvType.Special)
                {
                    IniRead.type = "s";
                }
                else if (this.invtype == InvType.transportation)
                {
                    IniRead.type = "f";
                }
                else if (this.invtype == InvType.vehiclesales)
                {
                    IniRead.type = "j";
                }
                DataTable table = WenBenItem.Items();
                foreach (DataRow row in table.Rows)
                {
                    string key = row["key"].ToString();
                    string privateProfileString = IniRead.GetPrivateProfileString("FieldCon", key);
                    str3 = row["ShuJuXiang"].ToString();
                    if (privateProfileString != "0.0")
                    {
                        string[] strArray = privateProfileString.Split(new char[] { '.' });
                        if (strArray.Length == 2)
                        {
                            num = Convert.ToInt32(strArray[1]);
                            if (strArray[0] == "1")
                            {
                                if (num > this.dgvMainTable.Columns.Count)
                                {
                                    this.AddBlankColumns(this.dgvMainTable, num - this.dgvMainTable.Columns.Count);
                                }
                                num--;
                                this.SetColumnHead(this.dgvMainTable, num, str3);
                                this.GroupBoxVisible(str3, false);
                            }
                            else if (strArray[0] == "2")
                            {
                                if (num > this.dgvViceTable.Columns.Count)
                                {
                                    this.AddBlankColumns(this.dgvViceTable, num - this.dgvViceTable.Columns.Count);
                                }
                                num--;
                                this.SetColumnHead(this.dgvViceTable, num, str3);
                                this.GroupBoxVisible(str3, false);
                            }
                        }
                    }
                }
                if (this.rdbtnDoubletb.Checked)
                {
                    num = Convert.ToInt32(IniRead.GetPrivateProfileString("TableCon", "AssistantTableField")) - 1;
                    str3 = "单据号码";
                    this.SetColumnHead(this.dgvViceTable, num, str3);
                    this.GroupBoxVisible(str3, false);
                }
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.DataCheck())
                {
                    if (!File.Exists(this.file_1.get_TextBoxFile().Text.Trim()))
                    {
                        MessageBoxHelper.Show(this.file_1.get_TextBoxFile().Text + "主表路径文件不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (this.rdbtnDoubletb.Checked && !File.Exists(this.file_2.get_TextBoxFile().Text.Trim()))
                    {
                        MessageBoxHelper.Show(this.file_2.get_TextBoxFile().Text + "副表路径文件不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        int num;
                        string headerText;
                        string str2;
                        ExcelMappingItem.Relation relation;
                        List<ExcelMappingItem.Relation> yingShe = new List<ExcelMappingItem.Relation>();
                        for (num = 0; num < this.dgvMainTable.ColumnCount; num++)
                        {
                            headerText = this.dgvMainTable.Columns[num].HeaderText;
                            if (!headerText.StartsWith("---") && this.ColDic.ContainsKey(headerText))
                            {
                                str2 = this.ColDic[headerText];
                                relation = new ExcelMappingItem.Relation {
                                    Key = str2,
                                    TableFlag = 1,
                                    ColumnName = num + 1
                                };
                                yingShe.Add(relation);
                            }
                        }
                        if (this.rdbtnDoubletb.Checked)
                        {
                            for (num = 0; num < this.dgvViceTable.ColumnCount; num++)
                            {
                                headerText = this.dgvViceTable.Columns[num].HeaderText;
                                if (!headerText.StartsWith("---") && this.ColDic.ContainsKey(headerText))
                                {
                                    str2 = this.ColDic[headerText];
                                    relation = new ExcelMappingItem.Relation {
                                        Key = str2,
                                        TableFlag = 2,
                                        ColumnName = num + 1
                                    };
                                    yingShe.Add(relation);
                                }
                            }
                        }
                        string defaultFuHeRen = this.txtFHR.Text.Trim();
                        string defaultShouKuanRen = this.txtSKR.Text.Trim();
                        string defaultShuiLv = this.txtSLV.Text.Trim();
                        ResolverExcel excel = new ResolverExcel();
                        if ((this.numUpDownMainHeadLine.Text.CompareTo("-") == 0) || (this.numUpDownSubHeadLine.Text.CompareTo("-") == 0))
                        {
                            MessageBoxHelper.Show("表头行数输入格式非法", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            string str7;
                            string text = this.numUpDownMainHeadLine.Text;
                            if (this.rdbtnSingletb.Checked)
                            {
                                str7 = this.com_sheet_1.Text;
                                this.dgvPreView.DataSource = excel.GetFileData(this.file_1.get_TextBoxFile().Text, str7, Convert.ToInt16(text), yingShe, defaultFuHeRen, defaultShouKuanRen, defaultShuiLv);
                            }
                            else
                            {
                                str7 = this.com_sheet_1.Text;
                                string str8 = this.com_sheet_2.Text;
                                this.dgvPreView.DataSource = ResolverExcel.GetFileData(this.file_1.get_TextBoxFile().Text, this.file_2.get_TextBoxFile().Text, str7, str8, Convert.ToInt16(text), Convert.ToInt16(this.numUpDownSubHeadLine.Text), this.DJHMColMain, this.DJHMColSub, yingShe, defaultFuHeRen, defaultShouKuanRen, defaultShuiLv);
                            }
                            if ((this.invtype == InvType.Common) || (this.invtype == InvType.Special))
                            {
                                TaxCard card = TaxCardFactory.CreateTaxCard();
                                bool iSNCPXS = card.get_QYLX().ISNCPXS;
                                bool iSNCPSG = card.get_QYLX().ISNCPSG;
                                for (num = 1; num < 0x42; num++)
                                {
                                    if (num < 0x1b)
                                    {
                                        this.dgvPreView.Columns[num].Visible = true;
                                    }
                                    else if (num >= 0x42)
                                    {
                                        if (iSNCPXS || iSNCPSG)
                                        {
                                            this.dgvPreView.Columns[num].Visible = true;
                                        }
                                        else
                                        {
                                            this.dgvPreView.Columns[num].Visible = false;
                                        }
                                    }
                                    else
                                    {
                                        this.dgvPreView.Columns[num].Visible = false;
                                    }
                                }
                            }
                            else if (this.invtype == InvType.transportation)
                            {
                                for (num = 1; num < 0x43; num++)
                                {
                                    if (num < 0x1b)
                                    {
                                        this.dgvPreView.Columns[num].Visible = false;
                                    }
                                    else
                                    {
                                        if (num < 0x2c)
                                        {
                                            this.dgvPreView.Columns[num].Visible = true;
                                        }
                                        else
                                        {
                                            this.dgvPreView.Columns[num].Visible = false;
                                        }
                                        if (num == 0x42)
                                        {
                                            this.dgvPreView.Columns[num].Visible = false;
                                        }
                                    }
                                }
                            }
                            else if (this.invtype == InvType.vehiclesales)
                            {
                                for (num = 1; num < 0x43; num++)
                                {
                                    if ((num < 0x2c) || (num >= 0x42))
                                    {
                                        this.dgvPreView.Columns[num].Visible = false;
                                    }
                                    else
                                    {
                                        this.dgvPreView.Columns[num].Visible = true;
                                    }
                                    if (num == 0x42)
                                    {
                                        this.dgvPreView.Columns[num].Visible = false;
                                    }
                                }
                            }
                            new ExcelSetPreviewForm(this.dgvPreView).ShowDialog();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                if (exception.Message.ToString().Equals("表头行数过大，请重新设置Excel文件或减少表头行数"))
                {
                    MessageManager.ShowMsgBox(exception.Message);
                }
                else if (exception.Message.ToString().Equals("当前Excel文档正在被打开，请先关闭"))
                {
                    MessageManager.ShowMsgBox(exception.Message);
                }
                else
                {
                    MessageBoxHelper.Show(exception.Message, "异常");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string path = this.file_1.get_TextBoxFile().Text.Trim();
                if (!File.Exists(path))
                {
                    MessageBoxHelper.Show(this.file_1.get_TextBoxFile().Text + "主表路径文件不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (this.numUpDownMainHeadLine.Text.CompareTo("-") == 0)
                {
                    MessageBoxHelper.Show("表头行数输入格式非法", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    int num2;
                    string text = this.com_sheet_1.Text;
                    if (this.numUpDownMainHeadLine.Text.Trim().Length == 0)
                    {
                        this.numUpDownMainHeadLine.Text = "0";
                    }
                    DataTable table = ExcelRead.ExcelToDataTable(Convert.ToInt32(this.numUpDownMainHeadLine.Text), path, text + "$");
                    List<string> list = new List<string>();
                    if (this.dgvMainTable.Columns.Count > 0)
                    {
                        for (num2 = 0; num2 < this.dgvMainTable.Columns.Count; num2++)
                        {
                            list.Add(this.dgvMainTable.Columns[num2].HeaderText);
                        }
                    }
                    this.dgvMainTable.DataSource = table;
                    for (num2 = 0; num2 < table.Columns.Count; num2++)
                    {
                        if (num2 < list.Count)
                        {
                            this.dgvMainTable.Columns[num2].HeaderText = list[num2];
                        }
                        else
                        {
                            this.dgvMainTable.Columns[num2].HeaderText = string.Format("--- 列{0} ---", num2 + 1);
                        }
                        this.dgvMainTable.Columns[num2].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            catch (CustomException exception)
            {
                MessageManager.ShowMsgBox(exception.Message);
            }
            catch (Exception exception2)
            {
                MessageManager.ShowMsgBox(exception2.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string path = this.file_2.get_TextBoxFile().Text.Trim();
                if (!File.Exists(path))
                {
                    MessageBoxHelper.Show(this.file_2.get_TextBoxFile().Text + "副表路径文件不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    string text = this.com_sheet_2.Text;
                    if (this.numUpDownSubHeadLine.Text.CompareTo("-") == 0)
                    {
                        MessageBoxHelper.Show("表头行数输入格式非法", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        int num2;
                        DataTable table = ExcelRead.ExcelToDataTable(Convert.ToInt32(this.numUpDownSubHeadLine.Text), path, text + "$");
                        List<string> list = new List<string>();
                        if (this.dgvViceTable.Columns.Count > 0)
                        {
                            for (num2 = 0; num2 < this.dgvViceTable.Columns.Count; num2++)
                            {
                                list.Add(this.dgvViceTable.Columns[num2].HeaderText);
                            }
                        }
                        this.dgvViceTable.DataSource = table;
                        for (num2 = 0; num2 < table.Columns.Count; num2++)
                        {
                            if (num2 < list.Count)
                            {
                                this.dgvViceTable.Columns[num2].HeaderText = list[num2];
                            }
                            else
                            {
                                this.dgvViceTable.Columns[num2].HeaderText = string.Format("--- 列{0} ---", num2 + 1);
                            }
                            this.dgvViceTable.Columns[num2].SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                    }
                }
            }
            catch (CustomException exception)
            {
                MessageManager.ShowMsgBox(exception.Message);
            }
            catch (Exception exception2)
            {
                MessageManager.ShowMsgBox(exception2.ToString());
            }
        }

        private void CheckInTableOneAndTwo(bool Checked)
        {
            this.splitContainer1.Panel2Collapsed = !Checked;
            if (this.com_sheet_1.Items.Count > 0)
            {
                this.com_sheet_1.SelectedIndex = 0;
            }
            if (this.com_sheet_2.Items.Count > 0)
            {
                this.com_sheet_2.SelectedIndex = 0;
            }
        }

        private void CheckLastSetType()
        {
            if (this.invtype == InvType.Common)
            {
                if (!File.Exists(IniRead.path))
                {
                    return;
                }
                IniRead.type = "c";
            }
            else if (this.invtype == InvType.Special)
            {
                if (!File.Exists(IniRead.path))
                {
                    return;
                }
                IniRead.type = "s";
            }
            else if (this.invtype == InvType.transportation)
            {
                if (!File.Exists(IniRead.path_2))
                {
                    return;
                }
                IniRead.type = "f";
            }
            else if (this.invtype == InvType.vehiclesales)
            {
                if (!File.Exists(IniRead.path_3))
                {
                    return;
                }
                IniRead.type = "j";
            }
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

        private void ComboboxSave()
        {
            XmlRead read = new XmlRead();
            int result = 0;
            try
            {
                int num2;
                read.Delete(FileName.File1);
                int.TryParse(this.numUpDownMainHeadLine.Text, out result);
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
            }
            catch (Exception exception)
            {
                MessageBoxHelper.Show(exception.ToString(), "异常");
            }
        }

        private bool DataCheck()
        {
            int num2;
            string headerText;
            List<string> list = new List<string>();
            int num = 0;
            for (num2 = 0; num2 < this.dgvMainTable.ColumnCount; num2++)
            {
                headerText = this.dgvMainTable.Columns[num2].HeaderText;
                if (!headerText.StartsWith("---"))
                {
                    list.Add(headerText);
                    if (headerText == "单据号码")
                    {
                        this.DJHMColMain = num2;
                        num += 10;
                    }
                }
            }
            if (this.rdbtnDoubletb.Checked)
            {
                for (num2 = 0; num2 < this.dgvViceTable.ColumnCount; num2++)
                {
                    headerText = this.dgvViceTable.Columns[num2].HeaderText;
                    if (!headerText.StartsWith("---"))
                    {
                        list.Add(headerText);
                        if (headerText == "单据号码")
                        {
                            this.DJHMColSub = num2;
                            num++;
                        }
                    }
                }
                switch (num)
                {
                    case 10:
                        MessageBoxHelper.Show("副表内缺少 单据号码列！");
                        return false;

                    case 1:
                        MessageBoxHelper.Show("主表内缺少 单据号码列！");
                        return false;
                }
            }
            if ((this.invtype == InvType.Common) || (this.invtype == InvType.Special))
            {
                if (!list.Contains("单据号码"))
                {
                    MessageBoxHelper.Show("单据号码 为必须项！");
                    return false;
                }
                if (!list.Contains("购方名称"))
                {
                    MessageBoxHelper.Show("购方名称 为必须项！");
                    return false;
                }
                if (!list.Contains("货物名称"))
                {
                    MessageBoxHelper.Show("货物名称 为必须项！");
                    return false;
                }
                double result = -1.0;
                if (double.TryParse(this.txtSLV.Text.Trim(), out result) && ((result < 100.0) && (result > 0.0)))
                {
                }
                if (!(list.Contains("金额") || list.Contains("税额")))
                {
                    MessageBoxHelper.Show("金额，税率，税额 三项至少要设置两项 ");
                    return false;
                }
            }
            else if (((this.invtype == InvType.transportation) || (this.invtype == InvType.vehiclesales)) && !list.Contains("单据号码"))
            {
                MessageBoxHelper.Show("单据号码 为必须项！");
                return false;
            }
            return true;
        }

        private void dgvMainTable_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.RowIndex == -1) && ((e.Button & MouseButtons.Left) == MouseButtons.Left))
            {
                string data = Convert.ToString(this.dgvMainTable.Columns[e.ColumnIndex].HeaderText);
                if (!data.StartsWith("---"))
                {
                    data = data + "@1$" + e.ColumnIndex;
                    this.dgvMainTable.DoDragDrop(data, DragDropEffects.Copy);
                    this.dgvCellMouseDownColIndex = e.ColumnIndex;
                }
            }
        }

        private void dgvTable_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.RowIndex == -1) && ((e.Button & MouseButtons.Left) == MouseButtons.Left))
            {
                DataGridView view = (DataGridView) sender;
                if (view.Cursor != Cursors.SizeWE)
                {
                    string str = Convert.ToString(view.Columns[e.ColumnIndex].HeaderText);
                    if (!str.StartsWith("---"))
                    {
                        int num = 0;
                        if (view.Name == "dgvViceTable")
                        {
                            num = 2;
                        }
                        else if (view.Name == "dgvMainTable")
                        {
                            num = 1;
                        }
                        str = string.Format("{0}@{1}${2}", str, num, e.ColumnIndex);
                        view.DoDragDrop(str, DragDropEffects.Copy);
                    }
                }
            }
        }

        private void dgvTable_DragDrop(object sender, DragEventArgs e)
        {
            DataGridView view = (DataGridView) sender;
            int tableName = 0;
            if (view.Name == "dgvViceTable")
            {
                tableName = 2;
            }
            else if (view.Name == "dgvMainTable")
            {
                tableName = 1;
            }
            int colIndex = this.GetColumnPoint(e.X, e.Y, tableName);
            if (colIndex >= 0)
            {
                string columnName = e.Data.GetData(DataFormats.Text).ToString();
                int num3 = -1;
                int num4 = 0;
                this.GetColumnNameIndex(ref columnName, ref num3, ref num4);
                if ((tableName != num4) || (colIndex != num3))
                {
                    string str2 = this.MovedHeadText(colIndex, tableName);
                    bool flag = false;
                    if (((num4 > 0) && (num4 != tableName)) && (columnName == "单据号码"))
                    {
                        int num5;
                        flag = true;
                        if ((num4 == 2) && (tableName == 1))
                        {
                            for (num5 = 0; num5 < this.dgvMainTable.Columns.Count; num5++)
                            {
                                if (this.dgvMainTable.Columns[num5].HeaderText == columnName)
                                {
                                    this.SetColumnHead(this.dgvMainTable, num5, string.Format("--- 列{0} ---", num5 + 1));
                                }
                            }
                        }
                        else if ((num4 == 1) && (tableName == 2))
                        {
                            for (num5 = 0; num5 < this.dgvViceTable.Columns.Count; num5++)
                            {
                                if (this.dgvViceTable.Columns[num5].HeaderText == columnName)
                                {
                                    this.SetColumnHead(this.dgvViceTable, num5, string.Format("--- 列{0} ---", num5 + 1));
                                }
                            }
                        }
                    }
                    if (view.Name == "dgvViceTable")
                    {
                        this.SetColumnHead(this.dgvViceTable, colIndex, columnName);
                    }
                    else if (view.Name == "dgvMainTable")
                    {
                        this.SetColumnHead(this.dgvMainTable, colIndex, columnName);
                    }
                    if (!flag)
                    {
                        foreach (Control control in this.groupBoxColNames.Controls)
                        {
                            if (control.Text == columnName)
                            {
                                control.Visible = false;
                                if (control.Text == "单据号码")
                                {
                                    control.Visible = true;
                                    if ((this.invtype == InvType.transportation) || (this.invtype == InvType.vehiclesales))
                                    {
                                        control.Visible = false;
                                    }
                                }
                                switch (num4)
                                {
                                    case 2:
                                        this.SetColumnHead(this.dgvViceTable, num3, string.Format("--- 列{0} ---", num3 + 1));
                                        break;

                                    case 1:
                                        this.SetColumnHead(this.dgvMainTable, num3, string.Format("--- 列{0} ---", num3 + 1));
                                        break;
                                }
                            }
                            if (control.Text == str2)
                            {
                                control.Visible = true;
                            }
                        }
                    }
                }
            }
        }

        private void dgvTable_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void ExcelSheetBindComBoBoxItems(AisinoCMB bobox, string path)
        {
        }

        private void file_1_onClickEnd(object sender, EventArgs e)
        {
            try
            {
                this.ExcelSheetBindComBoBoxItems(this.com_sheet_1, this.file_1.get_TextBoxFile().Text);
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
            }
            catch (Exception exception)
            {
                MessageBoxHelper.Show(exception.ToString(), "异常");
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            if ((this.invtype == InvType.transportation) || (this.invtype == InvType.vehiclesales))
            {
                this.rdbtnDoubletb.Visible = false;
                this.groupBox5.Visible = false;
            }
            this.CheckLastSetType();
            this.labExplain.Text = "说明：单据头信息放主表,明细信息放副表,\n     主副表必须设置“单据号码”列";
            this.groupBoxColNames.AllowDrop = true;
            DataTable table = WenBenItem.Items();
            int num = 0;
            int num2 = 0;
            TaxCard card = TaxCardFactory.CreateTaxCard();
            bool iSNCPXS = card.get_QYLX().ISNCPXS;
            bool iSNCPSG = card.get_QYLX().ISNCPSG;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                int num4 = int.Parse(table.Rows[i]["id"].ToString());
                if ((this.invtype == InvType.Common) || (this.invtype == InvType.Special))
                {
                    if ((((num4 >= 0x1c) && (num4 <= 0x42)) || (num4 >= 0x49)) || (((num4 >= 0x43) && (num4 <= 0x45)) && !(iSNCPXS || iSNCPSG)))
                    {
                        continue;
                    }
                }
                else if (this.invtype == InvType.transportation)
                {
                    if ((((num4 >= 2) && (num4 <= 0x1b)) || ((num4 >= 0x2d) && (num4 <= 0x45))) || (num4 >= 0x48))
                    {
                        continue;
                    }
                }
                else if ((this.invtype == InvType.vehiclesales) && ((((num4 >= 2) && (num4 <= 0x2c)) || ((num4 >= 0x43) && (num4 <= 0x45))) || (num4 >= 0x48)))
                {
                    continue;
                }
                WenBenItem.MapItem item = new WenBenItem.MapItem {
                    Key = table.Rows[i]["key"].ToString(),
                    ShuJuXiang = table.Rows[i]["ShuJuXiang"].ToString(),
                    BiXuanShuXin = table.Rows[i]["BiXuanShuXin"].ToString()
                };
                DataGridViewColumn dataGridViewColumn = new DataGridViewTextBoxColumn {
                    Name = item.Key,
                    HeaderText = item.ShuJuXiang,
                    DataPropertyName = item.Key
                };
                this.dgvPreView.Columns.Add(dataGridViewColumn);
                this.ColDic.Add(item.ShuJuXiang, item.Key);
                AisinoLBL olbl = new AisinoLBL {
                    AllowDrop = true,
                    AutoSize = true,
                    BorderStyle = BorderStyle.FixedSingle,
                    Font = new Font("微软雅黑", 10f, FontStyle.Bold, GraphicsUnit.Point, 0x86)
                };
                if (item.BiXuanShuXin == "必选")
                {
                    olbl.BackColor = Color.OrangeRed;
                }
                else
                {
                    olbl.BackColor = Color.White;
                }
                olbl.Tag = item;
                olbl.Name = item.Key;
                olbl.Text = item.ShuJuXiang;
                int x = 5 + num;
                olbl.Location = new Point(x, 20 + num2);
                int width = olbl.Size.Width;
                olbl.MouseMove += new MouseEventHandler(this.label9_MouseMove);
                olbl.DragDrop += new DragEventHandler(this.label9_DragDrop);
                this.groupBoxColNames.Controls.Add(olbl);
                if ((x + olbl.Size.Width) > this.groupBoxColNames.Size.Width)
                {
                    num = 0;
                    x = 5 + num;
                    num2 += 0x1c;
                    this.groupBoxColNames.Controls.Remove(olbl);
                    olbl.Location = new Point(x, 20 + num2);
                    this.groupBoxColNames.Controls.Add(olbl);
                }
                num = x + olbl.Size.Width;
            }
            this.LoadConfigInfo();
        }

        private void GetColumnNameIndex(ref string ColumnName, ref int ColIndex, ref int TableName)
        {
            if (ColumnName.Contains("$"))
            {
                int index = ColumnName.IndexOf('$');
                string str = ColumnName.Remove(0, index + 1);
                ColIndex = Convert.ToInt32(str);
                if (ColumnName.Contains("@"))
                {
                    int length = ColumnName.IndexOf('@');
                    string str2 = ColumnName.Substring(length + 1, 1);
                    TableName = Convert.ToInt32(str2);
                    ColumnName = ColumnName.Substring(0, length);
                }
            }
            else
            {
                ColIndex = -1;
                TableName = 0;
            }
        }

        private int GetColumnPoint(int x, int y, int tableName)
        {
            int num;
            Rectangle columnDisplayRectangle;
            if (tableName == 1)
            {
                for (num = 0; num < this.dgvMainTable.ColumnCount; num++)
                {
                    columnDisplayRectangle = this.dgvMainTable.GetColumnDisplayRectangle(num, false);
                    if (this.dgvMainTable.RectangleToScreen(columnDisplayRectangle).Contains(x, y))
                    {
                        return num;
                    }
                }
                return -1;
            }
            if (tableName == 2)
            {
                for (num = 0; num < this.dgvViceTable.ColumnCount; num++)
                {
                    columnDisplayRectangle = this.dgvViceTable.GetColumnDisplayRectangle(num, false);
                    if (this.dgvViceTable.RectangleToScreen(columnDisplayRectangle).Contains(x, y))
                    {
                        return num;
                    }
                }
                return -1;
            }
            return -2;
        }

        private string GetDictValue(string key, Dictionary<string, string> dict)
        {
            if (dict.ContainsKey(key))
            {
                return ("[" + key + "]" + dict[key]);
            }
            return string.Empty;
        }

        private void groupBoxColNames_DragDrop(object sender, DragEventArgs e)
        {
            string columnName = e.Data.GetData(DataFormats.Text).ToString();
            foreach (Control control in this.groupBoxColNames.Controls)
            {
                if (columnName.Contains(control.Text))
                {
                    control.Visible = true;
                    int colIndex = -1;
                    int tableName = 0;
                    this.GetColumnNameIndex(ref columnName, ref colIndex, ref tableName);
                    switch (tableName)
                    {
                        case 1:
                            this.SetColumnHead(this.dgvMainTable, colIndex, string.Format("--- 列{0} ---", colIndex + 1));
                            break;

                        case 2:
                            this.SetColumnHead(this.dgvViceTable, colIndex, string.Format("--- 列{0} ---", colIndex + 1));
                            break;
                    }
                    break;
                }
            }
        }

        private void groupBoxColNames_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void GroupBoxVisible(string ColumnName, bool Visible)
        {
            foreach (Control control in this.groupBoxColNames.Controls)
            {
                if (ColumnName.Contains(control.Text))
                {
                    control.Visible = Visible;
                    break;
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            DataGridViewCellStyle style4 = new DataGridViewCellStyle();
            DataGridViewCellStyle style5 = new DataGridViewCellStyle();
            DataGridViewCellStyle style6 = new DataGridViewCellStyle();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ExcelSetForm));
            this.btnPreview = new AisinoBTN();
            this.btnConfigSave = new AisinoBTN();
            this.groupBoxColNames = new AisinoGRP();
            this.dgvPreView = new CustomStyleDataGrid();
            this.btnClearYS = new AisinoBTN();
            this.txtSKR = new AisinoTXT();
            this.txtFHR = new AisinoTXT();
            this.txtSLV = new AisinoTXT();
            this.labSKR = new AisinoLBL();
            this.labFHR = new AisinoLBL();
            this.labSLV = new AisinoLBL();
            this.groupBox5 = new AisinoGRP();
            this.label4 = new AisinoLBL();
            this.btnloadColName = new AisinoBTN();
            this.button1 = new AisinoBTN();
            this.dgvMainTable = new DataGridView();
            this.rdbtnSingletb = new AisinoRDO();
            this.rdbtnDoubletb = new AisinoRDO();
            this.label1 = new AisinoLBL();
            this.label8 = new AisinoLBL();
            this.file_1 = new FileControl();
            this.com_sheet_1 = new AisinoCMB();
            this.numUpDownMainHeadLine = new AisinoNUD();
            this.file_2 = new FileControl();
            this.label3 = new AisinoLBL();
            this.com_sheet_2 = new AisinoCMB();
            this.dgvViceTable = new DataGridView();
            this.button2 = new AisinoBTN();
            this.label2 = new AisinoLBL();
            this.numUpDownSubHeadLine = new AisinoNUD();
            this.labExplain = new AisinoLBL();
            this.splitContainer1 = new SplitContainer();
            this.btnAddBlankColumn_Main = new AisinoBTN();
            this.btnAddBlankColumn_Vice = new AisinoBTN();
            this.toolTip1 = new ToolTip(this.components);
            this.label5 = new Label();
            this.dgvPreView.BeginInit();
            this.groupBox5.SuspendLayout();
            ((ISupportInitialize) this.dgvMainTable).BeginInit();
            this.numUpDownMainHeadLine.BeginInit();
            ((ISupportInitialize) this.dgvViceTable).BeginInit();
            this.numUpDownSubHeadLine.BeginInit();
            this.splitContainer1.BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            base.SuspendLayout();
            this.btnPreview.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnPreview.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.btnPreview.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.btnPreview.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.btnPreview.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            this.btnPreview.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.btnPreview.Location = new Point(0x330, 0x266);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new Size(0x61, 30);
            this.btnPreview.TabIndex = 7;
            this.btnPreview.Text = "导入预览";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new EventHandler(this.btnPreview_Click);
            this.btnConfigSave.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnConfigSave.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.btnConfigSave.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.btnConfigSave.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.btnConfigSave.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            this.btnConfigSave.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.btnConfigSave.Location = new Point(0x3a1, 0x266);
            this.btnConfigSave.Name = "btnConfigSave";
            this.btnConfigSave.Size = new Size(0x61, 30);
            this.btnConfigSave.TabIndex = 13;
            this.btnConfigSave.Text = "保存配置";
            this.btnConfigSave.UseVisualStyleBackColor = true;
            this.btnConfigSave.Click += new EventHandler(this.btnConfigSave_Click);
            this.groupBoxColNames.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.groupBoxColNames.BackColor = Color.Transparent;
            this.groupBoxColNames.Location = new Point(11, 7);
            this.groupBoxColNames.Name = "groupBoxColNames";
            this.groupBoxColNames.Size = new Size(0x371, 0x6d);
            this.groupBoxColNames.TabIndex = 0x22;
            this.groupBoxColNames.TabStop = false;
            this.groupBoxColNames.Text = "映射列名";
            this.groupBoxColNames.DragDrop += new DragEventHandler(this.groupBoxColNames_DragDrop);
            this.groupBoxColNames.DragEnter += new DragEventHandler(this.groupBoxColNames_DragEnter);
            this.dgvPreView.set_AborCellPainting(false);
            this.dgvPreView.set_AllowColumnHeadersVisible(true);
            this.dgvPreView.AllowUserToAddRows = false;
            this.dgvPreView.set_AllowUserToResizeRows(false);
            style.BackColor = Color.FromArgb(0xff, 250, 240);
            this.dgvPreView.AlternatingRowsDefaultCellStyle = style;
            this.dgvPreView.BackgroundColor = SystemColors.ButtonFace;
            this.dgvPreView.BorderStyle = BorderStyle.None;
            this.dgvPreView.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            this.dgvPreView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            style2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style2.BackColor = Color.FromArgb(0x15, 0x87, 0xca);
            style2.Font = new Font("微软雅黑", 10f);
            style2.ForeColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.dgvPreView.ColumnHeadersDefaultCellStyle = style2;
            this.dgvPreView.set_ColumnHeadersHeight(0);
            style3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style3.BackColor = Color.FromArgb(220, 230, 240);
            style3.Font = new Font("宋体", 10f);
            style3.ForeColor = Color.FromArgb(0, 0, 0);
            style3.SelectionBackColor = Color.FromArgb(0x7d, 150, 0xb9);
            style3.SelectionForeColor = Color.FromArgb(0xff, 0xff, 0xff);
            style3.WrapMode = DataGridViewTriState.False;
            this.dgvPreView.DefaultCellStyle = style3;
            this.dgvPreView.EnableHeadersVisualStyles = false;
            this.dgvPreView.GridColor = Color.Gray;
            this.dgvPreView.set_GridStyle(0);
            this.dgvPreView.Location = new Point(0x34b, 0x68);
            this.dgvPreView.Name = "dgvPreView";
            this.dgvPreView.ReadOnly = true;
            this.dgvPreView.RowHeadersVisible = false;
            style4.BackColor = Color.White;
            style4.SelectionBackColor = Color.SlateGray;
            this.dgvPreView.RowsDefaultCellStyle = style4;
            this.dgvPreView.RowTemplate.Height = 0x17;
            this.dgvPreView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.dgvPreView.Size = new Size(0x26, 0x24);
            this.dgvPreView.TabIndex = 3;
            this.dgvPreView.Visible = false;
            this.btnClearYS.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnClearYS.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.btnClearYS.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.btnClearYS.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.btnClearYS.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            this.btnClearYS.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.btnClearYS.Location = new Point(0x2bf, 0x266);
            this.btnClearYS.Name = "btnClearYS";
            this.btnClearYS.Size = new Size(0x61, 30);
            this.btnClearYS.TabIndex = 0x23;
            this.btnClearYS.Text = "清除映射设置";
            this.btnClearYS.UseVisualStyleBackColor = true;
            this.btnClearYS.Click += new EventHandler(this.btnClearYS_Click);
            this.txtSKR.AllowDrop = true;
            this.txtSKR.Location = new Point(0x2d, 0x11);
            this.txtSKR.Name = "txtSKR";
            this.txtSKR.Size = new Size(0x5d, 0x15);
            this.txtSKR.TabIndex = 0x24;
            this.txtFHR.AllowDrop = true;
            this.txtFHR.Location = new Point(0x2d, 0x2b);
            this.txtFHR.Name = "txtFHR";
            this.txtFHR.Size = new Size(0x5d, 0x15);
            this.txtFHR.TabIndex = 0x25;
            this.txtSLV.AllowDrop = true;
            this.txtSLV.Location = new Point(0x2d, 0x47);
            this.txtSLV.Name = "txtSLV";
            this.txtSLV.Size = new Size(0x33, 0x15);
            this.txtSLV.TabIndex = 0x26;
            this.txtSLV.Text = "0.17";
            this.labSKR.AutoSize = true;
            this.labSKR.Location = new Point(4, 0x15);
            this.labSKR.Name = "labSKR";
            this.labSKR.Size = new Size(0x29, 12);
            this.labSKR.TabIndex = 40;
            this.labSKR.Text = "收款人";
            this.labFHR.AutoSize = true;
            this.labFHR.Location = new Point(4, 0x2f);
            this.labFHR.Name = "labFHR";
            this.labFHR.Size = new Size(0x29, 12);
            this.labFHR.TabIndex = 0x29;
            this.labFHR.Text = "复核人";
            this.labSLV.AutoSize = true;
            this.labSLV.Location = new Point(0x10, 0x4b);
            this.labSLV.Name = "labSLV";
            this.labSLV.Size = new Size(0x1d, 12);
            this.labSLV.TabIndex = 0x2a;
            this.labSLV.Text = "税率";
            this.groupBox5.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.groupBox5.BackColor = Color.Transparent;
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.txtFHR);
            this.groupBox5.Controls.Add(this.labSLV);
            this.groupBox5.Controls.Add(this.txtSKR);
            this.groupBox5.Controls.Add(this.labFHR);
            this.groupBox5.Controls.Add(this.txtSLV);
            this.groupBox5.Controls.Add(this.labSKR);
            this.groupBox5.Location = new Point(0x381, 7);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0x8f, 0x6d);
            this.groupBox5.TabIndex = 0x2b;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "默认值";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x6c, 0x4b);
            this.label4.Name = "label4";
            this.label4.Size = new Size(11, 12);
            this.label4.TabIndex = 0x2b;
            this.label4.Text = "%";
            this.btnloadColName.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnloadColName.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.btnloadColName.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.btnloadColName.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.btnloadColName.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            this.btnloadColName.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.btnloadColName.Location = new Point(590, 0x266);
            this.btnloadColName.Name = "btnloadColName";
            this.btnloadColName.Size = new Size(0x61, 30);
            this.btnloadColName.TabIndex = 0x31;
            this.btnloadColName.Text = "加载列名配置";
            this.btnloadColName.UseVisualStyleBackColor = true;
            this.btnloadColName.Click += new EventHandler(this.btnloadColName_Click);
            this.button1.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.button1.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.button1.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.button1.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            this.button1.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.button1.Location = new Point(0x240, 9);
            this.button1.Name = "button1";
            this.button1.Size = new Size(50, 0x19);
            this.button1.TabIndex = 0x1d;
            this.button1.Text = "预览";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.dgvMainTable.AllowDrop = true;
            this.dgvMainTable.AllowUserToAddRows = false;
            this.dgvMainTable.AllowUserToDeleteRows = false;
            this.dgvMainTable.AllowUserToResizeColumns = false;
            this.dgvMainTable.AllowUserToResizeRows = false;
            this.dgvMainTable.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.dgvMainTable.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            style5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style5.BackColor = Color.Silver;
            style5.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style5.ForeColor = SystemColors.WindowText;
            style5.SelectionBackColor = SystemColors.Highlight;
            style5.SelectionForeColor = SystemColors.HighlightText;
            style5.WrapMode = DataGridViewTriState.True;
            this.dgvMainTable.ColumnHeadersDefaultCellStyle = style5;
            this.dgvMainTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMainTable.Location = new Point(11, 40);
            this.dgvMainTable.Name = "dgvMainTable";
            this.dgvMainTable.ReadOnly = true;
            this.dgvMainTable.RowHeadersVisible = false;
            this.dgvMainTable.RowTemplate.Height = 0x17;
            this.dgvMainTable.Size = new Size(0x3f4, 0xcb);
            this.dgvMainTable.TabIndex = 30;
            this.rdbtnSingletb.AutoSize = true;
            this.rdbtnSingletb.BackColor = Color.Transparent;
            this.rdbtnSingletb.Location = new Point(0x299, 13);
            this.rdbtnSingletb.Name = "rdbtnSingletb";
            this.rdbtnSingletb.Size = new Size(0x2f, 0x10);
            this.rdbtnSingletb.TabIndex = 0;
            this.rdbtnSingletb.Text = "单表";
            this.rdbtnSingletb.UseVisualStyleBackColor = false;
            this.rdbtnSingletb.CheckedChanged += new EventHandler(this.rdbtnSingletb_CheckedChanged);
            this.rdbtnDoubletb.AutoSize = true;
            this.rdbtnDoubletb.BackColor = Color.Transparent;
            this.rdbtnDoubletb.Location = new Point(0x2ce, 13);
            this.rdbtnDoubletb.Name = "rdbtnDoubletb";
            this.rdbtnDoubletb.Size = new Size(0x3b, 0x10);
            this.rdbtnDoubletb.TabIndex = 1;
            this.rdbtnDoubletb.Text = "主副表";
            this.rdbtnDoubletb.UseVisualStyleBackColor = false;
            this.rdbtnDoubletb.CheckedChanged += new EventHandler(this.rdbtnDoubletb_CheckedChanged);
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.Location = new Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x59, 12);
            this.label1.TabIndex = 0x2e;
            this.label1.Text = "主表所在文件：";
            this.label8.AutoSize = true;
            this.label8.BackColor = Color.Transparent;
            this.label8.Location = new Point(470, 15);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x35, 12);
            this.label8.TabIndex = 0x2f;
            this.label8.Text = "表头行数";
            this.file_1.BackColor = Color.Transparent;
            this.file_1.set_CheckFileExists(true);
            this.file_1.set_FileFilter("Excel文件(*.xls,*.xlsx)|*.xls;*.xlsx");
            this.file_1.set_FileFullname(null);
            this.file_1.Location = new Point(0x73, 11);
            this.file_1.Name = "file_1";
            this.file_1.set_OsFlag("open");
            this.file_1.Size = new Size(0x111, 0x15);
            this.file_1.TabIndex = 0x2c;
            this.com_sheet_1.FormattingEnabled = true;
            this.com_sheet_1.Location = new Point(0x18a, 11);
            this.com_sheet_1.Name = "com_sheet_1";
            this.com_sheet_1.Size = new Size(0x4c, 20);
            this.com_sheet_1.TabIndex = 0x2d;
            this.numUpDownMainHeadLine.Location = new Point(0x211, 11);
            this.numUpDownMainHeadLine.Name = "numUpDownMainHeadLine";
            this.numUpDownMainHeadLine.Size = new Size(0x29, 0x15);
            this.numUpDownMainHeadLine.TabIndex = 0x30;
            this.file_2.BackColor = Color.Transparent;
            this.file_2.set_CheckFileExists(true);
            this.file_2.set_FileFilter("Excel文件(*.xls,*.xlsx)|*.xls;*.xlsx");
            this.file_2.set_FileFullname(null);
            this.file_2.Location = new Point(0x65, 7);
            this.file_2.Name = "file_2";
            this.file_2.set_OsFlag("open");
            this.file_2.Size = new Size(0x11f, 0x15);
            this.file_2.TabIndex = 3;
            this.label3.AutoSize = true;
            this.label3.BackColor = Color.Transparent;
            this.label3.Location = new Point(11, 11);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x59, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "副表所在文件：";
            this.com_sheet_2.FormattingEnabled = true;
            this.com_sheet_2.Location = new Point(0x18a, 7);
            this.com_sheet_2.Name = "com_sheet_2";
            this.com_sheet_2.Size = new Size(0x4c, 20);
            this.com_sheet_2.TabIndex = 5;
            this.dgvViceTable.AllowDrop = true;
            this.dgvViceTable.AllowUserToAddRows = false;
            this.dgvViceTable.AllowUserToDeleteRows = false;
            this.dgvViceTable.AllowUserToResizeColumns = false;
            this.dgvViceTable.AllowUserToResizeRows = false;
            this.dgvViceTable.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.dgvViceTable.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            style6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style6.BackColor = Color.Silver;
            style6.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            style6.ForeColor = SystemColors.WindowText;
            style6.SelectionBackColor = SystemColors.Highlight;
            style6.SelectionForeColor = SystemColors.HighlightText;
            style6.WrapMode = DataGridViewTriState.True;
            this.dgvViceTable.ColumnHeadersDefaultCellStyle = style6;
            this.dgvViceTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvViceTable.Location = new Point(11, 0x24);
            this.dgvViceTable.Name = "dgvViceTable";
            this.dgvViceTable.ReadOnly = true;
            this.dgvViceTable.RowHeadersVisible = false;
            this.dgvViceTable.RowTemplate.Height = 0x17;
            this.dgvViceTable.Size = new Size(0x3f4, 0xc2);
            this.dgvViceTable.TabIndex = 0x1f;
            this.button2.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.button2.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.button2.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.button2.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            this.button2.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.button2.Location = new Point(0x250, 5);
            this.button2.Name = "button2";
            this.button2.Size = new Size(50, 0x19);
            this.button2.TabIndex = 0x20;
            this.button2.Text = "预览";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.label2.AutoSize = true;
            this.label2.BackColor = Color.Transparent;
            this.label2.Location = new Point(0x1dc, 11);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 0x31;
            this.label2.Text = "表头行数";
            this.numUpDownSubHeadLine.Location = new Point(0x217, 7);
            this.numUpDownSubHeadLine.Name = "numUpDownSubHeadLine";
            this.numUpDownSubHeadLine.Size = new Size(0x27, 0x15);
            this.numUpDownSubHeadLine.TabIndex = 50;
            this.labExplain.AutoSize = true;
            this.labExplain.BackColor = Color.Transparent;
            this.labExplain.ForeColor = Color.Red;
            this.labExplain.Location = new Point(0x298, 6);
            this.labExplain.Name = "labExplain";
            this.labExplain.Size = new Size(0x29, 12);
            this.labExplain.TabIndex = 0x33;
            this.labExplain.Text = "\"说明\"";
            this.splitContainer1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.splitContainer1.Location = new Point(11, 0x7a);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = Orientation.Horizontal;
            this.splitContainer1.Panel1.Controls.Add(this.btnAddBlankColumn_Main);
            this.splitContainer1.Panel1.Controls.Add(this.dgvPreView);
            this.splitContainer1.Panel1.Controls.Add(this.dgvMainTable);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.numUpDownMainHeadLine);
            this.splitContainer1.Panel1.Controls.Add(this.rdbtnSingletb);
            this.splitContainer1.Panel1.Controls.Add(this.rdbtnDoubletb);
            this.splitContainer1.Panel1.Controls.Add(this.com_sheet_1);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.file_1);
            this.splitContainer1.Panel2.Controls.Add(this.btnAddBlankColumn_Vice);
            this.splitContainer1.Panel2.Controls.Add(this.dgvViceTable);
            this.splitContainer1.Panel2.Controls.Add(this.labExplain);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.numUpDownSubHeadLine);
            this.splitContainer1.Panel2.Controls.Add(this.file_2);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.com_sheet_2);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Size = new Size(0x405, 0x1e3);
            this.splitContainer1.SplitterDistance = 0xf6;
            this.splitContainer1.TabIndex = 50;
            this.btnAddBlankColumn_Main.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.btnAddBlankColumn_Main.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.btnAddBlankColumn_Main.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.btnAddBlankColumn_Main.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            this.btnAddBlankColumn_Main.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.btnAddBlankColumn_Main.Location = new Point(0x39e, 6);
            this.btnAddBlankColumn_Main.Name = "btnAddBlankColumn_Main";
            this.btnAddBlankColumn_Main.Size = new Size(0x61, 30);
            this.btnAddBlankColumn_Main.TabIndex = 0x31;
            this.btnAddBlankColumn_Main.Text = "增加空白列";
            this.btnAddBlankColumn_Main.UseVisualStyleBackColor = true;
            this.btnAddBlankColumn_Main.Click += new EventHandler(this.btnAddBlankColumn_Main_Click);
            this.btnAddBlankColumn_Vice.set_BackColorActive(Color.FromArgb(0x19, 0x76, 210));
            this.btnAddBlankColumn_Vice.set_ColorDefaultA(Color.FromArgb(0, 0xac, 0xfb));
            this.btnAddBlankColumn_Vice.set_ColorDefaultB(Color.FromArgb(0, 0x91, 0xe0));
            this.btnAddBlankColumn_Vice.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            this.btnAddBlankColumn_Vice.set_FontColor(Color.FromArgb(0xff, 0xff, 0xff));
            this.btnAddBlankColumn_Vice.Location = new Point(0x39e, 3);
            this.btnAddBlankColumn_Vice.Name = "btnAddBlankColumn_Vice";
            this.btnAddBlankColumn_Vice.Size = new Size(0x61, 30);
            this.btnAddBlankColumn_Vice.TabIndex = 0x31;
            this.btnAddBlankColumn_Vice.Text = "增加空白列";
            this.btnAddBlankColumn_Vice.UseVisualStyleBackColor = true;
            this.btnAddBlankColumn_Vice.Click += new EventHandler(this.btnAddBlankColumn_Vice_Click);
            this.toolTip1.AutoPopDelay = 0x2710;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipIcon = ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "使用帮助";
            this.label5.AutoSize = true;
            this.label5.BorderStyle = BorderStyle.FixedSingle;
            this.label5.FlatStyle = FlatStyle.Flat;
            this.label5.Font = new Font("宋体", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label5.Location = new Point(0x16, 0x26d);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x59, 0x11);
            this.label5.TabIndex = 0x33;
            this.label5.Text = "如何使用？";
            this.label5.Click += new EventHandler(this.label5_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x416, 0x292);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.splitContainer1);
            base.Controls.Add(this.btnloadColName);
            base.Controls.Add(this.btnConfigSave);
            base.Controls.Add(this.btnPreview);
            base.Controls.Add(this.btnClearYS);
            base.Controls.Add(this.groupBox5);
            base.Controls.Add(this.groupBoxColNames);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "ExcelSetForm";
            this.Text = "导入Excel配置";
            base.Load += new EventHandler(this.Form4_Load);
            this.dgvPreView.EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((ISupportInitialize) this.dgvMainTable).EndInit();
            this.numUpDownMainHeadLine.EndInit();
            ((ISupportInitialize) this.dgvViceTable).EndInit();
            this.numUpDownSubHeadLine.EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.EndInit();
            this.splitContainer1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            new ExcelToolTipForm().ShowDialog();
        }

        private void label9_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void label9_DragEnter(object sender, DragEventArgs e)
        {
        }

        private void label9_MouseMove(object sender, MouseEventArgs e)
        {
            AisinoLBL olbl = (AisinoLBL) sender;
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                DragDropEffects effects = olbl.DoDragDrop(olbl.Text, DragDropEffects.Link | DragDropEffects.Copy);
            }
        }

        private void LoadConfigInfo()
        {
            try
            {
                if (this.invtype == InvType.Common)
                {
                    IniRead.type = "c";
                }
                else if (this.invtype == InvType.Special)
                {
                    IniRead.type = "s";
                }
                else if (this.invtype == InvType.transportation)
                {
                    IniRead.type = "f";
                }
                else if (this.invtype == InvType.vehiclesales)
                {
                    IniRead.type = "j";
                }
                Dictionary<string, string> item = new XmlRead().GetItem(FileName.File1);
                this.file_1.set_FileFilter(ResolverExcel.FileFilter);
                this.file_2.set_FileFilter(ResolverExcel.FileFilter);
                this.file_1.get_TextBoxFile().Text = IniRead.GetPrivateProfileString("File", "File1Path");
                this.file_2.get_TextBoxFile().Text = IniRead.GetPrivateProfileString("File", "File2Path");
                string privateProfileString = IniRead.GetPrivateProfileString("FieldCon", "FileNumber");
                int result = 1;
                int.TryParse(privateProfileString, out result);
                this.rdbtnSingletb.Checked = result == 1;
                this.rdbtnDoubletb.Checked = result == 2;
                this.txtFHR.Text = IniRead.GetPrivateProfileString("FieldCon", "DefaultFuHeRen");
                this.txtSKR.Text = IniRead.GetPrivateProfileString("FieldCon", "DefaultShouKuanRen");
                this.txtSLV.Text = IniRead.GetPrivateProfileString("FieldCon", "DefaultShuiLv");
                this.numUpDownMainHeadLine.Text = IniRead.GetPrivateProfileString("TableCon", "MainTableIgnoreRow");
                this.numUpDownSubHeadLine.Text = IniRead.GetPrivateProfileString("TableCon", "AssistantTableIgnoreRow");
                this.com_sheet_1.DropDownStyle = ComboBoxStyle.DropDownList;
                this.com_sheet_2.DropDownStyle = ComboBoxStyle.DropDownList;
                this.com_sheet_1.Text = IniRead.GetPrivateProfileString("File", "TableInFile1");
                this.com_sheet_2.Text = IniRead.GetPrivateProfileString("File", "TableInFile2");
            }
            catch (Exception exception)
            {
                MessageBoxHelper.Show(exception.ToString(), "异常");
            }
        }

        private string MovedHeadText(int colIndex, int tbName)
        {
            string headerText = "";
            if (tbName == 2)
            {
                return this.dgvViceTable.Columns[colIndex].HeaderText;
            }
            if (tbName == 1)
            {
                headerText = this.dgvMainTable.Columns[colIndex].HeaderText;
            }
            return headerText;
        }

        private void rdbtnDoubletb_CheckedChanged(object sender, EventArgs e)
        {
            if ((this.file_1.get_TextBoxFile().Text.Trim().Length > 0) && (this.file_2.get_TextBoxFile().Text.Trim().Length > 0))
            {
                this.ExcelSheetBindComBoBoxItems(this.com_sheet_1, this.file_1.get_TextBoxFile().Text);
                this.ExcelSheetBindComBoBoxItems(this.com_sheet_2, this.file_2.get_TextBoxFile().Text);
            }
            this.CheckInTableOneAndTwo(this.rdbtnDoubletb.Checked);
        }

        private void rdbtnSingletb_CheckedChanged(object sender, EventArgs e)
        {
            if (this.file_1.get_TextBoxFile().Text.Trim().Length > 0)
            {
                this.ExcelSheetBindComBoBoxItems(this.com_sheet_1, this.file_1.get_TextBoxFile().Text);
            }
            if (!this.rdbtnDoubletb.Checked)
            {
                for (int i = 0; i < this.dgvViceTable.ColumnCount; i++)
                {
                    string headerText = this.dgvViceTable.Columns[i].HeaderText;
                    this.SetColumnHead(this.dgvViceTable, i, string.Format("--- 列{0} ---", i + 1));
                    this.GroupBoxVisible(headerText, true);
                }
            }
            this.CheckInTableOneAndTwo(this.rdbtnDoubletb.Checked);
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

        private void SetColumnHead(DataGridView dgvTable, int ColumnIndex, string HeaderText)
        {
            if (ColumnIndex < dgvTable.Columns.Count)
            {
                for (int i = 0; i < dgvTable.Columns.Count; i++)
                {
                    if (dgvTable.Columns[i].HeaderText == HeaderText)
                    {
                        dgvTable.Columns[i].HeaderText = string.Format("--- 列{0} ---", i + 1);
                        dgvTable.Columns[i].DefaultCellStyle.BackColor = Color.White;
                    }
                }
                dgvTable.Columns[ColumnIndex].HeaderText = HeaderText;
                if (HeaderText.StartsWith("---"))
                {
                    dgvTable.Columns[ColumnIndex].DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    dgvTable.Columns[ColumnIndex].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }
            }
        }

        private void txtFHR_Validating(object sender, CancelEventArgs e)
        {
            this.txtFHR.Text = GetSafeData.GetSafeString(this.txtFHR.Text.Trim(), 8);
        }

        private void txtSKR_Validating(object sender, CancelEventArgs e)
        {
            this.txtSKR.Text = GetSafeData.GetSafeString(this.txtSKR.Text.Trim(), 8);
        }

        private void txtSLV_TextChanged(object sender, EventArgs e)
        {
            double result = 0.0;
            if (!double.TryParse(this.txtSLV.Text.Trim(), out result) || ((result > 100.0) || (result < 0.0)))
            {
                this.txtSLV.Text = "";
            }
        }
    }
}

