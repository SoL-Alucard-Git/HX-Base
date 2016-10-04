namespace Aisino.FTaxBase
{
    using Aisino.FTaxBase.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MsgForm : FormBase
    {
        private IContainer icontainer_1;
        private Label label1;
        private Panel panel1;
        private PictureBox pictureBox1;

        public MsgForm()
        {
            
            this.InitializeComponent_1();
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
            this.label1 = new Label();
            this.pictureBox1 = new PictureBox();
            this.panel1 = new Panel();
            base.BodyBounds.SuspendLayout();
            base.BodyClient.SuspendLayout();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            base.lblTitle.Size = new Size(0x178, 30);
            base.TitleArea.Size = new Size(400, 30);
            base.BodyBounds.Size = new Size(400, 110);
            base.BodyClient.Controls.Add(this.panel1);
            base.BodyClient.Size = new Size(0x184, 0x68);
            this.label1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.label1.BackColor = Color.Transparent;
            this.label1.Font = new Font("微软雅黑", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label1.Location = new Point(0x47, 7);
            this.label1.Name = "label1";
            this.label1.Size = new Size(310, 0x59);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextAlign = ContentAlignment.MiddleLeft;
            this.pictureBox1.BackColor = Color.Transparent;
            this.pictureBox1.Image = Resources.smethod_9();
            this.pictureBox1.Location = new Point(5, 0x19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(60, 60);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.panel1.BackColor = Color.Transparent;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x184, 0x68);
            this.panel1.TabIndex = 4;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            base.ClientSize = new Size(400, 140);
            base.CloseBtnVisible = true;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "MsgForm";
            this.Text = "金税设备提示信息";
            base.TopMost = false;
            base.BodyBounds.ResumeLayout(false);
            base.BodyClient.ResumeLayout(false);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public string MessageStr
        {
            set
            {
                this.label1.Text = value;
            }
        }
    }
}

