namespace ns4
{
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Xml;

    internal class Class100
    {
        private Class81 class81_0;

        public Class100()
        {
            
            this.class81_0 = new Class81();
        }

        public bool method_0()
        {
            new Class96();
            try
            {
                if (!Class97.dataTable_0.Columns.Contains("FPNO"))
                {
                    Class97.dataTable_0.Columns.Add("FPNO", typeof(string));
                }
                if (!Class97.dataTable_0.Columns.Contains("FPDM"))
                {
                    Class97.dataTable_0.Columns.Add("FPDM", typeof(string));
                }
                if (!Class97.dataTable_0.Columns.Contains("FPSLH"))
                {
                    Class97.dataTable_0.Columns.Add("FPSLH", typeof(string));
                }
                if (!Class97.dataTable_0.Columns.Contains("FPSQHRecieveTime"))
                {
                    Class97.dataTable_0.Columns.Add("FPSQHRecieveTime", typeof(string));
                }
                if (!Class97.dataTable_0.Columns.Contains("FPStatus"))
                {
                    Class97.dataTable_0.Columns.Add("FPStatus", typeof(string));
                }
                if (!Class97.dataTable_0.Columns.Contains("FpNSRSBH"))
                {
                    Class97.dataTable_0.Columns.Add("FpNSRSBH", typeof(string));
                }
                if (!Class97.dataTable_0.Columns.Contains("FpUploadTime"))
                {
                    Class97.dataTable_0.Columns.Add("FpUploadTime", typeof(string));
                }
                if (!Class97.dataTable_0.Columns.Contains("FpKPJH"))
                {
                    Class97.dataTable_0.Columns.Add("FpKPJH", typeof(string));
                }
                if (!Class97.dataTable_0.Columns.Contains("FpSBBH"))
                {
                    Class97.dataTable_0.Columns.Add("FpSBBH", typeof(string));
                }
                if (!Class97.dataTable_0.Columns.Contains("IsFpUpFailed"))
                {
                    Class97.dataTable_0.Columns.Add("IsFpUpFailed", typeof(bool));
                }
                if (!Class97.dataTable_0.Columns.Contains("ISFpDownFailed"))
                {
                    Class97.dataTable_0.Columns.Add("ISFpDownFailed", typeof(bool));
                }
                if (!Class97.dataTable_0.Columns.Contains("Fplx"))
                {
                    Class97.dataTable_0.Columns.Add("Fplx", typeof(string));
                }
                if (!Class97.dataTable_0.Columns.Contains("isDBUpdated"))
                {
                    Class97.dataTable_0.Columns.Add("isDBUpdated", typeof(bool));
                }
                if (!Class97.dataTable_0.Columns.Contains("ZFBZ"))
                {
                    Class97.dataTable_0.Columns.Add("ZFBZ", typeof(bool));
                }
                if (!Class97.dataTable_0.Columns.Contains("DZSYH"))
                {
                    Class97.dataTable_0.Columns.Add("DZSYH", typeof(string));
                }
                return true;
            }
            catch (Exception exception)
            {
                Class101.smethod_1("init UpDownLoad  FpTable Error!" + exception.ToString());
                return false;
            }
        }

        public bool method_1(Class96 class96_0, Enum10 enum10_0)
        {
            if (class96_0 != null)
            {
                Class101.smethod_0("(发票上传)存临时表开始：fpdm：" + class96_0.FPDM + "  fphm:" + class96_0.FPNO + "   fpzl:" + class96_0.Fplx + "  method:" + enum10_0.ToString());
                try
                {
                    lock (Class97.dataTable_0)
                    {
                        if (enum10_0 == Enum10.Insert)
                        {
                            if (this.method_3(Class97.dataTable_0, class96_0.FPNO, class96_0.FPDM) != null)
                            {
                                return this.method_1(class96_0, Enum10.Update);
                            }
                            DataRow row = Class97.dataTable_0.NewRow();
                            row["FPNO"] = class96_0.FPNO;
                            row["FPDM"] = class96_0.FPDM;
                            row["FPSLH"] = class96_0.FPSLH;
                            row["FPStatus"] = class96_0.FPStatus;
                            row["FPSQHRecieveTime"] = class96_0.FPSQHRecieveTime;
                            row["Fplx"] = class96_0.Fplx;
                            row["FpUploadTime"] = class96_0.FpUploadTime;
                            row["FpNSRSBH"] = class96_0.FpNSRSBH;
                            row["FpSBBH"] = class96_0.FpSBBH;
                            row["FpKPJH"] = class96_0.FpKPJH;
                            row["IsFpUpFailed"] = class96_0.IsFpUpFailed;
                            row["ISFpDownFailed"] = class96_0.ISFpDownFailed;
                            row["isDBUpdated"] = class96_0.Boolean_0;
                            row["ZFBZ"] = class96_0.ZFBZ;
                            row["DZSYH"] = class96_0.DZSYH;
                            Class97.dataTable_0.Rows.Add(row);
                            Class97.dataTable_0.AcceptChanges();
                            return true;
                        }
                        if (enum10_0 == Enum10.Update)
                        {
                            DataRow[] rowArray = Class97.dataTable_0.Select("FPNO='" + class96_0.FPNO + "' AND FPDM='" + class96_0.FPDM + "' AND FPLX='" + class96_0.Fplx + "'");
                            if ((rowArray != null) && (rowArray.Length >= 1))
                            {
                                rowArray[0]["FPSLH"] = class96_0.FPSLH;
                                rowArray[0]["FPDM"] = class96_0.FPDM;
                                rowArray[0]["FPStatus"] = class96_0.FPStatus;
                                rowArray[0]["FPSQHRecieveTime"] = class96_0.FPSQHRecieveTime;
                                rowArray[0]["Fplx"] = class96_0.Fplx;
                                rowArray[0]["FpUploadTime"] = class96_0.FpUploadTime;
                                rowArray[0]["FpNSRSBH"] = class96_0.FpNSRSBH;
                                rowArray[0]["FpSBBH"] = class96_0.FpSBBH;
                                rowArray[0]["FpKPJH"] = class96_0.FpKPJH;
                                rowArray[0]["IsFpUpFailed"] = class96_0.IsFpUpFailed;
                                rowArray[0]["ISFpDownFailed"] = class96_0.ISFpDownFailed;
                                rowArray[0]["isDBUpdated"] = class96_0.Boolean_0;
                                rowArray[0]["ZFBZ"] = class96_0.ZFBZ;
                                rowArray[0]["DZSYH"] = class96_0.DZSYH;
                                Class97.dataTable_0.AcceptChanges();
                                return true;
                            }
                            return false;
                        }
                        if (enum10_0 == Enum10.Delete)
                        {
                            DataRow[] rowArray2 = Class97.dataTable_0.Select("FPNO='" + class96_0.FPNO + "' AND FPDM='" + class96_0.FPDM + "' AND FPLX='" + class96_0.Fplx + "'");
                            if ((rowArray2 != null) && (rowArray2.Length >= 1))
                            {
                                Class97.dataTable_0.Rows.Remove(rowArray2[0]);
                                Class97.dataTable_0.AcceptChanges();
                                return true;
                            }
                            return false;
                        }
                    }
                    Class101.smethod_0("(发票上传)存临时表结束：fpdm：" + class96_0.FPDM + "  fphm:" + class96_0.FPNO + "   fpzl:" + class96_0.Fplx);
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("Update UpDownload FpTableInfo Error!" + exception.ToString());
                }
            }
            return false;
        }

        public int method_10(UpdateTransMethod updateTransMethod_0)
        {
            Class81 class2 = new Class81();
            switch (updateTransMethod_0)
            {
                case UpdateTransMethod.WBS:
                    return class2.method_0(0);

                case UpdateTransMethod.YBS:
                    return class2.method_0(1);

                case UpdateTransMethod.BSSB:
                    return class2.method_0(2);

                case UpdateTransMethod.BSZ:
                    return class2.method_0(3);
            }
            return 0;
        }

        public DataRow[] method_11(string string_0)
        {
            if (!string.IsNullOrEmpty(string_0))
            {
                try
                {
                    lock (Class97.dataTable_0)
                    {
                        return Class97.dataTable_0.Select("FPSLH='" + string_0 + "'");
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("(下载线程)根据受理序列号查询发票异常：" + exception.ToString());
                }
            }
            return null;
        }

        public bool method_2(ref List<Fpxx> list_0, ref List<Fpxx> list_1, ref List<Fpxx> list_2, ref List<Fpxx> list_3, ref List<Fpxx> list_4, string string_0, UpdateTransMethod updateTransMethod_0, string string_1, string string_2, string string_3, string string_4)
        {
            List<Fpxx> list = new GetFpInfoDal().GetFpInfo(string_0, Class87.int_1.ToString(), updateTransMethod_0, string_2, string_1, string_3, string_4);
            Class81 class2 = new Class81();
            if ((list != null) && (list.Count >= 1))
            {
                Class96 class3 = new Class96();
                TaxCard card = TaxCard.CreateInstance(CTaxCardType.const_7);
                try
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        Class101.smethod_0("发票类型：" + list[i].fplx.ToString());
                        if ((list[i].bszt.ToString() == "3") && !Class87.bool_3)
                        {
                            class3.FpKPJH = card.Machine.ToString();
                            class3.FPNO = list[i].fphm;
                            class3.FPDM = list[i].fpdm;
                            class3.FpNSRSBH = card.TaxCode;
                            class3.FpSBBH = card.GetInvControlNum();
                            class3.Fplx = list[i].fplx.ToString();
                            class3.ISFpDownFailed = false;
                            class3.IsFpUpFailed = false;
                            class3.Boolean_0 = true;
                            class3.FpUploadTime = this.class81_0.method_1(class3.FPNO, class3.FPDM, class3.Fplx).ToString();
                            class3.FPSQHRecieveTime = DateTime.Now.ToString();
                            class3.FPSLH = class2.method_3(class3.FPNO, class3.FPDM, class3.Fplx);
                            string str = this.class81_0.method_2(class3.FPNO, class3.FPDM, class3.Fplx);
                            class3.DZSYH = list[i].dzsyh;
                            Class101.smethod_0("(发票上传)上传报送中发票--fpdm：" + list[i].fpdm + "    fphm：" + list[i].fphm + "   dzsyh：" + list[i].dzsyh + "  zfbz:" + str + "  fpslh:" + class3.FPSLH);
                            if (str.Equals("1"))
                            {
                                class3.ZFBZ = true;
                            }
                            else if (str.Equals("0"))
                            {
                                class3.ZFBZ = false;
                            }
                            else
                            {
                                class3.ZFBZ = list[i].zfbz;
                            }
                            if ((class3.FPSLH != null) && !(class3.FPSLH == ""))
                            {
                                class3.FPStatus = list[i].bszt.ToString();
                            }
                            else
                            {
                                class3.FPStatus = "0";
                            }
                            this.method_1(class3, Enum10.Insert);
                        }
                        else if ((list[i].fplx != FPLX.PTFP) && (list[i].fplx != FPLX.ZYFP))
                        {
                            if (list[i].fplx == FPLX.HYFP)
                            {
                                list_1.Add(list[i]);
                            }
                            else if (list[i].fplx == FPLX.JDCFP)
                            {
                                list_2.Add(list[i]);
                            }
                            else if (list[i].fplx == FPLX.DZFP)
                            {
                                list_3.Add(list[i]);
                            }
                            else if (list[i].fplx == FPLX.JSFP)
                            {
                                list_4.Add(list[i]);
                            }
                        }
                        else
                        {
                            list_0.Add(list[i]);
                        }
                        Class101.smethod_0("发票上传：fpdm:" + list[i].fpdm + "  发票号码:" + list[i].fphm.ToString() + "  发票种类：" + list[i].fplx.ToString() + "  报送状态：" + list[i].bszt.ToString());
                    }
                    return true;
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("（上传线程）分发发票信息表到上传线程表出错！" + exception.ToString());
                }
                finally
                {
                    list.Clear();
                    list = null;
                }
                Class101.smethod_0("（上传线程）分发发票信息表到上传线程表出错！");
                return false;
            }
            return false;
        }

        public Class96 method_3(DataTable dataTable_0, string string_0, string string_1)
        {
            if ((dataTable_0 == null) || (dataTable_0.Rows.Count < 1))
            {
                return null;
            }
            Class96 class2 = new Class96();
            try
            {
                DataRow[] rowArray;
                lock (Class97.dataTable_0)
                {
                    rowArray = dataTable_0.Select("FPNO='" + string_0 + "' AND FPDM='" + string_1 + "'");
                }
                if ((rowArray == null) || (rowArray.Length < 1))
                {
                    return null;
                }
                class2.FpKPJH = rowArray[0]["FpKPJH"].ToString();
                class2.FPNO = rowArray[0]["FPNO"].ToString();
                class2.FPDM = rowArray[0]["FPDM"].ToString();
                class2.FpNSRSBH = rowArray[0]["FpNSRSBH"].ToString();
                class2.FpSBBH = rowArray[0]["FpSBBH"].ToString();
                class2.FPSLH = rowArray[0]["FPSLH"].ToString();
                class2.FPSQHRecieveTime = rowArray[0]["FPSQHRecieveTime"].ToString();
                class2.ZFBZ = Convert.ToBoolean(rowArray[0]["ZFBZ"]);
                Class101.smethod_0("GetOneFpRow--fpdm:" + rowArray[0]["FPDM"].ToString() + "   fphm:" + rowArray[0]["FPNO"].ToString() + "  fpzl:" + rowArray[0]["Fplx"].ToString());
                int result = -1;
                if (int.TryParse(rowArray[0]["Fplx"].ToString(), out result) && (result > -1))
                {
                    class2.Fplx = Enum.GetName(typeof(FPLX), result);
                }
                else if (rowArray[0]["Fplx"].ToString() == "c")
                {
                    class2.Fplx = FPLX.PTFP.ToString();
                }
                else if (rowArray[0]["Fplx"].ToString() == "s")
                {
                    class2.Fplx = FPLX.ZYFP.ToString();
                }
                else if (rowArray[0]["Fplx"].ToString() == "f")
                {
                    class2.Fplx = FPLX.HYFP.ToString();
                }
                else if (rowArray[0]["Fplx"].ToString() == "j")
                {
                    class2.Fplx = FPLX.JDCFP.ToString();
                }
                else if (rowArray[0]["Fplx"].ToString() == "p")
                {
                    class2.Fplx = FPLX.DZFP.ToString();
                }
                else if (rowArray[0]["Fplx"].ToString() == "q")
                {
                    class2.Fplx = FPLX.JSFP.ToString();
                }
                else
                {
                    class2.Fplx = rowArray[0]["Fplx"].ToString();
                }
                class2.FPStatus = rowArray[0]["FPStatus"].ToString();
                class2.FpUploadTime = rowArray[0]["FpUploadTime"].ToString();
                class2.IsFpUpFailed = Convert.ToBoolean(rowArray[0]["IsFpUpFailed"]);
                class2.ISFpDownFailed = Convert.ToBoolean(rowArray[0]["ISFpDownFailed"]);
                class2.Boolean_0 = Convert.ToBoolean(rowArray[0]["isDBUpdated"]);
                class2.DZSYH = Convert.ToString(rowArray[0]["DZSYH"]);
                return class2;
            }
            catch (Exception)
            {
            }
            return class2;
        }

        public DataTable method_4()
        {
            DataTable table = new DataTable();
            try
            {
                lock (Class97.dataTable_0)
                {
                    Class97.dataTable_0.DefaultView.RowFilter = "isDBUpdated = true AND FPStatus='3'";
                    table = Class97.dataTable_0.DefaultView.ToTable(true, new string[] { "FPSLH" });
                    Class97.dataTable_0.DefaultView.RowFilter = "";
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_1("获取需下载发票信息失败！" + exception.ToString());
            }
            return table;
        }

        public bool method_5(string string_0, string string_1)
        {
            DateTime time;
            DateTime time2;
            if (!DateTime.TryParse(string_0, out time))
            {
                return false;
            }
            if (!DateTime.TryParse(string_1, out time2))
            {
                return false;
            }
            if (time2 < time)
            {
                return false;
            }
            TimeSpan span = (TimeSpan) (time2 - time);
            return (span.Minutes >= Class87.int_4);
        }

        public bool method_6(XmlDocument xmlDocument_0, XmlDocument xmlDocument_1)
        {
            if ((xmlDocument_0 != null) && (xmlDocument_1 != null))
            {
                Class84 class2 = new Class84();
                string str = "";
                try
                {
                    string str2 = string.Empty;
                    string str3 = class2.method_10(xmlDocument_0, ref str, out str2);
                    if ((!string.IsNullOrEmpty(str3) && !(str3 != "0000")) && !string.IsNullOrEmpty(str))
                    {
                        class2.method_12(xmlDocument_1, str);
                        class2.method_19(xmlDocument_1, "已执行发票上传，发票报送状态已置为报送中。", 3);
                        Class87.string_0 = "0000";
                        Class87.string_1 = str;
                        return true;
                    }
                    string str4 = "已执行发票上传，服务器处理异常";
                    if (string.IsNullOrEmpty(str2))
                    {
                        str4 = str4 + "。";
                    }
                    else
                    {
                        str4 = str4 + "，服务器返回错误信息：" + str2;
                    }
                    Class87.string_0 = "-0005";
                    Class87.string_1 = "服务器处理异常，发票上传失败！";
                    class2.method_19(xmlDocument_1, str4, 0);
                    Class101.smethod_1("(发票上传):局端处理错误：" + str3);
                    return false;
                }
                catch (Exception)
                {
                }
                return false;
            }
            Class101.smethod_1("(发票上传):无返回信息！");
            return false;
        }

        public bool method_7(XmlDocument xmlDocument_0, XmlDocument xmlDocument_1)
        {
            if ((xmlDocument_0 != null) && (xmlDocument_1 != null))
            {
                Class84 class2 = new Class84();
                string str = "";
                try
                {
                    string str2 = string.Empty;
                    string str3 = class2.method_11(xmlDocument_0, ref str, out str2);
                    if (string.IsNullOrEmpty(str))
                    {
                        string str4 = "已执行发票上传，服务器处理异常";
                        if (string.IsNullOrEmpty(str2))
                        {
                            str4 = str4 + "。";
                        }
                        else
                        {
                            str4 = str4 + "，服务器返回错误信息：" + str2;
                        }
                        Class87.string_0 = "-0005";
                        Class87.string_1 = "服务器处理异常，发票上传失败！";
                        class2.method_20(xmlDocument_1, str4, 0);
                        Class101.smethod_1("(发票上传):局端处理错误：" + str3);
                        return false;
                    }
                    Class101.smethod_0("(发票上传)开始解析卷票上传结果中序列号");
                    class2.method_13(xmlDocument_1, str);
                    class2.method_20(xmlDocument_1, "已执行发票上传，发票报送状态已置为报送中。", 3);
                    Class87.string_0 = "0000";
                    Class87.string_1 = str;
                    return true;
                }
                catch (Exception)
                {
                }
                return false;
            }
            Class101.smethod_1("(发票上传):无返回信息！");
            return false;
        }

        public bool method_8(XmlDocument xmlDocument_0, XmlDocument xmlDocument_1, string string_0)
        {
            if ((xmlDocument_0 != null) && (xmlDocument_1 != null))
            {
                try
                {
                    Class84 class2 = new Class84();
                    XmlNodeList elementsByTagName = xmlDocument_0.GetElementsByTagName("data");
                    if ((elementsByTagName != null) && (elementsByTagName.Count >= 1))
                    {
                        string str = string.Empty;
                        string str5 = string.Empty;
                        string str2 = string.Empty;
                        string str4 = string.Empty;
                        for (int i = 0; i < elementsByTagName.Count; i++)
                        {
                            if (elementsByTagName[i].Attributes["name"].Value.Equals("fplx_dm", StringComparison.CurrentCultureIgnoreCase))
                            {
                                if (elementsByTagName[i].Attributes["value"] != null)
                                {
                                    str = elementsByTagName[i].Attributes["value"].Value;
                                }
                            }
                            else if (elementsByTagName[i].Attributes["name"].Value.Equals("returnCode", StringComparison.CurrentCultureIgnoreCase))
                            {
                                if (elementsByTagName[i].Attributes["value"] != null)
                                {
                                    str5 = elementsByTagName[i].Attributes["value"].Value;
                                }
                            }
                            else if (elementsByTagName[i].Attributes["name"].Value.Equals("slxlh", StringComparison.CurrentCultureIgnoreCase))
                            {
                                if (elementsByTagName[i].Attributes["value"] != null)
                                {
                                    str2 = elementsByTagName[i].Attributes["value"].Value;
                                }
                            }
                            else if (elementsByTagName[i].Attributes["name"].Value.Equals("returnMessage", StringComparison.CurrentCultureIgnoreCase) && (elementsByTagName[i].Attributes["value"] != null))
                            {
                                str4 = elementsByTagName[i].Attributes["value"].Value;
                            }
                        }
                        if ((!string.IsNullOrEmpty(str2) && !string.IsNullOrEmpty(str)) && str5.Equals("00"))
                        {
                            class2.method_14(xmlDocument_1, str2, str);
                            class2.method_21(xmlDocument_1, string_0, "已执行发票上传，发票报送状态已置为报送中", 3);
                            Class87.string_0 = "0000";
                            Class87.string_1 = str2;
                            return true;
                        }
                        string str3 = "已执行发票上传，服务器处理异常";
                        if (string.IsNullOrEmpty(str4))
                        {
                            str3 = str3 + "。";
                        }
                        else
                        {
                            str3 = str3 + "，返回错误信息为：" + str4;
                        }
                        Class87.string_0 = "-0005";
                        Class87.string_1 = "服务器处理异常，发票上传失败！";
                        class2.method_21(xmlDocument_1, string_0, str3, 0);
                        Class101.smethod_1("(上传线程)返回货运机动车发票处理失败！   受理序列号：" + str2 + "    fplx：" + str + "  returnCode=" + str5);
                        return false;
                    }
                    Class87.string_0 = "-0005";
                    Class87.string_1 = "服务器处理异常，发票上传失败！";
                    Class101.smethod_1("(上传线程)获取返回的货运机动车发票data节点失败！");
                    return false;
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("(上传线程)设置货运机动车发票信息至共享表失败！" + exception.ToString());
                }
                return false;
            }
            Class101.smethod_1("(发票上传):无返回信息！");
            return false;
        }

        public bool method_9(string string_0, string string_1, int int_0)
        {
            if (!string.IsNullOrEmpty(string_0) && !string.IsNullOrEmpty(string_1))
            {
                try
                {
                    Class101.smethod_0("(发票下载)：发票明文串：" + string_0);
                    string[] strArray = string_0.Split(new char[] { ';' });
                    Class101.smethod_0("(发票下载)：fpxl长度：" + strArray.Length);
                    if ((strArray != null) && (strArray.Length >= 1))
                    {
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            Dictionary<string, object> item = new Dictionary<string, object>();
                            string[] strArray2 = strArray[i].Split(new char[] { ',' });
                            Class101.smethod_0("(发票下载)：fp长度：" + strArray2.Length);
                            if ((strArray2 != null) && (strArray2.Length > 0))
                            {
                                Class96 class2 = new Class96 {
                                    FPDM = strArray2[0],
                                    FPNO = strArray2[1]
                                };
                                Class101.smethod_0("(发票下载)解析成功明文开始：" + string_0 + "   fpdm:" + strArray2[0] + "   fphm:" + strArray2[1]);
                                class2 = this.method_3(Class97.dataTable_0, class2.FPNO, class2.FPDM);
                                if (class2 != null)
                                {
                                    Class101.smethod_0("(发票下载)解析成功明文-从临时表获取发票信息：" + string_0 + "   fpdm:" + class2.FPDM + "   fphm:" + class2.FPNO + "  fpzl:" + class2.Fplx);
                                    class2.FPSQHRecieveTime = DateTime.Now.ToString();
                                    if (!string.IsNullOrEmpty(string_1))
                                    {
                                        class2.Boolean_0 = false;
                                        class2.FPStatus = "1";
                                        if ((int_0 != 0) && (i >= (int_0 - 1)))
                                        {
                                            class2.FPStatus = "999";
                                        }
                                        this.method_1(class2, Enum10.Update);
                                    }
                                    item.Add("FPDM", class2.FPDM);
                                    int result = 0;
                                    if (int.TryParse(class2.FPNO, out result))
                                    {
                                        item.Add("FPHM", result);
                                    }
                                    if (class2.Fplx.Equals(FPLX.PTFP.ToString()))
                                    {
                                        item.Add("FPZL", "c");
                                    }
                                    else if (class2.Fplx.Equals(FPLX.ZYFP.ToString()))
                                    {
                                        item.Add("FPZL", "s");
                                    }
                                    else if (class2.Fplx.Equals(FPLX.HYFP.ToString()))
                                    {
                                        item.Add("FPZL", "f");
                                    }
                                    else if (class2.Fplx.Equals(FPLX.JDCFP.ToString()))
                                    {
                                        item.Add("FPZL", "j");
                                    }
                                    else if (class2.Fplx.Equals(FPLX.DZFP.ToString()))
                                    {
                                        item.Add("FPZL", "p");
                                    }
                                    else if (class2.Fplx.Equals(FPLX.JSFP.ToString()))
                                    {
                                        item.Add("FPZL", "q");
                                    }
                                    else
                                    {
                                        item.Add("FPZL", class2.Fplx);
                                    }
                                    if ((int_0 != 0) && (i >= (int_0 - 1)))
                                    {
                                        item.Add("BSRZ", "已执行发票上传结果查询，底层更新发票异常，状态已置为报送失败，请做发票修复以重新上传该发票。");
                                        item.Add("BSZT", 2);
                                    }
                                    else
                                    {
                                        item.Add("BSRZ", "已执行发票上传结果查询，发票报送状态已置为已报送。");
                                        item.Add("BSZT", 1);
                                    }
                                    if (item.Count >= 4)
                                    {
                                        Class87.list_1.Add(item);
                                    }
                                }
                            }
                        }
                        return true;
                    }
                    return false;
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("发票上传结果下载接口查询结果解析--对于上传成功的发票 失败！" + exception.ToString());
                }
            }
            return false;
        }
    }
}

