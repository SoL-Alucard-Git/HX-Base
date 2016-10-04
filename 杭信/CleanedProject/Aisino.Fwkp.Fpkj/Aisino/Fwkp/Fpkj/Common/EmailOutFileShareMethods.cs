namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fpkj.Form.SendFP;
    using log4net;
    using System;
    using System.Collections.Generic;

    internal sealed class EmailOutFileShareMethods : AbstractService
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
                if (3 > param.Length)
                {
                    return null;
                }
                int num1 = (int) param[0];
                List<Dictionary<string, object>> listEmailInfo = (List<Dictionary<string, object>>) param[1];
                string text1 = (string) param[2];
                EmailOutFilePrompt prompt = new EmailOutFilePrompt(listEmailInfo);
                prompt.SetFormTitle(param);
                prompt.ShowDialog();
                return new object[] { "0000" };
            }
            catch (Exception exception)
            {
                this.loger.Error("[EmailOutFileShareMethods函数异常]" + exception.Message);
                return null;
            }
        }
    }
}

