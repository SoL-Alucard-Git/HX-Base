namespace Aisino.Fwkp.HzfpHy.Form
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class HyXiaZaiTiaoJian : DockForm
    {
        private AisinoDataGrid _AisinoGrid;
        private AisinoBTN but_close;
        private AisinoBTN but_ok;
        private AisinoCHK che_jsrq;
        private AisinoCHK che_ksrq;
        private IContainer components;
        private DateTimePicker data_jsrq;
        private DateTimePicker data_ksrq;
        public DateTime DateEnd;
        public DateTime DateStart;
        private AisinoTXT text_xxbbh;
        private XmlComponentLoader xmlComponentLoader1;
        public string Xxbbh = string.Empty;

        public HyXiaZaiTiaoJian()
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
            if (this.DateStart >= this.DateEnd)
            {
                MessageManager.ShowMsgBox("INP-431401");
            }
            else
            {
                this.Xxbbh = this.text_xxbbh.Text;
                base.DialogResult = DialogResult.OK;
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
            this.text_xxbbh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("text_xxbbh");
            this.text_xxbbh.MaxLength = 0x10;
            this.text_xxbbh.KeyPress += new KeyPressEventHandler(this.text_xxbbh_KeyPress);
            this.but_close.Click += new EventHandler(this.but_close_Click);
            this.but_ok.Click += new EventHandler(this.but_ok_Click);
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(HyXiaZaiTiaoJian));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1c8, 0xe2);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath=(@"..\Config\Components\Aisino.Fwkp.HzfpHy.Form.HyXiaZaiTiaoJian\Aisino.Fwkp.HzfpHy.Form.HyXiaZaiTiaoJian.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1c8, 0xe2);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "HyXiaZaiTiaoJian";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "红字货物运输业增值税专用发票信息表审核结果下载条件设置";
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

        private void text_xxbbh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
            }
            else if (!char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
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

