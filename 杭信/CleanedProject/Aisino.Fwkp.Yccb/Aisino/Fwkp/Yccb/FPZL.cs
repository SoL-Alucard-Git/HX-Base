namespace Aisino.Fwkp.Yccb
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Size=1)]
    internal struct FPZL
    {
        internal static int ZP;
        internal static int HY;
        internal static int JDC;
        internal static int DZFP;
        internal static int JSFP;
        static FPZL()
        {
            ZP = 0;
            HY = 11;
            JDC = 12;
            DZFP = 0x33;
            JSFP = 0x29;
        }
    }
}

