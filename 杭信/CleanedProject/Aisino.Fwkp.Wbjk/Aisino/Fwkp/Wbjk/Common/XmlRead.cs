namespace Aisino.Fwkp.Wbjk.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class XmlRead
    {
        private string path = GetXmlcfg.FileTableHead;
        private XmlDocument xmlDoc = new XmlDocument();

        public XmlRead()
        {
            this.CreateXml();
            this.xmlDoc.Load(this.path);
        }

        public void CreateXml()
        {
            if (!File.Exists(this.path))
            {
                XmlDeclaration newChild = this.xmlDoc.CreateXmlDeclaration("1.0", "gb2312", null);
                this.xmlDoc.AppendChild(newChild);
                XmlElement element = this.xmlDoc.CreateElement("Aisino");
                this.xmlDoc.AppendChild(element);
                XmlElement element2 = this.xmlDoc.CreateElement("File1");
                element.AppendChild(element2);
                XmlElement element3 = this.xmlDoc.CreateElement("File2");
                element.AppendChild(element3);
                XmlElement element4 = this.xmlDoc.CreateElement("key1");
                element.AppendChild(element4);
                XmlElement element5 = this.xmlDoc.CreateElement("key2");
                element.AppendChild(element5);
                this.xmlDoc.Save(this.path);
            }
        }

        public void Delete(FileName filename)
        {
            string xpath = string.Empty;
            switch (filename)
            {
                case FileName.File1:
                    xpath = "/Aisino/File1";
                    break;

                case FileName.File2:
                    xpath = "/Aisino/File2";
                    break;
            }
            this.xmlDoc.SelectSingleNode(xpath).RemoveAll();
            this.xmlDoc.Save(this.path);
        }

        public void File1AppendChild(string key, string value)
        {
            this.itemAdd(key, value, "/Aisino/File1");
        }

        public void File2AppendChild(string key, string value)
        {
            this.itemAdd(key, value, "/Aisino/File2");
        }

        public Dictionary<string, string> GetItem(FileName filename)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string xpath = string.Empty;
            switch (filename)
            {
                case FileName.File1:
                    xpath = "/Aisino/File1";
                    break;

                case FileName.File2:
                    xpath = "/Aisino/File2";
                    break;
            }
            XmlNodeList childNodes = this.xmlDoc.SelectSingleNode(xpath).ChildNodes;
            foreach (XmlNode node in childNodes)
            {
                XmlElement element = (XmlElement) node;
                string attribute = element.GetAttribute("Key");
                string str3 = element.GetAttribute("Value");
                dictionary.Add(attribute, str3);
            }
            return dictionary;
        }

        public string GetKey1()
        {
            XmlElement element = (XmlElement) this.xmlDoc.SelectSingleNode("/Aisino/key1");
            return element.GetAttribute("value");
        }

        public string GetKey2()
        {
            XmlElement element = (XmlElement) this.xmlDoc.SelectSingleNode("/Aisino/key2");
            return element.GetAttribute("value");
        }

        private void itemAdd(string key, string value, string xPath)
        {
            XmlNodeList list = this.xmlDoc.SelectNodes(xPath);
            if (list.Count > 0)
            {
                XmlNode node = list[0];
                XmlElement newChild = this.xmlDoc.CreateElement("Item");
                newChild.SetAttribute("Key", key);
                newChild.SetAttribute("Value", value);
                node.AppendChild(newChild);
                this.xmlDoc.Save(this.path);
            }
        }

        public void SetKey1(string name)
        {
            ((XmlElement) this.xmlDoc.SelectSingleNode("/Aisino/key1")).SetAttribute("value", name);
            this.xmlDoc.Save(this.path);
        }

        public void SetKey2(string name)
        {
            ((XmlElement) this.xmlDoc.SelectSingleNode("/Aisino/key2")).SetAttribute("value", name);
            this.xmlDoc.Save(this.path);
        }
    }
}

