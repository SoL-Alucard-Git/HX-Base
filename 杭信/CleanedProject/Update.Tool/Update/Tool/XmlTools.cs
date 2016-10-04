namespace Update.Tool
{
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Xml;
    using Update.Model;

    public class XmlTools
    {
        private static ILog ilog_0;

        static XmlTools()
        {
           
            ilog_0 = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public XmlTools()
        {
           
        }

        public static string AreaCodeAddressListToXml(IList<AreaCodeAddressInfo> areaCodeAddressList)
        {
            if ((areaCodeAddressList == null) || (areaCodeAddressList.Count <= 0))
            {
                return "";
            }
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            document.PreserveWhitespace = false;
            document.AppendChild(newChild);
            XmlElement element = document.CreateElement("AddressList");
            document.AppendChild(element);
            foreach (AreaCodeAddressInfo info in areaCodeAddressList)
            {
                XmlElement element2 = document.CreateElement("AddressInfo");
                element.AppendChild(element2);
                XmlElement element3 = document.CreateElement("AreaCode");
                element3.InnerText = info.AreaCode;
                element2.AppendChild(element3);
                element3 = document.CreateElement("Address");
                element3.InnerText = info.Address;
                element2.AppendChild(element3);
            }
            return document.InnerXml;
        }

        public static string DownloadListInfoToXml(DownloadInfo fileListInfo, XmlDocument doc, XmlElement root)
        {
            if (fileListInfo == null)
            {
                return "";
            }
            XmlElement newChild = doc.CreateElement("DownloadListInfo");
            root.AppendChild(newChild);
            XmlElement element2 = doc.CreateElement("ClientName");
            element2.InnerText = fileListInfo.SoftVer.ClientName;
            newChild.AppendChild(element2);
            element2 = doc.CreateElement("ClientIp");
            element2.InnerText = fileListInfo.SoftVer.ClientIp;
            newChild.AppendChild(element2);
            element2 = doc.CreateElement("ClientPort");
            element2.InnerText = fileListInfo.SoftVer.ClientPort.ToString();
            newChild.AppendChild(element2);
            element2 = doc.CreateElement("SoftName");
            element2.InnerText = fileListInfo.SoftVer.SoftName;
            newChild.AppendChild(element2);
            element2 = doc.CreateElement("Version");
            element2.InnerText = fileListInfo.SoftVer.Version;
            newChild.AppendChild(element2);
            element2 = doc.CreateElement("Force");
            element2.InnerText = fileListInfo.SoftVer.Force ? "1" : "0";
            newChild.AppendChild(element2);
            element2 = doc.CreateElement("VerDesc");
            element2.InnerText = fileListInfo.SoftVer.VerDesc;
            newChild.AppendChild(element2);
            element2 = doc.CreateElement("BserverStr");
            element2.InnerText = fileListInfo.SoftVer.BserverStr;
            newChild.AppendChild(element2);
            element2 = doc.CreateElement("EnableStr");
            element2.InnerText = fileListInfo.SoftVer.EnableStr;
            newChild.AppendChild(element2);
            element2 = doc.CreateElement("DisableStr");
            element2.InnerText = fileListInfo.SoftVer.DisableStr;
            newChild.AppendChild(element2);
            element2 = doc.CreateElement("FilePath");
            element2.InnerText = fileListInfo.SoftVer.FilePath;
            newChild.AppendChild(element2);
            element2 = doc.CreateElement("StartTime");
            element2.InnerText = fileListInfo.SoftVer.StartTime.ToString("yyyyMMddHHmmss");
            newChild.AppendChild(element2);
            element2 = doc.CreateElement("ExpireTime");
            element2.InnerText = fileListInfo.SoftVer.ExpireTime.ToString("yyyyMMddHHmmss");
            newChild.AppendChild(element2);
            element2 = doc.CreateElement("Xzbz");
            element2.InnerText = fileListInfo.Xzbz.ToString();
            newChild.AppendChild(element2);
            element2 = doc.CreateElement("UpdateFlag");
            element2.InnerText = "";
            newChild.AppendChild(element2);
            return doc.InnerXml;
        }

        [DllImport("Security.dll", CallingConvention=CallingConvention.StdCall, CharSet=CharSet.Ansi)]
        public static extern int FileCryptEx(int nIndex, string strDllPath, string strTaxCode);
        public static string FileListInfoToXml(DownloadInfo fileListInfo)
        {
            if (fileListInfo == null)
            {
                return "";
            }
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            document.PreserveWhitespace = false;
            document.AppendChild(newChild);
            XmlElement element = document.CreateElement("FileListInfo");
            document.AppendChild(element);
            XmlElement element2 = document.CreateElement("ClientName");
            element2.InnerText = fileListInfo.SoftVer.ClientName;
            element.AppendChild(element2);
            element2 = document.CreateElement("ClientIp");
            element2.InnerText = fileListInfo.SoftVer.ClientIp;
            element.AppendChild(element2);
            element2 = document.CreateElement("ClientPort");
            element2.InnerText = fileListInfo.SoftVer.ClientPort.ToString();
            element.AppendChild(element2);
            element2 = document.CreateElement("SoftName");
            element2.InnerText = fileListInfo.SoftVer.SoftName;
            element.AppendChild(element2);
            element2 = document.CreateElement("Version");
            element2.InnerText = fileListInfo.SoftVer.Version;
            element.AppendChild(element2);
            element2 = document.CreateElement("Force");
            element2.InnerText = fileListInfo.SoftVer.Force ? "1" : "0";
            element.AppendChild(element2);
            element2 = document.CreateElement("VerDesc");
            element2.InnerText = fileListInfo.SoftVer.VerDesc;
            element.AppendChild(element2);
            element2 = document.CreateElement("BserverStr");
            element2.InnerText = fileListInfo.SoftVer.BserverStr;
            element.AppendChild(element2);
            element2 = document.CreateElement("EnableStr");
            element2.InnerText = fileListInfo.SoftVer.EnableStr;
            element.AppendChild(element2);
            element2 = document.CreateElement("DisableStr");
            element2.InnerText = fileListInfo.SoftVer.DisableStr;
            element.AppendChild(element2);
            element2 = document.CreateElement("FilePath");
            element2.InnerText = fileListInfo.SoftVer.FilePath;
            element.AppendChild(element2);
            element2 = document.CreateElement("StartTime");
            element2.InnerText = fileListInfo.SoftVer.StartTime.ToString("yyyyMMddHHmmss");
            element.AppendChild(element2);
            element2 = document.CreateElement("ExpireTime");
            element2.InnerText = fileListInfo.SoftVer.ExpireTime.ToString("yyyyMMddHHmmss");
            element.AppendChild(element2);
            XmlElement element3 = document.CreateElement("FileList");
            element.AppendChild(element3);
            long num2 = 0L;
            foreach (SoftFileInfo info in fileListInfo.FileList)
            {
                XmlElement element4 = document.CreateElement("SoftFileInfo");
                element3.AppendChild(element4);
                element2 = document.CreateElement("RelativePath");
                element2.InnerText = info.RelativePath;
                element4.AppendChild(element2);
                element2 = document.CreateElement("Length");
                element2.InnerText = info.Length.ToString();
                element4.AppendChild(element2);
                num2 += info.Length;
                element2 = document.CreateElement("CreationTime");
                element2.InnerText = info.CreationTime.ToString("yyyyMMddHHmmss");
                element4.AppendChild(element2);
                element2 = document.CreateElement("LastAccessTime");
                element2.InnerText = info.LastAccessTime.ToString("yyyyMMddHHmmss");
                element4.AppendChild(element2);
                element2 = document.CreateElement("LastWriteTime");
                element2.InnerText = info.LastWriteTime.ToString("yyyyMMddHHmmss");
                element4.AppendChild(element2);
                element2 = document.CreateElement("CRCValue");
                element2.InnerText = info.CRCValue;
                element4.AppendChild(element2);
            }
            element2 = document.CreateElement("TotalSize");
            element2.InnerText = num2.ToString();
            element.AppendChild(element2);
            return document.InnerXml;
        }

        public static string FileListInfoToXml(SoftVersionInfo softVersionInfo)
        {
            if (!((softVersionInfo != null) && Directory.Exists(softVersionInfo.FilePath)))
            {
                return "";
            }
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            document.PreserveWhitespace = false;
            document.AppendChild(newChild);
            XmlElement element3 = document.CreateElement("FileListInfo");
            document.AppendChild(element3);
            XmlElement element = document.CreateElement("ClientName");
            element.InnerText = softVersionInfo.ClientName;
            element3.AppendChild(element);
            element = document.CreateElement("ClientIp");
            element.InnerText = softVersionInfo.ClientIp;
            element3.AppendChild(element);
            element = document.CreateElement("ClientPort");
            element.InnerText = softVersionInfo.ClientPort.ToString();
            element3.AppendChild(element);
            element = document.CreateElement("SoftName");
            element.InnerText = softVersionInfo.SoftName;
            element3.AppendChild(element);
            element = document.CreateElement("Version");
            element.InnerText = softVersionInfo.Version;
            element3.AppendChild(element);
            element = document.CreateElement("Force");
            element.InnerText = softVersionInfo.Force ? "1" : "0";
            element3.AppendChild(element);
            element = document.CreateElement("IsUses");
            element.InnerText = softVersionInfo.IsUses ? "1" : "0";
            element3.AppendChild(element);
            element = document.CreateElement("VerDesc");
            element.InnerText = softVersionInfo.VerDesc;
            element3.AppendChild(element);
            element = document.CreateElement("BserverStr");
            element.InnerText = softVersionInfo.BserverStr;
            element3.AppendChild(element);
            element = document.CreateElement("EnableStr");
            element.InnerText = softVersionInfo.EnableStr;
            element3.AppendChild(element);
            element = document.CreateElement("DisableStr");
            element.InnerText = softVersionInfo.DisableStr;
            element3.AppendChild(element);
            element = document.CreateElement("FilePath");
            element.InnerText = softVersionInfo.FilePath;
            element3.AppendChild(element);
            element = document.CreateElement("StartTime");
            element.InnerText = softVersionInfo.StartTime.ToString("yyyyMMddHHmmss");
            element3.AppendChild(element);
            element = document.CreateElement("ExpireTime");
            element.InnerText = softVersionInfo.ExpireTime.ToString("yyyyMMddHHmmss");
            element3.AppendChild(element);
            XmlElement element4 = document.CreateElement("FileList");
            element3.AppendChild(element4);
            string searchPattern = "*.*";
            FileInfo[] files = new DirectoryInfo(softVersionInfo.FilePath).GetFiles(searchPattern, SearchOption.AllDirectories);
            long num4 = 0L;
            foreach (FileInfo info in files)
            {
                XmlElement element2 = document.CreateElement("SoftFileInfo");
                element4.AppendChild(element2);
                element = document.CreateElement("RelativePath");
                if (softVersionInfo.FilePath.EndsWith(@"\"))
                {
                    element.InnerText = info.FullName.Substring(softVersionInfo.FilePath.Length);
                }
                else
                {
                    element.InnerText = info.FullName.Substring(softVersionInfo.FilePath.Length + 1);
                }
                element2.AppendChild(element);
                element = document.CreateElement("Length");
                element.InnerText = info.Length.ToString();
                element2.AppendChild(element);
                num4 += info.Length;
                element = document.CreateElement("CreationTime");
                element.InnerText = info.CreationTime.ToString("yyyyMMddHHmmss");
                element2.AppendChild(element);
                element = document.CreateElement("LastAccessTime");
                element.InnerText = info.LastAccessTime.ToString("yyyyMMddHHmmss");
                element2.AppendChild(element);
                element = document.CreateElement("LastWriteTime");
                element.InnerText = info.LastWriteTime.ToString("yyyyMMddHHmmss");
                element2.AppendChild(element);
                if ((info.FullName.Contains("AddedRealTax.dll") && (softVersionInfo.TaxCode != null)) && (softVersionInfo.TaxCode != ""))
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "Temp");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string str2 = Path.Combine(path, Guid.NewGuid().ToString());
                    if (!Directory.Exists(str2))
                    {
                        Directory.CreateDirectory(str2);
                    }
                    File.Copy(info.FullName, Path.Combine(str2, "AddedRealTax.dll"), true);
                    FileCryptEx(2, Path.Combine(str2, "AddedRealTax.dll"), softVersionInfo.TaxCode);
                    element = document.CreateElement("CRCValue");
                    element.InnerText = MD5Tools.smethod_0(Path.Combine(str2, "AddedRealTax.dll"));
                    element2.AppendChild(element);
                    File.Delete(Path.Combine(str2, "AddedRealTax.dll"));
                    Directory.Delete(str2);
                }
                else
                {
                    element = document.CreateElement("CRCValue");
                    element.InnerText = MD5Tools.smethod_0(info.FullName);
                    element2.AppendChild(element);
                }
            }
            element = document.CreateElement("TotalSize");
            element.InnerText = num4.ToString();
            element3.AppendChild(element);
            return document.InnerXml;
        }

        public static string NoticeListToXml(IList<NoticeInfo> noticeList)
        {
            if ((noticeList == null) || (noticeList.Count <= 0))
            {
                return "";
            }
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            document.PreserveWhitespace = false;
            document.AppendChild(newChild);
            XmlElement element = document.CreateElement("NoticeList");
            document.AppendChild(element);
            foreach (NoticeInfo info in noticeList)
            {
                XmlElement element2 = document.CreateElement("Notice");
                element.AppendChild(element2);
                XmlElement element3 = document.CreateElement("NoticeId");
                element3.InnerText = info.NoticeId.ToString();
                element2.AppendChild(element3);
                element3 = document.CreateElement("NoticeType");
                element3.InnerText = info.NoticeType.ToString();
                element2.AppendChild(element3);
                element3 = document.CreateElement("NoticeTitle");
                element3.InnerText = info.NoticeTitle;
                element2.AppendChild(element3);
                element3 = document.CreateElement("NoticeContent");
                element3.InnerText = info.NoticeContent;
                element2.AppendChild(element3);
                element3 = document.CreateElement("PublishDate");
                element3.InnerText = info.PublishDate.ToString("yyyyMMddHHmmss");
                element2.AppendChild(element3);
                element3 = document.CreateElement("PID");
                element3.InnerText = info.PID.ToString();
                element2.AppendChild(element3);
            }
            return document.InnerXml;
        }

        public static string SoftListToXml(IList<SoftInfo> softList)
        {
            if ((softList == null) || (softList.Count <= 0))
            {
                return "";
            }
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            document.PreserveWhitespace = false;
            document.AppendChild(newChild);
            XmlElement element = document.CreateElement("SoftList");
            document.AppendChild(element);
            foreach (SoftInfo info in softList)
            {
                XmlElement element2 = document.CreateElement("Soft");
                element.AppendChild(element2);
                XmlElement element3 = document.CreateElement("SoftId");
                element3.InnerText = info.SoftId.ToString();
                element2.AppendChild(element3);
                element3 = document.CreateElement("SoftName");
                element3.InnerText = info.SoftName;
                element2.AppendChild(element3);
                element3 = document.CreateElement("SoftDesc");
                element3.InnerText = info.SoftDesc;
                element2.AppendChild(element3);
            }
            return document.InnerXml;
        }

        public static string VerListInfoToXml(List<SoftVersionInfo> verList)
        {
            if ((verList == null) || (verList.Count <= 0))
            {
                return "";
            }
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            document.PreserveWhitespace = false;
            document.AppendChild(newChild);
            XmlElement element = document.CreateElement("VerList");
            document.AppendChild(element);
            foreach (SoftVersionInfo info in verList)
            {
                if (Directory.Exists(info.FilePath))
                {
                    XmlElement element2 = document.CreateElement("FileListInfo");
                    element.AppendChild(element2);
                    XmlElement element3 = document.CreateElement("SoftVersionId");
                    element3.InnerText = info.SoftVersionId.ToString();
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("ClientName");
                    element3.InnerText = info.ClientName;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("ClientIp");
                    element3.InnerText = info.ClientIp;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("ClientPort");
                    element3.InnerText = info.ClientPort.ToString();
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("SoftName");
                    element3.InnerText = info.SoftName;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("Version");
                    element3.InnerText = info.Version;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("Force");
                    element3.InnerText = info.Force ? "1" : "0";
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("IsUses");
                    element3.InnerText = info.IsUses ? "1" : "0";
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("VerDesc");
                    element3.InnerText = info.VerDesc;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("BserverStr");
                    element3.InnerText = info.BserverStr;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("EnableStr");
                    element3.InnerText = info.EnableStr;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("RelativeSofts");
                    element3.InnerText = info.RelativeSofts;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("RestrictVer");
                    element3.InnerText = info.RestrictVer;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("DisableStr");
                    element3.InnerText = info.DisableStr;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("FilePath");
                    element3.InnerText = info.FilePath;
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("StartTime");
                    element3.InnerText = info.StartTime.ToString("yyyyMMddHHmmss");
                    element2.AppendChild(element3);
                    element3 = document.CreateElement("ExpireTime");
                    element3.InnerText = info.ExpireTime.ToString("yyyyMMddHHmmss");
                    element2.AppendChild(element3);
                    XmlElement element4 = document.CreateElement("FileList");
                    element2.AppendChild(element4);
                    string searchPattern = "*.*";
                    FileInfo[] files = new DirectoryInfo(info.FilePath).GetFiles(searchPattern, SearchOption.AllDirectories);
                    long num2 = 0L;
                    foreach (FileInfo info3 in files)
                    {
                        XmlElement element5 = document.CreateElement("SoftFileInfo");
                        element4.AppendChild(element5);
                        element3 = document.CreateElement("RelativePath");
                        if (info.FilePath.EndsWith(@"\"))
                        {
                            element3.InnerText = info3.FullName.Substring(info.FilePath.Length);
                        }
                        else
                        {
                            element3.InnerText = info3.FullName.Substring(info.FilePath.Length + 1);
                        }
                        element5.AppendChild(element3);
                        element3 = document.CreateElement("Length");
                        element3.InnerText = info3.Length.ToString();
                        element5.AppendChild(element3);
                        num2 += info3.Length;
                        element3 = document.CreateElement("CreationTime");
                        element3.InnerText = info3.CreationTime.ToString("yyyyMMddHHmmss");
                        element5.AppendChild(element3);
                        element3 = document.CreateElement("LastAccessTime");
                        element3.InnerText = info3.LastAccessTime.ToString("yyyyMMddHHmmss");
                        element5.AppendChild(element3);
                        element3 = document.CreateElement("LastWriteTime");
                        element3.InnerText = info3.LastWriteTime.ToString("yyyyMMddHHmmss");
                        element5.AppendChild(element3);
                        if ((info3.FullName.Contains("AddedRealTax.dll") && (info.TaxCode != null)) && (info.TaxCode != ""))
                        {
                            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "Temp");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            string str4 = Path.Combine(path, Guid.NewGuid().ToString());
                            if (!Directory.Exists(str4))
                            {
                                Directory.CreateDirectory(str4);
                            }
                            File.Copy(info3.FullName, Path.Combine(str4, "AddedRealTax.dll"), true);
                            FileCryptEx(2, Path.Combine(str4, "AddedRealTax.dll"), info.TaxCode);
                            element3 = document.CreateElement("CRCValue");
                            element3.InnerText = MD5Tools.smethod_0(Path.Combine(str4, "AddedRealTax.dll"));
                            element5.AppendChild(element3);
                            File.Delete(Path.Combine(str4, "AddedRealTax.dll"));
                            Directory.Delete(str4);
                        }
                        else
                        {
                            element3 = document.CreateElement("CRCValue");
                            element3.InnerText = MD5Tools.smethod_0(info3.FullName);
                            element5.AppendChild(element3);
                        }
                    }
                    element3 = document.CreateElement("TotalSize");
                    element3.InnerText = num2.ToString();
                    element2.AppendChild(element3);
                }
            }
            return document.InnerXml;
        }

        public static DownloadInfo XmlToDownloadInfo(XmlNode selectNode)
        {
            try
            {
                DownloadInfo info = new DownloadInfo();
                XmlNode node = selectNode.SelectSingleNode("ClientName");
                info.SoftVer.ClientName = node.InnerText;
                node = selectNode.SelectSingleNode("ClientIp");
                info.SoftVer.ClientIp = node.InnerText;
                node = selectNode.SelectSingleNode("ClientPort");
                info.SoftVer.ClientPort = int.Parse(node.InnerText);
                node = selectNode.SelectSingleNode("SoftName");
                info.SoftVer.SoftName = node.InnerText;
                node = selectNode.SelectSingleNode("Version");
                info.SoftVer.Version = node.InnerText;
                node = selectNode.SelectSingleNode("Force");
                info.SoftVer.Force = node.InnerText.Trim() == "1";
                node = selectNode.SelectSingleNode("IsUses");
                if (node != null)
                {
                    info.SoftVer.IsUses = node.InnerText.Trim() == "1";
                }
                node = selectNode.SelectSingleNode("VerDesc");
                info.SoftVer.VerDesc = node.InnerText;
                node = selectNode.SelectSingleNode("BserverStr");
                info.SoftVer.BserverStr = node.InnerText;
                node = selectNode.SelectSingleNode("EnableStr");
                info.SoftVer.EnableStr = node.InnerText;
                node = selectNode.SelectSingleNode("DisableStr");
                info.SoftVer.DisableStr = node.InnerText;
                node = selectNode.SelectSingleNode("FilePath");
                info.SoftVer.FilePath = node.InnerText;
                node = selectNode.SelectSingleNode("StartTime");
                info.SoftVer.StartTime = DateTime.ParseExact(node.InnerText, "yyyyMMddHHmmss", CultureInfo.CurrentCulture);
                node = selectNode.SelectSingleNode("ExpireTime");
                info.SoftVer.ExpireTime = DateTime.ParseExact(node.InnerText, "yyyyMMddHHmmss", CultureInfo.CurrentCulture);
                node = selectNode.SelectSingleNode("Xzbz");
                info.Xzbz = int.Parse(node.InnerText);
                return info;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DownloadInfo XmlToFileListInfo(string xml)
        {
            if (xml == "")
            {
                return null;
            }
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);
                XmlElement documentElement = document.DocumentElement;
                DownloadInfo info2 = new DownloadInfo();
                XmlNode node = documentElement.SelectSingleNode("ClientName");
                info2.SoftVer.ClientName = node.InnerText;
                node = documentElement.SelectSingleNode("ClientIp");
                info2.SoftVer.ClientIp = node.InnerText;
                node = documentElement.SelectSingleNode("ClientPort");
                info2.SoftVer.ClientPort = int.Parse(node.InnerText);
                node = documentElement.SelectSingleNode("SoftName");
                info2.SoftVer.SoftName = node.InnerText;
                node = documentElement.SelectSingleNode("Version");
                info2.SoftVer.Version = node.InnerText;
                node = documentElement.SelectSingleNode("Force");
                info2.SoftVer.Force = node.InnerText.Trim() == "1";
                node = documentElement.SelectSingleNode("VerDesc");
                info2.SoftVer.VerDesc = node.InnerText;
                node = documentElement.SelectSingleNode("BserverStr");
                info2.SoftVer.BserverStr = node.InnerText;
                node = documentElement.SelectSingleNode("EnableStr");
                info2.SoftVer.EnableStr = node.InnerText;
                node = documentElement.SelectSingleNode("DisableStr");
                info2.SoftVer.DisableStr = node.InnerText;
                node = documentElement.SelectSingleNode("FilePath");
                info2.SoftVer.FilePath = node.InnerText;
                node = documentElement.SelectSingleNode("StartTime");
                info2.SoftVer.StartTime = DateTime.ParseExact(node.InnerText, "yyyyMMddHHmmss", CultureInfo.CurrentCulture);
                node = documentElement.SelectSingleNode("ExpireTime");
                info2.SoftVer.ExpireTime = DateTime.ParseExact(node.InnerText, "yyyyMMddHHmmss", CultureInfo.CurrentCulture);
                node = documentElement.SelectSingleNode("TotalSize");
                info2.TotalSize = long.Parse(node.InnerText);
                info2.FileList = new List<SoftFileInfo>();
                foreach (XmlNode node3 in documentElement.SelectSingleNode("FileList").ChildNodes)
                {
                    SoftFileInfo item = new SoftFileInfo {
                        RelativePath = node3.SelectSingleNode("RelativePath").InnerText,
                        Length = int.Parse(node3.SelectSingleNode("Length").InnerText),
                        CreationTime = DateTime.ParseExact(node3.SelectSingleNode("CreationTime").InnerText, "yyyyMMddHHmmss", CultureInfo.CurrentCulture),
                        LastAccessTime = DateTime.ParseExact(node3.SelectSingleNode("LastAccessTime").InnerText, "yyyyMMddHHmmss", CultureInfo.CurrentCulture),
                        LastWriteTime = DateTime.ParseExact(node3.SelectSingleNode("LastWriteTime").InnerText, "yyyyMMddHHmmss", CultureInfo.CurrentCulture),
                        CRCValue = node3.SelectSingleNode("CRCValue").InnerText
                    };
                    info2.FileList.Add(item);
                }
                return info2;
            }
            catch (Exception exception)
            {
                ilog_0.Info("XmlToFileListInfo" + exception.Message);
                return null;
            }
        }

        public static IList<NoticeInfo> XmlToNoticeList(string xml)
        {
            if (xml == "")
            {
                return null;
            }
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);
                XmlElement documentElement = document.DocumentElement;
                IList<NoticeInfo> list2 = new List<NoticeInfo>();
                foreach (XmlNode node in documentElement.ChildNodes)
                {
                    NoticeInfo item = new NoticeInfo();
                    XmlNode node2 = node.SelectSingleNode("NoticeId");
                    item.NoticeId = int.Parse(node2.InnerText);
                    node2 = node.SelectSingleNode("NoticeType");
                    item.NoticeType = int.Parse(node2.InnerText);
                    node2 = node.SelectSingleNode("NoticeTitle");
                    item.NoticeTitle = node2.InnerText;
                    node2 = node.SelectSingleNode("NoticeContent");
                    item.NoticeContent = node2.InnerText;
                    node2 = node.SelectSingleNode("PublishDate");
                    item.PublishDate = DateTime.ParseExact(node2.InnerText, "yyyyMMddHHmmss", CultureInfo.CurrentCulture);
                    node2 = node.SelectSingleNode("PID");
                    item.PID = int.Parse(node2.InnerText);
                    list2.Add(item);
                }
                return list2;
            }
            catch (Exception exception)
            {
                ilog_0.Info("XmlToNoticeList" + exception.Message);
                return null;
            }
        }

        public static IList<SoftInfo> XmlToSoftList(string xml)
        {
            if (xml == "")
            {
                return null;
            }
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);
                XmlElement documentElement = document.DocumentElement;
                IList<SoftInfo> list2 = new List<SoftInfo>();
                foreach (XmlNode node in documentElement.ChildNodes)
                {
                    SoftInfo item = new SoftInfo();
                    XmlNode node2 = node.SelectSingleNode("SoftId");
                    item.SoftId = int.Parse(node2.InnerText);
                    node2 = node.SelectSingleNode("SoftName");
                    item.SoftName = node2.InnerText;
                    node2 = node.SelectSingleNode("SoftDesc");
                    item.SoftDesc = node2.InnerText;
                    list2.Add(item);
                }
                return list2;
            }
            catch (Exception exception)
            {
                ilog_0.Info("XmlToSoftList" + exception.Message);
                return null;
            }
        }

        public static List<DownloadInfo> XmlToVerList(string xml)
        {
            if (xml == "")
            {
                return null;
            }
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);
                XmlElement documentElement = document.DocumentElement;
                List<DownloadInfo> list2 = new List<DownloadInfo>();
                foreach (XmlNode node in documentElement.ChildNodes)
                {
                    DownloadInfo item = new DownloadInfo();
                    XmlNode node2 = node.SelectSingleNode("SoftVersionId");
                    item.SoftVer.SoftVersionId = int.Parse(node2.InnerText);
                    node2 = node.SelectSingleNode("ClientName");
                    item.SoftVer.ClientName = node2.InnerText;
                    node2 = node.SelectSingleNode("ClientIp");
                    item.SoftVer.ClientIp = node2.InnerText;
                    node2 = node.SelectSingleNode("ClientPort");
                    item.SoftVer.ClientPort = int.Parse(node2.InnerText);
                    node2 = node.SelectSingleNode("SoftName");
                    item.SoftVer.SoftName = node2.InnerText;
                    node2 = node.SelectSingleNode("Version");
                    item.SoftVer.Version = node2.InnerText;
                    node2 = node.SelectSingleNode("Force");
                    item.SoftVer.Force = node2.InnerText.Trim() == "1";
                    node2 = node.SelectSingleNode("IsUses");
                    if (node2 != null)
                    {
                        item.SoftVer.IsUses = node2.InnerText.Trim() == "1";
                    }
                    node2 = node.SelectSingleNode("VerDesc");
                    item.SoftVer.VerDesc = node2.InnerText;
                    node2 = node.SelectSingleNode("BserverStr");
                    item.SoftVer.BserverStr = node2.InnerText;
                    node2 = node.SelectSingleNode("EnableStr");
                    item.SoftVer.EnableStr = node2.InnerText;
                    node2 = node.SelectSingleNode("DisableStr");
                    item.SoftVer.DisableStr = node2.InnerText;
                    node2 = node.SelectSingleNode("RelativeSofts");
                    item.SoftVer.RelativeSofts = node2.InnerText;
                    node2 = node.SelectSingleNode("RestrictVer");
                    item.SoftVer.RestrictVer = node2.InnerText;
                    node2 = node.SelectSingleNode("FilePath");
                    item.SoftVer.FilePath = node2.InnerText;
                    node2 = node.SelectSingleNode("StartTime");
                    item.SoftVer.StartTime = DateTime.ParseExact(node2.InnerText, "yyyyMMddHHmmss", CultureInfo.CurrentCulture);
                    node2 = node.SelectSingleNode("ExpireTime");
                    item.SoftVer.ExpireTime = DateTime.ParseExact(node2.InnerText, "yyyyMMddHHmmss", CultureInfo.CurrentCulture);
                    node2 = node.SelectSingleNode("TotalSize");
                    item.TotalSize = long.Parse(node2.InnerText);
                    item.FileList = new List<SoftFileInfo>();
                    foreach (XmlNode node4 in node.SelectSingleNode("FileList").ChildNodes)
                    {
                        SoftFileInfo info2 = new SoftFileInfo {
                            RelativePath = node4.SelectSingleNode("RelativePath").InnerText,
                            Length = int.Parse(node4.SelectSingleNode("Length").InnerText),
                            CreationTime = DateTime.ParseExact(node4.SelectSingleNode("CreationTime").InnerText, "yyyyMMddHHmmss", CultureInfo.CurrentCulture),
                            LastAccessTime = DateTime.ParseExact(node4.SelectSingleNode("LastAccessTime").InnerText, "yyyyMMddHHmmss", CultureInfo.CurrentCulture),
                            LastWriteTime = DateTime.ParseExact(node4.SelectSingleNode("LastWriteTime").InnerText, "yyyyMMddHHmmss", CultureInfo.CurrentCulture),
                            CRCValue = node4.SelectSingleNode("CRCValue").InnerText
                        };
                        item.FileList.Add(info2);
                    }
                    list2.Add(item);
                }
                return list2;
            }
            catch (Exception exception)
            {
                ilog_0.Info("XmlToFileListInfo" + exception.Message);
                return null;
            }
        }
    }
}

