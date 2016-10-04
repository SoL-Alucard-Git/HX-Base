namespace Aisino.Fwkp.Wbjk.Service
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    public class comboboxXMLcfg
    {
        private static string TxtWbcrXmlPath = GetXmlcfg.XmlFilePath;

        internal static BindingSource ReadXMLDict(string XMLnode)
        {
            if (File.Exists(TxtWbcrXmlPath))
            {
                BindingSource source = new BindingSource();
                XmlDocument document = new XmlDocument();
                document.Load(TxtWbcrXmlPath);
                string xpath = "/configuration/" + XMLnode;
                XmlNodeList childNodes = document.SelectSingleNode(xpath).ChildNodes;
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (XmlNode node in childNodes)
                {
                    XmlElement element = (XmlElement) node;
                    string attribute = element.GetAttribute("display");
                    string key = element.GetAttribute("value");
                    dictionary.Add(key, attribute);
                }
                source.DataSource = dictionary;
                return source;
            }
            new CreateXmlFile().CreateXml(TxtWbcrXmlPath);
            return null;
        }
    }
}

