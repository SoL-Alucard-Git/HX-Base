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
    using System.Windows.Forms;

    public class ChaXunTiaoJian : BaseForm
    {
        private AisinoDataGrid _AisinoGrid;
        private DateTime _CardClock;
        public bool blDateEnd_Bkjr;
        public bool blDateStart_Bkjr;
        public bool blNameQry;
        public bool blTaxCodeQry;
        public int BSZT1;
        public int BSZT2;
        private AisinoBTN btn_close;
        private AisinoBTN btn_ok;
        private AisinoBTN btn_zhuhechaxun;
        private AisinoCHK check_Sort;
        private AisinoCMB com_fpzl;
        private IContainer components;
        private DateTimePicker data_jsrq;
        private DateTimePicker data_ksrq;
        public DateTime DateEnd;
        public DateTime DateStart;
        private FaPiaoChaXun fpChaXun;
        public bool IsFpzl;
        public string KindQry;
        private string KindStr;
        private ILog loger;
        private readonly DateTime MaxDateTime;
        public int Month;
        public string NameQry;
        public int SortWay;
        public string TaxCodeQry;
        private AisinoTXT txt_gfmc;
        private AisinoTXT txt_gfsh;
        private XmlComponentLoader xmlComponentLoader1;
        private XXFP xxfpChaXunBll;
        public int Year;

        public ChaXunTiaoJian(DateTime CardClock)
        {
            this.Year = 0x76b;
            this.loger = LogUtil.GetLogger<ChaXunTiaoJian>();
            this.NameQry = string.Empty;
            this.TaxCodeQry = string.Empty;
            this.KindQry = string.Empty;
            this.KindStr = " ";
            this.xxfpChaXunBll = new XXFP(false);
            this.MaxDateTime = new DateTime(0x270e, 12, DateTime.DaysInMonth(0x270e, 12));
            this.SortWay = 1;
            this.BSZT1 = -1;
            this.BSZT2 = -1;
            try
            {
                this.Initialize();
                this.fpChaXun = null;
                this._CardClock = CardClock;
                this.Year = CardClock.Year;
                this.txt_gfmc = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_gfmc");
                this.txt_gfsh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_gfsh");
                this.data_ksrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_ksrq");
                this.data_jsrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_jsrq");
                this.com_fpzl = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("com_fpzl");
                this.btn_zhuhechaxun = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_zhuhechaxun");
                this.btn_ok = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_ok");
                this.btn_close = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_close");
                this.com_fpzl = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("com_fpzl");
                this.btn_ok = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_ok");
                this.btn_close = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_close");
                this.check_Sort = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("check_Sort");
                this.SetSearchControlAttritute();
                this.com_fpzl.Items.Clear();
                this.SetFPZLAuthorization();
                this.com_fpzl.SelectedIndex = 0;
                this.com_fpzl.DropDownStyle = ComboBoxStyle.DropDownList;
                this.NameQry = this.txt_gfmc.Text = string.Empty;
                this.TaxCodeQry = this.txt_gfsh.Text = string.Empty;
                this.blNameQry = true;
                this.blTaxCodeQry = true;
                this.btn_zhuhechaxun.Click += new EventHandler(this.btnZhuHeChaXun_Click);
                this.btn_ok.Click += new EventHandler(this.btnOK_Click);
                this.btn_close.Click += new EventHandler(this.btnCancle_Click);
                this.data_ksrq.TextChanged += new EventHandler(this.data_ksrq_TextChanged);
                this.data_jsrq.TextChanged += new EventHandler(this.data_jsrq_TextChanged);
                this.txt_gfmc.MaxLength = 100;
                this.txt_gfsh.MaxLength = 20;
                this.txt_gfmc.KeyPress += new KeyPressEventHandler(this.textBox_GFMC_KeyPress);
                this.txt_gfsh.KeyPress += new KeyPressEventHandler(this.textBox_GFSH_KeyPress);
                this.txt_gfmc.TextChanged += new EventHandler(this.txt_gfmc_TextChanged);
                this.BSZT1 = -1;
                this.BSZT2 = -1;
                this.IsFpzl = false;
                string str = PropertyUtil.GetValue("Aisino.Fwkp.Fpkj.Form.FPCX_FaPiaoChaXunTiaoJian_SortWay");
                if ((str == "1") || (str == ""))
                {
                    this.SortWay = 1;
                    this.check_Sort.Checked = true;
                }
                else
                {
                    this.SortWay = 0;
                    this.check_Sort.Checked = false;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[ChaXunTiaoJian构造函数异常]：" + exception.Message);
            }
        }

        public ChaXunTiaoJian(DateTime CardClock, FaPiaoChaXun ChaXunHandle)
        {
            this.Year = 0x76b;
            this.loger = LogUtil.GetLogger<ChaXunTiaoJian>();
            this.NameQry = string.Empty;
            this.TaxCodeQry = string.Empty;
            this.KindQry = string.Empty;
            this.KindStr = " ";
            this.xxfpChaXunBll = new XXFP(false);
            this.MaxDateTime = new DateTime(0x270e, 12, DateTime.DaysInMonth(0x270e, 12));
            this.SortWay = 1;
            this.BSZT1 = -1;
            this.BSZT2 = -1;
            try
            {
                this.Initialize();
                this.fpChaXun = ChaXunHandle;
                this._CardClock = CardClock;
                this.Year = CardClock.Year;
                this.txt_gfmc = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_gfmc");
                this.txt_gfsh = this.xmlComponentLoader1.GetControlByName<AisinoTXT>("txt_gfsh");
                this.data_ksrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_ksrq");
                this.data_jsrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_jsrq");
                this.com_fpzl = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("com_fpzl");
                this.btn_zhuhechaxun = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_zhuhechaxun");
                this.btn_ok = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_ok");
                this.btn_close = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_close");
                this.com_fpzl = this.xmlComponentLoader1.GetControlByName<AisinoCMB>("com_fpzl");
                this.btn_ok = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_ok");
                this.btn_close = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_close");
                this.check_Sort = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("check_Sort");
                this.SetSearchControlAttritute();
                this.com_fpzl.Items.Clear();
                this.SetFPZLAuthorization();
                this.com_fpzl.SelectedIndex = 0;
                this.com_fpzl.DropDownStyle = ComboBoxStyle.DropDownList;
                this.NameQry = this.txt_gfmc.Text = string.Empty;
                this.TaxCodeQry = this.txt_gfsh.Text = string.Empty;
                this.blNameQry = true;
                this.blTaxCodeQry = true;
                this.btn_zhuhechaxun.Click += new EventHandler(this.btnZhuHeChaXun_Click);
                this.btn_ok.Click += new EventHandler(this.btnOK_Click);
                this.btn_close.Click += new EventHandler(this.btnCancle_Click);
                this.data_ksrq.TextChanged += new EventHandler(this.data_ksrq_TextChanged);
                this.data_jsrq.TextChanged += new EventHandler(this.data_jsrq_TextChanged);
                this.txt_gfmc.MaxLength = 100;
                this.txt_gfsh.MaxLength = 20;
                this.txt_gfmc.KeyPress += new KeyPressEventHandler(this.textBox_GFMC_KeyPress);
                this.txt_gfsh.KeyPress += new KeyPressEventHandler(this.textBox_GFSH_KeyPress);
                this.txt_gfmc.TextChanged += new EventHandler(this.txt_gfmc_TextChanged);
                this.BSZT1 = -1;
                this.BSZT2 = -1;
                this.check_Sort.Checked = true;
                this.IsFpzl = false;
                switch (PropertyUtil.GetValue("Aisino.Fwkp.Fpkj.Form.FPCX_FaPiaoChaXunTiaoJian_SortWay"))
                {
                    case "1":
                    case "":
                        this.SortWay = 1;
                        this.check_Sort.Checked = true;
                        break;

                    case "0":
                        this.SortWay = 0;
                        this.check_Sort.Checked = false;
                        break;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("[ChaXunTiaoJian构造函数异常]：" + exception.Message);
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            try
            {
                base.DialogResult = DialogResult.Cancel;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.GetFormData();
                base.DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void btnZhuHeChaXun_Click(object sender, EventArgs e)
        {
            try
            {
                this._AisinoGrid.Select(this);
                base.DialogResult = DialogResult.Cancel;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void com_fpzl_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void data_jsrq_TextChanged(object sender, EventArgs e)
        {
            this.data_jsrq.MinDate = this.data_ksrq.Value;
            this.data_ksrq.MaxDate = this.data_jsrq.Value;
        }

        private void data_ksrq_TextChanged(object sender, EventArgs e)
        {
            this.data_ksrq.MaxDate = this.data_jsrq.Value;
            this.data_jsrq.MinDate = this.data_ksrq.Value;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void GetFormData()
        {
            try
            {
                this.NameQry = this.txt_gfmc.Text.Trim();
                this.TaxCodeQry = this.txt_gfsh.Text.Trim();
                this.DateStart = this.data_ksrq.Value;
                this.DateEnd = this.data_jsrq.Value;
                if (this.com_fpzl.SelectedIndex > 0)
                {
                    this.KindQry = this.KindStr.Substring(this.com_fpzl.SelectedIndex, 1);
                }
                else
                {
                    this.KindQry = "";
                }
                int index = this.com_fpzl.Items.IndexOf(Aisino.Fwkp.Fpkj.Common.Tool.GetFPType(this.KindQry));
                if (((index >= 0) && (index < this.com_fpzl.Items.Count)) && (this.fpChaXun != null))
                {
                    this.IsFpzl = true;
                    this.fpChaXun.SetToolFpZl(index);
                }
                this.SortWay = this.check_Sort.Checked ? 1 : 0;
                PropertyUtil.SetValue("Aisino.Fwkp.Fpkj.Form.FPCX_FaPiaoChaXunTiaoJian_SortWay", this.SortWay.ToString());
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        public static string GetSubString(string s, int len)
        {
            if ((s == null) || (s.Length == 0))
            {
                return string.Empty;
            }
            if (ToolUtil.GetByteCount(s) > len)
            {
                for (int i = s.Length; i >= 0; i--)
                {
                    s = s.Substring(0, i);
                    if (ToolUtil.GetByteCount(s) <= len)
                    {
                        return s;
                    }
                }
            }
            return s;
        }

        private void Initialize()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ChaXunTiaoJian));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x16e, 340);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "查询条件";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Fpkj.Form.FPCX.ChaXunTiaoJian\Aisino.Fwkp.Fpkj.Form.FPCX.ChaXunTiaoJian.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x16e, 340);
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

        private void SetFPZLAuthorization()
        {
            try
            {
                string str = this.xxfpChaXunBll.SelectFPZLListFromDB(null);
                if (str == null)
                {
                    str = "";
                }
                this.com_fpzl.Items.Add("全部发票");
                if ((((int)base.TaxCardInstance.TaxMode == 2) && base.TaxCardInstance.QYLX.ISZYFP) || (str.IndexOf('s') != -1))
                {
                    this.com_fpzl.Items.Add("专用发票");
                    this.KindStr = this.KindStr + "s";
                }
                if ((((int)base.TaxCardInstance.TaxMode == 2) && base.TaxCardInstance.QYLX.ISPTFP) || (str.IndexOf('c') != -1))
                {
                    this.com_fpzl.Items.Add("普通发票");
                    this.KindStr = this.KindStr + "c";
                }
                if ((((int)base.TaxCardInstance.TaxMode == 2) && base.TaxCardInstance.QYLX.ISHY) || (str.IndexOf('f') != -1))
                {
                    this.com_fpzl.Items.Add("货物运输业增值税专用发票");
                    this.KindStr = this.KindStr + "f";
                }
                if ((((int)base.TaxCardInstance.TaxMode == 2) && base.TaxCardInstance.QYLX.ISJDC) || (str.IndexOf('j') != -1))
                {
                    this.com_fpzl.Items.Add("机动车销售统一发票");
                    this.KindStr = this.KindStr + "j";
                }
                if ((((int)base.TaxCardInstance.TaxMode == 2) && base.TaxCardInstance.QYLX.ISPTFPDZ) || (str.IndexOf('p') != -1))
                {
                    this.com_fpzl.Items.Add(Aisino.Fwkp.Fpkj.Common.Tool.DZFP);
                    this.KindStr = this.KindStr + "p";
                }
                if ((((int)base.TaxCardInstance.TaxMode == 2) && base.TaxCardInstance.QYLX.ISPTFPJSP) || (str.IndexOf('q') != -1))
                {
                    this.com_fpzl.Items.Add(Aisino.Fwkp.Fpkj.Common.Tool.JSFP);
                    this.KindStr = this.KindStr + "q";
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("SetFPZLAuthorization 异常" + exception.Message);
            }
        }

        public void SetSearchControlAttritute()
        {
            try
            {
                int month = this.Month;
                if (this.Month == 0)
                {
                    month = 1;
                }
                int year = this.Year;
                DateTime time = new DateTime(year, month, 1);
                if (this.data_ksrq.MaxDate.CompareTo(time) <= 0)
                {
                    this.data_ksrq.MaxDate = this.MaxDateTime;
                }
                this.data_ksrq.MinDate = time;
                if (this.Month != 0)
                {
                    this.data_ksrq.MinDate = new DateTime(year, month, 1);
                    this.data_ksrq.MaxDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                }
                else
                {
                    this.data_ksrq.MinDate = new DateTime(year, 1, 1);
                    this.data_ksrq.MaxDate = new DateTime(year, 12, DateTime.DaysInMonth(year, 12));
                }
                this.data_ksrq.Value = new DateTime(year, month, 1);
                this.DateStart = this.data_ksrq.Value;
                if (this.Month == 0)
                {
                    month = 12;
                }
                int num3 = this.Year;
                time = new DateTime(num3, month, 1);
                if (this.data_jsrq.MaxDate.CompareTo(time) <= 0)
                {
                    this.data_jsrq.MaxDate = this.MaxDateTime;
                }
                this.data_jsrq.MinDate = time;
                if (this.Month == 0)
                {
                    this.data_jsrq.MinDate = new DateTime(num3, 1, 1);
                    this.data_jsrq.MaxDate = new DateTime(num3, 12, DateTime.DaysInMonth(num3, 12));
                }
                else
                {
                    this.data_jsrq.MinDate = new DateTime(num3, month, 1);
                    this.data_jsrq.MaxDate = new DateTime(num3, month, DateTime.DaysInMonth(num3, month));
                }
                this.data_jsrq.Value = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                this.DateEnd = this.data_jsrq.Value;
                if ((this.com_fpzl != null) && (this.com_fpzl.Items.Count != 0))
                {
                    if (this.com_fpzl.Items.Count <= 1)
                    {
                        this.com_fpzl.Items.Clear();
                        this.SetFPZLAuthorization();
                        this.com_fpzl.SelectedIndex = 0;
                        this.com_fpzl.DropDownStyle = ComboBoxStyle.DropDownList;
                    }
                    int index = this.com_fpzl.Items.IndexOf(Aisino.Fwkp.Fpkj.Common.Tool.GetFPType(this.KindQry));
                    if ((index >= 0) && (index < this.com_fpzl.Items.Count))
                    {
                        this.com_fpzl.SelectedIndex = index;
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void textBox_GFMC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.txt_gfmc.Text.Length > 100)
            {
                e.Handled = true;
            }
        }

        private void textBox_GFSH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsLetterOrDigit(e.KeyChar) && (e.KeyChar != '\r')) && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }

        private void txt_gfmc_TextChanged(object sender, EventArgs e)
        {
            AisinoTXT otxt = (AisinoTXT) sender;
            int selectionStart = otxt.SelectionStart;
            otxt.Text = GetSubString(otxt.Text, 100).Trim();
            otxt.SelectionStart = selectionStart;
        }

        private void txt_gfsh_TextChanged(object sender, EventArgs e)
        {
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

