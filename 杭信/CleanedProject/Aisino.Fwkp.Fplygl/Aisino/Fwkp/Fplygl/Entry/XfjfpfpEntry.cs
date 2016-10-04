namespace Aisino.Fwkp.Fplygl.Entry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.FJFPGL_2;
    using log4net;
    using System;

    public sealed class XfjfpfpEntry : AbstractCommand
    {
        private ILog loger = LogUtil.GetLogger<ShfjtpEntry>();

        protected override void RunCommand()
        {
            try
            {
                if (!Tool.Instance().IsChaoBaoQi())
                {
                    DockForm form = base.ShowForm<XfjfpfpForm>();
                    if (form != null)
                    {
                        form.set_TabText("选择要分配的发票卷");
                        (form as XfjfpfpForm).FormAction();
                    }
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
                if (!PopUpBox.NoHaveFen_KaiPiaoJi(GetTaxMode.GetTaxCard(), GetTaxMode.GetTaxStateInfo()))
                {
                    string dHYBZ = GetTaxMode.GetTaxCard().get_SQInfo().DHYBZ;
                    return (dHYBZ.Equals("Y") || dHYBZ.Equals("Z"));
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

