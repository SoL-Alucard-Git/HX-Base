namespace Aisino.Fwkp.Fplygl
{
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Fplygl.Form.WSFTP_6.Common;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public sealed class QueryAllocateInfoService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            List<string> queryParams = new List<string>();
            foreach (object obj2 in param)
            {
                queryParams.Add(obj2.ToString());
            }
            XmlDocument document = AllocateCommon.CreateAllocateQueryInput(queryParams);
            int num = 0;
            string xml = string.Empty;
            num = HttpsSender.SendMsg("0040", document.InnerXml, ref xml);
            if (num != 0)
            {
                XmlDocument document2 = new XmlDocument();
                XmlDeclaration newChild = document2.CreateXmlDeclaration("1.0", "GBK", null);
                document2.PreserveWhitespace = false;
                document2.AppendChild(newChild);
                XmlElement element = document2.CreateElement("FPXT");
                document2.AppendChild(element);
                XmlElement element2 = document2.CreateElement("OUTPUT");
                element.AppendChild(element2);
                XmlElement element3 = document2.CreateElement("CODE");
                element3.InnerText = num.ToString();
                element2.AppendChild(element3);
                XmlElement element4 = document2.CreateElement("MESS");
                element4.InnerText = xml;
                element2.AppendChild(element4);
                document2.PreserveWhitespace = true;
                return new object[] { document2.InnerXml };
            }
            XmlDocument document3 = new XmlDocument();
            document3.LoadXml(xml);
            return new object[] { document3.InnerXml };
        }
    }
}

