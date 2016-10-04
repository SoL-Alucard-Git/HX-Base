namespace Aisino.Fwkp.Bsgl.Commands
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Bsgl;
    using System;

    public class JskStatusQueryWebEntry
    {
        public BaseForm RunCommand()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            TaxStateInfo stateInfo = card.GetStateInfo(false);
            if (card.get_RetCode() > 0)
            {
                MessageManager.ShowMsgBox(card.get_ErrCode());
                return null;
            }
            if (stateInfo.MachineNumber != card.get_Machine())
            {
                MessageManager.ShowMsgBox("INP-252101");
                return null;
            }
            if (card.get_ECardType() == 3)
            {
                return new JSPStateQuery();
            }
            return new JSKStateQuery();
        }
    }
}

