namespace Aisino.Fwkp.Sjbf.Entry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Startup.Login;
    using Aisino.Fwkp.Sjbf.Froms;
    using Framework.Plugin.Core.Util;
    using log4net;
    using System;

    public sealed class Recovery_ExecutionEntry : AbstractCommand
    {
        private ILog _Loger = LogUtil.GetLogger<RunCopy_ExecutionEntry>();

        protected override void RunCommand()
        {
            try
            {
                new RecoveryForm().ShowDialog();
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        protected override bool SetValid()
        {
            try
            {
                if (UserInfo.IsAdmin)
                {
                    return true;
                }
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
            return false;
        }
    }
}

