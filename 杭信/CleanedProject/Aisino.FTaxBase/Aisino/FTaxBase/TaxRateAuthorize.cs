namespace Aisino.FTaxBase
{
    using System;
    using System.Collections.Generic;

    public class TaxRateAuthorize
    {
        public List<double> TaxRateNoTax;
        public List<double> TaxRateTax;

        public TaxRateAuthorize()
        {
            
            this.TaxRateNoTax = new List<double>();
            this.TaxRateTax = new List<double>();
        }
    }
}

