namespace Aisino.Fwkp.DataMigrationTool.Common
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.DataMigrationTool.Form;
    using log4net;
    using System;
    using System.Reflection.Emit;
    using System.Reflection;
    internal sealed class DataMigrationToolShareMethods : AbstractService
    {
        private ILog _Loger = LogUtil.GetLogger<DataMigrationToolShareMethods>();

        protected override object[] doService(object[] param)
        {
            try
            {
                if (DataMigrationToolForm.LinkParadox())
                {
                    new DataMigrationToolForm().ShowDialog();
                }
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

