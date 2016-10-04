namespace Aisino.Framework.Plugin.Core.Util
{
    using log4net;
    using System;
    using System.Collections.Generic;

    public sealed class SQLUtil
    {
        private static Dictionary<string, Dictionary<string, string>> dictionary_0;
        private static ILog ilog_0;

        static SQLUtil()
        {
            
            dictionary_0 = new Dictionary<string, Dictionary<string, string>>();
            ilog_0 = LogUtil.GetLogger<SQLUtil>();
        }

        public SQLUtil()
        {
            
        }

        public static Dictionary<string, string> GetSQL(string string_0)
        {
            return dictionary_0[string_0];
        }

        internal static void smethod_0(object object_0, Dictionary<string, string> dic)
        {
            if (!dictionary_0.ContainsKey((string) object_0))
            {
                dictionary_0.Add((string) object_0, dic);
            }
            else
            {
                ilog_0.WarnFormat("数据库SQL已存在：{0}", object_0);
            }
        }

        internal static void smethod_1()
        {
            dictionary_0.Clear();
        }
    }
}

