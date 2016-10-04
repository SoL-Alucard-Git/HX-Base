namespace Aisino.Fwkp.Sjbf.Common
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Sjbf.Froms;
    using Framework.Plugin.Core.Util;
    using log4net;
    using System;

    internal sealed class SjbfShareMethods : AbstractService
    {
        private ILog _Loger = LogUtil.GetLogger<SjbfShareMethods>();

        protected override object[] doService(object[] param)
        {
            try
            {
                if (param.Length < 0)
                {
                    return null;
                }
                bool bDirectCopy = (bool) param[0];
                CsSetForm.BeginRunCopy(bDirectCopy, false);
                return new object[] { 1 };
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return null;
            }
        }
    }
}

