namespace Aisino.Framework.Plugin.Core.MessageDlg
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SysMessageBox : MessageBaseForm
    {
        private AisinoBTN btnCancel;
        private AisinoBTN btnNo;
        private AisinoBTN btnYes;
        private IContainer icontainer_1;
        private ImageList imageList_0;
        private Label lblMsg;
        private AisinoPIC picEventIco;
        private Panel pnlBottom;
        private Panel pnlMid;

        public SysMessageBox(string string_0, string string_1, MessageBoxButtons messageBoxButtons_0, MessageBoxIcon messageBoxIcon_0, MessageBoxDefaultButton messageBoxDefaultButton_0)
        {
            
            this.InitializeComponent_1();
            base.lblTitle.Text = string_0;
            int num2 = this.method_1(string_1) * 20;
            if (num2 < 100)
            {
                num2 = 100;
            }
            base.Height = num2 + 100;
            this.lblMsg.Text = string_1.Replace(@"\n", Environment.NewLine);
            this.method_2(messageBoxButtons_0);
            this.method_3(messageBoxIcon_0);
            this.method_4(messageBoxDefaultButton_0);
            this.pnlMid.SizeChanged += new EventHandler(this.pnlMid_SizeChanged);
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            base.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_1 != null))
            {
                this.icontainer_1.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent_1()
        {
            this.icontainer_1 = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SysMessageBox));
            this.imageList_0 = new ImageList(this.icontainer_1);
            this.pnlBottom = new Panel();
            this.btnNo = new AisinoBTN();
            this.btnCancel = new AisinoBTN();
            this.btnYes = new AisinoBTN();
            this.pnlMid = new Panel();
            this.lblMsg = new Label();
            this.picEventIco = new AisinoPIC();
            base.pnlForm.SuspendLayout();
            base.TitleArea.SuspendLayout();
            base.BodyBounds.SuspendLayout();
            base.BodyClient.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlMid.SuspendLayout();
            ((ISupportInitialize) this.picEventIco).BeginInit();
            base.SuspendLayout();
            base.pnlForm.Size = new Size(400, 200);
            base.BodyBounds.Size = new Size(400, 170);
            base.BodyClient.Controls.Add(this.pnlMid);
            base.BodyClient.Controls.Add(this.pnlBottom);
            base.BodyClient.Size = new Size(0x184, 0xa4);
            this.imageList_0.ImageStream = (ImageListStreamer) manager.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.imageList_0.Images.SetKeyName(0, "i.png");
            this.imageList_0.Images.SetKeyName(1, "2.png");
            this.imageList_0.Images.SetKeyName(2, "3.png");
            this.imageList_0.Images.SetKeyName(3, "4.png");
            this.pnlBottom.BackColor = Color.FromArgb(0xe4, 0xe7, 0xe9);
            this.pnlBottom.Controls.Add(this.btnNo);
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnYes);
            this.pnlBottom.Dock = DockStyle.Bottom;
            this.pnlBottom.Location = new Point(0, 0x72);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new Size(0x184, 50);
            this.pnlBottom.TabIndex = 8;
            this.btnNo.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnNo.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnNo.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnNo.DialogResult = DialogResult.No;
            this.btnNo.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold);
            this.btnNo.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnNo.ForeColor = Color.White;
            this.btnNo.ImageKey = "(无)";
            this.btnNo.Location = new Point(0xda, 9);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new Size(0x4b, 30);
            this.btnNo.TabIndex = 0x11;
            this.btnNo.Text = "否";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new EventHandler(this.btnYes_Click);
            this.btnCancel.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnCancel.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnCancel.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold);
            this.btnCancel.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.ImageKey = "(无)";
            this.btnCancel.Location = new Point(0x12b, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 30);
            this.btnCancel.TabIndex = 0x10;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnYes_Click);
            this.btnYes.BackColorActive = Color.FromArgb(0x19, 0x76, 210);
            this.btnYes.ColorDefaultA = Color.FromArgb(0, 0xac, 0xfb);
            this.btnYes.ColorDefaultB = Color.FromArgb(0, 0x91, 0xe0);
            this.btnYes.DialogResult = DialogResult.OK;
            this.btnYes.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold);
            this.btnYes.FontColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.btnYes.ForeColor = Color.White;
            this.btnYes.ImageKey = "(无)";
            this.btnYes.Location = new Point(0x89, 9);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new Size(0x4b, 30);
            this.btnYes.TabIndex = 15;
            this.btnYes.Text = "是";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new EventHandler(this.btnYes_Click);
            this.pnlMid.BackColor = Color.Transparent;
            this.pnlMid.Controls.Add(this.lblMsg);
            this.pnlMid.Controls.Add(this.picEventIco);
            this.pnlMid.Dock = DockStyle.Fill;
            this.pnlMid.Location = new Point(0, 0);
            this.pnlMid.Name = "pnlMid";
            this.pnlMid.Size = new Size(0x184, 0x72);
            this.pnlMid.TabIndex = 9;
            this.lblMsg.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.lblMsg.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lblMsg.Location = new Point(60, 4);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new Size(0x142, 0x6a);
            this.lblMsg.TabIndex = 3;
            this.lblMsg.Text = "无退票信息，请直接到税务机关购买新发票，金税设备没有可用发票。";
            this.lblMsg.TextAlign = ContentAlignment.MiddleLeft;
            this.picEventIco.Anchor = AnchorStyles.Left;
            this.picEventIco.Location = new Point(15, 0x29);
            this.picEventIco.Name = "picEventIco";
            this.picEventIco.Size = new Size(0x20, 0x20);
            this.picEventIco.TabIndex = 2;
            this.picEventIco.TabStop = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(400, 200);
            base.Name = "SysMessageBox";
            this.Text = "SysMessageBox";
            base.TopMost = true;
            base.pnlForm.ResumeLayout(false);
            base.TitleArea.ResumeLayout(false);
            base.BodyBounds.ResumeLayout(false);
            base.BodyClient.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.pnlMid.ResumeLayout(false);
            ((ISupportInitialize) this.picEventIco).EndInit();
            base.ResumeLayout(false);
        }

        private int method_1(string string_0)
        {
            double length = 0.0;
            string[] strArray = string_0.Split(new char[] { '\n' });
            length = strArray.Length;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i] != null)
                {
                    length += ((double) strArray[i].Length) / 22.0;
                }
            }
            return (int) (length + 1.0);
        }

        private void method_2(MessageBoxButtons messageBoxButtons_0)
        {
            switch (messageBoxButtons_0)
            {
                case MessageBoxButtons.OK:
                    this.btnYes.Text = "确认";
                    this.btnYes.DialogResult = DialogResult.OK;
                    this.btnYes.Location = this.btnCancel.Location;
                    this.btnNo.Visible = false;
                    this.btnCancel.Visible = false;
                    return;

                case MessageBoxButtons.OKCancel:
                    this.btnYes.Text = "确认";
                    this.btnYes.DialogResult = DialogResult.OK;
                    this.btnNo.Visible = false;
                    this.btnCancel.Text = "取消";
                    this.btnCancel.DialogResult = DialogResult.Cancel;
                    this.btnYes.Location = this.btnNo.Location;
                    return;

                case MessageBoxButtons.AbortRetryIgnore:
                    this.btnYes.Text = "终止";
                    this.btnYes.DialogResult = DialogResult.Abort;
                    this.btnNo.Text = "重试";
                    this.btnNo.DialogResult = DialogResult.Retry;
                    this.btnCancel.Text = "忽略";
                    this.btnCancel.DialogResult = DialogResult.Ignore;
                    return;

                case MessageBoxButtons.YesNoCancel:
                    this.btnYes.Text = "是";
                    this.btnYes.DialogResult = DialogResult.Yes;
                    this.btnNo.Text = "否";
                    this.btnNo.DialogResult = DialogResult.No;
                    this.btnCancel.Text = "取消";
                    this.btnCancel.DialogResult = DialogResult.Cancel;
                    return;

                case MessageBoxButtons.YesNo:
                    this.btnYes.Text = "是";
                    this.btnYes.DialogResult = DialogResult.Yes;
                    this.btnNo.Text = "否";
                    this.btnNo.DialogResult = DialogResult.No;
                    this.btnYes.Location = this.btnNo.Location;
                    this.btnNo.Location = this.btnCancel.Location;
                    this.btnCancel.Visible = false;
                    return;

                case MessageBoxButtons.RetryCancel:
                    this.btnNo.Text = "重试";
                    this.btnNo.DialogResult = DialogResult.Retry;
                    this.btnCancel.Text = "取消";
                    this.btnCancel.DialogResult = DialogResult.Cancel;
                    this.btnYes.Visible = false;
                    return;
            }
        }

        private void method_3(MessageBoxIcon messageBoxIcon_0)
        {
            MessageBoxIcon icon = messageBoxIcon_0;
            if (icon <= MessageBoxIcon.Hand)
            {
                switch (icon)
                {
                    case MessageBoxIcon.None:
                        this.picEventIco.Image = null;
                        break;

                    case MessageBoxIcon.Hand:
                        this.picEventIco.Image = this.imageList_0.Images[2];
                        break;
                }
            }
            else
            {
                switch (icon)
                {
                    case MessageBoxIcon.Question:
                        this.picEventIco.Image = this.imageList_0.Images[3];
                        return;

                    case MessageBoxIcon.Exclamation:
                        this.picEventIco.Image = this.imageList_0.Images[1];
                        return;
                }
                if (icon == MessageBoxIcon.Asterisk)
                {
                    this.picEventIco.Image = this.imageList_0.Images[0];
                }
            }
        }

        private void method_4(MessageBoxDefaultButton messageBoxDefaultButton_0)
        {
            if (messageBoxDefaultButton_0 == MessageBoxDefaultButton.Button1)
            {
                if (this.btnYes.Visible)
                {
                    this.btnYes.Focus();
                }
                else if (this.btnNo.Visible)
                {
                    this.btnNo.Focus();
                }
                else if (this.btnCancel.Visible)
                {
                    this.btnCancel.Focus();
                }
            }
            if (messageBoxDefaultButton_0 == MessageBoxDefaultButton.Button2)
            {
                if (this.btnYes.Visible)
                {
                    if (this.btnNo.Visible)
                    {
                        this.btnNo.Focus();
                    }
                    else if (this.btnCancel.Visible)
                    {
                        this.btnCancel.Focus();
                    }
                    else
                    {
                        this.btnYes.Focus();
                    }
                }
                else if (this.btnNo.Visible)
                {
                    if (this.btnCancel.Visible)
                    {
                        this.btnCancel.Focus();
                    }
                    else
                    {
                        this.btnNo.Focus();
                    }
                }
                else if (this.btnCancel.Visible)
                {
                    this.btnCancel.Focus();
                }
            }
            if (messageBoxDefaultButton_0 == MessageBoxDefaultButton.Button3)
            {
                if (this.btnCancel.Visible)
                {
                    this.btnCancel.Focus();
                }
                else if (this.btnNo.Visible)
                {
                    this.btnNo.Focus();
                }
                else if (this.btnYes.Visible)
                {
                    this.btnYes.Focus();
                }
            }
        }

        private void pnlMid_SizeChanged(object sender, EventArgs e)
        {
            int y = (this.pnlMid.Height - this.picEventIco.Height) / 2;
            this.picEventIco.Location = new Point(this.picEventIco.Location.X, y);
        }
    }
}

