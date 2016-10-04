namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class ButtonPw : Button
    {
        private bool bool_0;
        private bool bool_1;
        private Color color_0;
        private Color color_1;
        private IContainer icontainer_0;
        private static int int_0;

        static ButtonPw()
        {
            
            int_0 = 1;
        }

        public ButtonPw()
        {
            
            this.color_0 = Color.FromArgb(0x21, 150, 0xf3);
            this.color_1 = Color.FromArgb(0x19, 0x76, 210);
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.Name = "ButtonPw";
            base.Size = new Size(0x8b, 0x39);
            base.ResumeLayout(false);
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
            e.Graphics.Clear(Color.White);
            using (GraphicsPath path = smethod_0(this.DisplayRectangle))
            {
                Brush brush;
                if (base.Enabled)
                {
                    if (this.bool_0 && !this.bool_1)
                    {
                        brush = new SolidBrush(this.color_1);
                    }
                    else
                    {
                        brush = new SolidBrush(this.color_0);
                    }
                }
                else
                {
                    brush = new SolidBrush(Color.FromArgb(180, 180, 180));
                }
                e.Graphics.FillPath(brush, path);
                if (this.Focused && this.ShowFocusCues)
                {
                    Pen pen = new Pen(Color.White) {
                        DashStyle = DashStyle.Dot
                    };
                    Rectangle rect = new Rectangle {
                        X = this.DisplayRectangle.X + 2,
                        Y = this.DisplayRectangle.Y + 2,
                        Width = this.DisplayRectangle.Width - 5,
                        Height = this.DisplayRectangle.Height - 5
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
                e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), displayRectangle, format);
            }
            else
            {
                displayRectangle.X++;
                displayRectangle.Y++;
                e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), displayRectangle, format);
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
                return this.color_1;
            }
            set
            {
                if (!this.color_1.Equals(value))
                {
                    this.color_1 = value;
                }
            }
        }

        public Color BackColorNormal
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
                }
            }
        }
    }
}

