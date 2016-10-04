namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.Plugin.Core.Command;
    using System;
    using System.Windows.Forms;

    public class UserManageCommand : AbstractCommand
    {
        protected override void RunCommand()
        {
            new UserForm { StartPosition = FormStartPosition.CenterScreen, ShowInTaskbar = true }.ShowDialog();
        }
    }
}

