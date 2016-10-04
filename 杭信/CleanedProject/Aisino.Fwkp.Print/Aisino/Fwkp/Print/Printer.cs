namespace Aisino.Fwkp.Print
{
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using Microsoft.Win32;
    using System;
    using System.Collections;
    using System.Drawing.Printing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Xml;

    public class Printer
    {
        public PrinterEventArgs DefaultPrinterArgs;
        private ILog ilog_0;
        private object[] object_0;
        private PrintDocument printDocument_0;
        public PrinterEventArgs RealPrinterArgs;
        [CompilerGenerated]
        private string string_0;
        public PrinterEventArgs UserPrinterArgs;

        public Printer(object[] object_1)
        {
            
            this.UserPrinterArgs = new PrinterEventArgs();
            this.DefaultPrinterArgs = new PrinterEventArgs();
            this.RealPrinterArgs = new PrinterEventArgs();
            this.ilog_0 = LogUtil.GetLogger<Printer>();
            this.printDocument_0 = new PrintDocument();
            this.object_0 = object_1;
            if ((object_1 != null) && (object_1.Length > 0))
            {
                string str = object_1[0] as string;
                this.Name = "";
                this.Name = this.Name + str[0];
                if (object_1.Length >= 4)
                {
                    this.Name = this.Name + object_1[3].ToString();
                }
            }
            else
            {
                this.Name = "user";
            }
        }

        public PrinterEventArgs GetPrinterArgs(bool bool_0 = false)
        {
            this.ilog_0.Debug("[发票打印]：IsZjFlag：" + bool_0.ToString());
            if (!bool_0)
            {
                this.method_0();
                return this.UserPrinterArgs;
            }
            this.ilog_0.Debug("[组件发票打印]：发票种类" + this.object_0[0].ToString());
            if ((this.object_0 == null) || (this.object_0.Length < 8))
            {
                return this.UserPrinterArgs;
            }
            string str = this.object_0[0].ToString();
            if (str != null)
            {
                if (((str == "s") || (str == "c")) || ((str == "f") || (str == "j")))
                {
                    if (this.object_0[4].ToString() == "")
                    {
                        this.RealPrinterArgs.PrinterName = this.printDocument_0.PrinterSettings.PrinterName;
                    }
                    else
                    {
                        this.RealPrinterArgs.PrinterName = this.object_0[4].ToString();
                    }
                    this.RealPrinterArgs.System = "1";
                    this.RealPrinterArgs.PageLenght = 0;
                    this.RealPrinterArgs.Name = "user";
                    this.RealPrinterArgs.Top = Common.ObjectToInt(this.object_0[5]);
                    this.RealPrinterArgs.Left = Common.ObjectToInt(this.object_0[6]);
                    this.RealPrinterArgs.IsQuanDa = Common.ObjectToBool(this.object_0[7]);
                }
                else if (str == "q")
                {
                    if (this.object_0[4].ToString() == "")
                    {
                        this.RealPrinterArgs.PrinterName = this.printDocument_0.PrinterSettings.PrinterName;
                    }
                    else
                    {
                        this.RealPrinterArgs.PrinterName = this.object_0[5].ToString();
                    }
                    this.RealPrinterArgs.System = "1";
                    this.RealPrinterArgs.PageLenght = 0;
                    this.RealPrinterArgs.Name = "user";
                    this.RealPrinterArgs.Top = Common.ObjectToInt(this.object_0[6]);
                    this.RealPrinterArgs.Left = Common.ObjectToInt(this.object_0[7]);
                    this.RealPrinterArgs.IsQuanDa = Common.ObjectToBool(this.object_0[8]);
                }
            }
            return this.RealPrinterArgs;
        }

        private void method_0()
        {
            try
            {
                this.UserPrinterArgs.System = "0";
                this.UserPrinterArgs.Top = 0f;
                this.UserPrinterArgs.Left = 0f;
                this.UserPrinterArgs.IsQuanDa = true;
                this.UserPrinterArgs.Name = "user";
                this.UserPrinterArgs.PageLenght = 0;
                this.DefaultPrinterArgs.System = "0";
                this.DefaultPrinterArgs.Top = 0f;
                this.DefaultPrinterArgs.Left = 0f;
                this.DefaultPrinterArgs.IsQuanDa = true;
                this.DefaultPrinterArgs.Name = "user";
                this.DefaultPrinterArgs.PageLenght = 0;
                this.method_1();
                this.method_2(this.UserPrinterArgs.PrinterName);
                this.RealPrinterArgs.PrinterName = this.UserPrinterArgs.PrinterName;
                this.RealPrinterArgs.System = this.UserPrinterArgs.System;
                this.RealPrinterArgs.PageLenght = this.UserPrinterArgs.PageLenght;
                this.RealPrinterArgs.Name = this.UserPrinterArgs.Name;
                this.RealPrinterArgs.IsQuanDa = this.UserPrinterArgs.IsQuanDa;
                this.RealPrinterArgs.Top = this.UserPrinterArgs.Top + this.DefaultPrinterArgs.Top;
                this.RealPrinterArgs.Left = this.UserPrinterArgs.Left + this.DefaultPrinterArgs.Left;
            }
            catch (Exception exception)
            {
                this.ilog_0.Error(exception.ToString());
            }
        }

        private void method_1()
        {
            string path = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Config\Print\PrinterManager.xml");
            XmlDocument document = new XmlDocument();
            try
            {
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
                FileStream stream = new FileStream(path, FileMode.Open);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                byte[] buffer8 = AES_Crypt.Decrypt(buffer, buffer5, buffer6, null);
                if (buffer8 == null)
                {
                    return;
                }
                document.Load(new XmlTextReader(new MemoryStream(buffer8)));
                XmlNode node = document.SelectSingleNode("/PrintConfig");
                new PrintDocument();
                string name = this.Name;
                XmlNodeList childNodes = node.ChildNodes;
                if ((childNodes == null) || (childNodes.Count == 0))
                {
                    goto Label_03ED;
                }
                bool flag = false;
                XmlElement element = null;
                IEnumerator enumerator = childNodes.GetEnumerator();
                {
                    XmlElement element2;
                    while (enumerator.MoveNext())
                    {
                        XmlNode current = (XmlNode) enumerator.Current;
                        element2 = current as XmlElement;
                        if (element2 != null)
                        {
                            XmlAttribute attribute = element2.Attributes["name"];
                            if (attribute.InnerText == this.Name)
                            {
                                goto Label_019C;
                            }
                            if (attribute.InnerText == "_QD")
                            {
                                element = element2;
                            }
                        }
                    }
                    goto Label_026B;
                Label_019C:
                    flag = true;
                    this.UserPrinterArgs.Name = element2.GetAttribute("name");
                    this.UserPrinterArgs.System = element2.GetAttribute("system");
                    this.UserPrinterArgs.Top = Common.ObjectToFloat(element2.GetAttribute("top"));
                    this.UserPrinterArgs.Left = Common.ObjectToFloat(element2.GetAttribute("left"));
                    this.UserPrinterArgs.PageLenght = Common.ObjectToInt(element2.GetAttribute("pageLenght"));
                    this.UserPrinterArgs.PrinterName = element2.GetAttribute("PrinterName");
                    this.UserPrinterArgs.IsQuanDa = Common.ObjectToBool(element2.GetAttribute("IsQuanDa"));
                }
            Label_026B:
                if (!flag && (element != null))
                {
                    XmlAttribute attribute2 = element.Attributes["name"];
                    if (attribute2.InnerText == "_QD")
                    {
                        flag = true;
                        this.UserPrinterArgs.Name = this.Name;
                        this.UserPrinterArgs.System = element.GetAttribute("system");
                        this.UserPrinterArgs.Top = Common.ObjectToFloat(element.GetAttribute("top"));
                        this.UserPrinterArgs.Left = Common.ObjectToFloat(element.GetAttribute("left"));
                        this.UserPrinterArgs.PageLenght = Common.ObjectToInt(element.GetAttribute("pageLenght"));
                        this.UserPrinterArgs.PrinterName = element.GetAttribute("PrinterName");
                        this.UserPrinterArgs.IsQuanDa = Common.ObjectToBool(element.GetAttribute("IsQuanDa"));
                    }
                }
                if ((this.UserPrinterArgs.PrinterName == null) || (this.UserPrinterArgs.PrinterName == ""))
                {
                    this.UserPrinterArgs.PrinterName = this.printDocument_0.PrinterSettings.PrinterName;
                    this.UserPrinterArgs.System = "1";
                    this.UserPrinterArgs.PageLenght = 0;
                    this.UserPrinterArgs.Name = "user";
                    this.UserPrinterArgs.Left = 0f;
                    this.UserPrinterArgs.Top = 0f;
                    this.UserPrinterArgs.IsQuanDa = true;
                }
                return;
            Label_03ED:
                this.UserPrinterArgs.PrinterName = this.printDocument_0.PrinterSettings.PrinterName;
                this.UserPrinterArgs.System = "1";
                this.UserPrinterArgs.PageLenght = 0;
                this.UserPrinterArgs.Name = "user";
                this.UserPrinterArgs.Left = 0f;
                this.UserPrinterArgs.Top = 0f;
                this.UserPrinterArgs.IsQuanDa = true;
            }
            catch (Exception)
            {
            }
        }

        private bool method_2(string string_1)
        {
            string path = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), "Config/Print/DefaultPrinterManager.xml");
            XmlDocument document = new XmlDocument();
            bool flag = false;
            try
            {
                string str3 = string_1;
                int num = str3.LastIndexOf(@"\");
                if (num != -1)
                {
                    str3 = str3.Substring(num + 1);
                }
                string str4 = str3;
                try
                {
                    object obj2 = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers\" + str3 + @"\PrinterDriverData", "Model", "");
                    if ((obj2 != null) && (obj2.ToString().Length > 0))
                    {
                        flag = true;
                        str4 = obj2.ToString();
                    }
                    else
                    {
                        flag = false;
                    }
                }
                catch (Exception exception)
                {
                    this.ilog_0.Error(exception.Message);
                }
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
                FileStream stream = new FileStream(path, FileMode.Open);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                byte[] buffer8 = AES_Crypt.Decrypt(buffer, buffer5, buffer6, null);
                if (buffer8 == null)
                {
                    return false;
                }
                document.Load(new XmlTextReader(new MemoryStream(buffer8)));
                XmlNode node = document.SelectSingleNode("/PrintConfig");
                new PrintDocument();
                XmlNodeList childNodes = node.ChildNodes;
                if ((childNodes != null) && (childNodes.Count != 0))
                {
                    if (!this.method_3(childNodes, str3) && flag)
                    {
                        this.method_3(childNodes, str4);
                    }
                    if ((this.DefaultPrinterArgs.PrinterName == null) || (this.DefaultPrinterArgs.PrinterName == ""))
                    {
                        this.DefaultPrinterArgs.PrinterName = this.printDocument_0.PrinterSettings.PrinterName;
                        this.DefaultPrinterArgs.System = "0";
                        this.DefaultPrinterArgs.PageLenght = 0;
                        this.DefaultPrinterArgs.Name = "user";
                        this.DefaultPrinterArgs.Left = 0f;
                        this.DefaultPrinterArgs.Top = 0f;
                        this.DefaultPrinterArgs.IsQuanDa = true;
                    }
                    return false;
                }
                return this.method_3(childNodes, str4);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool method_3(XmlNodeList xmlNodeList_0, string string_1)
        {
            if ((xmlNodeList_0 == null) || (xmlNodeList_0.Count == 0))
            {
                this.DefaultPrinterArgs.PrinterName = this.printDocument_0.PrinterSettings.PrinterName;
                this.DefaultPrinterArgs.System = "0";
                this.DefaultPrinterArgs.PageLenght = 0;
                this.DefaultPrinterArgs.Name = "user";
                this.DefaultPrinterArgs.Left = 0f;
                this.DefaultPrinterArgs.Top = 0f;
                this.DefaultPrinterArgs.IsQuanDa = true;
                return false;
            }
            IEnumerator enumerator = xmlNodeList_0.GetEnumerator();
            {
                XmlElement element;
                while (enumerator.MoveNext())
                {
                    XmlNode current = (XmlNode) enumerator.Current;
                    element = current as XmlElement;
                    if (element != null)
                    {
                        XmlAttribute attribute = element.Attributes["PrinterName"];
                        if (attribute.InnerText == string_1)
                        {
                            goto Label_005F;
                        }
                    }
                }
                goto Label_0100;
            Label_005F:
                this.DefaultPrinterArgs.PrinterName = element.GetAttribute("PrinterName");
                this.DefaultPrinterArgs.System = element.GetAttribute("system");
                this.DefaultPrinterArgs.Top = Common.ObjectToFloat(element.GetAttribute("top")) / 10f;
                this.DefaultPrinterArgs.Left = Common.ObjectToFloat(element.GetAttribute("left")) / 10f;
                this.DefaultPrinterArgs.PageLenght = Common.ObjectToInt(element.GetAttribute("pageLenght"));
                return true;
            }
        Label_0100:
            return false;
        }

        public void SaveUserPrinterEdge(PrinterEventArgs printerEventArgs_0)
        {
            string path = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), @"Config\Print\PrinterManager.xml");
            XmlDocument document = new XmlDocument();
            try
            {
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
                FileStream stream = new FileStream(path, FileMode.Open);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                byte[] buffer8 = AES_Crypt.Decrypt(buffer, buffer5, buffer6, null);
                if (buffer8 != null)
                {
                    document.Load(new XmlTextReader(new MemoryStream(buffer8)));
                    XmlNode node = document.SelectSingleNode("/PrintConfig");
                    new PrintDocument();
                    if (this.Name == null)
                    {
                        this.Name = "user";
                    }
                    bool flag = (((this.object_0 != null) && (this.object_0.Length >= 4)) && (this.object_0[1].ToString() == "1100053620")) && (this.object_0[2].ToString() == "593803");
                    bool flag2 = false;
                    if ((node.ChildNodes != null) || (node.ChildNodes.Count > 0))
                    {
                        for (int i = 0; i < node.ChildNodes.Count; i++)
                        {
                            XmlNode oldChild = node.ChildNodes[i];
                            XmlElement element = oldChild as XmlElement;
                            if (element.GetAttribute("name") == this.Name)
                            {
                                node.RemoveChild(oldChild);
                                i--;
                            }
                            else if (element.GetAttribute("name") == "_QD")
                            {
                                node.RemoveChild(oldChild);
                                i--;
                                flag2 = true;
                            }
                            else if ((element.GetAttribute("name") == "c_QD") && flag)
                            {
                                node.RemoveChild(oldChild);
                                i--;
                            }
                        }
                    }
                    if ((flag && (this.Name == "s_QD")) || flag2)
                    {
                        XmlElement newChild = document.CreateElement("Printer");
                        newChild.SetAttribute("name", "c_QD");
                        newChild.SetAttribute("left", printerEventArgs_0.Left.ToString());
                        newChild.SetAttribute("top", printerEventArgs_0.Top.ToString());
                        newChild.SetAttribute("system", "1");
                        newChild.SetAttribute("pagelenght", printerEventArgs_0.PageLenght.ToString());
                        newChild.SetAttribute("PrinterName", printerEventArgs_0.PrinterName);
                        newChild.SetAttribute("IsQuanDa", printerEventArgs_0.IsQuanDa.ToString());
                        node.AppendChild(newChild);
                        XmlElement element3 = document.CreateElement("Printer");
                        element3.SetAttribute("name", "s_QD");
                        element3.SetAttribute("left", printerEventArgs_0.Left.ToString());
                        element3.SetAttribute("top", printerEventArgs_0.Top.ToString());
                        element3.SetAttribute("system", "1");
                        element3.SetAttribute("pagelenght", printerEventArgs_0.PageLenght.ToString());
                        element3.SetAttribute("PrinterName", printerEventArgs_0.PrinterName);
                        element3.SetAttribute("IsQuanDa", printerEventArgs_0.IsQuanDa.ToString());
                        node.AppendChild(element3);
                    }
                    else
                    {
                        XmlElement element4 = document.CreateElement("Printer");
                        element4.SetAttribute("name", this.Name);
                        element4.SetAttribute("left", printerEventArgs_0.Left.ToString());
                        element4.SetAttribute("top", printerEventArgs_0.Top.ToString());
                        element4.SetAttribute("system", "1");
                        element4.SetAttribute("pagelenght", printerEventArgs_0.PageLenght.ToString());
                        element4.SetAttribute("PrinterName", printerEventArgs_0.PrinterName);
                        element4.SetAttribute("IsQuanDa", printerEventArgs_0.IsQuanDa.ToString());
                        node.AppendChild(element4);
                    }
                    document.Save(path);
                }
            }
            catch (Exception exception)
            {
                XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "UTF-8", "");
                document.AppendChild(declaration);
                XmlElement element5 = document.CreateElement("PrintConfig");
                element5.SetAttribute("Name", "user");
                document.AppendChild(element5);
                document.Save(path);
                this.UserPrinterArgs.PrinterName = this.printDocument_0.PrinterSettings.PrinterName;
                this.UserPrinterArgs.System = "1";
                this.UserPrinterArgs.PageLenght = 0;
                this.UserPrinterArgs.Name = "user";
                this.UserPrinterArgs.IsQuanDa = true;
                this.ilog_0.Error(exception.ToString());
            }
            finally
            {
                string str3 = "ikAJxQPU3bNUWK0fMgeHxMFk5wjhSQPYnARPgkVEKVU4yjA7KoD3eo7c6tLP745I";
                FileInfo info = new FileInfo(path);
                Common.Encrypt(new FileInfo[] { info }, str3);
            }
        }

        private string Name
        {
            [CompilerGenerated]
            get
            {
                return this.string_0;
            }
            [CompilerGenerated]
            set
            {
                this.string_0 = value;
            }
        }
    }
}

