namespace Aisino.Framework.SplashForm
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FormSplash : Form
    {
        private IContainer components;
        private static FormSplash splashScreen;
        private Label status;

        public FormSplash()
        {
            this.InitializeComponent();
        }

        public static void CloseSplash()
        {
            splashScreen.Close();
            splashScreen.Dispose(true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormSplash));
            this.status = new Label();
            base.SuspendLayout();
            this.status.AutoSize = true;
            this.status.BackColor = Color.Transparent;
            this.status.Cursor = Cursors.Default;
            this.status.ImageAlign = ContentAlignment.BottomLeft;
            this.status.Location = new Point(0x18, 0xb8);
            this.status.Name = "status";
            this.status.Size = new Size(0x47, 12);
            this.status.TabIndex = 0;
            this.status.Text = "正在启动...";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackgroundImage = (Image) manager.GetObject("$this.BackgroundImage");
            base.ClientSize = new Size(560, 220);
            base.Controls.Add(this.status);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "FormSplash";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "SplashForm";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public static void InitSplash()
        {
            if (splashScreen == null)
            {
                splashScreen = new FormSplash();
                splashScreen.Show();
            }
        }

        public static void SetStatus(string statusStr)
        {
            if (splashScreen != null)
            {
                splashScreen.status.Text = statusStr;
                splashScreen.status.Refresh();
            }
        }

        public static FormSplash SplashScreen
        {
            get
            {
                return splashScreen;
            }
        }
    }
}

