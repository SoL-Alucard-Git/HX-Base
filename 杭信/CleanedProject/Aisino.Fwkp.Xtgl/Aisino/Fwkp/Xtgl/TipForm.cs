namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TipForm : BaseForm
    {
        private IContainer components;
        private AisinoLBL label1;
        private AisinoRTX richTextBox1;

        public TipForm()
        {
            this.InitializeComponent();
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Load += new EventHandler(this.TipForm_Load);
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(TipForm));
            this.richTextBox1 = new AisinoRTX();
            this.label1 = new AisinoLBL();
            base.SuspendLayout();
            this.richTextBox1.BackColor = Color.AliceBlue;
            this.richTextBox1.BorderStyle = BorderStyle.None;
            this.richTextBox1.ForeColor = Color.Red;
            this.richTextBox1.Location = new Point(12, 0x2b);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new Size(0x100, 0xb5);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "注册文件1；\n注册文件2";
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.Location = new Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x7d, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "以下注册文件已过期：";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(280, 0xec);
            base.Controls.Add(this.richTextBox1);
            base.Controls.Add(this.label1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Location = new Point(0, 0);
            base.Name = "TipForm";
            this.Text = "注册文件过期提醒";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void TipForm_Load(object sender, EventArgs e)
        {
            int x = (Screen.PrimaryScreen.WorkingArea.Size.Width - base.Width) - 10;
            int y = (Screen.PrimaryScreen.WorkingArea.Size.Height - base.Height) - 10;
            base.SetDesktopLocation(x, y);
            Win32.SetWindowPos(base.Handle, 100, Screen.PrimaryScreen.Bounds.Width - base.Width, (Screen.PrimaryScreen.Bounds.Height - base.Height) - 30, 50, 50, 1);
            Win32.AnimateWindow(base.Handle, 500, 2);
        }
    }
}

