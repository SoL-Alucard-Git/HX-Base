namespace Aisino.FTaxBase
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct TaxRepCommInfo
    {
        public ushort InvType;
        public uint InvWasteCount;
        public double InvWasteAmount;
        public double InvWasteTaxAmount;
        public uint InvCount;
        public double InvAmount;
        public double InvTaxAmount;
        public uint NavInvWasteCount;
        public double NavInvWasteAmount;
        public double NavInvWasteTaxAmount;
        public uint NavInvCount;
        public double NavInvAmount;
        public double NavInvTaxAmount;
    }
}

