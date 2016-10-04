namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Framework.Plugin.Core.Mail;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Collections.Generic;

    public class PopShareMethods : AbstractService
    {
        private ILog loger = LogUtil.GetLogger<PopShareMethods>();

        protected override object[] doService(object[] param)
        {
            try
            {
                object[] objArray = null;
                string str = PropertyUtil.GetValue("POP3_SERVER").Trim();
                string s = PropertyUtil.GetValue("POP3_PORT").Trim();
                int result = 0;
                int.TryParse(s.Trim(), out result);
                if (0 >= result)
                {
                    result = 110;
                }
                PropertyUtil.GetValue("POP3_USER").Trim();
                PropertyUtil.GetValue("POP3_PASS").Trim();
                string str3 = PropertyUtil.GetValue("MAIL_DEL").Trim();
                bool flag = !string.IsNullOrEmpty(str3.Trim()) && (str3 != "0");
                string str4 = string.Empty;
                MailService service = new MailService(str, int.Parse(s));
                List<MailData> list = service.ReceiveMail(flag, out str4);
                service.Close();
                if (list != null)
                {
                    objArray = new object[] { list };
                    service = null;
                    return objArray;
                }
                service = null;
                MessageManager.ShowMsgBox(str4);
                return null;
            }
            catch (Exception exception)
            {
                this.loger.Error("PopShareMethods函数异常" + exception.Message);
                return null;
            }
        }
    }
}

