namespace Aisino.Framework.Plugin.Core.Controls
{
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class ToolBarItem : Control
    {
        private System.Drawing.Bitmap bitmap_0;
        private bool bool_0;
        private CtlState ctlState_0;
        private IContainer icontainer_0;
        private int int_0;
        private string string_0;

        public ToolBarItem()
        {
            
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.Selectable, true);
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.White;
            base.Size = new Size(0x48, 0x48);
            this.Font = new Font("微软雅黑", 9f);
        }

        public System.Drawing.Bitmap BitMapZoom(System.Drawing.Bitmap bitmap_1, int int_1, int int_2)
        {
            System.Drawing.Bitmap bitmap;
            System.Drawing.Bitmap bitmap2;
            try
            {
                bitmap = new System.Drawing.Bitmap(bitmap_1);
                bitmap2 = new System.Drawing.Bitmap(int_1, int_2);
                Graphics graphics = Graphics.FromImage(bitmap2);
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(bitmap, new Rectangle(0, 0, int_1, int_2), new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
                graphics.Dispose();
                return bitmap2;
            }
            catch
            {
            }
            finally
            {
                bitmap = null;
                bitmap2 = null;
                GC.Collect();
            }
            return null;
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
            graphics_0.DrawImage(Class131.smethod_43(), base.ClientRectangle);
        }

        private void method_2(Graphics graphics_0)
        {
            if (this.bitmap_0 != null)
            {
                System.Drawing.Bitmap image = this.BitMapZoom(this.bitmap_0, 0x30, 0x30);
                this.int_0 = (base.Width - image.Width) / 2;
                graphics_0.DrawImage(image, new Rectangle(this.int_0, 4, image.Width, image.Height));
            }
        }

        private void method_3(Graphics graphics_0)
        {
            SizeF ef = graphics_0.MeasureString(this.Text, this.Font);
            float x = (base.Width - ef.Width) / 2f;
            if (x <= 0f)
            {
                x = 0f;
            }
            graphics_0.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), x, 52f);
        }

        protected override void OnClick(EventArgs e)
        {
            MouseEventArgs args = (MouseEventArgs) e;
            if (args.Button == MouseButtons.Left)
            {
                base.OnClick(e);
                base.Focus();
                if ((this.string_0 != null) || (this.string_0 != string.Empty))
                {
                    ToolUtil.RunFuction(this.string_0);
                }
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            this.Focused = true;
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            this.Focused = false;
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
            if (!this.Focused)
            {
                this.ctlState_0 = CtlState.LostFocused;
                base.Invalidate(false);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            switch (this.ctlState_0)
            {
                case CtlState.Focused:
                    this.method_1(graphics);
                    break;

                case CtlState.MouseEnter:
                    if (this.Focused)
                    {
                        this.method_1(graphics);
                        break;
                    }
                    graphics.DrawImage(Class131.smethod_44(), base.ClientRectangle);
                    break;
            }
            this.method_2(graphics);
            this.method_3(graphics);
        }

        protected override void OnResize(EventArgs e)
        {
            base.Width = 0x48;
            base.Height = 0x48;
        }

        public System.Drawing.Bitmap ScaleZoom(System.Drawing.Bitmap bitmap_1)
        {
            double num3;
            if ((bitmap_1 == null) || ((bitmap_1.Width <= 0x30) && (bitmap_1.Height <= 0x30)))
            {
                return bitmap_1;
            }
            double num = ((double) bitmap_1.Width) / ((double) bitmap_1.Height);
            double num2 = 1.0;
            if (num > num2)
            {
                num3 = 48.0 / ((double) bitmap_1.Width);
                return this.BitMapZoom(bitmap_1, 0x30, (int) (bitmap_1.Height * num3));
            }
            num3 = 48.0 / ((double) bitmap_1.Height);
            return this.BitMapZoom(bitmap_1, (int) (bitmap_1.Width * num3), 0x30);
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

        [Description("重写控件焦点属性")]
        public bool Focused
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
                if (this.bool_0)
                {
                    this.ctlState_0 = CtlState.Focused;
                }
                else
                {
                    this.ctlState_0 = CtlState.LostFocused;
                }
                base.Invalidate(false);
            }
        }

        [Description("获取或设置控件对应的菜单ID")]
        public string MenuID
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                base.Invalidate();
            }
        }
    }
}

