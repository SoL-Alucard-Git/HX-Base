namespace Aisino.Fwkp.Bmgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using Framework.Plugin.Core.Util;
    using Forms;

    public sealed class BMXHDWManagerEntry : AbstractCommand
    {
        private ILog _Loger = LogUtil.GetLogger<BMXHDWManagerEntry>();
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();

        private bool IsSWDK()
        {
            try
            {
                string taxCode = this.taxCard.TaxCode;
                if ((!string.IsNullOrEmpty(taxCode) && (taxCode.Length == 15)) && ((taxCode.Substring(8, 2) == "DK") && (TaxCardFactory.CreateTaxCard().StateInfo.CompanyType == 0)))
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                this._Loger.Error("读取金税卡接口时异常:" + exception.ToString());
            }
            return false;
        }

        protected override void RunCommand()
        {
            base.ShowForm<BMXHDW>();
        }

        protected override bool SetValid()
        {
            try
            {
                return this.IsSWDK();
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

