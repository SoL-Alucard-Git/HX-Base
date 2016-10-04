namespace Aisino.Fwkp.Fplygl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public sealed class GetStockInfoService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            List<InvVolumeApp> invStock = new List<InvVolumeApp>();
            invStock = card.GetInvStock();
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
            document.PreserveWhitespace = false;
            document.AppendChild(newChild);
            XmlElement element = document.CreateElement("FPXT");
            document.AppendChild(element);
            XmlElement element2 = document.CreateElement("OUTPUT");
            element.AppendChild(element2);
            if (card.get_RetCode() == 0)
            {
                XmlElement element3 = document.CreateElement("CODE");
                element3.InnerText = "0000";
                element2.AppendChild(element3);
                XmlElement element4 = document.CreateElement("MESS");
                element4.InnerText = string.Empty;
                element2.AppendChild(element4);
                XmlElement element5 = document.CreateElement("DATA");
                element2.AppendChild(element5);
                if (invStock != null)
                {
                    foreach (InvVolumeApp app in invStock)
                    {
                        XmlElement element6 = document.CreateElement("FPMX");
                        element2.AppendChild(element6);
                        XmlElement element7 = document.CreateElement("FPZL");
                        element7.InnerText = app.InvType.ToString();
                        element6.AppendChild(element7);
                        XmlElement element8 = document.CreateElement("LBDM");
                        element8.InnerText = app.TypeCode;
                        element6.AppendChild(element8);
                        XmlElement element9 = document.CreateElement("QSHM");
                        element9.InnerText = app.HeadCode.ToString();
                        element6.AppendChild(element9);
                        XmlElement element10 = document.CreateElement("FPFS");
                        element10.InnerText = app.Number.ToString();
                        element6.AppendChild(element10);
                        XmlElement element11 = document.CreateElement("GMRQ");
                        element11.InnerText = app.BuyDate.ToString("yyyy-MM-dd");
                        element6.AppendChild(element11);
                    }
                }
            }
            else
            {
                XmlElement element12 = document.CreateElement("CODE");
                element12.InnerText = card.get_ErrCode();
                element2.AppendChild(element12);
                XmlElement element13 = document.CreateElement("MESS");
                element13.InnerText = MessageManager.GetMessageInfo(card.get_ErrCode());
                element2.AppendChild(element13);
            }
            document.PreserveWhitespace = true;
            return new object[] { document.InnerXml };
        }
    }
}

