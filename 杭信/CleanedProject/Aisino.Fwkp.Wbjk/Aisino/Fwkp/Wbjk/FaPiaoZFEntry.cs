namespace Aisino.Fwkp.Wbjk
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Framework.Startup.Login;
    using Aisino.Fwkp.Wbjk.BLL;
    using System;

    public sealed class FaPiaoZFEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            FPZFbll fbll = new FPZFbll();
            bool isAdmin = false;
            isAdmin = UserInfo.get_IsAdmin();
            string mc = (UserInfo.get_Yhmc() == null) ? "" : UserInfo.get_Yhmc().Trim();
            if (fbll.GetZuoFeiXSDJ(fbll.Pagesize, fbll.CurrentPage, isAdmin, mc).get_AllRows() <= 0)
            {
                MessageManager.ShowMsgBox("INP-274201");
            }
            else
            {
                base.ShowForm<FaPiaoZF>();
            }
        }

        protected override bool SetValid()
        {
            try
            {
                bool flag = WbjkEntry.RegFlag_JT;
                return true;
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
            return true;
        }
    }
}

