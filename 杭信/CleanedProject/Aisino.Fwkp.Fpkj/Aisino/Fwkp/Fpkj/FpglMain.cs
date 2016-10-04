namespace Aisino.Fwkp.Fpkj
{
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Fpkj.Common;
    using log4net;
    using System;
    using Framework.Plugin.Core.Util;
    public sealed class FpglMain : AbstractCommand
    {
        private ILog loger = LogUtil.GetLogger<FpglMain>();

        protected override bool SetValid()
        {
            try
            {
                if (!GetTaxMode.GetTaxModValue())
                {
                    return false;
                }
                if (!Tool.YiDaoShuoShiQi_JinShuiCard(GetTaxMode.GetTaxCard(), GetTaxMode.GetTaxStateInfo()))
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                return false;
            }
            return true;
        }
    }
}

