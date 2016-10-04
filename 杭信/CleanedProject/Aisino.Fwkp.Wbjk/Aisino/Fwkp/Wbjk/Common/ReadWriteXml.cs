namespace Aisino.Fwkp.Wbjk.Common
{
    using Aisino.Fwkp.Wbjk;
    using System;
    using System.IO;
    using System.Xml;

    internal class ReadWriteXml
    {
        private static readonly string WbjkXmlPath = ConfigFile.GetXmlFilePath;

        internal static string Read(string node, string attribute)
        {
            string str2;
            try
            {
                string str = "";
                if (!File.Exists(WbjkXmlPath))
                {
                    new CreateFile().CreateXmlFile(WbjkXmlPath);
                }
                XmlDocument document = new XmlDocument();
                document.Load(WbjkXmlPath);
                XmlElement element = document.DocumentElement.SelectSingleNode(node) as XmlElement;
                if (element == null)
                {
                    new CreateFile().CreateXmlFile(WbjkXmlPath);
                    str = Read(node, attribute);
                }
                else
                {
                    str = element.GetAttribute(attribute);
                }
                str2 = str;
            }
            catch
            {
                throw new CustomException("配置文件损坏！请检查配置文件内容\n路径：" + WbjkXmlPath);
            }
            return str2;
        }

        internal static void Write(string node, string attributeName, string attributeValue)
        {
            if (File.Exists(WbjkXmlPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(WbjkXmlPath);
                (document.DocumentElement.SelectSingleNode(node) as XmlElement).SetAttribute(attributeName, attributeValue);
                document.Save(WbjkXmlPath);
            }
        }
    }
}

