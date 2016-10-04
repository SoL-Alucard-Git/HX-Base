namespace Aisino.Fwkp.Fptk
{
    using Aisino.Framework.Plugin.Core;
    using System;

    public static class FLBM_lock
    {
        public static bool isCes()
        {
            return (TaxCardFactory.CreateTaxCard().GetExtandParams("CEBTVisble") == "1");
        }

        public static bool isFlbm()
        {
            return (TaxCardFactory.CreateTaxCard().GetExtandParams("FLBMFlag") == "FLBM");
        }
    }
}

