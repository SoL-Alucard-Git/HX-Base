namespace Aisino.Fwkp.HzfpHy.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.HzfpHy.Common;
    using Aisino.Fwkp.HzfpHy.Form.Common;
    using Aisino.Fwkp.HzfpHy.IBLL;
    using Aisino.Fwkp.HzfpHy.Model;
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

    public class HySqdSelect : DockForm
    {
        private string _result = string.Empty;
        private AisinoDataGrid aisinoGrid;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip;
        private string exePath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Log\");
        private ILog loger = LogUtil.GetLogger<HySqdSelect>();
        private FPProgressBar progressBar = new FPProgressBar();
        private string queryGfmc = "";
        private string queryGfsh = "";
        private string queryJsrq = "2099-12-31";
        private string queryKsrq = "2000-01-01";
        private string queryRetrieve = string.Empty;
        private string querySqdh = "";
        private string queryState = "WTK00";
        private string queryXfmc = "";
        private string queryXfsh = "";
        private readonly IHZFPHY_SQD sqdDal = BLLFactory.CreateInstant<IHZFPHY_SQD>("HZFPHY_SQD");
        private readonly IHZFPHY_SQD_MX sqdMxDal = BLLFactory.CreateInstant<IHZFPHY_SQD_MX>("HZFPHY_SQD_MX");
        private ToolStripButton tool_xiazai;
        private ToolStripButton tool_xuanze;
        private ToolStrip toolStrip;
        private XmlComponentLoader xmlComponentLoader1;

        public HySqdSelect()
        {
            this.Initialize();
            this.ResetPagesize();
            List<Dictionary<string, string>> list = TransportInvInfoTableCommon.CreateGridHeader();
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
            this.BindGridData();
        }

        private void aisinoGrid_DataGridRowClickEvent(object sender, DataGridRowEventArgs e)
        {
        }

        private void aisinoGrid_DataGridRowDbClickEvent(object sender, EventArgs e)
        {
            this.tool_xuanze_Click(sender, e);
        }

        private void aisinoGrid_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            this.sqdDal.CurrentPage = e.PageNO;
            this.sqdDal.Pagesize = e.PageSize;
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("sqdh", "%" + this.querySqdh + "%");
            dict.Add("gfmc", "%" + this.queryGfmc + "%");
            dict.Add("gfsh", "%" + this.queryGfsh + "%");
            dict.Add("xfmc", "%" + this.queryXfmc + "%");
            dict.Add("xfsh", "%" + this.queryXfsh + "%");
            dict.Add("reqnsrsbh", base.TaxCardInstance.TaxCode.Trim());
            dict.Add("oldreqnsrsbh", HySqdTianKai.oldsh);
            dict.Add("ksrq", this.queryKsrq);
            dict.Add("jsrq", this.queryJsrq);
            dict.Add("xxbzt", this.queryState);
            dict.Add("bbbz", "2");
            this.aisinoGrid.DataSource=(this.sqdDal.SelectList(this.sqdDal.CurrentPage, this.sqdDal.Pagesize, dict));
            PropertyUtil.SetValue("Aisino.Fwkp.HzfpHy.HySqdSelect.aisinoGrid", this.sqdDal.Pagesize.ToString());
        }

        private void BindGridData()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("sqdh", "%" + this.querySqdh + "%");
            dict.Add("gfmc", "%" + this.queryGfmc + "%");
            dict.Add("gfsh", "%" + this.queryGfsh + "%");
            dict.Add("xfmc", "%" + this.queryXfmc + "%");
            dict.Add("xfsh", "%" + this.queryXfsh + "%");
            dict.Add("reqnsrsbh", base.TaxCardInstance.TaxCode.Trim());
            dict.Add("oldreqnsrsbh", HySqdTianKai.oldsh);
            dict.Add("ksrq", this.queryKsrq);
            dict.Add("jsrq", this.queryJsrq);
            dict.Add("xxbzt", this.queryState);
            dict.Add("bbbz", "2");
            dict.Add("rtrv", "%" + this.queryRetrieve + "%");
            this.sqdDal.CurrentPage = 1;
            this.aisinoGrid.DataSource=(this.sqdDal.SelectList(this.sqdDal.CurrentPage, this.sqdDal.Pagesize, dict));
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
            this.toolStrip = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.tool_xuanze = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_xuanze");
            this.tool_xiazai = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_xiazai");
            ControlStyleUtil.SetToolStripStyle(this.toolStrip);
            this.aisinoGrid = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoGrid");
            this.aisinoGrid.MultiSelect=(false);
            this.aisinoGrid.DataGrid.AllowUserToDeleteRows = false;
            this.tool_xuanze.Click += new EventHandler(this.tool_xuanze_Click);
            this.tool_xiazai.Click += new EventHandler(this.tool_xiazai_Click);
            this.contextMenuStrip = new ContextMenuStrip();
            this.aisinoGrid.GoToPageEvent+=(new EventHandler<GoToPageEventArgs>(this.aisinoGrid_GoToPageEvent));
            this.aisinoGrid.DataGridRowDbClickEvent+=(new EventHandler<DataGridRowEventArgs>(this.aisinoGrid_DataGridRowDbClickEvent));
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(HySqdSelect));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x2c2, 0x21a);
            this.xmlComponentLoader1.TabIndex = 1;
            this.xmlComponentLoader1.XMLPath=(@"..\Config\Components\Aisino.Fwkp.HzfpHy.Form.HySqdSelect\Aisino.Fwkp.HzfpHy.Form.HySqdSelect.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2c2, 0x21a);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Location = new Point(0, 0);
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Name = "HySqdSelect";
            this.Text = "红字货物运输业增值税专用发票信息表选择";
            base.ResumeLayout(false);
        }

        private void layoutToolRightToLeft()
        {
            this.toolStrip.RightToLeft = RightToLeft.Yes;
            this.tool_xuanze.RightToLeft = RightToLeft.No;
            this.tool_xiazai.RightToLeft = RightToLeft.No;
        }

        private string ParseReason(XmlElement SQF, XmlElement SFJXSEZC, string reason)
        {
            string str = string.Empty;
            if ((reason.Length > 11) || (reason.Length < 10))
            {
                return string.Empty;
            }
            if (reason.Length == 11)
            {
                reason = reason.Substring(0, 10);
            }
            if ("0".Equals(reason.Substring(0, 1)) && "0".Equals(reason.Substring(7, 1)))
            {
                return string.Empty;
            }
            if ("1".Equals(reason.Substring(0, 1)) && "1".Equals(reason.Substring(7, 1)))
            {
                return string.Empty;
            }
            if ("1".Equals(reason.Substring(0, 1)))
            {
                SQF.InnerText = "0";
                if ("1".Equals(reason.Substring(1, 1)))
                {
                    SFJXSEZC.InnerText = "0";
                    return str;
                }
                if ("1".Equals(reason.Substring(2, 1)))
                {
                    SFJXSEZC.InnerText = "1";
                    for (int i = 3; i < 7; i++)
                    {
                        if ("1".Equals(reason.Substring(i, 1)))
                        {
                            str = Convert.ToString((int) (i - 2));
                        }
                    }
                }
                return str;
            }
            if ("1".Equals(reason.Substring(7, 1)))
            {
                SQF.InnerText = "1";
                SFJXSEZC.InnerText = string.Empty;
                for (int j = 8; j < 10; j++)
                {
                    if ("1".Equals(reason.Substring(j, 1)))
                    {
                        str = Convert.ToString((int) (j - 2));
                    }
                }
            }
            return str;
        }

        private string ParseReasonCode(string ShenQing, string DiKou, string LiYou)
        {
            string str = "";
            string str2 = ShenQing;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "0"))
            {
                if (str2 != "1")
                {
                    return str;
                }
            }
            else
            {
                str = str + "1";
                string str3 = DiKou;
                if (str3 == null)
                {
                    return str;
                }
                if (!(str3 == "0"))
                {
                    if (str3 != "1")
                    {
                        return str;
                    }
                }
                else
                {
                    return (str + "100000000");
                }
                str = str + "01";
                string str4 = LiYou;
                if (str4 == null)
                {
                    return str;
                }
                if (!(str4 == "1"))
                {
                    if (str4 != "2")
                    {
                        if (str4 == "3")
                        {
                            return (str + "0010");
                        }
                        if (str4 != "4")
                        {
                            return str;
                        }
                        return (str + "0001");
                    }
                }
                else
                {
                    return (str + "1000");
                }
                return (str + "0100");
            }
            str = str + "00000001";
            string str5 = LiYou;
            if (str5 == null)
            {
                return str;
            }
            if (!(str5 == "6"))
            {
                if (str5 != "7")
                {
                    return str;
                }
            }
            else
            {
                return (str + "10");
            }
            return (str + "01");
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

        public void ProcessStartThread(int value)
        {
            this.PerformStep(value);
        }

        private void ResetPagesize()
        {
            string str = PropertyUtil.GetValue("Aisino.Fwkp.HzfpHy.HySqdSelect.aisinoGrid");
            if (string.IsNullOrEmpty(str))
            {
                str = "30";
                PropertyUtil.SetValue("Aisino.Fwkp.HzfpHy.HySqdSelect.aisinoGrid", str);
            }
            this.sqdDal.Pagesize = Convert.ToInt32(str);
        }

        private void tool_xiazai_Click(object sender, EventArgs e)
        {
            try
            {
                string str = string.Empty;
                string str2 = string.Empty;
                string str3 = string.Empty;
                string str4 = string.Empty;
                string xxbbh = string.Empty;
                str = base.TaxCardInstance.TaxCode;
                str2 = base.TaxCardInstance.GetInvControlNum().Trim();
                HyXiaZaiTiaoJian jian = new HyXiaZaiTiaoJian {
                    AisinoGrid = this.aisinoGrid
                };
                if (jian.ShowDialog(this) == DialogResult.OK)
                {
                    str3 = jian.DateTimeStart.ToString("yyyyMMdd");
                    str4 = jian.DateTimeEnd.ToString("yyyyMMdd");
                    xxbbh = jian.Xxbbh;
                    XmlDocument document = new XmlDocument();
                    XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                    document.PreserveWhitespace = false;
                    document.AppendChild(newChild);
                    XmlElement element = document.CreateElement("business");
                    element.SetAttribute("id", "HX_HZXXBXZ");
                    element.SetAttribute("comment", "红字信息表下载");
                    document.AppendChild(element);
                    XmlElement element2 = document.CreateElement("body");
                    element2.SetAttribute("count", "1");
                    element2.SetAttribute("skph", str2);
                    element2.SetAttribute("nsrsbh", str);
                    element.AppendChild(element2);
                    XmlElement element3 = document.CreateElement("group");
                    element3.SetAttribute("xh", "1");
                    element2.AppendChild(element3);
                    XmlElement element4 = document.CreateElement("data");
                    element4.SetAttribute("name", "fplx_dm");
                    element4.SetAttribute("value", "009");
                    element3.AppendChild(element4);
                    XmlElement element5 = document.CreateElement("data");
                    element5.SetAttribute("name", "xxbbh");
                    element5.SetAttribute("value", xxbbh);
                    element3.AppendChild(element5);
                    XmlElement element6 = document.CreateElement("data");
                    element6.SetAttribute("name", "sqrq_q");
                    element6.SetAttribute("value", str3);
                    element3.AppendChild(element6);
                    XmlElement element7 = document.CreateElement("data");
                    element7.SetAttribute("name", "sqrq_z");
                    element7.SetAttribute("value", str4);
                    element3.AppendChild(element7);
                    XmlElement element8 = document.CreateElement("data");
                    element8.SetAttribute("name", "xxblx");
                    element8.SetAttribute("value", "0");
                    element3.AppendChild(element8);
                    document.PreserveWhitespace = true;
                    document.Save(this.exePath + @"\Download_HYInputRed.xml");
                    this.progressBar.SetTip("正在连接服务器", "请等待任务完成", "红字货物运输业增值税专用发票信息表下载中");
                    this.progressBar.fpxf_progressBar.Value = 1;
                    this.progressBar.Show();
                    this.progressBar.Refresh();
                    this.ProcessStartThread(0x1b58);
                    this.progressBar.Refresh();
                    int num = 0;
                    string str7 = string.Empty;
                    string str8 = string.Empty;
                    string str9 = string.Empty;
                    string xml = string.Empty;
                    int num2 = HttpsSender.SendMsg("0011", document.InnerXml, out xml);
                    if (num2 != 0)
                    {
                        MessageManager.ShowMsgBox("INP-431421", new string[] { string.Empty, "下载", Convert.ToString(num2), xml });
                        if (File.Exists(Path.Combine(new string[] { this.exePath + "Download_HYInputRed.xml" })))
                        {
                            File.Delete(Path.Combine(new string[] { this.exePath + "Download_HYInputRed.xml" }));
                        }
                    }
                    else
                    {
                        if (xml != null)
                        {
                            this.progressBar.SetTip("正在下载红字货物运输业增值税专用发票信息表", "请等待任务完成", "红字货物运输业增值税专用发票信息表下载中");
                            XmlDocument document2 = new XmlDocument();
                            document2.LoadXml(xml);
                            document2.Save(this.exePath + @"\Download_HYOutputRed_Feedback.xml");
                            XmlDocument document3 = new XmlDocument();
                            document3.Load(this.exePath + @"\Download_HYOutputRed_Feedback.xml");
                            XmlNode node = document3.SelectSingleNode("//body");
                            int num3 = Convert.ToInt32(node.Attributes["count"].Value);
                            if (num3 <= 0)
                            {
                                num3 = 1;
                            }
                            foreach (XmlNode node2 in node.ChildNodes)
                            {
                                num++;
                                XmlElement element9 = (XmlElement) node2;
                                string s = "";
                                string str12 = "";
                                foreach (XmlNode node3 in element9.ChildNodes)
                                {
                                    string str16;
                                    if ((node3.Name == "data") && ((str16 = node3.Attributes["name"].Value) != null))
                                    {
                                        if (!(str16 == "xxbmc"))
                                        {
                                            if (str16 == "returnCode")
                                            {
                                                goto Label_0549;
                                            }
                                            if (str16 == "returnMessage")
                                            {
                                                goto Label_0563;
                                            }
                                            if (str16 == "hzxxb")
                                            {
                                                goto Label_057D;
                                            }
                                        }
                                        else
                                        {
                                            str7 = node3.Attributes["value"].Value;
                                        }
                                    }
                                    continue;
                                Label_0549:
                                    str8 = node3.Attributes["value"].Value;
                                    continue;
                                Label_0563:
                                    str9 = node3.Attributes["value"].Value;
                                    continue;
                                Label_057D:
                                    s = node3.Attributes["value"].Value;
                                }
                                if (str8.Equals("00"))
                                {
                                    byte[] buffer = Convert.FromBase64String(s);
                                    if ((buffer != null) && (buffer.Length > 0))
                                    {
                                        str12 = ToolUtil.GetString(buffer);
                                    }
                                    XmlDocument document4 = new XmlDocument();
                                    document4.LoadXml(str12);
                                    document4.Save(this.exePath + @"\Download_HYOutputRed_SqdInfo_" + str7 + ".xml");
                                    XmlDocument document5 = new XmlDocument();
                                    document5.Load(this.exePath + @"\Download_HYOutputRed_SqdInfo_" + str7 + ".xml");
                                    HZFPHY_SQD model = new HZFPHY_SQD {
                                        SQDH = str7,
                                        XXBZT = str8,
                                        XXBMS = "下载" + str9
                                    };
                                    List<HZFPHY_SQD_MX> models = new List<HZFPHY_SQD_MX>();
                                    string diKou = string.Empty;
                                    string shenQing = string.Empty;
                                    string liYou = string.Empty;
                                    foreach (XmlNode node5 in document5.SelectSingleNode("INFO").ChildNodes)
                                    {
                                        if ("BMB_BBH".Equals(node5.Name))
                                        {
                                            model.FLBMBBBH = node5.InnerText;
                                        }
                                        if ("XXBLX".Equals(node5.Name))
                                        {
                                            model.BBBZ = Convert.ToInt32(node5.InnerText);
                                        }
                                        if ("BH".Equals(node5.Name))
                                        {
                                            model.XXBBH = node5.InnerText;
                                        }
                                        if ("TKRQ".Equals(node5.Name))
                                        {
                                            model.TKRQ = Convert.ToDateTime(node5.InnerText);
                                        }
                                        if ("FPLX_DM".Equals(node5.Name))
                                        {
                                            model.FPZL = node5.InnerText.Equals("009") ? "f" : "c";
                                        }
                                        if ("SFJXSEZC".Equals(node5.Name))
                                        {
                                            diKou = node5.InnerText;
                                        }
                                        if ("SQF".Equals(node5.Name))
                                        {
                                            shenQing = node5.InnerText;
                                        }
                                        if ("KJLY".Equals(node5.Name))
                                        {
                                            liYou = node5.InnerText;
                                        }
                                        "YQKJLY".Equals(node5.Name);
                                        if ("KJLYSM".Equals(node5.Name))
                                        {
                                            model.SQLY = node5.InnerText;
                                        }
                                        if ("YFPDM".Equals(node5.Name))
                                        {
                                            model.FPDM = node5.InnerText;
                                        }
                                        if ("YFPHM".Equals(node5.Name))
                                        {
                                            model.FPHM = node5.InnerText;
                                        }
                                        if ("XSFDM".Equals(node5.Name))
                                        {
                                            model.XFSH = node5.InnerText;
                                        }
                                        if ("XSFMC".Equals(node5.Name))
                                        {
                                            model.XFMC = node5.InnerText;
                                        }
                                        if ("GMFDM".Equals(node5.Name))
                                        {
                                            model.GFSH = node5.InnerText;
                                        }
                                        if ("GMFMC".Equals(node5.Name))
                                        {
                                            model.GFMC = node5.InnerText;
                                        }
                                        if ("HJJE".Equals(node5.Name))
                                        {
                                            model.HJJE = Convert.ToDecimal(node5.InnerText);
                                        }
                                        if ("SL".Equals(node5.Name))
                                        {
                                            model.SL = Convert.ToDouble(node5.InnerText);
                                            foreach (HZFPHY_SQD_MX hzfphy_sqd_mx in models)
                                            {
                                                hzfphy_sqd_mx.SE = hzfphy_sqd_mx.JE * Convert.ToDecimal(model.SL);
                                            }
                                        }
                                        if ("SE".Equals(node5.Name))
                                        {
                                            model.HJSE = Convert.ToDecimal(node5.InnerText);
                                        }
                                        if ("SHRSBH".Equals(node5.Name))
                                        {
                                            model.SHFSH = node5.InnerText;
                                        }
                                        if ("SHRMC".Equals(node5.Name))
                                        {
                                            model.SHFMC = node5.InnerText;
                                        }
                                        if ("FHRSBH".Equals(node5.Name))
                                        {
                                            model.FHFSH = node5.InnerText;
                                        }
                                        if ("FHRMC".Equals(node5.Name))
                                        {
                                            model.FHFMC = node5.InnerText;
                                        }
                                        if ("YSHWXX".Equals(node5.Name))
                                        {
                                            model.YSHWXX = node5.InnerText;
                                        }
                                        if ("JQBH".Equals(node5.Name))
                                        {
                                            model.JQBH = node5.InnerText;
                                        }
                                        if ("CZCH".Equals(node5.Name))
                                        {
                                            model.CZCH = node5.InnerText;
                                        }
                                        if ("CCDW".Equals(node5.Name))
                                        {
                                            model.CCDW = node5.InnerText;
                                        }
                                        if ("FYXMJJE".Equals(node5.Name))
                                        {
                                            int num4 = 0;
                                            XmlElement element1 = (XmlElement) node5;
                                            foreach (XmlNode node6 in node5.ChildNodes)
                                            {
                                                if ("ZB".Equals(node6.Name))
                                                {
                                                    HZFPHY_SQD_MX item = new HZFPHY_SQD_MX {
                                                        SQDH = model.SQDH,
                                                        MXXH = num4,
                                                        FPHXZ = 0,
                                                        HSJBZ = false
                                                    };
                                                    XmlElement element10 = (XmlElement) node6;
                                                    foreach (XmlNode node7 in element10.ChildNodes)
                                                    {
                                                        switch (node7.Name)
                                                        {
                                                            case "FYXM":
                                                                item.SPMC = node7.InnerText;
                                                                break;

                                                            case "JE":
                                                                item.JE = Convert.ToDecimal(node7.InnerText);
                                                                item.SE = item.JE * Convert.ToDecimal(model.SL);
                                                                break;

                                                            case "SPBM":
                                                                if (HySqdTianKai.isFLBM)
                                                                {
                                                                    item.FLBM = node7.InnerText;
                                                                }
                                                                break;

                                                            case "ZXBM":
                                                                if (HySqdTianKai.isFLBM)
                                                                {
                                                                    item.QYSPBM = node7.InnerText;
                                                                }
                                                                break;

                                                            case "YHZCBS":
                                                                if (HySqdTianKai.isFLBM)
                                                                {
                                                                    item.SFXSYHZC = node7.InnerText;
                                                                }
                                                                break;

                                                            case "ZZSTSGL":
                                                                if (HySqdTianKai.isFLBM)
                                                                {
                                                                    item.YHZCMC = node7.InnerText;
                                                                }
                                                                break;

                                                            case "LSLBS":
                                                                if (HySqdTianKai.isFLBM)
                                                                {
                                                                    item.LSLBS = node7.InnerText;
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    models.Add(item);
                                                    num4++;
                                                }
                                            }
                                        }
                                    }
                                    model.SQXZ = this.ParseReasonCode(shenQing, diKou, liYou);
                                    if (shenQing.Trim() == "1")
                                    {
                                        model.REQNSRSBH = model.XFSH;
                                    }
                                    else
                                    {
                                        model.REQNSRSBH = model.GFSH;
                                    }
                                    bool flag = false;
                                    if (this.sqdDal.Select(model.SQDH) != null)
                                    {
                                        flag = this.sqdDal.Updatazt(model);
                                    }
                                    else if (this.sqdDal.Insert(model))
                                    {
                                        this.sqdMxDal.Insert(models);
                                    }
                                    if (File.Exists(Path.Combine(new string[] { this.exePath + "Download_HYOutputRed_SqdInfo_" + str7 + ".xml" })))
                                    {
                                        File.Delete(Path.Combine(new string[] { this.exePath + "Download_HYOutputRed_SqdInfo_" + str7 + ".xml" }));
                                    }
                                }
                                else
                                {
                                    string[] strArray = str9.Split(':');
                                    HZFPHY_SQD hzfphy_sqd2 = new HZFPHY_SQD {
                                        SQDH = str7,
                                        XXBZT = str8
                                    };
                                    if (strArray.Length > 1)
                                    {
                                        hzfphy_sqd2.XXBMS = strArray[1];
                                    }
                                    else
                                    {
                                        hzfphy_sqd2.XXBMS = str9;
                                    }
                                    hzfphy_sqd2.XXBBH = "";
                                    if (this.sqdDal.Select(hzfphy_sqd2.SQDH) != null)
                                    {
                                        this.sqdDal.Updatazt(hzfphy_sqd2);
                                    }
                                }
                                this.ProcessStartThread(0x1388 / num3);
                                this.progressBar.Refresh();
                            }
                            if (File.Exists(Path.Combine(new string[] { this.exePath + "Download_HYOutputRed_Feedback.xml" })))
                            {
                                File.Delete(Path.Combine(new string[] { this.exePath + "Download_HYOutputRed_Feedback.xml" }));
                            }
                        }
                        if (File.Exists(Path.Combine(new string[] { this.exePath + "Download_HYInput.xml" })))
                        {
                            File.Delete(Path.Combine(new string[] { this.exePath + "Download_HYInput.xml" }));
                        }
                        this.progressBar.SetTip("正在下载红字货物运输业增值税专用发票信息表", "请等待任务完成", "红字货物运输业增值税专用发票信息表下载中");
                        this.progressBar.Visible = false;
                        this.BindGridData();
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[红字货物运输业增值税专用发票信息表下载]" + exception.Message);
            }
            finally
            {
                if (this.progressBar != null)
                {
                    this.progressBar.Visible = false;
                    this.Refresh();
                }
            }
        }

        private void tool_xuanze_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = this.aisinoGrid.SelectedRows;
            if (rows.Count <= 0)
            {
                MessageManager.ShowMsgBox("INP-431403");
            }
            else
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    string sQDH = rows[i].Cells["SQDH"].Value.ToString();
                    if (this.sqdDal.Select(sQDH) != null)
                    {
                        XmlDocument document = new XmlDocument();
                        XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                        document.PreserveWhitespace = false;
                        document.AppendChild(newChild);
                        XmlElement element = document.CreateElement("INFO");
                        document.AppendChild(element);
                        if (HySqdTianKai.isFLBM)
                        {
                            XmlElement element2 = document.CreateElement("BMB_BBH");
                            element2.InnerText = this.aisinoGrid.SelectedRows[i].Cells["FLBMBBBH"].Value.ToString();
                            element.AppendChild(element2);
                        }
                        XmlElement element3 = document.CreateElement("XXBLX");
                        element3.InnerText = this.aisinoGrid.SelectedRows[i].Cells["BBBZ"].Value.ToString();
                        element.AppendChild(element3);
                        XmlElement element4 = document.CreateElement("BH");
                        element4.InnerText = this.aisinoGrid.SelectedRows[i].Cells["XXBBH"].Value.ToString();
                        element.AppendChild(element4);
                        XmlElement element5 = document.CreateElement("SQDMC");
                        element5.InnerText = this.aisinoGrid.SelectedRows[i].Cells["SQDH"].Value.ToString();
                        element.AppendChild(element5);
                        XmlElement element6 = document.CreateElement("TKRQ");
                        element6.InnerText = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(this.aisinoGrid.SelectedRows[i].Cells["TKRQ"].Value.ToString()));
                        element.AppendChild(element6);
                        XmlElement element7 = document.CreateElement("FPLX_DM");
                        element7.InnerText = "009";
                        element.AppendChild(element7);
                        XmlElement sFJXSEZC = document.CreateElement("SFJXSEZC");
                        XmlElement sQF = document.CreateElement("SQF");
                        XmlElement element10 = document.CreateElement("KJLY");
                        XmlElement element11 = document.CreateElement("YQKJLY");
                        XmlElement element12 = document.CreateElement("KJLYSM");
                        element11.InnerText = "";
                        string reason = this.aisinoGrid.SelectedRows[i].Cells["SQXZ"].Value.ToString();
                        if (reason.Length > 10)
                        {
                            reason = reason.Substring(0, 10);
                        }
                        element12.InnerText = reason;
                        element10.InnerText = this.ParseReason(sQF, sFJXSEZC, reason);
                        element.AppendChild(sFJXSEZC);
                        element.AppendChild(sQF);
                        element.AppendChild(element10);
                        element.AppendChild(element11);
                        element.AppendChild(element12);
                        XmlElement element13 = document.CreateElement("YFPDM");
                        element13.InnerText = this.aisinoGrid.SelectedRows[i].Cells["FPDM"].Value.ToString();
                        element.AppendChild(element13);
                        XmlElement element14 = document.CreateElement("YFPHM");
                        element14.InnerText = this.aisinoGrid.SelectedRows[i].Cells["FPHM"].Value.ToString();
                        element.AppendChild(element14);
                        XmlElement element15 = document.CreateElement("XSFDM");
                        element15.InnerText = this.aisinoGrid.SelectedRows[i].Cells["XFSH"].Value.ToString();
                        element.AppendChild(element15);
                        XmlElement element16 = document.CreateElement("XSFMC");
                        element16.InnerText = this.aisinoGrid.SelectedRows[i].Cells["XFMC"].Value.ToString();
                        element.AppendChild(element16);
                        XmlElement element17 = document.CreateElement("GMFDM");
                        element17.InnerText = this.aisinoGrid.SelectedRows[i].Cells["GFSH"].Value.ToString();
                        element.AppendChild(element17);
                        XmlElement element18 = document.CreateElement("GMFMC");
                        element18.InnerText = this.aisinoGrid.SelectedRows[i].Cells["GFMC"].Value.ToString();
                        element.AppendChild(element18);
                        double num2 = 0.0;
                        double num3 = 0.0;
                        XmlElement element19 = document.CreateElement("HJJE");
                        num2 = Convert.ToDouble(this.aisinoGrid.SelectedRows[i].Cells["HJJE"].Value.ToString());
                        element19.InnerText = string.Format("{0:0.00}", num2);
                        element.AppendChild(element19);
                        XmlElement element20 = document.CreateElement("SL");
                        element20.InnerText = this.aisinoGrid.SelectedRows[i].Cells["SL"].Value.ToString();
                        element.AppendChild(element20);
                        XmlElement element21 = document.CreateElement("SE");
                        num3 = Convert.ToDouble(this.aisinoGrid.SelectedRows[i].Cells["HJSE"].Value.ToString());
                        element21.InnerText = string.Format("{0:0.00}", num3);
                        element.AppendChild(element21);
                        XmlElement element22 = document.CreateElement("JSHJ");
                        element22.InnerText = string.Format("{0:0.00}", num2 + num3);
                        element.AppendChild(element22);
                        XmlElement element23 = document.CreateElement("XSFSWJG_DM");
                        element.AppendChild(element23);
                        XmlElement element24 = document.CreateElement("XSFSWJG_MC");
                        element.AppendChild(element24);
                        XmlElement element25 = document.CreateElement("GMFSWJG_DM");
                        element.AppendChild(element25);
                        XmlElement element26 = document.CreateElement("GMFSWJG_MC");
                        element.AppendChild(element26);
                        XmlElement element27 = document.CreateElement("SHRSBH");
                        element27.InnerText = this.aisinoGrid.SelectedRows[i].Cells["SHRSH"].Value.ToString();
                        element.AppendChild(element27);
                        XmlElement element28 = document.CreateElement("SHRMC");
                        element28.InnerText = this.aisinoGrid.SelectedRows[i].Cells["SHRMC"].Value.ToString();
                        element.AppendChild(element28);
                        XmlElement element29 = document.CreateElement("FHRSBH");
                        element29.InnerText = this.aisinoGrid.SelectedRows[i].Cells["FHRSH"].Value.ToString();
                        element.AppendChild(element29);
                        XmlElement element30 = document.CreateElement("FHRMC");
                        element30.InnerText = this.aisinoGrid.SelectedRows[i].Cells["FHRMC"].Value.ToString();
                        element.AppendChild(element30);
                        XmlElement element31 = document.CreateElement("YSHWXX");
                        element31.InnerText = this.aisinoGrid.SelectedRows[i].Cells["YSHWXX"].Value.ToString();
                        element.AppendChild(element31);
                        XmlElement element32 = document.CreateElement("JQBH");
                        element32.InnerText = this.aisinoGrid.SelectedRows[i].Cells["JQBH"].Value.ToString();
                        element.AppendChild(element32);
                        XmlElement element33 = document.CreateElement("CZCH");
                        element33.InnerText = this.aisinoGrid.SelectedRows[i].Cells["CZCH"].Value.ToString();
                        element.AppendChild(element33);
                        XmlElement element34 = document.CreateElement("CCDW");
                        element34.InnerText = this.aisinoGrid.SelectedRows[i].Cells["CCDW"].Value.ToString();
                        element.AppendChild(element34);
                        XmlElement element35 = document.CreateElement("FYXMJJE");
                        element.AppendChild(element35);
                        DataTable table = this.sqdMxDal.SelectList(this.aisinoGrid.SelectedRows[i].Cells["SQDH"].Value.ToString());
                        for (int j = 0; j < table.Rows.Count; j++)
                        {
                            DataRow row = table.Rows[j];
                            XmlElement element36 = document.CreateElement("ZB");
                            element35.AppendChild(element36);
                            XmlElement element37 = document.CreateElement("FYXM");
                            element37.InnerText = GetSafeData.ValidateValue<string>(row, "SPMC");
                            element36.AppendChild(element37);
                            XmlElement element38 = document.CreateElement("JE");
                            element38.InnerText = GetSafeData.ValidateValue<decimal>(row, "JE").ToString("0.00");
                            element36.AppendChild(element38);
                            XmlElement element39 = document.CreateElement("MXSE");
                            element39.InnerText = GetSafeData.ValidateMxseValue(row, "SE");
                            element36.AppendChild(element39);
                            if (HySqdTianKai.isFLBM)
                            {
                                XmlElement element40 = document.CreateElement("SPBM");
                                element40.InnerText = GetSafeData.ValidateValue<string>(row, "FLBM");
                                element36.AppendChild(element40);
                                XmlElement element41 = document.CreateElement("ZXBM");
                                element41.InnerText = GetSafeData.ValidateValue<string>(row, "QYSPBM");
                                element36.AppendChild(element41);
                                XmlElement element42 = document.CreateElement("YHZCBS");
                                element42.InnerText = GetSafeData.ValidateValue<string>(row, "SFXSYHZC");
                                element36.AppendChild(element42);
                                XmlElement element43 = document.CreateElement("ZZSTSGL");
                                element43.InnerText = GetSafeData.ValidateValue<string>(row, "YHZCMC");
                                element36.AppendChild(element43);
                                XmlElement element44 = document.CreateElement("LSLBS");
                                element44.InnerText = GetSafeData.ValidateValue<string>(row, "LSLBS");
                                element36.AppendChild(element44);
                            }
                        }
                        document.PreserveWhitespace = true;
                        this._result = document.InnerXml;
                    }
                }
                base.DialogResult = DialogResult.OK;
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

