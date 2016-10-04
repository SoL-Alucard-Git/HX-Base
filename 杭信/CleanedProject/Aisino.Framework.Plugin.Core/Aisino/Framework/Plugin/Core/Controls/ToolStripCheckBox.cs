namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;
    using ns8;
    [DefaultProperty("Items"), ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ContextMenuStrip | ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ToolStrip)]
    public class ToolStripCheckBox : ToolStripControlHost
    {
        public ToolStripCheckBox() : base(new Class133())
        {
            
            this.Size = new Size(0x54, 0x10);
            this.BackColor = Color.Transparent;
        }

        public ToolStripCheckBox(string string_0) : this()
        {
            
            base.Name = string_0;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public AisinoCHK AisinoCHK_0
        {
            get
            {
                return (AisinoCHK) base.Control;
            }
        }
    }
}

