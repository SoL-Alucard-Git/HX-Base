namespace Aisino.Fwkp.Wbjk
{
    using Aisino.Framework.Plugin.Core.Command;
    using System;

    public sealed class DanJuXGEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            base.ShowForm<DJXG>();
        }
    }
}

