namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using System;
    using System.Windows.Forms;

    public class FpDataExport : AbstractCommand
    {
        protected override void RunCommand()
        {
            FPExport.QDFPFlag = false;
            new FPExport { StartPosition = FormStartPosition.CenterScreen, ShowInTaskbar = true }.ShowDialog();
        }

        protected override bool SetValid()
        {
            return (TaxCardFactory.CreateTaxCard().get_TaxMode() == 2);
        }
    }
}

