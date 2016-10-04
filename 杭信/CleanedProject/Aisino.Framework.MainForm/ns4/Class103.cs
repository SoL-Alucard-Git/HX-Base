namespace ns4
{
    using System;
    using System.IO;
    using System.Xml;

    internal static class Class103
    {
        internal static bool bool_0;
        internal static string string_0;
        internal static string string_1;

        static Class103()
        {
            
            string_0 = Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName + @"\Log";
            string_1 = Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName + @"\OutPutFile\EInvExportFile";
            bool_0 = false;
            if (!Directory.Exists(string_0))
            {
                Directory.CreateDirectory(string_0);
            }
            if (!Directory.Exists(string_1))
            {
                Directory.CreateDirectory(string_1);
            }
        }

        internal static void smethod_0(XmlDocument xmlDocument_0, string string_2)
        {
            if ((xmlDocument_0 != null) && bool_0)
            {
                string str = string.Empty;
                if (string_2.LastIndexOf(".xml") > -1)
                {
                    str = string_0 + @"\" + string_2;
                }
                else
                {
                    str = string_0 + @"\" + string_2 + ".xml";
                }
                if (!string.IsNullOrEmpty(str))
                {
                    try
                    {
                        xmlDocument_0.Save(str);
                    }
                    catch (Exception exception)
                    {
                        Class101.smethod_1("保存xml文件(" + str + ")失败：" + exception.ToString());
                    }
                }
            }
        }

        internal static string smethod_1(string string_2)
        {
            string str = string.Empty;
            if ((string_2.LastIndexOf(".xml") <= -1) && (string_2.LastIndexOf(".XML") <= -1))
            {
                str = string_1 + @"\" + string_2 + ".xml";
            }
            else
            {
                str = string_1 + @"\" + string_2;
            }
            if (string.IsNullOrEmpty(str))
            {
                str = "";
            }
            return str;
        }

        internal static void smethod_2(XmlDocument xmlDocument_0, string string_2)
        {
            if (xmlDocument_0 != null)
            {
                string str = string.Empty;
                if ((string_2.LastIndexOf(".xml") <= -1) && (string_2.LastIndexOf(".XML") <= -1))
                {
                    str = string_1 + @"\" + string_2 + ".xml";
                }
                else
                {
                    str = string_1 + @"\" + string_2;
                }
                if (!string.IsNullOrEmpty(str))
                {
                    try
                    {
                        xmlDocument_0.Save(str);
                    }
                    catch (Exception exception)
                    {
                        Class101.smethod_1("保存xml文件(" + str + ")失败：" + exception.ToString());
                    }
                }
            }
        }
    }
}

