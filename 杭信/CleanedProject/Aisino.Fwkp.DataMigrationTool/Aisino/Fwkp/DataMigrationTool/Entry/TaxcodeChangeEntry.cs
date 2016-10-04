namespace Aisino.Fwkp.DataMigrationTool.Entry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.DataMigrationTool.Form;
    using log4net;
    using System;

    public sealed class TaxcodeChangeEntry : AbstractCommand
    {
        private ILog _Loger = LogUtil.GetLogger<TaxcodeChangeEntry>();

        protected override void RunCommand()
        {
            try
            {
                ServiceFactory.InvokePubService("Aisino.Fwkp.Sjbf", new object[] { true });
                new TaxcodeChangeForm().ShowDialog();
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
    }
}

