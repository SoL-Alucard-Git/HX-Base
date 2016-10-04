namespace Aisino.Fwkp.Yccb
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Size=1)]
    internal struct OPTYPE
    {
        internal static string ZPCB;
        internal static string HYCB;
        internal static string JDCCB;
        internal static string JSPCB;
        internal static string ZPQK;
        internal static string HYQK;
        internal static string JDCQK;
        internal static string JSPQK;
        static OPTYPE()
        {
            ZPCB = "0013";
            HYCB = "0015";
            JDCCB = "0016";
            JSPCB = "JSPCB:0019";
            ZPQK = "0014";
            HYQK = "0017";
            JDCQK = "0018";
            JSPQK = "JSPQK:0020";
        }
    }
}

