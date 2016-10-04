namespace Aisino.Fwkp.Fplygl.Entry.WebModualEntry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.WSFTP_6;
    using log4net;
    using System;

    public sealed class Allocate_QueryEntry : AbstractCommand
    {
        private ILog loger = LogUtil.GetLogger<Allocate_QueryEntry>();

        protected override void RunCommand()
        {
            try
            {
                string driverVersion = Tool.Instance().GetDriverVersion();
                string softVersion = Tool.Instance().GetSoftVersion();
                string str3 = driverVersion.Substring(7, 2) + driverVersion.Substring(10, 6);
                if (softVersion.Equals("FWKP_V2.0_Svr_Client"))
                {
                    if (str3.CompareTo("A0160120") < 0)
                    {
                        MessageManager.ShowMsgBox("INP-441208", new string[] { "领用状态查询" });
                        return;
                    }
                }
                else if (str3.CompareTo("L1160120") < 0)
                {
                    MessageManager.ShowMsgBox("INP-441208", new string[] { "领用状态查询" });
                    return;
                }
                new QueryController().RunCommand();
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        protected override bool SetValid()
        {
            try
            {
                if (!GetTaxMode.GetTaxModValue())
                {
                    return false;
                }
                if (!PopUpBox.NoIsZhu_KaiPiaoJi(GetTaxMode.GetTaxCard(), GetTaxMode.GetTaxStateInfo()))
                {
                    return false;
                }
                if (GetTaxMode.GetTaxCard().get_QYLX().ISTDQY)
                {
                    return false;
                }
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return true;
        }
    }
}

