namespace Aisino.Fwkp.Wbjk.Service
{
    using Aisino.Fwkp.Wbjk;
    using System;
    using System.IO;
    using System.Xml;

    public class TxtCRConfig
    {
        private bool _FaPiaobool;
        private int _JEIndex;
        private string TxtWbcrXmlPath = GetXmlcfg.XmlFilePath;

        internal string ReadTxtResolverConfig()
        {
            if (File.Exists(this.TxtWbcrXmlPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(this.TxtWbcrXmlPath);
                XmlNode node = document.DocumentElement.SelectSingleNode("TxtJieXiPath");
                XmlNode node2 = document.DocumentElement.SelectSingleNode("JinEandFaPiao");
                XmlNode node3 = node2.SelectSingleNode("JinE");
                XmlNode node4 = node2.SelectSingleNode("FaPiao");
                this._JEIndex = (node3.InnerText == "HanShui") ? 0 : 1;
                this._FaPiaobool = node4.InnerText == "Special";
                return node.InnerText;
            }
            new CreateXmlFile().CreateXml(this.TxtWbcrXmlPath);
            return "";
        }

        internal void WriteTxtResolverConfig(string strTxtpath, JiaGeType jg, InvType fapiao)
        {
            if (File.Exists(this.TxtWbcrXmlPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(this.TxtWbcrXmlPath);
                document.DocumentElement.SelectSingleNode("TxtJieXiPath").InnerText = strTxtpath;
                XmlNode node2 = document.DocumentElement.SelectSingleNode("JinEandFaPiao");
                XmlNode node3 = node2.SelectSingleNode("JinE");
                XmlNode node4 = node2.SelectSingleNode("FaPiao");
                node3.InnerText = jg.ToString();
                node4.InnerText = fapiao.ToString();
                document.Save(this.TxtWbcrXmlPath);
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

