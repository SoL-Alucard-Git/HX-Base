namespace Aisino.Fwkp.Bmgl.Forms
{
    using Aisino.Fwkp.Bmgl.Properties;
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using Framework.Plugin.Core.Util;
    public class MsgForm : Form
    {
        private ILog _Loger = LogUtil.GetLogger<MsgForm>();
        private IContainer components;
        private Label label1;
        private PictureBox pictureBox1;

        public MsgForm(string msg)
        {
            this.InitializeComponent();
            base.StartPosition = FormStartPosition.CenterScreen;
            base.TopMost = true;
            this.label1.Text = msg;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int width = radius;
            Rectangle rectangle = new Rectangle(rect.Location, new Size(width, width));
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rectangle, 180f, 90f);
            rectangle.X = rect.Right - width;
            path.AddArc(rectangle, 270f, 90f);
            rectangle.Y = rect.Bottom - width;
            path.AddArc(rectangle, 0f, 90f);
            rectangle.X = rect.Left;
            path.AddArc(rectangle, 90f, 90f);
            path.CloseFigure();
            return path;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(MsgForm));
            this.label1 = new Label();
            this.pictureBox1 = new PictureBox();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Font = new Font("微软雅黑", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.Location = new Point(0x7c, 0x2a);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x8d, 0x13);
            this.label1.TabIndex = 1;
            this.label1.Text = "正在导入稀土编码...";
            this.label1.UseWaitCursor = true;
            this.pictureBox1.Image = Resources.Wait;
            this.pictureBox1.Location = new Point(0x37, 0x22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x24, 0x24);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.UseWaitCursor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = SystemColors.Control;
            this.BackgroundImage = (Image) manager.GetObject("$this.BackgroundImage");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            base.ClientSize = new Size(0x175, 0x73);
            base.Controls.Add(this.pictureBox1);
            base.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            base.FormBorderStyle = FormBorderStyle.None;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "MsgForm";
            this.Text = "提示";
            base.TopMost = true;
            base.UseWaitCursor = true;
            base.Paint += new PaintEventHandler(this.MsgForm_Paint);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void MsgForm_Paint(object sender, PaintEventArgs e)
        {
            this.SetWindowRegion();
        }

        public void SetFormClose()
        {
            if (base.InvokeRequired)
            {
                SetFormCloseCallBack method = new SetFormCloseCallBack(this.SetFormClose);
                base.Invoke(method, new object[0]);
            }
            else
            {
                base.Close();
            }
        }

        public void SetFormShow()
        {
            if (base.InvokeRequired)
            {
                SetFromShowCallBack method = new SetFromShowCallBack(this.SetFormShow);
                base.Invoke(method, new object[0]);
            }
            else
            {
                base.ShowDialog();
            }
        }

        public void SetLabelMsg(string msg)
        {
            try
            {
                if (this.label1.InvokeRequired)
                {
                    SetLabelMsgCallBack method = new SetLabelMsgCallBack(this.SetLabelMsg);
                    base.Invoke(method, new object[] { msg });
                }
                else
                {
                    this.label1.Text = msg;
                }
            }
            catch (Exception exception)
            {
                this._Loger.Error(exception.Message);
            }
        }

        public void SetWindowRegion()
        {
            GraphicsPath roundedRectPath = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, base.Width, base.Height);
            roundedRectPath = this.GetRoundedRectPath(rect, 6);
            base.Region = new Region(roundedRectPath);
        }

        private delegate void SetFormCloseCallBack();

        private delegate void SetFromShowCallBack();

        private delegate void SetLabelMsgCallBack(string msg);
    }
}

