namespace Aisino.Fwkp.DataMigrationTool.Common
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.DataMigrationTool.Form;
    using log4net;
    using System;

    public class ShareMethodClass : AbstractCommand
    {
        private ILog _Loger = LogUtil.GetLogger<ShareMethodClass>();

        protected override void RunCommand()
        {
            try
            {
                ServiceFactory.InvokePubService("Aisino.Fwkp.RunInitial", new object[1]);
                ServiceFactory.InvokePubService("Aisino.Fwkp.SetCorpInfo", new object[1]);
                ServiceFactory.InvokePubService("Aisino.Fwkp.CheckCorpInfo", new object[1]);
                string str = PropertyUtil.GetValue("FullInstall");
                string str2 = PropertyUtil.GetValue("SjqySuccess");
                if (((!string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str2)) && (str.Equals("1") || string.IsNullOrEmpty(str2))) && DataMigrationToolForm.LinkParadox())
                {
                    DataMigrationToolForm form = new DataMigrationToolForm();
                    form.SetFuZhiBtnV(false);
                    form.ShowDialog();
                }
                PropertyUtil.SetValue("SjqySuccess", "1");
                PropertyUtil.Save();
                ServiceFactory.InvokePubService("Aisino.Fwkp.Fpzpz.InsertDQBM_To_KHBM", new object[] { false });
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

