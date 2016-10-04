namespace Aisino.Fwkp.Fpkj.Form.FPCX
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Fpkj.Common;
    using Aisino.Fwkp.Fpkj.Model;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using Framework.Plugin.Core.Util;
    public class SelectTime : BaseForm
    {
        private AisinoBTN but_back;
        private AisinoBTN but_ok;
        private DateTime CardClock = DateTime.Now;
        private AisinoCHK che_jsrq;
        private AisinoCHK che_ksrq;
        private IContainer components;
        private DateTimePicker data_jsrq;
        private DateTimePicker data_ksrq;
        public AisinoGRP group_box;
        private ILog loger = LogUtil.GetLogger<SelectTime>();
        public static Dictionary<string, object> OutPutCondition = new Dictionary<string, object>();
        private XmlComponentLoader xmlComponentLoader1;

        public SelectTime()
        {
            try
            {
                this.Refresh();
                this.Initialize();
                this.CardClock = base.TaxCardInstance.GetCardClock();
                this.but_back.Click += new EventHandler(this.but_back_Click);
                this.but_ok.Click += new EventHandler(this.but_ok_Click);
                this.SelectTimeBind();
            }
            catch (Exception exception)
            {
                this.loger.Error("[函数SelectTime异常]" + exception.Message);
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
                OutPutCondition.Clear();
                DateTime time = this.data_ksrq.Value;
                DateTime time2 = this.data_jsrq.Value;
                string str = "";
                string str2 = "";
                if (!this.che_ksrq.Checked)
                {
                    str = time.AddDays(1.0).ToString("yyyy-MM-dd") + " 00:00:00";
                }
                else
                {
                    str = time.ToString("yyyy-MM-dd") + " 00:00:00";
                }
                if (!this.che_jsrq.Checked)
                {
                    str2 = time2.AddDays(-1.0).ToString("yyyy-MM-dd") + " 23:59:59";
                }
                else
                {
                    str2 = time2.ToString("yyyy-MM-dd") + " 23:59:59";
                }
                OutPutCondition.Add("KSRQ", str);
                OutPutCondition.Add("JSRQ", str2);
                base.DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                this.loger.Error("[函数but_ok_Click异常]" + exception.Message);
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

        public DataTable GetData(int year)
        {
            try
            {
                IBaseDAO baseDAOSQLite = BaseDAOFactory.GetBaseDAOSQLite();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                string str = year.ToString();
                dictionary.Add("Year", str);
                UserMsg msg = new UserMsg();
                if (msg.IsAdmin)
                {
                    dictionary.Add("AdminBz", 1);
                }
                else
                {
                    dictionary.Add("AdminBz", 0);
                }
                dictionary.Add("Admin", msg.MC);
                return baseDAOSQLite.querySQLDataTable("aisino.fwkp.fpkj.YFCX", dictionary);
            }
            catch (Exception exception)
            {
                this.loger.Error("[函数GetData异常]" + exception.Message);
                return null;
            }
        }

        private void Initialize()
        {
            this.InitializeComponent();
            this.but_ok = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_ok");
            this.but_back = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_back");
            this.group_box = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.data_ksrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_ksrq");
            this.che_ksrq = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("che_ksrq");
            this.data_jsrq = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("data_jsrq");
            this.che_jsrq = this.xmlComponentLoader1.GetControlByName<AisinoCHK>("che_jsrq");
            base.FormClosing += new FormClosingEventHandler(this.SelectTime_FormClosing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SelectTime));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x163, 0xb9);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.Tag = manager.GetObject("xmlComponentLoader1.Tag");
            this.xmlComponentLoader1.Text = "发票查询";
            this.xmlComponentLoader1.XMLPath = @"..\Config\Components\Aisino.Fwkp.Fpkj.Form.FPCX.SelectTime\Aisino.Fwkp.Fpkj.Form.FPCX.SelectTime.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x163, 0xb9);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "SelectTime";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Tag = manager.GetObject("$this.Tag");
            this.Text = "发票查询";
            base.ResumeLayout(false);
        }

        private string ReadKzyf()
        {
            try
            {
                return base.TaxCardInstance.CardEffectDate;
            }
            catch (Exception exception)
            {
                this.loger.Error("[函数ReadKzyf异常]" + exception.Message);
                return base.TaxCardInstance.GetCardClock().ToString("yyyyMM");
            }
        }

        private void SelectTime_FormClosing(object sender, EventArgs e)
        {
        }

        private void SelectTimeBind()
        {
            try
            {
                string str = this.ReadKzyf();
                int year = this.CardClock.Year;
                int month = this.CardClock.Month;
                if (!string.IsNullOrEmpty(str))
                {
                    year = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(str.Substring(0, 4));
                    month = Aisino.Fwkp.Fpkj.Common.Tool.ObjectToInt(str.Substring(4, 2));
                }
                else
                {
                    return;
                }
                int num3 = this.CardClock.Month;
                int num4 = this.CardClock.Year;
                if (this.CardClock.Year == year)
                {
                    if (this.CardClock.Month == month)
                    {
                        num3 = month;
                    }
                    else
                    {
                        num3--;
                    }
                }
                else if (this.CardClock.Year > year)
                {
                    if (this.CardClock.Month == 1)
                    {
                        num4--;
                        num3 = 12;
                    }
                    else
                    {
                        num3--;
                    }
                }
                this.data_ksrq.Value = new DateTime(num4, num3, 1);
                this.data_jsrq.Value = new DateTime(num4, num3, DateTime.DaysInMonth(num4, num3));
            }
            catch (Exception exception)
            {
                this.loger.Error("[函数SelectTimeBind异常]" + exception.Message);
            }
        }

        public delegate void MonthChangeEventHandler(object sender, MonthChangeEventArgs e);
    }
}

