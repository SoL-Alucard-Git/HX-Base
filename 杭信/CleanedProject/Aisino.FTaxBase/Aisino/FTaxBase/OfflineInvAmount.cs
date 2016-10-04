namespace Aisino.FTaxBase
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct OfflineInvAmount
    {
        public double InvTotalAmount;
        public double InvAmount;
    }
}

