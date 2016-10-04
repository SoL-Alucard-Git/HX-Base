namespace Aisino.Fwkp.Fpkj.Form.FPCX
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fpkj.Common;
    using Aisino.Fwkp.Fpkj.DAL;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class SelectMonth : BaseForm
    {
        public static int _StartMonth = 0;
        public static int _StartYear = 0;
        private AisinoLBL aisinoLBL2;
        private AisinoLBL aisinoLBL3;
        private AisinoBTN but_back;
        private AisinoBTN but_ok;
        public DateTime CardClock;
        private RadioButton chxDate;
        private RadioButton chxSingle;
        private AisinoCMB com_month;
        private AisinoCMB com_year;
        private IContainer components;
        public int Day;
        public static string DBMinDate = null;
        public static string FPDM = "";
        public static string FPHM = "";
        public AisinoGRP group_box;
        public static bool IsChaxun = true;
        public static bool IsSingle = false;
        private ILog loger = LogUtil.GetLogger<SelectMonth>();
        public int Month;
        private AisinoTXT tbxfpdm;
        private AisinoTXT tbxfphm;
        private XmlComponentLoader xmlComponentLoader1;
        private XXFP xxfpChaXunBll = new XXFP(false);
        public int Year = 0x7d0;

        public SelectMonth()
        {
            try
            {
                this.Refresh();
                this.Initialize();
                this.but_back.Click += new EventHandler(this.but_back_Click);
                this.but_ok.Click += new EventHandler(this.but_ok_Click);
                this.CardClock = base.TaxCardInstance.GetCardClock();
                this.ComboxBind();
            }
            catch (Exception exception)
            {
                this.loger.Error("[函数SelectMonth异常]" + exception.ToString());
            }
        }

        private void but_back_Click(object sender, EventArgs e)
        {
            try
            {
                base.DialogResult = DialogResult.Cancel;
            }
            catch (Exception exception)
            {
                this.loger.Error("[函数but_back_Click异常]" + exception.Message);
            }
        }

        private void but_ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsChaxun)
                {
                    this.GetData_FPCX();
                }
                else
                {
                    this.GetData_FPXF();
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[函数but_ok_Click异常]" + exception.Message);
            }
        }

        private void chxDate_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = (RadioButton) sender;
            string name = button.Name;
            if (name != null)
            {
                if (!(name == "chxDate"))
                {
                    if (!(name == "chxSingle"))
                    {
                        return;
                    }
                }
                else
                {
                    this.com_year.Enabled = this.chxDate.Checked;
                    this.com_month.Enabled = this.chxDate.Checked;
                    return;
                }
                this.tbxfpdm.Enabled = this.chxSingle.Checked;
                this.tbxfphm.Enabled = this.chxSingle.Checked;
            }
        }

        private void com_day_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Year = this.CardClock.Year;
            if (this.com_year.Text.Length >= 4)
            {
                this.Year = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(this.com_year.Text.Substring(0, 4));
            }
            this.Month = this.CardClock.Month;
            if (this.com_month.Text.Length >= 4)
            {
                this.Month = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(this.com_month.Text.Substring(0, 4));
            }
            DateTime.DaysInMonth(this.Year, this.Month);
        }

        private void com_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = this.CardClock.Year;
            if (this.com_year.Text.Length >= 4)
            {
                year = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(this.com_year.Text.Substring(0, 4));
            }
            int startYear = _StartYear;
            int cardYear = this.CardClock.Year;
            this.SetMonthList_FPXF(year, startYear, cardYear);
            this.com_month.SelectedIndex = 0;
        }

        private void ComboxBind()
        {
            try
            {
                string str = this.ReadKzyf();
                _StartYear = this.CardClock.Year;
                _StartMonth = this.CardClock.Month;
                if (!string.IsNullOrEmpty(str))
                {
                    _StartYear = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(str.Substring(0, 4));
                    _StartMonth = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(str.Substring(4, 2));
                }
                this.com_year.Items.Clear();
                this.com_month.Items.Clear();
                for (int i = this.CardClock.Year; i >= _StartYear; i--)
                {
                    this.com_year.Items.Add(i.ToString() + "年");
                }
                if (this.com_year.Items.Count <= 0)
                {
                    this.but_back.Enabled = false;
                    this.but_ok.Enabled = false;
                }
                else
                {
                    this.com_year.SelectedIndex = 0;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[ComboxBind函数异常]读取金税发行日期错误，" + exception.ToString());
                if (this.com_year.Items.Count <= 0)
                {
                    this.but_back.Enabled = false;
                    this.but_ok.Enabled = false;
                }
                MessageManager.ShowMsgBox(exception.ToString());
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

        private void GetData_FPCX()
        {
            this.Year = this.CardClock.Year;
            if ((this.com_year.Text != null) && (this.com_year.Text.Length >= 4))
            {
                this.Year = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(this.com_year.Text.Substring(0, 4));
            }
            this.Month = this.CardClock.Month;
            if (this.com_month.Text == "全年数据")
            {
                this.Month = 0;
            }
            else if ((this.com_month.Text != null) && (this.com_month.Text.Length >= 2))
            {
                this.Month = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(this.com_month.Text.Substring(0, 2));
            }
            base.DialogResult = DialogResult.OK;
        }

        private void GetData_FPXF()
        {
            this.Year = this.CardClock.Year;
            if (this.com_year.Text.Length >= 4)
            {
                this.Year = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(this.com_year.Text.Substring(0, 4));
            }
            this.Month = this.CardClock.Month;
            if (this.com_month.Text == "全年数据")
            {
                this.Month = 0;
            }
            else if ((this.com_month.Text != null) && (this.com_month.Text.Length >= 2))
            {
                this.Month = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(this.com_month.Text.Substring(0, 2));
            }
            if (base.TaxCardInstance.SoftVersion != "FWKP_V2.0_Svr_Client")
            {
                IsSingle = false;
            }
            else if (this.chxDate.Checked)
            {
                IsSingle = false;
            }
            else
            {
                IsSingle = true;
            }
            FPDM = this.tbxfpdm.Text;
            FPHM = this.tbxfphm.Text;
            base.DialogResult = DialogResult.OK;
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.com_month = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("com_month");
            this.com_year = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("aisinoCMB_Year");
            this.tbxfpdm = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("tbxfpdm");
            this.tbxfphm = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("tbxfphm");
            this.but_ok = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_ok");
            this.but_back = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_back");
            this.group_box = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.com_year.DropDownStyle = ComboBoxStyle.DropDownList;
            this.com_month.DropDownStyle = ComboBoxStyle.DropDownList;
            this.aisinoLBL2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL2");
            this.aisinoLBL3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("aisinoLBL3");
            this.chxDate = this.xmlComponentLoader1.GetControlByName<RadioButton>("chxDate");
            this.chxSingle = this.xmlComponentLoader1.GetControlByName<RadioButton>("chxSingle");
            this.group_box.Text = "发票修复";
            this.com_year.SelectedIndexChanged += new EventHandler(this.com_year_SelectedIndexChanged);
            if (base.TaxCardInstance.SoftVersion == "FWKP_V2.0_Svr_Client")
            {
                base.ClientSize = new Size(0x163, 0xff);
                this.but_ok.Location = new Point(this.but_ok.Location.X - 0x2d, this.but_ok.Location.Y);
                this.but_back.Location = new Point(this.but_back.Location.X - 0x2d, this.but_back.Location.Y);
                this.tbxfpdm.MaxLength = 12;
                this.tbxfphm.MaxLength = 8;
                this.tbxfpdm.KeyPress += new KeyPressEventHandler(this.tbxfpdm_KeyPress);
                this.tbxfphm.KeyPress += new KeyPressEventHandler(this.tbxfpdm_KeyPress);
                this.tbxfpdm.Text = "";
                this.tbxfphm.Text = "";
                this.chxDate.CheckedChanged += new EventHandler(this.chxDate_CheckedChanged);
                this.chxSingle.CheckedChanged += new EventHandler(this.chxDate_CheckedChanged);
                this.chxDate.Checked = true;
                this.tbxfpdm.Enabled = false;
                this.tbxfphm.Enabled = false;
            }
            else
            {
                this.xmlComponentLoader1.Size = new Size(0x163, 180);
                base.ClientSize = new Size(0x163, 180);
                this.group_box.ClientSize = new Size(0x163, 120);
                this.but_ok.Location = new Point(this.but_ok.Location.X - 0x2d, this.but_ok.Location.Y - 80);
                this.but_back.Location = new Point(this.but_back.Location.X - 0x2d, this.but_back.Location.Y - 80);
                this.chxSingle.Visible = false;
                this.chxDate.Visible = false;
                this.tbxfpdm.Visible = false;
                this.tbxfphm.Visible = false;
                this.aisinoLBL2.Visible = false;
                this.aisinoLBL3.Visible = false;
            }
            base.FormClosing += new FormClosingEventHandler(this.SelectMonth_FormClosing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SelectMonth));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            if (base.TaxCardInstance.SoftVersion == "FWKP_V2.0_Svr_Client")
            {
                this.xmlComponentLoader1.Size = new Size(0x163, 280);
            }
            else
            {
                this.xmlComponentLoader1.Size = new Size(0x163, 180);
            }
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "发票查询";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Fpkj.Form.FPCX.SelectMonth\Aisino.Fwkp.Fpkj.Form.FPCX.SelectMonth.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "SelectMonth";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "发票查询";
            base.ResumeLayout(false);
        }

        private string ReadKzyf()
        {
            try
            {
                if (!IsChaxun)
                {
                    if (base.TaxCardInstance.SoftVersion == "FWKP_V2.0_Svr_Client")
                    {
                        string str = base.TaxCardInstance.CardEffectDate;
                        if (str.Length > 4)
                        {
                            return Aisino.Fwkp.Fpkj.Common.Tool.ObjectToDateTime(str.Insert(4, "-")).AddMonths(-6).ToString("yyyyMM");
                        }
                    }
                    return base.TaxCardInstance.CardEffectDate;
                }
                if (DBMinDate == null)
                {
                    DBMinDate = this.xxfpChaXunBll.SelectMinKprq(null);
                }
                if ((DBMinDate.Length == 0) || (DBMinDate.CompareTo(base.TaxCardInstance.CardEffectDate) > 0))
                {
                    DBMinDate = base.TaxCardInstance.CardEffectDate;
                }
                return DBMinDate;
            }
            catch (Exception exception)
            {
                this.loger.Error("[函数ReadKzyf异常]读取金税设备发行时间异常，" + exception.Message);
                MessageManager.ShowMsgBox("读取金税设备发行时间异常" + exception.Message);
                return this.CardClock.ToString("yyyyMM");
            }
        }

        private void SelectMonth_FormClosing(object sender, EventArgs e)
        {
            this.xxfpChaXunBll = null;
            this.loger = null;
        }

        private void SetMonthList_FPXF(int CurYear, int StartYear, int CardYear)
        {
            this.com_month.Items.Clear();
            int month = 12;
            int num2 = 1;
            if ((CurYear == StartYear) && (CurYear == CardYear))
            {
                month = this.CardClock.Month;
                num2 = _StartMonth;
            }
            else if ((CurYear != StartYear) && (CurYear == CardYear))
            {
                month = this.CardClock.Month;
                num2 = 1;
            }
            else if ((CurYear == StartYear) && (CurYear != CardYear))
            {
                month = 12;
                num2 = _StartMonth;
            }
            else
            {
                month = 12;
                num2 = 1;
            }
            for (int i = month; i >= num2; i--)
            {
                this.com_month.Items.Add(i.ToString("00") + "月份");
            }
            this.com_month.Items.Add("全年数据");
        }

        private void tbxfpdm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '\b') && ((e.KeyChar < '0') || (e.KeyChar > '9')))
            {
                e.Handled = true;
            }
        }

        public delegate void MonthChangeEventHandler(object sender, MonthChangeEventArgs e);
    }
}

