namespace Aisino.Fwkp.Fplygl.Entry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.FPKCGL_3;
    using log4net;
    using System;

    public sealed class FpkccxEntry : AbstractCommand
    {
        private static FpKuChunChaXunForm dlg;
        private ILog loger = LogUtil.GetLogger<FjfpfpEntry>();

        protected override void RunCommand()
        {
            try
            {
                if (((dlg == null) || !dlg.HasShow()) && !Tool.Instance().IsChaoBaoQi())
                {
                    DockForm form = base.ShowForm<FpKuChunChaXunForm>();
                    if (form != null)
                    {
                        form.set_TabText("金税设备库存发票查询");
                        form.Text = "金税设备库存发票查询";
                        dlg = form as FpKuChunChaXunForm;
                        dlg.FormAction();
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

