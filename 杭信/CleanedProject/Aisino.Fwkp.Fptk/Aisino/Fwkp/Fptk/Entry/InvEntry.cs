namespace Aisino.Fwkp.Fptk.Entry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using System;

    public class InvEntry : AbstractCommand
    {
        protected override bool SetValid()
        {
            return ((int)TaxCardFactory.CreateTaxCard().TaxMode == 2);
        }
    }
}

