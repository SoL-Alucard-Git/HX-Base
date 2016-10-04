namespace Aisino.Fwkp.Fplygl.Entry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.AbsForms;
    using Aisino.Fwkp.Fplygl.IBLL;
    using log4net;
    using System;
    using System.Collections.Generic;

    public sealed class SetFormatEntry : AbstractCommand
    {
        private readonly ILYGL_JPXX jpxxDal = BLLFactory.CreateInstant<ILYGL_JPXX>("LYGL_JPXX");
        private ILog loger = LogUtil.GetLogger<SetFormatEntry>();

        protected override void RunCommand()
        {
            try
            {
                List<InvVolumeApp> invList = new List<InvVolumeApp>();
                List<string> typeList = new List<string>();
                this.jpxxDal.SelectVolumnList(out invList, out typeList);
                new SetFormat(invList, typeList).ShowDialog();
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

