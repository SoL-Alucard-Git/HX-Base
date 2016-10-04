namespace Aisino.Framework.Plugin.Core.Controls.PrintControl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Xml;

    public class ReadXml
    {
        public Dictionary<string, PrintFileModel> dict;
        protected ILog loger;
        private static ReadXml readXml_0;
        private static string string_0;
        private static string string_1;
        private string string_2;
        public Dictionary<string, PrintZheHangModel> ZheHang;

        static ReadXml()
        {
            
            string_0 = Assembly.GetExecutingAssembly().Location;
            string_1 = Path.GetDirectoryName(string_0);
        }

        private ReadXml()
        {
            
            this.string_2 = Path.Combine(string_1, @"..\Config\Print\PrintConfig.xml");
            this.loger = LogUtil.GetLogger<ReadXml>();
            this.dict = new Dictionary<string, PrintFileModel>();
            this.ZheHang = new Dictionary<string, PrintZheHangModel>();
            if (string.Equals(TaxCardFactory.CreateTaxCard().SoftVersion, "FWKP_V2.0_Svr_Server"))
            {
                string_0 = Path.Combine(Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe").GetValue("Path").ToString(), @"Bin\");
                string_1 = Path.GetDirectoryName(string_0);
                this.string_2 = Path.Combine(string_1, @"..\Config\Print\PrintConfig.xml");
            }
            if (File.Exists(this.string_2))
            {
                XmlDocument document = new XmlDocument();
                byte[] sourceArray = Convert.FromBase64String("FZoo0+wH8AgXWEjMAFRnOVt+ZImrQik1jiVirx3SQzoTTc8H/D9o32mIm2Fb6CnC");
                byte[] destinationArray = new byte[0x20];
                Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
                byte[] buffer3 = new byte[0x10];
                Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
                byte[] buffer4 = AES_Crypt.Decrypt(Convert.FromBase64String("FkC25FGxr7ANG8kSXdMQ1dc1q5h2nMtkTSy90S2NQks6FTRmwMwaGUhrgVdlpMrhTSdJ9l7s5jbUyGMhyCd26w=="), destinationArray, buffer3, null);
                byte[] buffer5 = new byte[0x20];
                Array.Copy(buffer4, 0, buffer5, 0, 0x20);
                byte[] buffer6 = new byte[0x10];
                Array.Copy(buffer4, 0x20, buffer6, 0, 0x10);
                FileStream stream = new FileStream(this.string_2, FileMode.Open);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                byte[] buffer8 = AES_Crypt.Decrypt(buffer, buffer5, buffer6, null);
                document.Load(new XmlTextReader(new MemoryStream(buffer8)));
                foreach (XmlNode node in document.DocumentElement.ChildNodes)
                {
                    XmlElement element2 = node as XmlElement;
                    if ((element2 != null) && (element2.Name == "Print"))
                    {
                        string attribute = element2.GetAttribute("Id");
                        if (!this.ContainsKey(attribute))
                        {
                            PrintFileModel model = new PrintFileModel {
                                Id = attribute,
                                CanvasName = element2.GetAttribute("Canvas"),
                                AssemblyName = element2.GetAttribute("Assembly"),
                                ClassName = element2.GetAttribute("Class")
                            };
                            this.Add(new KeyValuePair<string, PrintFileModel>(attribute, model));
                        }
                    }
                    if ((element2 != null) && (element2.Name == "Zh"))
                    {
                        foreach (XmlNode node2 in element2.ChildNodes)
                        {
                            XmlElement element3 = node2 as XmlElement;
                            if ((element3 != null) && (element3.Name == "Item"))
                            {
                                string key = element3.GetAttribute("Id");
                                string str3 = element3.GetAttribute("ConfigId");
                                string str4 = element3.GetAttribute("TempId");
                                if (!this.ZheHang.ContainsKey(key))
                                {
                                    PrintZheHangModel model2 = new PrintZheHangModel {
                                        Id = key,
                                        ConfigId = str3,
                                        TempId = str4
                                    };
                                    this.ZheHang.Add(key, model2);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Add(KeyValuePair<string, PrintFileModel> item)
        {
            this.Add(item.Key, item.Value);
        }

        public void Add(string string_3, PrintFileModel printFileModel_0)
        {
            this.dict.Add(string_3, printFileModel_0);
        }

        public void Clear()
        {
            this.dict.Clear();
        }

        public bool ContainsKey(string string_3)
        {
            return this.dict.ContainsKey(string_3);
        }

        public static ReadXml Get()
        {
            if (readXml_0 == null)
            {
                readXml_0 = new ReadXml();
            }
            return readXml_0;
        }

        public bool Remove(string string_3)
        {
            return this.dict.Remove(string_3);
        }

        public bool TryGetValue(string string_3, out PrintFileModel printFileModel_0)
        {
            return this.dict.TryGetValue(string_3, out printFileModel_0);
        }

        public int Count
        {
            get
            {
                return this.dict.Count;
            }
        }

        public PrintFileModel this[string string_3]
        {
            get
            {
                return this.dict[string_3];
            }
            set
            {
                this.dict[string_3] = value;
            }
        }

        public ICollection<string> Keys
        {
            get
            {
                return this.dict.Keys;
            }
        }

        public ICollection<PrintFileModel> Values
        {
            get
            {
                return this.dict.Values;
            }
        }
    }
}

