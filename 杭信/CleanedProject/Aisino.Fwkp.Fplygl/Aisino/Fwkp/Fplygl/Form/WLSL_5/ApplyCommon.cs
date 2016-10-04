namespace Aisino.Fwkp.Fplygl.Form.WLSL_5
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.GeneralStructure;
    using Aisino.Fwkp.Fplygl.IBLL;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Xml;

    internal class ApplyCommon
    {
        private static string cofPath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Bin\");
        private static string IDKind = string.Empty;
        private static string IDNum = string.Empty;
        private static ILog loger = LogUtil.GetLogger<ApplyCommon>();
        private static bool logFlag = false;
        private static string logPath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Log\");
        private static readonly ILYGL_PSXX psxxDal = BLLFactory.CreateInstant<ILYGL_PSXX>("LYGL_PSXX");
        private static List<AddressInfo> synAddr = new List<AddressInfo>();
        private static TaxCard TaxCardInstance = TaxCardFactory.CreateTaxCard();

        public static string Code2InvtypeMix(string code)
        {
            switch (code)
            {
                case "0":
                    return "增值税专用发票";

                case "2":
                    return "增值税普通发票";

                case "005":
                    return "机动车销售统一发票";

                case "009":
                    return "货物运输业增值税专用发票";

                case "025":
                    return "增值税普通发票(卷票)";

                case "026":
                    return "电子增值税普通发票";
            }
            return "未知类型发票";
        }

        private static XmlDocument CreateAddressInfoInput()
        {
            try
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.PreserveWhitespace = false;
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("FPXT");
                document.AppendChild(element);
                XmlElement element2 = document.CreateElement("INPUT");
                element.AppendChild(element2);
                XmlElement element3 = document.CreateElement("NSRSBH");
                element3.InnerText = TaxCardInstance.get_TaxCode();
                element2.AppendChild(element3);
                document.PreserveWhitespace = true;
                return document;
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                return null;
            }
        }

        public static XmlDocument CreateGetAdminTypeInput()
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
            document.PreserveWhitespace = false;
            document.AppendChild(newChild);
            XmlElement element = document.CreateElement("FPXT");
            document.AppendChild(element);
            XmlElement element2 = document.CreateElement("INPUT");
            element.AppendChild(element2);
            XmlElement element3 = document.CreateElement("BUSINESSID");
            element3.InnerText = "ZGLX_CX";
            element2.AppendChild(element3);
            document.PreserveWhitespace = true;
            return document;
        }

        public static bool DownloadAdminType()
        {
            XmlDocument document = CreateGetAdminTypeInput();
            if (logFlag)
            {
                document.Save(logPath + @"\GetAdminTypeInput.xml");
            }
            string xml = string.Empty;
            if (HttpsSender.SendMsg("0043", document.InnerXml, ref xml) != 0)
            {
                MessageManager.ShowMsgBox(xml);
                return false;
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return ParseGetAdminTypeOutput(doc);
        }

        public static string GetAdminType()
        {
            XmlDocument document = new XmlDocument();
            document.Load(cofPath + @"\AdminType.xml");
            return document.SelectSingleNode("//ZGLX").InnerText;
        }

        public static bool HasAuthorizationWithCode(string typeCode)
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            switch (typeCode)
            {
                case "0":
                    return card.get_QYLX().ISZYFP;

                case "2":
                    return card.get_QYLX().ISPTFP;

                case "005":
                    return card.get_QYLX().ISJDC;

                case "009":
                    return card.get_QYLX().ISHY;

                case "025":
                    return card.get_QYLX().ISPTFPJSP;

                case "026":
                    return card.get_QYLX().ISPTFPDZ;
            }
            return false;
        }

        public static bool HasHxAuthorization()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if (!card.get_QYLX().ISZYFP)
            {
                return card.get_QYLX().ISPTFP;
            }
            return true;
        }

        public static bool HasZcAuthorization()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if ((!card.get_QYLX().ISJDC && !card.get_QYLX().ISHY) && !card.get_QYLX().ISPTFPDZ)
            {
                return card.get_QYLX().ISPTFPJSP;
            }
            return true;
        }

        public static string Invtype2CodeMix(string invType)
        {
            switch (invType)
            {
                case "增值税专用发票":
                    return "0";

                case "增值税普通发票":
                    return "2";

                case "机动车销售统一发票":
                    return "005";

                case "货物运输业增值税专用发票":
                    return "009";

                case "增值税普通发票(卷票)":
                    return "025";

                case "电子增值税普通发票":
                    return "026";
            }
            return "000";
        }

        public static bool IsAllType(string type)
        {
            return type.Equals(string.Empty);
        }

        public static bool IsHxInvType(string type)
        {
            if (!type.Equals("0") && !type.Equals("2"))
            {
                return false;
            }
            return true;
        }

        public static bool IsZcInvType(string type)
        {
            if ((!type.Equals("005") && !type.Equals("009")) && (!type.Equals("025") && !type.Equals("026")))
            {
                return false;
            }
            return true;
        }

        private static bool ParseAddressInfoOutput(XmlDocument doc, out string msg)
        {
            try
            {
                XmlNode node = doc.SelectSingleNode("//CODE");
                XmlNode node2 = doc.SelectSingleNode("//MESS");
                if (!node.InnerText.Equals("0000"))
                {
                    msg = node2.InnerText;
                    return false;
                }
                synAddr.Clear();
                XmlNode node3 = doc.SelectSingleNode("//LPRXX");
                AddressInfo item = new AddressInfo {
                    isSyn = true
                };
                foreach (XmlNode node4 in node3.ChildNodes)
                {
                    if (node4.Name.Equals("XM"))
                    {
                        item.receiverName = node4.InnerText;
                    }
                    else if (node4.Name.Equals("YDDH"))
                    {
                        item.cellphone = node4.InnerText;
                    }
                    else if (node4.Name.Equals("GDDH"))
                    {
                        item.landline = node4.InnerText;
                    }
                    else if (node4.Name.Equals("ZJLX"))
                    {
                        IDKind = node4.InnerText;
                    }
                    else if (node4.Name.Equals("ZJHM"))
                    {
                        IDNum = node4.InnerText;
                    }
                    else if (node4.Name.Equals("JYDZ"))
                    {
                        item.address = node4.InnerText;
                    }
                    else if (node4.Name.Equals("YZBM"))
                    {
                        item.postcode = node4.InnerText;
                    }
                }
                synAddr.Add(item);
                PropertyUtil.SetValue("Aisino.Fwkp.Fplygl.WLSL_5.ExcutiveUpdateTime", TaxCardInstance.GetCardClock().ToString("yyyyMMdd"));
                PropertyUtil.SetValue("Aisino.Fwkp.Fplygl.WLSL_5.ExcutiveName", synAddr[0].receiverName);
                PropertyUtil.SetValue("Aisino.Fwkp.Fplygl.WLSL_5.ExcutiveIDKind", IDKind);
                PropertyUtil.SetValue("Aisino.Fwkp.Fplygl.WLSL_5.ExcutiveIDNum", IDNum);
                msg = string.Empty;
                return true;
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                msg = exception.Message;
                return false;
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                msg = exception2.Message;
                return false;
            }
        }

        public static bool ParseGetAdminTypeOutput(XmlDocument doc)
        {
            XmlNode node = doc.SelectSingleNode("//CODE");
            doc.SelectSingleNode("//MESS");
            if (node.InnerText.Equals("0000"))
            {
                string innerText = doc.SelectSingleNode("//ZGLX").InnerText;
                if (innerText.Equals("CTAIS2.0") || innerText.Equals("JS"))
                {
                    doc.Save(cofPath + @"\AdminType.xml");
                    return true;
                }
            }
            return false;
        }

        public static void SynAddressExcutive()
        {
            XmlDocument document = CreateAddressInfoInput();
            if (logFlag)
            {
                document.Save(logPath + @"\SynAddressInput.xml");
            }
            string xml = string.Empty;
            if (HttpsSender.SendMsg("0044", document.InnerXml, ref xml) == 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                if (logFlag)
                {
                    doc.Save(logPath + @"\SynAddressOutput.xml");
                }
                string msg = string.Empty;
                if (ParseAddressInfoOutput(doc, out msg))
                {
                    psxxDal.DeleteSynAddrInfos();
                    foreach (AddressInfo info in synAddr)
                    {
                        psxxDal.InsertAddrInfo(info, false);
                    }
                }
            }
        }
    }
}

