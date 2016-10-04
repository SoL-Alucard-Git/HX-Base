namespace Aisino.FTaxBase
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct InvVolumeFace
    {
        public uint InvNo;
        public ushort Remain;
        public ushort BuyMonth;
        public ushort BuyDay;
        public ushort BuyHour;
        public ushort BuyMinute;
        public string InvCode;
        public byte Type;
        public byte InvLimit;
        public byte RetCentury;
        public byte RetYear;
        public byte RetMonth;
        public byte RetDay;
        public ushort Count;
        public byte Flag;
    }
}

