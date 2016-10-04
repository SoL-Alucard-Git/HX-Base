namespace ns4
{
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    internal class Class84
    {
        private Class100 class100_0;
        private Class81 class81_0;
        private Class83 class83_0;
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;
        private TaxCard taxCard_0;

        public Class84()
        {
            
            this.taxCard_0 = TaxCard.CreateInstance(CTaxCardType.const_7);
            this.class100_0 = new Class100();
            this.class83_0 = new Class83();
            this.class81_0 = new Class81();
            this.string_0 = string.Empty;
            this.string_1 = "0";
            this.string_2 = string.Empty;
            this.string_3 = string.Empty;
            this.string_0 = this.taxCard_0.GetInvControlNum();
            this.string_1 = this.taxCard_0.Machine.ToString();
            this.string_2 = this.taxCard_0.TaxCode;
            this.string_3 = this.taxCard_0.CompressCode;
        }

        private XmlDocument method_0(List<Fpxx> fpInfo)
        {
            if ((fpInfo == null) || (fpInfo.Count < 1))
            {
                return null;
            }
            XmlDocument document = new XmlDocument();
            try
            {
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", "");
                document.AppendChild(newChild);
                XmlNode node = document.CreateNode(XmlNodeType.Element, "FPXT", "");
                document.AppendChild(node);
                XmlNode node2 = document.CreateNode(XmlNodeType.Element, "INPUT", "");
                node.AppendChild(node2);
                XmlElement element = document.CreateElement("NSRSBH");
                element.InnerText = this.string_2;
                node2.AppendChild(element);
                XmlElement element2 = document.CreateElement("KPJH");
                element2.InnerText = this.string_1;
                node2.AppendChild(element2);
                XmlElement element3 = document.CreateElement("SBBH");
                element3.InnerText = this.string_0;
                node2.AppendChild(element3);
                for (int i = 0; i < fpInfo.Count; i++)
                {
                    XmlNode node3 = this.method_4(fpInfo[i], ref document);
                    if (node3 != null)
                    {
                        node2.AppendChild(node3);
                    }
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_1("(上传线程)上传服务器xml文档组织失败！" + exception.ToString());
            }
            return document;
        }

        private XmlDocument method_1(List<Fpxx> fpInfo)
        {
            if ((fpInfo == null) || (fpInfo.Count < 1))
            {
                return null;
            }
            XmlDocument document = new XmlDocument();
            try
            {
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", "");
                document.AppendChild(newChild);
                XmlNode node = document.CreateNode(XmlNodeType.Element, "business", "");
                ((XmlElement) node).SetAttribute("id", "FPSSSC");
                document.AppendChild(node);
                XmlNode node2 = document.CreateNode(XmlNodeType.Element, "body", "");
                XmlElement element2 = (XmlElement) node2;
                element2.SetAttribute("count", "1");
                element2.SetAttribute("skph", this.string_0);
                element2.SetAttribute("nsrsbh", this.string_2);
                element2.SetAttribute("kpjh", this.string_1);
                node.AppendChild(node2);
                XmlNode node3 = document.CreateNode(XmlNodeType.Element, "input", "");
                node2.AppendChild(node3);
                XmlNode node4 = document.CreateNode(XmlNodeType.Element, "group", "");
                ((XmlElement) node4).SetAttribute("xh", "1");
                node3.AppendChild(node4);
                XmlElement element3 = document.CreateElement("fplxdm");
                element3.InnerText = "025";
                node4.AppendChild(element3);
                XmlElement element4 = document.CreateElement("fpzs");
                element4.InnerText = fpInfo.Count.ToString();
                node4.AppendChild(element4);
                XmlDocument document2 = new XmlDocument();
                string str = "";
                document2 = this.method_8(fpInfo);
                Class101.smethod_0("(发票上传)卷票上传原包：" + document2.InnerXml);
                byte[] bytes = ToolUtil.GetBytes(document2.InnerXml.ToString());
                if ((bytes != null) && (bytes.Length > 0))
                {
                    str = Convert.ToBase64String(bytes);
                }
                XmlElement element5 = document.CreateElement("fpmx");
                element5.InnerText = str;
                node4.AppendChild(element5);
            }
            catch (Exception exception)
            {
                Class101.smethod_1("(上传线程)上传服务器xml文档组织失败！" + exception.ToString());
            }
            return document;
        }

        public string method_10(XmlDocument xmlDocument_0, ref string string_4, out string string_5)
        {
            string innerText = string.Empty;
            string_5 = string.Empty;
            if (xmlDocument_0 != null)
            {
                XmlNodeList childNodes = xmlDocument_0.GetElementsByTagName("OUTPUT")[0].ChildNodes;
                if (childNodes == null)
                {
                    return innerText;
                }
                foreach (XmlNode node in childNodes)
                {
                    if (string.Equals(node.Name, "CODE", StringComparison.CurrentCultureIgnoreCase))
                    {
                        innerText = node.InnerText;
                    }
                    else if (string.Equals(node.Name, "MESS", StringComparison.CurrentCultureIgnoreCase))
                    {
                        string_5 = node.InnerText;
                    }
                    else if (string.Equals(node.Name, "DATA", StringComparison.CurrentCultureIgnoreCase))
                    {
                        XmlNodeList list2 = node.ChildNodes;
                        if (list2 != null)
                        {
                            foreach (XmlNode node2 in list2)
                            {
                                if (string.Equals(node2.Name, "SLXLH", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    string_4 = node2.InnerText;
                                }
                                if (!string.IsNullOrEmpty(innerText) && !string.IsNullOrEmpty(string_4))
                                {
                                    return innerText;
                                }
                            }
                        }
                    }
                }
            }
            return innerText;
        }

        public string method_11(XmlDocument xmlDocument_0, ref string string_4, out string string_5)
        {
            string innerText = string.Empty;
            string_5 = string.Empty;
            if (xmlDocument_0 != null)
            {
                XmlNodeList childNodes = xmlDocument_0.GetElementsByTagName("group")[0].ChildNodes;
                if (childNodes == null)
                {
                    return innerText;
                }
                foreach (XmlNode node in childNodes)
                {
                    if (string.Equals(node.Name, "returncode", StringComparison.CurrentCultureIgnoreCase))
                    {
                        innerText = node.InnerText;
                    }
                    else if (string.Equals(node.Name, "returnmsg", StringComparison.CurrentCultureIgnoreCase))
                    {
                        string_5 = node.InnerText;
                    }
                    else
                    {
                        if (string.Equals(node.Name, "slxlh", StringComparison.CurrentCultureIgnoreCase))
                        {
                            string_4 = node.InnerText;
                        }
                        if (!string.IsNullOrEmpty(innerText) && !string.IsNullOrEmpty(string_4))
                        {
                            return innerText;
                        }
                    }
                }
            }
            return innerText;
        }

        public void method_12(XmlDocument xmlDocument_0, string string_4)
        {
            if (xmlDocument_0 != null)
            {
                try
                {
                    XmlNodeList childNodes = xmlDocument_0.GetElementsByTagName("INPUT")[0].ChildNodes;
                    if (childNodes != null)
                    {
                        new Class81();
                        Class96 class2 = new Class96 {
                            FPSLH = string_4,
                            FpUploadTime = DateTime.Now.ToString(),
                            FPStatus = "3",
                            FPSQHRecieveTime = DateTime.Now.ToString(),
                            ISFpDownFailed = false,
                            IsFpUpFailed = false,
                            Boolean_0 = false
                        };
                        foreach (XmlNode node in childNodes)
                        {
                            if (string.Equals(node.Name, "NSRSBH", StringComparison.CurrentCultureIgnoreCase))
                            {
                                class2.FpNSRSBH = node.InnerText;
                            }
                            else if (string.Equals(node.Name, "KPJH", StringComparison.CurrentCultureIgnoreCase))
                            {
                                class2.FpKPJH = node.InnerText;
                            }
                            else if (string.Equals(node.Name, "SBBH", StringComparison.CurrentCultureIgnoreCase))
                            {
                                class2.FpSBBH = node.InnerText;
                            }
                            else
                            {
                                if (string.Equals(node.Name, "FP", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    XmlNodeList list2 = node.ChildNodes;
                                    if (list2 != null)
                                    {
                                        class2.FPNO = string.Empty;
                                        class2.FPDM = string.Empty;
                                        class2.Fplx = string.Empty;
                                        IEnumerator enumerator2 = list2.GetEnumerator();
                                        {
                                            while (enumerator2.MoveNext())
                                            {
                                                XmlNode current = (XmlNode) enumerator2.Current;
                                                bool flag = false;
                                                if (string.Equals(current.Name, "FPHM", StringComparison.CurrentCultureIgnoreCase))
                                                {
                                                    class2.FPNO = current.InnerText;
                                                }
                                                if (string.Equals(current.Name, "FPZL", StringComparison.CurrentCultureIgnoreCase))
                                                {
                                                    if (string.Equals(current.InnerText, Convert.ToString(2), StringComparison.CurrentCultureIgnoreCase))
                                                    {
                                                        class2.Fplx = FPLX.PTFP.ToString();
                                                    }
                                                    else if (string.Equals(current.InnerText, Convert.ToString(0), StringComparison.CurrentCultureIgnoreCase))
                                                    {
                                                        class2.Fplx = FPLX.ZYFP.ToString();
                                                    }
                                                    else if (string.Equals(current.InnerText, Convert.ToString(12), StringComparison.CurrentCultureIgnoreCase))
                                                    {
                                                        class2.Fplx = FPLX.JDCFP.ToString();
                                                    }
                                                    else if (string.Equals(current.InnerText, Convert.ToString(11), StringComparison.CurrentCultureIgnoreCase))
                                                    {
                                                        class2.Fplx = FPLX.HYFP.ToString();
                                                    }
                                                    else if (!string.Equals(current.InnerText, Convert.ToString(0x33), StringComparison.CurrentCultureIgnoreCase) && !string.Equals(current.InnerText, "026", StringComparison.CurrentCultureIgnoreCase))
                                                    {
                                                        if (string.Equals(current.InnerText, Convert.ToString(0x29), StringComparison.CurrentCultureIgnoreCase) || string.Equals(current.InnerText, "026", StringComparison.CurrentCultureIgnoreCase))
                                                        {
                                                            class2.Fplx = FPLX.JSFP.ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        class2.Fplx = FPLX.DZFP.ToString();
                                                    }
                                                }
                                                if (string.Equals(current.Name, "FPDM", StringComparison.CurrentCultureIgnoreCase))
                                                {
                                                    class2.FPDM = current.InnerText;
                                                }
                                                if (string.Equals(current.Name, "ZFBZ", StringComparison.CurrentCultureIgnoreCase))
                                                {
                                                    if (current.InnerText.Equals("Y", StringComparison.CurrentCultureIgnoreCase))
                                                    {
                                                        class2.ZFBZ = true;
                                                    }
                                                    else
                                                    {
                                                        class2.ZFBZ = false;
                                                    }
                                                    flag = true;
                                                }
                                                if ((!string.IsNullOrEmpty(class2.FPNO) && !string.IsNullOrEmpty(class2.Fplx)) && (!string.IsNullOrEmpty(class2.FPDM) && flag))
                                                {
                                                    goto Label_0372;
                                                }
                                            }
                                            goto Label_03DB;
                                        Label_0372:
                                            class2.DZSYH = this.class81_0.method_11(class2.FPNO, class2.FPDM, class2.Fplx);
                                            this.class100_0.method_1(class2, Enum10.Insert);
                                            class2.FPDM = string.Empty;
                                            class2.FPNO = string.Empty;
                                            class2.DZSYH = string.Empty;
                                        }
                                    }
                                }
                            Label_03DB:;
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("(上传线程)往临时表写记录失败！ " + exception.ToString());
                }
            }
        }

        public void method_13(XmlDocument xmlDocument_0, string string_4)
        {
            if (xmlDocument_0 != null)
            {
                Class101.smethod_0("(发票上传)卷式发票受理序列号：" + string_4 + "   上传原文：" + xmlDocument_0.InnerXml);
                try
                {
                    byte[] buffer = Convert.FromBase64String(xmlDocument_0.GetElementsByTagName("fpmx")[0].InnerText);
                    XmlDocument document = new XmlDocument();
                    document.LoadXml(ToolUtil.GetString(buffer));
                    XmlNodeList childNodes = document.GetElementsByTagName("INPUT")[0].ChildNodes;
                    if (childNodes != null)
                    {
                        new Class81();
                        Class96 class2 = new Class96 {
                            FPSLH = string_4,
                            FpUploadTime = DateTime.Now.ToString(),
                            FPStatus = "3",
                            FPSQHRecieveTime = DateTime.Now.ToString(),
                            ISFpDownFailed = false,
                            IsFpUpFailed = false,
                            Boolean_0 = false
                        };
                        foreach (XmlNode node in childNodes)
                        {
                            if (string.Equals(node.Name, "NSRSBH", StringComparison.CurrentCultureIgnoreCase))
                            {
                                class2.FpNSRSBH = node.InnerText;
                            }
                            else if (string.Equals(node.Name, "KPJH", StringComparison.CurrentCultureIgnoreCase))
                            {
                                class2.FpKPJH = node.InnerText;
                            }
                            else if (string.Equals(node.Name, "SBBH", StringComparison.CurrentCultureIgnoreCase))
                            {
                                class2.FpSBBH = node.InnerText;
                            }
                            else
                            {
                                if (string.Equals(node.Name, "FP", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    XmlNodeList list3 = node.ChildNodes;
                                    if (list3 != null)
                                    {
                                        class2.FPNO = string.Empty;
                                        class2.FPDM = string.Empty;
                                        class2.Fplx = string.Empty;
                                        IEnumerator enumerator2 = list3.GetEnumerator();
                                        {
                                            while (enumerator2.MoveNext())
                                            {
                                                XmlNode current = (XmlNode) enumerator2.Current;
                                                bool flag = false;
                                                if (string.Equals(current.Name, "FPHM", StringComparison.CurrentCultureIgnoreCase))
                                                {
                                                    class2.FPNO = current.InnerText;
                                                }
                                                if (string.Equals(current.Name, "FPZL", StringComparison.CurrentCultureIgnoreCase))
                                                {
                                                    if (string.Equals(current.InnerText, Convert.ToString(2), StringComparison.CurrentCultureIgnoreCase))
                                                    {
                                                        class2.Fplx = FPLX.PTFP.ToString();
                                                    }
                                                    else if (string.Equals(current.InnerText, Convert.ToString(0), StringComparison.CurrentCultureIgnoreCase))
                                                    {
                                                        class2.Fplx = FPLX.ZYFP.ToString();
                                                    }
                                                    else if (string.Equals(current.InnerText, Convert.ToString(12), StringComparison.CurrentCultureIgnoreCase))
                                                    {
                                                        class2.Fplx = FPLX.JDCFP.ToString();
                                                    }
                                                    else if (string.Equals(current.InnerText, Convert.ToString(11), StringComparison.CurrentCultureIgnoreCase))
                                                    {
                                                        class2.Fplx = FPLX.HYFP.ToString();
                                                    }
                                                    else if (!string.Equals(current.InnerText, Convert.ToString(0x33), StringComparison.CurrentCultureIgnoreCase) && !string.Equals(current.InnerText, "026", StringComparison.CurrentCultureIgnoreCase))
                                                    {
                                                        if (string.Equals(current.InnerText, Convert.ToString(0x29), StringComparison.CurrentCultureIgnoreCase) || string.Equals(current.InnerText, "025", StringComparison.CurrentCultureIgnoreCase))
                                                        {
                                                            class2.Fplx = FPLX.JSFP.ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        class2.Fplx = FPLX.DZFP.ToString();
                                                    }
                                                }
                                                if (string.Equals(current.Name, "FPDM", StringComparison.CurrentCultureIgnoreCase))
                                                {
                                                    class2.FPDM = current.InnerText;
                                                }
                                                if (string.Equals(current.Name, "ZFBZ", StringComparison.CurrentCultureIgnoreCase))
                                                {
                                                    if (current.InnerText.Equals("Y", StringComparison.CurrentCultureIgnoreCase))
                                                    {
                                                        class2.ZFBZ = true;
                                                    }
                                                    else
                                                    {
                                                        class2.ZFBZ = false;
                                                    }
                                                    flag = true;
                                                }
                                                if ((!string.IsNullOrEmpty(class2.FPNO) && !string.IsNullOrEmpty(class2.Fplx)) && (!string.IsNullOrEmpty(class2.FPDM) && flag))
                                                {
                                                    goto Label_03DD;
                                                }
                                            }
                                            goto Label_049E;
                                        Label_03DD:
                                            class2.DZSYH = this.class81_0.method_11(class2.FPNO, class2.FPDM, class2.Fplx);
                                            Class101.smethod_0("(发票上传)临时表置报送中：fpdm：" + class2.FPDM + "  fphm:" + class2.FPNO + "   fpzl:" + class2.Fplx);
                                            this.class100_0.method_1(class2, Enum10.Insert);
                                            class2.FPDM = string.Empty;
                                            class2.FPNO = string.Empty;
                                            class2.DZSYH = string.Empty;
                                        }
                                    }
                                }
                            Label_049E:;
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("(上传线程)往临时表写记录失败！ " + exception.ToString());
                }
            }
        }

        public void method_14(XmlDocument xmlDocument_0, string string_4, string string_5)
        {
            if (((xmlDocument_0 != null) && !string.IsNullOrEmpty(string_4)) && !string.IsNullOrEmpty(string_5))
            {
                Class96 class2 = new Class96 {
                    FpKPJH = this.string_1,
                    FpNSRSBH = this.string_2,
                    FpSBBH = this.string_0,
                    FPSLH = string_4,
                    FPStatus = "3",
                    FpUploadTime = DateTime.Now.ToString(),
                    FPSQHRecieveTime = DateTime.Now.ToString(),
                    ISFpDownFailed = false,
                    IsFpUpFailed = false,
                    Boolean_0 = false
                };
                if (string_5.Equals("009"))
                {
                    class2.Fplx = FPLX.HYFP.ToString();
                }
                else if (string_5.Equals("005"))
                {
                    class2.Fplx = FPLX.JDCFP.ToString();
                }
                try
                {
                    XmlNodeList elementsByTagName = xmlDocument_0.GetElementsByTagName("data");
                    string str = string.Empty;
                    int num = 0;
                    while (num < elementsByTagName.Count)
                    {
                        if (elementsByTagName[num].Attributes["name"].Value.Equals("fpmx", StringComparison.CurrentCultureIgnoreCase) && elementsByTagName[num].Attributes["name"].Value.Equals("fpmx", StringComparison.CurrentCultureIgnoreCase))
                        {
                            goto Label_015B;
                        }
                        num++;
                    }
                    goto Label_0192;
                Label_015B:
                    if (elementsByTagName[num].Attributes["value"] != null)
                    {
                        str = elementsByTagName[num].Attributes["value"].Value;
                    }
                Label_0192:
                    if (!string.IsNullOrEmpty(str))
                    {
                        byte[] buffer = Convert.FromBase64String(str);
                        if ((buffer == null) || (buffer.Length < 1))
                        {
                            return;
                        }
                        string xml = ToolUtil.GetString(buffer);
                        XmlDocument document = new XmlDocument();
                        document.LoadXml(xml);
                        XmlNodeList list3 = document.GetElementsByTagName("mx");
                        if ((list3 == null) || (list3.Count < 1))
                        {
                            return;
                        }
                        for (int i = 0; i < list3.Count; i++)
                        {
                            XmlNodeList childNodes = list3[i].ChildNodes;
                            if ((childNodes != null) && (childNodes.Count >= 1))
                            {
                                for (int j = 0; j < childNodes.Count; j++)
                                {
                                    bool flag = false;
                                    if (childNodes[j].Attributes["name"].Value.Equals("fpdm", StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        class2.FPDM = childNodes[j].InnerText;
                                    }
                                    if (childNodes[j].Attributes["name"].Value.Equals("fphm", StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        class2.FPNO = childNodes[j].InnerText;
                                    }
                                    if (childNodes[j].Attributes["name"].Value.Equals("fpztbz", StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        if (!childNodes[j].InnerText.Equals("0") && !childNodes[j].InnerText.Equals("1"))
                                        {
                                            class2.ZFBZ = true;
                                        }
                                        else
                                        {
                                            class2.ZFBZ = false;
                                        }
                                        flag = true;
                                    }
                                    if ((!string.IsNullOrEmpty(class2.FPDM) && !string.IsNullOrEmpty(class2.FPNO)) && flag)
                                    {
                                        goto Label_034D;
                                    }
                                }
                            }
                            continue;
                        Label_034D:
                            class2.DZSYH = this.class81_0.method_11(class2.FPNO, class2.FPDM, class2.Fplx);
                            this.class100_0.method_1(class2, Enum10.Insert);
                            class2.FPNO = string.Empty;
                            class2.FPDM = string.Empty;
                            class2.DZSYH = string.Empty;
                        }
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("货运机动车发票上传回馈信息写临时表失败！" + exception.ToString());
                }
            }
        }

        public string method_15(XmlDocument xmlDocument_0)
        {
            string str = string.Empty;
            if (xmlDocument_0 != null)
            {
                XmlNode node = xmlDocument_0.GetElementsByTagName("SLXLH")[0];
                if (node == null)
                {
                    return str;
                }
                string innerText = node.InnerText;
                try
                {
                    DataTable table2;
                    lock (Class97.dataTable_0)
                    {
                        Class97.dataTable_0.DefaultView.RowFilter = "FPSLH='" + innerText + "'";
                        table2 = Class97.dataTable_0.DefaultView.ToTable(true, new string[] { "FPNO", "FPDM", "Fplx" });
                        Class97.dataTable_0.DefaultView.RowFilter = "";
                    }
                    if ((table2 != null) && (table2.Rows.Count >= 1))
                    {
                        Class96 class2 = new Class96();
                        new Class81();
                        for (int i = 0; i < table2.Rows.Count; i++)
                        {
                            class2.FPNO = table2.Rows[0]["FPNO"].ToString();
                            class2.FPDM = table2.Rows[0]["FPDM"].ToString();
                            class2.Fplx = table2.Rows[0]["Fplx"].ToString();
                            class2 = this.class100_0.method_3(Class97.dataTable_0, class2.FPNO, class2.FPDM);
                            if (class2 != null)
                            {
                                class2.FPSQHRecieveTime = DateTime.Now.ToString();
                                Class101.smethod_0("(发票下载)SetLookNoFpInfo：FPSQHRecieveTime：" + class2.FPSQHRecieveTime + "   FpUploadTime:" + class2.FpUploadTime);
                                TimeSpan span = (TimeSpan) (DateTime.Parse(class2.FPSQHRecieveTime) - DateTime.Parse(class2.FpUploadTime));
                                if (span.Minutes >= Class87.int_4)
                                {
                                    class2.ISFpDownFailed = true;
                                    class2.FPStatus = "0";
                                    class2.Boolean_0 = false;
                                    str = "1";
                                }
                                this.class100_0.method_1(class2, Enum10.Update);
                            }
                        }
                    }
                    return str;
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("（下载线程）更改发票信息失败！" + exception.ToString());
                }
            }
            return str;
        }

        public XmlDocument method_16()
        {
            XmlDocument document = new XmlDocument();
            new SQXXLIST();
            try
            {
                document.AppendChild(document.CreateXmlDeclaration("1.0", "GBK", ""));
                XmlNode newChild = document.CreateNode(XmlNodeType.Element, "FPXT", "");
                document.AppendChild(newChild);
                XmlNode node2 = document.CreateNode(XmlNodeType.Element, "INPUT", "");
                newChild.AppendChild(node2);
                XmlElement element = document.CreateElement("NSRSBH");
                element.InnerText = this.string_2;
                node2.AppendChild(element);
                XmlElement element2 = document.CreateElement("KPJH");
                element2.InnerText = this.string_1;
                node2.AppendChild(element2);
                XmlElement element3 = document.CreateElement("SBBH");
                element3.InnerText = this.string_0;
                node2.AppendChild(element3);
                XmlElement element4 = document.CreateElement("FXSH");
                element4.InnerText = this.string_3;
                node2.AppendChild(element4);
                XmlElement element5 = document.CreateElement("HSSH");
                element5.InnerText = this.taxCard_0.GetHashTaxCode();
                node2.AppendChild(element5);
                XmlElement element6 = document.CreateElement("SQXX");
                element6.InnerText = this.class83_0.method_1();
                node2.AppendChild(element6);
                if (Class87.bool_5)
                {
                    Class92 class2 = new Class92();
                    XmlElement element7 = document.CreateElement("SPBMBBH");
                    element7.InnerText = class2.method_0();
                    node2.AppendChild(element7);
                    XmlElement element8 = document.CreateElement("SPBM");
                    node2.AppendChild(element8);
                }
            }
            catch (Exception)
            {
            }
            return document;
        }

        public string method_17(XmlDocument xmlDocument_0)
        {
            string innerText = string.Empty;
            if (xmlDocument_0 != null)
            {
                try
                {
                    XmlNodeList elementsByTagName = xmlDocument_0.GetElementsByTagName("SLXLH");
                    if ((elementsByTagName != null) && (elementsByTagName.Count >= 1))
                    {
                        innerText = elementsByTagName[0].InnerText;
                    }
                    else
                    {
                        return innerText;
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("(下载线程)获取受理序列号异常：" + exception.ToString());
                }
            }
            return innerText;
        }

        public void method_18(XmlDocument xmlDocument_0)
        {
            if (xmlDocument_0 == null)
            {
                if (Class87.bool_3)
                {
                    Class87.dictionary_0["UpdateBZ"] = "1";
                    Class87.dictionary_0["Message"] = "服务器端异常，没有返回信息！";
                }
                Class95.bool_0 = true;
                Class95.string_0 = "更新企业参数失败：服务器端没有返回信息！";
                Class95.bool_1 = true;
                Class95.string_1 = Class95.string_2 + "同步失败：服务器端没有返回信息！";
            }
            try
            {
                XmlNode node = xmlDocument_0.GetElementsByTagName("CODE")[0];
                XmlNode node2 = xmlDocument_0.GetElementsByTagName("MESS")[0];
                if ((node == null) || (node.InnerText != "0000"))
                {
                    if (!Class87.bool_3)
                    {
                        goto Label_02E9;
                    }
                    Class87.dictionary_0["UpdateBZ"] = "1";
                    Class87.dictionary_0["Message"] = "服务端异常，错误码：" + node.InnerText + "  错误描述：" + node2.InnerText;
                }
                XmlNodeList childNodes = xmlDocument_0.GetElementsByTagName("DATA")[0].ChildNodes;
                if ((childNodes == null) || (childNodes.Count < 1))
                {
                    if (!Class87.bool_3)
                    {
                        goto Label_02BD;
                    }
                    Class87.dictionary_0["UpdateBZ"] = "1";
                    Class87.dictionary_0["Message"] = "服务端异常，返回节点不全！";
                }
                string innerText = string.Empty;
                string str2 = string.Empty;
                foreach (XmlNode node3 in childNodes)
                {
                    if (string.Equals(node3.Name, "SQXX", StringComparison.CurrentCultureIgnoreCase) && !string.IsNullOrEmpty(node3.InnerText))
                    {
                        innerText = node3.InnerText;
                    }
                    else if (string.Equals(node3.Name, "SCCS", StringComparison.CurrentCultureIgnoreCase) && !string.IsNullOrEmpty(node3.InnerText))
                    {
                        str2 = node3.InnerText;
                    }
                    else if ((Class87.bool_5 && string.Equals(node3.Name, "SPBMXX", StringComparison.CurrentCultureIgnoreCase)) && !string.IsNullOrEmpty(node3.InnerText))
                    {
                        if (node3.InnerText.Trim() == "")
                        {
                            Class95.bool_1 = false;
                            Class95.string_1 = "没有需要同步的" + Class95.string_2 + "信息。";
                        }
                        else
                        {
                            this.method_32(node3.InnerText, true);
                        }
                    }
                }
                bool flag = this.class83_0.method_2(innerText);
                if (!Class87.bool_3)
                {
                    MessageHelper.MsgWait();
                }
                if (flag)
                {
                    this.class83_0.method_3(str2);
                    if (!string.IsNullOrEmpty(innerText) && !Class87.bool_3)
                    {
                        TaxCard card = TaxCard.CreateInstance(CTaxCardType.tctAddedTax);
                        if ((card != null) && (card.SubSoftVersion == "Linux"))
                        {
                            Class86.bool_0 = true;
                            if (Class86.smethod_9() == DialogResult.OK)
                            {
                                Environment.Exit(0);
                            }
                        }
                    }
                }
                return;
            Label_02BD:
                Class95.bool_0 = true;
                Class95.string_0 = "更新企业参数失败：服务端返回节点不全！";
                Class95.bool_1 = true;
                Class95.string_1 = Class95.string_2 + "同步失败：服务端返回节点不全！";
                return;
            Label_02E9:
                MessageHelper.MsgWait();
                Class95.bool_0 = true;
                Class95.string_0 = "更新企业参数失败：" + node2.InnerText;
                Class95.bool_1 = true;
                Class95.string_1 = Class95.string_2 + "同步失败：" + node2.InnerText;
            }
            catch (Exception exception)
            {
                Class101.smethod_1("更新企业参数信息异常！" + exception.ToString());
                Class95.bool_1 = true;
                Class95.string_0 = "更新企业参数信息异常！" + exception.ToString();
            }
        }

        public void method_19(XmlDocument xmlDocument_0, string string_4, int int_0)
        {
            if (xmlDocument_0 != null)
            {
                try
                {
                    XmlNodeList childNodes = xmlDocument_0.GetElementsByTagName("INPUT")[0].ChildNodes;
                    if (childNodes != null)
                    {
                        new Class81();
                        foreach (XmlNode node in childNodes)
                        {
                            Dictionary<string, object> item = new Dictionary<string, object>();
                            if (string.Equals(node.Name, "FP", StringComparison.CurrentCultureIgnoreCase))
                            {
                                XmlNodeList list2 = node.ChildNodes;
                                if (list2 != null)
                                {
                                    IEnumerator enumerator2 = list2.GetEnumerator();
                                    {
                                        while (enumerator2.MoveNext())
                                        {
                                            XmlNode current = (XmlNode) enumerator2.Current;
                                            if (string.Equals(current.Name, "FPHM", StringComparison.CurrentCultureIgnoreCase))
                                            {
                                                int result = 0;
                                                if (int.TryParse(current.InnerText, out result))
                                                {
                                                    item.Add("FPHM", result);
                                                }
                                            }
                                            if (string.Equals(current.Name, "FPZL", StringComparison.CurrentCultureIgnoreCase))
                                            {
                                                if (current.InnerText.Equals("0"))
                                                {
                                                    item.Add("FPZL", "s");
                                                }
                                                else if (current.InnerText.Equals("2"))
                                                {
                                                    item.Add("FPZL", "c");
                                                }
                                                else
                                                {
                                                    if (!current.InnerText.Equals("026"))
                                                    {
                                                        int num3 = 0x33;
                                                        if (!current.InnerText.Equals(num3.ToString()))
                                                        {
                                                            if (!current.InnerText.Equals("025"))
                                                            {
                                                                int num = 0x29;
                                                                if (!current.InnerText.Equals(num.ToString()))
                                                                {
                                                                    goto Label_01A1;
                                                                }
                                                            }
                                                            item.Add("FPZL", "q");
                                                            goto Label_01A1;
                                                        }
                                                    }
                                                    item.Add("FPZL", "p");
                                                }
                                            }
                                        Label_01A1:
                                            if (string.Equals(current.Name, "FPDM", StringComparison.CurrentCultureIgnoreCase))
                                            {
                                                item.Add("FPDM", current.InnerText);
                                            }
                                            if (item.Count >= 3)
                                            {
                                                goto Label_01DE;
                                            }
                                        }
                                        continue;
                                    Label_01DE:
                                        item.Add("BSZT", int_0);
                                        item.Add("BSRZ", string_4);
                                        Class87.list_0.Add(item);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("(上传线程)更新发票上传异常信息失败！" + exception.ToString());
                }
            }
        }

        private XmlDocument method_2(List<Fpxx> fpInfo, FPLX fplx_0)
        {
            if ((fpInfo == null) || (fpInfo.Count < 1))
            {
                return null;
            }
            XmlDocument document = new XmlDocument();
            try
            {
                document.AppendChild(document.CreateXmlDeclaration("1.0", "GBK", ""));
                XmlNode newChild = document.CreateNode(XmlNodeType.Element, "business", "");
                XmlElement element = (XmlElement) newChild;
                element.SetAttribute("id", "HX_FPMXSC");
                element.SetAttribute("comment", "发票明细上传");
                if (Class87.bool_5)
                {
                    element.SetAttribute("version", "2.0");
                }
                else
                {
                    element.SetAttribute("version", "1.0");
                }
                document.AppendChild(newChild);
                XmlNode node2 = document.CreateNode(XmlNodeType.Element, "body", "");
                element = (XmlElement) node2;
                element.SetAttribute("count", "1");
                element.SetAttribute("skph", this.string_0);
                element.SetAttribute("nsrsbh", this.string_2);
                newChild.AppendChild(node2);
                XmlNode node3 = document.CreateNode(XmlNodeType.Element, "group", "");
                ((XmlElement) node3).SetAttribute("xh", "1");
                node2.AppendChild(node3);
                string str = "";
                if (fplx_0 == FPLX.HYFP)
                {
                    str = "009";
                }
                else if (fplx_0 == FPLX.JDCFP)
                {
                    str = "005";
                }
                XmlElement element2 = document.CreateElement("data");
                element2.SetAttribute("name", "fplx_dm");
                element2.SetAttribute("value", str);
                node3.AppendChild(element2);
                XmlDocument document2 = new XmlDocument();
                if (fplx_0 == FPLX.HYFP)
                {
                    document2 = this.method_6(fpInfo);
                }
                else if (fplx_0 == FPLX.JDCFP)
                {
                    document2 = this.method_7(fpInfo);
                }
                Class103.smethod_0(document2, "HYMXToServer");
                string str2 = "";
                byte[] bytes = ToolUtil.GetBytes(document2.InnerXml.ToString());
                if ((bytes != null) && (bytes.Length > 0))
                {
                    str2 = Convert.ToBase64String(bytes);
                }
                XmlElement element3 = document.CreateElement("data");
                element3.SetAttribute("name", "fpmx");
                element3.SetAttribute("value", str2);
                node3.AppendChild(element3);
            }
            catch (Exception exception)
            {
                Class101.smethod_1("组合上传所需的货运机动车发票失败！" + exception.ToString());
            }
            return document;
        }

        public void method_20(XmlDocument xmlDocument_0, string string_4, int int_0)
        {
            if (xmlDocument_0 != null)
            {
                try
                {
                    byte[] buffer = Convert.FromBase64String(xmlDocument_0.GetElementsByTagName("fpmx")[0].InnerText);
                    XmlDocument document = new XmlDocument();
                    document.LoadXml(ToolUtil.GetString(buffer));
                    XmlNodeList childNodes = document.GetElementsByTagName("INPUT")[0].ChildNodes;
                    if (childNodes != null)
                    {
                        new Class81();
                        foreach (XmlNode node in childNodes)
                        {
                            Dictionary<string, object> item = new Dictionary<string, object>();
                            if (string.Equals(node.Name, "FP", StringComparison.CurrentCultureIgnoreCase))
                            {
                                XmlNodeList list3 = node.ChildNodes;
                                if (list3 != null)
                                {
                                    IEnumerator enumerator2 = list3.GetEnumerator();
                                    {
                                        while (enumerator2.MoveNext())
                                        {
                                            XmlNode current = (XmlNode) enumerator2.Current;
                                            if (string.Equals(current.Name, "FPHM", StringComparison.CurrentCultureIgnoreCase))
                                            {
                                                int result = 0;
                                                if (int.TryParse(current.InnerText, out result))
                                                {
                                                    item.Add("FPHM", result);
                                                }
                                            }
                                            if (string.Equals(current.Name, "FPZL", StringComparison.CurrentCultureIgnoreCase))
                                            {
                                                if (current.InnerText.Equals("0"))
                                                {
                                                    item.Add("FPZL", "s");
                                                }
                                                else if (current.InnerText.Equals("2"))
                                                {
                                                    item.Add("FPZL", "c");
                                                }
                                                else
                                                {
                                                    if (!current.InnerText.Equals("026"))
                                                    {
                                                        int num2 = 0x33;
                                                        if (!current.InnerText.Equals(num2.ToString()))
                                                        {
                                                            if (!current.InnerText.Equals("025"))
                                                            {
                                                                int num = 0x29;
                                                                if (!current.InnerText.Equals(num.ToString()))
                                                                {
                                                                    goto Label_01DC;
                                                                }
                                                            }
                                                            item.Add("FPZL", "q");
                                                            goto Label_01DC;
                                                        }
                                                    }
                                                    item.Add("FPZL", "p");
                                                }
                                            }
                                        Label_01DC:
                                            if (string.Equals(current.Name, "FPDM", StringComparison.CurrentCultureIgnoreCase))
                                            {
                                                item.Add("FPDM", current.InnerText);
                                            }
                                            if (item.Count >= 3)
                                            {
                                                goto Label_021B;
                                            }
                                        }
                                        continue;
                                    Label_021B:
                                        item.Add("BSZT", int_0);
                                        item.Add("BSRZ", string_4);
                                        Class87.list_0.Add(item);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("(上传线程)更新发票上传异常信息失败！" + exception.ToString());
                }
            }
        }

        public void method_21(XmlDocument xmlDocument_0, string string_4, string string_5, int int_0)
        {
            if (xmlDocument_0 != null)
            {
                try
                {
                    XmlNodeList elementsByTagName = xmlDocument_0.GetElementsByTagName("data");
                    string str = string.Empty;
                    int num = 0;
                    while (num < elementsByTagName.Count)
                    {
                        if (elementsByTagName[num].Attributes["name"].Value.Equals("fpmx", StringComparison.CurrentCultureIgnoreCase))
                        {
                            goto Label_0051;
                        }
                        num++;
                    }
                    goto Label_0085;
                Label_0051:
                    if (elementsByTagName[num].Attributes["value"] != null)
                    {
                        str = elementsByTagName[num].Attributes["value"].Value;
                    }
                Label_0085:
                    if (!string.IsNullOrEmpty(str))
                    {
                        byte[] buffer = Convert.FromBase64String(str);
                        if ((buffer == null) || (buffer.Length < 1))
                        {
                            return;
                        }
                        string xml = ToolUtil.GetString(buffer);
                        XmlDocument document = new XmlDocument();
                        document.LoadXml(xml);
                        XmlNodeList list2 = document.GetElementsByTagName("mx");
                        if ((list2 == null) || (list2.Count < 1))
                        {
                            return;
                        }
                        for (int i = 0; i < list2.Count; i++)
                        {
                            Dictionary<string, object> dictionary;
                            XmlNodeList childNodes = list2[i].ChildNodes;
                            if ((childNodes != null) && (childNodes.Count >= 1))
                            {
                                dictionary = new Dictionary<string, object>();
                                for (int j = 0; j < childNodes.Count; j++)
                                {
                                    if (childNodes[j].Attributes["name"].Value.Equals("fpdm", StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        dictionary.Add("FPDM", childNodes[j].InnerText);
                                    }
                                    if (childNodes[j].Attributes["name"].Value.Equals("fphm", StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        int result = 0;
                                        if (int.TryParse(childNodes[j].InnerText, out result))
                                        {
                                            dictionary.Add("FPHM", result);
                                        }
                                    }
                                    if (dictionary.Count >= 2)
                                    {
                                        goto Label_01D8;
                                    }
                                }
                            }
                            continue;
                        Label_01D8:
                            dictionary.Add("BSZT", int_0);
                            dictionary.Add("FPZL", string_4);
                            dictionary.Add("BSRZ", string_5);
                            Class87.list_0.Add(dictionary);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("货运机动车发票上传回馈信息写临时表失败！" + exception.ToString());
                }
            }
        }

        public void method_22(XmlDocument xmlDocument_0, string string_4, int int_0)
        {
            if (xmlDocument_0 == null)
            {
                Class101.smethod_0("未从局端获取信息，重置发票状态：发送局端数据为null");
            }
            else
            {
                try
                {
                    string str = this.method_17(xmlDocument_0);
                    if (string.IsNullOrEmpty(str))
                    {
                        Class101.smethod_0("未从局端获取信息，重置发票状态：未查询到发送给局端手里序列号");
                    }
                    else
                    {
                        DataRow[] rowArray = this.class100_0.method_11(str);
                        if ((rowArray != null) && (rowArray.Length >= 1))
                        {
                            Class101.smethod_0(string.Concat(new object[] { "未从局端获取信息，重置发票状态：内存临时表查询到发票张数：", rowArray.Length, "   发票受理序列号：", str }));
                            foreach (DataRow row in rowArray)
                            {
                                Dictionary<string, object> item = new Dictionary<string, object>();
                                item.Add("FPDM", row["FPDM"]);
                                int result = 0;
                                if (int.TryParse(row["FPNO"].ToString(), out result))
                                {
                                    item.Add("FPHM", result);
                                }
                                if (row["Fplx"].ToString().Equals(FPLX.PTFP.ToString()))
                                {
                                    item.Add("FPZL", "c");
                                }
                                else if (row["Fplx"].ToString().Equals(FPLX.ZYFP.ToString()))
                                {
                                    item.Add("FPZL", "s");
                                }
                                else if (row["Fplx"].ToString().Equals(FPLX.HYFP.ToString()))
                                {
                                    item.Add("FPZL", "f");
                                }
                                else if (row["Fplx"].ToString().Equals(FPLX.JDCFP.ToString()))
                                {
                                    item.Add("FPZL", "j");
                                }
                                else if (row["Fplx"].ToString().Equals(FPLX.DZFP.ToString()))
                                {
                                    item.Add("FPZL", "p");
                                }
                                else if (row["Fplx"].ToString().Equals(FPLX.JSFP.ToString()))
                                {
                                    item.Add("FPZL", "q");
                                }
                                else
                                {
                                    item.Add("FPZL", row["Fplx"]);
                                }
                                item.Add("BSRZ", string_4);
                                item.Add("BSZT", int_0);
                                if (item.Count >= 4)
                                {
                                    Class87.list_1.Add(item);
                                }
                            }
                        }
                        else
                        {
                            Class101.smethod_0("未从局端获取信息，重置发票状态：内存表未查询到相应发票信息   发票受理序列号：" + str);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("(下载线程)组合异常信息异常：" + exception.ToString());
                }
            }
        }

        public XmlDocument method_23(List<string> slxlhs, List<Dictionary<string, string>> listFP)
        {
            XmlDocument document = new XmlDocument();
            new Class100();
            Class81 class2 = new Class81();
            try
            {
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", "");
                document.AppendChild(newChild);
                XmlNode node = document.CreateNode(XmlNodeType.Element, "PLSC", "");
                document.AppendChild(node);
                XmlNode node2 = document.CreateNode(XmlNodeType.Element, "CODE", "");
                node2.InnerText = "0000";
                node.AppendChild(node2);
                XmlNode node3 = document.CreateNode(XmlNodeType.Element, "MESS", "");
                node3.InnerText = "处理成功";
                node.AppendChild(node3);
                XmlNode node4 = document.CreateNode(XmlNodeType.Element, "DATA", "");
                node.AppendChild(node4);
                XmlNode node5 = document.CreateNode(XmlNodeType.Element, "FP_SUCC", "");
                node4.AppendChild(node5);
                XmlNode node6 = document.CreateNode(XmlNodeType.Element, "FP_ERR", "");
                node4.AppendChild(node6);
                foreach (string str in slxlhs)
                {
                    XmlNode node7 = document.CreateElement("SLXLH");
                    node7.InnerText = str;
                    node5.AppendChild(node7);
                }
                foreach (Dictionary<string, string> dictionary in listFP)
                {
                    string str2 = class2.method_18(dictionary["FPDM"], dictionary["FPHM"]);
                    XmlNode node8 = document.CreateNode(XmlNodeType.Element, "FP", "");
                    node6.AppendChild(node8);
                    XmlNode node9 = document.CreateElement("FPDM");
                    node9.InnerText = dictionary["FPDM"];
                    node8.AppendChild(node9);
                    XmlNode node10 = document.CreateElement("FPHM");
                    node10.InnerText = dictionary["FPHM"];
                    node8.AppendChild(node10);
                    XmlNode node11 = document.CreateElement("CODE");
                    node11.InnerText = "-0001";
                    node8.AppendChild(node11);
                    XmlNode node12 = document.CreateElement("MESS");
                    if (Class87.bool_4)
                    {
                        node12.InnerText = dictionary["BSRZ"];
                    }
                    else if (string.IsNullOrEmpty(str2))
                    {
                        node12.InnerText = (str2 == null) ? "发票号码代码不存在。" : "发票报送失败。";
                    }
                    else
                    {
                        node12.InnerText = str2;
                    }
                    node8.AppendChild(node12);
                }
                Class101.smethod_0("发票上传-批量接口获取发票上传结果：" + document.InnerXml);
            }
            catch (Exception exception)
            {
                Class101.smethod_1("发票上传-批量接口获取发票上传信息异常：" + exception.ToString());
            }
            return document;
        }

        public XmlDocument method_24()
        {
            XmlDocument document = new XmlDocument();
            try
            {
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", "");
                document.AppendChild(newChild);
                XmlNode node = document.CreateNode(XmlNodeType.Element, "PLSC", "");
                document.AppendChild(node);
                XmlNode node2 = document.CreateNode(XmlNodeType.Element, "CODE", "");
                node2.InnerText = "-0001";
                document.AppendChild(node2);
                XmlNode node3 = document.CreateNode(XmlNodeType.Element, "MESS", "");
                node3.InnerText = "无发票信息";
                document.AppendChild(node3);
                XmlNode node4 = document.CreateNode(XmlNodeType.Element, "DATA", "");
                document.AppendChild(node4);
                XmlNode node5 = document.CreateNode(XmlNodeType.Element, " FP_SUCC", "");
                node4.AppendChild(node5);
                XmlNode node6 = document.CreateNode(XmlNodeType.Element, " FP_ERR", "");
                node4.AppendChild(node6);
            }
            catch (Exception exception)
            {
                Class101.smethod_1("发票上传-批量接口获取发票上传信息异常：" + exception.ToString());
            }
            return document;
        }

        public XmlDocument method_25(XmlDocument xmlDocument_0)
        {
            XmlDocument document = xmlDocument_0;
            try
            {
                XmlNodeList elementsByTagName = document.GetElementsByTagName("FLAG");
                if ((elementsByTagName != null) && (elementsByTagName.Count > 0))
                {
                    XmlNode oldChild = elementsByTagName[0];
                    oldChild.ParentNode.RemoveChild(oldChild);
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_1("发票下载-批量接口：去除密文包异常：" + exception.ToString());
            }
            return document;
        }

        public XmlDocument method_26(int int_0)
        {
            XmlDocument document = new XmlDocument();
            try
            {
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", "");
                document.AppendChild(newChild);
                XmlNode node = document.CreateNode(XmlNodeType.Element, "FPXT", "");
                document.AppendChild(node);
                XmlNode node2 = document.CreateNode(XmlNodeType.Element, "OUTPUT", "");
                node.AppendChild(node2);
                XmlNode node3 = document.CreateNode(XmlNodeType.Element, "CODE", "");
                node3.InnerText = "-0001";
                node2.AppendChild(node3);
                XmlNode node4 = document.CreateNode(XmlNodeType.Element, "MESS", "");
                if (int_0 == 0)
                {
                    node4.InnerText = "无受理序列号信息";
                }
                else if (!Class87.bool_1)
                {
                    node4.InnerText = Class87.string_2;
                }
                node2.AppendChild(node4);
                XmlNode node5 = document.CreateNode(XmlNodeType.Element, "DATA", "");
                node2.AppendChild(node5);
            }
            catch (Exception exception)
            {
                Class101.smethod_1("发票下载-批量接口：组装错误信息异常：" + exception.ToString());
            }
            return document;
        }

        private string method_27(string string_4, int int_0)
        {
            string str = string_4;
            if (string.IsNullOrEmpty(string_4))
            {
                return string_4;
            }
            try
            {
                str = decimal.Round(decimal.Parse(string_4), int_0, MidpointRounding.AwayFromZero).ToString("f" + int_0);
            }
            catch (Exception exception)
            {
                Class101.smethod_1("上传线程-格式化数据出错：" + exception.ToString());
            }
            return str;
        }

        public string method_28(XmlDocument xmlDocument_0)
        {
            string str = "s";
            if (xmlDocument_0 == null)
            {
                Class101.smethod_0("从受理序列号获取票种，传入参数为null！");
                return str;
            }
            try
            {
                XmlNodeList elementsByTagName = xmlDocument_0.GetElementsByTagName("SLXLH");
                if ((elementsByTagName != null) && (elementsByTagName.Count >= 1))
                {
                    str = this.class81_0.method_19(elementsByTagName[0].InnerText);
                }
                else
                {
                    Class101.smethod_0("从受理序列号获取票种，未查询到受理序列号！");
                    return str;
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_1("从受理序列号获取票种异常：" + exception.ToString());
            }
            return str;
        }

        public void method_29(string string_4, string string_5)
        {
            if ((string_4.Trim() != "") && (string_5 != ""))
            {
                Class101.smethod_0("导出文件");
                try
                {
                    List<Fpxx> list = new GetFpInfoDal().GetFpInfo("1", "50", UpdateTransMethod.BszAndWbs, string_5, string_4, "p", "");
                    if ((list != null) && (list.Count >= 1))
                    {
                        Class101.smethod_0(string.Concat(new object[] { "开始导出文件：fpdm：", string_4, "   fphm:", string_5, "  count:", list.Count }));
                        XmlDocument document = new XmlDocument();
                        XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "gbk", "");
                        document.AppendChild(newChild);
                        Class101.smethod_0("导出文件0");
                        XmlNode node = document.CreateNode(XmlNodeType.Element, "business", "");
                        document.AppendChild(node);
                        XmlNode node2 = document.CreateNode(XmlNodeType.Element, "REQUEST_COMMON_FPKJ", "");
                        node.AppendChild(node2);
                        XmlNode node3 = document.CreateNode(XmlNodeType.Element, "COMMON_FPKJ_FPT", "");
                        node2.AppendChild(node3);
                        Class101.smethod_0("导出文件1");
                        XmlElement element = document.CreateElement("XSF_NSRSBH");
                        if (!list[0].xfsh.Equals("000000000000000"))
                        {
                            Class101.smethod_0("导出文件:fpdm:" + string_4 + "    fphm:" + string_5 + "   xfsh:" + list[0].xfsh);
                            element.InnerText = list[0].xfsh;
                        }
                        node3.AppendChild(element);
                        Class101.smethod_0("导出文件jiezhi");
                        XmlElement element2 = document.CreateElement("XSF_MC");
                        element2.InnerText = list[0].xfmc;
                        node3.AppendChild(element2);
                        XmlElement element3 = document.CreateElement("XSF_DZDH");
                        element3.InnerText = list[0].xfdzdh;
                        node3.AppendChild(element3);
                        XmlElement element4 = document.CreateElement("XSF_YHZH");
                        element4.InnerText = list[0].xfyhzh;
                        node3.AppendChild(element4);
                        XmlElement element5 = document.CreateElement("GMF_NSRSBH");
                        if (!list[0].gfsh.Equals("000000000000000"))
                        {
                            Class101.smethod_0("导出文件:fpdm:" + string_4 + "    fphm:" + string_5 + "   gfsh:" + list[0].gfsh);
                            element5.InnerText = list[0].gfsh;
                        }
                        node3.AppendChild(element5);
                        XmlElement element6 = document.CreateElement("GMF_MC");
                        element6.InnerText = list[0].gfmc;
                        node3.AppendChild(element6);
                        XmlElement element7 = document.CreateElement("GMF_DZDH");
                        element7.InnerText = list[0].gfdzdh;
                        node3.AppendChild(element7);
                        XmlElement element8 = document.CreateElement("GMF_YHZH");
                        element8.InnerText = list[0].gfyhzh;
                        node3.AppendChild(element8);
                        XmlElement element9 = document.CreateElement("KPR");
                        element9.InnerText = list[0].kpr;
                        node3.AppendChild(element9);
                        XmlElement element10 = document.CreateElement("SKR");
                        element10.InnerText = list[0].skr;
                        node3.AppendChild(element10);
                        XmlElement element11 = document.CreateElement("FHR");
                        element11.InnerText = list[0].fhr;
                        node3.AppendChild(element11);
                        Class101.smethod_0("导出文件2");
                        double result = 0.0;
                        double.TryParse(list[0].je, out result);
                        double num2 = 0.0;
                        double.TryParse(list[0].se, out num2);
                        XmlElement element12 = document.CreateElement("JSHJ");
                        element12.InnerText = this.method_27((result + num2).ToString(), 2);
                        node3.AppendChild(element12);
                        Class101.smethod_0("导出文件3");
                        XmlElement element13 = document.CreateElement("HJJE");
                        element13.InnerText = this.method_27(list[0].je, 2);
                        node3.AppendChild(element13);
                        XmlElement element14 = document.CreateElement("HJSE");
                        element14.InnerText = this.method_27(list[0].se, 2);
                        node3.AppendChild(element14);
                        XmlElement element15 = document.CreateElement("BZ");
                        string str = ToolUtil.GetString(ToolUtil.FromBase64String(list[0].bz));
                        element15.InnerText = str;
                        node3.AppendChild(element15);
                        XmlElement element16 = document.CreateElement("FPZT");
                        element16.InnerText = "0";
                        if (list[0].zfbz)
                        {
                            element16.InnerText = "1";
                        }
                        node3.AppendChild(element16);
                        XmlElement element17 = document.CreateElement("JQBH");
                        element17.InnerText = list[0].jqbh;
                        node3.AppendChild(element17);
                        XmlElement element18 = document.CreateElement("FPDM");
                        element18.InnerText = list[0].fpdm;
                        node3.AppendChild(element18);
                        XmlElement element19 = document.CreateElement("FPHM");
                        element19.InnerText = list[0].fphm;
                        node3.AppendChild(element19);
                        Class101.smethod_0("导出文件4");
                        DateTime time = new DateTime();
                        DateTime.TryParse(list[0].kprq, out time);
                        XmlElement element20 = document.CreateElement("KPRQ");
                        element20.InnerText = time.ToString("yyyyMMddHHmmss");
                        node3.AppendChild(element20);
                        XmlElement element21 = document.CreateElement("FPMW");
                        DateTime time2 = new DateTime(0x7dd, 9, 10, 8, 0x22, 30);
                        TimeSpan span = (TimeSpan) (DateTime.Now - time2);
                        byte[] buffer2 = AES_Crypt.Encrypt(ToolUtil.GetBytes(span.TotalSeconds.ToString("F1")), new byte[] { 
                            0xff, 0x42, 0xae, 0x95, 11, 0x51, 0xca, 0x15, 0x21, 140, 0x4f, 170, 220, 0x92, 170, 0xed, 
                            0xfd, 0xeb, 0x4e, 13, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
                         }, new byte[] { 0xf2, 0x1f, 0xac, 0x5b, 0x2c, 0xc0, 0xa9, 0xd0, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 });
                        list[0].Get_Print_Dj(null, 0, buffer2);
                        element21.InnerText = list[0].mw;
                        node3.AppendChild(element21);
                        XmlElement element22 = document.CreateElement("JYM");
                        element22.InnerText = list[0].jym;
                        node3.AppendChild(element22);
                        XmlElement element23 = document.CreateElement("EWM");
                        element23.InnerText = "";
                        node3.AppendChild(element23);
                        Class101.smethod_0("导出文件5");
                        XmlNode node4 = document.CreateNode(XmlNodeType.Element, "COMMON_FPKJ_XMXXS", "");
                        node2.AppendChild(node4);
                        for (int i = 0; i < list[0].Mxxx.Count; i++)
                        {
                            XmlNode node5 = document.CreateNode(XmlNodeType.Element, "COMMON_FPKJ_XMXX", "");
                            node4.AppendChild(node5);
                            string str2 = list[0].Get_Print_Dj(list[0].Mxxx[i], 1, null);
                            element21.InnerText = list[0].mw;
                            XmlElement element24 = document.CreateElement("SPMC");
                            element24.InnerText = list[0].Mxxx[i][SPXX.SPMC];
                            node5.AppendChild(element24);
                            XmlElement element25 = document.CreateElement("GGXH");
                            element25.InnerText = list[0].Mxxx[i][SPXX.GGXH];
                            node5.AppendChild(element25);
                            XmlElement element26 = document.CreateElement("JLDW");
                            element26.InnerText = list[0].Mxxx[i][SPXX.JLDW];
                            node5.AppendChild(element26);
                            XmlElement element27 = document.CreateElement("SL");
                            element27.InnerText = list[0].Mxxx[i][SPXX.SL];
                            node5.AppendChild(element27);
                            XmlElement element28 = document.CreateElement("DJ");
                            element28.InnerText = list[0].Mxxx[i][SPXX.DJ];
                            if (list[0].Mxxx[i][SPXX.HSJBZ].Equals("1"))
                            {
                                element28.InnerText = str2;
                            }
                            node5.AppendChild(element28);
                            XmlElement element29 = document.CreateElement("JE");
                            element29.InnerText = this.method_27(list[0].Mxxx[i][SPXX.JE], 2);
                            node5.AppendChild(element29);
                            XmlElement element30 = document.CreateElement("SLV");
                            element30.InnerText = list[0].Mxxx[i][SPXX.SLV];
                            node5.AppendChild(element30);
                            XmlElement element31 = document.CreateElement("SE");
                            element31.InnerText = this.method_27(list[0].Mxxx[i][SPXX.SE], 2);
                            node5.AppendChild(element31);
                        }
                        Class101.smethod_0("导出文件结束");
                        string str3 = string_4 + "_" + string_5 + "_开票结果_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".XML";
                        Class103.smethod_2(document, str3);
                        string str4 = Class103.smethod_1(str3);
                        if (str4 != "")
                        {
                            try
                            {
                                string destFileName = str4.Replace(".XML", "_" + Convert.ToString((long) Class99.smethod_1(str4), 0x10).ToUpper() + ".XML");
                                File.Move(str4, destFileName);
                            }
                            catch (Exception exception)
                            {
                                Class101.smethod_1("给电子发票验签失败自动生成结果文件名添加CRC值出错：" + exception.ToString());
                                throw exception;
                            }
                        }
                    }
                    else
                    {
                        Class101.smethod_1("导出验签失败作废成功发票(发票代码：" + string_4 + "  发票号码：" + string_5 + ")退出：未查询到指定发票");
                    }
                }
                catch (Exception exception2)
                {
                    Class101.smethod_1("导出验签失败作废成功发票(发票代码：" + string_4 + "  发票号码：" + string_5 + ")异常：" + exception2.ToString());
                }
            }
            else
            {
                Class101.smethod_1("导出验签失败作废成功发票(发票代码：" + string_4 + "  发票号码：" + string_5 + ")退出：发票代码或者号码为空");
            }
        }

        public bool method_3(ref List<XmlDocument> list_0, ref List<XmlDocument> list_1, ref List<XmlDocument> list_2, ref List<XmlDocument> list_3, ref List<XmlDocument> list_4, int int_0, UpdateTransMethod updateTransMethod_0, string string_4, string string_5, string string_6, string string_7)
        {
            List<Fpxx> list = new List<Fpxx>();
            List<Fpxx> list2 = new List<Fpxx>();
            List<Fpxx> list3 = new List<Fpxx>();
            List<Fpxx> list4 = new List<Fpxx>();
            List<Fpxx> list5 = new List<Fpxx>();
            List<Fpxx> fpInfo = new List<Fpxx>();
            List<Fpxx> list7 = new List<Fpxx>();
            List<Fpxx> list8 = new List<Fpxx>();
            List<Fpxx> list9 = new List<Fpxx>();
            List<Fpxx> list10 = new List<Fpxx>();
            List<Fpxx> list11 = new List<Fpxx>();
            if (int_0 >= 1)
            {
                try
                {
                    if (!this.class100_0.method_2(ref list, ref list2, ref list3, ref list4, ref list5, int_0.ToString(), updateTransMethod_0, string_4, string_5, string_6, string_7))
                    {
                        return false;
                    }
                    if ((list != null) && (list.Count > 0))
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            if ((list[i].Qdxx != null) && (list[i].Qdxx.Count >= Class87.int_7))
                            {
                                fpInfo.Add(list[i]);
                            }
                            else
                            {
                                list7.Add(list[i]);
                            }
                            if (fpInfo.Count > 0)
                            {
                                list_0.Add(this.method_0(fpInfo));
                                fpInfo.Clear();
                            }
                            if ((list7.Count > 0) && ((((list7.Count % Class87.int_2) == 0) || (list7.Count >= (list.Count - 1))) || (i >= (list.Count - 1))))
                            {
                                list_0.Add(this.method_0(list7));
                                list7.Clear();
                            }
                        }
                    }
                    if ((list2 != null) && (list2.Count > 0))
                    {
                        for (int j = 0; j < list2.Count; j++)
                        {
                            list8.Add(list2[j]);
                            if (((j != 0) && (((j + 1) % Class87.int_3) == 0)) || (j >= (list2.Count - 1)))
                            {
                                list_1.Add(this.method_2(list8, FPLX.HYFP));
                                list8.Clear();
                            }
                        }
                    }
                    if ((list3 != null) && (list3.Count > 0))
                    {
                        for (int k = 0; k < list3.Count; k++)
                        {
                            list9.Add(list3[k]);
                            if (((k != 0) && (((k + 1) % Class87.int_3) == 0)) || (k >= (list3.Count - 1)))
                            {
                                list_2.Add(this.method_2(list9, FPLX.JDCFP));
                                list9.Clear();
                            }
                        }
                    }
                    if ((list4 != null) && (list4.Count > 0))
                    {
                        for (int m = 0; m < list4.Count; m++)
                        {
                            list10.Add(list4[m]);
                            if (((m != 0) && (((m + 1) % Class87.int_2) == 0)) || (m >= (list4.Count - 1)))
                            {
                                list_3.Add(this.method_0(list10));
                                list10.Clear();
                            }
                        }
                    }
                    if ((list5 != null) && (list5.Count > 0))
                    {
                        for (int n = 0; n < list5.Count; n++)
                        {
                            list11.Add(list5[n]);
                            if (((n != 0) && (((n + 1) % Class87.int_2) == 0)) || (n >= (list5.Count - 1)))
                            {
                                list_4.Add(this.method_1(list11));
                                list11.Clear();
                            }
                        }
                    }
                    return true;
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("(上传线程)获取发票信息异常：" + exception.ToString());
                }
            }
            return false;
        }

        public string method_30(List<Dictionary<string, string>> list)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", "");
            document.AppendChild(newChild);
            XmlNode node = document.CreateNode(XmlNodeType.Element, "FPS", "");
            document.AppendChild(node);
            try
            {
                foreach (Dictionary<string, string> dictionary in list)
                {
                    XmlNode node2 = document.CreateNode(XmlNodeType.Element, "FP", "");
                    node.AppendChild(node2);
                    XmlElement element = document.CreateElement("FPDM");
                    element.InnerText = dictionary["FPDM"];
                    node2.AppendChild(element);
                    XmlElement element2 = document.CreateElement("FPHM");
                    element2.InnerText = dictionary["FPHM"];
                    node2.AppendChild(element2);
                    XmlElement element3 = document.CreateElement("FPZL");
                    element3.InnerText = dictionary["FPZL"];
                    node2.AppendChild(element3);
                    XmlElement element4 = document.CreateElement("BSZT");
                    element4.InnerText = dictionary["BSZT"];
                    node2.AppendChild(element4);
                    XmlElement element5 = document.CreateElement("BSRZ");
                    element5.InnerText = dictionary["BSRZ"];
                    node2.AppendChild(element5);
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_1("getFPSToLinuxServer异常：" + exception.ToString());
            }
            return document.InnerXml;
        }

        public string method_31()
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", "");
            document.AppendChild(newChild);
            XmlNode node = document.CreateNode(XmlNodeType.Element, "FPXT", "");
            document.AppendChild(node);
            XmlNode node2 = document.CreateNode(XmlNodeType.Element, "INPUT", "");
            node.AppendChild(node2);
            XmlElement element = document.CreateElement("NSRSBH");
            element.InnerText = this.string_2;
            node2.AppendChild(element);
            Class92 class2 = new Class92();
            XmlElement element2 = document.CreateElement("SPBMBBH");
            element2.InnerText = class2.method_0();
            node2.AppendChild(element2);
            XmlElement element3 = document.CreateElement("SPBM");
            node2.AppendChild(element3);
            return document.InnerXml;
        }

        public void method_32(string string_4, bool bool_0)
        {
            if (((string_4 == null) || (string_4 == "")) || (string_4.ToLower() == "null"))
            {
                if (!bool_0)
                {
                    MessageHelper.MsgWait();
                    Class86.smethod_7(Class95.string_2 + "更新失败，请选择手动更新或去税局下载更新包导入更新：局端返回数据为空");
                }
                Class95.bool_1 = true;
                Class95.string_1 = Class95.string_2 + "更新失败，请选择手动更新或去税局下载更新包导入更新：局端返回数据为空。";
                Class101.smethod_0("解析同步数据传入参数为空或者null");
            }
            else
            {
                try
                {
                    Class101.smethod_0("商品分类同步:局端返回数据原始报文：" + string_4);
                    string innerText = "";
                    if (bool_0)
                    {
                        innerText = string_4;
                    }
                    else
                    {
                        XmlDocument document = new XmlDocument();
                        document.LoadXml(string_4);
                        XmlNodeList list = document.GetElementsByTagName("DATA");
                        if (((list == null) || (list.Count < 1)) || (list[0].InnerText == ""))
                        {
                            goto Label_0807;
                        }
                        innerText = list[0].InnerText;
                    }
                    string xml = ToolUtil.GetString(ZipUtil.UnCompress(ToolUtil.FromBase64String(innerText)));
                    Class101.smethod_0("商品分类同步:局端返回数据处理后报文：" + xml);
                    XmlDocument document2 = new XmlDocument();
                    document2.LoadXml(xml);
                    XmlNodeList elementsByTagName = document2.GetElementsByTagName("SPBMBBH");
                    if ((elementsByTagName != null) && (elementsByTagName.Count >= 1))
                    {
                        string str3 = elementsByTagName[0].InnerText;
                        List<string> sqlID = new List<string>();
                        List<Dictionary<string, object>> listSPFL = new List<Dictionary<string, object>>();
                        XmlNodeList list5 = document2.GetElementsByTagName("SPBMXX");
                        if ((list5 != null) && (list5.Count >= 1))
                        {
                            Dictionary<string, object> dictionary;
                            Class92 class2 = new Class92();
                            bool flag = class2.method_4() <= 0;
                            List<string> spbm = new List<string>();
                            foreach (XmlNode node in list5)
                            {
                                dictionary = new Dictionary<string, object>();
                                XmlNodeList childNodes = node.ChildNodes;
                                if (childNodes != null)
                                {
                                    dictionary.Add("BMB_BBH", str3);
                                    dictionary.Add("WJ", "");
                                    foreach (XmlNode node2 in childNodes)
                                    {
                                        if (node2.InnerText.IndexOf("![CDATA[") > -1)
                                        {
                                            node2.InnerText = this.method_41(node2);
                                        }
                                        switch (node2.Name)
                                        {
                                            case "SPBM":
                                            {
                                                dictionary.Add("HBBM", node2.InnerText);
                                                string str5 = this.method_35(node2.InnerText);
                                                dictionary.Add("BM", str5);
                                                dictionary.Add("SJBM", this.method_36(str5));
                                                spbm.Add(str5);
                                                break;
                                            }
                                            case "SPMC":
                                                dictionary.Add("MC", node2.InnerText);
                                                break;

                                            case "SM":
                                                dictionary.Add("SM", node2.InnerText);
                                                break;

                                            case "ZZSSL":
                                                dictionary.Add("SLV", node2.InnerText);
                                                break;

                                            case "GJZ":
                                                dictionary.Add("GJZ", node2.InnerText);
                                                break;

                                            case "HZX":
                                                dictionary.Add("HZX", node2.InnerText);
                                                break;

                                            case "BBH":
                                                dictionary.Add("BBH", node2.InnerText);
                                                break;

                                            case "KYZT":
                                                dictionary.Add("KYZT", node2.InnerText);
                                                break;

                                            case "ZZSTSGL":
                                                dictionary.Add("ZZSTSGL", node2.InnerText);
                                                break;

                                            case "ZZSZCYJ":
                                                dictionary.Add("ZZSZCYJ", node2.InnerText);
                                                break;

                                            case "ZZSTSNRDM":
                                                dictionary.Add("ZZSTSNRDM", node2.InnerText);
                                                break;

                                            case "XFSGL":
                                                dictionary.Add("XFSGL", node2.InnerText);
                                                break;

                                            case "XFSZCYJ":
                                                dictionary.Add("XFSZCYJ", node2.InnerText);
                                                break;

                                            case "XFSTSNRDM":
                                                dictionary.Add("XFSTSNRDM", node2.InnerText);
                                                break;

                                            case "TJJBM":
                                                dictionary.Add("TJJBM", node2.InnerText);
                                                break;

                                            case "HGJCKSPPM":
                                                dictionary.Add("HGJCKSPPM", node2.InnerText);
                                                break;

                                            case "QYSJ":
                                                dictionary.Add("QYSJ", node2.InnerText);
                                                break;

                                            case "GDQJZSJ":
                                                dictionary.Add("GDQJZSJ", node2.InnerText);
                                                break;

                                            case "GXSJ":
                                                dictionary.Add("GXSJ", DateTime.Parse(node2.InnerText));
                                                break;
                                        }
                                    }
                                    sqlID.Add("Aisino.Framework.MainForm.UpDown.ReplaceSPFL");
                                    listSPFL.Add(dictionary);
                                }
                            }
                            if ((listSPFL != null) && (listSPFL.Count > 0))
                            {
                                listSPFL = this.method_39(listSPFL);
                            }
                            XmlNodeList list8 = document2.GetElementsByTagName("ZZSYHZC");
                            if ((list8 != null) && (list8.Count > 1))
                            {
                                foreach (XmlNode node3 in list8)
                                {
                                    dictionary = new Dictionary<string, object>();
                                    XmlNodeList list9 = node3.ChildNodes;
                                    if ((list9 != null) && (list9.Count >= 1))
                                    {
                                        dictionary = new Dictionary<string, object>();
                                        foreach (XmlNode node4 in list9)
                                        {
                                            string name = node4.Name;
                                            if (name != null)
                                            {
                                                if (!(name == "YHZCMC"))
                                                {
                                                    if (name == "SL")
                                                    {
                                                        dictionary.Add("SLv", node4.InnerText);
                                                    }
                                                }
                                                else
                                                {
                                                    dictionary.Add("Yhzcmc", node4.InnerText);
                                                }
                                            }
                                        }
                                        sqlID.Add("Aisino.Framework.MainForm.UpDown.InsertYHZC");
                                        listSPFL.Add(dictionary);
                                    }
                                }
                            }
                            bool flag2 = new Class92().method_1(sqlID, listSPFL, bool_0);
                            if (((str3 != null) && (str3 != "")) && flag2)
                            {
                                Class101.smethod_0("更新编码表版本号开始：" + str3);
                                new Class92().method_3(str3);
                                Class101.smethod_0("更新编码表版本号结束：" + str3);
                                PropertyUtil.SetValue("BMBBBH", str3);
                                if (!flag)
                                {
                                    this.method_42(spbm, bool_0);
                                }
                            }
                        }
                        else
                        {
                            Class95.bool_1 = true;
                            Class95.string_1 = Class95.string_2 + "更新失败，请选择手动更新或去税局下载更新包导入更新：无法获取编码表编码信息";
                            Class101.smethod_0("解析同步数据：无法获取编码表编码信息");
                        }
                    }
                    else
                    {
                        if (!bool_0)
                        {
                            MessageHelper.MsgWait();
                            Class86.smethod_7(Class95.string_2 + "更新失败，请选择手动更新或去税局下载更新包导入更新：局端返回数据无编码表版本号");
                        }
                        Class95.bool_1 = true;
                        Class95.string_1 = Class95.string_2 + "更新失败，请选择手动更新或去税局下载更新包导入更新：局端返回数据无编码表版本号";
                        Class101.smethod_0("解析同步数据：无法获取编码表版本号");
                    }
                    return;
                Label_0807:
                    if (!bool_0)
                    {
                        MessageHelper.MsgWait();
                        Class86.smethod_8("没有需要同步的" + Class95.string_2 + "信息。");
                    }
                    Class95.bool_1 = false;
                    Class95.string_1 = "没有需要同步的" + Class95.string_2 + "信息。";
                    Class101.smethod_0("没有需要同步的商品编码信息。");
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("解析同步数据异常AnalySPFLUpdateInfoFromServer：" + exception.ToString());
                    if (!bool_0)
                    {
                        MessageHelper.MsgWait();
                        Class86.smethod_7(Class95.string_2 + "更新失败，请选择手动更新或去税局下载更新包导入更新：" + exception.Message);
                    }
                    Class95.bool_1 = true;
                    Class95.string_1 = Class95.string_2 + "更新失败，请选择手动更新或去税局下载更新包导入更新：" + exception.Message;
                }
            }
        }

        public string method_33(string string_4)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", "");
            document.AppendChild(newChild);
            XmlNode node = document.CreateNode(XmlNodeType.Element, "ZZSFPXT", "");
            document.AppendChild(node);
            XmlElement element = document.CreateElement("SK_TYPE");
            element.InnerText = "JSP";
            node.AppendChild(element);
            XmlElement element2 = document.CreateElement("OP_TYPE");
            element2.InnerText = "0037";
            node.AppendChild(element2);
            XmlNode node2 = document.CreateNode(XmlNodeType.Element, "INPUT", "");
            node.AppendChild(node2);
            XmlElement element3 = document.CreateElement("NSRSBH");
            element3.InnerText = this.string_2;
            node2.AppendChild(element3);
            XmlElement element4 = document.CreateElement("KPJH");
            element4.InnerText = this.string_1;
            node2.AppendChild(element4);
            XmlElement element5 = document.CreateElement("SBBH");
            element5.InnerText = this.string_0;
            node2.AppendChild(element5);
            XmlElement element6 = document.CreateElement("YSBZ");
            element6.InnerText = "N";
            node2.AppendChild(element6);
            try
            {
                byte[] bytes = ToolUtil.GetBytes(string_4);
                string hashString = MD5_Crypt.GetHashString(bytes);
                string str2 = Convert.ToBase64String(bytes);
                XmlElement element7 = document.CreateElement("DATA");
                element7.InnerText = str2;
                node2.AppendChild(element7);
                XmlElement element8 = document.CreateElement("CRC");
                element8.InnerText = hashString;
                node2.AppendChild(element8);
            }
            catch (Exception exception)
            {
                Class101.smethod_1("TestSPFLUpdate:" + exception.ToString());
            }
            return document.InnerXml;
        }

        public string method_34(string string_4)
        {
            XmlDocument document = new XmlDocument();
            string str = "";
            try
            {
                document.LoadXml(string_4);
                str = ToolUtil.GetString(ToolUtil.FromBase64String(document.GetElementsByTagName("DATA")[0].InnerText));
            }
            catch (Exception exception)
            {
                Class101.smethod_1("GetYWBSFromJSBWTest:" + exception.ToString());
            }
            return str;
        }

        private string method_35(string string_4)
        {
            while (string_4.EndsWith("00"))
            {
                string_4 = string_4.Substring(0, string_4.Length - 2);
            }
            return string_4;
        }

        private string method_36(string string_4)
        {
            string str = string_4;
            if ((str != null) && (str.Length > 2))
            {
                return str.Substring(0, str.Length - 2);
            }
            return "";
        }

        private string method_37(string string_4, List<Dictionary<string, object>> listSPFL, DataTable dataTable_0)
        {
            if (((string_4 != "") && (listSPFL != null)) && (listSPFL.Count >= 1))
            {
                try
                {
                    for (int i = 0; i < listSPFL.Count; i++)
                    {
                        if (string_4 == listSPFL[i]["SJBM"].ToString())
                        {
                            return "0";
                        }
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("GetWJ:" + exception.ToString());
                }
            }
            return this.method_38(string_4, dataTable_0);
        }

        private string method_38(string string_4, DataTable dataTable_0)
        {
            if (((string_4 != null) && (string_4 != "")) && ((dataTable_0 != null) && (dataTable_0.Rows.Count >= 1)))
            {
                try
                {
                    DataRow[] rowArray = dataTable_0.Select("SJBM='" + string_4 + "'");
                    if ((rowArray != null) && (rowArray.Length > 0))
                    {
                        return "0";
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("GetWJFromDB:" + exception.ToString());
                }
            }
            return "1";
        }

        private List<Dictionary<string, object>> method_39(List<Dictionary<string, object>> listSPFL)
        {
            List<Dictionary<string, object>> list = listSPFL;
            DataTable table = new Class92().method_2();
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i]["WJ"] = this.method_37(list[i]["BM"].ToString(), list, table);
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_1("HandleWJJD:" + exception.ToString());
            }
            return list;
        }

        private XmlNode method_4(Fpxx fpxx_0, ref XmlDocument xmlDocument_0)
        {
            DateTime time;
            DateTime time2;
            if ((fpxx_0 == null) || (xmlDocument_0 == null))
            {
                return null;
            }
            DateTime time3 = new DateTime(0x7dd, 9, 10, 8, 0x22, 30);
            TimeSpan span = (TimeSpan) (DateTime.Now - time3);
            byte[] buffer = AES_Crypt.Encrypt(ToolUtil.GetBytes(span.TotalSeconds.ToString("F1")), new byte[] { 
                0xff, 0x42, 0xae, 0x95, 11, 0x51, 0xca, 0x15, 0x21, 140, 0x4f, 170, 220, 0x92, 170, 0xed, 
                0xfd, 0xeb, 0x4e, 13, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
             }, new byte[] { 0xf2, 0x1f, 0xac, 0x5b, 0x2c, 0xc0, 0xa9, 0xd0, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 });
            fpxx_0.Get_Print_Dj(null, 1, buffer);
            XmlNode node = xmlDocument_0.CreateNode(XmlNodeType.Element, "FP", "");
            if (Class87.bool_5 && (fpxx_0.fplx == FPLX.DZFP))
            {
                XmlElement element11 = xmlDocument_0.CreateElement("BMB_BBH");
                element11.InnerText = fpxx_0.bmbbbh;
                node.AppendChild(element11);
            }
            XmlElement newChild = xmlDocument_0.CreateElement("FPZL");
            if (fpxx_0.fplx == FPLX.DZFP)
            {
                newChild.InnerText = "026";
            }
            else
            {
                newChild.InnerText = Convert.ToString((int) fpxx_0.fplx);
            }
            node.AppendChild(newChild);
            XmlElement element28 = xmlDocument_0.CreateElement("KPJH");
            if (string.Equals(this.taxCard_0.SoftVersion, "FWKP_V2.0_Svr_Client"))
            {
                element28.InnerText = this.string_1;
            }
            else
            {
                element28.InnerText = Convert.ToString(fpxx_0.kpjh);
            }
            node.AppendChild(element28);
            XmlElement element68 = xmlDocument_0.CreateElement("SL");
            element68.InnerText = fpxx_0.sLv;
            node.AppendChild(element68);
            XmlElement element69 = xmlDocument_0.CreateElement("FPDM");
            element69.InnerText = fpxx_0.fpdm;
            node.AppendChild(element69);
            XmlElement element70 = xmlDocument_0.CreateElement("FPHM");
            element70.InnerText = fpxx_0.fphm;
            node.AppendChild(element70);
            XmlElement element8 = xmlDocument_0.CreateElement("TSPZ");
            if (fpxx_0.Zyfplx == ZYFP_LX.NCP_SG)
            {
                element8.InnerText = "02";
            }
            else if (fpxx_0.Zyfplx == ZYFP_LX.NCP_XS)
            {
                element8.InnerText = "01";
            }
            else if ((fpxx_0.Zyfplx != ZYFP_LX.XT_CCP) && (fpxx_0.Zyfplx != ZYFP_LX.XT_YCL))
            {
                element8.InnerText = "";
            }
            else
            {
                element8.InnerText = "03";
            }
            node.AppendChild(element8);
            string str = "V1";
            switch (fpxx_0.Zyfplx)
            {
                case ZYFP_LX.SNY:
                    str = "V4";
                    break;

                case ZYFP_LX.XT_YCL:
                    str = "V2";
                    break;

                case ZYFP_LX.XT_CCP:
                    str = "V3";
                    break;

                case ZYFP_LX.NCP_XS:
                    str = "V5";
                    break;

                case ZYFP_LX.NCP_SG:
                    str = "V6";
                    break;
            }
            XmlElement element57 = xmlDocument_0.CreateElement("WPLB");
            element57.InnerText = str;
            node.AppendChild(element57);
            XmlElement element54 = xmlDocument_0.CreateElement("ZFBZ");
            if (fpxx_0.zfbz)
            {
                element54.InnerText = "Y";
            }
            else
            {
                element54.InnerText = "N";
            }
            node.AppendChild(element54);
            XmlElement element59 = xmlDocument_0.CreateElement("MW");
            element59.InnerText = fpxx_0.mw;
            node.AppendChild(element59);
            XmlElement element60 = xmlDocument_0.CreateElement("XFSH");
            element60.InnerText = fpxx_0.xfsh;
            node.AppendChild(element60);
            XmlElement element61 = xmlDocument_0.CreateElement("XFMC");
            element61.InnerText = fpxx_0.xfmc;
            node.AppendChild(element61);
            XmlElement element62 = xmlDocument_0.CreateElement("XFDZDH");
            element62.InnerText = fpxx_0.xfdzdh;
            node.AppendChild(element62);
            XmlElement element63 = xmlDocument_0.CreateElement("XFYHZH");
            element63.InnerText = fpxx_0.xfyhzh;
            node.AppendChild(element63);
            XmlElement element64 = xmlDocument_0.CreateElement("GFSH");
            element64.InnerText = fpxx_0.gfsh;
            node.AppendChild(element64);
            XmlElement element65 = xmlDocument_0.CreateElement("GFMC");
            element65.InnerText = fpxx_0.gfmc;
            node.AppendChild(element65);
            XmlElement element66 = xmlDocument_0.CreateElement("GFDZDH");
            element66.InnerText = fpxx_0.gfdzdh;
            node.AppendChild(element66);
            XmlElement element67 = xmlDocument_0.CreateElement("GFYHZH");
            element67.InnerText = fpxx_0.gfyhzh;
            node.AppendChild(element67);
            XmlElement element29 = xmlDocument_0.CreateElement("KPRQ");
            if (DateTime.TryParse(fpxx_0.kprq, out time))
            {
                if (fpxx_0.fplx == FPLX.DZFP)
                {
                    element29.InnerText = time.ToString("yyyyMMdd");
                }
                else
                {
                    element29.InnerText = time.ToString("yyyyMMddHHmmss");
                }
            }
            node.AppendChild(element29);
            if (fpxx_0.fplx == FPLX.DZFP)
            {
                XmlElement element33 = xmlDocument_0.CreateElement("KPSJ");
                element33.InnerText = time.ToString("HHmmss");
                node.AppendChild(element33);
            }
            if (fpxx_0.fplx == FPLX.DZFP)
            {
                XmlElement element12 = xmlDocument_0.CreateElement("FPZTBZ");
                double num5 = 0.0;
                double.TryParse(fpxx_0.je, out num5);
                if ((num5 > 0.0) && !fpxx_0.zfbz)
                {
                    element12.InnerText = "0";
                }
                else if ((num5 < 0.0) && !fpxx_0.zfbz)
                {
                    element12.InnerText = "1";
                }
                else if ((num5 == 0.0) && fpxx_0.zfbz)
                {
                    element12.InnerText = "2";
                }
                else if ((num5 > 0.0) && fpxx_0.zfbz)
                {
                    element12.InnerText = "3";
                }
                else if ((num5 < 0.0) && fpxx_0.zfbz)
                {
                    element12.InnerText = "4";
                }
                node.AppendChild(element12);
            }
            XmlElement element38 = xmlDocument_0.CreateElement("ZFRQ");
            if (DateTime.TryParse(fpxx_0.zfsj, out time2))
            {
                if (fpxx_0.fplx == FPLX.DZFP)
                {
                    element38.InnerText = time2.ToString("yyyyMMdd");
                }
                else
                {
                    element38.InnerText = time2.ToString("yyyyMMddHHmmss");
                }
            }
            node.AppendChild(element38);
            if (fpxx_0.fplx == FPLX.DZFP)
            {
                XmlElement element47 = xmlDocument_0.CreateElement("ZFSJ");
                element47.InnerText = time2.ToString("HHmmss");
                node.AppendChild(element47);
            }
            XmlElement element55 = xmlDocument_0.CreateElement("JE");
            element55.InnerText = this.method_27(fpxx_0.je, 2);
            node.AppendChild(element55);
            XmlElement element56 = xmlDocument_0.CreateElement("SE");
            element56.InnerText = this.method_27(fpxx_0.se, 2);
            node.AppendChild(element56);
            double result = 0.0;
            double num2 = 0.0;
            XmlElement element = xmlDocument_0.CreateElement("JSHJ");
            if (!double.TryParse(fpxx_0.je, out result))
            {
                result = 0.0;
            }
            if (!double.TryParse(fpxx_0.se, out num2))
            {
                num2 = 0.0;
            }
            element.InnerText = this.method_27(Convert.ToString((double) (result + num2)), 2);
            node.AppendChild(element);
            XmlElement element2 = xmlDocument_0.CreateElement("BZ");
            element2.InnerText = fpxx_0.bz;
            node.AppendChild(element2);
            XmlElement element3 = xmlDocument_0.CreateElement("KPR");
            element3.InnerText = fpxx_0.kpr;
            node.AppendChild(element3);
            XmlElement element4 = xmlDocument_0.CreateElement("SKR");
            element4.InnerText = fpxx_0.skr;
            node.AppendChild(element4);
            XmlElement element5 = xmlDocument_0.CreateElement("FHR");
            element5.InnerText = fpxx_0.fhr;
            node.AppendChild(element5);
            XmlElement element6 = xmlDocument_0.CreateElement("HZFPXXBBH");
            if (fpxx_0.fplx == FPLX.PTFP)
            {
                element6.InnerText = "";
            }
            else
            {
                element6.InnerText = fpxx_0.redNum;
            }
            node.AppendChild(element6);
            XmlElement element35 = xmlDocument_0.CreateElement("JYM");
            element35.InnerText = fpxx_0.jym;
            node.AppendChild(element35);
            XmlElement element36 = xmlDocument_0.CreateElement("LZFPDM");
            if (fpxx_0.fplx == FPLX.ZYFP)
            {
                element36.InnerText = "";
            }
            else
            {
                element36.InnerText = fpxx_0.blueFpdm;
            }
            node.AppendChild(element36);
            XmlElement element13 = xmlDocument_0.CreateElement("LZFPHM");
            if (fpxx_0.fplx == FPLX.ZYFP)
            {
                element13.InnerText = "";
            }
            else
            {
                element13.InnerText = fpxx_0.blueFphm;
            }
            node.AppendChild(element13);
            XmlElement element14 = xmlDocument_0.CreateElement("BSQ");
            element14.InnerText = fpxx_0.bsq.ToString();
            node.AppendChild(element14);
            XmlElement element15 = xmlDocument_0.CreateElement("DKNSRSBH");
            element15.InnerText = fpxx_0.dkqysh.ToString();
            node.AppendChild(element15);
            XmlElement element16 = xmlDocument_0.CreateElement("DKQYMC");
            element16.InnerText = fpxx_0.dkqymc.ToString();
            node.AppendChild(element16);
            if (Class87.bool_5 && (fpxx_0.fplx != FPLX.DZFP))
            {
                XmlElement element73 = xmlDocument_0.CreateElement("SPBMBBH");
                element73.InnerText = fpxx_0.bmbbbh;
                node.AppendChild(element73);
            }
            if (fpxx_0.fplx != FPLX.DZFP)
            {
                XmlElement element48 = xmlDocument_0.CreateElement("SLBZ");
                element48.InnerText = "0";
                double num6 = 0.0;
                double.TryParse(fpxx_0.sLv, out num6);
                if ((((fpxx_0.fplx == FPLX.ZYFP) && (num6 == 0.05)) && (fpxx_0.yysbz.Substring(8, 1) == "0")) || (num6 == 0.015))
                {
                    element48.InnerText = "1";
                }
                else if (fpxx_0.yysbz.Substring(8, 1) == "2")
                {
                    element48.InnerText = "2";
                }
                node.AppendChild(element48);
            }
            if ((fpxx_0.Mxxx != null) && (fpxx_0.Mxxx.Count > 0))
            {
                XmlNode node5 = xmlDocument_0.CreateNode(XmlNodeType.Element, "MX", "");
                node.AppendChild(node5);
                for (int i = 0; i < fpxx_0.Mxxx.Count; i++)
                {
                    XmlNode node2 = xmlDocument_0.CreateNode(XmlNodeType.Element, "MXXX", "");
                    node5.AppendChild(node2);
                    string str2 = fpxx_0.Get_Print_Dj(fpxx_0.Mxxx[i], 1, null);
                    XmlElement element45 = xmlDocument_0.CreateElement("MXXH");
                    element45.InnerText = fpxx_0.Mxxx[i][SPXX.XH];
                    node2.AppendChild(element45);
                    if (Class87.bool_5 && (fpxx_0.fplx == FPLX.DZFP))
                    {
                        XmlElement element30 = xmlDocument_0.CreateElement("SPBM");
                        element30.InnerText = fpxx_0.Mxxx[i][SPXX.FLBM];
                        node2.AppendChild(element30);
                        XmlElement element31 = xmlDocument_0.CreateElement("ZXBM");
                        element31.InnerText = fpxx_0.Mxxx[i][SPXX.SPBH];
                        node2.AppendChild(element31);
                        XmlElement element7 = xmlDocument_0.CreateElement("YHZCBS");
                        if (fpxx_0.Mxxx[i][SPXX.XSYH] == null)
                        {
                            element7.InnerText = "";
                        }
                        else
                        {
                            element7.InnerText = fpxx_0.Mxxx[i][SPXX.XSYH].ToString();
                        }
                        node2.AppendChild(element7);
                        XmlElement element39 = xmlDocument_0.CreateElement("LSLBS");
                        element39.InnerText = fpxx_0.Mxxx[i][SPXX.LSLVBS];
                        node2.AppendChild(element39);
                        XmlElement element40 = xmlDocument_0.CreateElement("ZZSTSGL");
                        element40.InnerText = fpxx_0.Mxxx[i][SPXX.YHSM];
                        node2.AppendChild(element40);
                    }
                    XmlElement element9 = xmlDocument_0.CreateElement("MC");
                    if (fpxx_0.Mxxx[i].ContainsKey(SPXX.SPMC))
                    {
                        element9.InnerText = fpxx_0.Mxxx[i][SPXX.SPMC];
                    }
                    node2.AppendChild(element9);
                    XmlElement element10 = xmlDocument_0.CreateElement("JE");
                    if (fpxx_0.Mxxx[i].ContainsKey(SPXX.JE))
                    {
                        element10.InnerText = this.method_27(fpxx_0.Mxxx[i][SPXX.JE], 2);
                    }
                    node2.AppendChild(element10);
                    XmlElement element18 = xmlDocument_0.CreateElement("SL");
                    if (fpxx_0.Mxxx[i].ContainsKey(SPXX.SLV))
                    {
                        element18.InnerText = fpxx_0.Mxxx[i][SPXX.SLV];
                    }
                    node2.AppendChild(element18);
                    XmlElement element41 = xmlDocument_0.CreateElement("SE");
                    if (fpxx_0.Mxxx[i].ContainsKey(SPXX.SE))
                    {
                        element41.InnerText = this.method_27(fpxx_0.Mxxx[i][SPXX.SE], 2);
                    }
                    node2.AppendChild(element41);
                    XmlElement element46 = xmlDocument_0.CreateElement("SHUL");
                    if (fpxx_0.Mxxx[i].ContainsKey(SPXX.SL))
                    {
                        if (fpxx_0.fplx == FPLX.DZFP)
                        {
                            element46.InnerText = this.method_27(fpxx_0.Mxxx[i][SPXX.SL], 8);
                        }
                        else
                        {
                            element46.InnerText = fpxx_0.Mxxx[i][SPXX.SL];
                        }
                    }
                    node2.AppendChild(element46);
                    XmlElement element34 = xmlDocument_0.CreateElement("DJ");
                    if (fpxx_0.Mxxx[i].ContainsKey(SPXX.DJ))
                    {
                        if (fpxx_0.fplx == FPLX.DZFP)
                        {
                            element34.InnerText = this.method_27(str2, 8);
                        }
                        else
                        {
                            element34.InnerText = str2;
                        }
                    }
                    node2.AppendChild(element34);
                    XmlElement element51 = xmlDocument_0.CreateElement("GGXH");
                    if (fpxx_0.Mxxx[i].ContainsKey(SPXX.GGXH))
                    {
                        element51.InnerText = fpxx_0.Mxxx[i][SPXX.GGXH];
                    }
                    node2.AppendChild(element51);
                    XmlElement element21 = xmlDocument_0.CreateElement("JLDW");
                    if (fpxx_0.Mxxx[i].ContainsKey(SPXX.JLDW))
                    {
                        element21.InnerText = fpxx_0.Mxxx[i][SPXX.JLDW];
                    }
                    node2.AppendChild(element21);
                    if (Class87.bool_5 && (fpxx_0.fplx != FPLX.DZFP))
                    {
                        XmlElement element71 = xmlDocument_0.CreateElement("SPBM");
                        element71.InnerText = fpxx_0.Mxxx[i][SPXX.FLBM];
                        node2.AppendChild(element71);
                        XmlElement element72 = xmlDocument_0.CreateElement("QYSPBM");
                        element72.InnerText = fpxx_0.Mxxx[i][SPXX.SPBH];
                        node2.AppendChild(element72);
                        XmlElement element44 = xmlDocument_0.CreateElement("SYYHZCBZ");
                        if (fpxx_0.Mxxx[i][SPXX.XSYH] == null)
                        {
                            element44.InnerText = "";
                        }
                        else
                        {
                            element44.InnerText = fpxx_0.Mxxx[i][SPXX.XSYH].ToString();
                        }
                        node2.AppendChild(element44);
                        XmlElement element52 = xmlDocument_0.CreateElement("YHZC");
                        element52.InnerText = fpxx_0.Mxxx[i][SPXX.YHSM];
                        node2.AppendChild(element52);
                        XmlElement element53 = xmlDocument_0.CreateElement("LSLBZ");
                        element53.InnerText = fpxx_0.Mxxx[i][SPXX.LSLVBS];
                        node2.AppendChild(element53);
                    }
                }
            }
            if ((fpxx_0.Qdxx != null) && (fpxx_0.Qdxx.Count > 0))
            {
                XmlNode node3 = xmlDocument_0.CreateNode(XmlNodeType.Element, "QD", "");
                node.AppendChild(node3);
                for (int j = 0; j < fpxx_0.Qdxx.Count; j++)
                {
                    XmlNode node4 = xmlDocument_0.CreateNode(XmlNodeType.Element, "QDXX", "");
                    node3.AppendChild(node4);
                    string str3 = fpxx_0.Get_Print_Dj(fpxx_0.Qdxx[j], 1, null);
                    XmlElement element19 = xmlDocument_0.CreateElement("MXXH");
                    if (fpxx_0.Qdxx[j].ContainsKey(SPXX.XH))
                    {
                        element19.InnerText = fpxx_0.Qdxx[j][SPXX.XH];
                    }
                    node4.AppendChild(element19);
                    XmlElement element20 = xmlDocument_0.CreateElement("MC");
                    if (fpxx_0.Qdxx[j].ContainsKey(SPXX.SPMC))
                    {
                        element20.InnerText = fpxx_0.Qdxx[j][SPXX.SPMC];
                    }
                    node4.AppendChild(element20);
                    XmlElement element24 = xmlDocument_0.CreateElement("JE");
                    if (fpxx_0.Qdxx[j].ContainsKey(SPXX.JE))
                    {
                        element24.InnerText = this.method_27(fpxx_0.Qdxx[j][SPXX.JE], 2);
                    }
                    node4.AppendChild(element24);
                    XmlElement element43 = xmlDocument_0.CreateElement("SL");
                    if (fpxx_0.Qdxx[j].ContainsKey(SPXX.SLV))
                    {
                        element43.InnerText = fpxx_0.Qdxx[j][SPXX.SLV];
                    }
                    node4.AppendChild(element43);
                    XmlElement element22 = xmlDocument_0.CreateElement("SE");
                    if (fpxx_0.Qdxx[j].ContainsKey(SPXX.SE))
                    {
                        element22.InnerText = this.method_27(fpxx_0.Qdxx[j][SPXX.SE], 2);
                    }
                    node4.AppendChild(element22);
                    XmlElement element23 = xmlDocument_0.CreateElement("SHUL");
                    if (fpxx_0.Qdxx[j].ContainsKey(SPXX.SL))
                    {
                        if (fpxx_0.fplx == FPLX.DZFP)
                        {
                            element23.InnerText = this.method_27(fpxx_0.Qdxx[j][SPXX.SL], 8);
                        }
                        else
                        {
                            element23.InnerText = fpxx_0.Qdxx[j][SPXX.SL];
                        }
                    }
                    node4.AppendChild(element23);
                    XmlElement element37 = xmlDocument_0.CreateElement("DJ");
                    if (fpxx_0.Qdxx[j].ContainsKey(SPXX.DJ))
                    {
                        if (fpxx_0.fplx == FPLX.DZFP)
                        {
                            element37.InnerText = this.method_27(str3, 8);
                        }
                        else
                        {
                            element37.InnerText = str3;
                        }
                    }
                    node4.AppendChild(element37);
                    XmlElement element32 = xmlDocument_0.CreateElement("GGXH");
                    if (fpxx_0.Qdxx[j].ContainsKey(SPXX.GGXH))
                    {
                        element32.InnerText = fpxx_0.Qdxx[j][SPXX.GGXH];
                    }
                    node4.AppendChild(element32);
                    XmlElement element58 = xmlDocument_0.CreateElement("JLDW");
                    if (fpxx_0.Qdxx[j].ContainsKey(SPXX.JLDW))
                    {
                        element58.InnerText = fpxx_0.Qdxx[j][SPXX.JLDW];
                    }
                    node4.AppendChild(element58);
                    if (Class87.bool_5)
                    {
                        XmlElement element25 = xmlDocument_0.CreateElement("SPBM");
                        element25.InnerText = fpxx_0.Qdxx[j][SPXX.FLBM];
                        node4.AppendChild(element25);
                        XmlElement element26 = xmlDocument_0.CreateElement("QYSPBM");
                        element26.InnerText = fpxx_0.Qdxx[j][SPXX.SPBH];
                        node4.AppendChild(element26);
                        XmlElement element27 = xmlDocument_0.CreateElement("SYYHZCBZ");
                        if (fpxx_0.Qdxx[j][SPXX.XSYH] == null)
                        {
                            element27.InnerText = "";
                        }
                        else
                        {
                            element27.InnerText = fpxx_0.Qdxx[j][SPXX.XSYH];
                        }
                        node4.AppendChild(element27);
                        XmlElement element49 = xmlDocument_0.CreateElement("YHZC");
                        element49.InnerText = fpxx_0.Qdxx[j][SPXX.YHSM];
                        node4.AppendChild(element49);
                        XmlElement element50 = xmlDocument_0.CreateElement("LSLBZ");
                        element50.InnerText = fpxx_0.Qdxx[j][SPXX.LSLVBS];
                        node4.AppendChild(element50);
                    }
                }
            }
            XmlElement element42 = xmlDocument_0.CreateElement("SIGN");
            element42.InnerText = fpxx_0.sign;
            node.AppendChild(element42);
            return node;
        }

        public void method_40(string string_4)
        {
            if (string_4 != "")
            {
                Class101.smethod_0("GetFPsIntoTempTableBySLH开始:" + string_4);
                try
                {
                    DataTable table = new Class81().method_20(string_4);
                    if ((table != null) && (table.Rows.Count >= 1))
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            Class96 class3 = new Class96 {
                                DZSYH = row["DZSYH"].ToString(),
                                FPDM = row["fpdm"].ToString(),
                                FpKPJH = row["KPJH"].ToString()
                            };
                            if (row["fpzl"].ToString() == "c")
                            {
                                class3.Fplx = FPLX.PTFP.ToString();
                            }
                            else if (row["fpzl"].ToString() == "s")
                            {
                                class3.Fplx = FPLX.ZYFP.ToString();
                            }
                            else if (row["fpzl"].ToString() == "f")
                            {
                                class3.Fplx = FPLX.HYFP.ToString();
                            }
                            else if (row["fpzl"].ToString() == "j")
                            {
                                class3.Fplx = FPLX.JDCFP.ToString();
                            }
                            else if (row["fpzl"].ToString() == "p")
                            {
                                class3.Fplx = FPLX.DZFP.ToString();
                            }
                            else if (row["fpzl"].ToString() == "q")
                            {
                                class3.Fplx = FPLX.JSFP.ToString();
                            }
                            else
                            {
                                class3.Fplx = row["fpzl"].ToString();
                            }
                            class3.FPNO = row["fphm"].ToString();
                            class3.FpNSRSBH = this.string_2;
                            class3.FpSBBH = this.string_0;
                            class3.FPSLH = string_4;
                            class3.FPSQHRecieveTime = DateTime.Now.ToString();
                            class3.FPStatus = "3";
                            class3.FpUploadTime = row["scsj"].ToString();
                            class3.Boolean_0 = true;
                            class3.ISFpDownFailed = false;
                            class3.IsFpUpFailed = false;
                            if (row["zfbz"].ToString() == "1")
                            {
                                class3.ZFBZ = true;
                            }
                            else if (row["zfbz"].ToString() == "0")
                            {
                                class3.ZFBZ = false;
                            }
                            else
                            {
                                class3.ZFBZ = Convert.ToBoolean(row["zfbz"]);
                            }
                            this.class100_0.method_1(class3, Enum10.Insert);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("GetFPsIntoTempTableBySLH异常：" + exception.ToString());
                }
            }
        }

        private string method_41(XmlNode xmlNode_0)
        {
            if (xmlNode_0 == null)
            {
                return "";
            }
            if (xmlNode_0.InnerText.IndexOf("![CDATA[") > -1)
            {
                return xmlNode_0.InnerText.Substring(xmlNode_0.InnerText.IndexOf("![CDATA[") + 8, (xmlNode_0.InnerText.IndexOf("]]>") - xmlNode_0.InnerText.IndexOf("![CDATA[")) - 8);
            }
            return xmlNode_0.InnerText;
        }

        private void method_42(List<string> spbm, bool bool_0)
        {
            if ((spbm != null) && (spbm.Count >= 1))
            {
                try
                {
                    string str = "";
                    foreach (string str2 in spbm)
                    {
                        object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Bmgl.UpdateDatabaseBySPFL", new object[] { str2 });
                        if ((objArray != null) && (objArray.Length >= 1))
                        {
                            Dictionary<string, int> dictionary = objArray[0] as Dictionary<string, int>;
                            int num = dictionary["SP"];
                            int num2 = dictionary["CL"];
                            int num3 = dictionary["FYXM"];
                            if (((num2 >= 0) && (num >= 0)) && ((num3 >= 0) && (((num + num2) + num3) > 0)))
                            {
                                bool flag = false;
                                str = str + "\t\t分类编码" + str2 + "：";
                                if (num2 > 0)
                                {
                                    if (flag)
                                    {
                                        str = str + "，";
                                    }
                                    object obj2 = str;
                                    str = string.Concat(new object[] { obj2, "车辆编码", num2, "条" });
                                    flag = true;
                                }
                                if (num > 0)
                                {
                                    if (flag)
                                    {
                                        str = str + "，";
                                    }
                                    object obj3 = str;
                                    str = string.Concat(new object[] { obj3, "商品编码", num, "条" });
                                    flag = true;
                                }
                                if (num3 > 0)
                                {
                                    if (flag)
                                    {
                                        str = str + "，";
                                    }
                                    object obj4 = str;
                                    str = string.Concat(new object[] { obj4, "费用项目编码", num3, "条" });
                                    flag = true;
                                }
                                str = str + "。\r\n";
                            }
                        }
                        else
                        {
                            Class101.smethod_0("ReleaseSPBMInZXBM  spbm:" + str2 + " 执行Aisino.Fwkp.Bmgl.UpdateDatabaseBySPFL无返回结果");
                        }
                    }
                    if (str != "")
                    {
                        str = "由于本次税收分类编码更新，系统自动维护了更新编码对应的商品编码、车辆编码及费用项目编码。";
                        if (!bool_0)
                        {
                            MessageBoxHelper.Show(str);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("ReleaseSPBMInZXBM 异常：" + exception.ToString());
                }
            }
        }

        public List<XmlDocument> method_5(DataTable dataTable_0)
        {
            List<XmlDocument> list = new List<XmlDocument>();
            if ((dataTable_0 != null) && (dataTable_0.Rows.Count >= 1))
            {
                try
                {
                    for (int i = 0; i < dataTable_0.Rows.Count; i++)
                    {
                        XmlDocument item = new XmlDocument();
                        XmlDeclaration newChild = item.CreateXmlDeclaration("1.0", "GBK", "");
                        item.AppendChild(newChild);
                        XmlNode node = item.CreateNode(XmlNodeType.Element, "FPXT", "");
                        item.AppendChild(node);
                        XmlNode node2 = item.CreateNode(XmlNodeType.Element, "INPUT", "");
                        node.AppendChild(node2);
                        XmlElement element = item.CreateElement("NSRSBH");
                        element.InnerText = this.string_2;
                        node2.AppendChild(element);
                        XmlElement element2 = item.CreateElement("KPJH");
                        element2.InnerText = this.string_1;
                        node2.AppendChild(element2);
                        XmlElement element3 = item.CreateElement("SBBH");
                        element3.InnerText = this.string_0;
                        node2.AppendChild(element3);
                        XmlElement element4 = item.CreateElement("FXSH");
                        element4.InnerText = this.string_3;
                        node2.AppendChild(element4);
                        XmlElement element5 = item.CreateElement("SLXLH");
                        element5.InnerText = dataTable_0.Rows[i]["FPSLH"].ToString();
                        node2.AppendChild(element5);
                        list.Add(item);
                    }
                }
                catch (Exception)
                {
                }
            }
            return list;
        }

        private XmlDocument method_6(List<Fpxx> fpList)
        {
            if ((fpList != null) && (fpList.Count >= 1))
            {
                try
                {
                    StringBuilder builder = new StringBuilder();
                    XmlDocument document = new XmlDocument();
                    document.AppendChild(document.CreateXmlDeclaration("1.0", "GBK", ""));
                    XmlNode newChild = document.CreateNode(XmlNodeType.Element, "root", "");
                    document.AppendChild(newChild);
                    XmlElement element = document.CreateElement("ywdm");
                    element.InnerText = "JSPJK_CBMX";
                    newChild.AppendChild(element);
                    XmlElement element2 = document.CreateElement("ywmc");
                    element2.InnerText = "读报税发票明细";
                    newChild.AppendChild(element2);
                    XmlElement element3 = document.CreateElement("cgbz");
                    element3.InnerText = "Y";
                    newChild.AppendChild(element3);
                    XmlElement element4 = document.CreateElement("errorMsg");
                    newChild.AppendChild(element4);
                    XmlNode node2 = document.CreateNode(XmlNodeType.Element, "resDatas", "");
                    newChild.AppendChild(node2);
                    XmlElement element5 = document.CreateElement("mxfs");
                    element5.InnerText = fpList.Count.ToString();
                    node2.AppendChild(element5);
                    XmlNode node3 = document.CreateNode(XmlNodeType.Element, "hyzpmx", "");
                    node2.AppendChild(node3);
                    for (int i = 0; i < fpList.Count; i++)
                    {
                        DateTime time2;
                        DateTime time3;
                        XmlNode node4 = document.CreateNode(XmlNodeType.Element, "mx", "");
                        node3.AppendChild(node4);
                        DateTime time = new DateTime(0x7dd, 9, 10, 8, 0x22, 30);
                        TimeSpan span = (TimeSpan) (DateTime.Now - time);
                        byte[] buffer = AES_Crypt.Encrypt(ToolUtil.GetBytes(span.TotalSeconds.ToString("F1")), new byte[] { 
                            0xff, 0x42, 0xae, 0x95, 11, 0x51, 0xca, 0x15, 0x21, 140, 0x4f, 170, 220, 0x92, 170, 0xed, 
                            0xfd, 0xeb, 0x4e, 13, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
                         }, new byte[] { 0xf2, 0x1f, 0xac, 0x5b, 0x2c, 0xc0, 0xa9, 0xd0, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 });
                        fpList[i].Get_Print_Dj(null, 1, buffer);
                        if (Class87.bool_5)
                        {
                            XmlElement element6 = document.CreateElement("data");
                            element6.SetAttribute("name", "bmb_bbh");
                            element6.InnerText = fpList[i].bmbbbh;
                            node4.AppendChild(element6);
                            builder.Append(element6.InnerText);
                        }
                        XmlElement element7 = document.CreateElement("data");
                        element7.SetAttribute("name", "fpdm");
                        element7.InnerText = fpList[i].fpdm.ToString();
                        node4.AppendChild(element7);
                        builder.Append(element7.InnerText);
                        XmlElement element8 = document.CreateElement("data");
                        element8.SetAttribute("name", "fphm");
                        element8.InnerText = fpList[i].fphm.ToString();
                        node4.AppendChild(element8);
                        builder.Append(element8.InnerText);
                        XmlElement element9 = document.CreateElement("data");
                        element9.SetAttribute("name", "kprq");
                        if (DateTime.TryParse(fpList[i].kprq, out time2))
                        {
                            element9.InnerText = time2.ToString("yyyyMMdd");
                        }
                        node4.AppendChild(element9);
                        builder.Append(element9.InnerText);
                        XmlElement element10 = document.CreateElement("data");
                        element10.SetAttribute("name", "kpsj");
                        if (DateTime.TryParse(fpList[i].kprq, out time2))
                        {
                            element10.InnerText = time2.ToString("HHmmss");
                        }
                        node4.AppendChild(element10);
                        builder.Append(element10.InnerText);
                        XmlElement element11 = document.CreateElement("data");
                        element11.SetAttribute("name", "skph");
                        element11.InnerText = this.string_0;
                        node4.AppendChild(element11);
                        builder.Append(element11.InnerText);
                        XmlElement element12 = document.CreateElement("data");
                        element12.SetAttribute("name", "hjje");
                        element12.InnerText = fpList[i].je;
                        node4.AppendChild(element12);
                        builder.Append(element12.InnerText);
                        XmlElement element13 = document.CreateElement("data");
                        element13.SetAttribute("name", "sl");
                        element13.InnerText = fpList[i].sLv;
                        node4.AppendChild(element13);
                        builder.Append(element13.InnerText);
                        XmlElement element14 = document.CreateElement("data");
                        element14.SetAttribute("name", "se");
                        element14.InnerText = fpList[i].se;
                        node4.AppendChild(element14);
                        builder.Append(element14.InnerText);
                        double result = 0.0;
                        double num4 = 0.0;
                        double.TryParse(fpList[i].je, out result);
                        double.TryParse(fpList[i].se, out num4);
                        XmlElement element15 = document.CreateElement("data");
                        element15.SetAttribute("name", "jshj");
                        element15.InnerText = (result + num4).ToString();
                        node4.AppendChild(element15);
                        builder.Append(element15.InnerText);
                        XmlElement element16 = document.CreateElement("data");
                        element16.SetAttribute("name", "czch");
                        element16.InnerText = fpList[i].czch;
                        node4.AppendChild(element16);
                        builder.Append(element16.InnerText);
                        XmlElement element17 = document.CreateElement("data");
                        element17.SetAttribute("name", "ccdw");
                        element17.InnerText = fpList[i].ccdw;
                        node4.AppendChild(element17);
                        builder.Append(element17.InnerText);
                        XmlElement element18 = document.CreateElement("data");
                        element18.SetAttribute("name", "wspzhm");
                        node4.AppendChild(element18);
                        builder.Append(element18.InnerText);
                        XmlElement element19 = document.CreateElement("data");
                        element19.SetAttribute("name", "skm");
                        element19.InnerText = fpList[i].mw;
                        node4.AppendChild(element19);
                        builder.Append(element19.InnerText);
                        XmlElement element20 = document.CreateElement("data");
                        element20.SetAttribute("name", "cyrsbh");
                        element20.InnerText = fpList[i].cyrnsrsbh;
                        node4.AppendChild(element20);
                        builder.Append(element20.InnerText);
                        XmlElement element21 = document.CreateElement("data");
                        element21.SetAttribute("name", "cyrmc");
                        element21.InnerText = fpList[i].cyrmc;
                        node4.AppendChild(element21);
                        builder.Append(element21.InnerText);
                        XmlElement element22 = document.CreateElement("data");
                        element22.SetAttribute("name", "spfsbh");
                        element22.InnerText = fpList[i].spfnsrsbh;
                        node4.AppendChild(element22);
                        builder.Append(element22.InnerText);
                        XmlElement element23 = document.CreateElement("data");
                        element23.SetAttribute("name", "spfmc");
                        element23.InnerText = fpList[i].spfmc;
                        node4.AppendChild(element23);
                        builder.Append(element23.InnerText);
                        XmlElement element24 = document.CreateElement("data");
                        element24.SetAttribute("name", "shrsbh");
                        element24.InnerText = fpList[i].shrnsrsbh;
                        node4.AppendChild(element24);
                        builder.Append(element24.InnerText);
                        XmlElement element25 = document.CreateElement("data");
                        element25.SetAttribute("name", "shrmc");
                        element25.InnerText = fpList[i].shrmc;
                        node4.AppendChild(element25);
                        builder.Append(element25.InnerText);
                        XmlElement element26 = document.CreateElement("data");
                        element26.SetAttribute("name", "fhrsbh");
                        element26.InnerText = fpList[i].fhrnsrsbh;
                        node4.AppendChild(element26);
                        builder.Append(element26.InnerText);
                        XmlElement element27 = document.CreateElement("data");
                        element27.SetAttribute("name", "fhrmc");
                        element27.InnerText = fpList[i].fhrmc;
                        node4.AppendChild(element27);
                        builder.Append(element27.InnerText);
                        XmlElement element28 = document.CreateElement("data");
                        element28.SetAttribute("name", "qyd");
                        element28.InnerText = fpList[i].qyd;
                        node4.AppendChild(element28);
                        builder.Append(element28.InnerText);
                        XmlElement element29 = document.CreateElement("data");
                        element29.SetAttribute("name", "yshwxx");
                        element29.InnerText = fpList[i].yshwxx;
                        node4.AppendChild(element29);
                        builder.Append(element29.InnerText);
                        XmlElement element30 = document.CreateElement("data");
                        element30.SetAttribute("name", "bz");
                        element30.InnerText = fpList[i].bz;
                        node4.AppendChild(element30);
                        builder.Append(element30.InnerText);
                        XmlElement element31 = document.CreateElement("data");
                        element31.SetAttribute("name", "swjg_dm");
                        element31.InnerText = fpList[i].zgswjgdm;
                        node4.AppendChild(element31);
                        builder.Append(element31.InnerText);
                        XmlElement element32 = document.CreateElement("data");
                        element32.SetAttribute("name", "swjg_mc");
                        element32.InnerText = fpList[i].zgswjgmc;
                        node4.AppendChild(element32);
                        builder.Append(element32.InnerText);
                        XmlElement element33 = document.CreateElement("data");
                        element33.SetAttribute("name", "fpztbz");
                        if ((result > 0.0) && !fpList[i].zfbz)
                        {
                            element33.InnerText = "0";
                        }
                        else if ((result < 0.0) && !fpList[i].zfbz)
                        {
                            element33.InnerText = "1";
                        }
                        else if ((result == 0.0) && fpList[i].zfbz)
                        {
                            element33.InnerText = "2";
                        }
                        else if ((result > 0.0) && fpList[i].zfbz)
                        {
                            element33.InnerText = "3";
                        }
                        else if ((result < 0.0) && fpList[i].zfbz)
                        {
                            element33.InnerText = "4";
                        }
                        node4.AppendChild(element33);
                        builder.Append(element33.InnerText);
                        XmlElement element34 = document.CreateElement("data");
                        element34.SetAttribute("name", "skr");
                        element34.InnerText = fpList[i].skr;
                        node4.AppendChild(element34);
                        builder.Append(element34.InnerText);
                        XmlElement element35 = document.CreateElement("data");
                        element35.SetAttribute("name", "fhr");
                        element35.InnerText = fpList[i].fhr;
                        node4.AppendChild(element35);
                        builder.Append(element35.InnerText);
                        XmlElement element36 = document.CreateElement("data");
                        element36.SetAttribute("name", "kpr");
                        element36.InnerText = fpList[i].kpr;
                        node4.AppendChild(element36);
                        builder.Append(element36.InnerText);
                        XmlElement element37 = document.CreateElement("data");
                        element37.SetAttribute("name", "yfpdm");
                        element37.InnerText = "";
                        node4.AppendChild(element37);
                        builder.Append(element37.InnerText);
                        XmlElement element38 = document.CreateElement("data");
                        element38.SetAttribute("name", "yfphm");
                        element38.InnerText = "";
                        node4.AppendChild(element38);
                        builder.Append(element38.InnerText);
                        XmlElement element39 = document.CreateElement("data");
                        element39.SetAttribute("name", "zfrq");
                        if (DateTime.TryParse(fpList[i].zfsj, out time3))
                        {
                            element39.InnerText = time3.ToString("yyyyMMdd");
                        }
                        else
                        {
                            element39.InnerText = "";
                        }
                        node4.AppendChild(element39);
                        builder.Append(element39.InnerText);
                        XmlElement element40 = document.CreateElement("data");
                        element40.SetAttribute("name", "zfsj");
                        if (DateTime.TryParse(fpList[i].zfsj, out time3) && !time3.ToString("HHmmss").Equals("000000"))
                        {
                            element40.InnerText = time3.ToString("HHmmss");
                        }
                        else
                        {
                            element40.InnerText = "";
                        }
                        node4.AppendChild(element40);
                        builder.Append(element40.InnerText);
                        XmlElement element41 = document.CreateElement("data");
                        element41.SetAttribute("name", "zfr");
                        element41.InnerText = fpList[i].kpr;
                        node4.AppendChild(element41);
                        builder.Append(element41.InnerText);
                        XmlElement element42 = document.CreateElement("data");
                        element42.SetAttribute("name", "tzdh");
                        element42.InnerText = fpList[i].redNum;
                        node4.AppendChild(element42);
                        builder.Append(element42.InnerText);
                        XmlElement element43 = document.CreateElement("data");
                        element43.SetAttribute("name", "qmz");
                        element43.InnerText = fpList[i].sign;
                        node4.AppendChild(element43);
                        builder.Append(element43.InnerText);
                        new MD5_Crypt();
                        XmlElement element44 = document.CreateElement("data");
                        element44.SetAttribute("name", "mxmd5");
                        element44.InnerText = MD5_Crypt.GetHashString32(builder.ToString());
                        for (int j = 0; j < fpList[i].Mxxx.Count; j++)
                        {
                            StringBuilder builder2 = new StringBuilder();
                            XmlNode node5 = document.CreateNode(XmlNodeType.Element, "gd", "");
                            node4.AppendChild(node5);
                            XmlElement element45 = document.CreateElement("data");
                            element45.SetAttribute("name", "fpdm");
                            element45.InnerText = fpList[i].fpdm;
                            node5.AppendChild(element45);
                            builder2.Append(element45.InnerText);
                            XmlElement element46 = document.CreateElement("data");
                            element46.SetAttribute("name", "fphm");
                            element46.InnerText = fpList[i].fphm;
                            node5.AppendChild(element46);
                            builder2.Append(element46.InnerText);
                            XmlElement element47 = document.CreateElement("data");
                            element47.SetAttribute("name", "xh");
                            element47.InnerText = fpList[i].Mxxx[j][SPXX.XH].ToString();
                            node5.AppendChild(element47);
                            builder2.Append(element47.InnerText);
                            if (Class87.bool_5)
                            {
                                XmlElement element48 = document.CreateElement("data");
                                element48.SetAttribute("name", "spbm");
                                element48.InnerText = fpList[i].Mxxx[j][SPXX.FLBM].ToString();
                                node5.AppendChild(element48);
                                builder2.Append(element48.InnerText);
                                XmlElement element49 = document.CreateElement("data");
                                element49.SetAttribute("name", "zxbm");
                                element49.InnerText = fpList[i].Mxxx[j][SPXX.SPBH].ToString();
                                node5.AppendChild(element49);
                                builder2.Append(element49.InnerText);
                                XmlElement element50 = document.CreateElement("data");
                                element50.SetAttribute("name", "yhzcbs");
                                if (fpList[i].Mxxx[j][SPXX.XSYH] == null)
                                {
                                    element50.InnerText = "";
                                }
                                else
                                {
                                    element50.InnerText = fpList[i].Mxxx[j][SPXX.XSYH].ToString();
                                }
                                node5.AppendChild(element50);
                                builder2.Append(element50.InnerText);
                                XmlElement element51 = document.CreateElement("data");
                                element51.SetAttribute("name", "lslbs");
                                element51.InnerText = fpList[i].Mxxx[j][SPXX.LSLVBS].ToString();
                                node5.AppendChild(element51);
                                builder2.Append(element51.InnerText);
                                XmlElement element52 = document.CreateElement("data");
                                element52.SetAttribute("name", "zzstsgl");
                                element52.InnerText = fpList[i].Mxxx[j][SPXX.YHSM].ToString();
                                node5.AppendChild(element52);
                                builder2.Append(element52.InnerText);
                            }
                            XmlElement element53 = document.CreateElement("data");
                            element53.SetAttribute("name", "fyxm");
                            element53.InnerText = fpList[i].Mxxx[j][SPXX.SPMC];
                            node5.AppendChild(element53);
                            builder2.Append(element53.InnerText);
                            XmlElement element54 = document.CreateElement("data");
                            element54.SetAttribute("name", "je");
                            element54.InnerText = fpList[i].Mxxx[j][SPXX.JE];
                            node5.AppendChild(element54);
                            builder2.Append(element54.InnerText);
                            XmlElement element55 = document.CreateElement("data");
                            element55.SetAttribute("name", "gdmd5");
                            element55.InnerText = MD5_Crypt.GetHashString32(builder2.ToString());
                            node5.AppendChild(element55);
                        }
                    }
                    return document;
                }
                catch (Exception)
                {
                    Class101.smethod_1("组装货运机动车发票失败！");
                }
            }
            return null;
        }

        private XmlDocument method_7(List<Fpxx> fpList)
        {
            if ((fpList != null) && (fpList.Count >= 1))
            {
                try
                {
                    StringBuilder builder = new StringBuilder();
                    XmlDocument document = new XmlDocument();
                    document.AppendChild(document.CreateXmlDeclaration("1.0", "GBK", ""));
                    XmlNode newChild = document.CreateNode(XmlNodeType.Element, "root", "");
                    document.AppendChild(newChild);
                    XmlElement element = document.CreateElement("ywdm");
                    element.InnerText = "JSPJK_CBMX";
                    newChild.AppendChild(element);
                    XmlElement element2 = document.CreateElement("ywmc");
                    element2.InnerText = "读报税发票明细";
                    newChild.AppendChild(element2);
                    XmlElement element3 = document.CreateElement("cgbz");
                    element3.InnerText = "Y";
                    newChild.AppendChild(element3);
                    XmlElement element4 = document.CreateElement("errorMsg");
                    newChild.AppendChild(element4);
                    XmlNode node2 = document.CreateNode(XmlNodeType.Element, "resDatas", "");
                    newChild.AppendChild(node2);
                    XmlElement element5 = document.CreateElement("mxfs");
                    element5.InnerText = fpList.Count.ToString();
                    node2.AppendChild(element5);
                    XmlNode node3 = document.CreateNode(XmlNodeType.Element, "jdcfpmx", "");
                    node2.AppendChild(node3);
                    for (int i = 0; i < fpList.Count; i++)
                    {
                        DateTime time2;
                        DateTime time3;
                        XmlNode node4 = document.CreateNode(XmlNodeType.Element, "mx", "");
                        node3.AppendChild(node4);
                        DateTime time = new DateTime(0x7dd, 9, 10, 8, 0x22, 30);
                        TimeSpan span = (TimeSpan) (DateTime.Now - time);
                        byte[] buffer = AES_Crypt.Encrypt(ToolUtil.GetBytes(span.TotalSeconds.ToString("F1")), new byte[] { 
                            0xff, 0x42, 0xae, 0x95, 11, 0x51, 0xca, 0x15, 0x21, 140, 0x4f, 170, 220, 0x92, 170, 0xed, 
                            0xfd, 0xeb, 0x4e, 13, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
                         }, new byte[] { 0xf2, 0x1f, 0xac, 0x5b, 0x2c, 0xc0, 0xa9, 0xd0, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 });
                        fpList[i].Get_Print_Dj(null, 1, buffer);
                        XmlElement element6 = document.CreateElement("data");
                        element6.SetAttribute("name", "fpdm");
                        element6.InnerText = fpList[i].fpdm.ToString();
                        node4.AppendChild(element6);
                        builder.Append(element6.InnerText);
                        XmlElement element7 = document.CreateElement("data");
                        element7.SetAttribute("name", "fphm");
                        element7.InnerText = fpList[i].fphm;
                        node4.AppendChild(element7);
                        builder.Append(element7.InnerText);
                        XmlElement element8 = document.CreateElement("data");
                        element8.SetAttribute("name", "skph");
                        element8.InnerText = this.string_0;
                        node4.AppendChild(element8);
                        builder.Append(element8.InnerText);
                        XmlElement element9 = document.CreateElement("data");
                        element9.SetAttribute("name", "kprq");
                        if (DateTime.TryParse(fpList[i].kprq, out time2))
                        {
                            element9.InnerText = time2.ToString("yyyyMMdd");
                        }
                        node4.AppendChild(element9);
                        builder.Append(element9.InnerText);
                        XmlElement element10 = document.CreateElement("data");
                        element10.SetAttribute("name", "kpsj");
                        if (DateTime.TryParse(fpList[i].kprq, out time2))
                        {
                            element10.InnerText = time2.ToString("HHmmss");
                        }
                        node4.AppendChild(element10);
                        builder.Append(element10.InnerText);
                        XmlElement element11 = document.CreateElement("data");
                        element11.SetAttribute("name", "skm");
                        element11.InnerText = fpList[i].mw;
                        node4.AppendChild(element11);
                        builder.Append(element11.InnerText);
                        XmlElement element12 = document.CreateElement("data");
                        element12.SetAttribute("name", "ghdw");
                        element12.InnerText = fpList[i].gfmc;
                        node4.AppendChild(element12);
                        builder.Append(element12.InnerText);
                        XmlElement element13 = document.CreateElement("data");
                        element13.SetAttribute("name", "scqymc");
                        element13.InnerText = fpList[i].sccjmc;
                        node4.AppendChild(element13);
                        builder.Append(element13.InnerText);
                        XmlElement element14 = document.CreateElement("data");
                        element14.SetAttribute("name", "sfzhm");
                        element14.InnerText = fpList[i].sfzhm;
                        node4.AppendChild(element14);
                        builder.Append(element14.InnerText);
                        XmlElement element15 = document.CreateElement("data");
                        element15.SetAttribute("name", "xhdwmc");
                        element15.InnerText = fpList[i].xfmc;
                        node4.AppendChild(element15);
                        builder.Append(element15.InnerText);
                        XmlElement element16 = document.CreateElement("data");
                        element16.SetAttribute("name", "nsrsbh");
                        element16.InnerText = this.string_2;
                        node4.AppendChild(element16);
                        builder.Append(element16.InnerText);
                        XmlElement element17 = document.CreateElement("data");
                        element17.SetAttribute("name", "dz");
                        element17.InnerText = fpList[i].xfdz;
                        node4.AppendChild(element17);
                        builder.Append(element17.InnerText);
                        XmlElement element18 = document.CreateElement("data");
                        element18.SetAttribute("name", "dh");
                        element18.InnerText = fpList[i].xfdh;
                        node4.AppendChild(element18);
                        builder.Append(element18.InnerText);
                        XmlElement element19 = document.CreateElement("data");
                        element19.SetAttribute("name", "khyh");
                        element19.InnerText = fpList[i].xfyh;
                        node4.AppendChild(element19);
                        builder.Append(element19.InnerText);
                        XmlElement element20 = document.CreateElement("data");
                        element20.SetAttribute("name", "zh");
                        element20.InnerText = fpList[i].xfzh;
                        node4.AppendChild(element20);
                        builder.Append(element20.InnerText);
                        XmlElement element21 = document.CreateElement("data");
                        element21.SetAttribute("name", "cpxh");
                        element21.InnerText = fpList[i].cpxh;
                        node4.AppendChild(element21);
                        builder.Append(element21.InnerText);
                        XmlElement element22 = document.CreateElement("data");
                        element22.SetAttribute("name", "cllx");
                        element22.InnerText = fpList[i].cllx;
                        node4.AppendChild(element22);
                        builder.Append(element22.InnerText);
                        XmlElement element23 = document.CreateElement("data");
                        element23.SetAttribute("name", "hgzs");
                        element23.InnerText = fpList[i].hgzh;
                        node4.AppendChild(element23);
                        builder.Append(element23.InnerText);
                        XmlElement element24 = document.CreateElement("data");
                        element24.SetAttribute("name", "jkzmsh");
                        element24.InnerText = fpList[i].jkzmsh;
                        node4.AppendChild(element24);
                        builder.Append(element24.InnerText);
                        XmlElement element25 = document.CreateElement("data");
                        element25.SetAttribute("name", "cd");
                        element25.InnerText = fpList[i].cd;
                        node4.AppendChild(element25);
                        builder.Append(element25.InnerText);
                        XmlElement element26 = document.CreateElement("data");
                        element26.SetAttribute("name", "sjdh");
                        element26.InnerText = fpList[i].sjdh;
                        node4.AppendChild(element26);
                        builder.Append(element26.InnerText);
                        XmlElement element27 = document.CreateElement("data");
                        element27.SetAttribute("name", "fdjhm");
                        element27.InnerText = fpList[i].fdjhm;
                        node4.AppendChild(element27);
                        builder.Append(element27.InnerText);
                        XmlElement element28 = document.CreateElement("data");
                        element28.SetAttribute("name", "cjhm");
                        element28.InnerText = fpList[i].clsbdh;
                        node4.AppendChild(element28);
                        builder.Append(element28.InnerText);
                        XmlElement element29 = document.CreateElement("data");
                        element29.SetAttribute("name", "cjfy");
                        element29.InnerText = fpList[i].je;
                        node4.AppendChild(element29);
                        builder.Append(element29.InnerText);
                        XmlElement element30 = document.CreateElement("data");
                        element30.SetAttribute("name", "zzssl");
                        element30.InnerText = fpList[i].sLv;
                        node4.AppendChild(element30);
                        builder.Append(element30.InnerText);
                        XmlElement element31 = document.CreateElement("data");
                        element31.SetAttribute("name", "zzsse");
                        element31.InnerText = fpList[i].se;
                        node4.AppendChild(element31);
                        builder.Append(element31.InnerText);
                        double result = 0.0;
                        double num4 = 0.0;
                        double.TryParse(fpList[i].je, out result);
                        double.TryParse(fpList[i].se, out num4);
                        XmlElement element32 = document.CreateElement("data");
                        element32.SetAttribute("name", "jshj");
                        element32.InnerText = (result + num4).ToString();
                        node4.AppendChild(element32);
                        builder.Append(element32.InnerText);
                        XmlElement element33 = document.CreateElement("data");
                        element33.SetAttribute("name", "kpr");
                        element33.InnerText = fpList[i].kpr;
                        node4.AppendChild(element33);
                        builder.Append(element33.InnerText);
                        XmlElement element34 = document.CreateElement("data");
                        element34.SetAttribute("name", "dw");
                        element34.InnerText = fpList[i].dw;
                        node4.AppendChild(element34);
                        builder.Append(element34.InnerText);
                        XmlElement element35 = document.CreateElement("data");
                        element35.SetAttribute("name", "xcrs");
                        element35.InnerText = fpList[i].xcrs;
                        node4.AppendChild(element35);
                        builder.Append(element35.InnerText);
                        XmlElement element36 = document.CreateElement("data");
                        element36.SetAttribute("name", "fpztbz");
                        if ((result > 0.0) && !fpList[i].zfbz)
                        {
                            element36.InnerText = "0";
                        }
                        else if ((result < 0.0) && !fpList[i].zfbz)
                        {
                            element36.InnerText = "1";
                        }
                        else if ((result == 0.0) && fpList[i].zfbz)
                        {
                            element36.InnerText = "2";
                        }
                        else if ((result > 0.0) && fpList[i].zfbz)
                        {
                            element36.InnerText = "3";
                        }
                        else if ((result < 0.0) && fpList[i].zfbz)
                        {
                            element36.InnerText = "4";
                        }
                        node4.AppendChild(element36);
                        builder.Append(element36.InnerText);
                        XmlElement element37 = document.CreateElement("data");
                        element37.SetAttribute("name", "swjg_dm");
                        element37.InnerText = fpList[i].zgswjgdm;
                        node4.AppendChild(element37);
                        builder.Append(element37.InnerText);
                        XmlElement element38 = document.CreateElement("data");
                        element38.SetAttribute("name", "swjg_mc");
                        element38.InnerText = fpList[i].zgswjgmc;
                        node4.AppendChild(element38);
                        builder.Append(element38.InnerText);
                        XmlElement element39 = document.CreateElement("data");
                        element39.SetAttribute("name", "yfpdm");
                        element39.InnerText = fpList[i].blueFpdm;
                        node4.AppendChild(element39);
                        builder.Append(element39.InnerText);
                        XmlElement element40 = document.CreateElement("data");
                        element40.SetAttribute("name", "yfphm");
                        element40.InnerText = fpList[i].blueFphm;
                        node4.AppendChild(element40);
                        builder.Append(element40.InnerText);
                        XmlElement element41 = document.CreateElement("data");
                        element41.SetAttribute("name", "zfrq");
                        if (DateTime.TryParse(fpList[i].zfsj, out time3))
                        {
                            element41.InnerText = time3.ToString("yyyyMMdd");
                        }
                        else
                        {
                            element41.InnerText = "";
                        }
                        node4.AppendChild(element41);
                        builder.Append(element41.InnerText);
                        XmlElement element42 = document.CreateElement("data");
                        element42.SetAttribute("name", "zfsj");
                        if (DateTime.TryParse(fpList[i].zfsj, out time3) && !time3.ToString("HHmmss").Equals("000000"))
                        {
                            element42.InnerText = time3.ToString("HHmmss");
                        }
                        else
                        {
                            element42.InnerText = "";
                        }
                        node4.AppendChild(element42);
                        builder.Append(element42.InnerText);
                        XmlElement element43 = document.CreateElement("data");
                        element43.SetAttribute("name", "zfr");
                        element43.InnerText = fpList[i].kpr;
                        node4.AppendChild(element43);
                        builder.Append(element43.InnerText);
                        XmlElement element44 = document.CreateElement("data");
                        element44.SetAttribute("name", "wspzhm");
                        element44.InnerText = "";
                        node4.AppendChild(element44);
                        builder.Append(element44.InnerText);
                        XmlElement element45 = document.CreateElement("data");
                        element45.SetAttribute("name", "gfsbh");
                        if (fpList[i].isNewJdcfp)
                        {
                            element45.InnerText = fpList[i].gfsh;
                        }
                        node4.AppendChild(element45);
                        builder.Append(element45.InnerText);
                        XmlElement element46 = document.CreateElement("data");
                        element46.SetAttribute("name", "qmz");
                        element46.InnerText = fpList[i].sign;
                        node4.AppendChild(element46);
                        builder.Append(element46.InnerText);
                        if (Class87.bool_5)
                        {
                            XmlElement element47 = document.CreateElement("data");
                            element47.SetAttribute("name", "bmb_bbh");
                            element47.InnerText = fpList[i].bmbbbh;
                            node4.AppendChild(element47);
                            builder.Append(element47.InnerText);
                            XmlElement element48 = document.CreateElement("data");
                            element48.SetAttribute("name", "spbm");
                            element48.InnerText = fpList[i].zyspmc;
                            node4.AppendChild(element48);
                            builder.Append(element48.InnerText);
                            string zyspsm = fpList[i].zyspsm;
                            int index = zyspsm.IndexOf("#%");
                            XmlElement element49 = document.CreateElement("data");
                            element49.SetAttribute("name", "zxbm");
                            if (index > -1)
                            {
                                element49.InnerText = zyspsm.Substring(0, index);
                            }
                            node4.AppendChild(element49);
                            builder.Append(element49.InnerText);
                            string skr = fpList[i].skr;
                            int length = skr.IndexOf("#%");
                            XmlElement element50 = document.CreateElement("data");
                            element50.SetAttribute("name", "yhzcbs");
                            if (length > -1)
                            {
                                element50.InnerText = skr.Substring(0, length);
                            }
                            node4.AppendChild(element50);
                            builder.Append(element50.InnerText);
                            XmlElement element51 = document.CreateElement("data");
                            element51.SetAttribute("name", "lslbs");
                            if (length > -1)
                            {
                                element51.InnerText = skr.Substring(length + 2);
                            }
                            node4.AppendChild(element51);
                            builder.Append(element51.InnerText);
                            XmlElement element52 = document.CreateElement("data");
                            element52.SetAttribute("name", "zzstsgl");
                            if (index > -1)
                            {
                                element52.InnerText = zyspsm.Substring(index + 2);
                            }
                            node4.AppendChild(element52);
                            builder.Append(element52.InnerText);
                        }
                        new MD5_Crypt();
                        XmlElement element53 = document.CreateElement("data");
                        element53.SetAttribute("name", "mxmd5");
                        element53.InnerText = MD5_Crypt.GetHashString32(builder.ToString());
                    }
                    return document;
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("(上传线程)组装货运机动车发票失败！" + exception.ToString());
                }
            }
            return null;
        }

        private XmlDocument method_8(List<Fpxx> fpList)
        {
            XmlDocument document = new XmlDocument();
            if ((fpList != null) && (fpList.Count >= 1))
            {
                try
                {
                    XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", "");
                    document.AppendChild(newChild);
                    XmlNode node = document.CreateNode(XmlNodeType.Element, "FPXT", "");
                    document.AppendChild(node);
                    XmlNode node2 = document.CreateNode(XmlNodeType.Element, "INPUT", "");
                    node.AppendChild(node2);
                    XmlNode node3 = document.CreateNode(XmlNodeType.Element, "NSRSBH", "");
                    node3.InnerText = this.string_2;
                    node2.AppendChild(node3);
                    XmlNode node4 = document.CreateNode(XmlNodeType.Element, "KPJH", "");
                    node4.InnerText = this.string_1;
                    node2.AppendChild(node4);
                    XmlNode node5 = document.CreateNode(XmlNodeType.Element, "SBBH", "");
                    node5.InnerText = this.string_0;
                    node2.AppendChild(node5);
                    XmlNode node6 = document.CreateNode(XmlNodeType.Element, "BMB_BBH", "");
                    node6.InnerText = this.string_0;
                    node2.AppendChild(node6);
                    foreach (Fpxx fpxx in fpList)
                    {
                        DateTime time2;
                        DateTime time3;
                        XmlNode node7 = document.CreateNode(XmlNodeType.Element, "FP", "");
                        node2.AppendChild(node7);
                        DateTime time = new DateTime(0x7dd, 9, 10, 8, 0x22, 30);
                        TimeSpan span = (TimeSpan) (DateTime.Now - time);
                        byte[] buffer = AES_Crypt.Encrypt(ToolUtil.GetBytes(span.TotalSeconds.ToString("F1")), new byte[] { 
                            0xff, 0x42, 0xae, 0x95, 11, 0x51, 0xca, 0x15, 0x21, 140, 0x4f, 170, 220, 0x92, 170, 0xed, 
                            0xfd, 0xeb, 0x4e, 13, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
                         }, new byte[] { 0xf2, 0x1f, 0xac, 0x5b, 0x2c, 0xc0, 0xa9, 0xd0, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 });
                        fpxx.Get_Print_Dj(null, 1, buffer);
                        if (Class87.bool_5)
                        {
                            XmlElement element = document.CreateElement("BMB_BBH");
                            element.InnerText = fpxx.bmbbbh;
                            node7.AppendChild(element);
                        }
                        XmlElement element2 = document.CreateElement("FPZL");
                        element2.InnerText = "025";
                        node7.AppendChild(element2);
                        XmlElement element3 = document.CreateElement("FPDM");
                        element3.InnerText = fpxx.fpdm;
                        node7.AppendChild(element3);
                        XmlElement element4 = document.CreateElement("FPHM");
                        element4.InnerText = fpxx.fphm;
                        node7.AppendChild(element4);
                        XmlElement element5 = document.CreateElement("TSPZ");
                        if (fpxx.Zyfplx == ZYFP_LX.NCP_SG)
                        {
                            element5.InnerText = "02";
                        }
                        else if (fpxx.Zyfplx == ZYFP_LX.NCP_XS)
                        {
                            element5.InnerText = "01";
                        }
                        else if ((fpxx.Zyfplx != ZYFP_LX.XT_CCP) && (fpxx.Zyfplx != ZYFP_LX.XT_YCL))
                        {
                            element5.InnerText = "";
                        }
                        else
                        {
                            element5.InnerText = "03";
                        }
                        node7.AppendChild(element5);
                        XmlElement element6 = document.CreateElement("FPZTBZ");
                        double result = 0.0;
                        double.TryParse(fpxx.je, out result);
                        if ((result > 0.0) && !fpxx.zfbz)
                        {
                            element6.InnerText = "0";
                        }
                        else if ((result < 0.0) && !fpxx.zfbz)
                        {
                            element6.InnerText = "1";
                        }
                        else if ((result == 0.0) && fpxx.zfbz)
                        {
                            element6.InnerText = "2";
                        }
                        else if ((result > 0.0) && fpxx.zfbz)
                        {
                            element6.InnerText = "3";
                        }
                        else if ((result < 0.0) && fpxx.zfbz)
                        {
                            element6.InnerText = "4";
                        }
                        node7.AppendChild(element6);
                        XmlElement element7 = document.CreateElement("ZFBZ");
                        if (fpxx.zfbz)
                        {
                            element7.InnerText = "Y";
                        }
                        else
                        {
                            element7.InnerText = "N";
                        }
                        node7.AppendChild(element7);
                        XmlElement element8 = document.CreateElement("MW");
                        element8.InnerText = fpxx.mw;
                        node7.AppendChild(element8);
                        XmlElement element9 = document.CreateElement("XFSH");
                        element9.InnerText = fpxx.xfsh;
                        node7.AppendChild(element9);
                        XmlElement element10 = document.CreateElement("XFMC");
                        element10.InnerText = fpxx.xfmc;
                        node7.AppendChild(element10);
                        XmlElement element11 = document.CreateElement("XFDZDH");
                        element11.InnerText = fpxx.xfdzdh;
                        node7.AppendChild(element11);
                        XmlElement element12 = document.CreateElement("XFYHZH");
                        element12.InnerText = fpxx.xfyhzh;
                        node7.AppendChild(element12);
                        XmlElement element13 = document.CreateElement("GFSH");
                        element13.InnerText = fpxx.gfsh;
                        node7.AppendChild(element13);
                        XmlElement element14 = document.CreateElement("GFMC");
                        element14.InnerText = fpxx.gfmc;
                        node7.AppendChild(element14);
                        XmlElement element15 = document.CreateElement("GFDZDH");
                        element15.InnerText = fpxx.gfdzdh;
                        node7.AppendChild(element15);
                        XmlElement element16 = document.CreateElement("GFYHZH");
                        element16.InnerText = fpxx.gfyhzh;
                        node7.AppendChild(element16);
                        XmlElement element17 = document.CreateElement("KPRQ");
                        if (DateTime.TryParse(fpxx.kprq, out time2))
                        {
                            element17.InnerText = time2.ToString("yyyyMMdd");
                        }
                        node7.AppendChild(element17);
                        XmlElement element18 = document.CreateElement("KPSJ");
                        if (DateTime.TryParse(fpxx.kprq, out time2))
                        {
                            element18.InnerText = time2.ToString("HHmmss");
                        }
                        node7.AppendChild(element18);
                        XmlElement element19 = document.CreateElement("ZFRQ");
                        if (DateTime.TryParse(fpxx.zfsj, out time3))
                        {
                            element19.InnerText = time3.ToString("yyyyMMdd");
                        }
                        node7.AppendChild(element19);
                        XmlElement element20 = document.CreateElement("ZFSJ");
                        if (DateTime.TryParse(fpxx.zfsj, out time3))
                        {
                            element20.InnerText = time3.ToString("HHmmss");
                        }
                        node7.AppendChild(element20);
                        XmlElement element21 = document.CreateElement("JE");
                        element21.InnerText = this.method_27(fpxx.je, 2);
                        node7.AppendChild(element21);
                        XmlElement element22 = document.CreateElement("SE");
                        element22.InnerText = this.method_27(fpxx.se, 2);
                        node7.AppendChild(element22);
                        double num3 = 0.0;
                        double num4 = 0.0;
                        XmlElement element23 = document.CreateElement("JSHJ");
                        if (!double.TryParse(fpxx.je, out num3))
                        {
                            num3 = 0.0;
                        }
                        if (!double.TryParse(fpxx.se, out num4))
                        {
                            num4 = 0.0;
                        }
                        element23.InnerText = this.method_27(Convert.ToString((double) (num3 + num4)), 2);
                        node7.AppendChild(element23);
                        XmlElement element24 = document.CreateElement("BZ");
                        element24.InnerText = fpxx.bz;
                        node7.AppendChild(element24);
                        XmlElement element25 = document.CreateElement("KPR");
                        element25.InnerText = fpxx.kpr;
                        node7.AppendChild(element25);
                        XmlElement element26 = document.CreateElement("SKR");
                        element26.InnerText = fpxx.skr;
                        node7.AppendChild(element26);
                        XmlElement element27 = document.CreateElement("FHR");
                        element27.InnerText = fpxx.fhr;
                        node7.AppendChild(element27);
                        XmlElement element28 = document.CreateElement("HZFPXXBBH");
                        if (fpxx.fplx == FPLX.PTFP)
                        {
                            element28.InnerText = "";
                        }
                        else
                        {
                            element28.InnerText = fpxx.redNum;
                        }
                        node7.AppendChild(element28);
                        XmlElement element29 = document.CreateElement("JYM");
                        element29.InnerText = fpxx.jym;
                        node7.AppendChild(element29);
                        XmlElement element30 = document.CreateElement("LZFPDM");
                        if (fpxx.fplx == FPLX.ZYFP)
                        {
                            element30.InnerText = "";
                        }
                        else
                        {
                            element30.InnerText = fpxx.blueFpdm;
                        }
                        node7.AppendChild(element30);
                        XmlElement element31 = document.CreateElement("LZFPHM");
                        if (fpxx.fplx == FPLX.ZYFP)
                        {
                            element31.InnerText = "";
                        }
                        else
                        {
                            element31.InnerText = fpxx.blueFphm;
                        }
                        node7.AppendChild(element31);
                        XmlElement element32 = document.CreateElement("DKNSRSBH");
                        element32.InnerText = fpxx.dkqysh;
                        node7.AppendChild(element32);
                        XmlElement element33 = document.CreateElement("DKQYMC");
                        element33.InnerText = fpxx.dkqymc;
                        node7.AppendChild(element33);
                        XmlNode node8 = document.CreateNode(XmlNodeType.Element, "MX", "");
                        node7.AppendChild(node8);
                        if ((fpxx.Mxxx != null) && (fpxx.Mxxx.Count > 0))
                        {
                            for (int i = 0; i < fpxx.Mxxx.Count; i++)
                            {
                                XmlNode node9 = document.CreateNode(XmlNodeType.Element, "MXXX", "");
                                node8.AppendChild(node9);
                                string str = fpxx.Mxxx[i][SPXX.DJ];
                                string str2 = fpxx.Get_Print_Dj(fpxx.Mxxx[i], 1, null);
                                XmlElement element34 = document.CreateElement("MXXH");
                                element34.InnerText = fpxx.Mxxx[i][SPXX.XH];
                                node9.AppendChild(element34);
                                if (Class87.bool_5)
                                {
                                    XmlElement element35 = document.CreateElement("SPBM");
                                    element35.InnerText = fpxx.Mxxx[i][SPXX.FLBM];
                                    node9.AppendChild(element35);
                                    XmlElement element36 = document.CreateElement("ZXBM");
                                    element36.InnerText = fpxx.Mxxx[i][SPXX.SPBH];
                                    node9.AppendChild(element36);
                                    XmlElement element37 = document.CreateElement("YHZCBS");
                                    if (fpxx.Mxxx[i][SPXX.XSYH] == null)
                                    {
                                        element37.InnerText = "";
                                    }
                                    else
                                    {
                                        element37.InnerText = fpxx.Mxxx[i][SPXX.XSYH].ToString();
                                    }
                                    node9.AppendChild(element37);
                                    XmlElement element38 = document.CreateElement("LSLBS");
                                    element38.InnerText = fpxx.Mxxx[i][SPXX.LSLVBS];
                                    node9.AppendChild(element38);
                                    XmlElement element39 = document.CreateElement("ZZSTSGL");
                                    element39.InnerText = fpxx.Mxxx[i][SPXX.YHSM];
                                    node9.AppendChild(element39);
                                }
                                XmlElement element40 = document.CreateElement("MC");
                                if (fpxx.Mxxx[i].ContainsKey(SPXX.SPMC))
                                {
                                    element40.InnerText = fpxx.Mxxx[i][SPXX.SPMC];
                                }
                                node9.AppendChild(element40);
                                XmlElement element41 = document.CreateElement("JE");
                                if (fpxx.Mxxx[i].ContainsKey(SPXX.JE))
                                {
                                    element41.InnerText = this.method_27(fpxx.Mxxx[i][SPXX.JE], 2);
                                }
                                node9.AppendChild(element41);
                                XmlElement element42 = document.CreateElement("SL");
                                if (fpxx.Mxxx[i].ContainsKey(SPXX.SLV))
                                {
                                    element42.InnerText = fpxx.Mxxx[i][SPXX.SLV];
                                }
                                node9.AppendChild(element42);
                                XmlElement element43 = document.CreateElement("SE");
                                if (fpxx.Mxxx[i].ContainsKey(SPXX.SE))
                                {
                                    element43.InnerText = this.method_27(fpxx.Mxxx[i][SPXX.SE], 2);
                                }
                                node9.AppendChild(element43);
                                double num6 = 0.0;
                                double.TryParse(fpxx.Mxxx[i][SPXX.JE], out num6);
                                double num7 = 0.0;
                                double.TryParse(fpxx.Mxxx[i][SPXX.SE], out num7);
                                XmlElement element44 = document.CreateElement("HSJE");
                                element44.InnerText = this.method_27(Convert.ToString((double) (num6 + num7)), 2);
                                node9.AppendChild(element44);
                                XmlElement element45 = document.CreateElement("HSDJ");
                                if (fpxx.Mxxx[i][SPXX.HSJBZ].Equals("1"))
                                {
                                    element45.InnerText = this.method_27(str, 8);
                                }
                                else
                                {
                                    element45.InnerText = this.method_27(str2, 8);
                                }
                                node9.AppendChild(element45);
                                XmlElement element46 = document.CreateElement("SHUL");
                                if (fpxx.Mxxx[i].ContainsKey(SPXX.SL))
                                {
                                    element46.InnerText = this.method_27(fpxx.Mxxx[i][SPXX.SL], 8);
                                }
                                node9.AppendChild(element46);
                                XmlElement element47 = document.CreateElement("DJ");
                                if (fpxx.Mxxx[i][SPXX.HSJBZ].Equals("1"))
                                {
                                    element47.InnerText = this.method_27(str2, 8);
                                }
                                else
                                {
                                    element47.InnerText = this.method_27(str, 8);
                                }
                                node9.AppendChild(element47);
                                XmlElement element48 = document.CreateElement("GGXH");
                                if (fpxx.Mxxx[i].ContainsKey(SPXX.GGXH))
                                {
                                    element48.InnerText = fpxx.Mxxx[i][SPXX.GGXH];
                                }
                                node9.AppendChild(element48);
                                XmlElement element49 = document.CreateElement("JLDW");
                                if (fpxx.Mxxx[i].ContainsKey(SPXX.JLDW))
                                {
                                    element49.InnerText = fpxx.Mxxx[i][SPXX.JLDW];
                                }
                                node9.AppendChild(element49);
                            }
                        }
                        XmlElement element50 = document.CreateElement("SIGN");
                        element50.InnerText = fpxx.sign;
                        node7.AppendChild(element50);
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_0("发票上传-卷式发票异常：" + exception.ToString());
                }
            }
            return document;
        }

        public bool method_9(XmlDocument xmlDocument_0, XmlDocument xmlDocument_1)
        {
            Class101.smethod_0("进入解析函数");
            if ((xmlDocument_0 == null) || (xmlDocument_1 == null))
            {
                return false;
            }
            try
            {
                Class101.smethod_0("开始解析返回xml");
                XmlNode node = xmlDocument_0.GetElementsByTagName("CODE")[0];
                XmlNode node2 = xmlDocument_0.GetElementsByTagName("MESS")[0];
                string innerText = "";
                if (node == null)
                {
                    Class101.smethod_1("（下载线程）发票下载失败，无法获取CODE节点！");
                    if (string.IsNullOrEmpty(this.method_15(xmlDocument_1)))
                    {
                        this.method_22(xmlDocument_1, "已执行发票上传结果查询，服务器端返回错误信息为：" + node2.InnerText, 3);
                    }
                    else
                    {
                        this.method_22(xmlDocument_1, "已执行发票上传结果查询，服务器端处理超时，发票报送状态已变更为未报送，即将重新上传该发票。", 0);
                    }
                    Class87.string_3 = "-0002";
                    Class87.string_2 = "服务器返回信息异常！";
                    return false;
                }
                innerText = node.InnerText;
                if (innerText != "0000")
                {
                    Class101.smethod_1("（下载线程）发票下载失败！服务器端处理未完成（CODE值为：" + innerText + "）。");
                    if (string.IsNullOrEmpty(this.method_15(xmlDocument_1)))
                    {
                        this.method_22(xmlDocument_1, "已执行发票上传结果查询，服务器端未处理完成，发票报送状态未变更。", 3);
                    }
                    else
                    {
                        this.method_22(xmlDocument_1, "已执行发票上传结果查询，服务器端处理超时，发票报送状态已变更为未报送，即将重新上传该发票。", 0);
                    }
                    Class87.string_3 = "-0003";
                    Class87.string_2 = "服务器未处理完成！";
                    return true;
                }
                XmlNodeList childNodes = xmlDocument_0.GetElementsByTagName("FP_SUCC")[0].ChildNodes;
                if ((childNodes != null) && (childNodes.Count > 0))
                {
                    Class101.smethod_0("正常情况处理");
                    string str4 = "";
                    string str5 = "";
                    foreach (XmlNode node3 in childNodes)
                    {
                        if (string.Equals(node3.Name, "FPXX", StringComparison.CurrentCultureIgnoreCase))
                        {
                            str4 = node3.InnerText;
                        }
                        else if (string.Equals(node3.Name, "FLAG", StringComparison.CurrentCultureIgnoreCase))
                        {
                            str5 = node3.InnerText;
                        }
                    }
                    if (!string.IsNullOrEmpty(str5) && !string.IsNullOrEmpty(str4))
                    {
                        byte[] buffer = Convert.FromBase64String(str5);
                        int length = buffer.Length;
                        if ((buffer != null) && (buffer.Length > 0))
                        {
                            byte[] buffer2 = this.class81_0.method_13(buffer, str4);
                            int num2 = this.taxCard_0.UpdateInvUploadFlag(buffer2, length, str4);
                            if (this.taxCard_0.RetCode == 0)
                            {
                                this.class100_0.method_9(str4, str5, num2);
                                Class101.smethod_1(string.Concat(new object[] { "（下载线程）更新底层发票状态成功！   发票：", str4, "  iRe=", num2 }));
                                Class88.smethod_2();
                                Class87.string_3 = "0000";
                                Class87.string_2 = "发票报送成功！";
                                Class87.bool_1 = true;
                            }
                            else
                            {
                                Class87.string_3 = "-0004";
                                Class87.string_2 = "更新底层失败！";
                                Class101.smethod_1(string.Concat(new object[] { "（下载线程）更新底层发票状态失败！    发票：", str4, "  ErrorCode：", this.taxCard_0.RetCode, "   原密文串：", str5, "  处理后密文串：", ToolUtil.ToBase64String(buffer2) }));
                            }
                        }
                    }
                }
                XmlNodeList list2 = xmlDocument_0.GetElementsByTagName("FP_ERR")[0].ChildNodes;
                Class101.smethod_0("上传失败");
                if ((list2 != null) && (list2.Count > 0))
                {
                    Class101.smethod_0("开始失败处理");
                    foreach (XmlNode node4 in list2)
                    {
                        if (!string.Equals(node4.Name, "FP", StringComparison.CurrentCultureIgnoreCase))
                        {
                            continue;
                        }
                        XmlNodeList list3 = node4.ChildNodes;
                        if ((list3 == null) || (list3.Count < 1))
                        {
                            continue;
                        }
                        Class96 class2 = new Class96();
                        string str6 = string.Empty;
                        string str7 = string.Empty;
                        string[] strArray = new string[2];
                        Dictionary<string, object> item = new Dictionary<string, object>();
                        foreach (XmlNode node5 in list3)
                        {
                            if (string.Equals(node5.Name, "FPDM", StringComparison.CurrentCultureIgnoreCase))
                            {
                                strArray[0] = node5.InnerText;
                                class2.FPDM = node5.InnerText;
                            }
                            if (string.Equals(node5.Name, "FPHM", StringComparison.CurrentCultureIgnoreCase))
                            {
                                class2.FPNO = node5.InnerText;
                                strArray[1] = node5.InnerText;
                            }
                            if (string.Equals(node5.Name, "CODE", StringComparison.CurrentCultureIgnoreCase))
                            {
                                str6 = node5.InnerText;
                                Class87.string_3 = str6;
                            }
                            if (string.Equals(node5.Name, "MESS", StringComparison.CurrentCultureIgnoreCase))
                            {
                                str7 = node5.InnerText;
                                Class87.string_2 = str7;
                            }
                        }
                        class2 = this.class100_0.method_3(Class97.dataTable_0, class2.FPNO, class2.FPDM);
                        if (class2 == null)
                        {
                            continue;
                        }
                        string str8 = string.Empty;
                        int num3 = this.class81_0.method_5(class2.FPNO, class2.FPDM, class2.Fplx);
                        Class101.smethod_0("开始判断是否错误为39");
                        switch (str6)
                        {
                            case "2005":
                            case "91340002":
                            case "91250003":
                            case "39":
                                break;

                            default:
                                if (str6.Equals("2010"))
                                {
                                    Class101.smethod_0("进入错误2010处理");
                                    Class86.smethod_6(((("发票代码：" + class2.FPDM + "  发票号码：" + class2.FPNO + "  为异常票，") + Environment.NewLine) + "局端处理结果返回信息为：" + str7) + Environment.NewLine + "请手工作废该张发票！");
                                }
                                else if (!str6.Equals("2008"))
                                {
                                    Class101.smethod_0("其他错误处理");
                                    class2.FPSQHRecieveTime = DateTime.Now.ToString();
                                    class2.FPStatus = "2";
                                    class2.Boolean_0 = false;
                                    this.class100_0.method_1(class2, Enum10.Update);
                                    if (num3 >= (Class87.int_5 - 1))
                                    {
                                        str8 = "已执行发票上传结果查询，发票报送失败，发票报送状态已置为报送失败，服务器返回错误信息为：" + str7;
                                    }
                                    else
                                    {
                                        str8 = "已执行发票上传结果查询，发票报送失败，服务器返回错误信息为：" + str7 + ",发票报送状态已置为未报送，即将重新上传";
                                    }
                                }
                                else
                                {
                                    DateTime time3;
                                    Class101.smethod_0("进入错误2008处理");
                                    if (DateTime.TryParse(class2.FpUploadTime, out time3) && (time3.AddMinutes((double) Class87.int_4) > DateTime.Now))
                                    {
                                        str8 = "已执行发票上传结果查询，发票报送状态未更改，服务器返回错误信息为：" + str7 + "，系统将会过15s重新查询该发票处理状态";
                                    }
                                    else
                                    {
                                        class2.FPSQHRecieveTime = DateTime.Now.ToString();
                                        class2.FPStatus = "0";
                                        class2.Boolean_0 = false;
                                        this.class100_0.method_1(class2, Enum10.Update);
                                        str8 = "已执行发票上传结果查询，服务器处理结果超时，发票报送状态已置为未报送，系统即将重新上传该发票";
                                    }
                                }
                                goto Label_0863;
                        }
                        Class101.smethod_0("sFpCode: " + str6.ToString());
                        class2.FPSQHRecieveTime = DateTime.Now.ToString();
                        class2.FPStatus = "4";
                        class2.Boolean_0 = false;
                        this.class100_0.method_1(class2, Enum10.Update);
                        str8 = "已执行发票上传结果查询，发票验签失败，服务器返回错误信息为：" + str7 + "，发票报送状态已置为验签失败。";
                        Class101.smethod_0("发票类型：" + class2.Fplx.ToString());
                        if (class2.Fplx == FPLX.DZFP.ToString())
                        {
                            Class101.smethod_0("Start ZF");
                            Class101.smethod_0("After ZF：" + this.class81_0.method_9(class2.FPNO, class2.FPDM, class2.Fplx).ToString());
                        }
                        else
                        {
                            this.class81_0.method_7(class2.FPNO, class2.FPDM, class2.Fplx, Class87.int_5 - 1);
                        }
                        if (Class87.bool_2)
                        {
                            Class87.string_3 = "-0006";
                            Class87.string_2 = str8;
                        }
                        else if (class2.Fplx != FPLX.DZFP.ToString())
                        {
                            Class86.smethod_0(strArray[0], strArray[1]);
                        }
                    Label_0863:
                        if ((class2.Fplx == FPLX.DZFP.ToString()) && (((str6 == "2005") || (str6 == "91340002")) || (str6 == "91250003")))
                        {
                            Class101.smethod_0("Ele 多一条件的分支，无实际代码");
                        }
                        else
                        {
                            item.Add("FPDM", class2.FPDM);
                            int result = 0;
                            if (int.TryParse(class2.FPNO, out result))
                            {
                                item.Add("FPHM", result);
                            }
                            if (class2.Fplx.Equals(FPLX.PTFP.ToString()))
                            {
                                item.Add("FPZL", "c");
                            }
                            else if (class2.Fplx.Equals(FPLX.ZYFP.ToString()))
                            {
                                item.Add("FPZL", "s");
                            }
                            else if (class2.Fplx.Equals(FPLX.HYFP.ToString()))
                            {
                                item.Add("FPZL", "f");
                            }
                            else if (class2.Fplx.Equals(FPLX.JDCFP.ToString()))
                            {
                                item.Add("FPZL", "j");
                            }
                            else if (class2.Fplx.Equals(FPLX.DZFP.ToString()))
                            {
                                item.Add("FPZL", "p");
                            }
                            else
                            {
                                item.Add("FPZL", class2.Fplx);
                            }
                            item.Add("BSRZ", str8);
                            if (str8.IndexOf("验签失败") > -1)
                            {
                                item.Add("BSZT", 4);
                            }
                            else if (str8.IndexOf("未报送") > -1)
                            {
                                item.Add("BSZT", 0);
                            }
                            else if (str8.IndexOf("已置为报送失败") > -1)
                            {
                                item.Add("BSZT", 2);
                            }
                            else
                            {
                                item.Add("BSZT", 3);
                            }
                            if (item.Count >= 4)
                            {
                                Class87.list_1.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return true;
        }
    }
}

