namespace Aisino.FTaxBase
{
    using System;

    public class InvAmountTaxStati
    {
        public uint AllotInvNum;
        private AmountTax amountTax_0;
        private AmountTax amountTax_1;
        private AmountTax amountTax_2;
        private AmountTax amountTax_3;
        private AmountTax amountTax_4;
        private AmountTax amountTax_5;
        public uint BuyNum;
        private InvoiceType invoiceType_0;
        public uint NegativeInvoiceNum;
        public uint NegativeInvWasteNum;
        public uint PeriodEarlyStockNum;
        public uint PeriodEndStockNum;
        public uint PlusInvoiceNum;
        public uint PlusInvWasteNum;
        public uint ReclaimStockNum;
        public uint ReturnInvNum;

        public InvAmountTaxStati(int int_0)
        {
            
            this.amountTax_0 = new AmountTax();
            this.amountTax_1 = new AmountTax();
            this.amountTax_2 = new AmountTax();
            this.amountTax_3 = new AmountTax();
            this.amountTax_4 = new AmountTax();
            this.amountTax_5 = new AmountTax();
            this.invoiceType_0 = (InvoiceType) int_0;
        }

        public AmountTax AmountTax_0
        {
            get
            {
                return this.amountTax_1;
            }
            set
            {
                this.amountTax_1 = value;
            }
        }

        public AmountTax AmountTax_1
        {
            get
            {
                return this.amountTax_2;
            }
            set
            {
                this.amountTax_2 = value;
            }
        }

        public InvoiceType InvTypeName
        {
            get
            {
                return this.invoiceType_0;
            }
        }

        public string InvTypeStr
        {
            get
            {
                return CommonTool.smethod_0(this.invoiceType_0);
            }
        }

        public AmountTax TaxClass4
        {
            get
            {
                return this.amountTax_4;
            }
            set
            {
                this.amountTax_4 = value;
            }
        }

        public AmountTax TaxClass6
        {
            get
            {
                return this.amountTax_3;
            }
            set
            {
                this.amountTax_3 = value;
            }
        }

        public AmountTax TaxClassOther
        {
            get
            {
                return this.amountTax_5;
            }
            set
            {
                this.amountTax_5 = value;
            }
        }

        public AmountTax Total
        {
            get
            {
                return this.amountTax_0;
            }
            set
            {
                this.amountTax_0 = value;
            }
        }
    }
}

