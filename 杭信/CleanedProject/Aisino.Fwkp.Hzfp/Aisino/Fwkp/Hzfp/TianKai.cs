namespace Aisino.Fwkp.Hzfp
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Hzfp.Form;
    using log4net;
    using System;

    public class TianKai : AbstractCommand
    {
        private ILog loger = LogUtil.GetLogger<TianKai>();

        public bool CanHzfpSqd()
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
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
        }

        protected override void RunCommand()
        {
            try
            {
                if (this.CanHzfpSqd())
                {
                    new SqdInfoSelect().ShowDialog();
                }
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
        }

        protected override bool SetValid()
        {
            try
            {
                return ((int) TaxCardFactory.CreateTaxCard().TaxMode == 2);
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
        }
    }
}

