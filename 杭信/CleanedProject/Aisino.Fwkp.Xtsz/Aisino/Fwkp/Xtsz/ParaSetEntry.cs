namespace Aisino.Fwkp.Xtsz
{
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Xtsz.Forms;
    using System;
    using System.Windows.Forms;

    public sealed class ParaSetEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            new Config().ResetAccountData();
            ParaSetForm instance = ParaSetForm.GetInstance();
            instance.StartPosition = FormStartPosition.CenterScreen;
            instance.ShowInTaskbar = true;
            instance.IsInitialFormFlag = false;
            instance.ShowDialog();
        }
    }
}

