namespace Aisino.Fwkp.Fpkj.Form.FPXF
{
    using Aisino.Framework.Plugin.Core.Controls;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Framework.Plugin.Core.Util;
    public class DeleteQueRen : BaseForm
    {
        private AisinoBTN but_AllBaoLiu;
        private AisinoBTN but_AllDelete;
        private AisinoBTN but_BaoLiu;
        private AisinoBTN but_Delete;
        private IContainer components;
        private string InvoiceTag = string.Empty;
        private ILog loger = LogUtil.GetLogger<DeleteQueRen>();
        private AisinoLBL textBox_Fphm;
        private AisinoLBL textBox_Hjje;
        private AisinoLBL textBox_Hjse;
        private AisinoLBL textBox_Kprq;
        private AisinoLBL textBox_Lbdm;
        private XmlComponentLoader xmlComponentLoader1;

        public DeleteQueRen()
        {
            try
            {
                this.Initialize();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void but_AllBaoLiu_Click(object sender, EventArgs e)
        {
            try
            {
                base.DialogResult = DialogResult.Yes;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void but_AllDelete_Click(object sender, EventArgs e)
        {
            try
            {
                base.DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void but_BaoLiu_Click(object sender, EventArgs e)
        {
            try
            {
                base.DialogResult = DialogResult.Ignore;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        private void but_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                base.DialogResult = DialogResult.No;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
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
            this.but_AllDelete = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_AllDelete");
            this.but_Delete = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_Delete");
            this.but_BaoLiu = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_BaoLiu");
            this.but_AllBaoLiu = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("but_AllBaoLiu");
            this.textBox_Lbdm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("textBox_Lbdm");
            this.textBox_Hjse = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("textBox_Hjse");
            this.textBox_Hjje = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("textBox_Hjje");
            this.textBox_Kprq = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("textBox_Kprq");
            this.textBox_Fphm = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("textBox_Fphm");
            this.but_AllDelete.Click += new EventHandler(this.but_AllDelete_Click);
            this.but_Delete.Click += new EventHandler(this.but_Delete_Click);
            this.but_BaoLiu.Click += new EventHandler(this.but_BaoLiu_Click);
            this.but_AllBaoLiu.Click += new EventHandler(this.but_AllBaoLiu_Click);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(510, 340);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.XMLPath =@"..\Config\Components\Aisino.Fwkp.Fpkj.Form.FPXF.DeleteQueRen\Aisino.Fwkp.Fpkj.Form.FPXF.DeleteQueRen.xml";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(510, 340);
            base.Controls.Add(this.xmlComponentLoader1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DeleteQueRen";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "删除确认";
            base.ResumeLayout(false);
        }

        public void SetFPHM(int FPHM)
        {
            try
            {
                this.textBox_Fphm.Text = Convert.ToString(FPHM);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        public void SetHJJE(decimal HJJE)
        {
            try
            {
                this.textBox_Hjje.Text = string.Format("{0:F2}", Convert.ToDouble(HJJE));
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        public void SetHJSE(decimal HJSE)
        {
            try
            {
                this.textBox_Hjse.Text = string.Format("{0:F2}", Convert.ToDouble(HJSE));
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        public void SetInvoiceTag(string InvoiceTag)
        {
            try
            {
                this.InvoiceTag = InvoiceTag;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        public void SetKPRQ(DateTime KPRQ)
        {
            try
            {
                this.textBox_Kprq.Text = KPRQ.ToLongDateString();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        public void SetLBDM(string LBDM)
        {
            try
            {
                this.textBox_Lbdm.Text = LBDM;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }
    }
}

