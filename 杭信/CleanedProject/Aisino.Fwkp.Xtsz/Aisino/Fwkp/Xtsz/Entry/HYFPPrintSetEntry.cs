namespace Aisino.Fwkp.Xtsz.Entry
{
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Print;
    using System;

    public sealed class HYFPPrintSetEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            FPModelPrint.Create("f", "9956107100", 0x90f8b, true).Print(true);
        }
    }
}

