namespace Aisino.Fwkp.Publish.Entry
{
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using System;

    public sealed class PubEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            FormMain.ExecuteBeforeExitEvent += PubClient.Instance.CloseClient;
            FormMain.ExecuteAfterLoadEvent += PubClient.Instance.AutoRun;
        }

        protected override bool SetValid()
        {
            return !TaxCardFactory.CreateTaxCard().QYLX.ISTDQY;
        }
    }
}

