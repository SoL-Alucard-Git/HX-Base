namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class CustomComboBox : ComboBox
    {
        public CustomComboBox()
        {
            
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr intptr_0);
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr intptr_0);
        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr intptr_0, IntPtr intptr_1);
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if ((m.Msg == 15) || (m.Msg == 0x133))
            {
                IntPtr dC = GetDC(m.HWnd);
                if (dC.ToInt32() != 0)
                {
                    Graphics graphics = Graphics.FromHdc(dC);
                    new Rectangle(0, 0, base.Width, base.Height);
                    Rectangle rect = new Rectangle((base.Width - base.Height) + 2, 2, base.Height - 4, base.Height - 4);
                    graphics.DrawImage(Class131.smethod_5(), rect);
                    ReleaseDC(m.HWnd, dC);
                }
            }
        }
    }
}

