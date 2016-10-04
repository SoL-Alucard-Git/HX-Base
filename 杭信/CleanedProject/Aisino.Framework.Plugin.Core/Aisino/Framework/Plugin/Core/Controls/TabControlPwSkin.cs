namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class TabControlPwSkin : AisinoTAB
    {
        private bool bool_0;
        private bool bool_1;
        private bool bool_2;
        private Color color_0;
        private Color color_1;
        private Color color_2;
        private Font font_0;
        private Font font_1;
        private IContainer icontainer_0;
        public TabCloseEventHandler OnCloseing;

        public TabControlPwSkin()
        {
            
            this.color_0 = Color.FromArgb(0xda, 0xe1, 0xe8);
            this.font_0 = new Font("微软雅黑", 10f, FontStyle.Bold);
            this.font_1 = new Font("微软雅黑", 10f, FontStyle.Bold);
            this.color_1 = Color.White;
            this.color_2 = Color.FromArgb(240, 240, 240);
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
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                };
                Rectangle tabRect = base.GetTabRect(base.TabPages.IndexOf(tabPage_0));
                if (base.SelectedTab.Equals(tabPage_0))
                {
                    Rectangle rect = new Rectangle {
                        Location = rectangle_0.Location
                    };
                    rect.Y++;
                    rect.Width = rectangle_0.Width + 3;
                    rect.Height = rectangle_0.Height;
                    graphics_0.FillRectangle(new SolidBrush(this.color_1), rect);
                    this.font_1 = new Font(this.font_0.Name, this.font_0.Size + 1f, FontStyle.Bold);
                    graphics_0.DrawString(tabPage_0.Text, this.font_1, new SolidBrush(Color.Blue), tabRect, format);
                    graphics_0.DrawPath(new Pen(this.color_0), graphicsPath_0);
                    GraphicsPath path = new GraphicsPath();
                    path.AddLine(rectangle_0.X, rectangle_0.Y + 1, rectangle_0.X, rectangle_0.Bottom + 1);
                    Pen pen = new Pen(Color.FromArgb(0, 0xa8, 0xf7)) {
                        Width = 3f
                    };
                    graphics_0.DrawPath(pen, path);
                    if (!"首页".Equals(tabPage_0.Text.Trim()) && this.bool_1)
                    {
                        smethod_0(rectangle_0);
                    }
                }
                else
                {
                    graphics_0.DrawString(tabPage_0.Text, this.font_0, new SolidBrush(tabPage_0.ForeColor), tabRect, format);
                }
            }
        }

        private bool method_2(Point point_0)
        {
            return ((base.TabCount > 0) && smethod_0(base.GetTabRect(base.SelectedIndex)).Contains(point_0));
        }

        private GraphicsPath method_3(Rectangle rectangle_0)
        {
            rectangle_0.Y++;
            rectangle_0.Width += 2;
            GraphicsPath path = new GraphicsPath();
            path.AddLine(rectangle_0.X, rectangle_0.Y, rectangle_0.Right, rectangle_0.Y);
            path.AddLine(rectangle_0.X, rectangle_0.Y, rectangle_0.X, rectangle_0.Bottom);
            path.AddLine(rectangle_0.X, rectangle_0.Bottom, rectangle_0.Right, rectangle_0.Bottom);
            return path;
        }

        private GraphicsPath method_4(Rectangle rectangle_0)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine(rectangle_0.X, rectangle_0.Y, rectangle_0.Right, rectangle_0.Y);
            path.AddLine(rectangle_0.Right, rectangle_0.Y, rectangle_0.Right, rectangle_0.Bottom);
            path.AddLine(rectangle_0.Right, rectangle_0.Bottom, rectangle_0.X, rectangle_0.Bottom);
            path.AddLine(rectangle_0.X, rectangle_0.Y, rectangle_0.X, rectangle_0.Bottom);
            return path;
        }

        private void method_5()
        {
            this.icontainer_0 = new Container();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (!"首页".Equals(base.SelectedTab.Text.Trim()) && this.bool_1)
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
            if (this.method_2(point) != this.bool_2)
            {
                this.bool_2 = !this.bool_2;
                base.Invalidate();
            }
            base.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.method_2(e.Location) != this.bool_2)
            {
                this.bool_2 = !this.bool_2;
                base.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            int height = base.ItemSize.Height;
            Rectangle rectangle = new Rectangle(this.DisplayRectangle.X - 1, this.DisplayRectangle.Y - 1, this.DisplayRectangle.Width + 1, this.DisplayRectangle.Height + 1);
            GraphicsPath path = this.method_4(rectangle);
            e.Graphics.DrawPath(new Pen(this.color_0), path);
            Rectangle rect = new Rectangle {
                Location = base.GetTabRect(0).Location
            };
            rect.X--;
            rect.Y++;
            rect.Width = base.GetTabRect(0).Width + 1;
            rect.Height = this.DisplayRectangle.Height + 1;
            e.Graphics.FillRectangle(new SolidBrush(this.color_2), rect);
            if (base.TabCount > 0)
            {
                foreach (TabPage page in base.TabPages)
                {
                    if (!base.SelectedTab.Equals(page))
                    {
                        Rectangle tabRect = base.GetTabRect(base.TabPages.IndexOf(page));
                        this.method_1(e.Graphics, tabRect, page, null);
                    }
                    else
                    {
                        this.method_1(e.Graphics, base.GetTabRect(base.SelectedIndex), base.SelectedTab, this.method_3(base.GetTabRect(base.SelectedIndex)));
                    }
                }
            }
        }

        private static Rectangle smethod_0(Rectangle rectangle_0)
        {
            return new Rectangle((rectangle_0.X + rectangle_0.Width) - 20, ((rectangle_0.Y + 2) + ((rectangle_0.Height - 2) / 2)) - 8, 0x10, 0x10);
        }

        public bool CloButton
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                this.bool_1 = value;
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

        public bool DelButton
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public Color SelectedTabColor
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

        public Color TabColor
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

        public Font TabFont
        {
            get
            {
                return this.font_0;
            }
            set
            {
                this.font_0 = value;
                this.font_1 = new Font(this.font_0.Name, this.font_0.Size + 1f, FontStyle.Bold);
            }
        }
    }
}

