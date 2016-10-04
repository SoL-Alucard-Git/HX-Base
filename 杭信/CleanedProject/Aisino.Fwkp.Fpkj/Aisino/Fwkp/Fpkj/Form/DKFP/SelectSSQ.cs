namespace Aisino.Fwkp.Fpkj.Form.DKFP
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SelectSSQ : BaseForm
    {
        private AisinoDataGrid _AisinoGrid;
        private AisinoBTN btn_cancle;
        private AisinoBTN btn_select;
        private DateTime CardClock = DateTime.Now;
        private IContainer components;
        private DateTimePicker data_ssq_e;
        private DateTimePicker data_ssq_s;
        private DateTimePicker data_tbrq;
        private ILog loger = LogUtil.GetLogger<DiKouFaPiaoXiaZai>();
        public string ssqQ = string.Empty;
        public string ssqZ = string.Empty;
        public string tbrq = string.Empty;
        private XmlComponentLoader xmlComponentLoader1;

        public SelectSSQ()
        {
            try
            {
                this.Initialize();
                this.data_tbrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_tbrq");
                this.data_ssq_s = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_ssq_s");
                this.data_ssq_e = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_ssq_e");
                this.btn_cancle = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_cancle");
                this.btn_select = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_select");
                this.data_ssq_s.Format = DateTimePickerFormat.Custom;
                this.data_ssq_s.CustomFormat = "yyyy年M月";
                this.data_ssq_s.ShowUpDown = true;
                this.data_ssq_e.Format = DateTimePickerFormat.Custom;
                this.data_ssq_e.CustomFormat = "yyyy年M月";
                this.data_ssq_e.ShowUpDown = true;
                this.CardClock = base.TaxCardInstance.GetCardClock();
                int year = this.CardClock.Year;
                int month = this.CardClock.Month;
                this.data_tbrq.Value = new DateTime(year, month, 1);
                this.btn_select.Click += new EventHandler(this.BtnSelect_Click);
                this.btn_cancle.Click += new EventHandler(this.BtnCancle_Click);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void BtnCancle_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            this.tbrq = this.data_tbrq.Value.ToString("yyyyMMdd");
            this.ssqQ = this.data_ssq_s.Value.ToString("yyyyMM") + "01";
            this.ssqZ = this.data_ssq_e.Value.ToString("yyyyMM");
            int year = this.data_ssq_e.Value.Year;
            int month = this.data_ssq_e.Value.Month;
            int num3 = DateTime.DaysInMonth(year, month);
            this.ssqZ = this.ssqZ + num3;
            if (string.Compare(this.ssqZ, this.ssqQ) != 1)
            {
                MessageManager.ShowMsgBox("DKFPXZ-0013", "提示");
            }
            else
            {
                base.DialogResult = DialogResult.OK;
                this.Refresh();
            }
        }

        public string ConvertDateFormat(string str)
        {
            string str2 = "";
            if (str.Length == 8)
            {
                str2 = str.Substring(0, 4) + "-" + str.Substring(4, 2) + "-" + str.Substring(6, 2);
            }
            return str2;
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
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(DiKouFaPiaoXiaZai));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x15d, 0x165);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "选择所属期";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Fpkj.Form.DKFP.SelectSSQ\Aisino.Fwkp.Fpkj.Form.DKFP.SelectSSQ.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x15d, 0x165);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "SelectSSQ";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "选择所属期";
            base.ResumeLayout(false);
        }

        public AisinoDataGrid AisinoGrid
        {
            set
            {
                this._AisinoGrid = value;
            }
        }
    }
}

