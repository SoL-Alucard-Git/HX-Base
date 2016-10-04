namespace Aisino.Fwkp.Yccb
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class PzxzForm : DockForm
    {
        private IContainer components;
        private AisinoLBL lblHyT;
        private AisinoLBL lblJdcT;
        private AisinoLBL lblZpT;
        private ILog loger = LogUtil.GetLogger<PzxzForm>();
        private AisinoPNL pnlHyErr;
        private AisinoPNL pnlHyInfo;
        private AisinoPNL pnlJdcErr;
        private AisinoPNL pnlJdcInfo;
        private AisinoPNL pnlZpErr;
        private AisinoPNL pnlZpInfo;
        private RemoteReport remote = new RemoteReport();
        private AisinoRTX rtbHy;
        private AisinoRTX rtbJdc;
        private AisinoRTX rtbZp;
        private TabControlEx tabControl;
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();
        private TabPageEx tbHy;
        private TabPageEx tbJdc;
        private TabPageEx tbZp;
        private XmlComponentLoader xmlComponentLoader1;

        public PzxzForm(string type)
        {
            this.Initial();
            if (type == "00")
            {
                this.Text = "上报汇总";
            }
            else
            {
                this.Text = "结果查询";
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

        private void Initial()
        {
            this.InitializeComponent();
            base.MinimizeBox = false;
            base.MaximizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.tbZp = this.xmlComponentLoader1.GetControlByName<TabPageEx>("tbZp");
            this.tbHy = this.xmlComponentLoader1.GetControlByName<TabPageEx>("tbHy");
            this.tbJdc = this.xmlComponentLoader1.GetControlByName<TabPageEx>("tbJdc");
            this.lblZpT = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblZpT");
            this.lblHyT = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblHyT");
            this.lblJdcT = this.xmlComponentLoader1.GetControlByName<AisinoLBL>("lblJdcT");
            this.pnlZpErr = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("pnlZpErr");
            this.pnlHyErr = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("pnlHyErr");
            this.pnlJdcErr = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("pnlJdcErr");
            this.pnlZpInfo = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("pnlZpInfo");
            this.pnlHyInfo = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("pnlHyInfo");
            this.pnlJdcInfo = this.xmlComponentLoader1.GetControlByName<AisinoPNL>("pnlJdcInfo");
            this.rtbZp = this.xmlComponentLoader1.GetControlByName<AisinoRTX>("rtbZp");
            this.rtbHy = this.xmlComponentLoader1.GetControlByName<AisinoRTX>("rtbHy");
            this.rtbJdc = this.xmlComponentLoader1.GetControlByName<AisinoRTX>("rtbJdc");
            base.Load += new EventHandler(this.PzxzForm_Load);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(PzxzForm));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x260, 0x17d);
            this.xmlComponentLoader1.TabIndex = 0;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Yccb.SelectFpzlForm\Aisino.Fwkp.Yccb.SelectFpzlForm.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x260, 0x17d);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.Name = "PzxzForm";
            this.Text = "YccbForm";
            base.ResumeLayout(false);
        }

        private void PzxzForm_Load(object sender, EventArgs e)
        {
        }
    }
}

