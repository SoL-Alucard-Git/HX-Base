namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.Plugin.Core.Service;
    using log4net;
    using System;
    using Framework.Plugin.Core.Util;
    internal sealed class EmailSetShareMethods : AbstractService
    {
        private ILog loger = LogUtil.GetLogger<EmailSetShareMethods>();

        protected override object[] doService(object[] param)
        {
            try
            {
                new EmailCanShuSet().ShowDialog();
                return new object[] { "0000" };
            }
            catch (Exception exception)
            {
                this.loger.Error("[EmailSetShareMethods函数异常]" + exception.Message);
                return null;
            }
        }
    }
}

