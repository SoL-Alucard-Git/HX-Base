namespace Aisino.Fwkp.Fplygl.Form.AbsForms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ChooseMedium : DockForm
    {
        protected AisinoBTN btnCancel;
        protected AisinoBTN btnOK;
        private IContainer components;
        protected AisinoGRP groupBox;
        protected bool isBaoshuipan;
        protected bool isJinshuipan = true;
        protected AisinoRDO rbtJinshuipan;
        protected AisinoRDO rbtnBaoshuipan;
        protected XmlComponentLoader xmlComponentLoader1;

        public ChooseMedium()
        {
            this.Initial();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public virtual string GetDeviceType()
        {
            if (!this.isJinshuipan && this.isBaoshuipan)
            {
                return "2";
            }
            return "1";
        }

        private void Initial()
        {
            this.InitializeComponent();
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.ShowInTaskbar = true;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.groupBox = this.xmlComponentLoader1.GetControlByName<AisinoGRP>("groupBox");
            this.rbtJinshuipan = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtJinshuipan");
            this.rbtnBaoshuipan = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtnBaoshuipan");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.rbtJinshuipan.Checked = true;
            this.isJinshuipan = true;
            this.rbtnBaoshuipan.Checked = false;
            this.isBaoshuipan = false;
            this.rbtJinshuipan.CheckedChanged += new EventHandler(this.rbtJinshuipan_CheckedChanged);
            this.rbtnBaoshuipan.CheckedChanged += new EventHandler(this.rbtnBaoshuipan_CheckedChanged);
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(ChooseMedium));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x145, 0xd5);
            this.xmlComponentLoader1.TabIndex = 1;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.ChooseMedium\Aisino.Fwkp.Fplygl.Forms.ChooseMedium.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x145, 0xd5);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Location = new Point(0, 0);
            base.Name = "ChooseMedium";
            this.Text = "介质选择";
            base.ResumeLayout(false);
        }

        private void rbtJinshuipan_CheckedChanged(object sender, EventArgs e)
        {
            this.isJinshuipan = this.rbtJinshuipan.Checked;
        }

        private void rbtnBaoshuipan_CheckedChanged(object sender, EventArgs e)
        {
            this.isBaoshuipan = this.rbtnBaoshuipan.Checked;
        }
    }
}

