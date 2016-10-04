namespace Aisino.Fwkp.Bmgl.Common
{
    using System;
    using System.IO;
    using System.Xml;

    internal class ReadWriteXml
    {
        private static readonly string BmglXmlPath = GetXmlFilePath.XmlFilePath;

        internal static void CreateXmlFile(string WbjkXmlPath)
        {
            XmlDocument document = new XmlDocument();
            string path = WbjkXmlPath.Remove(WbjkXmlPath.LastIndexOf(@"\"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "gb2312", null);
            document.AppendChild(newChild);
            XmlElement element = document.CreateElement("Configuration");
            document.AppendChild(element);
            XmlElement element2 = document.CreateElement("Bmgl");
            element.AppendChild(element2);
            XmlElement element3 = document.CreateElement("ImportCustomer");
            XmlElement element4 = document.CreateElement("ExportCustomer");
            XmlElement element5 = document.CreateElement("ImportGoods");
            XmlElement element6 = document.CreateElement("ExportGoods");
            XmlElement element7 = document.CreateElement("ImportExpense");
            XmlElement element8 = document.CreateElement("ExportExpense");
            XmlElement element9 = document.CreateElement("ImportRecSen");
            XmlElement element10 = document.CreateElement("ExportRecSen");
            XmlElement element11 = document.CreateElement("ImportPurchase");
            XmlElement element12 = document.CreateElement("ExportPurchase");
            XmlElement element13 = document.CreateElement("ImportCar");
            XmlElement element14 = document.CreateElement("ExportCar");
            XmlElement element15 = document.CreateElement("ImportXHDW");
            XmlElement element16 = document.CreateElement("ExportXHDW");
            element2.AppendChild(element3);
            element2.AppendChild(element4);
            element2.AppendChild(element5);
            element2.AppendChild(element6);
            element2.AppendChild(element7);
            element2.AppendChild(element8);
            element2.AppendChild(element9);
            element2.AppendChild(element10);
            element2.AppendChild(element11);
            element2.AppendChild(element12);
            element2.AppendChild(element13);
            element2.AppendChild(element14);
            element2.AppendChild(element15);
            element2.AppendChild(element16);
            document.Save(WbjkXmlPath);
        }

        internal static string Read(string node)
        {
            string innerText = "";
            if (File.Exists(BmglXmlPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(BmglXmlPath);
                XmlNode node3 = document.DocumentElement.SelectSingleNode("Bmgl").SelectSingleNode(node);
                if (node3 != null)
                {
                    innerText = node3.InnerText;
                }
                return innerText;
            }
            CreateXmlFile(BmglXmlPath);
            return innerText;
        }

        internal static void Write(string node, string NodeInnerText)
        {
            if (File.Exists(BmglXmlPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(BmglXmlPath);
                XmlNode node3 = document.DocumentElement.SelectSingleNode("Bmgl").SelectSingleNode(node);
                if (node3 != null)
                {
                    node3.InnerText = NodeInnerText;
                    document.Save(BmglXmlPath);
                }
            }
        }
    }
}

