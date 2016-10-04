namespace Aisino.Fwkp.Wbjk.Service
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class XmlConfig
    {
        private static string supNode = "";
        private static string XmlPath = GetXmlcfg.XmlFilePath;

        internal static string[] ReadXml(string[] SubNodes)
        {
            if (File.Exists(XmlPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(XmlPath);
                XmlNode node = document.DocumentElement.SelectSingleNode(supNode);
                string[] strArray = new string[SubNodes.Length];
                for (int i = 0; i < SubNodes.Length; i++)
                {
                    XmlNode node2 = node.SelectSingleNode(SubNodes[i]);
                    strArray[i] = node2.InnerText;
                }
                return strArray;
            }
            new CreateXmlFile().CreateXml(XmlPath);
            return new string[1];
        }

        internal static void SetSupNode(string SupNode)
        {
            supNode = SupNode;
        }

        internal static void WriteXml(Dictionary<string, string> NodeContent)
        {
            if (File.Exists(XmlPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(XmlPath);
                XmlNode node = document.DocumentElement.SelectSingleNode(supNode);
                foreach (KeyValuePair<string, string> pair in NodeContent)
                {
                    XmlNode introduced6 = node.SelectSingleNode(pair.Key);
                    introduced6.InnerText = pair.Value;
                }
                document.Save(XmlPath);
            }
        }
    }
}

