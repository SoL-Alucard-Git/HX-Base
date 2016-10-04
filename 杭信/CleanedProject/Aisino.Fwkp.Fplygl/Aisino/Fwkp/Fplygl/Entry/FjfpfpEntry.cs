namespace Aisino.Fwkp.Fplygl.Entry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Fplygl.Common;
    using log4net;
    using System;

    public sealed class FjfpfpEntry : AbstractCommand
    {
        private ILog loger = LogUtil.GetLogger<FjfpfpEntry>();

        protected override void RunCommand()
        {
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

