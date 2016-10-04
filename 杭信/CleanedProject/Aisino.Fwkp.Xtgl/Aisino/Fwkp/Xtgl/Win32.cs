namespace Aisino.Fwkp.Xtgl
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
        public const int SW_HIDE = 0;

        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
    }
}

