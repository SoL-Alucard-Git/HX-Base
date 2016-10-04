namespace Aisino.Fwkp.Hzfp.Form
{
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Hzfp.Common;
    using Aisino.Fwkp.Hzfp.Form.Common;
    using Aisino.Fwkp.Hzfp.IBLL;
    using Aisino.Fwkp.Hzfp.Model;
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

    public class SqdChaXun : DockForm
    {
        private AisinoDataGrid aisinoGrid;
        private ToolStripButton but_mingxi;
        private AisinoBTN but_quxiao;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip;
        private string exePath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Log\");
        private ILog loger = LogUtil.GetLogger<SqdChaXun>();
        private FPProgressBar progressBar = new FPProgressBar();
        private string queryGfmc = "";
        private string queryGfsh = "";
        private string queryJsrq = "2099-12-31";
        private string queryKsrq = "1900-01-01";
        private string queryQuick = "";
        private string querySqdh = "";
        private string queryState = "all";
        private string queryXfmc = "";
        private string queryXfsh = "";
        private bool quickTrigger = true;
        private readonly IHZFP_SQD sqdDal = BLLFactory.CreateInstant<IHZFP_SQD>("HZFP_SQD");
        private readonly IHZFP_SQD_MX sqdMxDal = BLLFactory.CreateInstant<IHZFP_SQD_MX>("HZFP_SQD_MX");
        private ToolStripButton tool_chazhao;
        private ToolStripButton tool_daochu;
        private ToolStripButton tool_daying;
        private ToolStripButton tool_geshi;
        private ToolStripLabel tool_LblRetrieve;
        private ToolStripButton tool_shanchu;
        private ToolStripButton tool_shangchuan;
        private ToolStripButton tool_tongji;
        private ToolStripButton tool_tuichu;
        private ToolStripTextBox tool_TxtQuick;
        private ToolStripButton tool_xiazai;
        private ToolStripButton tool_xiugai;
        private ToolStrip toolStrip1;
        private ToolStripMenuItem ToolStripMenuItemLb;
        private ToolStripMenuItem ToolStripMenuItemSqd;
        private XmlComponentLoader xmlComponentLoader1;

        public SqdChaXun()
        {
            this.Initialize();
            List<Dictionary<string, string>> list = SpecialInvInfoTableCommon.CreateGridHeader();
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

        private void aisinoGrid_DataGridRowClickEvent(object sender, DataGridRowEventArgs e)
        {
        }

        private void aisinoGrid_DataGridRowDbClickEvent(object sender, DataGridRowEventArgs e)
        {
            DataGridViewSelectedRowCollection rows = this.aisinoGrid.SelectedRows;
            if (rows.Count > 0)
            {
                DataGridViewRow row = rows[0];
                string str = row.Cells["SQDH"].Value.ToString();
                SqdTianKai kai = new SqdTianKai {
                    Text = "红字发票信息表查看",
                    sqdh = str
                };
                kai.InitSqdMx(InitSqdMxType.Read, null);
                kai.ShowDialog();
            }
        }

        private void aisinoGrid_GoToPageEvent(object sender, GoToPageEventArgs e)
        {
            this.sqdDal.CurrentPage = e.PageNO;
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("sqdh", "%" + this.querySqdh + "%");
            dict.Add("gfmc", "%" + this.queryGfmc + "%");
            dict.Add("gfsh", "%" + this.queryGfsh + "%");
            dict.Add("xfmc", "%" + this.queryXfmc + "%");
            dict.Add("xfsh", "%" + this.queryXfsh + "%");
            dict.Add("ksrq", this.queryKsrq);
            dict.Add("jsrq", this.queryJsrq);
            dict.Add("xxbzt", this.queryState);
            dict.Add("bbbz", "0");
            dict.Add("reqnsrsbh", base.TaxCardInstance.TaxCode.Trim());
            dict.Add("oldreqnsrsbh", SqdTianKai.oldsh);
            dict.Add("qquick", "%" + this.queryQuick + "%");
            this.aisinoGrid.DataSource=(this.sqdDal.SelectSqdlist(e.PageNO, e.PageSize, dict));
            PropertyUtil.SetValue("Aisino.Fwkp.Hzfp.pagesize", e.PageSize.ToString());
        }

        private void BindGridData()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("sqdh", "%" + this.querySqdh + "%");
            dict.Add("gfmc", "%" + this.queryGfmc + "%");
            dict.Add("gfsh", "%" + this.queryGfsh + "%");
            dict.Add("xfmc", "%" + this.queryXfmc + "%");
            dict.Add("xfsh", "%" + this.queryXfsh + "%");
            dict.Add("ksrq", this.queryKsrq);
            dict.Add("jsrq", this.queryJsrq);
            dict.Add("xxbzt", this.queryState);
            dict.Add("bbbz", "0");
            dict.Add("reqnsrsbh", base.TaxCardInstance.TaxCode.Trim());
            dict.Add("oldreqnsrsbh", SqdTianKai.oldsh);
            dict.Add("qquick", "%" + this.queryQuick + "%");
            this.sqdDal.CurrentPage = 1;
            this.aisinoGrid.DataSource=(this.sqdDal.SelectSqdlist(this.sqdDal.CurrentPage, this.sqdDal.Pagesize, dict));
        }

        private void but_mingxi_Click(object sender, EventArgs e)
        {
            if (this.aisinoGrid.SelectedRows.Count <= 0)
            {
                MessageManager.ShowMsgBox("INP-431331");
            }
            else
            {
                this.aisinoGrid_DataGridRowDbClickEvent(null, null);
            }
        }

        private void but_quxiao_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void CreateSingleTableNodeInfo(XmlDocument doc, XmlElement xminput, int i)
        {
            XmlNode newChild = doc.CreateElement("RedInvReqBill");
            xminput.AppendChild(newChild);
            XmlElement element = doc.CreateElement("ReqNsrsbh");
            element.InnerText = this.aisinoGrid.SelectedRows[i].Cells["REQNSRSBH"].Value.ToString();
            newChild.AppendChild(element);
            XmlElement element2 = doc.CreateElement("Kpjh");
            element2.InnerText = this.aisinoGrid.SelectedRows[i].Cells["KPJH"].Value.ToString();
            newChild.AppendChild(element2);
            XmlElement element3 = doc.CreateElement("Sbbh");
            element3.InnerText = this.aisinoGrid.SelectedRows[i].Cells["JSPH"].Value.ToString();
            newChild.AppendChild(element3);
            XmlElement element4 = doc.CreateElement("ReqBillNo");
            element4.InnerText = this.aisinoGrid.SelectedRows[i].Cells["SQDH"].Value.ToString();
            newChild.AppendChild(element4);
            XmlElement element5 = doc.CreateElement("BillType");
            object obj2 = this.aisinoGrid.SelectedRows[i].Cells["BBBZ"].Value;
            element5.InnerText = (obj2 == null) ? "0" : obj2.ToString();
            newChild.AppendChild(element5);
            XmlElement element6 = doc.CreateElement("TypeCode");
            element6.InnerText = this.aisinoGrid.SelectedRows[i].Cells["FPDM"].Value.ToString();
            newChild.AppendChild(element6);
            XmlElement element7 = doc.CreateElement("InvNo");
            if (this.aisinoGrid.SelectedRows[i].Cells["FPHM"].Value.ToString() != "")
            {
                element7.InnerText = string.Format("{0:00000000}", Convert.ToInt32(this.aisinoGrid.SelectedRows[i].Cells["FPHM"].Value.ToString()));
            }
            else
            {
                element7.InnerText = "";
            }
            newChild.AppendChild(element7);
            XmlElement element8 = doc.CreateElement("Szlb");
            element8.InnerText = "1";
            newChild.AppendChild(element8);
            object obj3 = this.aisinoGrid.SelectedRows[i].Cells["SL"].Value;
            short num = 0;
            if (obj3.ToString() == "多税率")
            {
                num = 1;
            }
            XmlElement element9 = doc.CreateElement("IsMutiRate");
            element9.InnerText = num.ToString();
            newChild.AppendChild(element9);
            XmlElement element10 = doc.CreateElement("Date");
            element10.InnerText = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(this.aisinoGrid.SelectedRows[i].Cells["TKRQ"].Value.ToString()));
            newChild.AppendChild(element10);
            XmlElement element11 = doc.CreateElement("BuyerName");
            element11.InnerText = this.aisinoGrid.SelectedRows[i].Cells["GFMC"].Value.ToString();
            newChild.AppendChild(element11);
            XmlElement element12 = doc.CreateElement("BuyTaxCode");
            element12.InnerText = this.aisinoGrid.SelectedRows[i].Cells["GFSH"].Value.ToString();
            newChild.AppendChild(element12);
            XmlElement element13 = doc.CreateElement("SellerName");
            element13.InnerText = this.aisinoGrid.SelectedRows[i].Cells["XFMC"].Value.ToString();
            newChild.AppendChild(element13);
            XmlElement element14 = doc.CreateElement("SellTaxCode");
            element14.InnerText = this.aisinoGrid.SelectedRows[i].Cells["XFSH"].Value.ToString();
            newChild.AppendChild(element14);
            XmlElement element15 = doc.CreateElement("Amount");
            element15.InnerText = string.Format("{0:0.00}", Convert.ToDouble(this.aisinoGrid.SelectedRows[i].Cells["HJJE"].Value.ToString()));
            newChild.AppendChild(element15);
            XmlElement element16 = doc.CreateElement("TaxRate");
            if (num == 1)
            {
                element16.InnerText = "";
            }
            else if (obj3.ToString() == "免税")
            {
                element16.InnerText = "0";
            }
            else
            {
                element16.InnerText = obj3.ToString();
            }
            newChild.AppendChild(element16);
            XmlElement element17 = doc.CreateElement("Tax");
            element17.InnerText = string.Format("{0:0.00}", Convert.ToDouble(this.aisinoGrid.SelectedRows[i].Cells["HJSE"].Value.ToString()));
            newChild.AppendChild(element17);
            XmlElement element18 = doc.CreateElement("ReqMemo");
            string str = this.aisinoGrid.SelectedRows[i].Cells["SQXZ"].Value.ToString();
            if (str.Length > 10)
            {
                str = str.Substring(0, 10);
            }
            element18.InnerText = str;
            newChild.AppendChild(element18);
            if (SqdTianKai.FLBMqy)
            {
                XmlElement element19 = doc.CreateElement("SPBMBBH");
                element19.InnerText = this.aisinoGrid.SelectedRows[i].Cells["FLBMBBBH"].Value.ToString();
                newChild.AppendChild(element19);
            }
            HZFP_SQD hzfp_sqd = this.sqdDal.Select(this.aisinoGrid.SelectedRows[i].Cells["SQDH"].Value.ToString());
            XmlElement element20 = doc.CreateElement("SLBZ");
            if ((hzfp_sqd.SL.ToString().Trim() == "0.015") || ((hzfp_sqd.YYSBZ.Trim().Substring(8, 1) == "0") && ((hzfp_sqd.SL.ToString().Trim() == "0.05") || (hzfp_sqd.SL.ToString().Trim() == "0.050"))))
            {
                element20.InnerText = "1";
            }
            else if (hzfp_sqd.YYSBZ.Trim().Substring(8, 1) == "2")
            {
                element20.InnerText = "2";
            }
            else
            {
                element20.InnerText = "0";
            }
            newChild.AppendChild(element20);
            XmlElement element21 = doc.CreateElement("RedInvReqBillMx");
            newChild.AppendChild(element21);
            DataTable table = this.sqdMxDal.SelectList(this.aisinoGrid.SelectedRows[i].Cells["SQDH"].Value.ToString());
            for (int j = 0; j < table.Rows.Count; j++)
            {
                DataRow row = table.Rows[j];
                XmlElement element22 = doc.CreateElement("GoodsMx");
                element21.AppendChild(element22);
                XmlElement element23 = doc.CreateElement("GoodsName");
                element23.InnerText = GetSafeData.ValidateValue<string>(row, "SPMC");
                element22.AppendChild(element23);
                XmlElement element24 = doc.CreateElement("GoodsUnit");
                element24.InnerText = GetSafeData.ValidateValue<string>(row, "JLDW");
                element22.AppendChild(element24);
                XmlElement element25 = doc.CreateElement("GoodsPrice");
                string str2 = !GetSafeData.ValidateValue<bool>(row, "HSJBZ") ? "N" : "Y";
                string s = GetSafeData.ValidateDoubleValue(row, "SLV");
                string str4 = GetSafeData.ValidateValue<string>(row, "DJ");
                if ((((s != null) && (str4 != null)) && ((s != "") && (str4 != ""))) && ((str2.Trim() == "Y") && (!(s.Trim() == "0.05") || !(hzfp_sqd.YYSBZ.Trim().Substring(8, 1) == "0"))))
                {
                    decimal d = decimal.Parse(s);
                    
                    element25.InnerText = decimal.Round(decimal.Divide(decimal.Parse(str4), decimal.Add(d,1)), 15, MidpointRounding.AwayFromZero).ToString();
                }
                else
                {
                    element25.InnerText = GetSafeData.ValidateValue<string>(row, "DJ");
                }
                element22.AppendChild(element25);
                XmlElement element26 = doc.CreateElement("GoodsTaxRate");
                element26.InnerText = GetSafeData.ValidateDoubleValue(row, "SLV");
                element22.AppendChild(element26);
                XmlElement element27 = doc.CreateElement("GoodsGgxh");
                element27.InnerText = GetSafeData.ValidateValue<string>(row, "GGXH");
                element22.AppendChild(element27);
                XmlElement element28 = doc.CreateElement("GoodsNum");
                element28.InnerText = GetSafeData.ValidateValue<string>(row, "SL");
                element22.AppendChild(element28);
                XmlElement element29 = doc.CreateElement("GoodsJE");
                element29.InnerText = string.Format("{0:0.00}", GetSafeData.ValidateValue<decimal>(row, "JE"));
                element22.AppendChild(element29);
                XmlElement element30 = doc.CreateElement("GoodsSE");
                element30.InnerText = string.Format("{0:0.00}", GetSafeData.ValidateValue<decimal>(row, "SE"));
                element22.AppendChild(element30);
                XmlElement element31 = doc.CreateElement("HS_BZ");
                if ((hzfp_sqd.YYSBZ.Trim().Substring(8, 1) == "0") && ((s.Trim() == "0.05") || (s.Trim() == "0.050")))
                {
                    element31.InnerText = "Y";
                }
                else
                {
                    element31.InnerText = "N";
                }
                element22.AppendChild(element31);
                if (SqdTianKai.FLBMqy)
                {
                    XmlElement element32 = doc.CreateElement("SPBM");
                    element32.InnerText = GetSafeData.ValidateValue<string>(row, "FLBM");
                    element22.AppendChild(element32);
                    XmlElement element33 = doc.CreateElement("QYSPBM");
                    element33.InnerText = GetSafeData.ValidateValue<string>(row, "QYSPBM");
                    element22.AppendChild(element33);
                    XmlElement element34 = doc.CreateElement("SYYHZCBZ");
                    string str5 = GetSafeData.ValidateValue<string>(row, "SFXSYHZC");
                    element34.InnerText = (str5 == "1") ? "1" : "0";
                    element22.AppendChild(element34);
                    XmlElement element35 = doc.CreateElement("YHZC");
                    element35.InnerText = GetSafeData.ValidateValue<string>(row, "YHZCMC");
                    element22.AppendChild(element35);
                    XmlElement element36 = doc.CreateElement("LSLBZ");
                    element36.InnerText = GetSafeData.ValidateValue<string>(row, "LSLBS");
                    element22.AppendChild(element36);
                }
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
            this.tool_tuichu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_tuichu");
            this.tool_chazhao = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_chazhao");
            this.tool_daying = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_daying");
            this.tool_tongji = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_tongji");
            this.tool_geshi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_geshi");
            this.tool_xiugai = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_xiugai");
            this.tool_shanchu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_shanchu");
            this.aisinoGrid = this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoGrid");
            this.tool_daochu = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_daochu");
            this.tool_shangchuan = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_shangchuan");
            this.tool_xiazai = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_xiazai");
            this.but_mingxi = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("but_mingxi");
            this.but_quxiao = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_quxiao");
            this.tool_LblRetrieve = this.xmlComponentLoader1.GetControlByName<ToolStripLabel>("toolLblRetrieve");
            this.tool_TxtQuick = this.xmlComponentLoader1.GetControlByName<ToolStripTextBox>("tool_TxtQuick");
            this.tool_TxtQuick.TextChanged += new EventHandler(this.tool_TxtQuick_TextChanged);
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            this.tool_tuichu.Margin = new Padding(20, 1, 0, 2);
            ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
            this.tool_TxtQuick.Alignment = ToolStripItemAlignment.Right;
            this.tool_LblRetrieve.Alignment = ToolStripItemAlignment.Right;
            this.tool_tuichu.Click += new EventHandler(this.tool_tuichu_Click);
            this.tool_chazhao.Click += new EventHandler(this.tool_chazhao_Click);
            this.tool_daying.Click += new EventHandler(this.tool_daying_Click);
            this.tool_tongji.Click += new EventHandler(this.tool_tongji_Click);
            this.tool_geshi.Click += new EventHandler(this.tool_geshi_Click);
            this.tool_xiugai.Click += new EventHandler(this.tool_xiugai_Click);
            this.tool_shanchu.Click += new EventHandler(this.tool_shanchu_Click);
            this.tool_daochu.Click += new EventHandler(this.tool_daochu_Click);
            this.tool_shangchuan.Click += new EventHandler(this.tool_shangchuan_Click);
            this.tool_xiazai.Click += new EventHandler(this.tool_xiazai_Click);
            this.but_mingxi.Click += new EventHandler(this.but_mingxi_Click);
            this.but_quxiao.Click += new EventHandler(this.but_quxiao_Click);
            if (TaxCardFactory.CreateTaxCard().QYLX.ISTDQY)
            {
                this.tool_shangchuan.Visible = false;
                this.tool_xiazai.Visible = false;
            }
            this.contextMenuStrip = new ContextMenuStrip();
            this.ToolStripMenuItemSqd = new ToolStripMenuItem("信息表");
            this.ToolStripMenuItemLb = new ToolStripMenuItem("信息表列表");
            this.contextMenuStrip.Items.Add(this.ToolStripMenuItemSqd);
            this.contextMenuStrip.Items.Add(this.ToolStripMenuItemLb);
            this.ToolStripMenuItemSqd.Click += new EventHandler(this.ToolStripMenuItemSqd_Click);
            this.ToolStripMenuItemLb.Click += new EventHandler(this.ToolStripMenuItemLb_Click);
            this.aisinoGrid.ReadOnly=(true);
            this.aisinoGrid.DataGridRowDbClickEvent+=(new EventHandler<DataGridRowEventArgs>(this.aisinoGrid_DataGridRowDbClickEvent));
            this.aisinoGrid.GoToPageEvent+=(new EventHandler<GoToPageEventArgs>(this.aisinoGrid_GoToPageEvent));
            this.aisinoGrid.DataGrid.AllowUserToDeleteRows = false;
            ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SqdChaXun));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x2c2, 0x21a);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.XMLPath=(@"..\Config\Components\Aisino.Fwkp.Hzfp.Form.SqdChaXun\Aisino.Fwkp.Hzfp.Form.SqdChaXun.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2c2, 0x21a);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "SqdChaXun";
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "红字发票信息表查询导出";
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

        private void tool_chazhao_Click(object sender, EventArgs e)
        {
            ChaXunTiaoJian jian = new ChaXunTiaoJian {
                AisinoGrid = this.aisinoGrid
            };
            if (jian.ShowDialog(this) == DialogResult.OK)
            {
                this.querySqdh = jian.Sqdh;
                this.queryGfmc = jian.Gfmc;
                this.queryGfsh = jian.Gfsh;
                this.queryXfmc = jian.Xfmc;
                this.queryXfsh = jian.Xfsh;
                this.queryKsrq = jian.DateTimeStart.ToString("yyyy-MM-dd");
                this.queryJsrq = jian.DateTimeEnd.ToString("yyyy-MM-dd");
                this.queryState = jian.Sqdzt;
                this.quickTrigger = false;
                this.tool_TxtQuick.Text = "";
                this.quickTrigger = true;
                this.BindGridData();
            }
        }

        private void tool_daochu_Click(object sender, EventArgs e)
        {
            int count = this.aisinoGrid.SelectedRows.Count;
            if (count <= 0)
            {
                MessageManager.ShowMsgBox("INP-431331");
            }
            else if (DialogResult.OK == MessageManager.ShowMsgBox("INP-431313", "提示", new string[] { Convert.ToString(count) }))
            {
                string path = string.Empty;
                FolderBrowserDialog dialog = new FolderBrowserDialog {
                    Description = "选择导出的目录",
                    RootFolder = Environment.SpecialFolder.Desktop,
                    ShowNewFolderButton = true
                };
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    path = dialog.SelectedPath;
                    bool flag = true;
                    for (int i = 0; i < this.aisinoGrid.SelectedRows.Count; i++)
                    {
                        string str2 = base.TaxCardInstance.TaxCode + "_" + Convert.ToString(base.TaxCardInstance.Machine) + "_" + this.aisinoGrid.SelectedRows[i].Cells["SQDH"].Value.ToString() + ".xml";
                        XmlDocument doc = new XmlDocument();
                        XmlDeclaration newChild = doc.CreateXmlDeclaration("1.0", "GBK", null);
                        doc.PreserveWhitespace = false;
                        doc.AppendChild(newChild);
                        XmlElement element = doc.CreateElement("FPXT");
                        doc.AppendChild(element);
                        XmlElement element2 = doc.CreateElement("INPUT");
                        element.AppendChild(element2);
                        this.CreateSingleTableNodeInfo(doc, element2, i);
                        doc.PreserveWhitespace = true;
                        if (!Directory.Exists(path))
                        {
                            MessageManager.ShowMsgBox("INP-431367", "提示", new string[] { path });
                            flag = false;
                            break;
                        }
                        doc.Save(path + @"\" + str2);
                    }
                    if (flag)
                    {
                        MessageManager.ShowMsgBox("INP-431360", "提示", new string[] { Convert.ToString(count) });
                    }
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

        private void tool_shanchu_Click(object sender, EventArgs e)
        {
            if (this.aisinoGrid.SelectedRows.Count <= 0)
            {
                MessageManager.ShowMsgBox("INP-431331");
            }
            else if (DialogResult.OK == MessageManager.ShowMsgBox("INP-431337", "提示", new string[] { Convert.ToString(this.aisinoGrid.SelectedRows.Count) }))
            {
                for (int i = 0; i < this.aisinoGrid.SelectedRows.Count; i++)
                {
                    string sQDH = this.aisinoGrid.SelectedRows[i].Cells["SQDH"].Value.ToString();
                    if (this.sqdDal.Delete(sQDH))
                    {
                        this.sqdMxDal.Delete(sQDH);
                    }
                }
                MessageManager.ShowMsgBox("INP-431338", "提示", new string[] { Convert.ToString(this.aisinoGrid.SelectedRows.Count) });
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
                    MessageManager.ShowMsgBox("INP-431331");
                }
                else
                {
                    if (DialogResult.OK == MessageManager.ShowMsgBox("INP-431332", "提示", new string[] { Convert.ToString(count) }))
                    {
                        XmlDocument doc = new XmlDocument();
                        XmlDeclaration newChild = doc.CreateXmlDeclaration("1.0", "GBK", null);
                        doc.PreserveWhitespace = false;
                        doc.AppendChild(newChild);
                        XmlElement element = doc.CreateElement("FPXT");
                        doc.AppendChild(element);
                        XmlElement element2 = doc.CreateElement("INPUT");
                        element.AppendChild(element2);
                        int num2 = 0;
                        int num3 = 0;
                        int num4 = 0;
                        for (int i = 0; i < this.aisinoGrid.SelectedRows.Count; i++)
                        {
                            this.CreateSingleTableNodeInfo(doc, element2, i);
                        }
                        doc.PreserveWhitespace = true;
                        doc.Save(this.exePath + "shangchuan_shuru.xml");
                        string innerText = string.Empty;
                        string str2 = string.Empty;
                        string sQDH = string.Empty;
                        string str4 = string.Empty;
                        string str5 = string.Empty;
                        string str6 = string.Empty;
                        this.progressBar.SetTip("正在连接服务器", "请等待任务完成", "信息表上传中");
                        this.progressBar.fpxf_progressBar.Value = 1;
                        this.progressBar.Show();
                        this.progressBar.Refresh();
                        this.ProcessStartThread(0x1b58);
                        this.progressBar.Refresh();
                        string xml = string.Empty;
                        int num6 = HttpsSender.SendMsg("0008", doc.InnerXml, out xml);
                        if (num6 != 0)
                        {
                            MessageManager.ShowMsgBox("INP-431370", new string[] { "上传", num6.ToString(), xml });
                        }
                        else if (xml == "")
                        {
                            MessageManager.ShowMsgBox("INP-431368", new string[] { "上传" });
                        }
                        else
                        {
                            this.progressBar.SetTip("正在上传信息表", "请等待任务完成", "信息表上传中");
                            XmlDocument document2 = new XmlDocument();
                            document2.LoadXml(xml);
                            document2.Save(this.exePath + "shangchuan_shuchu.xml");
                            XmlDocument document3 = new XmlDocument();
                            document3.Load(this.exePath + "shangchuan_shuchu.xml");
                            foreach (XmlNode node2 in document3.SelectSingleNode("FPXT").ChildNodes)
                            {
                                XmlElement element3 = (XmlElement) node2;
                                foreach (XmlNode node3 in element3.ChildNodes)
                                {
                                    if ("CODE".Equals(node3.Name))
                                    {
                                        innerText = node3.InnerText;
                                        innerText = innerText.Replace("\r\n", "");
                                        innerText = innerText.Trim();
                                    }
                                    else if ("MESS".Equals(node3.Name))
                                    {
                                        str2 = node3.InnerText;
                                        str2 = str2.Replace("\r\n", "");
                                        str2 = str2.Trim();
                                    }
                                    else if ("DATA".Equals(node3.Name))
                                    {
                                        if (innerText != "0000")
                                        {
                                            MessageManager.ShowMsgBox("INP-431336", new string[] { innerText + " " + str2 });
                                        }
                                        else
                                        {
                                            XmlElement element4 = (XmlElement) node3;
                                            foreach (XmlNode node4 in element4.ChildNodes)
                                            {
                                                if ("RedInvReqBill".Equals(node4.Name))
                                                {
                                                    this.ProcessStartThread(0x1388 / count);
                                                    this.progressBar.Refresh();
                                                    num2++;
                                                    XmlElement element5 = (XmlElement) node4;
                                                    foreach (XmlNode node5 in element5.ChildNodes)
                                                    {
                                                        string name = node5.Name;
                                                        if (name != null)
                                                        {
                                                            if (!(name == "ReqBillNo"))
                                                            {
                                                                if (name == "ResBillNo")
                                                                {
                                                                    goto Label_0462;
                                                                }
                                                                if (name == "StatusDM")
                                                                {
                                                                    goto Label_046D;
                                                                }
                                                                if (name == "StatusMC")
                                                                {
                                                                    goto Label_0499;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                sQDH = node5.InnerText;
                                                            }
                                                        }
                                                        continue;
                                                    Label_0462:
                                                        str4 = node5.InnerText;
                                                        continue;
                                                    Label_046D:
                                                        str5 = node5.InnerText;
                                                        if (str5.Trim() == "TZD0000")
                                                        {
                                                            num3++;
                                                        }
                                                        else
                                                        {
                                                            num4++;
                                                        }
                                                        continue;
                                                    Label_0499:
                                                        str6 = node5.InnerText;
                                                    }
                                                    HZFP_SQD model = this.sqdDal.Select(sQDH);
                                                    if (model != null)
                                                    {
                                                        model.XXBBH = str4;
                                                        model.XXBZT = str5;
                                                        model.XXBMS = str6;
                                                        this.sqdDal.Updatazt(model);
                                                    }
                                                    else
                                                    {
                                                        MessageManager.ShowMsgBox("此信息表流水号在本地数据库中不存在");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        this.progressBar.SetTip("正在上传信息表", "请等待任务完成", "信息表上传中");
                        this.progressBar.Visible = false;
                        this.BindGridData();
                    }
                    if (File.Exists(Path.Combine(this.exePath, "shangchuan_shuru.xml")))
                    {
                        File.Delete(Path.Combine(this.exePath, "shangchuan_shuru.xml"));
                    }
                    if (File.Exists(Path.Combine(this.exePath, "shangchuan_shuchu.xml")))
                    {
                        File.Delete(Path.Combine(this.exePath, "shangchuan_shuchu.xml"));
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[信息表上传]" + exception.Message);
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

        private void tool_TxtQuick_TextChanged(object sender, EventArgs e)
        {
            this.queryQuick = this.tool_TxtQuick.Text;
            if (this.quickTrigger)
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
                string str5 = string.Empty;
                string ghfsh = string.Empty;
                string xhfsh = string.Empty;
                string xxbbh = string.Empty;
                string xxbfw = string.Empty;
                string str10 = string.Empty;
                str10 = "1000";
                int num = 0;
                int num2 = 0;
                string str11 = string.Empty;
                int num3 = 0;
                int num4 = 0;
                string innerText = string.Empty;
                string str13 = string.Empty;
                string str14 = string.Empty;
                string sQDH = string.Empty;
                string str16 = string.Empty;
                string str17 = string.Empty;
                string str18 = string.Empty;
                str = base.TaxCardInstance.TaxCode;
                str2 = base.TaxCardInstance.GetInvControlNum().Trim();
                str3 = Convert.ToString(base.TaxCardInstance.Machine);
                XiaZaiTiaoJian jian = new XiaZaiTiaoJian {
                    AisinoGrid = this.aisinoGrid
                };
                if (jian.ShowDialog(this) == DialogResult.OK)
                {
                    str4 = jian.DateTimeStart.ToString("yyyy-MM-dd");
                    str5 = jian.DateTimeEnd.ToString("yyyy-MM-dd");
                    ghfsh = jian.Ghfsh;
                    xhfsh = jian.Xhfsh;
                    xxbbh = jian.Xxbbh;
                    xxbfw = jian.Xxbfw;
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
                        element6.InnerText = "N";
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
                        element14.InnerText = str10;
                        element2.AppendChild(element14);
                        document.PreserveWhitespace = true;
                        document.Save(this.exePath + "xiazai_shuru_" + num.ToString() + ".xml");
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
                            document2.Save(this.exePath + "xiazai_shuchu_" + num.ToString() + ".xml");
                            XmlDocument document3 = new XmlDocument();
                            document3.Load(this.exePath + "xiazai_shuchu_" + num.ToString() + ".xml");
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
                                        str13 = node3.InnerText;
                                        str13 = str13.Replace("\r\n", "");
                                        str13 = str13.Trim();
                                    }
                                    else if ("DATA".Equals(node3.Name))
                                    {
                                        if (innerText != "0000")
                                        {
                                            MessageManager.ShowMsgBox("INP-431334", new string[] { innerText + " " + str13 });
                                        }
                                        else
                                        {
                                            XmlElement element16 = (XmlElement) node3;
                                            foreach (XmlNode node4 in element16.ChildNodes)
                                            {
                                                if ("ALLCOUNT".Equals(node4.Name))
                                                {
                                                    str11 = node4.InnerText;
                                                    str11 = str11.Replace("\r\n", "");
                                                    str11 = str11.Trim();
                                                    num2 = Convert.ToInt32(str11);
                                                    int num1 = Convert.ToInt32(str11) / Convert.ToInt32(str10);
                                                }
                                                else if ("RedInvReqBill".Equals(node4.Name) && (Convert.ToDouble(str11) != 0.0))
                                                {
                                                    this.ProcessStartThread(0x1388 / num2);
                                                    this.progressBar.Refresh();
                                                    bool flag = true;
                                                    bool flag2 = true;
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
                                                        string str23;
                                                        string str25;
                                                        string str27;
                                                        string str28;
                                                        string str29;
                                                        if ("ReqBillNo".Equals(node5.Name))
                                                        {
                                                            string str20 = element17.SelectSingleNode("ReqMemo").InnerXml.Trim();
                                                            string str21 = element17.SelectSingleNode("BuyTaxCode").InnerXml.Trim();
                                                            string str22 = element17.SelectSingleNode("SellTaxCode").InnerXml.Trim();
                                                            sQDH = node5.InnerText.Trim();
                                                            str14 = sQDH.Substring(0, 12);
                                                            switch (str20)
                                                            {
                                                                case "Y":
                                                                case "N1":
                                                                case "N2":
                                                                case "N3":
                                                                case "N4":
                                                                {
                                                                    if (str21 == base.TaxCardInstance.TaxCode.Trim())
                                                                    {
                                                                        flag = true;
                                                                        num3++;
                                                                        Convert.ToString(base.TaxCardInstance.Machine);
                                                                        model.REQNSRSBH = str21;
                                                                        model.KPJH = base.TaxCardInstance.Machine;
                                                                        model.JSPH = str14;
                                                                        if (this.sqdDal.Select(sQDH) == null)
                                                                        {
                                                                            flag2 = false;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        flag = false;
                                                                        model.REQNSRSBH = str21;
                                                                        model.KPJH = 0;
                                                                        model.JSPH = str14;
                                                                    }
                                                                    continue;
                                                                }
                                                            }
                                                            flag = true;
                                                            num3++;
                                                            Convert.ToString(base.TaxCardInstance.Machine);
                                                            model.REQNSRSBH = str22;
                                                            model.KPJH = base.TaxCardInstance.Machine;
                                                            model.JSPH = str14;
                                                            if (this.sqdDal.Select(sQDH) == null)
                                                            {
                                                                flag2 = false;
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
                                                                    goto Label_0925;
                                                                }
                                                                if (name == "StatusMC")
                                                                {
                                                                    goto Label_0948;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                str16 = node5.InnerText;
                                                            }
                                                        }
                                                        goto Label_0951;
                                                    Label_0925:
                                                        str17 = node5.InnerText;
                                                        if (flag && (str17 == "TZD0000"))
                                                        {
                                                            num4++;
                                                        }
                                                        goto Label_0951;
                                                    Label_0948:
                                                        str18 = node5.InnerText;
                                                    Label_0951:
                                                        if (flag && flag2)
                                                        {
                                                            continue;
                                                        }
                                                        model.SQDH = sQDH;
                                                        model.XXBBH = str16;
                                                        model.XXBZT = str17;
                                                        model.XXBMS = str18;
                                                        switch (node5.Name)
                                                        {
                                                            case "BillType":
                                                                model.BBBZ = node5.InnerText;
                                                                goto Label_1055;

                                                            case "TypeCode":
                                                                str23 = node5.InnerText.Replace("\r\n", "").Trim();
                                                                if (Convert.ToDouble(str23) != 0.0)
                                                                {
                                                                    break;
                                                                }
                                                                model.FPDM = "";
                                                                goto Label_1055;

                                                            case "InvNo":
                                                            {
                                                                string str24 = node5.InnerText.Replace("\r\n", "").Trim();
                                                                if (!(str24 != ""))
                                                                {
                                                                    goto Label_0B5E;
                                                                }
                                                                model.FPHM = Convert.ToInt32(str24);
                                                                goto Label_1055;
                                                            }
                                                            case "Date":
                                                            {
                                                                DateTime now = DateTime.Now;
                                                                DateTime.TryParse(Convert.ToDateTime(node5.InnerText.Trim()).Date.ToString("yyyy-MM-dd").ToString().Trim(), out now);
                                                                model.TKRQ = now;
                                                                model.SSYF = Convert.ToInt32(model.TKRQ.ToString("yyyyMM"));
                                                                goto Label_1055;
                                                            }
                                                            case "BuyerName":
                                                                model.GFMC = node5.InnerText;
                                                                goto Label_1055;

                                                            case "BuyTaxCode":
                                                                model.GFSH = node5.InnerText;
                                                                goto Label_1055;

                                                            case "SellerName":
                                                                model.XFMC = node5.InnerText;
                                                                goto Label_1055;

                                                            case "SellTaxCode":
                                                                node5.InnerText.Trim();
                                                                model.XFSH = node5.InnerText;
                                                                goto Label_1055;

                                                            case "Amount":
                                                                model.HJJE = Convert.ToDecimal(node5.InnerText);
                                                                goto Label_1055;

                                                            case "TaxRate":
                                                                str25 = node5.InnerText.Replace("\r\n", "").Trim();
                                                                if ((!(str25 == "null") && (str25 != null)) && !(str25 == ""))
                                                                {
                                                                    goto Label_0CA6;
                                                                }
                                                                model.SL = -1.0;
                                                                goto Label_1055;

                                                            case "Tax":
                                                            {
                                                                string str26 = node5.InnerText.Replace("\r\n", "").Trim();
                                                                model.HJSE = Convert.ToDecimal(str26);
                                                                goto Label_1055;
                                                            }
                                                            case "ReqMemo":
                                                                str27 = node5.InnerText.Replace("\r\n", "").Trim();
                                                                str28 = string.Empty;
                                                                if (base.TaxCardInstance.StateInfo.CompanyType == 0)
                                                                {
                                                                    goto Label_0D38;
                                                                }
                                                                str28 = "1";
                                                                goto Label_0D3F;

                                                            case "SPBMBBH":
                                                                if (SqdTianKai.FLBMqy && "SPBMBBH".Equals(node5.Name))
                                                                {
                                                                    model.FLBMBBBH = node5.InnerText;
                                                                }
                                                                goto Label_1055;

                                                            case "SLBZ":
                                                                str29 = node5.InnerText.Replace("\r\n", "").Trim();
                                                                if (!str29.Equals("1") || (model.SL != 0.05))
                                                                {
                                                                    goto Label_102D;
                                                                }
                                                                model.YYSBZ = "0000000000";
                                                                goto Label_1055;

                                                            default:
                                                                goto Label_1055;
                                                        }
                                                        model.FPDM = str23;
                                                        goto Label_1055;
                                                    Label_0B5E:
                                                        model.FPHM = 0;
                                                        goto Label_1055;
                                                    Label_0CA6:
                                                        model.SL = Convert.ToDouble(str25);
                                                        goto Label_1055;
                                                    Label_0D38:
                                                        str28 = "0";
                                                    Label_0D3F:
                                                        switch (str27)
                                                        {
                                                            case "Y":
                                                                model.SQXZ = "1100000000" + str28;
                                                                goto Label_1055;

                                                            case "N1":
                                                                model.SQXZ = "1011000000" + str28;
                                                                goto Label_1055;

                                                            case "N2":
                                                                model.SQXZ = "1010100000" + str28;
                                                                goto Label_1055;

                                                            case "N3":
                                                                model.SQXZ = "1010010000" + str28;
                                                                goto Label_1055;

                                                            case "N4":
                                                                model.SQXZ = "1010001000" + str28;
                                                                goto Label_1055;

                                                            case "N5":
                                                                model.SQXZ = "0000000110" + str28;
                                                                goto Label_1055;

                                                            case "N6":
                                                                model.SQXZ = "0000000101" + str28;
                                                                goto Label_1055;

                                                            case "X1":
                                                                model.SQXZ = "10000000";
                                                                goto Label_1055;

                                                            case "X2":
                                                                model.SQXZ = "01000000";
                                                                goto Label_1055;

                                                            case "X3":
                                                                model.SQXZ = "00100000";
                                                                goto Label_1055;

                                                            case "X4":
                                                                model.SQXZ = "00010000";
                                                                goto Label_1055;

                                                            case "X5":
                                                                model.SQXZ = "00001000";
                                                                goto Label_1055;

                                                            case "X6":
                                                                model.SQXZ = "00000100";
                                                                goto Label_1055;

                                                            case "X7":
                                                                model.SQXZ = "00000010";
                                                                goto Label_1055;

                                                            case "X8":
                                                                model.SQXZ = "00000001";
                                                                goto Label_1055;

                                                            default:
                                                                goto Label_1055;
                                                        }
                                                    Label_102D:
                                                        if (str29.Equals("2"))
                                                        {
                                                            model.YYSBZ = "0000000020";
                                                        }
                                                        else
                                                        {
                                                            model.YYSBZ = "0000000010";
                                                        }
                                                    Label_1055:
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
                                                                        string str32;
                                                                        string str33;
                                                                        string str35;
                                                                        switch (node7.Name)
                                                                        {
                                                                            case "GoodsName":
                                                                            {
                                                                                item.SPMC = node7.InnerText.Replace("\r\n", "").Trim();
                                                                                continue;
                                                                            }
                                                                            case "GoodsUnit":
                                                                            {
                                                                                string str31 = node7.InnerText.Replace("\r\n", "").Trim();
                                                                                item.JLDW = str31;
                                                                                continue;
                                                                            }
                                                                            case "GoodsPrice":
                                                                            {
                                                                                str32 = node7.InnerText.Replace("\r\n", "").Trim();
                                                                                if ((!(str32 == "") && !(str32 == "0")) && ((!(str32 == "0.0") && !(str32 == "null")) && (str32 != null)))
                                                                                {
                                                                                    break;
                                                                                }
                                                                                item.DJ = 0M;
                                                                                continue;
                                                                            }
                                                                            case "GoodsTaxRate":
                                                                            {
                                                                                str33 = node7.InnerText.Replace("\r\n", "").Trim();
                                                                                if ((!(str33 == "") && (str33 != null)) && !(str33 == "null"))
                                                                                {
                                                                                    goto Label_1399;
                                                                                }
                                                                                item.SLV = -1.0;
                                                                                continue;
                                                                            }
                                                                            case "GoodsGgxh":
                                                                            {
                                                                                string str34 = node7.InnerText.Replace("\r\n", "").Trim();
                                                                                item.GGXH = str34;
                                                                                continue;
                                                                            }
                                                                            case "GoodsNum":
                                                                            {
                                                                                str35 = node7.InnerText.Replace("\r\n", "").Trim();
                                                                                if ((!(str35 == "") && !(str35 == "0")) && ((!(str35 == "0.0") && !(str35 == "null")) && (str35 != null)))
                                                                                {
                                                                                    goto Label_1455;
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
                                                                                string str36 = node7.InnerText.Replace("\r\n", "").Trim();
                                                                                item.SE = Convert.ToDecimal(str36);
                                                                                continue;
                                                                            }
                                                                            case "HS_BZ":
                                                                            {
                                                                                item.HSJBZ = node7.InnerText != "N";
                                                                                continue;
                                                                            }
                                                                            case "SPBM":
                                                                            {
                                                                                if (SqdTianKai.FLBMqy)
                                                                                {
                                                                                    item.FLBM = node7.InnerText;
                                                                                }
                                                                                continue;
                                                                            }
                                                                            case "QYSPBM":
                                                                            {
                                                                                if (SqdTianKai.FLBMqy)
                                                                                {
                                                                                    item.QYSPBM = node7.InnerText;
                                                                                }
                                                                                continue;
                                                                            }
                                                                            case "SYYHZCBZ":
                                                                            {
                                                                                if (SqdTianKai.FLBMqy)
                                                                                {
                                                                                    item.SFXSYHZC = node7.InnerText;
                                                                                }
                                                                                continue;
                                                                            }
                                                                            case "YHZC":
                                                                            {
                                                                                if (SqdTianKai.FLBMqy)
                                                                                {
                                                                                    item.YHZCMC = node7.InnerText;
                                                                                }
                                                                                continue;
                                                                            }
                                                                            case "LSLBZ":
                                                                            {
                                                                                if (SqdTianKai.FLBMqy)
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
                                                                        item.DJ = Convert.ToDecimal(str32);
                                                                        continue;
                                                                    Label_1399:
                                                                        item.SLV = Convert.ToDouble(str33);
                                                                        continue;
                                                                    Label_1455:
                                                                        item.SL = Convert.ToDouble(str35);
                                                                    }
                                                                    models.Add(item);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (flag && flag2)
                                                    {
                                                        HZFP_SQD hzfp_sqd4 = this.sqdDal.Select(sQDH);
                                                        if (hzfp_sqd4 != null)
                                                        {
                                                            hzfp_sqd4.XXBBH = str16;
                                                            hzfp_sqd4.XXBZT = str17;
                                                            hzfp_sqd4.XXBMS = str18;
                                                            this.sqdDal.Updatazt(hzfp_sqd4);
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
                    while (num2 >= Convert.ToInt32(str10));
                }
                this.progressBar.SetTip("正在下载信息表", "请等待任务完成", "信息表下载中");
                this.progressBar.Visible = false;
                this.BindGridData();
                for (int i = 0; i < num; i++)
                {
                    if (File.Exists(Path.Combine(new string[] { this.exePath + "xiazai_shuru_" + num.ToString() + ".xml" })))
                    {
                        File.Delete(Path.Combine(new string[] { this.exePath + "xiazai_shuru_" + num.ToString() + ".xml" }));
                    }
                    if (File.Exists(Path.Combine(new string[] { this.exePath + "xiazai_shuchu_" + num.ToString() + ".xml" })))
                    {
                        File.Delete(Path.Combine(new string[] { this.exePath + "xiazai_shuchu_" + num.ToString() + ".xml" }));
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

        private void tool_xiugai_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = this.aisinoGrid.SelectedRows;
            if (rows.Count > 0)
            {
                DataGridViewRow row = rows[0];
                string str = row.Cells["SQDH"].Value.ToString();
                SqdTianKai kai = new SqdTianKai {
                    Text = "红字发票信息表修改",
                    sqdh = str
                };
                if (!kai.InitSqdMx(InitSqdMxType.Edit, null))
                {
                    MessageManager.ShowMsgBox("INP-242185", new string[] { "差额税信息表不可修改！" });
                }
                else if (SqdTianKai.xiugai_no == 0)
                {
                    kai.Show(FormMain.control_0);
                }
                else
                {
                    MessageManager.ShowMsgBox("INP-431366");
                    SqdTianKai.xiugai_no = 0;
                }
            }
            else
            {
                MessageManager.ShowMsgBox("INP-431331");
            }
        }

        private void ToolStripMenuItemLb_Click(object sender, EventArgs e)
        {
            if (this.aisinoGrid.Rows.Count <= 0)
            {
                MessageManager.ShowMsgBox("INP-431359");
            }
            else
            {
                try
                {
                    this.xmlComponentLoader1.GetControlByName<AisinoDataGrid>("aisinoGrid").Print("信息表列表", this, null, null, true);
                }
                catch (Exception exception)
                {
                    MessageManager.ShowMsgBox(exception.Message);
                }
            }
        }

        private void ToolStripMenuItemSqd_Click(object sender, EventArgs e)
        {
            if (this.aisinoGrid.SelectedRows.Count <= 0)
            {
                MessageManager.ShowMsgBox("INP-431331");
            }
            else
            {
                string[] strArray = new string[this.aisinoGrid.SelectedRows.Count];
                for (int i = 0; i < this.aisinoGrid.SelectedRows.Count; i++)
                {
                    strArray[i] = this.aisinoGrid.SelectedRows[i].Cells["SQDH"].Value.ToString();
                    new HZFPSQDPrint("0" + strArray[i]).Print(true);
                }
            }
        }

        private delegate void PerformStepHandle(int step);
    }
}

