namespace Aisino.Fwkp.Fplygl.Form.FPLYGL_1
{
    using Aisino.Fwkp.Fplygl.Form.AbsForms;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class InvInfoReadMsg : InvInfoMsg
    {
        private IContainer components;

        public InvInfoReadMsg()
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
            base.lblMsg.Text = "从金税设备读入新购发票完毕，发票卷信息如下：";
            base.lblMsg.Location = new Point((base.Width / 2) - (base.lblMsg.Width / 2), base.lblMsg.Location.Y);
            this.Text = "从金税设备读入新购发票";
            base.btnExecute.Visible = false;
            base.btnCancel.Visible = false;
        }
    }
}

