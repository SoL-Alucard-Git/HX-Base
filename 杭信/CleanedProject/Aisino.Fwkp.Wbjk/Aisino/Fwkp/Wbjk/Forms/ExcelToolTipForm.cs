namespace Aisino.Fwkp.Wbjk.Forms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ExcelToolTipForm : Form
    {
        private IContainer components = null;
        private RichTextBox richTextBox1;
        private ToolTip toolTip1;

        public ExcelToolTipForm()
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
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ExcelToolTipForm));
            this.richTextBox1 = new RichTextBox();
            this.toolTip1 = new ToolTip(this.components);
            base.SuspendLayout();
            this.richTextBox1.BackColor = SystemColors.Info;
            this.richTextBox1.BorderStyle = BorderStyle.None;
            this.richTextBox1.Dock = DockStyle.Fill;
            this.richTextBox1.Enabled = false;
            this.richTextBox1.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.richTextBox1.Location = new Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new Size(0x1d0, 0xb7);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = manager.GetString("richTextBox1.Text");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.ClientSize = new Size(0x1d0, 0xb7);
            base.Controls.Add(this.richTextBox1);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ExcelToolTipForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "使用帮助";
            base.ResumeLayout(false);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}

