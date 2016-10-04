namespace Aisino.Fwkp.Bsgl
{
    using System;
    using System.IO;
    using System.Xml;

    public class XMLOPEntity
    {
        public static string m_stastrFilePath = (AppDomain.CurrentDomain.BaseDirectory + "Volumns.xml");
        private XmlDocument m_xmlDoc;
        private XmlNode m_xmlNode;

        public string GetAttributeValue(string _strNode, string _strAttributeName)
        {
            string attribute = "";
            try
            {
                if (!File.Exists(m_stastrFilePath))
                {
                    return attribute;
                }
                if (this.m_xmlDoc == null)
                {
                    this.m_xmlDoc = new XmlDocument();
                    this.m_xmlDoc.Load(m_stastrFilePath);
                }
                if (this.m_xmlNode == null)
                {
                    this.m_xmlNode = this.m_xmlDoc.DocumentElement.SelectSingleNode(_strNode);
                }
                XmlElement xmlNode = this.m_xmlNode as XmlElement;
                if (xmlNode == null)
                {
                    return "";
                }
                attribute = xmlNode.GetAttribute(_strAttributeName);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return attribute;
        }

        public int GetNodeCount()
        {
            int count = -1;
            try
            {
                if (File.Exists(m_stastrFilePath))
                {
                    this.m_xmlDoc = new XmlDocument();
                    this.m_xmlDoc.Load(m_stastrFilePath);
                    XmlElement documentElement = this.m_xmlDoc.DocumentElement;
                    if (documentElement != null)
                    {
                        count = documentElement.ChildNodes.Count;
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return count;
        }

        public void MoveToNextNode()
        {
            this.m_xmlNode = this.m_xmlNode.NextSibling;
        }
    }
}

