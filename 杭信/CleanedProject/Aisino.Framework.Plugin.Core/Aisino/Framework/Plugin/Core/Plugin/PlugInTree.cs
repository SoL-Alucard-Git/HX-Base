namespace Aisino.Framework.Plugin.Core.Plugin
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using FTaxBase;
    using log4net;
    using ns10;
    using ns11;
    using ns12;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    public sealed class PlugInTree
    {
        private static Class120 class120_0;
        private static Dictionary<string, Interface3> dictionary_0;
        private static readonly ILog ilog_0;
        private static List<PlugIn> list_0;

        static PlugInTree()
        {
            
            ilog_0 = LogUtil.GetLogger<PlugInTree>();
            class120_0 = new Class120();
            list_0 = new List<PlugIn>();
            dictionary_0 = new Dictionary<string, Interface3>();
            dictionary_0.Add("Class", new Class121());
            dictionary_0.Add("ToolbarItem", new Class124());
            dictionary_0.Add("MenuItem", new Class128());
            dictionary_0.Add("TreeNode", new Class119());
            dictionary_0.Add("TreeNode_All", new Class127());
        }

        public PlugInTree()
        {
            
        }

        public static void RefreshLoad()
        {
            class120_0 = new Class120();
            list_0 = new List<PlugIn>();
            SQLUtil.smethod_1();
            ServiceFactory.ClearService();
            smethod_3(FileUtil.SearchDirectory(Path.Combine(FileUtil.ApplicationRootPath, "plugin"), ".plugin"));
            ilog_0.Info("运行自动执行程序...");
            foreach (object obj2 in smethod_4("/Aisino/Auto", null))
            {
                Interface2 interface2 = obj2 as Interface2;
                if (interface2 != null)
                {
                    interface2.imethod_0();
                }
                else
                {
                    ilog_0.ErrorFormat("自动执行功能类型错误：{0}", obj2.GetType().ToString());
                }
            }
        }

        internal static List<PlugIn> smethod_0()
        {
            return list_0;
        }

        internal static Dictionary<string, Interface3> smethod_1()
        {
            return dictionary_0;
        }

        internal static void smethod_2(Dictionary<string, Interface3> value)
        {
            dictionary_0 = value;
        }

        internal static void smethod_3(List<string> plugInFiles)
        {
            foreach (string str in plugInFiles)
            {
                try
                {
                    smethod_6(PlugIn.smethod_0(str));
                }
                catch (Exception exception)
                {
                    ilog_0.ErrorFormat("加载插件{0}异常:{1}", str, exception);
                    MessageManager.ShowMsgBox("FRM-000001", new string[] { str, exception.ToString() });
                }
            }
            foreach (PlugIn in2 in list_0)
            {
                string str2;
                if (in2.Properties.TryGetValue("depend", out str2))
                {
                    foreach (string str3 in str2.Split(new char[] { ',' }))
                    {
                        bool flag = false;
                        using (List<PlugIn>.Enumerator enumerator3 = list_0.GetEnumerator())
                        {
                            while (enumerator3.MoveNext())
                            {
                                string str4;
                                PlugIn current = enumerator3.Current;
                                if (current.Properties.TryGetValue("id", out str4) && str4.Equals(str3))
                                {
                                    goto Label_0112;
                                }
                            }
                            goto Label_0125;
                        Label_0112:
                            flag = true;
                        }
                    Label_0125:
                        if (!flag)
                        {
                            MessageManager.ShowMsgBox("FRM-000011", new string[] { in2.Properties["id"], str3 });
                        }
                    }
                }
            }
        }

        internal static ArrayList smethod_4(string string_0, object object_0)
        {
            Class120 class2 = smethod_5(string_0);
            if (class2 == null)
            {
                return new ArrayList();
            }
            return class2.method_2(object_0, false);
        }

        internal static Class120 smethod_5(string string_0)
        {
            if ((string_0 == null) || (string_0.Length == 0))
            {
                return null;
            }
            string[] strArray = string_0.Split(new char[] { '/' });
            Class120 class2 = class120_0;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (!class2.method_0().ContainsKey(strArray[i]))
                {
                    ilog_0.ErrorFormat("不存在的插件路径:{0}", strArray[i]);
                    return null;
                }
                class2 = class2.method_0()[strArray[i]];
            }
            return class2;
        }

        private static void smethod_6(PlugIn plugIn_0)
        {
            if (plugIn_0.method_5())
            {
                foreach (Class135 class2 in plugIn_0.method_4().Values)
                {
                    Class120 class3 = smethod_7(class120_0, class2.Name);
                    foreach (Function function in class2.method_0())
                    {
                        bool flag = false;
                        using (List<Function>.Enumerator enumerator3 = class3.method_1().GetEnumerator())
                        {
                            while (enumerator3.MoveNext())
                            {
                                Function current = enumerator3.Current;
                                if (current.Id.Equals(function.Id))
                                {
                                    goto Label_0092;
                                }
                            }
                            goto Label_00A5;
                        Label_0092:
                            flag = true;
                        }
                    Label_00A5:
                        if (!flag)
                        {
                            string str;
                            bool flag2 = true;
                            if (function.Properties.TryGetValue("class", out str))
                            {
                                flag2 = ((Interface2) plugIn_0.method_9(str)).imethod_3();

                            }
                            if (flag2)
                            {
                                class3.method_1().Add(function);
                            }
                        }
                    }
                }
            }
            list_0.Add(plugIn_0);
        }

        private static Class120 smethod_7(Class120 class120_1, string string_0)
        {
            if ((string_0 == null) || (string_0.Length == 0))
            {
                return class120_1;
            }
            string[] strArray = string_0.Split(new char[] { '/' });
            Class120 class2 = class120_1;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (!class2.method_0().ContainsKey(strArray[i]))
                {
                    class2.method_0()[strArray[i]] = new Class120();
                }
                class2 = class2.method_0()[strArray[i]];
            }
            return class2;
        }
    }
}

