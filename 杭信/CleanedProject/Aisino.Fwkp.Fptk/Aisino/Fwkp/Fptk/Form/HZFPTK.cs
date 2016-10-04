namespace Aisino.Fwkp.Fptk.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fptk;
    using Aisino.Fwkp.Fptk.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class HZFPTK : BaseForm
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
        private bool istlhy;
        private AisinoLBL label10;
        private AisinoLBL label8;
        private AisinoLBL label9;
        private AisinoLBL lbl_title;
        private AisinoLBL lblXxbTip;
        internal bool mIsSny;
        private Color orange = Color.FromArgb(0xff, 150, 50);
        private PictureBox picBox1;
        private PictureBox picBox2;
        private PictureBox picBox3;
        internal string redNum = string.Empty;
        private int start;
        private AisinoTAB tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private AisinoTXT txt_bz;
        private NoPasteText txt_fpdm;
        private NoPasteText txt_fphm;
        private TextBoxRegex txt_tzdh;
        private TextBoxRegex txt_tzdh_1;
        private XmlComponentLoader xmlComponentLoader1;

        internal HZFPTK(FPLX fplx)
        {
            this.Initialize();
            this.txt_tzdh.PasswordChar = '*';
            this.txt_tzdh_1.PasswordChar = '*';
            this.txt_tzdh.MaxLength = 0x10;
            this.txt_tzdh_1.MaxLength = 0x10;
            this.txt_fpdm.MaxLength = 10;
            this.txt_fphm.MaxLength = 8;
            this.fplx = fplx;
            this.istlhy = base.TaxCardInstance.QYLX.ISTLQY && ((int)fplx == 11);
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            List<string[]> data = new List<string[]>();
            string[] item = new string[] { Invoice.FPLX2Str(this.fplx), this.blueFpdm, this.blueFphm, "0" };
            data.Add(item);
            BaseForm form = null;
            if ((int)this.fplx == 0)
            {
                form = new InvoiceShowForm(false, 0, data);
            }
            else if ((int)this.fplx == 11)
            {
                form = new HYInvoiceForm(false, 0, data);
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

        private string CheckBlueFp()
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
            if (((this.blueFpxx.fplx == 0) && this.mIsSny) && (this.blueFpxx.Zyfplx == 0))
            {
                str = "发票类型不一致";
            }
            return str;
        }

        private bool CheckNote(string noteNum)
        {
            IFpManager manager = new FpManager();
            if (manager.CheckRedNum(noteNum, this.fplx))
            {
                if (noteNum.Substring(0, 6) == "000000")
                {
                    string[] textArray1 = new string[] { noteNum };
                    MessageManager.ShowMsgBox("INP-242130", textArray1);
                    return false;
                }
                return true;
            }
            string[] textArray2 = new string[] { noteNum };
            MessageManager.ShowMsgBox(manager.Code(), "错误", textArray2);
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
            if ((int)this.fplx == 11)
            {
                this.lblXxbTip.Text = this.lblXxbTip.Text.Replace("《开具红字增值税专用发票信息表》", "《开具红字货物运输业增值税专用发票信息表》");
            }
            if (this.istlhy)
            {
                this.picBox1.Visible = false;
                this.label8.Visible = false;
                this.label9.Text = this.label9.Text.Replace("2.", "1.");
                this.label10.Text = this.label10.Text.Replace("3.", "2.");
                this.label9.Location = new Point(this.label9.Location.X - 90, this.label9.Location.Y);
                this.label10.Location = new Point(this.label10.Location.X - 90, this.label10.Location.Y);
                this.picBox2.Location = new Point(this.picBox2.Location.X - 90, this.picBox2.Location.Y);
                this.picBox3.Location = new Point(this.picBox3.Location.X - 90, this.picBox3.Location.Y);
                this.Request(1);
            }
            else
            {
                this.Request(0);
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.txt_fphm = this.xmlComponentLoader1.GetControlByName<NoPasteText>("txt_fphm");
            this.txt_fpdm = this.xmlComponentLoader1.GetControlByName<NoPasteText>("txt_fpdm");
            this.but_qx = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_qx");
            this.but_xyb = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_xyb");
            this.but_syb = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_syb");
            this.txt_tzdh_1 = this.xmlComponentLoader1.GetControlByName<TextBoxRegex>("txt_tzdh_1");
            this.txt_tzdh = this.xmlComponentLoader1.GetControlByName<TextBoxRegex>("txt_tzdh");
            this.lbl_title = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label7");
            this.lblXxbTip = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label6");
            this.txt_bz = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_bz");
            this.tabControl1 = this.xmlComponentLoader1.GetControlByName<AisinoTAB>("tabControl1");
            this.tabPage1 = this.xmlComponentLoader1.GetControlByName<TabPage>("tabPage1");
            this.tabPage2 = this.xmlComponentLoader1.GetControlByName<TabPage>("tabPage2");
            this.tabPage3 = this.xmlComponentLoader1.GetControlByName<TabPage>("tabPage3");
            this.picBox1 = this.xmlComponentLoader1.GetControlByName<PictureBox>("picBox1");
            this.picBox2 = this.xmlComponentLoader1.GetControlByName<PictureBox>("picBox2");
            this.picBox3 = this.xmlComponentLoader1.GetControlByName<PictureBox>("picBox3");
            this.label8 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label8");
            this.label9 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label9");
            this.label10 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label10");
            this.btnDetail = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnDetail");
            this.txt_bz.ScrollBars = ScrollBars.Vertical;
            this.but_qx.Click += new EventHandler(this.but_qx_Click);
            this.but_xyb.Click += new EventHandler(this.but_xyb_Click);
            this.but_syb.Click += new EventHandler(this.but_syb_Click);
            this.btnDetail.Click += new EventHandler(this.btnDetail_Click);
            this.txt_tzdh.KeyPress += new KeyPressEventHandler(this.txt_tzdh_KeyPress);
            this.txt_tzdh_1.KeyPress += new KeyPressEventHandler(this.txt_tzdh_KeyPress);
            this.txt_tzdh.TextChanged += new EventHandler(this.txt_tzdh_TextChanged);
            this.txt_fpdm.KeyPress += new KeyPressEventHandler(this.txt_tzdh_KeyPress);
            this.txt_fphm.KeyPress += new KeyPressEventHandler(this.txt_tzdh_KeyPress);
            this.txt_fpdm.KeyDown += new KeyEventHandler(this.txt_KeyDown);
            this.txt_fphm.KeyDown += new KeyEventHandler(this.txt_KeyDown);
            this.txt_fpdm.ImeMode = ImeMode.Disable;
            this.txt_fphm.ImeMode = ImeMode.Disable;
            this.picBox1.BackgroundImage = Resources.ZH1;
            this.picBox1.BackgroundImageLayout = ImageLayout.Stretch;
            this.picBox2.BackgroundImage = Resources.ZH2;
            this.picBox2.BackgroundImageLayout = ImageLayout.Stretch;
            this.picBox3.BackgroundImage = Resources.ZH3;
            this.picBox3.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(HZFPTK));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.RightToLeft = RightToLeft.No;
            this.xmlComponentLoader1.Size = new Size(0x23e, 0x174);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.XMLPath=@"..\Config\Components\Aisino.Fwkp.Fpkj.Form.HZFPTK_new\Aisino.Fwkp.Fpkj.Form.HZFPTK_new.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x23e, 0x174);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "HZFPTK";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "对应信息表编号填写、确认";
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
                    return;

                case 1:
                    if (!this.istlhy)
                    {
                        this.zt2();
                        return;
                    }
                    this.ztHY1();
                    return;

                case 2:
                    this.zt3();
                    return;

                case 3:
                    this.qd();
                    return;
            }
        }

        private void SetTipStyle(int progress)
        {
            if (progress == 0)
            {
                this.picBox1.BackgroundImage = Resources.ZH_1;
                this.picBox2.BackgroundImage = Resources.ZH2;
                this.label8.BackColor = this.orange;
                this.label9.BackColor = this.gray;
                this.label10.BackColor = this.gray;
            }
            else if (progress == 1)
            {
                if (this.istlhy)
                {
                    this.picBox2.BackgroundImage = Resources.ZH_1;
                }
                else
                {
                    this.picBox2.BackgroundImage = Resources.ZH_2;
                }
                this.picBox3.BackgroundImage = Resources.ZH3;
                this.label8.BackColor = this.orange;
                this.label9.BackColor = this.orange;
                this.label10.BackColor = this.gray;
            }
            else if (progress == 2)
            {
                this.picBox3.BackgroundImage = Resources.ZH_3;
                this.label8.BackColor = this.orange;
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

        private void txt_tzdh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar == '\b') || ((e.KeyChar >= '%') && (e.KeyChar <= '('))) || ((e.KeyChar == '.') || (e.KeyChar == '\x0011')))
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_tzdh_TextChanged(object sender, EventArgs e)
        {
            if (((TextBoxRegex) sender).Text.Length > 0)
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
            this.txt_tzdh.Select();
            this.but_syb.Visible = false;
            this.but_xyb.Visible = true;
            if (this.start == 0)
            {
                this.but_xyb.Enabled = false;
            }
            this.start = 0;
        }

        public void zt2()
        {
            string noteNum = this.txt_tzdh.Text.Trim();
            if (this.start == 0)
            {
                string str2 = this.txt_tzdh_1.Text.Trim();
                if (!noteNum.Equals(str2) || noteNum.StartsWith("1234569911"))
                {
                    MessageManager.ShowMsgBox("INP-242105");
                    this.start = 0;
                    return;
                }
            }
            new FpManager();
            if ((this.start == 2) || this.CheckNote(noteNum))
            {
                this.start = 1;
                this.redNum = noteNum;
                this.Text = "销项正数发票代码号码填写、确认";
                this.tabControl1.SelectedIndex = 1;
                this.SetTipStyle(1);
                this.but_syb.Visible = true;
                this.but_xyb.Visible = true;
                this.but_xyb.Enabled = true;
                this.but_xyb.Text = "下一步";
            }
            else
            {
                this.start = 0;
            }
        }

        public void zt3()
        {
            this.btnDetail.Enabled = false;
            this.blueFpdm = this.txt_fpdm.Text.Trim();
            this.blueFphm = this.txt_fphm.Text.Trim();
            if ((this.blueFpdm.Length == 0) || (this.blueFphm.Length != 0))
            {
                if (!(this.blueFphm != ""))
                {
                    goto Label_0099;
                }
                char[] trimChars = new char[] { '0' };
                if (!(this.blueFphm.Trim(trimChars) == ""))
                {
                    goto Label_0099;
                }
            }
            MessageManager.ShowMsgBox("INP-242149");
            this.start = 1;
            return;
        Label_0099:
            if (this.blueFpdm != "")
            {
                char[] chArray2 = new char[] { '0' };
                if (this.blueFpdm.Trim(chArray2) == "")
                {
                    MessageManager.ShowMsgBox("INP-242150");
                    this.start = 1;
                    return;
                }
            }
            if (((this.blueFpdm.Length == 0) && (this.blueFphm.Length != 0)) || ((this.blueFpdm.Length > 0) && (this.blueFpdm.Length != 10)))
            {
                MessageManager.ShowMsgBox("INP-242107");
                this.start = 1;
            }
            else
            {
                int result = 0;
                if ((this.blueFpdm != "") && (this.blueFphm != ""))
                {
                    Regex regex = new Regex(@"^\d{1,}$");
                    if ((!new Regex(@"^\d{10,12}$").IsMatch(this.blueFpdm) || !regex.IsMatch(this.blueFphm)) || !int.TryParse(this.blueFphm, out result))
                    {
                        MessageManager.ShowMsgBox("INP-242190");
                        this.start = 1;
                        return;
                    }
                }
                IFpManager manager = new FpManager();
                string[] current = manager.GetCurrent(this.fplx);
                if (((current != null) && (current.Length == 2)) && ((current[0] == this.blueFpdm) && (int.Parse(current[1]) == result)))
                {
                    MessageManager.ShowMsgBox("A074");
                    this.start = 1;
                }
                else
                {
                    this.blueFpxx = manager.GetXxfp(this.fplx, this.blueFpdm, result);
                    if (this.blueFpxx != null)
                    {
                        string str = this.CheckBlueFp();
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
                        this.lbl_title.Text = "本张发票可以开红字发票！";
                        this.txt_bz.Text = "在当前发票库没有找到相应信息。";
                        this.but_xyb.Enabled = true;
                    }
                    this.tabControl1.SelectedIndex = 2;
                    this.SetTipStyle(2);
                    this.but_xyb.Focus();
                    this.but_syb.Visible = true;
                    this.but_xyb.Visible = true;
                    this.but_syb.Text = "上一步";
                    this.but_xyb.Text = "确  定";
                    this.start = 2;
                }
            }
        }

        private void ztHY1()
        {
            this.Text = "销项正数发票代码号码填写、确认";
            this.but_syb.Visible = false;
            this.but_xyb.Visible = true;
            this.but_xyb.Enabled = true;
            this.but_xyb.Text = "下一步";
            this.tabControl1.SelectedIndex = 1;
            this.SetTipStyle(1);
            this.start = 1;
        }
    }
}

