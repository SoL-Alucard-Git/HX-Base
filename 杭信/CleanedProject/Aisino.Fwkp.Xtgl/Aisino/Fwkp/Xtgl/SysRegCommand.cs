namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.Plugin.Core.Command;
    using System;
    using System.Windows.Forms;

    public class SysRegCommand : AbstractCommand
    {
        protected override void RunCommand()
        {
            new RegistForm { StartPosition = FormStartPosition.CenterScreen, ShowInTaskbar = true }.ShowDialog();
        }
    }
}

