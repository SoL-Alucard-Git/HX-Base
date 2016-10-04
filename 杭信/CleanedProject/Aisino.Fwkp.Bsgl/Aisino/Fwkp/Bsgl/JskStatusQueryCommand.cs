namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.FTaxBase;
    using System;
    using System.Windows.Forms;

    public class JskStatusQueryCommand : AbstractCommand
    {
        protected override void RunCommand()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            card.GetStateInfo(false);
            if (card.get_ECardType() == 3)
            {
                new JSPStateQuery { StartPosition = FormStartPosition.CenterScreen, ShowInTaskbar = true }.ShowDialog();
            }
            else
            {
                new JSKStateQuery { StartPosition = FormStartPosition.CenterScreen, ShowInTaskbar = true }.ShowDialog();
            }
        }

        protected override bool SetValid()
        {
            return (TaxCardFactory.CreateTaxCard().get_TaxMode() == 2);
        }
    }
}

