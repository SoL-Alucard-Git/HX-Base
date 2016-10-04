namespace Aisino.Fwkp.Fplygl.Form.WSFTP_6.Common
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.Common;
    using Aisino.Fwkp.Fplygl.GeneralStructure;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Xml;

    public class AllocateCommon
    {
        private static ILog loger = LogUtil.GetLogger<AllocateCommon>();
        private static bool logFlag = false;
        private static string logPath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Log\");
        private static TaxCard TaxCardInstance = TaxCardFactory.CreateTaxCard();

        public static bool AllocateOneVolume(UnlockInvoice unlockVolume, out bool isDeviceError, out string msg)
        {
            XmlDocument document = CreateAllocateInput(unlockVolume);
            if (logFlag)
            {
                document.Save(logPath + @"\AllocateInput.xml");
            }
            string xml = string.Empty;
            if (HttpsSender.SendMsg("0040", document.InnerXml, ref xml) != 0)
            {
                isDeviceError = false;
                msg = xml;
                return false;
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            if (logFlag)
            {
                doc.Save(logPath + @"\AllocateOutput.xml");
            }
            return ParseAllocateOutput(doc, out isDeviceError, out msg);
        }

        public static XmlDocument CreateAllocateInput(UnlockInvoice volumeInfo)
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
                XmlElement element4 = document.CreateElement("KPJH");
                element4.InnerText = TaxCardInstance.get_Machine().ToString();
                element2.AppendChild(element4);
                XmlElement element5 = document.CreateElement("SBBH");
                element5.InnerText = TaxCardInstance.GetInvControlNum();
                element2.AppendChild(element5);
                XmlElement element6 = document.CreateElement("DCBB");
                element6.InnerText = TaxCardInstance.get_StateInfo().DriverVersion;
                element2.AppendChild(element6);
                XmlElement element7 = document.CreateElement("HASH");
                element7.InnerText = DownloadCommon.GetDecimalStr(TaxCardInstance.Get9BitHashTaxCode());
                element2.AppendChild(element7);
                XmlElement element8 = document.CreateElement("YSSH");
                element8.InnerText = TaxCardInstance.get_CompressCode();
                element2.AppendChild(element8);
                XmlElement element9 = document.CreateElement("CZLX");
                element9.InnerText = "Z1";
                element2.AppendChild(element9);
                XmlElement element10 = document.CreateElement("FPJSFP");
                element2.AppendChild(element10);
                XmlElement element11 = document.CreateElement("FKPJH");
                element11.InnerText = volumeInfo.get_Machine().ToString();
                element10.AppendChild(element11);
                XmlElement element12 = document.CreateElement("FPZL");
                element12.InnerText = volumeInfo.get_Kind().ToString();
                element10.AppendChild(element12);
                XmlElement element13 = document.CreateElement("LBDM");
                element13.InnerText = volumeInfo.get_TypeCode();
                element10.AppendChild(element13);
                XmlElement element14 = document.CreateElement("QSHM");
                element14.InnerText = volumeInfo.get_Number().PadLeft(8, '0');
                element10.AppendChild(element14);
                XmlElement element15 = document.CreateElement("FPFS");
                element15.InnerText = volumeInfo.get_Count().ToString();
                element10.AppendChild(element15);
                XmlElement element16 = document.CreateElement("FLAG");
                element16.InnerText = Convert.ToBase64String(volumeInfo.Buffer);
                element10.AppendChild(element16);
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

        public static XmlDocument CreateAllocateQueryInput(List<string> queryParams)
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
                XmlElement element4 = document.CreateElement("CZLX");
                element4.InnerText = "Z2";
                element2.AppendChild(element4);
                XmlElement element5 = document.CreateElement("FKPJH");
                element5.InnerText = queryParams[0];
                element2.AppendChild(element5);
                XmlElement element6 = document.CreateElement("FPZT");
                element6.InnerText = queryParams[2];
                element2.AppendChild(element6);
                XmlElement element7 = document.CreateElement("FPZL");
                element7.InnerText = queryParams[1];
                element2.AppendChild(element7);
                XmlElement element8 = document.CreateElement("FPSJ_Q");
                element8.InnerText = queryParams[3];
                element2.AppendChild(element8);
                XmlElement element9 = document.CreateElement("FPSJ_Z");
                element9.InnerText = queryParams[4];
                element2.AppendChild(element9);
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

        public static bool ParseAllocateOutput(XmlDocument doc, out bool isDeviceError, out string msg)
        {
            try
            {
                new InvVolumeApp();
                XmlNode node = doc.SelectSingleNode("//CODE");
                XmlNode node2 = doc.SelectSingleNode("//MESS");
                if (!node.InnerText.Equals("0000"))
                {
                    isDeviceError = false;
                    msg = node2.InnerText;
                    return false;
                }
                byte[] buffer = Convert.FromBase64String(doc.SelectSingleNode("//FPJJSMW").InnerText);
                TaxCardInstance.NInvWriteConfirmFromMain(buffer, buffer.Length);
                if (TaxCardInstance.get_RetCode() != 0)
                {
                    isDeviceError = true;
                    msg = TaxCardInstance.get_ErrCode();
                    return false;
                }
                isDeviceError = false;
                msg = string.Empty;
                return true;
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                isDeviceError = false;
                msg = exception.Message;
                return false;
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                isDeviceError = false;
                msg = exception2.Message;
                return false;
            }
        }

        public static bool ParseAllocateQueryOutput(XmlDocument doc, out string msg, out List<AllocateInfo> outAllocList)
        {
            try
            {
                List<AllocateInfo> list = new List<AllocateInfo>();
                XmlNode node = doc.SelectSingleNode("//CODE");
                XmlNode node2 = doc.SelectSingleNode("//MESS");
                if (!node.InnerText.Equals("0000"))
                {
                    msg = node2.InnerText;
                    outAllocList = null;
                    return false;
                }
                XmlNodeList list2 = doc.SelectSingleNode("//DATA").SelectNodes("//FPMX");
                if ((list2 != null) && (list2.Count > 0))
                {
                    foreach (XmlNode node4 in list2)
                    {
                        AllocateInfo item = new AllocateInfo();
                        foreach (XmlNode node5 in node4.ChildNodes)
                        {
                            if (node5.Name.Equals("FKPJH"))
                            {
                                item.machineNum = node5.InnerText;
                            }
                            else if (node5.Name.Equals("FPZL"))
                            {
                                item.typeName = ShareMethods.GetInvType(Convert.ToByte(node5.InnerText));
                            }
                            else if (node5.Name.Equals("LBDM"))
                            {
                                item.typeCode = node5.InnerText;
                            }
                            else if (node5.Name.Equals("QSHM"))
                            {
                                item.startNum = node5.InnerText.PadLeft(8, '0');
                            }
                            else if (node5.Name.Equals("FPFS"))
                            {
                                item.count = node5.InnerText;
                            }
                            else if (node5.Name.Equals("FPZT"))
                            {
                                item.allocStatus = node5.InnerText;
                            }
                            else if (node5.Name.Equals("GMRQ"))
                            {
                                string innerText = node5.InnerText;
                                item.buyDate = innerText.Substring(0, 4) + "-" + innerText.Substring(4, 2) + "-" + innerText.Substring(6, 2);
                            }
                        }
                        int num = (Convert.ToInt32(item.startNum) + Convert.ToInt32(item.count)) - 1;
                        item.endNum = num.ToString().PadLeft(8, '0');
                        list.Add(item);
                    }
                }
                msg = string.Empty;
                outAllocList = list;
                return true;
            }
            catch (BaseException exception)
            {
                loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
                msg = exception.Message;
                outAllocList = null;
                return false;
            }
            catch (Exception exception2)
            {
                loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
                msg = exception2.Message;
                outAllocList = null;
                return false;
            }
        }

        public static string RequestListInput(string startDate, string endDate)
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
            XmlElement element4 = document.CreateElement("KPJH");
            element4.InnerText = TaxCardInstance.get_Machine().ToString();
            element2.AppendChild(element4);
            XmlElement element5 = document.CreateElement("SBBH");
            element5.InnerText = TaxCardInstance.GetInvControlNum();
            element2.AppendChild(element5);
            XmlElement element6 = document.CreateElement("DCBB");
            element6.InnerText = TaxCardInstance.get_StateInfo().DriverVersion;
            element2.AppendChild(element6);
            XmlElement element7 = document.CreateElement("CZLX");
            element7.InnerText = "F1";
            element2.AppendChild(element7);
            XmlElement element8 = document.CreateElement("FSSJ_Q");
            element8.InnerText = startDate;
            element2.AppendChild(element8);
            XmlElement element9 = document.CreateElement("FSSJ_Z");
            element9.InnerText = endDate;
            element2.AppendChild(element9);
            document.PreserveWhitespace = true;
            if (logFlag)
            {
                document.Save(logPath + @"\AllocateRequestListInput.xml");
            }
            return document.InnerXml;
        }
    }
}

