namespace Aisino.FTaxBase
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct InvWasteResult
    {
        public string AddrIndex;
        public DateTime WasteTime;
    }
}

