namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.BusinessObject;
    using log4net;
    using System;
    using Framework.Plugin.Core.Util;
    internal sealed class EmailSendFPTK : AbstractService
    {
        private ILog loger = LogUtil.GetLogger<EmailSetShareMethods>();

        protected override object[] doService(object[] param)
        {
            try
            {
                if (param == null)
                {
                    return null;
                }
                if (0 > param.Length)
                {
                    return null;
                }
                Fpxx fpxxTemp = (Fpxx) param[0];
                if (fpxxTemp == null)
                {
                    return null;
                }
                new EmailManager().SendEmail_FPTK(fpxxTemp);
                return new object[] { "0000" };
            }
            catch (Exception exception)
            {
                this.loger.Error("[EmailSendFPTK函数异常]" + exception.Message);
                return null;
            }
        }
    }
}

