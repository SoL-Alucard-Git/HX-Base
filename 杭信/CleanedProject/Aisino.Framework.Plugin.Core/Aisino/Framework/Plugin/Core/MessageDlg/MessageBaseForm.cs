namespace Aisino.Framework.Plugin.Core.MessageDlg
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    public class MessageBaseForm : Form
    {
        protected bool _IsCloseVisble;
        protected bool _IsMoveFlag;
        protected internal Panel BodyBounds;
        protected internal Panel BodyClient;
        private bool bool_0;
        private IContainer icontainer_0;
        protected internal Label lblTitle;
        private PictureBox picClose;
        private PictureBox picLogo;
        protected internal Panel pnlCaption;
        protected internal Panel pnlForm;
        private Panel pnlLogo;
        private Panel pnlTitle;
        private Point point_0;
        protected internal Panel TitleArea;

        public MessageBaseForm()
        {
            
            this._IsMoveFlag = true;
            this._IsCloseVisble = true;
            this.InitializeComponent();
            this.picClose.Image = this.method_0(0, 0);
            this.picLogo.BackgroundImage = Class131.smethod_16().ToBitmap();
            this.pnlTitle.SizeChanged += new EventHandler(this.pnlTitle_SizeChanged);
            this.pnlLogo.SizeChanged += new EventHandler(this.pnlLogo_SizeChanged);
            base.Load += new EventHandler(this.MessageBaseForm_Load);
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
            this.pnlForm = new Panel();
            this.BodyBounds = new Panel();
            this.BodyClient = new Panel();
            this.TitleArea = new Panel();
            this.pnlCaption = new Panel();
            this.pnlTitle = new Panel();
            this.picClose = new PictureBox();
            this.lblTitle = new Label();
            this.pnlLogo = new Panel();
            this.picLogo = new PictureBox();
            this.pnlForm.SuspendLayout();
            this.BodyBounds.SuspendLayout();
            this.TitleArea.SuspendLayout();
            this.pnlCaption.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            ((ISupportInitialize) this.picClose).BeginInit();
            this.pnlLogo.SuspendLayout();
            ((ISupportInitialize) this.picLogo).BeginInit();
            base.SuspendLayout();
            this.pnlForm.Controls.Add(this.BodyBounds);
            this.pnlForm.Controls.Add(this.TitleArea);
            this.pnlForm.Dock = DockStyle.Fill;
            this.pnlForm.Location = new Point(0, 0);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new Size(400, 300);
            this.pnlForm.TabIndex = 3;
            this.BodyBounds.BackColor = Color.FromArgb(0x33, 0x57, 0x74);
            this.BodyBounds.Controls.Add(this.BodyClient);
            this.BodyBounds.Dock = DockStyle.Fill;
            this.BodyBounds.Location = new Point(0, 30);
            this.BodyBounds.Name = "BodyBounds";
            this.BodyBounds.Padding = new Padding(6, 0, 6, 6);
            this.BodyBounds.Size = new Size(400, 270);
            this.BodyBounds.TabIndex = 6;
            this.BodyClient.BackColor = Color.White;
            this.BodyClient.Dock = DockStyle.Fill;
            this.BodyClient.Location = new Point(6, 0);
            this.BodyClient.Name = "BodyClient";
            this.BodyClient.Size = new Size(0x184, 0x108);
            this.BodyClient.TabIndex = 0;
            this.TitleArea.Controls.Add(this.pnlCaption);
            this.TitleArea.Dock = DockStyle.Top;
            this.TitleArea.Location = new Point(0, 0);
            this.TitleArea.Name = "TitleArea";
            this.TitleArea.Size = new Size(400, 30);
            this.TitleArea.TabIndex = 4;
            this.pnlCaption.BackColor = Color.FromArgb(0x33, 0x57, 0x74);
            this.pnlCaption.Controls.Add(this.pnlTitle);
            this.pnlCaption.Controls.Add(this.pnlLogo);
            this.pnlCaption.Dock = DockStyle.Fill;
            this.pnlCaption.Location = new Point(0, 0);
            this.pnlCaption.Name = "pnlCaption";
            this.pnlCaption.Size = new Size(400, 30);
            this.pnlCaption.TabIndex = 7;
            this.pnlTitle.BackColor = Color.Transparent;
            this.pnlTitle.Controls.Add(this.picClose);
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = DockStyle.Fill;
            this.pnlTitle.Font = new Font("微软雅黑", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.pnlTitle.ForeColor = Color.White;
            this.pnlTitle.Location = new Point(30, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new Size(370, 30);
            this.pnlTitle.TabIndex = 5;
            this.picClose.Anchor = AnchorStyles.Right;
            this.picClose.Location = new Point(0x148, 7);
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
            this.lblTitle.Size = new Size(370, 30);
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
            this.pnlLogo.Size = new Size(30, 30);
            this.pnlLogo.TabIndex = 6;
            this.picLogo.Anchor = AnchorStyles.Left;
            this.picLogo.BackgroundImageLayout = ImageLayout.Zoom;
            this.picLogo.Location = new Point(10, 6);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new Size(0x12, 0x12);
            this.picLogo.TabIndex = 2;
            this.picLogo.TabStop = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            base.ClientSize = new Size(400, 300);
            base.Controls.Add(this.pnlForm);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "MessageBaseForm";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "MessageBaseForm";
            this.pnlForm.ResumeLayout(false);
            this.BodyBounds.ResumeLayout(false);
            this.TitleArea.ResumeLayout(false);
            this.pnlCaption.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            ((ISupportInitialize) this.picClose).EndInit();
            this.pnlLogo.ResumeLayout(false);
            ((ISupportInitialize) this.picLogo).EndInit();
            base.ResumeLayout(false);
        }

        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (this._IsMoveFlag && (e.Button == MouseButtons.Left))
            {
                this.point_0 = new Point(-(e.X + this.pnlLogo.Width), -e.Y);
                this.bool_0 = true;
            }
        }

        private void lblTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (this._IsMoveFlag && this.bool_0)
            {
                Point mousePosition = Control.MousePosition;
                mousePosition.Offset(this.point_0.X, this.point_0.Y);
                base.Location = mousePosition;
            }
        }

        private void lblTitle_MouseUp(object sender, MouseEventArgs e)
        {
            if (this._IsMoveFlag && this.bool_0)
            {
                this.bool_0 = false;
            }
        }

        private void MessageBaseForm_Load(object sender, EventArgs e)
        {
            this.picClose.Visible = this._IsCloseVisble;
        }

        private Bitmap method_0(int int_0, int int_1)
        {
            Bitmap bitmap;
            Rectangle rect = new Rectangle(int_1 * 0x19, 0, 0x19, 0x12);
            switch (int_0)
            {
                case 0:
                    bitmap = Class131.smethod_1();
                    rect = new Rectangle(int_1 * 0x26, 0, 0x26, 0x12);
                    break;

                case 1:
                    bitmap = Class131.smethod_2();
                    break;

                case 2:
                    bitmap = Class131.smethod_3();
                    break;

                case 3:
                    bitmap = Class131.smethod_4();
                    break;

                default:
                    bitmap = Class131.smethod_1();
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

        private void pnlLogo_SizeChanged(object sender, EventArgs e)
        {
            int y = (this.pnlLogo.Height - this.picLogo.Height) / 2;
            this.picLogo.Location = new Point(this.picLogo.Location.X, y);
        }

        private void pnlTitle_SizeChanged(object sender, EventArgs e)
        {
            int x = (this.pnlTitle.Width - this.picClose.Width) - 3;
            int y = (this.pnlTitle.Height - this.picClose.Height) / 2;
            this.picClose.Location = new Point(x, y);
        }
    }
}

