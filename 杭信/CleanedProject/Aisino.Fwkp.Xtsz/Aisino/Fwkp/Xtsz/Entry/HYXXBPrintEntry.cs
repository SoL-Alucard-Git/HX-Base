namespace Aisino.Fwkp.Xtsz.Entry
{
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Print;
    using System;

    public sealed class HYXXBPrintEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            string[] strArray = new string[] { "1661477676760150319164114" };
            new HZFPSQDModelPrint(strArray).Print(true);
        }
    }
}

