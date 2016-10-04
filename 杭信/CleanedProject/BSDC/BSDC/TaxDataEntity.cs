namespace BSDC
{
    using System;
    using System.Collections.Generic;

    public class TaxDataEntity
    {
        public List<int> m_nListPeriod;
        public int m_nMonth;
        public int m_nYear;

        public TaxDataEntity()
        {
            
            this.m_nYear = 0;
            this.m_nMonth = 0;
            this.m_nListPeriod = new List<int>();
        }
    }
}

