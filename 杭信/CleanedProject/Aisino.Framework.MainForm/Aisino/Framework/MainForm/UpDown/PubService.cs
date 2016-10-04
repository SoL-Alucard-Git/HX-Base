namespace Aisino.Framework.MainForm.UpDown
{
    using Aisino.Framework.Plugin.Core.Command;
    using ns4;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    public class PubService : AbstractCommand
    {
        private Class100 class100_0;

        public PubService()
        {
            
            this.class100_0 = new Class100();
            if ((Class97.dataTable_0 == null) || (Class97.dataTable_0.Columns.Count < 10))
            {
                this.class100_0.method_0();
            }
            if (Class87.list_1 == null)
            {
                Class87.list_1 = new List<Dictionary<string, object>>();
            }
            if (Class87.list_0 == null)
            {
                Class87.list_0 = new List<Dictionary<string, object>>();
            }
            new ServerUP().TestIsServerUp();
        }

        public object[] DownloadFPBySingle(object[] object_2)
        {
            if ((object_2 != null) && (object_2.Length >= 1))
            {
                try
                {
                    Class87.bool_2 = true;
                    Class87.bool_1 = false;
                    if (Class87.bool_4)
                    {
                        new ServerUP().DonwloadInInterface(object_2[0].ToString());
                    }
                    else
                    {
                        new Class105().method_1(object_2[0].ToString());
                    }
                }
                catch (Exception exception)
                {
                    Class87.string_2 = Class87.string_2 + exception.ToString();
                }
                return new object[] { Class87.bool_1, Class87.string_3, Class87.string_2 };
            }
            return new object[] { false, "-0001", "没有传递需要查询的发票信息！" };
        }

        public object[] DownloadFPInPatch(object[] object_2)
        {
            Class84 class2 = new Class84();
            Class101.smethod_0("发票下载-批量接口：进入");
            if ((object_2 != null) && (object_2.Length >= 1))
            {
                Class101.smethod_0("发票下载-批量接口：传入受理序列号：" + object_2[0].ToString());
                try
                {
                    Class87.bool_2 = true;
                    Class87.bool_1 = false;
                    if (Class87.bool_4)
                    {
                        new ServerUP().DonwloadInInterface(object_2[0].ToString());
                    }
                    else
                    {
                        new Class105().method_1(object_2[0].ToString());
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_0("发票下载-批量接口异常：接口" + exception.ToString());
                }
                if (Class87.string_3.Equals("0000"))
                {
                    Class101.smethod_0("发票下载-批量接口：下载返回结果：" + Class87.xmlDocument_1.InnerXml);
                    return new object[] { Class87.xmlDocument_1 };
                }
                XmlNode oldChild = Class87.xmlDocument_1.SelectSingleNode("//FPXT/OUTPUT/DATA/FP_SUCC/FPXX");
                if (oldChild != null)
                {
                    string[] strArray = oldChild.InnerText.Split(new char[] { ';' });
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        string[] strArray2 = strArray[i].Split(new char[] { ',' });
                        if (strArray2.Length >= 2)
                        {
                            XmlNode node2 = Class87.xmlDocument_1.SelectSingleNode("//FPXT/OUTPUT/DATA/FP_ERR");
                            XmlElement newChild = Class87.xmlDocument_1.CreateElement("FP");
                            node2.AppendChild(newChild);
                            XmlElement element2 = Class87.xmlDocument_1.CreateElement("FPDM");
                            element2.InnerText = strArray2[0];
                            newChild.AppendChild(element2);
                            XmlElement element3 = Class87.xmlDocument_1.CreateElement("FPHM");
                            element3.InnerText = strArray2[1];
                            newChild.AppendChild(element3);
                            XmlElement element4 = Class87.xmlDocument_1.CreateElement("CODE");
                            element4.InnerText = Class87.string_3;
                            newChild.AppendChild(element4);
                            XmlElement element5 = Class87.xmlDocument_1.CreateElement("MESS");
                            element5.InnerText = Class87.string_2;
                            newChild.AppendChild(element5);
                        }
                    }
                    oldChild.ParentNode.RemoveChild(oldChild);
                    return new object[] { Class87.xmlDocument_1 };
                }
                return new object[] { Class87.xmlDocument_1 };
            }
            Class101.smethod_0("发票下载-批量接口：没有传入受理序列号信息");
            return new object[] { class2.method_26(0) };
        }

        public object[] UpdateEnterpriseinfo(object[] object_2)
        {
            Class87.bool_3 = true;
            if (Class87.dictionary_0 == null)
            {
                Class87.dictionary_0 = new Dictionary<string, object>();
            }
            if (!Class87.dictionary_0.Keys.Contains<string>("UpdateBZ"))
            {
                Class87.dictionary_0.Add("UpdateBZ", "");
            }
            if (!Class87.dictionary_0.Keys.Contains<string>("Message"))
            {
                Class87.dictionary_0.Add("Message", "");
            }
            try
            {
                new Class91().method_0();
            }
            catch (Exception exception)
            {
                Class87.dictionary_0["UpdateBZ"] = "0";
                Class87.dictionary_0["Message"] = exception.Message;
            }
            return new object[] { Class87.dictionary_0 };
        }

        public object[] UploadFPBySingle(object[] object_2)
        {
            if ((object_2 != null) && (object_2.Length >= 1))
            {
                try
                {
                    Class87.bool_3 = true;
                    Class101.smethod_0(string.Concat(new object[] { "传递的参数个数：", object_2.Length, "    ", object_2[0].ToString(), "    ", object_2[1].ToString(), "      ", object_2[2].ToString() }));
                    Class87.bool_2 = true;
                    if (Class87.bool_4)
                    {
                        new ServerUP().UPLoadSingleInInterface(object_2[0].ToString(), object_2[1].ToString(), object_2[2].ToString());
                    }
                    else
                    {
                        Class85 class2 = new Class85();
                        Class87.string_1 = string.Empty;
                        Class87.bool_0 = false;
                        Class101.smethod_0("接口开始上传");
                        class2.method_2(object_2[0].ToString(), object_2[1].ToString(), object_2[2].ToString());
                    }
                    Class101.smethod_0("接口上传结束");
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("接口上传异常：" + exception.ToString());
                    Class87.string_1 = exception.ToString();
                }
                Class101.smethod_0(string.Concat(new object[] { "接口调用结束：", Class87.bool_0, "   ", Class87.string_0, "   ", Class87.string_1 }));
                return new object[] { Class87.bool_0, Class87.string_0, Class87.string_1 };
            }
            return new object[] { false, "-0001", "没有传递需要上传的发票信息！" };
        }

        public object[] UploadFPInPatch(object[] object_2)
        {
            Class84 class2 = new Class84();
            Class101.smethod_0("发票上传-批量接口：进入");
            if ((object_2 != null) && (object_2.Length >= 1))
            {
                Class101.smethod_0("发票上传-批量接口：传入发票为：" + object_2[0].ToString());
                try
                {
                    Class87.bool_3 = true;
                    if (Class87.bool_4)
                    {
                        new ServerUP().UPloadInPatch(object_2[0].ToString());
                    }
                    else
                    {
                        Class87.bool_2 = true;
                        Class85 class3 = new Class85();
                        Class87.string_1 = string.Empty;
                        Class87.bool_0 = false;
                        class3.method_3(object_2[0].ToString());
                    }
                    Class101.smethod_0("发票上传-批量接口：离开");
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("发票上传-批量接口：接口异常" + exception.ToString());
                    Class87.string_1 = exception.ToString();
                }
                return new object[] { Class87.xmlDocument_0 };
            }
            Class101.smethod_0("发票上传-批量接口：没有传入发票信息");
            return new object[] { class2.method_24() };
        }
    }
}

