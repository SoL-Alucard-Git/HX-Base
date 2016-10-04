namespace ns4
{
    using Aisino.Framework.Plugin.Core.Https;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Xml;

    internal class Class105
    {
        private bool bool_0;
        private Class100 class100_0;
        private Class84 class84_0;
        private DataTable dataTable_0;
        private DataTable dataTable_1;
        private int int_0;
        private int int_1;
        private List<XmlDocument> list_0;

        public Class105()
        {
            
            this.dataTable_0 = new DataTable();
            this.list_0 = new List<XmlDocument>();
            this.class84_0 = new Class84();
            this.class100_0 = new Class100();
            this.dataTable_1 = new DataTable();
            this.bool_0 = true;
            if (!this.dataTable_1.Columns.Contains("FPSLH"))
            {
                this.dataTable_1.Columns.Add("FPSLH", typeof(string));
            }
        }

        public void method_0()
        {
            Class101.smethod_0("(下载线程)开始查询下载结果");
            this.method_2();
            Class101.smethod_0("(下载线程)结束查询下载结果");
        }

        public void method_1(string string_0)
        {
            try
            {
                Class84 class2 = new Class84();
                class2.method_40(string_0);
                Class87.list_1.Clear();
                DataTable table = new DataTable();
                table.Columns.Add("FPSLH", typeof(string));
                DataRow row = table.NewRow();
                row["FPSLH"] = string_0;
                table.Rows.Add(row);
                table.AcceptChanges();
                this.list_0.Clear();
                this.list_0 = this.class84_0.method_5(table);
                XmlDocument document = new XmlDocument();
                string str = "";
                int num4 = (this.list_0.Count > 5) ? 5 : this.list_0.Count;
                for (int i = 0; i < num4; i++)
                {
                    Class103.smethod_0(this.list_0[i], "DownMethodToServer" + i);
                    string str2 = this.class84_0.method_28(this.list_0[i]);
                    Class101.smethod_0("(发票下载)通过受理序列号得到的票种为：" + str2);
                    if (!str2.Equals("q") && !str2.Equals("JSFP"))
                    {
                        if (HttpsSender.SendMsg("0004", this.list_0[i].InnerXml.ToString(), out str) == 0)
                        {
                            goto Label_023F;
                        }
                        Class87.bool_1 = false;
                        int index = str.IndexOf("[");
                        int num = str.IndexOf("]");
                        if ((index > -1) && (num > index))
                        {
                            Class87.string_3 = str.Substring(index + 1, num - 1);
                        }
                        if (num > 0)
                        {
                            Class87.string_2 = str.Substring(num + 1);
                        }
                        else
                        {
                            Class87.string_2 = str;
                        }
                        Class87.xmlDocument_1 = class2.method_26(1);
                        class2.method_22(this.list_0[i], str, 3);
                        continue;
                    }
                    Class101.smethod_0("(发票下载)开始卷票上传，原文：" + this.list_0[i].InnerXml);
                    if (HttpsSender.SendMsg("0030", this.list_0[i].InnerXml.ToString(), out str) != 0)
                    {
                        class2.method_22(this.list_0[i], str, 3);
                        Class101.smethod_1("（下载线程）发票下载失败！            " + str);
                        continue;
                    }
                Label_023F:
                    Class87.bool_1 = true;
                    document.LoadXml(str);
                    if (Class87.bool_2)
                    {
                        XmlDocument document2 = new XmlDocument();
                        document2.LoadXml(document.InnerXml);
                        Class101.smethod_0("发票下载-批量接口：局端返回报文处理前：" + document2.InnerXml);
                        Class87.xmlDocument_1 = this.class84_0.method_25(document2);
                        Class101.smethod_0("发票下载-批量接口：局端返回报文处理后：" + document2.InnerXml);
                    }
                    Class103.smethod_0(document, "DownMethodFromServer" + i);
                    Class101.smethod_0("开始调用AnalyzeDownMethodXmlDoc-Single");
                    this.class84_0.method_9(document, this.list_0[i]);
                }
                Class88.smethod_1();
                Class88.smethod_4();
            }
            catch (Exception exception)
            {
                Class101.smethod_1("DownLoadBySingle异常:" + exception.ToString());
            }
        }

        private void method_2()
        {
            try
            {
                Class101.smethod_0("进入beginDownloadTread");
                Class87.list_1.Clear();
                if (this.bool_0)
                {
                    this.int_0 = 0;
                    this.int_1 = 0;
                    this.dataTable_0 = this.class100_0.method_4();
                }
                if (((this.dataTable_0 != null) && (this.dataTable_0.Rows.Count >= 1)) && ((this.int_0 < this.dataTable_0.Rows.Count) && (this.int_1 < this.dataTable_0.Rows.Count)))
                {
                    this.int_1 = this.int_0 + 10;
                    if (this.int_1 > this.dataTable_0.Rows.Count)
                    {
                        this.int_1 = this.dataTable_0.Rows.Count;
                    }
                    this.bool_0 = false;
                    this.dataTable_1.Clear();
                    for (int i = this.int_0; i < this.int_1; i++)
                    {
                        this.dataTable_1.Rows.Add(this.dataTable_0.Rows[i].ItemArray);
                    }
                    this.list_0.Clear();
                    this.list_0 = this.class84_0.method_5(this.dataTable_1);
                    XmlDocument document = new XmlDocument();
                    string str = "";
                    Class101.smethod_0("Begin For Loop : " + this.list_0.Count.ToString());
                    for (int j = 0; j < this.list_0.Count; j++)
                    {
                        Class101.smethod_1("（下载线程）开始下载发票信息！");
                        Class103.smethod_0(this.list_0[j], "DownMethodToServer" + j);
                        string str2 = this.class84_0.method_28(this.list_0[j]);
                        Class101.smethod_0("(发票下载)通过受理序列号得到的票种为：" + str2);
                        if (!str2.Equals("q") && !str2.Equals("JSFP"))
                        {
                            if (HttpsSender.SendMsg("0004", this.list_0[j].InnerXml.ToString(), out str) == 0)
                            {
                                goto Label_02AC;
                            }
                            this.class84_0.method_22(this.list_0[j], str, 3);
                            Class101.smethod_1("（下载线程）发票下载失败！            " + str);
                            continue;
                        }
                        Class101.smethod_0("(发票下载)开始卷票上传，原文：" + this.list_0[j].InnerXml);
                        if (HttpsSender.SendMsg("0030", this.list_0[j].InnerXml.ToString(), out str) != 0)
                        {
                            this.class84_0.method_22(this.list_0[j], str, 3);
                            Class101.smethod_1("（下载线程）发票下载失败！            " + str);
                            continue;
                        }
                    Label_02AC:
                        Class101.smethod_0("（下载线程）开始下载发票信息结束，准备解析服务器返回数据:" + str);
                        document.LoadXml(str);
                        Class103.smethod_0(document, "DownMethodFromServer" + j);
                        Class101.smethod_0("开始调用AnalyzeDownMethodXmlDoc,发送给局端数据：" + this.list_0[j].InnerXml);
                        this.class84_0.method_9(document, this.list_0[j]);
                    }
                    Class88.smethod_1();
                    Class88.smethod_4();
                    this.int_0 += 10;
                }
                else
                {
                    this.bool_0 = true;
                    this.dataTable_0.Clear();
                    this.dataTable_1.Clear();
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_1("（下载线程）发票下载失败！" + exception.ToString());
            }
        }
    }
}

