namespace Aisino.Framework.Plugin.Core.Controls.AisinoControls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class PrintPreviewDialogEx : PrintPreviewDialog
    {
        public string _FilePath;
        public ToolStripButton SaveStripBT;

        public PrintPreviewDialogEx()
        {
            
            foreach (Control control in base.Controls)
            {
                if (control.GetType() == typeof(ToolStrip))
                {
                    ToolStrip strip = control as ToolStrip;
                    this.SaveStripBT = this.method_0();
                    strip.Items.Insert(8, this.SaveStripBT);
                }
            }
        }

        private ToolStripButton method_0()
        {
            return new ToolStripButton { DisplayStyle = ToolStripItemDisplayStyle.Text, ImageTransparentColor = Color.Magenta, Name = "PPD_Save", Size = new Size(0x17, 0x16), Text = "保存" };
        }
    }
}

