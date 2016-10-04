namespace Aisino.Fwkp.Wbjk
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Forms;
    using System;

    public sealed class DanJuZKHZEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            new XSDJZKHuiZong().ShowDialog();
        }

        protected override bool SetValid()
        {
            try
            {
                bool flag = CommonTool.isSPBMVersion();
                return (WbjkEntry.RegFlag_JT && !flag);
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

