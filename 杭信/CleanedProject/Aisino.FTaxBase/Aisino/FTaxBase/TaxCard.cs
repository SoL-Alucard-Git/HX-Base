namespace Aisino.FTaxBase
{
    using ns2;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    public class TaxCard
    {
        private bool bool_0;
        private bool bool_1;
        private bool bool_2;
        private bool bool_3;
        private bool bool_4;
        private bool bool_5;
        private bool bool_6;
        private bool bool_7;
        private bool bool_8;
        internal bool bool_9;
        private byte[] byte_0;
        private Class17 class17_0;
        private Class21 class21_0;
        private CTaxCardMode ctaxCardMode_0;
        private CTaxCardType ctaxCardType_0;
        private DateTime dateTime_0;
        private DateTime dateTime_1;
        private DateTime dateTime_2;
        private DateTime dateTime_3;
        private DateTime dateTime_4;
        private DateTime dateTime_5;
        private DateTime dateTime_6;
        private DateTime dateTime_7;
        private Dictionary<string, string> dictionary_0;
        private ECardType ecardType_0;
        private int int_0;
        private int int_1;
        private int int_2;
        private int int_3;
        internal int int_4;
        internal int int_5;
        private int int_6;
        private IntPtr intptr_0;
        private InvCodeNum invCodeNum_0;
        private InvoiceType invoiceType_0;
        private InvSQInfo invSQInfo_0;
        private List<InvVolumeApp> list_0;
        private long long_0;
        private long long_1;
        private long long_2;
        private static readonly object object_0;
        private static readonly object object_1;
        private static readonly object object_2;
        private SQXXLIST sqxxlist_0;
        private string InvSignServer;
        private string string_1;
        private string string_10;
        private string string_11;
        private string string_12;
        private string string_13;
        private string string_14;
        private string string_15;
        private string string_16;
        private string string_17;
        private string string_18;
        private string string_19;
        private string string_2;
        private string string_20;
        private string string_21;
        private string string_22;
        private string string_23;
        private string string_24;
        private string string_25;
        private string string_26;
        private string string_27;
        private string string_28;
        private string string_29;
        private string string_3;
        private string string_30;
        private string string_31;
        private string string_32;
        private string string_33;
        [CompilerGenerated]
        private string string_34;
        private string string_4;
        private string string_5;
        private string _taxCode;
        private string string_7;
        private string string_8;
        private string string_9;
        private static TaxCard taxCard_0;
        private Aisino.FTaxBase.TaxRateAuthorize taxRateAuthorize_0;
        private Aisino.FTaxBase.TaxRateAuthorize taxRateAuthorize_1;
        private TaxStateInfo taxStateInfo_0;
        private ulong[] ulong_0;

        public event EventHandler ChangeDateEvent;

        public event CustomMsgBox CustomMessageBox;

        public event GetInvQryNo GetInvoiceQryNo;

        public event WriteInvVolumn WriteInvVolumDB;

        static TaxCard()
        {
            
            object_0 = new object();
            object_1 = new object();
            object_2 = new object();
            taxCard_0 = null;
        }

        private TaxCard(CTaxCardType ctaxCardType_1)
        {
            
            this.InvSignServer = "CARD";
            this.string_1 = "Single";
            this.string_2 = string.Empty;
            this.string_4 = string.Empty;
            this.string_5 = string.Empty;
            this._taxCode = "";
            this.string_7 = "";
            this.string_8 = "";
            this.string_9 = "";
            this.string_10 = "";
            this.string_11 = "";
            this.string_12 = string.Empty;
            this.string_13 = "";
            this.string_14 = "";
            this.taxRateAuthorize_0 = new Aisino.FTaxBase.TaxRateAuthorize();
            this.taxRateAuthorize_1 = new Aisino.FTaxBase.TaxRateAuthorize();
            this.taxStateInfo_0 = new TaxStateInfo();
            this.ecardType_0 = ECardType.ectNewBulky;
            this.string_15 = "11";
            this.int_1 = -1;
            this.dateTime_4 = DateTime.Parse("1978-01-01");
            this.dateTime_6 = DateTime.Parse("1978-01-01");
            this.dateTime_7 = DateTime.Parse("1900-1-1");
            this.string_17 = "";
            this.ctaxCardType_0 = CTaxCardType.const_7;
            this.ctaxCardMode_0 = CTaxCardMode.tcmHave;
            this.bool_6 = true;
            this.ulong_0 = new ulong[2];
            this.int_3 = -1;
            this.int_4 = -1;
            this.list_0 = new List<InvVolumeApp>();
            this.string_19 = "FWKP_V2.0";
            this.string_20 = string.Empty;
            this.string_21 = string.Empty;
            this.string_22 = string.Empty;
            this.string_23 = string.Empty;
            this.string_24 = string.Empty;
            this.string_25 = string.Empty;
            this.string_26 = string.Empty;
            this.string_27 = string.Empty;
            this.string_28 = string.Empty;
            this.string_29 = string.Empty;
            this.string_30 = string.Empty;
            this.string_31 = string.Empty;
            this.string_32 = string.Empty;
            this.string_33 = string.Empty;
            this.intptr_0 = IntPtr.Zero;
            try
            {
                this.InvSignServer = CommonTool.GetInvSignServer();
                this._taxCode = CommonTool.GetTaxCode();
                if ((this._taxCode.Length == 12) && (this._taxCode.StartsWith("99") || this._taxCode.StartsWith("98")))
                {
                    this.bool_0 = true;
                }
                TaxCodeUtil util = TaxCodeUtil.CreateInstance();
                string orgCode = CommonTool.GetOrgCode();
                if (string.IsNullOrEmpty(orgCode))
                {
                    orgCode = this._taxCode.Substring(0, 6);
                }
                string str2 = util.CompressTaxCode(this._taxCode.ToUpper(), orgCode);
                this.string_11 = this.string_16 = str2;
                Class20.smethod_1("开卡，压缩税号：" + this.string_16);
                this.byte_0 = new byte[0x280000];
                this.invSQInfo_0 = new InvSQInfo();
                this.string_2 = CommonTool.smethod_2();
                this.dictionary_0 = new Dictionary<string, string>();
                if ((this.TaxCode.Length == 15) && (this.TaxCode.Substring(8, 2) == "DK"))
                {
                    this.dictionary_0.Add("FLBMFlag", "FLBMV");
                }
                else
                {
                    this.dictionary_0.Add("FLBMFlag", "FLBM");
                }
                this.dictionary_0.Add("UPLOADTYPE", "CARD");
            }
            catch (Exception exception)
            {
                Class20.smethod_3(exception.ToString());
            }
        }

        public string AcqMonthTax(int int_7, int int_8)
        {
            string str = "";
            double num = 0.0;
            double num2 = 0.0;
            try
            {
                List<TaxRepCommInfo> list = this.method_29(int_8, int_7);
                if (list.Count <= 0)
                {
                    return str;
                }
                using (List<TaxRepCommInfo>.Enumerator enumerator = list.GetEnumerator())
                {
                    TaxRepCommInfo current;
                    while (enumerator.MoveNext())
                    {
                        current = enumerator.Current;
                        if (current.InvType == 0)
                        {
                            goto Label_0054;
                        }
                    }
                    goto Label_0084;
                Label_0054:
                    num = current.InvAmount - current.NavInvAmount;
                    num2 = current.InvTaxAmount - current.NavInvTaxAmount;
                }
            Label_0084:
                str = string.Format("{0},{1}", num, num2);
            }
            catch (Exception exception)
            {
                Class20.smethod_2(string.Format("Month ={0}, Year ={1};{2}", int_8, int_7, exception));
            }
            return str;
        }

        public TaxStatisData AcqTaxHistStaticsInfo(int int_7, int int_8)
        {
            TaxStatisData data = new TaxStatisData();
            this.RetCode = this.method_31(int_7, int_8);
            if ((this.RetCode == 0) && (this.ctaxCardType_0 >= CTaxCardType.tctCommonInv))
            {
                for (int i = 0; i < 8; i++)
                {
                    ushort num2 = Convert.ToUInt16(this.byte_0[i * 0x79]);
                    switch (num2)
                    {
                        case 0:
                        case 2:
                        case 11:
                        case 12:
                        case 0x33:
                        case 0x29:
                        {
                            InvAmountTaxStati stati = data.InvTypeStatData(num2);
                            if (stati != null)
                            {
                                stati.PeriodEarlyStockNum = BitConverter.ToUInt32(this.byte_0, (i * 0x79) + 1);
                                stati.BuyNum = BitConverter.ToUInt32(this.byte_0, ((i * 0x79) + 1) + 4);
                                stati.PeriodEndStockNum = BitConverter.ToUInt32(this.byte_0, ((i * 0x79) + 1) + 8);
                                stati.ReturnInvNum = BitConverter.ToUInt32(this.byte_0, ((i * 0x79) + 1) + 12);
                                stati.AllotInvNum = BitConverter.ToUInt32(this.byte_0, ((i * 0x79) + 1) + 0x10);
                                stati.ReclaimStockNum = BitConverter.ToUInt32(this.byte_0, ((i * 0x79) + 1) + 20);
                                stati.PlusInvoiceNum = BitConverter.ToUInt32(this.byte_0, ((i * 0x79) + 1) + 0x18);
                                stati.PlusInvWasteNum = BitConverter.ToUInt32(this.byte_0, ((i * 0x79) + 1) + 0x1c);
                                stati.NegativeInvoiceNum = BitConverter.ToUInt32(this.byte_0, ((i * 0x79) + 1) + 0x20);
                                stati.NegativeInvWasteNum = BitConverter.ToUInt32(this.byte_0, ((i * 0x79) + 1) + 0x24);
                                Class20.smethod_1(string.Format("发票种类{0},期初库存{1},购进发票份数{2}，期末库存份数{3}，退回发票份数{4}，分配发票份数{5},收回发票份数{6}", new object[] { num2, stati.PeriodEarlyStockNum, stati.BuyNum, stati.PeriodEndStockNum, stati.ReturnInvNum, stati.AllotInvNum, stati.ReclaimStockNum }));
                                stati.Total.XXZSJE = Convert.ToDouble(CommonTool.GetStringFromBuffer(this.byte_0, ((i * 0x79) + 1) + 40, 20));
                                stati.Total.XXZSSE = Convert.ToDouble(CommonTool.GetStringFromBuffer(this.byte_0, ((i * 0x79) + 1) + 60, 20));
                                stati.Total.XXFSJE = Convert.ToDouble(CommonTool.GetStringFromBuffer(this.byte_0, ((i * 0x79) + 1) + 80, 20));
                                stati.Total.XXFSSE = Convert.ToDouble(CommonTool.GetStringFromBuffer(this.byte_0, ((i * 0x79) + 1) + 100, 20));
                                stati.Total.SJXSJE = stati.Total.XXZSJE - stati.Total.XXFSJE;
                                stati.Total.SJXXSE = stati.Total.XXZSSE - stati.Total.XXFSSE;
                            }
                            break;
                        }
                    }
                }
            }
            return data;
        }

        public string AllotInvFromMainDev(byte[] byte_1)
        {
            ushort num = 100;
            byte[] buffer = new byte[100];
            this.int_4 = WebInvStockAPI.jsk_operate_r(0x23, 0, (short) this.int_2, this._taxCode, byte_1, (ushort) byte_1.Length, buffer, ref num);
            return ("TCD_" + this.int_4.ToString() + "_35");
        }

        public string AllotInvToSubDev(InvoiceType invoiceType_1, string string_35, int int_7, int int_8, out byte[] byte_1)
        {
            ushort num = 100;
            byte[] array = new byte[0x17];
            BitConverter.GetBytes((uint) int_7).CopyTo(array, 0);
            Encoding.GetEncoding("GBK").GetBytes(string_35).CopyTo(array, 4);
            BitConverter.GetBytes((ushort) int_8).CopyTo(array, 20);
            array[0x16] = Convert.ToByte((int) invoiceType_1);
            byte[] buffer5 = new byte[100];
            this.int_4 = WebInvStockAPI.jsk_operate_r(0x22, 0, (short) this.int_2, this._taxCode, array, 0x17, buffer5, ref num);
            if (this.int_4 == 0)
            {
                byte_1 = new byte[num];
                Array.Copy(buffer5, 0, byte_1, 0, num);
            }
            else
            {
                byte[] buffer6 = new byte[1];
                byte_1 = buffer6;
            }
            return ("TCD_" + this.int_4.ToString() + "_34");
        }

        public void BakFileForHighDev()
        {
            List<string> inList = new List<string> { "0" };
            byte[] buffer = this.method_64(0x1f, 0, inList);
            this.int_4 = this.method_63(buffer, this.byte_0);
        }

        public bool blQDEWM()
        {
            bool flag = false;
            if (this.StateInfo.CompanyType <= 0)
            {
                return flag;
            }
            string str = this.string_17;
            return (((str.Length > 0) && (str.Substring(6, 1) == "8")) || ((this.ECardType == ECardType.ectDefault) && this.class17_0.bool_0));
        }

        public string CanWriteInvCount(InvoiceType invoiceType_1, int int_7, out bool bool_10, out long long_3)
        {
            bool_10 = false;
            long_3 = 0L;
            List<string> inList = new List<string>();
            inList.Clear();
            inList.Add("1");
            inList.Add("0");
            inList.Add(((int) invoiceType_1).ToString());
            inList.Add(int_7.ToString());
            byte[] buffer = this.method_64(0x1d, 0, inList);
            this.int_4 = this.method_63(buffer, this.byte_0);
            Class20.smethod_1(string.Format("CanWriteInvCount返回值：{0}", this.int_4.ToString()));
            if (this.int_4 == 0)
            {
                Class20.smethod_1(string.Format("CanWriteInvCount返内容：{0}", BitConverter.ToString(this.byte_0, 0, 10)));
                if (this.byte_0[8] == 0)
                {
                    bool_10 = true;
                    long_3 = BitConverter.ToUInt32(this.byte_0, 4);
                }
                Class20.smethod_1(string.Format("CanWriteInvCount，canWrite{0}，invCount{1}：", (bool) bool_10, (long) long_3));
            }
            return ("TCD_" + this.int_4.ToString() + "_29");
        }

        [DllImport("CertSecurity.dll")]
        public static extern int ChangeCertPwd(string string_35, string string_36);
        public void CheckInOutBSP()
        {
            List<string> inList = new List<string>();
            byte[] buffer = new byte[0x11800];
            inList.Clear();
            inList.Add("A");
            inList.Add("0");
            byte[] buffer2 = this.method_64(0x15, 0, inList);
            this.int_4 = this.method_63(buffer2, buffer);
            Class20.smethod_1("调用接口：" + Encoding.GetEncoding("GBK").GetString(buffer2));
        }

        public bool CheckIsSelf()
        {
            List<string> inList = new List<string>();
            inList.Clear();
            inList.Add("x");
            inList.Add("0");
            byte[] buffer = this.method_64(0x15, 0, inList);
            byte[] buffer2 = new byte[0x11800];
            this.int_4 = this.method_63(buffer, buffer2);
            if (this.int_4 != 0)
            {
                return false;
            }
            string str = Class24.smethod_1(buffer2[2]);
            int num = Class24.smethod_0(buffer2[2]);
            if (string.IsNullOrEmpty(str) || (num == -1))
            {
                return false;
            }
            return ((str == this.CompressCode) && (num == this.Machine));
        }

        public byte[] CheckRegCode(byte[] byte_1)
        {
            IntPtr ptr;
            Class20.smethod_3("CheckRegCode start >>>>>>>>>>>>>>>>");
            if (this.ctaxCardMode_0 != CTaxCardMode.tcmHave)
            {
                byte[] buffer2 = new byte[0x10];
                for (int j = 0; j < 0x10; j++)
                {
                    buffer2[j] = (byte) j;
                }
                return buffer2;
            }
            List<string> inList = new List<string>();
            byte[] buffer5 = new byte[4];
            buffer5[0] = 4;
            buffer5[1] = 1;
            byte[] buffer6 = buffer5;
            this.method_62(buffer6, out ptr);
            inList.Clear();
            int length = byte_1.Length;
            for (int i = 0; i < length; i++)
            {
                this.byte_0[i] = byte_1[i];
                Marshal.WriteByte(ptr, i, byte_1[i]);
            }
            inList.Add("4");
            inList.Add(length.ToString());
            byte[] buffer4 = this.method_64(0x15, 0, inList);
            Class20.smethod_3("CheckRegCode CallTax  start >>>>>>>>>>>>>>>>");
            this.int_4 = this.method_63(buffer4, this.byte_0);
            Class20.smethod_3("CheckRegCode CallTax  end ,RetCode is:" + this.int_4.ToString());
            if (this.RetCode == 0)
            {
                length = BitConverter.ToUInt16(this.byte_0, 0);
                byte[] buffer = CommonTool.GetSubArray(this.byte_0, 2, length);
                Class20.smethod_3(string.Format("Size:{0},FC:{1}", length.ToString(), BitConverter.ToString(buffer)));
                return buffer;
            }
            return new byte[1];
        }

        public string CheckRegCode(byte[] byte_1, out byte[] byte_2, int int_7 = 0)
        {
            byte_2 = this.CheckRegCode(byte_1);
            return this.ErrCode;
        }

        public void ClearInvStore()
        {
            List<string> inList = new List<string>();
            byte[] buffer = new byte[0x11800];
            inList.Clear();
            inList.Add("OVER");
            inList.Add("0");
            byte[] buffer2 = this.method_64(4, 0, inList);
            this.int_4 = this.method_63(buffer2, buffer);
        }

        public void CloseDevice()
        {
            if (this.intptr_0 == IntPtr.Zero)
            {
                this.int_4 = 0;
            }
            else
            {
                ISignAPI napi;
                if (this.InvSignServer.ToUpper() != "CARD")
                {
                    napi = new SignSvrAPI();
                }
                else
                {
                    napi = new SignAPI();
                }
                if (napi.CloseDevice(this.intptr_0) != 0)
                {
                    this.int_4 = 0;
                }
                else
                {
                    this.intptr_0 = IntPtr.Zero;
                }
            }
        }

        public static TaxCard CreateInstance(CTaxCardType ctaxCardType_1)
        {
            if (taxCard_0 == null)
            {
                lock (object_0)
                {
                    if (taxCard_0 == null)
                    {
                        taxCard_0 = new TaxCard(ctaxCardType_1);
                    }
                }
            }
            return taxCard_0;
        }

        public string CreateInvCipher(string string_35, double double_0, double double_1, double double_2, string string_36, string string_37, byte[] byte_1, InvoiceType invoiceType_1, object[] object_3)
        {
            Class20.smethod_1(string.Format("=============================================================生成密文开始", new object[0]));
            object[] args = new object[] { string_35, double_0.ToString(), double_1.ToString(), double_2.ToString(), string_36, string_37, (byte_1 != null) ? Encoding.GetEncoding("GBK").GetString(byte_1) : "null", ((int) invoiceType_1).ToString() };
            Class20.smethod_1(string.Format("生成发票密文:购方税号{0},金额{1},税率{2},税额{3},发票明细串{4},红票通知单编号{5},汉字防伪加密串{6},发票种类{7}", args));
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string str6 = "";
            int length = string_37.Length;
            if (length < 0x10)
            {
                str6 = string_37;
            }
            else
            {
                int num6 = string_37.Length;
                str6 = string_37.Substring(num6 - 3, 3);
            }
            string item = "";
            byte[] bytes = Encoding.GetEncoding("GBK").GetBytes(string_36);
            if ((((byte_1 != null) && (byte_1.Length == 0x20)) && ((Encoding.Default.GetString(byte_1, 0, 10) == "BarcodeKey") && (invoiceType_1 != InvoiceType.transportation))) && (invoiceType_1 != InvoiceType.vehiclesales))
            {
                bytes = CommonTool.ByteArrayMerge(byte_1, bytes);
            }
            if (bytes.Length > 20)
            {
                if (length < 0x10)
                {
                    item = "0000000000";
                }
                else
                {
                    int num8 = string_37.Length;
                    item = string_37.Substring(0, num8 - 3);
                }
            }
            else
            {
                item = string_36;
            }
            List<string> inList = new List<string> {
                this.method_17(false, double_0 < 0.0, double_1).ToString(),
                double_1.ToString("0.00"),
                str6
            };
            if (this.ctaxCardType_0 != CTaxCardType.tctFuelTax)
            {
                string str3 = "";
                int num3 = string_35.Length;
                for (byte i = 0; i < string_35.Length; i = (byte) (i + 1))
                {
                    byte num7 = (byte) string_35[i];
                    num7 = (byte) (num7 ^ 0xc1);
                    num7 = (byte) (num7 ^ ((byte) (i % 0x10)));
                    if (i < (num3 - 3))
                    {
                        num7 = (byte) (num7 ^ ((byte) string_35[(num3 - 1) - (i % 3)]));
                    }
                    str3 = str3 + ((char) ((num7 >> 4) + 0x61)) + ((char) ((num7 & 15) + 0x6b));
                }
                inList.Add(str3);
            }
            else
            {
                inList.Add(string_35);
            }
            inList.Add((double_2 < 0.0) ? Math.Abs(double_2).ToString() : double_2.ToString());
            inList.Add((double_0 < 0.0) ? Math.Abs(double_0).ToString() : double_0.ToString());
            inList.Add(item);
            if (this.ctaxCardType_0 >= CTaxCardType.tctCommonInv)
            {
                inList.Add(((int) invoiceType_1).ToString());
            }
            if (invoiceType_1 == InvoiceType.transportation)
            {
                inList.Add("0");
            }
            else if (invoiceType_1 == InvoiceType.vehiclesales)
            {
                string str = string_36;
                for (int j = 0; j < 3; j++)
                {
                    str = str.Substring(str.IndexOf("\n") + 1);
                }
                str = str.Substring(0, str.IndexOf("\n")).Replace(" ", "[");
                if (str.Length > 0x11)
                {
                    str = str.Substring(0, 0x11);
                }
                Class20.smethod_3("OldTypeCode" + string_36);
                Class20.smethod_3("机动车销售统一发票需要解析车架号:" + str);
                inList.Add(str);
            }
            else
            {
                inList.Add("0");
            }
            string str4 = "0";
            if ((((object_3 != null) && (object_3.Length > 0)) && ((object_3[0] != null) && !string.IsNullOrEmpty(object_3[0].ToString()))) && (object_3[0].ToString().ToUpper() == "NCPSG"))
            {
                str4 = "ncp";
            }
            inList.Add(str4);
            byte[] buffer2 = this.method_64(0x19, 0, inList);
            Class20.smethod_1("生成密文参数：" + BitConverter.ToString(buffer2, 0, 200));
            int num10 = bytes.Length;
            BitConverter.GetBytes((uint) num10).CopyTo(buffer2, 0x3e8);
            bytes.CopyTo(buffer2, 0x3ec);
            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            this.int_4 = this.method_63(buffer2, this.byte_0);
            stopwatch2.Stop();
            double totalMilliseconds = stopwatch2.Elapsed.TotalMilliseconds;
            Class20.smethod_1(string.Format("调用底层生成密文执行时间========================================", totalMilliseconds.ToString()));
            string str5 = string.Empty;
            if (this.int_4 == 0)
            {
                ushort count = BitConverter.ToUInt16(this.byte_0, 0);
                str5 = Encoding.GetEncoding("GBK").GetString(this.byte_0, 2, count);
                string introduced32 = this.invCodeNum_0.InvTypeCode.ToString();
                Class20.smethod_1(string.Format("生成密文：代码{0},号码{1},密文{2}", introduced32, this.invCodeNum_0.InvNum.ToString(), str5));
            }
            Class20.smethod_1(string.Format("=============================================================生成密文结束：返回值{0}", this.int_4));
            stopwatch.Stop();
            double num5 = stopwatch.Elapsed.TotalMilliseconds;
            Class20.smethod_1(string.Format("生成密文接口执行总时间========================================", num5.ToString()));
            return str5;
        }

        public string CreateInvCipher(string string_35, double double_0, double double_1, double double_2, string string_36, string string_37, byte[] byte_1, InvoiceType invoiceType_1, object[] object_3, int int_7, out string string_38)
        {
            string_38 = this.CreateInvCipher(string_35, double_0, double_1, double_2, string_36, string_37, byte_1, invoiceType_1, object_3);
            return this.ErrCode;
        }

        public string GeneralCmdHandle(string string_35, string string_36)
        {
            return "TCD_0_0";
        }

        public byte[] Get9BitHashTaxCode()
        {
            byte[] buffer = new byte[9];
            List<string> inList = new List<string> { "o", "0" };
            byte[] buffer2 = this.method_64(0x15, 0, inList);
            this.int_4 = this.method_63(buffer2, this.byte_0);
            if (this.int_4 == 0)
            {
                for (int i = 2; i < 11; i++)
                {
                    buffer[i - 2] = this.byte_0[i];
                }
            }
            return buffer;
        }

        public string Get9BitHashTaxCode(out byte[] byte_1, int int_7 = 0)
        {
            byte_1 = this.Get9BitHashTaxCode();
            return string.Concat(new object[] { "TCD_", this.int_4.ToString(), "_", this.int_5 });
        }

        public List<InvVolumeFace> GetAllInvInfo()
        {
            List<InvVolumeFace> list = null;
            List<string> inList = new List<string>();
            inList.Clear();
            inList.Add("2");
            inList.Add("0");
            inList.Add("0");
            inList.Add("0");
            byte[] buffer = this.method_64(0x1d, 0, inList);
            this.int_4 = this.method_63(buffer, this.byte_0);
            if (this.int_4 == 0)
            {
                list = new List<InvVolumeFace>();
                uint num = BitConverter.ToUInt32(this.byte_0, 4);
                Class20.smethod_1("GetAllInvInfo：" + num.ToString());
                Class20.smethod_1("GetAllInvInfo,data：" + BitConverter.ToString(this.byte_0, 0, (int) (num * 0x1c)));
                for (int i = 0; i < num; i++)
                {
                    InvVolumeFace item = new InvVolumeFace {
                        InvNo = BitConverter.ToUInt32(this.byte_0, 8 + (i * 0x1c)),
                        Remain = BitConverter.ToUInt16(this.byte_0, (8 + (i * 0x1c)) + 4),
                        BuyMonth = BitConverter.ToUInt16(this.byte_0, (8 + (i * 0x1c)) + 6),
                        BuyDay = BitConverter.ToUInt16(this.byte_0, (8 + (i * 0x1c)) + 8),
                        BuyHour = BitConverter.ToUInt16(this.byte_0, (8 + (i * 0x1c)) + 10),
                        BuyMinute = BitConverter.ToUInt16(this.byte_0, (8 + (i * 0x1c)) + 12)
                    };
                    byte[] buffer2 = new byte[5];
                    for (int j = 0; j < 5; j++)
                    {
                        buffer2[j] = this.byte_0[((8 + (i * 0x1c)) + 14) + j];
                    }
                    if ((buffer2[0] & 0xc0) != 0xc0)
                    {
                        item.InvCode = this.method_50(buffer2, 5);
                    }
                    else
                    {
                        item.InvCode = this.method_54(buffer2);
                    }
                    item.Type = this.byte_0[(8 + (i * 0x1c)) + 0x13];
                    item.InvLimit = this.byte_0[(8 + (i * 0x1c)) + 20];
                    item.RetCentury = this.byte_0[(8 + (i * 0x1c)) + 0x15];
                    item.RetYear = this.byte_0[(8 + (i * 0x1c)) + 0x16];
                    item.RetMonth = this.byte_0[(8 + (i * 0x1c)) + 0x17];
                    item.RetDay = this.byte_0[(8 + (i * 0x1c)) + 0x18];
                    item.Count = BitConverter.ToUInt16(this.byte_0, (8 + (i * 0x1c)) + 0x19);
                    item.Flag = this.byte_0[(8 + (i * 0x1c)) + 0x1b];
                    list.Add(item);
                }
            }
            return list;
        }

        public DateTime GetCardClock()
        {
            //逻辑修改:试本地运行时需要直接返回DateTime.Now
            return DateTime.Now;
            if (this.ctaxCardMode_0 == CTaxCardMode.tcmNone)
            {
                return DateTime.Now;
            }
            if (this.method_4() != DrvState.dsClose)
            {
                byte[] buffer = this.method_64(15, 0, null);
                byte[] buffer2 = new byte[0x11800];
                this.int_4 = this.method_63(buffer, buffer2);
                if (this.RetCode == 0)
                {
                    Class20.smethod_1("获取金税盘时钟：" + BitConverter.ToString(buffer2, 0, 20));
                    this.dateTime_5 = CommonTool.AcqTaxDateTime(buffer2, 0, 7, 2);
                }
            }
            return this.dateTime_5;
        }

        public string GetCardClock(out DateTime dateTime_8, int int_7 = 0)
        {
            dateTime_8 = this.GetCardClock();
            return string.Concat(new object[] { "TCD_", this.int_4, "_", this.int_5 });
        }

        public int GetCertInfo(CertInfo certInfo_0)
        {
            ISignAPI napi;
            certInfo_0.Nsrsbh = string.Empty;
            certInfo_0.Qsrq = string.Empty;
            certInfo_0.Jzrq = string.Empty;
            if (this.intptr_0 == IntPtr.Zero)
            {
                Class20.smethod_1("=========================句柄为空==========================");
                return 0x25;
            }
            if (this.InvSignServer.ToUpper() != "CARD")
            {
                napi = new SignSvrAPI();
            }
            else
            {
                napi = new SignAPI();
            }
            return napi.GetCertInfo(this.intptr_0, certInfo_0);
        }

        public long GetCurInvCount()
        {
            if (this.ctaxCardType_0 == CTaxCardType.tctBizCommerce)
            {
                return (long) this.GetHistInvCount();
            }
            long num = -1L;
            byte[] buffer = this.method_64(4, 1, null);
            this.int_4 = this.method_63(buffer, this.byte_0);
            if (this.RetCode == 0)
            {
                num = BitConverter.ToUInt32(this.byte_0, 0);
                Class20.smethod_1(string.Format("取当前报税期发票明细份数:2字节值{0}，四字节值{1}", BitConverter.ToUInt16(this.byte_0, 0), BitConverter.ToUInt32(this.byte_0, 0)));
                if (num == 0xffffffffL)
                {
                    num = -1L;
                }
            }
            return num;
        }

        public InvCodeNum GetCurrentInvCode(InvoiceType invoiceType_1)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            InvCodeNum num = new InvCodeNum();
            this.invoiceType_0 = invoiceType_1;
            if (this.method_4() == DrvState.dsOpen)
            {
                List<string> inList = new List<string>();
                for (int i = 0; i < 7; i++)
                {
                    inList.Add("0");
                }
                if (this.ctaxCardType_0 >= CTaxCardType.tctCommonInv)
                {
                    inList.Add(((int) this.invoiceType_0).ToString());
                }
                byte[] buffer2 = this.method_64(5, 0, inList);
                if ((this.SQInfo.DHYBZ != "") && ((this.SQInfo.DHYBZ == "Z") || (this.SQInfo.DHYBZ == "Y")))
                {
                    buffer2[0x3e8] = Convert.ToByte((int) invoiceType_1);
                }
                this.int_4 = this.method_63(buffer2, this.byte_0);
                if (this.RetCode == 0)
                {
                    uint num2 = BitConverter.ToUInt32(this.byte_0, 0);
                    byte[] buffer = new byte[5];
                    for (int j = 0; j < 5; j++)
                    {
                        buffer[j] = this.byte_0[j + 4];
                    }
                    string str = num2.ToString();
                    if (str.Length < 8)
                    {
                        str = str.PadLeft(8, '0');
                    }
                    num.InvNum = str;
                    if (num.InvNum == "00000000")
                    {
                        num.InvTypeCode = new string('0', 10);
                    }
                    else if ((buffer[0] & 0xc0) != 0xc0)
                    {
                        num.InvTypeCode = this.method_50(buffer, 5);
                    }
                    else
                    {
                        num.InvTypeCode = this.method_54(buffer);
                    }
                    num.EndNum = BitConverter.ToUInt32(this.byte_0, 10).ToString();
                }
                if (this.ctaxCardType_0 >= CTaxCardType.tctCommonInv)
                {
                    int num7 = 0;
                    int num6 = this.byte_0[9];
                    if (num6 == 0)
                    {
                        num7 = 8;
                    }
                    else
                    {
                        num7 = num6 - 0x2e;
                    }
                    this.string_31 = CommonTool.ToPow10(num7).ToString();
                    switch (invoiceType_1)
                    {
                        case InvoiceType.special:
                            this.string_25 = this.string_31;
                            break;

                        case InvoiceType.common:
                            this.string_26 = this.string_31;
                            break;

                        case InvoiceType.transportation:
                            this.string_29 = this.string_31;
                            break;

                        case InvoiceType.vehiclesales:
                            this.string_30 = this.string_31;
                            break;

                        case InvoiceType.volticket:
                            this.string_27 = this.string_31;
                            break;

                        case InvoiceType.Electronic:
                            this.string_28 = this.string_31;
                            break;
                    }
                }
                this.invCodeNum_0 = num;
                this.invoiceType_0 = invoiceType_1;
            }
            stopwatch.Stop();
            double totalMilliseconds = stopwatch.Elapsed.TotalMilliseconds;
            Class20.smethod_1(string.Format("获取当前代码号码执行总时间========================================", totalMilliseconds.ToString()));
            return num;
        }

        public string GetCurrentInvCode(InvoiceType invoiceType_1, int int_7, out InvCodeNum invCodeNum_1)
        {
            invCodeNum_1 = this.GetCurrentInvCode(invoiceType_1);
            return this.ErrCode;
        }

        public DrvState GetDriverState()
        {
            return this.method_4();
        }

        public string GetExtandParams(string string_35)
        {
            if ((this.dictionary_0 != null) && this.dictionary_0.ContainsKey(string_35))
            {
                return this.dictionary_0[string_35];
            }
            return string.Empty;
        }

        public InvDetail GetFirstInvDetail(int int_7, int int_8)
        {
            List<string> inList = new List<string> { "1", "0", "F" };
            byte[] array = this.method_64(4, 0, inList);
            BitConverter.GetBytes((ushort) int_7).CopyTo(array, 0x3e8);
            BitConverter.GetBytes((ushort) int_8).CopyTo(array, 0x3ea);
            this.int_4 = this.method_63(array, this.byte_0);
            return this.method_22(true);
        }

        public string GetHashTaxCode()
        {
            string str = string.Empty;
            this.method_42();
            List<string> inList = new List<string> { "o", "0" };
            byte[] buffer = this.method_64(0x15, 0, inList);
            this.int_4 = this.method_63(buffer, this.byte_0);
            if (this.int_4 == 0)
            {
                BitConverter.ToUInt16(this.byte_0, 0);
                str = BitConverter.ToString(this.byte_0, 2, 8);
                if (!string.IsNullOrEmpty(str))
                {
                    str = str.Replace("-", "");
                }
            }
            return str;
        }

        public int GetHistInvCount()
        {
            int num = -1;
            if (this.ctaxCardType_0 >= CTaxCardType.tctBizCommerce)
            {
                byte[] buffer = this.method_64(4, 2, null);
                this.int_4 = this.method_63(buffer, this.byte_0);
                if (this.RetCode == 0)
                {
                    num = BitConverter.ToUInt16(this.byte_0, 0);
                    if (num == 0xffff)
                    {
                        num = -1;
                    }
                }
                return num;
            }
            byte[] buffer2 = this.method_64(4, 2, null);
            this.int_4 = this.method_63(buffer2, this.byte_0);
            if (this.RetCode > 0)
            {
                num = BitConverter.ToUInt16(this.byte_0, 0);
                if (num == 0xffff)
                {
                    num = -1;
                }
            }
            return num;
        }

        public List<int> GetHistMonInvCount()
        {
            List<int> list = new List<int>();
            int item = 0;
            for (int i = 1; i < 14; i++)
            {
                byte[] buffer = this.method_64(4, i, null);
                this.int_4 = this.method_63(buffer, this.byte_0);
                if (this.int_4 != 0)
                {
                    return list;
                }
                item = BitConverter.ToUInt16(this.byte_0, 0);
                list.Add(item);
                Class20.smethod_1("GetHistMonInvCount()取历史月份发票张数=" + item);
            }
            return list;
        }

        public string GetInvControlBSNum()
        {
            this.method_42();
            List<string> inList = new List<string>();
            string str = string.Empty;
            inList.Clear();
            inList.Add("d");
            inList.Add("0");
            byte[] buffer = this.method_64(0x15, 0, inList);
            this.int_4 = this.method_63(buffer, this.byte_0);
            if (this.int_4 != 0)
            {
                return str;
            }
            if (BitConverter.ToUInt16(this.byte_0, 0) == 0x18)
            {
                return ((str + Encoding.Default.GetString(this.byte_0, 2, 8)) + "," + Encoding.Default.GetString(this.byte_0, 11, 12));
            }
            return ((str + Encoding.Default.GetString(this.byte_0, 2, 8)) + "," + Encoding.Default.GetString(this.byte_0, 11, 9));
        }

        public string GetInvControlNum()
        {
            this.int_4 = 0;
            Class20.smethod_1("取机器编号2:" + this.string_32);
            return this.string_32;
        }

        public string GetInvControlNum(out string string_35, int int_7 = 0)
        {
            string_35 = this.GetInvControlNum();
            Class20.smethod_1("取机器编号:" + string_35);
            return "TCD_0_21";
        }

        public long GetInvCount(int int_7)
        {
            return this.GetInvCount(this.SysYear, int_7);
        }

        public long GetInvCount(int int_7, int int_8)
        {
            Class20.smethod_2(string.Format("GetInvCount()开始：年{0}，月{1}", int_7, int_8));
            long num = 0L;
            int month = 0;
            DateTime time = new DateTime(int_7, int_8, 1);
            month = this.GetMonth(time, this.TaxClock);
            if (month == -1)
            {
                return -1L;
            }
            month++;
            byte[] buffer = this.method_64(4, month, null);
            int num3 = this.method_63(buffer, this.byte_0);
            Class20.smethod_2("GetInvCount() retCode=" + num3);
            this.int_4 = num3;
            if (this.RetCode == 0)
            {
                num = BitConverter.ToUInt32(this.byte_0, 0);
                Class20.smethod_1(string.Format("取金税设备中某月的发票张数:2字节值{0}，四字节值{1}", BitConverter.ToUInt16(this.byte_0, 0), BitConverter.ToUInt32(this.byte_0, 0)));
                if (num == 0xffffffffL)
                {
                    num = -1L;
                }
                this.long_1 = num;
            }
            Class20.smethod_1(string.Format("取金税设备中某月的发票张数:返回值{0}，发票张数{1}", this.RetCode, num));
            return num;
        }

        public InvDetail GetInvDetail(long long_3)
        {
            Class20.smethod_1(string.Format(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>发票修复开始，参数：Index={0}>>>>>>>>>>>>>>>>>>>>>>>>>>", long_3));
            if (this.ecardType_0 > ECardType.ectDefault)
            {
                long_3 += this.long_1;
            }
            return this.method_19(long_3);
        }

        public List<InvSimpleDetail> GetInvDetailList(int int_7, int int_8, string string_35, int int_9, int int_10, int int_11, int int_12)
        {
            List<InvSimpleDetail> list = null;
            List<string> inList = new List<string> {
                int_7.ToString(),
                int_8.ToString(),
                string_35,
                int_9.ToString(),
                int_10.ToString()
            };
            byte[] array = this.method_64(4, 0, inList);
            byte[] bytes = BitConverter.GetBytes((uint) int_11);
            Array.Reverse(bytes, 0, bytes.Length);
            bytes.CopyTo(array, 0x3e8);
            BitConverter.GetBytes((uint) int_12).CopyTo(array, 0x3ec);
            this.int_4 = this.method_63(array, this.byte_0);
            if (this.int_4 == 0)
            {
                list = new List<InvSimpleDetail>();
                uint num = BitConverter.ToUInt32(this.byte_0, 0);
                for (int i = 0; i < num; i++)
                {
                    InvSimpleDetail item = new InvSimpleDetail {
                        InvNo = BitConverter.ToUInt32(this.byte_0, 4 + (i * 0x18)),
                        TypeCode = Encoding.GetEncoding("GBK").GetString(this.byte_0, (4 + (i * 0x18)) + 4, 0x10).TrimEnd(new char[1])
                    };
                    if (((this.byte_0[(4 + (i * 0x18)) + 20] == 0xff) && (this.byte_0[((4 + (i * 0x18)) + 20) + 1] == 0xff)) && ((this.byte_0[((4 + (i * 0x18)) + 20) + 2] == 0xff) && (this.byte_0[((4 + (i * 0x18)) + 20) + 3] == 0xff)))
                    {
                        item.Index = "";
                    }
                    else
                    {
                        Array.Reverse(this.byte_0, (4 + (i * 0x18)) + 20, 4);
                        item.Index = BitConverter.ToUInt32(this.byte_0, (4 + (i * 0x18)) + 20).ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public double GetInvLimit(InvoiceType invoiceType_1)
        {
            PZSQType type = this.invSQInfo_0.method_0(((int) invoiceType_1).ToString());
            if (type != null)
            {
                return type.InvAmountLimit;
            }
            if ((invoiceType_1 == InvoiceType.special) && string.IsNullOrEmpty(this.string_25))
            {
                return Convert.ToDouble(this.string_25);
            }
            if ((invoiceType_1 == InvoiceType.special) && string.IsNullOrEmpty(this.string_26))
            {
                return Convert.ToDouble(this.string_26);
            }
            if ((invoiceType_1 == InvoiceType.transportation) && string.IsNullOrEmpty(this.string_29))
            {
                return Convert.ToDouble(this.string_29);
            }
            if ((invoiceType_1 == InvoiceType.vehiclesales) && string.IsNullOrEmpty(this.string_30))
            {
                return Convert.ToDouble(this.string_30);
            }
            if ((invoiceType_1 == InvoiceType.Electronic) && string.IsNullOrEmpty(this.string_28))
            {
                return Convert.ToDouble(this.string_28);
            }
            if ((invoiceType_1 == InvoiceType.volticket) && string.IsNullOrEmpty(this.string_27))
            {
                return Convert.ToDouble(this.string_27);
            }
            this.string_31 = string.Empty;
            this.GetCurrentInvCode(invoiceType_1);
            return Convert.ToDouble(this.string_31);
        }

        public List<InvoiceAmountType> GetInvMonthAmount()
        {
            this.method_42();
            List<string> inList = new List<string>();
            string str = string.Empty;
            inList.Clear();
            inList.Add("b");
            inList.Add("0");
            byte[] buffer = this.method_64(0x15, 0, inList);
            this.int_4 = this.method_63(buffer, this.byte_0);
            if (this.int_4 == 0)
            {
                int num = BitConverter.ToUInt16(this.byte_0, 0);
                byte[] bytes = new byte[num];
                for (int j = 0; j < num; j++)
                {
                    bytes[j] = this.byte_0[j + 2];
                }
                int num3 = -76;
                if (bytes[0] > 0)
                {
                    str = str + Convert.ToString(bytes[0x4c + num3]) + ",";
                    string s = Encoding.Default.GetString(bytes, 0x4d + num3, 12).Trim();
                    s = CommonTool.GetStringFromBuffer(Encoding.GetEncoding("GBK").GetBytes(s), 0, s.Length);
                    while (s.Length < 3)
                    {
                        s = "0" + s;
                    }
                    string str7 = str;
                    str = str7 + s.Substring(0, s.Length - 2) + "." + s.Substring(s.Length - 2, 2) + ",";
                    s = Encoding.Default.GetString(bytes, 0x59 + num3, 12).Trim();
                    s = CommonTool.GetStringFromBuffer(Encoding.GetEncoding("GBK").GetBytes(s), 0, s.Length);
                    while (s.Length < 3)
                    {
                        s = "0" + s;
                    }
                    string str8 = str;
                    str = str8 + s.Substring(0, s.Length - 2) + "." + s.Substring(s.Length - 2, 2) + ",;";
                }
                num3 += 0x19;
                if (Convert.ToInt32(bytes[0x4c + num3]) > 0)
                {
                    str = str + Convert.ToString(bytes[0x4c + num3]) + ",";
                    string str3 = Encoding.Default.GetString(bytes, 0x4d + num3, 12).Trim();
                    str3 = CommonTool.GetStringFromBuffer(Encoding.GetEncoding("GBK").GetBytes(str3), 0, str3.Length);
                    while (str3.Length < 3)
                    {
                        str3 = "0" + str3;
                    }
                    string str9 = str;
                    str = str9 + str3.Substring(0, str3.Length - 2) + "." + str3.Substring(str3.Length - 2, 2) + ",";
                    str3 = Encoding.Default.GetString(bytes, 0x59 + num3, 12).Trim();
                    str3 = CommonTool.GetStringFromBuffer(Encoding.GetEncoding("GBK").GetBytes(str3), 0, str3.Length);
                    while (str3.Length < 3)
                    {
                        str3 = "0" + str3;
                    }
                    string str10 = str;
                    str = str10 + str3.Substring(0, str3.Length - 2) + "." + str3.Substring(str3.Length - 2, 2) + ",;";
                }
                num3 += 0x19;
                if (Convert.ToInt32(bytes[0x4c + num3]) > 0)
                {
                    str = str + Convert.ToString(bytes[0x4c + num3]) + ",";
                    string str4 = Encoding.Default.GetString(bytes, 0x4d + num3, 12).Trim();
                    str4 = CommonTool.GetStringFromBuffer(Encoding.GetEncoding("GBK").GetBytes(str4), 0, str4.Length);
                    while (str4.Length < 3)
                    {
                        str4 = "0" + str4;
                    }
                    string str11 = str;
                    str = str11 + str4.Substring(0, str4.Length - 2) + "." + str4.Substring(str4.Length - 2, 2) + ",";
                    str4 = Encoding.Default.GetString(bytes, 0x59 + num3, 12).Trim();
                    str4 = CommonTool.GetStringFromBuffer(Encoding.GetEncoding("GBK").GetBytes(str4), 0, str4.Length);
                    while (str4.Length < 3)
                    {
                        str4 = "0" + str4;
                    }
                    string str12 = str;
                    str = str12 + str4.Substring(0, str4.Length - 2) + "." + str4.Substring(str4.Length - 2, 2) + ",;";
                }
                num3 += 0x19;
                if (bytes[0x4c + num3] > 0)
                {
                    str = str + Convert.ToString(bytes[0x4c + num3]) + ",";
                    string str5 = Encoding.Default.GetString(bytes, 0x4d + num3, 12).Trim();
                    str5 = CommonTool.GetStringFromBuffer(Encoding.GetEncoding("GBK").GetBytes(str5), 0, str5.Length);
                    while (str5.Length < 3)
                    {
                        str5 = "0" + str5;
                    }
                    string str13 = str;
                    str = str13 + str5.Substring(0, str5.Length - 2) + "." + str5.Substring(str5.Length - 2, 2) + ",";
                    str5 = Encoding.Default.GetString(bytes, 0x59 + num3, 12).Trim();
                    str5 = CommonTool.GetStringFromBuffer(Encoding.GetEncoding("GBK").GetBytes(str5), 0, str5.Length);
                    while (str5.Length < 3)
                    {
                        str5 = "0" + str5;
                    }
                    string str14 = str;
                    str = str14 + str5.Substring(0, str5.Length - 2) + "." + str5.Substring(str5.Length - 2, 2) + ",;";
                }
                num3 += 0x19;
                if (bytes[0x4c + num3] > 0)
                {
                    str = str + Convert.ToString(bytes[0x4c + num3]) + ",";
                    string str6 = Encoding.Default.GetString(bytes, 0x4d + num3, 12).Trim();
                    str6 = CommonTool.GetStringFromBuffer(Encoding.GetEncoding("GBK").GetBytes(str6), 0, str6.Length);
                    while (str6.Length < 3)
                    {
                        str6 = "0" + str6;
                    }
                    string str15 = str;
                    str = str15 + str6.Substring(0, str6.Length - 2) + "." + str6.Substring(str6.Length - 2, 2) + ",";
                    str6 = Encoding.Default.GetString(bytes, 0x59 + num3, 12).Trim();
                    str6 = CommonTool.GetStringFromBuffer(Encoding.GetEncoding("GBK").GetBytes(str6), 0, str6.Length);
                    while (str6.Length < 3)
                    {
                        str6 = "0" + str6;
                    }
                    string str16 = str;
                    str = str16 + str6.Substring(0, str6.Length - 2) + "." + str6.Substring(str6.Length - 2, 2) + ",;";
                }
            }
            List<InvoiceAmountType> list2 = new List<InvoiceAmountType>();
            string[] strArray = str.Split(new char[] { ';' });
            for (int i = 0; i < strArray.Length; i++)
            {
                string[] strArray2 = strArray[i].Split(new char[] { ',' });
                if (strArray2.Length >= 3)
                {
                    InvoiceAmountType item = new InvoiceAmountType {
                        InvType = CommonTool.GetStringFromBuffer(Encoding.GetEncoding("GBK").GetBytes(strArray2[0]), 0, strArray2[0].Length),
                        KPMoney = CommonTool.GetStringFromBuffer(Encoding.GetEncoding("GBK").GetBytes(strArray2[1]), 0, strArray2[1].Length),
                        TPMoney = CommonTool.GetStringFromBuffer(Encoding.GetEncoding("GBK").GetBytes(strArray2[2]), 0, strArray2[2].Length)
                    };
                    list2.Add(item);
                }
            }
            return list2;
        }

        public uint GetInvNumber(InvoiceType invoiceType_1)
        {
            uint num = 0;
            this.GetInvStock();
            for (int i = 0; i < this.list_0.Count; i++)
            {
                if (this.list_0[i].InvType == ((byte) invoiceType_1))
                {
                    num += this.list_0[i].Number;
                }
            }
            return num;
        }

        public List<InvVolumeApp> GetInvStock()
        {
            if (this.method_4() == DrvState.dsOpen)
            {
                List<string> inList = new List<string>();
                byte[] array = this.method_64(13, 1, inList);
                Encoding.GetEncoding("GBK").GetBytes("JSP").CopyTo(array, 0x3e8);
                this.int_4 = this.method_63(array, this.byte_0);
                if (this.RetCode == 0)
                {
                    this.list_0.Clear();
                    this.method_23();
                    return this.list_0;
                }
            }
            return null;
        }

        public int GetInvStockCount(int int_7, int int_8)
        {
            List<string> inList = new List<string>();
            byte[] buffer = new byte[0x11800];
            inList.Clear();
            inList.Add(int_7.ToString());
            inList.Add(int_8.ToString());
            inList.Add("0");
            inList.Add("0");
            inList.Add("0");
            byte[] buffer2 = this.method_64(4, 0, inList);
            this.int_4 = this.method_63(buffer2, buffer);
            uint num = 0;
            if (this.int_4 == 0)
            {
                num = BitConverter.ToUInt32(buffer, 0);
                Class20.smethod_1("GetInvStockCount返回值：" + BitConverter.ToString(buffer, 0, 4));
            }
            //return (int) num;
            return 10000;
        }

        public List<InvVolume> GetInvStockMonthStat(int int_7, int int_8)
        {
            if (int_8 < this.LastRepDateMonth)
            {
                throw new ArgumentException("月份不能到上次抄税月之前");
            }
            SortedDictionary<string, InvVolume> dictionary = new SortedDictionary<string, InvVolume>();
            List<InvVolumeApp> volumns = CommonTool.GetVolumns();
            if (volumns.Count == 0)
            {
                this.GetInvStock();
                int count = this.list_0.Count;
                for (int n = 0; n < count; n++)
                {
                    volumns.Add(this.list_0[n]);
                }
            }
            new DateTime(int_7, int_8, 1);
            for (int i = 0; i < volumns.Count; i++)
            {
                InvVolumeApp app = volumns[i];
                InvVolume volume = new InvVolume {
                    InvType = (InvoiceType) volumns[i].InvType,
                    TypeCode = app.TypeCode
                };
                uint num4 = (app.HeadCode - Convert.ToUInt32(app.BuyNumber)) + Convert.ToUInt32(app.Number);
                volume.HeadCode = num4;
                volume.EndCode = app.HeadCode + Convert.ToUInt32(app.Number);
                int year = volumns[i].BuyDate.Year;
                int num6 = volumns[i].BuyDate.Month - ((int_7 - year) * 12);
                if (num6 <= int_8)
                {
                    if ((num6 >= int_8) && (num6 == int_8))
                    {
                        volume.PrdThisBuyNum = app.BuyNumber;
                        volume.PrdThisBuyNO = app.HeadCode.ToString("00000000");
                    }
                    string key = string.Concat(new object[] { volumns[i].InvType, "#", volume.TypeCode, "@", num4.ToString().PadLeft(8, '0') });
                    if (!dictionary.ContainsKey(key))
                    {
                        dictionary.Add(key, volume);
                    }
                }
            }
            for (int j = 0; j < volumns.Count; j++)
            {
                InvVolumeApp app2 = volumns[j];
                uint num8 = (app2.HeadCode - Convert.ToUInt32(app2.BuyNumber)) + Convert.ToUInt32(app2.Number);
                string str2 = string.Concat(new object[] { app2.InvType, app2.TypeCode, "|", num8 });
                if (dictionary.ContainsKey(str2))
                {
                    InvVolume volume2 = dictionary[str2];
                    volume2.PrdEndStockNO = app2.HeadCode.ToString().PadLeft(8, '0');
                    volume2.PrdEndStockNum = app2.Number;
                    long num13 = app2.HeadCode - (app2.BuyNumber - volume2.PrdEndStockNum);
                    volume2.PrdEarlyStockNO = num13.ToString().PadLeft(8, '0');
                    volume2.PrdThisBuyNum = app2.BuyNumber;
                    volume2.PrdThisBuyNO = volume2.PrdEarlyStockNO;
                    if (volume2.PrdThisBuyNum > 0)
                    {
                        volume2.PrdEarlyStockNO = "";
                    }
                }
                else
                {
                    InvVolume volume3 = new InvVolume {
                        InvType = (InvoiceType) app2.InvType,
                        TypeCode = app2.TypeCode,
                        HeadCode = (app2.HeadCode - Convert.ToUInt32(app2.BuyNumber)) + Convert.ToUInt32(app2.Number),
                        EndCode = app2.HeadCode + Convert.ToUInt32(app2.Number),
                        PrdEndStockNO = app2.HeadCode.ToString().PadLeft(8, '0'),
                        PrdEndStockNum = app2.Number
                    };
                    long num14 = app2.HeadCode - (app2.BuyNumber - volume3.PrdEndStockNum);
                    volume3.PrdEarlyStockNO = num14.ToString().PadLeft(8, '0');
                    volume3.PrdThisBuyNum = app2.BuyNumber;
                    volume3.PrdThisBuyNO = volume3.PrdEarlyStockNO;
                    if (volume3.PrdThisBuyNum > 0)
                    {
                        volume3.PrdEarlyStockNO = "";
                    }
                    dictionary.Add(str2, volume3);
                }
            }
            List<InvVolume> list2 = new List<InvVolume>();
            foreach (KeyValuePair<string, InvVolume> pair in dictionary)
            {
                InvVolume volume4 = pair.Value;
                list2.Add(volume4);
            }
            long invCount = this.GetInvCount(int_7, int_8);
            for (int k = 0; k < invCount; k++)
            {
                InvVolume volume5;
                InvDetail detail = this.method_19((long) k);
                int num11 = 0;
                while (num11 < list2.Count)
                {
                    if (((detail.InvType == ((ushort) list2[num11].InvType)) && (detail.TypeCode == list2[num11].TypeCode)) && ((detail.InvNo >= list2[num11].HeadCode) && (detail.InvNo < list2[num11].EndCode)))
                    {
                        goto Label_04D9;
                    }
                    num11++;
                }
                continue;
            Label_04D9:
                volume5 = list2[num11];
                if (detail.CancelFlag)
                {
                    volume5.WasteNum++;
                    volume5.WasteNO = volume5.WasteNO + detail.InvNo.ToString().PadLeft(8, '0') + " ";
                }
                if (volume5.PrdThisIssueNO.Length == 0)
                {
                    volume5.PrdThisIssueNO = detail.InvNo.ToString().PadLeft(8, '0');
                    if (volume5.PrdThisBuyNum == 0)
                    {
                        volume5.PrdEarlyStockNO = volume5.PrdThisIssueNO;
                    }
                }
                volume5.PrdThisIssueNum++;
                if (volume5.PrdThisBuyNum == 0)
                {
                    volume5.PrdEarlyStockNum++;
                }
            }
            InvVolume item = new InvVolume();
            string str3 = "";
            for (int m = 0; m < list2.Count; m++)
            {
                string str4 = ((int) list2[m].InvType) + list2[m].TypeCode;
                if (!(str3 == str4) && (str3.Length != 0))
                {
                    InvVolume volume7 = new InvVolume {
                        InvType = item.InvType,
                        TypeCode = item.TypeCode,
                        PrdEarlyStockNum = item.PrdEarlyStockNum,
                        PrdThisBuyNum = item.PrdThisBuyNum,
                        PrdThisIssueNum = item.PrdThisIssueNum,
                        WasteNum = item.WasteNum,
                        MistakeNum = item.MistakeNum,
                        PrdEndStockNum = item.PrdEndStockNum
                    };
                    item.PrdEarlyStockNum = 0;
                    item.PrdThisBuyNum = 0;
                    item.PrdThisIssueNum = 0;
                    item.WasteNum = 0;
                    item.MistakeNum = 0;
                    item.PrdEndStockNum = 0;
                    list2.Insert(m, volume7);
                }
                else
                {
                    item.InvType = list2[m].InvType;
                    item.TypeCode = "小计";
                    item.PrdEarlyStockNum += list2[m].PrdEarlyStockNum;
                    item.PrdThisBuyNum += list2[m].PrdThisBuyNum;
                    item.PrdThisIssueNum += list2[m].PrdThisIssueNum;
                    item.WasteNum += list2[m].WasteNum;
                    item.MistakeNum += list2[m].MistakeNum;
                    item.PrdEndStockNum += list2[m].PrdEndStockNum;
                }
                str3 = str4;
            }
            list2.Add(item);
            return list2;
        }

        public InvDetail GetLastInvDetailFormDev()
        {
            List<string> inList = new List<string> { "0", "0", "F" };
            byte[] buffer = this.method_64(4, 0, inList);
            this.int_4 = this.method_63(buffer, this.byte_0);
            return this.method_22(true);
        }

        public int GetMonth(DateTime dateTime_8, DateTime dateTime_9)
        {
            int num = 0;
            if ((dateTime_9.Year < dateTime_8.Year) || ((dateTime_9.Year == dateTime_8.Year) && (dateTime_9.Month < dateTime_8.Month)))
            {
                return -1;
            }
            if ((dateTime_9.Year - dateTime_8.Year) == 0)
            {
                num = dateTime_9.Month - dateTime_8.Month;
            }
            if (dateTime_9.Year <= dateTime_8.Year)
            {
                return num;
            }
            if (dateTime_9.Month < dateTime_8.Month)
            {
                return (((((dateTime_9.Year - dateTime_8.Year) - 1) * 12) + (12 - dateTime_8.Month)) + dateTime_9.Month);
            }
            return ((((dateTime_9.Year - dateTime_8.Year) * 12) + dateTime_9.Month) - dateTime_8.Month);
        }

        public TaxStatisData GetMonthStatistics(int int_7, int int_8, int int_9)
        {
            TaxStatisData data2;
            long num = 0L;
            try
            {
                int num13;
                Class20.smethod_1(string.Concat(new object[] { "月度统计资料 ", int_7, ",", int_8, ",", int_9 }));
                if ((int_8 > 12) || (int_8 < 1))
                {
                    throw new ArgumentException("月份不能大于12或小于1");
                }
                int sysYear = this.SysYear;
                TaxStatisData data = new TaxStatisData();
                int_9.ToString();
                long invCount = 0L;
                if (this.bool_1)
                {
                    Class20.smethod_3("大容量盘月统>>>>>>>>>>>>>>>>>>>>>>>");
                    int invStockCount = this.GetInvStockCount(int_7, int_8);
                    if ((this.int_4 == 0) && (invStockCount > 0))
                    {
                        Class20.smethod_1(string.Format("取出{0}年{1}月，共{2}块", int_7, int_8, invStockCount));
                        for (int j = 0; j < invStockCount; j++)
                        {
                            invCount = this.GetStockInvCount(int_7, int_8, j, j);
                            num += invCount;
                            if (this.int_4 == 0)
                            {
                                if (invCount <= 0L)
                                {
                                    break;
                                }
                                Class20.smethod_1(string.Format("取出{0}年{1}月第{2}块，共{3}张发票", new object[] { int_7, int_8, j, invCount }));
                                Class20.smethod_1(string.Concat(new object[] { "invCount： ", int_7, ",", int_8, ",", invCount.ToString() }));
                                for (int k = 0; k < invCount; k++)
                                {
                                    InvDetail detail = this.method_18((long) k);
                                    if (this.int_4 != 0)
                                    {
                                        return new TaxStatisData();
                                    }
                                    Class20.smethod_1((("获取发票对象：" + detail) == null) ? "发票对象为空" : "发票对象不为空");
                                    if (((this.ecardType_0 == ECardType.ectDefault) || (int_9 <= 0)) || (detail.InvRepPeriod == int_9))
                                    {
                                        Class20.smethod_1("发票对象的发票种类：" + detail.InvType.ToString());
                                        InvAmountTaxStati stati = data.InvTypeStatData(detail.InvType);
                                        if (stati == null)
                                        {
                                            Class20.smethod_1("InvAmountTaxStati对象为空");
                                        }
                                        else
                                        {
                                            Class20.smethod_1("InvAmountTaxStati对象不为空");
                                        }
                                        if (detail.Amount >= 0.0)
                                        {
                                            stati.PlusInvoiceNum++;
                                            if (detail.CancelFlag)
                                            {
                                                stati.PlusInvWasteNum++;
                                            }
                                        }
                                        else
                                        {
                                            stati.NegativeInvoiceNum++;
                                            if (detail.CancelFlag)
                                            {
                                                stati.NegativeInvWasteNum++;
                                            }
                                        }
                                        string str4 = string.Empty;
                                        if (Encoding.GetEncoding("GBK").GetString(detail.OldTypeCode, 0, 10) == "BarcodeKey")
                                        {
                                            str4 = Encoding.GetEncoding("GBK").GetString(detail.OldTypeCode, 0x20, detail.OldTypeCode.Length - 0x20);
                                        }
                                        else
                                        {
                                            str4 = Encoding.GetEncoding("GBK").GetString(detail.OldTypeCode);
                                        }
                                        if (!string.IsNullOrEmpty(str4))
                                        {
                                            string[] strArray8 = str4.Split(new string[] { "\n" }, StringSplitOptions.None);
                                            if ((strArray8 != null) && (strArray8.Length >= 2))
                                            {
                                                string str6 = strArray8[2];
                                                Class20.smethod_3(">>>>>：" + str6);
                                                int index = str6.IndexOf("V");
                                                if (index > 0)
                                                {
                                                    string str5 = str6.Substring(index + 1, (str6.Length - index) - 1);
                                                    if (!string.IsNullOrEmpty(str5))
                                                    {
                                                        string[] strArray4 = str5.TrimEnd(new char[] { ';' }).Split(new char[] { ';' });
                                                        if (strArray4.Length > 0)
                                                        {
                                                            foreach (string str7 in strArray4)
                                                            {
                                                                string[] strArray7 = str7.Split(new char[] { ',' });
                                                                double num11 = 0.0;
                                                                if ((strArray7[0] != null) && (strArray7[0] != ""))
                                                                {
                                                                    num11 = Convert.ToDouble(strArray7[0]);
                                                                }
                                                                num13 = Convert.ToInt32((double) (num11 * 1000.0));
                                                                if (num13 <= 60)
                                                                {
                                                                    if (num13 != 40)
                                                                    {
                                                                        if (num13 != 60)
                                                                        {
                                                                            goto Label_04BB;
                                                                        }
                                                                        this.method_34(detail, stati.TaxClass6, double.Parse(strArray7[1]), double.Parse(strArray7[2]));
                                                                    }
                                                                    else
                                                                    {
                                                                        this.method_34(detail, stati.TaxClass4, double.Parse(strArray7[1]), double.Parse(strArray7[2]));
                                                                    }
                                                                    continue;
                                                                }
                                                                switch (num13)
                                                                {
                                                                    case 130:
                                                                    {
                                                                        Class20.smethod_2(">>>>>0.3税档：" + Encoding.GetEncoding("GBK").GetString(detail.OldTypeCode).Substring(0, 100));
                                                                        this.method_34(detail, stati.AmountTax_1, double.Parse(strArray7[1]), double.Parse(strArray7[2]));
                                                                        continue;
                                                                    }
                                                                    case 170:
                                                                    {
                                                                        this.method_34(detail, stati.AmountTax_0, double.Parse(strArray7[1]), double.Parse(strArray7[2]));
                                                                        continue;
                                                                    }
                                                                }
                                                            Label_04BB:
                                                                this.method_34(detail, stati.TaxClassOther, double.Parse(strArray7[1]), double.Parse(strArray7[2]));
                                                            }
                                                        }
                                                        else
                                                        {
                                                            this.method_32(detail, stati, detail.TaxClass);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        this.method_32(detail, stati, detail.TaxClass);
                                                    }
                                                }
                                                else
                                                {
                                                    this.method_32(detail, stati, detail.TaxClass);
                                                }
                                                stati.Total.SJXSJE = (((stati.AmountTax_0.SJXSJE + stati.AmountTax_1.SJXSJE) + stati.TaxClass6.SJXSJE) + stati.TaxClass4.SJXSJE) + stati.TaxClassOther.SJXSJE;
                                                stati.Total.SJXXSE = (((stati.AmountTax_0.SJXXSE + stati.AmountTax_1.SJXXSE) + stati.TaxClass6.SJXXSE) + stati.TaxClass4.SJXXSE) + stati.TaxClassOther.SJXXSE;
                                                stati.Total.XXFFJE = (((stati.AmountTax_0.XXFFJE + stati.AmountTax_1.XXFFJE) + stati.TaxClass6.XXFFJE) + stati.TaxClass4.XXFFJE) + stati.TaxClassOther.XXFFJE;
                                                stati.Total.XXFFSE = (((stati.AmountTax_0.XXFFSE + stati.AmountTax_1.XXFFSE) + stati.TaxClass6.XXFFSE) + stati.TaxClass4.XXFFSE) + stati.TaxClassOther.XXFFSE;
                                                stati.Total.XXFSJE = (((stati.AmountTax_0.XXFSJE + stati.AmountTax_1.XXFSJE) + stati.TaxClass6.XXFSJE) + stati.TaxClass4.XXFSJE) + stati.TaxClassOther.XXFSJE;
                                                stati.Total.XXFSSE = (((stati.AmountTax_0.XXFSSE + stati.AmountTax_1.XXFSSE) + stati.TaxClass6.XXFSSE) + stati.TaxClass4.XXFSSE) + stati.TaxClassOther.XXFSSE;
                                                stati.Total.XXZFJE = (((stati.AmountTax_0.XXZFJE + stati.AmountTax_1.XXZFJE) + stati.TaxClass6.XXZFJE) + stati.TaxClass4.XXZFJE) + stati.TaxClassOther.XXZFJE;
                                                stati.Total.XXZFSE = (((stati.AmountTax_0.XXZFSE + stati.AmountTax_1.XXZFSE) + stati.TaxClass6.XXZFSE) + stati.TaxClass4.XXZFSE) + stati.TaxClassOther.XXZFSE;
                                                stati.Total.XXZSJE = (((stati.AmountTax_0.XXZSJE + stati.AmountTax_1.XXZSJE) + stati.TaxClass6.XXZSJE) + stati.TaxClass4.XXZSJE) + stati.TaxClassOther.XXZSJE;
                                                stati.Total.XXZSSE = (((stati.AmountTax_0.XXZSSE + stati.AmountTax_1.XXZSSE) + stati.TaxClass6.XXZSSE) + stati.TaxClass4.XXZSSE) + stati.TaxClassOther.XXZSSE;
                                            }
                                        }
                                    }
                                }
                                for (int m = 0; m < data.Count; m++)
                                {
                                    InvAmountTaxStati stati3 = data[m];
                                    Class20.smethod_2("票种：" + stati3.InvTypeStr + "的统计情况===============================");
                                    AmountTax taxClassOther = stati3.AmountTax_0;
                                    Class20.smethod_2(string.Format("TaxClass17：正数金额{0},正废金额{1},负数金额{2}，负废金额{3},正数税额{4},正废税额{5},负数税额{6}，负废税额{7},实际销售金额{8}，实际销售税额{9}", new object[] { taxClassOther.XXZSJE, taxClassOther.XXZFJE, taxClassOther.XXFSJE, taxClassOther.XXFFJE, taxClassOther.XXZSSE, taxClassOther.XXZFSE, taxClassOther.XXFSSE, taxClassOther.XXFFSE, taxClassOther.SJXSJE, taxClassOther.SJXXSE }));
                                    taxClassOther = stati3.AmountTax_1;
                                    Class20.smethod_2(string.Format("TaxClass13：正数金额{0},正废金额{1},负数金额{2}，负废金额{3},正数税额{4},正废税额{5},负数税额{6}，负废税额{7},实际销售金额{8}，实际销售税额{9}", new object[] { taxClassOther.XXZSJE, taxClassOther.XXZFJE, taxClassOther.XXFSJE, taxClassOther.XXFFJE, taxClassOther.XXZSSE, taxClassOther.XXZFSE, taxClassOther.XXFSSE, taxClassOther.XXFFSE, taxClassOther.SJXSJE, taxClassOther.SJXXSE }));
                                    taxClassOther = stati3.TaxClass6;
                                    Class20.smethod_2(string.Format("TaxClass6：正数金额{0},正废金额{1},负数金额{2}，负废金额{3},正数税额{4},正废税额{5},负数税额{6}，负废税额{7},实际销售金额{8}，实际销售税额{9}", new object[] { taxClassOther.XXZSJE, taxClassOther.XXZFJE, taxClassOther.XXFSJE, taxClassOther.XXFFJE, taxClassOther.XXZSSE, taxClassOther.XXZFSE, taxClassOther.XXFSSE, taxClassOther.XXFFSE, taxClassOther.SJXSJE, taxClassOther.SJXXSE }));
                                    taxClassOther = stati3.TaxClass4;
                                    Class20.smethod_2(string.Format("TaxClass4：正数金额{0},正废金额{1},负数金额{2}，负废金额{3},正数税额{4},正废税额{5},负数税额{6}，负废税额{7},实际销售金额{8}，实际销售税额{9}", new object[] { taxClassOther.XXZSJE, taxClassOther.XXZFJE, taxClassOther.XXFSJE, taxClassOther.XXFFJE, taxClassOther.XXZSSE, taxClassOther.XXZFSE, taxClassOther.XXFSSE, taxClassOther.XXFFSE, taxClassOther.SJXSJE, taxClassOther.SJXXSE }));
                                    taxClassOther = stati3.TaxClassOther;
                                    Class20.smethod_2(string.Format("TaxClassOther：正数金额{0},正废金额{1},负数金额{2}，负废金额{3},正数税额{4},正废税额{5},负数税额{6}，负废税额{7},实际销售金额{8}，实际销售税额{9}", new object[] { taxClassOther.XXZSJE, taxClassOther.XXZFJE, taxClassOther.XXFSJE, taxClassOther.XXFFJE, taxClassOther.XXZSSE, taxClassOther.XXZFSE, taxClassOther.XXFSSE, taxClassOther.XXFFSE, taxClassOther.SJXSJE, taxClassOther.SJXXSE }));
                                }
                            }
                        }
                        this.ClearInvStore();
                    }
                    Class20.smethod_3(string.Format("===============大盘修复，年{0}，月{1}，总块数{2}，总张数{3}", new object[] { int_7, int_8, invStockCount, num }));
                }
                else
                {
                    Class20.smethod_3("小容量盘月统>>>>>>>>>>>>>>>>>>>>>>>");
                    invCount = this.GetInvCount(int_7, int_8);
                    num += invCount;
                    Class20.smethod_3(string.Format("===============小容量修复，年{0}，月{1}，发票总数{2}", int_7, int_8, num));
                    Class20.smethod_1(string.Concat(new object[] { "invCount： ", int_7, ",", int_8, ",", invCount.ToString() }));
                    for (int n = 0; n < invCount; n++)
                    {
                        InvDetail detail2 = this.method_18((long) n);
                        if (this.int_4 != 0)
                        {
                            goto Label_19DB;
                        }
                        Class20.smethod_1((("获取发票对象：" + detail2) == null) ? "发票对象为空" : "发票对象不为空");
                        if (((this.ecardType_0 == ECardType.ectDefault) || (int_9 <= 0)) || (detail2.InvRepPeriod == int_9))
                        {
                            Class20.smethod_1("发票对象的发票种类：" + detail2.InvType.ToString());
                            InvAmountTaxStati stati2 = data.InvTypeStatData(detail2.InvType);
                            Class20.smethod_3(">>>>>发票修复：" + Encoding.GetEncoding("GBK").GetString(detail2.OldTypeCode).Substring(0, 100));
                            if (stati2 == null)
                            {
                                Class20.smethod_1("InvAmountTaxStati对象为空");
                            }
                            else
                            {
                                Class20.smethod_1("InvAmountTaxStati对象不为空");
                            }
                            if (detail2.Amount >= 0.0)
                            {
                                stati2.PlusInvoiceNum++;
                                if (detail2.CancelFlag)
                                {
                                    stati2.PlusInvWasteNum++;
                                }
                            }
                            else
                            {
                                stati2.NegativeInvoiceNum++;
                                if (detail2.CancelFlag)
                                {
                                    stati2.NegativeInvWasteNum++;
                                }
                            }
                            string str2 = string.Empty;
                            if (Encoding.GetEncoding("GBK").GetString(detail2.OldTypeCode, 0, 10) == "BarcodeKey")
                            {
                                str2 = Encoding.GetEncoding("GBK").GetString(detail2.OldTypeCode, 0x20, detail2.OldTypeCode.Length - 0x20);
                            }
                            else
                            {
                                str2 = Encoding.GetEncoding("GBK").GetString(detail2.OldTypeCode);
                            }
                            if (!string.IsNullOrEmpty(str2))
                            {
                                string[] strArray3 = str2.Split(new string[] { "\n" }, StringSplitOptions.None);
                                if ((strArray3 != null) && (strArray3.Length >= 2))
                                {
                                    string str3 = strArray3[2];
                                    Class20.smethod_3(">>>>>slvStr：" + str3);
                                    int num4 = str3.IndexOf("V");
                                    if (num4 > 0)
                                    {
                                        string str = str3.Substring(num4 + 1, (str3.Length - num4) - 1);
                                        if (!string.IsNullOrEmpty(str))
                                        {
                                            string[] strArray = str.TrimEnd(new char[] { ';' }).Split(new char[] { ';' });
                                            if (strArray.Length > 0)
                                            {
                                                Class20.smethod_3(">>>>>发票修复1：" + str);
                                                foreach (string str8 in strArray)
                                                {
                                                    string[] strArray6 = str8.Split(new char[] { ',' });
                                                    double num16 = 0.0;
                                                    if ((strArray6[0] != null) && (strArray6[0] != ""))
                                                    {
                                                        num16 = Convert.ToDouble(strArray6[0]);
                                                    }
                                                    num13 = Convert.ToInt32((double) (num16 * 1000.0));
                                                    if (num13 <= 60)
                                                    {
                                                        if (num13 != 40)
                                                        {
                                                            if (num13 != 60)
                                                            {
                                                                goto Label_10EC;
                                                            }
                                                            this.method_34(detail2, stati2.TaxClass6, double.Parse(strArray6[1]), double.Parse(strArray6[2]));
                                                        }
                                                        else
                                                        {
                                                            this.method_34(detail2, stati2.TaxClass4, double.Parse(strArray6[1]), double.Parse(strArray6[2]));
                                                        }
                                                        continue;
                                                    }
                                                    switch (num13)
                                                    {
                                                        case 130:
                                                        {
                                                            Class20.smethod_3(">>>>>0.13税档：" + Encoding.GetEncoding("GBK").GetString(detail2.OldTypeCode).Substring(0, 100));
                                                            this.method_34(detail2, stati2.AmountTax_1, double.Parse(strArray6[1]), double.Parse(strArray6[2]));
                                                            continue;
                                                        }
                                                        case 170:
                                                        {
                                                            this.method_34(detail2, stati2.AmountTax_0, double.Parse(strArray6[1]), double.Parse(strArray6[2]));
                                                            continue;
                                                        }
                                                    }
                                                Label_10EC:
                                                    this.method_34(detail2, stati2.TaxClassOther, double.Parse(strArray6[1]), double.Parse(strArray6[2]));
                                                }
                                            }
                                            else
                                            {
                                                Class20.smethod_3(">>>>>发票修复2：" + detail2.TaxClass.ToString());
                                                this.method_32(detail2, stati2, detail2.TaxClass);
                                            }
                                        }
                                        else
                                        {
                                            Class20.smethod_3(">>>>>发票修复3：" + detail2.TaxClass.ToString());
                                            this.method_32(detail2, stati2, detail2.TaxClass);
                                        }
                                    }
                                    else
                                    {
                                        Class20.smethod_3(">>>>>发票修复4：" + detail2.TaxClass.ToString());
                                        this.method_32(detail2, stati2, detail2.TaxClass);
                                    }
                                    stati2.Total.SJXSJE = (((stati2.AmountTax_0.SJXSJE + stati2.AmountTax_1.SJXSJE) + stati2.TaxClass6.SJXSJE) + stati2.TaxClass4.SJXSJE) + stati2.TaxClassOther.SJXSJE;
                                    stati2.Total.SJXXSE = (((stati2.AmountTax_0.SJXXSE + stati2.AmountTax_1.SJXXSE) + stati2.TaxClass6.SJXXSE) + stati2.TaxClass4.SJXXSE) + stati2.TaxClassOther.SJXXSE;
                                    stati2.Total.XXFFJE = (((stati2.AmountTax_0.XXFFJE + stati2.AmountTax_1.XXFFJE) + stati2.TaxClass6.XXFFJE) + stati2.TaxClass4.XXFFJE) + stati2.TaxClassOther.XXFFJE;
                                    stati2.Total.XXFFSE = (((stati2.AmountTax_0.XXFFSE + stati2.AmountTax_1.XXFFSE) + stati2.TaxClass6.XXFFSE) + stati2.TaxClass4.XXFFSE) + stati2.TaxClassOther.XXFFSE;
                                    stati2.Total.XXFSJE = (((stati2.AmountTax_0.XXFSJE + stati2.AmountTax_1.XXFSJE) + stati2.TaxClass6.XXFSJE) + stati2.TaxClass4.XXFSJE) + stati2.TaxClassOther.XXFSJE;
                                    stati2.Total.XXFSSE = (((stati2.AmountTax_0.XXFSSE + stati2.AmountTax_1.XXFSSE) + stati2.TaxClass6.XXFSSE) + stati2.TaxClass4.XXFSSE) + stati2.TaxClassOther.XXFSSE;
                                    stati2.Total.XXZFJE = (((stati2.AmountTax_0.XXZFJE + stati2.AmountTax_1.XXZFJE) + stati2.TaxClass6.XXZFJE) + stati2.TaxClass4.XXZFJE) + stati2.TaxClassOther.XXZFJE;
                                    stati2.Total.XXZFSE = (((stati2.AmountTax_0.XXZFSE + stati2.AmountTax_1.XXZFSE) + stati2.TaxClass6.XXZFSE) + stati2.TaxClass4.XXZFSE) + stati2.TaxClassOther.XXZFSE;
                                    stati2.Total.XXZSJE = (((stati2.AmountTax_0.XXZSJE + stati2.AmountTax_1.XXZSJE) + stati2.TaxClass6.XXZSJE) + stati2.TaxClass4.XXZSJE) + stati2.TaxClassOther.XXZSJE;
                                    stati2.Total.XXZSSE = (((stati2.AmountTax_0.XXZSSE + stati2.AmountTax_1.XXZSSE) + stati2.TaxClass6.XXZSSE) + stati2.TaxClass4.XXZSSE) + stati2.TaxClassOther.XXZSSE;
                                }
                            }
                        }
                    }
                    for (int num3 = 0; num3 < data.Count; num3++)
                    {
                        InvAmountTaxStati stati4 = data[num3];
                        Class20.smethod_3("票种：" + stati4.InvTypeStr + "的统计情况===============================");
                        AmountTax tax2 = stati4.AmountTax_0;
                        Class20.smethod_3(string.Format("TaxClass17：正数金额{0},正废金额{1},负数金额{2}，负废金额{3},正数税额{4},正废税额{5},负数税额{6}，负废税额{7},实际销售金额{8}，实际销售税额{9}", new object[] { tax2.XXZSJE, tax2.XXZFJE, tax2.XXFSJE, tax2.XXFFJE, tax2.XXZSSE, tax2.XXZFSE, tax2.XXFSSE, tax2.XXFFSE, tax2.SJXSJE, tax2.SJXXSE }));
                        tax2 = stati4.AmountTax_1;
                        Class20.smethod_3(string.Format("TaxClass13：正数金额{0},正废金额{1},负数金额{2}，负废金额{3},正数税额{4},正废税额{5},负数税额{6}，负废税额{7},实际销售金额{8}，实际销售税额{9}", new object[] { tax2.XXZSJE, tax2.XXZFJE, tax2.XXFSJE, tax2.XXFFJE, tax2.XXZSSE, tax2.XXZFSE, tax2.XXFSSE, tax2.XXFFSE, tax2.SJXSJE, tax2.SJXXSE }));
                        tax2 = stati4.TaxClass6;
                        Class20.smethod_3(string.Format("TaxClass6：正数金额{0},正废金额{1},负数金额{2}，负废金额{3},正数税额{4},正废税额{5},负数税额{6}，负废税额{7},实际销售金额{8}，实际销售税额{9}", new object[] { tax2.XXZSJE, tax2.XXZFJE, tax2.XXFSJE, tax2.XXFFJE, tax2.XXZSSE, tax2.XXZFSE, tax2.XXFSSE, tax2.XXFFSE, tax2.SJXSJE, tax2.SJXXSE }));
                        tax2 = stati4.TaxClass4;
                        Class20.smethod_3(string.Format("TaxClass4：正数金额{0},正废金额{1},负数金额{2}，负废金额{3},正数税额{4},正废税额{5},负数税额{6}，负废税额{7},实际销售金额{8}，实际销售税额{9}", new object[] { tax2.XXZSJE, tax2.XXZFJE, tax2.XXFSJE, tax2.XXFFJE, tax2.XXZSSE, tax2.XXZFSE, tax2.XXFSSE, tax2.XXFFSE, tax2.SJXSJE, tax2.SJXXSE }));
                        tax2 = stati4.TaxClassOther;
                        Class20.smethod_3(string.Format("TaxClassOther：正数金额{0},正废金额{1},负数金额{2}，负废金额{3},正数税额{4},正废税额{5},负数税额{6}，负废税额{7},实际销售金额{8}，实际销售税额{9}", new object[] { tax2.XXZSJE, tax2.XXZFJE, tax2.XXFSJE, tax2.XXFFJE, tax2.XXZSSE, tax2.XXZFSE, tax2.XXFSSE, tax2.XXFFSE, tax2.SJXSJE, tax2.SJXXSE }));
                    }
                }
                TaxStatisData data3 = this.AcqTaxHistStaticsInfo(int_8, int_7);
                for (int i = 0; i < data.Count; i++)
                {
                    data[i].PeriodEarlyStockNum = data3[i].PeriodEarlyStockNum;
                    data[i].BuyNum = data3[i].BuyNum;
                    InvAmountTaxStati stati1 = data[i];
                    stati1.ReturnInvNum += data3[i].ReturnInvNum;
                    InvAmountTaxStati stati5 = data[i];
                    stati5.AllotInvNum += data3[i].AllotInvNum;
                    InvAmountTaxStati stati6 = data[i];
                    stati6.ReclaimStockNum += data3[i].ReclaimStockNum;
                    data[i].PeriodEndStockNum = data3[i].PeriodEndStockNum;
                }
                Class20.smethod_1("月统完成>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>.");
                return data;
            Label_19DB:
                data2 = new TaxStatisData();
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
                throw;
            }
            return data2;
        }

        public List<int> GetMonthStatPeriod(int int_7)
        {
            List<int> list = new List<int>();
            list.Clear();
            for (int i = 1; i < 13; i++)
            {
                switch (this.method_31(i, int_7))
                {
                    case 0:
                        list.Add(i);
                        break;

                    case 0x53:
                        goto Label_0033;
                }
            }
        Label_0033:
            Class20.smethod_1("GetMonth:" + list.Count.ToString());
            return list;
        }

        public OfflineInvAmount GetOfflineInvAmout(InvoiceType invoiceType_1)
        {
            List<string> inList = new List<string>();
            OfflineInvAmount amount = new OfflineInvAmount();
            inList.Add("w");
            inList.Add("1");
            byte[] buffer = this.method_64(0x15, 0, inList);
            buffer[0x3e8] = Convert.ToByte((int) invoiceType_1);
            byte[] buffer2 = new byte[0x11800];
            this.int_4 = this.method_63(buffer, buffer2);
            if (this.int_4 == 0)
            {
                ulong num = BitConverter.ToUInt64(buffer2, 2) % ((ulong) 0x1000000000000L);
                string str = num.ToString();
                while (str.Length < 3)
                {
                    str = "0" + str;
                }
                amount.InvTotalAmount = Convert.ToDouble(str.Substring(0, str.Length - 2) + "." + str.Substring(str.Length - 2, 2));
                str = (BitConverter.ToUInt64(buffer2, 8) % ((ulong) 0x1000000000000L)).ToString();
                while (str.Length < 3)
                {
                    str = "0" + str;
                }
                amount.InvAmount = Convert.ToDouble(str.Substring(0, str.Length - 2) + "." + str.Substring(str.Length - 2, 2));
            }
            return amount;
        }

        public long GetOffLineInvCount(int int_7, int int_8)
        {
            this.long_2 = 0L;
            List<string> inList = new List<string> {
                int_7.ToString(),
                int_8.ToString(),
                "0",
                "0"
            };
            byte[] buffer = this.method_64(4, 0, inList);
            this.int_4 = this.method_63(buffer, this.byte_0);
            long num = 0L;
            if (this.int_4 == 0)
            {
                num = BitConverter.ToUInt32(this.byte_0, 0);
                Class20.smethod_1(string.Format("取金税设备中月份的离线开票份数:2字节值{0}，四字节值{1}", BitConverter.ToUInt16(this.byte_0, 0), BitConverter.ToUInt32(this.byte_0, 0)));
                if (num == 0xffffffffL)
                {
                    num = -1L;
                }
                this.long_2 = num;
            }
            return num;
        }

        public InvDetail GetOffLineInvDetial(int int_7, int int_8, int int_9)
        {
            this.method_42();
            List<string> inList = new List<string> {
                int_7.ToString(),
                int_8.ToString(),
                int_9.ToString(),
                "1"
            };
            byte[] buffer = this.method_64(4, 0, inList);
            this.int_4 = this.method_63(buffer, this.byte_0);
            return this.method_22(true);
        }

        public List<int> GetPeriodCount(int int_7)
        {
            List<int> list = new List<int>();
            List<string> inList = new List<string> { "9", "10" };
            byte[] buffer = this.method_64(0x15, 0, inList);
            if ((this.SQInfo.DHYBZ != "") && ((this.SQInfo.DHYBZ == "Z") || (this.SQInfo.DHYBZ == "Y")))
            {
                buffer[0x3e8] = Convert.ToByte(int_7);
            }
            this.int_4 = this.method_63(buffer, this.byte_0);
            int item = 0;
            int num2 = 0;
            if (this.RetCode == 0)
            {
                int index = BitConverter.ToUInt16(this.byte_0, 0);
                item = Convert.ToInt32(this.byte_0[index]);
                num2 = Convert.ToInt32(this.byte_0[index + 1]);
            }
            list.Add(item);
            list.Add(num2);
            this.int_1 = item;
            this.int_3 = num2;
            return list;
        }

        public string GetPeriodCount(int int_7, out List<int> list_1, int int_8 = 0)
        {
            list_1 = this.GetPeriodCount(int_7);
            return string.Concat(new object[] { "TCD_", this.int_4, "_", this.int_5 });
        }

        public TaxStateInfo GetStateInfo(bool bool_10 = false)
        {
            try
            {
                return this.method_28();
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
                return null;
            }
        }

        public int GetStockInvCount(int int_7, int int_8, int int_9, int int_10)
        {
            List<string> inList = new List<string>();
            byte[] buffer = new byte[0x11800];
            inList.Clear();
            inList.Add(int_7.ToString());
            inList.Add(int_8.ToString());
            inList.Add("1");
            inList.Add(int_9.ToString());
            inList.Add(int_10.ToString());
            byte[] buffer2 = this.method_64(4, 0, inList);
            this.int_4 = this.method_63(buffer2, buffer);
            uint num = 0;
            if (this.int_4 == 0)
            {
                num = BitConverter.ToUInt32(buffer, 0);
                this.long_1 = num;
                Class20.smethod_1("GetStockInvCount返回值：" + BitConverter.ToString(buffer, 0, 4));
            }
            return (int) num;
        }

        public TaxStatisData GetYearStatistics(int int_7, int int_8, int int_9)
        {
            if (int_8 > int_9)
            {
                throw new ArgumentException("起始月份大于结束月份");
            }
            int num6 = int_7;
            int num2 = int_8;
            TaxStatisData data2 = new TaxStatisData();
            while (num2 <= int_9)
            {
                List<TaxRepCommInfo> list = this.method_29(num2, num6);
                TaxStatisData data = this.AcqTaxHistStaticsInfo(num2, num6);
                for (int i = 0; i < data.Count; i++)
                {
                    Class20.smethod_1("统计发票：" + data[i].InvTypeStr);
                    if (num2 == int_8)
                    {
                        data2[i].PeriodEarlyStockNum = data[i].PeriodEarlyStockNum;
                    }
                    if (num2 == int_9)
                    {
                        data2[i].PeriodEndStockNum = data[i].PeriodEndStockNum;
                    }
                    InvAmountTaxStati stati1 = data2[i];
                    stati1.BuyNum += data[i].BuyNum;
                    InvAmountTaxStati stati2 = data2[i];
                    stati2.ReturnInvNum += data[i].ReturnInvNum;
                    InvAmountTaxStati stati3 = data2[i];
                    stati3.AllotInvNum += data[i].AllotInvNum;
                    InvAmountTaxStati stati4 = data2[i];
                    stati4.ReclaimStockNum += data[i].ReclaimStockNum;
                    int num4 = -1;
                    switch (i)
                    {
                        case 0:
                            num4 = 0;
                            break;

                        case 1:
                            num4 = 2;
                            break;

                        case 2:
                            num4 = 11;
                            break;

                        case 3:
                            num4 = 12;
                            break;

                        case 4:
                            num4 = 0x33;
                            break;

                        case 5:
                            num4 = 0x29;
                            break;
                    }
                    Class20.smethod_1("票种：" + num4.ToString());
                    int num3 = 0;
                    while (num3 < list.Count)
                    {
                        if (num4 == list[num3].InvType)
                        {
                            goto Label_0198;
                        }
                        num3++;
                    }
                    continue;
                Label_0198:
                    Class20.smethod_1("合并票种：" + list[num3].InvType.ToString());
                    TaxRepCommInfo info = list[num3];
                    AmountTax total = data2[i].Total;
                    total.XXZFJE += info.InvWasteAmount;
                    AmountTax tax2 = data2[i].Total;
                    tax2.XXZFSE += info.InvWasteTaxAmount;
                    InvAmountTaxStati stati5 = data2[i];
                    stati5.PlusInvWasteNum += info.InvWasteCount;
                    AmountTax tax3 = data2[i].Total;
                    tax3.XXZSJE += info.InvAmount;
                    AmountTax tax4 = data2[i].Total;
                    tax4.XXZSSE += info.InvTaxAmount;
                    InvAmountTaxStati stati6 = data2[i];
                    stati6.PlusInvoiceNum += info.InvCount + info.InvWasteCount;
                    AmountTax tax5 = data2[i].Total;
                    tax5.XXFFJE += info.NavInvWasteAmount;
                    AmountTax tax6 = data2[i].Total;
                    tax6.XXFFSE += info.NavInvWasteTaxAmount;
                    InvAmountTaxStati stati7 = data2[i];
                    stati7.NegativeInvWasteNum += info.NavInvWasteCount;
                    InvAmountTaxStati stati8 = data2[i];
                    stati8.NegativeInvoiceNum += info.NavInvCount + info.NavInvWasteCount;
                    AmountTax tax7 = data2[i].Total;
                    tax7.XXFSJE += info.NavInvAmount;
                    AmountTax tax8 = data2[i].Total;
                    tax8.XXFSSE += info.NavInvTaxAmount;
                    data2[i].Total.SJXSJE = data2[i].Total.XXZSJE - data2[i].Total.XXFSJE;
                    data2[i].Total.SJXXSE = data2[i].Total.XXZSSE - data2[i].Total.XXFSSE;
                }
                num2++;
            }
            return data2;
        }

        public bool InvAllot(int int_7, string string_35, int int_8, string string_36)
        {
            InvVolumeApp app = this.list_0[int_7];
            List<string> inList = new List<string>();
            if (string_35.Length == 0)
            {
                inList.Clear();
                inList.Add(int_7.ToString());
                for (int i = 0; i < 6; i++)
                {
                    inList.Add("000");
                }
                byte[] buffer2 = this.method_64(5, 0, inList);
                this.int_4 = this.method_63(buffer2, this.byte_0);
                return (this.RetCode == 0);
            }
            TimeSpan span = (TimeSpan) (DateTime.Now - this.dateTime_7);
            if (span.TotalSeconds < 60.0)
            {
                this.int_4 = 0x231;
                return false;
            }
            inList.Clear();
            if ((this.ctaxCardType_0 < CTaxCardType.tctCommonInv) && (this.method_4() != DrvState.dsInvAllot))
            {
                byte[] buffer = this.method_64(0x13, 1, inList);
                this.int_4 = this.method_63(buffer, this.byte_0);
            }
            else if ((this.ctaxCardType_0 < CTaxCardType.tctCommonInv) && (this.method_4() == DrvState.dsInvAllot))
            {
                inList.Clear();
                inList.Add(string_36);
                inList.Add("50");
                inList.Add("0");
                inList.Add("1");
            }
            else
            {
                inList.Add(string_36);
                inList.Add(int_7.ToString());
                inList.Add(string_35);
                inList.Add(int_8.ToString());
            }
            this.dateTime_7 = DateTime.Now;
            MessageShow.MsgWait("正在向分开票机分配发票 ...");
            byte[] buffer3 = this.method_64(0x13, 0, inList);
            this.int_4 = this.method_63(buffer3, this.byte_0);
            MessageShow.MsgWait();
            if ((this.RetCode == 0) && (inList.Count == 3))
            {
                this.taxStateInfo_0.IsInvEmpty = BitConverter.ToUInt16(this.byte_0, 0);
                CommonTool.smethod_8(app, string_35, int_8);
            }
            return (this.RetCode == 0);
        }

        public bool InvDetailClose()
        {
            byte[] buffer = new byte[4];
            buffer[0] = 0x99;
            byte[] buffer2 = buffer;
            this.method_63(buffer2, this.byte_0);
            return true;
        }

        public bool InvDetailOpen()
        {
            byte[] buffer = new byte[4];
            buffer[0] = 0x99;
            buffer[1] = 1;
            byte[] buffer2 = buffer;
            this.method_63(buffer2, this.byte_0);
            return true;
        }

        public InvoiceResult Invoice(string string_35, double double_0, double double_1, double double_2, string string_36, string string_37, byte[] byte_1, InvoiceType invoiceType_1, byte[] byte_2, string string_38, string string_39, object[] object_3)
        {
            Class20.smethod_1(string.Format("=============================================================金税设备开具发票开始", new object[0]));
            object[] args = new object[] { string_35, double_0.ToString(), double_1.ToString(), double_2.ToString(), string_36, string_37, (byte_1 != null) ? Encoding.GetEncoding("GBK").GetString(byte_1) : "null", ((int) invoiceType_1).ToString(), (byte_2 != null) ? Encoding.GetEncoding("GBK").GetString(byte_2) : "null", string_38, string_39 };
            Class20.smethod_1(string.Format("金税设备开具发票:购方税号{0},金额{1},税率{2},税额{3},发票明细串{4},红票通知单编号{5},汉字防伪加密串{6},发票种类{7},签名{8},密文{9}，校验码{10}", args));
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            InvoiceResult result = new InvoiceResult();
            string str = "";
            int length = string_37.Length;
            if (length < 0x10)
            {
                str = string_37;
            }
            else
            {
                int num12 = string_37.Length;
                str = string_37.Substring(num12 - 3, 3);
            }
            string item = "";
            byte[] bytes = Encoding.GetEncoding("GBK").GetBytes(string_36);
            if ((((byte_1 != null) && (byte_1.Length == 0x20)) && ((Encoding.Default.GetString(byte_1, 0, 10) == "BarcodeKey") && (invoiceType_1 != InvoiceType.transportation))) && (invoiceType_1 != InvoiceType.vehiclesales))
            {
                bytes = CommonTool.ByteArrayMerge(byte_1, bytes);
            }
            if (bytes.Length > 20)
            {
                if (length < 0x10)
                {
                    item = "0000000000";
                }
                else
                {
                    item = string_37.Substring(0, 0x10);
                }
            }
            else
            {
                item = string_36;
            }
            List<string> inList = new List<string> {
                this.method_17(false, double_0 < 0.0, double_1).ToString(),
                double_1.ToString("0.00"),
                str
            };
            if (this.ctaxCardType_0 != CTaxCardType.tctFuelTax)
            {
                string str2 = "";
                int num3 = string_35.Length;
                for (byte i = 0; i < string_35.Length; i = (byte) (i + 1))
                {
                    byte num10 = (byte) string_35[i];
                    num10 = (byte) (num10 ^ 0xc1);
                    num10 = (byte) (num10 ^ ((byte) (i % 0x10)));
                    if (i < (num3 - 3))
                    {
                        num10 = (byte) (num10 ^ ((byte) string_35[(num3 - 1) - (i % 3)]));
                    }
                    str2 = str2 + ((char) ((num10 >> 4) + 0x61)) + ((char) ((num10 & 15) + 0x6b));
                }
                inList.Add(str2);
            }
            else
            {
                inList.Add(string_35);
            }
            inList.Add((double_2 < 0.0) ? Math.Abs(double_2).ToString() : double_2.ToString());
            inList.Add((double_0 < 0.0) ? Math.Abs(double_0).ToString() : double_0.ToString());
            inList.Add(item);
            if (this.ctaxCardType_0 >= CTaxCardType.tctCommonInv)
            {
                inList.Add(((int) invoiceType_1).ToString());
            }
            if (invoiceType_1 == InvoiceType.transportation)
            {
                inList.Add("0");
            }
            else if (invoiceType_1 == InvoiceType.vehiclesales)
            {
                string str3 = string_36;
                for (int j = 0; j < 3; j++)
                {
                    str3 = str3.Substring(str3.IndexOf("\n") + 1);
                }
                str3 = str3.Substring(0, str3.IndexOf("\n")).Replace(" ", "[");
                if (str3.Length > 0x11)
                {
                    str3 = str3.Substring(0, 0x11);
                }
                Class20.smethod_1("车架号：" + str3);
                inList.Add(str3);
            }
            else
            {
                inList.Add("0");
            }
            string str5 = "0";
            if ((((object_3 != null) && (object_3.Length > 0)) && ((object_3[0] != null) && !string.IsNullOrEmpty(object_3[0].ToString()))) && (object_3[0].ToString().ToUpper() == "NCPSG"))
            {
                str5 = "ncp";
            }
            inList.Add(str5);
            byte[] buffer2 = this.method_64(5, 0, inList);
            Class20.smethod_1("开票参数：" + BitConverter.ToString(buffer2, 0, 200));
            int num14 = bytes.Length;
            BitConverter.GetBytes((uint) num14).CopyTo(buffer2, 0x3e8);
            bytes.CopyTo(buffer2, 0x3ec);
            Encoding.GetEncoding("GBK").GetBytes("sign").CopyTo(buffer2, (int) (0x3ec + num14));
            BitConverter.GetBytes((ushort) byte_2.Length).CopyTo(buffer2, (int) ((0x3ec + num14) + 4));
            byte_2.CopyTo(buffer2, (int) ((0x3ec + num14) + 6));
            byte[] buffer6 = Encoding.GetEncoding("GBK").GetBytes(string_38);
            BitConverter.GetBytes((ushort) buffer6.Length).CopyTo(buffer2, (int) (((0x3ec + num14) + byte_2.Length) + 6));
            buffer6.CopyTo(buffer2, (int) (((0x3ec + num14) + byte_2.Length) + 8));
            byte[] buffer8 = Encoding.GetEncoding("GBK").GetBytes(string_39);
            BitConverter.GetBytes((ushort) buffer8.Length).CopyTo(buffer2, (int) ((((0x3ec + num14) + byte_2.Length) + buffer6.Length) + 8));
            buffer8.CopyTo(buffer2, (int) ((((0x3ec + num14) + byte_2.Length) + buffer6.Length) + 10));
            Class20.smethod_1(BitConverter.ToString(buffer2, 0x3e8, 0x3e8));
            Class20.smethod_1(string.Format("开票传入参数，调用接口前：代码{0},号码{1},密文{2},校验码{3}", new object[] { this.invCodeNum_0.InvTypeCode.ToString(), this.invCodeNum_0.InvNum.ToString(), string_38, string_39 }));
            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            this.int_4 = this.method_63(buffer2, this.byte_0);
            stopwatch2.Stop();
            long elapsedMilliseconds = stopwatch2.ElapsedMilliseconds;
            this.int_0++;
            this.long_0 += elapsedMilliseconds;
            Class20.smethod_1(string.Format("调用底层接口开票总数:{0},总时间:{1},当时时间:{2}", this.int_0.ToString(), this.long_0.ToString(), elapsedMilliseconds.ToString()));
            Class20.smethod_1(string.Format("开票{0}； 代码:{1}  号码:{2}", this.RetCode, this.invCodeNum_0.InvTypeCode, this.invCodeNum_0.InvNum));
            if ((this.RetCode == 0) || (this.RetCode == 0x21b))
            {
                int num18 = 0;
                result.InvCipher = CommonTool.GetStingByteArray(this.byte_0, 0, out num18);
                num18++;
                result.InvDate = CommonTool.AcqTaxDateTime(this.byte_0, num18, 6, 2);
                this.taxStateInfo_0.IsInvEmpty = this.byte_0[num18 + 12];
                result.CipherVersion = Convert.ToString(this.byte_0[num18 + 15]);
                this.string_15 = result.CipherVersion;
                int num19 = num18 + 15;
                Class20.smethod_1(string.Format("开票以后获取加密版本号：{0}，位置：{1}", result.CipherVersion, num19.ToString()));
                int num5 = 0;
                result.InvVerify = CommonTool.GetStingByteArray(this.byte_0, num18 + 0x10, out num5);
                result.RapPeriod = this.byte_0[num5 + 1];
                Class20.smethod_1("开票以后返回抄税期：" + result.RapPeriod.ToString());
                if (((this.byte_0[num5 + 6] != 0xff) || (this.byte_0[num5 + 7] != 0xff)) || ((this.byte_0[num5 + 8] != 0xff) || (this.byte_0[num5 + 9] != 0xff)))
                {
                    Array.Reverse(this.byte_0, num5 + 6, 4);
                    result.InvIndex = BitConverter.ToUInt32(this.byte_0, num5 + 6).ToString();
                }
                if (((this.byte_0[num5 + 10] != 0xff) || (this.byte_0[num5 + 11] != 0xff)) || ((this.byte_0[num5 + 12] != 0xff) || (this.byte_0[num5 + 13] != 0xff)))
                {
                    result.InvSeqNo = BitConverter.ToUInt32(this.byte_0, num5 + 10).ToString();
                }
                Class20.smethod_1(string.Format("金税设备开具发票成功：开票日期{0},剩余发票是否为空{1},已到抄税期{2},已到锁死期{3}，密文{4}，校验码{5}", new object[] { result.InvDate.ToLongTimeString(), this.taxStateInfo_0.IsInvEmpty, ((invoiceType_1 == InvoiceType.transportation) || (invoiceType_1 == InvoiceType.vehiclesales)) ? this.taxStateInfo_0.method_0(invoiceType_1).IsRepTime : this.taxStateInfo_0.IsRepReached, ((invoiceType_1 == InvoiceType.transportation) || (invoiceType_1 == InvoiceType.vehiclesales)) ? this.taxStateInfo_0.method_0(invoiceType_1).IsLockTime : this.taxStateInfo_0.IsLockReached, result.InvCipher, result.InvVerify }));
            }
            result.InvResultEnum = InvResult.irSuccess;
            if (this.RetCode != 0)
            {
                result.InvResultEnum = InvResult.irFault;
                if (this.RetCode == 0x1fb)
                {
                    result.InvResultEnum = InvResult.irWaste;
                }
            }
            else
            {
                CommonTool.SetVolumnInvoice((byte) this.invoiceType_0, this.invCodeNum_0.InvTypeCode, uint.Parse(this.invCodeNum_0.InvNum));
            }
            Class20.smethod_1(string.Format("=============================================================金税设备开具发票结束：返回值{0}", this.RetCode));
            stopwatch.Stop();
            double totalMilliseconds = stopwatch.Elapsed.TotalMilliseconds;
            Class20.smethod_1(string.Format("开票接口执行总时间========================================", totalMilliseconds.ToString()));
            return result;
        }

        public string Invoice(string string_35, double double_0, double double_1, double double_2, string string_36, string string_37, byte[] byte_1, InvoiceType invoiceType_1, byte[] byte_2, string string_38, string string_39, InvCodeNum invCodeNum_1, object[] object_3, int int_7, out InvoiceResult invoiceResult_0)
        {
            invoiceResult_0 = this.Invoice(string_35, double_0, double_1, double_2, string_36, string_37, byte_1, invoiceType_1, byte_2, string_38, string_39, object_3);
            return string.Concat(new object[] { "TCD_", this.int_4, "_", this.int_5 });
        }

        public InvReadReValue InvRead(string string_35, byte[] byte_1 = null, int int_7 = 0, int int_8 = 0)
        {
            InvReadReValue value3;
            try
            {
                InvReadReValue value2 = new InvReadReValue();
                MessageShow.MsgWait("正在读入发票卷 ...");
                List<InvVolumeApp> list = new List<InvVolumeApp>();
                List<InvVolumeApp> listInvVolumeApp = new List<InvVolumeApp>();
                Class20.smethod_1(">>>>>11...............");
                this.GetInvStock();
                Class20.smethod_1(">>>>>12...............");
                int count = this.list_0.Count;
                if (this.list_0 != null)
                {
                    foreach (InvVolumeApp app in this.list_0)
                    {
                        list.Add(app);
                    }
                }
                string s = string.Empty;
                if (string_35 == "1")
                {
                    s = "JSP";
                }
                else if (string_35 == "2")
                {
                    s = "BSP";
                }
                else if (string_35 == "3")
                {
                    s = "NInvoiceFunction";
                }
                List<string> inList = new List<string>();
                byte[] array = this.method_64(9, 0, inList);
                Encoding.GetEncoding("GBK").GetBytes(s).CopyTo(array, 0x3e8);
                if (string_35 == "3")
                {
                    if ((int_7 == 0) && (byte_1 != null))
                    {
                        int_7 = byte_1.Length;
                    }
                    if (byte_1 != null)
                    {
                        BitConverter.GetBytes((ushort) int_7).CopyTo(array, 0x3f8);
                        byte_1.CopyTo(array, 0x3fa);
                        array[0x3fa + int_7] = (byte) int_8;
                    }
                }
                Class20.smethod_1(">>>>>13...............");
                this.int_4 = this.method_63(array, this.byte_0);
                Class20.smethod_1(">>>>>14...............");
                if ((this.int_4 == 0) || (this.int_4 == 510))
                {
                    this.taxStateInfo_0.IsInvEmpty = this.byte_0[0];
                    if (this.int_4 == 510)
                    {
                        Class20.smethod_1(">>>>>解析未读入的发票卷开始...............");
                        List<InvStockRepeat> list4 = new List<InvStockRepeat>();
                        Class20.smethod_1(">>>>>1..............." + this.byte_0.Length.ToString());
                        for (int i = 0; i < 8; i++)
                        {
                            int index = (i * 0x4b) + 2;
                            Class20.smethod_1(">>>>>2..............." + index.ToString());
                            Class20.smethod_1(">>>>>3..............." + Convert.ToString(this.byte_0[index]));
                            if (this.byte_0[index] == 0xff)
                            {
                                int num5 = i + 1;
                                Class20.smethod_1(string.Format("循环8次检测返回果：第{0}次，票种位置{1}为0xFF,数据无效，跳过继续下个循环。。。", num5.ToString(), index));
                            }
                            else
                            {
                                int num6 = i + 1;
                                Class20.smethod_1(string.Format("循环8次检测返回果：第{0}次，票种：{1},票种位置：{2},数据有效，继续解析。。。", num6.ToString(), this.byte_0[index], index));
                                InvStockRepeat item = new InvStockRepeat {
                                    Kind = this.byte_0[index],
                                    InvNoReadNum = this.byte_0[index + 1],
                                    InvSucessReadNum = this.byte_0[index + 2],
                                    ErrInfo = new List<NoReadInvStock>()
                                };
                                for (int j = 0; j < 5; j++)
                                {
                                    int num8 = ((j * 13) + index) + 3;
                                    if ((this.byte_0[num8] != 0xff) || (this.byte_0[num8 + 1] != 0xff))
                                    {
                                        NoReadInvStock stock = new NoReadInvStock {
                                            ErrNo = BitConverter.ToUInt16(this.byte_0, num8)
                                        };
                                        byte[] buffer4 = new byte[5];
                                        for (int k = 0; k < 5; k++)
                                        {
                                            buffer4[k] = this.byte_0[(num8 + 2) + k];
                                        }
                                        if ((buffer4[0] & 0xc0) != 0xc0)
                                        {
                                            stock.InvCode = this.method_50(buffer4, 5);
                                        }
                                        else
                                        {
                                            stock.InvCode = this.method_54(buffer4);
                                        }
                                        stock.InvNo = BitConverter.ToUInt32(this.byte_0, num8 + 7);
                                        stock.Count = BitConverter.ToUInt16(this.byte_0, num8 + 11);
                                        item.ErrInfo.Add(stock);
                                    }
                                }
                                list4.Add(item);
                            }
                        }
                        value2.NoReadInvStock = list4;
                        Class20.smethod_1(">>>>>解析未读入的发票卷结束...............");
                    }
                    this.GetInvStock();
                    int num10 = this.list_0.Count;
                    if (count < num10)
                    {
                        foreach (InvVolumeApp app2 in this.list_0)
                        {
                            bool flag = false;
                            using (List<InvVolumeApp>.Enumerator enumerator3 = list.GetEnumerator())
                            {
                                while (enumerator3.MoveNext())
                                {
                                    InvVolumeApp current = enumerator3.Current;
                                    if (((app2.InvType == current.InvType) && (app2.TypeCode == current.TypeCode)) && (app2.HeadCode == current.HeadCode))
                                    {
                                        goto Label_048A;
                                    }
                                }
                                goto Label_049D;
                            Label_048A:
                                flag = true;
                            }
                        Label_049D:
                            if (!flag)
                            {
                                listInvVolumeApp.Add(app2);
                            }
                        }
                    }
                    CommonTool.smethod_6(listInvVolumeApp);
                    this.method_28();
                }
                value2.ReadNewInvStock = listInvVolumeApp;
                value3 = value2;
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
                throw;
            }
            finally
            {
                MessageShow.MsgWait();
            }
            return value3;
        }

        public List<InvVolumeApp> InvReclaim(string string_35)
        {
            List<InvVolumeApp> list4;
            try
            {
                MessageShow.MsgWait("正在读分开票机退票 ...");
                List<InvVolumeApp> list = new List<InvVolumeApp>();
                List<InvVolumeApp> listInvVolumeApp = new List<InvVolumeApp>();
                this.GetInvStock();
                int count = this.list_0.Count;
                if (this.list_0 != null)
                {
                    foreach (InvVolumeApp app in this.list_0)
                    {
                        list.Add(app);
                    }
                }
                string s = string.Empty;
                if (string_35 == "1")
                {
                    s = "JSP";
                }
                else if (string_35 == "2")
                {
                    s = "BSP";
                }
                else if (string_35 == "3")
                {
                    s = "NInvoiceFunction";
                }
                new List<InvVolumeApp>();
                List<string> inList = new List<string>();
                byte[] array = this.method_64(10, 0, inList);
                Encoding.GetEncoding("GBK").GetBytes(s).CopyTo(array, 0x3e8);
                this.int_4 = this.method_63(array, this.byte_0);
                if (this.RetCode == 0)
                {
                    this.taxStateInfo_0.IsInvEmpty = this.byte_0[0];
                    this.GetInvStock();
                    int num2 = this.list_0.Count;
                    if (count < num2)
                    {
                        foreach (InvVolumeApp app2 in this.list_0)
                        {
                            bool flag = false;
                            using (List<InvVolumeApp>.Enumerator enumerator3 = list.GetEnumerator())
                            {
                                while (enumerator3.MoveNext())
                                {
                                    InvVolumeApp current = enumerator3.Current;
                                    if (((app2.InvType == current.InvType) && (app2.TypeCode == current.TypeCode)) && (app2.HeadCode == current.HeadCode))
                                    {
                                        goto Label_01AF;
                                    }
                                }
                                goto Label_01C2;
                            Label_01AF:
                                flag = true;
                            }
                        Label_01C2:
                            if (!flag)
                            {
                                listInvVolumeApp.Add(app2);
                            }
                        }
                    }
                    CommonTool.smethod_6(listInvVolumeApp);
                    this.method_28();
                }
                list4 = listInvVolumeApp;
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
                throw;
            }
            finally
            {
                MessageShow.MsgWait();
            }
            return list4;
        }

        public bool InvReturn(int int_7, string string_35)
        {
            bool flag;
            try
            {
                MessageShow.MsgWait("正在将发票退回信息写入金税设备 ...");
                this.GetInvStock();
                if ((int_7 > (this.list_0.Count - 1)) || (int_7 < 0))
                {
                    throw new ArgumentException("发票卷号超出库存");
                }
                InvVolumeApp local1 = this.list_0[int_7];
                List<string> inList = new List<string>();
                if (this.method_4() != DrvState.dsInvReturn)
                {
                    string s = string.Empty;
                    if (string_35 == "1")
                    {
                        s = "JSP";
                    }
                    else if (string_35 == "2")
                    {
                        s = "BSP";
                    }
                    else if (string_35 == "3")
                    {
                        s = "NInvoiceFunction";
                    }
                    inList.Add(string_35);
                    inList.Add(int_7.ToString());
                    byte[] array = this.method_64(13, 0, inList);
                    Encoding.GetEncoding("GBK").GetBytes(s).CopyTo(array, 0x3e8);
                    this.int_4 = this.method_63(array, this.byte_0);
                    if (this.RetCode == 0x83)
                    {
                        this.RetCode = 0x2522;
                    }
                    if (this.RetCode == 0)
                    {
                        this.taxStateInfo_0.IsInvEmpty = this.byte_0[0];
                    }
                }
                if (this.method_4() == DrvState.dsInvReturn)
                {
                    inList.Clear();
                    inList.Add("50");
                }
                if (this.RetCode == 0)
                {
                    return true;
                }
                flag = false;
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
                flag = false;
            }
            finally
            {
                MessageShow.MsgWait();
            }
            return flag;
        }

        public string InvWaste(string string_35, string string_36, InvoiceType invoiceType_1, DateTime dateTime_8, string string_37, double double_0, double double_1, double double_2, byte[] byte_1, string string_38, int int_7, out InvWasteResult invWasteResult_0)
        {
            Class20.smethod_1("=============================================================已开发票作废开始");
            object[] args = new object[] { string_35, string_36, ((int) invoiceType_1).ToString(), dateTime_8.ToLongDateString(), string_37, double_0, double_1, double_2, (byte_1 != null) ? Encoding.GetEncoding("GBK").GetString(byte_1) : "null", string_38 };
            Class20.smethod_1(string.Format("金税设备开具发票:发票代码{0},发票号码{1},发票种类{2},开票日期{3},购方税号{4},不含税金额{5},税率{6},税额{7},签名{8},索引号{9}", args));
            invWasteResult_0 = new InvWasteResult();
            List<string> inList = new List<string> {
                string_35,
                string_36,
                this.method_17(true, double_0 < 0.0, double_1).ToString(),
                dateTime_8.ToString("yyyyMMdd"),
                string_37
            };
            if (double_2 < 0.0)
            {
                inList.Add(Math.Abs(double_2).ToString());
            }
            else
            {
                inList.Add(double_2.ToString());
            }
            if (double_0 < 0.0)
            {
                inList.Add(Math.Abs(double_0).ToString());
            }
            else
            {
                inList.Add(double_0.ToString());
            }
            byte[] array = this.method_64(6, 0, inList);
            if ((this.SQInfo.DHYBZ != "") && ((this.SQInfo.DHYBZ == "Z") || (this.SQInfo.DHYBZ == "Y")))
            {
                array[0x3e8] = Convert.ToByte((int) invoiceType_1);
            }
            BitConverter.GetBytes((ushort) byte_1.Length).CopyTo(array, 0x3e9);
            byte_1.CopyTo(array, 0x3eb);
            if (!string.IsNullOrEmpty(string_38))
            {
                byte[] bytes = BitConverter.GetBytes(Convert.ToUInt32(string_38));
                Array.Reverse(bytes, 0, bytes.Length);
                bytes.CopyTo(array, (int) (0x3eb + byte_1.Length));
            }
            this.int_4 = this.method_63(array, this.byte_0);
            if (this.int_4 == 0)
            {
                if (((this.byte_0[0] != 0xff) || (this.byte_0[1] != 0xff)) || ((this.byte_0[2] != 0xff) || (this.byte_0[3] != 0xff)))
                {
                    Array.Reverse(this.byte_0, 0, 4);
                    invWasteResult_0.AddrIndex = BitConverter.ToUInt32(this.byte_0, 0).ToString();
                }
                try
                {
                    int year = (this.byte_0[4] * 0x10) + (this.byte_0[5] >> 4);
                    int month = this.byte_0[5] & 15;
                    int day = this.byte_0[6] >> 3;
                    int hour = ((this.byte_0[6] & 7) * 4) + (this.byte_0[7] >> 6);
                    int minute = this.byte_0[7] & 0x3f;
                    Class20.smethod_1(string.Format("作废时间:年{0}月{1}日{2},时{3}分{4}", new object[] { year, month, day, hour, minute }));
                    invWasteResult_0.WasteTime = new DateTime(year, month, day, hour, minute, 0);
                }
                catch (Exception)
                {
                }
            }
            Class20.smethod_1(string.Format("=============================================================已开发票作废结束：返回值{0}", this.int_4));
            return string.Concat(new object[] { "TCD_", this.int_4, "_", this.int_5 });
        }

        public bool IsOffLineInv()
        {
            List<string> inList = new List<string> { "n", "0" };
            byte[] buffer = this.method_64(0x15, 0, inList);
            this.int_4 = this.method_63(buffer, this.byte_0);
            for (int i = 0; i < 4; i++)
            {
                if (this.byte_0[i + 2] != 0xff)
                {
                    return true;
                }
            }
            return false;
        }

        public List<string> MaintPassWord(string string_35, string string_36, bool bool_10)
        {
            List<string> list2;
            try
            {
                list2 = new Class19().method_13(string_35, string_36, bool_10);
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
                throw;
            }
            return list2;
        }

        private int method_0()
        {
            return this.int_4;
        }

        private void method_1(int int_7)
        {
            this.int_4 = int_7;
            this.int_5 = -1;
        }

        private int method_10(int int_7, string string_35)
        {
            this.sqxxlist_0 = null;
            List<string> inList = new List<string>();
            if (string_35.Trim().Length == 0)
            {
                inList.Add("23456789");
            }
            else
            {
                inList.Add(string_35);
            }
            inList.Insert(0, inList[0].Length.ToString());
            byte[] buffer = this.method_64(int_7, 0, inList);
            this.int_4 = this.method_63(buffer, this.byte_0);
            if ((this.RetCode == 0) && (int_7 == 1))
            {
                Class20.smethod_3("开卡成功>>>>>>");
                this.method_11(this.byte_0);
            }
            return this.RetCode;
        }

        private void method_11(byte[] byte_1)
        {
            PZSQType type;
            bool flag;
            ulong num61;
            this.string_24 = "";
            int num = byte_1[0] - 0x2e;
            this.taxStateInfo_0.InvLimit = CommonTool.ToPow10(num);
            Class20.smethod_3("jxkkcsh _stateInfo.InvLimit:" + BitConverter.ToString(byte_1, 0, 2));
            if (byte_1[2] > 0x20)
            {
                this.taxStateInfo_0.TaxCode = Encoding.GetEncoding("GBK").GetString(byte_1, 2, 0x10);
            }
            this.dateTime_5 = CommonTool.AcqTaxDateTime(byte_1, 0x12, 7, 2);
            this.dateTime_6 = CommonTool.AcqTaxDateTime(byte_1, 0x20, 4, 2);
            this.taxStateInfo_0.IsInvEmpty = byte_1[40];
            this.taxStateInfo_0.IsRepReached = byte_1[0x29];
            this.taxStateInfo_0.IsLockReached = byte_1[0x2a];
            this.taxStateInfo_0.IsMainMachine = byte_1[0x2b];
            this.taxStateInfo_0.IsWithChild = byte_1[0x2c];
            ushort num54 = Convert.ToUInt16((int) (byte_1[1] * 0x100));
            this.taxStateInfo_0.MachineNumber = Convert.ToUInt16((int) (byte_1[0x2d] + num54));
            if (this.taxStateInfo_0.IsMainMachine > 0)
            {
                this.int_2 = 0;
            }
            else
            {
                this.int_2 = this.taxStateInfo_0.MachineNumber;
            }
            Class20.smethod_1(string.Format("开启金税设备：是否主机{0}，开票机号{1}，开票机数量{2}", this.taxStateInfo_0.IsMainMachine, this.int_2, this.taxStateInfo_0.MachineNumber));
            int count = byte_1[0x2e];
            if (count > 0)
            {
                this.string_7 = Encoding.GetEncoding("GBK").GetString(byte_1, 0x2f, count).Trim();
            }
            Class20.smethod_3(string.Format("Size：{0}，_corporation{1}", count.ToString(), this.string_7));
            Convert.ToInt64(this.dateTime_5.ToString("yyyyMMdd"));
            this.dateTime_4 = CommonTool.AcqTaxDateTime(byte_1[0x9b], byte_1[0x9c], byte_1[0x9d], byte_1[0x9e], byte_1[0x9f], byte_1[160], 0);
            this.taxStateInfo_0.CompanyType = byte_1[0xa1];
            this.taxStateInfo_0.TutorialFlag = byte_1[0xa2];
            this.taxStateInfo_0.BigAmountInvCount = byte_1[0xa3];
            this.taxStateInfo_0.ushort_4 = byte_1[0xa4];
            this.taxStateInfo_0.MajorVersion = byte_1[0xa5];
            this.taxStateInfo_0.MinorVersion = byte_1[0xa6];
            this.taxStateInfo_0.CompanyAuth = (byte_1[0xa7] == 0) ? "" : Encoding.Default.GetString(byte_1, 0xa7, 10);
            this.string_14 = CommonTool.GetStingByteArray(byte_1, 0xa7);
            if (this.string_14.Length > 10)
            {
                this.string_14 = this.string_14.Substring(0, 10) + ";";
            }
            else
            {
                this.string_14 = this.string_14.PadRight(10, ' ') + ";";
            }
            this.taxStateInfo_0.TaxCode = this._taxCode;
            string str2 = "";
            string str22 = "";
            if (byte_1[0xb1] != 0xff)
            {
                byte[] buffer = CommonTool.GetSubArray(byte_1, 0xb1, byte_1.Length - 0xb1);
                str2 = this.method_14(buffer);
                Class20.smethod_3("=====专票不含税税率授权：" + str2);
            }
            if (byte_1[0xcf] != 0xff)
            {
                byte[] buffer3 = CommonTool.GetSubArray(byte_1, 0xcf, byte_1.Length - 0xcf);
                str22 = this.method_14(buffer3);
            }
            Class20.smethod_3("=====专票含税税率授权：" + str22);
            this.taxRateAuthorize_0.TaxRateNoTax.Clear();
            this.taxRateAuthorize_0.TaxRateTax.Clear();
            foreach (string str16 in str2.Split(new char[] { ',' }))
            {
                double result = 0.0;
                if (double.TryParse(str16, out result))
                {
                    this.taxRateAuthorize_0.TaxRateNoTax.Add(result / 100.0);
                }
            }
            foreach (string str6 in str22.Split(new char[] { ',' }))
            {
                double num22 = 0.0;
                if (double.TryParse(str6, out num22))
                {
                    this.taxRateAuthorize_0.TaxRateTax.Add(num22 / 100.0);
                }
            }
            double item = 0.05;
            double num56 = 0.015;
            DateTime time = new DateTime(0x7e0, 5, 1);
            if (this.dateTime_5.Subtract(time).TotalDays >= 0.0)
            {
                this.taxRateAuthorize_0.TaxRateNoTax.Add(item);
                this.taxRateAuthorize_0.TaxRateTax.Add(num56);
                if (this.dictionary_0.ContainsKey("CEBTVisble"))
                {
                    this.dictionary_0["CEBTVisble"] = "1";
                }
                else
                {
                    this.dictionary_0.Add("CEBTVisble", "1");
                }
            }
            else if (this.dictionary_0.ContainsKey("CEBTVisble"))
            {
                this.dictionary_0["CEBTVisble"] = "0";
            }
            else
            {
                this.dictionary_0.Add("CEBTVisble", "0");
            }
            Encoding.Default.GetString(byte_1);
            int num5 = 0;
            if (byte_1[0xed] == 170)
            {
                num5++;
                string str5 = "";
                string str18 = "";
                string str13 = "";
                string str19 = "";
                string str = "";
                string str20 = "Y";
                string str4 = "";
                string str9 = "Y";
                string str10 = "H";
                while ((byte_1[0xed + num5] == 6) || (((((byte_1[0xed + num5] == 7) || (byte_1[0xed + num5] == 0x1c)) || ((byte_1[0xed + num5] == 0x1d) || (byte_1[0xed + num5] == 30))) || (((byte_1[0xed + num5] == 0x1f) || (byte_1[0xed + num5] == 9)) || ((byte_1[0xed + num5] == 10) || (byte_1[0xed + num5] == 0x21)))) || ((byte_1[0xed + num5] == 0x22) || (byte_1[0xed + num5] == 11))))
                {
                    int num6;
                    ushort num24;
                    if ((byte_1[0xed + num5] != 6) || !(str5 == ""))
                    {
                        if ((byte_1[0xed + num5] == 7) && (str18 == ""))
                        {
                            ushort num60 = BitConverter.ToUInt16(byte_1, 0xee + num5);
                            if (num60 > 0)
                            {
                                byte num39 = 0;
                                for (int m = 0; m < (num60 - 1); m++)
                                {
                                    num39 = (byte) (num39 + byte_1[(240 + num5) + m]);
                                }
                                if (num39 == byte_1[((240 + num5) + num60) - 1])
                                {
                                    byte num12 = byte_1[240 + num5];
                                    int num13 = 0;
                                    for (int n = 0; n < num12; n++)
                                    {
                                        uint num49 = BitConverter.ToUInt16(byte_1, (0xf1 + num5) + num13);
                                        str18 = (str18 + (num49 % 0x1000000)) + this.method_47(num49.ToString(), 6, '0') + ",";
                                        byte num50 = byte_1[(0xf4 + num5) + num13];
                                        str18 = str18 + Encoding.Default.GetString(byte_1, (0xf5 + num5) + num13, num50) + ",";
                                        num13 = (num13 + 4) + num50;
                                    }
                                }
                            }
                            num5 = (num5 + num60) + 3;
                            goto Label_0DBF;
                        }
                        if ((byte_1[0xed + num5] == 0x1c) && (str13 == ""))
                        {
                            ushort num34 = BitConverter.ToUInt16(byte_1, 0xee + num5);
                            str13 = str13 + ((BitConverter.ToUInt64(byte_1, 240 + num5) % ((ulong) 0x10000000000L))).ToString() + ",";
                            string str14 = Encoding.Default.GetString(byte_1, 0xf5 + num5, num34 - 5).Trim();
                            str13 = str13 + str14;
                            num5 = (num5 + num34) + 3;
                            goto Label_0DBF;
                        }
                        if ((byte_1[0xed + num5] == 0x1d) && (str19 == ""))
                        {
                            ushort num65 = BitConverter.ToUInt16(byte_1, 0xee + num5);
                            string str26 = Encoding.Default.GetString(byte_1, 240 + num5, num65).Trim();
                            str19 = str19 + str26;
                            num5 = (num5 + num65) + 3;
                            goto Label_0DBF;
                        }
                        if ((byte_1[0xed + num5] == 0x22) && (this.string_24 == ""))
                        {
                            ushort num31 = BitConverter.ToUInt16(byte_1, 0xee + num5);
                            string str12 = Encoding.Default.GetString(byte_1, 240 + num5, num31).Trim();
                            this.string_24 = str12;
                            num5 = (num5 + num31) + 3;
                            goto Label_0DBF;
                        }
                        if ((byte_1[0xed + num5] != 30) || !(str == ""))
                        {
                            if ((byte_1[0xed + num5] == 0x1f) && (str20 == "Y"))
                            {
                                ushort length = BitConverter.ToUInt16(byte_1, 0xee + num5);
                                str20 = Encoding.Default.GetString(byte_1, 240 + num5, (byte_1.Length - 240) - num5).Trim().Substring(0, length);
                                num5 = (num5 + length) + 3;
                            }
                            goto Label_0DBF;
                        }
                        num24 = BitConverter.ToUInt16(byte_1, 0xee + num5);
                        if (num24 <= 0)
                        {
                            goto Label_0D4D;
                        }
                        byte num25 = 0;
                        for (int k = 0; k < (num24 - 1); k++)
                        {
                            num25 = (byte) (num25 + byte_1[(240 + num5) + k]);
                        }
                        if (num25 != byte_1[((240 + num5) + num24) - 1])
                        {
                            goto Label_0D4D;
                        }
                        num6 = 0;
                    }
                    else
                    {
                        num5++;
                        ushort num20 = BitConverter.ToUInt16(byte_1, 0xed + num5);
                        num5 += 2;
                        if (num20 > 0)
                        {
                            byte num29 = 0;
                            for (int num30 = 0; num30 < (num20 - 1); num30++)
                            {
                                num29 = (byte) (num29 + byte_1[(0xed + num5) + num30]);
                            }
                            if (num29 == byte_1[((0xed + num5) + num20) - 1])
                            {
                                int num15 = 0;
                                do
                                {
                                    str5 = str5 + Convert.ToString(byte_1[(0xed + num5) + num15]) + "-";
                                    ulong num28 = BitConverter.ToUInt64(byte_1, (0xee + num5) + num15) % ((ulong) 0x1000000000000L);
                                    string str7 = num28.ToString();
                                    while (str7.Length < 3)
                                    {
                                        str7 = "0" + str7;
                                    }
                                    string str8 = str5;
                                    str5 = str8 + str7.Substring(0, str7.Length - 2) + "." + str7.Substring(str7.Length - 2, 2) + "-";
                                    num28 = BitConverter.ToUInt64(byte_1, (0xf4 + num5) + num15) % ((ulong) 0x1000000000000L);
                                    str7 = num28.ToString();
                                    while (str7.Length < 3)
                                    {
                                        str7 = "0" + str7;
                                    }
                                    str8 = str5;
                                    str5 = str8 + str7.Substring(0, str7.Length - 2) + "." + str7.Substring(str7.Length - 2, 2) + "-";
                                    str7 = (BitConverter.ToUInt64(byte_1, (250 + num5) + num15) % ((ulong) 0x1000000000000L)).ToString();
                                    while (str7.Length < 3)
                                    {
                                        str7 = "0" + str7;
                                    }
                                    str8 = str5;
                                    str5 = str8 + str7.Substring(0, str7.Length - 2) + "." + str7.Substring(str7.Length - 2, 2) + "-";
                                    byte num19 = byte_1[(0x100 + num5) + num15];
                                    str5 = str5 + Convert.ToString(byte_1[(0x100 + num5) + num15]) + "-";
                                    for (int num16 = 0; num16 < num19; num16++)
                                    {
                                        uint num17 = BitConverter.ToUInt32(byte_1, ((0x101 + num5) + num15) + (num16 * 6)) % 0x1000000;
                                        str5 = str5 + this.method_47(num17.ToString(), 6, '0') + "-";
                                        num17 = BitConverter.ToUInt32(byte_1, (((0x101 + num5) + num15) + (num16 * 6)) + 3) % 0x1000000;
                                        double num18 = num17;
                                        str5 = str5 + ((num18 / 1000.0)).ToString("0.00") + "-";
                                    }
                                    str5 = str5 + ",";
                                    num15 = (num15 + 20) + (6 * num19);
                                }
                                while (num15 < (num20 - 1));
                            }
                            num5 += num20;
                        }
                        goto Label_0DBF;
                    }
                    do
                    {
                        str = str + byte_1[(240 + num5) + num6].ToString() + "-";
                        num6++;
                        byte num7 = byte_1[(240 + num5) + num6];
                        str = str + num7.ToString() + "-";
                        num6++;
                        for (int num8 = 0; num8 < num7; num8++)
                        {
                            byte num47 = byte_1[(240 + num5) + num6];
                            num6++;
                            str = str + Encoding.Default.GetString(byte_1, (240 + num5) + num6, num47) + "-";
                            num6 += num47;
                            byte num48 = byte_1[(240 + num5) + num6];
                            num6++;
                            str = str + Encoding.Default.GetString(byte_1, (240 + num5) + num6, num48) + "-";
                            num6 += num48;
                        }
                        str = str + ",";
                    }
                    while (num6 < (num24 - 1));
                Label_0D4D:
                    num5 = (num5 + num24) + 3;
                Label_0DBF:
                    if ((byte_1[0xed + num5] == 0x21) && (str4 == ""))
                    {
                        num5++;
                        ushort num53 = BitConverter.ToUInt16(byte_1, 0xed + num5);
                        num5 += 2;
                        if (num53 <= 0)
                        {
                            continue;
                        }
                        int num27 = 0;
                        for (int num38 = 0; num38 < 3; num38++)
                        {
                            num27 = 9 * num38;
                            if (num27 > num53)
                            {
                                break;
                            }
                            if (!string.Empty.Equals(str4))
                            {
                                str4 = str4 + ";";
                            }
                            str4 = str4 + byte_1[(0xed + num5) + num27].ToString() + "-" + Encoding.GetEncoding("GBK").GetString(byte_1, ((0xed + num5) + num27) + 1, 8);
                            if (byte_1[(0xed + num5) + num27] == 12)
                            {
                                this.string_21 = Encoding.GetEncoding("GBK").GetString(byte_1, ((0xed + num5) + num27) + 1, 8);
                            }
                            if (byte_1[(0xed + num5) + num27] == 0x33)
                            {
                                this.string_22 = Encoding.GetEncoding("GBK").GetString(byte_1, ((0xed + num5) + num27) + 1, 8);
                            }
                            if (byte_1[(0xed + num5) + num27] == 0x29)
                            {
                                this.string_23 = Encoding.GetEncoding("GBK").GetString(byte_1, ((0xed + num5) + num27) + 1, 8);
                            }
                        }
                        num5 += num53;
                    }
                }
                str9 = Encoding.Default.GetString(byte_1, 0x1000, 1);
                str10 = Encoding.Default.GetString(byte_1, 0x1001, 1);
                this.string_18 = Convert.ToString(byte_1[0x1002], 0x10);
                string str11 = Convert.ToString(byte_1[0x1003], 0x10);
                if (str11.Length == 1)
                {
                    str11 = "0" + str11;
                }
                this.string_18 = this.string_18 + str11;
                str11 = Convert.ToString(byte_1[0x1004], 0x10);
                if (str11.Length == 1)
                {
                    str11 = "0" + str11;
                }
                this.string_18 = this.string_18 + str11;
                byte[] buffer2 = new byte[0xd0];
                for (int i = 0; i < 0xd0; i++)
                {
                    buffer2[i] = byte_1[i + 0x1005];
                }
                Class20.smethod_1("===========================26*8字节的货运票和机动车票的相关信息开始========================");
                Class20.smethod_1(BitConverter.ToString(buffer2));
                Class20.smethod_1("===========================26*8字节的货运票和机动车票的相关信息结束============================");
                this.taxStateInfo_0.InvTypeInfo.Clear();
                Class20.smethod_1("清除26*8字节授权信息");
                for (int j = 0; j < 8; j++)
                {
                    int index = j * 0x1a;
                    int num36 = buffer2[index];
                    switch (num36)
                    {
                        case 11:
                        case 12:
                        case 0x33:
                        case 0x29:
                        {
                            Class20.smethod_1("新加类型：" + num36.ToString());
                            InvTypeInfo info3 = new InvTypeInfo {
                                InvType = buffer2[index],
                                IsRepTime = buffer2[index + 1],
                                IsLockTime = buffer2[index + 2],
                                ushort_0 = buffer2[index + 3],
                                ushort_1 = buffer2[index + 4],
                                ICBuyInv = buffer2[index + 5],
                                ICRetInv = buffer2[index + 6],
                                Reserve = buffer2[index + 7],
                                LastRepDate = BitConverter.ToString(buffer2, index + 8, 1) + BitConverter.ToString(buffer2, (index + 8) + 1, 1) + "-" + BitConverter.ToString(buffer2, (index + 8) + 2, 1) + "-" + BitConverter.ToString(buffer2, (index + 8) + 3, 1) + " " + BitConverter.ToString(buffer2, (index + 8) + 4, 1) + ":" + BitConverter.ToString(buffer2, (index + 8) + 5, 1),
                                NextRepDate = BitConverter.ToString(buffer2, index + 14, 1) + BitConverter.ToString(buffer2, (index + 14) + 1, 1) + "-" + BitConverter.ToString(buffer2, (index + 14) + 2, 1) + "-" + BitConverter.ToString(buffer2, (index + 14) + 3, 1),
                                LockedDate = BitConverter.ToString(buffer2, index + 0x12, 1) + BitConverter.ToString(buffer2, (index + 0x12) + 1, 1) + "-" + BitConverter.ToString(buffer2, (index + 0x12) + 2, 1) + "-" + BitConverter.ToString(buffer2, (index + 0x12) + 3, 1),
                                ushort_2 = buffer2[index + 0x16],
                                ushort_3 = buffer2[index + 0x17],
                                ushort_4 = buffer2[index + 0x18],
                                ushort_5 = buffer2[index + 0x19]
                            };
                            this.taxStateInfo_0.InvTypeInfo.Add(info3);
                            Class20.smethod_1(string.Format("开卡状态：票种类别{0}，上个抄税日{1},下个抄税日{2}，锁死日{3}", new object[] { num36, info3.LastRepDate, info3.NextRepDate, info3.LockedDate }));
                            break;
                        }
                    }
                }
                InvTypeInfo info = this.taxStateInfo_0.method_0(InvoiceType.transportation);
                Class20.smethod_1(string.Format("货运开卡状态：上个抄税日{0},下个抄税日{1}，锁死日{2}", info.LastRepDate, info.NextRepDate, info.LockedDate));
                InvTypeInfo info2 = this.taxStateInfo_0.method_0(InvoiceType.vehiclesales);
                Class20.smethod_1(string.Format("机动车开卡状态：上个抄税日{0},下个抄税日{1}，锁死日{2}", info2.LastRepDate, info2.NextRepDate, info2.LockedDate));
                string str15 = str5;
                Class20.smethod_1("票种授权type6：" + str5);
                this.invSQInfo_0.PZSQType.Clear();
                if (!string.IsNullOrEmpty(str15))
                {
                    string[] strArray3 = str15.Split(new char[] { ',' });
                    if (strArray3.Length > 0)
                    {
                        foreach (string str21 in strArray3)
                        {
                            if (string.IsNullOrEmpty(str21.Trim()))
                            {
                                continue;
                            }
                            PZSQType type2 = new PZSQType();
                            string[] strArray5 = str21.Split(new char[] { '-' });
                            if (strArray5.Length > 0)
                            {
                                InvoiceType vehiclesales = InvoiceType.vehiclesales;
                                switch (strArray5[0])
                                {
                                    case "0":
                                        vehiclesales = InvoiceType.special;
                                        break;

                                    case "2":
                                        vehiclesales = InvoiceType.common;
                                        break;

                                    case "11":
                                        vehiclesales = InvoiceType.transportation;
                                        break;

                                    case "12":
                                        vehiclesales = InvoiceType.vehiclesales;
                                        break;

                                    case "51":
                                        vehiclesales = InvoiceType.Electronic;
                                        break;

                                    case "41":
                                        vehiclesales = InvoiceType.volticket;
                                        break;

                                    default:
                                        vehiclesales = InvoiceType.bk1;
                                        break;
                                }
                                type2.invType = vehiclesales;
                                type2.InvAmountLimit = Convert.ToDouble(strArray5[1]);
                                type2.MonthAmountLimit = Convert.ToDouble(strArray5[2]);
                                type2.ReturnAmountLimit = Convert.ToDouble(strArray5[3]);
                                int num57 = Convert.ToInt32(strArray5[4]);
                                for (int num45 = 0; num45 < num57; num45++)
                                {
                                    TaxRateType type3 = new TaxRateType {
                                        Sm = strArray5[5 + (num45 * 2)]
                                    };
                                    type3.Rate = Convert.ToDouble((Convert.ToDouble(strArray5[6 + (num45 * 2)]) / 100.0).ToString("F02"));
                                    type2.TaxRate.Add(type3);
                                    Class20.smethod_1(string.Format("=====票种授权，票种：{0}，税率：{1}", type2.invType, type3.Rate));
                                }
                                Class20.smethod_1(string.Format("=====type6票种授权，票种：{0}，InvAmountLimit：{1}，MonthAmountLimit：{1}，ReturnAmountLimit：{1}", new object[] { type2.invType, type2.InvAmountLimit, type2.MonthAmountLimit, type2.ReturnAmountLimit }));
                                if ((type2.invType == InvoiceType.Electronic) && (this.dateTime_5.Subtract(time).TotalDays >= 0.0))
                                {
                                    TaxRateType type8 = new TaxRateType {
                                        Rate = item
                                    };
                                    type2.TaxRate.Add(type8);
                                }
                                this.invSQInfo_0.PZSQType.Add(type2);
                            }
                        }
                    }
                }
                this.invSQInfo_0.HYSQ = str18;
                this.invSQInfo_0.ZGJGDMMC = str13;
                this.invSQInfo_0.DJKHZC = str19;
                this.invSQInfo_0.string_0 = str;
                this.invSQInfo_0.string_5 = str20;
                this.invSQInfo_0.FXBZ = str9;
                this.invSQInfo_0.DHYBZ = str10;
            }
            this.invSQInfo_0.string_1 = Convert.ToString(byte_1[0x10f2]);
            if (Encoding.ASCII.GetString(byte_1, 0x10f3, 1) == "1")
            {
                this.invSQInfo_0.bool_0 = true;
            }
            else
            {
                this.invSQInfo_0.bool_0 = false;
            }
            this.invSQInfo_0.string_2 = Encoding.ASCII.GetString(byte_1, 0x10f4, 1);
            if (this.invSQInfo_0.string_2 == "2")
            {
                this.invSQInfo_0.string_2 = "0";
            }
            else if (this.invSQInfo_0.string_2 == "3")
            {
                this.invSQInfo_0.string_2 = "1";
            }
            this.invSQInfo_0.string_3 = Encoding.ASCII.GetString(byte_1, 0x10f5, 1);
            this.invSQInfo_0.string_4 = Encoding.ASCII.GetString(byte_1, 0x10f6, 1);
            Class20.smethod_1("电信企业标识" + this.invSQInfo_0.string_4.ToString());
            if (!(Encoding.ASCII.GetString(byte_1, 0x10fd, 1) == "1") && !(Encoding.ASCII.GetString(byte_1, 0x10fd, 1) == "2"))
            {
                this.invSQInfo_0.bool_1 = false;
            }
            else
            {
                this.invSQInfo_0.bool_1 = true;
            }
            this.invSQInfo_0.LXSSQ = byte_1[0x10fe];
            this.invSQInfo_0.LXKPTIME = BitConverter.ToUInt32(byte_1, 0x10ff);
            int num2 = 0;
            string str17 = string.Empty;
            ushort num4 = BitConverter.ToUInt16(byte_1, 0x1103);
            num2 = 2;
            if (num4 <= 0)
            {
                goto Label_1BEF;
            }
            goto Label_1BC1;
        Label_1970:
            num61 = BitConverter.ToUInt64(byte_1, 0x1104 + num2) % ((ulong) 0x1000000000000L);
            string str23 = num61.ToString();
            while (str23.Length < 3)
            {
                str23 = "0" + str23;
            }
            type.InvAmountLimit = Convert.ToDouble(str23.Substring(0, str23.Length - 2) + "." + str23.Substring(str23.Length - 2, 2));
            num61 = BitConverter.ToUInt64(byte_1, 0x110a + num2) % ((ulong) 0x1000000000000L);
            for (str23 = num61.ToString(); str23.Length < 3; str23 = "0" + str23)
            {
            }
            str23 = (BitConverter.ToUInt64(byte_1, 0x1110 + num2) % ((ulong) 0x1000000000000L)).ToString();
            while (str23.Length < 3)
            {
                str23 = "0" + str23;
            }
            type.OffLineAmoutLimit = Convert.ToDouble(str23.Substring(0, str23.Length - 2) + "." + str23.Substring(str23.Length - 2, 2));
            byte num3 = byte_1[0x1116 + num2];
            Class20.smethod_1(string.Format("=====票种新增授权 4096 + 227 + 32，票种：{0}，InvAmountLimit：{1}，MonthAmountLimit：{1}，OffLineAmoutLimit：{1}", new object[] { type.invType, type.InvAmountLimit, type.MonthAmountLimit, type.OffLineAmoutLimit }));
            if (flag)
            {
                this.invSQInfo_0.PZSQType.Add(type);
            }
            num2 = (num2 + 20) + num3;
            if (num2 >= (num4 - 1))
            {
                goto Label_1BEF;
            }
        Label_1BC1:
            str17 = Convert.ToString(byte_1[0x1103 + num2]);
            type = this.invSQInfo_0.method_0(str17);
            flag = false;
            if (type != null)
            {
                goto Label_1970;
            }
            flag = true;
            type = new PZSQType();
            switch (Convert.ToInt32(str17))
            {
                case 0:
                    type.invType = InvoiceType.special;
                    goto Label_1970;

                case 2:
                    type.invType = InvoiceType.common;
                    goto Label_1970;

                case 11:
                    type.invType = InvoiceType.transportation;
                    goto Label_1970;

                case 12:
                    type.invType = InvoiceType.vehiclesales;
                    goto Label_1970;

                case 0x33:
                    type.invType = InvoiceType.Electronic;
                    goto Label_1970;

                case 0x29:
                    type.invType = InvoiceType.volticket;
                    goto Label_1970;

                default:
                    type.invType = InvoiceType.bk1;
                    goto Label_1970;
            }
        Label_1BEF:
            if (!this.QYLX.ISPTFP)
            {
                bool iSPTFPDZ = this.QYLX.ISPTFPDZ;
            }
            else if (byte_1[0x1105 + num4] != 0xff)
            {
                ushort num64 = Convert.ToUInt16(byte_1[0x1105 + num4]);
                Class20.smethod_1(string.Format("税率授权数据长度：{0} ,所处位置{1}", num64, 0x1105 + num4));
                byte[] buffer4 = CommonTool.GetSubArray(byte_1, 0x1105 + num4, (((byte_1.Length - 0x1000) - 0xe5) - 0x20) - num4);
                string str25 = this.method_14(buffer4);
                Class20.smethod_3("普票不含税税率授权：" + str25);
                string[] strArray6 = str25.Split(new char[] { ',' });
                this.taxRateAuthorize_1.TaxRateNoTax.Clear();
                this.taxRateAuthorize_1.TaxRateTax.Clear();
                for (int num11 = 0; num11 < strArray6.Length; num11++)
                {
                    string str27 = strArray6[num11];
                    double num51 = 0.0;
                    if (double.TryParse(str27, out num51))
                    {
                        this.taxRateAuthorize_1.TaxRateNoTax.Add(num51 / 100.0);
                    }
                }
                if (this.dateTime_5.Subtract(time).TotalDays >= 0.0)
                {
                    this.taxRateAuthorize_1.TaxRateNoTax.Add(item);
                    this.taxRateAuthorize_1.TaxRateTax.Add(num56);
                }
            }
            this.string_15 = Convert.ToString(byte_1[(0x1105 + num4) + 30]);
            Class20.smethod_1(string.Format("加密版本号：{0} ,所处位置{1}", this.string_15, (0x1105 + num4) + 30));
            int year = BitConverter.ToUInt16(byte_1, ((0x1105 + num4) + 30) + 1);
            int month = Convert.ToUInt16(byte_1[((0x1105 + num4) + 30) + 3]);
            this.string_3 = year.ToString() + month.ToString().PadLeft(2, '0');
            Class20.smethod_1(string.Format("发行有效期：{0} ,所处位置{1}", this.string_3, ((0x1105 + num4) + 30) + 1));
            string[] strArray2 = new string[] { ((char) byte_1[(((0x1105 + num4) + 30) + 3) + 1]).ToString(), ((char) byte_1[(((0x1105 + num4) + 30) + 3) + 2]).ToString(), ((char) byte_1[(((0x1105 + num4) + 30) + 3) + 3]).ToString(), ((char) byte_1[(((0x1105 + num4) + 30) + 3) + 4]).ToString(), "-", ((char) byte_1[(((0x1105 + num4) + 30) + 3) + 5]).ToString(), ((char) byte_1[(((0x1105 + num4) + 30) + 3) + 6]).ToString(), "-", ((char) byte_1[(((0x1105 + num4) + 30) + 3) + 7]).ToString(), ((char) byte_1[(((0x1105 + num4) + 30) + 3) + 8]).ToString() };
            string s = string.Concat(strArray2);
            try
            {
                this.dateTime_2 = DateTime.Parse(s);
            }
            catch (Exception)
            {
                this.dateTime_2 = new DateTime(year, month, 1);
            }
            if (this.invSQInfo_0.method_0("0") != null)
            {
                if (this.taxRateAuthorize_0.TaxRateNoTax.Count > 0)
                {
                    this.invSQInfo_0.method_0("0").TaxRate.Clear();
                    foreach (double num58 in this.taxRateAuthorize_0.TaxRateNoTax)
                    {
                        TaxRateType type6 = new TaxRateType {
                            Rate = num58
                        };
                        this.invSQInfo_0.method_0("0").TaxRate.Add(type6);
                    }
                }
                if (this.taxRateAuthorize_0.TaxRateTax.Count > 0)
                {
                    this.invSQInfo_0.method_0("0").TaxRate2.Clear();
                    foreach (double num52 in this.taxRateAuthorize_0.TaxRateTax)
                    {
                        TaxRateType type5 = new TaxRateType {
                            Rate = num52
                        };
                        this.invSQInfo_0.method_0("0").TaxRate2.Add(type5);
                    }
                }
            }
            if (this.invSQInfo_0.method_0("2") != null)
            {
                if (this.taxRateAuthorize_1.TaxRateNoTax.Count > 0)
                {
                    this.invSQInfo_0.method_0("2").TaxRate.Clear();
                    foreach (double num63 in this.taxRateAuthorize_1.TaxRateNoTax)
                    {
                        TaxRateType type7 = new TaxRateType {
                            Rate = num63
                        };
                        this.invSQInfo_0.method_0("2").TaxRate.Add(type7);
                    }
                }
                if (this.taxRateAuthorize_1.TaxRateTax.Count > 0)
                {
                    this.invSQInfo_0.method_0("2").TaxRate2.Clear();
                    foreach (double num66 in this.taxRateAuthorize_1.TaxRateTax)
                    {
                        TaxRateType type9 = new TaxRateType {
                            Rate = num66
                        };
                        this.invSQInfo_0.method_0("2").TaxRate2.Add(type9);
                    }
                }
            }
        }

        private bool method_12()
        {
            InputPasswordForm form;
            string str;
            this.int_4 = this.method_10(1, "");
            if (this.RetCode == 0)
            {
                return false;
            }
            if (this.RetCode != 0x1fd)
            {
                goto Label_00BE;
            }
            return this.method_13();
        Label_00A4:
            MessageShow.PromptDlg("金税设备口令长度不正确，请重新输入", "口令错误");
            form.method_3("");
        Label_00BE:
            form = new InputPasswordForm();
            if (form.ShowDialog() != DialogResult.OK)
            {
                this.int_4 = 0x2b6a;
                return true;
            }
            if (form.method_2().Length == 0)
            {
                str = "23456789";
            }
            else
            {
                str = form.method_2();
            }
            int num = this.method_10(1, str);
            if (num <= 0x10)
            {
                switch (num)
                {
                    case 0:
                        return false;

                    case 4:
                        goto Label_00E3;

                    case 0x10:
                        goto Label_00A4;
                }
            }
            else
            {
                if (num != 0xeb)
                {
                    if (num == 0x1fd)
                    {
                        goto Label_00E3;
                    }
                    if (num != 0x20a)
                    {
                        goto Label_00D4;
                    }
                    if (!MessageShow.ConfirmDlg("金税设备口令输入错误，请重新输入 ...", "口令错误"))
                    {
                        this.int_4 = 0x2b6a;
                        return true;
                    }
                    form.method_3("");
                    goto Label_00BE;
                }
                goto Label_00A4;
            }
        Label_00D4:
            return true;
        Label_00E3:
            if (!MessageShow.ConfirmDlg("口令输入错误次数太多造成金税设备\n锁止，请输入二级口令解锁 ...", "口令错误"))
            {
                this.int_4 = 0x2b6a;
                return true;
            }
            return this.method_13();
        }

        private bool method_13()
        {
            InputPasswordForm form = new InputPasswordForm();
            form.method_1("输入金税设备二级口令");
            while (true)
            {
                if (form.ShowDialog() != DialogResult.OK)
                {
                    this.int_4 = 0x2b6a;
                    return true;
                }
                this.int_4 = this.method_10(2, form.method_2());
                if (this.RetCode == 0)
                {
                    MessageShow.PromptDlg("二级口令已正确输入，金税设备\n已成功重置一级口令。", "口令正确");
                    return this.method_12();
                }
                if (!MessageShow.ConfirmDlg("金税设备口令输入错误，请重新输入 ...", "口令错误"))
                {
                    this.int_4 = 0x2b6a;
                    return true;
                }
                form.method_3("");
            }
        }

        private string method_14(byte[] byte_1)
        {
            string str = "";
            int index = byte_1[0];
            byte[] buffer = new byte[30];
            byte num2 = 0;
            for (int i = 1; i < index; i++)
            {
                buffer[i - 1] = byte_1[i];
                num2 = (byte) (num2 + byte_1[i]);
            }
            if (num2 != byte_1[index])
            {
                index = 0;
            }
            else
            {
                index--;
            }
            for (int j = 0; j < index; j++)
            {
                byte num5 = buffer[j];
                int num6 = j;
                for (int m = j + 1; m < index; m++)
                {
                    if (buffer[m] > num5)
                    {
                        num5 = buffer[m];
                        num6 = m;
                    }
                }
                if (num6 != j)
                {
                    byte num10 = buffer[j];
                    buffer[j] = buffer[num6];
                    buffer[num6] = num10;
                }
            }
            str = "";
            for (int k = 0; k < index; k++)
            {
                str = str + ((buffer[k] / 10)).ToString() + ",";
            }
            return str;
        }

        private void method_15()
        {
            List<string> inList = new List<string>();
            byte[] buffer = new byte[0x11800];
            inList.Clear();
            inList.Add("B");
            inList.Add("1");
            byte[] buffer2 = this.method_64(0x15, 0, inList);
            this.int_4 = this.method_63(buffer2, buffer);
            if (this.int_4 == 0)
            {
                ushort count = BitConverter.ToUInt16(buffer, 0);
                this.string_12 = Encoding.GetEncoding("GBK").GetString(buffer, 2, count);
            }
        }

        private bool method_16()
        {
            bool flag;
            try
            {
                if (this.bool_5)
                {
                    if (this.method_4() != DrvState.dsClose)
                    {
                        byte[] buffer = this.method_64(3, 0, null);
                        this.method_63(buffer, this.byte_0);
                    }
                    this.method_61();
                }
                this.bool_5 = false;
                this.bool_4 = false;
                this.string_17 = "";
                flag = true;
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
                flag = false;
            }
            finally
            {
                if (this.class21_0 != null)
                {
                    this.class21_0.method_3();
                }
            }
            return flag;
        }

        private int method_17(bool bool_10, bool bool_11, double double_0)
        {
            int num = 80;
            if (bool_10)
            {
                num += this.bool_6 ? -64 : 0x20;
            }
            if (bool_11)
            {
                num += 0x10;
            }
            switch (this.ctaxCardType_0)
            {
                case CTaxCardType.tctAddedTax:
                case CTaxCardType.tctAgentInv:
                    if (Math.Abs((double) (double_0 - 0.17)) >= 0.001)
                    {
                        if (Math.Abs((double) (double_0 - 0.13)) < 0.001)
                        {
                            num++;
                            return num;
                        }
                        if ((Math.Abs((double) (double_0 - 0.06)) >= 0.001) && this.bool_6)
                        {
                            if (Math.Abs((double) (double_0 - 0.04)) < 0.001)
                            {
                                return (num + 3);
                            }
                            return (num + 4);
                        }
                        return (num + 2);
                    }
                    return num;

                case CTaxCardType.tctFuelTax:
                    return (num + Convert.ToInt32((double) (double_0 - 0.5)));

                case CTaxCardType.tctWasteOld:
                    return (num + 4);
            }
            return num;
        }

        internal InvDetail method_18(long long_3)
        {
            if (this.ecardType_0 > ECardType.ectDefault)
            {
                long_3 += this.long_1;
            }
            return this.method_21(long_3, true);
        }

        private InvDetail method_19(long long_3)
        {
            return this.method_21(long_3, true);
        }

        private int method_2()
        {
            int num = 0;
            int num2 = 0;
            if (this.dateTime_6.Day <= 15)
            {
                num2 = this.dateTime_6.Month - 1;
            }
            if (num2 < 1)
            {
                num = this.dateTime_6.Year - 1;
            }
            return num;
        }

        private InvDetail method_20(string string_35, int int_7, double double_0, bool bool_10, bool bool_11)
        {
            InvDetail detail2;
            try
            {
                this.method_42();
                string str = this.method_17(bool_11, bool_10, double_0).ToString();
                List<string> inList = new List<string> {
                    string_35,
                    int_7.ToString(),
                    str
                };
                byte[] buffer = this.method_64(4, 0, inList);
                this.int_4 = this.method_63(buffer, this.byte_0);
                detail2 = this.method_22(true);
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
                throw;
            }
            return detail2;
        }

        private InvDetail method_21(long long_3, bool bool_10)
        {
            InvDetail detail2;
            try
            {
                Class20.smethod_1(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>发票修复-1>>>>>>>>>>>>>>>>>>>>>>>>>");
                this.method_42();
                Class20.smethod_1(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>发票修复-2>>>>>>>>>>>>>>>>>>>>>>>>>");
                List<string> inList = new List<string> {
                    long_3.ToString()
                };
                Class20.smethod_1(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>发票修复-3>>>>>>>>>>>>>>>>>>>>>>>>>");
                byte[] buffer = this.method_64(4, 2, inList);
                Class20.smethod_1(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>发票修复-4>>>>>>>>>>>>>>>>>>>>>>>>>");
                Class20.smethod_1("===============调用4功能号获取发票明细开始================");
                this.int_4 = this.method_63(buffer, this.byte_0);
                Class20.smethod_1("===============调用4功能号获取发票明细结束================");
                Class20.smethod_1("===============解析发票明细开始================");
                InvDetail detail = this.method_22(bool_10);
                Class20.smethod_1("===============解析发票明细结束================");
                if (bool_10 && this.bool_7)
                {
                    if (this.bool_8)
                    {
                        detail.OldTypeCode = this.class17_0.method_19(long_3, detail.TypeCode, detail.InvNo.ToString("00000000"), ref detail.KeyFlagNo, ref detail.InvQryNo);
                    }
                    else
                    {
                        detail.OldTypeCode = this.class17_0.method_16(detail.Date, detail.TypeCode, detail.InvNo.ToString("00000000"), ref detail.KeyFlagNo, ref detail.InvQryNo);
                    }
                }
                GC.Collect();
                detail2 = detail;
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
                throw;
            }
            return detail2;
        }

        private InvDetail method_22(bool bool_10)
        {
            InvDetail detail2;
            try
            {
                Class20.smethod_1("=============================================================解析发票明细数据开始");
                InvDetail detail = new InvDetail();
                if (this.RetCode == 0)
                {
                    detail.SaleTaxCode = this.TaxCode;
                    detail.CancelFlag = this.byte_0[0] < 80;
                    double num = ((this.byte_0[0] & 0x10) > 0) ? ((double) 100) : ((double) (-100));
                    detail.InvNo = BitConverter.ToUInt32(this.byte_0, 1) & 0xfffffff;
                    detail.TaxClass = this.byte_0[0] & 15;
                    detail.Amount = 0.0;
                    for (int i = 0; i < 5; i++)
                    {
                        detail.Amount = (detail.Amount * 256.0) + this.byte_0[9 - i];
                    }
                    detail.Amount /= num;
                    if (this.ctaxCardType_0 == CTaxCardType.tctFuelTax)
                    {
                        detail.Amount /= 10.0;
                    }
                    detail.Tax = 0.0;
                    detail.Tax = (this.byte_0[4] & 240) + (this.byte_0[0x10] >> 4);
                    for (int j = 0; j < 4; j++)
                    {
                        detail.Tax = (detail.Tax * 256.0) + this.byte_0[13 - j];
                    }
                    detail.Tax /= num;
                    try
                    {
                        int result = 0;
                        int.TryParse(string.Format("{0}{1}", this.byte_0[14], this.byte_0[15]), out result);
                        int month = this.byte_0[0x10] & 15;
                        int day = this.byte_0[0x11];
                        Class20.smethod_1(string.Format("开票日期：年{0}{1},月{2},日{3}", new object[] { this.byte_0[14], this.byte_0[15], this.byte_0[0x10] & 15, this.byte_0[0x11] }));
                        detail.Date = new DateTime(result, month, day);
                    }
                    catch (Exception)
                    {
                    }
                    int num7 = 5;
                    string str = Encoding.GetEncoding("GBK").GetString(this.byte_0, 0x12, 20).Trim();
                    if (str.IndexOf('\0') > -1)
                    {
                        str = str.Substring(0, str.IndexOf('\0'));
                    }
                    if (str.StartsWith("000000123456789"))
                    {
                        str = new string('0', 15);
                    }
                    detail.BuyTaxCode = str;
                    if ((this.byte_0[0x22 + num7] & 0xc0) != 0xc0)
                    {
                        detail.TypeCode = Encoding.Default.GetString(this.byte_0, 0x21 + num7, 10);
                    }
                    else
                    {
                        byte[] buffer = new byte[10];
                        for (int k = 0; k < 10; k++)
                        {
                            buffer[k] = this.byte_0[(0x22 + num7) + k];
                        }
                        detail.TypeCode = this.method_54(buffer);
                    }
                    if (this.ctaxCardType_0 >= CTaxCardType.tctCommonInv)
                    {
                        detail.InvType = this.byte_0[0x40];
                    }
                    else
                    {
                        detail.InvType = 0;
                    }
                    Class20.smethod_1(string.Format(">>>>>>>>>>>>>>>>发票读取：发票代码{0}，发票号码{1}，金额{2}，税额{3}", new object[] { detail.TypeCode, detail.InvNo, detail.Amount, detail.Tax }));
                    Class20.smethod_1(">>>>>>>>>>>>>>>>发票读取前30字节：" + BitConverter.ToString(this.byte_0, 0, 30));
                    detail.ClientNum = BitConverter.ToUInt16(this.byte_0, 0x37);
                    detail.InvRepPeriod = BitConverter.ToUInt16(this.byte_0, 0x37 + num7);
                    detail.OldTypeCode = CommonTool.GetSubArray(this.byte_0, 0x2b + num7, 12);
                    detail.OldInvNo = BitConverter.ToUInt32(this.byte_0, 0x37 + num7).ToString();
                    if (bool_10)
                    {
                        if ((detail.Amount < 0.0) && (this.ecardType_0 == ECardType.ectDefault))
                        {
                            string str2 = Encoding.GetEncoding("GBK").GetString(detail.OldTypeCode).Trim(new char[] { ' ' });
                            string str3 = BitConverter.ToUInt32(this.byte_0, 0x37 + num7).ToString().PadLeft(8, '0');
                            if (detail.InvType == 0)
                            {
                                str3 = str3.Substring(3, 6);
                                detail.OldInvNo = str2 + str3;
                            }
                            else if (detail.InvType == 2)
                            {
                                detail.OldInvNo = str2 + str3;
                            }
                        }
                        if ((detail.OldTypeCode != null) && (detail.OldTypeCode[0] == 0x4c))
                        {
                            int num11 = BitConverter.ToUInt16(detail.OldTypeCode, 1);
                            int num12 = detail.OldTypeCode[9];
                            num11 += num12 * 0x10000;
                            Class20.smethod_1("一万行清单>>>>解析发票，发票长度：" + num11);
                            Class20.smethod_1("Revert 12BIT:" + BitConverter.ToString(detail.OldTypeCode, 0, 12));
                            detail.OldTypeCode = CommonTool.GetSubArray(this.byte_0, 0x41, num11);
                            Encoding.GetEncoding("GBK").GetString(detail.OldTypeCode);
                            int count = 0;
                            if ((detail.InvType != 0) && (detail.InvType != 2))
                            {
                                if (detail.InvType == 11)
                                {
                                    count = 0x90;
                                }
                                else if (detail.InvType == 12)
                                {
                                    count = 190;
                                }
                                else if (detail.InvType == 0x33)
                                {
                                    count = 0x6c;
                                }
                                else if (detail.InvType == 0x29)
                                {
                                    count = 0x6c;
                                }
                                else
                                {
                                    count = 0;
                                }
                            }
                            else
                            {
                                count = 0x6c;
                            }
                            detail.MW = Encoding.GetEncoding("GBK").GetString(this.byte_0, ((0x41 + num11) - count) - 20, count);
                            Class20.smethod_1(string.Format("===============解析发票明细--截取密文:{0}", detail.MW));
                            detail.JYM = Encoding.GetEncoding("GBK").GetString(this.byte_0, (0x41 + num11) - 20, 20);
                            Class20.smethod_1(string.Format("===============解析发票明细--截取校验码:{0}", detail.JYM));
                            ushort num14 = BitConverter.ToUInt16(this.byte_0, 0x41 + num11);
                            detail.SignBuffer = CommonTool.GetSubArray(this.byte_0, (0x41 + num11) + 2, num14);
                            Class20.smethod_1(string.Format("===============解析发票明细--截取签名:{0}", Encoding.GetEncoding("GBK").GetString(detail.SignBuffer)));
                            if (((this.byte_0[((0x41 + num11) + 2) + num14] != 0xff) || (this.byte_0[(((0x41 + num11) + 2) + num14) + 1] != 0xff)) || ((this.byte_0[(((0x41 + num11) + 2) + num14) + 2] != 0xff) || (this.byte_0[(((0x41 + num11) + 2) + num14) + 3] != 0xff)))
                            {
                                Array.Reverse(this.byte_0, ((0x41 + num11) + 2) + num14, 4);
                                detail.Index = BitConverter.ToUInt32(this.byte_0, ((0x41 + num11) + 2) + num14).ToString();
                                Class20.smethod_1(string.Format("===============解析发票明细--截取索引号:{0}", detail.Index));
                            }
                            try
                            {
                                detail.IsUpload = Convert.ToInt32(this.byte_0[(((0x41 + num11) + 2) + num14) + 4]) == 0;
                                Class20.smethod_1(string.Format("===============解析发票明细--截取上传标志:{0}", detail.IsUpload.ToString()));
                            }
                            catch
                            {
                                detail.IsUpload = false;
                            }
                            try
                            {
                                int year = (this.byte_0[((((0x41 + num11) + 2) + num14) + 4) + 1] * 0x10) + (this.byte_0[((((0x41 + num11) + 2) + num14) + 4) + 2] >> 4);
                                int num17 = this.byte_0[((((0x41 + num11) + 2) + num14) + 4) + 2] & 15;
                                int num18 = this.byte_0[((((0x41 + num11) + 2) + num14) + 4) + 3] >> 3;
                                int hour = ((this.byte_0[((((0x41 + num11) + 2) + num14) + 4) + 3] & 7) * 4) + (this.byte_0[((((0x41 + num11) + 2) + num14) + 4) + 4] >> 6);
                                int minute = this.byte_0[((((0x41 + num11) + 2) + num14) + 4) + 4] & 0x3f;
                                Class20.smethod_1(string.Format("作废时间:年{0}月{1}日{2},时{3}分{4}", new object[] { year, num17, num18, hour, minute }));
                                detail.WasteTime = new DateTime(year, num17, num18, hour, minute, 0);
                                Class20.smethod_1(string.Format("===============解析发票明细--截取作废时间:{0}", detail.WasteTime.ToLongDateString()));
                            }
                            catch (Exception)
                            {
                            }
                            if (((this.byte_0[(((0x41 + num11) + 2) + num14) + 9] != 0xff) || (this.byte_0[(((0x41 + num11) + 2) + num14) + 10] != 0xff)) || ((this.byte_0[(((0x41 + num11) + 2) + num14) + 11] != 0xff) || (this.byte_0[(((0x41 + num11) + 2) + num14) + 12] != 0xff)))
                            {
                                detail.InvSqeNo = BitConverter.ToUInt32(this.byte_0, (((0x41 + num11) + 2) + num14) + 9).ToString();
                            }
                        }
                        int num22 = 0x2d + "CardInv".Length;
                        if (this.dateTime_3 > DateTime.Parse("2006-06-01"))
                        {
                            if (this.blQDEWM())
                            {
                                byte num23 = this.byte_0[0x463];
                                int num24 = num23;
                                num22 = (num24 * 0x200) + "CardInv".Length;
                            }
                            else
                            {
                                num22 = 0x800 + "CardInv".Length;
                            }
                        }
                        if (Encoding.GetEncoding("GBK").GetString(this.byte_0, 0x3e8, "CardInv".Length) == "CardInv")
                        {
                            byte[] buffer2 = CommonTool.GetSubArray(this.byte_0, 0x3e8, num22);
                            detail.OldTypeCode = buffer2;
                        }
                    }
                }
                else
                {
                    detail.OldTypeCode = new byte[1];
                    Class20.smethod_2(string.Format("GetInvDetailBase 查询本期的RetCode={0}", this.RetCode));
                }
                if (detail.OldTypeCode == null)
                {
                    detail.OldTypeCode = new byte[1];
                }
                Class20.smethod_1(string.Format("=============================================================解析发票明细结束--返回值:{0}", this.RetCode));
                detail2 = detail;
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
                throw;
            }
            return detail2;
        }

        private void method_23()
        {
            int num = BitConverter.ToUInt16(this.byte_0, 0);
            int startIndex = 2;
            for (int i = 0; i < num; i++)
            {
                InvVolumeApp item = new InvVolumeApp {
                    HeadCode = BitConverter.ToUInt32(this.byte_0, startIndex),
                    Number = BitConverter.ToUInt16(this.byte_0, 4 + startIndex)
                };
                ushort year = BitConverter.ToUInt16(this.byte_0, 12 + startIndex);
                byte month = this.byte_0[6 + startIndex];
                byte day = this.byte_0[8 + startIndex];
                byte hour = this.byte_0[10 + startIndex];
                try
                {
                    item.BuyDate = new DateTime(year, month, day, hour, 0, 0);
                }
                catch
                {
                }
                byte[] buffer = new byte[5];
                for (int j = 0; j < 5; j++)
                {
                    buffer[j] = this.byte_0[(14 + startIndex) + j];
                }
                if ((buffer[0] & 0xc0) != 0xc0)
                {
                    item.TypeCode = this.method_50(buffer, 5);
                }
                else
                {
                    item.TypeCode = this.method_54(buffer);
                }
                startIndex += 0x13;
                if (this.ctaxCardType_0 >= CTaxCardType.tctCommonInv)
                {
                    item.InvType = this.byte_0[startIndex];
                    item.InvLimit = CommonTool.ToPow10(this.byte_0[1 + startIndex]);
                    item.SaledDate = CommonTool.AcqTaxDateTime(this.byte_0[2 + startIndex], this.byte_0[3 + startIndex], this.byte_0[4 + startIndex], this.byte_0[5 + startIndex]);
                    item.BuyNumber = BitConverter.ToUInt16(this.byte_0, 6 + startIndex);
                    item.Status = (char) (this.byte_0[8 + startIndex] + 0x36);
                    startIndex += 9;
                }
                this.list_0.Add(item);
            }
        }

        private int method_24()
        {
            int sysMonth = this.SysMonth;
            if (this.ecardType_0 != ECardType.ectDefault)
            {
                return this.method_25();
            }
            int sysYear = this.SysYear;
            int num3 = this.SysMonth;
            while (true)
            {
                num3--;
                if (num3 == 0)
                {
                    sysYear = this.SysYear - 1;
                    for (int i = 12; i > 0; i--)
                    {
                        if (this.method_31(num3, sysYear) != 0)
                        {
                            return ((i - 12) + 1);
                        }
                    }
                    return sysMonth;
                }
                if (this.method_31(num3, sysYear) != 0)
                {
                    return (num3 + 1);
                }
            }
        }

        private int method_25()
        {
            int num = 1;
            while (num < 14)
            {
                byte[] buffer = this.method_64(4, num, null);
                this.int_4 = this.method_63(buffer, this.byte_0);
                Class20.smethod_2("GetInvCount() retCode=" + this.int_4);
                if (this.RetCode != 0)
                {
                    break;
                }
                BitConverter.ToUInt16(this.byte_0, 0);
                num++;
            }
            return ((this.SysMonth - num) + 2);
        }

        public RegResult method_26()
        {
            RegResult fail = RegResult.fail;
            try
            {
                IntPtr ptr;
                MessageShow.MsgWait("正在注册报税盘...");
                this.method_28();
                if (this.StateInfo.ushort_10 == 1)
                {
                    fail = RegResult.hasReged;
                    return RegResult.hasReged;
                }
                byte[] buffer = new byte[] { 2, 1 };
                byte[] buffer5 = new byte[4];
                buffer5[0] = 4;
                buffer5[1] = 1;
                byte[] buffer2 = buffer5;
                this.method_62(buffer2, out ptr);
                List<string> inList = new List<string>();
                int length = buffer.Length;
                for (int i = 0; i < length; i++)
                {
                    Marshal.WriteByte(ptr, i, buffer[i]);
                }
                inList.Add("7");
                inList.Add(length.ToString());
                byte[] buffer3 = this.method_64(0x15, 0, inList);
                this.int_4 = this.method_63(buffer3, this.byte_0);
                if (this.RetCode == 0)
                {
                    this.method_28();
                    if (this.StateInfo.ushort_10 == 1)
                    {
                        fail = RegResult.success;
                    }
                }
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
            }
            finally
            {
                MessageShow.MsgWait();
            }
            return fail;
        }

        public bool method_27()
        {
            if (this.bool_7)
            {
                return this.class17_0.method_11();
            }
            return true;
        }

        private TaxStateInfo method_28()
        {
            if (this.method_4() == DrvState.dsOpen)
            {
                this.method_42();
                byte[] buffer2 = this.method_64(0x10, 0, null);
                Class20.smethod_1(">>>>>>>GetStateInfoBase>>>>>>>>1");
                this.int_4 = this.method_63(buffer2, this.byte_0);
                Class20.smethod_1(">>>>>>>GetStateInfoBase>>>>>>>>2");
                if (this.RetCode == 0)
                {
                    this.taxStateInfo_0.IsInvEmpty = this.byte_0[0];
                    this.taxStateInfo_0.IsRepReached = this.byte_0[1];
                    this.taxStateInfo_0.IsLockReached = this.byte_0[2];
                    this.taxStateInfo_0.IsMainMachine = this.byte_0[3];
                    this.taxStateInfo_0.MachineNumber = this.byte_0[4];
                    this.taxStateInfo_0.IsWithChild = this.byte_0[5];
                    this.taxStateInfo_0.MajorVersion = this.byte_0[6];
                    this.taxStateInfo_0.MinorVersion = this.byte_0[7];
                    Class20.smethod_1(string.Format("专普票：已到报税期{0},已到锁死期{1}，是主开票机{2}", this.taxStateInfo_0.IsRepReached, this.taxStateInfo_0.IsMainMachine, this.taxStateInfo_0.MachineNumber));
                    this.taxStateInfo_0.LockedDays = this.byte_0[8];
                    this.taxStateInfo_0.ICCardNo = this.byte_0[9];
                    Class20.smethod_1("卡对应开票机号:" + this.taxStateInfo_0.ICCardNo.ToString());
                    Class20.smethod_1("本机开票机号：" + this.Machine.ToString());
                    this.taxStateInfo_0.ICBuyInv = this.byte_0[10];
                    this.taxStateInfo_0.ushort_0 = this.byte_0[11];
                    this.taxStateInfo_0.ICRetInv = this.byte_0[12];
                    this.taxStateInfo_0.ushort_1 = this.byte_0[13];
                    this.taxStateInfo_0.ushort_2 = this.byte_0[14];
                    this.taxStateInfo_0.ushort_3 = this.byte_0[15];
                    if (this.byte_0[0x21] == 0xfe)
                    {
                        this.taxStateInfo_0.ushort_10 = 1;
                        this.taxStateInfo_0.IsInvEmpty = this.byte_0[0x11];
                        this.taxStateInfo_0.IsRepReached = this.byte_0[0x12];
                        this.taxStateInfo_0.IsLockReached = this.byte_0[0x13];
                        this.taxStateInfo_0.IsMainMachine = this.byte_0[20];
                        this.taxStateInfo_0.MachineNumber = this.byte_0[0x15];
                        this.taxStateInfo_0.IsWithChild = this.byte_0[0x16];
                        this.taxStateInfo_0.MajorVersion = this.byte_0[0x17];
                        this.taxStateInfo_0.MinorVersion = this.byte_0[0x18];
                        this.taxStateInfo_0.LockedDays = this.byte_0[0x19];
                        this.taxStateInfo_0.TBCardNo = this.byte_0[0x1a];
                        this.taxStateInfo_0.TBBuyInv = this.byte_0[0x1b];
                        this.taxStateInfo_0.ushort_7 = this.byte_0[0x1c];
                        this.taxStateInfo_0.TBRetInv = this.byte_0[0x1d];
                        this.taxStateInfo_0.ushort_8 = this.byte_0[30];
                        this.taxStateInfo_0.ushort_9 = this.byte_0[0x1f];
                        this.taxStateInfo_0.ushort_5 = (ushort) (this.byte_0[0x20] * 0x80);
                        this.taxStateInfo_0.ushort_6 = (this.byte_0[0x20] > 0) ? ((ushort) 1) : ((ushort) 0);
                    }
                    byte[] buffer = new byte[0xd0];
                    for (int i = 0; i < 0xd0; i++)
                    {
                        buffer[i] = this.byte_0[i + 0x27];
                    }
                    Class20.smethod_1("取金税卡状态:" + BitConverter.ToString(buffer));
                    this.taxStateInfo_0.InvTypeInfo.Clear();
                    for (int j = 0; j < 8; j++)
                    {
                        int index = j * 0x1a;
                        InvTypeInfo item = new InvTypeInfo {
                            InvType = buffer[index]
                        };
                        if (((item.InvType == 11) || (item.InvType == 12)) || ((item.InvType == 0x33) || (item.InvType == 0x29)))
                        {
                            Class20.smethod_1("GetStateInfo:" + item.InvType.ToString());
                            item.IsRepTime = buffer[index + 1];
                            item.IsLockTime = buffer[index + 2];
                            item.ushort_0 = buffer[index + 3];
                            item.ushort_1 = buffer[index + 4];
                            Class20.smethod_1(string.Format("货运机动车：已到报税期{0},已到锁死期{1}，票种{2}", item.IsRepTime, item.IsLockTime, item.InvType));
                            item.ICBuyInv = buffer[index + 5];
                            item.ICRetInv = buffer[index + 6];
                            item.Reserve = buffer[index + 7];
                            item.LastRepDate = BitConverter.ToString(buffer, index + 8, 1) + BitConverter.ToString(buffer, (index + 8) + 1, 1) + "-" + BitConverter.ToString(buffer, (index + 8) + 2, 1) + "-" + BitConverter.ToString(buffer, (index + 8) + 3, 1) + " " + BitConverter.ToString(buffer, (index + 8) + 4, 1) + ":" + BitConverter.ToString(buffer, (index + 8) + 5, 1);
                            item.NextRepDate = BitConverter.ToString(buffer, index + 14, 1) + BitConverter.ToString(buffer, (index + 14) + 1, 1) + "-" + BitConverter.ToString(buffer, (index + 14) + 2, 1) + "-" + BitConverter.ToString(buffer, (index + 14) + 3, 1);
                            item.LockedDate = BitConverter.ToString(buffer, index + 0x12, 1) + BitConverter.ToString(buffer, (index + 0x12) + 1, 1) + "-" + BitConverter.ToString(buffer, (index + 0x12) + 2, 1) + "-" + BitConverter.ToString(buffer, (index + 0x12) + 3, 1);
                            item.ushort_2 = buffer[index + 0x16];
                            item.ushort_3 = buffer[index + 0x17];
                            item.ushort_4 = buffer[index + 0x18];
                            item.ushort_5 = buffer[index + 0x19];
                            this.taxStateInfo_0.InvTypeInfo.Add(item);
                        }
                    }
                    this.taxStateInfo_0.ushort_4 = this.byte_0[0xf7];
                    ushort num2 = Convert.ToUInt16((int) (this.byte_0[0x3e8] * 0x100));
                    Class20.smethod_1("1000bit value is:" + BitConverter.ToString(this.byte_0, 0x3e8, 1));
                    Class20.smethod_1("hVal value is:" + num2.ToString());
                    this.taxStateInfo_0.MachineNumber = Convert.ToUInt16((int) (this.taxStateInfo_0.MachineNumber + num2));
                    if (this.taxStateInfo_0.IsMainMachine != 0)
                    {
                        this.int_2 = 0;
                    }
                    else
                    {
                        this.int_2 = this.taxStateInfo_0.MachineNumber;
                    }
                    Class20.smethod_1(string.Format("状态查询：是否主机{0}，开票机号{1}，开票机数量{2}", this.taxStateInfo_0.IsMainMachine, this.int_2, this.taxStateInfo_0.MachineNumber));
                }
            }
            InvTypeInfo info = this.taxStateInfo_0.method_0(InvoiceType.transportation);
            Class20.smethod_1(string.Format("货运刷新状态：上个抄税日{0},下个抄税日{1}，锁死日{2}", info.LastRepDate, info.NextRepDate, info.LockedDate));
            InvTypeInfo info2 = this.taxStateInfo_0.method_0(InvoiceType.vehiclesales);
            Class20.smethod_1(string.Format("机动车刷新状态：上个抄税日{0},下个抄税日{1}，锁死日{2}", info2.LastRepDate, info2.NextRepDate, info2.LockedDate));
            return this.taxStateInfo_0;
        }

        private List<TaxRepCommInfo> method_29(int int_7, int int_8)
        {
            List<TaxRepCommInfo> list = new List<TaxRepCommInfo>();
            int month = this.dateTime_5.Month;
            int year = this.dateTime_5.Year;
            int_7 = (int_7 == 0) ? month : int_7;
            int_8 = (int_8 == 0) ? year : int_8;
            List<string> inList = new List<string>();
            string item = ((int_8 * 100) + int_7).ToString();
            inList.Add(item);
            byte[] buffer = this.method_64(7, 0, inList);
            this.int_4 = this.method_63(buffer, this.byte_0);
            if (this.RetCode == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    TaxRepCommInfo info = new TaxRepCommInfo();
                    ushort num4 = Convert.ToUInt16(this.byte_0[i * 0xb1]);
                    switch (num4)
                    {
                        case 0:
                        case 2:
                        case 11:
                        case 12:
                        case 0x33:
                        case 0x29:
                            info.InvType = num4;
                            info.InvWasteCount = BitConverter.ToUInt32(this.byte_0, (i * 0xb1) + 1);
                            info.InvWasteAmount = Convert.ToDouble(CommonTool.GetStringFromBuffer(this.byte_0, ((i * 0xb1) + 1) + 4, 20));
                            info.InvWasteTaxAmount = Convert.ToDouble(CommonTool.GetStringFromBuffer(this.byte_0, ((i * 0xb1) + 1) + 0x18, 20));
                            info.InvCount = BitConverter.ToUInt32(this.byte_0, ((i * 0xb1) + 1) + 0x2c);
                            info.InvAmount = Convert.ToDouble(CommonTool.GetStringFromBuffer(this.byte_0, ((i * 0xb1) + 1) + 0x30, 20));
                            info.InvTaxAmount = Convert.ToDouble(CommonTool.GetStringFromBuffer(this.byte_0, ((i * 0xb1) + 1) + 0x44, 20));
                            info.NavInvWasteCount = BitConverter.ToUInt32(this.byte_0, ((i * 0xb1) + 1) + 0x58);
                            info.NavInvWasteAmount = Convert.ToDouble(CommonTool.GetStringFromBuffer(this.byte_0, ((i * 0xb1) + 1) + 0x5c, 20));
                            info.NavInvWasteTaxAmount = Convert.ToDouble(CommonTool.GetStringFromBuffer(this.byte_0, ((i * 0xb1) + 1) + 0x70, 20));
                            info.NavInvCount = BitConverter.ToUInt32(this.byte_0, ((i * 0xb1) + 1) + 0x84);
                            info.NavInvAmount = Convert.ToDouble(CommonTool.GetStringFromBuffer(this.byte_0, ((i * 0xb1) + 1) + 0x88, 20));
                            info.NavInvTaxAmount = Convert.ToDouble(CommonTool.GetStringFromBuffer(this.byte_0, ((i * 0xb1) + 1) + 0x9c, 20));
                            list.Add(info);
                            break;
                    }
                }
            }
            return list;
        }

        private int method_3()
        {
            int num = 0;
            if (this.dateTime_6.Day <= 15)
            {
                num = this.dateTime_6.Month - 1;
            }
            if (num < 1)
            {
                num = 12;
            }
            return num;
        }

        private List<InvVolume> method_30(int int_7, int int_8)
        {
            if (int_8 < this.LastRepDateMonth)
            {
                throw new ArgumentException("月份不能到上次抄税月之前");
            }
            this.GetMonthStatPeriod(int_7);
            this.GetInvStock();
            SortedDictionary<string, InvVolume> dictionary = new SortedDictionary<string, InvVolume>();
            List<InvVolumeApp> volumns = CommonTool.GetVolumns();
            for (int i = 0; i < volumns.Count; i++)
            {
                InvVolumeApp app = volumns[i];
                InvVolume volume = new InvVolume {
                    InvType = (InvoiceType) volumns[i].InvType,
                    TypeCode = app.TypeCode,
                    HeadCode = app.HeadCode,
                    EndCode = app.HeadCode + Convert.ToUInt32(app.BuyNumber)
                };
                if (volumns[i].BuyDate.Month <= int_8)
                {
                    if ((volumns[i].BuyDate.Month >= int_8) && (volumns[i].BuyDate.Month == int_8))
                    {
                        volume.PrdThisBuyNum = app.BuyNumber;
                        volume.PrdThisBuyNO = app.HeadCode.ToString("00000000");
                    }
                    string key = string.Concat(new object[] { volumns[i].InvType, volume.TypeCode, "|", volume.HeadCode });
                    if (!dictionary.ContainsKey(key))
                    {
                        dictionary.Add(key, volume);
                    }
                }
            }
            for (int j = 0; j < this.list_0.Count; j++)
            {
                InvVolumeApp app2 = this.list_0[j];
                if (app2.BuyDate.Month <= int_8)
                {
                    uint num3 = (app2.HeadCode - Convert.ToUInt32(app2.BuyNumber)) + Convert.ToUInt32(app2.Number);
                    string str2 = string.Concat(new object[] { app2.InvType, app2.TypeCode, "|", num3 });
                    if (dictionary.ContainsKey(str2))
                    {
                        InvVolume volume2 = dictionary[str2];
                        volume2.PrdEndStockNO = app2.HeadCode.ToString().PadLeft(8, '0');
                        volume2.PrdEndStockNum = app2.Number;
                        long num8 = app2.HeadCode - (app2.BuyNumber - volume2.PrdEndStockNum);
                        volume2.PrdEarlyStockNO = num8.ToString().PadLeft(8, '0');
                        volume2.PrdThisBuyNum = app2.BuyNumber;
                        volume2.PrdThisBuyNO = volume2.PrdEarlyStockNO;
                        if (volume2.PrdThisBuyNum > 0)
                        {
                            volume2.PrdEarlyStockNO = "";
                        }
                    }
                    else
                    {
                        InvVolume volume3 = new InvVolume {
                            InvType = (InvoiceType) app2.InvType,
                            TypeCode = app2.TypeCode,
                            HeadCode = (app2.HeadCode - Convert.ToUInt32(app2.BuyNumber)) + Convert.ToUInt32(app2.Number),
                            EndCode = app2.HeadCode + Convert.ToUInt32(app2.BuyNumber),
                            PrdEndStockNO = app2.HeadCode.ToString().PadLeft(8, '0'),
                            PrdEndStockNum = app2.Number
                        };
                        long num9 = app2.HeadCode - (app2.BuyNumber - volume3.PrdEndStockNum);
                        volume3.PrdEarlyStockNO = num9.ToString().PadLeft(8, '0');
                        volume3.PrdThisBuyNum = app2.BuyNumber;
                        volume3.PrdThisBuyNO = volume3.PrdEarlyStockNO;
                        if (volume3.PrdThisBuyNum > 0)
                        {
                            volume3.PrdEarlyStockNO = "";
                        }
                        dictionary.Add(str2, volume3);
                    }
                }
            }
            List<InvVolume> list2 = new List<InvVolume>();
            foreach (KeyValuePair<string, InvVolume> pair in dictionary)
            {
                InvVolume volume4 = pair.Value;
                list2.Add(volume4);
            }
            long invCount = this.GetInvCount(int_7, int_8);
            for (int k = 0; k < invCount; k++)
            {
                InvVolume volume5;
                InvDetail detail = this.method_19((long) k);
                int num6 = 0;
                while (num6 < list2.Count)
                {
                    if (((detail.InvType == ((ushort) list2[num6].InvType)) && (detail.TypeCode == list2[num6].TypeCode)) && ((detail.InvNo >= list2[num6].HeadCode) && (detail.InvNo < list2[num6].EndCode)))
                    {
                        goto Label_0487;
                    }
                    num6++;
                }
                continue;
            Label_0487:
                volume5 = list2[num6];
                if (detail.CancelFlag)
                {
                    volume5.WasteNum++;
                    volume5.WasteNO = volume5.WasteNO + detail.InvNo.ToString().PadLeft(8, '0') + " ";
                }
                if (volume5.PrdThisIssueNO.Length == 0)
                {
                    volume5.PrdThisIssueNO = detail.InvNo.ToString().PadLeft(8, '0');
                    if (volume5.PrdThisBuyNum == 0)
                    {
                        volume5.PrdEarlyStockNO = volume5.PrdThisIssueNO;
                    }
                }
                volume5.PrdThisIssueNum++;
                if (volume5.PrdThisBuyNum == 0)
                {
                    volume5.PrdEarlyStockNum++;
                }
            }
            InvVolume item = new InvVolume();
            string str3 = "";
            for (int m = 0; m < list2.Count; m++)
            {
                string str4 = ((int) list2[m].InvType) + list2[m].TypeCode;
                if (!(str3 == str4) && (str3.Length != 0))
                {
                    InvVolume volume7 = new InvVolume {
                        InvType = item.InvType,
                        TypeCode = item.TypeCode,
                        PrdEarlyStockNum = item.PrdEarlyStockNum,
                        PrdThisBuyNum = item.PrdThisBuyNum,
                        PrdThisIssueNum = item.PrdThisIssueNum,
                        WasteNum = item.WasteNum,
                        MistakeNum = item.MistakeNum,
                        PrdEndStockNum = item.PrdEndStockNum
                    };
                    item.PrdEarlyStockNum = 0;
                    item.PrdThisBuyNum = 0;
                    item.PrdThisIssueNum = 0;
                    item.WasteNum = 0;
                    item.MistakeNum = 0;
                    item.PrdEndStockNum = 0;
                    list2.Insert(m, volume7);
                }
                else
                {
                    item.InvType = list2[m].InvType;
                    item.TypeCode = "小计";
                    item.PrdEarlyStockNum += list2[m].PrdEarlyStockNum;
                    item.PrdThisBuyNum += list2[m].PrdThisBuyNum;
                    item.PrdThisIssueNum += list2[m].PrdThisIssueNum;
                    item.WasteNum += list2[m].WasteNum;
                    item.MistakeNum += list2[m].MistakeNum;
                    item.PrdEndStockNum += list2[m].PrdEndStockNum;
                }
                str3 = str4;
            }
            list2.Add(item);
            return list2;
        }

        private int method_31(int int_7, int int_8)
        {
            int month = this.dateTime_5.Month;
            int year = this.dateTime_5.Year;
            Class20.smethod_1("_ftaxClock.Month：" + month.ToString());
            Class20.smethod_1("_ftaxClock.Year：" + year.ToString());
            Class20.smethod_1("input Month：" + int_7.ToString());
            Class20.smethod_1("input Year：" + int_8.ToString());
            int_7 = (int_7 == 0) ? month : int_7;
            int_8 = (int_8 == 0) ? year : int_8;
            Class20.smethod_1("result Month：" + int_7.ToString());
            Class20.smethod_1("result Year：" + int_8.ToString());
            List<string> inList = new List<string>();
            string item = ((int_8 * 100) + int_7).ToString();
            inList.Add(item);
            byte[] buffer = this.method_64(8, 0, inList);
            Class20.smethod_1("功能号8调用，参数：" + item);
            Class20.smethod_1("功能号8调用，参数BUF：" + BitConverter.ToString(buffer, 0, 50));
            return this.method_63(buffer, this.byte_0);
        }

        private void method_32(InvDetail invDetail_0, InvAmountTaxStati invAmountTaxStati_0, int int_7)
        {
            switch (int_7)
            {
                case 0:
                    this.method_33(invDetail_0, invAmountTaxStati_0.AmountTax_0);
                    return;

                case 1:
                    this.method_33(invDetail_0, invAmountTaxStati_0.AmountTax_1);
                    return;

                case 2:
                    this.method_33(invDetail_0, invAmountTaxStati_0.TaxClass6);
                    return;

                case 3:
                    this.method_33(invDetail_0, invAmountTaxStati_0.TaxClass4);
                    return;

                case 4:
                case 5:
                    this.method_33(invDetail_0, invAmountTaxStati_0.TaxClassOther);
                    return;
            }
        }

        private void method_33(InvDetail invDetail_0, AmountTax amountTax_0)
        {
            if (invDetail_0.Amount > 0.0)
            {
                amountTax_0.XXZSJE += invDetail_0.Amount;
                amountTax_0.XXZSSE += invDetail_0.Tax;
            }
            else
            {
                amountTax_0.XXFSJE -= invDetail_0.Amount;
                amountTax_0.XXFSSE -= invDetail_0.Tax;
            }
            if (invDetail_0.CancelFlag)
            {
                if (invDetail_0.Amount > 0.0)
                {
                    amountTax_0.XXZFJE += invDetail_0.Amount;
                    amountTax_0.XXZFSE += invDetail_0.Tax;
                }
                else
                {
                    amountTax_0.XXFFJE -= invDetail_0.Amount;
                    amountTax_0.XXFFSE -= invDetail_0.Tax;
                }
            }
            amountTax_0.SJXSJE = ((amountTax_0.XXZSJE - amountTax_0.XXZFJE) - amountTax_0.XXFSJE) + amountTax_0.XXFFJE;
            amountTax_0.SJXXSE = ((amountTax_0.XXZSSE - amountTax_0.XXZFSE) - amountTax_0.XXFSSE) + amountTax_0.XXFFSE;
        }

        private void method_34(InvDetail invDetail_0, AmountTax amountTax_0, double double_0, double double_1)
        {
            if (invDetail_0.Amount > 0.0)
            {
                amountTax_0.XXZSJE += double_0;
                amountTax_0.XXZSSE += double_1;
            }
            else
            {
                amountTax_0.XXFSJE -= double_0;
                amountTax_0.XXFSSE -= double_1;
            }
            if (invDetail_0.CancelFlag)
            {
                if (invDetail_0.Amount > 0.0)
                {
                    amountTax_0.XXZFJE += double_0;
                    amountTax_0.XXZFSE += double_1;
                }
                else
                {
                    amountTax_0.XXFFJE -= double_0;
                    amountTax_0.XXFFSE -= double_1;
                }
            }
            amountTax_0.SJXSJE = ((amountTax_0.XXZSJE - amountTax_0.XXZFJE) - amountTax_0.XXFSJE) + amountTax_0.XXFFJE;
            amountTax_0.SJXXSE = ((amountTax_0.XXZSSE - amountTax_0.XXZFSE) - amountTax_0.XXFSSE) + amountTax_0.XXFFSE;
        }

        public string method_35(string string_35)
        {
            byte[] bytes = Encoding.GetEncoding("GBK").GetBytes(string_35);
            byte[] buffer2 = this.method_36(bytes);
            return Encoding.GetEncoding("GBK").GetString(buffer2);
        }

        public byte[] method_36(byte[] byte_1)
        {
            IntPtr ptr;
            byte[] buffer = new byte[4];
            buffer[0] = 4;
            buffer[1] = 1;
            byte[] buffer2 = buffer;
            this.method_62(buffer2, out ptr);
            List<string> inList = new List<string>();
            int length = byte_1.Length;
            for (int i = 0; i < length; i++)
            {
                Marshal.WriteByte(ptr, i, byte_1[i]);
            }
            inList.Add("1");
            inList.Add(length.ToString());
            byte[] buffer3 = this.method_64(0x15, 0, inList);
            this.int_4 = this.method_63(buffer3, this.byte_0);
            if (this.RetCode == 0)
            {
                int num3 = BitConverter.ToUInt16(this.byte_0, 0);
                return CommonTool.GetSubArray(this.byte_0, 2, num3);
            }
            return new byte[1];
        }

        public byte[] method_37(byte[] byte_1)
        {
            IntPtr ptr;
            byte[] buffer = new byte[4];
            buffer[0] = 4;
            buffer[1] = 1;
            byte[] buffer2 = buffer;
            this.method_62(buffer2, out ptr);
            List<string> inList = new List<string>();
            int length = byte_1.Length;
            for (int i = 0; i < length; i++)
            {
                Marshal.WriteByte(ptr, i, byte_1[i]);
            }
            inList.Add("2");
            inList.Add(length.ToString());
            byte[] buffer4 = this.method_64(0x15, 0, inList);
            this.int_4 = this.method_63(buffer4, this.byte_0);
            if (this.RetCode == 0)
            {
                int num3 = BitConverter.ToUInt16(this.byte_0, 0);
                return CommonTool.GetSubArray(this.byte_0, 2, num3);
            }
            return new byte[1];
        }

        public string method_38(string string_35, int int_7, byte[] byte_1)
        {
            string str = "";
            try
            {
                byte num6;
                if (string_35.Length < 10)
                {
                    string_35 = string_35.PadLeft(10, '0');
                }
                else if (string_35.Length > 10)
                {
                    int startIndex = string_35.Length - 10;
                    string_35 = string_35.Substring(startIndex, 10);
                }
                string str6 = int_7.ToString().PadLeft(8, '0').Substring(2, 6);
                string str4 = string_35 + str6;
                byte[] buffer3 = new byte[str4.Length];
                for (int i = 0; i < str4.Length; i++)
                {
                    buffer3[i] = (byte) str4[i];
                }
                byte[] buffer = this.method_36(buffer3);
                IDEA idea = new IDEA();
                byte[] buffer2 = new byte[] { 0x31, 50, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30, 0x31, 50, 0x33, 0x34, 0x35, 0x36 };
                for (int j = 0; j < 15; j++)
                {
                    buffer[j] = (byte) (buffer[j] + buffer2[j]);
                }
                for (int k = 0; k < 15; k++)
                {
                    buffer3[k] = (byte) (buffer3[k] + buffer2[k]);
                }
                idea.GetEncryptoKey(buffer);
                idea.MakeDecryptoKey();
                byte[] bytes = byte_1;
                int length = bytes.Length;
                if (length != 0)
                {
                    goto Label_0153;
                }
                return Encoding.GetEncoding("GBK").GetString(bytes);
            Label_0148:
                if (length <= 8)
                {
                    goto Label_0160;
                }
                length--;
            Label_0153:
                if (bytes[length - 1] == 0x20)
                {
                    goto Label_0148;
                }
            Label_0160:
                num6 = bytes[length - 1];
                if (num6 >= 0x20)
                {
                    return Encoding.GetEncoding("GBK").GetString(bytes);
                }
                byte[] buffer5 = CommonTool.GetSubArray(bytes, 0, 8 * num6);
                str = idea.IdeaCrypto(buffer5, false);
            }
            catch (Exception)
            {
            }
            return str;
        }

        public List<string> method_39(string string_35, int int_7, List<byte[]> CipherList)
        {
            List<string> list = null;
            try
            {
                Class20.smethod_1(">>>>>>>>>>>>>>>解密开始，类别代码" + string_35);
                Class20.smethod_1(">>>>>>>>>>>>>>>解密开始，发票号码" + int_7.ToString());
                Class20.smethod_1(">>>>>>>>>>>>>>>解密开始，共几条数据" + CipherList.Count.ToString());
                list = new List<string>();
                if (CipherList.Count == 0)
                {
                    return list;
                }
                if (string_35.Length < 8)
                {
                    string_35 = string_35.PadLeft(10, '0');
                }
                else if (string_35.Length > 10)
                {
                    int startIndex = string_35.Length - 10;
                    string_35 = string_35.Substring(startIndex, 10);
                }
                string str = int_7.ToString().PadLeft(8, '0').Substring(2, 6);
                string str2 = string_35 + str;
                Class20.smethod_1(">>>>>>>>>>>>>>>密钥:" + str2);
                byte[] buffer = new byte[str2.Length];
                for (int i = 0; i < str2.Length; i++)
                {
                    buffer[i] = (byte) str2[i];
                }
                byte[] buffer2 = this.method_36(buffer);
                IDEA idea = new IDEA();
                byte[] buffer3 = new byte[] { 0x31, 50, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30, 0x31, 50, 0x33, 0x34, 0x35, 0x36 };
                for (int j = 0; j < 15; j++)
                {
                    buffer2[j] = (byte) (buffer2[j] + buffer3[j]);
                }
                Class20.smethod_1(">>>>>>>>>>>>>>>密钥转换开始。");
                idea.GetEncryptoKey(buffer2);
                idea.MakeDecryptoKey();
                Class20.smethod_1(">>>>>>>>>>>>>>>密钥转换结束");
                for (int k = 0; k < CipherList.Count; k++)
                {
                    byte num7;
                    string item = "";
                    byte[] bytes = CipherList[k];
                    int length = bytes.Length;
                    if (length != 0)
                    {
                        goto Label_01BD;
                    }
                    item = Encoding.GetEncoding("GBK").GetString(bytes);
                    list.Add(item);
                    continue;
                Label_01B2:
                    if (length <= 8)
                    {
                        goto Label_01CA;
                    }
                    length--;
                Label_01BD:
                    if (bytes[length - 1] == 0x20)
                    {
                        goto Label_01B2;
                    }
                Label_01CA:
                    num7 = bytes[length - 1];
                    if (num7 >= 0x20)
                    {
                        item = Encoding.GetEncoding("GBK").GetString(bytes);
                        list.Add(item);
                    }
                    else
                    {
                        Class20.smethod_1(">>>>>>>>>>>>>>>解密开始");
                        byte[] buffer5 = CommonTool.GetSubArray(bytes, 0, 8 * num7);
                        item = idea.IdeaCrypto(buffer5, false);
                        list.Add(item);
                        if (string.IsNullOrEmpty(item))
                        {
                            Class20.smethod_1(string.Format("解密第{0}个为空", k.ToString()));
                        }
                        else
                        {
                            Class20.smethod_1(string.Format("解密第{0}个值为：{1}", k.ToString(), item));
                        }
                        Class20.smethod_1(">>>>>>>>>>>>>>>解密结束");
                    }
                }
            }
            catch (Exception)
            {
                Class20.smethod_1(">>>>>>>>>>>>>>>解密异常");
            }
            return list;
        }

        private DrvState method_4()
        {
            byte[] buffer = new byte[0x11800];
            byte[] buffer2 = this.method_64(20, 0, null);
            //逻辑修改
            return DrvState.dsFirstPasswd;
            switch (this.method_63(buffer2, buffer))
            {
                case 1:
                    return DrvState.dsFirstPasswd;

                case 2:
                    return DrvState.dsSecondPasswd;

                case 3:
                    return DrvState.dsInvReturn;

                case 4:
                    return DrvState.dsInvAllot;

                case 5:
                    return DrvState.dsOpen;
            }
            return DrvState.dsClose;
        }

        private DateTime method_40(byte[] byte_1, int int_7)
        {
            int year = (byte_1[0] * 0x10) + (byte_1[1] >> 4);
            int month = byte_1[1] & 15;
            int day = byte_1[2] >> 3;
            int hour = ((byte_1[2] & 7) * 4) + (byte_1[3] >> 6);
            int minute = byte_1[3] & 0x3f;
            Class20.smethod_1(string.Format("作废时间:年{0}月{1}日{2},时{3}分{4}", new object[] { year, month, day, hour, minute }));
            DateTime time = new DateTime();
            try
            {
                time = new DateTime(year, month, day, hour, minute, 0);
            }
            catch (Exception)
            {
            }
            return time;
        }

        public List<string> method_41(int int_7)
        {
            List<string> inList = new List<string>();
            List<string> list2 = new List<string> { "v", "1" };
            byte[] buffer = this.method_64(0x15, 0, inList);
            buffer[0x3e8] = Convert.ToByte(int_7);
            byte[] buffer2 = new byte[0x11800];
            this.int_4 = this.method_63(buffer, buffer2);
            if (this.int_4 == 0)
            {
                try
                {
                    DateTime time = new DateTime(Convert.ToInt32(BitConverter.ToString(buffer2, 2, 1) + BitConverter.ToString(buffer2, 3, 1)), Convert.ToInt32(BitConverter.ToString(buffer2, 4, 1)), Convert.ToInt32(BitConverter.ToString(buffer2, 5, 1)), Convert.ToInt32(BitConverter.ToString(buffer2, 6, 1)), Convert.ToInt32(BitConverter.ToString(buffer2, 7, 1)), 0);
                    string item = BitConverter.ToString(buffer2, 8, 1) + BitConverter.ToString(buffer2, 9, 1) + "-" + BitConverter.ToString(buffer2, 10, 1) + "-" + BitConverter.ToString(buffer2, 11, 1);
                    string str2 = BitConverter.ToString(buffer2, 12, 1) + BitConverter.ToString(buffer2, 13, 1) + "-" + BitConverter.ToString(buffer2, 14, 1) + "-" + BitConverter.ToString(buffer2, 15, 1);
                    list2.Add(time.ToString());
                    list2.Add(item);
                    list2.Add(str2);
                    if ((int_7 != 0) && (int_7 != 2))
                    {
                        if (((int_7 == 11) || (int_7 == 12)) || ((int_7 == 0x33) || (int_7 == 0x29)))
                        {
                            if (int_7 == 11)
                            {
                                this.taxStateInfo_0.method_0(InvoiceType.transportation);
                            }
                            else if (int_7 == 12)
                            {
                                this.taxStateInfo_0.method_0(InvoiceType.vehiclesales);
                            }
                            else if (int_7 == 0x33)
                            {
                                this.taxStateInfo_0.method_0(InvoiceType.Electronic);
                            }
                            else if (int_7 == 0x29)
                            {
                                this.taxStateInfo_0.method_0(InvoiceType.volticket);
                            }
                            time.ToString();
                        }
                        return list2;
                    }
                    this.dateTime_4 = time;
                    this.dateTime_6 = Convert.ToDateTime(str2);
                }
                catch
                {
                }
            }
            return list2;
        }

        private void method_42()
        {
        }

        private void method_43(InvVolumeApp invVolumeApp_0)
        {
            if (this.WriteInvVolumDB != null)
            {
                this.WriteInvVolumDB(invVolumeApp_0);
            }
        }

        private byte[] method_44()
        {
            return new byte[0x11800];
        }

        private ulong method_45(byte[] byte_1, int int_7)
        {
            byte[] buffer = new byte[8];
            for (int i = 0; i < 6; i++)
            {
                buffer[i] = byte_1[int_7 + i];
            }
            buffer[6] = 0;
            buffer[7] = 0;
            return BitConverter.ToUInt64(buffer, 0);
        }

        private void method_46()
        {
            this.sqxxlist_0 = new SQXXLIST();
            this.sqxxlist_0.ISXT = false;
            this.sqxxlist_0.ISSNY = false;
            if (!string.IsNullOrEmpty(this.string_14) && (this.string_14.Length > 10))
            {
                string str = this.string_14.Substring(0, 10);
                if ((Class22.string_1.Equals(str) || Class22.string_3.Equals(str)) || Class22.string_2.Equals(str))
                {
                    this.sqxxlist_0.ISXT = true;
                }
                if (str.Equals(Class22.string_0))
                {
                    this.sqxxlist_0.ISSNY = true;
                }
            }
            this.sqxxlist_0.ISZYFP = false;
            this.sqxxlist_0.ISPTFP = false;
            this.sqxxlist_0.ISHY = false;
            this.sqxxlist_0.ISJDC = false;
            this.sqxxlist_0.ISPTFPDZ = false;
            this.sqxxlist_0.ISPTFPJSP = false;
            if ((this.invSQInfo_0.PZSQType != null) && (this.invSQInfo_0.PZSQType.Count > 0))
            {
                foreach (PZSQType type in this.invSQInfo_0.PZSQType)
                {
                    if (type.invType == InvoiceType.special)
                    {
                        this.sqxxlist_0.ISZYFP = true;
                    }
                    if (type.invType == InvoiceType.common)
                    {
                        this.sqxxlist_0.ISPTFP = true;
                    }
                    if (type.invType == InvoiceType.transportation)
                    {
                        this.sqxxlist_0.ISHY = true;
                    }
                    if (type.invType == InvoiceType.vehiclesales)
                    {
                        this.sqxxlist_0.ISJDC = true;
                    }
                    if (type.invType == InvoiceType.Electronic)
                    {
                        this.sqxxlist_0.ISPTFPDZ = true;
                    }
                    if (type.invType == InvoiceType.volticket)
                    {
                        this.sqxxlist_0.ISPTFPJSP = true;
                    }
                }
            }
            this.sqxxlist_0.ISNCPSG = false;
            this.sqxxlist_0.ISNCPXS = false;
            if (!string.IsNullOrEmpty(this.invSQInfo_0.string_2))
            {
                if (this.invSQInfo_0.string_2 == "1")
                {
                    this.sqxxlist_0.ISNCPSG = true;
                }
                else if (this.invSQInfo_0.string_2 == "2")
                {
                    this.sqxxlist_0.ISNCPXS = true;
                }
                else if (this.invSQInfo_0.string_2 == "3")
                {
                    this.sqxxlist_0.ISNCPSG = true;
                    this.sqxxlist_0.ISNCPXS = true;
                }
            }
            this.sqxxlist_0.ISXGM = false;
            this.sqxxlist_0.ISYBNSR = false;
            if (!string.IsNullOrEmpty(this.invSQInfo_0.string_1))
            {
                if (this.invSQInfo_0.string_1 == "1")
                {
                    this.sqxxlist_0.ISYBNSR = true;
                }
                else if (this.invSQInfo_0.string_1 == "8")
                {
                    this.sqxxlist_0.ISXGM = true;
                }
            }
            this.sqxxlist_0.ISTDQY = this.invSQInfo_0.bool_0;
            this.sqxxlist_0.ISTLQY = this.invSQInfo_0.bool_1;
            if (this.invSQInfo_0.string_3 == "1")
            {
                this.sqxxlist_0.ISDZFPQY = true;
            }
            else if (this.invSQInfo_0.string_3 == "0")
            {
                this.sqxxlist_0.ISDZFPQY = false;
            }
            if (this.invSQInfo_0.string_4 == "1")
            {
                this.sqxxlist_0.ISDXQY = true;
            }
            else if (this.invSQInfo_0.string_4 == "0")
            {
                this.sqxxlist_0.ISDXQY = false;
            }
        }

        private string method_47(string string_35, int int_7, char char_0)
        {
            while (string_35.Length < int_7)
            {
                string_35 = char_0.ToString() + string_35;
            }
            return string_35;
        }

        internal int method_48(int int_7)
        {
            int num = -1;
            int num2 = (int_7 + 0x3e8) + 1;
            byte[] buffer = this.method_64(4, num2, null);
            this.int_4 = this.method_63(buffer, this.byte_0);
            if (this.RetCode == 0)
            {
                num = BitConverter.ToUInt16(this.byte_0, 0);
                if (num == 0xffff)
                {
                    num = -1;
                }
            }
            return num;
        }

        private int method_49()
        {
            int num = 0;
            for (int i = 1; i <= 7; i++)
            {
                if (this.method_48(i) < 0)
                {
                    break;
                }
                InvDetail detail = this.method_18(0L);
                int month = detail.Date.Month;
                int year = detail.Date.Year;
                if ((month == this.SysMonth) && (year == this.SysYear))
                {
                    num = i;
                }
            }
            return (num + 1);
        }

        private bool method_5()
        {
            bool flag2;
            Class20.smethod_1("TaxCardOpen>>>>>>>>>>>>>>>>");
            try
            {
                Class20.smethod_0();
                if (this.bool_5)
                {
                    return true;
                }
                if (this.class21_0 == null)
                {
                    this.class21_0 = Class21.smethod_0(this._taxCode);
                }
                if (!this.class21_0.method_2())
                {
                    this.int_4 = 0x2b69;
                    return false;
                }
                if (this.TaxMode == CTaxCardMode.tcmNone)
                {
                    this.method_60(10);
                    this.TaxCardClose();
                    this.class21_0.method_1(this.TaxMode);
                    return true;
                }
                this.bool_5 = false;
                MessageShow.MsgWait("正在检测金税设备 ...");
                ushort num2 = 0;
                switch (this.ctaxCardType_0)
                {
                    case CTaxCardType.tctAddedTax:
                        num2 = 0;
                        break;

                    case CTaxCardType.tctFuelTax:
                        num2 = 1;
                        break;

                    case CTaxCardType.tctWasteOld:
                        num2 = 2;
                        break;

                    case CTaxCardType.tctAgentInv:
                        num2 = 3;
                        break;

                    case CTaxCardType.tctBizCommerce:
                        num2 = 4;
                        break;

                    case CTaxCardType.tctCommonInv:
                        num2 = 5;
                        if (this.TaxMode == CTaxCardMode.tcmSimulate)
                        {
                            num2 = (ushort) (num2 + 0x10);
                        }
                        break;

                    case CTaxCardType.tctMultiInv:
                        num2 = 6;
                        break;

                    case CTaxCardType.const_7:
                        num2 = 7;
                        break;
                }
                if (this.bool_0)
                {
                    num2 = 8;
                }
                string str = this._taxCode;
                int num3 = int.Parse(CommonTool.GetMachine());
                Class20.smethod_1(string.Format("开卡税号{0}，开票机号{1}，CardType：{2}", this._taxCode, num3.ToString(), num2.ToString()));
                int num = this.method_59(num2, str, num3);
                bool flag = false;
                if (num == 0)
                {
                    if (this.method_7())
                    {
                        flag = true;
                        this.ecardType_0 = this.method_9();
                        Class20.smethod_3("开启成功");
                        flag = this.method_58();
                    }
                    else
                    {
                        this.TaxCardClose();
                        flag = false;
                    }
                }
                else
                {
                    this.int_4 = num;
                    Class20.smethod_3("金税设备开启失败: retCode=" + num);
                    switch (num)
                    {
                        case 0x3e8:
                            this.RetCode = 0x2523;
                            flag = false;
                            goto Label_01CC;

                        case 0x3e9:
                            flag = false;
                            goto Label_01CC;
                    }
                    flag = false;
                }
            Label_01CC:
                flag2 = flag;
            }
            catch (Exception exception)
            {
                Class20.smethod_2("开卡时异常错误:" + exception.ToString());
                MessageShow.MsgWait();
                this.RetCode = 0x2710;
                flag2 = false;
            }
            finally
            {
                Thread.Sleep(100);
                MessageShow.MsgWait();
            }
            return flag2;
        }

        private string method_50(byte[] byte_1, int int_7)
        {
            return this.method_51(byte_1, 0, int_7);
        }

        private string method_51(byte[] byte_1, int int_7, int int_8)
        {
            string str = "";
            for (int i = int_7; i < (int_8 + int_7); i++)
            {
                str = str + ((char) ((byte_1[i] >> 4) + 0x30)) + ((char) ((byte_1[i] % 0x10) + 0x30));
            }
            return str;
        }

        private string method_52()
        {
            int num;
            byte[] buffer = new byte[] { 
                0x37, 6, 11, 0x37, 8, 0, 10, 4, 0x37, 12, 1, 5, 0x37, 9, 2, 13, 
                7, 14, 0x37, 3
             };
            byte[] buffer2 = new byte[0x29];
            string str = this.string_16;
            string str2 = "";
            if (str.Length != 15)
            {
                return "";
            }
            for (num = 0; num < 20; num++)
            {
                if (buffer[num] < 15)
                {
                    buffer[num] = (byte) str[buffer[num]];
                }
            }
            for (num = 0; num < 5; num++)
            {
                buffer[num] = (byte) (buffer[num] ^ ((byte) str[num]));
                buffer[num + 5] = (byte) (buffer[num + 5] ^ ((byte) str[num + 10]));
                buffer[num + 10] = (byte) (buffer[num + 10] ^ ((byte) str[num + 10]));
                buffer[num + 15] = (byte) (buffer[num + 15] ^ ((byte) str[num + 5]));
            }
            for (num = 0; num < 20; num++)
            {
                buffer2[num * 2] = this.method_53(buffer[num] >> 4);
                buffer2[(num * 2) + 1] = this.method_53(buffer[num] & 15);
            }
            buffer2[40] = 0;
            for (num = 0; num < 0x29; num++)
            {
                str2 = str2 + ((char) buffer2[num]);
            }
            return str2;
        }

        private byte method_53(int int_7)
        {
            if (int_7 <= 9)
            {
                return (byte) (int_7 + 0x30);
            }
            switch (int_7)
            {
                case 10:
                    return 0x2b;

                case 11:
                    return 0x2d;

                case 12:
                    return 0x2a;

                case 13:
                    return 0x2f;

                case 14:
                    return 60;

                case 15:
                    return 0x3e;
            }
            return 0x30;
        }

        private string method_54(byte[] byte_1)
        {
            byte[] bytes = new byte[12];
            uint num = BitConverter.ToUInt32(byte_1, 1);
            byte[] buffer2 = Encoding.GetEncoding("GBK").GetBytes(num.ToString());
            int num2 = 10 - buffer2.Length;
            for (int i = 0; i < 5; i++)
            {
                bytes[i] = (i < num2) ? ((byte) 0x30) : buffer2[i - num2];
            }
            num = (uint) (byte_1[0] & 0x3f);
            bytes[5] = (byte) ((num / 10) + 0x30);
            bytes[6] = (byte) ((num % 10) + 0x30);
            for (int j = 5; j < 10; j++)
            {
                bytes[j + 2] = (j < num2) ? ((byte) 0x30) : buffer2[j - num2];
            }
            return Encoding.Default.GetString(bytes);
        }

        private string method_55()
        {
            string[] strArray = this.string_14.Split(new char[] { ';' });
            if (strArray.Length > 11)
            {
                return strArray[11];
            }
            return "";
        }

        internal bool method_56(string string_35)
        {
            if (this.CustomMessageBox != null)
            {
                return this.CustomMessageBox(string_35);
            }
            return MessageShow.ConfirmDlg(string_35, "金税设备提示");
        }

        internal string[] method_57(string string_35, string string_36, InvoiceType invoiceType_1)
        {
            if (this.GetInvoiceQryNo != null)
            {
                return this.GetInvoiceQryNo(string_35, string_36, invoiceType_1);
            }
            return new string[] { "0", "0" };
        }

        private bool method_58()
        {
            bool flag = true;
            if ((this.ECardType == ECardType.ectDefault) && (this.StateInfo.CompanyType > 0))
            {
                this.class17_0 = Class17.smethod_0(this);
                if (!this.class17_0.method_9())
                {
                    this.TaxCardClose();
                    flag = false;
                    this.class17_0 = null;
                    this.bool_7 = false;
                    return flag;
                }
                this.bool_7 = true;
                return flag;
            }
            if ((this.StateInfo.CompanyType > 0) && ((this.ecardType_0 == ECardType.const_2) || (this.ecardType_0 == ECardType.ectNewBulky)))
            {
                this.bool_9 = true;
            }
            return flag;
        }

        private int method_59(int int_7, string string_35, int int_8)
        {
            return this.class21_0.method_4(int_7, string_35, int_8);
        }

        private int method_6()
        {
            List<string> inList = new List<string>();
            MessageShow.MsgWait("正在启动金税设备 ...");
            if (this.ctaxCardType_0 != CTaxCardType.tctFuelTax)
            {
                StringBuilder builder = new StringBuilder();
                for (byte i = 0; i < this._taxCode.Length; i = (byte) (i + 1))
                {
                    byte num7 = (byte) this._taxCode[i];
                    num7 = (byte) (num7 ^ 0xc1);
                    num7 = (byte) (num7 ^ i);
                    builder.Append((char) ((num7 >> 4) + 0x41));
                    builder.Append((char) ((num7 & 15) + 0x41));
                }
                inList.Add(builder.ToString());
            }
            else
            {
                inList.Add(this.string_16);
            }
            inList.Add(this.method_52());
            byte[] array = this.method_64(0, 0, inList);
            string machine = CommonTool.GetMachine();
            if (string.IsNullOrEmpty(machine))
            {
                return -1;
            }
            BitConverter.GetBytes(Convert.ToUInt16(machine)).CopyTo(array, 0x3e8);
            this.int_4 = this.method_63(array, this.byte_0);
            if ((this.RetCode == 0x1fa) && MessageShow.ConfirmDlg("系统时钟错误！必须持金税设备到税务部门发行系统取得修改时钟授权，并将本机时钟调整正确，然后重新进入系统，系统就将金税设备时钟设置为计算机时钟。是否作好准备并修改金税设备时钟？"))
            {
                this.SetCardClock(DateTime.Now);
            }
            if ((this.RetCode == 510) || (this.RetCode == 0x1fd))
            {
                int num = 0;
                if (this.byte_0[0] == 0xff)
                {
                    num = 1;
                    if (this.string_11.Length <= 6)
                    {
                        this.string_11 = Encoding.GetEncoding("GBK").GetString(this.byte_0, 7, 6);
                    }
                    else
                    {
                        this.string_11 = Encoding.GetEncoding("GBK").GetString(this.byte_0, 7, 6) + this.string_11.Substring(6, this.string_11.Length - 6);
                    }
                    int num4 = 6;
                    int num2 = 13;
                    string str3 = string.Empty;
                    if (this.byte_0[0x12] == 0)
                    {
                        str3 = this.method_45(this.byte_0, num2).ToString();
                    }
                    else
                    {
                        int num5 = Convert.ToInt32(this.byte_0[(num4 + num2) - 1]);
                        str3 = ((((str3 + Convert.ToString((int) ((num5 / 10) % 10))[0] + Convert.ToString((int) (num5 % 10))[0]) + Encoding.Default.GetString(new byte[] { this.byte_0[(num4 + num2) - 2] }) + Encoding.Default.GetString(new byte[] { this.byte_0[(num4 + num2) - 3] })) + Encoding.Default.GetString(new byte[] { (byte) ((this.byte_0[(num4 + num2) - 4] >> 4) + 0x30) }) + Encoding.Default.GetString(new byte[] { (byte) ((this.byte_0[(num4 + num2) - 4] & 15) + 0x30) })) + Encoding.Default.GetString(new byte[] { (byte) ((this.byte_0[(num4 + num2) - 5] >> 4) + 0x30) }) + Encoding.Default.GetString(new byte[] { (byte) ((this.byte_0[(num4 + num2) - 5] & 15) + 0x30) })) + Encoding.Default.GetString(new byte[] { (byte) ((this.byte_0[(num4 + num2) - 6] >> 4) + 0x30) }) + Encoding.Default.GetString(new byte[] { (byte) ((this.byte_0[(num4 + num2) - 6] & 15) + 0x30) });
                    }
                    Class20.smethod_1("金税盘编号：" + str3);
                    this.string_32 = str3;
                }
                string s = this.method_51(this.byte_0, num, 6);
                this.dateTime_3 = DateTime.ParseExact(s, "yyyyMMddHHmm", null, DateTimeStyles.AllowWhiteSpaces);
            }
            return this.RetCode;
        }

        private int method_60(int int_7)
        {
            return this.class21_0.method_5(int_7);
        }

        private int method_61()
        {
            return this.class21_0.method_6();
        }

        private int method_62(byte[] byte_1, out IntPtr intptr_1)
        {
            lock (object_2)
            {
                int num = this.class21_0.method_7(byte_1, out intptr_1);
                if (num == 0x2c2)
                {
                    if (this.ReStartTaxCard() != 0)
                    {
                        return 0x2c2;
                    }
                    num = this.class21_0.method_7(byte_1, out intptr_1);
                }
                return num;
            }
        }

        private int method_63(byte[] byte_1, byte[] byte_2)
        {
            lock (object_2)
            {
                IntPtr zero = IntPtr.Zero;
                int num = this.class21_0.method_7(byte_1, out zero);
                if (num == 0x2c2)
                {
                    if (this.ReStartTaxCard() != 0)
                    {
                        return 0x2c2;
                    }
                    num = this.class21_0.method_7(byte_1, out zero);
                }
                if (zero != IntPtr.Zero)
                {
                    for (int i = 0; i < byte_2.Length; i++)
                    {
                        byte_2[i] = Marshal.ReadByte(zero, i);
                    }
                }
                return num;
            }
        }

        private byte[] method_64(int int_7, int int_8, List<string> inList)
        {
            this.int_5 = int_7;
            int num = 0x10400;
            if (((int_7 == 5) || (int_7 == 6)) || (int_7 == 0x19))
            {
                num = 0x2807d0;
            }
            byte[] buffer = new byte[num];
            for (int i = 0; i < num; i++)
            {
                buffer[0] = Convert.ToByte(0);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(int_7);
            if (int_8 > 0)
            {
                builder.Append(" ");
                builder.Append((int) (int_8 - 1));
            }
            if ((inList != null) && (inList.Count > 0))
            {
                for (int k = 0; k < inList.Count; k++)
                {
                    builder.Append(" ");
                    builder.Append(inList[k]);
                }
            }
            int length = builder.Length;
            for (int j = 0; j < length; j++)
            {
                buffer[j] = (byte) builder[j];
            }
            buffer[length] = 0;
            if (Class20.bool_0)
            {
                Class20.smethod_1(string.Format("InPara={0} ", builder));
            }
            return buffer;
        }

        private bool method_7()
        {
            bool flag;
            switch (this.method_6())
            {
                case 0x1fd:
                    MessageShow.MsgWait();
                    flag = this.method_13();
                    break;

                case 510:
                    MessageShow.MsgWait();
                    flag = this.method_12();
                    break;

                default:
                    MessageShow.MsgWait();
                    return false;
            }
            if (flag)
            {
                return false;
            }
            this.taxStateInfo_0.DriverVersion = this.method_8();
            this.bool_5 = true;
            return true;
        }

        private string method_8()
        {
            if (this.string_17.Trim().Length == 0)
            {
                byte[] buffer = this.method_64(0x17, 0, null);
                this.int_4 = this.method_63(buffer, this.byte_0);
                if (this.RetCode != 0)
                {
                    throw new Exception("未成功获取金税设备底层版本号");
                }
                this.string_17 = Encoding.Default.GetString(this.byte_0, 0, 9);
                string str = this.method_51(this.byte_0, 15, 6);
                if ((string.Compare(this.string_17, "SKN6K08L1") >= 0) && (string.Compare(str.Substring(0, 8), "20150820") >= 0))
                {
                    this.bool_2 = true;
                }
                if (!string.IsNullOrWhiteSpace(str))
                {
                    if (str.Length >= 8)
                    {
                        this.string_17 = this.string_17 + "-" + str.Substring(2, 6);
                    }
                    else
                    {
                        this.string_17 = this.string_17 + "-" + str;
                    }
                }
                string str2 = Encoding.GetEncoding("GBK").GetString(this.byte_0, 12, 3);
                if (!string.IsNullOrEmpty(str2) && (str2.ToUpper() == "YES"))
                {
                    this.bool_1 = true;
                }
            }
            return this.string_17;
        }

        private ECardType method_9()
        {
            ECardType ectDefault = ECardType.ectDefault;
            string str = this.method_8().Trim();
            string str2 = str.Substring(0, 3);
            bool flag = ((str2 == "KPN") && (str.CompareTo("KPN6K02B") > 0)) && (str != "KPN6K03B");
            bool flag2 = str.Substring(0, 2) == "SK";
            if (flag)
            {
                return ECardType.const_2;
            }
            if (flag2)
            {
                return ECardType.ectSK;
            }
            if (!string.IsNullOrEmpty(str2) && !(str2 == "KPH"))
            {
                ectDefault = ECardType.ectNewBulky;
            }
            return ectDefault;
        }

        public UnlockInvoice NInvAllotToSubMachine(string string_35, long long_3, long long_4, InvoiceType invoiceType_1, int int_7)
        {
            Class20.smethod_1("NInvAllotToSubMachine start>>>>>>>>>>");
            UnlockInvoice invoice = new UnlockInvoice {
                Buffer = null
            };
            List<string> inList = new List<string> {
                "2",
                string_35,
                long_3.ToString(),
                long_4.ToString()
            };
            inList.Add(((int) invoiceType_1).ToString());
            inList.Add(int_7.ToString());
            byte[] buffer = this.method_64(0x13, 0, inList);
            this.int_4 = this.method_63(buffer, this.byte_0);
            if (this.RetCode == 0)
            {
                Class20.smethod_1("NInvAllotToSubMachine60字节：" + BitConverter.ToString(this.byte_0, 0, 60));
                this.taxStateInfo_0.IsInvEmpty = BitConverter.ToUInt16(this.byte_0, 0);
                invoice.Buffer = new byte[40];
                invoice.Kind = this.byte_0[2];
                invoice.Number = BitConverter.ToUInt32(this.byte_0, 3).ToString();
                byte[] buffer2 = new byte[5];
                for (int i = 7; i < 12; i++)
                {
                    buffer2[i - 7] = this.byte_0[i];
                }
                if ((buffer2[0] & 0xc0) != 0xc0)
                {
                    invoice.TypeCode = this.method_50(buffer2, 5);
                }
                else
                {
                    invoice.TypeCode = this.method_54(buffer2);
                }
                invoice.Count = BitConverter.ToUInt16(this.byte_0, 12);
                invoice.Machine = BitConverter.ToUInt16(this.byte_0, 14);
                for (int j = 0x10; j < 0x38; j++)
                {
                    invoice.Buffer[j - 0x10] = this.byte_0[j];
                }
                Class20.smethod_1("NInvAllotToSubMachine解析出40字节：" + BitConverter.ToString(invoice.Buffer, 0, 40));
            }
            return invoice;
        }

        public byte[] NInvGetAuthorization()
        {
            byte[] buffer = new byte[0x10];
            List<string> inList = new List<string>();
            byte[] buffer2 = new byte[0x11800];
            inList.Clear();
            inList.Add("g");
            inList.Add("1");
            byte[] buffer3 = this.method_64(0x15, 0, inList);
            this.int_4 = this.method_63(buffer3, buffer2);
            if (this.int_4 == 0)
            {
                for (int i = 0; i < 0x10; i++)
                {
                    buffer[i] = buffer2[i + 2];
                }
            }
            return buffer;
        }

        public UnlockInvoice NInvGetUnlockInvoice(int int_7)
        {
            IntPtr ptr;
            byte[] buffer = new byte[4];
            buffer[0] = 4;
            buffer[1] = 1;
            byte[] buffer2 = buffer;
            this.method_62(buffer2, out ptr);
            byte val = (byte) int_7;
            Marshal.WriteByte(ptr, 0, val);
            List<string> inList = new List<string>();
            byte[] buffer3 = new byte[0x11800];
            inList.Add("C");
            inList.Add("1");
            byte[] buffer4 = this.method_64(0x15, 0, inList);
            Class20.smethod_1("查询金税盘网上购票已下载未解锁状态输入参数>>>>>>" + BitConverter.ToString(buffer4, 0, 20));
            this.int_4 = this.method_63(buffer4, buffer3);
            UnlockInvoice invoice = null;
            if (this.int_4 == 0)
            {
                invoice = new UnlockInvoice {
                    Buffer = new byte[40],
                    Kind = buffer3[2],
                    Number = BitConverter.ToUInt32(buffer3, 3).ToString(),
                    Count = BitConverter.ToUInt16(buffer3, 7)
                };
                byte[] buffer5 = new byte[5];
                for (int i = 9; i < 14; i++)
                {
                    buffer5[i - 9] = buffer3[i];
                }
                if ((buffer5[0] & 0xc0) != 0xc0)
                {
                    invoice.TypeCode = this.method_50(buffer5, 5);
                }
                else
                {
                    invoice.TypeCode = this.method_54(buffer5);
                }
                for (int j = 14; j < 0x36; j++)
                {
                    invoice.Buffer[j - 14] = buffer3[j];
                }
            }
            return invoice;
        }

        public UnlockInvoice NInvSearchUnlockFromMain()
        {
            Class20.smethod_1("NInvSearchUnlockFromMain start>>>>>>>>>>");
            UnlockInvoice invoice = new UnlockInvoice {
                Buffer = null
            };
            List<string> inList = new List<string> { "j", "0" };
            byte[] buffer = this.method_64(0x15, 0, inList);
            this.int_4 = this.method_63(buffer, this.byte_0);
            if (this.RetCode == 0)
            {
                Class20.smethod_1("NInvSearchUnlockFromMain字节：" + BitConverter.ToString(this.byte_0, 0, 60));
                invoice.Buffer = new byte[40];
                invoice.Kind = this.byte_0[2];
                invoice.Number = BitConverter.ToUInt32(this.byte_0, 3).ToString();
                byte[] buffer2 = new byte[5];
                for (int i = 7; i < 12; i++)
                {
                    buffer2[i - 7] = this.byte_0[i];
                }
                if ((buffer2[0] & 0xc0) != 0xc0)
                {
                    invoice.TypeCode = this.method_50(buffer2, 5);
                }
                else
                {
                    invoice.TypeCode = this.method_54(buffer2);
                }
                invoice.Count = BitConverter.ToUInt16(this.byte_0, 12);
                invoice.Machine = BitConverter.ToUInt16(this.byte_0, 14);
                for (int j = 0x10; j < 0x38; j++)
                {
                    invoice.Buffer[j - 0x10] = this.byte_0[j];
                }
                Class20.smethod_1("NInvSearchUnlockFromMain解析出40字节：" + BitConverter.ToString(invoice.Buffer, 0, 40));
            }
            return invoice;
        }

        public void NInvWirteConfirmResult(int int_7, byte[] byte_1, int int_8)
        {
            IntPtr ptr;
            byte[] buffer = new byte[4];
            buffer[0] = 4;
            buffer[1] = 1;
            byte[] buffer2 = buffer;
            this.method_62(buffer2, out ptr);
            for (int i = 0; i < int_8; i++)
            {
                Marshal.WriteByte(ptr, i, byte_1[i]);
            }
            Marshal.WriteByte(ptr, int_8, (byte) int_7);
            List<string> inList = new List<string>();
            byte[] buffer3 = new byte[0x11800];
            inList.Add("D");
            inList.Add((int_8 + 1).ToString());
            byte[] buffer4 = this.method_64(0x15, 0, inList);
            this.int_4 = this.method_63(buffer4, buffer3);
            if (this.int_4 == 0)
            {
                this.method_28();
            }
        }

        public void NInvWriteConfirmFromMain(byte[] byte_1, int int_7)
        {
            IntPtr ptr;
            byte[] buffer = new byte[4];
            buffer[0] = 4;
            buffer[1] = 1;
            byte[] buffer2 = buffer;
            this.method_62(buffer2, out ptr);
            for (int i = 0; i < int_7; i++)
            {
                Marshal.WriteByte(ptr, i, byte_1[i]);
            }
            List<string> inList = new List<string>();
            byte[] buffer3 = new byte[0x11800];
            inList.Add("k");
            inList.Add(int_7.ToString());
            byte[] buffer4 = this.method_64(0x15, 0, inList);
            this.int_4 = this.method_63(buffer4, buffer3);
        }

        public void OpenDevice(string string_35, string string_36 = "")
        {
            ISignAPI napi;
            string str = "Aisino Cryptographic Service Provider V1.0";
            if (this.bool_0)
            {
                str = "ComyiSafe ZXGJ CSP V3.0";
            }
            Class20.smethod_1("打开加密设备，_signDevFlag=" + this.InvSignServer);
            if (this.InvSignServer.ToUpper() != "CARD")
            {
                Class20.smethod_1("打开加密设备，使用验签服务器");
                napi = new SignSvrAPI();
                string_36 = this._taxCode;
            }
            else
            {
                Class20.smethod_1("打开加密设备，使用金税盘证书");
                napi = new SignAPI();
            }
            int num = -1;
            IntPtr ptr = new IntPtr();
            num = napi.OpenDevice(ref ptr, string_35, string_36, str);
            Class20.smethod_1(string.Format("打开设备,返回值：{0},句柄：{1}", num, ptr));
            if (num == 0)
            {
                this.intptr_0 = ptr;
                if (this.bool_0)
                {
                    this.string_4 = string_35;
                }
                this.int_4 = num;
            }
            else
            {
                this.int_4 = num;
            }
        }

        public string PreMakeInvoice(byte[] byte_1, int int_7 = 0)
        {
            IntPtr ptr;
            byte[] buffer = new byte[4];
            buffer[0] = 4;
            buffer[1] = 1;
            byte[] buffer2 = buffer;
            this.method_62(buffer2, out ptr);
            int length = byte_1.Length;
            for (int i = 0; i < length; i++)
            {
                Marshal.WriteByte(ptr, i, byte_1[i]);
            }
            List<string> inList = new List<string> {
                "8",
                length.ToString()
            };
            byte[] buffer3 = this.method_64(0x15, 0, inList);
            this.int_4 = this.method_63(buffer3, this.byte_0);
            return string.Concat(new object[] { "TCD_", this.int_4, "_", this.int_5 });
        }

        public InvDetail QueryInvInfo(string string_35, int int_7, string string_36, string string_37, DateTime dateTime_8)
        {
            InvDetail detail2;
            try
            {
                this.method_42();
                List<string> inList = new List<string> {
                    string_35,
                    int_7.ToString(),
                    "F"
                };
                byte[] array = this.method_64(4, 0, inList);
                if (!string.IsNullOrEmpty(string_36))
                {
                    byte[] bytes = BitConverter.GetBytes(Convert.ToUInt32(string_36));
                    Array.Reverse(bytes, 0, bytes.Length);
                    bytes.CopyTo(array, 0x3e8);
                }
                if (!string.IsNullOrEmpty(string_37))
                {
                    BitConverter.GetBytes(Convert.ToUInt32(string_37)).CopyTo(array, 0x3ec);
                }
                BitConverter.GetBytes((ushort) dateTime_8.Year).CopyTo(array, 0x3f0);
                array[0x3f2] = (byte) dateTime_8.Month;
                Class20.smethod_1("发票查询接口,输入参数：" + BitConverter.ToString(array, 0x3e8, 20));
                this.int_4 = this.method_63(array, this.byte_0);
                detail2 = this.method_22(true);
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
                throw;
            }
            return detail2;
        }

        public string QueryInvInfo(string string_35, int int_7, string string_36, string string_37, DateTime dateTime_8, out InvDetail invDetail_0, int int_8 = 0)
        {
            Class20.smethod_1(string.Format(">>>>>>>>>>>>>读取发票：发票代码{0}，发票号码{1}", string_35, int_7));
            invDetail_0 = this.QueryInvInfo(string_35, int_7, string_36, string_37, dateTime_8);
            return string.Concat(new object[] { "TCD_", this.int_4, "_", this.int_5 });
        }

        public byte[] QueryRegCode(byte[] byte_1)
        {
            IntPtr ptr;
            byte[] buffer = new byte[4];
            buffer[0] = 4;
            buffer[1] = 1;
            byte[] buffer2 = buffer;
            this.method_62(buffer2, out ptr);
            List<string> inList = new List<string>();
            inList.Clear();
            int length = byte_1.Length;
            for (int i = 0; i < length; i++)
            {
                Marshal.WriteByte(ptr, i, byte_1[i]);
            }
            inList.Add("5");
            inList.Add(length.ToString());
            byte[] buffer3 = this.method_64(0x15, 0, inList);
            this.int_4 = this.method_63(buffer3, this.byte_0);
            if (this.RetCode == 0)
            {
                length = BitConverter.ToUInt16(this.byte_0, 0);
                return CommonTool.GetSubArray(this.byte_0, 2, length);
            }
            return new byte[1];
        }

        public string QueryRegCode(byte[] byte_1, out byte[] byte_2, int int_7 = 0)
        {
            byte_2 = this.QueryRegCode(byte_1);
            return string.Concat(new object[] { "TCD_", this.int_4, "_", this.int_5 });
        }

        public byte[] ReadCompanyInfo()
        {
            List<string> inList = new List<string> { "r", "0" };
            byte[] buffer = this.method_64(0x15, 0, inList);
            byte[] buffer2 = new byte[0x11800];
            this.int_4 = this.method_63(buffer, buffer2);
            ushort num = BitConverter.ToUInt16(buffer2, 0);
            byte[] buffer3 = new byte[num];
            for (int i = 0; i < num; i++)
            {
                buffer3[i] = buffer2[i + 2];
            }
            return buffer3;
        }

        public List<InvVolumeApp> ReadFJNewInv(int int_7, string string_35)
        {
            List<InvVolumeApp> list4;
            try
            {
                MessageShow.MsgWait("正在读分开票机新购票 ...");
                List<InvVolumeApp> list = new List<InvVolumeApp>();
                List<InvVolumeApp> listInvVolumeApp = new List<InvVolumeApp>();
                this.GetInvStock();
                int count = this.list_0.Count;
                if (this.list_0 != null)
                {
                    foreach (InvVolumeApp app in this.list_0)
                    {
                        list.Add(app);
                    }
                }
                new List<InvVolumeApp>();
                List<string> inList = new List<string> {
                    string_35,
                    int_7.ToString()
                };
                byte[] buffer = this.method_64(12, 0, inList);
                this.int_4 = this.method_63(buffer, this.byte_0);
                if (this.RetCode == 0)
                {
                    this.GetInvStock();
                    int num2 = this.list_0.Count;
                    if (count < num2)
                    {
                        foreach (InvVolumeApp app2 in this.list_0)
                        {
                            bool flag = false;
                            using (List<InvVolumeApp>.Enumerator enumerator3 = list.GetEnumerator())
                            {
                                while (enumerator3.MoveNext())
                                {
                                    InvVolumeApp current = enumerator3.Current;
                                    if (((app2.InvType == current.InvType) && (app2.TypeCode == current.TypeCode)) && (app2.HeadCode == current.HeadCode))
                                    {
                                        goto Label_014A;
                                    }
                                }
                                goto Label_015D;
                            Label_014A:
                                flag = true;
                            }
                        Label_015D:
                            if (!flag)
                            {
                                listInvVolumeApp.Add(app2);
                            }
                        }
                    }
                    CommonTool.smethod_6(listInvVolumeApp);
                    this.method_28();
                }
                list4 = listInvVolumeApp;
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
                throw;
            }
            finally
            {
                MessageShow.MsgWait();
            }
            return list4;
        }

        public void RemoteClearCard(string string_35, int int_7)
        {
            IntPtr ptr;
            this.method_42();
            byte[] buffer = new byte[4];
            buffer[0] = 4;
            buffer[1] = 1;
            byte[] buffer2 = buffer;
            this.method_62(buffer2, out ptr);
            byte[] bytes = Encoding.GetEncoding("GBK").GetBytes(string_35);
            for (int i = 0; i < bytes.Length; i++)
            {
                Marshal.WriteByte(ptr, i, bytes[i]);
            }
            List<string> inList = new List<string> { "6" };
            inList.Add(bytes.Length.ToString());
            byte[] buffer4 = this.method_64(0x15, 0, inList);
            buffer4[0x3e8] = Convert.ToByte(int_7);
            buffer4[0x3e9] = Convert.ToByte(1);
            this.int_4 = this.method_63(buffer4, this.byte_0);
            if (this.int_4 == 0)
            {
                this.int_4 = this.ReStartTaxCard();
                if (this.int_4 != 0)
                {
                    this.int_4 = 0x2c2;
                }
            }
        }

        public void RemoteClearCard(byte[] byte_1, int int_7, int int_8)
        {
            IntPtr ptr;
            this.method_42();
            byte[] buffer = new byte[4];
            buffer[0] = 4;
            buffer[1] = 1;
            byte[] buffer2 = buffer;
            this.method_62(buffer2, out ptr);
            for (int i = 0; i < int_7; i++)
            {
                Marshal.WriteByte(ptr, i, byte_1[i]);
            }
            List<string> inList = new List<string> {
                "6",
                int_7.ToString()
            };
            byte[] buffer3 = this.method_64(0x15, 0, inList);
            buffer3[0x3e8] = Convert.ToByte(int_8);
            buffer3[0x3e9] = Convert.ToByte(1);
            this.int_4 = this.method_63(buffer3, this.byte_0);
            if (this.int_4 == 0)
            {
                this.int_4 = this.ReStartTaxCard();
                if (this.int_4 != 0)
                {
                    this.int_4 = 0x2c2;
                }
            }
        }

        public byte[] RemoteRep(int int_7, out DateTime dateTime_8, out DateTime dateTime_9)
        {
            dateTime_8 = new DateTime();
            dateTime_9 = new DateTime();
            List<string> inList = new List<string> { "0", "1" };
            byte[] buffer = this.method_64(0x16, 0, inList);
            buffer[0x3e8] = Convert.ToByte(int_7);
            Class20.smethod_1("远程抄报：发票类别" + int_7.ToString());
            this.int_4 = this.method_63(buffer, this.byte_0);
            Class20.smethod_1("远程抄报：返回值" + this.int_4.ToString());
            if (this.int_4 == 0)
            {
                Class20.smethod_1("远程抄报：初始返回数组：" + this.byte_0.Length);
                byte[] array = new byte[this.byte_0.Length];
                this.byte_0.CopyTo(array, 0);
                Class20.smethod_1("远程抄报：1。");
                dateTime_8 = this.method_40(this.byte_0, 320);
                dateTime_9 = this.method_40(this.byte_0, 0x144);
                Class20.smethod_1("远程抄报：刷新状态。");
                this.method_28();
                Class20.smethod_1("远程抄报：刷新锁死期。");
                this.method_41(int_7);
                return array;
            }
            return new byte[1];
        }

        public void RepairClose()
        {
            if (this.bool_7 && this.bool_8)
            {
                this.class17_0.method_21();
                this.class17_0.long_0 = 0L;
                this.class17_0.int_2 = 0;
            }
            Class20.smethod_1("发票修复结束");
        }

        public void RepairOpen(int int_7)
        {
            Class20.smethod_1("开始发票修复 月" + int_7);
            if (this.bool_7 && this.bool_8)
            {
                DateTime time;
                if (int_7 > 0)
                {
                    time = new DateTime(this.SysYear, int_7, 1);
                }
                else
                {
                    time = new DateTime(this.SysYear - 1, 12 + int_7, 1);
                }
                ulong num = 0L;
                int num2 = 0;
                long num3 = this.class17_0.method_20(time, this.long_1, ref num, ref num2);
                this.class17_0.long_0 = num3;
            }
        }

        public int ReStartTaxCard()
        {
            IntPtr ptr;
            this.sqxxlist_0 = null;
            List<string> inList = new List<string>();
            byte[] buffer = this.method_64(30, 0, inList);
            byte[] buffer2 = new byte[0x11800];
            int num = this.class21_0.method_7(buffer, out ptr);
            for (int i = 0; i < buffer2.Length; i++)
            {
                buffer2[i] = Marshal.ReadByte(ptr, i);
            }
            if (num == 0)
            {
                Class20.smethod_3(">>>>>>>>>>>>>>>>>跨日了>>>>>>>>>>>>>>>>>>>>>>>>>");
                this.method_11(buffer2);
                if (this.ChangeDateEvent != null)
                {
                    Class20.smethod_1("跨日了，刷新状态栏");
                    this.ChangeDateEvent(null, new EventArgs());
                    return num;
                }
                Class20.smethod_1("跨日了，没刷新状态栏");
            }
            return num;
        }

        public int ReStartTaxCard(int int_7, out byte[] byte_1)
        {
            byte_1 = null;
            return this.ReStartTaxCard();
        }

        public void RestoreFileForHighDev(int int_7)
        {
            List<string> inList = new List<string> {
                "1",
                int_7.ToString()
            };
            byte[] buffer = this.method_64(0x1f, 0, inList);
            this.int_4 = this.method_63(buffer, this.byte_0);
        }

        public bool SetCardClock(DateTime dateTime_8)
        {
            bool flag;
            try
            {
                MessageShow.MsgWait("正在修改金税设备时钟 ...");
                List<string> inList = new List<string>();
                string item = (((int) dateTime_8.DayOfWeek) + 1).ToString();
                inList.Add((dateTime_8.Year % 100).ToString());
                inList.Add(dateTime_8.Month.ToString());
                inList.Add(dateTime_8.Day.ToString());
                inList.Add(item);
                inList.Add(dateTime_8.Hour.ToString());
                inList.Add(dateTime_8.Minute.ToString());
                inList.Add(dateTime_8.Second.ToString());
                byte[] buffer = this.method_64(0x12, 0, inList);
                this.int_4 = this.method_63(buffer, this.byte_0);
                MessageShow.MsgWait();
                if (this.RetCode == 0)
                {
                    this.int_4 = this.ReStartTaxCard();
                    if (this.int_4 != 0)
                    {
                        this.int_4 = 0x2c2;
                        return false;
                    }
                    return true;
                }
                flag = false;
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
                flag = false;
            }
            finally
            {
                MessageShow.MsgWait();
            }
            return flag;
        }

        public SetPWResult SetCardPassword(string string_35, string string_36)
        {
            SetPWResult oldPwError;
            try
            {
                List<string> inList = new List<string> {
                    string_35.Length.ToString(),
                    string_35,
                    string_36.Length.ToString(),
                    string_36
                };
                byte[] buffer = this.method_64(0x11, 1, inList);
                this.int_4 = this.method_63(buffer, this.byte_0);
                if (this.RetCode == 0)
                {
                    return SetPWResult.success;
                }
                oldPwError = SetPWResult.oldPwError;
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
                throw;
            }
            return oldPwError;
        }

        public void SetExtandParams(string string_35, string string_36)
        {
            if ((this.dictionary_0 != null) && this.dictionary_0.ContainsKey(string_35))
            {
                this.dictionary_0[string_35] = string_36;
            }
            else
            {
                this.dictionary_0.Add(string_35, string_36);
            }
        }

        public string SetInvVols(InvoiceType invoiceType_1, string string_35, string string_36)
        {
            List<string> inList = new List<string>();
            inList.Add(((int) invoiceType_1).ToString());
            inList.Add(string_35);
            inList.Add(string_36);
            byte[] buffer = this.method_64(0x1c, 0, inList);
            this.int_4 = this.method_63(buffer, this.byte_0);
            return ("TCD_" + this.int_4 + "_28");
        }

        public void SetOffLineState()
        {
            List<string> inList = new List<string> { "u", "0" };
            byte[] buffer = this.method_64(0x15, 0, inList);
            this.int_4 = this.method_63(buffer, this.byte_0);
        }

        public int SignChangePassword(string string_35, string string_36)
        {
            if (this.intptr_0 == IntPtr.Zero)
            {
                this.int_4 = 0x25;
                return this.int_4;
            }
            int num = -1;
            num = ChangeCertPwd(string_35, string_36);
            if (num == 0)
            {
                this.string_4 = string_36;
            }
            this.int_4 = num;
            return this.int_4;
        }

        public string SignData(string string_35, out string string_36)
        {
            ISignAPI napi;
            Class20.smethod_1("加签方法，_signDevFlag=" + this.InvSignServer);
            string_36 = "";
            if (this.intptr_0 == IntPtr.Zero)
            {
                this.int_4 = 0x25;
                return string.Concat(new object[] { "CA_", this.int_4, "_", 4 });
            }
            if ((this.InvSignServer.ToUpper() == "CARD") && !this.bool_0)
            {
                if (DateTime.Compare(this.TaxClock, this.dateTime_0) < 0)
                {
                    this.int_4 = 100;
                    return string.Concat(new object[] { "CA_", this.int_4, "_", 4 });
                }
                if (DateTime.Compare(this.TaxClock, this.dateTime_1) > 0)
                {
                    this.int_4 = 0x65;
                    return string.Concat(new object[] { "CA_", this.int_4, "_", 4 });
                }
            }
            if (this.InvSignServer.ToUpper() != "CARD")
            {
                Class20.smethod_1("加签方法，使用验签服务器加签");
                napi = new SignSvrAPI();
            }
            else
            {
                Class20.smethod_1("加签方法，使用金税盘证书加签");
                napi = new SignAPI();
            }
            int num = napi.SignData(this.intptr_0, string_35, this.string_4, out string_36);
            this.int_4 = num;
            return string.Concat(new object[] { "CA_", this.int_4, "_", 4 });
        }

        public bool TaxCardClose()
        {
            bool flag;
            try
            {
                if (this.bool_5)
                {
                    MessageShow.MsgWait("正在关闭金税设备 ...");
                    if (this.method_4() != DrvState.dsClose)
                    {
                        byte[] buffer = this.method_64(3, 0, null);
                        this.int_4 = this.method_63(buffer, this.byte_0);
                    }
                    this.method_61();
                    this.string_17 = "";
                    if (this.IsUseCert && !this.IsTrain)
                    {
                        try
                        {
                            if (this.bool_4)
                            {
                                this.CloseDevice();
                                if (this.int_4 != 0)
                                {
                                    this.int_6 = this.int_4;
                                    this.int_4 = Convert.ToInt32("777777" + this.int_4.ToString());
                                }
                            }
                        }
                        catch
                        {
                            if (this.int_4 != 0)
                            {
                                this.int_6 = this.int_4;
                                this.int_4 = Convert.ToInt32("777777" + this.int_4.ToString());
                            }
                        }
                    }
                    MessageShow.MsgWait();
                }
                this.bool_5 = false;
                this.bool_4 = false;
                flag = true;
            }
            catch (Exception exception)
            {
                Class20.smethod_2(exception.ToString());
                flag = false;
            }
            finally
            {
                if (this.class21_0 != null)
                {
                    this.class21_0.method_3();
                }
            }
            return flag;
        }

        public bool TaxCardOpen(string string_35)
        {
            Class20.smethod_1("TaxCardOpen>>>>>>>>>>>>>>>>");
            if (!this.method_5())
            {
                return false;
            }
            if ((this.IsUseCert && !this.IsTrain) && !this.bool_0)
            {
                if (this.bool_4)
                {
                    goto Label_0149;
                }
                try
                {
                    string invControlNum = this.GetInvControlNum();
                    Class20.smethod_1(string.Format("OpenDevice：certPWD{0}，BH{1}", string_35, invControlNum));
                    this.OpenDevice(string_35, "*44" + invControlNum + "  ");
                    if (this.int_4 == 0)
                    {
                        this.string_4 = string_35;
                        Class20.smethod_1(string.Format("证书登录成功，证书密码是：", string_35));
                        if (((CommonTool.GetInvSignServer().ToUpper() == "CARD") && string.IsNullOrWhiteSpace(this.string_2)) && "88888888".Equals(string_35))
                        {
                            this.int_4 = -1111;
                            this.method_16();
                            return false;
                        }
                        this.bool_4 = true;
                        goto Label_0149;
                    }
                    this.int_6 = this.int_4;
                    this.int_4 = Convert.ToInt32("777777" + this.int_4.ToString());
                    this.method_16();
                    return false;
                }
                catch (Exception)
                {
                    this.int_6 = this.int_4;
                    this.int_4 = -1112;
                    this.method_16();
                    return false;
                }
            }
            this.string_4 = string_35;
        Label_0149:
            this.method_15();
            Class20.smethod_1("取压缩税号，返回值：" + this.int_4.ToString());
            if (this.int_4 != 0)
            {
                return false;
            }
            if ((this.IsUseCert && this.bool_4) && (!this.IsTrain && !this.bool_0))
            {
                if (!(this.InvSignServer.ToUpper() == "CARD"))
                {
                    return true;
                }
                CertInfo info = new CertInfo();
                int certInfo = this.GetCertInfo(info);
                Class20.smethod_1("读取证书信息，返回值：" + certInfo.ToString());
                if (certInfo == 0)
                {
                    Class20.smethod_1(string.Format("证书税号{0}，金税盘税号{1}", info.Nsrsbh, this.TaxCode));
                    if (this.TaxCode != info.Nsrsbh)
                    {
                        this.int_4 = 0x2b67;
                        return false;
                    }
                    try
                    {
                        Class20.smethod_1("_certStartTime");
                        this.dateTime_0 = new DateTime(0x7d0 + Convert.ToInt32(info.Qsrq.Substring(0, 2)), Convert.ToInt32(info.Qsrq.Substring(2, 2)), Convert.ToInt32(info.Qsrq.Substring(4, 2)), Convert.ToInt32(info.Qsrq.Substring(6, 2)), Convert.ToInt32(info.Qsrq.Substring(8, 2)), Convert.ToInt32(info.Qsrq.Substring(10, 2)));
                        Class20.smethod_1("_certEndTime");
                        this.dateTime_1 = new DateTime(0x7d0 + Convert.ToInt32(info.Jzrq.Substring(0, 2)), Convert.ToInt32(info.Jzrq.Substring(2, 2)), Convert.ToInt32(info.Jzrq.Substring(4, 2)), Convert.ToInt32(info.Jzrq.Substring(6, 2)), Convert.ToInt32(info.Jzrq.Substring(8, 2)), Convert.ToInt32(info.Jzrq.Substring(10, 2)));
                        if (DateTime.Compare(this.TaxClock.AddMonths(3), this.dateTime_1) > 0)
                        {
                            if (DateTime.Compare(this.TaxClock, this.dateTime_1) > 0)
                            {
                                MessageShow.PromptDlg(string.Format("您的税务数字证书已过期,请及时携带您的金税设备到办税大厅更新您的数字证书！", this.dateTime_1.ToString("yyyy-MM-dd")), "金税设备登录");
                            }
                            else
                            {
                                MessageShow.PromptDlg(string.Format("您的税务数字证书将于{0}到期,请及时携带您的金税设备到办税大厅更新您的数字证书！", this.dateTime_1.ToString("yyyy-MM-dd")), "金税设备登录");
                            }
                        }
                        if (this.IsTrain)
                        {
                            MessageShow.PromptDlg("航信培训用户，我们将屏蔽一些真实的操作，仅供培训使用！", "金税设备登录");
                        }
                        Class20.smethod_1("return true");
                        return true;
                    }
                    catch
                    {
                        Class20.smethod_1("catch");
                        return false;
                    }
                }
                Class20.smethod_1("11112");
                this.int_4 = 0x2b68;
                return false;
            }
            if (this.IsTrain)
            {
                MessageShow.PromptDlg("航信培训用户，我们将屏蔽一些真实的操作，仅供培训使用！", "金税设备登录");
            }
            return true;
        }

        public string TaxDevAuthentication1(int int_7, out byte[] byte_1)
        {
            ushort num = 100;
            byte[] buffer = new byte[10];
            buffer[0] = (byte) int_7;
            byte[] buffer2 = new byte[100];
            this.int_4 = WebInvStockAPI.jsk_operate_r(0x20, 0, 0, this._taxCode, buffer, 10, buffer2, ref num);
            if (this.int_4 == 0)
            {
                byte_1 = new byte[num];
                Array.Copy(buffer2, 0, byte_1, 0, num);
            }
            else
            {
                byte[] buffer3 = new byte[1];
                byte_1 = buffer3;
            }
            return ("TCD_" + this.int_4.ToString() + "_32");
        }

        public string TaxDevAuthentication2(byte[] byte_1, out byte[] byte_2)
        {
            ushort num = 100;
            byte[] buffer = new byte[100];
            this.int_4 = WebInvStockAPI.jsk_operate_r(0x21, 0, (short) this.int_2, this._taxCode, byte_1, (ushort) byte_1.Length, buffer, ref num);
            if (this.int_4 == 0)
            {
                byte_2 = new byte[num];
                Array.Copy(buffer, 0, byte_2, 0, num);
            }
            else
            {
                byte[] buffer2 = new byte[1];
                byte_2 = buffer2;
            }
            return ("TCD_" + this.int_4.ToString() + "_33");
        }

        public string TaxDevAuthentication3(byte[] byte_1)
        {
            ushort num = 100;
            byte[] buffer = new byte[100];
            this.int_4 = WebInvStockAPI.jsk_operate_r(0x20, 1, (short) this.int_2, this._taxCode, byte_1, (ushort) byte_1.Length, buffer, ref num);
            return ("TCD_" + this.int_4.ToString() + "_32");
        }

        public RepResult TaxReport(int int_7, int int_8, int int_9, out TaxReportResult taxReportResult_0)
        {
            RepResult rrSuccess;
            taxReportResult_0 = new TaxReportResult();
            try
            {
                MessageShow.MsgWait("正在将税务资料写入金税设备 ...");
                List<string> inList = new List<string> {
                    int_7.ToString()
                };
                byte[] buffer = this.method_64(11, 0, inList);
                if ((this.SQInfo.DHYBZ != "") && ((this.SQInfo.DHYBZ == "Z") || (this.SQInfo.DHYBZ == "Y")))
                {
                    buffer[0x3e8] = Convert.ToByte(int_8);
                }
                buffer[0x3e9] = Convert.ToByte(int_9);
                this.int_4 = this.method_63(buffer, this.byte_0);
                if (this.RetCode == 0)
                {
                    if ((int_8 != 11) && (int_8 != 12))
                    {
                        this.dateTime_6 = CommonTool.AcqTaxDateTime(this.byte_0[2], this.byte_0[4], this.byte_0[6], this.byte_0[8]);
                        this.taxStateInfo_0.IsInvEmpty = this.byte_0[9];
                        this.taxStateInfo_0.IsRepReached = this.byte_0[10];
                        this.taxStateInfo_0.IsLockReached = this.byte_0[11];
                        this.taxStateInfo_0.IsMainMachine = this.byte_0[12];
                        this.taxStateInfo_0.IsWithChild = this.byte_0[13];
                        ushort num = Convert.ToUInt16((int) (this.byte_0[1] * 0x100));
                        this.taxStateInfo_0.MachineNumber = Convert.ToUInt16((int) (this.byte_0[14] + num));
                        this.dateTime_4 = CommonTool.AcqTaxDateTime(this.byte_0, 0x10, 6, 1);
                        this.taxStateInfo_0.CompanyType = this.byte_0[0x16];
                        this.taxStateInfo_0.TutorialFlag = this.byte_0[0x17];
                        this.taxStateInfo_0.BigAmountInvCount = this.byte_0[0x18];
                        this.taxStateInfo_0.ushort_4 = this.byte_0[0x19];
                    }
                    taxReportResult_0.ReportDate = CommonTool.AcqTaxDateTime(this.byte_0, 0x10, 6, 1).ToString("yyyyMM");
                    taxReportResult_0.NewOldFlag = this.byte_0[0x15a].ToString();
                    taxReportResult_0.CurPeriod = this.byte_0[0x15b].ToString();
                    taxReportResult_0.Period = this.byte_0[0x15c].ToString();
                    Class20.smethod_1(string.Format("抄税完成，返回抄税年月：{0}，新旧税标志：{1}，征期非征期：{2}，抄税期数：{3}", new object[] { taxReportResult_0.ReportDate, taxReportResult_0.NewOldFlag, taxReportResult_0.CurPeriod, taxReportResult_0.Period }));
                }
                int retCode = this.RetCode;
                if (retCode != 0)
                {
                    if (retCode != 0x26)
                    {
                        return RepResult.rrFault;
                    }
                    return RepResult.rrNextCard;
                }
                this.method_28();
                this.method_41(int_9);
                this.int_3 = -1;
                this.int_1 = -1;
                rrSuccess = RepResult.rrSuccess;
            }
            catch (Exception exception)
            {
                Class20.smethod_2("抄税时错误: " + exception.ToString());
                rrSuccess = RepResult.rrFault;
            }
            finally
            {
                MessageShow.MsgWait();
            }
            return rrSuccess;
        }

        public RepResult TaxTotal()
        {
            MessageShow.MsgWait("正在读入分开票机金税设备税务资料 ...");
            byte[] buffer = this.method_64(12, 0, null);
            this.int_4 = this.method_63(buffer, this.byte_0);
            MessageShow.MsgWait();
            int num = 0;
            if (this.RetCode == 0)
            {
                num = BitConverter.ToInt16(this.byte_0, 0);
            }
            if (this.RetCode == 0)
            {
                switch (num)
                {
                    case 0x20:
                        return RepResult.rrNextCard;

                    case 0x21:
                    case 20:
                        return RepResult.rrSuccess;
                }
                MessageShow.PromptDlg("汇总分开票机操作产生错误汇总标志：" + num.ToString());
            }
            return RepResult.rrFault;
        }

        public void UpdateCompanyInfo(byte[] byte_1, uint uint_0)
        {
            IntPtr ptr;
            byte[] buffer = new byte[4];
            buffer[0] = 4;
            buffer[1] = 1;
            byte[] buffer2 = buffer;
            this.method_62(buffer2, out ptr);
            for (int i = 0; i < uint_0; i++)
            {
                Marshal.WriteByte(ptr, i, byte_1[i]);
            }
            List<string> inList = new List<string>();
            byte[] buffer3 = new byte[0x11800];
            inList.Add("s");
            inList.Add(uint_0.ToString());
            byte[] buffer4 = this.method_64(0x15, 0, inList);
            this.int_4 = this.method_63(buffer4, buffer3);
            if (this.int_4 == 0)
            {
                this.int_4 = this.ReStartTaxCard();
                if (this.int_4 != 0)
                {
                    this.int_4 = 0x2c2;
                }
            }
        }

        public string UpdateDybzToDB(int int_7, string string_35, string string_36)
        {
            return "0000";
        }

        public int UpdateInvUploadFlag(byte[] byte_1, int int_7, string string_35 = "")
        {
            IntPtr ptr;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            byte[] buffer = new byte[4];
            buffer[0] = 4;
            buffer[1] = 1;
            byte[] buffer2 = buffer;
            this.method_62(buffer2, out ptr);
            int length = byte_1.Length;
            for (int i = 0; i < length; i++)
            {
                Marshal.WriteByte(ptr, i, byte_1[i]);
            }
            List<string> inList = new List<string>();
            byte[] buffer3 = new byte[0x11800];
            inList.Add("t");
            inList.Add(byte_1.Length.ToString());
            byte[] array = this.method_64(0x15, 0, inList);
            BitConverter.GetBytes((ushort) int_7).CopyTo(array, 0x3e8);
            this.int_4 = this.method_63(array, buffer3);
            int num4 = 0;
            if ((this.int_4 == 0) && (BitConverter.ToUInt16(buffer3, 0) == 1))
            {
                num4 = buffer3[2];
                num4++;
            }
            stopwatch.Stop();
            Class20.smethod_1("=========21T花费时间：" + stopwatch.ElapsedMilliseconds);
            return num4;
        }

        public List<ClearCardInfo> UpdateUpLoadFlag()
        {
            List<ClearCardInfo> list = new List<ClearCardInfo>();
            List<string> inList = new List<string>();
            inList.Clear();
            inList.Add("y");
            inList.Add("1");
            byte[] buffer = this.method_64(0x15, 0, inList);
            for (int i = 0; i < 3; i++)
            {
                byte[] buffer2 = new byte[0x11800];
                this.int_4 = this.method_63(buffer, buffer2);
                Class20.smethod_1("调用更新发票上传标志" + i.ToString() + ":返回值-" + this.int_4.ToString());
                if (this.int_4 == 0)
                {
                    ClearCardInfo item = new ClearCardInfo();
                    if ((buffer2[2] != 0xff) && (buffer2[3] != 0xff))
                    {
                        item.InvKind = buffer2[2];
                        item.InvPeriod = buffer2[3];
                        int num2 = (buffer2[4] * 0x10) + (buffer2[5] >> 4);
                        int num3 = buffer2[5] & 15;
                        item.CSTime = num2.ToString() + num3.ToString("D2");
                        list.Add(item);
                        Class20.smethod_1(string.Concat(new object[] { "调用更新发票上传标志", i.ToString(), ":返回内容-", item.InvKind, "-", item.InvPeriod, "-", num2, num3, item.CSTime }));
                    }
                    else
                    {
                        this.int_4 = -1;
                    }
                }
            }
            Class20.smethod_1("更新发票上传标志,返回结果数：" + list.Count.ToString());
            return list;
        }

        public string VerifySignedData(string string_35, string string_36)
        {
            ISignAPI napi;
            if (this.intptr_0 == IntPtr.Zero)
            {
                this.int_4 = 0x25;
                return string.Concat(new object[] { "CA_", this.int_4, "_", 4 });
            }
            if (this.InvSignServer.ToUpper() != "CARD")
            {
                napi = new SignSvrAPI();
            }
            else
            {
                napi = new SignAPI();
            }
            int num = napi.VerifySignedData(this.intptr_0, string_35, string_36);
            this.int_4 = num;
            return string.Concat(new object[] { "CA_", this.int_4, "_", 4 });
        }

        public string Address
        {
            get
            {
                return this.string_9;
            }
            set
            {
                this.string_9 = value;
            }
        }

        public string BankAccount
        {
            get
            {
                return this.string_8;
            }
            set
            {
                this.string_8 = value;
            }
        }

        public bool Boolean_0
        {
            get
            {
                return this.bool_7;
            }
        }

        public DateTime CardBeginDate
        {
            get
            {
                return this.dateTime_2;
            }
        }

        public string CardEffectDate
        {
            get
            {
                return this.string_3;
            }
        }

        public string CertPassWord
        {
            get
            {
                return this.string_4;
            }
        }

        public string CipherVersion
        {
            get
            {
                return this.string_15;
            }
        }

        public string CompressCode
        {
            get
            {
                return this.string_12;
            }
            set
            {
                this.string_12 = value;
            }
        }

        public string CorpAgent
        {
            get
            {
                return this.string_14;
            }
        }

        public string CorpCode
        {
            get
            {
                return this.string_11;
            }
        }

        public string Corporation
        {
            get
            {
                return this.string_7;
            }
            set
            {
                if (this.ctaxCardMode_0 == CTaxCardMode.tcmNone)
                {
                    this.string_7 = value;
                }
            }
        }

        public string DZFPFlag
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        public bool EasyLevy
        {
            get
            {
                return this.bool_3;
            }
        }

        public ECardType ECardType
        {
            get
            {
                return this.ecardType_0;
            }
        }

        public string ErrCode
        {
            get
            {
                if (this.int_4 == -1111)
                {
                    return ("CA_" + Math.Abs(this.int_4));
                }
                if (this.int_4 == -1112)
                {
                    return ("CA_" + this.int_6);
                }
                if (this.int_4.ToString().StartsWith("777777"))
                {
                    return ("CA_" + this.int_6);
                }
                if (this.int_5 != -1)
                {
                    return string.Concat(new object[] { "TCD_", this.int_4, "_", this.int_5 });
                }
                return string.Concat(new object[] { "TCD_", this.int_4, "_", " " });
            }
        }

        public string FXSJDZ
        {
            get
            {
                return this.string_22;
            }
        }

        public string FXSJJDC
        {
            get
            {
                return this.string_21;
            }
        }

        public string FXSJJT
        {
            get
            {
                return this.string_23;
            }
        }

        public int InvEleKindCode
        {
            get
            {
                return 0x33;
            }
        }

        public bool IsLargeInvDetail
        {
            get
            {
                return this.bool_2;
            }
        }

        public bool IsLargeStorage
        {
            get
            {
                return this.bool_1;
            }
        }

        public bool IsTaxOpen
        {
            get
            {
                return this.bool_5;
            }
        }

        public bool IsTrain
        {
            get
            {
                if (this._taxCode.Length < 12)
                {
                    return false;
                }
                if (!(this._taxCode.Substring(6, 6) == "999999"))
                {
                    return false;
                }
                return (this.string_7 == "航信培训企业");
            }
        }

        public bool IsUseCert
        {
            get
            {
                return true;
            }
        }

        public DateTime LastRepDate
        {
            get
            {
                return this.dateTime_4;
            }
        }

        public int LastRepDateMonth
        {
            get
            {
                return this.dateTime_4.Month;
            }
        }

        public int LastRepDateYear
        {
            get
            {
                return this.dateTime_4.Year;
            }
        }

        public int Machine
        {
            get
            {
                return this.int_2;
            }
        }

        public string OldTaxCode
        {
            get
            {
                return this.string_24;
            }
        }

        public SQXXLIST QYLX
        {
            get
            {
                this.method_46();
                return this.sqxxlist_0;
            }
        }

        public string RegionCode
        {
            get
            {
                return this.string_18;
            }
        }

        public string RegType
        {
            get
            {
                return this.string_13;
            }
        }

        public DateTime RepDate
        {
            get
            {
                return this.dateTime_6;
            }
        }

        public string Reserve
        {
            get
            {
                return this.string_20;
            }
            set
            {
                this.string_20 = value;
            }
        }

        public int RetCode
        {
            get
            {
                return this.int_4;
            }
            set
            {
                this.int_4 = value;
            }
        }

        public string SoftVersion
        {
            get
            {
                return this.string_19;
            }
            set
            {
                this.string_19 = value;
            }
        }

        public InvSQInfo SQInfo
        {
            get
            {
                return this.invSQInfo_0;
            }
        }

        public TaxStateInfo StateInfo
        {
            get
            {
                return this.taxStateInfo_0;
            }
        }

        public string String_0
        {
            [CompilerGenerated]
            get
            {
                return this.string_34;
            }
            [CompilerGenerated]
            set
            {
                this.string_34 = value;
            }
        }

        public string SubSoftVersion
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public int SysMonth
        {
            get
            {
                return this.dateTime_5.Month;
            }
        }

        public int SysYear
        {
            get
            {
                return this.dateTime_5.Year;
            }
        }

        public DateTime TaxClock
        {
            get
            {
                return this.dateTime_5;
            }
        }

        public string TaxCode
        {
            get
            {
                return this._taxCode;
            }
        }

        public int TaxDay
        {
            get
            {
                return this.dateTime_6.Day;
            }
        }

        public CTaxCardMode TaxMode
        {
            get
            {
                return this.ctaxCardMode_0;
            }
            set
            {
                if (!this.bool_5)
                {
                    this.ctaxCardMode_0 = value;
                }
            }
        }

        public Aisino.FTaxBase.TaxRateAuthorize TaxRateAuthorize
        {
            get
            {
                return this.taxRateAuthorize_0;
            }
        }

        public string Telephone
        {
            get
            {
                return this.string_10;
            }
            set
            {
                this.string_10 = value;
            }
        }

        public delegate bool CustomMsgBox(string retCode);

        public delegate string[] GetInvQryNo(string InvTypeCode, string InvNum, InvoiceType InvType);

        public delegate bool WriteInvVolumn(InvVolumeApp volumn);
    }
}

