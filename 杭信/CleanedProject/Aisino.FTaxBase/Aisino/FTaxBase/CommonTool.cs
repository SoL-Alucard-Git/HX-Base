namespace Aisino.FTaxBase
{
    using Microsoft.Win32;
    using ns2;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml;

    public static class CommonTool
    {
        public static string pathVolumns;
        private static string string_0;
        private static string string_1;
        private static string string_2;
        private static string string_3;
        private static string string_4;
        private static string string_5;

        static CommonTool()
        {
            
            string_0 = string.Empty;
            string_1 = string.Empty;
            string_2 = string.Empty;
            string_3 = string.Empty;
            string_4 = string.Empty;
            string_5 = string.Empty;
            pathVolumns = smethod_4(@"\Config\Volumns.xml");
        }

        public static DateTime AcqTaxDateTime(byte byte_0, byte byte_1, byte byte_2, byte byte_3)
        {
            return AcqTaxDateTime(byte_0, byte_1, byte_2, byte_3, 0, 0, 0);
        }

        public static DateTime AcqTaxDateTime(byte[] byte_0, int int_0, int int_1, int int_2)
        {
            try
            {
                byte[] buffer = new byte[7];
                for (int i = 0; i < int_1; i++)
                {
                    buffer[i] = byte_0[int_0 + (i * int_2)];
                }
                return AcqTaxDateTime(buffer[0], buffer[1], buffer[2], buffer[3], buffer[4], buffer[5], buffer[6]);
            }
            catch
            {
                return DateTime.Parse("1900-01-01");
            }
        }

        public static DateTime AcqTaxDateTime(byte byte_0, byte byte_1, byte byte_2, byte byte_3, byte byte_4, byte byte_5, byte byte_6)
        {
            try
            {
                return DateTime.Parse(string.Format("{0}{1}-{2}-{3} {4}:{5}:{6}", new object[] { byte_0, byte_1, byte_2, byte_3, byte_4, byte_5, byte_6 }));
            }
            catch
            {
                return DateTime.Parse("1900-01-01");
            }
        }

        public static byte[] ByteArrayMerge(byte[] byte_0, byte[] byte_1)
        {
            byte[] array = new byte[byte_0.Length + byte_1.Length];
            byte_0.CopyTo(array, 0);
            byte_1.CopyTo(array, byte_0.Length);
            return array;
        }

        public static T BytesToStruct<T>(byte[] byte_0, int int_0)
        {
            T local2;
            T local = default(T);
            Type t = typeof(T);
            int cb = Marshal.SizeOf(t);
            IntPtr destination = Marshal.AllocHGlobal(cb);
            try
            {
                Marshal.Copy(byte_0, int_0, destination, cb);
                local = (T) Marshal.PtrToStructure(destination, t);
                local2 = local;
            }
            finally
            {
                Marshal.FreeHGlobal(destination);
            }
            return local2;
        }

        public static double CharArrayToDouble(char[] char_0)
        {
            return Convert.ToDouble(CharArrayToStr(char_0));
        }

        public static string CharArrayToStr(char[] char_0)
        {
            string str = new string(char_0);
            return str.Remove(str.IndexOf("\0"));
        }

        public static void CreateVolumnsXml()
        {
            if (!File.Exists(pathVolumns))
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "utf-8", null);
                document.AppendChild(newChild);
                XmlElement element = document.CreateElement("Volumns");
                document.AppendChild(element);
                document.Save(pathVolumns);
            }
        }

        public static void DelVolumns(DateTime dateTime_0)
        {
            CreateVolumnsXml();
            XmlDocument document = new XmlDocument();
            document.Load(pathVolumns);
            XmlElement documentElement = document.DocumentElement as XmlElement;
            for (int i = 0; i < documentElement.ChildNodes.Count; i++)
            {
                XmlNode oldChild = documentElement.ChildNodes[i];
                if (DateTime.Parse(((XmlElement) oldChild).GetAttribute("BuyDate")) < dateTime_0)
                {
                    documentElement.RemoveChild(oldChild);
                    i--;
                }
            }
            document.Save(pathVolumns);
        }

        public static string GetCert()
        {
            try
            {
                string str = smethod_1("cert");
                if (string.IsNullOrWhiteSpace(str))
                {
                    string keyName = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe";
                    string valueName = "cert";
                    str = (string) Registry.GetValue(keyName, valueName, "");
                }
                return str;
            }
            catch
            {
                return "";
            }
        }

        public static string GetCertProvider()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "CertProviderCfg.txt");
            Class20.smethod_1("CertProviderCfg.txt文件路径：" + path);
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            return "";
        }

        public static string GetInvSignServer()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "InvSignServer.txt");
            Class20.smethod_1("InvSignServer.txt文件路径：" + path);
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            return "Card";
        }

        public static string GetMachine()
        {
            try
            {
                string str = smethod_1("machine");
                if (string.IsNullOrWhiteSpace(str))
                {
                    string keyName = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe";
                    string valueName = "machine";
                    str = (string) Registry.GetValue(keyName, valueName, "");
                }
                return str;
            }
            catch
            {
                return "";
            }
        }

        public static string GetOrgCode()
        {
            try
            {
                string str = smethod_1("orgcode");
                if (string.IsNullOrWhiteSpace(str))
                {
                    string keyName = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe";
                    string valueName = "orgcode";
                    str = (string) Registry.GetValue(keyName, valueName, "");
                }
                return str;
            }
            catch
            {
                return "";
            }
        }

        public static string GetStingByteArray(byte[] byte_0)
        {
            return GetStingByteArray(byte_0, 0);
        }

        public static string GetStingByteArray(byte[] byte_0, int int_0)
        {
            int index = int_0;
            while ((byte_0.Length - 1) > index)
            {
                if (byte_0[index] == 0)
                {
                    break;
                }
                index++;
            }
            return Encoding.Default.GetString(byte_0, int_0, index - int_0);
        }

        public static string GetStingByteArray(byte[] byte_0, int int_0, out int int_1)
        {
            int index = int_0;
            while ((byte_0.Length - 1) > index)
            {
                if (byte_0[index] == 0)
                {
                    break;
                }
                index++;
            }
            int_1 = index;
            return Encoding.Default.GetString(byte_0, int_0, index - int_0);
        }

        public static string GetStringFromBuffer(byte[] byte_0, int int_0, int int_1)
        {
            string str2 = Encoding.Default.GetString(byte_0, int_0, int_1);
            int length = -1;
            length = str2.IndexOf("\0");
            if (length > -1)
            {
                return str2.Substring(0, length);
            }
            return str2;
        }

        public static byte[] GetSubArray(byte[] byte_0, int int_0, int int_1)
        {
            byte[] buffer = new byte[int_1];
            for (int i = 0; i < int_1; i++)
            {
                buffer[i] = byte_0[i + int_0];
            }
            return buffer;
        }

        public static string GetTaxCode()
        {
            try
            {
                string str = smethod_1("code");
                if (string.IsNullOrWhiteSpace(str))
                {
                    string keyName = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe";
                    string valueName = "code";
                    str = (string) Registry.GetValue(keyName, valueName, "");
                }
                return str;
            }
            catch
            {
                return "";
            }
        }

        public static InvVolumeApp GetVolumn(byte byte_0, string string_6, uint uint_0)
        {
            CreateVolumnsXml();
            InvVolumeApp app = new InvVolumeApp();
            string str = byte_0.ToString();
            string str2 = uint_0.ToString();
            using (XmlTextReader reader = new XmlTextReader("Volumns.xml"))
            {
                reader.WhitespaceHandling = WhitespaceHandling.None;
                while (reader.Read())
                {
                    if (((reader.NodeType == XmlNodeType.Element) && (reader.Name == "InvVolume")) && (((str == reader.GetAttribute("InvType")) && (string_6 == reader.GetAttribute("TypeCode"))) && (str2 == reader.GetAttribute("HeadCode"))))
                    {
                        goto Label_0090;
                    }
                }
                return app;
            Label_0090:
                app.InvType = byte.Parse(reader.GetAttribute("InvType"));
                app.InvLimit = long.Parse(reader.GetAttribute("InvLimit"));
                app.BuyDate = DateTime.Parse(reader.GetAttribute("BuyDate"));
                app.SaledDate = DateTime.Parse(reader.GetAttribute("SaledDate"));
                app.TypeCode = reader.GetAttribute("TypeCode");
                app.HeadCode = uint.Parse(reader.GetAttribute("HeadCode"));
                app.BuyNumber = int.Parse(reader.GetAttribute("BuyNumber"));
                app.Number = ushort.Parse(reader.GetAttribute("Number"));
                app.Status = reader.GetAttribute("Status").ToCharArray()[0];
            }
            return app;
        }

        public static List<InvVolumeApp> GetVolumns()
        {
            List<InvVolumeApp> list = new List<InvVolumeApp>();
            try
            {
                CreateVolumnsXml();
                using (XmlTextReader reader = new XmlTextReader(pathVolumns))
                {
                    reader.WhitespaceHandling = WhitespaceHandling.None;
                    while (reader.Read())
                    {
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "InvVolume"))
                        {
                            InvVolumeApp item = new InvVolumeApp {
                                InvType = byte.Parse(reader.GetAttribute("InvType")),
                                InvLimit = long.Parse(reader.GetAttribute("InvLimit")),
                                BuyDate = DateTime.Parse(reader.GetAttribute("BuyDate")),
                                SaledDate = DateTime.Parse(reader.GetAttribute("SaledDate")),
                                TypeCode = reader.GetAttribute("TypeCode"),
                                HeadCode = uint.Parse(reader.GetAttribute("HeadCode")),
                                BuyNumber = int.Parse(reader.GetAttribute("BuyNumber")),
                                Number = ushort.Parse(reader.GetAttribute("Number")),
                                Status = reader.GetAttribute("Status").ToCharArray()[0]
                            };
                            list.Add(item);
                        }
                    }
                }
                return list;
            }
            catch (FileNotFoundException exception)
            {
                Class20.smethod_2(exception.ToString());
                return null;
            }
        }

        public static void SetInBytes(byte[] byte_0, byte[] byte_1, int int_0)
        {
            for (int i = 0; i < byte_1.Length; i++)
            {
                byte_0[int_0 + i] = byte_1[i];
            }
        }

        public static void SetVolumnInvoice(byte byte_0, string string_6, uint uint_0)
        {
            smethod_7(byte_0, string_6, uint_0, "Invoice");
        }

        public static void SetVolumnReturn(byte byte_0, string string_6, uint uint_0)
        {
            smethod_7(byte_0, string_6, uint_0, "return");
        }

        internal static string smethod_0(InvoiceType invoiceType_0)
        {
            string str = "";
            InvoiceType type = invoiceType_0;
            if (type <= InvoiceType.vehiclesales)
            {
                switch (type)
                {
                    case InvoiceType.special:
                        return "专用发票";

                    case InvoiceType.bk1:
                        return "";

                    case InvoiceType.common:
                        return "普通发票";

                    case InvoiceType.transportation:
                        return "货物运输业增值税专用发票";

                    case InvoiceType.vehiclesales:
                        return "机动车销售统一发票";
                }
                return str;
            }
            if (type != InvoiceType.volticket)
            {
                if (type == InvoiceType.Electronic)
                {
                    str = "电子增值税普通发票";
                }
                return str;
            }
            return "增值税普通发票(卷票)";
        }

        private static string smethod_1(string string_6)
        {
            string str = string.Empty;
            switch (string_6.ToUpper())
            {
                case "CODE":
                    str = string_0;
                    break;

                case "MACHINE":
                    str = string_1;
                    break;

                case "ORGCODE":
                    str = string_2;
                    break;

                case "DATABASEVERSION":
                    str = string_4;
                    break;

                case "VERSION":
                    str = string_3;
                    break;

                case "PATH":
                    str = string_5;
                    break;
            }
            if (string.IsNullOrWhiteSpace(str))
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "RegFile.txt");
                if (File.Exists(path))
                {
                    string[] strArray = File.ReadAllText(path).Split(new char[] { ';' });
                    string_0 = strArray[0];
                    string_1 = strArray[1];
                    string_2 = strArray[2];
                    string_3 = strArray[3];
                    string_4 = strArray[4];
                    string_5 = strArray[5];
                    switch (string_6.ToUpper())
                    {
                        case "CODE":
                            return string_0;

                        case "MACHINE":
                            return string_1;

                        case "ORGCODE":
                            return string_2;

                        case "DATABASEVERSION":
                            return string_4;

                        case "VERSION":
                            return string_3;

                        case "PATH":
                            return string_5;
                    }
                }
            }
            return str;
        }

        public static string smethod_2()
        {
            try
            {
                string keyName = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe";
                string valueName = "dzfpflag";
                return (string) Registry.GetValue(keyName, valueName, "");
            }
            catch
            {
                return "";
            }
        }

        internal static string smethod_3()
        {
            try
            {
                string str = smethod_1("Path");
                if (string.IsNullOrWhiteSpace(str))
                {
                    string keyName = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe";
                    string valueName = "Path";
                    str = (string) Registry.GetValue(keyName, valueName, "");
                }
                return str;
            }
            catch
            {
                return "";
            }
        }

        internal static string smethod_4(string string_6)
        {
            return (smethod_3() + string_6);
        }

        private static void smethod_5(InvVolumeApp string_6)
        {
            XmlDocument document = new XmlDocument();
            document.Load(pathVolumns);
            XmlNode documentElement = document.DocumentElement;
            XmlElement newChild = document.CreateElement("InvVolume");
            newChild.SetAttribute("InvType", string_6.InvType.ToString());
            newChild.SetAttribute("InvLimit", string_6.InvLimit.ToString());
            newChild.SetAttribute("BuyDate", string_6.BuyDate.ToShortDateString());
            newChild.SetAttribute("SaledDate", string_6.SaledDate.ToShortDateString());
            newChild.SetAttribute("TypeCode", string_6.TypeCode.ToString());
            newChild.SetAttribute("HeadCode", string_6.HeadCode.ToString());
            newChild.SetAttribute("BuyNumber", string_6.BuyNumber.ToString());
            newChild.SetAttribute("Number", string_6.Number.ToString());
            newChild.SetAttribute("Status", string_6.Status.ToString());
            documentElement.AppendChild(newChild);
            document.Save(pathVolumns);
        }

        internal static void smethod_6(List<InvVolumeApp> listInvVolumeApp)
        {
            CreateVolumnsXml();
            foreach (InvVolumeApp app in listInvVolumeApp)
            {
                smethod_5(app);
            }
        }

        private static void smethod_7(byte byte_0, string string_6, uint uint_0, string string_7)
        {
            CreateVolumnsXml();
            string str = byte_0.ToString();
            string str2 = uint_0.ToString();
            XmlDocument document = new XmlDocument();
            document.Load(pathVolumns);
            XmlElement documentElement = document.DocumentElement as XmlElement;
            for (int i = 0; i < documentElement.ChildNodes.Count; i++)
            {
                XmlNode node2 = documentElement.ChildNodes[i];
                XmlElement element2 = node2 as XmlElement;
                if (((str == element2.GetAttribute("InvType")) && (string_6 == element2.GetAttribute("TypeCode"))) && (str2 == element2.GetAttribute("HeadCode")))
                {
                    if (string_7 == "return")
                    {
                        element2.SetAttribute("Status", "C");
                    }
                    else if (string_7 == "Invoice")
                    {
                        int num2 = int.Parse(element2.GetAttribute("Number")) - 1;
                        if (num2 == 0)
                        {
                            element2.SetAttribute("Status", "0");
                        }
                        element2.SetAttribute("HeadCode", (uint_0 + 1).ToString());
                        element2.SetAttribute("Number", num2.ToString());
                    }
                    document.Save(pathVolumns);
                    return;
                }
            }
        }

        internal static void smethod_8(InvVolumeApp string_6, string string_7, int int_0)
        {
            CreateVolumnsXml();
            string typeCode = string_6.TypeCode;
            string str2 = string_6.InvType.ToString();
            string str3 = string_6.HeadCode.ToString();
            XmlDocument document = new XmlDocument();
            document.Load(pathVolumns);
            XmlElement documentElement = document.DocumentElement as XmlElement;
            for (int i = 0; i < documentElement.ChildNodes.Count; i++)
            {
                XmlNode node2 = documentElement.ChildNodes[i];
                XmlElement element2 = node2 as XmlElement;
                if (((str2 == element2.GetAttribute("InvType")) && (typeCode == element2.GetAttribute("TypeCode"))) && (str3 == element2.GetAttribute("HeadCode")))
                {
                    int num2 = int.Parse(element2.GetAttribute("Number")) - int_0;
                    if (num2 == 0)
                    {
                        element2.SetAttribute("Status", "0");
                    }
                    element2.SetAttribute("HeadCode", (int.Parse(string_7) + int_0).ToString());
                    element2.SetAttribute("Number", num2.ToString());
                    document.Save(pathVolumns);
                    return;
                }
            }
        }

        public static byte[] StringToBytes(string string_6)
        {
            byte[] buffer = new byte[string_6.Length];
            for (int i = 0; i < string_6.Length; i++)
            {
                buffer[i] = (byte) string_6[i];
            }
            return buffer;
        }

        public static long ToPow10(byte byte_0)
        {
            int num = byte_0;
            if ((num > 2) && (num <= 8))
            {
                return Convert.ToInt64(Math.Pow(10.0, (double) num));
            }
            return 0L;
        }

        public static long ToPow10(int int_0)
        {
            if ((int_0 > 2) && (int_0 <= 8))
            {
                return Convert.ToInt64(Math.Pow(10.0, (double) int_0));
            }
            return 0L;
        }
    }
}

