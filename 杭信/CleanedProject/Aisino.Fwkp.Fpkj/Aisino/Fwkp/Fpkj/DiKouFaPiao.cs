namespace Aisino.Fwkp.Fpkj
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.FTaxBase;
    using System;

    public class DiKouFaPiao : AbstractCommand
    {
        protected override void RunCommand()
        {
            base.ShowForm<DiKouFaPiao>();
        }

        protected override bool SetValid()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            return ((card.TaxMode == CTaxCardMode.tcmHave) && !card.QYLX.ISTDQY);
        }
    }
}

