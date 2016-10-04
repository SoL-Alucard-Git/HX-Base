namespace Aisino.Fwkp.HzfpHy.Form
{
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.HzfpHy.Common;
    using Aisino.Fwkp.HzfpHy.Form.Common;
    using Aisino.Fwkp.HzfpHy.IBLL;
    using Aisino.Fwkp.HzfpHy.Model;
    using Aisino.Fwkp.Print;
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

    public class HySqdChaXun : DockForm
    {
        private AisinoDataGrid aisinoGrid;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip;
        private string exePath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Log\");
        private ILog loger = LogUtil.GetLogger<HySqdChaXun>();
        private string[] ModifyForbidSet = new string[] { "U00", "00" };
        private FPProgressBar progressBar = new FPProgressBar();
        private string queryGfmc = "";
        private string queryGfsh = "";
        private string queryJsrq = "2099-12-31";
        private string queryKsrq = "2000-01-01";
        private string queryRetrieve = "";
        private string querySqdh = "";
        private string queryState = "WTKSH00";
        private string queryXfmc = "";
        private string queryXfsh = "";
        private bool retrieveTrigger = true;
        private readonly IHZFPHY_SQD sqdDal = BLLFactory.CreateInstant<IHZFPHY_SQD>("HZFPHY_SQD");
        private readonly IHZFPHY_SQD_MX sqdMxDal = BLLFactory.CreateInstant<IHZFPHY_SQD_MX>("HZFPHY_SQD_MX");
        private ToolStripButton tool_ceshi;
        private ToolStripButton tool_chazhao;
        private ToolStripButton tool_daochu;
        private ToolStripButton tool_daying;
        private ToolStripButton tool_geshi;
        private ToolStripLabel tool_LblRetrieve;
        private ToolStripButton tool_mingxi;
        private ToolStripButton tool_shanchu;
        private ToolStripButton tool_shangchuan;
        private ToolStripButton tool_tongji;
        private ToolStripButton tool_tuichu;
        private ToolStripTextBox tool_TxtRetrieve;
        private ToolStripButton tool_xiazai;
        private ToolStripButton tool_xiugai;
        private ToolStrip toolStrip;
        private ToolStripMenuItem ToolStripMenuItemLb;
        private ToolStripMenuItem ToolStripMenuItemSqd;
        private string[] UploadForbidSet = new string[] { "00" };
        private XmlComponentLoader xmlComponentLoader1;

        public HySqdChaXun()
        {
            this.Initialize();
            this.ResetPagesize();
            List<Dictionary<string, string>> list = TransportInvInfoTableCommon.CreateGridHeader();
            this.aisinoGrid.ColumeHead=(list);
            DataGridViewColumn column = this.aisinoGrid.Columns["FLBMBBBH"];
            column.Visible = false;
            DataGridViewColumn column2 = this.aisinoGrid.Columns["HJJE"];
            if (column2 != null)
            {
                column2.DefaultCellStyle.Format = "0.00";
            }
            DataGridViewColumn column3 = this.aisinoGrid.Columns["HJSE"];
            if (column3 != null)
            {
                column3.DefaultCellStyle.Format = "0.00";
            }
            this.BindGridData();
        }

        private void aisinoGrid_DataGridRowDbClickEvent(object sender, DataGridRowEventArgs e)
        {
            DataGridViewSelectedRowCollection rows = this.aisinoGrid.SelectedRows;
            if (rows.Count > 0)
            {
                DataGridViewRow row = rows[0];
                string str = row.Cells["SQDH"].Value.ToString();
                try
                {
                    HySqdTianKai kai = new HySqdTianKai {
                        Text = "红字货物运输业增值税专用发票信息表查看",
                        sqdh = str
                    };
                    kai.InitSqdMx(InitSqdMxType.Read, null);
                    kai.ShowDialog();
                }
                catch (BaseException exception)
                {
                    ExceptionHandler.HandleError(exception);
                }
                catch (Exception exception2)
                {
                    ExceptionHandler.HandleError(exception2);
                }
            }
            else
            {
                MessageManager.ShowMsgBox("INP-431403");
            }
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
            dict.Add("bbbz", "0");
            dict.Add("rtrv", "%" + this.queryRetrieve + "%");
            this.aisinoGrid.DataSource=(this.sqdDal.SelectList(this.sqdDal.CurrentPage, this.sqdDal.Pagesize, dict));
            PropertyUtil.SetValue("Aisino.Fwkp.HzfpHy.HySqdChaXun.aisinoGrid", this.sqdDal.Pagesize.ToString());
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
            dict.Add("bbbz", "0");
            dict.Add("rtrv", "%" + this.queryRetrieve + "%");
            this.sqdDal.CurrentPage = 1;
            this.aisinoGrid.DataSource=(this.sqdDal.SelectList(this.sqdDal.CurrentPage, this.sqdDal.Pagesize, dict));
        }

        private XmlDocument createTableXML(DataGridViewRow curRow)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
            document.PreserveWhitespace = false;
            document.AppendChild(newChild);
            XmlElement element = document.CreateElement("INFO");
            element.SetAttribute("version", "2.0");
            document.AppendChild(element);
            if (HySqdTianKai.isFLBM)
            {
                XmlElement element2 = document.CreateElement("BMB_BBH");
                element2.InnerText = (curRow.Cells["FLBMBBBH"].Value == null) ? "" : curRow.Cells["FLBMBBBH"].Value.ToString();
                element.AppendChild(element2);
            }
            XmlElement element3 = document.CreateElement("XXBLX");
            element3.InnerText = curRow.Cells["BBBZ"].Value.ToString();
            element.AppendChild(element3);
            XmlElement element4 = document.CreateElement("BH");
            element4.InnerText = curRow.Cells["SQDH"].Value.ToString();
            element.AppendChild(element4);
            XmlElement element5 = document.CreateElement("TKRQ");
            element5.InnerText = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(curRow.Cells["TKRQ"].Value.ToString()));
            element.AppendChild(element5);
            XmlElement element6 = document.CreateElement("FPLX_DM");
            element6.InnerText = "009";
            element.AppendChild(element6);
            XmlElement sFJXSEZC = document.CreateElement("SFJXSEZC");
            XmlElement sQF = document.CreateElement("SQF");
            XmlElement element9 = document.CreateElement("KJLY");
            XmlElement element10 = document.CreateElement("YQKJLY");
            XmlElement element11 = document.CreateElement("KJLYSM");
            element10.InnerText = "";
            string reason = curRow.Cells["SQXZ"].Value.ToString();
            if (reason.Length > 10)
            {
                reason = reason.Substring(0, 10);
            }
            element11.InnerText = string.Empty;
            element9.InnerText = this.ParseReason(sQF, sFJXSEZC, reason);
            element.AppendChild(sFJXSEZC);
            element.AppendChild(sQF);
            element.AppendChild(element9);
            element.AppendChild(element10);
            element.AppendChild(element11);
            XmlElement element12 = document.CreateElement("YFPDM");
            element12.InnerText = curRow.Cells["FPDM"].Value.ToString();
            element.AppendChild(element12);
            XmlElement element13 = document.CreateElement("YFPHM");
            string str2 = curRow.Cells["FPHM"].Value.ToString();
            if (!str2.Equals(string.Empty))
            {
                int length = str2.Length;
                if (str2.Length < 8)
                {
                    for (int j = 0; j < (8 - length); j++)
                    {
                        str2 = "0" + str2;
                    }
                }
            }
            element13.InnerText = str2;
            element.AppendChild(element13);
            XmlElement element14 = document.CreateElement("XSFDM");
            element14.InnerText = curRow.Cells["XFSH"].Value.ToString();
            element.AppendChild(element14);
            XmlElement element15 = document.CreateElement("XSFMC");
            element15.InnerText = curRow.Cells["XFMC"].Value.ToString();
            element.AppendChild(element15);
            XmlElement element16 = document.CreateElement("GMFDM");
            element16.InnerText = curRow.Cells["GFSH"].Value.ToString();
            element.AppendChild(element16);
            XmlElement element17 = document.CreateElement("GMFMC");
            element17.InnerText = curRow.Cells["GFMC"].Value.ToString();
            element.AppendChild(element17);
            double num3 = 0.0;
            double num4 = 0.0;
            XmlElement element18 = document.CreateElement("HJJE");
            num3 = Convert.ToDouble(curRow.Cells["HJJE"].Value.ToString());
            element18.InnerText = string.Format("{0:0.00}", num3);
            element.AppendChild(element18);
            XmlElement element19 = document.CreateElement("SL");
            string str3 = curRow.Cells["SL"].Value.ToString();
            if (str3.Equals("免税"))
            {
                element19.InnerText = "0";
            }
            else
            {
                element19.InnerText = str3;
            }
            element.AppendChild(element19);
            XmlElement element20 = document.CreateElement("SE");
            num4 = Convert.ToDouble(curRow.Cells["HJSE"].Value.ToString());
            element20.InnerText = string.Format("{0:0.00}", num4);
            element.AppendChild(element20);
            XmlElement element21 = document.CreateElement("JSHJ");
            element21.InnerText = string.Format("{0:0.00}", num3 + num4);
            element.AppendChild(element21);
            XmlElement element22 = document.CreateElement("XSFSWJG_DM");
            element.AppendChild(element22);
            XmlElement element23 = document.CreateElement("XSFSWJG_MC");
            element.AppendChild(element23);
            XmlElement element24 = document.CreateElement("GMFSWJG_DM");
            element.AppendChild(element24);
            XmlElement element25 = document.CreateElement("GMFSWJG_MC");
            element.AppendChild(element25);
            XmlElement element26 = document.CreateElement("SHRSBH");
            element26.InnerText = curRow.Cells["SHRSH"].Value.ToString();
            element.AppendChild(element26);
            XmlElement element27 = document.CreateElement("SHRMC");
            element27.InnerText = curRow.Cells["SHRMC"].Value.ToString();
            element.AppendChild(element27);
            XmlElement element28 = document.CreateElement("FHRSBH");
            element28.InnerText = curRow.Cells["FHRSH"].Value.ToString();
            element.AppendChild(element28);
            XmlElement element29 = document.CreateElement("FHRMC");
            element29.InnerText = curRow.Cells["FHRMC"].Value.ToString();
            element.AppendChild(element29);
            XmlElement element30 = document.CreateElement("YSHWXX");
            element30.InnerText = curRow.Cells["YSHWXX"].Value.ToString();
            element.AppendChild(element30);
            XmlElement element31 = document.CreateElement("JQBH");
            element31.InnerText = curRow.Cells["JQBH"].Value.ToString();
            element.AppendChild(element31);
            XmlElement element32 = document.CreateElement("CZCH");
            element32.InnerText = curRow.Cells["CZCH"].Value.ToString();
            element.AppendChild(element32);
            XmlElement element33 = document.CreateElement("CCDW");
            element33.InnerText = curRow.Cells["CCDW"].Value.ToString();
            element.AppendChild(element33);
            XmlElement element34 = document.CreateElement("FYXMJJE");
            element.AppendChild(element34);
            DataTable table = this.sqdMxDal.SelectList(curRow.Cells["SQDH"].Value.ToString());
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                XmlElement element35 = document.CreateElement("ZB");
                element34.AppendChild(element35);
                XmlElement element36 = document.CreateElement("FYXM");
                element36.InnerText = GetSafeData.ValidateValue<string>(row, "SPMC");
                element35.AppendChild(element36);
                XmlElement element37 = document.CreateElement("JE");
                element37.InnerText = GetSafeData.ValidateValue<decimal>(row, "JE").ToString();
                element35.AppendChild(element37);
                if (HySqdTianKai.isFLBM)
                {
                    XmlElement element38 = document.CreateElement("SPBM");
                    element38.InnerText = GetSafeData.ValidateValue<string>(row, "FLBM");
                    element35.AppendChild(element38);
                    XmlElement element39 = document.CreateElement("ZXBM");
                    element39.InnerText = GetSafeData.ValidateValue<string>(row, "QYSPBM");
                    element35.AppendChild(element39);
                    XmlElement element40 = document.CreateElement("YHZCBS");
                    string str4 = GetSafeData.ValidateValue<string>(row, "SFXSYHZC");
                    element40.InnerText = (str4 == "1") ? "1" : "0";
                    element35.AppendChild(element40);
                    XmlElement element41 = document.CreateElement("ZZSTSGL");
                    element41.InnerText = GetSafeData.ValidateValue<string>(row, "YHZCMC");
                    element35.AppendChild(element41);
                    XmlElement element42 = document.CreateElement("LSLBS");
                    element42.InnerText = GetSafeData.ValidateValue<string>(row, "LSLBS");
                    element35.AppendChild(element42);
                }
            }
            document.PreserveWhitespace = true;
            return document;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ForbidOperation(string state, string[] OperSet)
        {
            foreach (string str in OperSet)
            {
                if (state.Equals(str))
                {
                    return true;
                }
            }
            return false;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.toolStrip = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.tool_tuichu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_tuichu");
            this.tool_chazhao = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_chazhao");
            this.tool_daying = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_daying");
            this.contextMenuStrip = new ContextMenuStrip();
            this.ToolStripMenuItemSqd = new ToolStripMenuItem("红字货物运输业增值税专用发票信息表");
            this.ToolStripMenuItemLb = new ToolStripMenuItem("红字货物运输业增值税专用发票信息表列表");
            this.contextMenuStrip.Items.Add(this.ToolStripMenuItemSqd);
            this.contextMenuStrip.Items.Add(this.ToolStripMenuItemLb);
            this.tool_tongji = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_tongji");
            this.tool_geshi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_geshi");
            this.tool_xiugai = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_xiugai");
            this.tool_shanchu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_shanchu");
            this.tool_mingxi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_mingxi");
            this.tool_daochu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_daochu");
            this.tool_shangchuan = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_shangchuan");
            this.tool_xiazai = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_xiazai");
            this.aisinoGrid = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoGrid");
            this.aisinoGrid.ReadOnly=(true);
            this.aisinoGrid.DataGrid.AllowUserToDeleteRows = false;
            if (base.TaxCardInstance.QYLX.ISTDQY)
            {
                this.tool_xiazai.Visible = false;
                this.tool_shangchuan.Visible = false;
            }
            this.tool_tuichu.Click += new EventHandler(this.tool_tuichu_Click);
            this.tool_chazhao.Click += new EventHandler(this.tool_chazhao_Click);
            this.tool_daying.Click += new EventHandler(this.tool_daying_Click);
            this.ToolStripMenuItemSqd.Click += new EventHandler(this.ToolStripMenuItemSqd_Click);
            this.ToolStripMenuItemLb.Click += new EventHandler(this.ToolStripMenuItemLb_Click);
            this.tool_tongji.Click += new EventHandler(this.tool_tongji_Click);
            this.tool_geshi.Click += new EventHandler(this.tool_geshi_Click);
            this.tool_xiugai.Click += new EventHandler(this.tool_xiugai_Click);
            this.tool_shanchu.Click += new EventHandler(this.tool_shanchu_Click);
            this.tool_mingxi.Click += new EventHandler(this.tool_mingxi_Click);
            this.tool_daochu.Click += new EventHandler(this.tool_daochu_Click);
            this.tool_shangchuan.Click += new EventHandler(this.tool_shangchuan_Click);
            this.tool_xiazai.Click += new EventHandler(this.tool_xiazai_Click);
            this.aisinoGrid.DataGridRowDbClickEvent+=(new EventHandler<DataGridRowEventArgs>(this.aisinoGrid_DataGridRowDbClickEvent));
            this.aisinoGrid.GoToPageEvent+=(new EventHandler<GoToPageEventArgs>(this.aisinoGrid_GoToPageEvent));
            this.tool_LblRetrieve = this.xmlComponentLoader1.GetControlByName<ToolStripLabel>("toolLblRetrieve");
            this.tool_TxtRetrieve = this.xmlComponentLoader1.GetControlByName<ToolStripTextBox>("toolTxtRetrieve");
            this.tool_TxtRetrieve.TextChanged += new EventHandler(this.tool_TxtRetrieve_TextChanged);
            this.tool_tuichu.Margin = new Padding(20, 1, 0, 2);
            ControlStyleUtil.SetToolStripStyle(this.toolStrip);
            this.QueryComponentsLayoutSet();
            this.tool_ceshi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_ceshi");
            this.tool_ceshi.Visible = false;
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(HySqdChaXun));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x2c2, 0x21a);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath=(@"..\Config\Components\Aisino.Fwkp.HzfpHy.Form.HySqdChaXun\Aisino.Fwkp.HzfpHy.Form.HySqdChaXun.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2c2, 0x21a);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "HySqdChaXun";
            this.Text = "红字货物运输业增值税专用发票信息表查询导出";
            base.ResumeLayout(false);
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

        protected void QueryComponentsLayoutSet()
        {
            this.tool_LblRetrieve.Alignment = ToolStripItemAlignment.Right;
            this.tool_TxtRetrieve.Alignment = ToolStripItemAlignment.Right;
        }

        private void ResetPagesize()
        {
            string str = PropertyUtil.GetValue("Aisino.Fwkp.HzfpHy.HySqdChaXun.aisinoGrid");
            if (string.IsNullOrEmpty(str) || str.Equals("0"))
            {
                str = "30";
                PropertyUtil.SetValue("Aisino.Fwkp.HzfpHy.HySqdChaXun.aisinoGrid", str);
            }
            this.sqdDal.Pagesize = Convert.ToInt32(str);
        }

        private void tool_chazhao_Click(object sender, EventArgs e)
        {
            HyChaXunTiaoJian jian = new HyChaXunTiaoJian {
                Text = "红字货物运输业增值税专用发票信息表查询条件",
                AisinoGrid = this.aisinoGrid
            };
            if (jian.ShowDialog(this) == DialogResult.OK)
            {
                this.querySqdh = jian.Sqdh;
                this.queryGfmc = jian.Spfmc;
                this.queryGfsh = jian.Spfsh;
                this.queryXfmc = jian.Cyfmc;
                this.queryXfsh = jian.Cyfsh;
                this.queryKsrq = jian.DateTimeStart.ToString("yyyy-MM-dd");
                this.queryJsrq = jian.DateTimeEnd.ToString("yyyy-MM-dd");
                this.queryState = jian.Sqdzt;
                this.retrieveTrigger = false;
                this.tool_TxtRetrieve.Text = "";
                this.retrieveTrigger = true;
                this.BindGridData();
            }
        }

        private void tool_daochu_Click(object sender, EventArgs e)
        {
            int count = this.aisinoGrid.SelectedRows.Count;
            if (count <= 0)
            {
                MessageManager.ShowMsgBox("INP-431403");
            }
            else if (DialogResult.OK == MessageManager.ShowMsgBox("INP-431418", new string[] { "导出", string.Empty, Convert.ToString(count) }))
            {
                string selectedPath = string.Empty;
                FolderBrowserDialog dialog = new FolderBrowserDialog {
                    Description = "选择导出的目录",
                    RootFolder = Environment.SpecialFolder.Desktop,
                    ShowNewFolderButton = true
                };
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    selectedPath = dialog.SelectedPath;
                    for (int i = 0; i < this.aisinoGrid.SelectedRows.Count; i++)
                    {
                        DataGridViewRow curRow = this.aisinoGrid.SelectedRows[i];
                        string str2 = curRow.Cells["SQDH"].Value.ToString() + ".xml";
                        this.createTableXML(curRow).Save(selectedPath + @"\" + str2);
                    }
                    MessageManager.ShowMsgBox("INP-431419", new string[] { "导出", string.Empty, Convert.ToString(count) });
                }
            }
        }

        private void tool_daying_Click(object sender, EventArgs e)
        {
            Rectangle bounds = this.tool_daying.Bounds;
            Point p = new Point(bounds.X, bounds.Y + bounds.Height);
            if (this.contextMenuStrip != null)
            {
                this.contextMenuStrip.Show(base.PointToScreen(p));
            }
        }

        private void tool_geshi_Click(object sender, EventArgs e)
        {
            this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoGrid").SetColumnsStyle(this.xmlComponentLoader1.XMLPath, this);
        }

        private void tool_mingxi_Click(object sender, EventArgs e)
        {
            this.aisinoGrid_DataGridRowDbClickEvent(null, null);
        }

        private void tool_NewWin_Click(object sender, EventArgs e)
        {
            HyXxbTianKai kai = new HyXxbTianKai();
            kai.TabText=("开具红字货物运输业增值税专用发票信息表");
            kai.ShowDialog();
        }

        private void tool_RedInv_Click(object sender, EventArgs e)
        {
            new HySqdSelect().ShowDialog();
        }

        private void tool_shanchu_Click(object sender, EventArgs e)
        {
            if (this.aisinoGrid.SelectedRows.Count <= 0)
            {
                MessageManager.ShowMsgBox("INP-431403");
            }
            else if (DialogResult.OK == MessageManager.ShowMsgBox("INP-431418", new string[] { "删除", string.Empty, Convert.ToString(this.aisinoGrid.SelectedRows.Count) }))
            {
                for (int i = 0; i < this.aisinoGrid.SelectedRows.Count; i++)
                {
                    string sQDH = this.aisinoGrid.SelectedRows[i].Cells["SQDH"].Value.ToString();
                    if (this.sqdDal.Delete(sQDH))
                    {
                        this.sqdMxDal.Delete(sQDH);
                    }
                }
                MessageManager.ShowMsgBox("INP-431419", new string[] { "删除", string.Empty, Convert.ToString(this.aisinoGrid.SelectedRows.Count) });
                this.BindGridData();
            }
        }

        private void tool_shangchuan_Click(object sender, EventArgs e)
        {
            try
            {
                int count = this.aisinoGrid.SelectedRows.Count;
                if (count <= 0)
                {
                    MessageManager.ShowMsgBox("INP-431403");
                }
                else if (DialogResult.OK == MessageManager.ShowMsgBox("INP-431418", new string[] { "上传", string.Empty, Convert.ToString(count) }))
                {
                    int num1 = this.aisinoGrid.SelectedRows.Count;
                    this.progressBar.SetTip("正在上传红字货物运输业增值税专用发票信息表", "请等待任务完成", "红字货物运输业增值税专用发票信息表上传中");
                    this.progressBar.fpxf_progressBar.Value = 1;
                    this.progressBar.Show();
                    this.progressBar.Refresh();
                    this.ProcessStartThread(0x1b58);
                    this.progressBar.Refresh();
                    this.progressBar.SetTip("正在上传红字货物运输业增值税专用发票信息表", "请等待任务完成", "红字货物运输业增值税专用发票信息表上传中");
                    for (int i = 0; i < this.aisinoGrid.SelectedRows.Count; i++)
                    {
                        string state = this.aisinoGrid.SelectedRows[i].Cells["XXBZT"].Value.ToString();
                        string str2 = this.aisinoGrid.SelectedRows[i].Cells["SQDH"].Value.ToString();
                        if (this.ForbidOperation(state, this.UploadForbidSet))
                        {
                            MessageManager.ShowMsgBox("INP-431420", new string[] { string.Empty, str2, this.aisinoGrid.SelectedRows[i].Cells["XXBMS"].Value.ToString() });
                        }
                        else
                        {
                            DataGridViewRow curRow = this.aisinoGrid.SelectedRows[i];
                            XmlDocument document = this.createTableXML(curRow);
                            document.Save(this.exePath + @"\Input_Detail_" + curRow.Cells["SQDH"].Value.ToString() + ".xml");
                            XmlDocument document2 = new XmlDocument();
                            XmlDeclaration newChild = document2.CreateXmlDeclaration("1.0", "GBK", null);
                            document2.PreserveWhitespace = false;
                            document2.AppendChild(newChild);
                            XmlElement element = document2.CreateElement("business");
                            element.SetAttribute("id", "HX_HZXXBSC");
                            element.SetAttribute("comment", "红字信息表上传");
                            document2.AppendChild(element);
                            XmlElement element2 = document2.CreateElement("body");
                            element2.SetAttribute("count", "1");
                            element2.SetAttribute("skph", base.TaxCardInstance.GetInvControlNum());
                            element2.SetAttribute("nsrsbh", base.TaxCardInstance.TaxCode);
                            element.AppendChild(element2);
                            XmlElement element3 = document2.CreateElement("group");
                            element3.SetAttribute("xh", "1");
                            element2.AppendChild(element3);
                            XmlElement element4 = document2.CreateElement("data");
                            element4.SetAttribute("name", "fplx_dm");
                            element4.SetAttribute("value", "009");
                            element3.AppendChild(element4);
                            XmlElement element5 = document2.CreateElement("data");
                            element5.SetAttribute("name", "xxbmc");
                            element5.SetAttribute("value", this.aisinoGrid.SelectedRows[i].Cells["SQDH"].Value.ToString());
                            element3.AppendChild(element5);
                            XmlElement element6 = document2.CreateElement("data");
                            element6.SetAttribute("name", "sqrq");
                            element6.SetAttribute("value", base.TaxCardInstance.GetCardClock().ToString("yyyyMMdd"));
                            element3.AppendChild(element6);
                            XmlElement element7 = document2.CreateElement("data");
                            element7.SetAttribute("name", "xxblx");
                            element7.SetAttribute("value", "0");
                            element3.AppendChild(element7);
                            XmlElement element8 = document2.CreateElement("data");
                            element8.SetAttribute("name", "hzxxb");
                            string str3 = "";
                            byte[] bytes = ToolUtil.GetBytes(document.InnerXml.ToString());
                            if ((bytes != null) && (bytes.Length > 0))
                            {
                                str3 = Convert.ToBase64String(bytes);
                            }
                            element8.SetAttribute("value", str3);
                            element3.AppendChild(element8);
                            document2.PreserveWhitespace = true;
                            string sQDH = string.Empty;
                            string str5 = string.Empty;
                            string str6 = string.Empty;
                            bool flag = false;
                            string xml = string.Empty;
                            int num3 = HttpsSender.SendMsg("0009", document2.InnerXml, out xml);
                            document2.Save(this.exePath + @"\Input_Req_" + this.aisinoGrid.SelectedRows[i].Cells["SQDH"].Value.ToString() + ".xml");
                            if (num3 != 0)
                            {
                                MessageManager.ShowMsgBox("INP-431421", new string[] { string.Empty, "上传", Convert.ToString(num3), xml });
                                flag = true;
                            }
                            else
                            {
                                XmlDocument document3 = new XmlDocument();
                                document3.LoadXml(xml);
                                document3.Save(this.exePath + @"\Output_" + this.aisinoGrid.SelectedRows[i].Cells["SQDH"].Value.ToString() + ".xml");
                                XmlDocument document4 = new XmlDocument();
                                document4.Load(this.exePath + @"\Output_" + this.aisinoGrid.SelectedRows[i].Cells["SQDH"].Value.ToString() + ".xml");
                                foreach (XmlNode node2 in document4.SelectSingleNode("//body").ChildNodes)
                                {
                                    XmlElement element9 = (XmlElement) node2;
                                    XmlNodeList childNodes = element9.ChildNodes;
                                    bool flag2 = false;
                                    foreach (XmlNode node3 in childNodes)
                                    {
                                        string str9;
                                        if ((node3.Name == "data") && ((str9 = node3.Attributes["name"].Value) != null))
                                        {
                                            if (!(str9 == "xxbmc"))
                                            {
                                                if (str9 == "returnCode")
                                                {
                                                    goto Label_06A7;
                                                }
                                                if (str9 == "returnMessage")
                                                {
                                                    goto Label_06D5;
                                                }
                                            }
                                            else
                                            {
                                                sQDH = node3.Attributes["value"].Value;
                                            }
                                        }
                                        continue;
                                    Label_06A7:
                                        if (node3.Attributes["value"].Value.Equals("00"))
                                        {
                                            str5 = "U00";
                                            flag2 = true;
                                        }
                                        continue;
                                    Label_06D5:
                                        str6 = "上传" + node3.Attributes["value"].Value;
                                    }
                                    if (flag2)
                                    {
                                        HZFPHY_SQD model = this.sqdDal.Select(sQDH);
                                        if (model != null)
                                        {
                                            model.SQDH = sQDH;
                                            model.XXBBH = string.Empty;
                                            model.XXBZT = str5;
                                            model.XXBMS = str6;
                                            this.sqdDal.Updatazt(model);
                                        }
                                    }
                                    else
                                    {
                                        string str8 = ("信息表：" + sQDH + " 上传失败！\n失败原因：" + str6) + "\n请点击确认继续";
                                        MessageManager.ShowMsgBox("INP-431432", new string[] { str8 });
                                    }
                                }
                                if (File.Exists(Path.Combine(this.exePath, "Output_" + this.aisinoGrid.SelectedRows[i].Cells["SQDH"].Value.ToString() + ".xml")))
                                {
                                    File.Delete(Path.Combine(this.exePath, "Output_" + this.aisinoGrid.SelectedRows[i].Cells["SQDH"].Value.ToString() + ".xml"));
                                }
                            }
                            if (File.Exists(Path.Combine(this.exePath, "Input_Detail_" + curRow.Cells["SQDH"].Value.ToString() + ".xml")))
                            {
                                File.Delete(Path.Combine(this.exePath, "Input_Detail_" + curRow.Cells["SQDH"].Value.ToString() + ".xml"));
                            }
                            if (File.Exists(Path.Combine(this.exePath, "Input_Req_" + this.aisinoGrid.SelectedRows[i].Cells["SQDH"].Value.ToString() + ".xml")))
                            {
                                File.Delete(Path.Combine(this.exePath, "Input_Req_" + this.aisinoGrid.SelectedRows[i].Cells["SQDH"].Value.ToString() + ".xml"));
                            }
                            if (flag)
                            {
                                break;
                            }
                            this.ProcessStartThread(0xbb8 / count);
                            this.progressBar.Refresh();
                        }
                    }
                    this.progressBar.SetTip("正在上传红字货物运输业增值税专用发票信息表", "请等待任务完成", "红字货物运输业增值税专用发票信息表上传中");
                    this.progressBar.Visible = false;
                    this.BindGridData();
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[红字货物运输业增值税专用发票信息表上传]" + exception.Message);
            }
            finally
            {
                if (this.progressBar != null)
                {
                    this.progressBar.Visible = false;
                }
            }
        }

        private void tool_tongji_Click(object sender, EventArgs e)
        {
            this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoGrid").Statistics(this.aisinoGrid);
        }

        private void tool_tuichu_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void tool_TxtRetrieve_TextChanged(object sender, EventArgs e)
        {
            this.queryRetrieve = this.tool_TxtRetrieve.Text;
            if (this.retrieveTrigger)
            {
                this.BindGridData();
            }
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
                    Text = "红字货物运输业增值税专用发票信息表审核结果下载条件设置",
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
                    document.Save(this.exePath + @"\Download_HYInput.xml");
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
                        if (File.Exists(Path.Combine(new string[] { this.exePath + "Download_HYInput.xml" })))
                        {
                            File.Delete(Path.Combine(new string[] { this.exePath + "Download_HYInput.xml" }));
                        }
                    }
                    else
                    {
                        if (xml != null)
                        {
                            this.progressBar.SetTip("正在下载红字货物运输业增值税专用发票信息表", "请等待任务完成", "红字货物运输业增值税专用发票信息表下载中");
                            XmlDocument document2 = new XmlDocument();
                            document2.LoadXml(xml);
                            document2.Save(this.exePath + @"\Download_HYOutput_Feedback.xml");
                            XmlDocument document3 = new XmlDocument();
                            document3.Load(this.exePath + @"\Download_HYOutput_Feedback.xml");
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
                                                goto Label_0555;
                                            }
                                            if (str16 == "returnMessage")
                                            {
                                                goto Label_056F;
                                            }
                                            if (str16 == "hzxxb")
                                            {
                                                goto Label_0589;
                                            }
                                        }
                                        else
                                        {
                                            str7 = node3.Attributes["value"].Value;
                                        }
                                    }
                                    continue;
                                Label_0555:
                                    str8 = node3.Attributes["value"].Value;
                                    continue;
                                Label_056F:
                                    str9 = node3.Attributes["value"].Value;
                                    continue;
                                Label_0589:
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
                                    document4.Save(this.exePath + @"\Download_HYOutput_SqdInfo_" + str7 + ".xml");
                                    XmlDocument document5 = new XmlDocument();
                                    document5.Load(this.exePath + @"\Download_HYOutput_SqdInfo_" + str7 + ".xml");
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
                                    if (File.Exists(Path.Combine(new string[] { this.exePath + "Download_HYOutput_SqdInfo_" + str7 + ".xml" })))
                                    {
                                        File.Delete(Path.Combine(new string[] { this.exePath + "Download_HYOutput_SqdInfo_" + str7 + ".xml" }));
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
                            if (File.Exists(Path.Combine(new string[] { this.exePath + "Download_HYOutput_Feedback.xml" })))
                            {
                                File.Delete(Path.Combine(new string[] { this.exePath + "Download_HYOutput_Feedback.xml" }));
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

        private void tool_xiugai_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = this.aisinoGrid.SelectedRows;
            if (rows.Count > 0)
            {
                DataGridViewRow row = rows[0];
                string str = row.Cells["SQDH"].Value.ToString();
                string state = row.Cells["XXBZT"].Value.ToString();
                if (this.ForbidOperation(state, this.ModifyForbidSet))
                {
                    MessageManager.ShowMsgBox("INP-431406", new string[] { string.Empty, str, row.Cells["XXBMS"].Value.ToString() });
                }
                else
                {
                    try
                    {
                        HySqdTianKai kai = new HySqdTianKai {
                            Text = "红字货物运输业增值税专用发票信息表修改",
                            sqdh = str
                        };
                        kai.InitSqdMx(InitSqdMxType.Edit, null);
                        kai.Show(FormMain.control_0);
                    }
                    catch (BaseException exception)
                    {
                        ExceptionHandler.HandleError(exception);
                    }
                    catch (Exception exception2)
                    {
                        ExceptionHandler.HandleError(exception2);
                    }
                }
            }
            else
            {
                MessageManager.ShowMsgBox("INP-431403");
            }
        }

        private void ToolStripMenuItemLb_Click(object sender, EventArgs e)
        {
            try
            {
                AisinoDataGrid controlByName = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoGrid");
                if (controlByName.Rows.Count <= 0)
                {
                    MessageManager.ShowMsgBox("INP-431425", new string[] { string.Empty });
                }
                else
                {
                    controlByName.Print("红字货物运输业增值税专用发票信息表列表", this, null, null, true);
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        private void ToolStripMenuItemSqd_Click(object sender, EventArgs e)
        {
            if (this.aisinoGrid.SelectedRows.Count <= 0)
            {
                MessageManager.ShowMsgBox("INP-431403");
            }
            else
            {
                string[] strArray = new string[this.aisinoGrid.SelectedRows.Count];
                for (int i = 0; i < this.aisinoGrid.SelectedRows.Count; i++)
                {
                    strArray[i] = this.aisinoGrid.SelectedRows[i].Cells["SQDH"].Value.ToString();
                    new HZFPSQDPrint("1" + strArray[i]).Print(true);
                }
            }
        }

        private delegate void PerformStepHandle(int step);
    }
}

