namespace Aisino.Fwkp.Fptk.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fptk;
    using Aisino.Fwkp.Fptk.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Xml;

    public class HZFPTK_PP : BaseForm
    {
        internal string blueFpdm = string.Empty;
        internal string blueFphm = string.Empty;
        internal Fpxx blueFpxx;
        private AisinoBTN btnDetail;
        private AisinoBTN but_qx;
        private AisinoBTN but_syb;
        private AisinoBTN but_xyb;
        private IContainer components;
        private FPLX fplx;
        private Color gray = Color.FromArgb(0xa5, 180, 0xbf);
        private AisinoLBL label10;
        private AisinoLBL label9;
        private AisinoLBL lbl_title;
        private string[] mCurDmHm;
        internal ZYFP_LX mZyfplx;
        private Color orange = Color.FromArgb(0xff, 150, 50);
        private AisinoPNL panel1;
        private PictureBox picBox1;
        private PictureBox picBox3;
        internal string redNum = string.Empty;
        private int start;
        private AisinoTAB tabControl1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private AisinoTXT txt_bz;
        private NoPasteText txt_fpdm;
        private NoPasteText txt_fpdm2;
        private NoPasteText txt_fphm;
        private NoPasteText txt_fphm2;
        private XmlComponentLoader xmlComponentLoader1;

        internal HZFPTK_PP(FPLX fplx)
        {
            this.Initialize();
            this.fplx = fplx;
            this.txt_fpdm.MaxLength = 12;
            this.txt_fphm.MaxLength = 8;
            this.txt_fpdm2.MaxLength = 12;
            this.txt_fphm2.MaxLength = 8;
            this.txt_fphm.PasswordChar = '*';
            this.txt_fpdm.PasswordChar = '*';
            this.txt_fphm2.PasswordChar = '*';
            this.txt_fpdm2.PasswordChar = '*';
            this.txt_fpdm.Focus();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            List<string[]> data = new List<string[]>();
            string[] item = new string[] { Invoice.FPLX2Str(this.fplx), this.blueFpdm, this.blueFphm, "0" };
            data.Add(item);
            BaseForm form = null;
            if ((int)this.fplx == 2)
            {
                form = new InvoiceShowForm(false, 0, data);
            }
            else if ((int)this.fplx == 0x33)
            {
                form = new InvoiceShowForm_DZ(false, 0, data);
            }
            else if ((int)this.fplx == 0x29)
            {
                form = new InvoiceShowForm_JS(false, 0, data);
            }
            else if ((int)this.fplx == 12)
            {
                if (this.blueFpxx.isNewJdcfp)
                {
                    form = new JDCInvoiceForm_new(false, 0, data);
                }
                else
                {
                    form = new JDCInvoiceForm_old(false, 0, data);
                }
            }
            if (form != null)
            {
                form.ShowDialog();
            }
        }

        private void but_qx_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void but_syb_Click(object sender, EventArgs e)
        {
            this.Request(this.start - 1);
        }

        private void but_xyb_Click(object sender, EventArgs e)
        {
            this.Request(this.start + 1);
        }

        private string CanEmptyRedFp()
        {
            string str = "";
            if ((((int)this.fplx == 2) || ((int)this.fplx == 0x33)) && !base.TaxCardInstance.QYLX.ISTDQY)
            {
                string xml = "";
                int num = int.Parse(this.blueFphm);
                FpManager manager = new FpManager();
                string xfsh = manager.GetXfsh();
                string str4 = new StringBuilder().Append("<?xml version=\"1.0\" encoding=\"GBK\"?>").Append("<FPXT><INPUT>").Append("<NSRSBH>").Append(xfsh).Append("</NSRSBH>").Append("<KPJH>").Append(base.TaxCardInstance.Machine).Append("</KPJH>").Append("<SBBH>").Append(manager.GetMachineNum()).Append("</SBBH>").Append("<LZFPDM>").Append(this.blueFpdm).Append("</LZFPDM>").Append("<LZFPHM>").Append(string.Format("{0:00000000}", num)).Append("</LZFPHM>").Append("<FPZL>").Append(((int)this.fplx == 2) ? "c" : "p").Append("</FPZL>").Append("</INPUT></FPXT>").ToString();
                if (HttpsSender.SendMsg("0007", str4, out xml) == 0)
                {
                    XmlDocument document1 = new XmlDocument();
                    document1.LoadXml(xml);
                    XmlNode node1 = document1.SelectSingleNode("/FPXT/OUTPUT");
                    string innerText = node1.SelectSingleNode("HZJE").InnerText;
                    if (node1.SelectSingleNode("CODE").InnerText.Trim().Equals("0000"))
                    {
                        if (!innerText.Trim().Equals("-0"))
                        {
                            decimal num2;
                            decimal.TryParse(innerText, out num2);
                            if (num2 <= decimal.Zero)
                            {
                                str = "数据库中未找到对应的蓝票，且受理平台无可开红字发票金额，不能开红字发票";
                            }
                        }
                        return str;
                    }
                    return "数据库中未找到对应的蓝票，且与受理平台网络不通，不能开红字发票";
                }
                return "数据库中未找到对应的蓝票，且与受理平台网络不通，不能开红字发票";
            }
            if ((int)this.fplx == 12)
            {
                bool iSTDQY = base.TaxCardInstance.QYLX.ISTDQY;
            }
            return str;
        }

        private string CanRedFp()
        {
            string str = "";
            if (decimal.Parse(this.blueFpxx.je).CompareTo(decimal.Parse("0.00")) < 0)
            {
                return "发票为红字发票";
            }
            if (this.blueFpxx.zfbz)
            {
                return "发票已作废";
            }
            if ((this.blueFpxx.fplx == (FPLX)2) || (this.blueFpxx.fplx == (FPLX)0x33))
            {
                if ((((this.mZyfplx != 0) || (this.blueFpxx.Zyfplx != (ZYFP_LX)9)) && ((this.mZyfplx != (ZYFP_LX)8) || (this.blueFpxx.Zyfplx == (ZYFP_LX)8))) && ((this.mZyfplx != (ZYFP_LX)9) || (this.blueFpxx.Zyfplx == (ZYFP_LX)9)))
                {
                    return str;
                }
                return "发票类型不一致";
            }
            if (((int)this.fplx == 12) && (new FpManager().GetTotalRedJe(this.blueFpxx.fpdm, this.blueFpxx.fphm) != decimal.Zero))
            {
                str = "当前蓝字发票已开具红字发票！";
            }
            return str;
        }

        private bool CheckFpdm()
        {
            string str = this.txt_fpdm.Text.Trim();
            if (str.Length != 0)
            {
                char[] trimChars = new char[] { '0' };
                if (!(str.Trim(trimChars) == ""))
                {
                    if ((((int)this.fplx == 2) && (str.Length != 12)) && (str.Length != 10))
                    {
                        return false;
                    }
                    if (((int)this.fplx == 0x33) && (str.Length != 12))
                    {
                        return false;
                    }
                    if ((((int)this.fplx == 0x29) && (str.Length != 12)) && (str.Length != 10))
                    {
                        return false;
                    }
                    if (((int)this.fplx == 12) && (str.Length != 12))
                    {
                        return false;
                    }
                    return true;
                }
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

        private void HZ_From2_Load(object sender, EventArgs e)
        {
            this.Request(0);
            IFpManager manager = new FpManager();
            this.mCurDmHm = manager.GetCurrent(this.fplx);
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.panel1 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel1");
            this.lbl_title = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label7");
            this.txt_fphm = this.xmlComponentLoader1.GetControlByName<NoPasteText>("txt_fphm");
            this.txt_fpdm = this.xmlComponentLoader1.GetControlByName<NoPasteText>("txt_fpdm");
            this.txt_fphm2 = this.xmlComponentLoader1.GetControlByName<NoPasteText>("text_fphm2");
            this.txt_fpdm2 = this.xmlComponentLoader1.GetControlByName<NoPasteText>("text_fpdm2");
            this.txt_bz = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_bz");
            this.but_qx = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_qx");
            this.but_xyb = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_xyb");
            this.but_syb = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_syb");
            this.label9 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label9");
            this.label10 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label10");
            this.tabControl1 = this.xmlComponentLoader1.GetControlByName<AisinoTAB>("tabControl1");
            this.tabPage2 = this.xmlComponentLoader1.GetControlByName<TabPage>("tabPage2");
            this.tabPage3 = this.xmlComponentLoader1.GetControlByName<TabPage>("tabPage3");
            this.picBox1 = this.xmlComponentLoader1.GetControlByName<PictureBox>("picBox1");
            this.picBox3 = this.xmlComponentLoader1.GetControlByName<PictureBox>("picBox3");
            this.btnDetail = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnDetail");
            this.txt_bz.ScrollBars = ScrollBars.Vertical;
            this.but_qx.Click += new EventHandler(this.but_qx_Click);
            this.but_xyb.Click += new EventHandler(this.but_xyb_Click);
            this.but_syb.Click += new EventHandler(this.but_syb_Click);
            this.btnDetail.Click += new EventHandler(this.btnDetail_Click);
            this.txt_fpdm.KeyDown += new KeyEventHandler(this.txt_KeyDown);
            this.txt_fphm.KeyDown += new KeyEventHandler(this.txt_KeyDown);
            this.txt_fpdm2.KeyDown += new KeyEventHandler(this.txt_KeyDown);
            this.txt_fphm2.KeyDown += new KeyEventHandler(this.txt_KeyDown);
            this.txt_fpdm.KeyUp += new KeyEventHandler(this.txt_KeyUp);
            this.txt_fphm.KeyUp += new KeyEventHandler(this.txt_KeyUp);
            this.txt_fpdm2.KeyUp += new KeyEventHandler(this.txt_KeyUp);
            this.txt_fphm2.KeyUp += new KeyEventHandler(this.txt_KeyUp);
            this.picBox1.BackgroundImage = Resources.ZH1;
            this.picBox1.BackgroundImageLayout = ImageLayout.Stretch;
            this.picBox3.BackgroundImage = Resources.ZH3;
            this.picBox3.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(HZFPTK_PP));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x252, 0x174);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.XMLPath=@"..\Config\Components\Aisino.Fwkp.Fpkj.Form.HZFPTK_PP_new\Aisino.Fwkp.Fpkj.Form.HZFPTK_PP_new.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.ClientSize = new Size(0x252, 0x174);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "HZFPTK_PP";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "销项正数发票代码号码填写、确认";
            base.Load += new EventHandler(this.HZ_From2_Load);
            base.ResumeLayout(false);
        }

        public void qd()
        {
            base.DialogResult = DialogResult.OK;
        }

        private void Request(int target)
        {
            switch (target)
            {
                case 0:
                    this.zt1();
                    this.txt_KeyUp(null, null);
                    return;

                case 1:
                    this.zt2();
                    return;

                case 2:
                    this.qd();
                    return;
            }
        }

        private void SetTipStyle(int progress)
        {
            if (progress == 0)
            {
                this.picBox1.BackgroundImage = Resources.ZH_1;
                this.picBox3.BackgroundImage = Resources.ZH3;
                this.label9.BackColor = this.orange;
                this.label10.BackColor = this.gray;
            }
            else if (progress == 1)
            {
                this.picBox3.BackgroundImage = Resources.ZH_3;
                this.label9.BackColor = this.orange;
                this.label10.BackColor = this.orange;
            }
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (((e.KeyValue == 8) || ((e.KeyValue >= 0x25) && (e.KeyValue <= 40))) || ((e.KeyValue == 0x2e) || (e.KeyValue == 0x11)))
            {
                e.SuppressKeyPress = false;
            }
            else
            {
                if (((e.KeyValue < 0x30) || ((e.KeyValue > 0x39) && (e.KeyValue < 0x60))) || (e.KeyValue > 0x69))
                {
                    e.SuppressKeyPress = true;
                }
                if (e.Shift)
                {
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void txt_KeyUp(object sender, KeyEventArgs e)
        {
            int num2;
            string input = this.txt_fphm.Text.Trim();
            string str2 = this.txt_fpdm.Text.Trim();
            if ((((int)this.fplx == 2) || ((int)this.fplx == 12)) || (((int)this.fplx == 0x33) || ((int)this.fplx == 0x29)))
            {
                if (!(input == ""))
                {
                    char[] trimChars = new char[] { '0' };
                    if (!(input.Trim(trimChars) == ""))
                    {
                        if (!this.CheckFpdm())
                        {
                            this.but_xyb.Enabled = false;
                            return;
                        }
                        goto Label_0095;
                    }
                }
                this.but_xyb.Enabled = false;
                return;
            }
        Label_0095:
            if ((str2 != "") && (input != ""))
            {
                int num;
                Regex regex = new Regex(@"^\d{1,}$");
                if ((!new Regex(@"^\d{10,12}$").IsMatch(str2) || !regex.IsMatch(input)) || !int.TryParse(input, out num))
                {
                    this.but_xyb.Enabled = false;
                    return;
                }
                this.but_xyb.Enabled = true;
            }
            if (((((int)this.fplx == 2) || ((int)this.fplx == 12)) || (((int)this.fplx == 0x33) || ((int)this.fplx == 0x29))) && ((((this.mCurDmHm != null) && (this.mCurDmHm.Length == 2)) && (int.TryParse(input, out num2) && (this.mCurDmHm[0] == str2))) && (int.Parse(this.mCurDmHm[1]) == num2)))
            {
                this.but_xyb.Enabled = false;
            }
            else if (this.txt_fpdm2.Text.Equals(this.txt_fpdm.Text) && this.txt_fphm2.Text.Equals(this.txt_fphm.Text))
            {
                this.but_xyb.Enabled = true;
            }
            else
            {
                this.but_xyb.Enabled = false;
            }
        }

        public void zt1()
        {
            this.tabControl1.SelectedIndex = 0;
            this.SetTipStyle(0);
            this.but_syb.Visible = false;
            this.but_xyb.Visible = true;
            this.but_xyb.Enabled = false;
            this.but_xyb.Text = "下一步";
            this.start = 0;
        }

        public void zt2()
        {
            this.txt_bz.ReadOnly = true;
            this.btnDetail.Enabled = false;
            this.blueFphm = this.txt_fphm.Text;
            this.blueFpdm = this.txt_fpdm.Text;
            int result = 0;
            if (int.TryParse(this.blueFphm, out result))
            {
                IFpManager manager = new FpManager();
                this.blueFpxx = manager.GetXxfp(this.fplx, this.blueFpdm, result);
            }
            if (this.blueFpxx != null)
            {
                string str = this.CanRedFp();
                if (str != "")
                {
                    this.lbl_title.Text = "本张发票不能开红字发票！";
                    this.txt_bz.Text = string.Format("(原因：{0})", str);
                    this.but_xyb.Enabled = false;
                }
                else
                {
                    this.lbl_title.Text = "本张发票可以开红字发票！";
                    this.txt_bz.Text = HzfpHelper.GetInvMainInfo(this.fplx, this.blueFpxx);
                    this.but_xyb.Enabled = true;
                }
                this.btnDetail.Enabled = true;
            }
            else
            {
                string str2 = this.CanEmptyRedFp();
                if (str2 != "")
                {
                    this.lbl_title.Text = "本张发票不能开红字发票！";
                    this.txt_bz.Text = string.Format("(原因：{0})", str2);
                    this.but_xyb.Enabled = false;
                }
                else
                {
                    this.lbl_title.Text = "本张发票可以开红字发票！";
                    this.txt_bz.Text = "在当前发票库没有找到相应信息。";
                    this.but_xyb.Enabled = true;
                }
            }
            this.tabControl1.SelectedIndex = 1;
            this.SetTipStyle(1);
            this.but_syb.Visible = true;
            this.but_xyb.Visible = true;
            this.but_syb.Text = "上一步";
            this.but_xyb.Text = "确  定";
            this.but_xyb.Focus();
            this.start = 1;
        }
    }
}

