namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Runtime.InteropServices;

    public class SysUtil
    {
        public SysUtil()
        {
            
        }

        public static int GetPInvokeStringLength(string string_0)
        {
            if (string_0 == null)
            {
                return 0;
            }
            if (Marshal.SystemDefaultCharSize == 2)
            {
                return string_0.Length;
            }
            if (string_0.Length == 0)
            {
                return 0;
            }
            if (string_0.IndexOf('\0') > -1)
            {
                return smethod_0(string_0);
            }
            return lstrlen(string_0);
        }

        public static int HIWORD(int int_0)
        {
            return ((int_0 >> 0x10) & 0xffff);
        }

        public static int HIWORD(IntPtr intptr_0)
        {
            return HIWORD((int) ((long) intptr_0));
        }

        public static int LOWORD(int int_0)
        {
            return (int_0 & 0xffff);
        }

        public static int LOWORD(IntPtr intptr_0)
        {
            return LOWORD((int) ((long) intptr_0));
        }

        [DllImport("kernel32.dll", CharSet=CharSet.Auto)]
        private static extern int lstrlen(string string_0);
        public static int MAKELONG(int int_0, int int_1)
        {
            return ((int_1 << 0x10) | (int_0 & 0xffff));
        }

        public static IntPtr MAKELPARAM(int int_0, int int_1)
        {
            return (IntPtr) ((int_1 << 0x10) | (int_0 & 0xffff));
        }

        public static int SignedHIWORD(int int_0)
        {
            return (short) ((int_0 >> 0x10) & 0xffff);
        }

        public static int SignedHIWORD(IntPtr intptr_0)
        {
            return SignedHIWORD((int) ((long) intptr_0));
        }

        public static int SignedLOWORD(int int_0)
        {
            return (short) (int_0 & 0xffff);
        }

        public static int SignedLOWORD(IntPtr intptr_0)
        {
            return SignedLOWORD((int) ((long) intptr_0));
        }

        private static int smethod_0(string string_0)
        {
            int index = string_0.IndexOf('\0');
            if (index > -1)
            {
                string str = string_0.Substring(0, index);
                string str2 = string_0.Substring(index + 1);
                return ((GetPInvokeStringLength(str) + smethod_0(str2)) + 1);
            }
            return GetPInvokeStringLength(string_0);
        }
    }
}

