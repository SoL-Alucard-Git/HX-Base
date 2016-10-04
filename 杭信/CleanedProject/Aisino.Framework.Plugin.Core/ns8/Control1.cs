namespace ns8
{
    using Aisino.Framework.Plugin.Core.Controls;
    using ns13;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Threading;
    using System.Windows.Forms;

    internal class Control1 : Control
    {
        private bool bool_0;
        private bool bool_1;
        private bool bool_2;
        private Container container_0;
        private Font font_0;
        private Font font_1;
        private int int_0;
        private int int_1;
        private int int_2;
        private int int_3;
        private int int_4;
        private int int_5;
        private List<NavigateMenuNodeRect> list_0;
        private MenuColorTable menuColorTable_0;
        private NavigateMenuNode navigateMenuNode_0;
        private Point point_0;
        private Rectangle rectangle_0;
        private Rectangle rectangle_1;
        private Rectangle rectangle_2;
        private Rectangle rectangle_3;
        private Rectangle rectangle_4;
        private ToolTip toolTip_0;

        internal event Delegate37 Event_0;

        internal event Delegate34 Event_1;

        public Control1()
        {
            
            this.int_0 = 0x20;
            this.int_1 = 0x20;
            this.int_2 = 0x18;
            this.int_3 = 0x1a;
            this.int_4 = 12;
            this.int_5 = 0x2c;
            this.font_0 = new Font("宋体", 11f);
            this.font_1 = new Font("宋体", 10f);
            this.toolTip_0 = new ToolTip();
            this.rectangle_4 = new Rectangle(0, 0, 0, 0);
            this.method_0();
            this.method_3();
            this.list_0 = new List<NavigateMenuNodeRect>();
            this.menuColorTable_0 = new MenuColorTable();
            this.point_0 = new Point(-1, -1);
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

        public NavigateMenuNode method_1()
        {
            return this.navigateMenuNode_0;
        }

        private void method_10(NavigateMenuNode navigateMenuNode_1)
        {
            if (((navigateMenuNode_1 != null) && (navigateMenuNode_1.Node != null)) && (navigateMenuNode_1.Node.Count > 0))
            {
                foreach (NavigateMenuNode node in navigateMenuNode_1.Node)
                {
                    node.IsExpand = false;
                    if ((node.Node != null) && (node.Node.Count > 0))
                    {
                        this.method_10(node);
                    }
                }
            }
        }

        public void method_2(NavigateMenuNode navigateMenuNode_1)
        {
            if (this.navigateMenuNode_0 != navigateMenuNode_1)
            {
                this.navigateMenuNode_0 = navigateMenuNode_1;
                this.int_5 = 0x2c;
                base.Invalidate();
            }
        }

        private void method_3()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.UpdateStyles();
        }

        private void method_4(PaintEventArgs paintEventArgs_0)
        {
            Image image = null;
            if (this.bool_0)
            {
                image = Class131.smethod_28();
            }
            else
            {
                image = Class131.smethod_25();
            }
            int x = (this.int_0 - image.Width) / 2;
            int y = ((this.int_1 - 2) - image.Height) / 2;
            this.rectangle_1 = new Rectangle(0, 0, this.int_0, this.int_1);
            if (this.rectangle_1.Contains(this.point_0))
            {
                paintEventArgs_0.Graphics.DrawImage(Class131.smethod_29(), new Rectangle(1, 1, this.int_0 - 2, this.int_1 - 2));
            }
            else
            {
                paintEventArgs_0.Graphics.FillRectangle(new SolidBrush(this.menuColorTable_0.MenuDarkBackColor), new Rectangle(1, 1, this.int_0 - 2, this.int_1 - 2));
            }
            paintEventArgs_0.Graphics.DrawImage(image, new Rectangle(x, y, image.Width, image.Height));
        }

        private void method_5(PaintEventArgs paintEventArgs_0, int int_6, ref int int_7, NavigateMenuNode navigateMenuNode_1, List<NavigateMenuNodeRect> nodeList)
        {
            for (int i = 0; i < navigateMenuNode_1.Node.Count; i++)
            {
                Rectangle rectangle = new Rectangle(this.int_0 + 1, int_7, (base.Width - this.int_0) - 3, this.int_3 - 1);
                nodeList.Add(new NavigateMenuNodeRect(navigateMenuNode_1.Node[i], rectangle));
                Color menuBackColor = this.menuColorTable_0.MenuBackColor;
                if (rectangle.Contains(this.point_0))
                {
                    menuBackColor = this.menuColorTable_0.MenuMouseHover;
                    if (this.bool_1 && this.rectangle_2.Contains(this.point_0))
                    {
                        menuBackColor = this.menuColorTable_0.MenuBackColor;
                    }
                    if (this.bool_2 && this.rectangle_3.Contains(this.point_0))
                    {
                        menuBackColor = this.menuColorTable_0.MenuBackColor;
                    }
                }
                paintEventArgs_0.Graphics.FillRectangle(new SolidBrush(menuBackColor), rectangle);
                string text = this.method_6(paintEventArgs_0, navigateMenuNode_1.Node[i].SimpleText, base.Width - int_6);
                SizeF ef = paintEventArgs_0.Graphics.MeasureString(text, this.font_1);
                paintEventArgs_0.Graphics.DrawString(text, this.font_1, new SolidBrush(this.menuColorTable_0.MenuFont), new PointF((float) int_6, (((float) int_7) + ((this.int_3 - ef.Height) / 2f)) + 2f));
                if ((navigateMenuNode_1.Node[i].Node != null) && (navigateMenuNode_1.Node[i].Node.Count > 0))
                {
                    Image image;
                    int num2 = int_6 + ((int) ef.Width);
                    if (navigateMenuNode_1.Node[i].IsExpand)
                    {
                        image = Class131.smethod_27();
                    }
                    else
                    {
                        image = Class131.smethod_26();
                    }
                    paintEventArgs_0.Graphics.DrawImage(image, new Point(num2 + ((this.int_3 - image.Width) / 2), int_7 + ((this.int_3 - image.Width) / 2)));
                }
                int_7 += this.int_3;
                if (navigateMenuNode_1.Node[i].IsExpand)
                {
                    this.method_5(paintEventArgs_0, int_6 + this.int_2, ref int_7, navigateMenuNode_1.Node[i], nodeList);
                }
            }
        }

        private string method_6(PaintEventArgs paintEventArgs_0, string string_0, int int_6)
        {
            string str = string.Empty;
            if (paintEventArgs_0.Graphics.MeasureString(string_0, this.font_1).Width <= (int_6 - this.int_3))
            {
                return string_0;
            }
            for (int i = string_0.Length - 1; i >= 0; i--)
            {
                string text = string_0.Substring(0, i) + "…";
                if (paintEventArgs_0.Graphics.MeasureString(text, this.font_1).Width <= (int_6 - this.int_3))
                {
                    return text;
                }
            }
            return str;
        }

        private void method_7(int int_6, int int_7)
        {
            Point pt = new Point(int_6, int_7);
            foreach (NavigateMenuNodeRect rect in this.list_0)
            {
                if (rect.Rect.Contains(pt))
                {
                    this.rectangle_4 = rect.Rect;
                    this.toolTip_0.Show(rect.Node.SimpleText, this, int_6, int_7, 0xbb8);
                }
            }
        }

        private void method_8(object sender, EventArgs0 e)
        {
            if (this.Event_1 != null)
            {
                this.Event_1(this, e);
            }
        }

        private void method_9(object object_0, Class122 class122_0)
        {
            if (this.Event_0 != null)
            {
                this.Event_0(object_0, class122_0);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                if (this.rectangle_1.Contains(this.point_0))
                {
                    this.bool_0 = !this.bool_0;
                    this.method_9(this, new Class122(this.bool_0));
                    base.Invalidate();
                }
                if (this.rectangle_2.Contains(this.point_0) && this.bool_1)
                {
                    this.int_5 += this.int_3;
                    base.Invalidate();
                }
                else if (this.rectangle_3.Contains(this.point_0) && this.bool_2)
                {
                    this.int_5 -= this.int_3;
                    base.Invalidate();
                }
                else if (this.rectangle_0.Contains(this.point_0))
                {
                    using (List<NavigateMenuNodeRect>.Enumerator enumerator = this.list_0.GetEnumerator())
                    {
                        NavigateMenuNodeRect current;
                        while (enumerator.MoveNext())
                        {
                            current = enumerator.Current;
                            if (current.Rect.Contains(this.point_0))
                            {
                                goto Label_0109;
                            }
                        }
                        return;
                    Label_0109:
                        current.Node.IsExpand = !current.Node.IsExpand;
                        if (!current.Node.IsExpand)
                        {
                            this.method_10(current.Node);
                        }
                        if ((current.Node.Node == null) || (current.Node.Node.Count <= 0))
                        {
                            this.method_8(this, new EventArgs0(current.Node));
                        }
                        base.Invalidate();
                    }
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.point_0 = new Point(-1, -1);
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
                    this.method_7(e.X + 20, e.Y);
                }
            }
            else
            {
                this.method_7(e.X + 20, e.Y);
            }
            base.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            this.rectangle_2 = new Rectangle(base.Width - this.int_3, this.int_1, this.int_3, this.int_3);
            this.rectangle_3 = new Rectangle(base.Width - this.int_3, base.Height - this.int_3, this.int_3, this.int_3);
            e.Graphics.FillRectangle(new SolidBrush(this.menuColorTable_0.MenuBackColor), new Rectangle(-1, -1, base.Width + 1, base.Height + 1));
            e.Graphics.FillRectangle(new SolidBrush(this.menuColorTable_0.MenuDarkBackColor), new Rectangle(0, 0, this.int_0, base.Height));
            e.Graphics.FillRectangle(new SolidBrush(this.menuColorTable_0.MenuDarkBackColor), new Rectangle(0, 0, base.Width, this.int_1));
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(this.menuColorTable_0.MenuBoderOuter)), new Rectangle(0, 0, base.Width - 1, base.Height));
            e.Graphics.DrawLine(new Pen(new SolidBrush(this.menuColorTable_0.MenuBoderOuter)), new Point(this.int_0, 0), new Point(this.int_0, base.Height));
            e.Graphics.DrawLine(new Pen(new SolidBrush(this.menuColorTable_0.MenuBoderOuter)), new Point(0, this.int_0), new Point(base.Width, this.int_0));
            if (this.bool_0)
            {
                StringFormat format = new StringFormat {
                    Alignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.DirectionVertical
                };
                e.Graphics.DrawString("导航菜单", this.font_0, new SolidBrush(this.menuColorTable_0.MenuFont), new RectangleF(0f, (float) this.int_1, (float) this.int_0, (float) (base.Height - this.int_1)), format);
            }
            this.method_4(e);
            if (base.Width > (this.int_0 + this.int_2))
            {
                string text = "导航栏";
                if (((this.navigateMenuNode_0 != null) && (this.navigateMenuNode_0.Text != null)) && !string.Empty.Equals(this.navigateMenuNode_0.Text))
                {
                    text = this.navigateMenuNode_0.Text;
                }
                SizeF ef = e.Graphics.MeasureString(text, this.font_0);
                int num4 = this.int_0 + this.int_2;
                int num5 = ((this.int_1 - ((int) ef.Height)) / 2) + 2;
                e.Graphics.DrawString(text, this.font_0, new SolidBrush(this.menuColorTable_0.MenuFont), (float) num4, (float) num5);
            }
            this.rectangle_0 = new Rectangle(0, 0, 0, 0);
            if (((this.navigateMenuNode_0 != null) && (base.Width > (this.int_0 + this.int_2))) && ((base.Height > (this.int_1 + this.int_4)) && this.navigateMenuNode_0.IsExpand))
            {
                int num = ((base.Height - this.int_1) - this.int_4) / this.int_3;
                if (num > 0)
                {
                    this.rectangle_0 = new Rectangle(this.int_0, this.int_1 + this.int_4, base.Width - this.int_0, num * this.int_3);
                    e.Graphics.Clip = new Region(this.rectangle_0);
                    int num2 = this.int_0 + this.int_2;
                    int num3 = this.int_5;
                    this.list_0.Clear();
                    this.method_5(e, num2, ref num3, this.navigateMenuNode_0, this.list_0);
                    e.Graphics.ResetClip();
                    if (this.int_5 >= (this.int_1 + this.int_4))
                    {
                        this.bool_1 = false;
                    }
                    else
                    {
                        this.bool_1 = true;
                    }
                    if (num3 > base.Height)
                    {
                        this.bool_2 = true;
                    }
                    else
                    {
                        this.bool_2 = false;
                    }
                    if (this.bool_1)
                    {
                        Image image2;
                        if (this.rectangle_2.Contains(this.point_0))
                        {
                            image2 = Class131.smethod_31();
                        }
                        else
                        {
                            image2 = Class131.smethod_32();
                        }
                        e.Graphics.DrawImage(image2, (int) ((base.Width - this.int_3) + ((this.int_3 - image2.Width) / 2)), (int) (this.int_1 + ((this.int_3 - image2.Height) / 2)));
                    }
                    if (this.bool_2)
                    {
                        Image image;
                        if (this.rectangle_3.Contains(this.point_0))
                        {
                            image = Class131.smethod_23();
                        }
                        else
                        {
                            image = Class131.smethod_24();
                        }
                        e.Graphics.DrawImage(image, (int) ((base.Width - this.int_3) + ((this.int_3 - image.Width) / 2)), (int) ((base.Height - this.int_3) + ((this.int_3 - image.Height) / 2)));
                    }
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (base.Width < this.int_0)
            {
                base.Width = this.int_0;
            }
            base.Invalidate();
        }
    }
}

