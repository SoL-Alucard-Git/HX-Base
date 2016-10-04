namespace ns4
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal class Class81
    {
        private IBaseDAO ibaseDAO_0;

        public Class81()
        {
            
            this.ibaseDAO_0 = BaseDAOFactory.GetBaseDAOSQLite();
        }

        public int method_0(int int_0)
        {
            int num = 0;
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("BSZT", int_0);
            try
            {
                num = this.ibaseDAO_0.queryValueSQL<int>("Aisino.Framework.MainForm.UpDown.GetFPNum", parameter);
            }
            catch (Exception exception)
            {
                Class101.smethod_1("获取发票张数失败！" + exception.ToString());
            }
            return num;
        }

        public DateTime method_1(string string_0, string string_1, string string_2)
        {
            DateTime now = DateTime.Now;
            if (!string.IsNullOrEmpty(string_0))
            {
                try
                {
                    string str = "";
                    if (string_2.Equals("ZYFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str = "s";
                    }
                    else if (string_2.Equals("PTFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str = "c";
                    }
                    else if (string_2.Equals("HYFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str = "f";
                    }
                    else if (string_2.Equals("JDCFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str = "j";
                    }
                    else if (string_2.Equals("DZFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str = "p";
                    }
                    else if (string_2.Equals("JSFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str = "q";
                    }
                    else
                    {
                        str = string_2;
                    }
                    int result = 0;
                    int.TryParse(string_0, out result);
                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    parameter.Add("FPHM", result);
                    parameter.Add("FPDM", string_1);
                    parameter.Add("FPZL", str);
                    string s = string.Empty;
                    s = this.ibaseDAO_0.queryValueSQL<string>("Aisino.Framework.MainForm.UpDown.FPSCSJ", parameter);
                    if ((s != null) && !(s == ""))
                    {
                        DateTime.TryParse(s, out now);
                    }
                    else
                    {
                        return now;
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("获取发票上传时间异常！" + exception.ToString());
                }
            }
            return now;
        }

        private Dictionary<int, int> method_10()
        {
            TaxCard card = TaxCard.CreateInstance(CTaxCardType.const_7);
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            try
            {
                List<int> periodCount = card.GetPeriodCount(0);
                if ((periodCount != null) && (periodCount.Count >= 2))
                {
                    dictionary.Add(0, periodCount[1]);
                    dictionary.Add(2, periodCount[1]);
                }
                else
                {
                    dictionary.Add(0, 1);
                    dictionary.Add(2, 1);
                }
                periodCount.Clear();
                periodCount = card.GetPeriodCount(12);
                if ((periodCount != null) && (periodCount.Count >= 2))
                {
                    dictionary.Add(12, periodCount[1]);
                }
                else
                {
                    dictionary.Add(12, 1);
                }
                periodCount.Clear();
                periodCount = card.GetPeriodCount(11);
                if ((periodCount != null) && (periodCount.Count >= 2))
                {
                    dictionary.Add(11, periodCount[1]);
                }
                else
                {
                    dictionary.Add(11, 1);
                }
                periodCount.Clear();
                periodCount = card.GetPeriodCount(0x33);
                if ((periodCount != null) && (periodCount.Count >= 2))
                {
                    dictionary.Add(0x33, periodCount[1]);
                }
                else
                {
                    dictionary.Add(0x33, 1);
                }
                return dictionary;
            }
            catch (Exception exception)
            {
                List<int> list2 = card.GetPeriodCount(0);
                dictionary.Add(0, list2[1]);
                Class101.smethod_1("[获取进水设备当前报税期异常]" + exception.ToString());
                if ((list2 != null) && (list2.Count >= 2))
                {
                    return dictionary;
                }
                return dictionary;
            }
        }

        public string method_11(string string_0, string string_1, string string_2)
        {
            string str = "0";
            Class101.smethod_0("(发票上传)开始获取地址索引号--fpdm：" + string_1 + "   fphm：" + string_0);
            if (!string.IsNullOrEmpty(string_0))
            {
                try
                {
                    string str2 = "";
                    if (string_2.Equals("ZYFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str2 = "s";
                    }
                    else if (string_2.Equals("PTFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str2 = "c";
                    }
                    else if (string_2.Equals("HYFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str2 = "f";
                    }
                    else if (string_2.Equals("JDCFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str2 = "j";
                    }
                    else if (string_2.Equals("DZFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str2 = "p";
                    }
                    else if (string_2.Equals("JSFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str2 = "q";
                    }
                    else
                    {
                        str2 = string_2;
                    }
                    int result = 0;
                    int.TryParse(string_0, out result);
                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    parameter.Add("FPHM", result);
                    parameter.Add("FPDM", string_1);
                    parameter.Add("FPZL", str2);
                    str = this.ibaseDAO_0.queryValueSQL<string>("Aisino.Framework.MainForm.UpDown.GetDZSHY", parameter);
                    Class101.smethod_0("(发票上传)获取地址索引号结束--fpdm：" + string_1 + "   fphm：" + string_0 + "   dzsyh：" + str);
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("获取发票上传时间异常！" + exception.ToString());
                }
            }
            return str;
        }

        public string method_12(string string_0, string string_1)
        {
            string str = "0";
            if (!string.IsNullOrEmpty(string_0))
            {
                try
                {
                    int result = 0;
                    int.TryParse(string_0, out result);
                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    parameter.Add("FPHM", result);
                    parameter.Add("FPDM", string_1);
                    str = this.ibaseDAO_0.queryValueSQL<string>("Aisino.Framework.MainForm.UpDown.GetDZSHY1", parameter);
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("获取发票上传时间异常！" + exception.ToString());
                }
            }
            return str;
        }

        public byte[] method_13(byte[] byte_0, string string_0)
        {
            byte[] array = new byte[byte_0.Length];
            if (!string.IsNullOrEmpty(string_0) && (byte_0.Length >= 1))
            {
                string text1 = string_0 + "#";
                try
                {
                    byte_0.CopyTo(array, 0);
                    Class100 class2 = new Class100();
                    string[] strArray = string_0.Split(new char[] { ';' });
                    if ((strArray != null) && (strArray.Length >= 1))
                    {
                        array = this.method_16(array, new byte[] { Convert.ToByte(strArray.Length) });
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            string[] strArray2 = strArray[i].Split(new char[] { ',' });
                            if ((strArray2 != null) && (strArray2.Length > 0))
                            {
                                Class96 class3 = new Class96 {
                                    FPDM = strArray2[0],
                                    FPNO = strArray2[1]
                                };
                                class3 = class2.method_3(Class97.dataTable_0, class3.FPNO, class3.FPDM);
                                string dZSYH = "0";
                                if ((class3 != null) && !string.IsNullOrEmpty(class3.DZSYH))
                                {
                                    dZSYH = class3.DZSYH;
                                    Class101.smethod_0("组装密文地址索引号:fpdm:" + strArray2[0] + "   fphm:" + strArray2[1] + "   dzsyh:" + dZSYH);
                                }
                                else
                                {
                                    Class101.smethod_0("组装密文地址索引号--fprow为null或者空:fpdm:" + strArray2[0] + "   fphm:" + strArray2[1]);
                                    dZSYH = this.method_12(strArray2[1], strArray2[0]);
                                    if (string.IsNullOrEmpty(dZSYH))
                                    {
                                        Class101.smethod_0("组装密文无地址索引号--补0:fpdm:" + strArray2[0] + "   fphm:" + strArray2[1]);
                                        dZSYH = "0";
                                    }
                                }
                                byte[] bytes = BitConverter.GetBytes(Convert.ToInt32(dZSYH));
                                bytes = this.method_17(bytes);
                                array = this.method_16(array, bytes);
                            }
                        }
                        return array;
                    }
                    Class101.smethod_1("(发票下载)组装地址索引号异常：受理序列号为空");
                    return array;
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("(发票下载)组装地址索引号异常：" + exception.ToString());
                    array = byte_0;
                }
                return array;
            }
            Class101.smethod_1("(发票下载)组装地址索引号异常：受理序列号为空");
            return array;
        }

        public FPLX method_14(string string_0)
        {
            FPLX zYFP = FPLX.ZYFP;
            try
            {
                Class100 class2 = new Class100();
                string[] strArray = string_0.Split(new char[] { ';' });
                if ((strArray != null) && (strArray.Length >= 1))
                {
                    string[] strArray2 = strArray[0].Split(new char[] { ',' });
                    Class96 class3 = new Class96 {
                        FPDM = strArray2[0],
                        FPNO = strArray2[1]
                    };
                    class3 = class2.method_3(Class97.dataTable_0, class3.FPNO, class3.FPDM);
                    if (class3 == null)
                    {
                        Class101.smethod_1("(发票下载)获取发票种类异常：fpRow为null");
                        return zYFP;
                    }
                    if (class3.Fplx.Equals("ZYFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return FPLX.ZYFP;
                    }
                    if (class3.Fplx.Equals("PTFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return FPLX.PTFP;
                    }
                    if (class3.Fplx.Equals("HYFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return FPLX.HYFP;
                    }
                    if (class3.Fplx.Equals("JDCFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return FPLX.JDCFP;
                    }
                    if (class3.Fplx.Equals("DZFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return FPLX.DZFP;
                    }
                    if (class3.Fplx.Equals("JSFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        zYFP = FPLX.JSFP;
                    }
                    return zYFP;
                }
                Class101.smethod_1("(发票下载)获取发票种类异常：受理序列号为空");
                return zYFP;
            }
            catch (Exception exception)
            {
                Class101.smethod_1("发票下载：" + exception.ToString());
            }
            return zYFP;
        }

        public void method_15()
        {
            Class100 class2 = new Class100();
            Class84 class3 = new Class84();
            List<string> slxlhs = new List<string>();
            List<Dictionary<string, string>> listFP = new List<Dictionary<string, string>>();
            try
            {
                foreach (string str in Class87.string_4.Split(new char[] { ';' }))
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        string[] strArray3 = str.Split(new char[] { ',' });
                        if (strArray3.Length >= 2)
                        {
                            string str2 = strArray3[1];
                            if (str2.Length < 8)
                            {
                                str2 = str2.PadLeft(8, '0');
                            }
                            Class96 class4 = class2.method_3(Class97.dataTable_0, str2, strArray3[0]);
                            if ((class4 != null) && !string.IsNullOrEmpty(class4.FPSLH))
                            {
                                if (!slxlhs.Contains(class4.FPSLH))
                                {
                                    slxlhs.Add(class4.FPSLH);
                                }
                            }
                            else
                            {
                                Dictionary<string, string> item = new Dictionary<string, string>();
                                item.Add("FPDM", strArray3[0]);
                                item.Add("FPHM", str2);
                                listFP.Add(item);
                            }
                        }
                    }
                }
                Class87.xmlDocument_0 = class3.method_23(slxlhs, listFP);
            }
            catch (Exception exception)
            {
                Class101.smethod_1("发票上传-获取批量上传结果异常：" + exception.ToString());
            }
        }

        private byte[] method_16(byte[] byte_0, byte[] byte_1)
        {
            byte[] array = new byte[byte_0.Length + byte_1.Length];
            try
            {
                byte_0.CopyTo(array, 0);
                byte_1.CopyTo(array, byte_0.Length);
            }
            catch (Exception exception)
            {
                Class101.smethod_1(exception.ToString());
            }
            return array;
        }

        private byte[] method_17(byte[] byte_0)
        {
            byte[] buffer = new byte[byte_0.Length];
            for (int i = 0; i < byte_0.Length; i++)
            {
                buffer[i] = byte_0[(byte_0.Length - i) - 1];
            }
            return buffer;
        }

        public string method_18(string string_0, string string_1)
        {
            string str = null;
            if (!string.IsNullOrEmpty(string_0) && !string.IsNullOrEmpty(string_1))
            {
                try
                {
                    foreach (Dictionary<string, object> dictionary in Class87.list_0)
                    {
                        int num = Convert.ToInt32(string_1);
                        if (dictionary["FPDM"].ToString().Equals(string_0) && dictionary["FPHM"].ToString().Equals(num.ToString()))
                        {
                            return dictionary["BSRZ"].ToString();
                        }
                    }
                    return str;
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("发票上传-批量接口获取报送日志异常：" + exception.ToString());
                }
            }
            return str;
        }

        public string method_19(string string_0)
        {
            string str = "s";
            if (!string.IsNullOrEmpty(string_0))
            {
                Class101.smethod_0("(发票下载)通过受理序列号查询fpzl：" + string_0);
                try
                {
                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    parameter.Add("SLXLH", string_0);
                    str = this.ibaseDAO_0.queryValueSQL<string>("Aisino.Framework.MainForm.UpDown.GetFPZLBySLXLH", parameter);
                    switch (str)
                    {
                        case null:
                        case "":
                            Class101.smethod_0("(发票下载)通过受理序列号查询fpzl不存在！初始化为s");
                            str = "s";
                            break;
                    }
                    Class101.smethod_0("(发票下载)通过受理序列号查询fpzl：" + str);
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("获取发票上传时间异常！" + exception.ToString());
                }
            }
            return str;
        }

        public string method_2(string string_0, string string_1, string string_2)
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(string_0))
            {
                try
                {
                    string str2 = "";
                    if (string_2.Equals("ZYFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str2 = "s";
                    }
                    else if (string_2.Equals("PTFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str2 = "c";
                    }
                    else if (string_2.Equals("HYFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str2 = "f";
                    }
                    else if (string_2.Equals("JDCFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str2 = "j";
                    }
                    else if (string_2.Equals("DZFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str2 = "p";
                    }
                    else if (string_2.Equals("JSFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str2 = "q";
                    }
                    else
                    {
                        str2 = string_2;
                    }
                    int result = 0;
                    int.TryParse(string_0, out result);
                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    parameter.Add("FPHM", result);
                    parameter.Add("FPDM", string_1);
                    parameter.Add("FPZL", str2);
                    str = this.ibaseDAO_0.queryValueSQL<string>("Aisino.Framework.MainForm.UpDown.FPZFBZ", parameter);
                    if (str == null)
                    {
                        str = "0";
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("获取发票受理序列号失败！" + exception.ToString());
                }
            }
            return str;
        }

        public DataTable method_20(string string_0)
        {
            DataTable table = new DataTable();
            if (string_0 != "")
            {
                try
                {
                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    parameter.Add("SLXLH", string_0);
                    table = this.ibaseDAO_0.querySQLDataTable("Aisino.Framework.MainForm.UpDown.SelectFPsBySLH", parameter);
                }
                catch (Exception)
                {
                }
            }
            return table;
        }

        public string method_3(string string_0, string string_1, string string_2)
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(string_0))
            {
                try
                {
                    string str2 = "";
                    if (string_2.Equals("ZYFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str2 = "s";
                    }
                    else if (string_2.Equals("PTFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str2 = "c";
                    }
                    else if (string_2.Equals("HYFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str2 = "f";
                    }
                    else if (string_2.Equals("JDCFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str2 = "j";
                    }
                    else if (string_2.Equals("DZFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str2 = "p";
                    }
                    else if (string_2.Equals("JSFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str2 = "q";
                    }
                    else
                    {
                        str2 = string_2;
                    }
                    int result = 0;
                    int.TryParse(string_0, out result);
                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    parameter.Add("FPHM", result);
                    parameter.Add("FPDM", string_1);
                    parameter.Add("FPZL", str2);
                    str = this.ibaseDAO_0.queryValueSQL<string>("Aisino.Framework.MainForm.UpDown.FPSLHChaxun", parameter);
                    if (str == null)
                    {
                        str = string.Empty;
                    }
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("获取发票受理序列号失败！" + exception.ToString());
                }
            }
            return str;
        }

        public string method_4()
        {
            string str = string.Empty;
            try
            {
                Dictionary<string, object> parameter = new Dictionary<string, object>();
                str = this.ibaseDAO_0.queryValueSQL<string>("Aisino.Framework.MainForm.UpDown.CXSCFS", parameter);
            }
            catch (Exception exception)
            {
                Class101.smethod_1("获取上传方式失败！" + exception.ToString());
            }
            return str;
        }

        public int method_5(string string_0, string string_1, string string_2)
        {
            int num = 0;
            string str = "";
            if (string_2.Equals("ZYFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "s";
            }
            else if (string_2.Equals("PTFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "c";
            }
            else if (string_2.Equals("HYFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "f";
            }
            else if (string_2.Equals("JDCFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "j";
            }
            else if (string_2.Equals("DZFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "p";
            }
            else if (string_2.Equals("JSFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "q";
            }
            else
            {
                str = string_2;
            }
            int result = 0;
            int.TryParse(string_0, out result);
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("FPHM", result);
            parameter.Add("FPDM", string_1);
            parameter.Add("FPZL", str);
            try
            {
                num = this.ibaseDAO_0.queryValueSQL<int>("Aisino.Framework.MainForm.UpDown.FPBSCS", parameter);
            }
            catch (Exception exception)
            {
                Class101.smethod_1("(下载线程)获取发票报送次数失败！" + exception.ToString());
            }
            return num;
        }

        public bool method_6(string string_0, string string_1, string string_2, string string_3)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(string_0))
            {
                string str = "";
                if (string_2.Equals("ZYFP", StringComparison.CurrentCultureIgnoreCase))
                {
                    str = "s";
                }
                else if (string_2.Equals("PTFP", StringComparison.CurrentCultureIgnoreCase))
                {
                    str = "c";
                }
                else if (string_2.Equals("HYFP", StringComparison.CurrentCultureIgnoreCase))
                {
                    str = "f";
                }
                else if (string_2.Equals("JDCFP", StringComparison.CurrentCultureIgnoreCase))
                {
                    str = "j";
                }
                else if (string_2.Equals("DZFP", StringComparison.CurrentCultureIgnoreCase))
                {
                    str = "p";
                }
                else if (string_2.Equals("JSFP", StringComparison.CurrentCultureIgnoreCase))
                {
                    str = "q";
                }
                else
                {
                    str = string_2;
                }
                int result = 0;
                int.TryParse(string_0, out result);
                int num = 0;
                int.TryParse(string_3, out num);
                switch (num)
                {
                    case 0:
                    case 2:
                    case 4:
                    {
                        int num2 = this.method_5(string_0, string_1, string_2);
                        if (num2 >= Class87.int_5)
                        {
                            return flag;
                        }
                        if (num != 4)
                        {
                            if (num2 < (Class87.int_5 - 1))
                            {
                                num = 0;
                            }
                            else
                            {
                                num = 2;
                            }
                        }
                        num2++;
                        Dictionary<string, object> parameter = new Dictionary<string, object>();
                        parameter.Add("FPHM", result);
                        parameter.Add("FPDM", string_1);
                        parameter.Add("FPZL", str);
                        parameter.Add("BSZT", num);
                        parameter.Add("BSCS", num2);
                        try
                        {
                            if (this.ibaseDAO_0.未确认DAO方法2_疑似updateSQL("Aisino.Framework.MainForm.UpDown.SetBSZTAndBSCS", parameter) > 0)
                            {
                                flag = true;
                                Class101.smethod_0("（下载线程-SetFpBSZTAndBSCS）更新数据库：发票号码：" + parameter["FPHM"].ToString().PadLeft(8, '0') + "  发票代码：" + parameter["FPDM"].ToString() + "  发票状态：" + parameter["BSZT"].ToString() + "  报送次数：" + parameter["BSCS"].ToString());
                            }
                        }
                        catch (Exception exception)
                        {
                            Class101.smethod_1("(下载线程)更新数据库报送状态和报送次数失败！" + exception.ToString());
                        }
                        break;
                    }
                }
            }
            return flag;
        }

        public int method_7(string string_0, string string_1, string string_2, int int_0)
        {
            int num = 0;
            string str = "";
            if (string_2.Equals("ZYFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "s";
            }
            else if (string_2.Equals("PTFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "c";
            }
            else if (string_2.Equals("HYFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "f";
            }
            else if (string_2.Equals("JDCFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "j";
            }
            else if (string_2.Equals("DZFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "p";
            }
            else if (string_2.Equals("JSFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "q";
            }
            else
            {
                str = string_2;
            }
            int result = 0;
            int.TryParse(string_0, out result);
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("FPHM", result);
            parameter.Add("FPDM", string_1);
            parameter.Add("FPZL", str);
            parameter.Add("BSCS", int_0);
            try
            {
                num = this.ibaseDAO_0.queryValueSQL<int>("Aisino.Framework.MainForm.UpDown.SetBSCS", parameter);
            }
            catch (Exception exception)
            {
                Class101.smethod_1("(下载线程)设置报送次数失败！" + exception.ToString());
            }
            return num;
        }

        public int method_8(List<string> sqlID, List<Dictionary<string, object>> param)
        {
            if ((sqlID.Count >= 1) && (param.Count >= 1))
            {
                try
                {
                    return this.ibaseDAO_0.未确认DAO方法1(sqlID.ToArray(), param);
                }
                catch (Exception exception)
                {
                    Class101.smethod_1("更新发票报送日志异常：" + exception.ToString());
                }
            }
            return 0;
        }

        public int method_9(string string_0, string string_1, string string_2)
        {
            int num = 0;
            string str = "";
            if (string_2.Equals("ZYFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "s";
            }
            else if (string_2.Equals("PTFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "c";
            }
            else if (string_2.Equals("HYFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "f";
            }
            else if (string_2.Equals("JDCFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "j";
            }
            else if (string_2.Equals("DZFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "p";
            }
            else if (string_2.Equals("JSFP", StringComparison.CurrentCultureIgnoreCase))
            {
                str = "q";
            }
            else
            {
                str = string_2;
            }
            int result = 0;
            int.TryParse(string_0, out result);
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("FPHM", result);
            parameter.Add("FPDM", string_1);
            parameter.Add("FPZL", str);
            parameter.Add("BSCS", "0");
            TaxCard card = TaxCard.CreateInstance(CTaxCardType.const_7);
            try
            {
                Dictionary<int, int> dictionary2 = this.method_10();
                int num3 = Convert.ToInt32(card.GetCardClock().ToString("yyyyMM"));
                int num4 = Convert.ToInt32(this.ibaseDAO_0.queryValueSQL<int>("Aisino.Framework.MainForm.UpDown.GetSSYF", parameter));
                int num5 = Convert.ToInt32(this.ibaseDAO_0.queryValueSQL<int>("Aisino.Framework.MainForm.UpDown.GetBSQ", parameter));
                Class101.smethod_0(string.Concat(new object[] { "发票延签作废：fpdm：", string_1, "  fphm:", string_0, "   fpzl:", str, "  _ssyf:", num4, "  _JSPYearAndMonth:", num3, "    _BSQ:", num5, "   _dictCurrentPeriod[(int)FPLX.DZFP]:", dictionary2[0x33] }));
                if ((num4 == num3) && (num5 == dictionary2[0x33]))
                {
                    Class101.smethod_0("延签作废：1");
                    parameter.Add("BSRZ", "【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】验签失败，系统已自动作废该发票，稍后系统会重新上传该张发票！");
                    num = this.ibaseDAO_0.queryValueSQL<int>("Aisino.Framework.MainForm.UpDown.SetBSCSAndBSRZ", parameter);
                    object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPYiKaiZuoFeiWenBenJieKouShareMethods", new object[] { "p", string_1, string_0, 3 });
                    if (Class87.bool_2)
                    {
                        Class87.string_3 = "-0009";
                        Class87.string_2 = "验签失败，系统已自动作废该发票，稍后系统会重新上传该张发票！";
                        return num;
                    }
                    if ((objArray2 != null) && ((bool) objArray2[0]))
                    {
                        Class101.smethod_0("电子发票延签失败自动作废成功：fpdm：" + string_1 + "  fphm:" + string_1);
                        new Class84().method_29(string_1, string_0);
                        Class86.smethod_4(string_1, string_0);
                        return num;
                    }
                    Class101.smethod_0("电子发票延签失败无法自动作废：fpdm：" + string_1 + "  fphm:" + string_1);
                    Class86.smethod_5(string_1, string_0);
                    return num;
                }
                Class101.smethod_0("延签作废：2");
                parameter.Add("BSRZ", "【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】验签失败，该发票已跨月或者已抄税，系统无法作废该发票，请开红票冲销该张发票！");
                num = this.ibaseDAO_0.queryValueSQL<int>("Aisino.Framework.MainForm.UpDown.SetBSCSAndBSRZ", parameter);
                if (Class87.bool_2)
                {
                    Class87.string_3 = "-0008";
                    Class87.string_2 = "验签失败，该发票已跨月或者已抄税，系统无法作废该发票，请开红票冲销该张发票！";
                    return num;
                }
                Class101.smethod_0("延签作废：3");
                Class86.smethod_5(string_1, string_0);
            }
            catch (Exception exception)
            {
                Class101.smethod_1("(下载线程)设置报送次数失败！" + exception.ToString());
            }
            return num;
        }
    }
}

