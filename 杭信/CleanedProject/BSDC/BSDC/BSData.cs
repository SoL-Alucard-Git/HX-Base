namespace BSDC
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class BSData
    {
        [CompilerGenerated]
        private BSDC.FPLB fplb_0;
        [CompilerGenerated]
        private int int_0;
        private readonly List<FPDetail> list_0;
        [CompilerGenerated]
        private string string_0;
        [CompilerGenerated]
        private string string_1;
        [CompilerGenerated]
        private string string_2;
        [CompilerGenerated]
        private string string_3;
        [CompilerGenerated]
        private string string_4;
        [CompilerGenerated]
        private string string_5;

        public BSData()
        {
            
            this.list_0 = new List<FPDetail>();
        }

        public string Address
        {
            [CompilerGenerated]
            get
            {
                return this.string_4;
            }
            [CompilerGenerated]
            set
            {
                this.string_4 = value;
            }
        }

        public List<FPDetail> FPDetailList
        {
            get
            {
                return this.list_0;
            }
        }

        public BSDC.FPLB FPLB
        {
            [CompilerGenerated]
            get
            {
                return this.fplb_0;
            }
            [CompilerGenerated]
            set
            {
                this.fplb_0 = value;
            }
        }

        public int KPJH
        {
            [CompilerGenerated]
            get
            {
                return this.int_0;
            }
            [CompilerGenerated]
            set
            {
                this.int_0 = value;
            }
        }

        public string KPNY
        {
            [CompilerGenerated]
            get
            {
                return this.string_1;
            }
            [CompilerGenerated]
            set
            {
                this.string_1 = value;
            }
        }

        public int MXJLS
        {
            get
            {
                return this.list_0.Count;
            }
        }

        public string NSRID
        {
            [CompilerGenerated]
            get
            {
                return this.string_3;
            }
            [CompilerGenerated]
            set
            {
                this.string_3 = value;
            }
        }

        public string NSRName
        {
            [CompilerGenerated]
            get
            {
                return this.string_2;
            }
            [CompilerGenerated]
            set
            {
                this.string_2 = value;
            }
        }

        public string Phone
        {
            [CompilerGenerated]
            get
            {
                return this.string_5;
            }
            [CompilerGenerated]
            set
            {
                this.string_5 = value;
            }
        }

        public string SWJGDM
        {
            [CompilerGenerated]
            get
            {
                return this.string_0;
            }
            [CompilerGenerated]
            set
            {
                this.string_0 = value;
            }
        }
    }
}

