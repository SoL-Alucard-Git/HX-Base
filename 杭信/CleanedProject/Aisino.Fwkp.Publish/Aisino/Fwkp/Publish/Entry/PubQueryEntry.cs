namespace Aisino.Fwkp.Publish.Entry
{
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Publish.Forms;
    using System;
    using Framework.Plugin.Core.Controls;
    public class PubQueryEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            new QueryPubForm().Show(new AisinoPNL());
        }

        protected override bool SetValid()
        {
            return false;
        }
    }
}

