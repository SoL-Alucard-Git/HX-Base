namespace Aisino.Fwkp.Fptk.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Http;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.BusinessObject;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using System.Xml;

    public class WSPZCXForm : BaseForm
    {
        private AisinoBTN btnQuery;
        private DateTime CardClock = DateTime.Now;
        private IContainer components;
        private DateTimePicker data_Jzrq;
        private DateTimePicker data_Qsrq;
        private CustomStyleDataGrid dgSwdk;
        private ILog log = LogUtil.GetLogger<InvoiceForm_ZZS>();
        private ILog loger = LogUtil.GetLogger<WSPZCXForm>();
        private FPLX mfplx;
        private bool mIsRed;
        private List<SwdkFpxx> swdkFpLists;
        public SwdkFpxx swdkFpxxRet;
        private ToolStripButton tool_Args;
        private ToolStripButton tool_exit;
        private ToolStripButton tool_select;
        private ToolStrip toolStrip1;
        private AisinoTXT txtWspzh;
        private AisinoTXT txtXfsh;
        private XmlComponentLoader xmlComponentLoader1;

        public WSPZCXForm(FPLX fplx)
        {
            this.mfplx = fplx;
            this.InitializeComponent();
            this.btnQuery = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnQuery");
            this.txtWspzh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtWspzh");
            this.txtXfsh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txtXfsh");
            this.data_Jzrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_Jzrq");
            this.data_Qsrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_Qsrq");
            this.tool_select = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_select");
            this.tool_exit = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_exit");
            this.tool_Args = this.xmlComponentLoader1.GetControlByName<ToolStripButton>("tool_Args");
            this.dgSwdk = this.xmlComponentLoader1.GetControlByName<CustomStyleDataGrid>("dgWspz");
            this.toolStrip1 = this.xmlComponentLoader1.GetControlByName<ToolStrip>("toolStrip1");
            ControlStyleUtil.SetToolStripStyle(this.toolStrip1);
            this.tool_exit.Click += new EventHandler(this.BtnExit_Click);
            this.tool_select.Click += new EventHandler(this.BtnSelect_Click);
            this.tool_Args.Click += new EventHandler(this.BtnArgs_Click);
            this.btnQuery.Click += new EventHandler(this.BtnSwdkFPFind_Click);
            this.dgSwdk.DoubleClick += new EventHandler(this.BtnSelect_Click);
            this.txtWspzh.KeyPress += new KeyPressEventHandler(this.txtWspzh_KeyPress);
            this.txtXfsh.KeyPress += new KeyPressEventHandler(this.txtXfsh_KeyPress);
            this.txtWspzh.MaxLength = 100;
            this.txtXfsh.MaxLength = 20;
            this.dgSwdk.AllowUserToAddRows = false;
            this.dgSwdk.MultiSelect = false;
            this.dgSwdk.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgSwdk.ReadOnly = true;
            this.SelectTimeBind();
            this.swdkFpLists = new List<SwdkFpxx>();
        }

        private void BtnArgs_Click(object sender, EventArgs e)
        {
            new ArgsSetForm().ShowDialog();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            if (this.dgSwdk.RowCount <= 0)
            {
                MessageManager.ShowMsgBox("SWDK-0008");
            }
            else if (this.dgSwdk.CurrentRow.Index <= -1)
            {
                MessageManager.ShowMsgBox("SWDK-0008");
            }
            else
            {
                this.swdkFpxxRet = this.getSwdkFpxx(this.dgSwdk.CurrentRow.Index);
                if ((this.swdkFpxxRet != null) && this.checkSwdkFpxx(this.swdkFpxxRet))
                {
                    if (!(this.swdkFpxxRet.sfykp == "Y"))
                    {
                        string[] textArray2 = new string[] { this.swdkFpxxRet.fpxx.wspzhm };
                        if (DialogResult.Yes == MessageManager.ShowMsgBox("SWDK-0007", textArray2))
                        {
                            base.DialogResult = DialogResult.OK;
                        }
                    }
                    else
                    {
                        string str = "";
                        if (this.swdkFpxxRet.sfykp == "Y")
                        {
                            int num = 0;
                            foreach (YkfpDmHm hm in this.swdkFpxxRet.ykfpDmHms)
                            {
                                if (num < 10)
                                {
                                    str = str + hm.kpgc_fpdm + ",";
                                    str = str + hm.kpgc_fphm + ";\n";
                                }
                                else
                                {
                                    str = str + "...\n";
                                    break;
                                }
                                num++;
                            }
                        }
                        string[] textArray1 = new string[] { str };
                        if (DialogResult.Yes == MessageManager.ShowMsgBox("SWDK-0006", textArray1))
                        {
                            base.DialogResult = DialogResult.OK;
                        }
                    }
                }
            }
        }

        private void BtnSwdkFPFind_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            string xml = string.Empty;
            this.swdkFpLists.Clear();
            this.swdkFpxxRet = null;
            try
            {
                if ((this.txtWspzh.Text.Trim().Length <= 0) && (this.txtXfsh.Text.Trim().Length <= 0))
                {
                    MessageManager.ShowMsgBox("SWDK-0001");
                }
                else if ((this.txtXfsh.Text.Trim().Length > 0) && (this.txtXfsh.Text.Trim().Length < 6))
                {
                    MessageManager.ShowMsgBox("SWDK-0002");
                }
                else if (string.Compare(this.data_Qsrq.Value.ToString("yyyy-MM-dd"), this.data_Jzrq.Value.ToString("yyyy-MM-dd")) == 1)
                {
                    MessageManager.ShowMsgBox("DKFPXZ-0013");
                }
                else
                {
                    str = this.GenDownloadXML();
                    this.log.Info("输入参数queryInfo" + str);
                    if (str == null)
                    {
                        MessageManager.ShowMsgBox("SWDK-0003");
                    }
                    else
                    {
                        string str3 = PropertyUtil.GetValue("SWDK_SERVER");
                        if (str3.Trim().Length == 0)
                        {
                            MessageManager.ShowMsgBox("SWDK-0019");
                        }
                        else
                        {
                            int num = 0;
                            string str4 = "";
                            byte[] bytes = ToolUtil.GetBytes(str);
                            if ((bytes != null) && (bytes.Length != 0))
                            {
                                str4 = Convert.ToBase64String(bytes);
                            }
                            string s = new WebClient().Post(str3, str4, out num);
                            if ((s != null) && (s.Length > 0))
                            {
                                xml = ToolUtil.GetString(Convert.FromBase64String(s));
                            }
                            this.log.Info("输出参数result" + xml);
                            XmlDocument document = new XmlDocument();
                            if (xml != string.Empty)
                            {
                                document.LoadXml(xml);
                            }
                            if ((xml == null) || (xml == ""))
                            {
                                xml = "受理服务器没有响应。";
                                MessageManager.ShowMsgBox("SWDK-0004");
                            }
                            this.parseSwdkfpXml(document);
                            this.FillGridView();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("SWDK-0005");
                this.loger.Error(exception.Message);
            }
        }

        private bool checkSwdkFpxx(SwdkFpxx swdkFpxx)
        {
            bool flag = true;
            if (swdkFpxx.fpxx.isRed == this.IsRed)
            {
                return flag;
            }
            if (!this.IsRed)
            {
                MessageManager.ShowMsgBox("SWDK-0015");
            }
            else
            {
                MessageManager.ShowMsgBox("SWDK-0016");
            }
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FillGridView()
        {
            this.dgSwdk.Rows.Clear();
            foreach (SwdkFpxx fpxx in this.swdkFpLists)
            {
                if (fpxx != null)
                {
                    string[] values = new string[0x10];
                    string str = "";
                    if (fpxx.sfykp == "Y")
                    {
                        foreach (YkfpDmHm hm in fpxx.ykfpDmHms)
                        {
                            str = str + hm.kpgc_fpdm + ",";
                            str = str + hm.kpgc_fphm + ";";
                        }
                    }
                    values = new string[] { fpxx.fpxx.wspzhm, fpxx.swdkFpzl, (fpxx.sfykp == "Y") ? "是" : "否", fpxx.fpxx.gfmc, fpxx.fpxx.gfsh, fpxx.fpxx.gfdzdh, fpxx.fpxx.gfyhzh, fpxx.fpxx.kprq, fpxx.fpxx.je, (double.Parse(fpxx.fpxx.sLv) * 100.0) + "%", fpxx.fpxx.se, fpxx.fpxx.bz, fpxx.fpxx.kpr, fpxx.fpxx.skr, fpxx.fpxx.fhr, str };
                    int num = this.dgSwdk.Rows.Add(values);
                    if (num >= 0)
                    {
                        this.dgSwdk.Rows[num].ReadOnly = true;
                    }
                }
            }
        }

        private string GenDownloadXML()
        {
            XmlNode node;
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            document.AppendChild(newChild);
            if (this.mfplx == 0)
            {
                node = document.CreateElement("SERVICE");
            }
            else
            {
                node = document.CreateElement("SERVICEPP");
            }
            XmlNode node2 = document.CreateElement("QSRQ");
            XmlNode node3 = document.CreateElement("JZRQ");
            XmlNode node4 = document.CreateElement("XFSH");
            XmlNode node5 = document.CreateElement("WSPZDH");
            node2.InnerText = this.data_Qsrq.Value.ToString("yyyy-MM-dd");
            node3.InnerText = this.data_Jzrq.Value.ToString("yyyy-MM-dd");
            node4.InnerText = this.txtXfsh.Text.Trim();
            node5.InnerText = this.txtWspzh.Text.Trim();
            document.AppendChild(node);
            node.AppendChild(node2);
            node.AppendChild(node3);
            node.AppendChild(node4);
            node.AppendChild(node5);
            return document.InnerXml;
        }

        private SwdkFpxx getSwdkFpxx(int index)
        {
            SwdkFpxx fpxx = new SwdkFpxx();
            foreach (SwdkFpxx fpxx2 in this.swdkFpLists)
            {
                if (fpxx2.index == index)
                {
                    fpxx = fpxx2;
                }
            }
            return fpxx;
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x2f7, 0x1ab);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath=@"..\Config\Components\Aisino.Fwkp.Fpkj.Form.SWDK.WSPZCXForm\Aisino.Fwkp.Fpkj.Form.SWDK.WSPZCXForm.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2f7, 0x1ab);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Name = "WSPZCXForm";
            this.Text = "完税凭证号查询";
            base.ResumeLayout(false);
        }

        private void parseSwdkfpXml(XmlDocument xml)
        {
            XmlNode node = xml.SelectSingleNode("/ROOT");
            if (node.SelectSingleNode("ERR_CODE").InnerText == "100")
            {
                int num14;
                if (this.mfplx == 0)
                {
                    XmlNodeList list = node.SelectNodes("FPXX");
                    if (list != null)
                    {
                        int num = 0;
                        foreach (XmlNode node2 in list)
                        {
                            decimal num3;
                            SwdkFpxx item = new SwdkFpxx {
                                index = num++,
                                fpxx = new Fpxx()
                            };
                            item.fpxx.fphm = node2.SelectSingleNode("FPHM").InnerText.Trim();
                            item.fpxx.fpdm = node2.SelectSingleNode("FPDM").InnerText.Trim();
                            item.fpxx.wspzhm = node2.SelectSingleNode("WSPZHM").InnerText.Trim();
                            item.fpxx.kprq = node2.SelectSingleNode("KPRQ").InnerText.Trim();
                            item.fpxx.je = node2.SelectSingleNode("HJJE").InnerText.Trim();
                            item.fpxx.je = Convert.ToDecimal(decimal.Parse(item.fpxx.je, NumberStyles.Float)).ToString();
                            if (decimal.TryParse(item.fpxx.je, out num3) && (num3 < decimal.Zero))
                            {
                                item.fpxx.isRed = true;
                            }
                            item.fpxx.sLv = node2.SelectSingleNode("SLV").InnerText.Trim();
                            if (item.fpxx.sLv.Length > 0)
                            {
                                item.fpxx.sLv = Convert.ToDecimal(decimal.Parse(item.fpxx.sLv, NumberStyles.Float)).ToString();
                            }
                            item.fpxx.se = node2.SelectSingleNode("HJSE").InnerText.Trim();
                            if (item.fpxx.se.Length > 0)
                            {
                                item.fpxx.se = Convert.ToDecimal(decimal.Parse(item.fpxx.se, NumberStyles.Float)).ToString();
                            }
                            item.fpxx.gfsh = node2.SelectSingleNode("GFSH").InnerText.Trim();
                            item.fpxx.gfmc = node2.SelectSingleNode("GFMC").InnerText.Trim();
                            item.fpxx.gfdzdh = node2.SelectSingleNode("GFDZ_DH").InnerText.Trim();
                            item.fpxx.gfyhzh = node2.SelectSingleNode("GFYHMC_YHZH").InnerText.Trim();
                            item.fpxx.dkqysh = node2.SelectSingleNode("XFSH").InnerText.Trim();
                            item.fpxx.dkqymc = node2.SelectSingleNode("XFMC").InnerText.Trim();
                            item.fpxx.xfyhzh = node2.SelectSingleNode("XFYHMC_YHZH").InnerText.Trim();
                            item.fpxx.bz = node2.SelectSingleNode("BZ").InnerText.Trim();
                            item.fpxx.zgswjgdm = node2.SelectSingleNode("SWJGDM").InnerText.Trim();
                            item.fpxx.zgswjgmc = node2.SelectSingleNode("SWJGMC").InnerText.Trim();
                            item.fpxx.skr = node2.SelectSingleNode("SKR").InnerText.Trim();
                            item.fpxx.fhr = node2.SelectSingleNode("FHR").InnerText.Trim();
                            item.fpxx.kpr = node2.SelectSingleNode("KPR").InnerText.Trim();
                            item.tsnsrsbh = node2.SelectSingleNode("TSNSRSBH").InnerText.Trim();
                            item.sfykp = node2.SelectSingleNode("SFYKP").InnerText.Trim();
                            if (item.sfykp.ToUpper() == "Y")
                            {
                                item.ykfpDmHms = new List<YkfpDmHm>();
                                foreach (XmlNode node3 in node2.SelectSingleNode("YKPXX").SelectNodes("YKP"))
                                {
                                    YkfpDmHm hm = new YkfpDmHm {
                                        kpgc_fpdm = node3.SelectSingleNode("KPCG_FPDM").InnerText.Trim(),
                                        kpgc_fphm = node3.SelectSingleNode("KPCG_FPHM").InnerText.Trim()
                                    };
                                    item.ykfpDmHms.Add(hm);
                                }
                            }
                            XmlNodeList list2 = node2.SelectSingleNode("ZBNRS").SelectNodes("ZBNR");
                            if (list2 != null)
                            {
                                int num6 = 0;
                                string str = "";
                                string str2 = "";
                                string str3 = "";
                                string s = "";
                                string str5 = "";
                                string str6 = "";
                                string str7 = "";
                                string str8 = "";
                                item.fpxx.Mxxx = new List<Dictionary<SPXX, string>>();
                                foreach (XmlNode node1 in list2)
                                {
                                    double num7;
                                    double num8;
                                    str = node1.SelectSingleNode("HWMC").InnerText.Trim();
                                    str2 = node1.SelectSingleNode("GGXH").InnerText.Trim();
                                    str3 = node1.SelectSingleNode("JLDW").InnerText.Trim();
                                    s = node1.SelectSingleNode("SL").InnerText.Trim();
                                    if (s.Length > 0)
                                    {
                                        s = Convert.ToDecimal(decimal.Parse(s, NumberStyles.Float)).ToString();
                                    }
                                    str5 = node1.SelectSingleNode("BHSDJ").InnerText.Trim();
                                    if (str5.Length > 0)
                                    {
                                        str5 = Convert.ToDecimal(decimal.Parse(str5, NumberStyles.Float)).ToString();
                                    }
                                    str6 = node1.SelectSingleNode("BHSJE").InnerText.Trim();
                                    if (str6.Length > 0)
                                    {
                                        str6 = Convert.ToDecimal(decimal.Parse(str6, NumberStyles.Float)).ToString();
                                    }
                                    str7 = node1.SelectSingleNode("SLV").InnerText.Trim();
                                    if (str7.Length > 0)
                                    {
                                        str7 = Convert.ToDecimal(decimal.Parse(str7, NumberStyles.Float)).ToString();
                                    }
                                    str8 = node1.SelectSingleNode("SE").InnerText.Trim();
                                    if (str8.Length > 0)
                                    {
                                        str8 = Convert.ToDecimal(decimal.Parse(str8, NumberStyles.Float)).ToString();
                                    }
                                    Dictionary<SPXX, string> dictionary = new Dictionary<SPXX, string>();
                                    num14 = num6 + 1;
                                    num6 = num14;
                                    dictionary.Add((SPXX)13, num14.ToString());
                                    dictionary.Add((SPXX)0, str);
                                    dictionary.Add((SPXX)7, str6);
                                    dictionary[(SPXX)8] = str7;
                                    dictionary[(SPXX)9] = str8;
                                    dictionary[(SPXX)3] = str2;
                                    dictionary[(SPXX)4] = str3;
                                    dictionary[(SPXX)2] = string.Empty;
                                    dictionary[(SPXX)6] = s;
                                    if (double.TryParse(s, out num7) && (num7 == 0.0))
                                    {
                                        dictionary[(SPXX)6] = string.Empty;
                                    }
                                    dictionary[(SPXX)5] = str5;
                                    if (double.TryParse(str5, out num8) && (num8 == 0.0))
                                    {
                                        dictionary[(SPXX)5] = string.Empty;
                                    }
                                    if (!item.fpxx.isRed)
                                    {
                                        dictionary[(SPXX)10] = str6.Contains("-") ? "4" : "0";
                                    }
                                    else
                                    {
                                        dictionary[(SPXX)10] = "0";
                                    }
                                    dictionary[(SPXX)11] = "0";
                                    dictionary[(SPXX)20] = "";
                                    dictionary[(SPXX)1] = "";
                                    dictionary[(SPXX)0x15] = "0";
                                    dictionary[(SPXX)0x16] = "";
                                    dictionary[(SPXX)0x17] = "";
                                    item.fpxx.Mxxx.Add(dictionary);
                                }
                            }
                            this.swdkFpLists.Add(item);
                        }
                    }
                }
                else
                {
                    XmlNodeList list3 = node.SelectNodes("FPXX");
                    if (list3 != null)
                    {
                        int num15 = 0;
                        foreach (XmlNode node4 in list3)
                        {
                            double num18;
                            SwdkFpxx fpxx2 = new SwdkFpxx {
                                index = num15++,
                                fpxx = new Fpxx()
                            };
                            fpxx2.fpxx.fphm = node4.SelectSingleNode("FPHM").InnerText.Trim();
                            fpxx2.fpxx.fpdm = node4.SelectSingleNode("FPDM").InnerText.Trim();
                            fpxx2.fpxx.wspzhm = node4.SelectSingleNode("WSPZHM").InnerText.Trim();
                            fpxx2.swdkFpzl = node4.SelectSingleNode("FPZL").InnerText.Trim();
                            fpxx2.ZFZHBZ = node4.SelectSingleNode("ZFZHBZ").InnerText.Trim();
                            fpxx2.fpxx.kprq = node4.SelectSingleNode("KPRQ").InnerText.Trim();
                            fpxx2.fpxx.je = node4.SelectSingleNode("HJJE").InnerText.Trim();
                            fpxx2.fpxx.je = Convert.ToDecimal(decimal.Parse(fpxx2.fpxx.je, NumberStyles.Float)).ToString();
                            decimal result = new decimal();
                            if (decimal.TryParse(fpxx2.fpxx.je, out result) && (result < decimal.Zero))
                            {
                                fpxx2.fpxx.isRed = true;
                            }
                            result = new decimal();
                            fpxx2.fpxx.sLv = node4.SelectSingleNode("SLV").InnerText.Trim();
                            if (fpxx2.fpxx.sLv.Length > 0)
                            {
                                fpxx2.fpxx.sLv = Convert.ToDecimal(decimal.Parse(fpxx2.fpxx.sLv, NumberStyles.Float)).ToString();
                            }
                            if (double.TryParse(fpxx2.fpxx.sLv, out num18) && (num18 == 0.015))
                            {
                                fpxx2.fpxx.Zyfplx = (ZYFP_LX)10;
                            }
                            fpxx2.fpxx.se = node4.SelectSingleNode("HJSE").InnerText.Trim();
                            if (fpxx2.fpxx.se.Length > 0)
                            {
                                fpxx2.fpxx.se = Convert.ToDecimal(decimal.Parse(fpxx2.fpxx.se, NumberStyles.Float)).ToString();
                            }
                            fpxx2.fpxx.gfsh = node4.SelectSingleNode("GFSH").InnerText.Trim();
                            fpxx2.fpxx.gfmc = node4.SelectSingleNode("GFMC").InnerText.Trim();
                            fpxx2.fpxx.gfdzdh = node4.SelectSingleNode("GFDZ_DH").InnerText.Trim();
                            fpxx2.fpxx.gfyhzh = node4.SelectSingleNode("GFYHMC_YHZH").InnerText.Trim();
                            fpxx2.fpxx.dkqysh = node4.SelectSingleNode("XFSH").InnerText.Trim();
                            fpxx2.fpxx.dkqymc = node4.SelectSingleNode("XFMC").InnerText.Trim();
                            fpxx2.fpxx.xfyhzh = node4.SelectSingleNode("XFYHMC_YHZH").InnerText.Trim();
                            fpxx2.fpxx.bz = node4.SelectSingleNode("BZ").InnerText.Trim();
                            fpxx2.fpxx.skr = node4.SelectSingleNode("SKR").InnerText.Trim();
                            fpxx2.fpxx.fhr = node4.SelectSingleNode("FHR").InnerText.Trim();
                            fpxx2.sfykp = node4.SelectSingleNode("SFYKP").InnerText.Trim();
                            if (fpxx2.sfykp.ToUpper() == "Y")
                            {
                                fpxx2.ykfpDmHms = new List<YkfpDmHm>();
                                foreach (XmlNode node5 in node4.SelectSingleNode("YKPXX").SelectNodes("YKP"))
                                {
                                    YkfpDmHm hm2 = new YkfpDmHm {
                                        kpgc_fpdm = node5.SelectSingleNode("KPCG_FPDM").InnerText.Trim(),
                                        kpgc_fphm = node5.SelectSingleNode("KPCG_FPHM").InnerText.Trim()
                                    };
                                    fpxx2.ykfpDmHms.Add(hm2);
                                }
                            }
                            XmlNodeList list4 = node4.SelectSingleNode("ZBNRS").SelectNodes("ZBNR");
                            if (list4 != null)
                            {
                                int num21 = 0;
                                string str9 = "";
                                string str10 = "";
                                string str11 = "";
                                string str12 = "";
                                string str13 = "";
                                string str14 = "";
                                string str15 = "";
                                string str16 = "";
                                fpxx2.fpxx.Mxxx = new List<Dictionary<SPXX, string>>();
                                foreach (XmlNode node7 in list4)
                                {
                                    double num22;
                                    double num23;
                                    str9 = node7.SelectSingleNode("HWMC").InnerText.Trim();
                                    str10 = node7.SelectSingleNode("GGXH").InnerText.Trim();
                                    str11 = node7.SelectSingleNode("JLDW").InnerText.Trim();
                                    str12 = node7.SelectSingleNode("SL").InnerText.Trim();
                                    if (str12.Length > 0)
                                    {
                                        str12 = Convert.ToDecimal(decimal.Parse(str12, NumberStyles.Float)).ToString();
                                    }
                                    str13 = node7.SelectSingleNode("HSDJ").InnerText.Trim();
                                    if (str13.Length > 0)
                                    {
                                        str13 = Convert.ToDecimal(decimal.Parse(str13, NumberStyles.Float)).ToString();
                                    }
                                    str14 = node7.SelectSingleNode("HSJE").InnerText.Trim();
                                    if (str14.Length > 0)
                                    {
                                        str14 = Convert.ToDecimal(decimal.Parse(str14, NumberStyles.Float)).ToString();
                                    }
                                    str15 = node7.SelectSingleNode("SLV").InnerText.Trim();
                                    if (str15.Length > 0)
                                    {
                                        str15 = Convert.ToDecimal(decimal.Parse(str15, NumberStyles.Float)).ToString();
                                    }
                                    str16 = node7.SelectSingleNode("SE").InnerText.Trim();
                                    if (str16.Length > 0)
                                    {
                                        str16 = Convert.ToDecimal(decimal.Parse(str16, NumberStyles.Float)).ToString();
                                    }
                                    Dictionary<SPXX, string> dictionary2 = new Dictionary<SPXX, string>();
                                    num14 = num21 + 1;
                                    num21 = num14;
                                    dictionary2.Add((SPXX)13, num14.ToString());
                                    dictionary2.Add((SPXX)0, str9);
                                    string str17 = decimal.Subtract(decimal.Parse(str14), decimal.Parse(str16)).ToString();
                                    result += decimal.Parse(str17);
                                    dictionary2.Add((SPXX)7, str17);
                                    dictionary2[(SPXX)8] = str15;
                                    dictionary2[(SPXX)9] = str16;
                                    dictionary2[(SPXX)3] = str10;
                                    dictionary2[(SPXX)4] = str11;
                                    dictionary2[(SPXX)2] = string.Empty;
                                    dictionary2[(SPXX)6] = str12;
                                    if (double.TryParse(str12, out num22) && (num22 == 0.0))
                                    {
                                        dictionary2[(SPXX)6] = string.Empty;
                                    }
                                    dictionary2[(SPXX)5] = str13;
                                    if (double.TryParse(str13, out num23) && (num23 == 0.0))
                                    {
                                        dictionary2[(SPXX)5] = string.Empty;
                                    }
                                    if (!fpxx2.fpxx.isRed)
                                    {
                                        dictionary2[(SPXX)10] = str17.Contains("-") ? "4" : "0";
                                    }
                                    else
                                    {
                                        dictionary2[(SPXX)10] = "0";
                                    }
                                    dictionary2[(SPXX)11] = "1";
                                    dictionary2[(SPXX)20] = "";
                                    dictionary2[(SPXX)1] = "";
                                    dictionary2[(SPXX)0x15] = "";
                                    dictionary2[(SPXX)0x16] = "";
                                    dictionary2[(SPXX)0x17] = "";
                                    fpxx2.fpxx.Mxxx.Add(dictionary2);
                                }
                                fpxx2.fpxx.je = result.ToString("F2");
                            }
                            this.swdkFpLists.Add(fpxx2);
                        }
                    }
                }
            }
            else
            {
                XmlNode node6 = node.SelectSingleNode("ERR_MSG");
                if (node6.InnerText != null)
                {
                    string[] textArray1 = new string[] { node6.InnerText };
                    MessageManager.ShowMsgBox("SWDK-0009", textArray1);
                }
                else
                {
                    MessageManager.ShowMsgBox("SWDK-0005");
                }
            }
        }

        private void SelectTimeBind()
        {
            try
            {
                this.CardClock = base.TaxCardInstance.GetCardClock();
                int year = this.CardClock.Year;
                int month = this.CardClock.Month;
                int day = this.CardClock.Day;
                this.data_Qsrq.Value = new DateTime(year, month, 1);
                this.data_Jzrq.Value = new DateTime(year, month, day);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
            }
        }

        private void txtWspzh_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtXfsh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsLetterOrDigit(e.KeyChar) && (e.KeyChar != '\r')) && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }

        public bool IsRed
        {
            get
            {
                return this.mIsRed;
            }
            set
            {
                this.mIsRed = value;
            }
        }
    }
}

