namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Registry;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
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
    using System.Windows.Forms;

    public class FPCX : DockForm
    {
        private AisinoDataGrid aisinoDataGrid1;
        private AisinoBTN btnQuery;
        private AisinoBTN buttonName;
        private AisinoCHK ckbAllDate;
        private AisinoCHK ckbContainTadayE;
        private AisinoCHK ckbContainTadayS;
        private AisinoCMB comboBoxFPZL;
        private AisinoCMB comboBoxZFBZ;
        private IContainer components = null;
        private DateTimePicker dTPEnd;
        private DateTimePicker dTPStart;
        private FPCXbll fpcxBLL = new FPCXbll();
        private AisinoGRP groupBox1;
        private InvoiceQueryCondition invoiceQueryCondition = new InvoiceQueryCondition();
        private AisinoLBL label1;
        private AisinoLBL label10;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoLBL label4;
        private AisinoLBL label5;
        private AisinoLBL label7;
        private AisinoLBL label8;
        private AisinoLBL label9;
        private List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
        private ArrayList m_ArrayList = new ArrayList();
        private const int nMaxSubStrCount = 0x200;
        private FaPiaoQueryArgs QueryArgs = new FaPiaoQueryArgs();
        private StatusStrip statusStrip1;
        private AisinoTXT textBoxDJH;
        private AisinoTXT textBoxFPDM;
        private AisinoTXT textBoxFPHM;
        private AisinoTXT textBoxGFMC;
        private AisinoTXT textBoxGFSH;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButtonExcel;
        private ToolStripButton toolStripButtonExit;
        private ToolStripButton toolStripButtonMX;
        private ToolStripButton toolStripButtonPrint;
        private ToolStripButton toolStripButtonStyle;
        private ToolStripButton toolStripButtonXML;
        private XmlComponentLoader xmlComponentLoader1;

        public FPCX()
        {
            this.Initialize();
        }

        private void aisinoDataGrid1_DataGridRowClickEvent(object sender, DataGridRowEventArgs e)
        {
        }

        private void aisinoDataGrid1_DataGridRowDbClickEvent(object sender, DataGridRowEventArgs e)
        {
            this.Refresh();
            try
            {
                if (((this.aisinoDataGrid1.get_Rows().Count > 0) && (this.aisinoDataGrid1.get_SelectedRows().Count > 0)) && (this.aisinoDataGrid1.get_CurrentCell() != null))
                {
                    if (this.IsEmptyInvData(this.aisinoDataGrid1.get_CurrentCell(), string.Empty))
                    {
                        MessageManager.ShowMsgBox("空白作废发票不支持此操作！");
                    }
                    else
                    {
                        List<string[]> list = new List<string[]>();
                        string str = Convert.ToString(this.aisinoDataGrid1.get_CurrentCell().RowIndex);
                        int num = 0;
                        List<int> list2 = new List<int>();
                        foreach (DataGridViewRow row in (IEnumerable) this.aisinoDataGrid1.get_Rows())
                        {
                            string str2 = Convert.ToString(row.Cells["FPDM"].Value);
                            string str3 = Convert.ToString(row.Cells["FPHM"].Value);
                            string str4 = Convert.ToString(row.Cells["ZFBZ"].Value);
                            string str5 = Convert.ToString(row.Cells["YYSBZ"].Value);
                            if (str4.Trim().Equals("是"))
                            {
                                str4 = "1";
                            }
                            else
                            {
                                str4 = "0";
                            }
                            int rowIndex = row.Cells[0].RowIndex;
                            string strInvType = row.Cells["FPZL"].Value.ToString();
                            if (((strInvType == "普通发票") || (strInvType == "农产品销售发票")) || (strInvType == "收购发票"))
                            {
                                strInvType = "c";
                            }
                            else if (strInvType == "专用发票")
                            {
                                strInvType = "s";
                            }
                            else if (strInvType == "货物运输业增值税专用发票")
                            {
                                strInvType = "f";
                            }
                            else if (strInvType == "机动车销售统一发票")
                            {
                                strInvType = "j";
                            }
                            else
                            {
                                continue;
                            }
                            string[] item = new string[] { strInvType, str2, str3, str4, rowIndex.ToString(), str5 };
                            if (rowIndex == this.aisinoDataGrid1.get_CurrentCell().RowIndex)
                            {
                                str = (this.aisinoDataGrid1.get_CurrentCell().RowIndex - num).ToString();
                            }
                            if (!this.IsEmptyInvData(row.Cells["GFSH"], strInvType))
                            {
                                list2.Add(list.Count);
                                list.Add(item);
                            }
                            else
                            {
                                num++;
                                list2.Add(-1000);
                            }
                        }
                        if (list.Count > 0)
                        {
                            object[] objArray = new object[] { "0", str, list };
                            if (ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPMX", objArray) == null)
                            {
                                MessageManager.ShowMsgBox("INP-274210");
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private void aisinoDataGrid1_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            string str = e.get_PageNO().ToString();
            PropertyUtil.SetValue("WBJK_FPCX_DATAGRID", str);
            this.aisinoDataGrid1.set_DataSource(this.fpcxBLL.GetInvoiceDetail(this.invoiceQueryCondition, e.get_PageSize(), e.get_PageNO()));
            int count = this.aisinoDataGrid1.get_Rows().Count;
            for (int i = 0; i < count; i++)
            {
                string str2 = this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value.ToString();
                if ((((str2 != null) && (str2 != "")) && ((str2 != "免税") && (str2 != "中外合作油气田"))) && (str2 != "多税率"))
                {
                    str2 = ((Convert.ToDouble(str2) * 100.0)).ToString() + "%";
                    if (str2 == "1.5%")
                    {
                        this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = "减按1.5%计算";
                    }
                    else
                    {
                        this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = str2;
                    }
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.aisinoDataGrid1.get_Rows().Count > 0)
                {
                    int num = this.aisinoDataGrid1.get_Rows().Count;
                    while (num-- > 0)
                    {
                        this.aisinoDataGrid1.get_Rows().RemoveAt(0);
                    }
                }
                string str = "";
                int result = 0;
                if (this.textBoxFPHM.Text.Trim().Length <= 8)
                {
                    if (int.TryParse(this.textBoxFPHM.Text.Trim(), out result))
                    {
                        str = result.ToString();
                    }
                    else
                    {
                        str = this.textBoxFPHM.Text.Trim();
                    }
                }
                else
                {
                    str = this.textBoxFPHM.Text.Trim();
                }
                this.invoiceQueryCondition.StrSplit(this.textBoxGFMC.Text.Trim(), ref this.invoiceQueryCondition.m_strBuyerNameList, ';', 0x200);
                this.invoiceQueryCondition.StrSplit(this.textBoxGFSH.Text.Trim(), ref this.invoiceQueryCondition.m_strBuyerCodeList, ';', 0x200);
                this.invoiceQueryCondition.StrSplit(this.textBoxFPDM.Text.Trim(), ref this.invoiceQueryCondition.m_strInvCodeList, ';', 0x200);
                this.invoiceQueryCondition.StrSplit(str.Trim(), ref this.invoiceQueryCondition.m_strInvNumList, ';', 0x200);
                this.invoiceQueryCondition.StrSplit(this.textBoxDJH.Text.Trim(), ref this.invoiceQueryCondition.m_strBillCodeList, ';', 0x200);
                this.invoiceQueryCondition.m_strInvType = this.comboBoxFPZL.SelectedValue.ToString();
                this.invoiceQueryCondition.m_strWasteFlag = this.comboBoxZFBZ.SelectedValue.ToString();
                if (!this.ckbAllDate.Checked)
                {
                    string str2 = this.dTPStart.Value.ToShortDateString();
                    string str3 = this.dTPEnd.Value.ToShortDateString();
                    if (str2.Equals(str3))
                    {
                        if (this.ckbContainTadayS.Checked && this.ckbContainTadayE.Checked)
                        {
                            this.invoiceQueryCondition.m_dtStart = this.dTPStart.Value;
                            this.invoiceQueryCondition.m_dtEnd = this.dTPEnd.Value.AddDays(1.0);
                        }
                        else
                        {
                            this.invoiceQueryCondition.m_dtStart = this.dTPStart.Value;
                            this.invoiceQueryCondition.m_dtEnd = this.dTPEnd.Value;
                        }
                    }
                    else
                    {
                        this.invoiceQueryCondition.m_dtStart = this.ckbContainTadayS.Checked ? this.dTPStart.Value : this.dTPStart.Value.AddDays(1.0);
                        this.invoiceQueryCondition.m_dtEnd = this.ckbContainTadayE.Checked ? this.dTPEnd.Value.AddDays(1.0) : this.dTPEnd.Value;
                    }
                }
                else
                {
                    this.invoiceQueryCondition.m_dtStart = Convert.ToDateTime("0001-01-01");
                    this.invoiceQueryCondition.m_dtEnd = Convert.ToDateTime("0001-01-01");
                }
                int num3 = 1;
                int.TryParse(PropertyUtil.GetValue("WBJK_FPCX_DATAGRID"), out num3);
                this.aisinoDataGrid1.set_DataSource(this.fpcxBLL.GetInvoiceDetail(this.invoiceQueryCondition, this.fpcxBLL.Pagesize, num3));
                PropValue.AllDate = this.ckbAllDate.Checked;
                PropValue.ContainStartDay = this.ckbContainTadayS.Checked;
                PropValue.ContainEndDay = this.ckbContainTadayE.Checked;
                int count = this.aisinoDataGrid1.get_Rows().Count;
                for (int i = 0; i < count; i++)
                {
                    string str4 = this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value.ToString();
                    if ((((str4 != null) && (str4 != "")) && ((str4 != "免税") && (str4 != "中外合作油气田"))) && (str4 != "多税率"))
                    {
                        str4 = ((Convert.ToDouble(str4) * 100.0)).ToString() + "%";
                        if (str4 == "1.5%")
                        {
                            this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = "减按1.5%计算";
                        }
                        else
                        {
                            this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = str4;
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
                HandleException.HandleError(exception2);
            }
        }

        private void buttonName_Click(object sender, EventArgs e)
        {
            string str = this.comboBoxFPZL.SelectedValue.ToString();
            AddCodeForm form = new AddCodeForm(this.m_ArrayList, false, str) {
                ShowInTaskbar = false,
                Text = "选择购方名称",
                strLabelSrc = "所有购方名称",
                strLabelDest = "已选购方名称"
            };
            form.ShowDialog();
            this.m_ArrayList = form.m_ArrayListDest;
            this.textBoxGFMC.Text = "";
            int num = 0;
            while (num < (this.m_ArrayList.Count - 1))
            {
                this.textBoxGFMC.Text = this.textBoxGFMC.Text + this.m_ArrayList[num].ToString();
                this.textBoxGFMC.Text = this.textBoxGFMC.Text + ";";
                num++;
            }
            if (this.m_ArrayList.Count > 0)
            {
                this.textBoxGFMC.Text = this.textBoxGFMC.Text + this.m_ArrayList[num].ToString();
            }
        }

        private void ckbAllDate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbAllDate.Checked)
            {
                this.label1.Enabled = false;
                this.label2.Enabled = false;
                this.dTPStart.Enabled = false;
                this.dTPEnd.Enabled = false;
                this.ckbContainTadayS.Enabled = false;
                this.ckbContainTadayE.Enabled = false;
            }
            else
            {
                this.label1.Enabled = true;
                this.label2.Enabled = true;
                this.dTPStart.Enabled = true;
                this.dTPEnd.Enabled = true;
                this.ckbContainTadayS.Enabled = true;
                this.ckbContainTadayE.Enabled = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FPCX_Load(object sender, EventArgs e)
        {
            try
            {
                TaxCard card = TaxCardFactory.CreateTaxCard();
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "销售单据编号");
                item.Add("Property", "XSDJBH");
                item.Add("Type", "Text");
                item.Add("Width", "120");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "发票种类");
                item.Add("Property", "FPZL");
                item.Add("Type", "Text");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "发票代码");
                item.Add("Property", "FPDM");
                item.Add("Type", "Text");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "发票号码");
                item.Add("Property", "FPHM");
                item.Add("Type", "Text");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "开票机号");
                item.Add("Property", "KPJH");
                item.Add("Type", "Text");
                item.Add("Align", "MiddleCenter");
                item.Add("Width", "80");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "购方名称");
                item.Add("Property", "GFMC");
                item.Add("Type", "Text");
                item.Add("Width", "100");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "购方税号");
                item.Add("Property", "GFSH");
                item.Add("Type", "Text");
                item.Add("Width", "100");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "购方地址电话");
                item.Add("Property", "GFDZDH");
                item.Add("Type", "Text");
                item.Add("Width", "120");
                item.Add("HeadAlign", "MiddleLeft");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "购方银行账号");
                item.Add("Property", "GFYHZH");
                item.Add("Type", "Text");
                item.Add("Width", "120");
                item.Add("HeadAlign", "MiddleLeft");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "身份证号码/组织机构代码");
                item.Add("Property", "XSBM");
                item.Add("Type", "Text");
                item.Add("Width", "120");
                item.Add("HeadAlign", "MiddleLeft");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "开票日期");
                item.Add("Property", "KPRQ");
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleLeft");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "合计金额");
                item.Add("Property", "HJJE");
                item.Add("Type", "Text");
                item.Add("Width", "60");
                item.Add("Align", "MiddleRight");
                item.Add("HeadAlign", "MiddleLeft");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "税率");
                item.Add("Property", "SLV");
                item.Add("Type", "Text");
                item.Add("Width", "60");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleLeft");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "合计税额");
                item.Add("Property", "HJSE");
                item.Add("Type", "Text");
                item.Add("Width", "60");
                item.Add("Align", "MiddleRight");
                item.Add("HeadAlign", "MiddleLeft");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "主要商品名称");
                item.Add("Property", "ZYSPMC");
                item.Add("Type", "Text");
                item.Add("Width", "160");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleLeft");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "商品税目");
                item.Add("Property", "SPSM");
                item.Add("Type", "Text");
                item.Add("Width", "100");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleLeft");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "备注");
                item.Add("Property", "BZ");
                item.Add("Type", "Text");
                item.Add("Visible", "False");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "开票人");
                item.Add("Property", "KPR");
                item.Add("Type", "Text");
                item.Add("Width", "80");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "收款人");
                item.Add("Property", "SKR");
                item.Add("Type", "Text");
                item.Add("Width", "80");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "复核人");
                item.Add("Property", "FHR");
                item.Add("Type", "Text");
                item.Add("Width", "80");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "作废标志");
                item.Add("Property", "ZFBZ");
                item.Add("Type", "Text");
                item.Add("Width", "80");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "清单标志");
                item.Add("Property", "QDBZ");
                item.Add("Type", "Text");
                item.Add("Width", "80");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "分页序号");
                item.Add("Property", "ROWNUMBER");
                item.Add("Type", "Text");
                item.Add("Visible", "False");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "销方名称");
                item.Add("Property", "XFMC");
                item.Add("Type", "Text");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "销方税号");
                item.Add("Property", "XFSH");
                item.Add("Type", "Text");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "销方地址电话");
                item.Add("Property", "XFDZDH");
                item.Add("Type", "Text");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "销方银行账号");
                item.Add("Property", "XFYHZH");
                item.Add("Type", "Text");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "报送状态");
                item.Add("Property", "BSZT");
                item.Add("Type", "Text");
                item.Add("Width", "100");
                if (card.get_QYLX().ISTDQY)
                {
                    item.Add("Visible", "False");
                }
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "作废日期");
                item.Add("Property", "ZFRQ");
                item.Add("Type", "Text");
                item.Add("Width", "100");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "营业税标志");
                item.Add("Property", "YYSBZ");
                item.Add("Type", "Text");
                item.Add("Visible", "False");
                this.list.Add(item);
                this.aisinoDataGrid1.set_ColumeHead(this.list);
                this.aisinoDataGrid1.get_Columns()["KPRQ"].DefaultCellStyle.Format = "yyyy-MM-dd";
                this.aisinoDataGrid1.get_Columns()["HJJE"].DefaultCellStyle.Format = "0.00";
                this.aisinoDataGrid1.get_Columns()["HJSE"].DefaultCellStyle.Format = "0.00";
                this.aisinoDataGrid1.get_Columns()["FPHM"].DefaultCellStyle.Format = new string('0', 8);
                this.aisinoDataGrid1.set_DataSource(new AisinoDataSet());
                this.comboBoxFPZL.DropDownStyle = ComboBoxStyle.DropDownList;
                this.comboBoxFPZL.DataSource = CbbXmlBind.ReadXmlNode("InvType_1", true);
                this.comboBoxFPZL.DisplayMember = "Value";
                this.comboBoxFPZL.ValueMember = "Key";
                if (RegisterManager.CheckRegFile("JIJS"))
                {
                    this.comboBoxFPZL.SelectedIndex = 2;
                    this.comboBoxFPZL.Enabled = false;
                }
                DataTable table = new DataTable();
                table.Columns.Add("Display");
                table.Columns.Add("Value");
                table.Rows.Add(new object[] { "否", "0" });
                table.Rows.Add(new object[] { "是", "1" });
                table.Rows.Add(new object[] { "全部", "both" });
                this.comboBoxZFBZ.DropDownStyle = ComboBoxStyle.DropDownList;
                this.comboBoxZFBZ.DataSource = table;
                this.comboBoxZFBZ.DisplayMember = "Display";
                this.comboBoxZFBZ.ValueMember = "Value";
                this.ckbAllDate.Checked = PropValue.AllDate;
                this.ckbContainTadayS.Checked = PropValue.ContainStartDay;
                this.ckbContainTadayE.Checked = PropValue.ContainEndDay;
                this.dTPStart.Value = this.dTPStart.Value.Date.AddDays((double) ((-1 * this.dTPStart.Value.Day) + 1));
                this.dTPEnd.Value = this.dTPEnd.Value.Date;
                if (!RegisterManager.CheckRegFile("JIJT"))
                {
                    this.toolStripButtonExcel.Visible = false;
                    this.toolStripButtonXML.Visible = false;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.buttonName = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("buttonName");
            this.buttonName.Click += new EventHandler(this.buttonName_Click);
            this.textBoxFPHM = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxFPHM");
            this.textBoxFPDM = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxFPDM");
            this.btnQuery = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnQuery");
            this.comboBoxZFBZ = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comboBoxZFBZ");
            this.comboBoxFPZL = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("comboBoxFPZL");
            this.label10 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label10");
            this.label9 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label9");
            this.label8 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label8");
            this.label7 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label7");
            this.ckbContainTadayE = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("ckbContainTadayE");
            this.aisinoDataGrid1 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid1");
            this.ckbAllDate = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("ckbAllDate");
            this.ckbContainTadayS = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("ckbContainTadayS");
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.textBoxDJH = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxDJH");
            this.label5 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label5");
            this.textBoxGFSH = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxGFSH");
            this.textBoxGFMC = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxGFMC");
            this.dTPEnd = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("dTPEnd");
            this.dTPStart = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("dTPStart");
            this.label4 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label4");
            this.label3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label3");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.statusStrip1 = this.xmlComponentLoader1.GetControlByName<StatusStrip>("statusStrip1");
            this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
            this.aisinoDataGrid1.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid1_GoToPageEvent));
            this.aisinoDataGrid1.add_DataGridRowDbClickEvent(new EventHandler<DataGridRowEventArgs>(this.aisinoDataGrid1_DataGridRowDbClickEvent));
            this.ckbAllDate.CheckedChanged += new EventHandler(this.ckbAllDate_CheckedChanged);
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.toolStripButtonExit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonExit");
            this.toolStripButtonExit.Click += new EventHandler(this.toolStripButtonExit_Click);
            this.toolStripButtonPrint = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonPrint");
            this.toolStripButtonPrint.Click += new EventHandler(this.toolStripButtonPrint_Click);
            this.toolStripButtonStyle = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonStyle");
            this.toolStripButtonStyle.Click += new EventHandler(this.toolStripButtonStyle_Click);
            this.toolStripButtonExcel = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonExcel");
            this.toolStripButtonExcel.Click += new EventHandler(this.toolStripButtonExcel_Click);
            this.toolStripButtonXML = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonXML");
            this.toolStripButtonXML.Click += new EventHandler(this.toolStripButtonXML_Click);
            this.toolStripButtonMX = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonMX");
            this.toolStripButtonMX.Click += new EventHandler(this.toolStripButtonMX_Click);
            this.aisinoDataGrid1.set_ReadOnly(true);
            this.aisinoDataGrid1.get_DataGrid().AllowUserToDeleteRows = false;
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x369, 0x23f);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.FPCX\Aisino.Fwkp.Wbjk.FPCX.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x369, 0x23f);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "FPCX";
            base.set_TabText("发票查询");
            this.Text = "发票查询";
            base.Load += new EventHandler(this.FPCX_Load);
            base.ResumeLayout(false);
        }

        private bool IsEmptyInvData(DataGridViewCell GridViewCell, string strInvType)
        {
            try
            {
                if (GridViewCell == null)
                {
                    return true;
                }
                if (string.IsNullOrEmpty(strInvType))
                {
                    strInvType = GridViewCell.OwningRow.Cells["FPZL"].Value.ToString().Trim();
                    if ((strInvType.Equals("普通发票") || strInvType.Equals("农产品销售发票")) || strInvType.Equals("收购发票"))
                    {
                        strInvType = "c";
                    }
                    else if (strInvType.Equals("专用发票"))
                    {
                        strInvType = "s";
                    }
                    else if (strInvType.Equals("货物运输业增值税专用发票"))
                    {
                        strInvType = "f";
                    }
                    else if (strInvType.Equals("机动车销售统一发票"))
                    {
                        strInvType = "j";
                    }
                }
                string str = GridViewCell.OwningRow.Cells["GFSH"].Value.ToString().Trim();
                string str2 = GridViewCell.OwningRow.Cells["ZFBZ"].Value.ToString().Trim();
                string str3 = GridViewCell.OwningRow.Cells["HJJE"].Value.ToString().Trim();
                string str4 = GridViewCell.OwningRow.Cells["HJSE"].Value.ToString().Trim();
                bool flag = false;
                switch (str2)
                {
                    case "是":
                        flag = true;
                        break;

                    case "否":
                        flag = false;
                        break;
                }
                double num = (str3 == null) ? 0.0 : Convert.ToDouble(str3);
                double num2 = (str4 == null) ? 0.0 : Convert.ToDouble(str4);
                if ((flag && (num == 0.0)) && (num2 == 0.0))
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            return false;
        }

        private void toolStripButtonExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.aisinoDataGrid1.get_Rows().Count == 0)
                {
                    MessageManager.ShowMsgBox("INP-274208");
                }
                else
                {
                    List<string> list = new List<string>();
                    for (int i = 0; i < this.aisinoDataGrid1.get_Columns().Count; i++)
                    {
                        list.Add(this.aisinoDataGrid1.get_Columns()[i].HeaderText);
                    }
                    SaveFileDialog dialog = new SaveFileDialog {
                        Filter = "Excel表格 (*.xls)|*.xls",
                        RestoreDirectory = true,
                        Title = "保存",
                        FilterIndex = 1,
                        CheckFileExists = false,
                        FileName = PropValue.InvExportXslPath
                    };
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        DataTable fpTable = this.fpcxBLL.QueryGetFaPiao(this.invoiceQueryCondition);
                        this.fpcxBLL.ExportExel(dialog.FileName, fpTable, "已开发票数据导出", this.list);
                        PropValue.InvExportXslPath = dialog.FileName;
                        MessageManager.ShowMsgBox("INP-274211");
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private void toolStripButtonExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void toolStripButtonMX_Click(object sender, EventArgs e)
        {
            this.Refresh();
            try
            {
                if (((this.aisinoDataGrid1.get_Rows().Count > 0) && (this.aisinoDataGrid1.get_SelectedRows().Count > 0)) && (this.aisinoDataGrid1.get_CurrentCell() != null))
                {
                    if (this.IsEmptyInvData(this.aisinoDataGrid1.get_CurrentCell(), string.Empty))
                    {
                        MessageManager.ShowMsgBox("空白作废发票不支持此操作！");
                    }
                    else
                    {
                        List<string[]> list = new List<string[]>();
                        string str = Convert.ToString(this.aisinoDataGrid1.get_CurrentCell().RowIndex);
                        int num = 0;
                        List<int> list2 = new List<int>();
                        foreach (DataGridViewRow row in (IEnumerable) this.aisinoDataGrid1.get_Rows())
                        {
                            string str2 = Convert.ToString(row.Cells["FPDM"].Value);
                            string str3 = Convert.ToString(row.Cells["FPHM"].Value);
                            string str4 = Convert.ToString(row.Cells["ZFBZ"].Value);
                            string str5 = Convert.ToString(row.Cells["YYSBZ"].Value);
                            if (str4.Trim().Equals("是"))
                            {
                                str4 = "1";
                            }
                            else
                            {
                                str4 = "0";
                            }
                            int rowIndex = row.Cells[0].RowIndex;
                            DataGridViewRow row2 = this.aisinoDataGrid1.get_Rows()[this.aisinoDataGrid1.get_CurrentCell().RowIndex];
                            string strInvType = row2.Cells["FPZL"].Value.ToString();
                            if (((strInvType == "普通发票") || (strInvType == "农产品销售发票")) || (strInvType == "收购发票"))
                            {
                                strInvType = "c";
                            }
                            else if (strInvType == "专用发票")
                            {
                                strInvType = "s";
                            }
                            else if (strInvType == "货物运输业增值税专用发票")
                            {
                                strInvType = "f";
                            }
                            else if (strInvType == "机动车销售统一发票")
                            {
                                strInvType = "j";
                            }
                            else
                            {
                                continue;
                            }
                            string[] item = new string[] { strInvType, str2, str3, str4, rowIndex.ToString(), str5 };
                            if (rowIndex == this.aisinoDataGrid1.get_CurrentCell().RowIndex)
                            {
                                str = (this.aisinoDataGrid1.get_CurrentCell().RowIndex - num).ToString();
                            }
                            if (!this.IsEmptyInvData(row.Cells["GFSH"], strInvType))
                            {
                                list2.Add(list.Count);
                                list.Add(item);
                            }
                            else
                            {
                                num++;
                                list2.Add(-1000);
                            }
                        }
                        if (list.Count > 0)
                        {
                            object[] objArray = new object[] { "0", str, list };
                            if (ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPMX", objArray) == null)
                            {
                                MessageManager.ShowMsgBox("INP-274210");
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.aisinoDataGrid1.Print("文本接口发票查询", this, null, null, true);
            }
            catch (Exception)
            {
            }
        }

        private void toolStripButtonStyle_Click(object sender, EventArgs e)
        {
            try
            {
                this.aisinoDataGrid1.SetColumnsStyle(this.xmlComponentLoader1.get_XMLPath(), this);
            }
            catch (Exception)
            {
            }
        }

        private void toolStripButtonXML_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.aisinoDataGrid1.get_Rows().Count == 0)
                {
                    MessageManager.ShowMsgBox("INP-274208");
                }
                else
                {
                    SaveFileDialog dialog = new SaveFileDialog {
                        Filter = "XML文件 (*.xml)|*.xml",
                        RestoreDirectory = true,
                        Title = "保存",
                        FilterIndex = 1,
                        CheckFileExists = false,
                        FileName = PropValue.InvExportXmlPath
                    };
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        DataTable fpTable = this.fpcxBLL.QueryGetFaPiaoxml(this.invoiceQueryCondition);
                        this.fpcxBLL.ExportXML(dialog.FileName, fpTable);
                        PropValue.InvExportXmlPath = dialog.FileName;
                        MessageManager.ShowMsgBox("INP-274209");
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }
    }
}

