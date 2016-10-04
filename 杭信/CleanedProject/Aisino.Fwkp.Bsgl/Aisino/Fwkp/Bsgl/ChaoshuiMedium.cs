namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core.Controls;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ChaoshuiMedium : DockForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnOK;
        private IContainer components;
        private AisinoGRP groupBox;
        private bool isJinshuipan = true;
        private ILog loger = LogUtil.GetLogger<ChaoshuiForm>();
        private AisinoRDO rbtJinshuipan;
        private AisinoRDO rbtnBaoshuipan;
        private XmlComponentLoader xmlComponentLoader1;

        public ChaoshuiMedium()
        {
            this.Initial();
            this.InitLoad();
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
            this.groupBox = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox");
            this.rbtJinshuipan = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtJinshuipan");
            this.rbtJinshuipan.Checked = true;
            this.rbtnBaoshuipan = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtnBaoshuipan");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.rbtJinshuipan.CheckedChanged += new EventHandler(this.rbtJinshuipan_CheckedChanged);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ChaoshuiMedium));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x152, 0xd5);
            this.xmlComponentLoader1.TabIndex = 1;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Bsgl.ChaoshuiMedium\Aisino.Fwkp.Bsgl.ChaoshuiMedium.xml");
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x152, 0xd5);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.Name = "ChaoshuiMedium";
            this.Text = "抄税介质";
            base.ResumeLayout(false);
        }

        private void InitLoad()
        {
            this.rbtJinshuipan.Checked = true;
            this.isJinshuipan = true;
        }

        private void rbtJinshuipan_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtJinshuipan.Checked)
            {
                this.isJinshuipan = true;
            }
            else
            {
                this.isJinshuipan = false;
            }
        }

        public bool jinshuipan
        {
            get
            {
                return this.isJinshuipan;
            }
            set
            {
                this.isJinshuipan = this.rbtJinshuipan.Checked;
            }
        }
    }
}

