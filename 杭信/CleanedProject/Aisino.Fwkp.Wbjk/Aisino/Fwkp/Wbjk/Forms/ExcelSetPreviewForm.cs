namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ExcelSetPreviewForm : Form
    {
        private IContainer components = null;

        public ExcelSetPreviewForm(CustomStyleDataGrid dgvPreView)
        {
            this.InitializeComponent();
            base.Controls.Add(dgvPreView);
            dgvPreView.Dock = DockStyle.Fill;
            dgvPreView.Visible = true;
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
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x310, 0x1ce);
            base.Name = "ExcelSetPreviewForm";
            this.Text = "导入预览";
            base.ResumeLayout(false);
        }
    }
}

