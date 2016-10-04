namespace Aisino.Framework.MainForm.UpDown
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using ns4;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    public class ServerUP
    {
        private Class84 class84_0;
        private IBaseDAO ibaseDAO_0;
        private int int_0;
        private string string_0;
        private TaxCard taxCard_0;

        public ServerUP()
        {
            
            this.string_0 = "0000";
            this.ibaseDAO_0 = BaseDAOFactory.GetBaseDAOSQLite();
            this.class84_0 = new Class84();
            this.taxCard_0 = TaxCard.CreateInstance(CTaxCardType.const_7);
        }

        public void DonwloadInInterface(string string_1)
        {
            if (string_1 == "")
            {
                Class87.xmlDocument_1 = this.class84_0.method_26(0);
            }
            else
            {
                try
                {
                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    parameter.Add("SLXLH", string_1);
                    DataTable table = this.ibaseDAO_0.querySQLDataTable("Aisino.Framework.MainForm.UpDown.SelectFPsBySLH", parameter);
                    if ((table != null) && (table.Rows.Count >= 1))
                    {
                        string str = this.method_6(table);
                        string str2 = "";
                        if (HttpsSender.SendMsg(this.string_0, str, out str2) == 0)
                        {
                            this.method_1(str2);
                        }
                        string str3 = "";
                        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                        List<Dictionary<string, object>> list2 = new List<Dictionary<string, object>>();
                        Dictionary<string, object> item = new Dictionary<string, object>();
                        Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                        foreach (DataRow row in table.Rows)
                        {
                            item = new Dictionary<string, object>();
                            dictionary3 = new Dictionary<string, object>();
                            if (row["BSZT"].ToString() == "1")
                            {
                                string str4 = str3;
                                str3 = str4 + row["FPDM"].ToString() + "," + row["FPHM"].ToString() + ";";
                                dictionary3.Add("FPDM", row["FPDM"]);
                                dictionary3.Add("FPHM", row["FPHM"]);
                                dictionary3.Add("FPZL", row["FPZL"]);
                            }
                            else
                            {
                                item.Add("FPDM", row["FPDM"]);
                                item.Add("FPHM", row["FPHM"]);
                                item.Add("FPZL", row["FPZL"]);
                                item.Add("BSRZ", row["BSRZ"]);
                            }
                            if (dictionary3.Count > 0)
                            {
                                list2.Add(dictionary3);
                            }
                            if (item.Count > 0)
                            {
                                list.Add(item);
                            }
                        }
                        if (str3 != "")
                        {
                            str3 = str3.Trim(new char[] { ';' });
                            this.ibaseDAO_0.未确认DAO方法3("Aisino.Framework.MainForm.UpDown.DeleteFPSLH", list2);
                            Class87.bool_1 = true;
                            Class87.string_0 = "0000";
                            Class87.string_2 = "处理完成！";
                        }
                        this.method_4(str3, list);
                    }
                }
                catch (Exception exception)
                {
                    Class87.bool_1 = false;
                    Class87.string_0 = "0000";
                    Class87.string_2 = "处理完成！";
                    Class101.smethod_1("DonwloadInInterface:" + exception.ToString());
                }
            }
        }

        private string method_0(string string_1)
        {
            string str = "";
            try
            {
                List<Fpxx> list = new GetFpInfoDal().GetFpInfo(string_1, Class87.int_1.ToString(), UpdateTransMethod.BszAndWbs, "", "", "", "");
                if ((list == null) || (list.Count < 1))
                {
                    return str;
                }
                XmlDocument document = new XmlDocument();
                document.AppendChild(document.CreateXmlDeclaration("1.0", "GBK", ""));
                XmlNode newChild = document.CreateNode(XmlNodeType.Element, "INPUT", "");
                document.AppendChild(newChild);
                foreach (Fpxx fpxx in list)
                {
                    XmlNode node2 = document.CreateNode(XmlNodeType.Element, "FP", "");
                    newChild.AppendChild(node2);
                    XmlElement element = document.CreateElement("FPDM");
                    element.InnerText = fpxx.fpdm;
                    node2.AppendChild(element);
                    XmlElement element2 = document.CreateElement("FPHM");
                    element2.InnerText = fpxx.fphm;
                    node2.AppendChild(element2);
                    XmlElement element3 = document.CreateElement("FPZL");
                    switch (fpxx.fplx)
                    {
                        case FPLX.ZYFP:
                            element3.InnerText = "s";
                            break;

                        case FPLX.PTFP:
                            element3.InnerText = "c";
                            break;

                        case FPLX.HYFP:
                            element3.InnerText = "f";
                            break;

                        case FPLX.JDCFP:
                            element3.InnerText = "j";
                            break;

                        case FPLX.JSFP:
                            element3.InnerText = "q";
                            break;

                        case FPLX.DZFP:
                            element3.InnerText = "p";
                            break;

                        default:
                            element3.InnerText = fpxx.fplx.ToString();
                            break;
                    }
                    node2.AppendChild(element3);
                }
                return document.InnerXml;
            }
            catch (Exception exception)
            {
                Class101.smethod_1("GetUPFPInfo:" + exception.ToString());
            }
            return str;
        }

        private void method_1(string string_1)
        {
            if (string_1 != "")
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    document.LoadXml(string_1);
                    List<string> list = new List<string>();
                    List<Dictionary<string, object>> parameter = new List<Dictionary<string, object>>();
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    XmlNodeList elementsByTagName = document.GetElementsByTagName("FP");
                    if ((elementsByTagName != null) && (elementsByTagName.Count >= 1))
                    {
                        foreach (XmlNode node in elementsByTagName)
                        {
                            XmlNodeList childNodes = node.ChildNodes;
                            if ((childNodes != null) && (childNodes.Count >= 1))
                            {
                                item = new Dictionary<string, object>();
                                foreach (XmlNode node2 in childNodes)
                                {
                                    switch (node2.Name)
                                    {
                                        case "FPDM":
                                        {
                                            item.Add("FPDM", node2.InnerText);
                                            continue;
                                        }
                                        case "FPHM":
                                        {
                                            item.Add("FPHM", node2.InnerText);
                                            continue;
                                        }
                                        case "FPZL":
                                        {
                                            item.Add("FPZL", node2.InnerText);
                                            continue;
                                        }
                                        case "BSZT":
                                        {
                                            if (!(node2.InnerText == "2"))
                                            {
                                                break;
                                            }
                                            item.Add("BSZT", "0");
                                            continue;
                                        }
                                        case "BSRZ":
                                        {
                                            item.Add("BSRZ", node2.InnerText);
                                            continue;
                                        }
                                        case "ZFBZ":
                                        {
                                            item.Add("ZFBZ", node2.InnerText);
                                            continue;
                                        }
                                        case "ZFRQ":
                                        {
                                            item.Add("ZFRQ", node2.InnerText);
                                            continue;
                                        }
                                        default:
                                        {
                                            continue;
                                        }
                                    }
                                    item.Add("BSZT", node2.InnerText);
                                }
                                if (item["BSZT"].ToString() == "4")
                                {
                                    if (Class87.bool_2)
                                    {
                                        Class87.string_3 = "-0009";
                                        Class87.string_2 = "验签失败，系统已自动作废该发票，稍后系统会重新上传该张发票！";
                                    }
                                    if (item["FPZL"].ToString() != "p")
                                    {
                                        string str2 = string.Concat(new object[] { "发票(发票代码：", item["FPDM"], "   发票号码：", item["FPHM"], ")验签失败，请手工作废该发票！或者稍后进行发票修复重新同步该发票报送状态。" });
                                        if (!Class87.bool_2)
                                        {
                                            MessageBoxHelper.Show(str2, "发票验签失败", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                        }
                                    }
                                    else if (item["FPZL"].ToString() == "p")
                                    {
                                        item["BSZT"] = "0";
                                        item["BSRZ"] = "该发票验签失败，服务器已自动作废该发票，客户端报送状态已置为未报送，稍后会从服务器重新同步报送状态！";
                                        string str3 = string.Concat(new object[] { "发票(发票代码：", item["FPDM"], "   发票号码：", item["FPHM"], ")验签失败，服务器已自动作废该发票，客户端报送状态已置为未报送，稍后会从服务器重新同步报送状态！" });
                                        if (!Class87.bool_2)
                                        {
                                            MessageBoxHelper.Show(str3, "发票验签失败", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                        }
                                    }
                                }
                                if ((item["FPZL"].ToString() == "p") && (item["ZFBZ"].ToString() == "1"))
                                {
                                    if (item["BSZT"].ToString() == "1")
                                    {
                                        string str4 = string.Concat(new object[] { "电子增值税普通发票(发票代码：", item["FPDM"], "   发票号码：", item["FPHM"], ")由于验签失败，服务器端已自动作废该发票，并且重新上传成功，状态置为已报送。" });
                                        if (!Class87.bool_2)
                                        {
                                            MessageBoxHelper.Show(str4, "电子增值税普通发票自动作废提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                        }
                                    }
                                    new Class84().method_29(item["FPDM"].ToString(), item["FPHM"].ToString());
                                }
                                list.Add("Aisino.Framework.MainForm.UpDown.UpdateFPWithResultFromKPServer");
                                parameter.Add(item);
                            }
                        }
                        if ((list.Count > 0) && (parameter.Count == list.Count))
                        {
                            this.ibaseDAO_0.未确认DAO方法1(list.ToArray(), parameter);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("AnalysInfoFromServer:" + exception.ToString());
                }
            }
        }

        private int method_2()
        {
            int num = -1;
            try
            {
                Class100 class2 = new Class100();
                num = class2.method_10(UpdateTransMethod.WBS) + class2.method_10(UpdateTransMethod.BSZ);
                if (num > 0)
                {
                    num = (num / Class87.int_1) + 1;
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_1("GetPageNum:" + exception.ToString());
            }
            return num;
        }

        private void method_3(DataTable dataTable_0)
        {
            if ((dataTable_0 != null) && (dataTable_0.Rows.Count >= 1))
            {
                try
                {
                    List<string> slxlhs = new List<string>();
                    List<string> list2 = new List<string>();
                    List<Dictionary<string, object>> parameter = new List<Dictionary<string, object>>();
                    List<Dictionary<string, string>> listFP = new List<Dictionary<string, string>>();
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                    string str = this.method_5();
                    string str2 = "";
                    foreach (DataRow row in dataTable_0.Rows)
                    {
                        item = new Dictionary<string, object>();
                        dictionary2 = new Dictionary<string, string>();
                        if (row["BSZT"].ToString() == "0")
                        {
                            dictionary2.Add("FPDM", row["FPDM"].ToString());
                            dictionary2.Add("FPHM", row["FPHM"].ToString());
                            dictionary2.Add("FPZL", row["FPZL"].ToString());
                            dictionary2.Add("BSRZ", row["BSRZ"].ToString());
                            str2 = row["BSRZ"].ToString();
                        }
                        else
                        {
                            item.Add("FPDM", row["FPDM"]);
                            item.Add("FPHM", row["FPHM"]);
                            item.Add("FPZL", row["FPZL"]);
                            item.Add("ZFBZ", row["ZFBZ"]);
                            item.Add("SCSJ", DateTime.Now.ToString());
                            item.Add("FPSLH", str);
                            str2 = row["BSRZ"].ToString();
                        }
                        if (dictionary2.Count > 0)
                        {
                            listFP.Add(dictionary2);
                        }
                        if (item.Count > 0)
                        {
                            parameter.Add(item);
                            list2.Add("Aisino.Framework.MainForm.UpDown.replaceFPSLH");
                        }
                    }
                    if (parameter.Count > 0)
                    {
                        slxlhs.Add(str);
                    }
                    if ((list2.Count > 0) && (this.ibaseDAO_0.未确认DAO方法1(list2.ToArray(), parameter) > 0))
                    {
                        this.class84_0.method_23(slxlhs, listFP);
                        Class87.bool_0 = true;
                        Class87.string_0 = "0000";
                        Class87.string_1 = str;
                    }
                    else
                    {
                        slxlhs.Clear();
                        foreach (Dictionary<string, object> dictionary3 in parameter)
                        {
                            Dictionary<string, string> dictionary4 = new Dictionary<string, string>();
                            foreach (KeyValuePair<string, object> pair in dictionary3)
                            {
                                dictionary4.Add(pair.Key, pair.Value.ToString());
                            }
                            listFP.Add(dictionary4);
                        }
                        this.class84_0.method_23(slxlhs, listFP);
                        Class87.bool_0 = false;
                        Class87.string_0 = "-0005";
                        Class87.string_1 = str2;
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("AnalysDatatbleFromXXFP:" + exception.ToString());
                }
            }
        }

        private void method_4(string string_1, List<Dictionary<string, object>> list_0)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.AppendChild(document.CreateXmlDeclaration("1.0", "GBK", ""));
                XmlElement newChild = document.CreateElement("FPXT");
                document.AppendChild(newChild);
                XmlElement element2 = document.CreateElement("OUTPUT");
                newChild.AppendChild(element2);
                XmlElement element3 = document.CreateElement("CODE");
                element3.InnerText = "0000";
                element2.AppendChild(element3);
                XmlElement element4 = document.CreateElement("MESS");
                element4.InnerText = "处理成功！";
                element2.AppendChild(element4);
                XmlElement element5 = document.CreateElement("DATA");
                element2.AppendChild(element5);
                XmlElement element6 = document.CreateElement("NSRSBH");
                element6.InnerText = this.taxCard_0.TaxCode;
                element5.AppendChild(element6);
                XmlElement element7 = document.CreateElement("KPJH");
                element7.InnerText = this.taxCard_0.Machine.ToString();
                element5.AppendChild(element7);
                XmlElement element8 = document.CreateElement("SBBH");
                element8.InnerText = this.taxCard_0.GetInvControlNum();
                element5.AppendChild(element8);
                XmlElement element9 = document.CreateElement("FP_SUCC");
                element5.AppendChild(element9);
                XmlElement element10 = document.CreateElement("FPXX");
                element10.InnerText = string_1;
                element9.AppendChild(element10);
                XmlElement element11 = document.CreateElement("FP_ERR");
                element5.AppendChild(element11);
                foreach (Dictionary<string, object> dictionary in list_0)
                {
                    XmlElement element12 = document.CreateElement("FP");
                    element11.AppendChild(element12);
                    XmlElement element13 = document.CreateElement("FPDM");
                    element13.InnerText = dictionary["FPDM"].ToString();
                    element12.AppendChild(element13);
                    XmlElement element14 = document.CreateElement("FPHM");
                    element14.InnerText = dictionary["FPHM"].ToString();
                    element12.AppendChild(element14);
                    XmlElement element15 = document.CreateElement("CODE");
                    element15.InnerText = "0001";
                    element12.AppendChild(element15);
                    XmlElement element16 = document.CreateElement("MESS");
                    element16.InnerText = dictionary["BSRZ"].ToString();
                    element12.AppendChild(element16);
                }
                Class87.xmlDocument_1 = document;
                Class87.string_3 = "0000";
                Class87.string_2 = "处理完成";
            }
            catch (Exception exception)
            {
                Class101.smethod_1("GetDownloadInfoInInterface:" + exception.ToString());
            }
        }

        private string method_5()
        {
            return Guid.NewGuid().ToString();
        }

        private string method_6(DataTable dataTable_0)
        {
            string innerXml = "";
            if ((dataTable_0 != null) && (dataTable_0.Rows.Count >= 1))
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    document.AppendChild(document.CreateXmlDeclaration("1.0", "GBK", ""));
                    XmlNode newChild = document.CreateNode(XmlNodeType.Element, "INPUT", "");
                    document.AppendChild(newChild);
                    foreach (DataRow row in dataTable_0.Rows)
                    {
                        XmlNode node2 = document.CreateNode(XmlNodeType.Element, "FP", "");
                        newChild.AppendChild(node2);
                        XmlElement element = document.CreateElement("FPDM");
                        element.InnerText = row["FPDM"].ToString();
                        node2.AppendChild(element);
                        XmlElement element2 = document.CreateElement("FPHM");
                        element2.InnerText = row["FPHM"].ToString();
                        node2.AppendChild(element2);
                        XmlElement element3 = document.CreateElement("FPZL");
                        element3.InnerText = row["FPZL"].ToString();
                        node2.AppendChild(element3);
                    }
                    innerXml = document.InnerXml;
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("GetUPFPInfo:" + exception.ToString());
                }
            }
            return innerXml;
        }

        public void TestIsServerUp()
        {
            Class101.smethod_0("测试是不是服务器上传方式");
            try
            {
                Class87.bool_5 = this.taxCard_0.GetExtandParams("FLBMFlag") == "FLBM";
                XmlDocument document = new XmlDocument();
                document.AppendChild(document.CreateXmlDeclaration("1.0", "GBK", ""));
                XmlElement newChild = document.CreateElement("ISSERVERUP");
                document.AppendChild(newChild);
                string str = "";
                if (HttpsSender.SendMsg(this.string_0, document.InnerXml, out str) == 0)
                {
                    XmlDocument document2 = new XmlDocument();
                    document2.LoadXml(str);
                    XmlNodeList elementsByTagName = document2.GetElementsByTagName("ISSERVERUP");
                    if (((elementsByTagName != null) && (elementsByTagName.Count > 0)) && (elementsByTagName[0].InnerText.Trim() == "1"))
                    {
                        Class87.bool_4 = true;
                        Class101.smethod_0("服务器上传方式：" + elementsByTagName[0].InnerText);
                    }
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_0("TestIsServerUp：" + exception.ToString());
            }
            Class101.smethod_0("结束测试是不是服务器上传方式");
        }

        public void UPDownLoadFP()
        {
            try
            {
                string str;
                this.int_0 = this.method_2();
                if (this.int_0 < 1)
                {
                    Class101.smethod_0("UPDownLoadFP begin:" + this.int_0 + "  没有需要上传数据，退出UPDownLoadFP");
                    return;
                }
                Class101.smethod_0("UPDownLoadFP begin:" + this.int_0);
                int num = 0;
                while (num < this.int_0)
                {
                    Thread.Sleep(0x7d0);
                    string str2 = this.method_0((num + 1).ToString());
                    if (str2.Trim() == "")
                    {
                        break;
                    }
                    str = "";
                    if (HttpsSender.SendMsg(this.string_0, str2, out str) != 0)
                    {
                        goto Label_00F3;
                    }
                    Class101.smethod_0(string.Concat(new object[] { "UPDownLoadFP ", num, "  服务器返回数据:", str }));
                    this.method_1(str);
                    num++;
                }
                goto Label_0128;
            Label_00F3:;
                Class101.smethod_0(string.Concat(new object[] { "UPDownLoadFP ", num, " 服务器返回错误信息:", str }));
            Label_0128:
                Class101.smethod_0("UPDownLoadFP over:");
            }
            catch (Exception exception)
            {
                Class101.smethod_1("UPDownLoadFP:" + exception.ToString());
            }
        }

        public void UPloadInPatch(string string_1)
        {
            Class84 class2 = new Class84();
            if (string_1 == "")
            {
                Class87.xmlDocument_0 = class2.method_24();
            }
            else
            {
                try
                {
                    string[] strArray = string_1.Split(new char[] { ';' });
                    if ((strArray != null) && (strArray.Length >= 1))
                    {
                        string str = "'0'";
                        foreach (string str3 in strArray)
                        {
                            if (!(str3 == ""))
                            {
                                str = str + ",'" + str3 + "'";
                            }
                        }
                        string str2 = "SELECT FPDM,FPHM,FPZL,BSZT,BSRZ,ZFBZ FROM XXFP WHERE FPDM||','||FPHM||','||FPZL IN (" + str + ")";
                        DataTable table = this.ibaseDAO_0.querySQLDataTable(str2);
                        this.method_3(table);
                    }
                    else
                    {
                        Class101.smethod_0("UPloadInPatch:未获取到发票信息");
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("UPloadInPatch:" + exception.ToString());
                }
            }
        }

        public void UPLoadSingleInInterface(string string_1, string string_2, string string_3)
        {
            if ((!(string_1 == "") && !(string_2 == "")) && !(string_3 == ""))
            {
                string str = string_1 + "," + string_2 + "," + string_3;
                this.UPloadInPatch(str);
            }
            else
            {
                Class87.bool_0 = false;
                Class87.string_0 = "-0001";
                Class87.string_1 = "没有传递需要上传的发票信息！";
            }
        }
    }
}

