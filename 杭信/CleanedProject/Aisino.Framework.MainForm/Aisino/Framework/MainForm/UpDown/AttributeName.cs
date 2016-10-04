namespace Aisino.Framework.MainForm.UpDown
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.FTaxBase;
    using System;

    public static class AttributeName
    {
        public static string HYCSDateName;
        public static string HYQKDateName;
        public static string JDCCSDateName;
        public static string JDCQKDateName;
        public static string JSPCSDateName;
        public static string JSPQKDateName;
        public static string QYXXFileName;
        internal static TaxCard taxCard_0;
        public static string ZPCSDateName;
        public static string ZPQKDateName;

        static AttributeName()
        {
            
            taxCard_0 = TaxCardFactory.CreateTaxCard();
            QYXXFileName = string.Concat(new object[] { taxCard_0.TaxCode, "-", taxCard_0.Machine, "QYXXParams.xml" });
            ZPCSDateName = string.Concat(new object[] { "Aisino.Fwkp.Yccb.zpcbrq.", taxCard_0.TaxCode, ".", taxCard_0.Machine });
            HYCSDateName = string.Concat(new object[] { "Aisino.Fwkp.Yccb.hycbrq.", taxCard_0.TaxCode, ".", taxCard_0.Machine });
            JDCCSDateName = string.Concat(new object[] { "Aisino.Fwkp.Yccb.jdccbrq.", taxCard_0.TaxCode, ".", taxCard_0.Machine });
            JSPCSDateName = string.Concat(new object[] { "Aisino.Fwkp.Yccb.jspcbrq.", taxCard_0.TaxCode, ".", taxCard_0.Machine });
            ZPQKDateName = string.Concat(new object[] { "Aisino.Fwkp.Yccb.zpqkrq.", taxCard_0.TaxCode, ".", taxCard_0.Machine });
            HYQKDateName = string.Concat(new object[] { "Aisino.Fwkp.Yccb.hyqkrq.", taxCard_0.TaxCode, ".", taxCard_0.Machine });
            JDCQKDateName = string.Concat(new object[] { "Aisino.Fwkp.Yccb.jdcqkrq.", taxCard_0.TaxCode, ".", taxCard_0.Machine });
            JSPQKDateName = string.Concat(new object[] { "Aisino.Fwkp.Yccb.jspqkrq.", taxCard_0.TaxCode, ".", taxCard_0.Machine });
        }
    }
}

