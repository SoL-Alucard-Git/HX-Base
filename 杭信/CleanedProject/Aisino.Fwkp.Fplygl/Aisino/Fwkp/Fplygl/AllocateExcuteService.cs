namespace Aisino.Fwkp.Fplygl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.Common;
    using Aisino.Fwkp.Fplygl.Form.WSFTP_6.Common;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public sealed class AllocateExcuteService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            List<InvVolumeApp> list = new List<InvVolumeApp>();
            List<int> list2 = new List<int>();
            bool flag = false;
            bool flag2 = true;
            bool isDeviceError = false;
            string msg = string.Empty;
            InvoiceType typeCode = (InvoiceType) ShareMethods.GetTypeCode(param[1].ToString());
            if (((0x33 == typeCode) || (11 == typeCode)) || (0x29 == typeCode))
            {
                flag = true;
                msg = "待分配发票类型有误";
            }
            if (!flag)
            {
                UnlockInvoice unlockVolume = card.NInvSearchUnlockFromMain();
                if (card.get_RetCode() != 0)
                {
                    flag2 = false;
                    isDeviceError = true;
                    msg = card.get_ErrCode();
                }
                else
                {
                    bool flag4 = DownloadCommon.CheckEmpty(unlockVolume.Buffer);
                    string str2 = "";
                    if (!flag4)
                    {
                        str2 = unlockVolume.get_Number().PadLeft(8, '0');
                        InvVolumeApp item = new InvVolumeApp();
                        flag2 = AllocateCommon.AllocateOneVolume(unlockVolume, out isDeviceError, out msg);
                        if (flag2)
                        {
                            item.InvType = Convert.ToByte(unlockVolume.get_Kind());
                            item.TypeCode = unlockVolume.get_TypeCode();
                            item.HeadCode = Convert.ToUInt32(unlockVolume.get_Number());
                            item.Number = Convert.ToUInt16(unlockVolume.get_Count());
                            list.Add(item);
                            list2.Add(unlockVolume.get_Machine());
                        }
                    }
                    bool flag5 = false;
                    if (Convert.ToInt32(param[3].ToString()).ToString("00000000") == str2)
                    {
                        flag5 = true;
                    }
                    if (flag2 && !flag5)
                    {
                        InvVolumeApp app2 = new InvVolumeApp();
                        string str3 = param[2].ToString();
                        long num = Convert.ToInt64(param[3].ToString());
                        long num2 = Convert.ToInt64(param[4].ToString());
                        InvoiceType type2 = (InvoiceType) ShareMethods.GetTypeCode(param[1].ToString());
                        int num3 = Convert.ToInt32(param[0].ToString());
                        UnlockInvoice invoice2 = card.NInvAllotToSubMachine(str3, num, num2, type2, num3);
                        if (card.get_RetCode() != 0)
                        {
                            flag2 = false;
                            isDeviceError = true;
                            msg = card.get_ErrCode();
                        }
                        else
                        {
                            flag2 = AllocateCommon.AllocateOneVolume(invoice2, out isDeviceError, out msg);
                            if (flag2)
                            {
                                app2.InvType = Convert.ToByte(invoice2.get_Kind());
                                app2.TypeCode = invoice2.get_TypeCode();
                                app2.HeadCode = Convert.ToUInt32(invoice2.get_Number());
                                app2.Number = Convert.ToUInt16(invoice2.get_Count());
                                list.Add(app2);
                                list2.Add(invoice2.get_Machine());
                            }
                        }
                    }
                }
            }
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
            document.PreserveWhitespace = false;
            document.AppendChild(newChild);
            XmlElement element = document.CreateElement("FPXT");
            document.AppendChild(element);
            XmlElement element2 = document.CreateElement("OUTPUT");
            element.AppendChild(element2);
            XmlElement element3 = document.CreateElement("CODE");
            XmlElement element4 = document.CreateElement("MESS");
            if (flag)
            {
                element3.InnerText = "0002";
                element4.InnerText = msg;
            }
            else if (flag2)
            {
                element3.InnerText = "0000";
                element4.InnerText = string.Empty;
            }
            else if (isDeviceError)
            {
                element3.InnerText = msg;
                element4.InnerText = MessageManager.GetMessageInfo(msg);
            }
            else
            {
                element3.InnerText = "0001";
                element4.InnerText = msg;
            }
            element2.AppendChild(element3);
            element2.AppendChild(element4);
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    XmlElement element5 = document.CreateElement("FPJFP");
                    element2.AppendChild(element5);
                    XmlElement element6 = document.CreateElement("FKPJH");
                    element6.InnerText = list2[i].ToString();
                    element5.AppendChild(element6);
                    XmlElement element7 = document.CreateElement("FPZL");
                    element7.InnerText = list[i].InvType.ToString();
                    element5.AppendChild(element7);
                    XmlElement element8 = document.CreateElement("LBDM");
                    element8.InnerText = list[i].TypeCode;
                    element5.AppendChild(element8);
                    XmlElement element9 = document.CreateElement("QSHM");
                    element9.InnerText = list[i].HeadCode.ToString();
                    element5.AppendChild(element9);
                    XmlElement element10 = document.CreateElement("FPFS");
                    element10.InnerText = list[i].Number.ToString();
                    element5.AppendChild(element10);
                }
            }
            document.PreserveWhitespace = true;
            return new object[] { document.InnerXml };
        }
    }
}

