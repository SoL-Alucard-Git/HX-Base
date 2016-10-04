namespace Aisino.Fwkp.Fplygl.Form.WLGP_4
{
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Fplygl.Common;
    using Aisino.Fwkp.Fplygl.Form.Common;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.Xml;

    public class DownloadController
    {
        private bool buffFull;
        private int curElectricDownloadNum;
        private int electricApplyNum;
        private bool hasElectricVolume;
        private bool hjErrTriger;
        private ILog loger = LogUtil.GetLogger<DownloadController>();
        private bool logFlag;
        private string logPath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Log\");
        private List<InvVolumeApp> successList = new List<InvVolumeApp>();
        private TaxCard TaxCardInstance = TaxCardFactory.CreateTaxCard();
        private bool webCommunicationError;
        private bool webPartialError;
        private bool zpErrTriger;

        private bool CheckHasElectricVolume(List<InvVolumeApp> reqList)
        {
            foreach (InvVolumeApp app in reqList)
            {
                if (0x33 == app.InvType)
                {
                    return true;
                }
            }
            return false;
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

        private bool ClearAllocateBuffer()
        {
            UnlockInvoice invoice = this.TaxCardInstance.NInvGetUnlockInvoice(11);
            if (this.TaxCardInstance.get_RetCode() != 0)
            {
                MessageManager.ShowMsgBox(this.TaxCardInstance.get_ErrCode());
                return false;
            }
            if (!DownloadCommon.CheckEmpty(invoice.Buffer))
            {
                InvVolumeApp confirmVolumn = new InvVolumeApp {
                    InvType = Convert.ToByte(invoice.get_Kind()),
                    TypeCode = invoice.get_TypeCode(),
                    HeadCode = Convert.ToUInt32(invoice.get_Number()),
                    Number = Convert.ToUInt16(invoice.get_Count())
                };
                if (0x33 == confirmVolumn.InvType)
                {
                    return true;
                }
                string xml = string.Empty;
                if (HttpsSender.SendMsg("0040", this.RequestDownloadInput(null, confirmVolumn, Convert.ToBase64String(invoice.Buffer), true), ref xml) != 0)
                {
                    MessageManager.ShowMsgBox(xml);
                    return false;
                }
                XmlDocument downInvXml = new XmlDocument();
                downInvXml.LoadXml(xml);
                if (this.logFlag)
                {
                    downInvXml.Save(this.logPath + @"\AllocateRequestDownloadOutput.xml");
                }
                this.RequestDownloadOutput(downInvXml);
            }
            return true;
        }

        private void ExecuteDownloadList(List<InvVolumeApp> reqList)
        {
            int num = 0;
            foreach (InvVolumeApp app in reqList)
            {
                if (this.buffFull || this.webPartialError)
                {
                    break;
                }
                num++;
                if ((!ShareMethods.IsHXInv(app.InvType) || !this.zpErrTriger) && (!ShareMethods.IsZCInv(app.InvType) || !this.hjErrTriger))
                {
                    bool flag;
                    UnlockInvoice invoice = new UnlockInvoice();
                    InvVolumeApp locked = new InvVolumeApp();
                    InvVolumeApp tarInv = app;
                    invoice = this.TaxCardInstance.NInvGetUnlockInvoice(tarInv.InvType);
                    if (this.TaxCardInstance.get_RetCode() != 0)
                    {
                        MessageManager.ShowMsgBox(this.TaxCardInstance.get_ErrCode());
                        break;
                    }
                    bool flag2 = DownloadCommon.CheckEmpty(invoice.Buffer);
                    if (flag2)
                    {
                        flag = false;
                    }
                    else
                    {
                        locked.InvType = Convert.ToByte(invoice.get_Kind());
                        locked.TypeCode = invoice.get_TypeCode();
                        locked.HeadCode = Convert.ToUInt32(invoice.get_Number());
                        locked.Number = Convert.ToUInt16(invoice.get_Count());
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
                    string xml = string.Empty;
                    if (HttpsSender.SendMsg("0020", this.RequestDownloadInput(tarInv, locked, Convert.ToBase64String(invoice.Buffer), false), ref xml) != 0)
                    {
                        MessageManager.ShowMsgBox(xml);
                        this.webCommunicationError = true;
                        break;
                    }
                    XmlDocument downInvXml = new XmlDocument();
                    downInvXml.LoadXml(xml);
                    if (this.logFlag)
                    {
                        downInvXml.Save(this.logPath + @"\RequestDownloadOutput.xml");
                    }
                    this.RequestDownloadOutput(downInvXml);
                    if (this.zpErrTriger && this.hjErrTriger)
                    {
                        break;
                    }
                    if (!flag && ((num == reqList.Count) || ((num != reqList.Count) && !this.CheckOneSystem(reqList[num - 1].InvType, reqList[num].InvType))))
                    {
                        invoice = this.TaxCardInstance.NInvGetUnlockInvoice(tarInv.InvType);
                        Convert.ToByte(invoice.get_Kind());
                        locked = new InvVolumeApp {
                            InvType = Convert.ToByte(invoice.get_Kind()),
                            TypeCode = invoice.get_TypeCode(),
                            HeadCode = Convert.ToUInt32(invoice.get_Number()),
                            Number = Convert.ToUInt16(invoice.get_Count())
                        };
                        if (HttpsSender.SendMsg("0020", this.RequestDownloadInput(null, locked, Convert.ToBase64String(invoice.Buffer), false), ref xml) != 0)
                        {
                            MessageManager.ShowMsgBox(xml);
                            this.webCommunicationError = true;
                            break;
                        }
                        XmlDocument document2 = new XmlDocument();
                        document2.LoadXml(xml);
                        if (this.logFlag)
                        {
                            document2.Save(this.logPath + @"\RequestDownloadOutputSingle.xml");
                        }
                        this.RequestDownloadOutput(document2);
                        if (this.zpErrTriger && this.hjErrTriger)
                        {
                            break;
                        }
                    }
                }
            }
        }

        private string RequestDownloadInput(InvVolumeApp downVolumn, InvVolumeApp confirmVolumn, string confirmCrypt, bool withOperation)
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
            if (node.InnerText.StartsWith("D0") || node.InnerText.Equals("0000"))
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
                    item.InvType = Convert.ToByte(invoice.get_Kind());
                    item.TypeCode = invoice.get_TypeCode();
                    item.HeadCode = Convert.ToUInt32(invoice.get_Number());
                    item.Number = Convert.ToUInt16(invoice.get_Count());
                    byte[] buffer = Convert.FromBase64String(s);
                    this.TaxCardInstance.NInvWirteConfirmResult(type, buffer, buffer.Length);
                    if (this.TaxCardInstance.get_RetCode() != 0)
                    {
                        MessageManager.ShowMsgBox(this.TaxCardInstance.get_ErrCode());
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
                    if (0x33 == item.InvType)
                    {
                        this.curElectricDownloadNum += item.Number;
                    }
                }
                if (flag2)
                {
                    int num2 = Convert.ToInt32(str2);
                    byte[] buffer2 = Convert.FromBase64String(str4);
                    this.TaxCardInstance.InvRead("3", buffer2, buffer2.Length, Convert.ToInt32(num2));
                    if (this.TaxCardInstance.get_RetCode() != 0)
                    {
                        MessageManager.ShowMsgBox(this.TaxCardInstance.get_ErrCode());
                        if (0x251d == this.TaxCardInstance.get_RetCode())
                        {
                            this.buffFull = true;
                        }
                        if (ShareMethods.IsHXInv(num2))
                        {
                            this.zpErrTriger = true;
                            return;
                        }
                        if (ShareMethods.IsZCInv(num2))
                        {
                            this.hjErrTriger = true;
                        }
                        return;
                    }
                }
                if (node.InnerText.StartsWith("D0"))
                {
                    this.webPartialError = true;
                    if (node.InnerText.Equals("D007"))
                    {
                        MessageManager.ShowMsgBox(node2.InnerText);
                    }
                }
            }
            else
            {
                MessageManager.ShowMsgBox("INP-441246", new string[] { node2.InnerText });
                this.zpErrTriger = true;
                this.hjErrTriger = true;
            }
        }

        public void RunCommond()
        {
            string driverVersion = this.TaxCardInstance.get_StateInfo().DriverVersion;
            string str2 = driverVersion.Substring(7, 2) + driverVersion.Substring(10, 6);
            bool flag = false;
            if (this.TaxCardInstance.get_SoftVersion().Equals("FWKP_V2.0_Svr_Client"))
            {
                if (this.TaxCardInstance.get_Machine() != 0)
                {
                    if (str2.CompareTo("A0150730") < 0)
                    {
                        MessageManager.ShowMsgBox("INP-441244");
                        return;
                    }
                }
                else if (str2.CompareTo("A0150729") < 0)
                {
                    MessageManager.ShowMsgBox("INP-441244");
                    return;
                }
            }
            else if (this.TaxCardInstance.get_Machine() == 0)
            {
                if (str2.CompareTo("L0110501") < 0)
                {
                    MessageManager.ShowMsgBox("INP-441244");
                    return;
                }
            }
            else if (str2.CompareTo("L1150730") < 0)
            {
                MessageManager.ShowMsgBox("INP-441244");
                return;
            }
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
                MessageManager.ShowMsgBox("INP-441245");
            }
            else
            {
                List<InvVolumeApp> reqList = new List<InvVolumeApp>();
                if (this.TaxCardInstance.get_Machine() == 0)
                {
                    ListDown down = new ListDown();
                    DialogResult result = down.ShowDialog();
                    if (DialogResult.OK == result)
                    {
                        reqList = down.reqInvList;
                    }
                    else
                    {
                        if (DialogResult.No != result)
                        {
                            return;
                        }
                        SingleVolumn volumn = new SingleVolumn();
                        if (volumn.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                        reqList = volumn.singleInvVolumn;
                        if (this.CheckHasElectricVolume(reqList))
                        {
                            this.hasElectricVolume = true;
                            this.electricApplyNum = volumn.electricApplyNum;
                        }
                    }
                }
                else
                {
                    SingleVolumn volumn2 = new SingleVolumn();
                    if (volumn2.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    reqList = volumn2.singleInvVolumn;
                    if (this.CheckHasElectricVolume(reqList))
                    {
                        this.hasElectricVolume = true;
                        this.electricApplyNum = volumn2.electricApplyNum;
                    }
                }
                if (this.hasElectricVolume)
                {
                    if (this.ClearAllocateBuffer())
                    {
                        while (((!this.buffFull && !this.hjErrTriger) && (!this.webCommunicationError && !this.webPartialError)) && (this.curElectricDownloadNum < this.electricApplyNum))
                        {
                            if (this.curElectricDownloadNum != 0)
                            {
                                reqList = DownloadCommon.GetElectricDownloadVolumes(0x33, this.electricApplyNum - this.curElectricDownloadNum, 0xffff);
                            }
                            this.ExecuteDownloadList(reqList);
                        }
                    }
                }
                else
                {
                    this.ExecuteDownloadList(reqList);
                }
                if (this.successList.Count > 0)
                {
                    InvInfoWebgetMsg msg = new InvInfoWebgetMsg();
                    msg.InsertInvVolume(this.successList);
                    msg.ShowDialog();
                }
                else
                {
                    MessageManager.ShowMsgBox("INP-441247");
                }
            }
        }
    }
}

