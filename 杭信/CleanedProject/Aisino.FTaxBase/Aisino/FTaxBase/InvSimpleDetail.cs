namespace Aisino.FTaxBase
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct InvSimpleDetail
    {
        public string TypeCode;
        public uint InvNo;
        public string Index;
    }
}

