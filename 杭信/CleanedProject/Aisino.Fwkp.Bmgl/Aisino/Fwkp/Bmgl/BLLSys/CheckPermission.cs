namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.FTaxBase;
    using System;
    using Framework.Plugin.Core;
    internal static class CheckPermission
    {
        private static TaxCard taxCard = TaxCardFactory.CreateTaxCard();

        public static bool Check(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return false;
            }
            if ((tableName == "SFHR") || (tableName == "FYXM"))
            {
                return taxCard.QYLX.ISHY;
            }
            if ((tableName == "GHDW") || (tableName == "CL"))
            {
                return taxCard.QYLX.ISJDC;
            }
            if ((tableName == "KH") || (tableName == "SP"))
            {
                return ((((taxCard.QYLX.ISPTFP || taxCard.QYLX.ISZYFP) || (taxCard.QYLX.ISNCPSG || taxCard.QYLX.ISNCPXS)) || taxCard.QYLX.ISPTFPDZ) || taxCard.QYLX.ISPTFPJSP);
            }
            return (tableName == "XHDW");
        }
    }
}

