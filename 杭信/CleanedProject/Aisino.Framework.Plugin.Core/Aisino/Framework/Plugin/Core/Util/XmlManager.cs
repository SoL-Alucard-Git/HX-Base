namespace Aisino.Framework.Plugin.Core.Util
{
    using System;
    using System.Collections;
    using System.Xml;

    public class XmlManager
    {
        private static Hashtable hashtable_0;
        private static Hashtable hashtable_1;
        private static Hashtable hashtable_2;
        private static Hashtable hashtable_3;
        private static Hashtable hashtable_4;
        private static string string_0;
        private static XmlDocument xmlDocument_0;

        static XmlManager()
        {
            
            hashtable_3 = new Hashtable();
            hashtable_4 = new Hashtable();
            string_0 = string.Empty;
        }

        private XmlManager()
        {
            
        }

        private XmlManager(string string_1)
        {
            
            string_0 = string_1;
            smethod_0();
        }

        public static Hashtable GetAllAttributes(string string_1, string string_2)
        {
            Hashtable hashtable2;
            try
            {
                GetXmlManager(string_1);
                if (hashtable_1 == null)
                {
                    hashtable_1 = new Hashtable();
                }
                Hashtable hashtable = hashtable_1[string_2] as Hashtable;
                if (hashtable == null)
                {
                    XmlNode node = hashtable_0[string_2] as XmlNode;
                    if (node == null)
                    {
                        return hashtable_4;
                    }
                    hashtable = new Hashtable();
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        hashtable.Add(attribute.Name, attribute.Value);
                    }
                    hashtable_1.Add(string_2, hashtable);
                }
                hashtable2 = hashtable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return hashtable2;
        }

        public static Hashtable GetAttributes(string string_1, string string_2, string string_3, string string_4)
        {
            Hashtable hashtable2;
            try
            {
                GetXmlManager(string_1);
                if (hashtable_1 == null)
                {
                    hashtable_1 = new Hashtable();
                }
                Hashtable hashtable = hashtable_1[string_2] as Hashtable;
                if (hashtable == null)
                {
                    XmlNode node = hashtable_0[string_2] as XmlNode;
                    if (node == null)
                    {
                        return hashtable_4;
                    }
                    hashtable = new Hashtable();
                    bool flag = false;
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        if (!hashtable.Contains(attribute.Name))
                        {
                            hashtable.Add(attribute.Name, attribute.Value);
                        }
                        if ((!flag && string_3.Equals(attribute.Name)) && string_4.Equals(attribute.Value))
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        hashtable = hashtable_4;
                    }
                    hashtable_1.Add(string_2, hashtable);
                }
                hashtable2 = hashtable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return hashtable2;
        }

        public static string GetValue(string string_1, string string_2)
        {
            string innerText;
            string str = string.Empty;
            try
            {
                GetXmlManager(string_1);
                XmlNode node = hashtable_0[string_2] as XmlNode;
                if (node == null)
                {
                    return str;
                }
                innerText = node.InnerText;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return innerText;
        }

        public static Hashtable GetValues(string string_1, string string_2)
        {
            Hashtable hashtable;
            try
            {
                GetXmlManager(string_1);
                XmlNode node = hashtable_0[string_2] as XmlNode;
                if (node == null)
                {
                    return hashtable_4;
                }
                if (hashtable_2 == null)
                {
                    hashtable_2 = new Hashtable();
                }
                Hashtable hashtable2 = hashtable_2[string_2] as Hashtable;
                if (hashtable2 == null)
                {
                    hashtable2 = new Hashtable();
                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        if ((node2.NodeType == XmlNodeType.Element) && !hashtable2.Contains(node2.Name))
                        {
                            hashtable2.Add(node2.Name, node2.InnerText);
                        }
                    }
                    hashtable_2.Add(string_2, hashtable2);
                }
                hashtable = hashtable2;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return hashtable;
        }

        public static XmlManager GetXmlManager(string string_1)
        {
            XmlManager manager2;
            try
            {
                XmlManager manager = hashtable_3[string_1] as XmlManager;
                string_0 = string_1;
                smethod_0();
                if (manager == null)
                {
                    manager = new XmlManager();
                    hashtable_3.Add(string_1, manager);
                }
                manager2 = manager;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return manager2;
        }

        public static bool SetNodeAttribute(string string_1, string string_2, string string_3, string string_4)
        {
            try
            {
                GetXmlManager(string_1);
                XmlNode node = hashtable_0[string_2] as XmlNode;
                if (node == null)
                {
                    return false;
                }
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    if (string_3.Equals(attribute.Name))
                    {
                        attribute.Value = string_4;
                    }
                }
                xmlDocument_0.Save(string_0);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool SetValue(string string_1, string string_2, string string_3)
        {
            bool flag;
            try
            {
                GetXmlManager(string_1);
                XmlNode node = hashtable_0[string_2] as XmlNode;
                if (node == null)
                {
                    return false;
                }
                node.InnerText = string_3;
                xmlDocument_0.Save(string_0);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool SetXML(string string_1, string string_2, string string_3)
        {
            bool flag;
            try
            {
                GetXmlManager(string_1);
                XmlNode node = hashtable_0[string_2] as XmlNode;
                if (node == null)
                {
                    return false;
                }
                node.InnerXml = string_3;
                xmlDocument_0.Save(string_0);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        private static void smethod_0()
        {
            try
            {
                xmlDocument_0 = new XmlDocument();
                xmlDocument_0.Load(string_0);
                hashtable_0 = new Hashtable();
                foreach (XmlNode node in xmlDocument_0.ChildNodes)
                {
                    smethod_1(hashtable_0, node, "");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void smethod_1(Hashtable hashtable_5, XmlNode xmlNode_0, string string_1)
        {
            string key = "".Equals(string_1) ? (xmlNode_0.Name ?? "") : (string_1 + "/" + xmlNode_0.Name);
            if (!hashtable_5.Contains(key))
            {
                hashtable_5.Add(key, xmlNode_0);
            }
            foreach (XmlNode node in xmlNode_0.ChildNodes)
            {
                smethod_1(hashtable_5, node, key);
            }
        }
    }
}

