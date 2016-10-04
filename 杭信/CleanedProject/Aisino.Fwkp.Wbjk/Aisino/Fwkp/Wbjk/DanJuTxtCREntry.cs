namespace Aisino.Fwkp.Wbjk
{
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Wbjk.Forms;
    using System;

    public sealed class DanJuTxtCREntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            new DanJuTxtCR().ShowDialog();
        }
    }
}

