namespace Aisino.Fwkp.Fplygl.Entry.WebModualEntry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.WLGP_4;
    using log4net;
    using System;

    public sealed class Download_VolumeEntry : AbstractCommand
    {
        private ILog loger = LogUtil.GetLogger<Download_VolumeEntry>();

        protected override void RunCommand()
        {
            try
            {
                new DownloadController().RunCommond();
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
                    return ShareMethods.CheckHasDownloadAllowInvType();
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

