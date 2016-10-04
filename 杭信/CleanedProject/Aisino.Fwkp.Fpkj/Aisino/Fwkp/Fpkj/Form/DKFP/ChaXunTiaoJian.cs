namespace Aisino.Fwkp.Fpkj.Form.DKFP
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ChaXunTiaoJian : BaseForm
    {
        private AisinoDataGrid _AisinoGrid;
        private AisinoBTN btn_cancle;
        private AisinoBTN btn_select;
        private DateTime CardClock = DateTime.Now;
        private AisinoCMB cmb_fpzl;
        private IContainer components;
        private DateTimePicker data_kprq_e;
        private DateTimePicker data_kprq_s;
        private DateTimePicker data_rzrq_e;
        private DateTimePicker data_rzrq_s;
        public string fpdm = string.Empty;
        public int fphm;
        public string fpzl = string.Empty;
        public string kprq_e = string.Empty;
        public string kprq_e_ck = string.Empty;
        public string kprq_s = string.Empty;
        public string kprq_s_ck = string.Empty;
        private ILog loger = LogUtil.GetLogger<DiKouFaPiaoXiaZai>();
        public string rzrq_e = string.Empty;
        public string rzrq_e_ck = string.Empty;
        public string rzrq_s = string.Empty;
        public string rzrq_s_ck = string.Empty;
        private AisinoTXT txt_fpdm;
        private AisinoTXT txt_fphm;
        public string xfsh = string.Empty;
        private XmlComponentLoader xmlComponentLoader1;

        public ChaXunTiaoJian()
        {
            try
            {
                this.Initialize();
                this.cmb_fpzl = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("cmb_fpzl");
                this.txt_fpdm = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_fpdm");
                this.txt_fphm = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_fphm");
                this.btn_cancle = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_cancle");
                this.btn_select = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btn_select");
                this.data_kprq_s = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_kprq_s");
                this.data_kprq_e = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_kprq_e");
                this.data_rzrq_s = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_rzrq_s");
                this.data_rzrq_e = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_rzrq_e");
                this.data_kprq_s.ShowCheckBox = true;
                this.data_kprq_e.ShowCheckBox = true;
                this.data_rzrq_s.ShowCheckBox = true;
                this.data_rzrq_e.ShowCheckBox = true;
                this.btn_select.Click += new EventHandler(this.BtnSelect_Click);
                this.btn_cancle.Click += new EventHandler(this.BtnCancle_Click);
                this.cmb_fpzl.Items.Add("全部发票");
                this.cmb_fpzl.Items.Add("增值税专用发票");
                this.cmb_fpzl.Items.Add("货物运输业增值税专用发票");
                this.cmb_fpzl.Items.Add("机动车销售统一发票");
                this.cmb_fpzl.DropDownStyle = ComboBoxStyle.DropDownList;
                this.cmb_fpzl.BackColor = Color.White;
                this.cmb_fpzl.SelectedIndex = 0;
                this.SelectTimeBind();
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
            try
            {
                this.fpdm = this.txt_fpdm.Text.Trim();
                if (this.txt_fphm.Text.Length > 0)
                {
                    this.fphm = int.Parse(this.txt_fphm.Text);
                }
                else
                {
                    this.fphm = 0;
                }
                this.kprq_s = this.data_kprq_s.Value.ToString("yyyy-MM-dd");
                this.kprq_e = this.data_kprq_e.Value.ToString("yyyy-MM-dd");
                this.rzrq_s = this.data_rzrq_s.Value.ToString("yyyy-MM-dd");
                this.rzrq_e = this.data_rzrq_e.Value.ToString("yyyy-MM-dd");
                this.kprq_s_ck = this.data_kprq_s.Checked ? "1" : "0";
                this.kprq_e_ck = this.data_kprq_e.Checked ? "1" : "0";
                this.rzrq_s_ck = this.data_rzrq_s.Checked ? "1" : "0";
                this.rzrq_e_ck = this.data_rzrq_e.Checked ? "1" : "0";
                if (this.cmb_fpzl.SelectedIndex == 0)
                {
                    this.fpzl = "-1";
                }
                else if (this.cmb_fpzl.SelectedIndex == 1)
                {
                    this.fpzl = "01";
                }
                else if (this.cmb_fpzl.SelectedIndex == 2)
                {
                    this.fpzl = "02";
                }
                else if (this.cmb_fpzl.SelectedIndex == 3)
                {
                    this.fpzl = "03";
                }
                else if (this.cmb_fpzl.SelectedIndex == 4)
                {
                    this.fpzl = "04";
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
            base.DialogResult = DialogResult.OK;
            this.Refresh();
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
            this.xmlComponentLoader1.Size = new Size(660, 0x152);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "查询条件";
            this.xmlComponentLoader1.XMLPath=@"..\Config\Components\Aisino.Fwkp.Fpkj.Form.DKFP.ChaXunTiaoJian\Aisino.Fwkp.Fpkj.Form.DKFP.ChaXunTiaoJian.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(660, 0x152);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ChaXunTiaoJian";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "查询条件";
            base.ResumeLayout(false);
        }

        private void SelectTimeBind()
        {
            try
            {
                this.CardClock = base.TaxCardInstance.GetCardClock();
                int year = this.CardClock.Year;
                int month = this.CardClock.Month;
                this.data_kprq_s.Value = new DateTime(year, month, 1);
                this.data_kprq_e.Value = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                this.data_rzrq_s.Value = new DateTime(year, month, 1);
                this.data_rzrq_e.Value = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                MessageManager.ShowMsgBox(exception.Message);
            }
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

