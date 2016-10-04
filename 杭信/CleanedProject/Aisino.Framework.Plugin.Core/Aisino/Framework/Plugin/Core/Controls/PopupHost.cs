namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Windows.Forms;

    public class PopupHost : ToolStripDropDown
    {
        private ToolStripControlHost toolStripControlHost_0;

        public PopupHost(Control control_0)
        {
            
            if (control_0 == null)
            {
                throw new ArgumentException("control");
            }
            this.DoubleBuffered = true;
            base.ResizeRedraw = true;
            this.AutoSize = false;
            base.Padding = Padding.Empty;
            base.Margin = Padding.Empty;
            this.toolStripControlHost_0 = new ToolStripControlHost(control_0, "popupControlHost");
            this.toolStripControlHost_0.Padding = Padding.Empty;
            this.toolStripControlHost_0.Margin = Padding.Empty;
            this.toolStripControlHost_0.AutoSize = false;
            base.Size = this.toolStripControlHost_0.Size;
            base.Items.Add(this.toolStripControlHost_0);
        }
    }
}

