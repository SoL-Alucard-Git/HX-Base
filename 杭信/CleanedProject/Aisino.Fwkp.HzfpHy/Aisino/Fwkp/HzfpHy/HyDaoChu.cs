namespace Aisino.Fwkp.HzfpHy
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Form;
    using System;

    public class HyDaoChu : AbstractCommand
    {
        protected override void RunCommand()
        {
            base.ShowForm<HySqdChaXun>();
        }

        protected override bool SetValid()
        {
            return ((int)TaxCardFactory.CreateTaxCard().TaxMode == 2);
        }
    }
}

