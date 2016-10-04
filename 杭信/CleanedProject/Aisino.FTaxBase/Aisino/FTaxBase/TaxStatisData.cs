namespace Aisino.FTaxBase
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class TaxStatisData
    {
        private List<InvAmountTaxStati> list_0;

        internal TaxStatisData()
        {
            
            this.list_0 = new List<InvAmountTaxStati>();
            InvAmountTaxStati item = new InvAmountTaxStati(0);
            this.list_0.Add(item);
            item = new InvAmountTaxStati(2);
            this.list_0.Add(item);
            item = new InvAmountTaxStati(11);
            this.list_0.Add(item);
            item = new InvAmountTaxStati(12);
            this.list_0.Add(item);
            item = new InvAmountTaxStati(0x33);
            this.list_0.Add(item);
            item = new InvAmountTaxStati(0x29);
            this.list_0.Add(item);
        }

        public InvAmountTaxStati InvTypeStatData(int int_0)
        {
            int num = -1;
            switch (int_0)
            {
                case 0:
                    num = 0;
                    break;

                case 2:
                    num = 1;
                    break;

                case 11:
                    num = 2;
                    break;

                case 12:
                    num = 3;
                    break;

                case 0x29:
                    num = 5;
                    break;

                case 0x33:
                    num = 4;
                    break;

                default:
                    num = -1;
                    break;
            }
            if (num == -1)
            {
                return null;
            }
            return this.list_0[num];
        }

        internal InvAmountTaxStati method_0(InvoiceType invoiceType_0)
        {
            int num = (int) invoiceType_0;
            return this.InvTypeStatData(num);
        }

        public int Count
        {
            get
            {
                return this.list_0.Count;
            }
        }

        public InvAmountTaxStati this[int int_0]
        {
            get
            {
                return this.list_0[int_0];
            }
        }
    }
}

