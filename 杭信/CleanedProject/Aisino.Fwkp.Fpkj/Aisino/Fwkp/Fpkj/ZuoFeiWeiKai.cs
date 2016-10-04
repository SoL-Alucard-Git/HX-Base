namespace Aisino.Fwkp.Fpkj
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using Framework.Plugin.Core.Util;
    public class ZuoFeiWeiKai : AbstractCommand
    {
        private ILog loger = LogUtil.GetLogger<ZuoFeiWeiKai>();

        protected override bool SetValid()
        {
            try
            {
                TaxCard card = TaxCardFactory.CreateTaxCard();
                return ((card.TaxMode == CTaxCardMode.tcmHave) && (((card.QYLX.ISPTFP || card.QYLX.ISZYFP) || (card.QYLX.ISHY || card.QYLX.ISJDC)) || card.QYLX.ISPTFPJSP));
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.ToString());
                return false;
            }
        }
    }
}

