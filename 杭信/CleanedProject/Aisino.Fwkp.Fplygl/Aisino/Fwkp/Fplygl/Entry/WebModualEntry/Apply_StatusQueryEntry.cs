namespace Aisino.Fwkp.Fplygl.Entry.WebModualEntry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.WLSL_5;
    using log4net;
    using System;

    public sealed class Apply_StatusQueryEntry : AbstractCommand
    {
        private ILog loger = LogUtil.GetLogger<Apply_StatusQueryEntry>();

        protected override void RunCommand()
        {
            try
            {
                string driverVersion = Tool.Instance().GetDriverVersion();
                string softVersion = Tool.Instance().GetSoftVersion();
                string str3 = driverVersion.Substring(7, 2) + driverVersion.Substring(10, 6);
                if (softVersion.Equals("FWKP_V2.0_Svr_Client"))
                {
                    if (str3.CompareTo("A0150729") < 0)
                    {
                        MessageManager.ShowMsgBox("INP-441291");
                        return;
                    }
                }
                else if (str3.CompareTo("L0110501") < 0)
                {
                    MessageManager.ShowMsgBox("INP-441291");
                    return;
                }
                if (ShareMethods.ApplyAdminCheck("申领状态查询"))
                {
                    new CheckApply(ApplyCommon.GetAdminType().Equals("JS")).ShowDialog();
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

