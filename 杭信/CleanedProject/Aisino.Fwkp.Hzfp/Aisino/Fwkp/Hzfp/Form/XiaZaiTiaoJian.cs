namespace Aisino.Fwkp.Hzfp.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class XiaZaiTiaoJian : DockForm
    {
        private AisinoDataGrid _AisinoGrid;
        private AisinoBTN but_close;
        private AisinoBTN but_ok;
        private AisinoCHK che_bqyjs;
        private AisinoCHK che_bqysq;
        private AisinoCHK che_jsrq;
        private AisinoCHK che_ksrq;
        private IContainer components;
        private DateTimePicker data_jsrq;
        private DateTimePicker data_ksrq;
        public DateTime DateEnd;
        public DateTime DateStart;
        public string Ghfsh = string.Empty;
        public int Month;
        private AisinoTXT text_ghfsh;
        private AisinoTXT text_xhfsh;
        private AisinoTXT text_xxbbh;
        public string Xhfsh = string.Empty;
        private XmlComponentLoader xmlComponentLoader1;
        public string Xxbbh = string.Empty;
        public string Xxbfw = string.Empty;

        public XiaZaiTiaoJian()
        {
            this.Initialize();
            this.SetDataCtrlAttritute();
        }

        private void but_close_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void but_ok_Click(object sender, EventArgs e)
        {
            this.DateStart = this.data_ksrq.Value;
            TimeSpan span = new TimeSpan(0x18, 0, 0);
            if (!this.che_ksrq.Checked)
            {
                this.DateStart += span;
            }
            this.DateEnd = this.data_jsrq.Value;
            if (this.che_jsrq.Checked)
            {
                this.DateEnd += span;
            }
            this.Xhfsh = this.text_xhfsh.Text;
            this.Ghfsh = this.text_ghfsh.Text;
            this.Xxbbh = this.text_xxbbh.Text;
            if (!this.che_bqysq.Checked && !this.che_bqyjs.Checked)
            {
                MessageManager.ShowMsgBox("INP-431378");
            }
            else
            {
                if (this.che_bqysq.Checked && this.che_bqyjs.Checked)
                {
                    this.Xxbfw = "0";
                }
                else if (this.che_bqysq.Checked && !this.che_bqyjs.Checked)
                {
                    this.Xxbfw = "1";
                }
                else if (!this.che_bqysq.Checked && this.che_bqyjs.Checked)
                {
                    this.Xxbfw = "2";
                }
                if (this.DateStart > this.DateEnd)
                {
                    MessageManager.ShowMsgBox("INP-431379");
                }
                else
                {
                    base.DialogResult = DialogResult.OK;
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
            this.data_jsrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_jsrq");
            this.data_ksrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_ksrq");
            this.che_jsrq = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("che_jsrq");
            this.che_ksrq = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("che_ksrq");
            this.but_ok = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_ok");
            this.but_close = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_close");
            this.text_xhfsh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_xhfsh");
            this.text_ghfsh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_ghfsh");
            this.text_xxbbh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_xxbbh");
            this.che_bqysq = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("che_bqysq");
            this.che_bqyjs = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("che_bqyjs");
            this.text_xhfsh.KeyPress += new KeyPressEventHandler(this.text_xhfsh_KeyPress);
            this.text_xhfsh.MaxLength = 20;
            this.text_ghfsh.KeyPress += new KeyPressEventHandler(this.text_ghfsh_KeyPress);
            this.text_ghfsh.MaxLength = 20;
            this.text_xxbbh.KeyPress += new KeyPressEventHandler(this.text_xxbbh_KeyPress);
            this.text_xxbbh.MaxLength = 0x10;
            this.but_close.Click += new EventHandler(this.but_close_Click);
            this.but_ok.Click += new EventHandler(this.but_ok_Click);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x166, 0x16e);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath=@"..\Config\Components\Aisino.Fwkp.Hzfp.Form.XiaZaiTiaoJian\Aisino.Fwkp.Hzfp.Form.XiaZaiTiaoJian.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x164, 0x15a);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "XiaZaiTiaoJian";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "红字发票信息表审核结果下载条件设置";
            base.ResumeLayout(false);
        }

        public void SetDataCtrlAttritute()
        {
            int month = base.TaxCardInstance.GetCardClock().Month;
            int year = base.TaxCardInstance.GetCardClock().Year;
            if (year < 0x6d9)
            {
                year = DateTime.Now.Year;
            }
            string str = base.TaxCardInstance.CardEffectDate.Substring(4, 2);
            if (str.Substring(0, 1) == "0")
            {
                str = str.Substring(1, 1);
            }
            DateTime.DaysInMonth(year, month);
            this.data_ksrq.Value = new DateTime(year, month, 1);
            this.DateStart = this.data_ksrq.Value;
            int num3 = base.TaxCardInstance.GetCardClock().Year;
            if (num3 < 0x6d9)
            {
                num3 = DateTime.Now.Year;
            }
            int day = DateTime.DaysInMonth(num3, month);
            this.data_jsrq.Value = new DateTime(year, month, day);
            this.DateEnd = this.data_jsrq.Value;
        }

        private void text_ghfsh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
        }

        private void text_xhfsh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
        }

        private void text_xxbbh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
        }

        public AisinoDataGrid AisinoGrid
        {
            set
            {
                this._AisinoGrid = value;
            }
        }

        public DateTime DateTimeEnd
        {
            get
            {
                return this.DateEnd;
            }
        }

        public DateTime DateTimeStart
        {
            get
            {
                return this.DateStart;
            }
        }
    }
}

