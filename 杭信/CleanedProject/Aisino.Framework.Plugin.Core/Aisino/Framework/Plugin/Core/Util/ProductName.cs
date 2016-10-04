namespace Aisino.Framework.Plugin.Core.Util
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.FTaxBase;
    using System;

    public class ProductName
    {
        public ProductName()
        {
            
        }

        public static string innerVersion
        {
            get
            {
                return PropertyUtil.GetValue("MAIN_VER", "");
            }
        }

        public static string productName
        {
            get
            {
                TaxCard card = TaxCardFactory.CreateTaxCard();
                if ((card.TaxCode.Length == 15) && (card.TaxCode.Substring(8, 2).ToUpper() == "DK"))
                {
                    return "增值税发票税控代开软件 ";
                }
                return "增值税发票税控开票软件（金税盘版） ";
            }
        }

        public static string xfVersion
        {
            get
            {
                TaxCard card = TaxCardFactory.CreateTaxCard();
                if ((card.TaxCode.Length == 15) && (card.TaxCode.Substring(8, 2).ToUpper() == "DK"))
                {
                    return "V1.0.0_ZS_20160425";
                }
                return "V2.0.09_ZS_20160425";
            }
        }
    }
}

