namespace Aisino.Fwkp.Xtsz
{
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Xtsz.Forms;
    using System;
    using System.Windows.Forms;

    public sealed class SystemSetEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            new Config().ResetAccountData();
            new WizardParaSetForm { StartPosition = FormStartPosition.CenterScreen, ShowInTaskbar = true, IsInitialFormFlag = true }.ShowDialog();
        }
    }
}

