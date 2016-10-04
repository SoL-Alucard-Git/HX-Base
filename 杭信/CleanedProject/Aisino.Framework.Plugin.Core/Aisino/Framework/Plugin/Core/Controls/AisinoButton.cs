namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class AisinoButton : AisinoBTN
    {
        private int int_1;

        public AisinoButton()
        {
            
            this.int_1 = 0x19;
            base.Height = 0x1c;
            base.Width = 0x4b;
        }

        private void method_0(PaintEventArgs paintEventArgs_0)
        {
            Graphics graphics = paintEventArgs_0.Graphics;
            paintEventArgs_0.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle clientRectangle = base.ClientRectangle;
            paintEventArgs_0.Graphics.Clear(this.BackColor);
            GraphicsPath path = this.method_2(clientRectangle, 5f);
            if (base.ClientRectangle.Contains(base.PointToClient(Control.MousePosition)))
            {
                if (Control.MouseButtons == MouseButtons.Left)
                {
                    this.method_5(paintEventArgs_0.Graphics, path);
                }
                else
                {
                    this.method_3(paintEventArgs_0.Graphics, path);
                }
            }
            else
            {
                this.method_4(paintEventArgs_0.Graphics, path);
            }
            this.method_1(paintEventArgs_0.Graphics, path);
            if (base.Image != null)
            {
                this.method_6(paintEventArgs_0.Graphics);
            }
            this.method_7(paintEventArgs_0.Graphics);
        }

        private void method_1(Graphics graphics_0, GraphicsPath graphicsPath_0)
        {
            Pen pen = new Pen(Color.Gray, 1f);
            graphics_0.DrawPath(pen, graphicsPath_0);
            pen.Dispose();
        }

        private GraphicsPath method_2(RectangleF rectangleF_0, float float_0)
        {
            rectangleF_0.Inflate(-2f, -2f);
            float x = rectangleF_0.X;
            float y = rectangleF_0.Y;
            float width = rectangleF_0.Width;
            float height = rectangleF_0.Height;
            GraphicsPath path = new GraphicsPath();
            path.AddBezier(x, y + float_0, x, y, x + float_0, y, x + float_0, y);
            path.AddLine(x + float_0, y, (x + width) - float_0, y);
            path.AddBezier((x + width) - float_0, y, x + width, y, x + width, y + float_0, x + width, y + float_0);
            path.AddLine((float) (x + width), (float) (y + float_0), (float) (x + width), (float) ((y + height) - float_0));
            path.AddBezier((float) (x + width), (float) ((y + height) - float_0), (float) (x + width), (float) (y + height), (float) ((x + width) - float_0), (float) (y + height), (float) ((x + width) - float_0), (float) (y + height));
            path.AddLine((float) ((x + width) - float_0), (float) (y + height), (float) (x + float_0), (float) (y + height));
            path.AddBezier(x + float_0, y + height, x, y + height, x, (y + height) - float_0, x, (y + height) - float_0);
            path.AddLine(x, (y + height) - float_0, x, y + float_0);
            return path;
        }

        private void method_3(Graphics graphics_0, GraphicsPath graphicsPath_0)
        {
            using (PathGradientBrush brush = new PathGradientBrush(graphicsPath_0))
            {
                brush.CenterColor = Color.Blue;
                brush.SurroundColors = new Color[] { Color.Black };
                graphics_0.FillPath(brush, graphicsPath_0);
            }
        }

        private void method_4(Graphics graphics_0, GraphicsPath graphicsPath_0)
        {
            Color[] colorArray2 = new Color[] { Color.LightGray, Color.Black, Color.Black, Color.Gray };
            float[] numArray = new float[] { 0f, 0.58f, 0.6f, 1f };
            ColorBlend blend = new ColorBlend {
                Colors = colorArray2,
                Positions = numArray
            };
            LinearGradientBrush brush = new LinearGradientBrush(base.ClientRectangle, Color.Gray, Color.Black, LinearGradientMode.Vertical) {
                InterpolationColors = blend
            };
            graphics_0.FillPath(brush, graphicsPath_0);
            brush.Dispose();
        }

        private void method_5(Graphics graphics_0, GraphicsPath graphicsPath_0)
        {
            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                graphics_0.FillPath(brush, graphicsPath_0);
            }
        }

        private void method_6(Graphics graphics_0)
        {
            if (this.TextAlign == ContentAlignment.BottomCenter)
            {
                Rectangle rect = new Rectangle((base.Width - this.int_1) / 2, base.Height / 8, this.int_1, this.int_1);
                graphics_0.DrawImage(base.Image, rect);
            }
            else if (this.TextAlign == ContentAlignment.MiddleRight)
            {
                Rectangle rectangle2 = new Rectangle(base.Width / 8, (base.Height - this.int_1) / 2, this.int_1, this.int_1);
                graphics_0.DrawImage(base.Image, rectangle2);
            }
        }

        private void method_7(Graphics graphics_0)
        {
            string text = this.Text;
            Font font = this.Font;
            Brush controlLightLight = SystemBrushes.ControlLightLight;
            StringFormat format = this.method_8(this.TextAlign);
            Rectangle layoutRectangle = new Rectangle(4, 4, base.Width - 8, base.Height - 8);
            graphics_0.DrawString(text, font, controlLightLight, layoutRectangle, format);
        }

        private StringFormat method_8(ContentAlignment contentAlignment_0)
        {
            ContentAlignment alignment2;
            StringFormat format = new StringFormat();
            ContentAlignment alignment = contentAlignment_0;
            if (alignment <= ContentAlignment.MiddleCenter)
            {
                switch (alignment)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.TopRight:
                        format.LineAlignment = StringAlignment.Near;
                        break;

                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.MiddleCenter:
                        goto Label_0051;
                }
            }
            else
            {
                if (alignment > ContentAlignment.BottomLeft)
                {
                    if ((alignment != ContentAlignment.BottomCenter) && (alignment != ContentAlignment.BottomRight))
                    {
                        goto Label_0073;
                    }
                    goto Label_006C;
                }
                switch (alignment)
                {
                    case ContentAlignment.MiddleRight:
                        goto Label_0051;

                    case ContentAlignment.BottomLeft:
                        goto Label_006C;
                }
            }
            goto Label_0073;
        Label_0051:
            format.LineAlignment = StringAlignment.Center;
            goto Label_0073;
        Label_006C:
            format.LineAlignment = StringAlignment.Far;
        Label_0073:
            alignment2 = contentAlignment_0;
            if (alignment2 <= ContentAlignment.MiddleCenter)
            {
                switch (alignment2)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.MiddleLeft:
                        goto Label_00B5;

                    case ContentAlignment.TopCenter:
                    case ContentAlignment.MiddleCenter:
                        goto Label_00D9;

                    case (ContentAlignment.TopCenter | ContentAlignment.TopLeft):
                        return format;

                    case ContentAlignment.TopRight:
                        goto Label_00D0;
                }
                return format;
            }
            if (alignment2 > ContentAlignment.BottomLeft)
            {
                if (alignment2 == ContentAlignment.BottomCenter)
                {
                    goto Label_00D9;
                }
                if (alignment2 != ContentAlignment.BottomRight)
                {
                    return format;
                }
                goto Label_00D0;
            }
            if (alignment2 == ContentAlignment.MiddleRight)
            {
                goto Label_00D0;
            }
            if (alignment2 != ContentAlignment.BottomLeft)
            {
                return format;
            }
        Label_00B5:
            format.Alignment = StringAlignment.Near;
            return format;
        Label_00D0:
            format.Alignment = StringAlignment.Far;
            return format;
        Label_00D9:
            format.Alignment = StringAlignment.Center;
            return format;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.method_0(e);
        }

        [Description("图片大小")]
        public int Imagesize
        {
            get
            {
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
            }
        }
    }
}

