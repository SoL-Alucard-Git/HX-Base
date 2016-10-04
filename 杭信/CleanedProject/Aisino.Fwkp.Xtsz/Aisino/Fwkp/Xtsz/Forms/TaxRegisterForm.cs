namespace Aisino.Fwkp.Xtsz.Forms
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Xtsz.BLL;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TaxRegisterForm : BaseForm
    {
        public bool bIsRegistered;
        private AisinoBTN buttonStart = new AisinoBTN();
        private IContainer components;
        private AisinoLBL labelRemind = new AisinoLBL();
        private static ILog loger = LogUtil.GetLogger<InitialDAL>();
        private const int nTotalStep = 10;
        private AisinoPRG progressBarRegister = new AisinoPRG();
        private TaxReportRegisterBLL taxReportRegisterBLL = new TaxReportRegisterBLL();
        private XmlComponentLoader xmlComponentLoaderRegister;

        public TaxRegisterForm()
        {
            this.Initial();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Initial()
        {
            this.InitializeComponent();
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.labelRemind = this.xmlComponentLoaderRegister.GetControlByName<AisinoLBL>("labelRemind");
            this.progressBarRegister = this.xmlComponentLoaderRegister.GetControlByName<AisinoPRG>("progressBarRegister");
            this.progressBarRegister.Maximum = 10;
            this.buttonStart = this.xmlComponentLoaderRegister.GetControlByName<AisinoBTN>("buttonStart");
            this.buttonStart.Click += new EventHandler(this.buttonStart_Click);
        }

        private void InitializeComponent()
        {
            this.xmlComponentLoaderRegister = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoaderRegister.Dock = DockStyle.Fill;
            this.xmlComponentLoaderRegister.Location = new Point(0, 0);
            this.xmlComponentLoaderRegister.Name = "xmlComponentLoaderRegister";
            this.xmlComponentLoaderRegister.Size = new Size(0x16a, 0x60);
            this.xmlComponentLoaderRegister.TabIndex = 0;
            this.xmlComponentLoaderRegister.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Xtsz.TaxRegisterForm\Aisino.Fwkp.Xtsz.TaxRegisterForm.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x16a, 0x60);
            base.Controls.Add(this.xmlComponentLoaderRegister);
            base.Name = "TaxRegisterForm";
            this.Text = "报税盘注册";
            base.Load += new EventHandler(this.TaxRegisterForm_Load);
            base.ResumeLayout(false);
        }

        public void Register()
        {
            try
            {
                TaxCard card = TaxCardFactory.CreateTaxCard();
                if (!this.taxReportRegisterBLL.GetRegeisteredSign())
                {
                    RegResult result = card.TBRegister();
                    if (result == null)
                    {
                        int message = card.get_RetCode();
                        loger.Debug(message);
                        if (message != 0)
                        {
                            string str = "TCD_" + message.ToString() + "_";
                            loger.Debug(str);
                            MessageManager.ShowMsgBox(str);
                        }
                        else
                        {
                            MessageManager.ShowMsgBox("INP-232102", new string[] { "报税盘注册成功" });
                            this.bIsRegistered = true;
                        }
                    }
                    else if (result == 1)
                    {
                        MessageManager.ShowMsgBox("INP-232102", new string[] { "报税盘已经注册" });
                        this.bIsRegistered = true;
                    }
                    else
                    {
                        MessageManager.ShowMsgBox("INP-232102", new string[] { "报税盘注册成功" });
                        this.bIsRegistered = true;
                    }
                }
                else
                {
                    MessageManager.ShowMsgBox("INP-232102", new string[] { "报税盘已经注册" });
                    this.bIsRegistered = true;
                }
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("INP-232101", new string[] { exception.ToString() });
            }
        }

        private void TaxRegisterForm_Load(object sender, EventArgs e)
        {
        }
    }
}

