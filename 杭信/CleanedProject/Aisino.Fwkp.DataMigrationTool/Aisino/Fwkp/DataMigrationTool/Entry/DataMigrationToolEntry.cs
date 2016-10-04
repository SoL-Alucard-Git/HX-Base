namespace Aisino.Fwkp.DataMigrationTool.Entry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.DataMigrationTool.Form;
    using log4net;
    using System;
    using System.IO;

    public sealed class DataMigrationToolEntry : AbstractCommand
    {
        private ILog _Loger = LogUtil.GetLogger<DataMigrationToolEntry>();

        protected override void RunCommand()
        {
            try
            {
                if (!Directory.Exists(@"C:\Program Files\Common Files\Borland Shared\BDE") && !Directory.Exists(@"C:\Program Files (x86)\Common Files\Borland Shared\BDE"))
                {
                    MessageBoxHelper.Show("BDE数据库驱动不存在，数据迁移功能不可用！\n请尝试安装旧版开票软件或BDE数据库驱动程序", "提示");
                }
                else
                {
                    ServiceFactory.InvokePubService("Aisino.Fwkp.Sjbf", new object[] { true });
                    new DataMigrationToolForm().ShowDialog();
                }
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

