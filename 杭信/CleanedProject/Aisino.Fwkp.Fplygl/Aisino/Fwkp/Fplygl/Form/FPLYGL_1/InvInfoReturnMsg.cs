namespace Aisino.Fwkp.Fplygl.Form.FPLYGL_1
{
    using Aisino.Fwkp.Fplygl.Form.AbsForms;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class InvInfoReturnMsg : InvInfoMsg
    {
        private IContainer components;

        public InvInfoReturnMsg()
        {
            this.Initialize();
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
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.Font;
            base.xmlComponentLoader1.Size = new Size(800, 0x8b);
            base.ClientSize = new Size(800, 0x8b);
        }

        protected override void ValidationNTxt()
        {
            base.lblMsg.Text = "本次退回发票卷信息如下，请确认是否退回。";
            base.lblMsg.Location = new Point((base.Width / 2) - (base.lblMsg.Width / 2), base.lblMsg.Location.Y);
            this.Text = "确认将发票卷退回金税设备";
            base.btnOK.Visible = false;
        }
    }
}

