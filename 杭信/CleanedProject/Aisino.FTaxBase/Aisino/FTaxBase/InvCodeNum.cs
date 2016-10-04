namespace Aisino.FTaxBase
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct InvCodeNum
    {
        public string InvTypeCode;
        public string InvNum;
        public string EndNum;
    }
}

