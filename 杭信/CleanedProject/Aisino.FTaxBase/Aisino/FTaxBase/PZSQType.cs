namespace Aisino.FTaxBase
{
    using System;
    using System.Collections.Generic;

    public class PZSQType
    {
        public double InvAmountLimit;
        public InvoiceType invType;
        public double MonthAmountLimit;
        public double OffLineAmoutLimit;
        public double ReturnAmountLimit;
        public List<TaxRateType> TaxRate;
        public List<TaxRateType> TaxRate2;

        public PZSQType()
        {
            
            this.TaxRate = new List<TaxRateType>();
            this.TaxRate2 = new List<TaxRateType>();
        }
    }
}

