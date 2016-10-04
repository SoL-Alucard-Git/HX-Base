namespace Aisino.Fwkp.Fplygl.Form.FJFPGL_2
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ChooseRevokeMedium : DockForm
    {
        protected AisinoBTN btnCancel;
        protected AisinoBTN btnOK;
        private IContainer components;
        protected AisinoGRP groupBox;
        protected bool isBaoshuipan;
        protected bool isHYZP;
        protected bool isJDC;
        protected bool isJinshuipan = true;
        protected bool isZZZP = true;
        protected AisinoRDO rbtHYZP;
        protected AisinoRDO rbtJDC;
        protected AisinoRDO rbtJinshuipan;
        protected AisinoRDO rbtnBaoshuipan;
        protected AisinoRDO rbtZZZP;
        protected XmlComponentLoader xmlComponentLoader1;

        public ChooseRevokeMedium()
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
                return "3";
            }
            return "1";
        }

        public virtual int GetInvType()
        {
            if (!this.isZZZP)
            {
                if (this.isHYZP)
                {
                    return 11;
                }
                if (this.isJDC)
                {
                    return 12;
                }
            }
            return 0;
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
            this.rbtZZZP = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtZZZP");
            this.rbtJDC = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtJDC");
            this.rbtHYZP = this.xmlComponentLoader1.GetControlByName<AisinoRDO>("rbtHYZP");
            this.btnOK = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnOK");
            this.btnCancel = this.xmlComponentLoader1.GetControlByName<AisinoBTN>("btnCancel");
            this.rbtZZZP.Text = "增值税专用发票、普通发票";
            this.rbtJDC.Text = "机动车销售统一发票、电子增值税普通发票及增值税普通发票(卷票)";
            this.rbtJinshuipan.Checked = true;
            this.isJinshuipan = true;
            this.rbtnBaoshuipan.Checked = false;
            this.isBaoshuipan = false;
            this.rbtZZZP.Checked = true;
            this.isZZZP = true;
            this.rbtJDC.Checked = false;
            this.isJDC = false;
            this.rbtHYZP.Checked = false;
            this.isHYZP = false;
            this.rbtJinshuipan.CheckedChanged += new EventHandler(this.rbtJinshuipan_CheckedChanged);
            this.rbtnBaoshuipan.CheckedChanged += new EventHandler(this.rbtnBaoshuipan_CheckedChanged);
            this.rbtZZZP.CheckedChanged += new EventHandler(this.rbtZZZP_CheckedChanged);
            this.rbtJDC.CheckedChanged += new EventHandler(this.rbtJDC_CheckedChanged);
            this.rbtHYZP.CheckedChanged += new EventHandler(this.rbtHYZP_CheckedChanged);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ChooseRevokeMedium));
            this.xmlComponentLoader1 = new XmlComponentLoader();
            base.SuspendLayout();
            this.xmlComponentLoader1.BackColor = Color.Transparent;
            this.xmlComponentLoader1.Dock = DockStyle.Fill;
            this.xmlComponentLoader1.Location = new Point(0, 0);
            this.xmlComponentLoader1.Name = "xmlComponentLoader1";
            this.xmlComponentLoader1.Size = new Size(0x157, 0x19c);
            this.xmlComponentLoader1.TabIndex = 2;
            this.xmlComponentLoader1.set_XMLPath(@"..\Config\Components\Aisino.Fwkp.Fplygl.Forms.ChooseMediumInvtype\Aisino.Fwkp.Fplygl.Forms.ChooseMediumInvtype.xml");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x157, 0x19c);
            base.Controls.Add(this.xmlComponentLoader1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "ChooseRevokeMedium";
            base.set_TabText("ChooseRevokeMedium");
            this.Text = "收回新购发票介质选择";
            base.ResumeLayout(false);
        }

        private void rbtHYZP_CheckedChanged(object sender, EventArgs e)
        {
            this.isHYZP = this.rbtHYZP.Checked;
        }

        private void rbtJDC_CheckedChanged(object sender, EventArgs e)
        {
            this.isJDC = this.rbtJDC.Checked;
        }

        private void rbtJinshuipan_CheckedChanged(object sender, EventArgs e)
        {
            this.isJinshuipan = this.rbtJinshuipan.Checked;
        }

        private void rbtnBaoshuipan_CheckedChanged(object sender, EventArgs e)
        {
            this.isBaoshuipan = this.rbtnBaoshuipan.Checked;
        }

        private void rbtZZZP_CheckedChanged(object sender, EventArgs e)
        {
            this.isZZZP = this.rbtZZZP.Checked;
        }
    }
}

