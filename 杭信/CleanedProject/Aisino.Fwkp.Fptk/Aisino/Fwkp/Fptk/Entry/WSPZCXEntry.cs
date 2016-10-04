namespace Aisino.Fwkp.Fptk.Entry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Fptk;
    using log4net;
    using System;
    using Framework.Plugin.Core.Util;
    using Form;
    public class WSPZCXEntry : AbstractCommand
    {
        private ILog log = LogUtil.GetLogger<WSPZCXEntry>();

        protected override void RunCommand()
        {
            base.ShowForm<WSPZCXForm>();
        }

        protected override bool SetValid()
        {
            new FpManager();
            return ((int)TaxCardFactory.CreateTaxCard().TaxMode == 2);
        }
    }
}

