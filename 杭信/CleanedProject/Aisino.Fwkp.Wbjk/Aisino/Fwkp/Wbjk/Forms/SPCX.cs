namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using Aisino.Fwkp.Wbjk.Common;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class SPCX : DockForm
    {
        private AisinoDataGrid aisinoDataGrid1;
        private AisinoBTN btnQuery;
        private AisinoBTN buttonCustomer;
        private AisinoBTN buttonGoods;
        private AisinoCHK ckbAllDate;
        private AisinoCHK ckbContainTadayE;
        private AisinoCHK ckbContainTadayS;
        private AisinoCMB comboBoxFPZL;
        private AisinoCMB comboBoxZFBZ;
        private IContainer components = null;
        private DateTimePicker dTPEnd;
        private DateTimePicker dTPStart;
        private AisinoGRP groupBox1;
        private InvoiceQueryCondition invoiceQueryCondition = new InvoiceQueryCondition();
        private AisinoLBL label1;
        private AisinoLBL label10;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoLBL label4;
        private AisinoLBL label5;
        private AisinoLBL label6;
        private AisinoLBL label7;
        private AisinoLBL label8;
        private AisinoLBL label9;
        private List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
        private ArrayList m_ArrayListCustomer = new ArrayList();
        private ArrayList m_ArrayListGood = new ArrayList();
        private const int nMaxSubStrCount = 0x200;
        private SPCXbll spcxBLL = new SPCXbll();
        private StatusStrip statusStrip1;
        private AisinoTXT textBoxFPDM;
        private AisinoTXT textBoxFPHM;
        private AisinoTXT textBoxGFMC;
        private AisinoTXT textBoxGFSH;
        private AisinoTXT textBoxGGXH;
        private AisinoTXT textBoxSPMC;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButtonExcel;
        private ToolStripButton toolStripButtonExit;
        private ToolStripButton toolStripButtonPrint;
        private ToolStripButton toolStripButtonStyle;
        private XmlComponentLoader xmlComponentLoader1;

        public SPCX()
        {
            this.Initialize();
        }

        private void aisinoDataGrid1_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            this.spcxBLL.Pagesize = e.get_PageSize();
            this.aisinoDataGrid1.set_DataSource(this.spcxBLL.QueryGoods(this.invoiceQueryCondition, e.get_PageSize(), e.get_PageNO()));
            int count = this.aisinoDataGrid1.get_Rows().Count;
            for (int i = 0; i < count; i++)
            {
                string str = this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value.ToString();
                string str2 = this.aisinoDataGrid1.get_Rows()[i].Cells["XXFP.SLV"].Value.ToString();
                if (((str != null) && (str != "")) && (str != "多税率"))
                {
                    string str3 = this.aisinoDataGrid1.get_Rows()[i].Cells["FPZL"].Value.ToString();
                    string str4 = this.aisinoDataGrid1.get_Rows()[i].Cells["YYSBZ"].Value.ToString();
                    if ((((str == "0.05") && (str4.Substring(8, 1) == "0")) && (str3 == "专用发票")) && (str2 != ""))
                    {
                        this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = "中外合作油气田";
                    }
                    else if (((CommonTool.isSPBMVersion() && (str == "0.00")) || ((str == "0.0") || (str == "0"))) || (str == "0%"))
                    {
                        string str5 = this.aisinoDataGrid1.get_Rows()[i].Cells["LSLVBS"].Value.ToString();
                        if (str5 == "0")
                        {
                            this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = "出口零税";
                        }
                        else if (str5 == "1")
                        {
                            this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = "免税";
                        }
                        else if (str5 == "2")
                        {
                            this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = "不征税";
                        }
                        else
                        {
                            this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = "0%";
                        }
                    }
                    else if (str == "0.015")
                    {
                        this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = "减按1.5%计算";
                    }
                    else
                    {
                        str = ((Convert.ToDouble(str) * 100.0)).ToString() + "%";
                        this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = str;
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
                string strSrc = this.textBoxFPHM.Text.Trim();
                if (this.textBoxFPHM.Text.Trim().Length == 8)
                {
                    strSrc = strSrc.TrimStart(new char[] { '0' });
                }
                this.invoiceQueryCondition.StrSplit(this.textBoxGFMC.Text.Trim(), ref this.invoiceQueryCondition.m_strBuyerNameList, ';', 0x200);
                this.invoiceQueryCondition.StrSplit(this.textBoxGFSH.Text.Trim(), ref this.invoiceQueryCondition.m_strBuyerCodeList, ';', 0x200);
                this.invoiceQueryCondition.StrSplit(this.textBoxFPDM.Text.Trim(), ref this.invoiceQueryCondition.m_strInvCodeList, ';', 0x200);
                this.invoiceQueryCondition.StrSplit(strSrc, ref this.invoiceQueryCondition.m_strInvNumList, ';', 0x200);
                this.invoiceQueryCondition.StrSplit(this.textBoxSPMC.Text.Trim(), ref this.invoiceQueryCondition.m_strGoodsNameList, ';', 0x200);
                this.invoiceQueryCondition.StrSplit(this.textBoxGGXH.Text.Trim(), ref this.invoiceQueryCondition.m_strStateList, ';', 0x200);
                this.invoiceQueryCondition.m_strInvType = this.comboBoxFPZL.SelectedValue.ToString();
                this.invoiceQueryCondition.m_strWasteFlag = this.comboBoxZFBZ.SelectedValue.ToString();
                if (!this.ckbAllDate.Checked)
                {
                    this.invoiceQueryCondition.m_dtStart = this.ckbContainTadayS.Checked ? this.dTPStart.Value : this.dTPStart.Value.AddDays(1.0);
                    this.invoiceQueryCondition.m_dtEnd = this.ckbContainTadayE.Checked ? this.dTPEnd.Value.AddDays(1.0) : this.dTPEnd.Value;
                }
                else
                {
                    this.invoiceQueryCondition.m_dtStart = Convert.ToDateTime("0001-01-01");
                    this.invoiceQueryCondition.m_dtEnd = Convert.ToDateTime("0001-01-01");
                }
                this.aisinoDataGrid1.set_DataSource(this.spcxBLL.QueryGoods(this.invoiceQueryCondition, this.spcxBLL.Pagesize, this.spcxBLL.CurrentPage));
                PropValue.AllDate = this.ckbAllDate.Checked;
                PropValue.ContainStartDay = this.ckbContainTadayS.Checked;
                PropValue.ContainEndDay = this.ckbContainTadayE.Checked;
                int count = this.aisinoDataGrid1.get_Rows().Count;
                for (int i = 0; i < count; i++)
                {
                    string str2 = this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value.ToString();
                    string str3 = this.aisinoDataGrid1.get_Rows()[i].Cells["XXFP.SLV"].Value.ToString();
                    if (((str2 != null) && (str2 != "")) && (str2 != "多税率"))
                    {
                        string str4 = this.aisinoDataGrid1.get_Rows()[i].Cells["FPZL"].Value.ToString();
                        string str5 = this.aisinoDataGrid1.get_Rows()[i].Cells["YYSBZ"].Value.ToString();
                        if ((((str2 == "0.05") && (str5.Substring(8, 1) == "0")) && (str4 == "专用发票")) && (str3 != ""))
                        {
                            this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = "中外合作油气田";
                        }
                        else if (str2 == "0.00")
                        {
                            string str6 = this.aisinoDataGrid1.get_Rows()[i].Cells["LSLVBS"].Value.ToString();
                            if (str6 == "0")
                            {
                                this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = "出口零税";
                            }
                            else if (str6 == "1")
                            {
                                this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = "免税";
                            }
                            else if (str6 == "2")
                            {
                                this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = "不征税";
                            }
                            else
                            {
                                this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = "0%";
                            }
                        }
                        else if (str2 == "0.015")
                        {
                            this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = "减按1.5%计算";
                        }
                        else
                        {
                            str2 = ((Convert.ToDouble(str2) * 100.0)).ToString() + "%";
                            this.aisinoDataGrid1.get_Rows()[i].Cells["SLV"].Value = str2;
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

        private void buttonCustomer_Click(object sender, EventArgs e)
        {
            AddCodeForm form = new AddCodeForm(this.m_ArrayListCustomer, false, this.comboBoxFPZL.SelectedValue.ToString()) {
                ShowInTaskbar = false,
                Text = "选择购方名称",
                strLabelSrc = "所有购方名称",
                strLabelDest = "已选购方名称"
            };
            form.ShowDialog();
            this.m_ArrayListCustomer = form.m_ArrayListDest;
            this.textBoxGFMC.Text = "";
            int num = 0;
            if (this.m_ArrayListCustomer.Count > 0)
            {
                while (num < this.m_ArrayListCustomer.Count)
                {
                    this.textBoxGFMC.Text = this.textBoxGFMC.Text + this.m_ArrayListCustomer[num].ToString();
                    if ((num + 1) != this.m_ArrayListCustomer.Count)
                    {
                        this.textBoxGFMC.Text = this.textBoxGFMC.Text + ";";
                    }
                    num++;
                }
            }
        }

        private void buttonGoods_Click(object sender, EventArgs e)
        {
            AddCodeForm form = new AddCodeForm(this.m_ArrayListGood, true, this.comboBoxFPZL.SelectedValue.ToString()) {
                ShowInTaskbar = false,
                Text = "选择商品名称",
                strLabelSrc = "所有商品名称",
                strLabelDest = "已选商品名称"
            };
            form.ShowDialog();
            this.m_ArrayListGood = form.m_ArrayListDest;
            this.textBoxSPMC.Text = "";
            int num = 0;
            if (this.m_ArrayListGood.Count > 0)
            {
                while (num < this.m_ArrayListGood.Count)
                {
                    this.textBoxSPMC.Text = this.textBoxSPMC.Text + this.m_ArrayListGood[num].ToString();
                    if ((num + 1) != this.m_ArrayListGood.Count)
                    {
                        this.textBoxSPMC.Text = this.textBoxSPMC.Text + ";";
                    }
                    num++;
                }
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

        private void Initialize()
        {
            this.InitializeComponent();
            this.buttonCustomer = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("buttonCustomer");
            this.buttonCustomer.Click += new EventHandler(this.buttonCustomer_Click);
            this.buttonGoods = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("buttonGoods");
            this.buttonGoods.Click += new EventHandler(this.buttonGoods_Click);
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
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
            this.ckbAllDate = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("ckbAllDate");
            this.ckbContainTadayS = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("ckbContainTadayS");
            this.textBoxGGXH = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxGGXH");
            this.textBoxSPMC = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxSPMC");
            this.textBoxGFSH = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxGFSH");
            this.textBoxGFMC = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("textBoxGFMC");
            this.label6 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label6");
            this.label5 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label5");
            this.dTPEnd = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("dTPEnd");
            this.dTPStart = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("dTPStart");
            this.label4 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label4");
            this.label3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label3");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.aisinoDataGrid1 = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoDataGrid1");
            this.statusStrip1 = this.xmlComponentLoader1.GetControlByName<StatusStrip>("statusStrip1");
            this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
            this.ckbAllDate.CheckedChanged += new EventHandler(this.ckbAllDate_CheckedChanged);
            this.aisinoDataGrid1.add_GoToPageEvent(new EventHandler<GoToPageEventArgs>(this.aisinoDataGrid1_GoToPageEvent));
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.toolStripButtonExit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonExit");
            this.toolStripButtonExit.Click += new EventHandler(this.toolStripButtonExit_Click);
            this.toolStripButtonPrint = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonPrint");
            this.toolStripButtonPrint.Click += new EventHandler(this.toolStripButtonPrint_Click);
            this.toolStripButtonStyle = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonStyle");
            this.toolStripButtonStyle.Click += new EventHandler(this.toolStripButtonStyle_Click);
            this.toolStripButtonExcel = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("toolStripButtonExcel");
            this.toolStripButtonExcel.Click += new EventHandler(this.toolStripButtonExcel_Click);
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
            this.xmlComponentLoader1.Size = new Size(0x361, 0x220);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Wbjk.SPCX\Aisino.Fwkp.Wbjk.SPCX.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x361, 0x220);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "SPCX";
            base.set_TabText("商品查询");
            this.Text = "商品查询";
            base.Load += new EventHandler(this.SPCX_Load);
            base.ResumeLayout(false);
        }

        private void SPCX_Load(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "分页序号");
                item.Add("Property", "ROWNUMBER");
                item.Add("Type", "Text");
                item.Add("Width", "100");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "False");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "发票明细序号");
                item.Add("Property", "FPMXXH");
                item.Add("Type", "Text");
                item.Add("Width", "100");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                item.Add("Visible", "false");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "销售单据编号");
                item.Add("Property", "XSDJBH");
                item.Add("Type", "Text");
                item.Add("Width", "120");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "商品名称");
                item.Add("Property", "SPMC");
                item.Add("Type", "Text");
                item.Add("HeadAlign", "MiddleLeft");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "规格型号");
                item.Add("Property", "GGXH");
                item.Add("Type", "Text");
                item.Add("HeadAlign", "MiddleLeft");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "计量单位");
                item.Add("Property", "JLDW");
                item.Add("Type", "Text");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleLeft");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "单价");
                item.Add("Property", "DJ");
                item.Add("Type", "Text");
                item.Add("Width", "60");
                item.Add("Align", "MiddleRight");
                item.Add("HeadAlign", "MiddleLeft");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "数量");
                item.Add("Property", "SL");
                item.Add("Type", "Text");
                item.Add("Width", "60");
                item.Add("Align", "MiddleRight");
                item.Add("HeadAlign", "MiddleLeft");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "金额");
                item.Add("Property", "JE");
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
                item.Add("Align", "MiddleRight");
                item.Add("HeadAlign", "MiddleLeft");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "税额");
                item.Add("Property", "SE");
                item.Add("Type", "Text");
                item.Add("Width", "60");
                item.Add("Align", "MiddleRight");
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
                item.Add("AisinoLBL", "商品税目");
                item.Add("Property", "SPSM");
                item.Add("Type", "Text");
                item.Add("Width", "100");
                item.Add("Align", "MiddleLeft");
                item.Add("HeadAlign", "MiddleCenter");
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
                item.Add("AisinoLBL", "开票日期");
                item.Add("Property", "KPRQ");
                item.Add("Type", "Text");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "作废标志");
                item.Add("Property", "ZFBZ");
                item.Add("Type", "Text");
                item.Add("Width", "80");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "开票机号");
                item.Add("Property", "KPJH");
                item.Add("Type", "Text");
                item.Add("Align", "MiddleCenter");
                item.Add("Width", "80");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "营业税标致");
                item.Add("Property", "YYSBZ");
                item.Add("Type", "Text");
                item.Add("Align", "MiddleCenter");
                item.Add("Width", "80");
                item.Add("Visible", "False");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "零税率标识");
                item.Add("Property", "LSLVBS");
                item.Add("Type", "Text");
                item.Add("Align", "MiddleCenter");
                item.Add("Width", "80");
                item.Add("Visible", "False");
                this.list.Add(item);
                item = new Dictionary<string, string>();
                item.Add("AisinoLBL", "发票税率");
                item.Add("Property", "XXFP.SLV");
                item.Add("Type", "Text");
                item.Add("Align", "MiddleCenter");
                item.Add("Width", "80");
                item.Add("Visible", "False");
                this.list.Add(item);
                this.aisinoDataGrid1.set_ColumeHead(this.list);
                this.aisinoDataGrid1.get_Columns()["KPRQ"].DefaultCellStyle.Format = "yyyy-MM-dd";
                this.aisinoDataGrid1.get_Columns()["JE"].DefaultCellStyle.Format = "0.00";
                this.aisinoDataGrid1.get_Columns()["SE"].DefaultCellStyle.Format = "0.00";
                this.aisinoDataGrid1.get_Columns()["FPHM"].DefaultCellStyle.Format = new string('0', 8);
                this.aisinoDataGrid1.set_DataSource(new AisinoDataSet());
                this.comboBoxFPZL.DropDownStyle = ComboBoxStyle.DropDownList;
                this.comboBoxFPZL.DataSource = CbbXmlBind.ReadXmlNode("InvType", false);
                this.comboBoxFPZL.DisplayMember = "Value";
                this.comboBoxFPZL.ValueMember = "Key";
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
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
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
                    for (int i = 1; i < this.aisinoDataGrid1.get_Columns().Count; i++)
                    {
                        list.Add(this.aisinoDataGrid1.get_Columns()[i].HeaderText);
                    }
                    SaveFileDialog dialog = new SaveFileDialog {
                        Filter = "Excel表格 (*.xls)|*.xls",
                        Title = "保存",
                        RestoreDirectory = true,
                        FilterIndex = 1,
                        CheckFileExists = false,
                        FileName = PropValue.InvSPExportXslPath
                    };
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        DataTable fpTable = this.spcxBLL.QueryGetGoods(this.invoiceQueryCondition);
                        this.spcxBLL.ExportExel(dialog.FileName, fpTable, "已开发票商品数据导出", this.list);
                        PropValue.InvSPExportXslPath = dialog.FileName;
                        MessageManager.ShowMsgBox("INP-274212");
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

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.aisinoDataGrid1.Print("单据管理商品查询", this, null, null, true);
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
    }
}

