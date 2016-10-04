namespace Aisino.Fwkp.Fplygl.Common
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.FTaxBase;
    using log4net;
    using System;

    internal class GetTaxStateInfo
    {
        private static TaxCard _taxcard = null;
        private static Aisino.FTaxBase.TaxStateInfo _taxStateInfo = null;
        private static ILog loger = LogUtil.GetLogger<GetTaxStateInfo>();

        public static TaxCard GetTaxCard()
        {
            try
            {
                if (_taxcard == null)
                {
                    _taxcard = TaxCardFactory.CreateTaxCard();
                }
                return _taxcard;
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return _taxcard;
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return _taxcard;
            }
        }

        public static Aisino.FTaxBase.TaxStateInfo TaxStateInfo()
        {
            try
            {
                if (_taxStateInfo == null)
                {
                    _taxStateInfo = GetTaxCard().GetStateInfo(false);
                }
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return _taxStateInfo;
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return _taxStateInfo;
            }
            return _taxStateInfo;
        }
    }
}

