namespace Aisino.Framework.Plugin.Core.Https
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Http;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml;

    public class HttpsSender
    {
        private static Dictionary<string, string> dictionary_0;
        private static ILog ilog_0;
        private static List<string> list_0;
        private static List<string> list_1;
        public static int resopnseSize;
        private static string string_0;
        private static string string_1;
        private static string string_2;
        private static string string_3;
        private static string string_4;
        private static string string_5;
        private static string string_6;
        private static string string_7;
        private static TaxCard taxCard_0;

        static HttpsSender()
        {
            
            string_0 = string.Empty;
            string_1 = string.Empty;
            taxCard_0 = TaxCardFactory.CreateTaxCard();
            string_2 = taxCard_0.TaxCode;
            string_3 = PropertyUtil.GetValue("Login_UserName", "KP_USER");
            ilog_0 = LogUtil.GetLogger<HttpsSender>();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("0000", "");
            dictionary.Add("0001", "zzs_fp_cgl_cj,jsp_dzdz_cb_zzszp");
            dictionary.Add("0003", "zzs_fp_cgl_cj,jsp_dzdz_cb_zzszp");
            dictionary.Add("0004", "zzs_fp_cgl_cj,jsp_dzdz_cb_zzszp");
            dictionary.Add("0005", "zzs_fp_cgl_cj,jsp_dzdz_cb_hyfp");
            dictionary.Add("0007", "zzs_fp_cgl_cj,jsp_dzdz_cb_zzszp");
            dictionary.Add("0008", "zzs_fp_cgl_cj,jsp_dzdz_cb_zzszp");
            dictionary.Add("0009", "zzs_fp_cgl_cj,jsp_dzdz_cb_hyfp");
            dictionary.Add("0010", "zzs_fp_cgl_cj,jsp_dzdz_cb_zzszp");
            dictionary.Add("0011", "zzs_fp_cgl_cj,jsp_dzdz_cb_hyfp");
            dictionary.Add("0012", "zzs_fp_cgl_cj,dzdz_xiazai_zzszp");
            dictionary.Add("0013", "zzs_fp_cgl_cj,jsp_dzdz_cb_zzszp");
            dictionary.Add("0014", "zzs_fp_cgl_cj,jsp_dzdz_cb_zzszp");
            dictionary.Add("0015", "zzs_fp_cgl_cj,jsp_dzdz_cb_hyfp");
            dictionary.Add("0016", "zzs_fp_cgl_cj,jsp_dzfp_cb");
            dictionary.Add("0017", "zzs_fp_cgl_cj,jsp_dzdz_cb_hyfp");
            dictionary.Add("0018", "zzs_fp_cgl_cj,jsp_dzfp_cb");
            dictionary.Add("0019", "zzs_fp_cgl_cj,dzdz_gpxx_xiazai_zzszp");
            dictionary.Add("0020", "zzs_fp_cgl_cj,dzdz_gpxx_xiazai_zzszp");
            dictionary.Add("0021", "zzs_fp_cgl_cj,dzdz_gpxx_shenqing_zzszp");
            dictionary.Add("0022", "zzs_fp_cgl_cj,dzdz_gpxx_shenqing_zzszp");
            dictionary.Add("0023", "zzs_fp_cgl_cj,dzdz_gpxx_shenqing_zzszp");
            dictionary.Add("0024", "zzs_fp_cgl_cj,dzdz_gpxx_shenqing_hyfp");
            dictionary.Add("0025", "zzs_fp_cgl_cj,dzdz_gpxx_shenqing_hyfp");
            dictionary.Add("0026", "zzs_fp_cgl_cj,dzdz_gpxx_shenqing_hyfp");
            dictionary.Add("0027", "zzs_fp_cgl_cj,dzdz_xiazai_zzszp");
            dictionary.Add("0028", "zzs_fp_cgl_cj,dzdz_xiazai_zzszp");
            dictionary.Add("0029", "zzs_fp_cgl_cj,jsp_skskj_cb");
            dictionary.Add("0030", "zzs_fp_cgl_cj,jsp_skskj_cb");
            dictionary.Add("0031", "zzs_fp_cgl_cj,jsp_dzfp_cb");
            dictionary.Add("0032", "zzs_fp_cgl_cj,jsp_dzfp_cb");
            dictionary.Add("0033", "zzs_fp_cgl_cj,jsp_dzfp_cb");
            dictionary.Add("0034", "zzs_fp_cgl_cj,dzdz_gpxx_xiazai_zzszp");
            dictionary.Add("0035", "nssb_fpcy,dzdz_gffp_cy");
            dictionary.Add("0036", "zzs_fp_cgl_cj,dzdz_gpxx_shenqing_zzszp");
            dictionary.Add("0037", "zzs_fp_cgl_cj,jsp_dzdz_cb_zzszp");
            dictionary.Add("0038", "zzs_fp_cgl_cj,dzdz_gpxx_shenqing_zzszp");
            dictionary.Add("0039", "zzs_fp_cgl_cj,dzdz_gpxx_shenqing_hyfp");
            dictionary.Add("0040", "zzs_fp_cgl_cj,dzdz_gpxx_xiazai_zzszp");
            dictionary.Add("0041", "zzs_fp_cgl_cj,dzdz_gpxx_shenqing_zzszp");
            dictionary.Add("0042", "zzs_fp_cgl_cj,dzdz_gpxx_shenqing_hyfp");
            dictionary.Add("0043", "zzs_fp_cgl_cj,dzdz_gpxx_shenqing_zzszp");
            dictionary.Add("0044", "zzs_fp_cgl_cj,dzdz_gpxx_shenqing_zzszp");
            dictionary_0 = dictionary;
            list_0 = new List<string> { "0005", "0009", "0011", "0015", "0017", "0024", "0025", "0026", "0035" };
            list_1 = new List<string> { "0005", "0009", "0011", "0015", "0017", "0024", "0025", "0026", "0016", "0018", "0029", "0035", "0039", "0042" };
            resopnseSize = 0x500000;
            string_4 = "";
            string_5 = "1";
            string_6 = "-960011100,-960011101,-960011102,-960011103,-960011104,-960011105,-960011107,-960011108,-960011109,-960011110,-960011111,-960011112,-960011113,-960032031,-960032032,-960032034,-960032035,-960032036,-960011120";
            string_7 = "-96003";
        }

        public HttpsSender()
        {
            
        }

        [DllImport("uniAcceptFramework.dll")]
        private static extern int aisino_communityFramework_call(byte[] byte_0, int int_0, byte[] byte_1, int int_1, byte[] byte_2, ref int int_2, byte[] byte_3);
        public static string GetInvSignServer()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "InvSignServer.txt");
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            return "Card";
        }

        public static int SendMsg(string string_8, string string_9, out string string_10)
        {
            Dictionary<string, object> dictionary = null;
            if (!dictionary_0.ContainsKey(string_8))
            {
                string_10 = "错误的操作类型(" + string_8 + ")";
                return -1;
            }
            byte[] buffer = new byte[HttpsSender.resopnseSize];
            int resopnseSize = HttpsSender.resopnseSize;
            try
            {
                object obj2;
                object obj3;
                object obj4;
                object obj5;
                object obj6;
                object obj7;
                object obj8;
                if (string_8.Equals("0000"))
                {
                    int num2 = 0;
                    using (XmlTextReader reader = new XmlTextReader(new MemoryStream(ToolUtil.GetBytes(string_9))))
                    {
                        while (reader.Read())
                        {
                            if (reader.IsStartElement() && string.Equals("ISSERVERUP", reader.LocalName))
                            {
                                num2 = 1;
                            }
                        }
                    }
                    if (num2 == 1)
                    {
                        string_10 = "<?xml version=\"1.0\" encoding=\"GBK\"?><ISSERVERUP>" + (string.Equals(taxCard_0.SoftVersion, "FWKP_V2.0_Svr_Client") ? string_5 : "0") + "</ISSERVERUP>";
                        return 0;
                    }
                    if (string.Equals(taxCard_0.SoftVersion, "FWKP_V2.0_Svr_Client"))
                    {
                        byte[] buffer2 = new byte[0x400];
                        int num4 = smethod_0(ToolUtil.GetBytes("FPZT_UPDATE"), ToolUtil.GetBytes(string_9), out buffer, out resopnseSize, out buffer2, "getStateFromServe");
                        string_10 = ToolUtil.GetString(buffer, 0, resopnseSize);
                        if (num4 != 0)
                        {
                            string_10 = string_10 + "," + ToolUtil.GetString(buffer2).Trim(new char[1]);
                        }
                        return num4;
                    }
                }
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.QueryDZDZInfo", new object[0]);
                if (objArray == null)
                {
                    string_10 = "读取服务地址失败";
                    return -1;
                }
                dictionary = objArray[0] as Dictionary<string, object>;
                if (dictionary == null)
                {
                    string_10 = "读取服务器地址配置信息失败";
                    return -1;
                }
                dictionary.TryGetValue("ACCEPT_WEB_SERVER", out obj2);
                dictionary.TryGetValue("proxyType", out obj3);
                dictionary.TryGetValue("proxyHost", out obj4);
                dictionary.TryGetValue("proxyPort", out obj5);
                dictionary.TryGetValue("proxyAuthType", out obj6);
                dictionary.TryGetValue("proxyAuthUser", out obj7);
                dictionary.TryGetValue("proxyAuthPassword", out obj8);
                string str = obj2 as string;
                if ((str != null) && (str.IndexOf("127.0.0.1") < 0))
                {
                    if (string.Equals(str + "," + (obj4 as string) + "," + (obj7 as string), string_1))
                    {
                        ilog_0.Error("因数字证书异常，已停止网络连接。请修改安全接入服务器地址或重启开票软件！");
                        string_10 = string_0;
                        return -1;
                    }
                    byte[] bytes = null;
                    int length = 0;
                    string str2 = "";
                    string str3 = "N";
                    if (list_0.Contains(string_8))
                    {
                        str2 = string_9;
                        bytes = ToolUtil.GetBytes(string_9);
                        length = bytes.Length;
                    }
                    else if (!smethod_3(ref string_8, string_9, out bytes, out length))
                    {
                        int result = 0x7d000;
                        int.TryParse(PropertyUtil.GetValue("COMPRESS_SIZE", "512000"), out result);
                        byte[] buffer4 = ToolUtil.GetBytes(string_9);
                        if (buffer4.Length > result)
                        {
                            buffer4 = ZipUtil.Compress(buffer4);
                            str3 = "Y";
                        }
                        string hashString = MD5_Crypt.GetHashString(buffer4);
                        string str6 = Convert.ToBase64String(buffer4);
                        bytes = ToolUtil.GetBytes(new StringBuilder().Append("<?xml version=\"1.0\" encoding=\"GBK\"?>").Append("<ZZSFPXT><SK_TYPE>JSP</SK_TYPE><OP_TYPE>").Append(string_8).Append("</OP_TYPE><INPUT>").Append("<NSRSBH>").Append(taxCard_0.TaxCode).Append("</NSRSBH>").Append("<KPJH>").Append(taxCard_0.Machine).Append("</KPJH>").Append("<SBBH>").Append(taxCard_0.GetInvControlNum()).Append("</SBBH><YSBZ>").Append(str3).Append("</YSBZ><DATA>").Append(str6).Append("</DATA><CRC>").Append(hashString).Append("</CRC>").Append("</INPUT></ZZSFPXT>").ToString());
                        length = bytes.Length;
                    }
                    int num7 = -1;
                    byte[] buffer5 = new byte[0x400];
                    if (((taxCard_0.TaxCode.Length == 15) && taxCard_0.TaxCode.Substring(8, 2).ToUpper().Equals("DK")) && !str.StartsWith("https"))
                    {
                        buffer = new WebClient().Post_Byte(str, bytes, out num7);
                        resopnseSize = buffer.Length;
                    }
                    else
                    {
                        string[] strArray2 = dictionary_0[string_8].Split(new char[] { ',' });
                        XmlDocument document = new XmlDocument();
                        XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "UTF-8", null);
                        document.AppendChild(newChild);
                        XmlElement element = document.CreateElement("tripTechnologyPackage");
                        element.SetAttribute("version", "1.0");
                        document.AppendChild(element);
                        XmlElement element2 = document.CreateElement("identity");
                        element.AppendChild(element2);
                        XmlElement element3 = document.CreateElement("SSL");
                        element3.InnerText = str.StartsWith("https") ? "TA" : "OA";
                        element2.AppendChild(element3);
                        XmlElement element4 = document.CreateElement("applicationId");
                        element4.InnerText = strArray2[0];
                        element2.AppendChild(element4);
                        XmlElement element5 = document.CreateElement("serviceId");
                        element5.InnerText = strArray2[1];
                        element2.AppendChild(element5);
                        XmlElement element6 = document.CreateElement("serviceURI");
                        element6.InnerText = str;
                        element2.AppendChild(element6);
                        XmlElement element7 = document.CreateElement("nsrsbh");
                        element7.InnerText = string_2;
                        element2.AppendChild(element7);
                        XmlElement element8 = document.CreateElement("senderName");
                        element8.InnerText = string_3;
                        element2.AppendChild(element8);
                        XmlElement element9 = document.CreateElement("showDigigalCertSelectDialog");
                        element9.InnerText = "FALSE";
                        element2.AppendChild(element9);
                        XmlElement element10 = document.CreateElement("logEnabled");
                        element10.InnerText = string.Equals(PropertyUtil.GetValue("TMP_SA", "0"), "0") ? "FALSE" : "TRUE";
                        element2.AppendChild(element10);
                        XmlElement element11 = document.CreateElement("deviceId");
                        if ((taxCard_0.TaxCode.Length == 12) && (taxCard_0.TaxCode.StartsWith("99") || taxCard_0.TaxCode.StartsWith("98")))
                        {
                            element11.InnerText = "";
                        }
                        else
                        {
                            element11.InnerText = PropertyUtil.GetValue("HIGH_DEVICE", "").Equals("") ? ("*44" + taxCard_0.GetInvControlNum()) : "";
                        }
                        element2.AppendChild(element11);
                        if (taxCard_0.GetExtandParams("UPLOADTYPE").Equals("SSL_UP"))
                        {
                            XmlElement element12 = document.CreateElement("p11Driver");
                            element12.InnerText = "upkcs11.dll";
                            element2.AppendChild(element12);
                            XmlElement element13 = document.CreateElement("cspDriver");
                            element13.InnerText = "authclt";
                            element2.AppendChild(element13);
                            XmlElement element14 = document.CreateElement("digigalCertPassword");
                            element14.InnerText = "88880001";
                            element2.AppendChild(element14);
                        }
                        else
                        {
                            XmlElement element15 = document.CreateElement("digigalCertPassword");
                            element15.InnerText = taxCard_0.CertPassWord;
                            element2.AppendChild(element15);
                        }
                        if (!string.IsNullOrEmpty(obj4 as string))
                        {
                            XmlElement element16 = document.CreateElement("proxy");
                            element2.AppendChild(element16);
                            XmlElement element17 = document.CreateElement("proxyType");
                            element17.InnerText = obj3 as string;
                            element16.AppendChild(element17);
                            XmlElement element18 = document.CreateElement("proxyHost");
                            element18.InnerText = obj4 as string;
                            element16.AppendChild(element18);
                            XmlElement element19 = document.CreateElement("proxyPort");
                            element19.InnerText = obj5 as string;
                            element16.AppendChild(element19);
                            if (string.IsNullOrEmpty(element19.InnerText))
                            {
                                string_10 = "代理服务器端口不能为空";
                                return -1;
                            }
                            XmlElement element20 = document.CreateElement("proxyAuthEnabled");
                            element20.InnerText = string.IsNullOrEmpty(obj7 as string) ? "FALSE" : "TRUE";
                            element16.AppendChild(element20);
                            XmlElement element21 = document.CreateElement("proxyAuthType");
                            element21.InnerText = obj6 as string;
                            element21.InnerText = string.IsNullOrEmpty(element21.InnerText) ? "BASIC" : element21.InnerText;
                            element16.AppendChild(element21);
                            XmlElement element22 = document.CreateElement("proxyAuthUser");
                            element22.InnerText = obj7 as string;
                            element16.AppendChild(element22);
                            XmlElement element23 = document.CreateElement("proxyAuthPassword");
                            element23.InnerText = obj8 as string;
                            element16.AppendChild(element23);
                        }
                        byte[] buffer6 = Encoding.UTF8.GetBytes(document.InnerXml.ToString());
                        int num8 = buffer6.Length;
                        if (string.Equals(taxCard_0.SoftVersion, "FWKP_V2.0_Svr_Client"))
                        {
                            num7 = smethod_0(buffer6, bytes, out buffer, out resopnseSize, out buffer5, "uploadService");
                        }
                        else
                        {
                            num7 = aisino_communityFramework_call(buffer6, num8, bytes, length, buffer, ref resopnseSize, buffer5);
                        }
                    }
                    if (num7 == 0)
                    {
                        string_1 = string.Empty;
                        string_0 = string.Empty;
                        string_10 = "";
                        string a = "";
                        string str8 = "";
                        if (list_1.Contains(string_8))
                        {
                            string_10 = ToolUtil.GetString(buffer, 0, resopnseSize);
                            return num7;
                        }
                        if (!smethod_4(string_8, buffer, resopnseSize, out string_10, out num7))
                        {
                            using (XmlTextReader reader2 = new XmlTextReader(new MemoryStream(buffer, 0, resopnseSize)))
                            {
                                while (reader2.Read())
                                {
                                    if (reader2.IsStartElement())
                                    {
                                        if (string.Equals("DATA", reader2.LocalName))
                                        {
                                            string_10 = reader2.ReadString();
                                        }
                                        else
                                        {
                                            if (string.Equals("responseCode", reader2.LocalName))
                                            {
                                                a = reader2.ReadString();
                                                continue;
                                            }
                                            if (string.Equals("responseMessage", reader2.LocalName))
                                            {
                                                str8 = reader2.ReadString();
                                                continue;
                                            }
                                            if (string.Equals("YSBZ", reader2.LocalName.ToUpper()))
                                            {
                                                str3 = reader2.ReadString();
                                            }
                                        }
                                    }
                                }
                            }
                            if (!string.Equals(a, "0"))
                            {
                                string_10 = "[" + a + "]" + str8;
                                return -1;
                            }
                            if (string_10.Length == 0)
                            {
                                string_10 = "[9999]远程服务返回的报文中没有发现数据节点";
                                return -1;
                            }
                            byte[] buffer7 = Convert.FromBase64String(string_10);
                            if (string.Equals(str3, "Y"))
                            {
                                buffer7 = ZipUtil.UnCompress(buffer7);
                            }
                            string_10 = ToolUtil.GetString(buffer7);
                        }
                        return num7;
                    }
                    if ((num7 != -1) && (((taxCard_0.TaxCode.Length != 15) || !taxCard_0.TaxCode.Substring(8, 2).ToUpper().Equals("DK")) || str.StartsWith("https")))
                    {
                        string str9 = ToolUtil.GetString(buffer5).Trim(new char[1]);
                        ilog_0.ErrorFormat("数据上传出现错误：{0}", str9);
                        string_10 = string.Concat(new object[] { "[", num7, "]", MessageManager.GetMessageSolution("CA_" + Convert.ToString(num7), null) });
                    }
                    else
                    {
                        string_10 = string.Concat(new object[] { "[", num7, "]", ToolUtil.GetString(buffer).Trim(new char[1]) });
                    }
                    string str10 = Convert.ToString(num7);
                    if (!taxCard_0.SoftVersion.Equals("FWKP_V2.0_Svr_Client") && ((string_6.IndexOf(str10) >= 0) || str10.StartsWith(string_7)))
                    {
                        string_0 = string_10;
                        string_1 = str + "," + (obj4 as string) + "," + (obj7 as string);
                        if (PropertyUtil.GetValue("MAIN_UI", "CLIENT").Equals("CLIENT"))
                        {
                            MessageManager.ShowMsgBox("CA99", new string[] { string_1, string_0 });
                            return num7;
                        }
                        ilog_0.ErrorFormat("证书接口调用失败({0}:{1})，已停止网络连接操作，需要重新运行软件。", str10, string_0);
                        return num7;
                    }
                    string_1 = string.Empty;
                    string_0 = string.Empty;
                    return num7;
                }
                string_10 = "没有正确配置服务器地址(" + str + ")";
                return -1;
            }
            catch (Exception exception)
            {
                string str11 = ToolUtil.GetString(buffer, 0, resopnseSize).Trim(new char[1]);
                string_10 = "网络服务调用异常!" + Environment.NewLine + "异常信息：【{0}】{1}";
                ilog_0.ErrorFormat(string_10, exception.ToString(), string.Concat(new object[] { Environment.NewLine, "服务地址：【", dictionary, "】", Environment.NewLine, "接收到的报文：【", str11, "】" }));
                string_10 = string.Format(string_10, exception.Message, "");
                return -1;
            }
        }

        private static int smethod_0(byte[] byte_0, byte[] byte_1, out byte[] byte_2, out int int_0, out byte[] byte_3, string string_8 = "uploadService")
        {
            int num = 0;
            byte_2 = new byte[0];
            int_0 = 0;
            byte_3 = new byte[0];
            int num2 = 0;
            try
            {
                string str = PropertyUtil.GetValue("KPS_PROXY_URL", "http://KPS_URL_NOT_FOUND").ToLower();
                if (!str.StartsWith("http://"))
                {
                    str = "http://" + str;
                }
                while (str.EndsWith("/"))
                {
                    str = str.Substring(0, str.Length - 1);
                }
                num2 = 1;
                WebClient client = new WebClient();
                if (string.IsNullOrEmpty(string_4))
                {
                    if (taxCard_0.SubSoftVersion.Equals("Linux"))
                    {
                        string str2 = "8080";
                        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "consolePort.txt");
                        if (File.Exists(path))
                        {
                            str2 = File.ReadAllText(path);
                        }
                        else
                        {
                            ilog_0.WarnFormat("配置文件不存在（{0}），使用默认端口8080", path);
                        }
                        string[] strArray = str.Split(new char[] { ':' });
                        if (strArray.Length > 2)
                        {
                            str = strArray[0] + ":" + strArray[1];
                        }
                        string_4 = string.Concat(new object[] { str, ":", str2, "/", taxCard_0.TaxCode, "_", taxCard_0.Machine, "/", string_8 });
                        if (!string.IsNullOrEmpty(taxCard_0.OldTaxCode))
                        {
                            int num3;
                            client.Post(string_4, new byte[0], out num3);
                            if (num3 != 0)
                            {
                                string_4 = string.Concat(new object[] { str, ":", str2, "/", taxCard_0.OldTaxCode, "_", taxCard_0.Machine, "/", string_8 });
                            }
                        }
                    }
                    else
                    {
                        string_4 = str + "/InvUploadHandler.ashx";
                    }
                }
                string_4 = string_4.Substring(0, string_4.LastIndexOf("/")) + "/" + string_8;
                num2 = 2;
                string str4 = new StringBuilder().Append("<?xml version=\"1.0\" encoding=\"GBK\"?>").Append("<ZZSFPXT><POST_DATA>").Append("<TEC>").Append(Convert.ToBase64String(byte_0)).Append("</TEC>").Append("<BUS>").Append(Convert.ToBase64String(byte_1)).Append("</BUS>").Append("</POST_DATA></ZZSFPXT>").ToString();
                byte[] buffer = client.Post_KPS(string_4, smethod_1(str4), out num);
                num2 = 3;
                if (num == 0)
                {
                    if (taxCard_0.SubSoftVersion.Equals("Linux"))
                    {
                        buffer = smethod_2(buffer);
                    }
                    using (XmlTextReader reader = new XmlTextReader(new MemoryStream(buffer)))
                    {
                        num2 = 4;
                        while (reader.Read())
                        {
                            if (reader.IsStartElement())
                            {
                                if (string.Equals("Code", reader.LocalName))
                                {
                                    num = int.Parse(reader.ReadString());
                                }
                                else
                                {
                                    if (string.Equals("DATA", reader.LocalName))
                                    {
                                        if (taxCard_0.SubSoftVersion.Equals("Linux"))
                                        {
                                            byte_2 = Convert.FromBase64String(reader.ReadString());
                                        }
                                        else
                                        {
                                            byte_2 = ToolUtil.GetBytes(reader.ReadString());
                                        }
                                        int_0 = byte_2.Length;
                                        continue;
                                    }
                                    if (string.Equals("MSG", reader.LocalName))
                                    {
                                        if (taxCard_0.SubSoftVersion.Equals("Linux"))
                                        {
                                            byte_3 = Convert.FromBase64String(reader.ReadString());
                                            continue;
                                        }
                                        byte_3 = ToolUtil.GetBytes(reader.ReadString());
                                    }
                                }
                            }
                        }
                        return num;
                    }
                }
                num2 = 5;
                byte_2 = buffer;
                int_0 = byte_2.Length;
                string_4 = "";
            }
            catch (Exception exception)
            {
                num = -1;
                byte_2 = ToolUtil.GetBytes(exception.Message);
                int_0 = byte_2.Length;
                ilog_0.ErrorFormat("向开票服务器发送请求异常({0})：{1}，{2}", num2, string_4, exception.ToString());
            }
            return num;
        }

        private static byte[] smethod_1(string string_8)
        {
            byte[] buffer = new byte[0];
            try
            {
                byte[] bytes = ToolUtil.GetBytes(string_8);
                byte[] buffer3 = new byte[] { 0xc9, 190, 0xa4, 0x37, 15, 0x58, 0xcd, 0x10, 0x23, 0x8b, 0x6a, 0x31, 11, 0xa6, 0x79, 0xea };
                byte[] buffer4 = new byte[] { 0x7e, 0xf9, 0xa8, 0xbc, 0x39, 0xe0, 0x47, 0xae, 0x38, 0xbd, 20, 160, 0xce, 120, 0xcf, 0x93 };
                buffer = AES_Crypt.Encrypt(bytes, buffer3, buffer4);
            }
            catch (Exception)
            {
            }
            return buffer;
        }

        private static byte[] smethod_2(byte[] byte_0)
        {
            byte[] buffer = byte_0;
            try
            {
                byte[] buffer2 = new byte[] { 0xc9, 190, 0xa4, 0x37, 15, 0x58, 0xcd, 0x10, 0x23, 0x8b, 0x6a, 0x31, 11, 0xa6, 0x79, 0xea };
                byte[] buffer3 = new byte[] { 0x7e, 0xf9, 0xa8, 0xbc, 0x39, 0xe0, 0x47, 0xae, 0x38, 0xbd, 20, 160, 0xce, 120, 0xcf, 0x93 };
                buffer = AES_Crypt.Decrypt(byte_0, buffer2, buffer3, null);
            }
            catch (Exception)
            {
            }
            return buffer;
        }

        private static bool smethod_3(ref string string_8, string string_9, out byte[] byte_0, out int int_0)
        {
            if (string.Equals(string_8, "0031"))
            {
                XmlDocument document2 = new XmlDocument();
                XmlDeclaration newChild = document2.CreateXmlDeclaration("1.0", "GBK", null);
                document2.AppendChild(newChild);
                XmlElement element7 = document2.CreateElement("business");
                element7.SetAttribute("id", "HX_FPMXSC");
                element7.SetAttribute("comment", "发票明细上传");
                element7.SetAttribute("version", (PropertyUtil.GetValue("MAIN_BMFLAG", "") == "FLBM") ? "2.0" : "1.0");
                document2.AppendChild(element7);
                XmlElement element8 = document2.CreateElement("body");
                element8.SetAttribute("count", "1");
                element8.SetAttribute("skph", taxCard_0.GetInvControlNum());
                element8.SetAttribute("nsrsbh", taxCard_0.TaxCode);
                element7.AppendChild(element8);
                XmlElement element9 = document2.CreateElement("group");
                element9.SetAttribute("xh", "1");
                element8.AppendChild(element9);
                XmlElement element10 = document2.CreateElement("data");
                element10.SetAttribute("name", "fplx_dm");
                element10.SetAttribute("value", "026");
                element9.AppendChild(element10);
                XmlElement element11 = document2.CreateElement("data");
                element11.SetAttribute("name", "fpmx");
                element11.SetAttribute("value", Convert.ToBase64String(ToolUtil.GetBytes(string_9)));
                element9.AppendChild(element11);
                string str5 = document2.InnerXml.ToString();
                smethod_5(ref str5);
                byte_0 = ToolUtil.GetBytes(str5);
                int_0 = byte_0.Length;
                return true;
            }
            if (string.Equals("0004", string_8))
            {
                string str3 = "";
                using (XmlTextReader reader2 = new XmlTextReader(new MemoryStream(ToolUtil.GetBytes(string_9))))
                {
                    while (reader2.Read())
                    {
                        if (reader2.IsStartElement() && string.Equals("SLXLH", reader2.LocalName))
                        {
                            str3 = reader2.ReadString();
                        }
                    }
                }
                if (str3.Length == 0x20)
                {
                    byte_0 = new byte[0];
                    int_0 = 0;
                    return false;
                }
                XmlDocument document = new XmlDocument();
                XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.AppendChild(declaration);
                XmlElement element = document.CreateElement("business");
                element.SetAttribute("id", "HX_FPMXJG");
                element.SetAttribute("comment", "发票明细结果");
                document.AppendChild(element);
                XmlElement element2 = document.CreateElement("body");
                element2.SetAttribute("count", "1");
                element2.SetAttribute("skph", taxCard_0.GetInvControlNum());
                element2.SetAttribute("nsrsbh", taxCard_0.TaxCode);
                element.AppendChild(element2);
                XmlElement element3 = document.CreateElement("group");
                element3.SetAttribute("xh", "1");
                element2.AppendChild(element3);
                XmlElement element4 = document.CreateElement("data");
                element4.SetAttribute("name", "fplx_dm");
                element4.SetAttribute("value", "026");
                element3.AppendChild(element4);
                XmlElement element5 = document.CreateElement("data");
                element5.SetAttribute("name", "slxlh");
                element5.SetAttribute("value", str3);
                element3.AppendChild(element5);
                XmlElement element6 = document.CreateElement("data");
                element6.SetAttribute("name", "fxsh");
                element6.SetAttribute("value", taxCard_0.CompressCode);
                element3.AppendChild(element6);
                string str4 = document.InnerXml.ToString();
                smethod_5(ref str4);
                byte_0 = ToolUtil.GetBytes(str4);
                int_0 = byte_0.Length;
                string_8 = "0032";
                return true;
            }
            if (string.Equals("0029", string_8))
            {
                smethod_5(ref string_9);
                byte_0 = ToolUtil.GetBytes(string_9);
                int_0 = byte_0.Length;
                return true;
            }
            if (string.Equals("0030", string_8))
            {
                string str2 = "";
                using (XmlTextReader reader = new XmlTextReader(new MemoryStream(ToolUtil.GetBytes(string_9))))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement() && string.Equals("SLXLH", reader.LocalName))
                        {
                            str2 = reader.ReadString();
                        }
                    }
                }
                XmlDocument document4 = new XmlDocument();
                XmlDeclaration declaration4 = document4.CreateXmlDeclaration("1.0", "GBK", null);
                document4.AppendChild(declaration4);
                XmlElement element18 = document4.CreateElement("business");
                element18.SetAttribute("id", "FPSCJGHQ");
                document4.AppendChild(element18);
                XmlElement element19 = document4.CreateElement("body");
                element19.SetAttribute("count", "1");
                element19.SetAttribute("skph", taxCard_0.GetInvControlNum());
                element19.SetAttribute("nsrsbh", taxCard_0.TaxCode);
                element19.SetAttribute("kpjh", taxCard_0.Machine.ToString());
                element18.AppendChild(element19);
                XmlElement element20 = document4.CreateElement("input");
                element19.AppendChild(element20);
                XmlElement element21 = document4.CreateElement("group");
                element21.SetAttribute("xh", "1");
                element20.AppendChild(element21);
                XmlElement element22 = document4.CreateElement("fplxdm");
                element22.InnerText = "025";
                element21.AppendChild(element22);
                XmlElement element23 = document4.CreateElement("slxlh");
                element23.InnerText = str2;
                element21.AppendChild(element23);
                XmlElement element24 = document4.CreateElement("qtxx");
                element24.InnerText = taxCard_0.CompressCode;
                element20.AppendChild(element24);
                string str10 = document4.InnerXml.ToString();
                smethod_5(ref str10);
                byte_0 = ToolUtil.GetBytes(str10);
                int_0 = byte_0.Length;
                return true;
            }
            if (string.Equals("0007", string_8))
            {
                string str6 = "";
                string str7 = "";
                string str8 = "";
                using (XmlTextReader reader3 = new XmlTextReader(new MemoryStream(ToolUtil.GetBytes(string_9))))
                {
                    while (reader3.Read())
                    {
                        if (reader3.IsStartElement())
                        {
                            if (string.Equals("FPZL", reader3.LocalName))
                            {
                                str6 = reader3.ReadString();
                            }
                            else
                            {
                                if (string.Equals("LZFPDM", reader3.LocalName))
                                {
                                    str7 = reader3.ReadString();
                                    continue;
                                }
                                if (string.Equals("LZFPHM", reader3.LocalName))
                                {
                                    str8 = reader3.ReadString();
                                }
                            }
                        }
                    }
                }
                if (string.Equals("p", str6.ToLower()))
                {
                    XmlDocument document3 = new XmlDocument();
                    XmlDeclaration declaration3 = document3.CreateXmlDeclaration("1.0", "GBK", null);
                    document3.AppendChild(declaration3);
                    XmlElement element12 = document3.CreateElement("business");
                    element12.SetAttribute("id", "hzkpje");
                    element12.SetAttribute("comment", "红字普通发票(电子)开具校验");
                    document3.AppendChild(element12);
                    XmlElement element13 = document3.CreateElement("body");
                    element13.SetAttribute("count", "1");
                    element13.SetAttribute("skph", taxCard_0.GetInvControlNum());
                    element13.SetAttribute("nsrsbh", taxCard_0.TaxCode);
                    element12.AppendChild(element13);
                    XmlElement element14 = document3.CreateElement("group");
                    element14.SetAttribute("xh", "1");
                    element13.AppendChild(element14);
                    XmlElement element15 = document3.CreateElement("data");
                    element15.SetAttribute("name", "fplx_dm");
                    element15.SetAttribute("value", "026");
                    element14.AppendChild(element15);
                    XmlElement element16 = document3.CreateElement("data");
                    element16.SetAttribute("name", "yfpdm");
                    element16.SetAttribute("value", str7);
                    element14.AppendChild(element16);
                    XmlElement element17 = document3.CreateElement("data");
                    element17.SetAttribute("name", "yfphm");
                    element17.SetAttribute("value", str8);
                    element14.AppendChild(element17);
                    string str9 = document3.InnerXml.ToString();
                    smethod_5(ref str9);
                    byte_0 = ToolUtil.GetBytes(str9);
                    int_0 = byte_0.Length;
                    string_8 = "0033";
                    return true;
                }
            }
            else if ((string.Equals("0016", string_8) || string.Equals("0018", string_8)) || (string.Equals("0039", string_8) || string.Equals("0042", string_8)))
            {
                string str = string_9;
                smethod_5(ref str);
                byte_0 = ToolUtil.GetBytes(str);
                int_0 = byte_0.Length;
                return true;
            }
            byte_0 = new byte[0];
            int_0 = 0;
            return false;
        }

        private static bool smethod_4(string string_8, byte[] byte_0, int int_0, out string string_9, out int int_1)
        {
            if (string.Equals(string_8, "0031"))
            {
                string str18 = "9999";
                string str20 = "电子发票报送返回报文格式错误";
                string str21 = "";
                using (XmlTextReader reader4 = new XmlTextReader(new MemoryStream(byte_0, 0, int_0)))
                {
                    while (reader4.Read())
                    {
                        if (reader4.IsStartElement() && string.Equals("data", reader4.LocalName))
                        {
                            string attribute = reader4.GetAttribute("value");
                            string str17 = reader4.GetAttribute("name");
                            if (str17 != null)
                            {
                                if (str17 != "returnCode")
                                {
                                    if (!(str17 == "returnMessage"))
                                    {
                                        if (str17 == "slxlh")
                                        {
                                            str21 = attribute;
                                        }
                                    }
                                    else
                                    {
                                        str20 = attribute;
                                    }
                                }
                                else
                                {
                                    str18 = attribute.Equals("00") ? "0000" : attribute;
                                }
                            }
                        }
                    }
                }
                XmlDocument document3 = new XmlDocument();
                XmlDeclaration newChild = document3.CreateXmlDeclaration("1.0", "GBK", null);
                document3.AppendChild(newChild);
                XmlElement element11 = document3.CreateElement("FPXT");
                document3.AppendChild(element11);
                XmlElement element12 = document3.CreateElement("OUTPUT");
                element11.AppendChild(element12);
                XmlElement element13 = document3.CreateElement("CODE");
                element13.InnerText = str18;
                element12.AppendChild(element13);
                XmlElement element14 = document3.CreateElement("MESS");
                element14.InnerText = str20;
                element12.AppendChild(element14);
                XmlElement element15 = document3.CreateElement("DATA");
                element12.AppendChild(element15);
                XmlElement element16 = document3.CreateElement("NSRSBH");
                element16.InnerText = taxCard_0.TaxCode;
                element15.AppendChild(element16);
                XmlElement element17 = document3.CreateElement("KPJH");
                element17.InnerText = taxCard_0.Machine.ToString();
                element15.AppendChild(element17);
                XmlElement element18 = document3.CreateElement("SBBH");
                element18.InnerText = taxCard_0.GetInvControlNum();
                element15.AppendChild(element18);
                XmlElement element19 = document3.CreateElement("SLXLH");
                element19.InnerText = str21;
                element15.AppendChild(element19);
                string_9 = document3.InnerXml.ToString();
                int_1 = 0;
                return true;
            }
            if (string.Equals(string_8, "0032"))
            {
                string a = "9999";
                string str2 = "电子发票报送结果返回报文格式错误";
                string s = "";
                using (XmlTextReader reader = new XmlTextReader(new MemoryStream(byte_0, 0, int_0)))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement() && string.Equals("data", reader.LocalName))
                        {
                            string str7 = reader.GetAttribute("value");
                            string str8 = reader.GetAttribute("name");
                            if (str8 != null)
                            {
                                if (str8 != "returnCode")
                                {
                                    if (!(str8 == "returnMessage"))
                                    {
                                        if (str8 == "mxjgmw")
                                        {
                                            s = str7;
                                        }
                                    }
                                    else
                                    {
                                        str2 = str7;
                                    }
                                }
                                else
                                {
                                    a = str7.Equals("00") ? "0000" : str7;
                                }
                            }
                        }
                    }
                }
                if (string.Equals(a, "0000"))
                {
                    string_9 = ToolUtil.GetString(Convert.FromBase64String(s));
                    int_1 = 0;
                    return true;
                }
                XmlDocument document = new XmlDocument();
                XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "GBK", null);
                document.AppendChild(declaration);
                XmlElement element = document.CreateElement("FPXT");
                document.AppendChild(element);
                XmlElement element2 = document.CreateElement("OUTPUT");
                element.AppendChild(element2);
                XmlElement element3 = document.CreateElement("CODE");
                element3.InnerText = a;
                element2.AppendChild(element3);
                XmlElement element4 = document.CreateElement("MESS");
                element4.InnerText = str2;
                element2.AppendChild(element4);
                XmlElement element5 = document.CreateElement("DATA");
                element2.AppendChild(element5);
                string_9 = document.InnerXml.ToString();
                int_1 = 0;
                return true;
            }
            if (string.Equals(string_8, "0030"))
            {
                string str4 = "9999";
                string str5 = "卷票报送结果返回报文格式错误";
                string str6 = "";
                using (XmlTextReader reader2 = new XmlTextReader(new MemoryStream(byte_0, 0, int_0)))
                {
                    while (reader2.Read())
                    {
                        if (reader2.IsStartElement())
                        {
                            if (string.Equals("data", reader2.LocalName))
                            {
                                reader2.GetAttribute("value");
                            }
                            string localName = reader2.LocalName;
                            if (localName != null)
                            {
                                if (localName != "returncode")
                                {
                                    if (!(localName == "returnmsg"))
                                    {
                                        if (localName == "mxjgmw")
                                        {
                                            str6 = reader2.ReadString();
                                        }
                                    }
                                    else
                                    {
                                        str5 = reader2.ReadString();
                                    }
                                }
                                else
                                {
                                    int result = 9;
                                    string str9 = reader2.ReadString();
                                    int.TryParse(str9, out result);
                                    if (result == 0)
                                    {
                                        str4 = "0000";
                                    }
                                    else
                                    {
                                        str4 = str9;
                                    }
                                }
                            }
                        }
                    }
                }
                if (string.Equals(str4, "0000"))
                {
                    string_9 = ToolUtil.GetString(Convert.FromBase64String(str6));
                    int_1 = 0;
                    return true;
                }
                XmlDocument document4 = new XmlDocument();
                XmlDeclaration declaration4 = document4.CreateXmlDeclaration("1.0", "GBK", null);
                document4.AppendChild(declaration4);
                XmlElement element20 = document4.CreateElement("FPXT");
                document4.AppendChild(element20);
                XmlElement element21 = document4.CreateElement("OUTPUT");
                element20.AppendChild(element21);
                XmlElement element22 = document4.CreateElement("CODE");
                element22.InnerText = str4;
                element21.AppendChild(element22);
                XmlElement element23 = document4.CreateElement("MESS");
                element23.InnerText = str5;
                element21.AppendChild(element23);
                XmlElement element24 = document4.CreateElement("DATA");
                element21.AppendChild(element24);
                string_9 = document4.InnerXml.ToString();
                int_1 = 0;
                return true;
            }
            if (string.Equals(string_8, "0033"))
            {
                string str13 = "9999";
                string str14 = "红字电子发票校验返回报文格式错误";
                string str16 = "";
                using (XmlTextReader reader3 = new XmlTextReader(new MemoryStream(byte_0, 0, int_0)))
                {
                    while (reader3.Read())
                    {
                        if (reader3.IsStartElement())
                        {
                            if (string.Equals("returnCode", reader3.LocalName))
                            {
                                string str12 = reader3.ReadString();
                                str13 = str12.Equals("00") ? "0000" : str12;
                            }
                            else
                            {
                                if (string.Equals("returnMessage", reader3.LocalName))
                                {
                                    str14 = reader3.ReadString();
                                    continue;
                                }
                                if (string.Equals("data", reader3.LocalName))
                                {
                                    string str11;
                                    string str15 = reader3.GetAttribute("value");
                                    if (((str11 = reader3.GetAttribute("name")) != null) && (str11 == "hzje"))
                                    {
                                        str16 = str15;
                                    }
                                }
                            }
                        }
                    }
                }
                XmlDocument document2 = new XmlDocument();
                XmlDeclaration declaration2 = document2.CreateXmlDeclaration("1.0", "GBK", null);
                document2.AppendChild(declaration2);
                XmlElement element6 = document2.CreateElement("FPXT");
                document2.AppendChild(element6);
                XmlElement element7 = document2.CreateElement("OUTPUT");
                element6.AppendChild(element7);
                XmlElement element8 = document2.CreateElement("CODE");
                element8.InnerText = str13;
                element7.AppendChild(element8);
                XmlElement element9 = document2.CreateElement("MESS");
                element9.InnerText = str14;
                element7.AppendChild(element9);
                XmlElement element10 = document2.CreateElement("HZJE");
                element10.InnerText = str16;
                element7.AppendChild(element10);
                string_9 = document2.InnerXml.ToString();
                int_1 = 0;
                return true;
            }
            string_9 = "";
            int_1 = 0;
            return false;
        }

        private static bool smethod_5(ref string string_8)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "GBK", null);
            document.AppendChild(newChild);
            XmlElement element = document.CreateElement("root");
            element.SetAttribute("id", "hx");
            document.AppendChild(element);
            XmlElement element2 = document.CreateElement("digitalEnvelope");
            element2.InnerText = "0";
            element.AppendChild(element2);
            XmlElement element3 = document.CreateElement("zip");
            element3.InnerText = "0";
            element.AppendChild(element3);
            XmlElement element4 = document.CreateElement("context");
            element4.InnerText = Convert.ToBase64String(ToolUtil.GetBytes(string_8));
            element.AppendChild(element4);
            string_8 = document.InnerXml.ToString();
            return true;
        }

        public static int TestConnect(Dictionary<string, object> serverInfo, out string string_8)
        {
            int num = -1;
            try
            {
                object obj2;
                object obj3;
                object obj4;
                object obj5;
                object obj6;
                object obj7;
                object obj8;
                if (serverInfo == null)
                {
                    object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.QueryDZDZInfo", new object[0]);
                    if (objArray == null)
                    {
                        string_8 = "读取服务信息失败";
                        return -1;
                    }
                    serverInfo = objArray[0] as Dictionary<string, object>;
                }
                if (serverInfo == null)
                {
                    string_8 = "读取服务器地址配置信息失败";
                    return -1;
                }
                serverInfo.TryGetValue("ACCEPT_WEB_SERVER", out obj2);
                StringBuilder builder = new StringBuilder().Append("<?xml version=\"1.0\" encoding=\"GBK\"?>").Append("<FPXT>").Append("<INPUT>").Append("<INITDATA>").Append("<![CDATA[11001115600009285220140924*-20<6</>545016*9->87>1285>>/922/9*50<95<48<03-5951418>/443<*1>*-/145>037388*>7>4264<598/523*-0<7/*+1>92>4**北京畅联电子有限公司110101251328321北京市海淀区浇灌里231号 62345123110.0018.70128.70jn-lingyongcun-xindiceng110101924000002北京市海淀区杏石口路甲18号 (+8610)8889666662227899999999jj管理员1N1资料PACK箱1.00000000110.00000000110.000.1718.70]]>").Append("</INITDATA>").Append("<SIGNDATA>").Append("<![CDATA[MIIBNgYJKoZIhvcNAQcCoIIBJzCCASMCAQExCzAJBgUrDgMCGgUAMAsGCSqGSIb3DQEHATGCAQIwgf8CAQEwXTBTMQswCQYDVQQGEwJDTjEbMBkGA1UECwwS5Zu95a6256iO5Yqh5oC75bGAMScwJQYDVQQDDB7nqI7liqHnlLXlrZDor4HkuabnrqHnkIbkuK3lv4MCBgEAAAAMxzAJBgUrDgMCGgUAMA0GCSqGSIb3DQEBAQUABIGANdQSApbmdNU1yKx6sEanoeeuuY9PdZl82VwsZmNmimN/jdBoigMRv2DhnguycD7keKj7f/H8zz4OuouUKc32m9C00XM0T3GbdWVOFVQITD2D3w2zVdfcWwv1imzuHlRNkqCvtKlDn0fyaplJWkEAD/ND/mLXMs+lKHKMFAdlsGE=]]>").Append("</SIGNDATA>").Append("<TESTJM>N</TESTJM>").Append("<TESTYQ>Y</TESTYQ></INPUT>").Append("</FPXT>");
                if (((taxCard_0.TaxCode.Length != 15) || !taxCard_0.TaxCode.Substring(8, 2).ToUpper().Equals("DK")) || (obj2 as string).StartsWith("https"))
                {
                    goto Label_027E;
                }
                byte[] buffer = ZipUtil.Compress(ToolUtil.GetBytes(builder.ToString()));
                string hashString = MD5_Crypt.GetHashString(buffer);
                string str2 = Convert.ToBase64String(buffer);
                string str3 = new StringBuilder().Append("<?xml version=\"1.0\" encoding=\"GBK\"?>").Append("<ZZSFPXT><SK_TYPE>JSP</SK_TYPE><OP_TYPE>0104").Append("</OP_TYPE><INPUT><DATA>").Append(str2).Append("</DATA><CRC>").Append(hashString).Append("</CRC></INPUT></ZZSFPXT>").ToString();
                byte[] buffer2 = new WebClient().Post_Byte(serverInfo["ACCEPT_WEB_SERVER"] as string, ToolUtil.GetBytes(str3), out num);
                if (num != 0)
                {
                    goto Label_0262;
                }
                string s = "";
                string str5 = "";
                using (XmlTextReader reader = new XmlTextReader(new MemoryStream(buffer2, 0, buffer2.Length)))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            if (string.Equals("responseCode", reader.LocalName))
                            {
                                s = reader.ReadString();
                                if (str5.Length > 0)
                                {
                                    goto Label_024F;
                                }
                            }
                            if (string.Equals("responseMessage", reader.LocalName))
                            {
                                str5 = reader.ReadString();
                                if (s.Length > 0)
                                {
                                    goto Label_024F;
                                }
                            }
                        }
                    }
                }
            Label_024F:
                if (!int.TryParse(s, out num))
                {
                    num = -1;
                }
                string_8 = str5;
                goto Label_026B;
            Label_0262:
                string_8 = ToolUtil.GetString(buffer2);
            Label_026B:
                if (num != 0)
                {
                    ToolUtil.GetString(buffer2);
                }
                return num;
            Label_027E:
                serverInfo.TryGetValue("proxyType", out obj3);
                serverInfo.TryGetValue("proxyHost", out obj4);
                serverInfo.TryGetValue("proxyPort", out obj5);
                serverInfo.TryGetValue("proxyAuthType", out obj6);
                serverInfo.TryGetValue("proxyAuthUser", out obj7);
                serverInfo.TryGetValue("proxyAuthPassword", out obj8);
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "UTF-8", null);
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("tripTechnologyPackage");
                element.SetAttribute("version", "1.0");
                document.AppendChild(element);
                XmlElement element2 = document.CreateElement("identity");
                element.AppendChild(element2);
                XmlElement element3 = document.CreateElement("SSL");
                element3.InnerText = (obj2 as string).StartsWith("https://") ? "TA" : "OA";
                element2.AppendChild(element3);
                XmlElement element4 = document.CreateElement("serviceURI");
                element4.InnerText = obj2 as string;
                element2.AppendChild(element4);
                XmlElement element5 = document.CreateElement("nsrsbh");
                element5.InnerText = string_2;
                element2.AppendChild(element5);
                XmlElement element6 = document.CreateElement("senderName");
                element6.InnerText = string_3;
                element2.AppendChild(element6);
                XmlElement element7 = document.CreateElement("digigalCertPassword");
                element7.InnerText = taxCard_0.CertPassWord;
                element2.AppendChild(element7);
                XmlElement element8 = document.CreateElement("showDigigalCertSelectDialog");
                element8.InnerText = "FALSE";
                element2.AppendChild(element8);
                XmlElement element9 = document.CreateElement("logEnabled");
                element9.InnerText = string.Equals(PropertyUtil.GetValue("TMP_SA", "0"), "0") ? "FALSE" : "TRUE";
                element2.AppendChild(element9);
                XmlElement element10 = document.CreateElement("deviceId");
                if ((taxCard_0.TaxCode.Length == 12) && (taxCard_0.TaxCode.StartsWith("99") || taxCard_0.TaxCode.StartsWith("98")))
                {
                    element10.InnerText = "";
                }
                else
                {
                    element10.InnerText = PropertyUtil.GetValue("HIGH_DEVICE", "").Equals("") ? ("*44" + taxCard_0.GetInvControlNum()) : "";
                }
                element2.AppendChild(element10);
                XmlElement element11 = document.CreateElement("command");
                element11.InnerText = "COMPATIBILITY_CUSTOM";
                element2.AppendChild(element11);
                if (!string.IsNullOrEmpty(obj4 as string))
                {
                    XmlElement element12 = document.CreateElement("proxy");
                    element2.AppendChild(element12);
                    XmlElement element13 = document.CreateElement("proxyType");
                    element13.InnerText = obj3 as string;
                    element12.AppendChild(element13);
                    XmlElement element14 = document.CreateElement("proxyHost");
                    element14.InnerText = obj4 as string;
                    element12.AppendChild(element14);
                    XmlElement element15 = document.CreateElement("proxyPort");
                    element15.InnerText = obj5 as string;
                    element12.AppendChild(element15);
                    if (string.IsNullOrEmpty(element15.InnerText))
                    {
                        string_8 = "代理服务器端口不能为空";
                        return -1;
                    }
                    XmlElement element16 = document.CreateElement("proxyAuthEnabled");
                    element16.InnerText = string.IsNullOrEmpty(obj7 as string) ? "FALSE" : "TRUE";
                    element12.AppendChild(element16);
                    XmlElement element17 = document.CreateElement("proxyAuthType");
                    element17.InnerText = obj6 as string;
                    element17.InnerText = string.IsNullOrEmpty(element17.InnerText) ? "BASIC" : element17.InnerText;
                    element12.AppendChild(element17);
                    XmlElement element18 = document.CreateElement("proxyAuthUser");
                    element18.InnerText = obj7 as string;
                    element12.AppendChild(element18);
                    XmlElement element19 = document.CreateElement("proxyAuthPassword");
                    element19.InnerText = obj8 as string;
                    element12.AppendChild(element19);
                }
                XmlElement element20 = document.CreateElement("attachment");
                element.AppendChild(element20);
                XmlElement element21 = document.CreateElement("Properties");
                element20.AppendChild(element21);
                XmlElement element22 = document.CreateElement("Key");
                element22.SetAttribute("Name", "FRAMEWORK_CURRENT_VERSION");
                element22.InnerText = PropertyUtil.GetValue("MAIN_VER", "1.0.00");
                element21.AppendChild(element22);
                XmlElement element23 = document.CreateElement("Key");
                element23.SetAttribute("Name", "FRAMEWORK_BIZ_SUBTYPE");
                element23.InnerText = "NETWORK_CONNECT_CHECK";
                element21.AppendChild(element23);
                byte[] bytes = Encoding.UTF8.GetBytes(document.InnerXml.ToString());
                int length = bytes.Length;
                byte[] buffer4 = ToolUtil.GetBytes(builder.ToString());
                int num3 = buffer4.Length;
                byte[] buffer5 = new byte[HttpsSender.resopnseSize];
                int resopnseSize = HttpsSender.resopnseSize;
                byte[] buffer6 = new byte[0x400];
                if (string.Equals(taxCard_0.SoftVersion, "FWKP_V2.0_Svr_Client"))
                {
                    num = smethod_0(bytes, buffer4, out buffer5, out resopnseSize, out buffer6, "uploadService");
                }
                else
                {
                    num = aisino_communityFramework_call(bytes, length, buffer4, num3, buffer5, ref resopnseSize, buffer6);
                }
                if (num == 0)
                {
                    string_8 = Encoding.GetEncoding("UTF-8").GetString(buffer5).Trim(new char[1]);
                    return num;
                }
                string str7 = ToolUtil.GetString(buffer6).Trim(new char[1]);
                ilog_0.ErrorFormat("测试数据上传出现错误：{0}", str7);
                string str8 = "CA_" + Convert.ToString(num);
                string_8 = MessageManager.GetMessageSolution(str8, null);
                if (str7.Length > 0)
                {
                    string_8 = string_8 + "(" + str7 + ")";
                }
                string str9 = ToolUtil.GetString(buffer5).Trim(new char[1]);
                if (str9.Length > 0)
                {
                    string_8 = string_8 + "(" + str9 + ")";
                }
            }
            catch (Exception exception)
            {
                ilog_0.ErrorFormat("测试安全接入服务器连接时异常：{0}", exception.ToString());
                num = -1;
                string_8 = exception.Message;
            }
            return num;
        }

        public static string GET_CA_ERR_CODE
        {
            get
            {
                return string_6;
            }
        }

        public static string GET_CA_ERR_CODE_HEAD
        {
            get
            {
                return string_7;
            }
        }
    }
}

