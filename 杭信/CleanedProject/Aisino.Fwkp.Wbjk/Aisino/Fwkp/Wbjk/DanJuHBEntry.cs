namespace Aisino.Fwkp.Wbjk
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Wbjk.Forms;
    using System;

    public sealed class DanJuHBEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            new DJHB().ShowDialog();
        }

        protected override bool SetValid()
        {
            try
            {
                return WbjkEntry.RegFlag_JT;
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
            return true;
        }
    }
}

