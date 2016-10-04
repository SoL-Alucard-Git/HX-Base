namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core;
    using System;

    public static class Flbm
    {
        public static bool IsDK()
        {
            string taxCode = TaxCardFactory.CreateTaxCard().TaxCode;
            ushort companyType = TaxCardFactory.CreateTaxCard().StateInfo.CompanyType;
            return ((!string.IsNullOrEmpty(taxCode) && (taxCode.Length == 15)) && (taxCode.Substring(8, 2) == "DK"));
        }

        public static bool IsYM()
        {
            if (TaxCardFactory.CreateTaxCard().GetExtandParams("FLBMFlag") != "FLBM")
            {
                return false;
            }
            return true;
        }
    }
}

