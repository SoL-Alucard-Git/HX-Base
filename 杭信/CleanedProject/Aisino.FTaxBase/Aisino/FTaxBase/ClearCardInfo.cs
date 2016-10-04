namespace Aisino.FTaxBase
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ClearCardInfo
    {
        public ushort InvKind;
        public ushort InvPeriod;
        public string CSTime;
    }
}

