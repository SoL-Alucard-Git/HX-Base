namespace Aisino.Fwkp.Wbjk
{
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Wbjk.Forms;
    using System;

    public sealed class DanJuZuoFeiCREntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            new XSDJZuoFeiCR { ShowInTaskbar = false }.ShowDialog();
        }
    }
}

