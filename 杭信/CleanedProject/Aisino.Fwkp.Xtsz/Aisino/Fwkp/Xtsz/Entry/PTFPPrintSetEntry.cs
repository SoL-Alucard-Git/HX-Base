namespace Aisino.Fwkp.Xtsz.Entry
{
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Print;
    using System;

    public sealed class PTFPPrintSetEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            FPModelPrint.Create("c", "1100053620", 0x90f8b, true).Print(true);
        }
    }
}

