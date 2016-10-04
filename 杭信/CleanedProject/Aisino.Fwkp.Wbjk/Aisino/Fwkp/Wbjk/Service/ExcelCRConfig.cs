namespace Aisino.Fwkp.Wbjk.Service
{
    using Aisino.Fwkp.Wbjk;
    using System;
    using System.IO;
    using System.Xml;

    public class ExcelCRConfig
    {
        private bool _FaPiaobool;
        private int _JEIndex;
        private string ExcelCRXmlPath = GetXmlcfg.XmlFilePath;

        internal void ReadExcelResolverConfig()
        {
            if (File.Exists(this.ExcelCRXmlPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(this.ExcelCRXmlPath);
                XmlNode node = document.DocumentElement.SelectSingleNode("ExcelJEandFP");
                XmlNode node2 = node.SelectSingleNode("JinE");
                XmlNode node3 = node.SelectSingleNode("FaPiao");
                this._JEIndex = (node2.InnerText == "HanShui") ? 0 : 1;
                this._FaPiaobool = node3.InnerText == "Special";
            }
            else
            {
                new CreateXmlFile().CreateXml(this.ExcelCRXmlPath);
            }
        }

        internal void WriteExcelResolverConfig(JiaGeType jg, InvType fapiao)
        {
            if (File.Exists(this.ExcelCRXmlPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(this.ExcelCRXmlPath);
                XmlNode node = document.DocumentElement.SelectSingleNode("ExcelJEandFP");
                XmlNode node2 = node.SelectSingleNode("JinE");
                XmlNode node3 = node.SelectSingleNode("FaPiao");
                node2.InnerText = jg.ToString();
                node3.InnerText = fapiao.ToString();
                document.Save(this.ExcelCRXmlPath);
            }
        }

        internal bool FaPiaobool
        {
            get
            {
                return this._FaPiaobool;
            }
        }

        internal int JEIndex
        {
            get
            {
                return this._JEIndex;
            }
        }
    }
}

