namespace Aisino.Fwkp.Fpzpz.Entry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using System;

    public sealed class FaPiaoZhuanPingZhengFind : AbstractCommand
    {
        protected override void RunCommand()
        {
            try
            {
                base.ShowForm<FaPiaoZhuanPingZhengFind>().set_TabText("发票转凭证查询");
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

