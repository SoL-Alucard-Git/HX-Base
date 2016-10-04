namespace Aisino.Framework.MainForm
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class BaseNavForm : Form
    {
        protected string FormTagFlag;
        private IContainer icontainer_0;
        protected AisinoPNL NavPanle;

        public BaseNavForm()
        {
            
            this.FormTagFlag = "NavForm";
            this.InitializeComponent();
            base.SizeChanged += new EventHandler(this.BaseNavForm_SizeChanged);
        }

        private void BaseNavForm_SizeChanged(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;
            x = (base.Width - this.NavPanle.Width) / 2;
            y = (base.Height - this.NavPanle.Height) / 2;
            this.NavPanle.Location = new Point(x, y);
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BaseNavForm));
            this.NavPanle = new AisinoPNL();
            base.SuspendLayout();
            this.NavPanle.Anchor = AnchorStyles.None;
            this.NavPanle.BackColor = Color.Transparent;
            this.NavPanle.BackgroundImageLayout = ImageLayout.None;
            this.NavPanle.Location = new Point(0, 0);
            this.NavPanle.Name = "NavPanle";
            this.NavPanle.Size = new Size(800, 530);
            this.NavPanle.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.BackgroundImageLayout = ImageLayout.None;
            base.ClientSize = new Size(800, 530);
            base.ControlBox = false;
            base.Controls.Add(this.NavPanle);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "BaseNavForm";
            base.Opacity = 0.0;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.ResumeLayout(false);
        }
    }
}

