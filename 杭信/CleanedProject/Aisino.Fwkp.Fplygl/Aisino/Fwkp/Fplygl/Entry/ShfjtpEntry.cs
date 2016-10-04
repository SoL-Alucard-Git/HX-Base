namespace Aisino.Fwkp.Fplygl.Entry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.FJFPGL_2;
    using log4net;
    using System;

    public sealed class ShfjtpEntry : AbstractCommand
    {
        private ILog loger = LogUtil.GetLogger<ShfjtpEntry>();

        protected override void RunCommand()
        {
            try
            {
                new ShfjtpForm().Run();
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

