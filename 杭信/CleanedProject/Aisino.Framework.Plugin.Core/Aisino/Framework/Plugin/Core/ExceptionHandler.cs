namespace Aisino.Framework.Plugin.Core
{
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Windows.Forms;

    public class ExceptionHandler
    {
        public ExceptionHandler()
        {
            
        }

        public static void HandleError(BaseException baseException_0)
        {
            if ((baseException_0.ExceptionID != null) && (baseException_0.ExceptionID != string.Empty))
            {
                MessageManager.ShowMsgBox(baseException_0.ExceptionID);
            }
            else
            {
                string str = baseException_0.UserMessage + "\n " + baseException_0.Message;
                string str2 = "系统错误";
                MessageBoxHelper.Show(str, str2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public static void HandleError(Exception exception_0)
        {
            string message = exception_0.Message;
            string str2 = "系统错误";
            MessageBoxHelper.Show(message, str2, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }
}

