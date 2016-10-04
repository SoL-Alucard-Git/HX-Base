namespace Aisino.Fwkp.Bmgl
{
    using Aisino.Framework.Plugin.Core.Command;
    using System;
    using Forms;

    public sealed class BMSPSMManagerEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            base.ShowForm<BMSPSM>();
        }
    }
}

