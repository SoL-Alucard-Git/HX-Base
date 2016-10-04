namespace Aisino.Fwkp.Fpkj.Form.FPXF
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FPXFStyle : DockForm
    {
        private IContainer components;

        public FPXFStyle()
        {
            this.InitializeComponent();
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FPXFStyle));
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(480, 0x153);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "FPXFStyle";
            base.TabText = "FPXFStyle";
            this.Text = "FPXFStyle";
            base.ResumeLayout(false);
        }
    }
}

