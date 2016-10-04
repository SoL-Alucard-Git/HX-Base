namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using BSDC;
    using System;
    using System.Windows.Forms;

    public class TaxDataOutput : AbstractCommand
    {
        protected override void RunCommand()
        {
            new BSDataOutput { StartPosition = FormStartPosition.CenterScreen, ShowInTaskbar = true }.ShowDialog();
        }

        protected override bool SetValid()
        {
            return (TaxCardFactory.CreateTaxCard().get_TaxMode() == 2);
        }
    }
}

