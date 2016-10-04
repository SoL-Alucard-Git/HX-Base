namespace Aisino.Fwkp.Fplygl.Form.WSFTP_6
{
    using Aisino.Fwkp.Fplygl.Form.AbsForms;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class InvInfoWebAllocMsg : InvInfoMsg
    {
        private IContainer components;

        public InvInfoWebAllocMsg()
        {
            base.lblMsg.Text = "向分开票机分配发票完毕，发票卷信息如下：";
            base.lblMsg.Location = new Point((base.Width / 2) - (base.lblMsg.Width / 2), base.lblMsg.Location.Y);
            this.Text = "向分开票机分配发票";
            base.btnExecute.Visible = false;
            base.btnCancel.Visible = false;
            base.dgInvInfo.Columns[6].Visible = false;
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
            this.Text = "InvInfoWebAllocMsg";
        }
    }
}

