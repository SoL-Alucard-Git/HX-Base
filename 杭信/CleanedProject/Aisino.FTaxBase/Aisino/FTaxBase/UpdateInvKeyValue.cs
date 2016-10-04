namespace Aisino.FTaxBase
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct UpdateInvKeyValue
    {
        public string TaxKindCode;
        public int InvNo;
    }
}

