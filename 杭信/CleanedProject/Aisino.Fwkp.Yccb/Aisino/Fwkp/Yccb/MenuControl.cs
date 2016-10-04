namespace Aisino.Fwkp.Yccb
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.FTaxBase;
    using System;

    public class MenuControl : AbstractCommand
    {
        protected override void RunCommand()
        {
        }

        protected override bool SetValid()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            return ((card.get_TaxMode() == 2) && !card.get_QYLX().ISTDQY);
        }
    }
}

