namespace Aisino.Framework.Plugin.Core.Util
{
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Xml;

    public class PropertyUtil
    {
        private static Dictionary<string, string> dictionary_0;
        private static readonly ILog ilog_0;
        private static string string_0;
        private static string string_1;
        private static string string_2;

        static PropertyUtil()
        {
            
            ilog_0 = LogUtil.GetLogger<PropertyUtil>();
            dictionary_0 = new Dictionary<string, string>();
            string_0 = null;
        }

        public PropertyUtil()
        {
            
        }

        public static Rectangle GetRectangle(string string_3, Rectangle rectangle_0)
        {
            try
            {
                if (!dictionary_0.ContainsKey(string_3))
                {
                    return rectangle_0;
                }
                string[] strArray = dictionary_0[string_3].Split(new char[] { ',' });
                return new Rectangle { X = int.Parse(strArray[0]), Y = int.Parse(strArray[1]), Width = int.Parse(strArray[2]), Height = int.Parse(strArray[3]) };
            }
            catch (Exception exception)
            {
                ilog_0.Error("读取矩形位置信息时异常：" + string_3, exception);
                return rectangle_0;
            }
        }

        public static string GetValue(string string_3)
        {
            return GetValue(string_3, "");
        }

        public static string GetValue(string string_3, string string_4)
        {
            if (!dictionary_0.ContainsKey(string_3))
            {
                return string_4;
            }
            return dictionary_0[string_3];
        }

        public static void Save()
        {
            if ((string_0 != null) && (string_1 != null))
            {
                smethod_2();
                using (XmlTextWriter writer = new XmlTextWriter(Path.Combine(string_0, string_1), Encoding.UTF8))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartElement(string_2);
                    foreach (KeyValuePair<string, string> pair in dictionary_0)
                    {
                        writer.WriteStartElement(pair.Key);
                        writer.WriteAttributeString("value", Convert.ToBase64String(Encoding.Unicode.GetBytes(pair.Value)));
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
            }
        }

        public static void SetRectangle(string string_3, Rectangle rectangle_0)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(rectangle_0.X).Append(",").Append(rectangle_0.Y).Append(",").Append(rectangle_0.Width).Append(",").Append(rectangle_0.Height);
            dictionary_0[string_3] = builder.ToString();
        }

        public static void SetValue(string string_3, string string_4)
        {
            dictionary_0[string_3] = string_4;
        }

        public static void SetValue(string string_3, string string_4, bool bool_0)
        {
            SetValue(string_3, string_4);
            if (bool_0)
            {
                Save();
            }
        }

        internal static void smethod_0(string string_3, string string_4)
        {
            if ((string_3 != null) && (string_4 != null))
            {
                string_0 = string_3;
                string_2 = string_4;
                string_1 = string_4 + ".xml";
                smethod_2();
            }
            else
            {
                ilog_0.Error("属性管理器初始化失败！");
            }
        }

        internal static Dictionary<string, string> smethod_1(XmlReader xmlReader_0)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (xmlReader_0.HasAttributes)
            {
                for (int i = 0; i < xmlReader_0.AttributeCount; i++)
                {
                    xmlReader_0.MoveToAttribute(i);
                    dictionary[xmlReader_0.Name] = xmlReader_0.Value;
                }
                xmlReader_0.MoveToElement();
            }
            return dictionary;
        }

        private static void smethod_2()
        {
            if (!Directory.Exists(string_0))
            {
                Directory.CreateDirectory(string_0);
            }
            smethod_3(Path.Combine(string_0, string_1));
            SetValue("Language", Thread.CurrentThread.CurrentUICulture.Name);
        }

        private static bool smethod_3(string string_3)
        {
            if (!File.Exists(string_3))
            {
                return false;
            }
            try
            {
                using (XmlTextReader reader = new XmlTextReader(string_3))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement() && (reader.LocalName == string_2))
                        {
                            goto Label_003E;
                        }
                    }
                    reader.Close();
                    goto Label_008E;
                Label_003E:
                    smethod_4(reader, string_2);
                    reader.Close();
                    return true;
                }
            }
            catch (XmlException exception)
            {
                ilog_0.Error("加载配置文件时异常：", exception);
                MessageManager.ShowMsgBox("FRM-000004", new string[] { exception.ToString() });
            }
        Label_008E:
            return false;
        }

        private static void smethod_4(XmlReader xmlReader_0, string string_3)
        {
            if (!xmlReader_0.IsEmptyElement)
            {
                while (xmlReader_0.Read())
                {
                    XmlNodeType nodeType = xmlReader_0.NodeType;
                    if (nodeType != XmlNodeType.Element)
                    {
                        if ((nodeType == XmlNodeType.EndElement) && (xmlReader_0.LocalName == string_3))
                        {
                            return;
                        }
                    }
                    else
                    {
                        string localName = xmlReader_0.LocalName;
                        try
                        {
                            if (!dictionary_0.ContainsKey(localName))
                            {
                                dictionary_0[localName] = xmlReader_0.HasAttributes ? Encoding.Unicode.GetString(Convert.FromBase64String(xmlReader_0.GetAttribute(0))) : null;
                            }
                        }
                        catch (Exception)
                        {
                            if (!dictionary_0.ContainsKey(localName))
                            {
                                dictionary_0[localName] = xmlReader_0.HasAttributes ? xmlReader_0.GetAttribute(0) : null;
                            }
                        }
                    }
                }
            }
        }
    }
}

