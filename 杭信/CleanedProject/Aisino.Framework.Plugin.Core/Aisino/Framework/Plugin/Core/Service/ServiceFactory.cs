namespace Aisino.Framework.Plugin.Core.Service
{
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class ServiceFactory
    {
        private static Dictionary<string, object> dictionary_0;
        private static Dictionary<string, string[]> dictionary_1;
        private static ILog ilog_0;

        static ServiceFactory()
        {
            
            dictionary_0 = new Dictionary<string, object>();
            ilog_0 = LogUtil.GetLogger<ServiceFactory>();
            dictionary_1 = new Dictionary<string, string[]>();
        }

        public ServiceFactory()
        {
            
        }

        public static void ClearService()
        {
            dictionary_1.Clear();
        }

        public static T1 GetService<T1, T2>()
        {
            try
            {
                return (T1) Activator.CreateInstance(typeof(T2));
            }
            catch (Exception exception)
            {
                ilog_0.Error("取得业务对象异常", exception);
                return default(T1);
            }
        }

        public static object[] InvokePubService(string string_0, object[] object_0)
        {
            string directoryName = "";
            try
            {
                if (dictionary_1.ContainsKey(string_0))
                {
                    object obj2 = null;
                    string[] strArray = dictionary_1[string_0];
                    if (dictionary_0.ContainsKey(string_0))
                    {
                        obj2 = dictionary_0[string_0];
                    }
                    else
                    {
                        directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
						//逻辑修改:新的文件无需解密
                        //byte[] sourceArray = Convert.FromBase64String("L3yC7zg8fQWhipDSFB284EahvoXH9kNV6TE843pajbE7Tyo53TJ95N4ahc1nunDe");
                        //byte[] destinationArray = new byte[0x20];
                        //Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
                        //byte[] buffer3 = new byte[0x10];
                        //Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
                        //byte[] buffer4 = AES_Crypt.Decrypt(Convert.FromBase64String("X7xxyMJDoje5XAwsxAOOIAjpm9iH+86h8HJE7kFSLV4K2rN/kb93R5TgadKYp4kTcuz3eg+TV8gMxLkltAAOow=="), destinationArray, buffer3, null);
                        //byte[] buffer5 = new byte[0x20];
                        //Array.Copy(buffer4, 0, buffer5, 0, 0x20);
                        //byte[] buffer6 = new byte[0x10];
                        //Array.Copy(buffer4, 0x20, buffer6, 0, 0x10);
                        //obj2 = Assembly.Load(AES_Crypt.Decrypt(File.ReadAllBytes(Path.Combine(directoryName, strArray[0])), buffer5, buffer6, null)).CreateInstance(strArray[1]);
                        obj2 = Assembly.Load(File.ReadAllBytes(Path.Combine(directoryName, strArray[0]))).CreateInstance(strArray[1]);
                        dictionary_0.Add(string_0, obj2);
                    }
                    if (obj2 == null)
                    {
                        ilog_0.ErrorFormat("初始化名称为{0}的公共方法异常:{1}", string_0, directoryName);
                        return null;
                    }
                    Interface4 interface2 = obj2 as Interface4;
                    if (interface2 != null)
                    {
                        return interface2.imethod_0(object_0);
                    }
                    ilog_0.ErrorFormat("名称为{0}的公共方法类型错误({1})", string_0, obj2.GetType().FullName);
                    return ((Interface4) obj2).imethod_0(object_0);
                }
                return null;
            }
            catch (Exception exception)
            {
                ilog_0.Error("调用公共方法异常:" + string_0 + "," + exception.ToString());
                return null;
            }
        }

        public static bool RegPubService(string string_0, string string_1, string string_2, Assembly assembly_0 = null)
        {
            if (dictionary_1.ContainsKey(string_0))
            {
                ilog_0.ErrorFormat("名称为{0}的公共方法已存在", string_0);
                return false;
            }
            if (string_0.StartsWith("__COM_"))
            {
                string_0 = string_0.Substring(6);
                try
                {
                    if (assembly_0 == null)
                    {
                        assembly_0 = Assembly.LoadFrom(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/" + string_1);
                    }
                    dictionary_0.Add(string_0, assembly_0.CreateInstance(string_2));
                }
                catch (Exception exception)
                {
                    ilog_0.ErrorFormat("接口注册异常：{0}", exception.ToString());
                }
            }
            dictionary_1.Add(string_0, new string[] { string_1, string_2 });
            return true;
        }
    }
}

