namespace Aisino.Fwkp.Fplygl
{
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.Common;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Xml;

    public sealed class DownloadExcuteService : AbstractService
    {
        private bool buffFull;
        private string errCode = string.Empty;
        private string errMessage = string.Empty;
        private bool errorOccur;
        private bool hjErrTriger;
        private bool logFlag;
        private string logPath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Log\");
        private List<InvVolumeApp> successList = new List<InvVolumeApp>();
        private TaxCard TaxCardInstance = TaxCardFactory.CreateTaxCard();
        private bool zpErrTriger;

        private bool CheckBufferEmpty()
        {
            byte[] buffer2 = new byte[2];
            buffer2[1] = 11;
            byte[] buffer = buffer2;
            bool[] flagArray = new bool[] { true, true };
            for (int i = 0; i < 2; i++)
            {
                UnlockInvoice invoice = this.TaxCardInstance.NInvGetUnlockInvoice(buffer[i]);
                if (this.TaxCardInstance.get_RetCode() != 0)
                {
                    flagArray[i] = true;
                }
                else if (!DownloadCommon.CheckEmpty(invoice.Buffer))
                {
                    flagArray[i] = false;
                    return false;
                }
            }
            return true;
        }

        private bool CheckOneSystem(int type1, int type2)
        {
            if (ShareMethods.IsHXInv(type1))
            {
                return ShareMethods.IsHXInv(type2);
            }
            if (!ShareMethods.IsZCInv(type1))
            {
                return false;
            }
            return ShareMethods.IsZCInv(type2);
        }

        private void ClearBuffer(byte fplx)
        {
            byte[] buffer = null;
            if ((fplx == 0) || (2 == fplx))
            {
                buffer = new byte[1];
            }
            else if (((11 == fplx) || (12 == fplx)) || (0x33 == fplx))
            {
                buffer = new byte[] { 11 };
            }
            foreach (byte num in buffer)
            {
                UnlockInvoice invoice = this.TaxCardInstance.NInvGetUnlockInvoice(num);
                if (this.TaxCardInstance.get_RetCode() != 0)
                {
                    MessageManager.ShowMsgBox(this.TaxCardInstance.get_ErrCode());
                    break;
                }
                if (!DownloadCommon.CheckEmpty(invoice.Buffer))
                {
                    InvVolumeApp confirmVolumn = new InvVolumeApp {
                        InvType = Convert.ToByte(invoice.get_Kind()),
                        TypeCode = invoice.get_TypeCode(),
                        HeadCode = Convert.ToUInt32(invoice.get_Number()),
                        Number = Convert.ToUInt16(invoice.get_Count())
                    };
                    bool withOperation = true;
                    string str = string.Empty;
                    string xml = string.Empty;
                    if (((confirmVolumn.InvType == 0) || (2 == confirmVolumn.InvType)) || (12 == confirmVolumn.InvType))
                    {
                        str = "0040";
                        withOperation = true;
                    }
                    else if (0x33 == confirmVolumn.InvType)
                    {
                        str = "0020";
                        withOperation = false;
                    }
                    if (HttpsSender.SendMsg(str, this.RequestDownloadInput(null, confirmVolumn, Convert.ToBase64String(invoice.Buffer), withOperation), ref xml) != 0)
                    {
                        MessageManager.ShowMsgBox(xml);
                        break;
                    }
                    XmlDocument downInvXml = new XmlDocument();
                    downInvXml.LoadXml(xml);
                    if (this.logFlag)
                    {
                        downInvXml.Save(this.logPath + @"\AllocateRequestDownloadOutput.xml");
                    }
                    this.RequestDownloadOutput(downInvXml);
                }
            }
        }

        protected override object[] doService(object[] param)
        {
            this.successList = new List<InvVolumeApp>();
            this.RunCommond(param);
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
            if (!this.errorOccur)
            {
                element3.InnerText = "0000";
                element4.InnerText = string.Empty;
            }
            else
            {
                element3.InnerText = this.errCode;
                element4.InnerText = this.errMessage;
            }
            element2.AppendChild(element3);
            element2.AppendChild(element4);
            if (this.successList.Count > 0)
            {
                for (int i = 0; i < this.successList.Count; i++)
                {
                    XmlElement element5 = document.CreateElement("FPJXZ");
                    element2.AppendChild(element5);
                    XmlElement element6 = document.CreateElement("FPZL");
                    element6.InnerText = this.successList[i].InvType.ToString();
                    element5.AppendChild(element6);
                    XmlElement element7 = document.CreateElement("LBDM");
                    element7.InnerText = this.successList[i].TypeCode;
                    element5.AppendChild(element7);
                    XmlElement element8 = document.CreateElement("QSHM");
                    element8.InnerText = this.successList[i].HeadCode.ToString();
                    element5.AppendChild(element8);
                    XmlElement element9 = document.CreateElement("FPFS");
                    element9.InnerText = this.successList[i].Number.ToString();
                    element5.AppendChild(element9);
                }
            }
            document.PreserveWhitespace = true;
            return new object[] { document.InnerXml };
        }

        private void ExecuteDownloadList(List<InvVolumeApp> reqList)
        {
            if (this.HasVehicleVolume(reqList))
            {
                UnlockInvoice invoice = this.TaxCardInstance.NInvGetUnlockInvoice(0x33);
                if (this.TaxCardInstance.get_RetCode() != 0)
                {
                    MessageManager.ShowMsgBox(this.TaxCardInstance.get_ErrCode());
                    return;
                }
                if (!DownloadCommon.CheckEmpty(invoice.Buffer) && (0x33 == invoice.get_Kind()))
                {
                    InvVolumeApp confirmVolumn = new InvVolumeApp {
                        InvType = Convert.ToByte(invoice.get_Kind()),
                        TypeCode = invoice.get_TypeCode(),
                        HeadCode = Convert.ToUInt32(invoice.get_Number()),
                        Number = Convert.ToUInt16(invoice.get_Count())
                    };
                    string xml = string.Empty;
                    if (HttpsSender.SendMsg("0020", this.RequestDownloadInput(null, confirmVolumn, Convert.ToBase64String(invoice.Buffer), true), ref xml) != 0)
                    {
                        MessageManager.ShowMsgBox(xml);
                        return;
                    }
                    XmlDocument downInvXml = new XmlDocument();
                    downInvXml.LoadXml(xml);
                    if (this.logFlag)
                    {
                        downInvXml.Save(this.logPath + @"\AllocateRequestDownloadOutput.xml");
                    }
                    this.RequestDownloadOutput(downInvXml);
                }
            }
            int num2 = 0;
            foreach (InvVolumeApp app2 in reqList)
            {
                if (this.buffFull)
                {
                    break;
                }
                num2++;
                if ((!ShareMethods.IsHXInv(app2.InvType) || !this.zpErrTriger) && (!ShareMethods.IsZCInv(app2.InvType) || !this.hjErrTriger))
                {
                    bool flag;
                    UnlockInvoice invoice2 = new UnlockInvoice();
                    InvVolumeApp locked = new InvVolumeApp();
                    InvVolumeApp tarInv = app2;
                    invoice2 = this.TaxCardInstance.NInvGetUnlockInvoice(tarInv.InvType);
                    if (this.TaxCardInstance.get_RetCode() != 0)
                    {
                        MessageManager.ShowMsgBox(this.TaxCardInstance.get_ErrCode());
                        break;
                    }
                    bool flag2 = DownloadCommon.CheckEmpty(invoice2.Buffer);
                    if (flag2)
                    {
                        flag = false;
                    }
                    else
                    {
                        locked.InvType = Convert.ToByte(invoice2.get_Kind());
                        locked.TypeCode = invoice2.get_TypeCode();
                        locked.HeadCode = Convert.ToUInt32(invoice2.get_Number());
                        locked.Number = Convert.ToUInt16(invoice2.get_Count());
                        flag = DownloadCommon.CheckRepeat(locked, tarInv);
                    }
                    if (flag)
                    {
                        tarInv = null;
                    }
                    if (flag2)
                    {
                        locked = null;
                    }
                    string str2 = string.Empty;
                    if (HttpsSender.SendMsg("0040", this.RequestDownloadInput(tarInv, locked, Convert.ToBase64String(invoice2.Buffer), true), ref str2) != 0)
                    {
                        MessageManager.ShowMsgBox(str2);
                        break;
                    }
                    XmlDocument document2 = new XmlDocument();
                    document2.LoadXml(str2);
                    if (this.logFlag)
                    {
                        document2.Save(this.logPath + @"\AllocateRequestDownloadOutput.xml");
                    }
                    this.RequestDownloadOutput(document2);
                    if (this.zpErrTriger || this.hjErrTriger)
                    {
                        break;
                    }
                    if (!flag && ((num2 == reqList.Count) || ((num2 != reqList.Count) && !this.CheckOneSystem(reqList[num2 - 1].InvType, reqList[num2].InvType))))
                    {
                        invoice2 = this.TaxCardInstance.NInvGetUnlockInvoice(tarInv.InvType);
                        Convert.ToByte(invoice2.get_Kind());
                        locked = new InvVolumeApp {
                            InvType = Convert.ToByte(invoice2.get_Kind()),
                            TypeCode = invoice2.get_TypeCode(),
                            HeadCode = Convert.ToUInt32(invoice2.get_Number()),
                            Number = Convert.ToUInt16(invoice2.get_Count())
                        };
                        if (HttpsSender.SendMsg("0040", this.RequestDownloadInput(null, locked, Convert.ToBase64String(invoice2.Buffer), true), ref str2) != 0)
                        {
                            MessageManager.ShowMsgBox(str2);
                            break;
                        }
                        XmlDocument document3 = new XmlDocument();
                        document3.LoadXml(str2);
                        if (this.logFlag)
                        {
                            document3.Save(this.logPath + @"\AllocateRequestDownloadOutputSingle.xml");
                        }
                        this.RequestDownloadOutput(document3);
                        if (this.zpErrTriger && this.hjErrTriger)
                        {
                            break;
                        }
                    }
                }
            }
        }

        private bool HasVehicleVolume(List<InvVolumeApp> reqList)
        {
            foreach (InvVolumeApp app in reqList)
            {
                if (12 == app.InvType)
                {
                    return true;
                }
            }
            return false;
        }

        private string RequestDownloadInput(InvVolumeApp downVolumn, InvVolumeApp confirmVolumn, string confirmCrypt, bool withOperation = true)
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
            element3.InnerText = this.TaxCardInstance.get_TaxCode();
            element2.AppendChild(element3);
            XmlElement element4 = document.CreateElement("HASH");
            element4.InnerText = DownloadCommon.GetDecimalStr(this.TaxCardInstance.Get9BitHashTaxCode());
            element2.AppendChild(element4);
            XmlElement element5 = document.CreateElement("YSSH");
            element5.InnerText = this.TaxCardInstance.get_CompressCode();
            element2.AppendChild(element5);
            XmlElement element6 = document.CreateElement("DQBH");
            element6.InnerText = this.TaxCardInstance.get_RegionCode();
            element2.AppendChild(element6);
            XmlElement element7 = document.CreateElement("KPJH");
            element7.InnerText = this.TaxCardInstance.get_Machine().ToString();
            element2.AppendChild(element7);
            XmlElement element8 = document.CreateElement("SBBH");
            element8.InnerText = this.TaxCardInstance.GetInvControlNum();
            element2.AppendChild(element8);
            XmlElement element9 = document.CreateElement("DCBB");
            element9.InnerText = this.TaxCardInstance.get_StateInfo().DriverVersion;
            element2.AppendChild(element9);
            if (withOperation)
            {
                XmlElement element10 = document.CreateElement("CZLX");
                element10.InnerText = "F2";
                element2.AppendChild(element10);
            }
            if (confirmVolumn != null)
            {
                XmlElement element11 = document.CreateElement("FPJS");
                element2.AppendChild(element11);
                XmlElement element12 = document.CreateElement("FPZL");
                element12.InnerText = confirmVolumn.InvType.ToString();
                element11.AppendChild(element12);
                XmlElement element13 = document.CreateElement("LBDM");
                element13.InnerText = confirmVolumn.TypeCode;
                element11.AppendChild(element13);
                XmlElement element14 = document.CreateElement("QSHM");
                element14.InnerText = confirmVolumn.HeadCode.ToString();
                element11.AppendChild(element14);
                XmlElement element15 = document.CreateElement("FPFS");
                element15.InnerText = confirmVolumn.Number.ToString();
                element11.AppendChild(element15);
                XmlElement element16 = document.CreateElement("FLAG");
                element16.InnerText = confirmCrypt;
                element11.AppendChild(element16);
            }
            if (downVolumn != null)
            {
                XmlElement element17 = document.CreateElement("FPXZ");
                element2.AppendChild(element17);
                XmlElement element18 = document.CreateElement("FPZL");
                element18.InnerText = downVolumn.InvType.ToString();
                element17.AppendChild(element18);
                XmlElement element19 = document.CreateElement("LBDM");
                if (0x33 == downVolumn.InvType)
                {
                    element19.InnerText = string.Empty;
                }
                else
                {
                    element19.InnerText = downVolumn.TypeCode;
                }
                element17.AppendChild(element19);
                XmlElement element20 = document.CreateElement("QSHM");
                if (0x33 == downVolumn.InvType)
                {
                    element20.InnerText = string.Empty;
                }
                else
                {
                    element20.InnerText = downVolumn.HeadCode.ToString();
                }
                element17.AppendChild(element20);
                XmlElement element21 = document.CreateElement("FPFS");
                element21.InnerText = downVolumn.Number.ToString();
                element17.AppendChild(element21);
                byte[] inArray = this.TaxCardInstance.NInvGetAuthorization();
                XmlElement element22 = document.CreateElement("GPSQ");
                element22.InnerText = Convert.ToBase64String(inArray);
                element17.AppendChild(element22);
            }
            document.PreserveWhitespace = true;
            if (this.logFlag)
            {
                document.Save(this.logPath + @"\RequestDownloadInput.xml");
            }
            return document.InnerXml;
        }

        private void RequestDownloadOutput(XmlDocument downInvXml)
        {
            XmlNode node = downInvXml.SelectSingleNode("//CODE");
            XmlNode node2 = downInvXml.SelectSingleNode("//MESS");
            if (node.InnerText.Equals("0000"))
            {
                new List<InvVolumeApp>();
                XmlNodeList childNodes = downInvXml.SelectSingleNode("//DATA").ChildNodes;
                bool flag = false;
                bool flag2 = false;
                string innerText = string.Empty;
                string str2 = string.Empty;
                string s = string.Empty;
                string str4 = string.Empty;
                foreach (XmlNode node4 in childNodes)
                {
                    XmlElement element = (XmlElement) node4;
                    if (element.Name.Equals("FPJS"))
                    {
                        flag = true;
                        foreach (XmlNode node5 in element.ChildNodes)
                        {
                            if (node5.Name.Equals("FPZL"))
                            {
                                innerText = node5.InnerText;
                            }
                            else if (node5.Name.Equals("FPJJSMW"))
                            {
                                s = node5.InnerText;
                            }
                        }
                    }
                    else if (element.Name.Equals("FPXZ"))
                    {
                        flag2 = true;
                        foreach (XmlNode node6 in element.ChildNodes)
                        {
                            if (node6.Name.Equals("FPZL"))
                            {
                                str2 = node6.InnerText;
                            }
                            else if (node6.Name.Equals("FPJMW"))
                            {
                                str4 = node6.InnerText;
                            }
                        }
                    }
                }
                if (flag)
                {
                    InvVolumeApp item = new InvVolumeApp();
                    int type = Convert.ToInt32(innerText);
                    UnlockInvoice invoice = this.TaxCardInstance.NInvGetUnlockInvoice(type);
                    if (this.TaxCardInstance.get_RetCode() != 0)
                    {
                        this.errorOccur = true;
                        this.errCode = this.TaxCardInstance.get_ErrCode();
                        this.errMessage = MessageManager.GetMessageInfo(this.TaxCardInstance.get_ErrCode());
                        return;
                    }
                    item.InvType = Convert.ToByte(invoice.get_Kind());
                    item.TypeCode = invoice.get_TypeCode();
                    item.HeadCode = Convert.ToUInt32(invoice.get_Number());
                    item.Number = Convert.ToUInt16(invoice.get_Count());
                    byte[] buffer = Convert.FromBase64String(s);
                    this.TaxCardInstance.NInvWirteConfirmResult(type, buffer, buffer.Length);
                    if (this.TaxCardInstance.get_RetCode() != 0)
                    {
                        this.errorOccur = true;
                        this.errCode = this.TaxCardInstance.get_ErrCode();
                        this.errMessage = MessageManager.GetMessageInfo(this.TaxCardInstance.get_ErrCode());
                        if (ShareMethods.IsHXInv(type))
                        {
                            this.zpErrTriger = true;
                            return;
                        }
                        if (ShareMethods.IsZCInv(type))
                        {
                            this.hjErrTriger = true;
                        }
                        return;
                    }
                    this.successList.Add(item);
                }
                if (flag2)
                {
                    int num2 = Convert.ToInt32(str2);
                    byte[] buffer2 = Convert.FromBase64String(str4);
                    this.TaxCardInstance.InvRead("3", buffer2, buffer2.Length, Convert.ToInt32(num2));
                    if (this.TaxCardInstance.get_RetCode() != 0)
                    {
                        this.errorOccur = true;
                        this.errCode = this.TaxCardInstance.get_ErrCode();
                        this.errMessage = MessageManager.GetMessageInfo(this.TaxCardInstance.get_ErrCode());
                        if (0x251d == this.TaxCardInstance.get_RetCode())
                        {
                            this.buffFull = true;
                        }
                        if (ShareMethods.IsHXInv(num2))
                        {
                            this.zpErrTriger = true;
                        }
                        else if (ShareMethods.IsZCInv(num2))
                        {
                            this.hjErrTriger = true;
                        }
                    }
                }
            }
            else
            {
                this.errorOccur = true;
                this.errCode = node.InnerText;
                this.errMessage = node2.InnerText;
                this.zpErrTriger = true;
                this.hjErrTriger = true;
            }
        }

        public void RunCommond(object[] param)
        {
            string driverVersion = this.TaxCardInstance.get_StateInfo().DriverVersion;
            string text1 = driverVersion.Substring(7, 2) + driverVersion.Substring(10, 6);
            bool flag = false;
            this.TaxCardInstance.get_SoftVersion().Equals("FWKP_V2.0_Svr_Client");
            if ((this.TaxCardInstance.get_QYLX().ISPTFP || this.TaxCardInstance.get_QYLX().ISZYFP) && (1 == this.TaxCardInstance.get_StateInfo().IsLockReached))
            {
                flag = true;
            }
            if ((this.TaxCardInstance.get_QYLX().ISHY || this.TaxCardInstance.get_QYLX().ISJDC) || (this.TaxCardInstance.get_QYLX().ISPTFPDZ || this.TaxCardInstance.get_QYLX().ISPTFPJSP))
            {
                for (int i = 0; i < this.TaxCardInstance.get_StateInfo().InvTypeInfo.Count; i++)
                {
                    if (ShareMethods.IsZCInv(this.TaxCardInstance.get_StateInfo().InvTypeInfo[i].InvType) && (1 == this.TaxCardInstance.get_StateInfo().InvTypeInfo[i].IsLockTime))
                    {
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                this.errorOccur = true;
                this.errCode = "L001";
                this.errMessage = "已到锁死期，无法领取主机分配的发票";
            }
            else if (!this.errorOccur)
            {
                if (!this.CheckBufferEmpty())
                {
                    byte fplx = Convert.ToByte(param[0].ToString());
                    this.ClearBuffer(fplx);
                    if (this.errorOccur)
                    {
                        return;
                    }
                }
                List<InvVolumeApp> reqList = new List<InvVolumeApp>();
                InvVolumeApp item = new InvVolumeApp {
                    InvType = Convert.ToByte(param[0].ToString()),
                    TypeCode = param[1].ToString(),
                    HeadCode = Convert.ToUInt32(param[2].ToString()),
                    Number = Convert.ToUInt16(param[3].ToString())
                };
                reqList.Add(item);
                this.ExecuteDownloadList(reqList);
            }
        }

        private byte Str2InvType(string str)
        {
            switch (str)
            {
                case "增值税专用发票":
                    return 0;

                case "增值税普通发票":
                    return 2;

                case "货物运输业增值税专用发票":
                    return 11;

                case "机动车销售统一发票":
                    return 12;

                case "电子增值税普通发票":
                    return 0x33;

                case "增值税普通发票(卷票)":
                    return 0x29;
            }
            return 0xff;
        }
    }
}

