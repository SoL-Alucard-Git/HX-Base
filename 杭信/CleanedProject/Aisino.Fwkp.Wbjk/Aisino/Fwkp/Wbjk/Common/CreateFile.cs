namespace Aisino.Fwkp.Wbjk.Common
{
    using Aisino.Fwkp.Wbjk.Service;
    using System;
    using System.IO;
    using System.Xml;

    public class CreateFile
    {
        private XmlDocument xmlDoc = new XmlDocument();

        private XmlElement AddXmlTypeNode(string display, string value)
        {
            XmlElement element = this.xmlDoc.CreateElement("type");
            element.SetAttribute("display", display);
            element.SetAttribute("value", value);
            return element;
        }

        internal void CreateXmlFile(string WbjkXmlPath)
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
            XmlElement element2 = this.xmlDoc.CreateElement("TxtImport");
            element2.SetAttribute("Path", "");
            element2.SetAttribute("JinEType", "BuHanShui");
            element2.SetAttribute("InvType", "Common");
            element.AppendChild(element2);
            XmlElement element3 = this.xmlDoc.CreateElement("ExcelImport");
            element3.SetAttribute("JinEType", "BuHanShui");
            element3.SetAttribute("InvType", "Common");
            element3.SetAttribute("File1Path", "");
            element3.SetAttribute("File2Path", "");
            element.AppendChild(element3);
            XmlElement element4 = this.xmlDoc.CreateElement("TxtZuoFeiImport");
            element4.SetAttribute("Path", "");
            element.AppendChild(element4);
            XmlElement element5 = this.xmlDoc.CreateElement("DanJuLuRu");
            element5.SetAttribute("IsJiaoYan", "True");
            element.AppendChild(element5);
            XmlElement element6 = this.xmlDoc.CreateElement("FaPiaoExport");
            element6.SetAttribute("Path", @"C:\INVOICEOUT.txt");
            element6.SetAttribute("MakeaList", "False");
            element.AppendChild(element6);
            XmlElement element7 = this.xmlDoc.CreateElement("DanJuMerge");
            element7.SetAttribute("KHSH", "KH");
            element7.SetAttribute("SimpleComplex", "Simple");
            element7.SetAttribute("IsMergeBZ", "false");
            element7.SetAttribute("IsBlankBZAdd", "false");
            element.AppendChild(element7);
            XmlElement element8 = this.xmlDoc.CreateElement("FPQueryExcelExport");
            element8.SetAttribute("Path", "");
            element8.SetAttribute("AllDate", "True");
            element8.SetAttribute("ContainStartDay", "False");
            element8.SetAttribute("ContainEndDay", "False");
            element.AppendChild(element8);
            XmlElement element9 = this.xmlDoc.CreateElement("FPQueryXMLExport");
            element9.SetAttribute("Path", "");
            element9.SetAttribute("AllDate", "True");
            element9.SetAttribute("ContainStartDay", "False");
            element9.SetAttribute("ContainEndDay", "False");
            element.AppendChild(element9);
            XmlElement element10 = this.xmlDoc.CreateElement("FPSPQueryExcelExport");
            element10.SetAttribute("Path", "");
            element10.SetAttribute("AllDate", "True");
            element10.SetAttribute("ContainStartDay", "False");
            element10.SetAttribute("ContainEndDay", "False");
            element.AppendChild(element10);
            XmlElement element11 = this.xmlDoc.CreateElement("InvType");
            element.AppendChild(element11);
            element11.AppendChild(this.AddXmlTypeNode("全部种类(专、普)", "a"));
            element11.AppendChild(this.AddXmlTypeNode("增值税普通发票", "c"));
            element11.AppendChild(this.AddXmlTypeNode("增值税专用发票", "s"));
            element11.AppendChild(this.AddXmlTypeNode("货物运输业增值税专用发票", "f"));
            element11.AppendChild(this.AddXmlTypeNode("机动车销售统一发票", "j"));
            XmlElement element12 = this.xmlDoc.CreateElement("InvType_1");
            element.AppendChild(element12);
            element12.AppendChild(this.AddXmlTypeNode("全部种类", "a"));
            element12.AppendChild(this.AddXmlTypeNode("增值税普通发票", "c"));
            element12.AppendChild(this.AddXmlTypeNode("增值税专用发票", "s"));
            element12.AppendChild(this.AddXmlTypeNode("货物运输业增值税专用发票", "f"));
            element12.AppendChild(this.AddXmlTypeNode("机动车销售统一发票", "j"));
            XmlElement element13 = this.xmlDoc.CreateElement("FaPiao");
            element.AppendChild(element13);
            element13.AppendChild(this.AddXmlTypeNode("普通发票", "c"));
            element13.AppendChild(this.AddXmlTypeNode("专用发票", "s"));
            XmlElement element14 = this.xmlDoc.CreateElement("JYGZ");
            element.AppendChild(element14);
            element14.AppendChild(this.AddXmlTypeNode("所有单据", "a"));
            element14.AppendChild(this.AddXmlTypeNode("超过开票限额", "c"));
            element14.AppendChild(this.AddXmlTypeNode("超过开票误差", "s"));
            element14.AppendChild(this.AddXmlTypeNode("包含多税率", "x"));
            XmlElement element15 = this.xmlDoc.CreateElement("SLV");
            element15.InnerText = "0.17;0.13;0.11;0.06;0.05;0.04;0.03";
            element.AppendChild(element15);
            this.xmlDoc.Save(WbjkXmlPath);
        }
    }
}

