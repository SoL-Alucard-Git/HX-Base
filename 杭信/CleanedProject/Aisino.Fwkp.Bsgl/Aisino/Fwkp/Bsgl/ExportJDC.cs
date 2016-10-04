namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core.Command;
    using System;
    using System.Windows.Forms;

    public class ExportJDC : AbstractCommand
    {
        protected override void RunCommand()
        {
            new FPExport { StartPosition = FormStartPosition.CenterScreen, ShowInTaskbar = true, isJDCOnly = true }.ShowDialog();
        }

        protected override bool SetValid()
        {
            return true;
        }
    }
}

