namespace BSDC
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.FTaxBase;
    using System;

    public class ATaxCard
    {
        private string string_0;
        private TaxCard taxCard_0;

        public ATaxCard()
        {
            
            this.string_0 = "";
        }

        public bool InterOpenCard(string string_1)
        {
            this.string_0 = string_1;
            TaxCardFactory.CardType = CTaxCardType.const_7;
            try
            {
                this.taxCard_0 = TaxCardFactory.CreateTaxCard();
                if (this.taxCard_0 == null)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return this.taxCard_0.TaxCardOpen(this.string_0);
        }

        public TaxCard TaxCardValue
        {
            get
            {
                return this.taxCard_0;
            }
            set
            {
                this.taxCard_0 = value;
            }
        }
    }
}

