namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.FTaxBase;
    using System;
    using System.Windows.Forms;

    public class QDFPDataExport : AbstractCommand
    {
        protected override void RunCommand()
        {
            FPExport.QDFPFlag = true;
            new FPExport { StartPosition = FormStartPosition.CenterScreen, ShowInTaskbar = true }.ShowDialog();
        }

        protected override bool SetValid()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if (!card.get_QYLX().ISZYFP)
            {
                bool iSZYFP = card.get_QYLX().ISZYFP;
            }
            return (card.get_TaxMode() == 2);
        }
    }
}

