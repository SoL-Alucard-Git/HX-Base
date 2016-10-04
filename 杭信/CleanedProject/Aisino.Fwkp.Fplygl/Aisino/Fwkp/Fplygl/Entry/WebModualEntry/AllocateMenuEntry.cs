namespace Aisino.Fwkp.Fplygl.Entry.WebModualEntry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Fplygl.Common;
    using log4net;
    using System;

    public sealed class AllocateMenuEntry : AbstractCommand
    {
        private ILog loger = LogUtil.GetLogger<AllocateMenuEntry>();

        protected override bool SetValid()
        {
            try
            {
                if (GetTaxMode.GetTaxCard().get_QYLX().ISTDQY)
                {
                    return false;
                }
                if ((((!GetTaxMode.GetTaxCard().get_QYLX().ISHY && !GetTaxMode.GetTaxCard().get_QYLX().ISJDC) && !GetTaxMode.GetTaxCard().get_QYLX().ISPTFPDZ) && !GetTaxMode.GetTaxCard().get_QYLX().ISPTFPJSP) && (GetTaxMode.GetTaxCard().get_StateInfo().MachineNumber == 0))
                {
                    return false;
                }
                if (PopUpBox.NoIsZhu_KaiPiaoJi(GetTaxMode.GetTaxCard(), GetTaxMode.GetTaxStateInfo()) && !((GetTaxMode.GetTaxCard().get_QYLX().ISPTFP || GetTaxMode.GetTaxCard().get_QYLX().ISZYFP) || GetTaxMode.GetTaxCard().get_QYLX().ISJDC))
                {
                    return false;
                }
                return true;
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

