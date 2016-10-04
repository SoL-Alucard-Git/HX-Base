namespace Aisino.Framework.Plugin.Core.Util
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class WaitForm : Form
    {
        private IContainer icontainer_0;
        private AisinoLBL lblContent;
        private AisinoPRG progressBar;

        public WaitForm()
        {
            
            this.InitializeComponent();
        }

        public void CloseProgress()
        {
            MethodInvoker method = null;
            if (base.InvokeRequired)
            {
                if (method == null)
                {
                    method = new MethodInvoker(this.method_0);
                }
                base.BeginInvoke(method);
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(WaitForm));
            this.progressBar = new AisinoPRG();
            this.lblContent = new AisinoLBL();
            base.SuspendLayout();
            this.progressBar.Location = new Point(0x1f, 0x17);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new Size(0xe3, 0x17);
            this.progressBar.TabIndex = 6;
            this.lblContent.AutoSize = true;
            this.lblContent.Location = new Point(0x1f, 0x43);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new Size(0x77, 12);
            this.lblContent.TabIndex = 5;
            this.lblContent.Text = "正在执行，请稍候...";
            this.lblContent.TextAlign = ContentAlignment.MiddleLeft;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(300, 0x66);
            base.ControlBox = false;
            base.Controls.Add(this.progressBar);
            base.Controls.Add(this.lblContent);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "WaitForm";
            base.Opacity = 0.9;
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "提示";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        [CompilerGenerated]
        private void method_0()
        {
            base.Close();
        }

        public void SetFormPos(Point point_0)
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new Action<Point>(this.SetFormPos), new object[] { point_0 });
            }
            else
            {
                base.Location = new Point(point_0.X, point_0.Y);
            }
        }

        public void SetIndeterminate(bool bool_0)
        {
            MethodInvoker method = null;
            bool isIndeterminate = bool_0;
            if (this.progressBar.InvokeRequired)
            {
                if (method == null)
                {
                    method = delegate {
                        if (isIndeterminate)
                        {
                            this.progressBar.Style = ProgressBarStyle.Marquee;
                        }
                        else
                        {
                            this.progressBar.Style = ProgressBarStyle.Blocks;
                        }
                    };
                }
                this.progressBar.BeginInvoke(method);
            }
            else if (isIndeterminate)
            {
                this.progressBar.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                this.progressBar.Style = ProgressBarStyle.Blocks;
            }
        }

        public void SetProgressMaxValue(int int_0)
        {
            if (this.progressBar.InvokeRequired)
            {
                base.BeginInvoke(new Action<int>(this.SetProgressMaxValue), new object[] { int_0 });
            }
            else
            {
                this.progressBar.Maximum = int_0;
            }
        }

        public void SetProgressValue(int int_0)
        {
            if (this.progressBar.InvokeRequired)
            {
                base.BeginInvoke(new Action<int>(this.SetProgressValue), new object[] { int_0 });
            }
            else
            {
                this.progressBar.Value = int_0;
            }
        }

        public void SetText(string string_0)
        {
            if (this.lblContent.InvokeRequired)
            {
                base.BeginInvoke(new Action<string>(this.SetText), new object[] { string_0 });
            }
            else
            {
                this.lblContent.Text = string_0;
            }
        }

        public void SetTipText(string string_0)
        {
            MethodInvoker method = null;
            string tip = string_0;
            if (this.lblContent.InvokeRequired)
            {
                if (method == null)
                {
                    method = () => this.lblContent.Text = tip;
                }
                this.lblContent.BeginInvoke(method);
            }
            else
            {
                this.lblContent.Text = tip;
            }
        }

        public void UpdateProgress(int int_0)
        {
            MethodInvoker method = null;
            int progress = int_0;
            if (this.progressBar.InvokeRequired)
            {
                if (method == null)
                {
                    method = () => this.progressBar.Value = progress;
                }
                this.progressBar.BeginInvoke(method);
            }
            else
            {
                this.progressBar.Value = progress;
            }
        }

        public void UpdateProgressTip(string string_0)
        {
            MethodInvoker method = null;
            string tip = string_0;
            if (this.lblContent.InvokeRequired)
            {
                if (method == null)
                {
                    method = () => this.lblContent.Text = tip;
                }
                this.lblContent.BeginInvoke(method);
            }
            else
            {
                this.lblContent.Text = tip;
            }
        }
    }
}

