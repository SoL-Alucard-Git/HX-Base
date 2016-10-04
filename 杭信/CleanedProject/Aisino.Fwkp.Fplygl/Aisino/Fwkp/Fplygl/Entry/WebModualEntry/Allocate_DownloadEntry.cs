namespace Aisino.Fwkp.Fplygl.Entry.WebModualEntry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.WSFTP_6;
    using log4net;
    using System;

    public sealed class Allocate_DownloadEntry : AbstractCommand
    {
        private ILog loger = LogUtil.GetLogger<Allocate_DownloadEntry>();

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
                        MessageManager.ShowMsgBox("INP-441208", new string[] { "下载主机分配发票" });
                        return;
                    }
                }
                else if (str3.CompareTo("L1160120") < 0)
                {
                    MessageManager.ShowMsgBox("INP-441208", new string[] { "下载主机分配发票" });
                    return;
                }
                new AllocateDownloadController().RunCommond();
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
                return (GetTaxMode.GetTaxModValue() && !PopUpBox.NoIsZhu_KaiPiaoJi(GetTaxMode.GetTaxCard(), GetTaxMode.GetTaxStateInfo()));
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

