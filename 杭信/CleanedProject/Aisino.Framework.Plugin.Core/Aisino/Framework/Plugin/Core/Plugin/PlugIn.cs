namespace Aisino.Framework.Plugin.Core.Plugin
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using ns12;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;

    public sealed class PlugIn
    {
        private bool bool_0;
        private static Dictionary<string, string> dictionary_0;
        private Dictionary<string, string> dictionary_1;
        private Dictionary<string, Class135> dictionary_2;
        private Dictionary<string, object> dictionary_3;
        private static readonly ILog ilog_0;
        private List<Class132> list_0;
        private List<string> list_1;
        private string string_0;

        static PlugIn()
        {
            
            ilog_0 = LogUtil.GetLogger<PlugIn>();
            dictionary_0 = null;
        }

        public PlugIn()
        {
            
            this.dictionary_1 = new Dictionary<string, string>();
            this.list_0 = new List<Class132>();
            this.dictionary_2 = new Dictionary<string, Class135>();
            this.dictionary_3 = new Dictionary<string, object>();
            this.list_1 = new List<string>();
        }

        internal string method_0()
        {
            return this.string_0;
        }

        internal void method_1(string string_1)
        {
            this.string_0 = string_1;
        }

        private void method_10(XmlTextReader xmlTextReader_0)
        {
            while (xmlTextReader_0.Read())
            {
                if (xmlTextReader_0.IsStartElement())
                {
                    string str2;
                    if (((str2 = xmlTextReader_0.LocalName) == null) || !(str2 == "SQL"))
                    {
                        throw new Exception1("根节点不支持的子节点：" + xmlTextReader_0.LocalName);
                    }
                    string attribute = xmlTextReader_0.GetAttribute("id");
                    this.method_11(xmlTextReader_0, attribute);
                }
            }
        }

        private void method_11(XmlTextReader xmlTextReader_0, string string_1)
        {
            string str = "";
            string str2 = "";
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            StringBuilder builder = new StringBuilder();
            while (xmlTextReader_0.Read())
            {
                XmlNodeType nodeType = xmlTextReader_0.NodeType;
                if (nodeType != XmlNodeType.Element)
                {
                    if ((nodeType != XmlNodeType.EndElement) || !(xmlTextReader_0.LocalName == "SQL"))
                    {
                        continue;
                    }
                    Dictionary<string, string>.KeyCollection.Enumerator enumerator = dictionary.Keys.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        string current = enumerator.Current;
                        str = str.Replace("#" + current + "#", dictionary[current]);
                    }
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("value", str);
                    dic.Add("type", str2);
                    if (builder.Length > 1)
                    {
                        dic.Add("param", builder.ToString(0, builder.Length - 1));
                    }
                    else
                    {
                        dic.Add("param", "");
                    }
                    SQLUtil.smethod_0(string_1, dic);
                    return;
                }
                if (xmlTextReader_0.LocalName.Equals("value"))
                {
                    str = xmlTextReader_0.ReadString();
                }
                else
                {
                    if (xmlTextReader_0.LocalName.Equals("type"))
                    {
                        str2 = xmlTextReader_0.ReadString();
                        continue;
                    }
                    if (xmlTextReader_0.LocalName.Equals("param"))
                    {
                        builder.Append(xmlTextReader_0.GetAttribute("name")).Append(",").Append(xmlTextReader_0.GetAttribute("type")).Append(";");
                        continue;
                    }
                    if (xmlTextReader_0.LocalName.Equals("var"))
                    {
                        dictionary.Add(xmlTextReader_0.GetAttribute("name"), xmlTextReader_0.GetAttribute("value"));
                    }
                }
            }
        }

        internal List<Class132> method_2()
        {
            return this.list_0;
        }

        internal void method_3(List<Class132> value)
        {
            this.list_0 = value;
        }

        internal Dictionary<string, Class135> method_4()
        {
            return this.dictionary_2;
        }

        internal bool method_5()
        {
            return this.bool_0;
        }

        internal void method_6(bool bool_1)
        {
            this.bool_0 = bool_1;
        }

        internal void method_7(string string_1, string string_2)
        {
            byte[] sourceArray = Convert.FromBase64String("FZoo0+wH8AgXWEjMAFRnOVt+ZImrQik1jiVirx3SQzoTTc8H/D9o32mIm2Fb6CnC");
            byte[] destinationArray = new byte[0x20];
            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
            byte[] buffer3 = new byte[0x10];
            Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
            byte[] buffer4 = AES_Crypt.Decrypt(Convert.FromBase64String("FkC25FGxr7ANG8kSXdMQ1dc1q5h2nMtkTSy90S2NQks6FTRmwMwaGUhrgVdlpMrhTSdJ9l7s5jbUyGMhyCd26w=="), destinationArray, buffer3, null);
            byte[] buffer5 = new byte[0x20];
            Array.Copy(buffer4, 0, buffer5, 0, 0x20);
            byte[] buffer6 = new byte[0x10];
            Array.Copy(buffer4, 0x20, buffer6, 0, 0x10);
            FileStream stream = new FileStream(Path.Combine(string_2, string_1), FileMode.Open);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream.Close();
            XmlTextReader reader = new XmlTextReader(new MemoryStream(AES_Crypt.Decrypt(buffer, buffer5, buffer6, null)));
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    string str;
                    if (((str = reader.LocalName) == null) || !(str == "SQLLIB"))
                    {
                        reader.Close();
                        throw new Exception1("不支持的SQL资源文件");
                    }
                    this.method_10(reader);
                }
            }
            reader.Close();
        }

        internal Class135 method_8(string string_1)
        {
            if (!this.method_4().ContainsKey(string_1))
            {
                Class135 class2;
                this.method_4()[string_1] = class2 = new Class135(string_1, this);
                return class2;
            }
            return this.method_4()[string_1];
        }

        internal object method_9(string string_1)
        {
            object obj2 = null;
            if (this.dictionary_3.TryGetValue(string_1, out obj2))
            {
                return obj2;
            }
            using (List<Class132>.Enumerator enumerator = this.list_0.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    obj2 = enumerator.Current.method_3(string_1);
                    if (obj2 != null)
                    {
                        goto Label_003F;
                    }
                }
                goto Label_005E;
            Label_003F:
                this.dictionary_3[string_1] = obj2;
                return obj2;
            }
        Label_005E:;
            MessageManager.ShowMsgBox("FRM-000009", new string[] { string_1 });
            return null;
        }

        internal static PlugIn smethod_0(string string_1)
        {
            PlugIn in2;
            try
            {
                if (dictionary_0 == null)
                {
                    dictionary_0 = new Dictionary<string, string>();
                    dictionary_0.Add("Auto", "/Aisino/Auto");
                    dictionary_0.Add("Toolbar", "/Aisino/Toolbar");
                    dictionary_0.Add("Menu", "/Aisino/Menu");
                    dictionary_0.Add("Tree", "/Aisino/Tree");
                }
                FormSplashHelper.MsgWait("正在加载插件：" + string_1);
                byte[] sourceArray = Convert.FromBase64String("7Na8rAMiwkrjjdD3ovBEp1+7a77N+o8nsrh3X6B/K99RYmTt2+X7j7sk0z2WhONH");
                byte[] destinationArray = new byte[0x20];
                Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
                byte[] buffer3 = new byte[0x10];
                Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
                byte[] buffer4 = AES_Crypt.Decrypt(Convert.FromBase64String("eqxLBR2DaUHwhQe5q4IYbBEPnlYiMBApixEZLPuKEP5GBTIJsWDHiTZLIa1KTjxql1eMbyDXPUn4BhJVyaLdfA=="), destinationArray, buffer3, null);
                byte[] buffer5 = new byte[0x20];
                Array.Copy(buffer4, 0, buffer5, 0, 0x20);
                byte[] buffer6 = new byte[0x10];
                Array.Copy(buffer4, 0x20, buffer6, 0, 0x10);
                FileStream stream = new FileStream(string_1, FileMode.Open);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                XmlTextReader reader = new XmlTextReader(new MemoryStream(AES_Crypt.Decrypt(buffer, buffer5, buffer6, null)));
                PlugIn @in = smethod_1(reader, Path.GetDirectoryName(string_1));
                @in.method_1(string_1);
                @in.method_6(true);
                reader.Close();
                in2 = @in;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return in2;
        }

        private static PlugIn smethod_1(XmlReader xmlReader_0, string string_1)
        {
            PlugIn @in = new PlugIn();
            while (xmlReader_0.Read())
            {
                if (xmlReader_0.IsStartElement())
                {
                    string str;
                    if (((str = xmlReader_0.LocalName) == null) || !(str == "PlugIn"))
                    {
                        xmlReader_0.Close();
                        throw new Exception2("不支持的插件描述文件");
                    }
                    smethod_2(xmlReader_0, @in, string_1);
                    xmlReader_0.Close();
                }
            }
            return @in;
        }

        private static void smethod_2(XmlReader xmlReader_0, PlugIn plugIn_0, string string_1)
        {
            plugIn_0.dictionary_1 = PropertyUtil.smethod_1(xmlReader_0);
            while (xmlReader_0.Read())
            {
                if ((xmlReader_0.NodeType == XmlNodeType.Element) && xmlReader_0.IsStartElement())
                {
                    string localName = xmlReader_0.LocalName;
                    if (localName.Equals("Runtime"))
                    {
                        if (!xmlReader_0.IsEmptyElement)
                        {
                            Class132.smethod_0(xmlReader_0, plugIn_0, string_1);
                        }
                    }
                    else
                    {
                        if ((!localName.Equals("Auto") && !localName.Equals("Toolbar")) && (!localName.Equals("Menu") && !localName.Equals("Tree")))
                        {
                            throw new Exception2("根节点不支持的子节点：" + xmlReader_0.LocalName);
                        }
                        if (xmlReader_0.AttributeCount > 0)
                        {
                            throw new Exception2(localName + "节点不能设置属性");
                        }
                        string str2 = dictionary_0[localName];
                        Class135 class2 = plugIn_0.method_8(str2);
                        if (!xmlReader_0.IsEmptyElement)
                        {
                            Class135.smethod_0(class2, xmlReader_0, localName);
                        }
                    }
                }
            }
        }

        internal Dictionary<string, string> Properties
        {
            get
            {
                return this.dictionary_1;
            }
        }

        internal List<string> SqlLib
        {
            get
            {
                return this.list_1;
            }
        }
    }
}

