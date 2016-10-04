namespace Aisino.FTaxBase
{
    using System;

    [Serializable]
    public class InvDetail
    {
        public double Amount;
        public string BuyTaxCode;
        public bool CancelFlag;
        public int ClientNum;
        public DateTime Date;
        public string Index;
        public uint InvNo;
        public long InvQryNo;
        public ushort InvRepPeriod;
        public string InvSqeNo;
        public ushort InvType;
        public bool IsUpload;
        public string JYM;
        public ulong KeyFlagNo;
        public string MW;
        public string OldInvNo;
        public byte[] OldTypeCode;
        public string SaleTaxCode;
        public byte[] SignBuffer;
        public double Tax;
        public int TaxClass;
        public string TypeCode;
        public DateTime WasteTime;

        public InvDetail()
        {
            
            this.TypeCode = "";
            this.BuyTaxCode = "";
            this.SaleTaxCode = "";
            this.OldInvNo = "";
        }
    }
}

