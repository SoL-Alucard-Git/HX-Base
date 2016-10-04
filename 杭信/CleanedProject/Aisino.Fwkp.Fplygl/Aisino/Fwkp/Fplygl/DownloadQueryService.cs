namespace Aisino.Fwkp.Fplygl
{
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Fplygl.Form.WSFTP_6.Common;
    using System;
    using System.Xml;

    public sealed class DownloadQueryService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            string startDate = param[0].ToString();
            string endDate = param[1].ToString();
            string xml = string.Empty;
            int num = HttpsSender.SendMsg("0040", AllocateCommon.RequestListInput(startDate, endDate), ref xml);
            if (num != 0)
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.PreserveWhitespace = false;
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("FPXT");
                document.AppendChild(element);
                XmlElement element2 = document.CreateElement("OUTPUT");
                element.AppendChild(element2);
                XmlElement element3 = document.CreateElement("CODE");
                element3.InnerText = num.ToString();
                element2.AppendChild(element3);
                XmlElement element4 = document.CreateElement("MESS");
                element4.InnerText = xml;
                element2.AppendChild(element4);
                document.PreserveWhitespace = true;
                return new object[] { document.InnerXml };
            }
            XmlDocument document2 = new XmlDocument();
            document2.LoadXml(xml);
            return new object[] { document2.InnerXml };
        }
    }
}

