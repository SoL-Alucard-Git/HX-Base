namespace Aisino.FTaxBase
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct InvTypeInfo
    {
        public ushort InvType;
        public ushort IsRepTime;
        public ushort IsLockTime;
        public ushort ushort_0;
        public ushort ushort_1;
        public ushort ICBuyInv;
        public ushort ICRetInv;
        public ushort Reserve;
        public string LastRepDate;
        public string NextRepDate;
        public string LockedDate;
        public ushort ushort_2;
        public ushort ushort_3;
        public ushort ushort_4;
        public ushort ushort_5;
    }
}

