namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Fpkj.Form.FPXF;
    using log4net;
    using System;
    using Framework.Plugin.Core;
    using Framework.Plugin.Core.Util;
    internal sealed class FPXiuFuShareMethods : AbstractService
    {
        private DateTime CardClock = TaxCardFactory.CreateTaxCard().GetCardClock();
        private FpxfMain fpxfMain;
        private ILog loger = LogUtil.GetLogger<FPXiuFuShareMethods>();

        protected override object[] doService(object[] param)
        {
            object[] objArray = new object[] { 0 };
            try
            {
                if (this.fpxfMain == null)
                {
                    this.fpxfMain = new FpxfMain();
                }
                this.fpxfMain.XFYear = this.CardClock.Year;
                this.fpxfMain.XFMonth = 0;
                this.fpxfMain.XFIsShowDialog = false;
                this.fpxfMain.ShowDialog();
                this.fpxfMain.AutoEvent.WaitOne();
                if (this.fpxfMain != null)
                {
                    this.fpxfMain = null;
                    GC.Collect();
                }
                this.loger.Error("fpxfMain.StartThread(fpxfArgs)执行结束");
                objArray[0] = 1;
                return objArray;
            }
            catch (Exception exception)
            {
                this.loger.Error("[FPXiuFuShareMethods函数异常]" + exception.Message);
                return objArray;
            }
        }
    }
}

