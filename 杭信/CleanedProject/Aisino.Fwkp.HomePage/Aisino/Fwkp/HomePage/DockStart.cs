namespace Aisino.Fwkp.HomePage
{
    using Aisino.Framework.MainForm;
    using Aisino.Framework.Plugin.Core.Command;
    using System;

    public class DockStart : AbstractCommand
    {
        private void FormMain_ExecuteAfterShowEvent()
        {
            new Home().Show(FormMain.AisinoPNL);
        }

        protected override void RunCommand()
        {
            FormMain.add_ExecuteAfterShowEvent(new FormMain.ExecuteAfterShowDelegate(this, (IntPtr) this.FormMain_ExecuteAfterShowEvent));
        }
    }
}

