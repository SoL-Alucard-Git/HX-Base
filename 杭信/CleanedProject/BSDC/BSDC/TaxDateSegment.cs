namespace BSDC
{
    using System;

    public class TaxDateSegment
    {
        public int m_nEndMonth;
        public int m_nStartMonth;
        public int m_nTaxPeriod;
        public int m_nYear;

        public TaxDateSegment()
        {
            
            this.m_nYear = 0;
            this.m_nStartMonth = 0;
            this.m_nEndMonth = 0;
            this.m_nTaxPeriod = 0;
        }
    }
}

