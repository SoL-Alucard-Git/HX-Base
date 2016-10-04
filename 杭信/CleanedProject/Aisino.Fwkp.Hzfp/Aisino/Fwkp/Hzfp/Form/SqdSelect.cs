namespace Aisino.Fwkp.Hzfp.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Hzfp.Common;
    using Aisino.Fwkp.Hzfp.Form.Common;
    using Aisino.Fwkp.Hzfp.IBLL;
    using Aisino.Fwkp.Hzfp.Model;
    using Factory;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Xml;

    public class SqdSelect : DockForm
    {
        private string _result = string.Empty;
        private AisinoDataGrid aisinoGrid;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip;
        private string exePath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Log\");
        private ILog loger = LogUtil.GetLogger<SqdSelect>();
        private int month;
        private FPProgressBar progressBar = new FPProgressBar();
        private readonly IHZFP_SQD sqdDal = BLLFactory.CreateInstant<IHZFP_SQD>("HZFP_SQD");
        private readonly IHZFP_SQD_MX sqdMxDal = BLLFactory.CreateInstant<IHZFP_SQD_MX>("HZFP_SQD_MX");
        private ToolStripButton tool_xiazai;
        private ToolStripButton tool_xuanze;
        private ToolStrip toolStrip1;
        private XmlComponentLoader xmlComponentLoader1;

        public SqdSelect()
        {
            this.Initialize();
            List<Dictionary<string, string>> list = SpecialInvInfoTableCommon.CreateGridHeader();
            this.aisinoGrid.ColumeHead=(list);
            DataGridViewColumn column = this.aisinoGrid.Columns["HJJE"];
            if (column != null)
            {
                column.DefaultCellStyle.Format = "0.00";
            }
            DataGridViewColumn column2 = this.aisinoGrid.Columns["HJSE"];
            if (column2 != null)
            {
                column2.DefaultCellStyle.Format = "0.00";
            }
            this.aisinoGrid.ReadOnly=(true);
            this.aisinoGrid.MultiSelect=(false);
            this.BindGridData();
        }

        private void aisinoGrid_DataGridRowDbClickEvent(object sender, DataGridRowEventArgs e)
        {
            if (this.aisinoGrid.SelectedRows.Count > 0)
            {
                this.tool_xuanze_Click(null, null);
            }
        }

        private void aisinoGrid_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            this.sqdDal.CurrentPage = e.PageNO;
            this.aisinoGrid.DataSource=(this.sqdDal.SelectSelList(e.PageNO, e.PageSize, this.month, base.TaxCardInstance.TaxCode.Trim()));
            PropertyUtil.SetValue("Aisino.Fwkp.Hzfp.pagesize", e.PageSize.ToString());
        }

        private void BindGridData()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("xfsh", "%" + base.TaxCardInstance.TaxCode + "%");
            if (SqdTianKai.oldsh.Trim() == "")
            {
                dict.Add("oldxfsh", "%" + base.TaxCardInstance.TaxCode + "%");
            }
            else
            {
                dict.Add("oldxfsh", "%" + SqdTianKai.oldsh + "%");
            }
            dict.Add("xxbzt", "%TZD0000%");
            dict.Add("xxbzt_yq", "%TZD1000%");
            this.sqdDal.CurrentPage = 1;
            this.aisinoGrid.DataSource=(this.sqdDal.SelectSqdradlist(this.sqdDal.CurrentPage, this.sqdDal.Pagesize, dict));
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
            this.tool_xuanze = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_xuanze");
            this.tool_xiazai = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_xiazai");
            this.aisinoGrid = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoGrid");
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.tool_xuanze.Click += new EventHandler(this.tool_xuanze_Click);
            this.tool_xiazai.Click += new EventHandler(this.tool_xiazai_Click);
            this.contextMenuStrip = new ContextMenuStrip();
            this.aisinoGrid.ReadOnly=(true);
            this.aisinoGrid.GoToPageEvent+=(new EventHandler<GoToPageEventArgs>(this.aisinoGrid_GoToPageEvent));
            this.aisinoGrid.DataGridRowDbClickEvent+=(new EventHandler<DataGridRowEventArgs>(this.aisinoGrid_DataGridRowDbClickEvent));
            this.aisinoGrid.DataGrid.AllowUserToDeleteRows = false;
            ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SqdSelect));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x2c2, 0x21a);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.XMLPath=(@"..\Config\Components\Aisino.Fwkp.Hzfp.Form.SqdSelect\Aisino.Fwkp.Hzfp.Form.SqdSelect.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2c2, 0x21a);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "SqdSelect";
            this.Text = "信息表选择";
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "信息表选择";
            base.ResumeLayout(false);
        }

        private void PerformStep(int step)
        {
            for (int i = 0; i < step; i++)
            {
                if ((this.progressBar.fpxf_progressBar.Value + 1) > this.progressBar.fpxf_progressBar.Maximum)
                {
                    this.progressBar.fpxf_progressBar.Value = this.progressBar.fpxf_progressBar.Maximum;
                }
                else
                {
                    this.progressBar.fpxf_progressBar.Value++;
                }
                this.progressBar.fpxf_progressBar.Refresh();
            }
        }

        private void ProccessBarShow(object obj)
        {
            try
            {
                int step = (int) obj;
                this.PerformStep(step);
            }
            catch (Exception exception)
            {
                this.loger.Error("[ThreadFun]" + exception.Message);
            }
        }

        public void ProcessStartThread(int value)
        {
            this.PerformStep(value);
        }

        private void tool_xiazai_Click(object sender, EventArgs e)
        {
            try
            {
                string str = string.Empty;
                string str2 = string.Empty;
                string str3 = string.Empty;
                string str4 = string.Empty;
                string str5 = string.Empty;
                string ghfsh = string.Empty;
                string xhfsh = string.Empty;
                string xxbbh = string.Empty;
                string xxbfw = string.Empty;
                string yqzt = string.Empty;
                string str11 = string.Empty;
                str11 = "1000";
                int num = 0;
                int num2 = 0;
                string str12 = string.Empty;
                int num3 = 0;
                int num4 = 0;
                string innerText = string.Empty;
                string str14 = string.Empty;
                string str15 = string.Empty;
                string sQDH = string.Empty;
                string str17 = string.Empty;
                string str18 = string.Empty;
                string str19 = string.Empty;
                str = base.TaxCardInstance.TaxCode;
                str2 = base.TaxCardInstance.GetInvControlNum().Trim();
                str3 = Convert.ToString(base.TaxCardInstance.Machine);
                XiaZaiTiaoJianKP nkp = new XiaZaiTiaoJianKP {
                    AisinoGrid = this.aisinoGrid
                };
                if (nkp.ShowDialog(this) == DialogResult.OK)
                {
                    str4 = nkp.DateTimeStart.ToString("yyyy-MM-dd");
                    str5 = nkp.DateTimeEnd.ToString("yyyy-MM-dd");
                    ghfsh = nkp.Ghfsh;
                    xhfsh = nkp.Xhfsh;
                    xxbbh = nkp.Xxbbh;
                    xxbfw = nkp.Xxbfw;
                    yqzt = nkp.Yqzt;
                    do
                    {
                        num++;
                        num2 = 0;
                        XmlDocument document = new XmlDocument();
                        XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                        document.PreserveWhitespace = false;
                        document.AppendChild(newChild);
                        XmlElement element = document.CreateElement("FPXT");
                        document.AppendChild(element);
                        XmlElement element2 = document.CreateElement("INPUT");
                        element.AppendChild(element2);
                        XmlElement element3 = document.CreateElement("NSRSBH");
                        element3.InnerText = str;
                        element2.AppendChild(element3);
                        XmlElement element4 = document.CreateElement("SBBH");
                        element4.InnerText = str2;
                        element2.AppendChild(element4);
                        XmlElement element5 = document.CreateElement("KPJH");
                        element5.InnerText = str3;
                        element2.AppendChild(element5);
                        XmlElement element6 = document.CreateElement("YQZT");
                        element6.InnerText = yqzt;
                        element2.AppendChild(element6);
                        XmlElement element7 = document.CreateElement("TKRQ_Q");
                        element7.InnerText = str4;
                        element2.AppendChild(element7);
                        XmlElement element8 = document.CreateElement("TKRQ_Z");
                        element8.InnerText = str5;
                        element2.AppendChild(element8);
                        XmlElement element9 = document.CreateElement("GFSH");
                        element9.InnerText = ghfsh;
                        element2.AppendChild(element9);
                        XmlElement element10 = document.CreateElement("XFSH");
                        element10.InnerText = xhfsh;
                        element2.AppendChild(element10);
                        XmlElement element11 = document.CreateElement("XXBBH");
                        element11.InnerText = xxbbh;
                        element2.AppendChild(element11);
                        XmlElement element12 = document.CreateElement("XXBFW");
                        element12.InnerText = xxbfw;
                        element2.AppendChild(element12);
                        XmlElement element13 = document.CreateElement("PAGENO");
                        element13.InnerText = num.ToString();
                        element2.AppendChild(element13);
                        XmlElement element14 = document.CreateElement("PAGESIZE");
                        element14.InnerText = str11;
                        element2.AppendChild(element14);
                        document.PreserveWhitespace = true;
                        document.Save(this.exePath + "xiazai_shuru_kp_" + num.ToString() + ".xml");
                        this.progressBar.SetTip("正在连接服务器", "请等待任务完成", "信息表下载中");
                        this.progressBar.fpxf_progressBar.Value = 1;
                        this.progressBar.Show();
                        this.progressBar.Refresh();
                        this.ProcessStartThread(0x1b58);
                        this.progressBar.Refresh();
                        string xml = string.Empty;
                        int num5 = HttpsSender.SendMsg("0010", document.InnerXml, out xml);
                        if (num5 != 0)
                        {
                            MessageManager.ShowMsgBox("INP-431370", new string[] { "下载", num5.ToString(), xml });
                        }
                        else if (xml == "")
                        {
                            MessageManager.ShowMsgBox("INP-431368", new string[] { "下载" });
                        }
                        else
                        {
                            this.progressBar.SetTip("正在下载信息表", "请等待任务完成", "信息表下载中");
                            XmlDocument document2 = new XmlDocument();
                            document2.LoadXml(xml);
                            document2.Save(this.exePath + "xiazai_shuchu_kp_" + num.ToString() + ".xml");
                            XmlDocument document3 = new XmlDocument();
                            document3.Load(this.exePath + "xiazai_shuchu_kp_" + num.ToString() + ".xml");
                            foreach (XmlNode node2 in document3.SelectSingleNode("FPXT").ChildNodes)
                            {
                                XmlElement element15 = (XmlElement) node2;
                                foreach (XmlNode node3 in element15.ChildNodes)
                                {
                                    if ("CODE".Equals(node3.Name))
                                    {
                                        innerText = node3.InnerText;
                                        innerText = innerText.Replace("\r\n", "");
                                        innerText = innerText.Trim();
                                    }
                                    else if ("MESS".Equals(node3.Name))
                                    {
                                        str14 = node3.InnerText;
                                        str14 = str14.Replace("\r\n", "");
                                        str14 = str14.Trim();
                                    }
                                    else if ("DATA".Equals(node3.Name))
                                    {
                                        if (innerText != "0000")
                                        {
                                            MessageManager.ShowMsgBox("INP-431334", new string[] { innerText + " " + str14 });
                                        }
                                        else
                                        {
                                            XmlElement element16 = (XmlElement) node3;
                                            foreach (XmlNode node4 in element16.ChildNodes)
                                            {
                                                if ("ALLCOUNT".Equals(node4.Name))
                                                {
                                                    str12 = node4.InnerText;
                                                    str12 = str12.Replace("\r\n", "");
                                                    str12 = str12.Trim();
                                                    num2 = Convert.ToInt32(str12);
                                                    int num1 = Convert.ToInt32(str12) / Convert.ToInt32(str11);
                                                }
                                                else if ("RedInvReqBill".Equals(node4.Name) && (Convert.ToDouble(str12) != 0.0))
                                                {
                                                    this.ProcessStartThread(0x1388 / num2);
                                                    this.progressBar.Refresh();
                                                    bool flag = false;
                                                    bool flag2 = true;
                                                    bool flag3 = true;
                                                    int num6 = 0;
                                                    HZFP_SQD model = new HZFP_SQD();
                                                    List<HZFP_SQD_MX> models = new List<HZFP_SQD_MX>();
                                                    model.FPZL = "s";
                                                    model.JBR = string.Empty;
                                                    model.SQLY = string.Empty;
                                                    model.SQRDH = string.Empty;
                                                    model.YYSBZ = "0000000010";
                                                    XmlElement element17 = (XmlElement) node4;
                                                    foreach (XmlNode node5 in element17.ChildNodes)
                                                    {
                                                        string str24;
                                                        string str26;
                                                        string str28;
                                                        string str29;
                                                        string str30;
                                                        string innerXml = element17.SelectSingleNode("SellTaxCode").InnerXml;
                                                        if ("ReqBillNo".Equals(node5.Name))
                                                        {
                                                            string str21 = element17.SelectSingleNode("ReqMemo").InnerXml.Trim();
                                                            string str22 = element17.SelectSingleNode("BuyTaxCode").InnerXml.Trim();
                                                            string str23 = element17.SelectSingleNode("SellTaxCode").InnerXml.Trim();
                                                            sQDH = node5.InnerText.Trim();
                                                            str15 = sQDH.Substring(0, 12);
                                                            switch (str21)
                                                            {
                                                                case "Y":
                                                                case "N1":
                                                                case "N2":
                                                                case "N3":
                                                                case "N4":
                                                                {
                                                                    if (str22 == base.TaxCardInstance.TaxCode.Trim())
                                                                    {
                                                                        flag2 = true;
                                                                        Convert.ToString(base.TaxCardInstance.Machine);
                                                                        model.REQNSRSBH = str22;
                                                                        model.KPJH = base.TaxCardInstance.Machine;
                                                                        model.JSPH = str15;
                                                                        if (this.sqdDal.Select(sQDH) == null)
                                                                        {
                                                                            flag3 = false;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        flag2 = false;
                                                                        flag = true;
                                                                        model.REQNSRSBH = str22;
                                                                        model.KPJH = 0;
                                                                        model.JSPH = str15;
                                                                    }
                                                                    continue;
                                                                }
                                                            }
                                                            flag2 = true;
                                                            flag = true;
                                                            num3++;
                                                            Convert.ToString(base.TaxCardInstance.Machine);
                                                            model.REQNSRSBH = str23;
                                                            model.KPJH = base.TaxCardInstance.Machine;
                                                            model.JSPH = str15;
                                                            if (this.sqdDal.Select(sQDH) == null)
                                                            {
                                                                flag3 = false;
                                                            }
                                                            continue;
                                                        }
                                                        string name = node5.Name;
                                                        if (name != null)
                                                        {
                                                            if (!(name == "ResBillNo"))
                                                            {
                                                                if (name == "StatusDM")
                                                                {
                                                                    goto Label_0947;
                                                                }
                                                                if (name == "StatusMC")
                                                                {
                                                                    goto Label_095C;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                str17 = node5.InnerText;
                                                            }
                                                        }
                                                        goto Label_0965;
                                                    Label_0947:
                                                        str18 = node5.InnerText;
                                                        if (flag)
                                                        {
                                                            num4++;
                                                        }
                                                        goto Label_0965;
                                                    Label_095C:
                                                        str19 = node5.InnerText;
                                                    Label_0965:
                                                        if (flag2 && flag3)
                                                        {
                                                            continue;
                                                        }
                                                        model.SQDH = sQDH;
                                                        model.XXBBH = str17;
                                                        model.XXBZT = str18;
                                                        model.XXBMS = str19;
                                                        switch (node5.Name)
                                                        {
                                                            case "BillType":
                                                                model.BBBZ = node5.InnerText;
                                                                goto Label_1069;

                                                            case "TypeCode":
                                                                str24 = node5.InnerText.Replace("\r\n", "").Trim();
                                                                if (Convert.ToDouble(str24) != 0.0)
                                                                {
                                                                    break;
                                                                }
                                                                model.FPDM = "";
                                                                goto Label_1069;

                                                            case "InvNo":
                                                            {
                                                                string str25 = node5.InnerText.Replace("\r\n", "").Trim();
                                                                if (!(str25 != ""))
                                                                {
                                                                    goto Label_0B72;
                                                                }
                                                                model.FPHM = Convert.ToInt32(str25);
                                                                goto Label_1069;
                                                            }
                                                            case "Date":
                                                            {
                                                                DateTime now = DateTime.Now;
                                                                DateTime.TryParse(Convert.ToDateTime(node5.InnerText.Trim()).Date.ToString("yyyy-MM-dd").ToString().Trim(), out now);
                                                                model.TKRQ = now;
                                                                model.SSYF = Convert.ToInt32(model.TKRQ.ToString("yyyyMM"));
                                                                goto Label_1069;
                                                            }
                                                            case "BuyerName":
                                                                model.GFMC = node5.InnerText;
                                                                goto Label_1069;

                                                            case "BuyTaxCode":
                                                                model.GFSH = node5.InnerText;
                                                                goto Label_1069;

                                                            case "SellerName":
                                                                model.XFMC = node5.InnerText;
                                                                goto Label_1069;

                                                            case "SellTaxCode":
                                                                node5.InnerText.Trim();
                                                                model.XFSH = node5.InnerText;
                                                                goto Label_1069;

                                                            case "Amount":
                                                                model.HJJE = Convert.ToDecimal(node5.InnerText);
                                                                goto Label_1069;

                                                            case "TaxRate":
                                                                str26 = node5.InnerText.Replace("\r\n", "").Trim();
                                                                if ((!(str26 == "null") && (str26 != null)) && !(str26 == ""))
                                                                {
                                                                    goto Label_0CBA;
                                                                }
                                                                model.SL = -1.0;
                                                                goto Label_1069;

                                                            case "Tax":
                                                            {
                                                                string str27 = node5.InnerText.Replace("\r\n", "").Trim();
                                                                model.HJSE = Convert.ToDecimal(str27);
                                                                goto Label_1069;
                                                            }
                                                            case "ReqMemo":
                                                                str28 = node5.InnerText.Replace("\r\n", "").Trim();
                                                                str29 = string.Empty;
                                                                if (base.TaxCardInstance.StateInfo.CompanyType == 0)
                                                                {
                                                                    goto Label_0D4C;
                                                                }
                                                                str29 = "1";
                                                                goto Label_0D53;

                                                            case "SPBMBBH":
                                                                if (SqdTianKai.isFLBM && "SPBMBBH".Equals(node5.Name))
                                                                {
                                                                    model.FLBMBBBH = node5.InnerText;
                                                                }
                                                                goto Label_1069;

                                                            case "SLBZ":
                                                                str30 = node5.InnerText.Replace("\r\n", "").Trim();
                                                                if (!str30.Equals("1") || (model.SL != 0.05))
                                                                {
                                                                    goto Label_1041;
                                                                }
                                                                model.YYSBZ = "0000000000";
                                                                goto Label_1069;

                                                            default:
                                                                goto Label_1069;
                                                        }
                                                        model.FPDM = str24;
                                                        goto Label_1069;
                                                    Label_0B72:
                                                        model.FPHM = 0;
                                                        goto Label_1069;
                                                    Label_0CBA:
                                                        model.SL = Convert.ToDouble(str26);
                                                        goto Label_1069;
                                                    Label_0D4C:
                                                        str29 = "0";
                                                    Label_0D53:
                                                        switch (str28)
                                                        {
                                                            case "Y":
                                                                model.SQXZ = "1100000000" + str29;
                                                                goto Label_1069;

                                                            case "N1":
                                                                model.SQXZ = "1011000000" + str29;
                                                                goto Label_1069;

                                                            case "N2":
                                                                model.SQXZ = "1010100000" + str29;
                                                                goto Label_1069;

                                                            case "N3":
                                                                model.SQXZ = "1010010000" + str29;
                                                                goto Label_1069;

                                                            case "N4":
                                                                model.SQXZ = "1010001000" + str29;
                                                                goto Label_1069;

                                                            case "N5":
                                                                model.SQXZ = "0000000110" + str29;
                                                                goto Label_1069;

                                                            case "N6":
                                                                model.SQXZ = "0000000101" + str29;
                                                                goto Label_1069;

                                                            case "X1":
                                                                model.SQXZ = "10000000";
                                                                goto Label_1069;

                                                            case "X2":
                                                                model.SQXZ = "01000000";
                                                                goto Label_1069;

                                                            case "X3":
                                                                model.SQXZ = "00100000";
                                                                goto Label_1069;

                                                            case "X4":
                                                                model.SQXZ = "00010000";
                                                                goto Label_1069;

                                                            case "X5":
                                                                model.SQXZ = "00001000";
                                                                goto Label_1069;

                                                            case "X6":
                                                                model.SQXZ = "00000100";
                                                                goto Label_1069;

                                                            case "X7":
                                                                model.SQXZ = "00000010";
                                                                goto Label_1069;

                                                            case "X8":
                                                                model.SQXZ = "00000001";
                                                                goto Label_1069;

                                                            default:
                                                                goto Label_1069;
                                                        }
                                                    Label_1041:
                                                        if (str30.Equals("2"))
                                                        {
                                                            model.YYSBZ = "0000000020";
                                                        }
                                                        else
                                                        {
                                                            model.YYSBZ = "0000000010";
                                                        }
                                                    Label_1069:
                                                        if ("RedInvReqBillMx".Equals(node5.Name))
                                                        {
                                                            XmlElement element18 = (XmlElement) node5;
                                                            foreach (XmlNode node6 in element18.ChildNodes)
                                                            {
                                                                if ("GoodsMx".Equals(node6.Name))
                                                                {
                                                                    num6++;
                                                                    HZFP_SQD_MX item = new HZFP_SQD_MX {
                                                                        SQDH = sQDH,
                                                                        MXXH = num6,
                                                                        SPSM = string.Empty,
                                                                        SPBH = string.Empty,
                                                                        FPHXZ = 0,
                                                                        XTHASH = string.Empty
                                                                    };
                                                                    XmlElement element19 = (XmlElement) node6;
                                                                    foreach (XmlNode node7 in element19.ChildNodes)
                                                                    {
                                                                        string str33;
                                                                        string str34;
                                                                        string str36;
                                                                        switch (node7.Name)
                                                                        {
                                                                            case "GoodsName":
                                                                            {
                                                                                item.SPMC = node7.InnerText.Replace("\r\n", "").Trim();
                                                                                continue;
                                                                            }
                                                                            case "GoodsUnit":
                                                                            {
                                                                                string str32 = node7.InnerText.Replace("\r\n", "").Trim();
                                                                                item.JLDW = str32;
                                                                                continue;
                                                                            }
                                                                            case "GoodsPrice":
                                                                            {
                                                                                str33 = node7.InnerText.Replace("\r\n", "").Trim();
                                                                                if ((!(str33 == "") && !(str33 == "0")) && ((!(str33 == "0.0") && !(str33 == "null")) && (str33 != null)))
                                                                                {
                                                                                    break;
                                                                                }
                                                                                item.DJ = 0M;
                                                                                continue;
                                                                            }
                                                                            case "GoodsTaxRate":
                                                                            {
                                                                                str34 = node7.InnerText.Replace("\r\n", "").Trim();
                                                                                if ((!(str34 == "") && (str34 != null)) && !(str34 == "null"))
                                                                                {
                                                                                    goto Label_13AD;
                                                                                }
                                                                                item.SLV = -1.0;
                                                                                continue;
                                                                            }
                                                                            case "GoodsGgxh":
                                                                            {
                                                                                string str35 = node7.InnerText.Replace("\r\n", "").Trim();
                                                                                item.GGXH = str35;
                                                                                continue;
                                                                            }
                                                                            case "GoodsNum":
                                                                            {
                                                                                str36 = node7.InnerText.Replace("\r\n", "").Trim();
                                                                                if ((!(str36 == "") && !(str36 == "0")) && ((!(str36 == "0.0") && !(str36 == "null")) && (str36 != null)))
                                                                                {
                                                                                    goto Label_1469;
                                                                                }
                                                                                item.SL = 0.0;
                                                                                continue;
                                                                            }
                                                                            case "GoodsJE":
                                                                            {
                                                                                item.JE = Convert.ToDecimal(node7.InnerText);
                                                                                continue;
                                                                            }
                                                                            case "GoodsSE":
                                                                            {
                                                                                string str37 = node7.InnerText.Replace("\r\n", "").Trim();
                                                                                item.SE = Convert.ToDecimal(str37);
                                                                                continue;
                                                                            }
                                                                            case "HS_BZ":
                                                                            {
                                                                                item.HSJBZ = node7.InnerText != "N";
                                                                                continue;
                                                                            }
                                                                            case "SPBM":
                                                                            {
                                                                                if (SqdTianKai.isFLBM)
                                                                                {
                                                                                    item.FLBM = node7.InnerText;
                                                                                }
                                                                                continue;
                                                                            }
                                                                            case "QYSPBM":
                                                                            {
                                                                                if (SqdTianKai.isFLBM)
                                                                                {
                                                                                    item.QYSPBM = node7.InnerText;
                                                                                }
                                                                                continue;
                                                                            }
                                                                            case "SYYHZCBZ":
                                                                            {
                                                                                if (SqdTianKai.isFLBM)
                                                                                {
                                                                                    item.SFXSYHZC = node7.InnerText;
                                                                                }
                                                                                continue;
                                                                            }
                                                                            case "YHZC":
                                                                            {
                                                                                if (SqdTianKai.isFLBM)
                                                                                {
                                                                                    item.YHZCMC = node7.InnerText;
                                                                                }
                                                                                continue;
                                                                            }
                                                                            case "LSLBZ":
                                                                            {
                                                                                if (SqdTianKai.isFLBM)
                                                                                {
                                                                                    item.LSLBS = node7.InnerText;
                                                                                }
                                                                                continue;
                                                                            }
                                                                            default:
                                                                            {
                                                                                continue;
                                                                            }
                                                                        }
                                                                        item.DJ = Convert.ToDecimal(str33);
                                                                        continue;
                                                                    Label_13AD:
                                                                        item.SLV = Convert.ToDouble(str34);
                                                                        continue;
                                                                    Label_1469:
                                                                        item.SL = Convert.ToDouble(str36);
                                                                    }
                                                                    models.Add(item);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (flag2 && flag3)
                                                    {
                                                        HZFP_SQD hzfp_sqd4 = this.sqdDal.Select(sQDH);
                                                        if (hzfp_sqd4 != null)
                                                        {
                                                            hzfp_sqd4.XXBBH = str17;
                                                            hzfp_sqd4.XXBZT = str18;
                                                            hzfp_sqd4.XXBMS = str19;
                                                            this.sqdDal.Updatazt(hzfp_sqd4);
                                                        }
                                                        else
                                                        {
                                                            MessageManager.ShowMsgBox("此信息表流水号在本地数据库中不存在");
                                                        }
                                                    }
                                                    else if (this.sqdDal.Select(sQDH) == null)
                                                    {
                                                        this.sqdDal.Insert(model);
                                                        this.sqdMxDal.Delete(sQDH);
                                                        this.sqdMxDal.Insert(models);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    while (num2 >= Convert.ToInt32(str11));
                }
                this.progressBar.SetTip("正在下载信息表", "请等待任务完成", "信息表下载中");
                this.progressBar.Visible = false;
                this.BindGridData();
                for (int i = 0; i < num; i++)
                {
                    if (File.Exists(Path.Combine(new string[] { this.exePath + "xiazai_shuru_kp_" + num.ToString() + ".xml" })))
                    {
                        File.Delete(Path.Combine(new string[] { this.exePath + "xiazai_shuru_kp_" + num.ToString() + ".xml" }));
                    }
                    if (File.Exists(Path.Combine(new string[] { this.exePath + "xiazai_shuchu_kp_" + num.ToString() + ".xml" })))
                    {
                        File.Delete(Path.Combine(new string[] { this.exePath + "xiazai_shuchu_kp_" + num.ToString() + ".xml" }));
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[信息表下载]" + exception.Message);
            }
            finally
            {
                if (this.progressBar != null)
                {
                    this.progressBar.Visible = false;
                }
            }
        }

        private void tool_xuanze_Click(object sender, EventArgs e)
        {
            if (this.aisinoGrid.CurrentCell == null)
            {
                MessageManager.ShowMsgBox("INP-431331");
            }
            else
            {
                string sQDH = this.aisinoGrid.Rows[this.aisinoGrid.CurrentCell.RowIndex].Cells["SQDH"].Value.ToString();
                HZFP_SQD hzfp_sqd = this.sqdDal.Select(sQDH);
                if (hzfp_sqd != null)
                {
                    XmlDocument document = new XmlDocument();
                    XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                    document.PreserveWhitespace = false;
                    document.AppendChild(newChild);
                    XmlElement element = document.CreateElement("FPXT");
                    document.AppendChild(element);
                    XmlElement element2 = document.CreateElement("OUTPUT");
                    element.AppendChild(element2);
                    XmlElement element3 = document.CreateElement("DATA");
                    element2.AppendChild(element3);
                    XmlNode node = document.CreateElement("RedInvReqBill");
                    element3.AppendChild(node);
                    XmlElement element4 = document.CreateElement("ReqBillNo");
                    element4.InnerText = hzfp_sqd.SQDH;
                    node.AppendChild(element4);
                    XmlElement element5 = document.CreateElement("ResBillNo");
                    element5.InnerText = hzfp_sqd.XXBBH;
                    node.AppendChild(element5);
                    XmlElement element6 = document.CreateElement("StatusDM");
                    element6.InnerText = hzfp_sqd.XXBZT;
                    node.AppendChild(element6);
                    XmlElement element7 = document.CreateElement("StatusMC");
                    element7.InnerText = hzfp_sqd.XXBMS;
                    node.AppendChild(element7);
                    XmlElement element8 = document.CreateElement("BillType");
                    element8.InnerText = hzfp_sqd.BBBZ;
                    node.AppendChild(element8);
                    XmlElement element9 = document.CreateElement("TypeCode");
                    element9.InnerText = hzfp_sqd.FPDM.ToString();
                    node.AppendChild(element9);
                    XmlElement element10 = document.CreateElement("InvNo");
                    element10.InnerText = string.Format("{0:00000000}", Convert.ToInt32(hzfp_sqd.FPHM.ToString()));
                    node.AppendChild(element10);
                    XmlElement element11 = document.CreateElement("Szlb");
                    element11.InnerText = "1";
                    node.AppendChild(element11);
                    object sL = hzfp_sqd.SL;
                    short num = 0;
                    if (sL.ToString() == "多税率")
                    {
                        num = 1;
                    }
                    XmlElement element12 = document.CreateElement("IsMutiRate");
                    element12.InnerText = num.ToString();
                    node.AppendChild(element12);
                    XmlElement element13 = document.CreateElement("Date");
                    element13.InnerText = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(hzfp_sqd.TKRQ.ToString()));
                    node.AppendChild(element13);
                    XmlElement element14 = document.CreateElement("BuyerName");
                    element14.InnerText = hzfp_sqd.GFMC.ToString();
                    node.AppendChild(element14);
                    XmlElement element15 = document.CreateElement("BuyTaxCode");
                    element15.InnerText = hzfp_sqd.GFSH.ToString();
                    node.AppendChild(element15);
                    XmlElement element16 = document.CreateElement("SellerName");
                    element16.InnerText = hzfp_sqd.XFMC.ToString();
                    node.AppendChild(element16);
                    XmlElement element17 = document.CreateElement("SellTaxCode");
                    element17.InnerText = hzfp_sqd.XFSH.ToString();
                    node.AppendChild(element17);
                    XmlElement element18 = document.CreateElement("Amount");
                    element18.InnerText = string.Format("{0:0.00}", Convert.ToDouble(hzfp_sqd.HJJE.ToString()));
                    node.AppendChild(element18);
                    XmlElement element19 = document.CreateElement("TaxRate");
                    if (num == 1)
                    {
                        element19.InnerText = "";
                    }
                    else
                    {
                        element19.InnerText = sL.ToString();
                    }
                    node.AppendChild(element19);
                    XmlElement element20 = document.CreateElement("Tax");
                    element20.InnerText = string.Format("{0:0.00}", Convert.ToDouble(hzfp_sqd.HJSE.ToString()));
                    node.AppendChild(element20);
                    XmlElement element21 = document.CreateElement("ReqMemo");
                    string str2 = hzfp_sqd.SQXZ.ToString();
                    if (str2.Length > 10)
                    {
                        str2 = str2.Substring(0, 10);
                    }
                    element21.InnerText = str2;
                    node.AppendChild(element21);
                    if (SqdTianKai.isFLBM)
                    {
                        XmlElement element22 = document.CreateElement("SPBMBBH");
                        element22.InnerText = hzfp_sqd.FLBMBBBH.ToString();
                        node.AppendChild(element22);
                    }
                    XmlElement element23 = document.CreateElement("SLBZ");
                    if ((hzfp_sqd.SL.ToString().Trim() == "0.015") || ((hzfp_sqd.YYSBZ.Trim().Substring(8, 1) == "0") && ((hzfp_sqd.SL.ToString().Trim() == "0.05") || (hzfp_sqd.SL.ToString().Trim() == "0.050"))))
                    {
                        element23.InnerText = "1";
                    }
                    else if (hzfp_sqd.YYSBZ.Trim().Substring(8, 1) == "2")
                    {
                        element23.InnerText = "2";
                    }
                    else
                    {
                        element23.InnerText = "0";
                    }
                    node.AppendChild(element23);
                    XmlElement element24 = document.CreateElement("RedInvReqBillMx");
                    node.AppendChild(element24);
                    DataTable table = this.sqdMxDal.SelectList(sQDH);
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DataRow row = table.Rows[i];
                        XmlElement element25 = document.CreateElement("GoodsMx");
                        element24.AppendChild(element25);
                        XmlElement element26 = document.CreateElement("GoodsName");
                        element26.InnerText = GetSafeData.ValidateValue<string>(row, "SPMC");
                        element25.AppendChild(element26);
                        XmlElement element27 = document.CreateElement("GoodsUnit");
                        element27.InnerText = GetSafeData.ValidateValue<string>(row, "JLDW");
                        element25.AppendChild(element27);
                        XmlElement element28 = document.CreateElement("GoodsPrice");
                        element28.InnerText = GetSafeData.ValidateValue<string>(row, "DJ");
                        element25.AppendChild(element28);
                        XmlElement element29 = document.CreateElement("GoodsTaxRate");
                        element29.InnerText = GetSafeData.ValidateDoubleValue(row, "SLV");
                        element25.AppendChild(element29);
                        XmlElement element30 = document.CreateElement("GoodsGgxh");
                        element30.InnerText = GetSafeData.ValidateValue<string>(row, "GGXH");
                        element25.AppendChild(element30);
                        XmlElement element31 = document.CreateElement("GoodsNum");
                        element31.InnerText = Convert.ToString(GetSafeData.ValidateValue<string>(row, "SL"));
                        element25.AppendChild(element31);
                        XmlElement element32 = document.CreateElement("GoodsJE");
                        element32.InnerText = string.Format("{0:0.00}", GetSafeData.ValidateValue<decimal>(row, "JE"));
                        element25.AppendChild(element32);
                        XmlElement element33 = document.CreateElement("GoodsSE");
                        element33.InnerText = string.Format("{0:0.00}", GetSafeData.ValidateValue<decimal>(row, "SE"));
                        element25.AppendChild(element33);
                        XmlElement element34 = document.CreateElement("HS_BZ");
                        bool flag = GetSafeData.ValidateValue<bool>(row, "HSJBZ");
                        element34.InnerText = !flag ? "N" : "Y";
                        element25.AppendChild(element34);
                        if (SqdTianKai.isFLBM)
                        {
                            XmlElement element35 = document.CreateElement("SPBM");
                            element35.InnerText = GetSafeData.ValidateValue<string>(row, "FLBM");
                            element25.AppendChild(element35);
                            XmlElement element36 = document.CreateElement("QYSPBM");
                            element36.InnerText = GetSafeData.ValidateValue<string>(row, "QYSPBM");
                            element25.AppendChild(element36);
                            XmlElement element37 = document.CreateElement("SYYHZCBZ");
                            element37.InnerText = GetSafeData.ValidateValue<string>(row, "SFXSYHZC");
                            element25.AppendChild(element37);
                            XmlElement element38 = document.CreateElement("YHZC");
                            element38.InnerText = GetSafeData.ValidateValue<string>(row, "YHZCMC");
                            element25.AppendChild(element38);
                            XmlElement element39 = document.CreateElement("LSLBZ");
                            element39.InnerText = GetSafeData.ValidateValue<string>(row, "LSLBS");
                            element25.AppendChild(element39);
                        }
                    }
                    document.PreserveWhitespace = true;
                    document.Save(this.exePath + "xuanze.xml");
                    this._result = document.InnerXml;
                }
                base.DialogResult = DialogResult.OK;
                base.Close();
                if (File.Exists(Path.Combine(this.exePath, "xuanze.xml")))
                {
                    File.Delete(Path.Combine(this.exePath, "xuanze.xml"));
                }
            }
        }

        public string Result
        {
            get
            {
                return this._result;
            }
            set
            {
                this._result = value;
            }
        }

        private delegate void PerformStepHandle(int step);
    }
}

