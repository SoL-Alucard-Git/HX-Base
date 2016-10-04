namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class TabControlSkin : AisinoTAB
    {
        private bool bool_0;
        private Color color_0;
        private Color color_1;
        private Color color_2;
        private Color color_3;
        private Color color_4;
        private IContainer icontainer_0;
        private static readonly int int_0;
        public TabCloseEventHandler OnCloseing;

        static TabControlSkin()
        {
            
            int_0 = 6;
        }

        public TabControlSkin()
        {
            
            this.color_0 = Color.FromArgb(0x9d, 0xa2, 0xa8);
            this.color_1 = Color.FromArgb(250, 250, 250);
            this.color_2 = Color.FromArgb(0xe0, 0xe0, 0xe0);
            this.color_3 = Color.FromArgb(0xd5, 0xe8, 0xf5);
            this.color_4 = Color.FromArgb(0xae, 0xc4, 0xd4);
            this.method_5();
            this.method_0();
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
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            base.UpdateStyles();
        }

        private void method_1(Graphics graphics_0, Rectangle rectangle_0, TabPage tabPage_0, GraphicsPath graphicsPath_0)
        {
            if (tabPage_0 != null)
            {
                StringFormat format = new StringFormat {
                    Trimming = StringTrimming.EllipsisCharacter,
                    FormatFlags = StringFormatFlags.NoWrap
                };
                Rectangle layoutRectangle = new Rectangle(rectangle_0.X + 5, rectangle_0.Y + 5, rectangle_0.Width - 10, tabPage_0.Font.Height);
                if (base.SelectedTab.Equals(tabPage_0))
                {
                    layoutRectangle = new Rectangle(rectangle_0.X + 5, rectangle_0.Y + 5, rectangle_0.Width - 0x19, rectangle_0.Height - 7);
                    graphics_0.FillPath(new LinearGradientBrush(new Point(rectangle_0.X, rectangle_0.Y), new Point(rectangle_0.X, rectangle_0.Bottom + 1), this.color_3, this.color_4), graphicsPath_0);
                    graphics_0.DrawString(tabPage_0.Text, tabPage_0.Font, new SolidBrush(tabPage_0.ForeColor), layoutRectangle, format);
                    graphics_0.DrawPath(new Pen(this.color_0), graphicsPath_0);
                    if (!"首页".Equals(tabPage_0.Text.Trim()))
                    {
                        Rectangle rect = smethod_0(rectangle_0);
                        if (this.bool_0)
                        {
                            graphics_0.DrawImage(Class131.smethod_42(), rect);
                        }
                        else
                        {
                            graphics_0.DrawImage(Class131.smethod_41(), rect);
                        }
                    }
                }
                else
                {
                    graphics_0.FillPath(new LinearGradientBrush(new Point(rectangle_0.X, rectangle_0.Y), new Point(rectangle_0.X, rectangle_0.Bottom + 1), this.color_1, this.color_2), graphicsPath_0);
                    graphics_0.DrawString(tabPage_0.Text, tabPage_0.Font, new SolidBrush(tabPage_0.ForeColor), layoutRectangle, format);
                    graphics_0.DrawPath(new Pen(this.color_0), graphicsPath_0);
                }
            }
        }

        private bool method_2(Point point_0)
        {
            return ((base.TabCount > 0) && smethod_0(base.GetTabRect(base.SelectedIndex)).Contains(point_0));
        }

        private GraphicsPath method_3(Rectangle rectangle_0)
        {
            GraphicsPath path = new GraphicsPath();
            rectangle_0.Width--;
            path.AddLine(rectangle_0.X + int_0, rectangle_0.Y, rectangle_0.Right - (int_0 / 2), rectangle_0.Y);
            path.AddArc(rectangle_0.Right - int_0, rectangle_0.Y, int_0, int_0, 270f, 90f);
            path.AddLine(rectangle_0.Right, rectangle_0.Y + (int_0 / 2), rectangle_0.Right, rectangle_0.Bottom + 1);
            path.AddLine(rectangle_0.Right, rectangle_0.Bottom + 1, rectangle_0.X - int_0, rectangle_0.Bottom + 1);
            path.CloseFigure();
            return path;
        }

        private GraphicsPath method_4(Rectangle rectangle_0)
        {
            GraphicsPath path = new GraphicsPath();
            rectangle_0.Width--;
            path.AddLine(rectangle_0.X + int_0, rectangle_0.Y, rectangle_0.Right - (int_0 / 2), rectangle_0.Y);
            path.AddArc(rectangle_0.Right - int_0, rectangle_0.Y, int_0, int_0, 270f, 90f);
            path.AddLine(rectangle_0.Right, rectangle_0.Y + (int_0 / 2), rectangle_0.Right, rectangle_0.Bottom + 1);
            path.AddLine(rectangle_0.Right, rectangle_0.Bottom + 1, rectangle_0.X - 1, rectangle_0.Bottom + 1);
            path.AddLine((int) (rectangle_0.X - 1), (int) (rectangle_0.Bottom + 1), (int) (rectangle_0.X - 1), (int) (rectangle_0.Y + (rectangle_0.Height / 2)));
            path.CloseFigure();
            return path;
        }

        private void method_5()
        {
            this.icontainer_0 = new Container();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (!"首页".Equals(base.SelectedTab.Text.Trim()))
            {
                Point position = Cursor.Position;
                position = base.PointToClient(position);
                if (smethod_0(base.GetTabRect(base.SelectedIndex)).Contains(position))
                {
                    CancelEventArgs args = new CancelEventArgs();
                    this.OnCloseMethod(this, args);
                    if (!args.Cancel)
                    {
                        base.TabPages.Remove(base.SelectedTab);
                    }
                }
            }
        }

        protected virtual void OnCloseMethod(object sender, CancelEventArgs e)
        {
            if (this.OnCloseing != null)
            {
                this.OnCloseing(sender, e);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Point point = base.PointToClient(Cursor.Position);
            if (this.method_2(point) != this.bool_0)
            {
                this.bool_0 = !this.bool_0;
                base.Invalidate();
            }
            base.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.method_2(e.Location) != this.bool_0)
            {
                this.bool_0 = !this.bool_0;
                base.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            int height = base.ItemSize.Height;
            Rectangle rect = new Rectangle(this.DisplayRectangle.X - 1, this.DisplayRectangle.Y - 1, this.DisplayRectangle.Width + 1, this.DisplayRectangle.Height + 1);
            e.Graphics.DrawRectangle(new Pen(this.color_0), rect);
            if (base.TabCount > 0)
            {
                foreach (TabPage page in base.TabPages)
                {
                    if (!base.SelectedTab.Equals(page))
                    {
                        Rectangle tabRect = base.GetTabRect(base.TabPages.IndexOf(page));
                        this.method_1(e.Graphics, tabRect, page, this.method_4(tabRect));
                    }
                }
                if (base.SelectedIndex == 0)
                {
                    Rectangle rectangle7 = base.GetTabRect(base.TabPages.IndexOf(base.SelectedTab));
                    this.method_1(e.Graphics, rectangle7, base.SelectedTab, this.method_4(rectangle7));
                }
                else
                {
                    this.method_1(e.Graphics, base.GetTabRect(base.SelectedIndex), base.SelectedTab, this.method_3(base.GetTabRect(base.SelectedIndex)));
                }
            }
        }

        private static Rectangle smethod_0(Rectangle rectangle_0)
        {
            return new Rectangle((rectangle_0.X + rectangle_0.Width) - 20, ((rectangle_0.Y + 2) + ((rectangle_0.Height - 2) / 2)) - 8, 0x10, 0x10);
        }

        public Color ColorActivateA
        {
            get
            {
                return this.color_3;
            }
            set
            {
                this.color_3 = value;
            }
        }

        public Color ColorActivateB
        {
            get
            {
                return this.color_4;
            }
            set
            {
                this.color_4 = value;
            }
        }

        public Color ColorDefaultA
        {
            get
            {
                return this.color_1;
            }
            set
            {
                this.color_1 = value;
            }
        }

        public Color ColorDefaultB
        {
            get
            {
                return this.color_2;
            }
            set
            {
                this.color_2 = value;
            }
        }

        public Color ColorLine
        {
            get
            {
                return this.color_0;
            }
            set
            {
                this.color_0 = value;
            }
        }
    }
}

