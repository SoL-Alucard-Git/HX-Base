namespace Aisino.Fwkp.Hzfp
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Form;
    using Framework.Plugin.Core.Util;
    using log4net;
    using System;

    public class Select : AbstractCommand
    {
        private ILog loger = LogUtil.GetLogger<Select>();

        protected override void RunCommand()
        {
            try
            {
                base.ShowForm<SqdSelect>();
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
                return ((int)TaxCardFactory.CreateTaxCard().TaxMode == 2);
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

