namespace Aisino.Fwkp.Fpzpz.Entry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using System;

    public sealed class FaPiaoZhuanPingZheng : AbstractCommand
    {
        protected override void RunCommand()
        {
            try
            {
                base.ShowForm<FaPiaoZhuanPingZheng>().set_TabText("发票转凭证");
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
        }
    }
}

