namespace Aisino.FTaxBase
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct InvoiceResult
    {
        public InvResult InvResultEnum;
        public string InvCipher;
        public string InvVerify;
        public string CipherVersion;
        public DateTime InvDate;
        public int RapPeriod;
        public ulong KeyFlagNo;
        public int InvQryNo;
        public string InvIndex;
        public string InvSeqNo;
    }
}

