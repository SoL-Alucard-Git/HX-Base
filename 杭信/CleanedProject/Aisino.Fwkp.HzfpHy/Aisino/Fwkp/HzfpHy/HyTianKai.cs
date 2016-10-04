namespace Aisino.Fwkp.HzfpHy
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.HzfpHy.Form;
    using log4net;
    using System;

    public sealed class HyTianKai : AbstractCommand
    {
        private ILog loger = LogUtil.GetLogger<HyTianKai>();

        public bool CanHyHzfpSqd()
        {
            try
            {
                if (TaxCardFactory.CreateTaxCard().TaxRateAuthorize == null)
                {
                    MessageManager.ShowMsgBox("INP-431321");
                    return false;
                }
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                return false;
            }
        }

        protected override void RunCommand()
        {
            if (this.CanHyHzfpSqd())
            {
                new HySqdInfoSelect().ShowDialog();
            }
        }

        protected override bool SetValid()
        {
            return ((int)TaxCardFactory.CreateTaxCard().TaxMode == 2);
        }
    }
}

