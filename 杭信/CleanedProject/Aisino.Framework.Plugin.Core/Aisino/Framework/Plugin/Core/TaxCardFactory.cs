namespace Aisino.Framework.Plugin.Core
{
    using Aisino.FTaxBase;
    using System;

    public sealed class TaxCardFactory
    {
        public static CTaxCardType CardType;
        private static readonly object object_0;
        private static TaxCard taxCard_0;

        static TaxCardFactory()
        {
            
            taxCard_0 = null;
            object_0 = new object();
            CardType = CTaxCardType.const_7;
        }

        private TaxCardFactory()
        {
            
        }

        public static TaxCard CreateTaxCard()
        {
            if (taxCard_0 == null)
            {
                lock (object_0)
                {
                    if (taxCard_0 == null)
                    {
                        taxCard_0 = TaxCard.CreateInstance(CardType);
                    }
                }
            }
            return taxCard_0;
        }
    }
}

