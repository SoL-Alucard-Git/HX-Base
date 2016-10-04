namespace Aisino.Fwkp.Wbjk.Entry
{
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Service;
    using System;

    public class ShareInterfaceEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            ServiceFactory.RegPubService("Aisino.Fwkp.Wbjk.FPZFSuccess", "Aisino.Fwkp.Wbjk.dll", "Aisino.Fwkp.Wbjk.Entry.Wbjk_FPZFSuccess", null);
        }
    }
}

