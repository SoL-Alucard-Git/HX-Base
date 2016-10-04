namespace Aisino.Fwkp.Fplygl.Entry.WebModualEntry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Fplygl.Common;
    using log4net;
    using System;

    public sealed class SeparatorEntry : AbstractCommand
    {
        private ILog loger = LogUtil.GetLogger<SeparatorEntry>();

        protected override bool SetValid()
        {
            try
            {
                if (GetTaxMode.GetTaxCard().get_QYLX().ISTDQY)
                {
                    return false;
                }
                return true;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return true;
        }
    }
}

