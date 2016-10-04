namespace Aisino.FTaxBase
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class InvSQInfo
    {
        internal bool bool_0;
        internal bool bool_1;
        public string DHYBZ;
        public string DJKHZC;
        public string FXBZ;
        public string HYSQ;
        public uint LXKPTIME;
        public byte LXSSQ;
        public List<Aisino.FTaxBase.PZSQType> PZSQType;
        internal string string_0;
        internal string string_1;
        internal string string_2;
        internal string string_3;
        internal string string_4;
        internal string string_5;
        public string ZGJGDMMC;

        public InvSQInfo()
        {
            
            this.PZSQType = new List<Aisino.FTaxBase.PZSQType>();
            this.HYSQ = "";
            this.ZGJGDMMC = "";
            this.DJKHZC = "";
            this.string_0 = "";
            this.FXBZ = "";
            this.DHYBZ = "";
            this.string_1 = "";
            this.string_2 = "";
            this.string_3 = "";
            this.string_4 = "";
            this.string_5 = "";
        }

        internal Aisino.FTaxBase.PZSQType method_0(string string_6)
        {
            Aisino.FTaxBase.PZSQType type = null;
            if (this.PZSQType.Count > 0)
            {
                using (List<Aisino.FTaxBase.PZSQType>.Enumerator enumerator = this.PZSQType.GetEnumerator())
                {
                    Aisino.FTaxBase.PZSQType current;
                    while (enumerator.MoveNext())
                    {
                        current = enumerator.Current;
                        if (((int) current.invType).ToString() == string_6)
                        {
                            goto Label_0048;
                        }
                    }
                    return type;
                Label_0048:
                    type = current;
                }
            }
            return type;
        }

        public Aisino.FTaxBase.PZSQType this[InvoiceType invoiceType_0]
        {
            get
            {
                Aisino.FTaxBase.PZSQType type = null;
                if (this.PZSQType.Count > 0)
                {
                    using (List<Aisino.FTaxBase.PZSQType>.Enumerator enumerator = this.PZSQType.GetEnumerator())
                    {
                        Aisino.FTaxBase.PZSQType current;
                        while (enumerator.MoveNext())
                        {
                            current = enumerator.Current;
                            if (current.invType == invoiceType_0)
                            {
                                goto Label_003D;
                            }
                        }
                        return type;
                    Label_003D:
                        type = current;
                    }
                }
                return type;
            }
        }
    }
}

