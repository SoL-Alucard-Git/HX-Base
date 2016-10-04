namespace Aisino.Fwkp.Wbjk
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using System;

    public sealed class ShangPinCXEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            base.ShowForm<SPCX>();
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

