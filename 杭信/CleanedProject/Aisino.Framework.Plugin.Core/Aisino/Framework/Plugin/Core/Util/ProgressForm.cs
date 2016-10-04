namespace Aisino.Framework.Plugin.Core.Util
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ProgressForm : Form
    {
        private bool bool_0;
        private IContainer icontainer_0;
        private AisinoLBL lblPercent;
        private AisinoPRG progressBar1;
        private string string_0;

        public ProgressForm()
        {
            
            this.InitializeComponent();
            this.SetText("");
        }

        public void CloseForm()
        {
            if (base.InvokeRequired)
            {
                base.Invoke(new MethodInvoker(this.Close));
            }
            else
            {
                base.Close();
            }
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
            this.progressBar1 = new AisinoPRG();
            this.lblPercent = new AisinoLBL();
            base.SuspendLayout();
            this.progressBar1.BackColor = SystemColors.ButtonShadow;
            this.progressBar1.Location = new Point(0x16, 0x4a);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(0x143, 0x17);
            this.progressBar1.TabIndex = 2;
            this.lblPercent.ForeColor = Color.DarkRed;
            this.lblPercent.Location = new Point(20, 0x24);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new Size(0x150, 0x23);
            this.lblPercent.TabIndex = 3;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            base.ClientSize = new Size(0x17d, 0x99);
            base.ControlBox = false;
            base.Controls.Add(this.lblPercent);
            base.Controls.Add(this.progressBar1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "ProgressForm";
            base.Opacity = 0.9;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "进度条窗口";
            base.ResumeLayout(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, base.ClientSize.Width, 0x1a);
            SolidBrush brush = new SolidBrush(SystemColors.Desktop);
            e.Graphics.FillRectangle(brush, rect);
            Font font = new Font("宋体", 12f, FontStyle.Bold);
            float x = base.Width / 2;
            float y = 4f;
            StringFormat format = new StringFormat {
                Alignment = StringAlignment.Center
            };
            e.Graphics.DrawString(this.Title, font, Brushes.DarkRed, x, y, format);
        }

        public void SetProgress(ProgressData progressData_0)
        {
            if (base.InvokeRequired)
            {
                base.Invoke(new Action<ProgressData>(this.SetProgress), new object[] { progressData_0 });
            }
            else if (progressData_0 != null)
            {
                this.progressBar1.Value = progressData_0.Current;
                this.progressBar1.Maximum = progressData_0.Total;
                this.lblPercent.Text = progressData_0.TipText;
                if (!this.IsCycle && (progressData_0.Current == progressData_0.Total))
                {
                    base.Close();
                }
            }
        }

        public void SetProgressValue(int int_0)
        {
            if (this.progressBar1.InvokeRequired)
            {
                base.Invoke(new Action<int>(this.SetProgressValue), new object[] { int_0 });
            }
            else
            {
                this.progressBar1.Value = int_0;
            }
        }

        public void SetText(string string_1)
        {
            if (this.lblPercent.InvokeRequired)
            {
                base.Invoke(new Action<string>(this.SetText), new object[] { string_1 });
            }
            else
            {
                this.lblPercent.Text = string_1;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                Point p = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 0x10);
                if (base.PointToClient(p).Y < 0x1a)
                {
                    m.Result = (IntPtr) 2;
                    return;
                }
            }
            base.WndProc(ref m);
        }

        public bool IsCycle
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public AisinoPRG PBar
        {
            get
            {
                return this.progressBar1;
            }
        }

        public string TipText
        {
            get
            {
                return this.lblPercent.Text;
            }
            set
            {
                this.lblPercent.Text = value;
            }
        }

        public string Title
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

