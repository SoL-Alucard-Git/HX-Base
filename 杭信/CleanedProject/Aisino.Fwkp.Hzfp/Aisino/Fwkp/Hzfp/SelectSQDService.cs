namespace Aisino.Fwkp.Hzfp
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Hzfp.Form;
    using Framework.Plugin.Core.Util;
    using log4net;
    using System;

    public sealed class SelectSQDService : AbstractService
    {
        private ILog loger = LogUtil.GetLogger<SelectSQDService>();

        protected override object[] doService(object[] param)
        {
            try
            {
                string result = string.Empty;
                SqdSelect select = new SqdSelect();
                select.ShowDialog();
                result = select.Result;
                return new object[] { result };
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return null;
            }
        }
    }
}

