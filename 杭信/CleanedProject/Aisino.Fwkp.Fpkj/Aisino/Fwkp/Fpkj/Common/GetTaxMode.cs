namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using Framework.Plugin.Core.Util;
    internal class GetTaxMode : DockForm
    {
        private static TaxCard _TaxCard = TaxCardFactory.CreateTaxCard();
        private static ILog loger = LogUtil.GetLogger<GetTaxMode>();

        public GetTaxMode()
        {
            try
            {
                this.InitializeZhiFuChuan();
            }
            catch (Exception exception)
            {
                loger.Error("[GetTaxMode函数异常]" + exception.Message);
            }
        }

        public static TaxCard GetTaxCard()
        {
            try
            {
                if (_TaxCard == null)
                {
                    _TaxCard = TaxCardFactory.CreateTaxCard();
                }
                return _TaxCard;
            }
            catch (Exception exception)
            {
                loger.Error("GetTaxCard函数异常" + exception.Message);
                return null;
            }
        }

        public static bool GetTaxModValue()
        {
            try
            {
                if (_TaxCard == null)
                {
                    _TaxCard = TaxCardFactory.CreateTaxCard();
                }
                return (_TaxCard.TaxMode == CTaxCardMode.tcmHave);
            }
            catch (Exception exception)
            {
                loger.Error("GetTaxModValue函数异常" + exception.Message);
                return false;
            }
        }

        public static TaxStateInfo GetTaxStateInfo()
        {
            try
            {
                if (_TaxCard == null)
                {
                    _TaxCard = TaxCardFactory.CreateTaxCard();
                }
                return _TaxCard.StateInfo;
            }
            catch (Exception exception)
            {
                loger.Error("GetTaxStateInfo函数异常" + exception.Message);
                return null;
            }
        }

        private void InitializeZhiFuChuan()
        {
        }
    }
}

