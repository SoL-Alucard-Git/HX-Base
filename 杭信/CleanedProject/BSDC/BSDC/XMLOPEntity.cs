namespace BSDC
{
    using System;
    using System.IO;
    using System.Xml;

    public class XMLOPEntity
    {
        public static string m_stastrFilePath;
        private XmlDocument xmlDocument_0;
        private XmlNode xmlNode_0;

        static XMLOPEntity()
        {
            
            m_stastrFilePath = AppDomain.CurrentDomain.BaseDirectory + "Volumns.xml";
        }

        public XMLOPEntity()
        {
            
        }

        public string GetAttributeValue(string string_0, string string_1)
        {
            string attribute = "";
            try
            {
                if (!File.Exists(m_stastrFilePath))
                {
                    return attribute;
                }
                if (this.xmlDocument_0 == null)
                {
                    this.xmlDocument_0 = new XmlDocument();
                    this.xmlDocument_0.Load(m_stastrFilePath);
                }
                if (this.xmlNode_0 == null)
                {
                    this.xmlNode_0 = this.xmlDocument_0.DocumentElement.SelectSingleNode(string_0);
                }
                XmlElement element = this.xmlNode_0 as XmlElement;
                if (element == null)
                {
                    return "";
                }
                attribute = element.GetAttribute(string_1);
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
                    this.xmlDocument_0 = new XmlDocument();
                    this.xmlDocument_0.Load(m_stastrFilePath);
                    XmlElement documentElement = this.xmlDocument_0.DocumentElement;
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
            this.xmlNode_0 = this.xmlNode_0.NextSibling;
        }
    }
}

