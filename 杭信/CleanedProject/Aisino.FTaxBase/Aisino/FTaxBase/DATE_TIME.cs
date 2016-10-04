namespace Aisino.FTaxBase
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DATE_TIME
    {
        public ushort year;
        public byte month;
        public byte day;
        public byte hour;
        public byte min;
        public byte sec;
        public byte x;
    }
}

