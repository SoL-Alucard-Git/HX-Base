namespace ns12
{
    using Aisino.Framework.Plugin.Core.Plugin;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    internal sealed class Class135
    {
        private static readonly ILog ilog_0;
        private List<Function> list_0;
        private Aisino.Framework.Plugin.Core.Plugin.PlugIn plugIn_0;
        private string string_0;

        static Class135()
        {
            
            ilog_0 = LogUtil.GetLogger<Class135>();
        }

        internal Class135(string string_1, Aisino.Framework.Plugin.Core.Plugin.PlugIn plugIn_1)
        {
            
            this.list_0 = new List<Function>();
            this.plugIn_0 = plugIn_1;
            this.string_0 = string_1;
        }

        internal List<Function> method_0()
        {
            return this.list_0;
        }

        internal static void smethod_0(Class135 object_0, XmlReader xmlReader_0, string string_1)
        {
            while (xmlReader_0.Read())
            {
                XmlNodeType nodeType = xmlReader_0.NodeType;
                if (nodeType != XmlNodeType.Element)
                {
                    if ((nodeType == XmlNodeType.EndElement) && (xmlReader_0.LocalName == string_1))
                    {
                        return;
                    }
                }
                else
                {
                    bool flag;
                    string localName = xmlReader_0.LocalName;
                    string attribute = xmlReader_0.GetAttribute("type");
                    string str = null;
                    if (localName.Equals("Class"))
                    {
                        str = xmlReader_0.GetAttribute("depend");
                    }
                    if (str == null)
                    {
                        str = xmlReader_0.GetAttribute("id");
                    }
                    string str4 = xmlReader_0.GetAttribute("label");
                    bool flag2 = string.Equals(xmlReader_0.GetAttribute("alwaysShow"), bool.TrueString, StringComparison.OrdinalIgnoreCase);
                    if ((flag = string.Equals(xmlReader_0.GetAttribute("alwaysPermit"), bool.TrueString, StringComparison.OrdinalIgnoreCase) || ToolUtil.smethod_7(localName, attribute, str, str4)) || flag2)
                    {
                        Function item = new Function(object_0.plugIn_0, localName, PropertyUtil.smethod_1(xmlReader_0), flag);
                        object_0.list_0.Add(item);
                        if (!xmlReader_0.IsEmptyElement)
                        {
                            smethod_0(object_0.plugIn_0.method_8(object_0.Name + "/" + item.Id), xmlReader_0, localName);
                        }
                    }
                }
            }
        }

        internal string Name
        {
            get
            {
                return this.string_0;
            }
        }

        internal Aisino.Framework.Plugin.Core.Plugin.PlugIn PlugIn
        {
            get
            {
                return this.plugIn_0;
            }
        }
    }
}

