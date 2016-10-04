namespace Aisino.Framework.Plugin.Core.Controls.OutlookBar
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class OutlookBounds : Control
    {
        private IContainer icontainer_0;

        public OutlookBounds()
        {
            
            this.method_0();
            base.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.ContainerControl, true);
            this.BackColor = Color.Transparent;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void method_0()
        {
            this.icontainer_0 = new Container();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }
    }
}

