namespace Aisino.Fwkp.Wbjk
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.FTaxBase;
    using System;

    public sealed class FaPiaoCXEntry : AbstractCommand
    {
        private bool CanXTInv(TaxCard taxCard)
        {
            try
            {
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMIsNeedImportXTSP", null);
                if ((((objArray != null) && (objArray.Length > 0)) && (objArray[0] != null)) && Convert.ToBoolean(objArray[0]))
                {
                    ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMImportXTSP", null);
                }
                if (taxCard.get_QYLX().ISXT)
                {
                    object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.BMCheckXTSP", null);
                    if (!(((objArray2 != null) && (objArray2[0] is bool)) && Convert.ToBoolean(objArray2[0])))
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        protected override void RunCommand()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            base.ShowForm<FPCX>();
        }

        protected override bool SetValid()
        {
            try
            {
                return true;
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
            return true;
        }
    }
}

