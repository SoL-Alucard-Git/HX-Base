namespace Aisino.FTaxBase
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MessageBoxST : FormBase
    {
        private Button btnCancel;
        private Button btnNo;
        private Button btnOK;
        private Button btnYes;
        private IContainer icontainer_1;
        private Label labelMessageText;
        private MsgBoxInfo msgBoxInfo_0;
        private PictureBox picIcon;

        public MessageBoxST()
        {
            
            this.InitializeComponent_1();
        }

        public MessageBoxST(MsgBoxInfo msgBoxInfo_1)
        {
            
            this.InitializeComponent_1();
            this.msgBoxInfo_0 = msgBoxInfo_1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Dispose();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.No;
            base.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            base.Dispose();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Yes;
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
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.btnYes = new Button();
            this.btnNo = new Button();
            this.picIcon = new PictureBox();
            this.labelMessageText = new Label();
            base.BodyBounds.SuspendLayout();
            base.BodyClient.SuspendLayout();
            ((ISupportInitialize) this.picIcon).BeginInit();
            base.SuspendLayout();
            base.lblTitle.Size = new Size(0x158, 30);
            base.TitleArea.Size = new Size(0x170, 30);
            base.BodyBounds.Size = new Size(0x170, 0x89);
            base.BodyClient.Controls.Add(this.picIcon);
            base.BodyClient.Controls.Add(this.labelMessageText);
            base.BodyClient.Controls.Add(this.btnOK);
            base.BodyClient.Controls.Add(this.btnCancel);
            base.BodyClient.Controls.Add(this.btnNo);
            base.BodyClient.Controls.Add(this.btnYes);
            base.BodyClient.Size = new Size(0x164, 0x83);
            this.btnOK.Location = new Point(0x13, 0x63);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(60, 0x18);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Visible = false;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Location = new Point(0x65, 0x63);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(60, 0x18);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnYes.Location = new Point(0xb7, 0x63);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new Size(60, 0x18);
            this.btnYes.TabIndex = 10;
            this.btnYes.Text = "是";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Visible = false;
            this.btnYes.Click += new EventHandler(this.btnYes_Click);
            this.btnNo.Location = new Point(260, 0x63);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new Size(60, 0x18);
            this.btnNo.TabIndex = 11;
            this.btnNo.Text = "否";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Visible = false;
            this.btnNo.Click += new EventHandler(this.btnNo_Click);
            this.picIcon.BackColor = Color.Transparent;
            this.picIcon.Location = new Point(5, 0x1f);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new Size(0x2a, 40);
            this.picIcon.TabIndex = 15;
            this.picIcon.TabStop = false;
            this.labelMessageText.BackColor = Color.Transparent;
            this.labelMessageText.Font = new Font("微软雅黑", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.labelMessageText.Location = new Point(0x35, 6);
            this.labelMessageText.Name = "labelMessageText";
            this.labelMessageText.Size = new Size(0x128, 0x58);
            this.labelMessageText.TabIndex = 0x10;
            this.labelMessageText.Text = "labelMessage";
            this.labelMessageText.TextAlign = ContentAlignment.MiddleLeft;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.PaleTurquoise;
            base.ClientSize = new Size(0x170, 0xa7);
            base.CloseBtnVisible = true;
            base.Name = "MessageBoxST";
            this.Text = "MessageBox";
            base.TopMost = false;
            base.Load += new EventHandler(this.MessageBoxST_Load);
            base.BodyBounds.ResumeLayout(false);
            base.BodyClient.ResumeLayout(false);
            ((ISupportInitialize) this.picIcon).EndInit();
            base.ResumeLayout(false);
        }

        private void MessageBoxST_Load(object sender, EventArgs e)
        {
            this.labelMessageText.Text = this.msgBoxInfo_0.Text;
            if (this.msgBoxInfo_0.Caption != null)
            {
                base.lblTitle.Text = this.msgBoxInfo_0.Caption;
            }
            switch (this.msgBoxInfo_0.MessageBoxButton)
            {
                case MessageBoxButtons.OK:
                    this.btnOK.Left = (base.Width / 2) - 0x26;
                    this.btnOK.Visible = true;
                    break;

                case MessageBoxButtons.OKCancel:
                    this.btnOK.Left = (base.Width / 2) - 0x4c;
                    this.btnOK.Visible = true;
                    this.btnCancel.Left = base.Width / 2;
                    this.btnCancel.Visible = true;
                    break;

                case MessageBoxButtons.YesNoCancel:
                {
                    int num = this.btnYes.Width / 2;
                    int num2 = base.Width / 2;
                    this.btnYes.Left = ((num2 - num) - this.btnYes.Width) - 10;
                    this.btnYes.Visible = true;
                    this.btnNo.Left = num2 - num;
                    this.btnNo.Visible = true;
                    this.btnCancel.Left = ((num2 - num) + this.btnYes.Width) + 10;
                    this.btnCancel.Visible = true;
                    break;
                }
                case MessageBoxButtons.YesNo:
                    this.btnYes.Left = (base.Width / 2) - 0x4c;
                    this.btnYes.Visible = true;
                    this.btnNo.Left = base.Width / 2;
                    this.btnNo.Visible = true;
                    break;
            }
            MessageBoxIcon messageBoxIcon = this.msgBoxInfo_0.MessageBoxIcon;
            if (messageBoxIcon <= MessageBoxIcon.Question)
            {
                if ((messageBoxIcon == MessageBoxIcon.Hand) || (messageBoxIcon == MessageBoxIcon.Question))
                {
                }
            }
            else if (messageBoxIcon == MessageBoxIcon.Exclamation)
            {
            }
        }
    }
}

