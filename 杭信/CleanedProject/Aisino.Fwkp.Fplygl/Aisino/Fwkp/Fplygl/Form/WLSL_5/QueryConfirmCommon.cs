namespace Aisino.Fwkp.Fplygl.Form.WLSL_5
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.GeneralStructure;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Xml;

    public class QueryConfirmCommon
    {
        private static string cofPath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Bin\");
        private static ILog loger = LogUtil.GetLogger<QueryConfirmCommon>();
        private static bool logFlag = false;
        private static string logPath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Log\");
        private static TaxCard TaxCardInstance = TaxCardFactory.CreateTaxCard();

        private static XmlDocument CreateHXQueryStateInput(QueryCondition qConditon)
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
                XmlElement element7 = document.CreateElement("CXTJ");
                element2.AppendChild(element7);
                XmlElement element8 = document.CreateElement("FPZL");
                element8.InnerText = qConditon.invType;
                element7.AppendChild(element8);
                XmlElement element9 = document.CreateElement("QSRQ");
                element9.InnerText = qConditon.startTime;
                element7.AppendChild(element9);
                XmlElement element10 = document.CreateElement("JZRQ");
                element10.InnerText = qConditon.endTime;
                element7.AppendChild(element10);
                XmlElement element11 = document.CreateElement("CLZT");
                element11.InnerText = qConditon.status;
                element7.AppendChild(element11);
                XmlElement element12 = document.CreateElement("SLXH");
                element7.AppendChild(element12);
                document.PreserveWhitespace = true;
                return document;
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
                return null;
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
                return null;
            }
        }

        private static XmlDocument CreateZCQueryStateInput(QueryCondition qConditon)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.PreserveWhitespace = false;
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("business");
                element.SetAttribute("id", "fp_sljg");
                element.SetAttribute("comment", "发票申领结果");
                document.AppendChild(element);
                XmlElement element2 = document.CreateElement("body");
                element.AppendChild(element2);
                XmlElement element3 = document.CreateElement("nsrsbh");
                element3.InnerText = TaxCardInstance.get_TaxCode();
                element2.AppendChild(element3);
                XmlElement element4 = document.CreateElement("fplx_dm");
                element4.InnerText = qConditon.invType;
                element2.AppendChild(element4);
                XmlElement element5 = document.CreateElement("slqssj");
                element5.InnerText = qConditon.startTime;
                element2.AppendChild(element5);
                XmlElement element6 = document.CreateElement("sljzsj");
                element6.InnerText = qConditon.endTime;
                element2.AppendChild(element6);
                XmlElement element7 = document.CreateElement("clzt");
                element7.InnerText = qConditon.status;
                element2.AppendChild(element7);
                XmlElement element8 = document.CreateElement("slxh");
                element2.AppendChild(element8);
                document.PreserveWhitespace = true;
                return document;
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
                return null;
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
                return null;
            }
        }

        private static Dictionary<string, string> GetZCTypeCodeNamePairs()
        {
            try
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                if (File.Exists(cofPath + @"\ZcAvailableListOutput.xml"))
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(cofPath + @"\ZcAvailableListOutput.xml");
                    XmlNode node = document.SelectSingleNode("//returnCode");
                    document.SelectSingleNode("//returnMessage");
                    if (!node.InnerText.Equals("00"))
                    {
                        return dictionary;
                    }
                    foreach (XmlNode node2 in document.SelectNodes("//group"))
                    {
                        XmlNodeList childNodes = node2.ChildNodes;
                        string innerText = string.Empty;
                        string key = string.Empty;
                        foreach (XmlNode node3 in childNodes)
                        {
                            if (node3.Name.Equals("fpzl_mc"))
                            {
                                innerText = node3.InnerText;
                            }
                            else if (node3.Name.Equals("fpzl_dm"))
                            {
                                key = node3.InnerText;
                            }
                        }
                        if ((key != string.Empty) && (innerText != string.Empty))
                        {
                            dictionary.Add(key, innerText);
                        }
                    }
                }
                return dictionary;
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
                return null;
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
                return null;
            }
        }

        private static void HxQueryOperate(QueryCondition qCondition, List<OneTypeVolumes> applyQueryList, bool isConfirmQuery)
        {
            try
            {
                XmlDocument document = CreateHXQueryStateInput(qCondition);
                if (logFlag)
                {
                    document.Save(logPath + "HxQueryStatusInput.xml");
                }
                string xml = string.Empty;
                if (HttpsSender.SendMsg("0022", document.InnerXml, ref xml) != 0)
                {
                    MessageManager.ShowMsgBox(xml);
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    if (logFlag)
                    {
                        doc.Save(logPath + @"\HxQueryStatusOutput.xml");
                    }
                    string msg = string.Empty;
                    if (!ParseHXQueryStateOutput(doc, out msg, applyQueryList, isConfirmQuery))
                    {
                        MessageManager.ShowMsgBox(msg);
                    }
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
        }

        private static bool ParseHXQueryStateOutput(XmlDocument doc, out string msg, List<OneTypeVolumes> applyQueryList, bool isConfirmQuery)
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
                foreach (XmlNode node4 in doc.SelectSingleNode("//DATA").SelectNodes("//SLXX"))
                {
                    if (!string.IsNullOrEmpty(node4.InnerText))
                    {
                        OneTypeVolumes item = new OneTypeVolumes();
                        foreach (XmlNode node5 in node4.ChildNodes)
                        {
                            if (node5.Name.Equals("FPZL"))
                            {
                                item.invType = ApplyCommon.Code2InvtypeMix(node5.InnerText);
                            }
                            else if (node5.Name.Equals("FPZLDM"))
                            {
                                item.typeCode = node5.InnerText;
                            }
                            else if (node5.Name.Equals("FPZLMC"))
                            {
                                item.typeName = node5.InnerText;
                            }
                            else if (node5.Name.Equals("SLSL"))
                            {
                                item.applyAmount = node5.InnerText;
                            }
                            else if (node5.Name.Equals("SQSJ"))
                            {
                                string innerText = node5.InnerText;
                                if (14 == innerText.Length)
                                {
                                    int year = Convert.ToInt32(innerText.Substring(0, 4));
                                    int month = Convert.ToInt32(innerText.Substring(4, 2));
                                    int day = Convert.ToInt32(innerText.Substring(6, 2));
                                    int hour = Convert.ToInt32(innerText.Substring(8, 2));
                                    int minute = Convert.ToInt32(innerText.Substring(10, 2));
                                    int second = Convert.ToInt32(innerText.Substring(12, 2));
                                    item.applyTime = new DateTime(year, month, day, hour, minute, second).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else
                                {
                                    item.applyTime = innerText;
                                }
                            }
                            else if (node5.Name.Equals("SLXH"))
                            {
                                item.applyNum = node5.InnerText;
                            }
                            else if (node5.Name.Equals("SLZT"))
                            {
                                item.applyStatus = node5.InnerText;
                            }
                            else if (node5.Name.Equals("CLXX"))
                            {
                                item.applyStatusMsg = node5.InnerText;
                            }
                            else if (node5.Name.Equals("CLSJ"))
                            {
                                string str2 = node5.InnerText;
                                if (14 == str2.Length)
                                {
                                    int num7 = Convert.ToInt32(str2.Substring(0, 4));
                                    int num8 = Convert.ToInt32(str2.Substring(4, 2));
                                    int num9 = Convert.ToInt32(str2.Substring(6, 2));
                                    int num10 = Convert.ToInt32(str2.Substring(8, 2));
                                    int num11 = Convert.ToInt32(str2.Substring(10, 2));
                                    int num12 = Convert.ToInt32(str2.Substring(12, 2));
                                    item.dealTime = new DateTime(num7, num8, num9, num10, num11, num12).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else
                                {
                                    item.dealTime = str2;
                                }
                            }
                            else if (node5.Name.Equals("YSQBH"))
                            {
                                item.dealNum = node5.InnerText;
                            }
                            else if (node5.Name.Equals("SLQRBZ"))
                            {
                                if (node5.InnerText.Equals("Y"))
                                {
                                    item.isConfirmed = true;
                                }
                                else
                                {
                                    item.isConfirmed = false;
                                }
                            }
                            else if (node5.Name.Equals("FPJXX"))
                            {
                                Volumn volumn = new Volumn();
                                foreach (XmlNode node6 in node5.ChildNodes)
                                {
                                    if (node6.Name.Equals("FPDM"))
                                    {
                                        volumn.typeCode = node6.InnerText;
                                    }
                                    else if (node6.Name.Equals("QSHM"))
                                    {
                                        volumn.startNum = node6.InnerText;
                                    }
                                    else if (node6.Name.Equals("ZZHM"))
                                    {
                                        volumn.endNum = node6.InnerText;
                                    }
                                    else if (node6.Name.Equals("FS"))
                                    {
                                        volumn.count = node6.InnerText;
                                    }
                                }
                                item.volumns.Add(volumn);
                            }
                        }
                        if (!isConfirmQuery || !item.isConfirmed)
                        {
                            applyQueryList.Add(item);
                        }
                    }
                }
                msg = string.Empty;
                return true;
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
                msg = exception.Message;
                return false;
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
                msg = exception2.Message;
                return false;
            }
        }

        private static bool ParseZCQueryStateOutput(XmlDocument doc, out string msg, List<OneTypeVolumes> applyQueryList, bool isConfirmQuery)
        {
            try
            {
                XmlNode node = doc.SelectSingleNode("//returnCode");
                XmlNode node2 = doc.SelectSingleNode("//returnMessage");
                if (!node.InnerText.Equals("00"))
                {
                    msg = node2.InnerText;
                    return false;
                }
                Dictionary<string, string> zCTypeCodeNamePairs = GetZCTypeCodeNamePairs();
                XmlNode node3 = doc.SelectSingleNode("//slxx");
                if (node3 != null)
                {
                    foreach (XmlNode node4 in node3.ChildNodes)
                    {
                        OneTypeVolumes item = new OneTypeVolumes();
                        foreach (XmlNode node5 in node4.ChildNodes)
                        {
                            if (node5.Name.Equals("fplx_dm"))
                            {
                                item.invType = ApplyCommon.Code2InvtypeMix(node5.InnerText);
                            }
                            else if (node5.Name.Equals("fpzl_dm"))
                            {
                                item.typeCode = node5.InnerText;
                                if (zCTypeCodeNamePairs.Keys.Contains<string>(node5.InnerText))
                                {
                                    item.typeName = zCTypeCodeNamePairs[node5.InnerText];
                                }
                            }
                            else if (node5.Name.Equals("slsl"))
                            {
                                item.applyAmount = node5.InnerText;
                            }
                            else if (node5.Name.Equals("slsj"))
                            {
                                string innerText = node5.InnerText;
                                if (14 == innerText.Length)
                                {
                                    int year = Convert.ToInt32(innerText.Substring(0, 4));
                                    int month = Convert.ToInt32(innerText.Substring(4, 2));
                                    int day = Convert.ToInt32(innerText.Substring(6, 2));
                                    int hour = Convert.ToInt32(innerText.Substring(8, 2));
                                    int minute = Convert.ToInt32(innerText.Substring(10, 2));
                                    int second = Convert.ToInt32(innerText.Substring(12, 2));
                                    item.applyTime = new DateTime(year, month, day, hour, minute, second).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else
                                {
                                    item.applyTime = innerText;
                                }
                            }
                            else if (node5.Name.Equals("slxh"))
                            {
                                item.applyNum = node5.InnerText;
                            }
                            else if (node5.Name.Equals("clzt"))
                            {
                                item.applyStatus = node5.InnerText;
                            }
                            else if (node5.Name.Equals("clxx"))
                            {
                                item.applyStatusMsg = node5.InnerText;
                            }
                            else if (node5.Name.Equals("clsj"))
                            {
                                string str2 = node5.InnerText;
                                if (14 == str2.Length)
                                {
                                    int num7 = Convert.ToInt32(str2.Substring(0, 4));
                                    int num8 = Convert.ToInt32(str2.Substring(4, 2));
                                    int num9 = Convert.ToInt32(str2.Substring(6, 2));
                                    int num10 = Convert.ToInt32(str2.Substring(8, 2));
                                    int num11 = Convert.ToInt32(str2.Substring(10, 2));
                                    int num12 = Convert.ToInt32(str2.Substring(12, 2));
                                    item.dealTime = new DateTime(num7, num8, num9, num10, num11, num12).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else
                                {
                                    item.dealTime = str2;
                                }
                            }
                            else if (node5.Name.Equals("ysqbh"))
                            {
                                item.dealNum = node5.InnerText;
                            }
                            else if (node5.Name.Equals("qrjg"))
                            {
                                if (node5.InnerText.Equals("1"))
                                {
                                    item.isConfirmed = true;
                                }
                                else
                                {
                                    item.isConfirmed = false;
                                }
                            }
                            else if (node5.Name.Equals("sljgmx"))
                            {
                                foreach (XmlNode node6 in node5.ChildNodes)
                                {
                                    Volumn volumn = new Volumn();
                                    foreach (XmlNode node7 in node6.ChildNodes)
                                    {
                                        if (node7.Name.Equals("fpdm"))
                                        {
                                            volumn.typeCode = node7.InnerText;
                                        }
                                        else if (node7.Name.Equals("qshm"))
                                        {
                                            volumn.startNum = node7.InnerText;
                                        }
                                        else if (node7.Name.Equals("zzhm"))
                                        {
                                            volumn.endNum = node7.InnerText;
                                        }
                                        else if (node7.Name.Equals("fs"))
                                        {
                                            volumn.count = node7.InnerText;
                                        }
                                    }
                                    item.volumns.Add(volumn);
                                }
                            }
                        }
                        if (!isConfirmQuery || !item.isConfirmed)
                        {
                            applyQueryList.Add(item);
                        }
                    }
                }
                msg = string.Empty;
                return true;
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
                msg = exception.Message;
                return false;
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
                msg = exception2.Message;
                return false;
            }
        }

        public static void QueryController(QueryCondition qCondition, List<OneTypeVolumes> queryList, bool isConfirmQuery)
        {
            string invType = qCondition.invType;
            if (ApplyCommon.IsHxInvType(invType))
            {
                HxQueryOperate(qCondition, queryList, isConfirmQuery);
            }
            else if (ApplyCommon.IsZcInvType(invType))
            {
                ZcQueryOperate(qCondition, queryList, isConfirmQuery);
            }
            else if (ApplyCommon.IsAllType(invType))
            {
                if (ApplyCommon.HasHxAuthorization() && ApplyCommon.HasZcAuthorization())
                {
                    HxQueryOperate(qCondition, queryList, isConfirmQuery);
                    ZcQueryOperate(qCondition, queryList, isConfirmQuery);
                }
                else if (ApplyCommon.HasHxAuthorization())
                {
                    HxQueryOperate(qCondition, queryList, isConfirmQuery);
                }
                else if (ApplyCommon.HasZcAuthorization())
                {
                    ZcQueryOperate(qCondition, queryList, isConfirmQuery);
                }
            }
        }

        private static void ZcQueryOperate(QueryCondition qCondition, List<OneTypeVolumes> applyQueryList, bool isConfirmQuery)
        {
            try
            {
                XmlDocument document = CreateZCQueryStateInput(qCondition);
                if (logFlag)
                {
                    document.Save(logPath + "ZcQueryStatusInput.xml");
                }
                string xml = string.Empty;
                if (HttpsSender.SendMsg("0025", document.InnerXml, ref xml) != 0)
                {
                    MessageManager.ShowMsgBox(xml);
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    if (logFlag)
                    {
                        doc.Save(logPath + @"\ZcQueryStatusOutput.xml");
                    }
                    string msg = string.Empty;
                    if (!ParseZCQueryStateOutput(doc, out msg, applyQueryList, isConfirmQuery))
                    {
                        MessageManager.ShowMsgBox(msg);
                    }
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                ExceptionHandler.HandleError(exception2);
            }
        }
    }
}

