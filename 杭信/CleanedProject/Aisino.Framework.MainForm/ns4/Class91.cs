namespace ns4
{
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.MessageDlg;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Xml;

    internal class Class91
    {
        private Class84 class84_0;
        private string string_0;
        private string string_1;

        public Class91()
        {
            
            this.class84_0 = new Class84();
            this.string_0 = "";
            this.string_1 = "";
        }

        public void method_0()
        {
            try
            {
                string location = Assembly.GetExecutingAssembly().Location;
                this.string_1 = location.Substring(0, location.LastIndexOf(@"\")) + @"\" + AttributeName.QYXXFileName;
                XmlDocument document = new XmlDocument();
                if (!Class87.bool_3)
                {
                    if (File.Exists(this.string_1))
                    {
                        Class101.smethod_0("更新企业参数同步数据：数据文件已存在。");
                        document.Load(this.string_1);
                        Class101.smethod_0("更新企业参数同步数据：！    " + document.InnerXml);
                        if (string.IsNullOrEmpty(document.InnerXml))
                        {
                            if (Class87.bool_3)
                            {
                                Class87.dictionary_0["UpdateBZ"] = "1";
                                Class87.dictionary_0["Message"] = "服务器端异常，返回信息为空";
                            }
                            return;
                        }
                        this.class84_0.method_18(document);
                        if (File.Exists(this.string_1))
                        {
                            File.Delete(this.string_1);
                        }
                        MessageHelper.MsgWait();
                        Class86.smethod_9();
                    }
                    document = this.class84_0.method_16();
                    this.string_0 = document.InnerXml;
                    ThreadStart start = new ThreadStart(this.method_1);
                    new Thread(start).Start();
                }
                else
                {
                    document = this.class84_0.method_16();
                    if (document != null)
                    {
                        Class103.smethod_0(document, "OptionToServer");
                        string str2 = "";
                        Class101.smethod_0("开始企业信息上传，【企业参数同步】上传报文：" + document.InnerXml);
                        if (HttpsSender.SendMsg("0001", document.InnerXml.ToString(), out str2) != 0)
                        {
                            Class101.smethod_1("企业信息上传失败！    " + str2.ToString());
                            Class95.bool_0 = false;
                            Class95.string_0 = "企业参数更新失败：" + str2;
                            Class95.bool_1 = false;
                            Class95.string_1 = Class95.string_2 + "同步失败：" + str2;
                            if (Class87.bool_3)
                            {
                                Class87.dictionary_0["UpdateBZ"] = "0";
                                Class87.dictionary_0["Message"] = str2;
                            }
                        }
                        else
                        {
                            Class101.smethod_0("企业参数同步，局端返回数据：！    " + str2.ToString());
                            if (string.IsNullOrEmpty(str2))
                            {
                                if (Class87.bool_3)
                                {
                                    Class87.dictionary_0["UpdateBZ"] = "1";
                                    Class87.dictionary_0["Message"] = "服务器端异常，返回信息为空";
                                }
                            }
                            else
                            {
                                XmlDocument document2 = new XmlDocument();
                                document2.LoadXml(str2);
                                Class103.smethod_0(document2, "OptionFromServer");
                                this.class84_0.method_18(document2);
                                MessageHelper.MsgWait();
                                Class86.smethod_9();
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_1("更新企业参数信息失败！" + exception.ToString());
            }
        }

        private void method_1()
        {
            if (File.Exists(this.string_1))
            {
                File.Delete(this.string_1);
            }
            if ((this.string_0 != "") && (this.string_1 != ""))
            {
                try
                {
                    string str = "";
                    Class101.smethod_0("开始企业信息上传，【企业参数同步】上传报文：" + this.string_0);
                    if (HttpsSender.SendMsg("0001", this.string_0, out str) != 0)
                    {
                        Class101.smethod_1("企业信息上传失败！    " + str.ToString());
                        Class95.bool_0 = false;
                        Class95.string_0 = "企业参数更新失败：" + str;
                        Class95.bool_1 = false;
                        Class95.string_1 = Class95.string_2 + "同步失败：" + str;
                        if (Class87.bool_3)
                        {
                            Class87.dictionary_0["UpdateBZ"] = "0";
                            Class87.dictionary_0["Message"] = str;
                        }
                    }
                    else
                    {
                        XmlDocument document = new XmlDocument();
                        document.LoadXml(str);
                        document.Save(this.string_1);
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("DownloadQYSQXX异常：" + exception.ToString());
                }
            }
            else
            {
                Class101.smethod_0("开始企业参数同步：contentToServer" + this.string_0 + "   filename:" + this.string_1);
            }
        }
    }
}

