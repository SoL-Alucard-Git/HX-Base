namespace Aisino.Framework.Plugin.Core.Util
{
    using System;

    public class UserLoginUtil
    {
        public UserLoginUtil()
        {
            
        }

        public static void ChangeRemaindNameAndPassword(string string_0, string string_1, string string_2)
        {
            ChangeRemaindUserName(string_0, string_1);
            UpdateRemaindPasswrodByName(string_1, string_2);
        }

        public static void ChangeRemaindUserName(string string_0, string string_1)
        {
            string str = PropertyUtil.GetValue(string_0);
            if (!string.IsNullOrEmpty(str))
            {
                PropertyUtil.SetValue("Login_" + BitConverter.ToString(ToolUtil.GetBytes(string_1)).Replace("-", ""), str);
                PropertyUtil.SetValue("Login_" + BitConverter.ToString(ToolUtil.GetBytes(string_0)).Replace("-", ""), "");
            }
        }

        public static void UpdateRemaindPasswrodByName(string string_0)
        {
            if (!string.IsNullOrEmpty(PropertyUtil.GetValue("Login_" + BitConverter.ToString(ToolUtil.GetBytes(string_0)).Replace("-", ""))))
            {
                PropertyUtil.SetValue("Login_" + BitConverter.ToString(ToolUtil.GetBytes(string_0)).Replace("-", ""), "");
            }
        }

        public static void UpdateRemaindPasswrodByName(string string_0, string string_1)
        {
            if (!string.IsNullOrEmpty(PropertyUtil.GetValue("Login_" + BitConverter.ToString(ToolUtil.GetBytes(string_0)).Replace("-", ""))))
            {
                string[] strArray = string_1.Split(new char[] { '-' });
                if (strArray.Length == 2)
                {
                    strArray[0] = string_1;
                    PropertyUtil.SetValue("Login_" + BitConverter.ToString(ToolUtil.GetBytes(string_0)).Replace("-", ""), strArray[0] + "-" + strArray[1]);
                }
            }
        }
    }
}

