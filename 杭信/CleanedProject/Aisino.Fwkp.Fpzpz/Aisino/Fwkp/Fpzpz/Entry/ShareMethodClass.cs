namespace Aisino.Fwkp.Fpzpz.Entry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Service;
    using log4net;
    using System;

    public class ShareMethodClass : AbstractCommand
    {
        private ILog _Loger = LogUtil.GetLogger<ShareMethodClass>();

        public ShareMethodClass()
        {
            try
            {
                ServiceFactory.RegPubService("Aisino.Fwkp.Fpzpz.InsertDQBM_To_KHBM", "Aisino.Fwkp.Fpzpz.dll", typeof(InsertDQBM_To_KHBM).FullName, null);
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.ToString());
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.ToString());
                ExceptionHandler.HandleError(exception2);
            }
        }

        protected override void RunCommand()
        {
        }
    }
}

