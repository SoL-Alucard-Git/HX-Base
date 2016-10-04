namespace Aisino.Fwkp.Yccb
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.FTaxBase;
    using System;

    public sealed class JgcxCommand : AbstractCommand
    {
        protected override void RunCommand()
        {
            Yccbqk.Ycqk(true);
        }

        protected override bool SetValid()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            return ((card.get_TaxMode() == 2) && !card.get_QYLX().ISTDQY);
        }
    }
}

