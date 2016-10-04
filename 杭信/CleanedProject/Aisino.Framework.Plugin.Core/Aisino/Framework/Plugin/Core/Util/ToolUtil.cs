namespace Aisino.Framework.Plugin.Core.Util
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Plugin;
    using Aisino.Framework.Plugin.Core.Tree;
    using log4net;
    using ns10;
    using ns12;
    using ns6;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;

    public sealed class ToolUtil
    {
        private static Encoding encoding_0;
        private static readonly ILog ilog_0;
        public static Dictionary<string, int> JSPrint;
        private static SortedDictionary<int, string> sortedDictionary_0;
        private static string string_0;
        private static string string_1;

        public static  event GetLoginUserInfoDelegate GetLoginUserInfoEvent;

        static ToolUtil()
        {
            
            string_0 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\Config\Print");
            JSPrint = new Dictionary<string, int>();
            sortedDictionary_0 = new SortedDictionary<int, string>();
            encoding_0 = null;
            string_1 = "GBK";
            ilog_0 = LogUtil.GetLogger<ToolUtil>();
        }

        public ToolUtil()
        {
            
        }

        public static string CheckPath(string string_2)
        {
            string str = string.Empty;
            try
            {
                string pattern = @"^[a-zA-Z]:\\([\w][\s])*";
                string str3 = @"(\.\.\\)*";
                if (Regex.IsMatch(string_2, pattern))
                {
                    return string_2;
                }
                if (Regex.IsMatch(string_2, str3))
                {
                    bool flag = true;
                    int num2 = 0;
                    string str5 = string_2;
                    while (flag)
                    {
                        if (str5.IndexOf(@"..\", 0) > -1)
                        {
                            num2++;
                            str5 = str5.Substring(3, str5.Length - 3);
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string str4 = string.Empty;
                    string[] strArray = directoryName.Split(new string[] { @"\" }, StringSplitOptions.None);
                    for (int i = 0; i < (strArray.Length - num2); i++)
                    {
                        str4 = str4 + strArray[i] + @"\";
                    }
                    return (str4 + str5);
                }
                str = "";
            }
            catch
            {
            }
            return str;
        }

        public static string FormatDateTime(DateTime dateTime_0)
        {
            return (smethod_2(dateTime_0.Year, 4) + "-" + smethod_2(dateTime_0.Month, 2) + "-" + smethod_2(dateTime_0.Day, 2) + " " + smethod_2(dateTime_0.Hour, 2) + ":" + smethod_2(dateTime_0.Minute, 2) + ":" + smethod_2(dateTime_0.Second, 2));
        }

        public static string FormatDateTimeEx(object object_0)
        {
            if ((object_0 == null) || (object_0.ToString() == ""))
            {
                return "";
            }
            string s = object_0.ToString().Trim();
            string str2 = "";
            string str3 = "";
            int index = s.IndexOf(' ');
            int length = s.Length;
            string oldValue = "";
            for (int i = 0; i < length; i++)
            {
                if ((s[i] < '0') || (s[i] > '9'))
                {
                    break;
                }
                oldValue = oldValue + s[i];
            }
            if (oldValue == "99")
            {
                s = s.Replace(oldValue, "18" + oldValue);
            }
            if (index != -1)
            {
                str2 = s.Substring(0, index);
                str3 = s.Substring(index);
                if (str3.Length <= 0)
                {
                }
            }
            else
            {
                str2 = s;
                str3 = "";
            }
            str2 = str2.Replace(".", "-").Replace(@"\", "-").Replace("/", "-").Replace(",", "-");
            str3 = str3.Replace(".", ":").Replace("/", ":").Replace(",", ":").Replace(@"\", ":").Replace("-", ":");
            s = str2 + str3;
            DateTime now = DateTime.Now;
            DateTime.TryParse(s, out now);
            return FormatDateTime(now);
        }

        public static byte[] FromBase64String(string string_2)
        {
            return Convert.FromBase64String(string_2);
        }

        public static int GetByteCount(string string_2)
        {
            if (encoding_0 == null)
            {
                encoding_0 = Encoding.GetEncoding(string_1);
            }
            return encoding_0.GetByteCount(string_2);
        }

        public static byte[] GetBytes(string string_2)
        {
            if (encoding_0 == null)
            {
                encoding_0 = Encoding.GetEncoding(string_1);
            }
            return encoding_0.GetBytes(string_2);
        }

        public static Encoding GetEncoding()
        {
            if (encoding_0 == null)
            {
                encoding_0 = Encoding.GetEncoding(string_1);
            }
            return encoding_0;
        }

        public static Dictionary<string, int> GetJsPrintTemplate()
        {
            try
            {
                if (JSPrint.Count <= 0)
                {
                    int num = 10;
                    JSPrint.Add("NEW76mmX177mm", 0);
                    JSPrint.Add("NEW76mmX152mm", 1);
                    JSPrint.Add("NEW76mmX127mm", 2);
                    JSPrint.Add("NEW57mmX177mm", 3);
                    JSPrint.Add("NEW57mmX152mm", 4);
                    JSPrint.Add("NEW57mmX127mm", 5);
                    JSPrint.Add("SH76mmX177mm", 6);
                    JSPrint.Add("HLJ76mmX177mm", 7);
                    JSPrint.Add("BJ76mmX177mm", 8);
                    JSPrint.Add("YN76mmX127mm", 9);
                    JSPrint.Add("YN76mmX177mm", 10);
                    sortedDictionary_0.Clear();
                    if (Directory.Exists(string_0))
                    {
                        DirectoryInfo info = new DirectoryInfo(string_0);
                        foreach (FileInfo info2 in info.GetFiles())
                        {
                            string str = info2.Name.Replace(info2.Extension, "");
                            if (str.StartsWith("Extend_"))
                            {
                                int key = smethod_0(str.Split(new char[] { '_' })[2]);
                                if (!sortedDictionary_0.ContainsKey(key) && (sortedDictionary_0.Count <= 0xf4))
                                {
                                    sortedDictionary_0.Add(key, str);
                                }
                            }
                        }
                        foreach (string str2 in sortedDictionary_0.Values)
                        {
                            if (!JSPrint.ContainsKey(str2))
                            {
                                JSPrint.Add(str2, ++num);
                            }
                        }
                    }
                }
                return JSPrint;
            }
            catch (Exception exception)
            {
                ilog_0.Debug(exception.ToString());
                return JSPrint;
            }
        }

        public static Dictionary<string, string>[] GetPlugIn()
        {
            ArrayList list = new ArrayList();
            foreach (PlugIn @in in PlugInTree.smethod_0())
            {
                if (!@in.Properties.ContainsKey("hidden") || !@in.Properties["hidden"].Equals("true"))
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (Class132 class2 in @in.method_2())
                    {
                        builder.Append(class2.Assembly).Append(";");
                    }
                    Dictionary<string, string> properties = new Dictionary<string, string>();
                    properties = @in.Properties;
                    if (!properties.ContainsKey("dll"))
                    {
                        properties.Add("dll", builder.ToString(0, builder.Length - 1));
                    }
                    if (!properties.ContainsKey("menifest"))
                    {
                        properties.Add("menifest", @in.method_0());
                    }
                    list.Add(properties);
                }
            }
            return (Dictionary<string, string>[]) list.ToArray(typeof(Dictionary<string, string>));
        }

        public static int GetReturnErrCode(string string_2)
        {
            int num = -1;
            if (string.IsNullOrWhiteSpace(string_2))
            {
                return num;
            }
            if (string_2.IndexOf("_") == -1)
            {
                if (string_2 == "0000")
                {
                    return 0;
                }
                return 1;
            }
            if (string_2.StartsWith("CA_") && TaxCardFactory.CreateTaxCard().SoftVersion.Equals("FWKP_V2.0_Svr_Client"))
            {
                return 1;
            }
            return int.Parse(string_2.Split(new char[] { '_' })[1]);
        }

        public static string GetString(byte[] byte_0)
        {
            if (encoding_0 == null)
            {
                encoding_0 = Encoding.GetEncoding(string_1);
            }
            return encoding_0.GetString(byte_0);
        }

        public static string GetString(byte[] byte_0, int int_0, int int_1)
        {
            if (encoding_0 == null)
            {
                encoding_0 = Encoding.GetEncoding(string_1);
            }
            return encoding_0.GetString(byte_0, int_0, int_1);
        }

        public static void LoadPlugInSql(string string_2)
        {
            new PlugIn().method_7("", string_2);
        }

        public static CommandType ParseCommandType(string string_2)
        {
            CommandType type2;
            CommandType text = CommandType.Text;
            if (string_2.Length <= 0)
            {
                return text;
            }
            try
            {
                return (CommandType) Enum.Parse(typeof(CommandType), string_2);
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("FRM-000005", new string[] { exception.ToString() });
                type2 = CommandType.Text;
            }
            return type2;
        }

        public static DbType ParseDbType(string string_2)
        {
            DbType type2;
            DbType ansiString = DbType.AnsiString;
            if (string_2.Length <= 0)
            {
                return ansiString;
            }
            try
            {
                return (DbType) Enum.Parse(typeof(DbType), string_2);
            }
            catch (Exception exception)
            {
                ilog_0.Error("数据类型转换失败：" + string_2);
                ilog_0.Error(exception.ToString());
                type2 = DbType.AnsiString;
            }
            return type2;
        }

        public static void RunFuction(string string_2)
        {
            foreach (PlugIn @in in PlugInTree.smethod_0())
            {
                Dictionary<string, Class135>.Enumerator enumerator = @in.method_4().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    KeyValuePair<string, Class135> current = enumerator.Current;
                    using (List<Aisino.Framework.Plugin.Core.Plugin.Function>.Enumerator enumerator3 = current.Value.method_0().GetEnumerator())
                    {
                        Aisino.Framework.Plugin.Core.Plugin.Function function;
                        Interface2 interface2;
                        while (enumerator3.MoveNext())
                        {
                            function = enumerator3.Current;
                            string id = function.Id;
                            if (function.Id.Equals(string_2) && function.HasPermit)
                            {
                                goto Label_0080;
                            }
                        }
                        continue;
                    Label_0080:
                        interface2 = (Interface2) function.PlugIn.method_9(function.Properties["class"]);
                        interface2.imethod_0();
                        break;
                    }
                }
            }
        }

        public static void SetFuctionEnable(string string_2, bool bool_0)
        {
            foreach (PlugIn @in in PlugInTree.smethod_0())
            {
                Dictionary<string, Class135>.Enumerator enumerator = @in.method_4().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    KeyValuePair<string, Class135> current = enumerator.Current;
                    using (List<Aisino.Framework.Plugin.Core.Plugin.Function>.Enumerator enumerator3 = current.Value.method_0().GetEnumerator())
                    {
                        Aisino.Framework.Plugin.Core.Plugin.Function function;
                        while (enumerator3.MoveNext())
                        {
                            function = enumerator3.Current;
                            string id = function.Id;
                            if (function.Id.Equals(string_2))
                            {
                                goto Label_0074;
                            }
                        }
                        continue;
                    Label_0074:
                        function.method_3(bool_0);
                        break;
                    }
                }
            }
        }

        public static void SetText(TreeNodeCommand treeNodeCommand_0, Aisino.Framework.Plugin.Core.Plugin.Function function_0)
        {
            if (function_0 != null)
            {
                if (function_0.Properties.ContainsKey("tip"))
                {
                    treeNodeCommand_0.ToolTipText = StringUtil.Parse(function_0.Properties["tip"]);
                }
                if (function_0.Properties.ContainsKey("label"))
                {
                    treeNodeCommand_0.Text = StringUtil.Parse(function_0.Properties["label"]);
                }
            }
        }

        private static int smethod_0(object object_0)
        {
            if (object_0 == null)
            {
                return 0;
            }
            int result = 0;
            int.TryParse(object_0.ToString().Trim(), out result);
            return result;
        }

        public static string RMBToDaXie(decimal decimal_0)
        {
            return Class126.smethod_0(decimal_0);
        }

        private static string smethod_2(int int_0, int int_1 = 2)
        {
            string str = string.Empty;
            int length = int_0.ToString().Length;
            if (length < int_1)
            {
                str = new string('0', int_1 - length);
            }
            return (str + int_0.ToString());
        }

        internal static Keys smethod_3(string string_2)
        {
            Keys keys2;
            Keys none = Keys.None;
            if (string_2.Length <= 0)
            {
                return none;
            }
            try
            {
                foreach (string str in string_2.Split(new char[] { '|' }))
                {
                    none |= (Keys) Enum.Parse(typeof(Keys), str);
                }
                return none;
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("FRM-000007", new string[] { exception.ToString() });
                keys2 = Keys.None;
            }
            return keys2;
        }

        internal static TextImageRelation smethod_4(string string_2)
        {
            TextImageRelation relation2;
            TextImageRelation overlay = TextImageRelation.Overlay;
            if (string_2.Length <= 0)
            {
                return overlay;
            }
            try
            {
                foreach (string str in string_2.Split(new char[] { '|' }))
                {
                    overlay |= (TextImageRelation) Enum.Parse(typeof(TextImageRelation), str);
                }
                return overlay;
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("FRM-000008", new string[] { exception.ToString() });
                relation2 = TextImageRelation.Overlay;
            }
            return relation2;
        }

        internal static void smethod_5(ToolStripItem object_0, Aisino.Framework.Plugin.Core.Plugin.Function function_0)
        {
            if (function_0 != null)
            {
                if (function_0.Properties.ContainsKey("icon"))
                {
                    object_0.Image = ResourceUtil.GetBitmap(function_0.Properties["icon"]);
                }
                if (function_0.Properties.ContainsKey("layout"))
                {
                    object_0.TextImageRelation = smethod_4(function_0.Properties["layout"]);
                }
                if (function_0.Properties.ContainsKey("tip"))
                {
                    object_0.ToolTipText = StringUtil.Parse(function_0.Properties["tip"]);
                }
                if (function_0.Properties.ContainsKey("label"))
                {
                    object_0.Text = StringUtil.Parse(function_0.Properties["label"]);
                }
                if ((object_0 is ToolStripMenuItem) && function_0.Properties.ContainsKey("shortcut"))
                {
                    ((ToolStripMenuItem) object_0).ShortcutKeys = smethod_3(function_0.Properties["shortcut"]);
                }
            }
        }

        internal static void smethod_6(string string_2, bool bool_0)
        {
            foreach (PlugIn @in in PlugInTree.smethod_0())
            {
                Dictionary<string, Class135>.Enumerator enumerator = @in.method_4().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    KeyValuePair<string, Class135> current = enumerator.Current;
                    foreach (Aisino.Framework.Plugin.Core.Plugin.Function function in current.Value.method_0())
                    {
                        string id = function.Id;
                        if (function.Id.Equals(string_2))
                        {
                            function.method_1(bool_0);
                        }
                    }
                }
            }
        }

        internal static bool smethod_7(object object_0, object object_1, object object_2, object object_3)
        {
            try
            {
                if (GetLoginUserInfoEvent != null)
                {
                    if (GetLoginUserInfoEvent.GetInvocationList().Length == 1)
                    {
                        object[] objArray = GetLoginUserInfoEvent();
                        bool flag2 = (bool) objArray[0];
                        List<string> list = objArray[3] as List<string>;
                        return (flag2 || list.Contains((string) object_2));
                    }
                    return false;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string ToBase64String(byte[] byte_0)
        {
            return Convert.ToBase64String(byte_0);
        }

        public delegate object[] GetLoginUserInfoDelegate();
    }
}

