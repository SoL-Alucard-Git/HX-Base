namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.Plugin.Core.Command;
    using System;
    using System.Windows.Forms;

    public class RoleManageCommand : AbstractCommand
    {
        protected override void RunCommand()
        {
            new RoleForm { StartPosition = FormStartPosition.CenterScreen, ShowInTaskbar = true }.ShowDialog();
        }
    }
}

