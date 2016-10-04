namespace Aisino.FTaxBase
{
    using Aisino.FTaxBase.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    public class FormBase : Form
    {
        protected internal Panel BodyBounds;
        protected internal Panel BodyClient;
        private bool bool_0;
        private IContainer icontainer_0;
        protected internal Label lblTitle;
        private PictureBox picClose;
        private PictureBox picLogo;
        private Panel pnlCaption;
        private Panel pnlLogo;
        private Panel pnlTitle;
        private Point point_0;
        protected internal Panel TitleArea;

        public FormBase()
        {
            
            this.InitializeComponent();
            this.picClose.Image = this.method_0(0, 0);
            this.picLogo.BackgroundImage = Resources.smethod_8().ToBitmap();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.TitleArea = new Panel();
            this.pnlCaption = new Panel();
            this.pnlTitle = new Panel();
            this.picClose = new PictureBox();
            this.lblTitle = new Label();
            this.pnlLogo = new Panel();
            this.picLogo = new PictureBox();
            this.BodyBounds = new Panel();
            this.BodyClient = new Panel();
            this.TitleArea.SuspendLayout();
            this.pnlCaption.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            ((ISupportInitialize) this.picClose).BeginInit();
            this.pnlLogo.SuspendLayout();
            ((ISupportInitialize) this.picLogo).BeginInit();
            this.BodyBounds.SuspendLayout();
            base.SuspendLayout();
            this.TitleArea.Controls.Add(this.pnlCaption);
            this.TitleArea.Dock = DockStyle.Top;
            this.TitleArea.Location = new Point(0, 0);
            this.TitleArea.Name = "TitleArea";
            this.TitleArea.Size = new Size(0x180, 30);
            this.TitleArea.TabIndex = 0;
            this.pnlCaption.BackColor = Color.FromArgb(0x33, 0x57, 0x74);
            this.pnlCaption.Controls.Add(this.pnlTitle);
            this.pnlCaption.Controls.Add(this.pnlLogo);
            this.pnlCaption.Dock = DockStyle.Top;
            this.pnlCaption.Location = new Point(0, 0);
            this.pnlCaption.Name = "pnlCaption";
            this.pnlCaption.Size = new Size(0x180, 30);
            this.pnlCaption.TabIndex = 7;
            this.pnlTitle.BackColor = Color.Transparent;
            this.pnlTitle.Controls.Add(this.picClose);
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = DockStyle.Fill;
            this.pnlTitle.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.pnlTitle.ForeColor = Color.White;
            this.pnlTitle.Location = new Point(0x18, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new Size(360, 30);
            this.pnlTitle.TabIndex = 5;
            this.picClose.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.picClose.Location = new Point(0x13e, 7);
            this.picClose.Name = "picClose";
            this.picClose.Size = new Size(0x26, 0x12);
            this.picClose.TabIndex = 1;
            this.picClose.TabStop = false;
            this.picClose.MouseDown += new MouseEventHandler(this.picClose_MouseDown);
            this.picClose.MouseEnter += new EventHandler(this.picClose_MouseEnter);
            this.picClose.MouseLeave += new EventHandler(this.picClose_MouseLeave);
            this.picClose.MouseHover += new EventHandler(this.picClose_MouseHover);
            this.picClose.MouseUp += new MouseEventHandler(this.picClose_MouseUp);
            this.lblTitle.Dock = DockStyle.Fill;
            this.lblTitle.Font = new Font("微软雅黑", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.lblTitle.Location = new Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(360, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "信息提示";
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            this.lblTitle.MouseDown += new MouseEventHandler(this.lblTitle_MouseDown);
            this.lblTitle.MouseMove += new MouseEventHandler(this.lblTitle_MouseMove);
            this.lblTitle.MouseUp += new MouseEventHandler(this.lblTitle_MouseUp);
            this.pnlLogo.BackColor = Color.Transparent;
            this.pnlLogo.Controls.Add(this.picLogo);
            this.pnlLogo.Dock = DockStyle.Left;
            this.pnlLogo.Location = new Point(0, 0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new Size(0x18, 30);
            this.pnlLogo.TabIndex = 6;
            this.picLogo.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.picLogo.BackgroundImageLayout = ImageLayout.Zoom;
            this.picLogo.Location = new Point(3, 6);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new Size(0x12, 0x12);
            this.picLogo.TabIndex = 2;
            this.picLogo.TabStop = false;
            this.BodyBounds.BackColor = Color.FromArgb(0x33, 0x57, 0x74);
            this.BodyBounds.Controls.Add(this.BodyClient);
            this.BodyBounds.Dock = DockStyle.Fill;
            this.BodyBounds.Location = new Point(0, 30);
            this.BodyBounds.Name = "BodyBounds";
            this.BodyBounds.Padding = new Padding(6, 0, 6, 6);
            this.BodyBounds.Size = new Size(0x180, 0xe7);
            this.BodyBounds.TabIndex = 2;
            this.BodyClient.BackColor = Color.White;
            this.BodyClient.Dock = DockStyle.Fill;
            this.BodyClient.Location = new Point(6, 0);
            this.BodyClient.Name = "BodyClient";
            this.BodyClient.Size = new Size(0x174, 0xe1);
            this.BodyClient.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            base.ClientSize = new Size(0x180, 0x105);
            base.Controls.Add(this.BodyBounds);
            base.Controls.Add(this.TitleArea);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "FormBase";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "MessageBaseForm";
            this.TitleArea.ResumeLayout(false);
            this.pnlCaption.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            ((ISupportInitialize) this.picClose).EndInit();
            this.pnlLogo.ResumeLayout(false);
            ((ISupportInitialize) this.picLogo).EndInit();
            this.BodyBounds.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.point_0 = new Point(-(e.X + this.pnlLogo.Width), -e.Y);
                this.bool_0 = true;
            }
        }

        private void lblTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.bool_0)
            {
                Point mousePosition = Control.MousePosition;
                mousePosition.Offset(this.point_0.X, this.point_0.Y);
                base.Location = mousePosition;
            }
        }

        private void lblTitle_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
            }
        }

        private Bitmap method_0(int int_0, int int_1)
        {
            Bitmap bitmap;
            Rectangle rect = new Rectangle(int_1 * 0x19, 0, 0x19, 0x12);
            switch (int_0)
            {
                case 0:
                    bitmap = Resources.smethod_4();
                    rect = new Rectangle(int_1 * 0x26, 0, 0x26, 0x12);
                    break;

                case 1:
                    bitmap = Resources.smethod_5();
                    break;

                case 2:
                    bitmap = Resources.smethod_6();
                    break;

                case 3:
                    bitmap = Resources.smethod_7();
                    break;

                default:
                    bitmap = Resources.smethod_4();
                    break;
            }
            return bitmap.Clone(rect, PixelFormat.Format64bppPArgb);
        }

        private void picClose_MouseDown(object sender, MouseEventArgs e)
        {
            this.picClose.Image = this.method_0(0, 2);
        }

        private void picClose_MouseEnter(object sender, EventArgs e)
        {
            this.picClose.Image = this.method_0(0, 1);
        }

        private void picClose_MouseHover(object sender, EventArgs e)
        {
            this.picClose.Image = this.method_0(0, 1);
        }

        private void picClose_MouseLeave(object sender, EventArgs e)
        {
            this.picClose.Image = this.method_0(0, 0);
        }

        private void picClose_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                base.Close();
            }
        }

        public bool CloseBtnVisible
        {
            get
            {
                return this.picClose.Visible;
            }
            set
            {
                this.picClose.Visible = value;
            }
        }
    }
}

