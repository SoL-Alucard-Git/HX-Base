namespace Aisino.Framework.Plugin.Core
{
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FormSplash : MessageBaseForm
    {
        private IContainer icontainer_1;
        private Label lblMsg;
        private Panel panel1;
        private PictureBox pictureBox1;
        private Label status;

        public FormSplash()
        {
            
            this.InitializeComponent_1();
            base.Load += new EventHandler(this.FormSplash_Load);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_1 != null))
            {
                this.icontainer_1.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FormSplash_Load(object sender, EventArgs e)
        {
            try
            {
                string str = PropertyUtil.GetValue("MAIN_CODE", string.Empty);
                if (!string.Empty.Equals(str))
                {
                    base.lblTitle.Text = ProductName + str;
                }
            }
            catch (Exception)
            {
            }
        }

        private void InitializeComponent_1()
        {
            this.pictureBox1 = new PictureBox();
            this.lblMsg = new Label();
            this.panel1 = new Panel();
            this.status = new Label();
            base.pnlForm.SuspendLayout();
            base.TitleArea.SuspendLayout();
            base.BodyBounds.SuspendLayout();
            base.BodyClient.SuspendLayout();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            base.pnlForm.BackColor = Color.Transparent;
            base.pnlForm.Size = new Size(500, 350);
            base.TitleArea.Size = new Size(500, 0x2d);
            base.pnlCaption.Size = new Size(500, 0x2d);
            base.lblTitle.Size = new Size(470, 0x2d);
            base.lblTitle.Text = "税控发票开票软件（金税盘版） ";
            base.BodyBounds.Location = new Point(0, 0x2d);
            base.BodyBounds.Size = new Size(500, 0x131);
            base.BodyClient.Controls.Add(this.panel1);
            base.BodyClient.Controls.Add(this.lblMsg);
            base.BodyClient.Controls.Add(this.pictureBox1);
            base.BodyClient.Size = new Size(0x1e8, 0x12b);
            this.pictureBox1.BackColor = Color.Transparent;
            this.pictureBox1.Image = Class131.smethod_46();
            this.pictureBox1.Location = new Point(0x69, 0x57);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x30, 0x30);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0x10;
            this.pictureBox1.TabStop = false;
            this.lblMsg.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblMsg.Font = new Font("黑体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lblMsg.ForeColor = Color.Black;
            this.lblMsg.Location = new Point(0xc0, 0x40);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new Size(0x8b, 90);
            this.lblMsg.TabIndex = 0x11;
            this.lblMsg.Text = "正在登录...";
            this.lblMsg.TextAlign = ContentAlignment.MiddleLeft;
            this.panel1.BackColor = Color.FromArgb(0xe4, 0xe7, 0xe9);
            this.panel1.Controls.Add(this.status);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0xdd);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1e8, 0x4e);
            this.panel1.TabIndex = 0x13;
            this.status.BackColor = Color.Transparent;
            this.status.Cursor = Cursors.Default;
            this.status.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.status.ForeColor = Color.Black;
            this.status.ImageAlign = ContentAlignment.BottomLeft;
            this.status.Location = new Point(0x17, 9);
            this.status.Name = "status";
            this.status.Size = new Size(0x1b1, 0x38);
            this.status.TabIndex = 0x12;
            this.status.Text = "正在启动...";
            this.status.TextAlign = ContentAlignment.MiddleLeft;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(500, 350);
            base.Name = "FormSplash";
            this.Text = "FormSplash";
            base.pnlForm.ResumeLayout(false);
            base.TitleArea.ResumeLayout(false);
            base.BodyBounds.ResumeLayout(false);
            base.BodyClient.ResumeLayout(false);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public string Message
        {
            get
            {
                return this.status.Text;
            }
            set
            {
                this.status.Text = value;
            }
        }
    }
}

