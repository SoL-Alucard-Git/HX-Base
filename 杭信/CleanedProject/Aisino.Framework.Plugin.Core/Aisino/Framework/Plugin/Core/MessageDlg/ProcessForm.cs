namespace Aisino.Framework.Plugin.Core.MessageDlg
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ProcessForm : Form
    {
        private IContainer icontainer_0;
        private Label lblMsg;
        private Label lblTitle;
        private Panel panel1;
        private Panel panel2;
        private PictureBox pictureBox1;

        public ProcessForm()
        {
            
            this.InitializeComponent();
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
            this.panel1 = new Panel();
            this.panel2 = new Panel();
            this.pictureBox1 = new PictureBox();
            this.lblMsg = new Label();
            this.lblTitle = new Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.panel1.BackColor = Color.FromArgb(0x33, 0x57, 0x74);
            this.panel1.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0x1b);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new Padding(6, 0, 6, 6);
            this.panel1.Size = new Size(400, 0x71);
            this.panel1.TabIndex = 0;
            this.panel2.BackColor = Color.White;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.lblMsg);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(6, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x182, 0x69);
            this.panel2.TabIndex = 1;
            this.pictureBox1.BackColor = Color.Transparent;
            this.pictureBox1.Image = Class131.smethod_46();
            this.pictureBox1.Location = new Point(13, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x30, 0x30);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            this.lblMsg.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblMsg.Font = new Font("微软雅黑", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lblMsg.ForeColor = Color.Black;
            this.lblMsg.Location = new Point(0x48, 7);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new Size(0x131, 90);
            this.lblMsg.TabIndex = 0x10;
            this.lblMsg.Text = "你是一个小红小";
            this.lblMsg.TextAlign = ContentAlignment.MiddleLeft;
            this.lblTitle.AllowDrop = true;
            this.lblTitle.BackColor = Color.FromArgb(0x33, 0x57, 0x74);
            this.lblTitle.Dock = DockStyle.Top;
            this.lblTitle.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(400, 0x1b);
            this.lblTitle.TabIndex = 0x11;
            this.lblTitle.Text = "过程提示";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Info;
            base.ClientSize = new Size(400, 140);
            base.ControlBox = false;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.lblTitle);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "ProcessForm";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "ProcessForm";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
        }

        public string Message
        {
            get
            {
                return this.lblMsg.Text;
            }
            set
            {
                this.lblMsg.Text = value;
            }
        }

        public string Title
        {
            get
            {
                return this.lblTitle.Text;
            }
            set
            {
                this.lblTitle.Text = value;
            }
        }
    }
}

