namespace ns8
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Threading;
    using System.Windows.Forms;

    internal class Control4 : Control
    {
        private bool bool_0;
        private bool bool_1;
        private Container container_0;
        private int int_0;
        private MenuColorTable menuColorTable_0;
        private Point point_0;
        private Rectangle rectangle_0;

        internal event Delegate38 Event_0;

        public Control4()
        {
            
            this.int_0 = 120;
            this.method_0();
            this.method_1();
            this.menuColorTable_0 = new MenuColorTable();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void method_0()
        {
            this.container_0 = new Container();
        }

        private void method_1()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.UpdateStyles();
        }

        private void method_2(object object_0, Class125 class125_0)
        {
            if (this.Event_0 != null)
            {
                this.Event_0(object_0, class125_0);
            }
        }

        private Bitmap method_3(Enum12 enum12_0, int int_1)
        {
            Bitmap bitmap;
            Rectangle rectangle;
            switch (enum12_0)
            {
                case ((Enum12) 0):
                    bitmap = Class131.smethod_35();
                    rectangle = new Rectangle((int_1 * 0x15) + int_1, 0, 0x15, 0x52);
                    break;

                case ((Enum12) 1):
                    bitmap = Class131.smethod_36();
                    rectangle = new Rectangle((int_1 * 0x15) + int_1, 0, 0x15, 0x52);
                    break;

                case ((Enum12) 3):
                    bitmap = Class131.smethod_37();
                    rectangle = new Rectangle(0, (int_1 * 7) + int_1, 120, 7);
                    break;

                case ((Enum12) 4):
                    bitmap = Class131.smethod_34();
                    rectangle = new Rectangle(0, (int_1 * 7) + int_1, 120, 7);
                    break;

                default:
                    bitmap = Class131.smethod_0();
                    rectangle = new Rectangle(0, 0, 5, 5);
                    break;
            }
            return bitmap.Clone(rectangle, PixelFormat.Format64bppPArgb);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if ((e.Button == MouseButtons.Left) && base.ClientRectangle.Contains(this.point_0))
            {
                this.bool_1 = true;
                if (this.rectangle_0.Contains(this.point_0))
                {
                    this.bool_0 = !this.bool_0;
                    this.method_2(this, new Class125(this.bool_0));
                }
                base.Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.point_0 = new Point(-1, -1);
            this.bool_1 = false;
            base.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.point_0 = new Point(e.X, e.Y);
            base.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            if ((mevent.Button == MouseButtons.Left) && base.ClientRectangle.Contains(this.point_0))
            {
                this.bool_1 = false;
                base.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Image image;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            e.Graphics.FillRectangle(new SolidBrush(this.menuColorTable_0.ToolHideBarBackColor), base.ClientRectangle);
            this.rectangle_0 = new Rectangle((base.Width - this.int_0) / 2, 0, 120, 7);
            int num = 0;
            if (this.rectangle_0.Contains(this.point_0))
            {
                if (this.bool_1)
                {
                    num = 1;
                }
                else
                {
                    num = 1;
                }
            }
            else
            {
                num = 0;
            }
            if (this.bool_0)
            {
                image = this.method_3((Enum12) 4, num);
            }
            else
            {
                image = this.method_3((Enum12) 3, num);
            }
            e.Graphics.DrawImage(image, this.rectangle_0);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            base.Height = 7;
            base.Invalidate();
        }
    }
}

