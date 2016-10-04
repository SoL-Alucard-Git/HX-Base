namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class tpButton : Control
    {
        private System.Drawing.Bitmap bitmap_0;
        private CtlState ctlState_0;
        private IContainer icontainer_0;
        private int int_0;

        public tpButton()
        {
            
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.Selectable, true);
            this.BackColor = Color.Transparent;
            base.Size = new Size(10, 50);
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

        private void method_1(Graphics graphics_0)
        {
            if (this.bitmap_0 != null)
            {
                int x = (base.Width - this.bitmap_0.Width) / 2;
                int y = (base.Height - this.bitmap_0.Height) / 2;
                graphics_0.DrawImage(this.bitmap_0, new Point(x, y));
            }
        }

        protected override void OnClick(EventArgs e)
        {
            MouseEventArgs args = (MouseEventArgs) e;
            if (args.Button == MouseButtons.Left)
            {
                base.OnClick(e);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.ctlState_0 = CtlState.MouseEnter;
            base.Invalidate(false);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.ctlState_0 = CtlState.Normal;
            base.Invalidate(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            if (this.ctlState_0 == CtlState.MouseEnter)
            {
                graphics.DrawImage(Class131.smethod_44(), base.ClientRectangle);
            }
            this.method_1(graphics);
        }

        [Description("获取或设置控件显示的图标")]
        public System.Drawing.Bitmap Bitmap
        {
            get
            {
                return this.bitmap_0;
            }
            set
            {
                this.bitmap_0 = value;
                base.Invalidate(false);
            }
        }
    }
}

