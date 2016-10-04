namespace ns2
{
    using Aisino.FTaxBase;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    internal class Class17
    {
        internal bool bool_0;
        private static Class17 class17_0;
        private Dictionary<string, long> dictionary_0;
        private int int_0;
        private int int_1;
        internal int int_2;
        private int int_3;
        internal long long_0;
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;
        private string string_4;
        private string string_5;
        private string string_6;
        private TaxCard taxCard_0;

        static Class17()
        {
            
        }

        private Class17(TaxCard taxCard_1)
        {
            
            this.dictionary_0 = new Dictionary<string, long>();
            this.string_5 = "";
            this.string_6 = "0123456789ABCDEFGHJKLMNPQRTUVWXY";
            this.taxCard_0 = taxCard_1;
        }

        public string method_0()
        {
            return this.string_0;
        }

        public string method_1()
        {
            return this.string_1;
        }

        internal bool method_10()
        {
            bool flag2;
            bool flag = true;
            try
            {
                if (!(flag = this.method_11()))
                {
                    this.taxCard_0.int_4 = 0x251e;
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                Class20.smethod_6("调用UsbKey前 检查设备存在" + exception.Message, DeviceType.UsbKey2);
                flag2 = flag;
            }
            finally
            {
                this.int_1 = Class26.Logout();
            }
            return flag2;
        }

        internal bool method_11()
        {
            bool flag = true;
        Label_002B:
            this.method_8(Class26.smethod_0());
            if (this.method_7() != 110)
            {
                if (this.method_7() == 0x6f)
                {
                    this.bool_0 = false;
                    return flag;
                }
                if (this.method_7() == 0)
                {
                    return flag;
                }
                if (!this.taxCard_0.method_56(this.string_3))
                {
                    return false;
                }
                goto Label_002B;
            }
            this.bool_0 = true;
            return flag;
        }

        internal bool method_12(string string_7, string string_8, InvoiceType invoiceType_0, string string_9, double double_0, double double_1, double double_2, DateTime dateTime_0, byte[] byte_0, ref InvoiceResult invoiceResult_0)
        {
            try
            {
                if (this.method_10())
                {
                    int.Parse(string_8);
                    InvDetail detail = null;
                    Class20.smethod_1("invDetail.OldTypeCode.Length =" + detail.OldTypeCode.Length);
                    Class20.smethod_1("invDetail.OldTypeCode=" + Encoding.GetEncoding("GBK").GetString(detail.OldTypeCode));
                    byte[] buffer = CommonTool.GetSubArray(detail.OldTypeCode, 7, 0x2d);
                    byte[] buffer2 = new byte[0x6b];
                    byte[] buffer3 = CommonTool.ByteArrayMerge(buffer, buffer2);
                    int num = -1;
                    num = this.method_13(ref invoiceResult_0, buffer3, byte_0);
                    switch (num)
                    {
                        case 0:
                            return true;

                        case -1:
                        {
                            string str = "发票填开,发票信息写入usbkey,写报税盘Ⅱ型出错，系统自动将其作废";
                            MessageShow.PromptDlg(str);
                            break;
                        }
                        default:
                            MessageShow.PromptDlg("发票填开,写报税盘Ⅱ型出错:" + num);
                            break;
                    }
                }
                return false;
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
                return false;
            }
        }

        private int method_13(ref InvoiceResult invoiceResult_0, byte[] byte_0, byte[] byte_1)
        {
            int num4;
            try
            {
                int num = 0;
                num = Class26.smethod_0();
                string str = new string('0', 0x20);
                ulong num2 = 0L;
                int num3 = 0;
                num = Class26.WriteInvoice(str, byte_0, byte_1, byte_1.Length, ref num2, ref num3);
                if (num == 0)
                {
                    invoiceResult_0.KeyFlagNo = num2;
                    invoiceResult_0.InvQryNo = num3;
                    Class20.smethod_1(string.Format("报税盘2型开票成功 keyFlagNo={0}  invQryNo={1};", num2, num3));
                }
                num4 = num;
            }
            catch
            {
                throw;
            }
            finally
            {
                Class26.Logout();
            }
            return num4;
        }

        internal bool method_14(ulong ulong_0, long long_1, DateTime dateTime_0, string string_7, string string_8)
        {
            string_8 = string_8.PadLeft(8, '0');
            bool flag = false;
            if ((ulong_0 == 0L) || (long_1 == 0L))
            {
                this.method_16(dateTime_0, string_7, string_8, ref ulong_0, ref long_1);
                if ((ulong_0 == 0L) || (long_1 == 0L))
                {
                    Class20.smethod_2(string.Format("报税盘2型遍历找不到此发票:代码{0}:号码{1}; 设备号{2}序号{3}，作废失败", new object[] { string_7, string_8, ulong_0, long_1 }));
                    return false;
                }
            }
            int num = 0;
            try
            {
                num = Class26.smethod_0();
                if (this.method_15(string_7, string_8, ulong_0, long_1))
                {
                    string str = "1";
                    num = Class26.SetInvAttrs(str.PadRight(0x20, '0'), long_1);
                    if (num == 0)
                    {
                        return true;
                    }
                    Class20.smethod_3(string.Format("设置发票属性标志出错:InvQryNo ={0} ,USBKey.SetInvAttrs返回结果={1}", long_1, num));
                }
                return flag;
            }
            catch
            {
                throw;
            }
            finally
            {
                this.int_1 = Class26.Logout();
            }
            return flag;
        }

        internal bool method_15(string string_7, string string_8, ulong ulong_0, long long_1)
        {
            string_8 = string_8.PadLeft(8, '0');
            bool flag = false;
            int num = 0;
            int num2 = 0x20;
            int num3 = 0x2800;
            byte[] buffer = new byte[0x2800];
            long num4 = long_1;
            num = Class26.ReadInvoice("1", ref num2, 0, buffer, ref num3, ref ulong_0, ref long_1);
            if (num4 != long_1)
            {
                Class20.smethod_2(string.Format("报税盘2型缺失发票:代码{0}-号码{1}-序号{2}的明细;", string_7, string_8, num4));
                return false;
            }
            if (num == 0)
            {
                int num5 = 0;
                string str2 = "";
                string str = "";
                this.method_17(buffer, ref str2, ref str, ref num5);
                if ((string_7 == str2) && (string_8 == str))
                {
                    flag = true;
                }
            }
            return flag;
        }

        internal byte[] method_16(DateTime dateTime_0, string string_7, string string_8, ref ulong ulong_0, ref long long_1)
        {
            byte[] buffer;
            try
            {
                Class26.smethod_0();
                int num = 0;
                int num2 = 0;
                Class26.GetInvStoreInfo(ref ulong_0, ref num, ref num2);
                buffer = this.method_18(num, dateTime_0, string_7, string_8, ref ulong_0, ref long_1);
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
                throw;
            }
            finally
            {
                Class26.Logout();
            }
            return buffer;
        }

        private void method_17(byte[] byte_0, ref string string_7, ref string string_8, ref int int_4)
        {
            try
            {
                int index = 0x20;
                if (Encoding.GetEncoding("GBK").GetString(byte_0, 0, 10) == "BarcodeKey")
                {
                    index = 0x20;
                }
                else
                {
                    index = 0;
                }
                string[] strArray = Encoding.GetEncoding("GBK").GetString(byte_0, index, byte_0.Length - index).Split(new char[] { '\n' });
                int_4 = int.Parse(strArray[3]);
                string_7 = Encoding.GetEncoding("GBK").GetString(byte_0, index + 2, 10);
                string_8 = strArray[2].PadLeft(8, '0');
            }
            catch (Exception exception)
            {
                string_7 = new string('0', 10);
                string_8 = new string('0', 8);
                Class20.smethod_2(exception.ToString());
            }
        }

        private byte[] method_18(int int_4, DateTime dateTime_0, string string_7, string string_8, ref ulong ulong_0, ref long long_1)
        {
            try
            {
                int num8;
                string str2;
                string str3;
                long num12;
                int num = 0;
                string_8 = string_8.PadLeft(8, '0');
                ulong num2 = 0L;
                int num3 = int.Parse(dateTime_0.ToString("yyyyMMdd"));
                Class20.smethod_1(string.Format("B2查找发票 代码{0} 号码{1} ,开票Date {2}", string_7, string_8, num3));
                int num4 = 0x20;
                new string('0', 0x20);
                int num5 = 0x2800;
                byte[] buffer = new byte[0x2800];
                long num6 = 0L;
                string key = string_7 + "-" + string_8;
                if (this.dictionary_0.ContainsKey(key))
                {
                    num6 = this.dictionary_0[key];
                    Class20.smethod_1(string.Format("InvBSPdic.ContainsKey({0})  序号{1}", key, num6));
                    num = Class26.ReadInvoice("1", ref num4, 0, buffer, ref num5, ref num2, ref num6);
                    ulong_0 = num2;
                    long_1 = num6;
                    return CommonTool.GetSubArray(buffer, 0, num5);
                }
                long num11 = 1L;
                long num10 = int_4;
                while (num11 <= num10)
                {
                    num6 = (num11 + num10) / 2L;
                    num5 = 0x2800;
                    num = Class26.ReadInvoice("1", ref num4, 0, buffer, ref num5, ref num2, ref num6);
                    if (num != 0)
                    {
                        goto Label_037C;
                    }
                    num8 = 0;
                    str2 = "";
                    str3 = "";
                    this.method_17(buffer, ref str2, ref str3, ref num8);
                    key = str2 + "-" + str3;
                    if (!this.dictionary_0.ContainsKey(key))
                    {
                        this.dictionary_0.Add(key, num6);
                        Class20.smethod_1(string.Format("InvBSPdic.Add({0})  序号{1}  ZB {2}", key, num6, num8));
                    }
                    if (num3 == num8)
                    {
                        goto Label_01F3;
                    }
                    if (num3 > num8)
                    {
                        num11 = num6 + 1L;
                    }
                    else
                    {
                        num10 = num6 - 1L;
                    }
                }
                Class20.smethod_2(string.Format("报税盘2找不到发票 代码{0} 号码{1},pStart > pEnd  开票Date ", string_7, string_8, num3));
                return new byte[1];
            Label_01F3:
                if ((str2 == string_7) && (str3 == string_8))
                {
                    ulong_0 = num2;
                    long_1 = num6;
                    return CommonTool.GetSubArray(buffer, 0, num5);
                }
                long num7 = num6;
                int num9 = -1;
                goto Label_02E0;
            Label_022E:
                num5 = 0x2800;
                num = Class26.ReadInvoice("1", ref num4, 0, buffer, ref num5, ref num2, ref num7);
                if ((num > 0) || (num12 == num7))
                {
                    goto Label_0356;
                }
                this.method_17(buffer, ref str2, ref str3, ref num8);
                key = str2 + "-" + str3;
                if (!this.dictionary_0.ContainsKey(key))
                {
                    this.dictionary_0.Add(key, num7);
                    Class20.smethod_1(string.Format("InvBSPdic.Add({0})  序号{1}  LX {2}", key, num7, num8));
                }
                if (num3 != num8)
                {
                    if (num9 != -1)
                    {
                        goto Label_031F;
                    }
                    num9 = 1;
                    num7 = num6;
                }
                else if ((str2 == string_7) && (str3 == string_8))
                {
                    goto Label_033F;
                }
            Label_02E0:
                num12 = num7;
                num7 += num9;
                if (num7 > 0L)
                {
                    goto Label_022E;
                }
                Class20.smethod_2(string.Format("报税盘2找不到发票 代码{0} 号码{1},序号小于0", string_7, string_8));
                return new byte[1];
            Label_031F:
                Class20.smethod_2(string.Format("报税盘2找不到发票 代码{0} 号码{1}", string_7, string_8));
                return new byte[1];
            Label_033F:
                ulong_0 = num2;
                long_1 = num7;
                return CommonTool.GetSubArray(buffer, 0, num5);
            Label_0356:
                Class20.smethod_2(string.Format("报税盘2找不到发票 代码{0} 号码{1} result={2}", string_7, string_8, num));
                return new byte[1];
            Label_037C:
                Class20.smethod_2("读取报税盘2型发票明细ReadInvoice,错误号：" + num);
                return new byte[1];
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
                return new byte[1];
            }
        }

        internal byte[] method_19(long long_1, string string_7, string string_8, ref ulong ulong_0, ref long long_2)
        {
            int num = 0;
            int num2 = 0x20;
            new string('0', 0x20);
            int num3 = 0x2800;
            byte[] buffer = new byte[0x2800];
            long num4 = (long_1 + this.int_2) + this.long_0;
            if (num4 < 0L)
            {
                Class20.smethod_2(string.Format("报税盘2找发票 出错 invQryNo={0}", num4));
                return new byte[1];
            }
            num = Class26.ReadInvoice("1", ref num2, 0, buffer, ref num3, ref ulong_0, ref num4);
            if (num == 0)
            {
                int num5 = 0;
                string str = "";
                string str2 = "";
                this.method_17(buffer, ref str, ref str2, ref num5);
                if ((str == string_7) && (str2 == string_8))
                {
                    long_2 = num4;
                    Class20.smethod_1(string.Format("找到报税盘2发票：序号{0}  代码:{1} 号码:{2} 日期:{3}", new object[] { num4, str, str2, num5 }));
                    return CommonTool.GetSubArray(buffer, 0, num3);
                }
                Class20.smethod_1(string.Format("GetInvMXStream没有找到报税盘2发票：卡内月序号{0}  代码:{1} 号码:{2} :止于报2盘序号{3}  代码:{4} 号码:{5} 日期:{6}", new object[] { long_1, string_7, string_8, num4, str, str2, num5 }));
                this.int_2 += -1;
                return new byte[1];
            }
            Class20.smethod_1(string.Format("拔出报税盘2  没有找到报税盘2发票：卡内月序号{0}  代码:{1} 号码:{2}  result= {0} ", new object[] { long_1, string_7, string_8, num }));
            this.int_3 = 1;
            return new byte[1];
        }

        public string method_2()
        {
            return this.string_2;
        }

        internal long method_20(DateTime dateTime_0, long long_1, ref ulong ulong_0, ref int int_4)
        {
            long num4;
            try
            {
                Class26.smethod_0();
                int num = 0;
                int num2 = 0;
                Class26.GetInvStoreInfo(ref ulong_0, ref num, ref num2);
                num4 = this.method_22(num, dateTime_0, long_1, ref ulong_0, ref int_4);
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
                throw;
            }
            return num4;
        }

        internal void method_21()
        {
            if ((Class26.Logout() == 0x7dd) && (this.int_3 == 1))
            {
                this.taxCard_0.method_56(string.Format("TCD_{0}_ ", "9505"));
            }
        }

        private long method_22(int int_4, DateTime dateTime_0, long long_1, ref ulong ulong_0, ref int int_5)
        {
            int num = 0;
            ulong num2 = 0L;
            int num3 = int.Parse(dateTime_0.ToString("yyyyMMdd"));
            Class20.smethod_1(string.Format(" 开票Date {0}", num3));
            int num4 = 0x20;
            int num5 = 0x2800;
            byte[] buffer = new byte[0x2800];
            long num6 = 0L;
            long num7 = 1L;
            long num8 = int_4;
            while (num7 <= num8)
            {
                num6 = (num7 + num8) / 2L;
                num5 = 0x2800;
                num = Class26.ReadInvoice("1", ref num4, 0, buffer, ref num5, ref num2, ref num6);
                if (num != 0)
                {
                    Class20.smethod_2("读取报税盘2型发票明细ReadInvoice,错误号：" + num);
                    return -2L;
                }
                int num10 = 0;
                string str = "";
                string str2 = "";
                this.method_17(buffer, ref str, ref str2, ref num10);
                if (num3 == num10)
                {
                    long num9 = long_1 / 20L;
                    num9 = (num9 == 0L) ? 1L : num9;
                    return this.method_23(num3, num9, ref num2, num6);
                }
                if (num3 > num10)
                {
                    num7 = num6 + 1L;
                }
                else
                {
                    num8 = num6 - 1L;
                }
            }
            return num7;
        }

        private long method_23(int int_4, long long_1, ref ulong ulong_0, long long_2)
        {
            int num = 0x20;
            int num2 = 0x2800;
            byte[] buffer = new byte[0x2800];
            long num3 = -1L * long_1;
            while (true)
            {
                int num4 = 0;
                string str = "";
                string str2 = "";
                long_2 += num3;
                if (long_2 > 0L)
                {
                    num2 = 0x2800;
                    Class26.ReadInvoice("1", ref num, 0, buffer, ref num2, ref ulong_0, ref long_2);
                    this.method_17(buffer, ref str, ref str2, ref num4);
                }
                if (int_4 > num4)
                {
                    if (num3 == -1L)
                    {
                        return (long_2 + 1L);
                    }
                    long_2 -= num3;
                    num3 /= 2L;
                }
                else if (int_4 < num4)
                {
                    Class20.smethod_1(string.Format("发票：出错  代码:{0} 号码:{1}  ", str, str2));
                    return 1L;
                }
            }
        }

        internal void method_24()
        {
            int num = 0;
            num = Class26.smethod_0();
            ulong num2 = 0L;
            int num3 = 0;
            int num4 = 0;
            num = Class26.GetInvStoreInfo(ref num2, ref num3, ref num4);
            this.method_44();
            int num5 = 0x20;
            new string('0', 0x20);
            int num6 = 0x2800;
            byte[] buffer = new byte[0x2800];
            for (long i = 1L; i <= num3; i += 1L)
            {
                if (Class26.ReadInvoice("1", ref num5, 0, buffer, ref num6, ref num2, ref i) == 0)
                {
                    int num8 = 0;
                    string str = "";
                    string str2 = "";
                    this.method_17(buffer, ref str, ref str2, ref num8);
                    Class20.smethod_1(string.Format("发票：序号{0}  代码:{1} 号码:{2} 日期:{3}", new object[] { i, str, str2, num8 }));
                }
                num6 = 0x2800;
            }
            Class20.smethod_1("usbKeyReadTaxInfo= " + this.method_44());
            num = Class26.Logout();
        }

        internal void method_25()
        {
            try
            {
                Class26.smethod_0();
                Class26.ReadTaxReturnFiles(0, @"C:\CSfile.dat");
                Class26.ReadTaxReturnFiles(1, @"C:\CSfileExcept.dat");
            }
            finally
            {
                Class26.Logout();
            }
        }

        internal int method_26(int int_4)
        {
            int num2;
            try
            {
                Class26.smethod_0();
                int num = 0;
                if (this.taxCard_0.ECardType >= ECardType.ectNewBulky)
                {
                    num = this.method_32(int_4);
                    if (num > 0)
                    {
                        Class20.smethod_6("新卡抄税过程" + num, DeviceType.UsbKey2);
                    }
                }
                else
                {
                    num = this.method_29(int_4);
                    if (num > 0)
                    {
                        Class20.smethod_6("老卡抄税" + num, DeviceType.UsbKey2);
                    }
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                Class20.smethod_6(exception.ToString(), DeviceType.UsbKey2);
                num2 = -1;
            }
            finally
            {
                Class26.Logout();
            }
            return num2;
        }

        private int method_27()
        {
            bool flag = false;
            string str2 = "";
            string str3 = "";
            this.method_46(ref str2, ref str3);
            string str4 = this.method_44();
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            if (str4.Trim().Length >= 20)
            {
                string s = str4.Substring(0, 10);
                string str7 = str4.Substring(10, 10);
                num3 = int.Parse(s);
                num5 = int.Parse(str7);
                if ((num3 <= 0) || (num5 <= 0))
                {
                    flag = true;
                }
            }
            else
            {
                flag = true;
            }
            int num8 = 0;
            int num6 = 0;
            ulong num9 = 0L;
            int num10 = Class26.GetInvStoreInfo(ref num9, ref num6, ref num8);
            if (num10 > 0)
            {
                Class20.smethod_6("USBKey.GetInvStoreInfo读发票存储信息出错:" + num10, DeviceType.UsbKey2);
                return num10;
            }
            if (!flag)
            {
                num4 = num5 + 1;
                if (num4 <= num6)
                {
                    str4 = num4.ToString("0000000000") + num6.ToString("0000000000");
                }
            }
            else
            {
                num4 = (num6 - num8) + 1;
                str4 = num4.ToString("0000000000") + num6.ToString("0000000000");
            }
            int num11 = (num6 - num4) + 1;
            int num7 = 0;
            byte[] buffer = this.method_42(num11);
            this.method_40(str2, buffer, false, 0);
            this.method_40(str3, buffer, false, 0);
            while (num4 <= num6)
            {
                int num12 = 0;
                if (!this.method_33(str2, str3, ref num4, ref num12))
                {
                    return -1;
                }
                if (num12 == 1)
                {
                    Class20.smethod_2("报税盘2型:第" + num4 + "张报税发票异常");
                    num2++;
                }
                num7++;
                num4++;
            }
            if (num2 > 10)
            {
                Class20.smethod_5("比对不符发票超过" + 10 + "条，请将该期的全部发票进行存根联补录，再进行报税！", DeviceType.UsbKey2);
            }
            else if (num2 > 0)
            {
                Class20.smethod_5("比对不符发票有 " + num2 + " 条，请将比对不符的发票进行存根联补录，再进行报税！", DeviceType.UsbKey2);
            }
            if (num7 != num2)
            {
                buffer = this.method_42(num2);
            }
            this.method_40(str3, buffer, false, 0);
            if (num7 != num11)
            {
                buffer = this.method_42(num7);
                this.method_40(str2, buffer, true, 0);
            }
            num10 = Class26.WriteTaxReturnFile(str2, str3);
            string str5 = "[" + this.taxCard_0.GetCardClock() + "]";
            if (num10 > 0)
            {
                Class20.smethod_5(str5 + "  0,抄本期税失败,错误号:" + num10, DeviceType.UsbKey2);
                Class20.smethod_8(str5 + "#0#抄本期税失败,错误号:" + num10);
                return num10;
            }
            Class20.smethod_5(string.Concat(new object[] { str5, "  1,抄本期税成功,发票总张数:", num7, " 异常发票张数：", num2 }), DeviceType.UsbKey2);
            Class20.smethod_8(string.Concat(new object[] { str5, "#1#抄本期税成功,发票总张数:", num7, " 异常发票张数：", num2 }));
            this.method_45(str4);
            return 0;
        }

        private int method_28()
        {
            bool flag = false;
            string str2 = "";
            string str3 = "";
            this.method_46(ref str2, ref str3);
            string str4 = this.method_44();
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            if (str4.Trim().Length >= 20)
            {
                string s = str4.Substring(0, 10);
                string str7 = str4.Substring(10, 10);
                num3 = int.Parse(s);
                num4 = int.Parse(str7);
                if ((num3 <= 0) && (num4 <= 0))
                {
                    flag = true;
                }
            }
            else
            {
                flag = true;
            }
            int num8 = 0;
            int num6 = 0;
            if (!flag)
            {
                num5 = num4;
                num4 = num3;
                num8 = (num5 - num4) + 1;
            }
            byte[] buffer = this.method_42(num8);
            this.method_40(str2, buffer, false, 0);
            this.method_40(str3, buffer, false, 0);
            if (!flag)
            {
                while (num4 <= num5)
                {
                    int num7 = 0;
                    if (!this.method_33(str2, str3, ref num4, ref num7))
                    {
                        return -1;
                    }
                    if (num7 == 1)
                    {
                        num2++;
                    }
                    num6++;
                    num4++;
                }
            }
            if (num2 > 10)
            {
                Class20.smethod_5("比对不符发票超过" + 10 + "条，请将该期的\n全部发票进行存根联补录，再进行报税！", DeviceType.UsbKey2);
            }
            else if (num2 > 0)
            {
                Class20.smethod_5("比对不符发票有 " + num2 + " 条，请将比对不符的\n发票进行存根联补录，再进行报税！", DeviceType.UsbKey2);
            }
            if (num6 != num2)
            {
                buffer = this.method_42(num2);
                this.method_40(str3, buffer, false, 0);
            }
            if (num6 != num8)
            {
                buffer = this.method_42(num6);
                this.method_40(str2, buffer, false, 0);
            }
            string str5 = "[" + this.taxCard_0.GetCardClock() + "]";
            int num9 = Class26.WriteTaxReturnFile(str2, str3);
            if (num9 > 0)
            {
                Class20.smethod_5(str5 + "  0,抄上期税失败,错误号:" + num9, DeviceType.UsbKey2);
                Class20.smethod_8(str5 + "#0#抄上期税失败,错误号:" + num9);
                return num9;
            }
            Class20.smethod_5(string.Concat(new object[] { str5, "  1,抄上期税成功,发票总张数:", num6, " 异常发票张数：", num2 }), DeviceType.UsbKey2);
            Class20.smethod_8(string.Concat(new object[] { str5, "#1#抄上期税成功,发票总张数:", num6, " 异常发票张数：", num2 }));
            return 0;
        }

        private int method_29(int int_4)
        {
            int num = 0;
            string str = "";
            bool flag = false;
            string str2 = "";
            string str3 = "";
            this.method_46(ref str2, ref str3);
            string str4 = this.method_44();
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            if (str4.Trim().Length >= 20)
            {
                string s = str4.Substring(0, 10);
                string str8 = str4.Substring(10, 10);
                num3 = int.Parse(s);
                num4 = int.Parse(str8);
                if ((num3 <= 0) || (num4 <= 0))
                {
                    flag = true;
                }
            }
            else
            {
                flag = true;
            }
            string str6 = "";
            int num8 = 0;
            int num9 = 0;
            if (int_4 == 0)
            {
                str6 = "本期";
                ulong num12 = 0L;
                int num6 = Class26.GetInvStoreInfo(ref num12, ref num9, ref num8);
                if (num6 > 0)
                {
                    Class20.smethod_6("USBKey.GetInvStoreInfo读发票存储信息出错:" + num6, DeviceType.UsbKey2);
                    return num6;
                }
                if (!flag)
                {
                    num5 = num4 + 1;
                    if (num5 <= num9)
                    {
                        str4 = num5.ToString("0000000000") + num9.ToString("0000000000");
                    }
                }
                else
                {
                    num5 = (num9 - num8) + 1;
                    str4 = num5.ToString("0000000000") + num9.ToString("0000000000");
                }
            }
            else
            {
                str6 = "上期";
                if (!flag)
                {
                    num9 = num4;
                    num5 = num3;
                }
                else
                {
                    num9 = 0;
                    num5 = 1;
                }
            }
            int num11 = (num9 - num5) + 1;
            int num10 = 0;
            byte[] buffer = this.method_42(num11);
            this.method_40(str2, buffer, false, 0);
            this.method_40(str3, buffer, false, 0);
            while (num5 <= num9)
            {
                int num13 = 0;
                if (!this.method_33(str2, str3, ref num5, ref num13))
                {
                    return -1;
                }
                if (num13 == 1)
                {
                    Class20.smethod_2("报税盘2型:第" + num5 + "张报税发票异常");
                    num2++;
                }
                num10++;
                num5++;
            }
            if (num2 > 10)
            {
                Class20.smethod_5("比对不符发票超过" + 10 + "条，请将该期的全部发票进行存根联补录，再进行报税！", DeviceType.UsbKey2);
            }
            else if (num2 > 0)
            {
                Class20.smethod_5("比对不符发票有 " + num2 + " 条，请将比对不符的发票进行存根联补录，再进行报税！", DeviceType.UsbKey2);
            }
            if (num10 != num2)
            {
                buffer = this.method_42(num2);
            }
            this.method_40(str3, buffer, false, 0);
            if (num10 != num11)
            {
                buffer = this.method_42(num10);
                this.method_40(str2, buffer, true, 0);
            }
            int num7 = Class26.WriteTaxReturnFile(str2, str3);
            string str5 = "[" + this.taxCard_0.GetCardClock() + "]";
            if (num7 > 0)
            {
                str = string.Concat(new object[] { str5, "  0,抄", str6, "税失败,错误号:", num7 });
                Class20.smethod_5(str, DeviceType.UsbKey2);
                Class20.smethod_8(str);
                num = num7;
            }
            else
            {
                str = string.Concat(new object[] { str5, "  1,抄", str6, "税成功,发票总张数:", num10, " 异常发票张数：", num2 });
                Class20.smethod_5(str, DeviceType.UsbKey2);
                Class20.smethod_8(str);
                if (int_4 == 0)
                {
                    this.string_5 = str4;
                }
            }
            File.Delete(str2);
            File.Delete(str3);
            return num;
        }

        internal void method_3()
        {
            int num = 20;
            int num2 = 20;
            byte[] buffer = new byte[20];
            byte[] buffer2 = new byte[20];
            Class26.GetKeyVersion(buffer, ref num, buffer2, ref num2);
            this.string_0 = CommonTool.GetStingByteArray(buffer);
            this.string_1 = Encoding.Default.GetString(buffer2);
        }

        internal void method_30()
        {
            try
            {
                Class26.smethod_0();
                if (this.string_5.Length > 0)
                {
                    int num = Class26.WriteTaxInfo(this.string_5);
                    if (num > 0)
                    {
                        Class20.smethod_6("写入发票报税附加信息出错,USBKey.WriteTaxInfo=" + num, DeviceType.UsbKey2);
                    }
                }
            }
            catch (Exception exception)
            {
                Class20.smethod_6(exception.ToString(), DeviceType.UsbKey2);
            }
            finally
            {
                Class26.Logout();
            }
        }

        private int method_31(int int_4)
        {
            int num = 0;
            string str = "";
            string str2 = "";
            this.method_46(ref str, ref str2);
            int histInvCount = 0;
            int num3 = 0;
            string str3 = "";
            int num4 = 0;
            bool flag = this.taxCard_0.ECardType == ECardType.const_2;
            if (this.taxCard_0.ECardType != ECardType.const_2)
            {
                List<int> periodCount = this.taxCard_0.GetPeriodCount(0);
                if (int_4 == 0)
                {
                    if (this.taxCard_0.LastRepDateMonth != this.taxCard_0.SysMonth)
                    {
                        this.taxCard_0.GetHistInvCount();
                        num3 = periodCount[0];
                        num4 = num3;
                        str3 = "征期0";
                    }
                    else
                    {
                        this.taxCard_0.GetCurInvCount();
                        num3 = periodCount[1];
                        num4 = num3 - 1;
                        str3 = "非征0";
                    }
                }
                else
                {
                    if (this.taxCard_0.LastRepDateMonth != this.taxCard_0.SysMonth)
                    {
                        str3 = "征期1";
                        num3 = periodCount[0];
                        num4 = num3 - 2;
                    }
                    else
                    {
                        str3 = "非征1";
                        num3 = periodCount[1];
                        num4 = num3 - 2;
                    }
                    this.taxCard_0.GetHistInvCount();
                }
            }
            byte[] buffer = this.method_42(0);
            this.method_40(str, buffer, false, 0);
            this.method_40(str2, buffer, false, 0);
            bool flag2 = true;
            if ((!flag && (this.taxCard_0.GetPeriodCount(0)[1] > 0)) && flag2)
            {
                num3--;
            }
            int num7 = this.taxCard_0.RepDate.Month - 1;
            long invCount = this.taxCard_0.GetInvCount(this.taxCard_0.SysYear, num7);
            try
            {
                this.taxCard_0.InvDetailOpen();
                for (int i = 0; i < invCount; i++)
                {
                    InvDetail detail = null;
                    if (detail.InvRepPeriod == num4)
                    {
                        if (Encoding.GetEncoding("GBK").GetString(detail.OldTypeCode, 0, 7) != "CardInv")
                        {
                            Class20.smethod_5(i + "发票明细头数居有误", DeviceType.UsbKey2);
                        }
                        else
                        {
                            byte[] buffer3 = CommonTool.GetSubArray(detail.OldTypeCode, 7, 0x800);
                            this.method_40(str, buffer3, true, 0);
                            histInvCount++;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                this.taxCard_0.InvDetailClose();
            }
            if (str3 == "征期0")
            {
                histInvCount = this.taxCard_0.GetHistInvCount();
            }
            byte[] buffer2 = this.method_42(histInvCount);
            this.method_41(str, buffer2);
            int num6 = Class26.WriteTaxReturnFile(str, str2);
            string str4 = "[" + this.taxCard_0.GetCardClock() + "]";
            int num5 = histInvCount;
            if (num6 > 0)
            {
                if (int_4 == 0)
                {
                    Class20.smethod_6(str4 + "  0,抄本期税失败,错误号:" + num6, DeviceType.UsbKey2);
                    Class20.smethod_8(str4 + "#0#抄本期税失败,错误号:" + num6);
                }
                else
                {
                    Class20.smethod_6(str4 + "  0,抄上期税失败,错误号:" + num6, DeviceType.UsbKey2);
                    Class20.smethod_8(str4 + "#1#抄上期税失败,发票总张数:" + num5);
                }
                num = num6;
            }
            else if (int_4 == 0)
            {
                Class20.smethod_5(str4 + "  1,抄本期税成功,发票总张数:" + num5, DeviceType.UsbKey2);
                Class20.smethod_8(str4 + "#1#抄本期税成功,发票总张数:" + num5);
            }
            else
            {
                Class20.smethod_5(str4 + "  1,抄上期税成功,发票总张数:" + num5, DeviceType.UsbKey2);
                Class20.smethod_8(str4 + "#1#抄上期税成功,发票总张数:" + num5);
            }
            File.Delete(str);
            File.Delete(str2);
            return num;
        }

        private int method_32(int int_4)
        {
            int num = 0;
            string str = "";
            string str2 = "";
            this.method_46(ref str, ref str2);
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            ECardType type1 = this.taxCard_0.ECardType;
            List<int> periodCount = this.taxCard_0.GetPeriodCount(0);
            if (int_4 == 0)
            {
                num2 = periodCount[1];
                num3 = num2;
            }
            else
            {
                num2 = periodCount[1];
                num3 = num2 - 1;
                if (num3 == 0)
                {
                    num2 = periodCount[0];
                    num3 = num2;
                    num4 = 1;
                }
            }
            byte[] buffer2 = this.method_42(0);
            this.method_40(str, buffer2, false, 0);
            this.method_40(str2, buffer2, false, 0);
            int num11 = this.taxCard_0.RepDate.Month - 1;
            if ((num11 == 0) && (this.taxCard_0.RepDate.Year != this.taxCard_0.SysYear))
            {
                num11 = 12;
            }
            num11 -= num4;
            long invCount = this.taxCard_0.GetInvCount(this.taxCard_0.SysYear, num11);
            int num7 = 0;
            this.taxCard_0.InvDetailOpen();
            try
            {
                for (int i = 0; i < invCount; i++)
                {
                    InvDetail detail = null;
                    if (detail.InvRepPeriod == num3)
                    {
                        if (Encoding.GetEncoding("GBK").GetString(detail.OldTypeCode, 0, "CardInv".Length) != "CardInv")
                        {
                            Class20.smethod_5(i + "发票明细头数居有误", DeviceType.UsbKey2);
                        }
                        else
                        {
                            if (this.taxCard_0.blQDEWM())
                            {
                                byte num13 = detail.OldTypeCode[0x7b];
                                int num14 = num13;
                                byte[] buffer4 = CommonTool.GetSubArray(detail.OldTypeCode, 7, num14 * 0x200);
                                this.method_40(str, buffer4, true, 0);
                            }
                            else
                            {
                                byte[] buffer3 = CommonTool.GetSubArray(detail.OldTypeCode, 7, 0x800);
                                this.method_40(str, buffer3, true, 0);
                            }
                            num7++;
                        }
                    }
                }
            }
            finally
            {
                this.taxCard_0.InvDetailClose();
            }
            if (this.taxCard_0.blQDEWM())
            {
                long length = 0L;
                using (FileStream stream = new FileStream(str, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    stream.Seek(0L, SeekOrigin.Begin);
                    length = stream.Length;
                }
                int num17 = (int) length;
                int num8 = (num17 - 0x800) / 0x800;
                int num5 = (num17 - 0x800) % 0x800;
                if (num5 > 0)
                {
                    num5 = 0x800 - num5;
                    num8++;
                }
                byte[] buffer = new byte[num5];
                for (int j = 0; j < num5; j++)
                {
                    buffer[j] = 0xff;
                }
                this.method_40(str, buffer, true, 0);
                buffer2 = this.method_43(num7, num8);
                Encoding.GetEncoding("GBK").GetString(buffer2, 0, 100);
            }
            else
            {
                buffer2 = this.method_42(num7);
            }
            this.method_41(str, buffer2);
            int num9 = Class26.WriteTaxReturnFile(str, str2);
            string str3 = "[" + this.taxCard_0.GetCardClock() + "]";
            int num10 = num7;
            if (num9 > 0)
            {
                if (int_4 == 0)
                {
                    Class20.smethod_6(str3 + "  0,抄本期税失败,错误号:" + num9, DeviceType.UsbKey2);
                    Class20.smethod_8(str3 + "#0#抄本期税失败,错误号:" + num9);
                    return num9;
                }
                Class20.smethod_6(str3 + "  0,抄上期税失败,错误号:" + num9, DeviceType.UsbKey2);
                Class20.smethod_8(str3 + "#1#抄上期税失败,发票总张数:" + num10);
                return num9;
            }
            if (int_4 == 0)
            {
                Class20.smethod_5(str3 + "  1,抄本期税成功,发票总张数:" + num10, DeviceType.UsbKey2);
                Class20.smethod_8(str3 + "#1#抄本期税成功,发票总张数:" + num10);
                return num;
            }
            Class20.smethod_5(str3 + "  1,抄上期税成功,发票总张数:" + num10, DeviceType.UsbKey2);
            Class20.smethod_8(str3 + "#1#抄上期税成功,发票总张数:" + num10);
            return num;
        }

        private bool method_33(string string_7, string string_8, ref int int_4, ref int int_5)
        {
            bool flag = true;
            string str = "";
            ulong num = 0L;
            int num2 = 0;
            int num3 = 0x20;
            byte[] buffer = new byte[0x20];
            int num4 = 0x800;
            byte[] buffer2 = new byte[0x800];
            num2 = Class26.ReadBSInvoice(buffer, ref num3, 0, buffer2, ref num4, ref num, ref int_4);
            string str2 = Encoding.GetEncoding("GBK").GetString(buffer);
            if (num2 > 0)
            {
                str = "读取发票信息(BS)出错";
                Class20.smethod_5(str, DeviceType.UsbKey2);
                return false;
            }
            Class18 class2 = new Class18();
            byte[] buffer4 = CommonTool.GetSubArray(buffer2, 0, 0x2d);
            if (this.taxCard_0.ECardType == ECardType.ectNewBulky)
            {
                this.method_35(buffer4, class2);
            }
            else
            {
                this.method_34(buffer4, class2);
            }
            bool flag2 = str2.Substring(0, 1) == "1";
            byte num8 = buffer4[0];
            bool flag3 = ((num8 & 2) >> 1) > 0;
            byte num7 = 0;
            if (!flag3)
            {
                if (!flag2)
                {
                    num7 = 1;
                }
            }
            else if (flag2)
            {
                num7 = 2;
            }
            else
            {
                num7 = 3;
            }
            num8 = (byte) ((num8 & 240) | num7);
            byte[] buffer5 = new byte[0x2d];
            buffer5[0] = num8;
            for (int i = 1; i < 0x2d; i++)
            {
                buffer5[i] = buffer2[i];
            }
            string str3 = string_8;
            string str4 = string_7;
            string str5 = "";
            if (!this.method_39(class2, flag2, buffer5, str5))
            {
                int length = buffer4.Length;
                byte[] buffer3 = new byte[buffer4.Length];
                buffer3[0] = num8;
                for (int j = 1; j < length; j++)
                {
                    buffer3[j] = buffer4[j];
                }
                this.method_40(str3, buffer3, true, 0);
                this.method_40(str4, buffer3, true, 0);
                int_5 = 1;
                return flag;
            }
            buffer2[0] = num8;
            this.method_40(str4, buffer2, true, 0);
            Class20.smethod_3(string.Format("老卡抄税发票成功，发票标志{0} ,设备={1},序号={2},代码={3},号码={4}", new object[] { num8, num, (int) int_4, class2.string_1, class2.int_0 }));
            int_5 = 0;
            return flag;
        }

        private bool method_34(byte[] byte_0, Class18 class18_0)
        {
            if (byte_0.Length < 0x2d)
            {
                return false;
            }
            ushort startIndex = 0;
            switch ((byte_0[0] >> 4))
            {
                case 0:
                    class18_0.string_0 = "s";
                    break;

                case 1:
                    class18_0.string_0 = "w";
                    break;

                case 2:
                    class18_0.string_0 = "c";
                    break;

                default:
                    class18_0.string_0 = "0";
                    break;
            }
            startIndex = (ushort) (startIndex + 1);
            class18_0.int_0 = BitConverter.ToInt32(byte_0, startIndex);
            startIndex = (ushort) (startIndex + 4);
            class18_0.string_1 = this.method_38(byte_0, startIndex);
            startIndex = (ushort) (startIndex + 5);
            uint num2 = BitConverter.ToUInt32(byte_0, startIndex) + byte_0[startIndex + 4];
            class18_0.double_0 = ((double) num2) / 100.0;
            startIndex = (ushort) (startIndex + 5);
            class18_0.double_1 = ((double) BitConverter.ToUInt32(byte_0, startIndex)) / 100.0;
            startIndex = (ushort) (startIndex + 4);
            class18_0.double_2 = ((double) byte_0[startIndex]) / 100.0;
            startIndex = (ushort) (startIndex + 1);
            class18_0.string_2 = this.method_36(byte_0, startIndex);
            startIndex = (ushort) (startIndex + 3);
            class18_0.string_3 = this.method_37(byte_0, startIndex);
            return true;
        }

        private bool method_35(byte[] byte_0, Class18 class18_0)
        {
            if (byte_0.Length < 0x2d)
            {
                return false;
            }
            int startIndex = 0;
            switch (byte_0[0])
            {
                case 0:
                    class18_0.string_0 = "s";
                    break;

                case 1:
                    class18_0.string_0 = "w";
                    break;

                case 2:
                    class18_0.string_0 = "c";
                    break;

                default:
                    class18_0.string_0 = "0";
                    break;
            }
            startIndex++;
            class18_0.int_0 = BitConverter.ToInt32(byte_0, startIndex);
            startIndex += 4;
            class18_0.string_1 = this.method_38(byte_0, startIndex);
            startIndex += 5;
            uint num4 = BitConverter.ToUInt32(byte_0, startIndex) + byte_0[startIndex + 4];
            class18_0.double_0 = ((double) num4) / 100.0;
            startIndex += 5;
            uint num5 = BitConverter.ToUInt32(byte_0, startIndex) + byte_0[startIndex + 4];
            class18_0.double_1 = ((double) num5) / 100.0;
            startIndex += 5;
            class18_0.double_2 = ((double) byte_0[startIndex]) / 100.0;
            startIndex++;
            class18_0.string_2 = this.method_36(byte_0, startIndex);
            startIndex += 3;
            class18_0.string_3 = this.method_37(byte_0, startIndex);
            return true;
        }

        private string method_36(byte[] byte_0, int int_4)
        {
            int num = (byte_0[int_4] << 4) + (byte_0[int_4 + 1] >> 4);
            int num2 = byte_0[int_4 + 1] & 15;
            int num3 = byte_0[int_4 + 2] >> 3;
            return (num + num2.ToString("00") + num3.ToString("00"));
        }

        private string method_37(byte[] byte_0, int int_4)
        {
            string str = "";
            int num = 0;
            int index = int_4 + 3;
            str = (BitConverter.ToInt32(byte_0, int_4) & 0xffffff).ToString();
            int num3 = 0;
            for (int i = 0; i < 14; i++)
            {
                if (num3 < 5)
                {
                    num |= byte_0[index] << num3;
                    index++;
                    num3 += 8;
                }
                str = str + this.string_6[num & 0x1f];
                num3 -= 5;
                num = num >> 5;
            }
            return str;
        }

        private string method_38(byte[] byte_0, int int_4)
        {
            byte[] buffer = CommonTool.GetSubArray(byte_0, int_4, 5);
            string str = "";
            if ((byte_0[0] & 0xc0) != 0xc0)
            {
                for (int i = 0; i < 5; i++)
                {
                    str = str + buffer[i].ToString("00");
                }
                string str3 = "";
                for (int j = 0; j < 5; j++)
                {
                    str3 = str3 + ((char) ((buffer[j] >> 4) + 0x30)) + ((char) ((buffer[j] % 0x10) + 0x30));
                }
                return str3;
            }
            string str2 = BitConverter.ToUInt32(buffer, 1).ToString("0000000000");
            int num2 = buffer[0] & 0x3f;
            return str2.Insert(5, num2.ToString("00"));
        }

        private bool method_39(Class18 class18_0, bool bool_1, byte[] byte_0, string string_7)
        {
            bool flag = true;
            string str = class18_0.string_1;
            int num = class18_0.int_0;
            InvDetail detail = null;
            if (detail.OldTypeCode == null)
            {
                Class20.smethod_2(string.Format("报税盘2型报税时写报税文件: [{0}]代码:{1} 号码:{2} 在金税设备中没有找到该发票QueryInvInfo", this.taxCard_0.GetCardClock(), str, num));
                return true;
            }
            string str6 = "CardInv";
            string str3 = Encoding.GetEncoding("GBK").GetString(detail.OldTypeCode, 0, str6.Length);
            if (str3 != str6)
            {
                Class20.smethod_8(string.Format("[{0}] {1}_{2} 比对不通过，发票明细头数居有误", this.taxCard_0.GetCardClock(), str, num));
                Class20.smethod_2(string.Format("报税盘2型报税时写报税文件：代码:{0} 号码:{1} 比对不通过，发票明细头数居有误 CardInv!={2}", str, num, str3));
                return false;
            }
            byte[] buffer = CommonTool.GetSubArray(detail.OldTypeCode, 7, 0x2d);
            bool flag2 = true;
            for (int i = 0; i < 0x2d; i++)
            {
                if (buffer[i] != byte_0[i])
                {
                    flag2 = false;
                    Class20.smethod_2(string.Format("报税盘2型报税时写报税文件：代码:{0} 号码:{1}  kt2CardInvHead[{2}] ={3}    !=    invMxHead[{2}] ={4} ", new object[] { str, num, i, buffer[i], byte_0[i] }));
                    break;
                }
            }
            if (flag2)
            {
                return flag;
            }
            Class20.smethod_2(string.Format("!isCheckOk 报税盘2型写报税文件： [{0}]代码:{1} 号码:{2} 比对不通过", this.taxCard_0.GetCardClock(), str, num));
            return false;
        }

        internal void method_4()
        {
            int num = 100;
            byte[] buffer = new byte[100];
            Class26.GetKeyUserInfo(buffer, ref num);
            this.string_2 = CommonTool.GetStingByteArray(buffer);
        }

        private void method_40(string string_7, byte[] byte_0, bool bool_1, int int_4)
        {
            if (bool_1)
            {
                using (FileStream stream = new FileStream(string_7, FileMode.Append, FileAccess.Write))
                {
                    stream.Write(byte_0, 0, byte_0.Length);
                    return;
                }
            }
            using (FileStream stream2 = new FileStream(string_7, FileMode.Create, FileAccess.Write))
            {
                stream2.Write(byte_0, 0, byte_0.Length);
            }
        }

        private void method_41(string string_7, byte[] byte_0)
        {
            using (FileStream stream = new FileStream(string_7, FileMode.OpenOrCreate, FileAccess.Write))
            {
                stream.Seek(0L, SeekOrigin.Begin);
                stream.Write(byte_0, 0, byte_0.Length);
            }
        }

        private byte[] method_42(int int_4)
        {
            string str = this.taxCard_0.CorpCode.Substring(0, 6);
            string str2 = this.taxCard_0.RepDate.Year.ToString("0000");
            string str3 = this.taxCard_0.RepDate.Month.ToString("00");
            StringBuilder builder = new StringBuilder();
            builder.Append(str);
            builder.Append("\n");
            builder.Append(str2);
            builder.Append(str3);
            builder.Append("\n");
            builder.Append(int_4);
            builder.Append("\n");
            builder.Append("#");
            builder.Append(" ");
            builder.Append(this.taxCard_0.TaxCode.Trim());
            builder.Append("\n");
            builder.Append(this.taxCard_0.Machine);
            string str4 = builder.ToString();
            byte[] buffer = new byte[0x800];
            for (int i = 0; i < str4.Length; i++)
            {
                buffer[i] = (byte) str4[i];
            }
            return buffer;
        }

        private byte[] method_43(int int_4, int int_5)
        {
            string str = this.taxCard_0.CorpCode.Substring(0, 6);
            string str2 = this.taxCard_0.RepDate.Year.ToString("0000");
            string str3 = this.taxCard_0.RepDate.Month.ToString("00");
            StringBuilder builder = new StringBuilder();
            builder.Append(str);
            builder.Append("\n");
            builder.Append(str2);
            builder.Append(str3);
            builder.Append("\n");
            builder.Append(int_5);
            builder.Append("\n");
            builder.Append("#");
            builder.Append(" ");
            builder.Append(this.taxCard_0.TaxCode.Trim());
            builder.Append("\n");
            builder.Append(this.taxCard_0.Machine);
            builder.Append("\n");
            builder.Append(int_4);
            string str4 = builder.ToString();
            byte[] buffer = new byte[0x800];
            for (int i = 0; i < str4.Length; i++)
            {
                buffer[i] = (byte) str4[i];
            }
            return buffer;
        }

        private string method_44()
        {
            int num = 0;
            int num2 = 0x20;
            byte[] buffer = new byte[0x20];
            num = Class26.ReadTaxInfo(buffer, ref num2);
            if (num > 0)
            {
                Class20.smethod_5("读出发票报税附加信息出错,USBKey.ReadTaxInfo=" + num, DeviceType.UsbKey2);
            }
            return CommonTool.GetStingByteArray(buffer);
        }

        private bool method_45(string string_7)
        {
            bool flag = false;
            int num = Class26.WriteTaxInfo(string_7);
            if (num == 0)
            {
                return true;
            }
            flag = false;
            Class20.smethod_6("写入发票报税附加信息出错,USBKey.WriteTaxInfo=" + num, DeviceType.UsbKey2);
            return flag;
        }

        private void method_46(ref string string_7, ref string string_8)
        {
            try
            {
                string str = CommonTool.smethod_4(@"\Config");
                string str2 = DateTime.Now.ToString("yyyyMMddHHmmss");
                string_7 = str + @"\NFPMXBS" + this.taxCard_0.TaxCode + str2 + ".dat";
                string_8 = str + @"\EFPMXBS" + this.taxCard_0.TaxCode + str2 + ".dat";
            }
            catch
            {
                string_7 = "0000";
                string_8 = "0000";
            }
        }

        internal string method_5(string string_7)
        {
            Class26.smethod_0();
            byte[] bytes = Encoding.GetEncoding("GBK").GetBytes(string_7);
            int length = bytes.Length;
            byte[] buffer2 = new byte[200];
            Class26.Compress(bytes, length, buffer2);
            string stingByteArray = CommonTool.GetStingByteArray(buffer2);
            Class26.Logout();
            return stingByteArray;
        }

        internal string method_6(string string_7)
        {
            Class26.smethod_0();
            byte[] bytes = Encoding.GetEncoding("GBK").GetBytes(string_7);
            int num = bytes.Length * 2;
            byte[] buffer2 = new byte[num];
            Class26.Expand(bytes, num, buffer2);
            string str = Encoding.GetEncoding("GBK").GetString(buffer2);
            Class26.Logout();
            return str;
        }

        public int method_7()
        {
            return this.int_0;
        }

        public void method_8(int int_4)
        {
            this.int_0 = int_4;
            this.string_3 = string.Concat(new object[] { "TCD_", int_4, "_", " " });
        }

        internal bool method_9()
        {
            bool flag = true;
            try
            {
                if (flag = this.method_11())
                {
                    this.string_4 = this.taxCard_0.TaxCode + ";" + this.taxCard_0.Machine;
                    this.method_8(Class26.GetKeyCheckInfo(this.string_4));
                    if (this.method_7() > 0)
                    {
                        flag = false;
                        this.taxCard_0.method_56(this.string_3);
                    }
                    else
                    {
                        int num2 = 20;
                        int num3 = 20;
                        byte[] buffer = new byte[20];
                        byte[] buffer2 = new byte[20];
                        if (Class26.GetKeyVersion(buffer, ref num2, buffer2, ref num3) == 0)
                        {
                            this.string_0 = CommonTool.GetStingByteArray(buffer);
                            this.string_1 = CommonTool.GetStingByteArray(buffer2);
                        }
                        int num4 = 100;
                        byte[] buffer3 = new byte[100];
                        if (Class26.GetKeyUserInfo(buffer3, ref num4) == 0)
                        {
                            this.string_2 = CommonTool.GetStingByteArray(buffer3);
                        }
                    }
                    this.int_1 = Class26.Logout();
                }
                return flag;
            }
            catch (Exception exception)
            {
                Class20.smethod_3("报税盘2型Login:" + exception.ToString());
                flag = false;
                return false;
            }
        }

        public static Class17 smethod_0(TaxCard taxCard_1)
        {
            if (class17_0 == null)
            {
                class17_0 = new Class17(taxCard_1);
            }
            return class17_0;
        }

        private class Class18
        {
            public double double_0;
            public double double_1;
            public double double_2;
            public int int_0;
            public string string_0;
            public string string_1;
            public string string_2;
            public string string_3;

            public Class18()
            {
                
            }
        }
    }
}

