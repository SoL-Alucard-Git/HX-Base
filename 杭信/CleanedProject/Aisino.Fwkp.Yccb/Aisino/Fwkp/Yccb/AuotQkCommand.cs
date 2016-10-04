namespace Aisino.Fwkp.Yccb
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.FTaxBase;
    using log4net;
    using System;

    public sealed class AuotQkCommand : AbstractCommand
    {
        private ILog loger = LogUtil.GetLogger<Yccbqk>();

        protected override void RunCommand()
        {
            this.loger.Error("开始自动清卡");
            Yccbqk.Ycqk(false);
            this.loger.Error("自动清卡结束");
        }

        protected override bool SetValid()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            return ((card.get_TaxMode() == 2) && !card.get_QYLX().ISTDQY);
        }
    }
}

