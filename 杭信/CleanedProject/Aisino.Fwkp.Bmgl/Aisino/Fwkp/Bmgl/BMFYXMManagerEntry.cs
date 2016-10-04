namespace Aisino.Fwkp.Bmgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using Framework.Plugin.Core.Util;
    using Forms;
    public sealed class BMFYXMManagerEntry : AbstractCommand
    {
        private ILog _Loger = LogUtil.GetLogger<BMFYXMManagerEntry>();
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();

        protected override void RunCommand()
        {
            base.ShowForm<BMFYXM>();
        }

        protected override bool SetValid()
        {
            try
            {
                return this.taxCard.QYLX.ISHY;
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

