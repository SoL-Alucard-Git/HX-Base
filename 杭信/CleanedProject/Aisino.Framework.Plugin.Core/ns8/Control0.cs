namespace ns8
{
    using Aisino.Framework.Plugin.Core.Controls;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Threading;
    using System.Windows.Forms;

    internal class Control0 : Control
    {
        private bool bool_0;
        private bool bool_1;
        private bool bool_2;
        private bool bool_3;
        private Container container_0;
        private Font font_0;
        private int int_0;
        private List<NavigateMenuNodeRect> list_0;
        private MenuColorTable menuColorTable_0;
        private NavigateMenuNodeCollection navigateMenuNodeCollection_0;
        private Point point_0;
        private Rectangle rectangle_0;
        private Rectangle rectangle_1;
        private Rectangle rectangle_2;
        private Rectangle rectangle_3;
        private Rectangle rectangle_4;
        private ToolTip toolTip_0;

        internal event Delegate34 Event_0;

        internal event EventHandler Event_1;

        public Control0()
        {
            
            this.font_0 = new Font("宋体", 9f);
            this.int_0 = 0x1f;
            this.rectangle_4 = new Rectangle(0, 0, 0, 0);
            this.toolTip_0 = new ToolTip();
            this.bool_3 = true;
            this.method_13();
            this.method_2();
            this.point_0 = new Point(-1, -1);
            this.menuColorTable_0 = new MenuColorTable();
            this.navigateMenuNodeCollection_0 = new NavigateMenuNodeCollection();
            this.list_0 = new List<NavigateMenuNodeRect>();
            this.navigateMenuNodeCollection_0.Changed += new EventHandler(this.navigateMenuNodeCollection_0_Changed);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool method_0()
        {
            return this.bool_3;
        }

        public void method_1(bool bool_4)
        {
            this.bool_3 = bool_4;
            base.Invalidate();
        }

        private Bitmap method_10(Enum12 enum12_0, int int_1)
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

        private string method_11(PaintEventArgs paintEventArgs_0, string string_0, int int_1)
        {
            string str = string.Empty;
            if (paintEventArgs_0.Graphics.MeasureString(string_0, this.font_0).Width <= int_1)
            {
                return string_0;
            }
            for (int i = string_0.Length - 1; i >= 0; i--)
            {
                string text = string_0.Substring(0, i);
                if (paintEventArgs_0.Graphics.MeasureString(text, this.font_0).Width <= int_1)
                {
                    return text;
                }
            }
            return str;
        }

        private Bitmap method_12(Bitmap bitmap_0, int int_1, int int_2)
        {
            Bitmap bitmap;
            Bitmap bitmap2;
            try
            {
                bitmap = new Bitmap(bitmap_0);
                bitmap2 = new Bitmap(int_1, int_2);
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

        private void method_13()
        {
            this.container_0 = new Container();
        }

        private void method_2()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.UpdateStyles();
        }

        private void method_3(PaintEventArgs paintEventArgs_0)
        {
            this.rectangle_3 = new Rectangle((base.Width - 0x15) - 40, 10, 0x25, 0x25);
            if (this.rectangle_3.Contains(this.point_0))
            {
                paintEventArgs_0.Graphics.FillPath(new SolidBrush(this.menuColorTable_0.MenuDarkBackColor), this.method_5(this.rectangle_3, 8));
            }
            Bitmap image = Class131.smethod_33();
            paintEventArgs_0.Graphics.DrawImage(image, (int) ((this.rectangle_3.X + ((this.rectangle_3.Width - image.Width) / 2)) + 1), (int) ((this.rectangle_3.Y + 1) + ((this.rectangle_3.Height - image.Height) / 2)));
        }

        private void method_4(ref int int_1, int int_2, PaintEventArgs paintEventArgs_0, List<NavigateMenuNodeRect> nodeList)
        {
            for (int i = 0; i < this.navigateMenuNodeCollection_0.Count; i++)
            {
                Rectangle rectangle = new Rectangle(int_1, int_2, 0x44, 0x44);
                nodeList.Add(new NavigateMenuNodeRect(this.navigateMenuNodeCollection_0[i], rectangle));
                if (!this.rectangle_3.Contains(this.point_0) && rectangle.Contains(this.point_0))
                {
                    paintEventArgs_0.Graphics.FillPath(new SolidBrush(this.menuColorTable_0.MenuDarkBackColor), this.method_5(rectangle, 8));
                }
                Bitmap icon = this.navigateMenuNodeCollection_0[i].Icon;
                icon = this.method_12(icon, 0x2a, 0x2a);
                paintEventArgs_0.Graphics.DrawImage(icon, new Point(int_1 + ((0x44 - icon.Width) / 2), int_2 + 5));
                string text = this.method_11(paintEventArgs_0, this.Items[i].SimpleText, 0x44);
                SizeF ef = paintEventArgs_0.Graphics.MeasureString(text, this.font_0);
                paintEventArgs_0.Graphics.DrawString(text, this.font_0, new SolidBrush(this.menuColorTable_0.MenuFont), new PointF(((float) int_1) + ((68f - ef.Width) / 2f), (float) ((int_2 + 0x30) + 5)));
                int_1 += 0x4e;
            }
        }

        private GraphicsPath method_5(Rectangle rectangle_5, int int_1)
        {
            int width = int_1;
            Rectangle rect = new Rectangle(rectangle_5.Location, new Size(width, width));
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect, 180f, 90f);
            rect.X = rectangle_5.Right - width;
            path.AddArc(rect, 270f, 90f);
            rect.Y = rectangle_5.Bottom - width;
            path.AddArc(rect, 0f, 90f);
            rect.X = rectangle_5.Left;
            path.AddArc(rect, 90f, 90f);
            path.CloseFigure();
            return path;
        }

        private void method_6(PaintEventArgs paintEventArgs_0)
        {
            int num = 0;
            this.rectangle_0 = new Rectangle(0, 0, 0x15, 0x52);
            if (this.bool_0)
            {
                if (this.rectangle_0.Contains(this.point_0))
                {
                    if (this.bool_2)
                    {
                        num = 3;
                    }
                    else
                    {
                        num = 2;
                    }
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
            Bitmap image = this.method_10((Enum12) 0, num);
            paintEventArgs_0.Graphics.DrawImage(image, this.rectangle_0);
            this.rectangle_1 = new Rectangle(base.Width - 0x15, 0, 0x15, 0x52);
            if (this.bool_1)
            {
                if (this.rectangle_1.Contains(this.point_0))
                {
                    if (this.bool_2)
                    {
                        num = 3;
                    }
                    else
                    {
                        num = 2;
                    }
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
            image = this.method_10((Enum12) 1, num);
            paintEventArgs_0.Graphics.DrawImage(image, this.rectangle_1);
        }

        private void method_7(int int_1, int int_2)
        {
            Point pt = new Point(int_1, int_2);
            foreach (NavigateMenuNodeRect rect in this.list_0)
            {
                if (rect.Rect.Contains(pt))
                {
                    this.rectangle_4 = rect.Rect;
                    this.toolTip_0.Show(rect.Node.Text, this, int_1, int_2, 0xbb8);
                }
            }
        }

        private void method_8(object sender, EventArgs0 e)
        {
            if (this.Event_0 != null)
            {
                this.Event_0(this, e);
            }
        }

        private void method_9(object sender, EventArgs e)
        {
            if (this.Event_1 != null)
            {
                this.Event_1(sender, e);
            }
        }

        private void navigateMenuNodeCollection_0_Changed(object sender, EventArgs e)
        {
            base.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if ((e.Button != MouseButtons.Left) || !base.ClientRectangle.Contains(this.point_0))
            {
                return;
            }
            this.bool_2 = true;
            if (this.rectangle_0.Contains(this.point_0) && this.bool_0)
            {
                this.int_0 += 390;
            }
            if (this.rectangle_1.Contains(this.point_0) && this.bool_1)
            {
                this.int_0 -= 390;
            }
            if (this.rectangle_3.Contains(this.point_0))
            {
                if (this.bool_3)
                {
                    this.method_9(this, new EventArgs());
                }
            }
            else if (this.rectangle_2.Contains(this.point_0))
            {
                using (List<NavigateMenuNodeRect>.Enumerator enumerator = this.list_0.GetEnumerator())
                {
                    NavigateMenuNodeRect current;
                    while (enumerator.MoveNext())
                    {
                        current = enumerator.Current;
                        if (current.Rect.Contains(this.point_0))
                        {
                            goto Label_0105;
                        }
                    }
                    goto Label_0127;
                Label_0105:
                    this.method_8(this, new EventArgs0(current.Node));
                }
            }
        Label_0127:
            base.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.point_0 = new Point(-1, -1);
            this.bool_2 = false;
            base.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.point_0 = new Point(e.X, e.Y);
            Rectangle rectangle = new Rectangle(0, 0, 0, 0);
            if ((e.X <= 0) || (e.Y <= 0))
            {
                this.rectangle_4 = rectangle;
            }
            if (!rectangle.Equals(this.rectangle_4))
            {
                if (!this.rectangle_4.Contains(new Point(e.X, e.Y)))
                {
                    this.method_7(e.X, e.Y + 20);
                }
            }
            else
            {
                this.method_7(e.X, e.Y + 20);
            }
            base.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            if ((mevent.Button == MouseButtons.Left) && base.ClientRectangle.Contains(this.point_0))
            {
                this.bool_2 = false;
                base.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(this.menuColorTable_0.ToolBorderColor)), base.ClientRectangle);
            this.list_0.Clear();
            if (this.navigateMenuNodeCollection_0.Count > 0)
            {
                int width = (base.Width - 0x2a) - 20;
                int num2 = width / 0x4e;
                int num3 = (((num2 + 1) * 0x4e) - 10) + 10;
                if (num3 > width)
                {
                    width = num2 * 0x4e;
                }
                this.rectangle_2 = new Rectangle(0x1f, 0, width, base.Height);
                e.Graphics.Clip = new Region(this.rectangle_2);
                int num4 = this.int_0;
                int num5 = (base.Height - 0x44) / 2;
                this.method_4(ref num4, num5, e, this.list_0);
                if ((num4 - 10) >= (this.rectangle_2.X + this.rectangle_2.Width))
                {
                    this.bool_1 = true;
                }
                else
                {
                    this.bool_1 = false;
                }
                e.Graphics.ResetClip();
            }
            if (this.int_0 < 0x1f)
            {
                this.bool_0 = true;
            }
            else
            {
                this.bool_0 = false;
            }
            this.method_6(e);
            if (this.bool_3)
            {
                this.method_3(e);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            LinearGradientBrush brush = new LinearGradientBrush(base.ClientRectangle, this.menuColorTable_0.ToolTopBackColor, this.menuColorTable_0.ToolBottomBackColor, 90f);
            e.Graphics.FillRectangle(brush, base.ClientRectangle);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            base.Height = 0x52;
            base.Invalidate();
        }

        public NavigateMenuNodeCollection Items
        {
            get
            {
                return this.navigateMenuNodeCollection_0;
            }
            set
            {
                if (this.navigateMenuNodeCollection_0 != value)
                {
                    this.navigateMenuNodeCollection_0.Clear();
                    this.navigateMenuNodeCollection_0 = value;
                    base.Invalidate();
                }
            }
        }
    }
}

