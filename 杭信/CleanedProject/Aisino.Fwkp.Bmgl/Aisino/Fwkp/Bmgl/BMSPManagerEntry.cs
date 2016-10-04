namespace Aisino.Fwkp.Bmgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using Framework.Plugin.Core.Util;
    using Framework.Plugin.Core.Util;
    using Forms;

    public sealed class BMSPManagerEntry : AbstractCommand
    {
        private ILog _Loger = LogUtil.GetLogger<BMSPManagerEntry>();
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();

        protected override void RunCommand()
        {
            base.ShowForm<BMSP>();
        }

        protected override bool SetValid()
        {
            try
            {
                return ((((this.taxCard.QYLX.ISPTFP || this.taxCard.QYLX.ISZYFP) || (this.taxCard.QYLX.ISNCPSG || this.taxCard.QYLX.ISNCPXS)) || this.taxCard.QYLX.ISPTFPDZ) || this.taxCard.QYLX.ISPTFPJSP);
            }
            catch (BaseException exception)
            {
                this._Loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                this._Loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
        }
    }
}

