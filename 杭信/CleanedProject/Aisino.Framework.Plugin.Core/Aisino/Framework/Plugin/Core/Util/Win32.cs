namespace Aisino.Framework.Plugin.Core.Util
{
    using System;
    using System.Runtime.InteropServices;

    public class Win32
    {
        public const int AW_ACTIVATE = 0x20000;
        public const int AW_BLEND = 0x80000;
        public const int AW_CENTER = 0x10;
        public const int AW_HIDE = 0x10000;
        public const int AW_HOR_NEGATIVE = 2;
        public const int AW_HOR_POSITIVE = 1;
        public const int AW_SLIDE = 0x40000;
        public const int AW_VER_NEGATIVE = 8;
        public const int AW_VER_POSITIVE = 4;
        public static readonly IntPtr HWND_TOPMOST;
        private static readonly IntPtr intptr_0;
        private static readonly IntPtr intptr_1;
        public const int SW_HIDE = 0;

        static Win32()
        {
            
            HWND_TOPMOST = new IntPtr(-1);
            intptr_0 = new IntPtr(-2);
            intptr_1 = new IntPtr(0);
        }

        public Win32()
        {
            
        }

        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern bool AnimateWindow(IntPtr intptr_2, int int_0, int int_1);
        [DllImport("gdi32.dll")]
        public static extern int CreateRoundRectRgn(int int_0, int int_1, int int_2, int int_3, int int_4, int int_5);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr intptr_2, int int_0, int int_1, int int_2);
        [DllImport("user32.dll")]
        public static extern int SetCapture(IntPtr intptr_2);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr intptr_2);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern bool SetWindowPos(IntPtr intptr_2, IntPtr intptr_3, int int_0, int int_1, int int_2, int int_3, int int_4);
        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr intptr_2, int int_0, bool bool_0);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr intptr_2, int int_0);
    }
}

