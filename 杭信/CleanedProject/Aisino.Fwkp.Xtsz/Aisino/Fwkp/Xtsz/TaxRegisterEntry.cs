namespace Aisino.Fwkp.Xtsz
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Xtsz.Forms;
    using System;

    public sealed class TaxRegisterEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            new TaxRegisterForm().Register();
        }

        protected override bool SetValid()
        {
            return (TaxCardFactory.CreateTaxCard().get_TaxMode() == 2);
        }
    }
}

