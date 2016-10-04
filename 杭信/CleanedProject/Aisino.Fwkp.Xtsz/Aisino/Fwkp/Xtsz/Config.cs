namespace Aisino.Fwkp.Xtsz
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Xtsz.DAL;
    using Aisino.Fwkp.Xtsz.Model;
    using log4net;
    using System;
    using System.IO;
    using System.Xml;

    public class Config
    {
        private static ILog loger = LogUtil.GetLogger<Config>();
        private static string m_strConfigPath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH").Trim(), @"Config\Common\Kzyf.Configure.XML");
        private static string m_strDZDZConfigPath = Path.Combine(PropertyUtil.GetValue("MAIN_PATH").Trim(), @"Config\Common\DZDZ.Configure.XML");
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();

        public static bool CreateDZDZXML(DZDZInfoModel dzdzInfoModel)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                string path = m_strDZDZConfigPath.Remove(m_strDZDZConfigPath.LastIndexOf(@"\"));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "utf-8", null);
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("root");
                document.AppendChild(element);
                XmlNode node = document.CreateNode("element", "AcceptWebServer", "");
                node.InnerText = dzdzInfoModel.AcceptWebServer;
                element.AppendChild(node);
                node = document.CreateNode("element", "IsUseProxy", "");
                node.InnerText = dzdzInfoModel.IsUseProxy.ToString();
                element.AppendChild(node);
                node = document.CreateNode("element", "ProxyType", "");
                node.InnerText = dzdzInfoModel.ProxyType;
                element.AppendChild(node);
                node = document.CreateNode("element", "ProxyHost", "");
                node.InnerText = dzdzInfoModel.ProxyHost;
                element.AppendChild(node);
                node = document.CreateNode("element", "ProxyPort", "");
                node.InnerText = dzdzInfoModel.ProxyPort;
                element.AppendChild(node);
                node = document.CreateNode("element", "IsAuthConfirm", "");
                node.InnerText = dzdzInfoModel.IsAuthConfirm.ToString();
                element.AppendChild(node);
                node = document.CreateNode("element", "ProxyAuthType", "");
                node.InnerText = dzdzInfoModel.ProxyAuthType;
                element.AppendChild(node);
                node = document.CreateNode("element", "ProxyAuthUser", "");
                node.InnerText = dzdzInfoModel.ProxyAuthUser;
                element.AppendChild(node);
                node = document.CreateNode("element", "ProxyAuthPassword", "");
                node.InnerText = dzdzInfoModel.ProxyAuthPassword;
                element.AppendChild(node);
                node = document.CreateNode("element", "UploadNowFlag", "");
                node.InnerText = dzdzInfoModel.UploadNowFlag.ToString();
                element.AppendChild(node);
                node = document.CreateNode("element", "IntervalFlag", "");
                node.InnerText = dzdzInfoModel.IntervalFlag.ToString();
                element.AppendChild(node);
                node = document.CreateNode("element", "IntervalTime", "");
                node.InnerText = dzdzInfoModel.IntervalTime.ToString();
                element.AppendChild(node);
                node = document.CreateNode("element", "AccumulateFlag", "");
                node.InnerText = dzdzInfoModel.AccumulateFlag.ToString();
                element.AppendChild(node);
                node = document.CreateNode("element", "AccumulateNum", "");
                node.InnerText = dzdzInfoModel.AccumulateNum.ToString();
                element.AppendChild(node);
                node = document.CreateNode("element", "DataSize", "");
                node.InnerText = dzdzInfoModel.DataSize.ToString();
                element.AppendChild(node);
                if (!SetAQJRDZ(dzdzInfoModel.AcceptWebServer))
                {
                    return false;
                }
                document.Save(m_strDZDZConfigPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool CreateXML()
        {
            try
            {
                XmlDocument document = new XmlDocument();
                string path = m_strConfigPath.Remove(m_strConfigPath.LastIndexOf(@"\"));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "utf-8", null);
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("root");
                document.AppendChild(element);
                XmlNode node = document.CreateNode("element", "kzyf", "");
                node.InnerText = TaxCardFactory.CreateTaxCard().get_CardEffectDate();
                element.AppendChild(node);
                document.Save(m_strConfigPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string GetAccountDate()
        {
            try
            {
                if (!File.Exists(m_strConfigPath))
                {
                    loger.Debug("开账月份配置文件不存在");
                    if (!CreateXML())
                    {
                        loger.Debug("创建开账月份配置文件失败");
                        return "";
                    }
                }
                XmlDocument document = new XmlDocument();
                document.Load(m_strConfigPath);
                XmlNode newChild = document.DocumentElement.SelectSingleNode("kzyf");
                if (newChild == null)
                {
                    XmlNode node2 = document.SelectSingleNode("root");
                    if (node2 == null)
                    {
                        node2 = document.CreateElement("root");
                        document.AppendChild(node2);
                    }
                    newChild = document.CreateNode("element", "kzyf", "");
                    newChild.InnerText = TaxCardFactory.CreateTaxCard().get_CardEffectDate();
                    node2.AppendChild(newChild);
                    document.Save(m_strConfigPath);
                    return newChild.InnerText;
                }
                return newChild.InnerText;
            }
            catch (Exception exception)
            {
                loger.Error(exception.Message);
                if (!CreateXML())
                {
                    return "";
                }
                return GetAccountDate();
            }
        }

        private static string GetAQJRAddrFromConfigFile()
        {
            string str = "";
            try
            {
                TaxCard card = TaxCardFactory.CreateTaxCard();
                string str2 = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Config\Common\AQJR.ini");
                string str3 = IniFileUtil.ReadIniData("AQJRAddr", card.get_RegionCode().Substring(0, 4), string.Empty, str2);
                if (!string.IsNullOrEmpty(str3))
                {
                    return str3;
                }
                str = IniFileUtil.ReadIniData("AQJRAddr", card.get_RegionCode().Substring(0, 2), string.Empty, str2);
            }
            catch (Exception exception)
            {
                loger.Error("GetAQJRAddrFromConfigFile异常：" + exception.ToString());
            }
            return str;
        }

        private static string GetAQJRDZ()
        {
            string path = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), "Bin", "AQJRDZ.txt");
            if (!File.Exists(path))
            {
                return GetAQJRAddrFromConfigFile();
            }
            try
            {
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream, ToolUtil.GetEncoding());
                reader.BaseStream.Seek(0L, SeekOrigin.Begin);
                string str2 = reader.ReadLine();
                reader.Close();
                stream.Close();
                string aQJRAddrFromConfigFile = "";
                if (((str2 == null) || (str2 == "")) || ((str2.IndexOf("127.0.0.1") > -1) || (str2.ToLower().IndexOf("localhost") > -1)))
                {
                    aQJRAddrFromConfigFile = GetAQJRAddrFromConfigFile();
                    if ((aQJRAddrFromConfigFile != null) && (aQJRAddrFromConfigFile != ""))
                    {
                        str2 = aQJRAddrFromConfigFile;
                    }
                }
                return str2;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static bool GetDZDZInfoFromXML(ref DZDZInfoModel dzdzInfoModel)
        {
            try
            {
                if (!File.Exists(m_strDZDZConfigPath))
                {
                    loger.Debug("电子抵账配置文件不存在");
                    ParaSetDAL tdal = new ParaSetDAL();
                    DZDZInfoModel model = new DZDZInfoModel();
                    if (!tdal.GetDZDZInfoFromDB(ref model))
                    {
                        loger.Debug("从数据库中获取电子抵账信息失败");
                        return false;
                    }
                    string aQJRDZ = GetAQJRDZ();
                    if (!string.IsNullOrEmpty(aQJRDZ))
                    {
                        model.AcceptWebServer = aQJRDZ;
                    }
                    if (!CreateDZDZXML(model))
                    {
                        loger.Debug("从数据库中更新电子抵账配置文件失败");
                        return false;
                    }
                }
                XmlDocument document = new XmlDocument();
                document.Load(m_strDZDZConfigPath);
                XmlNode node = document.DocumentElement.SelectSingleNode("IsUseProxy");
                if (node != null)
                {
                    dzdzInfoModel.IsUseProxy = bool.Parse(node.InnerText);
                }
                node = document.DocumentElement.SelectSingleNode("ProxyType");
                if (node != null)
                {
                    dzdzInfoModel.ProxyType = node.InnerText;
                }
                node = document.DocumentElement.SelectSingleNode("ProxyHost");
                if (node != null)
                {
                    dzdzInfoModel.ProxyHost = node.InnerText;
                }
                node = document.DocumentElement.SelectSingleNode("ProxyPort");
                if (node != null)
                {
                    dzdzInfoModel.ProxyPort = node.InnerText;
                }
                node = document.DocumentElement.SelectSingleNode("IsAuthConfirm");
                if (node != null)
                {
                    dzdzInfoModel.IsAuthConfirm = bool.Parse(node.InnerText);
                }
                node = document.DocumentElement.SelectSingleNode("ProxyAuthType");
                if (node != null)
                {
                    dzdzInfoModel.ProxyAuthType = node.InnerText;
                }
                node = document.DocumentElement.SelectSingleNode("ProxyAuthUser");
                if (node != null)
                {
                    dzdzInfoModel.ProxyAuthUser = node.InnerText;
                }
                node = document.DocumentElement.SelectSingleNode("ProxyAuthPassword");
                if (node != null)
                {
                    dzdzInfoModel.ProxyAuthPassword = node.InnerText;
                }
                node = document.DocumentElement.SelectSingleNode("UploadNowFlag");
                if (node != null)
                {
                    dzdzInfoModel.UploadNowFlag = bool.Parse(node.InnerText);
                }
                node = document.DocumentElement.SelectSingleNode("IntervalFlag");
                if (node != null)
                {
                    dzdzInfoModel.IntervalFlag = bool.Parse(node.InnerText);
                }
                node = document.DocumentElement.SelectSingleNode("IntervalTime");
                if (node != null)
                {
                    dzdzInfoModel.IntervalTime = int.Parse(node.InnerText);
                }
                node = document.DocumentElement.SelectSingleNode("AccumulateFlag");
                if (node != null)
                {
                    dzdzInfoModel.AccumulateFlag = bool.Parse(node.InnerText);
                }
                node = document.DocumentElement.SelectSingleNode("AccumulateNum");
                if (node != null)
                {
                    dzdzInfoModel.AccumulateNum = int.Parse(node.InnerText);
                }
                node = document.DocumentElement.SelectSingleNode("DataSize");
                if (node != null)
                {
                    dzdzInfoModel.DataSize = int.Parse(node.InnerText);
                }
                node = document.DocumentElement.SelectSingleNode("AcceptWebServer");
                if (node != null)
                {
                    dzdzInfoModel.AcceptWebServer = node.InnerText;
                    string str2 = GetAQJRDZ();
                    if (!string.IsNullOrEmpty(str2))
                    {
                        if (str2 != dzdzInfoModel.AcceptWebServer)
                        {
                            dzdzInfoModel.AcceptWebServer = str2;
                            CreateDZDZXML(dzdzInfoModel);
                        }
                    }
                    else if (!SetAQJRDZ(dzdzInfoModel.AcceptWebServer))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                loger.Error(exception.Message);
                File.Delete(m_strDZDZConfigPath);
                if (!GetDZDZInfoFromXML(ref dzdzInfoModel))
                {
                    return false;
                }
            }
            return true;
        }

        public void ResetAccountData()
        {
            if (!string.Equals(GetAccountDate(), this.taxCard.get_CardEffectDate()) && !SetAccountDate(this.taxCard.get_CardEffectDate()))
            {
                loger.Error("set account date failed");
                MessageManager.ShowMsgBox("INP-233116");
            }
        }

        public static bool SetAccountDate(string strValue)
        {
            try
            {
                if (!File.Exists(m_strConfigPath) && !CreateXML())
                {
                    return false;
                }
                XmlDocument document = new XmlDocument();
                document.Load(m_strConfigPath);
                XmlNode node = document.DocumentElement.SelectSingleNode("kzyf");
                if ((node == null) && !CreateXML())
                {
                    document.Save(m_strConfigPath);
                    return false;
                }
                node.InnerText = strValue;
                document.Save(m_strConfigPath);
                return true;
            }
            catch (Exception exception)
            {
                loger.Error(exception.Message);
                return false;
            }
        }

        private static bool SetAQJRDZ(string addrStr)
        {
            string path = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), "Bin", "AQJRDZ.txt");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            try
            {
                FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(stream, ToolUtil.GetEncoding());
                writer.BaseStream.Seek(0L, SeekOrigin.Begin);
                writer.WriteLine(addrStr);
                writer.Close();
                stream.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

