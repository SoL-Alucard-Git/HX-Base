namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Framework.Plugin.Core.Http;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.BusinessObject;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Xml;

    public class DaiKaiXml
    {
        private ILog loger = LogUtil.GetLogger<DaiKaiXml>();
        private WebClient swdkClient = new WebClient();
        private string swdkUrl = PropertyUtil.GetValue("SWDK_SERVER");

        private List<XmlDocument> DaiKaiFpCreateXml(List<Fpxx> fpList)
        {
            if ((fpList == null) || (fpList.Count == 0))
            {
                return null;
            }
            try
            {
                List<XmlDocument> list = new List<XmlDocument>();
                list.Clear();
                foreach (Fpxx fpxx in fpList)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    XmlDeclaration newChild = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "");
                    xmlDoc.AppendChild(newChild);
                    switch (fpxx.fplx)
                    {
                        case FPLX.ZYFP:
                        case FPLX.PTFP:
                            this.DaiKaiZYPTFPCreateXml(fpxx, ref xmlDoc);
                            break;

                        default:
                            xmlDoc = null;
                            break;
                    }
                    if (xmlDoc != null)
                    {
                        list.Add(xmlDoc);
                    }
                }
                fpList = null;
                return list;
            }
            catch (Exception exception)
            {
                this.loger.Error("税务代开发票遍历构造XML异常：" + exception.ToString());
                return null;
            }
        }

        private object[] DaiKaiFpParseXml(string ReturnMsg)
        {
            if ((ReturnMsg == null) || (ReturnMsg.Length == 0))
            {
                return null;
            }
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(ReturnMsg);
                document.GetElementsByTagName("FPXX");
                return null;
            }
            catch (Exception exception)
            {
                this.loger.Error("税务代开发票遍历构造XML异常：" + exception.ToString());
                return null;
            }
        }

        public object[] DaiKaiFpUpload_New(List<Fpxx> fpList)
        {
            if ((fpList == null) || (fpList.Count == 0))
            {
                return null;
            }
            List<SWDKDMHM> zyfpList = new List<SWDKDMHM>();
            List<SWDKDMHM> ptfpList = new List<SWDKDMHM>();
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            try
            {
                this.swdkUrl = PropertyUtil.GetValue("SWDK_SERVER");
                if ((this.swdkUrl == null) || (this.swdkUrl == ""))
                {
                    this.loger.Error("[DaiKaiFpUpload函数]：税务代开发票的回传IP地址错误");
                    this.swdkUrl = "http://127.0.0.1:80";
                    MessageManager.ShowMsgBox("FPCX-000012");
                    return new object[] { num3 };
                }
                for (int i = 0; i < fpList.Count; i++)
                {
                    try
                    {
                        object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.UploadSWDKFP", new object[] { fpList[i] });
                        if (objArray == null)
                        {
                            MessageManager.ShowMsgBox("FPCX-000010", "提示", new string[] { fpList[i].fpdm, fpList[i].fphm });
                            this.loger.Error("Aisino.Fwkp.UploadSWDKFP接口错误");
                            continue;
                        }
                        XmlDocument document = objArray[0] as XmlDocument;
                        byte[] bytes = ToolUtil.GetBytes(document.OuterXml);
                        if ((bytes != null) && (bytes.Length > 0))
                        {
                            string str = Convert.ToBase64String(bytes);
                            string s = this.swdkClient.Post(this.swdkUrl, str, out num);
                            num2 = 0;
                            if ((s != null) && (s.Length > 0))
                            {
                                s = ToolUtil.GetString(Convert.FromBase64String(s));
                                num2 = Tool.ObjectToInt(s);
                            }
                            if ((num == 0) && (num2 >= 1))
                            {
                                if (i < fpList.Count)
                                {
                                    this.loger.Error("回传第" + i.ToString() + "条记录成功发票代码：" + fpList[i].fpdm + "发票号码：" + fpList[i].fphm);
                                }
                                num3++;
                            }
                            else
                            {
                                if (i < fpList.Count)
                                {
                                    this.loger.Error("回传第" + i.ToString() + "条记录失败:发票代码：" + fpList[i].fpdm + "发票号码：" + fpList[i].fphm + " 错误信息：" + s);
                                }
                                MessageManager.ShowMsgBox("FPCX-000010", "提示", new string[] { fpList[i].fpdm, fpList[i].fphm });
                            }
                        }
                        else
                        {
                            MessageManager.ShowMsgBox("FPCX-000010", "提示", new string[] { fpList[i].fpdm, fpList[i].fphm });
                            if (i < fpList.Count)
                            {
                                this.loger.Error("回传第" + i.ToString() + "条记录失败:发票代码：" + fpList[i].fpdm + "发票号码：" + fpList[i].fphm);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        this.loger.Error(exception.ToString());
                        MessageManager.ShowMsgBox("FPCX-000010", "提示", new string[] { fpList[i].fpdm, fpList[i].fphm });
                    }
                    try
                    {
                        if (fpList[i].zfbz)
                        {
                            SWDKDMHM item = new SWDKDMHM {
                                fpdm = fpList[i].fpdm,
                                fphm = fpList[i].fphm
                            };
                            if (fpList[i].fplx == 0)
                            {
                                zyfpList.Add(item);
                                this.DaiKaiFpZuoFeiUpload(zyfpList, null);
                                zyfpList.Clear();
                            }
                            else if (fpList[i].fplx == FPLX.PTFP)
                            {
                                ptfpList.Add(item);
                                this.DaiKaiFpZuoFeiUpload(null, ptfpList);
                                ptfpList.Clear();
                            }
                        }
                    }
                    catch (Exception exception2)
                    {
                        this.loger.Error(exception2.ToString());
                        this.loger.Error("[DaiKaiFpUpload_New函数异常]:本张发票的作废标志已经上传或者网络原因导致上传失败");
                    }
                    finally
                    {
                        zyfpList.Clear();
                        ptfpList.Clear();
                    }
                }
                num4 = fpList.Count - num3;
                if (num4 < 0)
                {
                    num4 = 0;
                }
                MessageManager.ShowMsgBox("FPCX-000011", "提示", new string[] { fpList.Count.ToString(), num3.ToString(), num4.ToString() });
                fpList = null;
                return new object[] { num3 };
            }
            catch (Exception exception3)
            {
                this.loger.Error("税务代开发票遍历构造XML异常：" + exception3.ToString());
                return new object[] { "1111" };
            }
        }

        public object[] DaiKaiFpUpload_Old(List<Fpxx> fpList)
        {
            if ((fpList == null) || (fpList.Count == 0))
            {
                return null;
            }
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            try
            {
                List<XmlDocument> list = this.DaiKaiFpCreateXml(fpList);
                if ((list == null) || (list.Count <= 0))
                {
                    this.loger.Error("[DaiKaiFpUpload函数]：税务代开发票遍历构造XML异常");
                    return new object[] { "2222" };
                }
                this.swdkUrl = PropertyUtil.GetValue("SWDK_SERVER");
                if ((this.swdkUrl == null) || (this.swdkUrl == ""))
                {
                    this.loger.Error("[DaiKaiFpUpload函数]：税务代开发票的回传IP地址错误");
                    this.swdkUrl = "http://127.0.0.1:80";
                    MessageManager.ShowMsgBox("FPCX-000012");
                    return new object[] { num3 };
                }
                for (int i = 0; i < list.Count; i++)
                {
                    byte[] bytes = ToolUtil.GetBytes(list[i].OuterXml);
                    if ((bytes != null) && (bytes.Length > 0))
                    {
                        string str = Convert.ToBase64String(bytes);
                        string s = this.swdkClient.Post(this.swdkUrl, str, out num);
                        num2 = 0;
                        if ((s != null) && (s.Length > 0))
                        {
                            num2 = Tool.ObjectToInt(ToolUtil.GetString(Convert.FromBase64String(s)));
                        }
                        if ((num == 0) && (num2 >= 1))
                        {
                            if (i < fpList.Count)
                            {
                                this.loger.Error("回传第" + i.ToString() + "条记录成功发票代码：" + fpList[i].fpdm + "发票号码：" + fpList[i].fphm);
                            }
                            num3++;
                        }
                        else
                        {
                            if (i < fpList.Count)
                            {
                                this.loger.Error("回传第" + i.ToString() + "条记录失败:发票代码：" + fpList[i].fpdm + "发票号码：" + fpList[i].fphm);
                            }
                            MessageManager.ShowMsgBox("FPCX-000010", "提示", new string[] { fpList[i].fpdm, fpList[i].fphm });
                        }
                    }
                }
                num4 = list.Count - num3;
                if (num4 < 0)
                {
                    num4 = 0;
                }
                MessageManager.ShowMsgBox("FPCX-000011", "提示", new string[] { list.Count.ToString(), num3.ToString(), num4.ToString() });
                fpList = null;
                return new object[] { num3 };
            }
            catch (Exception exception)
            {
                this.loger.Error("税务代开发票遍历构造XML异常：" + exception.ToString());
                return new object[] { "1111" };
            }
        }

        public object[] DaiKaiFpZuoFeiUpload(List<SWDKDMHM> ZyfpList, List<SWDKDMHM> PtfpList)
        {
            int num = 0;
            int num2 = 0;
            try
            {
                XmlDocument document = this.DaiKaiZYFPZuofeiCreateXml(ZyfpList);
                this.swdkUrl = PropertyUtil.GetValue("SWDK_SERVER");
                if (document != null)
                {
                    if ((this.swdkUrl == null) || (this.swdkUrl == ""))
                    {
                        this.loger.Error("[DaiKaiFpZuoFeiUpload函数]：税务代开专用发票的回传IP地址错误");
                        this.swdkUrl = "http://127.0.0.1:80";
                    }
                    byte[] bytes = ToolUtil.GetBytes(document.OuterXml);
                    if ((bytes != null) && (bytes.Length > 0))
                    {
                        string str = Convert.ToBase64String(bytes);
                        string s = this.swdkClient.Post(this.swdkUrl, str, out num);
                        num2 = 0;
                        if ((s != null) && (s.Length > 0))
                        {
                            num2 = Tool.ObjectToInt(ToolUtil.GetString(Convert.FromBase64String(s)));
                        }
                        if ((num == 0) && (num2 >= 1))
                        {
                            int num3 = ZyfpList.Count - num;
                            if (num3 < 0)
                            {
                                num3 = 0;
                            }
                            this.loger.Error("[DaiKaiFpZuoFeiUpload函数]：税务代开专用发票作废回传服务器成功" + num.ToString() + "条，失败" + num3.ToString() + "条");
                        }
                        else
                        {
                            this.loger.Error("[DaiKaiFpZuoFeiUpload函数]：税务代开专用发票作废回传服务器成功0条，失败:" + ZyfpList.Count.ToString() + "条");
                        }
                    }
                }
                else
                {
                    this.loger.Error("税务代开专票构建XML失败");
                }
                XmlDocument document2 = this.DaiKaiPTFPZuofeiCreateXml(PtfpList);
                if (document2 != null)
                {
                    if ((this.swdkUrl == null) || (this.swdkUrl == ""))
                    {
                        this.loger.Error("[DaiKaiFpZuoFeiUpload函数]：税务代开普通发票的回传IP地址错误");
                        this.swdkUrl = "http://127.0.0.1:80";
                    }
                    byte[] inArray = ToolUtil.GetBytes(document2.OuterXml);
                    if ((inArray != null) && (inArray.Length > 0))
                    {
                        string str3 = Convert.ToBase64String(inArray);
                        string str4 = this.swdkClient.Post(this.swdkUrl, str3, out num);
                        num2 = 0;
                        if ((str4 != null) && (str4.Length > 0))
                        {
                            num2 = Tool.ObjectToInt(ToolUtil.GetString(Convert.FromBase64String(str4)));
                        }
                        if ((num == 0) && (num2 >= 1))
                        {
                            int num4 = PtfpList.Count - num;
                            if (num4 < 0)
                            {
                                num4 = 0;
                            }
                            this.loger.Error("[DaiKaiFpZuoFeiUpload函数]：税务代开普通发票作废回传服务器成功" + num.ToString() + "条，失败" + num4.ToString() + "条");
                        }
                        else
                        {
                            this.loger.Error("[DaiKaiFpZuoFeiUpload函数]：税务代开普通发票作废回传服务器成功0条，失败:" + PtfpList.Count.ToString() + "条");
                        }
                    }
                }
                else
                {
                    this.loger.Error("[DaiKaiFpZuoFeiUpload函数]：税务代开普通发票作废遍历构造XML异常");
                    return new object[] { "1111" };
                }
                return new object[] { "0000" };
            }
            catch (Exception exception)
            {
                this.loger.Error("税务代开发票作废构造XML异常：" + exception.ToString());
                return new object[] { "1111" };
            }
        }

        private void DaiKaiHYFPCreateXml(Fpxx fp, ref XmlDocument xmlDoc)
        {
            if (fp != null)
            {
                try
                {
                    xmlDoc = null;
                }
                catch (Exception exception)
                {
                    this.loger.Error("税务代开发票构造XML异常：" + exception.ToString());
                }
            }
        }

        private void DaiKaiJDCFPCreateXml(Fpxx fp, ref XmlDocument xmlDoc)
        {
            if (fp != null)
            {
                try
                {
                    xmlDoc = null;
                }
                catch (Exception exception)
                {
                    this.loger.Error("税务代开发票构造XML异常：" + exception.ToString());
                }
            }
        }

        private object[] DaiKaiParseReturnMsg(int Type, string ReturnMsg)
        {
            if ((ReturnMsg != null) && (ReturnMsg.Length > 0))
            {
                try
                {
                    switch (Type)
                    {
                        case 0:
                            return this.DaiKaiFpParseXml(ReturnMsg);
                    }
                    return null;
                }
                catch (Exception exception)
                {
                    this.loger.Error("税务代开发票解析服务器返回信息异常：" + exception.ToString());
                }
            }
            return null;
        }

        private XmlDocument DaiKaiPTFPZuofeiCreateXml(List<SWDKDMHM> fpList)
        {
            if ((fpList != null) && (fpList.Count != 0))
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "UTF-8", "");
                    document.AppendChild(newChild);
                    System.Xml.XmlNode node = document.CreateNode(XmlNodeType.Element, "INVALIDPP", "");
                    document.AppendChild(node);
                    foreach (SWDKDMHM swdkdmhm in fpList)
                    {
                        System.Xml.XmlNode node2 = document.CreateElement("FP");
                        node.AppendChild(node2);
                        System.Xml.XmlNode node3 = document.CreateElement("FPHM");
                        node3.InnerText = ShareMethods.FPHMTo8Wei(swdkdmhm.fphm);
                        node2.AppendChild(node3);
                        System.Xml.XmlNode node4 = document.CreateElement("FPDM");
                        node4.InnerText = swdkdmhm.fpdm;
                        node2.AppendChild(node4);
                    }
                    fpList = null;
                    return document;
                }
                catch (Exception exception)
                {
                    this.loger.Error("税务代开发票作废构造XML异常：" + exception.ToString());
                }
            }
            return null;
        }

        private XmlDocument DaiKaiZYFPZuofeiCreateXml(List<SWDKDMHM> fpList)
        {
            if ((fpList != null) && (fpList.Count != 0))
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "UTF-8", "");
                    document.AppendChild(newChild);
                    System.Xml.XmlNode node = document.CreateNode(XmlNodeType.Element, "INVALID", "");
                    document.AppendChild(node);
                    foreach (SWDKDMHM swdkdmhm in fpList)
                    {
                        System.Xml.XmlNode node2 = document.CreateElement("FP");
                        node.AppendChild(node2);
                        System.Xml.XmlNode node3 = document.CreateElement("FPHM");
                        node3.InnerText = ShareMethods.FPHMTo8Wei(swdkdmhm.fphm);
                        node2.AppendChild(node3);
                        System.Xml.XmlNode node4 = document.CreateElement("FPDM");
                        node4.InnerText = swdkdmhm.fpdm;
                        node2.AppendChild(node4);
                    }
                    fpList = null;
                    return document;
                }
                catch (Exception exception)
                {
                    this.loger.Error("税务代开发票作废构造XML异常：" + exception.ToString());
                }
            }
            return null;
        }

        private void DaiKaiZYPTFPCreateXml(Fpxx fp, ref XmlDocument xmlDoc)
        {
            if (fp != null)
            {
                try
                {
                    System.Xml.XmlNode newChild = xmlDoc.CreateNode(XmlNodeType.Element, "INSDATA", "");
                    xmlDoc.AppendChild(newChild);
                    System.Xml.XmlNode node2 = xmlDoc.CreateElement("KPCG_FPHM");
                    node2.InnerText = fp.fphm;
                    newChild.AppendChild(node2);
                    System.Xml.XmlNode node3 = xmlDoc.CreateElement("KPCG_FPDM");
                    node3.InnerText = fp.fpdm;
                    newChild.AppendChild(node3);
                    System.Xml.XmlNode node4 = xmlDoc.CreateElement("WSPZHM");
                    node4.InnerText = fp.xfyhzh;
                    newChild.AppendChild(node4);
                    System.Xml.XmlNode node5 = xmlDoc.CreateElement("FPZL");
                    node5.InnerText = this.PareFpType(fp.fplx).ToString();
                    newChild.AppendChild(node5);
                    System.Xml.XmlNode node6 = xmlDoc.CreateElement("ZFZHBZ");
                    node6.InnerText = "Y";
                    newChild.AppendChild(node6);
                    System.Xml.XmlNode node7 = xmlDoc.CreateElement("KPRQ");
                    node7.InnerText = Tool.ObjectToDateTime(fp.kprq).ToString("yyyy-MM-dd");
                    newChild.AppendChild(node7);
                    System.Xml.XmlNode node8 = xmlDoc.CreateElement("HJJE");
                    node8.InnerText = fp.je;
                    newChild.AppendChild(node8);
                    System.Xml.XmlNode node9 = xmlDoc.CreateElement("SLV");
                    node9.InnerText = fp.sLv;
                    newChild.AppendChild(node9);
                    System.Xml.XmlNode node10 = xmlDoc.CreateElement("HJSE");
                    node10.InnerText = fp.se;
                    newChild.AppendChild(node10);
                    System.Xml.XmlNode node11 = xmlDoc.CreateElement("GFSH");
                    node11.InnerText = fp.gfsh;
                    newChild.AppendChild(node11);
                    System.Xml.XmlNode node12 = xmlDoc.CreateElement("GFMC");
                    node12.InnerText = fp.gfmc;
                    newChild.AppendChild(node12);
                    System.Xml.XmlNode node13 = xmlDoc.CreateElement("GFDZ_DH");
                    node13.InnerText = fp.gfdzdh;
                    newChild.AppendChild(node13);
                    System.Xml.XmlNode node14 = xmlDoc.CreateElement("GFYHMC_YHZH");
                    node14.InnerText = fp.gfyhzh;
                    newChild.AppendChild(node14);
                    System.Xml.XmlNode node15 = xmlDoc.CreateElement("XFSH");
                    node15.InnerText = fp.xfsh;
                    newChild.AppendChild(node15);
                    System.Xml.XmlNode node16 = xmlDoc.CreateElement("XFMC");
                    node16.InnerText = fp.xfmc;
                    newChild.AppendChild(node16);
                    System.Xml.XmlNode node17 = xmlDoc.CreateElement("XFYHMC_YHZH");
                    node17.InnerText = fp.xfdzdh;
                    newChild.AppendChild(node17);
                    System.Xml.XmlNode node18 = xmlDoc.CreateElement("BZ");
                    node18.InnerText = fp.bz;
                    newChild.AppendChild(node18);
                    System.Xml.XmlNode node19 = xmlDoc.CreateElement("SWJGDM");
                    node19.InnerText = "0";
                    newChild.AppendChild(node19);
                    System.Xml.XmlNode node20 = xmlDoc.CreateElement("SWJGMC");
                    node20.InnerText = "0";
                    newChild.AppendChild(node20);
                    System.Xml.XmlNode node21 = xmlDoc.CreateElement("SKR");
                    node21.InnerText = fp.skr;
                    newChild.AppendChild(node21);
                    System.Xml.XmlNode node22 = xmlDoc.CreateElement("FHR");
                    node22.InnerText = fp.fhr;
                    newChild.AppendChild(node22);
                    System.Xml.XmlNode node23 = xmlDoc.CreateElement("KPR");
                    node23.InnerText = fp.kpr;
                    newChild.AppendChild(node23);
                    System.Xml.XmlNode node24 = xmlDoc.CreateElement("TSNSRSBH");
                    node24.InnerText = "0";
                    newChild.AppendChild(node24);
                    System.Xml.XmlNode node25 = xmlDoc.CreateElement("ZBNRS");
                    newChild.AppendChild(node25);
                    if ((fp.Qdxx != null) && (fp.Qdxx.Count > 0))
                    {
                        foreach (Dictionary<SPXX, string> dictionary in fp.Qdxx)
                        {
                            System.Xml.XmlNode node26 = xmlDoc.CreateElement("ZBNR");
                            node25.AppendChild(node26);
                            System.Xml.XmlNode node27 = xmlDoc.CreateElement("HWMC");
                            node27.InnerText = dictionary[(SPXX)0];
                            node26.AppendChild(node27);
                            System.Xml.XmlNode node28 = xmlDoc.CreateElement("GGXH");
                            node28.InnerText = dictionary[(SPXX)3];
                            node26.AppendChild(node28);
                            System.Xml.XmlNode node29 = xmlDoc.CreateElement("JLDW");
                            node29.InnerText = dictionary[(SPXX)4];
                            node26.AppendChild(node29);
                            System.Xml.XmlNode node30 = xmlDoc.CreateElement("SL");
                            node30.InnerText = dictionary[(SPXX)6];
                            node26.AppendChild(node30);
                            System.Xml.XmlNode node31 = xmlDoc.CreateElement("BHSDJ");
                            node31.InnerText = dictionary[(SPXX)5];
                            node26.AppendChild(node31);
                            System.Xml.XmlNode node32 = xmlDoc.CreateElement("BHSJE");
                            node32.InnerText = dictionary[(SPXX)7];
                            node26.AppendChild(node32);
                            System.Xml.XmlNode node33 = xmlDoc.CreateElement("SLV");
                            node33.InnerText = dictionary[(SPXX)8];
                            node26.AppendChild(node33);
                            System.Xml.XmlNode node34 = xmlDoc.CreateElement("SE");
                            node34.InnerText = dictionary[(SPXX)9];
                            node26.AppendChild(node34);
                        }
                    }
                    else if ((fp.Mxxx != null) && (fp.Mxxx.Count > 0))
                    {
                        foreach (Dictionary<SPXX, string> dictionary2 in fp.Mxxx)
                        {
                            System.Xml.XmlNode node35 = xmlDoc.CreateElement("ZBNR");
                            node25.AppendChild(node35);
                            System.Xml.XmlNode node36 = xmlDoc.CreateElement("HWMC");
                            node36.InnerText = dictionary2[0];
                            node35.AppendChild(node36);
                            System.Xml.XmlNode node37 = xmlDoc.CreateElement("GGXH");
                            node37.InnerText = dictionary2[(SPXX)3];
                            node35.AppendChild(node37);
                            System.Xml.XmlNode node38 = xmlDoc.CreateElement("JLDW");
                            node38.InnerText = dictionary2[(SPXX)4];
                            node35.AppendChild(node38);
                            System.Xml.XmlNode node39 = xmlDoc.CreateElement("SL");
                            node39.InnerText = dictionary2[(SPXX)6];
                            node35.AppendChild(node39);
                            System.Xml.XmlNode node40 = xmlDoc.CreateElement("BHSDJ");
                            node40.InnerText = dictionary2[(SPXX)5];
                            node35.AppendChild(node40);
                            System.Xml.XmlNode node41 = xmlDoc.CreateElement("BHSJE");
                            node41.InnerText = dictionary2[(SPXX)7];
                            node35.AppendChild(node41);
                            System.Xml.XmlNode node42 = xmlDoc.CreateElement("SLV");
                            node42.InnerText = dictionary2[(SPXX)8];
                            node35.AppendChild(node42);
                            System.Xml.XmlNode node43 = xmlDoc.CreateElement("SE");
                            node43.InnerText = dictionary2[(SPXX)9];
                            node35.AppendChild(node43);
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.loger.Error("税务代开发票构造XML异常：" + exception.ToString());
                }
            }
        }

        private int PareFpType(FPLX fplx)
        {
            switch (fplx)
            {
                case 0:
                    return 1;

                case FPLX.PTFP:
                    return 2;
            }
            return 0;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SWDKDMHM
        {
            public string fpdm;
            public string fphm;
        }
    }
}

