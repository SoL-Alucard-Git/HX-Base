namespace Aisino.Fwkp.Wbjk.Service
{
    using System;
    using System.IO;
    using System.Xml;

    internal class CreateXmlFile
    {
        private XmlDocument xmlDoc = new XmlDocument();

        private XmlElement AddXmlTypeNode(string display, string value)
        {
            XmlElement element = this.xmlDoc.CreateElement("type");
            element.SetAttribute("display", display);
            element.SetAttribute("value", value);
            return element;
        }

        internal void CreateXml(string WbjkXmlPath)
        {
            string path = GetXmlcfg.XmlFilePath.Remove(GetXmlcfg.XmlFilePath.LastIndexOf(@"\"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            XmlDeclaration newChild = this.xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            this.xmlDoc.AppendChild(newChild);
            XmlElement element = this.xmlDoc.CreateElement("configuration");
            this.xmlDoc.AppendChild(element);
            XmlElement element2 = this.xmlDoc.CreateElement("TxtJieXiPath");
            element.AppendChild(element2);
            XmlElement element3 = this.xmlDoc.CreateElement("JinEandFaPiao");
            element.AppendChild(element3);
            XmlElement element4 = this.xmlDoc.CreateElement("JinE");
            XmlElement element5 = this.xmlDoc.CreateElement("FaPiao");
            element4.InnerText = "BuHanShui";
            element5.InnerText = "Common";
            element3.AppendChild(element4);
            element3.AppendChild(element5);
            XmlElement element6 = this.xmlDoc.CreateElement("ExcelJEandFP");
            element.AppendChild(element6);
            XmlElement element7 = this.xmlDoc.CreateElement("JinE");
            XmlElement element8 = this.xmlDoc.CreateElement("FaPiao");
            element7.InnerText = "BuHanShui";
            element8.InnerText = "Common";
            element6.AppendChild(element7);
            element6.AppendChild(element8);
            XmlElement element9 = this.xmlDoc.CreateElement("ZuoFeitextPath");
            element.AppendChild(element9);
            XmlElement element10 = this.xmlDoc.CreateElement("path");
            element9.AppendChild(element10);
            XmlElement element11 = this.xmlDoc.CreateElement("DanJuLuRu");
            element.AppendChild(element11);
            XmlElement element12 = this.xmlDoc.CreateElement("IsJiaoYan");
            element12.InnerText = "True";
            element11.AppendChild(element12);
            XmlElement element13 = this.xmlDoc.CreateElement("FaPiaoCC");
            element.AppendChild(element13);
            XmlElement element14 = this.xmlDoc.CreateElement("path");
            element13.AppendChild(element14);
            XmlElement element15 = this.xmlDoc.CreateElement("KaiQingDan");
            element15.InnerText = "False";
            element13.AppendChild(element15);
            XmlElement element16 = this.xmlDoc.CreateElement("InvType");
            element.AppendChild(element16);
            element16.AppendChild(this.AddXmlTypeNode("全部种类(专、普)", "a"));
            element16.AppendChild(this.AddXmlTypeNode("增值税普通发票", "c"));
            element16.AppendChild(this.AddXmlTypeNode("增值税专用发票", "s"));
            element16.AppendChild(this.AddXmlTypeNode("货物运输业增值税专用发票", "f"));
            element16.AppendChild(this.AddXmlTypeNode("机动车销售统一发票", "j"));
            XmlElement element17 = this.xmlDoc.CreateElement("InvType_1");
            element.AppendChild(element17);
            element17.AppendChild(this.AddXmlTypeNode("全部种类", "a"));
            element17.AppendChild(this.AddXmlTypeNode("增值税普通发票", "c"));
            element17.AppendChild(this.AddXmlTypeNode("增值税专用发票", "s"));
            element17.AppendChild(this.AddXmlTypeNode("货物运输业增值税专用发票", "f"));
            element17.AppendChild(this.AddXmlTypeNode("机动车销售统一发票", "j"));
            XmlElement element18 = this.xmlDoc.CreateElement("FaPiao");
            element.AppendChild(element18);
            element18.AppendChild(this.AddXmlTypeNode("普通发票", "c"));
            element18.AppendChild(this.AddXmlTypeNode("专用发票", "s"));
            XmlElement element19 = this.xmlDoc.CreateElement("JYGZ");
            element.AppendChild(element19);
            element19.AppendChild(this.AddXmlTypeNode("所有单据", "a"));
            element19.AppendChild(this.AddXmlTypeNode("超过开票限额", "c"));
            element19.AppendChild(this.AddXmlTypeNode("超过开票误差", "s"));
            element19.AppendChild(this.AddXmlTypeNode("包含多税率", "x"));
            XmlElement element20 = this.xmlDoc.CreateElement("SLV");
            element20.InnerText = "0.17;0.13;0.11;0.05;0.04;0.03";
            element.AppendChild(element20);
            this.xmlDoc.Save(WbjkXmlPath);
        }
    }
}

