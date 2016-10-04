namespace Aisino.Framework.Startup.Login
{
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections.Generic;

    public class UserInfo
    {
        private static bool bool_0;
        private static List<string> list_0;
        private static List<string> list_1;
        private static string string_0;
        private static string string_1;
        private static string string_2;
        private static string string_3;
        private static string string_4;

        public UserInfo()
        {
            
        }

        public static void setValue(ILogin ilogin_0)
        {
            list_0 = ilogin_0.Gnqx;
            string_0 = ilogin_0.Yhmc;
            string_1 = ilogin_0.Yhdm;
            bool_0 = ilogin_0.IsAdmin;
            list_1 = ilogin_0.Jsqx;
            string_2 = ilogin_0.Bk1;
            string_3 = ilogin_0.Bk2;
            string_4 = ilogin_0.Bk3;
            UserNamePatternConvert.UserName = string_0;
        }

        public static string Bk1
        {
            get
            {
                return string_2;
            }
        }

        public static string Bk2
        {
            get
            {
                return string_3;
            }
        }

        public static string Bk3
        {
            get
            {
                return string_4;
            }
        }

        public static List<string> Gnqx
        {
            get
            {
                return list_0;
            }
        }

        public static bool IsAdmin
        {
            get
            {
                return bool_0;
            }
        }

        public static List<string> Jsqx
        {
            get
            {
                return list_1;
            }
        }

        public static string Yhdm
        {
            get
            {
                return string_1;
            }
        }

        public static string Yhmc
        {
            get
            {
                return string_0;
            }
            set
            {
                string_0 = value;
            }
        }
    }
}

