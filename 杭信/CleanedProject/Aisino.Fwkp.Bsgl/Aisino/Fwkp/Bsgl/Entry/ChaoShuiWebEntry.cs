namespace Aisino.Fwkp.Bsgl.Entry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bsgl;
    using System;
    using System.Windows.Forms;

    public class ChaoShuiWebEntry
    {
        public void RunCommand()
        {
            if (TaxCardFactory.CreateTaxCard().get_ECardType() == 3)
            {
                if (MessageManager.ShowMsgBox("INP-251108") == DialogResult.OK)
                {
                    new ChaoshuiForm().ShowDialog();
                }
            }
            else
            {
                new ChaoshuiForm().ShowDialog();
            }
        }
    }
}

