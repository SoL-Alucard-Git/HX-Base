namespace Aisino.Fwkp.Fplygl.Entry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.FPLYGL_1;
    using log4net;
    using System;

    public sealed class YgfpthFromCardEntry : AbstractCommand
    {
        private static YgfpthFromCard dlg;
        private ILog loger = LogUtil.GetLogger<YgfpthFromCardEntry>();

        protected override void RunCommand()
        {
            try
            {
                if (((dlg == null) || !dlg.HasShow()) && !Tool.Instance().IsChaoBaoQi())
                {
                    DockForm form = base.ShowForm<YgfpthFromCard>();
                    if (form != null)
                    {
                        dlg = form as YgfpthFromCard;
                        dlg.set_TabText("已购发票退回金税设备");
                        dlg.Text = "已购发票退回金税设备";
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

