namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class AisinoBTN : Button
    {
        private bool bool_0;
        private bool bool_1;
        private Color color_0;
        private Color color_1;
        private Color color_2;
        private Color color_3;
        private static int int_0;

        static AisinoBTN()
        {
            
            int_0 = 1;
        }

        public AisinoBTN()
        {
            
            this.color_0 = Color.FromArgb(0x19, 0x76, 210);
            this.color_1 = Color.FromArgb(0, 0xac, 0xfb);
            this.color_2 = Color.FromArgb(0, 0x91, 0xe0);
            this.color_3 = Color.FromArgb(0xff, 0xff, 0xff);
            this.Font = new Font("宋体", 9f);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.bool_1 = true;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.bool_0 = true;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.bool_0 = false;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                this.bool_1 = false;
                this.Refresh();
            }
            base.OnMouseUp(mevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            using (GraphicsPath path = smethod_0(this.DisplayRectangle))
            {
                Brush brush;
                if (base.Enabled)
                {
                    if (this.bool_0 && !this.bool_1)
                    {
                        brush = new LinearGradientBrush(new Point(this.DisplayRectangle.X, this.DisplayRectangle.Y), new Point(this.DisplayRectangle.X, this.DisplayRectangle.Bottom + 1), this.color_1, this.color_2);
                    }
                    else
                    {
                        brush = new SolidBrush(this.color_0);
                    }
                }
                else
                {
                    brush = new SolidBrush(Color.FromArgb(0xc0, 0xc0, 0xb8));
                }
                e.Graphics.FillPath(brush, path);
                if (this.Focused)
                {
                    Pen pen = new Pen(Color.White) {
                        DashStyle = DashStyle.Solid,
                        Width = 1f
                    };
                    Rectangle rect = new Rectangle {
                        X = this.DisplayRectangle.X + 1,
                        Y = this.DisplayRectangle.Y + 1,
                        Width = this.DisplayRectangle.Width - 3,
                        Height = this.DisplayRectangle.Height - 3
                    };
                    e.Graphics.DrawRectangle(pen, rect);
                }
            }
            StringFormat format = new StringFormat {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter
            };
            Rectangle displayRectangle = this.DisplayRectangle;
            if (!this.bool_1)
            {
                e.Graphics.DrawString(this.Text, new Font("微软雅黑", 10f, FontStyle.Bold), new SolidBrush(this.color_3), displayRectangle, format);
            }
            else
            {
                displayRectangle.X++;
                displayRectangle.Y++;
                e.Graphics.DrawString(this.Text, new Font("微软雅黑", 10f, FontStyle.Bold), new SolidBrush(this.color_3), displayRectangle, format);
            }
        }

        private static GraphicsPath smethod_0(Rectangle rectangle_0)
        {
            GraphicsPath path = new GraphicsPath();
            int left = rectangle_0.Left;
            int top = rectangle_0.Top;
            int width = rectangle_0.Width;
            int height = rectangle_0.Height;
            path.AddLine(left + int_0, top, width - int_0, top);
            path.AddLine(width - int_0, top, width, top + int_0);
            path.AddLine(width, top + int_0, width, (height - int_0) - 1);
            path.AddLine(width, (height - int_0) - 1, (width - int_0) - 1, height);
            path.AddLine((width - int_0) - 1, height, (left + int_0) + 1, height);
            path.AddLine((left + int_0) + 1, height, left, (height - int_0) - 1);
            path.AddLine(left, (height - int_0) - 1, left, top + int_0);
            path.AddLine(left, top + int_0, left + int_0, top);
            return path;
        }

        public Color BackColorActive
        {
            get
            {
                return this.color_0;
            }
            set
            {
                if (!this.color_0.Equals(value))
                {
                    this.color_0 = value;
                    base.Invalidate();
                }
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
                base.Invalidate();
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
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "255, 255, 255")]
        public Color FontColor
        {
            get
            {
                return this.color_3;
            }
            set
            {
                this.color_3 = value;
                base.Invalidate();
            }
        }
    }
}

