namespace Aisino.Fwkp.BusinessObject
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls.PrintControl;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using log4net;
    using ns3;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    public class InvoiceHandler
    {
        private bool bool_0;
        private static byte[] byte_0;
        private static byte[] byte_1;
        private Class37 class37_0;
        private DateTime dateTime_0;
        private Fpxx fpxx_0;
        private static ILog ilog_0;
        private string string_0;
        private TaxCard taxCard_0;
        private static ushort ushort_0;

        static InvoiceHandler()
        {
            
            ilog_0 = LogUtil.GetLogger<InvoiceHandler>();
            byte_0 = new byte[] { 
                0xae, 0x51, 0xdd, 0x44, 14, 0x57, 0xcc, 9, 0x23, 0x8b, 0x45, 0x6a, 0x10, 0x90, 0xaf, 0xbd, 
                0xad, 0xdb, 0xae, 0x8d, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
             };
            byte_1 = new byte[] { 1, 0xae, 0xbb, 170, 0xcb, 0x89, 0x38, 0x89, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 };
        }

        public InvoiceHandler()
        {
            
            this.taxCard_0 = TaxCardFactory.CreateTaxCard();
            this.bool_0 = TaxCardFactory.CreateTaxCard().GetExtandParams("FLBMFlag") == "FLBM";
        }

        public string CheckDkqysh(string string_1)
        {
            if (string.IsNullOrEmpty(string_1))
            {
                return "A681";
            }
            if (string_1.Length > 20)
            {
                return "A682";
            }
            for (int i = 0; i < string_1.Length; i++)
            {
                if ("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(string_1[i]) < 0)
                {
                    return "A683";
                }
            }
            return "0000";
        }

        public static string CheckFpdm(string string_1)
        {
            for (int i = 0; i < string_1.Length; i++)
            {
                if ((i < 15) && !char.IsDigit(string_1[i]))
                {
                    ilog_0.Error("发票代码包含有非法字符=" + string_1[i].ToString());
                    return "A643";
                }
            }
            return "0000";
        }

        public bool CheckHzfwFpxx(Fpxx fpxx_1)
        {
            try
            {
                if (fpxx_1.Mxxx == null)
                {
                    ilog_0.Error("CheckHzfwFpxx 传入的Mxxx is null");
                }
                Fpxx fpxx = fpxx_1.Clone();
                this.string_0 = "0000";
                if (fpxx == null)
                {
                    ilog_0.Error("CheckHzfwFpxx fpxx is null");
                    return false;
                }
                if (fpxx.Mxxx == null)
                {
                    ilog_0.Error("CheckHzfwFpxx Clone后的Mxxx is null");
                }
                this.dateTime_0 = DateTime.Now;
                string str2 = string.Empty;
                string str = this.method_20(fpxx, out str2, true);
                if (str != "0000")
                {
                    ilog_0.Error("CheckHzfwFpxx 获取写卡发票明细串失败，错误号=" + str);
                    this.string_0 = str;
                    return false;
                }
                if (Regex.IsMatch(str2, "\x00a4"))
                {
                    this.string_0 = "A607";
                    return false;
                }
                if (!fpxx.hzfw)
                {
                    this.string_0 = "0000";
                    return true;
                }
                if ((fpxx.fplx != FPLX.ZYFP) && (fpxx.fplx != FPLX.PTFP))
                {
                    this.string_0 = "0000";
                    return true;
                }
                if ((fpxx.Mxxx != null) && (fpxx.Mxxx.Count > 0))
                {
                    if (fpxx.Mxxx.Count > 7)
                    {
                        this.string_0 = "A602";
                        return false;
                    }
                    if (this.taxCard_0.IsTrain)
                    {
                        fpxx.gfsh = "110101000000000";
                        fpxx.gfmc = "仅供培训使用";
                        fpxx.gfdzdh = "仅供培训使用";
                        fpxx.gfyhzh = "仅供培训使用";
                        fpxx.spfnsrsbh = "110101000000000";
                        fpxx.spfmc = "仅供培训使用";
                    }
                    if (((fpxx.fplx == FPLX.PTFP) && (fpxx.Zyfplx == ZYFP_LX.NCP_SG)) && (fpxx.gfsh.Equals(this.taxCard_0.TaxCode) || fpxx.gfsh.Equals(this.taxCard_0.OldTaxCode)))
                    {
                        string gfsh = fpxx.gfsh;
                        fpxx.gfsh = fpxx.xfsh;
                        fpxx.xfsh = gfsh;
                        gfsh = fpxx.gfmc;
                        fpxx.gfmc = fpxx.xfmc;
                        fpxx.xfmc = gfsh;
                        gfsh = fpxx.gfyhzh;
                        fpxx.gfyhzh = fpxx.xfyhzh;
                        fpxx.xfyhzh = gfsh;
                        gfsh = fpxx.gfdzdh;
                        fpxx.gfdzdh = fpxx.xfdzdh;
                        fpxx.xfdzdh = gfsh;
                    }
                    this.string_0 = this.method_26(fpxx);
                    if (this.string_0 != "0000")
                    {
                        return false;
                    }
                    if (!this.method_36(str2))
                    {
                        this.string_0 = "A608";
                        return false;
                    }
                    if (!this.CheckHzfwInvFace(fpxx))
                    {
                        return false;
                    }
                    this.string_0 = "0000";
                    return true;
                }
                this.string_0 = "A601";
                return false;
            }
            catch (Exception exception)
            {
                ilog_0.Error("CheckHzfwFpxx: " + exception.Message);
                this.string_0 = "A699";
                return false;
            }
        }

        public bool CheckHzfwFpxxForWBJK(Fpxx fpxx_1)
        {
            try
            {
                Fpxx fpxx = fpxx_1.Clone();
                this.string_0 = "0000";
                if (fpxx != null)
                {
                    this.dateTime_0 = DateTime.Now;
                    string str3 = string.Empty;
                    string str2 = this.method_20(fpxx, out str3, true);
                    if (str2 != "0000")
                    {
                        ilog_0.Error("CheckHzfwFpxx 获取写卡发票明细串失败，错误号=" + str2);
                        this.string_0 = str2;
                        return false;
                    }
                    if (Regex.IsMatch(str3, "\x00a4"))
                    {
                        this.string_0 = "A607";
                        return false;
                    }
                    if (!fpxx.hzfw)
                    {
                        this.string_0 = "0000";
                        return true;
                    }
                    if ((fpxx.fplx != FPLX.ZYFP) && (fpxx.fplx != FPLX.PTFP))
                    {
                        this.string_0 = "0000";
                        return true;
                    }
                    if ((fpxx.Mxxx != null) && (fpxx.Mxxx.Count > 0))
                    {
                        if (this.taxCard_0.IsTrain)
                        {
                            fpxx.gfsh = "110101000000000";
                            fpxx.gfmc = "仅供培训使用";
                            fpxx.gfdzdh = "仅供培训使用";
                            fpxx.gfyhzh = "仅供培训使用";
                            fpxx.spfnsrsbh = "110101000000000";
                            fpxx.spfmc = "仅供培训使用";
                        }
                        if (((fpxx.fplx == FPLX.PTFP) && (fpxx.Zyfplx == ZYFP_LX.NCP_SG)) && (fpxx.gfsh.Equals(this.taxCard_0.TaxCode) || fpxx.gfsh.Equals(this.taxCard_0.OldTaxCode)))
                        {
                            string gfsh = fpxx.gfsh;
                            fpxx.gfsh = fpxx.xfsh;
                            fpxx.xfsh = gfsh;
                            gfsh = fpxx.gfmc;
                            fpxx.gfmc = fpxx.xfmc;
                            fpxx.xfmc = gfsh;
                            gfsh = fpxx.gfyhzh;
                            fpxx.gfyhzh = fpxx.xfyhzh;
                            fpxx.xfyhzh = gfsh;
                            gfsh = fpxx.gfdzdh;
                            fpxx.gfdzdh = fpxx.xfdzdh;
                            fpxx.xfdzdh = gfsh;
                        }
                        this.string_0 = this.method_26(fpxx);
                        if (this.string_0 != "0000")
                        {
                            return false;
                        }
                        if (!this.method_36(str3))
                        {
                            this.string_0 = "A608";
                            return false;
                        }
                        if (!this.CheckHzfwInvFace(fpxx))
                        {
                            return false;
                        }
                        this.string_0 = "0000";
                        return true;
                    }
                    this.string_0 = "A601";
                }
                return false;
            }
            catch (Exception exception)
            {
                ilog_0.Error("CheckHzfwFpxx: " + exception.Message);
                this.string_0 = "A699";
                return false;
            }
        }

        public bool CheckHzfwInvFace(Fpxx fpxx_1)
        {
            this.fpxx_0 = this.ConvertInvoiceToZH(fpxx_1);
            if (((this.fpxx_0 != null) && (this.fpxx_0.Mxxx.Count >= fpxx_1.Mxxx.Count)) && (this.fpxx_0.Mxxx.Count > 0))
            {
                if (this.fpxx_0.Mxxx.Count > 7)
                {
                    this.string_0 = "A605";
                    return false;
                }
                this.class37_0 = this.method_29(fpxx_1, this.fpxx_0);
                if (this.class37_0 == null)
                {
                    this.string_0 = "A611";
                    return false;
                }
                int num5 = ToolUtil.GetByteCount(this.class37_0.method_0()) + this.class37_0.method_2().Count;
                if (num5 > 0x150)
                {
                    this.string_0 = "A606";
                    return false;
                }
                int num2 = this.class37_0.method_2().Count - 1;
                for (int i = 0; i < this.class37_0.method_2().Count; i++)
                {
                    if (((i != 2) && (i != 3)) && ((i <= 3) || ((i % 3) != 0)))
                    {
                        string str = this.class37_0.method_2()[i];
                        int length = str.Length;
                        int num3 = 0;
                        if (length > 0)
                        {
                            num3 = length / 8;
                            if ((length % 8) > 0)
                            {
                                num3++;
                            }
                        }
                        num2 += num3;
                    }
                }
                if (num2 > 0x2e)
                {
                    this.string_0 = "A606";
                    return false;
                }
                this.string_0 = "0000";
                return true;
            }
            this.string_0 = "A604";
            return false;
        }

        public string CheckTaxCode(string string_1, FPLX fplx_0)
        {
            return this.method_1(string_1, false, fplx_0);
        }

        public string CheckZzjgdm(string string_1)
        {
            if (string.IsNullOrEmpty(string_1))
            {
                return "A638";
            }
            if (string_1.Length > 20)
            {
                return "A639";
            }
            for (int i = 0; i < string_1.Length; i++)
            {
                if ("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(string_1[i]) < 0)
                {
                    return "A640";
                }
            }
            return "0000";
        }

        public string CheckZzjgdm_New(string string_1)
        {
            if (string.IsNullOrEmpty(string_1))
            {
                return "A638";
            }
            if (string_1.Length > 0x16)
            {
                return "A639";
            }
            return "0000";
        }

        public static string ComputeFloatNumber(string string_1, int int_0)
        {
            int length = string_1.Length;
            int index = string_1.IndexOf('.');
            bool flag = true;
            if ((length > 0) && (string_1[0] == '-'))
            {
                string_1 = string_1.Substring(1, length - 1);
                length = string_1.Length;
                index = string_1.IndexOf('.');
                flag = false;
            }
            if (index == -1)
            {
                int_0 = length;
            }
            else if (index > int_0)
            {
                int_0 = index + 1;
            }
            if (length > int_0)
            {
                string_1 = string_1.Substring(0, int_0 + 1);
                length = string_1.Length;
                decimal num3 = ObjectToDecimal(string_1);
                int num4 = (length - index) - 2;
                if (num4 < 0)
                {
                    num4 = 0;
                }
                decimal num5 = smethod_1(num3, (double) num4);
                if (!flag)
                {
                    return ("-" + num5.ToString());
                }
                return num5.ToString();
            }
            if (index != -1)
            {
                int num6 = 0;
                num6 = index + 1;
                while (num6 < length)
                {
                    if (string_1[num6] != '0')
                    {
                        break;
                    }
                    num6++;
                }
                if (num6 == length)
                {
                    string_1 = string_1.Substring(0, index);
                }
            }
            if (!flag)
            {
                return ("-" + string_1);
            }
            return string_1;
        }

        public Fpxx ConvertInvoiceToZH(Fpxx fpxx_1)
        {
            Fpxx fpxx;
            try
            {
                if (fpxx_1 == null)
                {
                    ilog_0.Error("[ConvertInvoiceToZH] 传入的发票信息为空");
                    return fpxx_1;
                }
                if (!fpxx_1.hzfw)
                {
                    goto Label_02AF;
                }
                if ((fpxx_1.yysbz != string.Empty) && (fpxx_1.yysbz[1] == '0'))
                {
                    return fpxx_1;
                }
                this.fpxx_0 = fpxx_1.Clone();
                this.fpxx_0.Mxxx = new List<Dictionary<SPXX, string>>();
                int num = 1;
                if (fpxx_1.Mxxx == null)
                {
                    ilog_0.Error("[ConvertInvoiceToZH] 传入的发票明细信息为空");
                    return fpxx_1;
                }
                using (List<Dictionary<SPXX, string>>.Enumerator enumerator = fpxx_1.Mxxx.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        Dictionary<SPXX, string> current = enumerator.Current;
                        string str = current[SPXX.SPMC];
                        string str2 = current[SPXX.JLDW];
                        string[] strArray = Common.ZheHang(str, "spmc");
                        string[] strArray2 = Common.ZheHang(str2, "jldw");
                        if ((strArray == null) || (strArray2 == null))
                        {
                            goto Label_027A;
                        }
                        int num3 = Math.Max(strArray.Length, strArray2.Length);
                        for (int i = 0; i < num3; i++)
                        {
                            if (i == 0)
                            {
                                Dictionary<SPXX, string> item = new Dictionary<SPXX, string>(current);
                                if (strArray.Length > i)
                                {
                                    item[SPXX.SPMC] = strArray[i];
                                }
                                else
                                {
                                    item[SPXX.SPMC] = "";
                                }
                                if (strArray2.Length > i)
                                {
                                    item[SPXX.JLDW] = strArray2[i];
                                }
                                else
                                {
                                    item[SPXX.JLDW] = "";
                                }
                                item[SPXX.BZ] = num.ToString();
                                this.fpxx_0.Mxxx.Add(item);
                            }
                            else
                            {
                                Dictionary<SPXX, string> dictionary3 = new Dictionary<SPXX, string>(12);
                                dictionary3[SPXX.SPMC] = "";
                                dictionary3[SPXX.JLDW] = "";
                                dictionary3[SPXX.GGXH] = "";
                                dictionary3[SPXX.SL] = "";
                                dictionary3[SPXX.DJ] = "";
                                dictionary3[SPXX.JE] = "";
                                dictionary3[SPXX.SLV] = "";
                                dictionary3[SPXX.SE] = "";
                                dictionary3[SPXX.HSJBZ] = "";
                                dictionary3[SPXX.SPSM] = "";
                                dictionary3[SPXX.FPHXZ] = "";
                                dictionary3[SPXX.BZ] = num.ToString();
                                if (strArray.Length > i)
                                {
                                    dictionary3[SPXX.SPMC] = strArray[i];
                                }
                                if (strArray2.Length > i)
                                {
                                    dictionary3[SPXX.JLDW] = strArray2[i];
                                }
                                this.fpxx_0.Mxxx.Add(dictionary3);
                            }
                        }
                        num++;
                    }
                    goto Label_02A6;
                Label_027A:
                    this.string_0 = "A614";
                    ilog_0.Error("[ConvertInvoiceToZH] 折行接口调用失败");
                    return null;
                }
            Label_02A6:
                return this.fpxx_0;
            Label_02AF:
                fpxx = fpxx_1;
            }
            catch (Exception exception)
            {
                ilog_0.Error("[ConvertInvoiceToZH] " + exception.Message);
                fpxx = fpxx_1;
            }
            return fpxx;
        }

        public string GetCode()
        {
            return this.string_0;
        }

        public string MakeInvoice(Fpxx fpxx_1, bool bool_1, byte[] byte_2)
        {
            string str = "0000";
            try
            {
                if (string.Equals(this.taxCard_0.SoftVersion, "FWKP_V2.0_Svr_Client"))
                {
                    ilog_0.Debug("[MakeInvoice] 非法调用：开票服务器客户端不能调用此接口");
                    return "A693";
                }
                if (fpxx_1 == null)
                {
                    ilog_0.Debug("[MakeInvoice] 传入的发票信息为空");
                    return "A651";
                }
                if (byte_2 == null)
                {
                    ilog_0.Debug("[MakeInvoice] 传入的校验信息为空");
                    return "A691";
                }
                string a = BitConverter.ToString(MD5_Crypt.GetHash(fpxx_1.fpdm + "AISINOKPS" + fpxx_1.fphm));
                string b = BitConverter.ToString(byte_2);
                if (!string.Equals(a, b))
                {
                    ilog_0.Error("[MakeInvoice] 传入的校验信息验证失败");
                    return "A692";
                }
                if (!bool_1)
                {
                    return this.method_16(fpxx_1);
                }
                str = this.method_17(fpxx_1, true);
            }
            catch (Exception exception)
            {
                str = "A699";
                ilog_0.Error("[MakeInvoice] 出现异常：" + exception.Message);
            }
            return str;
        }

        private string method_0(string string_1, bool bool_1, FPLX fplx_0)
        {
            char ch;
            if (string.Equals(string_1, "000000123456789"))
            {
                return "A641";
            }
            if ((fplx_0 == FPLX.HYFP) || (fplx_0 == FPLX.JDCFP))
            {
                if (fplx_0 == FPLX.HYFP)
                {
                    if (string.IsNullOrEmpty(string_1))
                    {
                        return "A631";
                    }
                    if (((string_1.Length != 15) && (string_1.Length != 0x11)) && ((string_1.Length != 0x12) && (string_1.Length != 20)))
                    {
                        return "A632";
                    }
                }
                if (fplx_0 == FPLX.JDCFP)
                {
                    if (string_1 == null)
                    {
                        string_1 = string.Empty;
                    }
                    if ((string_1.Length > 0) && ((string_1.Length < 9) || (string_1.Length > 20)))
                    {
                        return "A632";
                    }
                }
                for (int k = 0; k < string_1.Length; k++)
                {
                    if ("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(string_1[k]) < 0)
                    {
                        return "A635";
                    }
                }
                return "0000";
            }
            if ((fplx_0 == FPLX.PTFP) && string.IsNullOrEmpty(string_1))
            {
                return "0000";
            }
            if (string.IsNullOrEmpty(string_1))
            {
                return "A631";
            }
            if (((string_1.Length != 15) && (string_1.Length != 0x11)) && ((string_1.Length != 0x12) && (string_1.Length != 20)))
            {
                return "A632";
            }
            if (!bool_1 && string_1.Substring(0, 1).Equals("0"))
            {
                return "A633";
            }
            if (string_1.Length != 15)
            {
                if (string_1.Length == 0x11)
                {
                    for (int num5 = 0; num5 < string_1.Length; num5++)
                    {
                        if (num5 < 15)
                        {
                            if (!char.IsDigit(string_1[num5]))
                            {
                                return "A635";
                            }
                        }
                        else if ("0123456789ABCDEFGHJKLMNPQRTUVWXY".IndexOf(string_1[num5]) < 0)
                        {
                            return "A635";
                        }
                    }
                    return "0000";
                }
                if ((string_1.Length != 0x12) && (string_1.Length != 20))
                {
                    return "A632";
                }
                for (int m = 0; m < string_1.Length; m++)
                {
                    if (m < 0x11)
                    {
                        if (!char.IsDigit(string_1[m]))
                        {
                            return "A635";
                        }
                    }
                    else if (m == 0x11)
                    {
                        if (!char.IsDigit(string_1[m]) && (string_1[m] != 'X'))
                        {
                            return "A635";
                        }
                    }
                    else if ("0123456789ABCDEFGHJKLMNPQRTUVWXY".IndexOf(string_1[m]) < 0)
                    {
                        return "A635";
                    }
                }
                try
                {
                    DateTime time = DateTime.ParseExact(string_1.Substring(6, 8), "yyyyMMdd", null);
                    if ((time.Year < 0x76c) || (time.Year > 0x815))
                    {
                        return "A637";
                    }
                }
                catch (Exception)
                {
                    return "A637";
                }
                int index = 0;
                byte[] buffer = new byte[] { 
                    7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 
                    2, 1
                 };
                char[] chArray = new char[] { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
                for (int n = 0; n < 0x11; n++)
                {
                    char ch2 = string_1[n];
                    index += int.Parse(ch2.ToString()) * buffer[n];
                }
                index = index % 11;
                if (string_1[0x11] == chArray[index])
                {
                    return "0000";
                }
                return "A636";
            }
            int num2 = 15;
            for (int i = 0; i < string_1.Length; i++)
            {
                if (!char.IsDigit(string_1[i]))
                {
                    num2 = i;
                    break;
                }
            }
            if (num2 == 15)
            {
                return "0000";
            }
            if (num2 < 6)
            {
                return "A634";
            }
            int num6 = 0;
            int[] numArray = new int[] { 3, 7, 9, 10, 5, 8, 4, 2 };
            for (int j = 6; j < 14; j++)
            {
                if ("0123456789ABCDEFGHJKLMNPQRTUVWXY".IndexOf(string_1[j]) < 0)
                {
                    return "A635";
                }
                num6 += numArray[j - 6] * ((string_1[j] <= '9') ? (string_1[j] - '0') : ((string_1[j] - 'A') + 10));
            }
            num6 = 11 - (num6 % 11);
            switch (num6)
            {
                case 10:
                    ch = 'X';
                    break;

                case 11:
                    ch = '0';
                    break;

                default:
                    ch = (char) (num6 + 0x30);
                    break;
            }
            if (string_1[14] == ch)
            {
                return "0000";
            }
            return "A636";
        }

        private string method_1(string string_1, bool bool_1, FPLX fplx_0)
        {
            string a = string_1;
            if (a == null)
            {
                a = string.Empty;
            }
            a = a.ToUpper();
            if (string.Equals(a, "000000123456789"))
            {
                return "A641";
            }
            if ((fplx_0 != FPLX.HYFP) && (fplx_0 != FPLX.JDCFP))
            {
                if ((a.Length < 6) || (a.Length > 20))
                {
                    return "A632";
                }
                if (a.Length <= 15)
                {
                    if (!this.method_3(a))
                    {
                        return "A635";
                    }
                    return "0000";
                }
                if ((a.Length >= 0x10) && (a.Length <= 0x12))
                {
                    if ((!this.method_5(a) && !this.method_6(a)) && !this.method_7(a))
                    {
                        return "A635";
                    }
                    return "0000";
                }
                if (((!this.method_11(a) && !this.method_12(a)) && (!this.method_13(a) && !this.method_14(a))) && !this.method_15(a))
                {
                    return "A635";
                }
                return "0000";
            }
            if ((a.Length < 6) || (a.Length > 20))
            {
                return "A632";
            }
            if (!this.method_3(a))
            {
                return "A635";
            }
            return "0000";
        }

        private bool method_10(string string_1)
        {
            if (string.IsNullOrEmpty(string_1))
            {
                string_1 = string.Empty;
            }
            if (string_1.Length == 0x12)
            {
                for (int i = 0; i < string_1.Length; i++)
                {
                    if (i < 0x11)
                    {
                        if (!char.IsDigit(string_1[i]))
                        {
                            return false;
                        }
                    }
                    else if (((i == 0x11) && !char.IsDigit(string_1[i])) && (string_1[i] != 'X'))
                    {
                        return false;
                    }
                }
                try
                {
                    DateTime time = DateTime.ParseExact(string_1.Substring(6, 8), "yyyyMMdd", null);
                    if ((time.Year < 0x76c) || (time.Year > 0x815))
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                int index = 0;
                byte[] buffer = new byte[] { 
                    7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 
                    2, 1
                 };
                char[] chArray = new char[] { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
                for (int j = 0; j < 0x11; j++)
                {
                    char ch = string_1[j];
                    index += int.Parse(ch.ToString()) * buffer[j];
                }
                index = index % 11;
                if (string_1[0x11] == chArray[index])
                {
                    return true;
                }
            }
            return false;
        }

        private bool method_11(string string_1)
        {
            if (string.IsNullOrEmpty(string_1))
            {
                string_1 = string.Empty;
            }
            if ((string_1.Length < 0x13) || (string_1.Length > 20))
            {
                return false;
            }
            string str2 = string_1.Substring(0, 15);
            if (!this.method_8(str2))
            {
                return false;
            }
            string str = string_1.Substring(15);
            return this.method_3(str);
        }

        private bool method_12(string string_1)
        {
            if (string.IsNullOrEmpty(string_1))
            {
                string_1 = string.Empty;
            }
            if ((string_1.Length < 0x13) || (string_1.Length > 20))
            {
                return false;
            }
            string str = string_1.Substring(0, 0x12);
            if (!this.method_9(str))
            {
                return false;
            }
            string str2 = string_1.Substring(0x12);
            return this.method_3(str2);
        }

        private bool method_13(string string_1)
        {
            if (string.IsNullOrEmpty(string_1))
            {
                string_1 = string.Empty;
            }
            if ((string_1.Length < 0x13) || (string_1.Length > 20))
            {
                return false;
            }
            string str2 = string_1.Substring(0, 1);
            if (!this.method_2(str2[0]))
            {
                return false;
            }
            string str3 = string_1.Substring(1, 15);
            if (!this.method_8(str3))
            {
                return false;
            }
            string str = string_1.Substring(0x10);
            return this.method_3(str);
        }

        private bool method_14(string string_1)
        {
            if (string.IsNullOrEmpty(string_1))
            {
                string_1 = string.Empty;
            }
            if ((string_1.Length < 0x13) || (string_1.Length > 20))
            {
                return false;
            }
            string str = string_1.Substring(0, 1);
            if (!this.method_2(str[0]))
            {
                return false;
            }
            string str2 = string_1.Substring(1, 0x12);
            if (!this.method_9(str2))
            {
                return false;
            }
            if (string_1.Length == 20)
            {
                string str3 = string_1.Substring(0x12);
                return this.method_3(str3);
            }
            return true;
        }

        private bool method_15(string string_1)
        {
            if (string.IsNullOrEmpty(string_1))
            {
                string_1 = string.Empty;
            }
            if ((string_1.Length < 0x13) || (string_1.Length > 20))
            {
                return false;
            }
            string str = string_1.Substring(0, 6);
            if (!this.method_4(str))
            {
                return false;
            }
            string str2 = string_1.Substring(6);
            for (int i = 0; i < str2.Length; i++)
            {
                if ("0123456789ABCDEFGHJKLMNPQRTUVWXY".IndexOf(str2[i]) < 0)
                {
                    return false;
                }
            }
            return true;
        }

        internal string method_16(Fpxx fpxx_1)
        {
            string str = "0000";
            ilog_0.Debug("[MakeInvoice] 本接口调用开始");
            try
            {
                InvCodeNum num7;
                DateTime time;
                string str34;
                InvoiceResult result2;
                InvDetail detail;
                if (fpxx_1 == null)
                {
                    ilog_0.Debug("[MakeInvoice] 传入的发票信息为空");
                    return "A651";
                }
                if (!string.Equals(this.taxCard_0.SoftVersion, "FWKP_V2.0_Svr_Server"))
                {
                    int num = -1;
                    if (fpxx_1.data != null)
                    {
                        string a = BitConverter.ToString(MD5_Crypt.GetHash(fpxx_1.fpdm + "00" + fpxx_1.fphm));
                        string b = BitConverter.ToString((byte[]) fpxx_1.data[0]);
                        if (string.Equals(a, b))
                        {
                            num = 0;
                            ilog_0.Debug("[MakeInvoice] 手工开具");
                        }
                        else if (fpxx_1.data.Length >= 3)
                        {
                            string str5 = BitConverter.ToString(MD5_Crypt.GetHash(fpxx_1.fpdm + "01" + fpxx_1.fphm));
                            string str6 = BitConverter.ToString(MD5_Crypt.GetHash(fpxx_1.fpdm + "02" + fpxx_1.fphm));
                            string str7 = BitConverter.ToString((byte[]) fpxx_1.data[2]);
                            if (string.Equals(str5, str7))
                            {
                                num = 1;
                                ilog_0.Debug("[MakeInvoice] 文本开具");
                            }
                            else if (string.Equals(str6, str7))
                            {
                                num = 2;
                                ilog_0.Debug("[MakeInvoice] 组件开具");
                            }
                        }
                    }
                    if (num != 0)
                    {
                        ilog_0.Debug("[MakeInvoice] 组件开具，开始还原发票信息");
                        string[] strArray = (string[]) fpxx_1.data[1];
                        int[] numArray = Invoice.int_1;
                        fpxx_1.fphm = strArray[numArray[0]];
                        fpxx_1.xsdjbh = strArray[numArray[1]];
                        fpxx_1.gfmc = strArray[numArray[2]];
                        fpxx_1.xfdzdh = strArray[numArray[3]];
                        fpxx_1.xfyhzh = strArray[numArray[4]];
                        fpxx_1.fpdm = strArray[numArray[5]];
                        fpxx_1.bz = strArray[numArray[6]];
                        if (fpxx_1.Zyfplx == ZYFP_LX.CEZS)
                        {
                            string str8 = fpxx_1.method_1();
                            if (fpxx_1.bz.IndexOf(str8) < 0)
                            {
                                fpxx_1.bz = str8 + fpxx_1.bz;
                            }
                        }
                        fpxx_1.bz = Convert.ToBase64String(ToolUtil.GetBytes(fpxx_1.bz));
                        fpxx_1.gfdzdh = strArray[numArray[7]];
                        fpxx_1.gfyhzh = strArray[numArray[8]];
                        fpxx_1.fhr = strArray[numArray[9]];
                        fpxx_1.skr = strArray[numArray[11]];
                        fpxx_1.gfsh = strArray[numArray[12]];
                        fpxx_1.kpr = strArray[numArray[13]];
                        if ((((num == 1) && (fpxx_1.fplx == FPLX.PTFP)) && (fpxx_1.Zyfplx == ZYFP_LX.NCP_SG)) && (fpxx_1.gfsh.Equals(this.taxCard_0.TaxCode) || fpxx_1.gfsh.Equals(this.taxCard_0.OldTaxCode)))
                        {
                            string str9 = fpxx_1.gfsh;
                            fpxx_1.gfsh = fpxx_1.xfsh;
                            fpxx_1.xfsh = str9;
                            str9 = fpxx_1.gfmc;
                            fpxx_1.gfmc = fpxx_1.xfmc;
                            fpxx_1.xfmc = str9;
                            str9 = fpxx_1.gfyhzh;
                            fpxx_1.gfyhzh = fpxx_1.xfyhzh;
                            fpxx_1.xfyhzh = str9;
                            str9 = fpxx_1.gfdzdh;
                            fpxx_1.gfdzdh = fpxx_1.xfdzdh;
                            fpxx_1.xfdzdh = str9;
                        }
                        ilog_0.Debug("[MakeInvoice] 组件开具，还原发票信息结束");
                    }
                }
                if (string.Equals(this.taxCard_0.SoftVersion, "FWKP_V2.0_Svr_Client"))
                {
                    InvoiceResult result;
                    ilog_0.Debug("[MakeInvoice] 开票服务器客户端，准备序列化");
                    string str10 = "";
                    byte[] buffer = null;
                    string bz = fpxx_1.bz;
                    string yshwxx = fpxx_1.yshwxx;
                    if (this.taxCard_0.SubSoftVersion != "Linux")
                    {
                        buffer = SerializeUtil.Serialize(fpxx_1);
                    }
                    else
                    {
                        if ((fpxx_1.Mxxx != null) && (fpxx_1.Mxxx.Count > 0))
                        {
                            for (int i = 0; i < fpxx_1.Mxxx.Count; i++)
                            {
                                fpxx_1.Mxxx[i][SPXX.SL] = ComputeFloatNumber(fpxx_1.Mxxx[i][SPXX.SL], 9);
                            }
                        }
                        if (fpxx_1.fplx == FPLX.JSFP)
                        {
                            int num3 = 0;
                            Dictionary<string, int> jsPrintTemplate = ToolUtil.GetJsPrintTemplate();
                            if ((jsPrintTemplate.Count > 0) && jsPrintTemplate.ContainsKey(fpxx_1.dy_mb))
                            {
                                num3 = jsPrintTemplate[fpxx_1.dy_mb];
                            }
                            fpxx_1.dy_mb = num3.ToString();
                        }
                        byte[] bytes = fpxx_1.Seriealize();
                        string name = "GBK";
                        string input = Encoding.GetEncoding(name).GetString(bytes);
                        if (Regex.IsMatch(input, "\x00a4"))
                        {
                            this.string_0 = "A607";
                            return this.string_0;
                        }
                        buffer = Encoding.GetEncoding(name).GetBytes(input);
                    }
                    str10 = ToolUtil.ToBase64String(buffer);
                    InvCodeNum num4 = new InvCodeNum();
                    ilog_0.Debug("[MakeInvoice] 开票服务器客户端，开始调用开票接口");
                    string str15 = this.taxCard_0.Invoice(str10, 0.0, 0.0, 0.0, string.Empty, string.Empty, null, InvoiceType.special, null, string.Empty, string.Empty, num4, null, 0, out result);
                    ilog_0.Debug("[MakeInvoice] 开票服务器客户端，调用开票接口结束");
                    switch (ToolUtil.GetReturnErrCode(str15))
                    {
                        case 0:
                        {
                            str = "0000";
                            Fpxx fpxx = null;
                            if (this.taxCard_0.SubSoftVersion != "Linux")
                            {
                                fpxx = Fpxx.DeSeriealize(ToolUtil.FromBase64String(this.taxCard_0.RegType));
                            }
                            else
                            {
                                fpxx = Fpxx.DeSeriealize_Linux(ToolUtil.FromBase64String(this.taxCard_0.RegType));
                            }
                            fpxx_1.bsq = fpxx.bsq;
                            fpxx_1.bszt = fpxx.bszt;
                            fpxx_1.dzsyh = fpxx.dzsyh;
                            fpxx_1.fpdm = fpxx.fpdm;
                            fpxx_1.fphm = fpxx.fphm;
                            fpxx_1.hxm = fpxx.hxm;
                            fpxx_1.hzfw = fpxx.hzfw;
                            fpxx_1.isBlankWaste = fpxx.isBlankWaste;
                            fpxx_1.jmbbh = fpxx.jmbbh;
                            fpxx_1.jqbh = fpxx.jqbh;
                            fpxx_1.jym = fpxx.jym;
                            fpxx_1.kprq = fpxx.kprq;
                            fpxx_1.kpsxh = fpxx.kpsxh;
                            fpxx_1.mw = fpxx.mw;
                            fpxx_1.retCode = fpxx.retCode;
                            fpxx_1.sign = fpxx.sign;
                            fpxx_1.ssyf = fpxx.ssyf;
                            fpxx_1.yysbz = fpxx.yysbz;
                            fpxx_1.zfbz = fpxx.zfbz;
                            fpxx_1.zfsj = fpxx.zfsj;
                            fpxx_1.gfsh = fpxx.gfsh;
                            fpxx_1.bz = bz;
                            fpxx_1.yshwxx = yshwxx;
                            return str;
                        }
                        case 1:
                            str = str15;
                            break;

                        default:
                            str = ToolUtil.GetReturnErrCode(str15).ToString();
                            break;
                    }
                    ilog_0.Debug("[MakeInvoice] 远程开票接口调用失败，错误号=" + str15);
                    return str;
                }
                fpxx_1.gfsh = string.IsNullOrEmpty(fpxx_1.gfsh) ? new string('0', 15) : fpxx_1.gfsh;
                InvoiceType special = InvoiceType.special;
                if (fpxx_1.fplx == FPLX.ZYFP)
                {
                    special = InvoiceType.special;
                }
                else if (fpxx_1.fplx == FPLX.PTFP)
                {
                    special = InvoiceType.common;
                }
                else if (fpxx_1.fplx == FPLX.HYFP)
                {
                    special = InvoiceType.transportation;
                }
                else if (fpxx_1.fplx == FPLX.JDCFP)
                {
                    special = InvoiceType.vehiclesales;
                }
                else if (fpxx_1.fplx == FPLX.DZFP)
                {
                    special = InvoiceType.Electronic;
                }
                else if (fpxx_1.fplx == FPLX.JSFP)
                {
                    special = InvoiceType.volticket;
                }
                ilog_0.Debug("[MakeInvoice] 获取发票代码号码开始.");
                string str16 = this.taxCard_0.GetCurrentInvCode(special, fpxx_1.kpjh, out num7);
                if (ToolUtil.GetReturnErrCode(str16) != 0)
                {
                    str = str16;
                    ilog_0.Error("获取发票代码号码失败，错误号：" + str);
                    return str;
                }
                if ((num7.InvTypeCode == null) || (num7.InvNum == null))
                {
                    goto Label_1623;
                }
                if (!string.IsNullOrEmpty(fpxx_1.fpdm) && !string.IsNullOrEmpty(fpxx_1.fphm))
                {
                    ilog_0.Debug("[MakeInvoice] 调用接口取到的发票代码1=" + num7.InvTypeCode);
                    ilog_0.Debug("[MakeInvoice] 调用接口取到的发票号码1=" + num7.InvNum);
                    int count = num7.InvNum.Length - fpxx_1.fphm.Length;
                    if (count > 0)
                    {
                        fpxx_1.fphm = new string('0', count) + fpxx_1.fphm;
                    }
                    if (!num7.InvTypeCode.Equals(fpxx_1.fpdm) || !num7.InvNum.Equals(fpxx_1.fphm))
                    {
                        ilog_0.Debug("[MakeInvoice] 传入的发票代码1=" + fpxx_1.fpdm);
                        ilog_0.Debug("[MakeInvoice] 传入的发票号码1=" + fpxx_1.fphm);
                        ilog_0.Error("[MakeInvoice] 传入的发票代码号码和从金税盘中取到的不一致");
                        return "A654";
                    }
                }
                else
                {
                    fpxx_1.fpdm = num7.InvTypeCode;
                    fpxx_1.fphm = num7.InvNum;
                    ilog_0.Debug("[MakeInvoice] 调用接口取到的发票代码2=" + num7.InvTypeCode);
                    ilog_0.Debug("[MakeInvoice] 调用接口取到的发票号码2=" + num7.InvNum);
                }
                string str17 = CheckFpdm(fpxx_1.fpdm);
                if (!str17.Equals("0000"))
                {
                    ilog_0.Error("调用取代码号码接口取到的发票代码不正确，代码=" + num7.InvTypeCode + "，号码=" + num7.InvNum);
                    return str17;
                }
                if ((fpxx_1.isRed && (fpxx_1.fpdm == fpxx_1.blueFpdm)) && (fpxx_1.fphm.PadLeft(8, '0') == fpxx_1.blueFphm.PadLeft(8, '0')))
                {
                    ilog_0.Debug("红字发票的备注中代码号码和发票当前代码号码一致");
                    return "A692";
                }
                string gfsh = fpxx_1.gfsh;
                string gfmc = fpxx_1.gfmc;
                string gfdzdh = fpxx_1.gfdzdh;
                string gfyhzh = fpxx_1.gfyhzh;
                string spfnsrsbh = fpxx_1.spfnsrsbh;
                string spfmc = fpxx_1.spfmc;
                if (this.taxCard_0.IsTrain)
                {
                    fpxx_1.gfsh = "110101000000000";
                    fpxx_1.gfmc = "仅供培训使用";
                    fpxx_1.gfdzdh = "仅供培训使用";
                    fpxx_1.gfyhzh = "仅供培训使用";
                    fpxx_1.bz = Convert.ToBase64String(ToolUtil.GetBytes("仅供培训使用"));
                    fpxx_1.spfnsrsbh = "110101000000000";
                    fpxx_1.spfmc = "仅供培训使用";
                }
                ilog_0.Debug("[MakeInvoice] 开始取金税盘时钟.");
                this.taxCard_0.GetCardClock(out time, fpxx_1.kpjh);
                this.dateTime_0 = time;
                DateTime time2 = new DateTime(0x7d0, 1, 1);
                if (DateTime.Compare(this.dateTime_0, time2) < 0)
                {
                    ilog_0.Error("MakeInvoice 开票时调用接口取到的开票日期不正确，开票日期=" + this.dateTime_0.ToString());
                    return "A667";
                }
                fpxx_1.kprq = ToolUtil.FormatDateTime(this.dateTime_0);
                ilog_0.Debug("kprq=" + fpxx_1.kprq);
                fpxx_1.ssyf = int.Parse(fpxx_1.kprq.Substring(0, 4) + fpxx_1.kprq.Substring(5, 2));
                string str24 = string.Empty;
                ilog_0.Debug("[MakeInvoice] 开始生成MX.");
                string str25 = this.method_20(fpxx_1, out str24, false);
                if (str25 != "0000")
                {
                    ilog_0.Error("MakeInvoice 获取写卡发票明细串失败，错误号=" + str25);
                    return str25;
                }
                ilog_0.Debug("[MakeInvoice] 生成MX成功.");
                if ((((fpxx_1.fplx == FPLX.ZYFP) || (fpxx_1.fplx == FPLX.PTFP)) || ((fpxx_1.fplx == FPLX.DZFP) || (fpxx_1.fplx == FPLX.JSFP))) && Regex.IsMatch(str24, "\x00a4"))
                {
                    return "A607";
                }
                string redNum = string.Empty;
                if (fpxx_1.isRed && ((fpxx_1.fplx == FPLX.ZYFP) || (fpxx_1.fplx == FPLX.HYFP)))
                {
                    redNum = fpxx_1.redNum;
                }
                redNum = redNum + 0xfc.ToString();
                byte[] buffer4 = null;
                if (fpxx_1.hzfw)
                {
                    ilog_0.Debug("[MakeInvoice] 汉字防伪发票，开始生成特定字节.");
                    buffer4 = this.method_25(fpxx_1);
                    if ((buffer4 == null) || (buffer4.Length != 0x20))
                    {
                        return "A653";
                    }
                    ilog_0.Debug("[MakeInvoice] 汉字防伪发票，生成特定字节成功.");
                }
                double num9 = double.Parse(fpxx_1.je);
                double num10 = double.Parse(fpxx_1.se);
                if ((fpxx_1.fplx == FPLX.ZYFP) && (fpxx_1.Zyfplx == ZYFP_LX.HYSY))
                {
                    num9 += num10;
                }
                string str27 = fpxx_1.gfsh;
                string str28 = string.IsNullOrEmpty(str27) ? new string('0', 15) : str27;
                bool flag = ((!string.IsNullOrEmpty(fpxx_1.sLv) && Class34.smethod_10("0.05", fpxx_1.sLv)) && (fpxx_1.fplx == FPLX.ZYFP)) && (fpxx_1.Zyfplx != ZYFP_LX.HYSY);
                ilog_0.DebugFormat("[MakeInvoice] 税率={0}，是否是非海洋石油5%：{1}", fpxx_1.sLv, flag ? "是" : "否");
                double num11 = ((string.IsNullOrEmpty(fpxx_1.sLv) || flag) || ((fpxx_1.Zyfplx == ZYFP_LX.JZ_50_15) || (fpxx_1.Zyfplx == ZYFP_LX.CEZS))) ? 9.99 : double.Parse(fpxx_1.sLv);
                ilog_0.DebugFormat("[MakeInvoice] 税率2={0}", num11);
                object[] objArray = null;
                if ((fpxx_1.fplx == FPLX.PTFP) && (fpxx_1.Zyfplx == ZYFP_LX.NCP_SG))
                {
                    objArray = new object[] { "NCPSG" };
                }
                string str29 = str28 + DateTime.Now.ToString("HHmmssfff");
                ilog_0.Debug("[MakeInvoice] 开始生成MW.");
                string str30 = null;
                str16 = this.taxCard_0.CreateInvCipher(str29, num9, num11, num10, str24, redNum, buffer4, special, objArray, fpxx_1.kpjh, out str30);
                int returnErrCode = ToolUtil.GetReturnErrCode(str16);
                if (returnErrCode != 0)
                {
                    str = str16;
                    ilog_0.Error("[MakeInvoice] 生成MW失败，错误号：" + str);
                    if (returnErrCode == 560)
                    {
                        str = "A641";
                    }
                    return str;
                }
                if (str30.Length < 20)
                {
                    str = "A661";
                    ilog_0.Error("获取发票密文失败，密文校验码长度=" + str30.Length.ToString());
                    return str;
                }
                ilog_0.Debug("[MakeInvoice] 生成MW成功.");
                fpxx_1.mw = str30.Substring(0, str30.Length - 20);
                fpxx_1.jym = str30.Substring(str30.Length - 20, 20);
                fpxx_1.zfbz = fpxx_1.isBlankWaste;
                string str31 = string.Empty;
                if (!this.taxCard_0.IsUseCert || this.taxCard_0.IsTrain)
                {
                    goto Label_0FCB;
                }
                ilog_0.Debug("[MakeInvoice] 开始生成QM.");
                string str32 = this.method_23(fpxx_1);
                byte[] inArray = AES_Crypt.Encrypt(ToolUtil.GetBytes(str32), byte_0, byte_1);
                ilog_0.Debug(Convert.ToBase64String(inArray));
                str31 = string.Empty;
                bool flag2 = false;
                int num13 = 0;
                while (num13 < 3)
                {
                    ilog_0.Debug("[MakeInvoice] QM开始.");
                    string str33 = this.taxCard_0.SignData(str32, out str31);
                    if (ToolUtil.GetReturnErrCode(str33) != 0)
                    {
                        str = str33;
                        ilog_0.Error("CA签名失败，错误号：" + str);
                        num13++;
                    }
                    else
                    {
                        ilog_0.Debug("[MakeInvoice] QM成功.");
                        ilog_0.Debug("[MakeInvoice] QMYZ开始.");
                        str33 = this.taxCard_0.VerifySignedData(str32, str31);
                        if (ToolUtil.GetReturnErrCode(str33) == 0)
                        {
                            goto Label_0FA8;
                        }
                        ilog_0.Error("CA签名验证失败，错误号：" + str33);
                        num13++;
                    }
                }
                goto Label_0FC0;
            Label_0FA8:
                ilog_0.Debug("[MakeInvoice] QMYZ成功.");
                str = "0";
                flag2 = true;
            Label_0FC0:
                if (flag2)
                {
                    goto Label_0FD2;
                }
                return str;
            Label_0FCB:
                str31 = "MIIBOgYJKoZIhvcNAQcCoIIBKzCCAScCAQExCzAJBgUrDgMCGgUAMAsGCSqGSIb3DQEHAjGCAQYwggECAgEBMGAwVTELMAkGA1UEBhMCY24xDTALBgNVBAseBABDAEExDTALBgNVBAgeBFMXTqwxGTAXBgNVBAMeEE4tVv16DlKhi6SLwU4tX8MxDTALBgNVBAceBFMXTqwCBwIBAAAAJEcwCQYFKw4DAhoFADANBgkqhkiG9w0BAQEFAASBgEnhTAZe2ygpABXdBcEkLoiDPuMfbEPKysACRJqmLRzjyykBgIfetmek+Iz43YnOX/LX3+OYplTn1hgCTTbLjbbefgaplmsD0p7EgIxU3yGvJyURK4ndVpk9/cVivoyx3yIyKot3cS6N9ZClI1R9NgwHhTwciDFjfihaRF94wICl";
            Label_0FD2:
                str34 = str28 + DateTime.Now.ToString("HHmmssfff");
                byte[] buffer6 = Convert.FromBase64String(str31);
                ilog_0.Debug("[MakeInvoice] 开始调用接口开票");
                str16 = this.taxCard_0.Invoice(str34, num9, num11, num10, str24, redNum, buffer4, special, buffer6, fpxx_1.mw, fpxx_1.jym, num7, objArray, fpxx_1.kpjh, out result2);
                if (ToolUtil.GetReturnErrCode(str16) != 0)
                {
                    return str16;
                }
                if (result2.InvResultEnum != InvResult.irSuccess)
                {
                    goto Label_1604;
                }
                string str35 = "0000";
                ilog_0.Debug("[MakeInvoice] 调用接口开票成功");
                ilog_0.Debug("[MakeInvoice] 开始进行发票核对");
                str16 = this.taxCard_0.QueryInvInfo(fpxx_1.fpdm, Convert.ToInt32(fpxx_1.fphm), result2.InvIndex, result2.InvSeqNo, result2.InvDate, out detail, fpxx_1.kpjh);
                int num15 = ToolUtil.GetReturnErrCode(str16);
                switch (num15)
                {
                    case 0:
                        break;

                    case 0xf4:
                        num15 = this.taxCard_0.ReStartTaxCard();
                        if (num15 == 0)
                        {
                            ilog_0.Debug("[MakeInvoice] 开票后发票核对接口返回244，ReStartTaxCard调用成功");
                            return str16;
                        }
                        ilog_0.Debug("[MakeInvoice] 开票后发票核对接口返回244，ReStartTaxCard调用失败，错误号=" + num15.ToString());
                        return str16;

                    default:
                        ilog_0.Error("发票开具后核对失败，错误号=" + str16);
                        str35 = "A666";
                        goto Label_1375;
                }
                ilog_0.Debug("[MakeInvoice] 发票核对接口调用成功");
                if (!Class34.smethod_10(fpxx_1.je, detail.Amount.ToString()) || !Class34.smethod_10(fpxx_1.se, detail.Tax.ToString()))
                {
                    string message = "发票开具后核对失败，开具金额=" + num9.ToString() + "，盘中金额=" + detail.Amount.ToString() + "，开具税额=" + num10.ToString() + "，盘中税额=" + detail.Tax.ToString();
                    ilog_0.Error(message);
                    str35 = "A666";
                }
                if (((detail.Date.Year != this.dateTime_0.Year) || (detail.Date.Month != this.dateTime_0.Month)) || (detail.Date.Day != this.dateTime_0.Day))
                {
                    string str37 = "发票开具后核对失败，核对日期年=" + detail.Date.Year.ToString() + "，开票日期年=" + this.dateTime_0.Year.ToString() + "，核对日期月=" + detail.Date.Month.ToString() + "，开票日期月=" + this.dateTime_0.Month.ToString() + "，核对日期日=" + detail.Date.Day.ToString() + "，开票日期日=" + this.dateTime_0.Day.ToString();
                    ilog_0.Error(str37);
                    str35 = "A666";
                }
                string str38 = Convert.ToBase64String(detail.SignBuffer);
                if (str38 != str31)
                {
                    string str39 = "发票开具后核对失败，核对签名数据=" + str38 + "，开票签名数据=" + str31;
                    ilog_0.Error(str39);
                    str35 = "A666";
                }
            Label_1375:
                if ((fpxx_1.fplx != FPLX.JSFP) && (string.Compare(fpxx_1.mw, result2.InvCipher) != 0))
                {
                    ilog_0.Error("签名时密文和开票后密文不一致：签名时=" + fpxx_1.mw + "，开票后=" + result2.InvCipher);
                    str35 = "A666";
                }
                if (string.Compare(fpxx_1.jym, result2.InvVerify) != 0)
                {
                    ilog_0.Error("签名时校验码和开票后校验码不一致：签名时=" + fpxx_1.jym + "，开票后=" + result2.InvVerify);
                    str35 = "A666";
                }
                fpxx_1.jmbbh = result2.CipherVersion;
                fpxx_1.bsq = result2.RapPeriod;
                fpxx_1.yysbz = this.method_37(fpxx_1);
                fpxx_1.sign = str31;
                if (fpxx_1.hzfw && !fpxx_1.isBlankWaste)
                {
                    fpxx_1.hxm = Convert.ToBase64String(this.method_18(fpxx_1));
                }
                fpxx_1.keyFlagNo = result2.KeyFlagNo;
                fpxx_1.invQryNo = result2.InvQryNo;
                fpxx_1.zfsj = string.Empty;
                fpxx_1.dzsyh = result2.InvIndex;
                ilog_0.Debug("[MakeInvoice] 索引号=" + result2.InvIndex);
                fpxx_1.kpsxh = result2.InvSeqNo;
                ilog_0.Debug("[MakeInvoice] 顺序号=" + result2.InvSeqNo);
                if (string.IsNullOrEmpty(fpxx_1.dzsyh))
                {
                    fpxx_1.dzsyh = detail.Index;
                }
                if (string.IsNullOrEmpty(fpxx_1.kpsxh))
                {
                    fpxx_1.kpsxh = detail.InvSqeNo;
                }
                ilog_0.Debug("[MakeInvoice] 调用核对后，索引号=" + fpxx_1.dzsyh);
                ilog_0.Debug("[MakeInvoice] 调用核对后，顺序号=" + fpxx_1.kpsxh);
                if (fpxx_1.isBlankWaste)
                {
                    DateTime wasteTime = detail.WasteTime;
                    fpxx_1.zfsj = ToolUtil.FormatDateTime(wasteTime);
                }
                try
                {
                    byte[] buffer7 = new byte[] { 
                        0xaf, 0x52, 0xde, 0x45, 15, 0x58, 0xcd, 0x10, 0x23, 0x8b, 0x45, 0x6a, 0x10, 0x90, 0xaf, 0xbd, 
                        0xad, 0xdb, 0xae, 0x8d, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
                     };
                    byte[] buffer8 = new byte[] { 2, 0xaf, 0xbc, 0xab, 0xcc, 0x90, 0x39, 0x90, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 };
                    byte[] buffer9 = AES_Crypt.Encrypt(ToolUtil.GetBytes(fpxx_1.mw), buffer7, buffer8);
                    fpxx_1.mw = Convert.ToBase64String(buffer9);
                    if (fpxx_1.fplx == FPLX.JSFP)
                    {
                        fpxx_1.mw = string.Empty;
                    }
                    if (this.taxCard_0.IsTrain)
                    {
                        fpxx_1.gfsh = gfsh;
                        fpxx_1.gfmc = gfmc;
                        fpxx_1.gfdzdh = gfdzdh;
                        fpxx_1.gfyhzh = gfyhzh;
                        fpxx_1.spfnsrsbh = spfnsrsbh;
                        fpxx_1.spfmc = spfmc;
                    }
                }
                catch (Exception)
                {
                }
                str = str35;
                ilog_0.Debug("[MakeInvoice] 本接口调用完毕，返回成功");
                return str;
            Label_1604:
                str = str16;
                ilog_0.Debug("[MakeInvoice] 开票接口调用失败，错误号=" + str);
                return str;
            Label_1623:
                ilog_0.Error("调用取代码号码接口失败，返回的发票代码或发票号码为NULL");
                return "A659";
            }
            catch (Exception exception)
            {
                ilog_0.Error("[MakeInvoice] : " + exception.Message);
                str = "A699";
            }
            return str;
        }

        internal string method_17(Fpxx fpxx_1, bool bool_1 = true)
        {
            ilog_0.Debug("[WasteInvoice] 本接口调用开始");
            string str = "0000";
            try
            {
                InvWasteResult result;
                string str9;
                if (fpxx_1 == null)
                {
                    ilog_0.Debug("[WasteInvoice] 传入的发票信息为空");
                    return "A651";
                }
                if (string.Equals(this.taxCard_0.SoftVersion, "FWKP_V2.0_Svr_Client"))
                {
                    InvWasteResult result2;
                    byte[] buffer6 = null;
                    if (this.taxCard_0.SubSoftVersion != "Linux")
                    {
                        buffer6 = SerializeUtil.Serialize(fpxx_1);
                    }
                    else
                    {
                        buffer6 = fpxx_1.Seriealize();
                    }
                    string str12 = ToolUtil.ToBase64String(buffer6);
                    string str13 = this.taxCard_0.InvWaste(str12, string.Empty, InvoiceType.special, DateTime.Now, string.Empty, 0.0, 0.0, 0.0, null, string.Empty, 0, out result2);
                    switch (ToolUtil.GetReturnErrCode(str13))
                    {
                        case 0:
                        {
                            str = "0000";
                            Fpxx fpxx2 = null;
                            if (this.taxCard_0.SubSoftVersion != "Linux")
                            {
                                fpxx2 = Fpxx.DeSeriealize(ToolUtil.FromBase64String(this.taxCard_0.RegType));
                            }
                            else
                            {
                                fpxx2 = Fpxx.DeSeriealize_Linux(ToolUtil.FromBase64String(this.taxCard_0.RegType));
                            }
                            Fpxx.Copy(fpxx2, fpxx_1);
                            return str;
                        }
                        case 1:
                            str = str13;
                            break;

                        default:
                            str = ToolUtil.GetReturnErrCode(str13).ToString();
                            break;
                    }
                    ilog_0.Debug("[WasteInvoice] 远程作废接口调用失败，错误号=" + str13);
                    return str;
                }
                string str3 = "0000";
                string dzsyh = fpxx_1.dzsyh;
                if (string.IsNullOrEmpty(dzsyh))
                {
                    InvDetail detail;
                    DateTime time2 = DateTime.Parse(ToolUtil.FormatDateTimeEx(fpxx_1.kprq));
                    str3 = this.taxCard_0.QueryInvInfo(fpxx_1.fpdm, Convert.ToInt32(fpxx_1.fphm), fpxx_1.dzsyh, fpxx_1.kpsxh, time2, out detail, fpxx_1.kpjh);
                    if (ToolUtil.GetReturnErrCode(str3) == 0)
                    {
                        dzsyh = detail.Index;
                        ilog_0.DebugFormat("发票作废时，传入的索引号为空，调用核对接口成功，索引号={0}", dzsyh);
                    }
                    else
                    {
                        ilog_0.DebugFormat("发票作废时，传入的索引号为空，调用核对接口失败，错误号={0}", str3);
                    }
                }
                Fpxx fpxx = fpxx_1;
                fpxx.gfsh = string.IsNullOrEmpty(fpxx.gfsh) ? new string('0', 15) : fpxx.gfsh;
                if (!bool_1)
                {
                    ilog_0.Debug("开票过程中核对失败，本张发票自动作废");
                }
                DateTime time = new DateTime(0x7dd, 9, 10, 8, 0x22, 30);
                TimeSpan span = (TimeSpan) (DateTime.Now - time);
                byte[] buffer = AES_Crypt.Encrypt(ToolUtil.GetBytes(span.TotalSeconds.ToString("F1")), new byte[] { 
                    0xff, 0x42, 0xae, 0x95, 11, 0x51, 0xca, 0x15, 0x21, 140, 0x4f, 170, 220, 0x92, 170, 0xed, 
                    0xfd, 0xeb, 0x4e, 13, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
                 }, new byte[] { 0xf2, 0x1f, 0xac, 0x5b, 0x2c, 0xc0, 0xa9, 0xd0, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 });
                fpxx.Get_Print_Dj(null, 3, buffer);
                fpxx.zfbz = true;
                string str4 = string.Empty;
                if (!this.taxCard_0.IsUseCert || this.taxCard_0.IsTrain)
                {
                    goto Label_0446;
                }
                ilog_0.Debug("[WasteInvoice] 生成QM串开始");
                string str5 = this.method_23(fpxx);
                ilog_0.Debug("[WasteInvoice] 生成QM串成功");
                if (!string.Equals(PropertyUtil.GetValue("TMP_SA", "0"), "0"))
                {
                    byte[] buffer2 = new byte[] { 
                        0xae, 0x51, 0xdd, 0x44, 14, 0x57, 0xcc, 9, 0x23, 0x8b, 0x45, 0x6a, 0x10, 0x90, 0xaf, 0xbd, 
                        0xad, 0xdb, 0xae, 0x8d, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
                     };
                    byte[] buffer3 = new byte[] { 1, 0xae, 0xbb, 170, 0xcb, 0x89, 0x38, 0x89, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 };
                    byte[] inArray = AES_Crypt.Encrypt(ToolUtil.GetBytes(str5), buffer2, buffer3);
                    ilog_0.Debug(Convert.ToBase64String(inArray));
                }
                str4 = string.Empty;
                bool flag = false;
                int num7 = 0;
                while (num7 < 3)
                {
                    ilog_0.Debug("[WasteInvoice] 调用接口QM开始");
                    string str6 = this.taxCard_0.SignData(str5, out str4);
                    if (ToolUtil.GetReturnErrCode(str6) != 0)
                    {
                        str = str6;
                        ilog_0.Error("[WasteInvoice] 调用接口QM失败，错误号：" + str);
                        num7++;
                    }
                    else
                    {
                        ilog_0.Debug("[WasteInvoice] 调用接口QM成功");
                        ilog_0.Debug("[WasteInvoice] 调用接口QMYZ开始");
                        str6 = this.taxCard_0.VerifySignedData(str5, str4);
                        if (ToolUtil.GetReturnErrCode(str6) == 0)
                        {
                            goto Label_0423;
                        }
                        ilog_0.Error("[WasteInvoice] 调用接口QMYZ失败，错误号：" + str6);
                        num7++;
                    }
                }
                goto Label_043B;
            Label_0423:
                ilog_0.Debug("[WasteInvoice] 调用接口QMYZ成功");
                str = "0000";
                flag = true;
            Label_043B:
                if (flag)
                {
                    goto Label_044D;
                }
                return str;
            Label_0446:
                str4 = "MIIBOgYJKoZIhvcNAQcCoIIBKzCCAScCAQExCzAJBgUrDgMCGgUAMAsGCSqGSIb3DQEHAjGCAQYwggECAgEBMGAwVTELMAkGA1UEBhMCY24xDTALBgNVBAseBABDAEExDTALBgNVBAgeBFMXTqwxGTAXBgNVBAMeEE4tVv16DlKhi6SLwU4tX8MxDTALBgNVBAceBFMXTqwCBwIBAAAAJEcwCQYFKw4DAhoFADANBgkqhkiG9w0BAQEFAASBgEnhTAZe2ygpABXdBcEkLoiDPuMfbEPKysACRJqmLRzjyykBgIfetmek+Iz43YnOX/LX3+OYplTn1hgCTTbLjbbefgaplmsD0p7EgIxU3yGvJyURK4ndVpk9/cVivoyx3yIyKot3cS6N9ZClI1R9NgwHhTwciDFjfihaRF94wICl";
            Label_044D:
                str9 = fpxx.fpdm;
                string fphm = fpxx.fphm;
                InvoiceType fplx = (InvoiceType) fpxx.fplx;
                DateTime time3 = DateTime.Parse(ToolUtil.FormatDateTimeEx(fpxx.kprq));
                string gfsh = fpxx.gfsh;
                string str11 = string.IsNullOrEmpty(gfsh) ? new string('0', 15) : gfsh;
                double num6 = double.Parse(fpxx.je);
                double num4 = string.IsNullOrEmpty(fpxx.sLv) ? 9.99 : double.Parse(fpxx.sLv);
                double num5 = double.Parse(fpxx.se);
                byte[] buffer5 = Convert.FromBase64String(str4);
                ilog_0.Debug("[WasteInvoice] 开始调用作废接口");
                str3 = this.taxCard_0.InvWaste(str9, fphm, fplx, time3, str11, num6, num4, num5, buffer5, dzsyh, fpxx.kpjh, out result);
                if (ToolUtil.GetReturnErrCode(str3) == 0)
                {
                    ilog_0.Debug("[WasteInvoice] 调用作废接口成功");
                    fpxx.zfsj = string.Empty;
                    if (result.WasteTime.Year < 0x7d0)
                    {
                        InvDetail detail2;
                        ilog_0.Debug("[WasteInvoice] 开始调用查询接口");
                        str3 = this.taxCard_0.QueryInvInfo(fpxx.fpdm, Convert.ToInt32(fpxx.fphm), result.AddrIndex, fpxx.kpsxh, time3, out detail2, fpxx.kpjh);
                        if (ToolUtil.GetReturnErrCode(str3) != 0)
                        {
                            ilog_0.Error("发票作废成功，读取发票作废时间出错，错误号=" + str3);
                            return str3;
                        }
                        ilog_0.Debug("[WasteInvoice] 调用查询接口成功，查询到的作废时间=" + detail2.WasteTime.ToString());
                        fpxx.zfsj = ToolUtil.FormatDateTime(detail2.WasteTime);
                    }
                    else
                    {
                        ilog_0.Debug("[WasteInvoice] 取得作废时间");
                        fpxx.zfsj = ToolUtil.FormatDateTime(result.WasteTime);
                    }
                    fpxx_1.dzsyh = result.AddrIndex;
                    fpxx_1.zfbz = true;
                    fpxx_1.zfsj = fpxx.zfsj;
                    fpxx_1.sign = str4;
                    str = "0000";
                    ilog_0.Debug("[WasteInvoice] 本接口调用结束，返回成功");
                }
                else
                {
                    ilog_0.Debug("[WasteInvoice] 调用作废接口失败，错误号=" + str3);
                }
                fpxx = null;
            }
            catch (Exception exception)
            {
                ilog_0.Error("[WasteInvoice] 出现异常" + exception.Message);
                str = "A799";
            }
            return str;
        }

        internal byte[] method_18(Fpxx fpxx_1)
        {
            try
            {
                if (fpxx_1 == null)
                {
                    return null;
                }
                if (!fpxx_1.hzfw)
                {
                    return null;
                }
                byte[] sourceArray = this.method_31(fpxx_1.mw);
                if (this.fpxx_0 == null)
                {
                    this.fpxx_0 = this.ConvertInvoiceToZH(fpxx_1);
                    if (this.fpxx_0 == null)
                    {
                        this.string_0 = "A604";
                        return null;
                    }
                }
                if (this.class37_0 == null)
                {
                    this.class37_0 = this.method_29(fpxx_1, this.fpxx_0);
                }
                byte[] destinationArray = new byte[0x150];
                int num = 0;
                while (num < destinationArray.Length)
                {
                    destinationArray[num++] = 0x20;
                }
                Array.Copy(this.class37_0.method_8(), 0, destinationArray, 0, this.class37_0.method_8().Length);
                byte[] bytes = ToolUtil.GetBytes(this.class37_0.method_0());
                Array.Copy(bytes, 0, destinationArray, this.class37_0.method_8().Length, bytes.Length);
                byte[] buffer4 = this.method_34(destinationArray, fpxx_1, ushort_0);
                byte[] buffer5 = this.class37_0.method_10();
                byte[] buffer6 = new byte[0x1b4];
                Array.Copy(sourceArray, 0, buffer6, 0, sourceArray.Length);
                Array.Copy(buffer4, 0, buffer6, sourceArray.Length, buffer4.Length);
                Array.Copy(buffer5, 0, buffer6, sourceArray.Length + buffer4.Length, buffer5.Length);
                return buffer6;
            }
            catch (Exception exception)
            {
                ilog_0.Error("GetHxm: " + exception.Message);
                this.string_0 = "A699";
                return null;
            }
        }

        internal byte[] method_19(Fpxx fpxx_1, ushort ushort_1, bool bool_1)
        {
            try
            {
                if (fpxx_1 == null)
                {
                    return null;
                }
                if (!fpxx_1.hzfw)
                {
                    return null;
                }
                byte[] sourceArray = this.method_31(fpxx_1.mw);
                if (bool_1)
                {
                    if (this.fpxx_0 == null)
                    {
                        this.fpxx_0 = this.ConvertInvoiceToZH(fpxx_1);
                        if (this.fpxx_0 == null)
                        {
                            this.string_0 = "A604";
                            return null;
                        }
                    }
                    if (this.class37_0 == null)
                    {
                        this.class37_0 = this.method_29(fpxx_1, this.fpxx_0);
                    }
                }
                else
                {
                    this.class37_0 = this.method_30(fpxx_1);
                }
                byte[] destinationArray = new byte[0x150];
                int num = 0;
                while (num < destinationArray.Length)
                {
                    destinationArray[num++] = 0x20;
                }
                Array.Copy(this.class37_0.method_8(), 0, destinationArray, 0, this.class37_0.method_8().Length);
                byte[] bytes = ToolUtil.GetBytes(this.class37_0.method_0());
                Array.Copy(bytes, 0, destinationArray, this.class37_0.method_8().Length, bytes.Length);
                if (ushort_1 != 0)
                {
                    ushort_0 = ushort_1;
                }
                byte[] buffer3 = this.method_34(destinationArray, fpxx_1, ushort_0);
                byte[] buffer4 = this.class37_0.method_10();
                byte[] buffer5 = new byte[0x1b4];
                Array.Copy(sourceArray, 0, buffer5, 0, sourceArray.Length);
                Array.Copy(buffer3, 0, buffer5, sourceArray.Length, buffer3.Length);
                Array.Copy(buffer4, 0, buffer5, sourceArray.Length + buffer3.Length, buffer4.Length);
                return buffer5;
            }
            catch (Exception exception)
            {
                ilog_0.Error("GetHxm2: " + exception.Message);
                this.string_0 = "A699";
                return null;
            }
        }

        private bool method_2(char char_0)
        {
            if ("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(char.ToUpper(char_0)) < 0)
            {
                return false;
            }
            return true;
        }

        private string method_20(Fpxx fpxx_1, out string string_1, bool bool_1 = false)
        {
            if (this.bool_0 && !string.IsNullOrEmpty(fpxx_1.bmbbbh))
            {
                return this.method_22(fpxx_1, out string_1, bool_1);
            }
            return this.method_21(fpxx_1, out string_1, bool_1);
        }

        private string method_21(Fpxx fpxx_1, out string string_1, bool bool_1 = false)
        {
            string_1 = string.Empty;
            try
            {
                int num;
                if (fpxx_1 == null)
                {
                    return "A656";
                }
                if ((fpxx_1.Mxxx == null) && (fpxx_1.fplx != FPLX.JDCFP))
                {
                    return "A657";
                }
                StringBuilder builder = new StringBuilder();
                if ((((fpxx_1.fplx != FPLX.ZYFP) && (fpxx_1.fplx != FPLX.PTFP)) && (fpxx_1.fplx != FPLX.DZFP)) && (fpxx_1.fplx != FPLX.JSFP))
                {
                    if (fpxx_1.fplx == FPLX.HYFP)
                    {
                        builder.Append("f").Append("\n");
                        builder.Append(fpxx_1.spfnsrsbh).Append("\n");
                        builder.Append(fpxx_1.cyrnsrsbh).Append("\n");
                        builder.Append(fpxx_1.zgswjgdm).Append("\n");
                        builder.Append(fpxx_1.zgswjgmc).Append("\n");
                        builder.Append(fpxx_1.fpdm).Append("\n");
                        builder.Append(fpxx_1.fphm).Append("\n");
                        string str12 = string.Empty;
                        this.taxCard_0.GetInvControlNum(out str12, fpxx_1.kpjh);
                        builder.Append(str12).Append("\n");
                        string str13 = ToolUtil.FormatDateTime(this.dateTime_0).Replace("-", "");
                        builder.Append(str13).Append("\n");
                        builder.Append(fpxx_1.cyrmc).Append("\n");
                        builder.Append(fpxx_1.spfmc).Append("\n");
                        builder.Append(fpxx_1.shrmc).Append("\n");
                        builder.Append(fpxx_1.shrnsrsbh).Append("\n");
                        builder.Append(fpxx_1.fhrmc).Append("\n");
                        builder.Append(fpxx_1.fhrnsrsbh).Append("\n");
                        builder.Append(fpxx_1.qyd).Append("\n");
                        builder.Append(fpxx_1.je).Append("\n");
                        builder.Append(fpxx_1.sLv).Append("\n");
                        builder.Append(fpxx_1.se).Append("\n");
                        builder.Append(fpxx_1.czch).Append("\n");
                        builder.Append(fpxx_1.ccdw).Append("\n");
                        builder.Append(fpxx_1.skr).Append("\n");
                        builder.Append(fpxx_1.fhr).Append("\n");
                        builder.Append(fpxx_1.kpr).Append("\n");
                        builder.Append(fpxx_1.bz).Append("\n");
                        builder.Append(fpxx_1.yshwxx).Append("\n");
                        builder.Append(this.taxCard_0.CipherVersion).Append("\n");
                        string str14 = string.Empty;
                        if (bool_1)
                        {
                            str14 = "1";
                        }
                        else
                        {
                            List<int> list3 = null;
                            string str15 = this.taxCard_0.GetPeriodCount(11, out list3, fpxx_1.kpjh);
                            ilog_0.Debug("<<<<<<<<<取报税期，返回值=" + str15);
                            if (ToolUtil.GetReturnErrCode(str15) != 0)
                            {
                                ilog_0.Debug("<<<<<<<<<取报税期出现错误，错误号=" + str15);
                                return str15;
                            }
                            if (list3 == null)
                            {
                                ilog_0.Debug("<<<<<<<<<取报税期出现错误，返回的list为空");
                                return "A698";
                            }
                            if (list3.Count < 2)
                            {
                                ilog_0.Debug("<<<<<<<<<取报税期出现错误，返回的list长度不对，" + list3.Count.ToString());
                                return "A698";
                            }
                            str14 = list3[1].ToString();
                        }
                        if (!string.IsNullOrEmpty(fpxx_1.blueFpdm) && !string.IsNullOrEmpty(fpxx_1.blueFphm))
                        {
                            str14 = str14 + "@" + fpxx_1.blueFpdm + "@" + string.Format("{0:D8}", int.Parse(fpxx_1.blueFphm));
                        }
                        else
                        {
                            str14 = str14 + "@" + string.Empty + "@" + string.Empty;
                        }
                        if ((fpxx_1.Mxxx != null) && (fpxx_1.Mxxx.Count > 0))
                        {
                            str14 = str14 + "@";
                            string str16 = string.Empty;
                            int num13 = 1;
                            foreach (Dictionary<SPXX, string> dictionary7 in fpxx_1.Mxxx)
                            {
                                str16 = str16 + dictionary7[SPXX.SE];
                                if (num13 != fpxx_1.Mxxx.Count)
                                {
                                    str16 = str16 + "#";
                                }
                                num13++;
                            }
                            str14 = str14 + str16;
                        }
                        builder.Append(str14).Append("\n");
                        builder.Append(fpxx_1.zyspmc).Append("\n");
                        int num14 = (0xfa00 - ToolUtil.GetByteCount(builder.ToString())) / 150;
                        if (this.taxCard_0.IsLargeInvDetail)
                        {
                            num14 = 0x2710;
                        }
                        if (fpxx_1.Mxxx != null)
                        {
                            int count = fpxx_1.Mxxx.Count;
                        }
                        int num15 = 1;
                        foreach (Dictionary<SPXX, string> dictionary8 in fpxx_1.Mxxx)
                        {
                            if (num15 > num14)
                            {
                                break;
                            }
                            builder.Append(Class34.smethod_23(dictionary8[SPXX.SPMC], 0x70, true, true));
                            builder.Append(Class34.smethod_23(num15.ToString(), 8, true, true));
                            builder.Append(Class34.smethod_24(dictionary8[SPXX.JE], 30, true));
                            num15++;
                        }
                    }
                    else if (fpxx_1.fplx == FPLX.JDCFP)
                    {
                        builder.Append("j").Append("\n");
                        builder.Append(fpxx_1.gfsh).Append("\n");
                        builder.Append(fpxx_1.xfsh).Append("\n");
                        builder.Append(fpxx_1.clsbdh).Append("\n");
                        builder.Append(fpxx_1.zgswjgdm).Append("\n");
                        builder.Append(fpxx_1.zgswjgmc).Append("\n");
                        builder.Append(fpxx_1.fpdm).Append("\n");
                        builder.Append(fpxx_1.fphm).Append("\n");
                        string str17 = string.Empty;
                        this.taxCard_0.GetInvControlNum(out str17, fpxx_1.kpjh);
                        builder.Append(str17).Append("\n");
                        string str18 = ToolUtil.FormatDateTime(this.dateTime_0).Replace("-", "");
                        builder.Append(str18).Append("\n");
                        builder.Append(fpxx_1.gfmc).Append("\n");
                        builder.Append(fpxx_1.cllx).Append("\n");
                        builder.Append(fpxx_1.cpxh).Append("\n");
                        builder.Append(fpxx_1.cd).Append("\n");
                        builder.Append(fpxx_1.hgzh).Append("\n");
                        builder.Append(fpxx_1.jkzmsh).Append("\n");
                        builder.Append(fpxx_1.sjdh).Append("\n");
                        builder.Append(fpxx_1.fdjhm).Append("\n");
                        builder.Append(fpxx_1.xfmc).Append("\n");
                        builder.Append(fpxx_1.xfdz).Append("\n");
                        builder.Append(fpxx_1.xfdh).Append("\n");
                        builder.Append(fpxx_1.xfyh).Append("\n");
                        builder.Append(fpxx_1.xfzh).Append("\n");
                        builder.Append(fpxx_1.je).Append("\n");
                        builder.Append(fpxx_1.sLv).Append("\n");
                        builder.Append(fpxx_1.se).Append("\n");
                        builder.Append(fpxx_1.dw).Append("\n");
                        builder.Append(fpxx_1.xcrs).Append("\n");
                        builder.Append(fpxx_1.kpr).Append("\n");
                        builder.Append(fpxx_1.sccjmc).Append("\n");
                        builder.Append(this.taxCard_0.CipherVersion).Append("\n");
                        string str19 = string.Empty;
                        if (bool_1)
                        {
                            str19 = "1";
                        }
                        else
                        {
                            List<int> list4 = null;
                            string str20 = this.taxCard_0.GetPeriodCount(12, out list4, fpxx_1.kpjh);
                            if (ToolUtil.GetReturnErrCode(str20) != 0)
                            {
                                ilog_0.Debug("<<<<<<<<<取报税期出现错误，错误号=" + str20);
                                return str20;
                            }
                            num = list4[1];
                            str19 = num.ToString();
                        }
                        builder.Append(str19).Append("\n");
                        builder.Append(fpxx_1.bz).Append("\n");
                        builder.Append(fpxx_1.sfzhm).Append("\n");
                        string str21 = "2";
                        if (!fpxx_1.isNewJdcfp)
                        {
                            str21 = "1";
                        }
                        builder.Append(str21).Append("\n");
                    }
                }
                else
                {
                    builder.Append(Invoice.FPLX2Str(fpxx_1.fplx)).Append("\n");
                    string str2 = string.Empty;
                    if (fpxx_1.hzfw)
                    {
                        str2 = "V1";
                        string str3 = "0";
                        if ((fpxx_1.fplx == FPLX.ZYFP) && (fpxx_1.Zyfplx == ZYFP_LX.XT_YCL))
                        {
                            str3 = "1";
                        }
                        else if ((fpxx_1.fplx == FPLX.ZYFP) && (fpxx_1.Zyfplx == ZYFP_LX.XT_CCP))
                        {
                            str3 = "2";
                        }
                        else if ((fpxx_1.fplx == FPLX.ZYFP) && (((fpxx_1.Zyfplx == ZYFP_LX.SNY) || (fpxx_1.Zyfplx == ZYFP_LX.SNY_DDZG)) || ((fpxx_1.Zyfplx == ZYFP_LX.RLY) || (fpxx_1.Zyfplx == ZYFP_LX.RLY_DDZG))))
                        {
                            str3 = "3";
                        }
                        if (str3 != "0")
                        {
                            num = Convert.ToInt32(str3) + 1;
                            str2 = "V" + num.ToString();
                        }
                    }
                    if ((fpxx_1.fplx == FPLX.PTFP) && (fpxx_1.Zyfplx == ZYFP_LX.NCP_XS))
                    {
                        str2 = "V5";
                    }
                    else if ((fpxx_1.fplx == FPLX.PTFP) && (fpxx_1.Zyfplx == ZYFP_LX.NCP_SG))
                    {
                        str2 = "V6";
                    }
                    if (((fpxx_1.fplx == FPLX.ZYFP) || (fpxx_1.fplx == FPLX.PTFP)) && ((fpxx_1.Zyfplx != ZYFP_LX.HYSY) && (fpxx_1.Zyfplx != ZYFP_LX.JZ_50_15)))
                    {
                        str2 = str2 + "H";
                    }
                    if (((fpxx_1.fplx == FPLX.ZYFP) || (fpxx_1.fplx == FPLX.PTFP)) && (fpxx_1.Zyfplx == ZYFP_LX.JZ_50_15))
                    {
                        str2 = str2 + "J";
                    }
                    if (((fpxx_1.fplx == FPLX.ZYFP) || (fpxx_1.fplx == FPLX.PTFP)) && (fpxx_1.Zyfplx == ZYFP_LX.CEZS))
                    {
                        str2 = str2 + "C";
                    }
                    builder.Append(fpxx_1.fpdm).Append(str2).Append("\n");
                    string str4 = "";
                    if (this.taxCard_0.ECardType == ECardType.ectSK)
                    {
                        Dictionary<string, string> dictionary = new Dictionary<string, string>();
                        Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                        List<Dictionary<SPXX, string>> list = new List<Dictionary<SPXX, string>>();
                        if (fpxx_1.Qdxx == null)
                        {
                            list.AddRange(fpxx_1.Mxxx);
                        }
                        else
                        {
                            list.AddRange(fpxx_1.Qdxx);
                        }
                        foreach (Dictionary<SPXX, string> dictionary3 in list)
                        {
                            FPHXZ fphxz = (FPHXZ) Enum.Parse(typeof(FPHXZ), dictionary3[SPXX.FPHXZ]);
                            if (fphxz != FPHXZ.XHQDZK)
                            {
                                string str5 = dictionary3[SPXX.SLV];
                                if (!string.IsNullOrEmpty(str5))
                                {
                                    str5 = Class34.smethod_19(str5, Struct40.int_55);
                                }
                                if (dictionary.ContainsKey(str5))
                                {
                                    dictionary[str5] = Class34.smethod_17(dictionary[str5], dictionary3[SPXX.JE]);
                                    dictionary2[str5] = Class34.smethod_17(dictionary2[str5], dictionary3[SPXX.SE]);
                                }
                                else
                                {
                                    dictionary.Add(str5, dictionary3[SPXX.JE]);
                                    dictionary2.Add(str5, dictionary3[SPXX.SE]);
                                }
                            }
                        }
                        foreach (string str6 in dictionary.Keys)
                        {
                            string str7 = str4;
                            str4 = str7 + str6 + "," + dictionary[str6] + "," + dictionary2[str6] + ";";
                        }
                        if (str4 != "")
                        {
                            str4 = "V" + str4;
                        }
                    }
                    builder.Append(fpxx_1.fphm).Append(str4).Append("\n");
                    string str8 = ToolUtil.FormatDateTime(this.dateTime_0).Replace("-", "");
                    builder.Append(str8).Append("\n");
                    builder.Append(fpxx_1.gfmc).Append("\n");
                    builder.Append(fpxx_1.gfsh).Append("\n");
                    builder.Append(fpxx_1.gfdzdh).Append("\n");
                    builder.Append(fpxx_1.gfyhzh).Append("\n");
                    if ((fpxx_1.fplx != FPLX.DZFP) && (fpxx_1.fplx != FPLX.JSFP))
                    {
                        builder.Append(this.taxCard_0.CipherVersion).Append("\n");
                    }
                    else
                    {
                        builder.Append(fpxx_1.jqbh).Append("\n");
                    }
                    builder.Append(fpxx_1.zyspmc).Append("\n");
                    ilog_0.Debug("<<<<<<<<<开始取报税期");
                    string str9 = string.Empty;
                    if (bool_1)
                    {
                        str9 = "1";
                    }
                    else
                    {
                        List<int> list2 = null;
                        string str10 = this.taxCard_0.GetPeriodCount((int) fpxx_1.fplx, out list2, fpxx_1.kpjh);
                        ilog_0.Debug("<<<<<<<<<取报税期，返回值=" + str10);
                        if (ToolUtil.GetReturnErrCode(str10) != 0)
                        {
                            ilog_0.Debug("<<<<<<<<<取报税期出现错误，错误号=" + str10);
                            return str10;
                        }
                        if (list2 == null)
                        {
                            ilog_0.Debug("<<<<<<<<<取报税期出现错误，返回的list为空");
                            return "A698";
                        }
                        if (list2.Count < 2)
                        {
                            ilog_0.Debug("<<<<<<<<<取报税期出现错误，返回的list长度不对，" + list2.Count.ToString());
                            return "A698";
                        }
                        str9 = list2[1].ToString();
                    }
                    if ((fpxx_1.fplx == FPLX.ZYFP) && fpxx_1.isRed)
                    {
                        if (!string.IsNullOrEmpty(fpxx_1.blueFpdm) && !string.IsNullOrEmpty(fpxx_1.blueFphm))
                        {
                            str9 = str9 + "@" + fpxx_1.blueFpdm + "@" + string.Format("{0:D8}", int.Parse(fpxx_1.blueFphm));
                        }
                    }
                    else if (fpxx_1.fplx == FPLX.JSFP)
                    {
                        int num4 = 0;
                        Dictionary<string, int> jsPrintTemplate = ToolUtil.GetJsPrintTemplate();
                        if ((jsPrintTemplate.Count > 0) && jsPrintTemplate.ContainsKey(fpxx_1.dy_mb))
                        {
                            num4 = jsPrintTemplate[fpxx_1.dy_mb];
                        }
                        string str11 = (num4 + 1).ToString("X2");
                        if ((fpxx_1.isRed && !string.IsNullOrEmpty(fpxx_1.blueFpdm)) && !string.IsNullOrEmpty(fpxx_1.blueFphm))
                        {
                            str9 = str9 + "@" + fpxx_1.blueFpdm + "@" + string.Format("{0:D8}", int.Parse(fpxx_1.blueFphm));
                        }
                        str9 = str9 + "@" + str11;
                    }
                    builder.Append(str9).Append("\n");
                    builder.Append(fpxx_1.xfmc).Append("\n");
                    builder.Append(fpxx_1.xfsh).Append("\n");
                    builder.Append(fpxx_1.xfdzdh).Append("\n");
                    builder.Append(fpxx_1.xfyhzh).Append("\n");
                    builder.Append(fpxx_1.kpr).Append("\n");
                    builder.Append(fpxx_1.fhr).Append("\n");
                    builder.Append(fpxx_1.skr).Append("\n");
                    builder.Append(fpxx_1.bz).Append("\n");
                    builder.Append(fpxx_1.je).Append("\n");
                    builder.Append(fpxx_1.se).Append("\n");
                    builder.Append((fpxx_1.Qdxx == null) ? "N" : "Y").Append("\n");
                    int num6 = 0xfa00;
                    if (this.taxCard_0.IsLargeInvDetail)
                    {
                        num6 = 0x280000;
                    }
                    int num7 = (num6 - ToolUtil.GetByteCount(builder.ToString())) / 0xfc;
                    if ((fpxx_1.Mxxx == null) || (fpxx_1.Mxxx.Count <= 0))
                    {
                        ilog_0.Debug("fpxx.Mxxx is null");
                    }
                    int num8 = 1;
                    foreach (Dictionary<SPXX, string> dictionary5 in fpxx_1.Mxxx)
                    {
                        if (num8 > num7)
                        {
                            break;
                        }
                        builder.Append(Class34.smethod_23(dictionary5[SPXX.SPMC], 0x5c, true, true));
                        builder.Append(Class34.smethod_23(dictionary5[SPXX.GGXH], 40, true, true));
                        builder.Append(Class34.smethod_23(dictionary5[SPXX.JLDW], 0x16, true, true));
                        builder.Append(Class34.smethod_24(dictionary5[SPXX.SL], 0x15, true));
                        builder.Append(Class34.smethod_24(dictionary5[SPXX.DJ], 0x15, true));
                        builder.Append(Class34.smethod_24(dictionary5[SPXX.JE], 0x12, true));
                        builder.Append(Class34.smethod_24(dictionary5[SPXX.SLV], 6, true));
                        builder.Append(Class34.smethod_24(dictionary5[SPXX.SE], 0x12, true));
                        builder.Append(Class34.smethod_23(num8.ToString(), 8, true, true));
                        builder.Append(Class34.smethod_23(dictionary5[SPXX.FPHXZ], 3, true, true));
                        builder.Append(Class34.smethod_23(dictionary5[SPXX.HSJBZ], 3, true, true));
                        num8++;
                    }
                    if (fpxx_1.Qdxx != null)
                    {
                        builder.Append("\n");
                        num7 = (num7 - num8) + 1;
                        if (fpxx_1.Qdxx.Count > num7)
                        {
                            ilog_0.Error("发票清单行数超过底层能存储的最大行数 " + fpxx_1.Qdxx.Count.ToString() + " " + num7.ToString());
                            return "A655";
                        }
                        int num10 = 1;
                        foreach (Dictionary<SPXX, string> dictionary6 in fpxx_1.Qdxx)
                        {
                            if (num10 > num7)
                            {
                                break;
                            }
                            builder.Append(Class34.smethod_23(dictionary6[SPXX.SPMC], 0x5c, true, true));
                            builder.Append(Class34.smethod_23(dictionary6[SPXX.GGXH], 40, true, true));
                            builder.Append(Class34.smethod_23(dictionary6[SPXX.JLDW], 0x16, true, true));
                            builder.Append(Class34.smethod_24(dictionary6[SPXX.SL], 0x15, true));
                            builder.Append(Class34.smethod_24(dictionary6[SPXX.DJ], 0x15, true));
                            builder.Append(Class34.smethod_24(dictionary6[SPXX.JE], 0x12, true));
                            builder.Append(Class34.smethod_24(dictionary6[SPXX.SLV], 6, true));
                            builder.Append(Class34.smethod_24(dictionary6[SPXX.SE], 0x12, true));
                            builder.Append(Class34.smethod_23(num10.ToString(), 8, true, true));
                            builder.Append(Class34.smethod_23(dictionary6[SPXX.FPHXZ], 3, true, true));
                            builder.Append(Class34.smethod_23(dictionary6[SPXX.HSJBZ], 3, true, true));
                            num10++;
                        }
                    }
                }
                MatchCollection matchs = Regex.Matches(builder.ToString(), "\n");
                if ((((fpxx_1.fplx == FPLX.ZYFP) || (fpxx_1.fplx == FPLX.PTFP)) || ((fpxx_1.fplx == FPLX.DZFP) || (fpxx_1.fplx == FPLX.JSFP))) && (((fpxx_1.Qdxx != null) && (matchs.Count != 0x17)) || ((fpxx_1.Qdxx == null) && (matchs.Count != 0x16))))
                {
                    num = 0x18;
                    ilog_0.Error(@"生成发票明细串时，字段中有非法字符 \n " + matchs.Count.ToString() + " " + num.ToString());
                    return "A658";
                }
                if ((fpxx_1.fplx == FPLX.HYFP) && (matchs.Count != 0x1d))
                {
                    num = 30;
                    ilog_0.Error(@"生成发票明细串时，字段中有非法字符 \n " + matchs.Count.ToString() + " " + num.ToString());
                    return "A658";
                }
                if ((fpxx_1.fplx == FPLX.JDCFP) && (matchs.Count != 0x23))
                {
                    ilog_0.Error(@"生成发票明细串时，字段中有非法字符 \n " + matchs.Count.ToString() + " " + 0x23.ToString());
                    return "A658";
                }
                string_1 = builder.ToString();
                return "0000";
            }
            catch (Exception exception)
            {
                ilog_0.Error("GetInvoiceString: " + exception.Message);
                return "A699";
            }
        }

        private string method_22(Fpxx fpxx_1, out string string_1, bool bool_1 = false)
        {
            string_1 = string.Empty;
            try
            {
                int num;
                if (fpxx_1 == null)
                {
                    return "A656";
                }
                if ((fpxx_1.Mxxx == null) && (fpxx_1.fplx != FPLX.JDCFP))
                {
                    return "A657";
                }
                StringBuilder builder = new StringBuilder();
                if ((((fpxx_1.fplx != FPLX.ZYFP) && (fpxx_1.fplx != FPLX.PTFP)) && (fpxx_1.fplx != FPLX.DZFP)) && (fpxx_1.fplx != FPLX.JSFP))
                {
                    if (fpxx_1.fplx == FPLX.HYFP)
                    {
                        builder.Append("f").Append("\n");
                        builder.Append(fpxx_1.spfnsrsbh).Append("\n");
                        builder.Append(fpxx_1.cyrnsrsbh).Append("\n");
                        builder.Append(fpxx_1.zgswjgdm).Append("\n");
                        builder.Append(fpxx_1.zgswjgmc).Append("\n");
                        string str12 = fpxx_1.fpdm + "B" + fpxx_1.bmbbbh;
                        builder.Append(str12).Append("\n");
                        builder.Append(fpxx_1.fphm).Append("\n");
                        string str13 = string.Empty;
                        this.taxCard_0.GetInvControlNum(out str13, fpxx_1.kpjh);
                        builder.Append(str13).Append("\n");
                        string str14 = ToolUtil.FormatDateTime(this.dateTime_0).Replace("-", "");
                        builder.Append(str14).Append("\n");
                        builder.Append(fpxx_1.cyrmc).Append("\n");
                        builder.Append(fpxx_1.spfmc).Append("\n");
                        builder.Append(fpxx_1.shrmc).Append("\n");
                        builder.Append(fpxx_1.shrnsrsbh).Append("\n");
                        builder.Append(fpxx_1.fhrmc).Append("\n");
                        builder.Append(fpxx_1.fhrnsrsbh).Append("\n");
                        builder.Append(fpxx_1.qyd).Append("\n");
                        builder.Append(fpxx_1.je).Append("\n");
                        builder.Append(fpxx_1.sLv).Append("\n");
                        builder.Append(fpxx_1.se).Append("\n");
                        builder.Append(fpxx_1.czch).Append("\n");
                        builder.Append(fpxx_1.ccdw).Append("\n");
                        builder.Append(fpxx_1.skr).Append("\n");
                        builder.Append(fpxx_1.fhr).Append("\n");
                        builder.Append(fpxx_1.kpr).Append("\n");
                        builder.Append(fpxx_1.bz).Append("\n");
                        builder.Append(fpxx_1.yshwxx).Append("\n");
                        builder.Append(this.taxCard_0.CipherVersion).Append("\n");
                        string str15 = string.Empty;
                        if (bool_1)
                        {
                            str15 = "1";
                        }
                        else
                        {
                            List<int> list3 = null;
                            string str16 = this.taxCard_0.GetPeriodCount(11, out list3, fpxx_1.kpjh);
                            ilog_0.Debug("<<<<<<<<<取报税期，返回值=" + str16);
                            if (ToolUtil.GetReturnErrCode(str16) != 0)
                            {
                                ilog_0.Debug("<<<<<<<<<取报税期出现错误，错误号=" + str16);
                                return str16;
                            }
                            if (list3 == null)
                            {
                                ilog_0.Debug("<<<<<<<<<取报税期出现错误，返回的list为空");
                                return "A698";
                            }
                            if (list3.Count < 2)
                            {
                                ilog_0.Debug("<<<<<<<<<取报税期出现错误，返回的list长度不对，" + list3.Count.ToString());
                                return "A698";
                            }
                            num = list3[1];
                            str15 = num.ToString();
                        }
                        if (!string.IsNullOrEmpty(fpxx_1.blueFpdm) && !string.IsNullOrEmpty(fpxx_1.blueFphm))
                        {
                            str15 = str15 + "@" + fpxx_1.blueFpdm + "@" + string.Format("{0:D8}", int.Parse(fpxx_1.blueFphm));
                        }
                        else
                        {
                            str15 = str15 + "@" + string.Empty + "@" + string.Empty;
                        }
                        if ((fpxx_1.Mxxx != null) && (fpxx_1.Mxxx.Count > 0))
                        {
                            str15 = str15 + "@";
                            string str17 = string.Empty;
                            int num9 = 1;
                            foreach (Dictionary<SPXX, string> dictionary7 in fpxx_1.Mxxx)
                            {
                                str17 = str17 + dictionary7[SPXX.SE];
                                if (num9 != fpxx_1.Mxxx.Count)
                                {
                                    str17 = str17 + "#";
                                }
                                num9++;
                            }
                            str15 = str15 + str17;
                        }
                        builder.Append(str15).Append("\n");
                        builder.Append(fpxx_1.zyspmc).Append("\n");
                        int num10 = (0xfa00 - ToolUtil.GetByteCount(builder.ToString())) / 0x10a;
                        if (this.taxCard_0.IsLargeInvDetail)
                        {
                            num10 = 0x1770;
                        }
                        if (fpxx_1.Mxxx != null)
                        {
                            int count = fpxx_1.Mxxx.Count;
                        }
                        int num11 = 1;
                        foreach (Dictionary<SPXX, string> dictionary8 in fpxx_1.Mxxx)
                        {
                            if (num11 > num10)
                            {
                                break;
                            }
                            builder.Append(Class34.smethod_23(dictionary8[SPXX.SPMC], 0x70, true, true));
                            builder.Append(Class34.smethod_23(num11.ToString(), 8, true, true));
                            builder.Append(Class34.smethod_24(dictionary8[SPXX.JE], 30, true));
                            builder.Append(Class34.smethod_23(dictionary8[SPXX.FLBM], 30, true, true));
                            builder.Append(Class34.smethod_23(dictionary8[SPXX.SPBH], 30, true, true));
                            builder.Append(Class34.smethod_23(dictionary8[SPXX.XSYH], 3, true, true));
                            builder.Append(Class34.smethod_23(dictionary8[SPXX.YHSM], 50, true, true));
                            builder.Append(Class34.smethod_23(dictionary8[SPXX.LSLVBS], 3, true, true));
                            num11++;
                        }
                    }
                    else if (fpxx_1.fplx == FPLX.JDCFP)
                    {
                        builder.Append("j").Append("\n");
                        builder.Append(fpxx_1.gfsh).Append("\n");
                        builder.Append(fpxx_1.xfsh).Append("\n");
                        builder.Append(fpxx_1.clsbdh).Append("\n");
                        builder.Append(fpxx_1.zgswjgdm).Append("\n");
                        builder.Append(fpxx_1.zgswjgmc).Append("\n");
                        string str18 = fpxx_1.fpdm + "B" + fpxx_1.bmbbbh;
                        builder.Append(str18).Append("\n");
                        builder.Append(fpxx_1.fphm).Append("\n");
                        string str19 = string.Empty;
                        this.taxCard_0.GetInvControlNum(out str19, fpxx_1.kpjh);
                        builder.Append(str19).Append("\n");
                        string str20 = ToolUtil.FormatDateTime(this.dateTime_0).Replace("-", "");
                        builder.Append(str20).Append("\n");
                        builder.Append(fpxx_1.gfmc).Append("\n");
                        builder.Append(fpxx_1.cllx).Append("\n");
                        builder.Append(fpxx_1.cpxh).Append("\n");
                        builder.Append(fpxx_1.cd).Append("\n");
                        builder.Append(fpxx_1.hgzh).Append("\n");
                        builder.Append(fpxx_1.jkzmsh).Append("\n");
                        builder.Append(fpxx_1.sjdh).Append("\n");
                        builder.Append(fpxx_1.fdjhm).Append("\n");
                        builder.Append(fpxx_1.xfmc).Append("\n");
                        builder.Append(fpxx_1.xfdz).Append("\n");
                        builder.Append(fpxx_1.xfdh).Append("\n");
                        builder.Append(fpxx_1.xfyh).Append("\n");
                        builder.Append(fpxx_1.xfzh).Append("\n");
                        builder.Append(fpxx_1.je).Append("\n");
                        builder.Append(fpxx_1.sLv).Append("\n");
                        builder.Append(fpxx_1.se).Append("\n");
                        builder.Append(fpxx_1.dw).Append("\n");
                        builder.Append(fpxx_1.xcrs).Append("\n");
                        builder.Append(fpxx_1.kpr).Append("\n");
                        builder.Append(fpxx_1.sccjmc).Append("\n");
                        builder.Append(this.taxCard_0.CipherVersion).Append("\n");
                        string str21 = string.Empty;
                        if (bool_1)
                        {
                            str21 = "1";
                        }
                        else
                        {
                            List<int> list4 = null;
                            string str22 = this.taxCard_0.GetPeriodCount(12, out list4, fpxx_1.kpjh);
                            if (ToolUtil.GetReturnErrCode(str22) != 0)
                            {
                                ilog_0.Debug("<<<<<<<<<取报税期出现错误，错误号=" + str22);
                                return str22;
                            }
                            num = list4[1];
                            str21 = num.ToString();
                        }
                        builder.Append(str21).Append("\n");
                        builder.Append(fpxx_1.bz).Append("\n");
                        builder.Append(fpxx_1.sfzhm).Append("\n");
                        string str23 = "2";
                        if (!fpxx_1.isNewJdcfp)
                        {
                            str23 = "1";
                        }
                        builder.Append(str23).Append("\n");
                        builder.Append(fpxx_1.zyspmc).Append("\n");
                        string str24 = string.Empty;
                        string str25 = "0";
                        string str26 = string.Empty;
                        string str27 = string.Empty;
                        string[] strArray2 = Regex.Split(fpxx_1.zyspsm, "#%");
                        if (strArray2.Length == 2)
                        {
                            str24 = strArray2[0];
                            str26 = strArray2[1];
                        }
                        string[] strArray3 = Regex.Split(fpxx_1.skr, "#%");
                        if (strArray3.Length == 2)
                        {
                            str25 = strArray3[0];
                            str27 = strArray3[1];
                        }
                        builder.Append(str24).Append("\n");
                        builder.Append(str25).Append("\n");
                        builder.Append(str26).Append("\n");
                        builder.Append(str27).Append("\n");
                    }
                }
                else
                {
                    builder.Append(Invoice.FPLX2Str(fpxx_1.fplx)).Append("\n");
                    string str2 = string.Empty;
                    if (fpxx_1.hzfw)
                    {
                        str2 = "V1";
                        string str3 = "0";
                        if ((fpxx_1.fplx == FPLX.ZYFP) && (fpxx_1.Zyfplx == ZYFP_LX.XT_YCL))
                        {
                            str3 = "1";
                        }
                        else if ((fpxx_1.fplx == FPLX.ZYFP) && (fpxx_1.Zyfplx == ZYFP_LX.XT_CCP))
                        {
                            str3 = "2";
                        }
                        else if ((fpxx_1.fplx == FPLX.ZYFP) && (((fpxx_1.Zyfplx == ZYFP_LX.SNY) || (fpxx_1.Zyfplx == ZYFP_LX.SNY_DDZG)) || ((fpxx_1.Zyfplx == ZYFP_LX.RLY) || (fpxx_1.Zyfplx == ZYFP_LX.RLY_DDZG))))
                        {
                            str3 = "3";
                        }
                        if (str3 != "0")
                        {
                            num = Convert.ToInt32(str3) + 1;
                            str2 = "V" + num.ToString();
                        }
                    }
                    if ((fpxx_1.fplx == FPLX.PTFP) && (fpxx_1.Zyfplx == ZYFP_LX.NCP_XS))
                    {
                        str2 = "V5";
                    }
                    else if ((fpxx_1.fplx == FPLX.PTFP) && (fpxx_1.Zyfplx == ZYFP_LX.NCP_SG))
                    {
                        str2 = "V6";
                    }
                    if (this.bool_0)
                    {
                        str2 = str2 + "B" + fpxx_1.bmbbbh;
                    }
                    if (((fpxx_1.fplx == FPLX.ZYFP) || (fpxx_1.fplx == FPLX.PTFP)) && ((fpxx_1.Zyfplx != ZYFP_LX.HYSY) && (fpxx_1.Zyfplx != ZYFP_LX.JZ_50_15)))
                    {
                        str2 = str2 + "H";
                    }
                    if (((fpxx_1.fplx == FPLX.ZYFP) || (fpxx_1.fplx == FPLX.PTFP)) && (fpxx_1.Zyfplx == ZYFP_LX.JZ_50_15))
                    {
                        str2 = str2 + "J";
                    }
                    if (((fpxx_1.fplx == FPLX.ZYFP) || (fpxx_1.fplx == FPLX.PTFP)) && (fpxx_1.Zyfplx == ZYFP_LX.CEZS))
                    {
                        str2 = str2 + "C";
                    }
                    builder.Append(fpxx_1.fpdm).Append(str2).Append("\n");
                    string str4 = "";
                    if (this.taxCard_0.ECardType == ECardType.ectSK)
                    {
                        Dictionary<string, string> dictionary = new Dictionary<string, string>();
                        Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                        List<Dictionary<SPXX, string>> list = new List<Dictionary<SPXX, string>>();
                        if (fpxx_1.Qdxx == null)
                        {
                            list.AddRange(fpxx_1.Mxxx);
                        }
                        else
                        {
                            list.AddRange(fpxx_1.Qdxx);
                        }
                        foreach (Dictionary<SPXX, string> dictionary3 in list)
                        {
                            FPHXZ fphxz = (FPHXZ) Enum.Parse(typeof(FPHXZ), dictionary3[SPXX.FPHXZ]);
                            if (fphxz != FPHXZ.XHQDZK)
                            {
                                string str5 = dictionary3[SPXX.SLV];
                                if (!string.IsNullOrEmpty(str5))
                                {
                                    str5 = Class34.smethod_19(str5, Struct40.int_55);
                                }
                                if (dictionary.ContainsKey(str5))
                                {
                                    dictionary[str5] = Class34.smethod_17(dictionary[str5], dictionary3[SPXX.JE]);
                                    dictionary2[str5] = Class34.smethod_17(dictionary2[str5], dictionary3[SPXX.SE]);
                                }
                                else
                                {
                                    dictionary.Add(str5, dictionary3[SPXX.JE]);
                                    dictionary2.Add(str5, dictionary3[SPXX.SE]);
                                }
                            }
                        }
                        foreach (string str6 in dictionary.Keys)
                        {
                            string str7 = str4;
                            str4 = str7 + str6 + "," + dictionary[str6] + "," + dictionary2[str6] + ";";
                        }
                        if (str4 != "")
                        {
                            str4 = "V" + str4;
                        }
                    }
                    builder.Append(fpxx_1.fphm).Append(str4).Append("\n");
                    string str8 = ToolUtil.FormatDateTime(this.dateTime_0).Replace("-", "");
                    builder.Append(str8).Append("\n");
                    builder.Append(fpxx_1.gfmc).Append("\n");
                    builder.Append(fpxx_1.gfsh).Append("\n");
                    builder.Append(fpxx_1.gfdzdh).Append("\n");
                    builder.Append(fpxx_1.gfyhzh).Append("\n");
                    if ((fpxx_1.fplx != FPLX.DZFP) && (fpxx_1.fplx != FPLX.JSFP))
                    {
                        builder.Append(this.taxCard_0.CipherVersion).Append("\n");
                    }
                    else
                    {
                        builder.Append(fpxx_1.jqbh).Append("\n");
                    }
                    builder.Append(fpxx_1.zyspmc).Append("\n");
                    ilog_0.Debug("<<<<<<<<<开始取报税期");
                    string str9 = string.Empty;
                    if (bool_1)
                    {
                        str9 = "1";
                    }
                    else
                    {
                        List<int> list2 = null;
                        string str10 = this.taxCard_0.GetPeriodCount((int) fpxx_1.fplx, out list2, fpxx_1.kpjh);
                        ilog_0.Debug("<<<<<<<<<取报税期，返回值=" + str10);
                        if (ToolUtil.GetReturnErrCode(str10) != 0)
                        {
                            ilog_0.Debug("<<<<<<<<<取报税期出现错误，错误号=" + str10);
                            return str10;
                        }
                        if (list2 == null)
                        {
                            ilog_0.Debug("<<<<<<<<<取报税期出现错误，返回的list为空");
                            return "A698";
                        }
                        if (list2.Count < 2)
                        {
                            ilog_0.Debug("<<<<<<<<<取报税期出现错误，返回的list长度不对，" + list2.Count.ToString());
                            return "A698";
                        }
                        str9 = list2[1].ToString();
                    }
                    if ((fpxx_1.fplx == FPLX.ZYFP) && fpxx_1.isRed)
                    {
                        if (!string.IsNullOrEmpty(fpxx_1.blueFpdm) && !string.IsNullOrEmpty(fpxx_1.blueFphm))
                        {
                            str9 = str9 + "@" + fpxx_1.blueFpdm + "@" + string.Format("{0:D8}", int.Parse(fpxx_1.blueFphm));
                        }
                    }
                    else if (fpxx_1.fplx == FPLX.JSFP)
                    {
                        int num4 = 0;
                        Dictionary<string, int> jsPrintTemplate = ToolUtil.GetJsPrintTemplate();
                        if ((jsPrintTemplate.Count > 0) && jsPrintTemplate.ContainsKey(fpxx_1.dy_mb))
                        {
                            num4 = jsPrintTemplate[fpxx_1.dy_mb];
                        }
                        num = num4 + 1;
                        string str11 = num.ToString("X2");
                        if ((fpxx_1.isRed && !string.IsNullOrEmpty(fpxx_1.blueFpdm)) && !string.IsNullOrEmpty(fpxx_1.blueFphm))
                        {
                            str9 = str9 + "@" + fpxx_1.blueFpdm + "@" + string.Format("{0:D8}", int.Parse(fpxx_1.blueFphm));
                        }
                        str9 = str9 + "@" + str11;
                    }
                    builder.Append(str9).Append("\n");
                    builder.Append(fpxx_1.xfmc).Append("\n");
                    builder.Append(fpxx_1.xfsh).Append("\n");
                    builder.Append(fpxx_1.xfdzdh).Append("\n");
                    builder.Append(fpxx_1.xfyhzh).Append("\n");
                    builder.Append(fpxx_1.kpr).Append("\n");
                    builder.Append(fpxx_1.fhr).Append("\n");
                    builder.Append(fpxx_1.skr).Append("\n");
                    builder.Append(fpxx_1.bz).Append("\n");
                    builder.Append(fpxx_1.je).Append("\n");
                    builder.Append(fpxx_1.se).Append("\n");
                    builder.Append((fpxx_1.Qdxx == null) ? "N" : "Y").Append("\n");
                    int num5 = 0xfa00;
                    if (this.taxCard_0.IsLargeInvDetail)
                    {
                        num5 = 0x280000;
                    }
                    int num6 = (num5 - ToolUtil.GetByteCount(builder.ToString())) / 0x178;
                    if ((fpxx_1.Mxxx == null) || (fpxx_1.Mxxx.Count <= 0))
                    {
                        ilog_0.Debug("fpxx.Mxxx is null");
                    }
                    int num7 = 1;
                    foreach (Dictionary<SPXX, string> dictionary5 in fpxx_1.Mxxx)
                    {
                        if (num7 > num6)
                        {
                            break;
                        }
                        builder.Append(Class34.smethod_23(dictionary5[SPXX.SPMC], 100, true, true));
                        builder.Append(Class34.smethod_23(dictionary5[SPXX.GGXH], 40, true, true));
                        builder.Append(Class34.smethod_23(dictionary5[SPXX.JLDW], 0x16, true, true));
                        builder.Append(Class34.smethod_24(dictionary5[SPXX.SL], 0x15, true));
                        builder.Append(Class34.smethod_24(dictionary5[SPXX.DJ], 0x15, true));
                        builder.Append(Class34.smethod_24(dictionary5[SPXX.JE], 0x12, true));
                        builder.Append(Class34.smethod_24(dictionary5[SPXX.SLV], 6, true));
                        builder.Append(Class34.smethod_24(dictionary5[SPXX.SE], 0x12, true));
                        builder.Append(Class34.smethod_23(num7.ToString(), 8, true, true));
                        builder.Append(Class34.smethod_23(dictionary5[SPXX.FPHXZ], 3, true, true));
                        builder.Append(Class34.smethod_23(dictionary5[SPXX.HSJBZ], 3, true, true));
                        builder.Append(Class34.smethod_23(dictionary5[SPXX.FLBM], 30, true, true));
                        builder.Append(Class34.smethod_23(dictionary5[SPXX.SPBH], 30, true, true));
                        builder.Append(Class34.smethod_23(dictionary5[SPXX.XSYH], 3, true, true));
                        builder.Append(Class34.smethod_23(dictionary5[SPXX.YHSM], 50, true, true));
                        builder.Append(Class34.smethod_23(dictionary5[SPXX.LSLVBS], 3, true, true));
                        num7++;
                    }
                    if (fpxx_1.Qdxx != null)
                    {
                        builder.Append("\n");
                        num6 = (num6 - num7) + 1;
                        if (fpxx_1.Qdxx.Count > num6)
                        {
                            ilog_0.Error("发票清单行数超过底层能存储的最大行数 " + fpxx_1.Qdxx.Count.ToString() + " " + num6.ToString());
                            return "A655";
                        }
                        int num8 = 1;
                        foreach (Dictionary<SPXX, string> dictionary6 in fpxx_1.Qdxx)
                        {
                            if (num8 > num6)
                            {
                                break;
                            }
                            builder.Append(Class34.smethod_23(dictionary6[SPXX.SPMC], 100, true, true));
                            builder.Append(Class34.smethod_23(dictionary6[SPXX.GGXH], 40, true, true));
                            builder.Append(Class34.smethod_23(dictionary6[SPXX.JLDW], 0x16, true, true));
                            builder.Append(Class34.smethod_24(dictionary6[SPXX.SL], 0x15, true));
                            builder.Append(Class34.smethod_24(dictionary6[SPXX.DJ], 0x15, true));
                            builder.Append(Class34.smethod_24(dictionary6[SPXX.JE], 0x12, true));
                            builder.Append(Class34.smethod_24(dictionary6[SPXX.SLV], 6, true));
                            builder.Append(Class34.smethod_24(dictionary6[SPXX.SE], 0x12, true));
                            builder.Append(Class34.smethod_23(num8.ToString(), 8, true, true));
                            builder.Append(Class34.smethod_23(dictionary6[SPXX.FPHXZ], 3, true, true));
                            builder.Append(Class34.smethod_23(dictionary6[SPXX.HSJBZ], 3, true, true));
                            if (!dictionary6[SPXX.FPHXZ].Equals("5"))
                            {
                                builder.Append(Class34.smethod_23(dictionary6[SPXX.FLBM], 30, true, true));
                                builder.Append(Class34.smethod_23(dictionary6[SPXX.SPBH], 30, true, true));
                                builder.Append(Class34.smethod_23(dictionary6[SPXX.XSYH], 3, true, true));
                                builder.Append(Class34.smethod_23(dictionary6[SPXX.YHSM], 50, true, true));
                                builder.Append(Class34.smethod_23(dictionary6[SPXX.LSLVBS], 3, true, true));
                            }
                            else
                            {
                                builder.Append(Class34.smethod_23("", 30, true, true));
                                builder.Append(Class34.smethod_23("", 30, true, true));
                                builder.Append(Class34.smethod_23("", 3, true, true));
                                builder.Append(Class34.smethod_23("", 50, true, true));
                                builder.Append(Class34.smethod_23("", 3, true, true));
                            }
                            num8++;
                        }
                    }
                }
                MatchCollection matchs = Regex.Matches(builder.ToString(), "\n");
                if ((((fpxx_1.fplx == FPLX.ZYFP) || (fpxx_1.fplx == FPLX.PTFP)) || ((fpxx_1.fplx == FPLX.DZFP) || (fpxx_1.fplx == FPLX.JSFP))) && (((fpxx_1.Qdxx != null) && (matchs.Count != 0x17)) || ((fpxx_1.Qdxx == null) && (matchs.Count != 0x16))))
                {
                    num = 0x18;
                    ilog_0.Error(@"生成发票明细串时，字段中有非法字符 \n " + matchs.Count.ToString() + " " + num.ToString());
                    return "A658";
                }
                if ((fpxx_1.fplx == FPLX.HYFP) && (matchs.Count != 0x1d))
                {
                    num = 30;
                    ilog_0.Error(@"生成发票明细串时，字段中有非法字符 \n " + matchs.Count.ToString() + " " + num.ToString());
                    return "A658";
                }
                if ((fpxx_1.fplx == FPLX.JDCFP) && (matchs.Count != 40))
                {
                    ilog_0.Error(@"生成发票明细串时，字段中有非法字符 \n " + matchs.Count.ToString() + " " + 40.ToString());
                    return "A658";
                }
                string_1 = builder.ToString();
                return "0000";
            }
            catch (Exception exception)
            {
                ilog_0.Error("GetInvoiceString: " + exception.Message);
                return "A699";
            }
        }

        private string method_23(Fpxx fpxx_1)
        {
            try
            {
                if (fpxx_1 == null)
                {
                    return null;
                }
                StringBuilder builder = new StringBuilder();
                if (fpxx_1.fplx == FPLX.ZYFP)
                {
                    builder.Append(fpxx_1.fpdm);
                    builder.Append(this.method_24(fpxx_1.fphm, 8, true, '0'));
                    if (fpxx_1.kprq.Length >= 10)
                    {
                        builder.Append(DateTime.Parse(fpxx_1.kprq).ToString("yyyyMMdd"));
                    }
                    else if (fpxx_1.kprq.Length == 8)
                    {
                        builder.Append(DateTime.ParseExact(fpxx_1.kprq, "yyyyMMdd", null).ToString("yyyyMMdd"));
                    }
                    builder.Append(fpxx_1.mw);
                    builder.Append(fpxx_1.jqbh);
                    builder.Append(fpxx_1.gfmc);
                    builder.Append(fpxx_1.gfsh);
                    builder.Append(fpxx_1.gfdzdh);
                    builder.Append(fpxx_1.gfyhzh);
                    builder.Append(Class34.smethod_19(fpxx_1.je, 2));
                    builder.Append(Class34.smethod_19(fpxx_1.se, 2));
                    builder.Append(Class34.smethod_19(Class34.smethod_17(fpxx_1.je, fpxx_1.se), 2));
                    builder.Append(fpxx_1.xfmc);
                    builder.Append(fpxx_1.xfsh);
                    builder.Append(fpxx_1.xfdzdh);
                    builder.Append(fpxx_1.xfyhzh);
                    builder.Append(fpxx_1.skr);
                    builder.Append(fpxx_1.fhr);
                    builder.Append(fpxx_1.kpr);
                    builder.Append(ToolUtil.GetString(Convert.FromBase64String(fpxx_1.bz)));
                    if (fpxx_1.isRed)
                    {
                        builder.Append(fpxx_1.redNum);
                    }
                    builder.Append(fpxx_1.zfbz ? "Y" : "N");
                    string str2 = "";
                    if (fpxx_1.Zyfplx == ZYFP_LX.NCP_XS)
                    {
                        str2 = "01";
                    }
                    else if (fpxx_1.Zyfplx == ZYFP_LX.NCP_SG)
                    {
                        str2 = "02";
                    }
                    else if ((fpxx_1.Zyfplx == ZYFP_LX.XT_CCP) || (fpxx_1.Zyfplx == ZYFP_LX.XT_YCL))
                    {
                        str2 = "03";
                    }
                    builder.Append(str2);
                    if (fpxx_1.Mxxx != null)
                    {
                        int num = 1;
                        foreach (Dictionary<SPXX, string> dictionary in fpxx_1.Mxxx)
                        {
                            builder.Append(num);
                            builder.Append(dictionary[SPXX.SPMC]);
                            builder.Append(dictionary[SPXX.GGXH]);
                            builder.Append(dictionary[SPXX.JLDW]);
                            if (string.IsNullOrEmpty(dictionary[SPXX.SL]))
                            {
                                builder.Append("0.00000000");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(dictionary[SPXX.SL], 8));
                            }
                            if (string.IsNullOrEmpty(dictionary[SPXX.DJ]))
                            {
                                builder.Append("0.00000000");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(fpxx_1.Get_Print_Dj(dictionary, 3, null), 8));
                            }
                            if (string.IsNullOrEmpty(dictionary[SPXX.JE]))
                            {
                                builder.Append("0.00");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(dictionary[SPXX.JE], 2));
                            }
                            if (string.IsNullOrEmpty(dictionary[SPXX.SLV]))
                            {
                                builder.Append("0.00");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(dictionary[SPXX.SLV], 2));
                            }
                            if (string.IsNullOrEmpty(dictionary[SPXX.SE]))
                            {
                                builder.Append("0.00");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(dictionary[SPXX.SE], 2));
                            }
                            num++;
                        }
                    }
                    if (fpxx_1.Qdxx != null)
                    {
                        int num2 = 1;
                        foreach (Dictionary<SPXX, string> dictionary2 in fpxx_1.Qdxx)
                        {
                            builder.Append(num2);
                            builder.Append(dictionary2[SPXX.SPMC]);
                            builder.Append(dictionary2[SPXX.GGXH]);
                            builder.Append(dictionary2[SPXX.JLDW]);
                            if (string.IsNullOrEmpty(dictionary2[SPXX.SL]))
                            {
                                builder.Append("0.00000000");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(dictionary2[SPXX.SL], 8));
                            }
                            if (string.IsNullOrEmpty(dictionary2[SPXX.DJ]))
                            {
                                builder.Append("0.00000000");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(fpxx_1.Get_Print_Dj(dictionary2, 3, null), 8));
                            }
                            if (string.IsNullOrEmpty(dictionary2[SPXX.JE]))
                            {
                                builder.Append("0.00");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(dictionary2[SPXX.JE], 2));
                            }
                            if (string.IsNullOrEmpty(dictionary2[SPXX.SLV]))
                            {
                                builder.Append("0.00");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(dictionary2[SPXX.SLV], 2));
                            }
                            if (string.IsNullOrEmpty(dictionary2[SPXX.SE]))
                            {
                                builder.Append("0.00");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(dictionary2[SPXX.SE], 2));
                            }
                            num2++;
                        }
                    }
                }
                else if ((fpxx_1.fplx != FPLX.PTFP) && (fpxx_1.fplx != FPLX.DZFP))
                {
                    if (fpxx_1.fplx == FPLX.HYFP)
                    {
                        builder.Append(fpxx_1.fpdm);
                        builder.Append(this.method_24(fpxx_1.fphm, 8, true, '0'));
                        if (fpxx_1.kprq.Length >= 10)
                        {
                            builder.Append(DateTime.Parse(fpxx_1.kprq).ToString("yyyyMMdd"));
                        }
                        else if (fpxx_1.kprq.Length == 8)
                        {
                            builder.Append(DateTime.ParseExact(fpxx_1.kprq, "yyyyMMdd", null).ToString("yyyyMMdd"));
                        }
                        builder.Append(fpxx_1.mw);
                        builder.Append(fpxx_1.cyrmc);
                        builder.Append(fpxx_1.cyrnsrsbh);
                        builder.Append(fpxx_1.spfmc);
                        builder.Append(fpxx_1.spfnsrsbh);
                        builder.Append(fpxx_1.shrmc);
                        builder.Append(fpxx_1.shrnsrsbh);
                        builder.Append(fpxx_1.fhrmc);
                        builder.Append(fpxx_1.fhrnsrsbh);
                        builder.Append(fpxx_1.qyd);
                        builder.Append(Class34.smethod_19(fpxx_1.je, 2));
                        builder.Append(Class34.smethod_19(fpxx_1.sLv, 2));
                        builder.Append(Class34.smethod_19(fpxx_1.se, 2));
                        builder.Append(Class34.smethod_19(Class34.smethod_17(fpxx_1.je, fpxx_1.se), 2));
                        builder.Append(fpxx_1.jqbh);
                        builder.Append(fpxx_1.czch);
                        builder.Append(fpxx_1.ccdw);
                        builder.Append(fpxx_1.zgswjgmc);
                        builder.Append(fpxx_1.zgswjgdm);
                        builder.Append(fpxx_1.skr);
                        builder.Append(fpxx_1.fhr);
                        builder.Append(fpxx_1.kpr);
                        builder.Append(ToolUtil.GetString(Convert.FromBase64String(fpxx_1.bz)));
                        builder.Append(ToolUtil.GetString(Convert.FromBase64String(fpxx_1.yshwxx)));
                        if (fpxx_1.isRed)
                        {
                            builder.Append(fpxx_1.redNum);
                        }
                        builder.Append(fpxx_1.zfbz ? "Y" : "N");
                        if (fpxx_1.Mxxx != null)
                        {
                            int num5 = 1;
                            foreach (Dictionary<SPXX, string> dictionary5 in fpxx_1.Mxxx)
                            {
                                builder.Append(num5);
                                builder.Append(dictionary5[SPXX.SPMC]);
                                if (string.IsNullOrEmpty(dictionary5[SPXX.JE]))
                                {
                                    builder.Append("0.00");
                                }
                                else
                                {
                                    builder.Append(Class34.smethod_19(dictionary5[SPXX.JE], 2));
                                }
                                num5++;
                            }
                        }
                        if (fpxx_1.Qdxx != null)
                        {
                            int num6 = 1;
                            foreach (Dictionary<SPXX, string> dictionary6 in fpxx_1.Qdxx)
                            {
                                builder.Append(num6);
                                builder.Append(dictionary6[SPXX.SPMC]);
                                if (string.IsNullOrEmpty(dictionary6[SPXX.JE]))
                                {
                                    builder.Append("0.00");
                                }
                                else
                                {
                                    builder.Append(Class34.smethod_19(dictionary6[SPXX.JE], 2));
                                }
                                num6++;
                            }
                        }
                    }
                    else if (fpxx_1.fplx == FPLX.JDCFP)
                    {
                        builder.Append(fpxx_1.fpdm);
                        builder.Append(this.method_24(fpxx_1.fphm, 8, true, '0'));
                        if (fpxx_1.kprq.Length >= 10)
                        {
                            builder.Append(DateTime.Parse(fpxx_1.kprq).ToString("yyyyMMdd"));
                        }
                        else if (fpxx_1.kprq.Length == 8)
                        {
                            builder.Append(DateTime.ParseExact(fpxx_1.kprq, "yyyyMMdd", null).ToString("yyyyMMdd"));
                        }
                        builder.Append(fpxx_1.mw);
                        builder.Append(fpxx_1.jqbh);
                        builder.Append(fpxx_1.gfmc);
                        builder.Append(fpxx_1.sfzhm);
                        if (fpxx_1.isNewJdcfp)
                        {
                            builder.Append(fpxx_1.gfsh);
                        }
                        builder.Append(fpxx_1.cllx);
                        builder.Append(fpxx_1.cpxh);
                        builder.Append(fpxx_1.cd);
                        builder.Append(fpxx_1.hgzh);
                        builder.Append(fpxx_1.jkzmsh);
                        builder.Append(fpxx_1.sjdh);
                        builder.Append(fpxx_1.fdjhm);
                        builder.Append(fpxx_1.clsbdh);
                        builder.Append(Class34.smethod_19(Class34.smethod_17(fpxx_1.je, fpxx_1.se), 2));
                        builder.Append(fpxx_1.xfmc);
                        builder.Append(fpxx_1.xfsh);
                        builder.Append(fpxx_1.xfdh);
                        builder.Append(fpxx_1.xfzh);
                        builder.Append(fpxx_1.xfdz);
                        builder.Append(fpxx_1.xfyh);
                        builder.Append(Class34.smethod_19(fpxx_1.sLv, 2));
                        builder.Append(Class34.smethod_19(fpxx_1.se, 2));
                        builder.Append(fpxx_1.zgswjgmc);
                        builder.Append(fpxx_1.zgswjgdm);
                        builder.Append(Class34.smethod_19(fpxx_1.je, 2));
                        builder.Append(fpxx_1.dw);
                        builder.Append(fpxx_1.xcrs);
                        builder.Append(fpxx_1.kpr);
                        if (fpxx_1.isRed)
                        {
                            if (!string.IsNullOrEmpty(fpxx_1.blueFpdm))
                            {
                                builder.Append(this.method_24(fpxx_1.blueFpdm, 12, true, '0'));
                            }
                            if (!string.IsNullOrEmpty(fpxx_1.blueFphm))
                            {
                                builder.Append(this.method_24(fpxx_1.blueFphm, 8, true, '0'));
                            }
                        }
                        builder.Append(fpxx_1.zfbz ? "Y" : "N");
                    }
                    else if (fpxx_1.fplx == FPLX.JSFP)
                    {
                        builder.Append(fpxx_1.fpdm);
                        builder.Append(this.method_24(fpxx_1.fphm, 8, true, '0'));
                        if (fpxx_1.kprq.Length >= 10)
                        {
                            builder.Append(DateTime.Parse(fpxx_1.kprq).ToString("yyyyMMdd"));
                        }
                        else if (fpxx_1.kprq.Length == 8)
                        {
                            builder.Append(DateTime.ParseExact(fpxx_1.kprq, "yyyyMMdd", null).ToString("yyyyMMdd"));
                        }
                        builder.Append(fpxx_1.jym);
                        builder.Append(fpxx_1.jqbh);
                        builder.Append(fpxx_1.gfmc);
                        builder.Append(fpxx_1.gfsh);
                        builder.Append(Class34.smethod_19(fpxx_1.je, 2));
                        builder.Append(Class34.smethod_19(fpxx_1.se, 2));
                        builder.Append(Class34.smethod_19(Class34.smethod_17(fpxx_1.je, fpxx_1.se), 2));
                        builder.Append(fpxx_1.xfmc);
                        builder.Append(fpxx_1.xfsh);
                        builder.Append(fpxx_1.skr);
                        builder.Append(ToolUtil.GetString(Convert.FromBase64String(fpxx_1.bz)));
                        if (fpxx_1.isRed)
                        {
                            if (!string.IsNullOrEmpty(fpxx_1.blueFpdm))
                            {
                                builder.Append(fpxx_1.blueFpdm);
                            }
                            if (!string.IsNullOrEmpty(fpxx_1.blueFphm))
                            {
                                builder.Append(this.method_24(fpxx_1.blueFphm, 8, true, '0'));
                            }
                        }
                        builder.Append(fpxx_1.zfbz ? "Y" : "N");
                        if (fpxx_1.Mxxx != null)
                        {
                            int num7 = 1;
                            foreach (Dictionary<SPXX, string> dictionary7 in fpxx_1.Mxxx)
                            {
                                builder.Append(num7);
                                builder.Append(dictionary7[SPXX.SPMC]);
                                if (string.IsNullOrEmpty(dictionary7[SPXX.SL]))
                                {
                                    builder.Append("0.00000000");
                                }
                                else
                                {
                                    builder.Append(Class34.smethod_19(dictionary7[SPXX.SL], 8));
                                }
                                if (string.IsNullOrEmpty(dictionary7[SPXX.DJ]))
                                {
                                    builder.Append("0.00000000");
                                }
                                else
                                {
                                    builder.Append(Class34.smethod_19(fpxx_1.Get_Print_Dj(dictionary7, 3, null), 8));
                                }
                                if (string.IsNullOrEmpty(dictionary7[SPXX.JE]))
                                {
                                    builder.Append("0.00");
                                }
                                else
                                {
                                    builder.Append(Class34.smethod_19(Class34.smethod_17(dictionary7[SPXX.JE], dictionary7[SPXX.SE]), 2));
                                }
                                num7++;
                            }
                        }
                    }
                }
                else
                {
                    builder.Append(fpxx_1.fpdm);
                    builder.Append(this.method_24(fpxx_1.fphm, 8, true, '0'));
                    if (fpxx_1.kprq.Length >= 10)
                    {
                        builder.Append(DateTime.Parse(fpxx_1.kprq).ToString("yyyyMMdd"));
                    }
                    else if (fpxx_1.kprq.Length == 8)
                    {
                        builder.Append(DateTime.ParseExact(fpxx_1.kprq, "yyyyMMdd", null).ToString("yyyyMMdd"));
                    }
                    builder.Append(fpxx_1.mw);
                    builder.Append(fpxx_1.jym);
                    builder.Append(fpxx_1.jqbh);
                    builder.Append(fpxx_1.gfmc);
                    builder.Append(fpxx_1.gfsh);
                    builder.Append(fpxx_1.gfdzdh);
                    builder.Append(fpxx_1.gfyhzh);
                    builder.Append(Class34.smethod_19(fpxx_1.je, 2));
                    builder.Append(Class34.smethod_19(fpxx_1.se, 2));
                    builder.Append(Class34.smethod_19(Class34.smethod_17(fpxx_1.je, fpxx_1.se), 2));
                    builder.Append(fpxx_1.xfmc);
                    builder.Append(fpxx_1.xfsh);
                    builder.Append(fpxx_1.xfdzdh);
                    builder.Append(fpxx_1.xfyhzh);
                    builder.Append(fpxx_1.skr);
                    builder.Append(fpxx_1.fhr);
                    builder.Append(fpxx_1.kpr);
                    builder.Append(ToolUtil.GetString(Convert.FromBase64String(fpxx_1.bz)));
                    if (fpxx_1.isRed)
                    {
                        if (!string.IsNullOrEmpty(fpxx_1.blueFpdm))
                        {
                            builder.Append(fpxx_1.blueFpdm);
                        }
                        if (!string.IsNullOrEmpty(fpxx_1.blueFphm))
                        {
                            builder.Append(this.method_24(fpxx_1.blueFphm, 8, true, '0'));
                        }
                    }
                    builder.Append(fpxx_1.zfbz ? "Y" : "N");
                    string str3 = "";
                    if (fpxx_1.Zyfplx == ZYFP_LX.NCP_XS)
                    {
                        str3 = "01";
                    }
                    else if (fpxx_1.Zyfplx == ZYFP_LX.NCP_SG)
                    {
                        str3 = "02";
                    }
                    else if ((fpxx_1.Zyfplx == ZYFP_LX.XT_CCP) || (fpxx_1.Zyfplx == ZYFP_LX.XT_YCL))
                    {
                        str3 = "03";
                    }
                    builder.Append(str3);
                    if (fpxx_1.Mxxx != null)
                    {
                        int num3 = 1;
                        foreach (Dictionary<SPXX, string> dictionary3 in fpxx_1.Mxxx)
                        {
                            builder.Append(num3);
                            builder.Append(dictionary3[SPXX.SPMC]);
                            builder.Append(dictionary3[SPXX.GGXH]);
                            builder.Append(dictionary3[SPXX.JLDW]);
                            if (string.IsNullOrEmpty(dictionary3[SPXX.SL]))
                            {
                                builder.Append("0.00000000");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(dictionary3[SPXX.SL], 8));
                            }
                            if (string.IsNullOrEmpty(dictionary3[SPXX.DJ]))
                            {
                                builder.Append("0.00000000");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(fpxx_1.Get_Print_Dj(dictionary3, 3, null), 8));
                            }
                            if (string.IsNullOrEmpty(dictionary3[SPXX.JE]))
                            {
                                builder.Append("0.00");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(dictionary3[SPXX.JE], 2));
                            }
                            if (string.IsNullOrEmpty(dictionary3[SPXX.SLV]))
                            {
                                builder.Append("0.00");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(dictionary3[SPXX.SLV], 2));
                            }
                            if (string.IsNullOrEmpty(dictionary3[SPXX.SE]))
                            {
                                builder.Append("0.00");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(dictionary3[SPXX.SE], 2));
                            }
                            num3++;
                        }
                    }
                    if (fpxx_1.Qdxx != null)
                    {
                        int num4 = 1;
                        foreach (Dictionary<SPXX, string> dictionary4 in fpxx_1.Qdxx)
                        {
                            builder.Append(num4);
                            builder.Append(dictionary4[SPXX.SPMC]);
                            builder.Append(dictionary4[SPXX.GGXH]);
                            builder.Append(dictionary4[SPXX.JLDW]);
                            if (string.IsNullOrEmpty(dictionary4[SPXX.SL]))
                            {
                                builder.Append("0.00000000");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(dictionary4[SPXX.SL], 8));
                            }
                            if (string.IsNullOrEmpty(dictionary4[SPXX.DJ]))
                            {
                                builder.Append("0.00000000");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(fpxx_1.Get_Print_Dj(dictionary4, 3, null), 8));
                            }
                            if (string.IsNullOrEmpty(dictionary4[SPXX.JE]))
                            {
                                builder.Append("0.00");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(dictionary4[SPXX.JE], 2));
                            }
                            if (string.IsNullOrEmpty(dictionary4[SPXX.SLV]))
                            {
                                builder.Append("0.00");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(dictionary4[SPXX.SLV], 2));
                            }
                            if (string.IsNullOrEmpty(dictionary4[SPXX.SE]))
                            {
                                builder.Append("0.00");
                            }
                            else
                            {
                                builder.Append(Class34.smethod_19(dictionary4[SPXX.SE], 2));
                            }
                            num4++;
                        }
                    }
                }
                string str4 = builder.ToString();
                foreach (char ch in Fpxx.SpecialChar)
                {
                    str4 = str4.Replace(ch, '?');
                }
                return str4;
            }
            catch (Exception exception)
            {
                ilog_0.Error("GetInvSignString: " + exception.Message);
                this.string_0 = "A699";
                return string.Empty;
            }
        }

        private string method_24(string string_1, int int_0, bool bool_1 = true, char char_0 = '0')
        {
            string str = string_1;
            if (str == null)
            {
                return new string(char_0, int_0);
            }
            int byteCount = ToolUtil.GetByteCount(str);
            if (byteCount < int_0)
            {
                if (bool_1)
                {
                    return new StringBuilder().Append(new string(char_0, int_0 - byteCount)).Append(str).ToString();
                }
                return new StringBuilder(str).Append(new string(char_0, int_0 - byteCount)).ToString();
            }
            if (byteCount > int_0)
            {
                return str.Substring(0, int_0);
            }
            return str;
        }

        private byte[] method_25(Fpxx fpxx_1)
        {
            try
            {
                this.string_0 = "0000";
                if (fpxx_1 == null)
                {
                    return null;
                }
                if (!fpxx_1.hzfw)
                {
                    return null;
                }
                if (this.fpxx_0 == null)
                {
                    this.fpxx_0 = this.ConvertInvoiceToZH(fpxx_1);
                    if (this.fpxx_0 == null)
                    {
                        this.string_0 = "A604";
                        return null;
                    }
                }
                if (this.class37_0 == null)
                {
                    this.class37_0 = this.method_29(fpxx_1, this.fpxx_0);
                }
                byte[] destinationArray = new byte[0x150];
                int num = 0;
                while (num < destinationArray.Length)
                {
                    destinationArray[num++] = 0x20;
                }
                Array.Copy(this.class37_0.method_8(), 0, destinationArray, 0, this.class37_0.method_8().Length);
                byte[] bytes = ToolUtil.GetBytes(this.class37_0.method_0());
                Array.Copy(bytes, 0, destinationArray, this.class37_0.method_8().Length, bytes.Length);
                byte[] buffer3 = new byte[0x20];
                byte[] sourceArray = Encoding.ASCII.GetBytes("BarcodeKey");
                Array.Copy(sourceArray, 0, buffer3, 0, sourceArray.Length);
                MD5 md = MD5.Create();
                byte[] buffer5 = md.ComputeHash(destinationArray);
                Array.Copy(buffer5, 0, buffer3, 10, buffer5.Length);
                Random random = new Random();
                int num2 = 0;
                num2 = (random.Next() % 0xff) + 1;
                ushort_0 = (ushort) num2;
                byte[] buffer6 = new byte[] { (byte) (num2 >> 8), (byte) num2 };
                Array.Copy(buffer6, 0, buffer3, 0x1a, buffer6.Length);
                string str = string.Empty;
                string str2 = this.method_20(fpxx_1, out str, false);
                if (str2 != "0000")
                {
                    ilog_0.Error("Get32Bytes 获取写卡发票明细串失败，错误号=" + str2);
                    return null;
                }
                byte[] buffer8 = md.ComputeHash(ToolUtil.GetBytes(str));
                byte[] buffer9 = new byte[] { buffer8[2], buffer8[6] };
                Array.Copy(buffer9, 0, buffer3, 0x1c, buffer9.Length);
                byte[] buffer10 = new byte[] { (byte) fpxx_1.Mxxx.Count, 0 };
                Array.Copy(buffer10, 0, buffer3, 30, buffer10.Length);
                return buffer3;
            }
            catch (Exception exception)
            {
                ilog_0.Error("Get32Bytes: " + exception.Message);
                this.string_0 = "A699";
                return null;
            }
        }

        private string method_26(Fpxx fpxx_1)
        {
            int num = 1;
            using (List<Dictionary<SPXX, string>>.Enumerator enumerator = fpxx_1.Mxxx.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Dictionary<SPXX, string> current = enumerator.Current;
                    string str = current[SPXX.SPMC];
                    string str2 = current[SPXX.JLDW];
                    if (str == null)
                    {
                        str = string.Empty;
                    }
                    if (str2 == null)
                    {
                        str2 = string.Empty;
                    }
                    if (this.method_27(fpxx_1.fplx, SplittingField.Spmc, str) == 3)
                    {
                        goto Label_0074;
                    }
                    if (this.method_27(fpxx_1.fplx, SplittingField.Jldw, str2) == 3)
                    {
                        goto Label_00A0;
                    }
                    num++;
                }
                goto Label_00DA;
            Label_0074:
                ilog_0.Error("汉字防伪发票校验失败，第" + num.ToString() + "行商品名称超长");
                return "A612";
            Label_00A0:
                ilog_0.Error("汉字防伪发票校验失败，第" + num.ToString() + "行计量单位超长");
                return "A613";
            }
        Label_00DA:
            if (fpxx_1.Qdxx != null)
            {
                num = 1;
                using (List<Dictionary<SPXX, string>>.Enumerator enumerator2 = fpxx_1.Qdxx.GetEnumerator())
                {
                    while (enumerator2.MoveNext())
                    {
                        Dictionary<SPXX, string> dictionary2 = enumerator2.Current;
                        string str4 = dictionary2[SPXX.SPMC];
                        string str5 = dictionary2[SPXX.JLDW];
                        if (str4 == null)
                        {
                            str4 = string.Empty;
                        }
                        if (str5 == null)
                        {
                            str5 = string.Empty;
                        }
                        if (this.method_27(fpxx_1.fplx, SplittingField.Spmc, str4) == 3)
                        {
                            goto Label_0161;
                        }
                        if (this.method_27(fpxx_1.fplx, SplittingField.Jldw, str5) == 3)
                        {
                            goto Label_018A;
                        }
                        num++;
                    }
                    goto Label_01C1;
                Label_0161:
                    ilog_0.Error("汉字防伪发票校验失败，第" + num.ToString() + "行商品名称超长");
                    return "A612";
                Label_018A:
                    ilog_0.Error("汉字防伪发票校验失败，第" + num.ToString() + "行计量单位超长");
                    return "A613";
                }
            }
        Label_01C1:
            return "0000";
        }

        private int method_27(FPLX fplx_0, SplittingField splittingField_0, string string_1)
        {
            int num = 0;
            if (string.IsNullOrEmpty(string_1))
            {
                return num;
            }
            double num5 = 0.0;
            double num7 = 0.0;
            double num6 = 0.0;
            if (fplx_0 == FPLX.ZYFP)
            {
                if (splittingField_0 == SplittingField.Spmc)
                {
                    num5 = 194.0;
                    num7 = 1.0;
                    num6 = 10.0;
                }
                else if (splittingField_0 == SplittingField.Jldw)
                {
                    num5 = 46.0;
                    num7 = 1.0;
                    num6 = 1.0;
                }
            }
            else if (fplx_0 == FPLX.PTFP)
            {
                if (splittingField_0 == SplittingField.Spmc)
                {
                    num5 = 194.0;
                    num7 = 1.0;
                    num6 = 10.0;
                }
                else if (splittingField_0 == SplittingField.Jldw)
                {
                    num5 = 46.0;
                    num7 = 1.0;
                    num6 = 1.0;
                }
            }
            double num4 = 0.0;
            int num2 = 12;
            new List<string>();
            int num8 = 0;
            num8 = 0;
            while (num8 < string_1.Length)
            {
                char ch = string_1[num8];
                int num9 = num2;
                if (ToolUtil.GetByteCount(ch.ToString()) == 1)
                {
                    num9 = num2 / 2;
                }
                if (((num4 + num9) + 1.5) > (num5 - 10.0))
                {
                    break;
                }
                num4 += num9 + num7;
                num8++;
            }
            if (num8 == string_1.Length)
            {
                return num;
            }
            num2 = 7;
            num4 = 0.0;
            num8 = 0;
            while (num8 < string_1.Length)
            {
                char ch3 = string_1[num8];
                int num3 = num2;
                if (ToolUtil.GetByteCount(ch3.ToString()) == 1)
                {
                    num3 = ((num2 % 2) == 0) ? (num2 / 2) : ((num2 / 2) + 1);
                }
                if ((num4 + num3) > (num5 - num6))
                {
                    break;
                }
                num4 += num3 + num7;
                num8++;
            }
            if (num8 == string_1.Length)
            {
                return 1;
            }
            num4 = 0.0;
            while (num8 < string_1.Length)
            {
                char ch2 = string_1[num8];
                int num11 = num2;
                if (ToolUtil.GetByteCount(ch2.ToString()) == 1)
                {
                    num11 = ((num2 % 2) == 0) ? (num2 / 2) : ((num2 / 2) + 1);
                }
                if ((num4 + num11) > (num5 - num6))
                {
                    break;
                }
                num4 += num11 + num7;
                num8++;
            }
            if (num8 == string_1.Length)
            {
                return 2;
            }
            return 3;
        }

        private List<string> method_28(FPLX fplx_0, SplittingField splittingField_0, string string_1)
        {
            double num = 0.0;
            double num2 = 0.0;
            if (fplx_0 == FPLX.ZYFP)
            {
                if (splittingField_0 == SplittingField.Spmc)
                {
                    num = 194.0;
                    num2 = 1.0;
                }
                else if (splittingField_0 == SplittingField.Jldw)
                {
                    num = 46.0;
                    num2 = 1.0;
                }
            }
            else if (fplx_0 == FPLX.PTFP)
            {
                if (splittingField_0 == SplittingField.Spmc)
                {
                    num = 194.0;
                    num2 = 1.0;
                }
                else if (splittingField_0 == SplittingField.Jldw)
                {
                    num = 46.0;
                    num2 = 1.0;
                }
            }
            double num3 = 0.0;
            string item = "";
            List<string> list = new List<string>();
            foreach (char ch in string_1)
            {
                int num4 = 12;
                if (ToolUtil.GetByteCount(ch.ToString()) == 1)
                {
                    num4 = 6;
                }
                if (((num3 + num4) + 1.5) > (num - 10.0))
                {
                    list.Add(item);
                    item = ch.ToString();
                    num3 = num4 + num2;
                }
                else
                {
                    item = item + ch.ToString();
                    num3 += num4 + num2;
                }
            }
            if (item.Length > 0)
            {
                list.Add(item);
            }
            if (list.Count == 0)
            {
                list.Add("");
            }
            return list;
        }

        private Class37 method_29(Fpxx fpxx_1, Fpxx fpxx_2)
        {
            string str = fpxx_2.gfmc + fpxx_2.xfmc;
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            string str2 = "00";
            StringBuilder builder = new StringBuilder(str2);
            list.Add(fpxx_2.gfmc);
            list2.Add(fpxx_2.gfmc);
            list.Add(fpxx_2.xfmc);
            list2.Add(fpxx_2.xfmc);
            list.Add(fpxx_1.Mxxx.Count.ToString());
            list.Add(fpxx_2.Mxxx.Count.ToString());
            list2.Add(fpxx_2.Mxxx.Count.ToString());
            int num4 = 1;
            int num5 = 0;
            string item = "";
            string str4 = "";
            string str5 = "";
            using (List<Dictionary<SPXX, string>>.Enumerator enumerator = fpxx_2.Mxxx.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Dictionary<SPXX, string> current = enumerator.Current;
                    int num16 = int.Parse(current[SPXX.BZ]);
                    if (num4 > 1)
                    {
                        if (num16 == num5)
                        {
                            builder.Append("1");
                        }
                        else
                        {
                            builder.Append("0");
                        }
                    }
                    num5 = num16;
                    item = current[SPXX.SPMC];
                    str4 = current[SPXX.JLDW];
                    str5 = current[SPXX.SL];
                    str = str + item + str4;
                    list.Add(item);
                    list2.Add(item);
                    list.Add(str4);
                    list2.Add(str4);
                    if ((str5 != null) && !str5.Equals(""))
                    {
                        string str10 = ComputeFloatNumber(str5, 9);
                        str = str + str10;
                        list.Add(str10);
                        string str9 = ComputeFloatNumber(str5, 9);
                        if (string.IsNullOrEmpty(str9))
                        {
                            goto Label_01E6;
                        }
                        list2.Add(str9);
                    }
                    else
                    {
                        str = str ?? "";
                        list.Add("");
                        list2.Add("");
                    }
                    num4++;
                }
                goto Label_0212;
            Label_01E6:
                ilog_0.Error("汉字防伪校验出错，发票明细中数量超长=" + str5);
                return null;
            }
        Label_0212:
            str2 = builder.ToString();
            byte[] sourceArray = new byte[30];
            for (int i = 0; i < list.Count; i++)
            {
                switch (i)
                {
                    case 2:
                        sourceArray[i] = (byte) ((8 | int.Parse(list[i])) << 4);
                        break;

                    case 3:
                        sourceArray[i - 1] = (byte) (sourceArray[i - 1] | int.Parse(list[i]));
                        for (int k = 0; k < str2.Length; k++)
                        {
                            byte num11 = 1;
                            if (str2[k] == '1')
                            {
                                num11 = (byte) (num11 << (8 - (k + 1)));
                                sourceArray[i] = (byte) (sourceArray[i] | num11);
                            }
                        }
                        break;

                    default:
                        sourceArray[i] = (byte) ToolUtil.GetByteCount(list[i]);
                        break;
                }
            }
            byte[] destinationArray = new byte[list.Count];
            Array.Copy(sourceArray, 0, destinationArray, 0, destinationArray.Length);
            byte[] buffer2 = new byte[460];
            int destinationIndex = 0;
            for (int j = 0; j < list2.Count; j++)
            {
                string s = list2[j];
                byte[] buffer = new byte[15];
                if (j == 2)
                {
                    int num8 = int.Parse(s);
                    buffer[0] = (byte) num8;
                    Array.Copy(buffer, 0, buffer2, destinationIndex, 1);
                    destinationIndex++;
                }
                else if ((j > 3) && ((j % 3) == 2))
                {
                    int length = s.Length;
                    buffer[0] = (byte) length;
                    Array.Copy(buffer, 0, buffer2, destinationIndex, 1);
                    destinationIndex++;
                }
                else
                {
                    int num6 = s.Length;
                    int num12 = 1;
                    if (num6 > 0)
                    {
                        num12 += num6 / 8;
                        if ((num6 % 8) > 0)
                        {
                            num12++;
                        }
                        buffer[0] = (byte) num6;
                        StringBuilder builder2 = new StringBuilder();
                        foreach (char ch in s)
                        {
                            if (ToolUtil.GetByteCount(ch.ToString()) == 2)
                            {
                                builder2.Append("0");
                            }
                            else
                            {
                                builder2.Append("1");
                            }
                        }
                        string str8 = builder2.ToString();
                        this.method_35(str8, ref buffer);
                        Array.Copy(buffer, 0, buffer2, destinationIndex, num12);
                        destinationIndex += num12;
                    }
                    else
                    {
                        buffer[0] = 0;
                        Array.Copy(buffer, 0, buffer2, destinationIndex, 1);
                        destinationIndex++;
                    }
                }
            }
            byte[] buffer5 = new byte[destinationIndex];
            Array.Copy(buffer2, 0, buffer5, 0, buffer5.Length);
            Class37 class3 = new Class37();
            class3.method_1(str);
            class3.method_3(list);
            class3.method_5(list2);
            class3.method_7(str2);
            class3.method_9(destinationArray);
            class3.method_11(buffer5);
            return class3;
        }

        private bool method_3(string string_1)
        {
            if (string.IsNullOrEmpty(string_1))
            {
                string_1 = string.Empty;
            }
            string_1 = string_1.ToUpper();
            for (int i = 0; i < string_1.Length; i++)
            {
                if ("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(string_1[i]) < 0)
                {
                    return false;
                }
            }
            return true;
        }

        private Class37 method_30(Fpxx fpxx_1)
        {
            byte[] buffer4;
            string str = fpxx_1.gfmc + fpxx_1.xfmc;
            List<string> list = new List<string>();
            List<string> list2 = new List<string> {
                fpxx_1.gfmc,
                fpxx_1.gfmc,
                fpxx_1.xfmc,
                fpxx_1.xfmc,
                fpxx_1.Mxxx.Count.ToString(),
                fpxx_1.Mxxx.Count.ToString()
            };
            int num3 = 1;
            string item = "";
            string str3 = "";
            string str4 = "";
            using (List<Dictionary<SPXX, string>>.Enumerator enumerator = fpxx_1.Mxxx.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Dictionary<SPXX, string> current = enumerator.Current;
                    item = current[SPXX.SPMC];
                    str3 = current[SPXX.JLDW];
                    str4 = current[SPXX.SL];
                    str = str + item + str3;
                    list.Add(item);
                    list2.Add(item);
                    list.Add(str3);
                    list2.Add(str3);
                    if ((str4 != null) && !str4.Equals(""))
                    {
                        string str6 = ComputeFloatNumber(str4, 9);
                        str = str + str6;
                        list.Add(str6);
                        string str7 = ComputeFloatNumber(str4, 9);
                        if (string.IsNullOrEmpty(str7))
                        {
                            goto Label_017F;
                        }
                        list2.Add(str7);
                    }
                    else
                    {
                        str = str ?? "";
                        list.Add("");
                        list2.Add("");
                    }
                    num3++;
                }
                goto Label_01AB;
            Label_017F:
                ilog_0.Error("汉字防伪校验出错2，发票明细中数量超长2=" + str4);
                return null;
            }
        Label_01AB:
            buffer4 = new byte[30];
            for (int i = 0; i < list.Count; i++)
            {
                if (i == 2)
                {
                    buffer4[i] = byte.Parse(list[i]);
                }
                else
                {
                    buffer4[i] = (byte) ToolUtil.GetByteCount(list[i]);
                }
            }
            byte[] destinationArray = new byte[list.Count];
            Array.Copy(buffer4, 0, destinationArray, 0, destinationArray.Length);
            byte[] buffer2 = new byte[460];
            int destinationIndex = 0;
            for (int j = 0; j < list2.Count; j++)
            {
                string s = list2[j];
                byte[] sourceArray = new byte[15];
                if (j == 2)
                {
                    int num10 = int.Parse(s);
                    sourceArray[0] = (byte) num10;
                    Array.Copy(sourceArray, 0, buffer2, destinationIndex, 1);
                    destinationIndex++;
                }
                else if ((j > 3) && ((j % 3) == 2))
                {
                    int length = s.Length;
                    sourceArray[0] = (byte) length;
                    Array.Copy(sourceArray, 0, buffer2, destinationIndex, 1);
                    destinationIndex++;
                }
                else
                {
                    int num4 = s.Length;
                    int num5 = 1;
                    if (num4 > 0)
                    {
                        num5 += num4 / 8;
                        if ((num4 % 8) > 0)
                        {
                            num5++;
                        }
                        sourceArray[0] = (byte) num4;
                        string str8 = Regex.Replace(Regex.Replace(s, "[\0-\x00ff]", "1", RegexOptions.IgnoreCase), "[^\0-\x00ff]", "0", RegexOptions.IgnoreCase);
                        this.method_35(str8, ref sourceArray);
                        Array.Copy(sourceArray, 0, buffer2, destinationIndex, num5);
                        destinationIndex += num5;
                    }
                    else
                    {
                        sourceArray[0] = 0;
                        Array.Copy(sourceArray, 0, buffer2, destinationIndex, 1);
                        destinationIndex++;
                    }
                }
            }
            byte[] buffer = new byte[destinationIndex];
            Array.Copy(buffer2, 0, buffer, 0, buffer.Length);
            Class37 class2 = new Class37();
            class2.method_1(str);
            class2.method_3(list);
            class2.method_5(list2);
            class2.method_7(string.Empty);
            class2.method_9(destinationArray);
            class2.method_11(buffer);
            return class2;
        }

        private byte[] method_31(string string_1)
        {
            if ((string_1 == null) || (string_1.Length != 0x6c))
            {
                throw new ArgumentException("发票密文位数不正确");
            }
            string str = this.method_33(string_1);
            return this.method_32(str);
        }

        private byte[] method_32(string string_1)
        {
            byte[] buffer = new byte[string_1.Length / 2];
            for (int i = 0; i < (string_1.Length - 1); i += 2)
            {
                char ch = string_1[i];
                char ch2 = string_1[i + 1];
                buffer[i / 2] = (byte) ((Convert.ToByte(ch.ToString(), 0x10) << 4) + Convert.ToByte(ch2.ToString(), 0x10));
            }
            return buffer;
        }

        private string method_33(string string_1)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char ch in string_1)
            {
                if ((ch >= '0') && (ch <= '9'))
                {
                    builder.Append(ch);
                }
                else
                {
                    builder.Append((char) (0x41 + "+-*/<>".IndexOf(ch)));
                }
            }
            return builder.ToString();
        }

        private byte[] method_34(byte[] byte_2, Fpxx fpxx_1, ushort ushort_1)
        {
            if (ushort_1 == 0)
            {
                throw new ArgumentException("发票信息加密参数不正确");
            }
            byte[] sourceArray = new byte[] { (byte) (ushort_1 >> 8), (byte) ushort_1 };
            byte[] destinationArray = new byte[(fpxx_1.fpdm.Length + 8) + 2];
            byte[] bytes = Encoding.ASCII.GetBytes(fpxx_1.fpdm + string.Format("{0:D8}", fpxx_1.fphm));
            Array.Copy(bytes, 0, destinationArray, 0, bytes.Length);
            Array.Copy(sourceArray, 0, destinationArray, bytes.Length, sourceArray.Length);
            IDEA_Ctypt.SetCryptoKey(MD5.Create().ComputeHash(destinationArray));
            return IDEA_Ctypt.Crypto(byte_2);
        }

        private void method_35(string string_1, ref byte[] byte_2)
        {
            if ((string_1 != null) && (string_1.Length > 0))
            {
                int length = string_1.Length;
                int num = 1;
                if (length > 0)
                {
                    num += length / 8;
                    if ((length % 8) > 0)
                    {
                        num++;
                    }
                    string str = "";
                    string str2 = "";
                    if (string_1.Length > 8)
                    {
                        str = string_1.Substring(0, 8);
                        str2 = string_1.Substring(8);
                    }
                    else
                    {
                        str = string_1.Substring(0);
                        str2 = "";
                    }
                    int index = 0;
                    index = 1;
                    while (index < num)
                    {
                        if (index < byte_2.Length)
                        {
                            byte_2[index] = 0;
                            int num5 = 0;
                            for (num5 = 0; num5 < str.Length; num5++)
                            {
                                byte num4 = 1;
                                if (str[num5] == '1')
                                {
                                    num4 = (byte) (num4 << (7 - num5));
                                    byte_2[index] = (byte) (byte_2[index] | num4);
                                }
                            }
                            if (str2.Length > 8)
                            {
                                str = str2.Substring(0, 8);
                                str2 = str2.Substring(8);
                            }
                            else
                            {
                                str = str2.Substring(0);
                                str2 = "";
                            }
                        }
                        else
                        {
                            ilog_0.Error("Str2Bytes出现异常，有字段超长");
                        }
                        index++;
                    }
                    byte_2[index] = 0;
                }
            }
        }

        private bool method_36(string string_1)
        {
            if (string_1.Length <= 0)
            {
                ilog_0.Error("校验GBK字符时，传入的参数为空");
                return false;
            }
            int index = 0;
            byte[] bytes = ToolUtil.GetBytes(string_1);
            if (bytes == null)
            {
            }
            int length = bytes.Length;
            while (index < length)
            {
                if (bytes[index] > 0x80)
                {
                    byte num2 = bytes[index];
                    index++;
                    if (index > length)
                    {
                        ilog_0.Error("校验GBK字符时，越界");
                        return false;
                    }
                    byte num = bytes[index];
                    if ((num2 >= 0x81) && (num2 < 0xb0))
                    {
                        if (((num > 0xfe) || (num == 0x7f)) || (num < 0x40))
                        {
                            ilog_0.Error("校验GBK字符时有非法字符： " + smethod_2(string_1, index, 6));
                            return false;
                        }
                        switch (num2)
                        {
                            case 0xa1:
                                if (num > 0xa1)
                                {
                                    goto Label_0352;
                                }
                                ilog_0.Error("校验GBK字符时有非法字符： " + smethod_2(string_1, index, 6));
                                return false;

                            case 0xa2:
                                if (((((num >= 0x40) && ((num < 0xab) || (num > 0xb0))) && ((num < 0xe3) || (num > 0xe4))) && ((num < 0xef) || (num > 240))) && ((num < 0xfd) || (num > 0xfe)))
                                {
                                    goto Label_0352;
                                }
                                ilog_0.Error("校验GBK字符时有非法字符： " + smethod_2(string_1, index, 6));
                                return false;

                            case 0xa3:
                                if ((num <= 0xfe) && (num >= 0x40))
                                {
                                    goto Label_0352;
                                }
                                ilog_0.Error("校验GBK字符时有非法字符： " + smethod_2(string_1, index, 6));
                                return false;

                            case 0xa4:
                                if ((num < 0xf4) && (num >= 0x40))
                                {
                                    goto Label_0352;
                                }
                                ilog_0.Error("校验GBK字符时有非法字符： " + smethod_2(string_1, index, 6));
                                return false;

                            case 0xa5:
                                if ((num < 0xf7) && (num >= 0x40))
                                {
                                    goto Label_0352;
                                }
                                ilog_0.Error("校验GBK字符时有非法字符： " + smethod_2(string_1, index, 6));
                                return false;

                            case 0xa6:
                                if ((((num >= 0x40) && ((num < 0xb9) || (num > 0xc0))) && ((num < 0xd9) || (num > 0xdf))) && ((num != 0xf3) && ((num < 0xf6) || (num > 0xfe))))
                                {
                                    goto Label_0352;
                                }
                                ilog_0.Error("校验GBK字符时有非法字符： " + smethod_2(string_1, index, 6));
                                return false;

                            case 0xa7:
                                if (((num >= 0x40) && ((num < 0xc2) || (num > 0xd0))) && ((num < 0xf2) || (num > 0xfe)))
                                {
                                    goto Label_0352;
                                }
                                ilog_0.Error("校验GBK字符时有非法字符： " + smethod_2(string_1, index, 6));
                                return false;

                            case 0xa8:
                                if ((((num >= 0x40) && ((num < 150) || (num > 160))) && ((num < 0xc1) || (num > 0xc4))) && ((num < 0xea) || (num > 0xfe)))
                                {
                                    goto Label_0352;
                                }
                                ilog_0.Error("校验GBK字符时有非法字符： " + smethod_2(string_1, index, 6));
                                return false;

                            case 0xa9:
                                if (((((num >= 0x40) && (num != 0x58)) && ((num < 0x5d) || (num > 0x5f))) && ((num < 0x97) || (num > 0xa3))) && ((num < 0xf5) || (num > 0xfe)))
                                {
                                    goto Label_0352;
                                }
                                ilog_0.Error("校验GBK字符时有非法字符： " + smethod_2(string_1, index, 6));
                                return false;
                        }
                        if (((num2 >= 170) && (num2 <= 0xaf)) && ((num < 0x40) || (num > 160)))
                        {
                            ilog_0.Error("校验GBK字符时有非法字符： " + smethod_2(string_1, index, 6));
                            return false;
                        }
                    }
                    else if ((num2 >= 0xb0) && (num2 <= 0xf7))
                    {
                        if (((num > 0xfe) || (num == 0x7f)) || (num < 0x40))
                        {
                            ilog_0.Error("校验GBK字符时有非法字符： " + smethod_2(string_1, index, 6));
                            return false;
                        }
                    }
                    else if (num2 > 0xf7)
                    {
                        if (num2 >= 0xff)
                        {
                            ilog_0.Error("校验GBK字符时有非法字符： " + smethod_2(string_1, index, 6));
                            return false;
                        }
                        if (((num > 160) || (num == 0x7f)) || (num < 0x40))
                        {
                            ilog_0.Error("校验GBK字符时有非法字符： " + smethod_2(string_1, index, 6));
                            return false;
                        }
                    }
                }
                else
                {
                    byte num5 = bytes[index];
                    if (((num5 < 0x20) || (num5 > 0x7e)) && ((num5 != 10) && (num5 != 9)))
                    {
                        ilog_0.Error("校验GBK字符时有非法字符： " + smethod_2(string_1, index, 6));
                        return false;
                    }
                }
            Label_0352:
                index++;
            }
            return true;
        }

        private string method_37(Fpxx fpxx_1)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("0");
            if (fpxx_1.hzfw && ((fpxx_1.fplx == FPLX.ZYFP) || (fpxx_1.fplx == FPLX.PTFP)))
            {
                builder.Append("1");
            }
            else
            {
                builder.Append("0");
            }
            string str = "0";
            if ((fpxx_1.fplx == FPLX.ZYFP) && (fpxx_1.Zyfplx == ZYFP_LX.XT_YCL))
            {
                str = "1";
            }
            else if ((fpxx_1.fplx == FPLX.ZYFP) && (fpxx_1.Zyfplx == ZYFP_LX.XT_CCP))
            {
                str = "2";
            }
            else if ((fpxx_1.fplx == FPLX.ZYFP) && (((fpxx_1.Zyfplx == ZYFP_LX.SNY) || (fpxx_1.Zyfplx == ZYFP_LX.SNY_DDZG)) || ((fpxx_1.Zyfplx == ZYFP_LX.RLY) || (fpxx_1.Zyfplx == ZYFP_LX.RLY_DDZG))))
            {
                str = "3";
            }
            builder.Append(str);
            builder.Append("0");
            if (fpxx_1.fplx == FPLX.JDCFP)
            {
                builder.Append(fpxx_1.isNewJdcfp ? "2" : "1");
            }
            else
            {
                builder.Append("0");
            }
            if ((fpxx_1.fplx == FPLX.PTFP) && (fpxx_1.Zyfplx == ZYFP_LX.NCP_XS))
            {
                builder.Append("1");
            }
            else if ((fpxx_1.fplx == FPLX.PTFP) && (fpxx_1.Zyfplx == ZYFP_LX.NCP_SG))
            {
                builder.Append("2");
            }
            else
            {
                builder.Append("0");
            }
            if (fpxx_1.fplx == FPLX.JSFP)
            {
                int num = 0;
                Dictionary<string, int> jsPrintTemplate = ToolUtil.GetJsPrintTemplate();
                if ((jsPrintTemplate.Count > 0) && jsPrintTemplate.ContainsKey(fpxx_1.dy_mb))
                {
                    num = jsPrintTemplate[fpxx_1.dy_mb];
                }
                string str2 = (num + 1).ToString("X2");
                builder.Append(str2);
            }
            else
            {
                builder.Append("00");
            }
            if (((fpxx_1.fplx == FPLX.ZYFP) || (fpxx_1.fplx == FPLX.PTFP)) && (fpxx_1.Zyfplx == ZYFP_LX.CEZS))
            {
                builder.Append("2");
            }
            else if ((!string.IsNullOrEmpty(fpxx_1.sLv) && Class34.smethod_10("0.05", fpxx_1.sLv)) && ((fpxx_1.fplx == FPLX.ZYFP) && (fpxx_1.Zyfplx != ZYFP_LX.HYSY)))
            {
                builder.Append("1");
            }
            else
            {
                builder.Append("0");
            }
            builder.Append('0', 1);
            return builder.ToString();
        }

        private bool method_4(string string_1)
        {
            if (string.IsNullOrEmpty(string_1))
            {
                string_1 = string.Empty;
            }
            if (string_1.Length != 6)
            {
                return false;
            }
            for (int i = 0; i < string_1.Length; i++)
            {
                if ("0123456789".IndexOf(string_1[i]) < 0)
                {
                    return false;
                }
            }
            return true;
        }

        private bool method_5(string string_1)
        {
            if (string.IsNullOrEmpty(string_1))
            {
                string_1 = string.Empty;
            }
            if ((string_1.Length < 0x10) || (string_1.Length > 0x12))
            {
                return false;
            }
            string str = string_1.Substring(0, 6);
            if (!this.method_4(str))
            {
                return false;
            }
            string str2 = string_1.Substring(6);
            return this.method_3(str2);
        }

        private bool method_6(string string_1)
        {
            if (string.IsNullOrEmpty(string_1))
            {
                string_1 = string.Empty;
            }
            if ((string_1.Length < 0x10) || (string_1.Length > 0x12))
            {
                return false;
            }
            string str = string_1.Substring(0, 1);
            if (!this.method_2(str[0]))
            {
                return false;
            }
            string str3 = string_1.Substring(1, 6);
            if (!this.method_4(str3))
            {
                return false;
            }
            string str2 = string_1.Substring(7);
            return this.method_3(str2);
        }

        private bool method_7(string string_1)
        {
            if (string.IsNullOrEmpty(string_1))
            {
                string_1 = string.Empty;
            }
            if ((string_1.Length < 0x10) || (string_1.Length > 0x12))
            {
                return false;
            }
            string str = string_1.Substring(0, 2);
            if (!this.method_2(str[0]) || !this.method_2(str[1]))
            {
                return false;
            }
            string str3 = string_1.Substring(2, 6);
            if (!this.method_4(str3))
            {
                return false;
            }
            string str2 = string_1.Substring(8);
            return this.method_3(str2);
        }

        private bool method_8(string string_1)
        {
            if (string.IsNullOrEmpty(string_1))
            {
                string_1 = string.Empty;
            }
            if (string_1.Length != 15)
            {
                return false;
            }
            for (int i = 0; i < string_1.Length; i++)
            {
                if ("0123456789".IndexOf(string_1[i]) < 0)
                {
                    return false;
                }
            }
            return true;
        }

        private bool method_9(string string_1)
        {
            if (string.IsNullOrEmpty(string_1))
            {
                string_1 = string.Empty;
            }
            if (string_1.Length != 0x12)
            {
                return false;
            }
            for (int i = 0; i < (string_1.Length - 1); i++)
            {
                if ("0123456789".IndexOf(string_1[i]) < 0)
                {
                    return false;
                }
            }
            return this.method_2(string_1[0x11]);
        }

        public static decimal ObjectToDecimal(object object_0)
        {
            if (object_0 == null)
            {
                return 0.0M;
            }
            decimal result = 0.0M;
            decimal.TryParse(object_0.ToString().Trim(), out result);
            return result;
        }

        private static void smethod_0()
        {
            InvoiceHandler handler = new InvoiceHandler();
            Fpxx fpxx = new Fpxx {
                bz = "备注",
                fhr = "复核人",
                fplx = FPLX.ZYFP,
                fpdm = "1234567890",
                fphm = "8",
                kprq = "2014-8-7",
                gfdzdh = "购方地址电话",
                gfmc = "北京畅联电子有限公司北京畅联电子有限公司北京畅联电子有限公司北京畅联电子有限公司北京畅联电子有限公司",
                gfsh = "110101010101001",
                gfyhzh = "购方银行 1234567890123456789",
                hzfw = true,
                je = "449.40",
                kpr = "开票人",
                se = "76.40",
                skr = "收款人",
                sLv = "0.17",
                xfmc = "",
                xfdzdh = "销方地址电话",
                xfyhzh = "销方银行 1234567890123456789",
                Zyfplx = ZYFP_LX.ZYFP
            };
            List<Dictionary<SPXX, string>> list = new List<Dictionary<SPXX, string>>();
            Dictionary<SPXX, string> item = new Dictionary<SPXX, string>(11);
            item[SPXX.SPMC] = "北京畅联电子有限公司北京畅联电子有限公司";
            item[SPXX.JLDW] = "12345";
            item[SPXX.GGXH] = "";
            item[SPXX.SL] = "1.1234567";
            item[SPXX.DJ] = "100.00";
            item[SPXX.JE] = "112.35";
            item[SPXX.SLV] = "0.17";
            item[SPXX.SE] = "19.10";
            item[SPXX.HSJBZ] = "0";
            item[SPXX.SPSM] = "";
            item[SPXX.FPHXZ] = "0";
            list.Add(item);
            item = new Dictionary<SPXX, string>(11);
            item[SPXX.SPMC] = "北京畅联电子有限公司北京畅联电子有限公司";
            item[SPXX.JLDW] = "12";
            item[SPXX.GGXH] = "";
            item[SPXX.SL] = "1.1234567";
            item[SPXX.DJ] = "100.00";
            item[SPXX.JE] = "112.35";
            item[SPXX.SLV] = "0.17";
            item[SPXX.SE] = "19.10";
            item[SPXX.HSJBZ] = "0";
            item[SPXX.SPSM] = "";
            item[SPXX.FPHXZ] = "0";
            list.Add(item);
            item = new Dictionary<SPXX, string>(11);
            item[SPXX.SPMC] = "北京畅联电子有限公司北京畅联电子有限公司";
            item[SPXX.JLDW] = "";
            item[SPXX.GGXH] = "";
            item[SPXX.SL] = "1.1234567";
            item[SPXX.DJ] = "100.00";
            item[SPXX.JE] = "112.35";
            item[SPXX.SLV] = "0.17";
            item[SPXX.SE] = "19.10";
            item[SPXX.HSJBZ] = "0";
            item[SPXX.SPSM] = "";
            item[SPXX.FPHXZ] = "0";
            list.Add(item);
            item = new Dictionary<SPXX, string>(11);
            item[SPXX.SPMC] = "北京畅联电子有限公司北京畅联";
            item[SPXX.JLDW] = "";
            item[SPXX.GGXH] = "";
            item[SPXX.SL] = "1.1234567";
            item[SPXX.DJ] = "100.00";
            item[SPXX.JE] = "112.35";
            item[SPXX.SLV] = "0.17";
            item[SPXX.SE] = "19.10";
            item[SPXX.HSJBZ] = "0";
            item[SPXX.SPSM] = "";
            item[SPXX.FPHXZ] = "0";
            list.Add(item);
            fpxx.Mxxx = list;
            fpxx.Qdxx = null;
            string str = handler.method_23(fpxx);
            Console.WriteLine("待签名原始数据：");
            Console.WriteLine(str);
        }

        private static decimal smethod_1(decimal decimal_0, double double_0)
        {
            decimal num = (decimal_0 < 0M) ? -0.5M : 0.5M;
            decimal num2 = Convert.ToDecimal(Math.Pow(10.0, double_0));
            return (decimal.Truncate((decimal_0 * num2) + num) / num2);
        }

        private static string smethod_2(string string_1, int int_0, int int_1 = 6)
        {
            string str = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(string_1))
                {
                    return str;
                }
                StringBuilder builder = new StringBuilder();
                foreach (char ch in string_1)
                {
                    if (ToolUtil.GetByteCount(ch.ToString()) == 2)
                    {
                        builder.Append("AB");
                    }
                    else
                    {
                        builder.Append("C");
                    }
                }
                string str3 = builder.ToString();
                byte[] bytes = ToolUtil.GetBytes(string_1);
                int startIndex = 0;
                int length = bytes.Length;
                if ((int_0 - int_1) < 0)
                {
                    startIndex = 0;
                }
                else
                {
                    startIndex = int_0 - int_1;
                }
                if ((int_0 + int_1) > bytes.Length)
                {
                    length = bytes.Length;
                }
                else
                {
                    length = int_0 + int_1;
                }
                if (str3.Substring(startIndex, length - startIndex).StartsWith("B"))
                {
                    startIndex--;
                }
                if (str3.Substring(startIndex, length - startIndex).EndsWith("A"))
                {
                    length++;
                }
                str = ToolUtil.GetString(bytes, startIndex, length - startIndex);
            }
            catch (Exception)
            {
            }
            return str;
        }
    }
}

