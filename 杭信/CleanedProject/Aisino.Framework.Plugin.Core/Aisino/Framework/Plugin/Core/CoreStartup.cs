namespace Aisino.Framework.Plugin.Core
{
    using Aisino.Framework.Plugin.Core.Plugin;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using ns10;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public sealed class CoreStartup
    {
        private static readonly ILog ilog_0;
        private List<string> list_0;
        private static string string_0;
        private static string string_1;

        static CoreStartup()
        {
            
            string_0 = "增值税防伪开票软件";
            string_1 = "Config";
            ilog_0 = LogUtil.GetLogger<CoreStartup>();
        }

        public CoreStartup()
        {
            
            this.list_0 = new List<string>();
        }

        public static void LoadPlugins(Assembly assembly_0)
        {
            FileUtil.ApplicationRootPath = Path.GetDirectoryName(assembly_0.Location);
            ilog_0.Info("加载系统插件...");
            CoreStartup startup = new CoreStartup();
            startup.method_0(Path.Combine(FileUtil.ApplicationRootPath, "plugin"));
            startup.method_1();
        }

        public static void LoadProperty()
        {
            PropertyUtil.smethod_0(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\Config\Common"), string_1);
        }

        private void method_0(string string_2)
        {
            this.list_0.AddRange(FileUtil.SearchDirectory(string_2, ".plugin"));
        }

        private void method_1()
        {
            PlugInTree.smethod_3(this.list_0);
            ilog_0.Info("运行自动执行程序...");
            FormSplashHelper.MsgWait("正在运行自动执行程序...");
            foreach (object obj2 in PlugInTree.smethod_4("/Aisino/Auto", null))
            {
                object[] objArray = obj2 as object[];
                Interface2 interface2 = objArray[0] as Interface2;
                if (interface2 != null)
                {
                    interface2.imethod_0();
                    Function function = objArray[1] as Function;
                    if (function != null)
                    {
                        string str = "false";
                        if (function.Properties.TryGetValue("runOnce", out str) && string.Equals(str, bool.TrueString, StringComparison.OrdinalIgnoreCase))
                        {
                            PropertyUtil.SetValue(function.Id + "HasRunOnce", "1", true);
                        }
                    }
                }
                else
                {
                    ilog_0.ErrorFormat("自动执行功能类型错误：{0}", obj2.GetType().ToString());
                }
            }
        }

        public delegate void SetStatusTextDelegate(string txt);
    }
}

