namespace Aisino.Fwkp.Bmgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using Framework.Plugin.Core.Util;
    using Forms;
    public sealed class BMCLManagerEntry : AbstractCommand
    {
        private ILog _Loger = LogUtil.GetLogger<BMCLManagerEntry>();
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();

        protected override void RunCommand()
        {
            base.ShowForm<BMCL>();
        }

        protected override bool SetValid()
        {
            try
            {
                return this.taxCard.QYLX.ISJDC;
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

