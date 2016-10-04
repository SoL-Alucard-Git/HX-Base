namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;

    internal sealed class BszldcMethod : AbstractService
    {
        private ILog loger = LogUtil.GetLogger<BszldcMethod>();

        protected override object[] doService(object[] param)
        {
            try
            {
                object[] objArray = new object[] { true, true };
                new BSLoginForm { ShowInTaskbar = true }.ShowDialog();
                return objArray;
            }
            catch (Exception exception)
            {
                this.loger.Error("[BszldcMethod函数异常]" + exception.Message);
                MessageManager.ShowMsgBox(exception.ToString());
                return null;
            }
        }
    }
}

