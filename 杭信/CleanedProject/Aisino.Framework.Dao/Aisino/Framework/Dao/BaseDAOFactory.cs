namespace Aisino.Framework.Dao
{
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    public class BaseDAOFactory
    {
        private static IBaseDAO ibaseDAO_0;
        private static IBaseDAO ibaseDAO_1;
        private static object object_0;
        private static object object_1;

        static BaseDAOFactory()
        {
            
            ibaseDAO_0 = null;
            ibaseDAO_1 = null;
            object_0 = new object();
            object_1 = new object();
        }

        public BaseDAOFactory()
        {
            
        }

        public static IBaseDAO GetBaseDAONpgSQL()
        {
            return GetBaseDAONpgSQL(false);
        }

        public static IBaseDAO GetBaseDAONpgSQL(bool bool_0)
        {
            if (ibaseDAO_1 == null)
            {
                lock (object_1)
                {
                    if (ibaseDAO_1 == null)
                    {
                        ibaseDAO_1 = new BaseDAONpgSQL(GetConnString(0), bool_0);
                    }
                }
            }
            return ibaseDAO_1;
        }

        public static IBaseDAO GetBaseDAOSQLite()
        {
            return GetBaseDAOSQLite(false);
        }

        public static IBaseDAO GetBaseDAOSQLite(bool bool_0)
        {
            if (ibaseDAO_0 == null)
            {
                lock (object_0)
                {
                    if (ibaseDAO_0 == null)
                    {
                        ibaseDAO_0 = new BaseDAOSQLite(GetConnString(1), bool_0);
                    }
                }
            }
            return ibaseDAO_0;
        }

        public static string GetConnString(int int_0 = 1)
        {
            string str2 = string.Empty;
            str2 = Path.Combine(PropertyUtil.GetValue("MAIN_PATH", ""), @"Bin\cc3268.dll");
            if (int_0 == 1)
            {
                return string.Format("Data Source={0};Pooling=true;Password=LoveR1314;", str2);
            }
            return "Server=127.0.0.1;Port=5432;User Id=fwkp;Password=AiSinO_618;Database=fwkp;CommandTimeout=0;ConnectionLifeTime=0;Pooling=true;MinPoolSize=10;MaxPoolSize=200";
        }
    }
}

