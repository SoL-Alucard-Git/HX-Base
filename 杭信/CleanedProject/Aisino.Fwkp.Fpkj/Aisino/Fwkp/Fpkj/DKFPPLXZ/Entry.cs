namespace Aisino.Fwkp.Fpkj.DKFPPLXZ
{
    using Aisino.Framework.Plugin.Core.Command;
    using System;

    public class Entry : AbstractCommand
    {
        protected override void RunCommand()
        {
            new PLXZ().BeginDownload();
        }

        protected override bool SetValid()
        {
            return true;
        }
    }
}

