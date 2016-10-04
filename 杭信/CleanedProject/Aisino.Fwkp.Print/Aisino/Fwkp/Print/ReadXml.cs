namespace Aisino.Fwkp.Print
{
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Xml;

    public class ReadXml
    {
        public Dictionary<string, PrintFileModel> dict;
        public static int int_0;
        private static ReadXml readXml_0;
        private string string_0;
        public Dictionary<string, PrintZheHangModel> ZheHang;

        static ReadXml()
        {
            
            int_0 = 1;
        }

        private ReadXml()
        {
            
            this.string_0 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\Config\Print\PrintConfig.xml");
            this.dict = new Dictionary<string, PrintFileModel>();
            this.ZheHang = new Dictionary<string, PrintZheHangModel>();
            if (File.Exists(this.string_0))
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
                FileStream stream = new FileStream(this.string_0, FileMode.Open);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                byte[] buffer8 = AES_Crypt.Decrypt(buffer, buffer5, buffer6, null);
                if (buffer8 == null)
                {
                    return;
                }
                document.Load(new XmlTextReader(new MemoryStream(buffer8)));
                foreach (XmlNode node in document.DocumentElement.ChildNodes)
                {
                    XmlElement element = node as XmlElement;
                    if ((element != null) && (element.Name == "Print"))
                    {
                        string attribute = element.GetAttribute("Id");
                        if (!this.ContainsKey(attribute))
                        {
                            PrintFileModel model = new PrintFileModel {
                                Id = attribute,
                                CanvasName = element.GetAttribute("Canvas"),
                                AssemblyName = element.GetAttribute("Assembly"),
                                ClassName = element.GetAttribute("Class")
                            };
                            this.Add(new KeyValuePair<string, PrintFileModel>(attribute, model));
                        }
                    }
                    if ((element != null) && (element.Name == "Zh"))
                    {
                        foreach (XmlNode node2 in element.ChildNodes)
                        {
                            XmlElement element2 = node2 as XmlElement;
                            if ((element2 != null) && (element2.Name == "Item"))
                            {
                                string key = element2.GetAttribute("Id");
                                string str3 = element2.GetAttribute("ConfigId");
                                string str4 = element2.GetAttribute("TempId");
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
                    if ((element != null) && (element.Name == "QRM"))
                    {
                        XmlNodeList childNodes = element.ChildNodes;
                        int_0 = Common.ObjectToInt(element.GetAttribute("IsPrint"));
                    }
                }
            }
            Dictionary<string, int> jsPrintTemplate = ToolUtil.GetJsPrintTemplate();
            if (jsPrintTemplate.Count > 0)
            {
                foreach (string str6 in jsPrintTemplate.Keys)
                {
                    if (!this.ContainsKey(str6))
                    {
                        PrintFileModel model3 = new PrintFileModel {
                            Id = str6,
                            CanvasName = str6 + ".xml",
                            AssemblyName = "",
                            ClassName = ""
                        };
                        this.Add(new KeyValuePair<string, PrintFileModel>(str6, model3));
                    }
                }
            }
        }

        public void Add(KeyValuePair<string, PrintFileModel> item)
        {
            this.Add(item.Key, item.Value);
        }

        public void Add(string string_1, PrintFileModel printFileModel_0)
        {
            this.dict.Add(string_1, printFileModel_0);
        }

        public void Clear()
        {
            this.dict.Clear();
        }

        public bool ContainsKey(string string_1)
        {
            return this.dict.ContainsKey(string_1);
        }

        public static ReadXml Get()
        {
            if (readXml_0 == null)
            {
                readXml_0 = new ReadXml();
            }
            return readXml_0;
        }

        public bool Remove(string string_1)
        {
            return this.dict.Remove(string_1);
        }

        public bool TryGetValue(string string_1, out PrintFileModel printFileModel_0)
        {
            return this.dict.TryGetValue(string_1, out printFileModel_0);
        }

        public int Count
        {
            get
            {
                return this.dict.Count;
            }
        }

        public PrintFileModel this[string string_1]
        {
            get
            {
                return this.dict[string_1];
            }
            set
            {
                this.dict[string_1] = value;
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

