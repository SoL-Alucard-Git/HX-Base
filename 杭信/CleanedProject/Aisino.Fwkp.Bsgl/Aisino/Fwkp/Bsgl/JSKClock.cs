namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Threading;
    using System.Windows.Forms;

    public class JSKClock : DockForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private DateTime clockTimeOld;
        private IContainer components;
        private DateTimePicker dtpDate;
        private DateTimePicker dtpTime;
        private AisinoGRP groupBox1;
        private DateTime kpMaxDate;
        private AisinoLBL label1;
        private AisinoLBL label2;
        private AisinoLBL label3;
        private AisinoLBL label4;
        private AisinoLBL label5;
        private AisinoLBL label6;
        private AisinoLBL lblFirstDay;
        private AisinoLBL lblKPMaxDay;
        private AisinoLBL lblMonth;
        private AisinoLBL lblWeek;
        private AisinoPNL panel1;
        private AisinoPNL panel2;
        private AisinoPIC pictureBox1;
        private TaxcardEntityBLL taxcardEntityBLL = new TaxcardEntityBLL();
        private XmlComponentLoader xmlComponentLoader1;

        public JSKClock()
        {
            this.Initialize();
            base.Load += new EventHandler(this.JSKClock_Load);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime time = this.dtpDate.Value;
            DateTime time2 = this.dtpTime.Value;
            DateTime time3 = new DateTime(time.Year, time.Month, time.Day, time2.Hour, time2.Minute, time2.Second);
            if (base.TaxCardInstance.GetStateInfo(false).IsLockReached != 0)
            {
                MessageManager.ShowMsgBox("INP-252201");
            }
            else if (this.taxcardEntityBLL.IsLockedHY())
            {
                MessageManager.ShowMsgBox("INP-252201");
            }
            else if (this.taxcardEntityBLL.IsLockedJDC())
            {
                MessageManager.ShowMsgBox("INP-252201");
            }
            else
            {
                if (base.TaxCardInstance.SetCardClock(time3))
                {
                    MessageManager.ShowMsgBox("TCD_9118_", new List<KeyValuePair<string, string>>(), new string[] { "" }, new string[] { "" });
                }
                else
                {
                    if (base.TaxCardInstance.get_RetCode() > 0)
                    {
                        MessageManager.ShowMsgBox(base.TaxCardInstance.get_ErrCode());
                        return;
                    }
                    MessageManager.ShowMsgBox("TCD_9117_", new List<KeyValuePair<string, string>>(), new string[] { "" }, new string[] { "" });
                    return;
                }
                base.Close();
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

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            this.lblWeek.Text = this.dtpDate.Value.ToString("dddd", new CultureInfo("zh-CN"));
        }

        private void InitClockDate()
        {
            this.clockTimeOld = base.TaxCardInstance.GetCardClock();
            this.lblMonth.Text = this.clockTimeOld.Month + "月";
            this.dtpDate.Value = this.clockTimeOld;
            this.dtpTime.Value = this.clockTimeOld;
            this.lblWeek.Text = this.dtpDate.Value.ToString("dddd", new CultureInfo("zh-CN"));
        }

        private void Initialize()
        {
            this.InitializeComponent();
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.label3 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label3");
            this.panel2 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel2");
            this.lblMonth = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblMonth");
            this.lblKPMaxDay = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblKPMaxDay");
            this.lblFirstDay = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblFirstDay");
            this.label6 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label6");
            this.label5 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label5");
            this.label4 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label4");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.groupBox1 = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox1");
            this.lblWeek = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblWeek");
            this.dtpDate = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("dtpDate");
            this.label2 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label2");
            this.label1 = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("label1");
            this.panel1 = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("panel1");
            this.pictureBox1 = this.xmlComponentLoader1.GetControlByName<AisinoPIC>("pictureBox1");
            this.dtpTime = this.xmlComponentLoader1.GetControlByName<DateTimePicker>("dtpTime");
            this.dtpTime.Format = DateTimePickerFormat.Time;
            this.dtpTime.ShowUpDown = true;
            this.dtpDate.ValueChanged += new EventHandler(this.dtpDate_ValueChanged);
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(JSKClock));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x1a5, 350);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.JSKClock\Aisino.Fwkp.Bsgl.JSKClock.xml");
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1a5, 350);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "JSKClock";
            base.set_TabText("金税设备时钟设置");
            this.Text = "金税设备时钟设置";
            base.ResumeLayout(false);
        }

        private void JSKClock_Load(object sender, EventArgs e)
        {
            this.InitClockDate();
            this.kpMaxDate = new FPDetailDAL().SelectLastKPMaxDate();
            if (DateTime.Compare(this.kpMaxDate, DateTime.MinValue) == 0)
            {
                this.lblKPMaxDay.Text = this.kpMaxDate.ToString("销项发票库为空，无法判断以前开票最大日期。");
            }
            else
            {
                this.lblKPMaxDay.Text = this.kpMaxDate.ToString("yyyy年MM月dd日");
            }
        }

        private void Restart()
        {
            Thread thread = new Thread(new ParameterizedThreadStart(this.Run));
            object executablePath = Application.ExecutablePath;
            Thread.Sleep(0x7d0);
            thread.Start(executablePath);
        }

        private void Run(object obj)
        {
            new Process { StartInfo = { FileName = obj.ToString() } }.Start();
        }
    }
}

