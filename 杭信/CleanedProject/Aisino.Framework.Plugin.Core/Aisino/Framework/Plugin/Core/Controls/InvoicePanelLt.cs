namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class InvoicePanelLt : UserControl
    {
        private Color color_0;
        public PointF DataLoca;
        private float float_0;
        private float float_1;
        private float float_2;
        private float float_3;
        private float float_4;
        private float float_5;
        private float float_6;
        private Font font_0;
        private IContainer icontainer_0;
        private int int_0;

        public InvoicePanelLt()
        {
            
            this.float_2 = 120f;
            this.float_3 = 60f;
            this.float_4 = 60f;
            this.float_5 = 110f;
            this.float_6 = 50f;
            this.font_0 = new Font("楷体_GB2312", 8f);
            this.color_0 = Color.Red;
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
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ActiveCaptionText;
            base.BorderStyle = BorderStyle.FixedSingle;
            base.Name = "InvoicePanel";
            base.Size = new Size(860, 0x24b);
            base.ResumeLayout(false);
        }

        private void method_0(Graphics graphics_0)
        {
            float num = 18f * this.float_1;
            PointF tf = new PointF(8f * this.float_0, 18f * this.float_1);
            PointF tf2 = new PointF(92f * this.float_0, 18f * this.float_1);
            PointF tf3 = new PointF(8f * this.float_0, 90f * this.float_1);
            PointF tf4 = new PointF(92f * this.float_0, 90f * this.float_1);
            float num2 = 16f * this.float_1;
            PointF tf5 = new PointF(tf.X, tf.Y + num2);
            PointF tf6 = new PointF(tf2.X, tf2.Y + num2);
            float num3 = 35f * this.float_1;
            PointF tf7 = this.method_2(tf, 0f, num2 + num3);
            PointF tf8 = this.method_2(tf2, 0f, num2 + num3);
            float num4 = 5f * this.float_1;
            PointF tf9 = this.method_2(tf, 0f, (num2 + num3) + num4);
            PointF tf10 = this.method_2(tf2, 0f, (num2 + num3) + num4);
            float num5 = 4f * this.float_0;
            PointF tf11 = this.method_2(tf, num5, 0f);
            PointF tf12 = this.method_2(tf5, num5, 0f);
            PointF tf13 = this.method_2(tf9, num5, 0f);
            PointF tf14 = this.method_2(tf3, num5, 0f);
            float num6 = 45f * this.float_0;
            PointF tf15 = this.method_2(tf, num5 + num6, 0f);
            PointF tf16 = this.method_2(tf5, num5 + num6, 0f);
            PointF tf17 = this.method_2(tf9, num5 + num6, 0f);
            PointF tf18 = this.method_2(tf3, num5 + num6, 0f);
            float num7 = 3f * this.float_0;
            PointF tf19 = this.method_2(tf, (num5 + num6) + num7, 0f);
            PointF tf20 = this.method_2(tf5, (num5 + num6) + num7, 0f);
            PointF tf21 = this.method_2(tf9, (num5 + num6) + num7, 0f);
            PointF tf22 = this.method_2(tf3, (num5 + num6) + num7, 0f);
            float num8 = 22f * this.float_0;
            PointF tf23 = this.method_2(tf5, num8, 0f);
            PointF tf24 = this.method_2(tf9, num8, 0f);
            float num9 = 10f * this.float_0;
            PointF tf25 = this.method_2(tf5, num8 + num9, 0f);
            PointF tf26 = this.method_2(tf7, num8 + num9, 0f);
            float num10 = 4f * this.float_0;
            PointF tf27 = this.method_2(tf5, (num8 + num9) + num10, 0f);
            PointF tf28 = this.method_2(tf7, (num8 + num9) + num10, 0f);
            float num11 = 8f * this.float_0;
            PointF tf29 = this.method_2(tf5, ((num8 + num9) + num10) + num11, 0f);
            PointF tf30 = this.method_2(tf7, ((num8 + num9) + num10) + num11, 0f);
            float num12 = 9f * this.float_0;
            PointF tf31 = this.method_2(tf5, (((num8 + num9) + num10) + num11) + num12, 0f);
            PointF tf32 = this.method_2(tf7, (((num8 + num9) + num10) + num11) + num12, 0f);
            float num13 = 13f * this.float_0;
            PointF tf33 = this.method_2(tf5, ((((num8 + num9) + num10) + num11) + num12) + num13, 0f);
            PointF tf34 = this.method_2(tf7, ((((num8 + num9) + num10) + num11) + num12) + num13, 0f);
            float num14 = 4f * this.float_0;
            PointF tf35 = this.method_2(tf5, (((((num8 + num9) + num10) + num11) + num12) + num13) + num14, 0f);
            PointF tf36 = this.method_2(tf7, (((((num8 + num9) + num10) + num11) + num12) + num13) + num14, 0f);
            using (Pen pen = new Pen(Color.Black))
            {
                graphics_0.DrawLine(pen, tf, tf2);
                graphics_0.DrawLine(pen, tf2, tf4);
                graphics_0.DrawLine(pen, tf, tf3);
                graphics_0.DrawLine(pen, tf3, tf4);
                graphics_0.DrawLine(pen, tf5, tf6);
                graphics_0.DrawLine(pen, tf7, tf8);
                graphics_0.DrawLine(pen, tf9, tf10);
                graphics_0.DrawLine(pen, tf11, tf12);
                graphics_0.DrawLine(pen, tf13, tf14);
                graphics_0.DrawLine(pen, tf15, tf16);
                graphics_0.DrawLine(pen, tf17, tf18);
                graphics_0.DrawLine(pen, tf19, tf20);
                graphics_0.DrawLine(pen, tf21, tf22);
                graphics_0.DrawLine(pen, tf23, tf24);
                graphics_0.DrawLine(pen, tf25, tf26);
                graphics_0.DrawLine(pen, tf27, tf28);
                graphics_0.DrawLine(pen, tf29, tf30);
                graphics_0.DrawLine(pen, tf31, tf32);
                graphics_0.DrawLine(pen, tf33, tf34);
                graphics_0.DrawLine(pen, tf35, tf36);
                graphics_0.DrawLine(pen, (float) (35f * this.float_0), (float) (num / 2f), (float) (65f * this.float_0), (float) (num / 2f));
                graphics_0.DrawLine(pen, (float) (35f * this.float_0), (float) ((num / 2f) + 2f), (float) (65f * this.float_0), (float) ((num / 2f) + 2f));
            }
            float num16 = 9f * this.float_1;
            float y = 2f * this.float_1;
            for (int i = 0; i < 12; i++)
            {
                graphics_0.FillEllipse(SystemBrushes.ControlDark, 1f * this.float_0, y, 1.5f * this.float_0, 1.5f * this.float_0);
                graphics_0.FillEllipse(SystemBrushes.ControlDark, base.Width - (3f * this.float_0), y, 1.5f * this.float_0, 1.5f * this.float_0);
                y += num16;
            }
            using (Pen pen2 = new Pen(Color.Red, 3f))
            {
                PointF tf37 = new PointF(50f * this.float_0, 0.6f * num);
                graphics_0.DrawEllipse(pen2, (float) (45f * this.float_0), (float) (0.4f * num), (float) 100f, (float) 40f);
                this.method_4(graphics_0, "全国统一发票监制章", 0f, tf37);
            }
        }

        private void method_1(Graphics graphics_0)
        {
            PointF tf = new PointF(this.float_3, this.float_5);
            PointF tf2 = new PointF(base.Width - this.float_4, this.float_5);
            PointF tf3 = new PointF(this.float_3, base.Height - this.float_6);
            PointF tf4 = new PointF(base.Width - this.float_4, base.Height - this.float_6);
            float num = 100f;
            PointF tf5 = new PointF(tf.X, tf.Y + num);
            PointF tf6 = new PointF(tf2.X, tf2.Y + num);
            PointF tf7 = this.method_2(tf3, 0f, -100f);
            PointF tf8 = this.method_2(tf4, 0f, -100f);
            PointF tf9 = this.method_2(tf7, 0f, -25f);
            PointF tf10 = this.method_2(tf8, 0f, -25f);
            float num2 = 3f * this.float_0;
            PointF tf11 = this.method_2(tf, num2, 0f);
            PointF tf12 = this.method_2(tf5, num2, 0f);
            PointF tf13 = this.method_2(tf7, num2, 0f);
            PointF tf14 = this.method_2(tf3, num2, 0f);
            float num3 = 47f * this.float_0;
            PointF tf15 = this.method_2(tf, num2 + num3, 0f);
            PointF tf16 = this.method_2(tf5, num2 + num3, 0f);
            PointF tf17 = this.method_2(tf7, num2 + num3, 0f);
            PointF tf18 = this.method_2(tf3, num2 + num3, 0f);
            float num4 = 2.5f * this.float_0;
            PointF tf19 = this.method_2(tf, (num2 + num3) + num4, 0f);
            PointF tf20 = this.method_2(tf5, (num2 + num3) + num4, 0f);
            PointF tf21 = this.method_2(tf7, (num2 + num3) + num4, 0f);
            PointF tf22 = this.method_2(tf3, (num2 + num3) + num4, 0f);
            PointF tf23 = this.method_2(tf9, (float) (base.Width / 4), 0f);
            PointF tf24 = this.method_2(tf7, (float) (base.Width / 4), 0f);
            using (Pen pen = new Pen(Color.Black))
            {
                graphics_0.DrawLine(pen, tf, tf2);
                graphics_0.DrawLine(pen, tf2, tf4);
                graphics_0.DrawLine(pen, tf, tf3);
                graphics_0.DrawLine(pen, tf3, tf4);
                graphics_0.DrawLine(pen, tf5, tf6);
                graphics_0.DrawLine(pen, tf9, tf10);
                graphics_0.DrawLine(pen, tf7, tf8);
                graphics_0.DrawLine(pen, tf11, tf12);
                graphics_0.DrawLine(pen, tf13, tf14);
                graphics_0.DrawLine(pen, tf15, tf16);
                graphics_0.DrawLine(pen, tf17, tf18);
                graphics_0.DrawLine(pen, tf19, tf20);
                graphics_0.DrawLine(pen, tf21, tf22);
                graphics_0.DrawLine(pen, tf23, tf24);
                graphics_0.DrawLine(pen, (float) (35f * this.float_0), (float) ((this.float_2 / 2f) + 10f), (float) (65f * this.float_0), (float) ((this.float_2 / 2f) + 10f));
                graphics_0.DrawLine(pen, (float) (35f * this.float_0), (float) ((this.float_2 / 2f) + 3f), (float) (65f * this.float_0), (float) ((this.float_2 / 2f) + 3f));
            }
            float num7 = 50f;
            float y = 10f;
            float width = 15f;
            while (y < base.Height)
            {
                graphics_0.FillEllipse(SystemBrushes.ControlDark, 10f, y, width, width);
                graphics_0.FillEllipse(SystemBrushes.ControlDark, (float) (base.Width - 30), y, width, width);
                y += num7;
            }
            using (Pen pen2 = new Pen(Color.Red, 3f))
            {
                PointF tf25 = new PointF(50f * this.float_0, 0.6f * this.float_2);
                graphics_0.DrawEllipse(pen2, (float) (45f * this.float_0), (float) (0.4f * this.float_2), (float) 100f, (float) 40f);
                this.method_4(graphics_0, "全国统一发票监制章", 0f, tf25);
            }
        }

        private PointF method_2(PointF pointF_0, float float_7, float float_8)
        {
            return new PointF(pointF_0.X + float_7, pointF_0.Y + float_8);
        }

        private void method_3(Graphics graphics_0)
        {
            string text = this.Text;
            Font font = this.Font;
            Brush controlLightLight = SystemBrushes.ControlLightLight;
            StringFormat format = new StringFormat {
                Alignment = StringAlignment.Center
            };
            Rectangle layoutRectangle = new Rectangle(4, 4, base.Width - 8, base.Height - 8);
            graphics_0.DrawString(text, font, controlLightLight, layoutRectangle, format);
        }

        private void method_4(Graphics graphics_0, string string_0, float float_7, PointF pointF_0)
        {
            StringFormat format = new StringFormat {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            GraphicsPath path = new GraphicsPath(FillMode.Winding);
            int x = (int) pointF_0.X;
            int y = (int) pointF_0.Y;
            path.AddString(string_0, this.font_0.FontFamily, (int) this.font_0.Style, this.font_0.Size, new Point(x, y), format);
            Matrix matrix = new Matrix();
            matrix.RotateAt(float_7, new PointF((float) x, (float) y));
            graphics_0.Transform = matrix;
            graphics_0.DrawPath(new Pen(this.color_0), path);
            graphics_0.FillPath(new SolidBrush(this.color_0), path);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.float_0 = ((float) base.Width) / 100f;
            this.float_1 = ((float) base.Height) / 100f;
            Graphics graphics = e.Graphics;
            if (this.Style == 0)
            {
                this.method_0(graphics);
            }
            else
            {
                this.method_1(graphics);
            }
        }

        public int Style
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }
    }
}

