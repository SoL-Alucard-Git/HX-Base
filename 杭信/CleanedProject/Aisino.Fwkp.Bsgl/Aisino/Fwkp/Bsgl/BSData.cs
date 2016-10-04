namespace Aisino.Fwkp.Bsgl
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class BSData
    {
        private readonly List<FPDetail> mFpDetailList = new List<FPDetail>();

        public string Address { get; set; }

        public List<FPDetail> FPDetailList
        {
            get
            {
                return this.mFpDetailList;
            }
        }

        public Aisino.Fwkp.Bsgl.FPLB FPLB { get; set; }

        public int KPJH { get; set; }

        public string KPNY { get; set; }

        public int MXJLS
        {
            get
            {
                return this.mFpDetailList.Count;
            }
        }

        public string NSRID { get; set; }

        public string NSRName { get; set; }

        public string Phone { get; set; }

        public string SWJGDM { get; set; }
    }
}

