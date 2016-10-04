namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class AisinoLBLEX : Label
    {
        private int int_0;

        public AisinoLBLEX()
        {
            
            this.int_0 = 5;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                string str2;
                Graphics graphics = e.Graphics;
                string text = this.Text;
                Font font = this.Font;
                SolidBrush brush = new SolidBrush(this.ForeColor);
                SizeF ef = graphics.MeasureString(this.Text, this.Font);
                int num = Convert.ToInt16((float) (ef.Width / ((float) base.Width))) + 1;
                this.AutoSize = false;
                float x = 0f;
                StringFormat format = new StringFormat();
                int length = 1;
                num = text.Length;
                int num4 = 0;
                while (num4 < num)
                {
                    int num5 = 0;
                    while (num5 < text.Length)
                    {
                        string str4 = text.Substring(0, num5);
                        string str3 = text.Substring(0, num5 + 1);
                        if ((graphics.MeasureString(str4, this.Font).Width <= base.Width) && (graphics.MeasureString(str3, this.Font).Width > base.Width))
                        {
                            goto Label_00E5;
                        }
                        num5++;
                    }
                    goto Label_00E9;
                Label_00E5:
                    length = num5;
                Label_00E9:
                    if (num5 == text.Length)
                    {
                        goto Label_0148;
                    }
                    str2 = text.Substring(0, length);
                    text = text.Substring(length);
                    e.Graphics.DrawString(str2, font, brush, x, (float) (Convert.ToInt16((float) (ef.Height * num4)) + (num4 * this.int_0)), format);
                    num4++;
                }
                return;
            Label_0148:
                str2 = text;
                e.Graphics.DrawString(str2, font, brush, x, (float) (Convert.ToInt16((float) (ef.Height * num4)) + (num4 * this.int_0)), format);
            }
            catch (Exception)
            {
            }
        }

        public int LineDistance
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

