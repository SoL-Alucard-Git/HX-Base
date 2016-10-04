namespace Aisino.Framework.Plugin.Core.Controls
{
    using Aisino.Framework.Plugin.Core.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class CustomInfoText : Control
    {
        private bool bool_0;
        private Color color_0;
        private IContainer icontainer_0;

        public CustomInfoText()
        {
            
            this.color_0 = Color.FromArgb(11, 0x51, 130);
            this.bool_0 = true;
            this.method_0();
            this.ForeColor = Color.FromArgb(11, 0x51, 130);
            this.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.UpdateStyles();
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
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            int x = 0;
            if (this.bool_0)
            {
                int num3 = (base.Height - 0x10) / 2;
                e.Graphics.DrawImage(Resources.smethod_2(), new Rectangle(x, num3, 0x10, 0x10));
                x += 0x15;
            }
            SizeF ef = e.Graphics.MeasureString(this.Text, this.Font);
            float y = (base.Height - ef.Height) / 2f;
            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), new RectangleF((float) x, y, ef.Width, ef.Height));
            Brush brush = new SolidBrush(this.color_0);
            e.Graphics.DrawLine(new Pen(brush), new Point(x, base.Height), new Point(base.Width, base.Height));
            e.Graphics.FillRectangle(brush, new Rectangle(x, base.Height - 3, ((int) ef.Width) - 2, 2));
        }

        public Color BorderLineColor
        {
            get
            {
                return this.color_0;
            }
            set
            {
                this.color_0 = value;
                base.Invalidate();
            }
        }

        public bool ShowIcon
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
                base.Invalidate();
            }
        }
    }
}

