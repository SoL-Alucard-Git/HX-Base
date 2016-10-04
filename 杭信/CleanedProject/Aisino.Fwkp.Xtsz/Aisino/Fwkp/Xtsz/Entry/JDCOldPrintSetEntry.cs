namespace Aisino.Fwkp.Xtsz.Entry
{
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Print;
    using System;

    public sealed class JDCOldPrintSetEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            FPModelPrint.Create("j", "9956107100_1", 0x90f8b, true).Print(true);
        }
    }
}

