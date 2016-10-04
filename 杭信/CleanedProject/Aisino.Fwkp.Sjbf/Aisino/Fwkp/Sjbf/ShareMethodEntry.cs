namespace Aisino.Fwkp.Sjbf
{
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Sjbf.Common;
    using Aisino.Fwkp.Sjbf.Froms;
    using log4net;
    using System;

    public class ShareMethodEntry : AbstractCommand
    {
        private static ILog _Loger = LogUtil.GetLogger<ShareMethodEntry>();

        public ShareMethodEntry()
        {
            ServiceFactory.RegPubService("Aisino.Fwkp.Sjbf", "Aisino.Fwkp.Sjbf.dll", typeof(SjbfShareMethods).FullName, null);
        }

        protected override void RunCommand()
        {
            try
            {
                FormMain.ExecuteBeforeExitEvent+=new FormMain.ExecuteBeforeExitDelegate(this.runexit);
                FormMain.ExecuteAfterLoadEvent+=new FormMain.ExecuteAfterLoadDelegate(this.runLoad);
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void runexit()
        {
            try
            {
                CsSetForm.EndRunCopy(false, false);
                string str = PropertyUtil.GetValue("FullInstall");
                if ((str != null) && ((str.Equals("1") || !str.Equals("0")) || string.IsNullOrEmpty(str)))
                {
                    PropertyUtil.SetValue("FullInstall", "0");
                }
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        private void runLoad()
        {
            try
            {
                CsSetForm.BeginRunCopy(false, false);
            }
            catch (BaseException exception)
            {
                _Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                _Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }
    }
}

