namespace Aisino.Framework.Plugin.Core.Util
{
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;

    public class ResourceUtil
    {
        internal static Dictionary<string, object> dictionary_0;
        private static readonly ILog ilog_0;

        static ResourceUtil()
        {
            
            ilog_0 = LogUtil.GetLogger<ResourceUtil>();
            dictionary_0 = new Dictionary<string, object>();
        }

        public ResourceUtil()
        {
            
        }

        public static Bitmap GetBitmap(string string_0)
        {
            object obj2 = smethod_3(string_0);
            if (obj2 != null)
            {
                return (obj2 as Bitmap);
            }
            return null;
        }

        internal static void smethod_0(ResourceManager resourceManager_0)
        {
            ResourceSet set = null;
            try
            {
                set = resourceManager_0.GetResourceSet(CultureInfo.CurrentCulture, true, true);
                IDictionaryEnumerator enumerator = set.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    dictionary_0[enumerator.Key.ToString()] = enumerator.Value;
                }
            }
            catch (Exception exception)
            {
                ilog_0.Error("加载资源文件异常:" + resourceManager_0.BaseName, exception);
            }
            finally
            {
                if (resourceManager_0 != null)
                {
                    resourceManager_0.ReleaseAllResources();
                }
                if (set != null)
                {
                    set.Dispose();
                }
            }
        }

        internal static string smethod_1(string string_0, object[] object_0)
        {
            string format = smethod_2(string_0);
            if (object_0 != null)
            {
                format = string.Format(format, object_0);
            }
            return format;
        }

        internal static string smethod_2(string string_0)
        {
            object obj2 = null;
            try
            {
                if (dictionary_0.TryGetValue(string_0, out obj2))
                {
                    return (string) obj2;
                }
                return string_0;
            }
            catch (Exception exception)
            {
                ilog_0.Error("读取资源时异常：" + string_0, exception);
                return string_0;
            }
        }

        private static object smethod_3(string string_0)
        {
            object obj2 = null;
            try
            {
                dictionary_0.TryGetValue(string_0, out obj2);
            }
            catch (Exception exception)
            {
                ilog_0.Error("读取资源时异常：" + string_0, exception);
            }
            return obj2;
        }
    }
}

