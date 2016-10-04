namespace Aisino.Fwkp.Fplygl.Form.WSFTP_6
{
    using Aisino.Fwkp.Fplygl.Form.AbsForms;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class InvInfoAllocDownloadConfirmMsg : InvInfoMsg
    {
        private IContainer components;

        public InvInfoAllocDownloadConfirmMsg()
        {
            this.Initialize();
            base.lblMsg.Text = "请与您要获取的纸质发票比对，确认无误后进行下载，发票卷信息如下：";
            base.lblMsg.Location = new Point((base.Width / 2) - (base.lblMsg.Width / 2), base.lblMsg.Location.Y);
            this.Text = "核对待获取的发票";
            base.btnOK.Visible = false;
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
            this.Text = "InvInfoAllocDownloadConfirmMsg";
        }
    }
}

