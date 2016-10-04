namespace ns12
{
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Plugin;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Resources;
    using System.Xml;

    internal sealed class Class132
    {
        private System.Reflection.Assembly assembly_0;
        private static readonly ILog ilog_0;
        private string string_0;
        private string string_1;
        private string string_2;

        static Class132()
        {
            
            ilog_0 = LogUtil.GetLogger<Class132>();
        }

        internal Class132(string string_3, string string_4, string string_5)
        {
            
            this.string_1 = string_3;
            this.string_0 = string_5;
            this.string_2 = string_4;
        }

        internal string method_0()
        {
            return this.string_2;
        }

        internal void method_1(string string_3)
        {
            this.string_2 = string_3;
        }

        internal System.Reflection.Assembly method_2()
        {
            if (this.assembly_0 == null)
            {
                ilog_0.Info("加载程序集：" + this.string_1);
                try
                {
                    FileStream stream = new FileStream(Path.Combine(this.string_0, this.string_1), FileMode.Open);
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    stream.Close();
                    
                    //逻辑修改:我们自己的文件是不需要解密的
                    //byte[] sourceArray = Convert.FromBase64String("L3yC7zg8fQWhipDSFB284EahvoXH9kNV6TE843pajbE7Tyo53TJ95N4ahc1nunDe");
                    //byte[] destinationArray = new byte[0x20];
                    //Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
                    //byte[] buffer3 = new byte[0x10];
                    //Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
                    //byte[] buffer4 = AES_Crypt.Decrypt(Convert.FromBase64String("X7xxyMJDoje5XAwsxAOOIAjpm9iH+86h8HJE7kFSLV4K2rN/kb93R5TgadKYp4kTcuz3eg+TV8gMxLkltAAOow=="), destinationArray, buffer3, null);
                    //byte[] buffer5 = new byte[0x20];
                    //Array.Copy(buffer4, 0, buffer5, 0, 0x20);
                    //byte[] buffer6 = new byte[0x10];
                    //Array.Copy(buffer4, 0x20, buffer6, 0, 0x10);
                    //byte[] rawAssembly = AES_Crypt.Decrypt(buffer, buffer5, buffer6, null);
                    //this.assembly_0 = System.Reflection.Assembly.Load(rawAssembly);

                    this.assembly_0 = System.Reflection.Assembly.Load(buffer);
                    if ((this.string_2 != null) && (this.string_2.Length > 0))
                    {
                        ResourceUtil.smethod_0(new ResourceManager(this.string_2, this.assembly_0));
                    }
                }
                catch (FileNotFoundException)
                {
                    ilog_0.ErrorFormat("程序集 '{0}' 不存在", this.string_1);
                }
                catch (FileLoadException)
                {
                    ilog_0.ErrorFormat("程序集 '{0}' 未能正常加载", this.string_1);
                }
            }
            return this.assembly_0;
        }

        internal object method_3(string string_3)
        {
            System.Reflection.Assembly assembly = this.method_2();
            if (assembly == null)
            {
                return null;
            }
            return assembly.CreateInstance(string_3);
        }

        internal static void smethod_0(XmlReader xmlReader_0, PlugIn plugIn_0, string string_3)
        {
            while (xmlReader_0.Read())
            {
                XmlNodeType nodeType = xmlReader_0.NodeType;
                if (nodeType != XmlNodeType.Element)
                {
                    if ((nodeType == XmlNodeType.EndElement) && (xmlReader_0.LocalName == "Runtime"))
                    {
                        return;
                    }
                }
                else
                {
                    string localName = xmlReader_0.LocalName;
                    if (localName == null)
                    {
                        goto Label_00CB;
                    }
                    if (!(localName == "Import"))
                    {
                        if (!(localName == "SqlLib"))
                        {
                            goto Label_00CB;
                        }
                        string attribute = xmlReader_0.GetAttribute("file");
                        plugIn_0.SqlLib.Add(Path.Combine(string_3, attribute));
                        plugIn_0.method_7(attribute, string_3);
                        continue;
                    }
                    if (xmlReader_0.AttributeCount < 1)
                    {
                        throw new Exception2("导入节点至少有一个属性");
                    }
                    Class132 item = new Class132(xmlReader_0.GetAttribute("assembly"), xmlReader_0.GetAttribute("resource"), string_3);
                    plugIn_0.method_2().Add(item);
                }
            }
            return;
        Label_00CB:
            throw new Exception2("运行时节点不支持的子节点：" + xmlReader_0.LocalName);
        }

        internal string Assembly
        {
            get
            {
                return this.string_1;
            }
        }
    }
}

