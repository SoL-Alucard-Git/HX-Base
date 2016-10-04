namespace Aisino.FTaxBase
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct InvoiceAmountType
    {
        public string InvType;
        public string KPMoney;
        public string TPMoney;
    }
}

