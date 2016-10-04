namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core.Command;
    using System;

    public class InvoiceReportEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            new InvoiceReportForm().Show(FormMain.AisinoPNL);
        }

        protected override bool SetValid()
        {
            return false;
        }
    }
}

