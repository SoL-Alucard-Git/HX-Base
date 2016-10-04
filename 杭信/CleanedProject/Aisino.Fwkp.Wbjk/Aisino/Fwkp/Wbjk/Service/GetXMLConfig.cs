namespace Aisino.Fwkp.Wbjk.Service
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class GetXMLConfig
    {
        private string XMLPath;

        internal GetXMLConfig(string XPath)
        {
            this.XMLPath = XPath;
        }

        internal string ReadXML(string Node)
        {
            if (File.Exists(this.XMLPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(this.XMLPath);
                return document.DocumentElement.SelectSingleNode(Node).FirstChild.InnerText;
            }
            new CreateXmlFile().CreateXml(this.XMLPath);
            return "";
        }

        internal void WriteXML(Dictionary<string, string> NodeContent)
        {
            if (File.Exists(this.XMLPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(this.XMLPath);
                foreach (KeyValuePair<string, string> pair in NodeContent)
                {
                    document.DocumentElement.SelectSingleNode(pair.Key).FirstChild.InnerText = pair.Value;
                }
                document.Save(this.XMLPath);
            }
        }
    }
}

