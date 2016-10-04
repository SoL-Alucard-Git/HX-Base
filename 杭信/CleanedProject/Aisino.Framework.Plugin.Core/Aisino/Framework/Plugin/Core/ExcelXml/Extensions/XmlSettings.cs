namespace Aisino.Framework.Plugin.Core.ExcelXml.Extensions
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Serialization;

    public abstract class XmlSettings
    {
        private static string string_0;
        private static XmlDocument xmlDocument_0;

        static XmlSettings()
        {
            
            xmlDocument_0 = new XmlDocument();
            string_0 = Path.ChangeExtension(Application.ExecutablePath, ".settings.xml");
            try
            {
                xmlDocument_0.Load(string_0);
            }
            catch
            {
                xmlDocument_0.LoadXml("<Configuration><Settings></Settings></Configuration>");
            }
        }

        protected XmlSettings()
        {
            
        }

        public void Load()
        {
            foreach (PropertyInfo info in base.GetType().GetProperties())
            {
                this.method_1(info);
            }
        }

        private string method_0(PropertyInfo propertyInfo_0, out bool bool_0)
        {
            XmlSettingAttribute[] customAttributes = (XmlSettingAttribute[]) propertyInfo_0.GetCustomAttributes(typeof(XmlSettingAttribute), false);
            bool_0 = false;
            if ((customAttributes != null) && (customAttributes.Length > 0))
            {
                bool_0 = customAttributes[0].Encrypt;
                if (!string.IsNullOrEmpty(customAttributes[0].Name))
                {
                    return (base.GetType().Name + "/" + customAttributes[0].Name);
                }
            }
            return (base.GetType().Name + "/" + propertyInfo_0.Name);
        }

        private void method_1(PropertyInfo propertyInfo_0)
        {
            if (!smethod_1(propertyInfo_0))
            {
                bool flag;
                string str2 = this.method_0(propertyInfo_0, out flag);
                string xpath = "Configuration/Settings/" + str2;
                XmlNode node = xmlDocument_0.SelectSingleNode(xpath);
                if (node != null)
                {
                    string innerText = node.InnerText;
                    if (flag)
                    {
                        innerText = EncryptDecrypt.Decrypt(innerText, "XmlSettingsPK_L0020P");
                    }
                    if (smethod_2(propertyInfo_0))
                    {
                        if (propertyInfo_0.PropertyType.FullName == "System.DateTime")
                        {
                            try
                            {
                                DateTime time = DateTime.ParseExact(innerText, @"yyyy-MM-dd\Thh:mm:ss.fff", CultureInfo.InvariantCulture);
                                propertyInfo_0.SetValue(this, time, null);
                            }
                            catch (FormatException)
                            {
                            }
                        }
                        else
                        {
                            try
                            {
                                propertyInfo_0.SetValue(this, Convert.ChangeType(innerText, propertyInfo_0.PropertyType, CultureInfo.InvariantCulture), null);
                            }
                            catch (FormatException)
                            {
                            }
                        }
                    }
                    else
                    {
                        if (!propertyInfo_0.PropertyType.IsSerializable)
                        {
                            throw new NotSupportedException("Unsupported data found in " + base.GetType().Name + " class");
                        }
                        XmlSerializer serializer = new XmlSerializer(propertyInfo_0.PropertyType);
                        if (node.FirstChild != null)
                        {
                            XmlNodeReader xmlReader = new XmlNodeReader(node.FirstChild);
                            propertyInfo_0.SetValue(this, serializer.Deserialize(xmlReader), null);
                        }
                    }
                }
            }
        }

        private void method_2(PropertyInfo propertyInfo_0)
        {
            if (!smethod_1(propertyInfo_0))
            {
                bool flag;
                string str = this.method_0(propertyInfo_0, out flag);
                string xpath = "Configuration/Settings/" + str;
                XmlNode node = xmlDocument_0.SelectSingleNode(xpath);
                if (node == null)
                {
                    node = smethod_0(xpath);
                }
                if (smethod_2(propertyInfo_0))
                {
                    string str3;
                    if (propertyInfo_0.PropertyType.FullName == "System.DateTime")
                    {
                        str3 = ((DateTime) propertyInfo_0.GetValue(this, null)).ToString(@"yyyy-MM-dd\Thh:mm:ss.fff", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        str3 = propertyInfo_0.GetValue(this, null).ToString();
                    }
                    if (flag)
                    {
                        str3 = EncryptDecrypt.Encrypt(str3, "XmlSettingsPK_L0020P");
                    }
                    node.InnerText = str3;
                }
                else
                {
                    XmlNode documentElement;
                    if (!propertyInfo_0.PropertyType.IsSerializable)
                    {
                        throw new NotSupportedException("Unsupported data found in " + base.GetType().Name + " class");
                    }
                    XmlSerializer serializer = new XmlSerializer(propertyInfo_0.PropertyType);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        serializer.Serialize((Stream) stream, propertyInfo_0.GetValue(this, null));
                        stream.Position = 0L;
                        XmlDocument document = new XmlDocument();
                        document.Load(stream);
                        documentElement = document.DocumentElement;
                    }
                    node.RemoveAll();
                    node.AppendChild(xmlDocument_0.ImportNode(documentElement, true));
                }
            }
        }

        public void Save()
        {
            foreach (PropertyInfo info in base.GetType().GetProperties())
            {
                this.method_2(info);
            }
            xmlDocument_0.Save(string_0);
        }

        private static XmlNode smethod_0(string string_1)
        {
            string[] strArray = string_1.Split(new char[] { '/' });
            string xpath = "";
            XmlNode node = xmlDocument_0.SelectSingleNode("Configuration/Settings");
            foreach (string str2 in strArray)
            {
                xpath = xpath + str2;
                if (xmlDocument_0.SelectSingleNode(xpath) == null)
                {
                    string innerXml = node.InnerXml;
                    node.InnerXml = innerXml + "<" + str2 + "></" + str2 + ">";
                }
                node = xmlDocument_0.SelectSingleNode(xpath);
                xpath = xpath + "/";
            }
            return node;
        }

        private static bool smethod_1(MemberInfo memberInfo_0)
        {
            XmlSettingIgnoreAttribute[] customAttributes = (XmlSettingIgnoreAttribute[]) memberInfo_0.GetCustomAttributes(typeof(XmlSettingIgnoreAttribute), false);
            return ((customAttributes != null) && (customAttributes.Length > 0));
        }

        private static bool smethod_2(PropertyInfo propertyInfo_0)
        {
            switch (propertyInfo_0.PropertyType.FullName)
            {
                case "System.DateTime":
                case "System.Byte":
                case "System.SByte":
                case "System.Double":
                case "System.Single":
                case "System.Decimal":
                case "System.Int64":
                case "System.Int32":
                case "System.Int16":
                case "System.UInt64":
                case "System.UInt32":
                case "System.UInt16":
                case "System.Boolean":
                case "System.Char":
                case "System.String":
                    return true;
            }
            return false;
        }
    }
}

