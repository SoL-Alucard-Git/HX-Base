namespace Aisino.Fwkp.Xtsz.Entry
{
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Print;
    using System;

    public sealed class QDPrintSetEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            new QDModelPrint("s", "1100053620", 0x90f8b).Print(true);
        }
    }
}

