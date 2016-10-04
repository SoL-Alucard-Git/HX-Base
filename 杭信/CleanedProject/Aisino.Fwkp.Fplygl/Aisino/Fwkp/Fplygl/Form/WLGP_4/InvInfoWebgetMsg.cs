namespace Aisino.Fwkp.Fplygl.Form.WLGP_4
{
    using Aisino.Fwkp.Fplygl.Form.AbsForms;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class InvInfoWebgetMsg : InvInfoMsg
    {
        private IContainer components;

        public InvInfoWebgetMsg()
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
        }

        protected override void ValidationNTxt()
        {
            base.lblMsg.Text = "从网络新购发票完毕，发票卷信息如下：";
            base.lblMsg.Location = new Point((base.Width / 2) - (base.lblMsg.Width / 2), base.lblMsg.Location.Y);
            this.Text = "从网络读入新购发票";
            base.btnExecute.Visible = false;
            base.btnCancel.Visible = false;
            base.LGRQ.Visible = false;
        }
    }
}

