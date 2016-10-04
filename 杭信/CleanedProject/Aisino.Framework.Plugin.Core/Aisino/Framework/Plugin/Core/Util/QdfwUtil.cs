namespace Aisino.Framework.Plugin.Core.Util
{
    using Aisino.Framework.Plugin.Core;
    using System;

    public class QdfwUtil
    {
        public QdfwUtil()
        {
            
        }

        public static bool IsQDFW()
        {
            return TaxCardFactory.CreateTaxCard().blQDEWM();
        }
    }
}

