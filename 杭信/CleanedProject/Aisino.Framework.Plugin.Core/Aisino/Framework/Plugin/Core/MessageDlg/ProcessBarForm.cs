namespace Aisino.Framework.Plugin.Core.MessageDlg
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ProcessBarForm : Form
    {
        private IContainer icontainer_0;
        private Label lblMsg;
        private Label lblTitle;
        private Panel panel1;
        private Panel panel2;
        private ProgressBar progressBar1;

        public ProcessBarForm()
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
            this.lblTitle = new Label();
            this.lblMsg = new Label();
            this.panel1 = new Panel();
            this.panel2 = new Panel();
            this.progressBar1 = new ProgressBar();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
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
            this.lblMsg.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblMsg.Font = new Font("微软雅黑", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.lblMsg.ForeColor = Color.Black;
            this.lblMsg.Location = new Point(7, 8);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new Size(0x174, 0x4b);
            this.lblMsg.TabIndex = 0x10;
            this.lblMsg.Text = "你是一个小红小";
            this.lblMsg.TextAlign = ContentAlignment.MiddleLeft;
            this.panel1.BackColor = Color.FromArgb(0x33, 0x57, 0x74);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0x1b);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new Padding(6, 0, 6, 6);
            this.panel1.Size = new Size(400, 0x71);
            this.panel1.TabIndex = 1;
            this.panel2.BackColor = Color.White;
            this.panel2.Controls.Add(this.lblMsg);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(6, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x184, 0x5c);
            this.panel2.TabIndex = 2;
            this.progressBar1.BackColor = Color.DarkSlateGray;
            this.progressBar1.Dock = DockStyle.Bottom;
            this.progressBar1.Location = new Point(6, 0x5c);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(0x184, 15);
            this.progressBar1.TabIndex = 0x12;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(400, 140);
            base.ControlBox = false;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.lblTitle);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "ProcessBarForm";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "ProcessBarForm";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public int Count
        {
            get
            {
                return this.progressBar1.Maximum;
            }
            set
            {
                this.progressBar1.Maximum = value;
            }
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

        public int Pos
        {
            get
            {
                return this.progressBar1.Value;
            }
            set
            {
                this.progressBar1.Value = value;
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

