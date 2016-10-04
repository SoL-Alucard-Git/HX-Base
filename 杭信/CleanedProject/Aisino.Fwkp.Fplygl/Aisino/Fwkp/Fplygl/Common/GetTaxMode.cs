namespace Aisino.Fwkp.Fplygl.Common
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.FTaxBase;
    using log4net;
    using System;

    internal class GetTaxMode
    {
        private static TaxCard _TaxCard = null;
        private static ILog loger = LogUtil.GetLogger<GetTaxMode>();

        public static TaxCard GetTaxCard()
        {
            try
            {
                if (_TaxCard == null)
                {
                    _TaxCard = Aisino.Fwkp.Fplygl.Common.GetTaxStateInfo.GetTaxCard();
                }
                return _TaxCard;
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return _TaxCard;
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return _TaxCard;
            }
        }

        public static bool GetTaxModValue()
        {
            try
            {
                _TaxCard = Aisino.Fwkp.Fplygl.Common.GetTaxStateInfo.GetTaxCard();
                return (2 == _TaxCard.get_TaxMode());
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return false;
            }
        }

        public static TaxStateInfo GetTaxStateInfo()
        {
            try
            {
                return Aisino.Fwkp.Fplygl.Common.GetTaxStateInfo.TaxStateInfo();
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return null;
            }
        }
    }
}

