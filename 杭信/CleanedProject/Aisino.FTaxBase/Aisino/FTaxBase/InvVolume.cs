namespace Aisino.FTaxBase
{
    using System;

    public class InvVolume
    {
        public uint EndCode;
        public uint HeadCode;
        public InvoiceType InvType;
        public string MistakeNO;
        public int MistakeNum;
        public string PrdEarlyStockNO;
        public int PrdEarlyStockNum;
        public string PrdEndStockNO;
        public int PrdEndStockNum;
        public string PrdThisBuyNO;
        public int PrdThisBuyNum;
        public string PrdThisIssueNO;
        public int PrdThisIssueNum;
        public string TypeCode;
        public string WasteNO;
        public int WasteNum;

        public InvVolume()
        {
            
            this.PrdEarlyStockNO = "";
            this.PrdThisBuyNO = "";
            this.PrdThisIssueNO = "";
            this.WasteNO = "";
            this.MistakeNO = "";
            this.PrdEndStockNO = "";
        }
    }
}

