namespace Aisino.Fwkp.Bmgl.Common
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public static class CommonFunc
    {
        public static void DrawBorder(object sender, Graphics g, Color color, Color bordercolor, int x, int y)
        {
            SolidBrush brush = new SolidBrush(color);
            Pen pen = new Pen(brush, 1f);
            (sender as ToolStripTextBox).BorderStyle = BorderStyle.FixedSingle;
            (sender as ToolStripTextBox).BackColor = color;
            pen.Color = Color.White;
            Rectangle bounds = new Rectangle(0, 0, x, y);
            ControlPaint.DrawBorder(g, bounds, bordercolor, ButtonBorderStyle.Solid);
        }

        public static string GenerateKJM(string MC)
        {
            try
            {
                string[] spellCode = StringUtils.GetSpellCode(MC.Trim());
                if (spellCode.Length >= 1)
                {
                    return spellCode[0];
                }
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}

