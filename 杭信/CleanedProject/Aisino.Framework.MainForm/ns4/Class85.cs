namespace ns4
{
    using Aisino.Framework.MainForm;
    using Aisino.Framework.MainForm.UpDown;
    using Aisino.Framework.Plugin.Core.Https;
    using Aisino.Framework.Plugin.Core.Registry;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    internal sealed class Class85
    {
        private bool bool_0;
        private bool bool_1;
        private Class100 class100_0;
        private Class81 class81_0;
        private Class84 class84_0;
        public Class90 class90_0;
        private DateTime dateTime_0;
        private DateTime dateTime_1;
        private int int_0;
        private int int_1;
        private int int_2;
        private int int_3;
        private List<XmlDocument> list_0;
        private List<XmlDocument> list_1;
        private List<XmlDocument> list_2;
        private List<XmlDocument> list_3;
        private List<XmlDocument> list_4;

        public Class85()
        {
            
            this.dateTime_0 = new DateTime(0x76c, 1, 1);
            this.list_0 = new List<XmlDocument>();
            this.list_1 = new List<XmlDocument>();
            this.list_2 = new List<XmlDocument>();
            this.list_3 = new List<XmlDocument>();
            this.list_4 = new List<XmlDocument>();
            this.dateTime_1 = new DateTime(0x76c, 1, 1);
            this.class90_0 = new Class90();
            this.class84_0 = new Class84();
            this.class100_0 = new Class100();
            this.class81_0 = new Class81();
            try
            {
                this.dateTime_1 = DateTime.Now;
                this.bool_1 = RegisterManager.CheckRegFile("JI");
            }
            catch
            {
            }
        }

        private void method_0(bool bool_2, bool bool_3, string string_0, string string_1, string string_2, string string_3)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                UpdateTransMethod wBS = UpdateTransMethod.WBS;
                string str = string.Empty;
                this.dateTime_0 = DateTime.Now;
                this.bool_0 = false;
                Class87.list_0.Clear();
                if (bool_3)
                {
                    this.int_0 = this.method_5(UpdateTransMethod.BSZ);
                    wBS = UpdateTransMethod.BSZ;
                }
                else if (this.int_0 < 1)
                {
                    this.int_0 = this.method_5(UpdateTransMethod.WBS);
                    wBS = UpdateTransMethod.WBS;
                }
                if (Class87.bool_3)
                {
                    this.int_0 = 1;
                }
                else
                {
                    if (this.int_0 < 1)
                    {
                        this.int_1 = 0;
                        this.int_2 = 0;
                        this.bool_0 = true;
                        Class101.smethod_0("发票上传：没有需要上传的发票");
                        return;
                    }
                    if (bool_3)
                    {
                        this.bool_0 = true;
                    }
                    else
                    {
                        this.bool_0 = false;
                    }
                }
                if (Class87.bool_3)
                {
                    this.int_1 = 1;
                    this.int_2 = this.int_0 + 1;
                }
                else if ((!bool_2 && !bool_3) && !FormMain.bContinuUpload)
                {
                    this.int_1++;
                    this.int_2 = this.int_1 + 1;
                }
                else
                {
                    this.int_1 = 1;
                    this.int_2 = this.int_0 + 1;
                }
                if (((!string.IsNullOrEmpty(string_1) && !string.IsNullOrEmpty(string_0)) && !string.IsNullOrEmpty(string_2)) || !string.IsNullOrEmpty(string_3))
                {
                    this.int_1 = 1;
                    this.int_2 = 2;
                    wBS = UpdateTransMethod.BszAndWbs;
                }
                bool flag = false;
                int num = this.int_1;
                while (true)
                {
                    if (num >= this.int_2)
                    {
                        break;
                    }
                    this.list_0.Clear();
                    this.list_1.Clear();
                    this.list_2.Clear();
                    this.list_3.Clear();
                    this.list_4.Clear();
                    Class101.smethod_1("(上传线程)开始获取待上传的发票信息");
                    if (!this.class84_0.method_3(ref this.list_0, ref this.list_1, ref this.list_2, ref this.list_3, ref this.list_4, num, wBS, string_1, string_0, string_2, string_3))
                    {
                        Class101.smethod_1("(上传线程)没有需要上传的发票信息");
                        this.bool_0 = true;
                        this.int_0 = 0;
                        this.int_1 = 0;
                        this.int_2 = 0;
                    }
                    else
                    {
                        Class101.smethod_1("(上传线程)获取待上传的发票信息结束");
                        try
                        {
                            str = string.Empty;
                            for (int i = 0; i < this.list_0.Count; i++)
                            {
                                if (!bool_2 && (Class97.dataTable_0.Rows.Count >= Class87.int_0))
                                {
                                    goto Label_0398;
                                }
                                Class103.smethod_0(this.list_0[i], "UpMethodZPToServer" + i);
                                Class101.smethod_1("（上传线程）开始专普发票上传");
                                if (HttpsSender.SendMsg("0003", this.list_0[i].InnerXml.ToString(), out str) != 0)
                                {
                                    Class101.smethod_1("（上传线程）专普发票上传失败！             " + str.ToString());
                                    Class87.bool_0 = false;
                                    int index = str.IndexOf("[");
                                    int num3 = str.IndexOf("]");
                                    if ((index > -1) && (num3 > index))
                                    {
                                        Class87.string_0 = str.Substring(index + 1, num3 - 1);
                                    }
                                    if (num3 > 0)
                                    {
                                        Class87.string_1 = str.Substring(num3 + 1);
                                    }
                                    else
                                    {
                                        Class87.string_1 = str;
                                    }
                                    this.class84_0.method_19(this.list_0[i], "执行发票上传失败，原因：" + str, 0);
                                    this.bool_0 = this.bool_0;
                                }
                                else
                                {
                                    Class101.smethod_1("(上传线程)开始解析专普发票上传结果。");
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        document.LoadXml(str);
                                        Class103.smethod_0(document, "UpMethodZPFromServer" + i);
                                        if (this.class100_0.method_6(document, this.list_0[i]))
                                        {
                                            Class87.bool_0 = true;
                                            this.bool_0 = true;
                                            flag = true;
                                            Class101.smethod_0("(上传线程)专普发票上传结束！");
                                        }
                                    }
                                }
                            }
                            goto Label_03A6;
                        Label_0398:
                            this.int_1 = 0;
                            this.int_2 = 0;
                        Label_03A6:
                            this.list_0.Clear();
                        }
                        catch (Exception exception)
                        {
                            Class101.smethod_1("（上传线程）专普发票上传失败！" + exception.ToString());
                        }
                        try
                        {
                            str = string.Empty;
                            for (int j = 0; j < this.list_1.Count; j++)
                            {
                                if (!bool_2 && (Class97.dataTable_0.Rows.Count >= Class87.int_0))
                                {
                                    goto Label_0575;
                                }
                                Class103.smethod_0(this.list_1[j], "UpMethodHYTOServer" + j);
                                Class101.smethod_1("(上传线程)开始上传货运发票！");
                                if (HttpsSender.SendMsg("0005", this.list_1[j].InnerXml.ToString(), out str) != 0)
                                {
                                    Class101.smethod_1("（上传线程）货运发票上传失败！        " + str.ToString());
                                    Class87.bool_0 = false;
                                    int num6 = str.IndexOf("[");
                                    int num7 = str.IndexOf("]");
                                    if ((num6 > -1) && (num7 > num6))
                                    {
                                        Class87.string_0 = str.Substring(num6 + 1, num7 - 1);
                                    }
                                    if (num7 > 0)
                                    {
                                        Class87.string_1 = str.Substring(num7 + 1);
                                    }
                                    else
                                    {
                                        Class87.string_1 = str;
                                    }
                                    this.class84_0.method_21(this.list_1[j], "f", "执行发票上传失败，原因：" + str, 0);
                                    this.bool_0 = this.bool_0;
                                }
                                else
                                {
                                    Class101.smethod_1("<上传线程>开始解析货运发票上传结果。");
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        document.LoadXml(str);
                                        Class103.smethod_0(document, "UpMethodHYFromServer" + j);
                                        if (this.class100_0.method_8(document, this.list_1[j], "f"))
                                        {
                                            this.bool_0 = true;
                                            Class87.bool_0 = true;
                                            flag = true;
                                            Class101.smethod_0("（上传线程）货运发票上传结束！");
                                        }
                                    }
                                }
                            }
                            goto Label_0583;
                        Label_0575:
                            this.int_1 = 0;
                            this.int_2 = 0;
                        Label_0583:
                            this.list_1.Clear();
                        }
                        catch (Exception exception2)
                        {
                            Class101.smethod_1("（上传线程）货运发票上传失败！" + exception2.ToString());
                        }
                        try
                        {
                            for (int k = 0; k < this.list_2.Count; k++)
                            {
                                if (!bool_2 && (Class97.dataTable_0.Rows.Count >= Class87.int_0))
                                {
                                    goto Label_0752;
                                }
                                str = string.Empty;
                                Class103.smethod_0(this.list_2[k], "UpMethodJDCTOServer" + k);
                                Class101.smethod_1("(上传线程)开始上传机动车发票！");
                                if (HttpsSender.SendMsg("0005", this.list_2[k].InnerXml.ToString(), out str) != 0)
                                {
                                    Class101.smethod_1("（上传线程）机动车发票上传失败！        " + str.ToString());
                                    Class87.bool_0 = false;
                                    int num10 = str.IndexOf("[");
                                    int num9 = str.IndexOf("]");
                                    if ((num10 > -1) && (num9 > num10))
                                    {
                                        Class87.string_0 = str.Substring(num10 + 1, num9 - 1);
                                    }
                                    if (num9 > 0)
                                    {
                                        Class87.string_1 = str.Substring(num9 + 1);
                                    }
                                    else
                                    {
                                        Class87.string_1 = str;
                                    }
                                    this.class84_0.method_21(this.list_2[k], "j", "执行发票上传失败，原因：" + str, 0);
                                    this.bool_0 = this.bool_0;
                                }
                                else
                                {
                                    Class101.smethod_1("(上传线程)开始解析机动车发票上传结果。");
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        document.LoadXml(str);
                                        Class103.smethod_0(document, "UpMethodJDCFromServer" + k);
                                        if (this.class100_0.method_8(document, this.list_2[k], "j"))
                                        {
                                            Class87.bool_0 = true;
                                            this.bool_0 = true;
                                            flag = true;
                                            Class101.smethod_0("（上传线程）机动车发票上传结束！");
                                        }
                                    }
                                }
                            }
                            goto Label_0760;
                        Label_0752:
                            this.int_1 = 0;
                            this.int_2 = 0;
                        Label_0760:
                            this.list_2.Clear();
                        }
                        catch (Exception exception3)
                        {
                            Class101.smethod_1("（上传线程）机动车发票上传失败！   " + exception3.ToString());
                        }
                        try
                        {
                            str = string.Empty;
                            for (int m = 0; m < this.list_3.Count; m++)
                            {
                                if (!bool_2 && (Class97.dataTable_0.Rows.Count >= Class87.int_0))
                                {
                                    goto Label_0925;
                                }
                                Class103.smethod_0(this.list_3[m], "UpMethodZPToServer" + m);
                                Class101.smethod_1("（上传线程）开始电子发票上传");
                                if (HttpsSender.SendMsg("0031", this.list_3[m].InnerXml.ToString(), out str) != 0)
                                {
                                    Class101.smethod_1("（上传线程）电子发票上传失败！             " + str.ToString());
                                    Class87.bool_0 = false;
                                    int num13 = str.IndexOf("[");
                                    int num12 = str.IndexOf("]");
                                    if ((num13 > -1) && (num12 > num13))
                                    {
                                        Class87.string_0 = str.Substring(num13 + 1, num12 - 1);
                                    }
                                    if (num12 > 0)
                                    {
                                        Class87.string_1 = str.Substring(num12 + 1);
                                    }
                                    else
                                    {
                                        Class87.string_1 = str;
                                    }
                                    this.class84_0.method_19(this.list_3[m], "执行发票上传失败，原因：" + str, 0);
                                    this.bool_0 = this.bool_0;
                                }
                                else
                                {
                                    Class101.smethod_1("(上传线程)开始解析电子发票上传结果。");
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        document.LoadXml(str);
                                        Class103.smethod_0(document, "UpMethodZPFromServer" + m);
                                        if (this.class100_0.method_6(document, this.list_3[m]))
                                        {
                                            Class87.bool_0 = true;
                                            flag = true;
                                            this.bool_0 = true;
                                            Class101.smethod_0("(上传线程)电子发票上传结束！");
                                        }
                                    }
                                }
                            }
                            goto Label_0933;
                        Label_0925:
                            this.int_1 = 0;
                            this.int_2 = 0;
                        Label_0933:
                            this.list_3.Clear();
                        }
                        catch (Exception exception4)
                        {
                            Class101.smethod_1("（上传线程）电子发票上传失败！" + exception4.ToString());
                        }
                        try
                        {
                            str = string.Empty;
                            for (int n = 0; n < this.list_4.Count; n++)
                            {
                                if (!bool_2 && (Class97.dataTable_0.Rows.Count >= Class87.int_0))
                                {
                                    goto Label_0B08;
                                }
                                Class103.smethod_0(this.list_4[n], "UpMethodJSFPToServer" + n);
                                Class101.smethod_1("（上传线程）开始卷式发票上传");
                                if (HttpsSender.SendMsg("0029", this.list_4[n].InnerXml.ToString(), out str) != 0)
                                {
                                    Class101.smethod_1("（上传线程）卷式发票上传失败！             " + str.ToString());
                                    Class87.bool_0 = false;
                                    int num16 = str.IndexOf("[");
                                    int num15 = str.IndexOf("]");
                                    if ((num16 > -1) && (num15 > num16))
                                    {
                                        Class87.string_0 = str.Substring(num16 + 1, num15 - 1);
                                    }
                                    if (num15 > 0)
                                    {
                                        Class87.string_1 = str.Substring(num15 + 1);
                                    }
                                    else
                                    {
                                        Class87.string_1 = str;
                                    }
                                    this.class84_0.method_20(this.list_4[n], "执行发票上传失败，原因：" + str, 0);
                                    this.bool_0 = this.bool_0;
                                }
                                else
                                {
                                    Class101.smethod_1("(上传线程)开始解析卷式发票上传结果。");
                                    Class101.smethod_0("(上传线程)上传卷票返回：" + str);
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        document.LoadXml(str);
                                        Class103.smethod_0(document, "UpMethodZPFromServer" + n);
                                        if (this.class100_0.method_7(document, this.list_4[n]))
                                        {
                                            Class87.bool_0 = true;
                                            flag = true;
                                            this.bool_0 = true;
                                            Class101.smethod_0("(上传线程)卷式发票上传结束！");
                                        }
                                    }
                                }
                            }
                            goto Label_0B16;
                        Label_0B08:
                            this.int_1 = 0;
                            this.int_2 = 0;
                        Label_0B16:
                            this.list_4.Clear();
                        }
                        catch (Exception exception5)
                        {
                            Class101.smethod_1("（上传线程）卷式发票上传失败！" + exception5.ToString());
                        }
                    }
                    num++;
                }
                Class88.smethod_0();
                if (Class87.bool_2)
                {
                    this.class81_0.method_15();
                }
                Class88.smethod_3();
                if ((this.int_2 > this.int_0) && (this.int_0 != 1))
                {
                    this.int_1 = 0;
                    this.int_2 = 0;
                    this.int_0 = 0;
                    this.bool_0 = true;
                }
                if ((!string.IsNullOrEmpty(string_1) && !string.IsNullOrEmpty(string_0)) && (!string.IsNullOrEmpty(string_2) && !flag))
                {
                    Class96 class2 = this.class100_0.method_3(Class97.dataTable_0, string_0, string_1);
                    if ((class2 != null) && !string.IsNullOrEmpty(class2.FPSLH))
                    {
                        Class87.bool_0 = true;
                        Class87.string_0 = "0000";
                        Class87.string_1 = class2.FPSLH;
                    }
                    else
                    {
                        Class87.bool_0 = false;
                        Class87.string_0 = "-0007";
                        Class87.string_1 = "上传失败，未查询到受理序列号！";
                    }
                }
            }
            catch (Exception exception6)
            {
                Class101.smethod_1("<上传线程>发票上传异常：" + exception6.ToString());
            }
            finally
            {
                this.list_0.Clear();
                this.list_1.Clear();
                this.list_2.Clear();
                this.list_3.Clear();
                this.list_4.Clear();
            }
        }

        public void method_1(bool bool_2, bool bool_3)
        {
            try
            {
                this.dateTime_0 = DateTime.Now;
                string str = string.Empty;
                if (bool_2 || (Class97.dataTable_0.Rows.Count < Class87.int_0))
                {
                    if (((Class94.dictionary_0 != null) && bool_2) && (HttpsSender.TestConnect(Class94.dictionary_0, out str) != 0))
                    {
                        Class101.smethod_0("（发票上传）不进行发票上传，原因：" + str);
                    }
                    else
                    {
                        Class101.smethod_0("(上传线程)开始，是否在主线程运行：" + bool_2);
                        this.method_0(bool_2, bool_3, string.Empty, string.Empty, string.Empty, string.Empty);
                        Class101.smethod_0("(上传线程)结束，是否在主线程运行：" + bool_2);
                    }
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_1("发票上传异常！" + exception.ToString());
            }
        }

        public void method_2(string string_0, string string_1, string string_2)
        {
            try
            {
                this.class90_0.method_0();
                this.dateTime_0 = DateTime.Now;
                this.method_0(false, false, string_1, string_0, string_2, string.Empty);
            }
            catch (Exception exception)
            {
                Class101.smethod_1("在主线程启用上传异常！" + exception.ToString());
            }
        }

        public void method_3(string string_0)
        {
            try
            {
                Class87.string_4 = string_0;
                this.class90_0.method_0();
                this.dateTime_0 = DateTime.Now;
                this.class90_0.method_1();
                Class101.smethod_0("发票上传-批量接口：设置包大小" + Class87.int_3.ToString());
                this.method_0(false, false, string.Empty, string.Empty, string.Empty, string_0);
            }
            catch (Exception exception)
            {
                Class101.smethod_1("在主线程启用上传异常！" + exception.ToString());
            }
        }

        public bool method_4(DateTime dateTime_2)
        {
            if ((Class94.bool_0 || Class94.bool_1) || Class94.bool_2)
            {
                if (!this.bool_1)
                {
                    this.int_3 = this.class100_0.method_10(UpdateTransMethod.WBS);
                }
                else if (this.dateTime_1.AddSeconds((double) Class87.int_6) <= DateTime.Now)
                {
                    this.dateTime_1 = DateTime.Now;
                    this.int_3 = this.class100_0.method_10(UpdateTransMethod.WBS);
                }
                if (Class94.bool_0)
                {
                    if (this.int_3 < 1)
                    {
                        return false;
                    }
                    this.int_3 = 0;
                    return true;
                }
                if (Class94.bool_1 && !Class94.bool_2)
                {
                    if (dateTime_2.AddMinutes((double) Class94.int_0) > DateTime.Now)
                    {
                        return false;
                    }
                    this.int_3 = 0;
                    return true;
                }
                if (!Class94.bool_1 && Class94.bool_2)
                {
                    if (this.int_3 < Class94.int_1)
                    {
                        return false;
                    }
                    this.int_3 = 0;
                    return true;
                }
                if ((dateTime_2.AddMinutes((double) Class94.int_0) > DateTime.Now) && (this.int_3 < Class94.int_1))
                {
                    return false;
                }
                this.int_3 = 0;
            }
            return true;
        }

        private int method_5(UpdateTransMethod updateTransMethod_0)
        {
            int num = -1;
            try
            {
                num = this.class100_0.method_10(updateTransMethod_0);
                if (num > 0)
                {
                    num = (num / Class87.int_1) + 1;
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_1("(上传线程)获取总页数失败！" + exception.ToString());
            }
            return num;
        }

        public bool method_6()
        {
            return this.bool_0;
        }
    }
}

