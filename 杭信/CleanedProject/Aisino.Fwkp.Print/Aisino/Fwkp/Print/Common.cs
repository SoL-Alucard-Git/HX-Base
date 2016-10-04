namespace Aisino.Fwkp.Print
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls.PrintControl;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Text;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class Common
    {
        [DecimalConstant(0, 0, (uint) 0, (uint) 0, (uint) 0)]
        private static readonly decimal decimal_0;

        static Common()
        {
            
        }

        public Common()
        {
            
        }

        public static string[] CheckInstalledFont()
        {
            InstalledFontCollection fonts = new InstalledFontCollection();
            FontFamily[] families = fonts.Families;
            int length = families.Length;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            string str = "";
            string str2 = "";
            for (int i = 0; i < length; i++)
            {
                str2 = str2 + families[i].Name + ",";
                if ((families[i].Name == "黑体") || (families[i].Name == "SimHei"))
                {
                    num3 = 1;
                }
                if ((families[i].Name == "宋体") || (families[i].Name == "SimSun"))
                {
                    num4 = 1;
                }
                if (families[i].Name == "Courier New")
                {
                    num5 = 1;
                }
                if (families[i].Name == "OCR A Extended")
                {
                    num6 = 1;
                }
            }
            num2 = ((num3 + num4) + num5) + num6;
            if (num2 < 4)
            {
                if (num3 == 0)
                {
                    str = str + "黑体，";
                }
                if (num4 == 0)
                {
                    str = str + "宋体，";
                }
                if (num5 == 0)
                {
                    str = str + "Courier New，";
                }
                if (num6 == 0)
                {
                    str = str + "OCR A Extended，";
                }
                str = str.TrimEnd(new char[] { (char)0xff0c });
            }
            else
            {
                str = "";
            }
            return new string[] { str, str2 };
        }

        public static FPLX DBFpzlToCardType(string string_0)
        {
            switch (string_0)
            {
                case "c":
                    return FPLX.PTFP;

                case "s":
                    return FPLX.ZYFP;

                case "f":
                    return FPLX.HYFP;

                case "j":
                    return FPLX.JDCFP;

                case "p":
                    return FPLX.DZFP;

                case "q":
                    return FPLX.JSFP;
            }
            return FPLX.ZYFP;
        }

        public static void Encrypt(FileInfo[] fileInfo_0, string string_0)
        {
            for (int i = 0; i < fileInfo_0.Length; i++)
            {
                FileInfo info = fileInfo_0[i];
                byte[] sourceArray = Convert.FromBase64String(string_0);
                byte[] destinationArray = new byte[0x20];
                Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
                byte[] buffer3 = new byte[0x10];
                Array.Copy(sourceArray, 0x20, buffer3, 0, 0x10);
                FileStream stream = info.OpenRead();
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                byte[] buffer5 = AES_Crypt.Encrypt(buffer, destinationArray, buffer3);
                FileStream stream2 = new FileStream(info.FullName, FileMode.Create);
                stream2.Write(buffer5, 0, buffer5.Length);
                stream2.Close();
                Console.WriteLine(info.FullName);
            }
        }

        public static string FormatString(string string_0)
        {
            if ((string_0 == null) || (string_0.Length == 0))
            {
                return "";
            }
            int index = string_0.IndexOf(".");
            if (index == -1)
            {
                string_0 = string_0 + ".00";
                return string_0;
            }
            if (string_0.Substring(index + 1).Length == 1)
            {
                string_0 = string_0 + "0";
            }
            return string_0;
        }

        public static string GetCRC(string string_0)
        {
            try
            {
                return Convert.ToString(smethod_3(ToolUtil.GetBytes(string_0)), 0x10).ToUpper();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static Fpxx GetHWYSFpxxModel(string string_0, string string_1, string string_2, bool bool_0 = false)
        {
            Fpxx fpxx = new Fpxx(FPLX.HYFP, string_1, string_2) {
                kprq = "2015-01-01 23:59:59",
                cyrmc = "航天信息股份有限公司",
                cyrnsrsbh = "110101222666888"
            };
            if (TaxCardFactory.CreateTaxCard().SubSoftVersion != "Linux")
            {
                fpxx.mw = "jxgbBHPrakY87yPR1ssXLMwcGySbtWkqZX7ZQuQ3Ymb/1rZHAgATgG4zJpsxZOWleQZWvbUF7gRuOfmE5fu6dieJ79ypqQ9S8Stmwlwg8tLjh/teigBPqulKmNzETSpkiIvIskwfi666A/JL90zgXVG8UxZBA8DE98Nh6hBcTB8aJU/JMUM/YFUlApPYiYoziqJTm1R6z0ZMLArWPy7h0g==";
            }
            else
            {
                fpxx.mw = "eogxeuPdZhQ2C4gbsTEGulg4tIIBTTLb2J+YcRz1+Nvl/p9li4PVcJiOwABQseU5okediyaR1jXPnN4oektiw+Nz8GX14k8Kx32i8NHeH8p/nbEjBsYWsTY0gx8tOqgvZ6HIMW0YBDXG9bjoFvg6QDzDb1jZ6DHiWfbmufMSq48w82Smxk/PvKPlkxWADaIpSNKn3/3tOFKZ8WqoTTrfpg==";
            }
            fpxx.hxm = "";
            fpxx.spfmc = "航天信息股份有限公司";
            fpxx.spfnsrsbh = "110101222666888";
            fpxx.shrmc = "航天信息股份有限公司";
            fpxx.shrnsrsbh = "110101222666888";
            fpxx.fhrmc = "航天信息股份有限公司";
            fpxx.fhrnsrsbh = "110101222666888";
            fpxx.qyd = "北京，上海";
            fpxx.yshwxx = "suLK1LTy06HR+cD9";
            if (!bool_0)
            {
                fpxx.Mxxx = new List<Dictionary<SPXX, string>>();
                smethod_0(fpxx.Mxxx);
            }
            else
            {
                fpxx.Qdxx = new List<Dictionary<SPXX, string>>();
                smethod_0(fpxx.Qdxx);
            }
            fpxx.je = "5";
            fpxx.sLv = "0.03";
            fpxx.se = "0.15";
            fpxx.kpjh = 0;
            fpxx.jqbh = "88888888";
            fpxx.czch = "88888888";
            fpxx.ccdw = "8888";
            fpxx.zgswjgmc = "北京税局";
            fpxx.zgswjgdm = "110101";
            fpxx.xsdjbh = "";
            fpxx.jmbbh = "1";
            fpxx.bsq = 1;
            fpxx.jym = "";
            fpxx.isRed = false;
            fpxx.zyspmc = "";
            fpxx.zyspsm = "";
            fpxx.bz = "suLK1LTy06HR+cD9";
            fpxx.kpr = "TEST";
            fpxx.fhr = "TEST";
            fpxx.skr = "TEST";
            fpxx.yysbz = "0000000000";
            fpxx.ssyf = 0x3131d;
            fpxx.zfbz = false;
            fpxx.bsbz = false;
            fpxx.dybz = false;
            fpxx.xfbz = false;
            fpxx.bszt = 1;
            fpxx.sign = "";
            fpxx.zfsj = "";
            fpxx.redNum = "";
            GetYYSBZ(ref fpxx);
            fpxx.blueFpdm = "";
            fpxx.blueFphm = "";
            fpxx.retCode = "";
            return fpxx;
        }

        public static string GetInvoiceType(Fpxx fpxx_0)
        {
            string str = "";
            switch (fpxx_0.fplx)
            {
                case FPLX.ZYFP:
                    str = "专用发票";
                    break;

                case FPLX.PTFP:
                    str = "普通发票";
                    break;

                case FPLX.HYFP:
                    str = "货物运输业增值税专用发票";
                    break;

                case FPLX.JDCFP:
                    str = "机动车销售统一发票";
                    break;

                default:
                    str = "专用发票";
                    break;
            }
            return (str + "代码：");
        }

        public static Fpxx GetJDCFpxxModel(string string_0, string string_1, string string_2)
        {
            FPLX jDCFP = FPLX.JDCFP;
            int index = string_1.IndexOf('_');
            string str = "1";
            if (index == -1)
            {
                return null;
            }
            if ((index + 1) < string_1.Length)
            {
                str = string_1.Substring(index + 1, 1);
            }
            string_1 = string_1.Substring(0, index);
            Fpxx fpxx = new Fpxx(jDCFP, string_1, string_2) {
                kprq = "2015-01-01 23:59:59",
                kpjh = 0,
                jqbh = "88888888"
            };
            if (TaxCardFactory.CreateTaxCard().SubSoftVersion != "Linux")
            {
                fpxx.mw = "uvoNsChvtQUYywcEgcrqYlVdtGUworaQItqCx8KgYo+ZL1ndxGBhQ/S+xwyRmq7UezzDfILYwH+Yiy0nQyY0/ZaM1omXTbs3SfoeFlW28Zg+3p99QEh1GWPO208SlV35foOjF5Flk6OvqseUTnR8CohQOCgRA24Uqw+LhddRbE306ZC8LZw9arK9qvmqJlQ538NWIlWhK7h8a4HwSiSBToJNfGj82QupmdLSyd3kX7GYaaxq8ghTwJoW5yB9Ri4+";
            }
            else
            {
                fpxx.mw = "FUVvO0sdqmIy6KBfe/nthByE009igK6uptaARxunJCKQCVgLttvD1KH9+TIwHG9n0kV2l+9AjmE4i+ETFGKUOkmhf9oLxkqrs+6+z+TbHDIIa3R25T2OX4BRMgZkxMyttqsh0289O0a/EuMurdXW/gt8wd0GZQmuN/JsnzwIbuQ71efHCPROT9bSkuO/3tkgJKc814M8Hpgih1I8WXRbzJsw3PU5VyTuM8XKBtQ2325ZCuY9n/4EKSc2QX7/RUUb";
            }
            fpxx.gfmc = "航天信息股份有限公司";
            fpxx.sfzhm = "999999999";
            fpxx.gfsh = "11010122266888";
            fpxx.cllx = "载货汽车";
            fpxx.cpxh = "福特4X4";
            fpxx.cd = "长安";
            fpxx.hgzh = "TEST";
            fpxx.jkzmsh = "TEST123456";
            fpxx.sjdh = "TEST123456";
            fpxx.fdjhm = "TEST123456";
            fpxx.clsbdh = "TEST123456";
            fpxx.sccjmc = "TEST123456";
            fpxx.xfmc = "航天信息股份有限公司";
            fpxx.xfdh = "010-8889 xxxx";
            fpxx.xfsh = "110101222666888";
            fpxx.xfzh = "TEST123456789";
            fpxx.xfdz = "北京市海淀区杏十口路甲18号航天信息园";
            fpxx.xfyh = "TEST123456";
            fpxx.sLv = "0.03";
            fpxx.se = "0.03";
            fpxx.zgswjgmc = "北京税局";
            fpxx.zgswjgdm = "110101";
            fpxx.je = "1";
            fpxx.dw = "吨";
            fpxx.xcrs = "6";
            fpxx.bz = "TEST";
            fpxx.kpr = "TEST";
            fpxx.fhr = "TEST";
            fpxx.skr = "TEST";
            fpxx.xsdjbh = "";
            fpxx.jmbbh = "1";
            fpxx.bsq = 1;
            fpxx.jym = "";
            fpxx.zfbz = false;
            fpxx.bsbz = false;
            fpxx.dybz = false;
            fpxx.xfbz = false;
            fpxx.redNum = "";
            fpxx.je = "1.03";
            fpxx.isRed = false;
            fpxx.zyspmc = "";
            fpxx.zyspsm = "";
            if (str == "1")
            {
                fpxx.yysbz = "0000100000";
            }
            else
            {
                fpxx.yysbz = "0000200000";
            }
            fpxx.ssyf = 0x3131d;
            fpxx.Qdxx = null;
            fpxx.Mxxx = null;
            fpxx.bszt = 0;
            fpxx.sign = "";
            fpxx.zfsj = "";
            GetYYSBZ(ref fpxx);
            fpxx.retCode = "0000";
            return fpxx;
        }

        public static Fpxx GetJSFpxxModel(string string_0, string string_1, string string_2, bool bool_0 = false)
        {
            Fpxx fpxx = new Fpxx(FPLX.JSFP, string_1, string_2) {
                kprq = "2015-01-01",
                xsdjbh = "",
                kpjh = 0,
                jmbbh = "8888888",
                jqbh = "10013333444",
                bsq = 1,
                jym = "49408202614293064556",
                zfbz = false,
                bsbz = false,
                dybz = false,
                xfbz = false
            };
            if (TaxCardFactory.CreateTaxCard().SubSoftVersion != "Linux")
            {
                fpxx.mw = "y8WilvYWrOfbueJQcblXwhNc4WHIBeDZxajCAyWDJwppj4DSGavb0kjjhgqquB8zwQ2SbW1l+f+3z5QxMOgluzQAP+kIf9gFnCfiumyhS7h2ePocbeY5d+7TNovzO96C6rqLl5cdLZLurdXnZATtKA==";
            }
            else
            {
                fpxx.mw = "4VintTyGyA7Q8jJvBjvPaNWTsT4G6MWvG5WZpYldMi4KB8thUAk49j/JSy5efbQLa/NAD1eGsZDKPNTI1Gsv7RgOGh56MKBfI2OTgASf99BqZrx4oBknnaxMHfMitqZPVQwBZLkDpgVEuzneHrRR3w==";
            }
            fpxx.hxm = "";
            fpxx.hzfw = fpxx.hxm.Length > 0;
            fpxx.redNum = "";
            fpxx.gfmc = "航天信息股份有限公司";
            fpxx.gfsh = "110101222666888";
            fpxx.gfdzdh = "北京市海淀区杏石口路甲18号航天信息园";
            fpxx.gfyhzh = "测试";
            fpxx.xfmc = "航天信息股份有限公司";
            fpxx.xfsh = "110101222666888";
            fpxx.xfdzdh = "TEST123456";
            fpxx.xfyhzh = "TEST123456";
            fpxx.je = "5";
            fpxx.isRed = false;
            fpxx.sLv = "0.03";
            fpxx.se = "0.15";
            fpxx.zyspmc = "";
            fpxx.zyspsm = "";
            fpxx.bz = "suLK1LTy06HR+cD9";
            fpxx.kpr = "TEST";
            fpxx.fhr = "TEST";
            fpxx.skr = "TEST";
            fpxx.yysbz = "0000000700";
            fpxx.ssyf = 0x3131d;
            if (bool_0)
            {
                fpxx.Qdxx = new List<Dictionary<SPXX, string>>();
                smethod_0(fpxx.Qdxx);
            }
            else
            {
                fpxx.Mxxx = new List<Dictionary<SPXX, string>>();
                smethod_0(fpxx.Mxxx);
            }
            fpxx.bszt = 1;
            fpxx.sign = "";
            fpxx.zfsj = "";
            GetYYSBZ(ref fpxx);
            string str = "";
            if (str.Length > 0)
            {
                int index = str.IndexOf('_');
                if (index < 0)
                {
                    index = 0;
                }
                fpxx.blueFpdm = str.Substring(0, index);
                if ((index + 1) > str.Length)
                {
                    index = str.Length - 1;
                }
                fpxx.blueFphm = str.Substring(index + 1);
                return fpxx;
            }
            fpxx.blueFpdm = "";
            fpxx.blueFphm = "";
            return fpxx;
        }

        public static int GetObjectDotLength(object object_0)
        {
            try
            {
                if (object_0 == null)
                {
                    return 0;
                }
                string str = object_0.ToString();
                int index = str.IndexOf('.');
                if (index <= 0)
                {
                    return 0;
                }
                return str.Substring(index + 1).Length;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static Fpxx GetPTZYFpxxModel(string string_0, string string_1, string string_2, bool bool_0 = false)
        {
            FPLX zYFP = FPLX.ZYFP;
            if (string_0 == "c")
            {
                zYFP = FPLX.PTFP;
            }
            Fpxx fpxx = new Fpxx(zYFP, string_1, string_2) {
                kprq = "2015-01-01",
                xsdjbh = "",
                kpjh = 0,
                jmbbh = "8888888",
                jqbh = "10013333444",
                bsq = 1,
                jym = "49408202614293064556",
                zfbz = false,
                bsbz = false,
                dybz = false,
                xfbz = false
            };
            if (TaxCardFactory.CreateTaxCard().SubSoftVersion != "Linux")
            {
                fpxx.mw = "y8WilvYWrOfbueJQcblXwhNc4WHIBeDZxajCAyWDJwppj4DSGavb0kjjhgqquB8zwQ2SbW1l+f+3z5QxMOgluzQAP+kIf9gFnCfiumyhS7h2ePocbeY5d+7TNovzO96C6rqLl5cdLZLurdXnZATtKA==";
            }
            else
            {
                fpxx.mw = "4VintTyGyA7Q8jJvBjvPaNWTsT4G6MWvG5WZpYldMi4KB8thUAk49j/JSy5efbQLa/NAD1eGsZDKPNTI1Gsv7RgOGh56MKBfI2OTgASf99BqZrx4oBknnaxMHfMitqZPVQwBZLkDpgVEuzneHrRR3w==";
            }
            fpxx.hxm = "";
            fpxx.hzfw = fpxx.hxm.Length > 0;
            fpxx.redNum = "";
            fpxx.gfmc = "航天信息股份有限公司";
            fpxx.gfsh = "110101222666888";
            fpxx.gfdzdh = "北京市海淀区杏石口路甲18号航天信息园";
            fpxx.gfyhzh = "测试";
            fpxx.xfmc = "航天信息股份有限公司";
            fpxx.xfsh = "110101222666888";
            fpxx.xfdzdh = "TEST123456";
            fpxx.xfyhzh = "TEST123456";
            fpxx.je = "5";
            fpxx.isRed = false;
            fpxx.sLv = "0.03";
            fpxx.se = "0.15";
            fpxx.zyspmc = "";
            fpxx.zyspsm = "";
            fpxx.bz = "suLK1LTy06HR+cD9";
            fpxx.kpr = "TEST";
            fpxx.fhr = "TEST";
            fpxx.skr = "TEST";
            fpxx.yysbz = "0000000000";
            fpxx.ssyf = 0x3131d;
            if (bool_0)
            {
                fpxx.Qdxx = new List<Dictionary<SPXX, string>>();
                smethod_0(fpxx.Qdxx);
            }
            else
            {
                fpxx.Mxxx = new List<Dictionary<SPXX, string>>();
                smethod_0(fpxx.Mxxx);
            }
            fpxx.bszt = 1;
            fpxx.sign = "";
            fpxx.zfsj = "";
            GetYYSBZ(ref fpxx);
            string str = "";
            if (str.Length > 0)
            {
                int index = str.IndexOf('_');
                if (index < 0)
                {
                    index = 0;
                }
                fpxx.blueFpdm = str.Substring(0, index);
                if ((index + 1) > str.Length)
                {
                    index = str.Length - 1;
                }
                fpxx.blueFphm = str.Substring(index + 1);
                return fpxx;
            }
            fpxx.blueFpdm = "";
            fpxx.blueFphm = "";
            return fpxx;
        }

        public static void GetYYSBZ(ref Fpxx fpxx_0)
        {
            if ((fpxx_0.yysbz != null) && (fpxx_0.yysbz.Length >= 10))
            {
                switch (fpxx_0.yysbz[2])
                {
                    case '1':
                        fpxx_0.Zyfplx = ZYFP_LX.XT_YCL;
                        break;

                    case '2':
                        fpxx_0.Zyfplx = ZYFP_LX.XT_CCP;
                        break;

                    case '3':
                        fpxx_0.Zyfplx = ZYFP_LX.SNY;
                        break;
                }
                char ch2 = fpxx_0.yysbz[4];
                if (ch2 == '1')
                {
                    fpxx_0.isNewJdcfp = false;
                }
                else
                {
                    fpxx_0.isNewJdcfp = true;
                }
                switch (fpxx_0.yysbz[5])
                {
                    case '1':
                        fpxx_0.Zyfplx = ZYFP_LX.NCP_XS;
                        break;

                    case '2':
                        fpxx_0.Zyfplx = ZYFP_LX.NCP_SG;
                        break;
                }
                if ((ObjectToDouble(fpxx_0.je) == 0.0) && fpxx_0.zfbz)
                {
                    fpxx_0.isBlankWaste = true;
                }
                else
                {
                    fpxx_0.isBlankWaste = false;
                }
            }
        }

        public static bool IsFlbm()
        {
            return (TaxCardFactory.CreateTaxCard().GetExtandParams("FLBMFlag") == "FLBM");
        }

        public static bool IsShuiWuDKSQ(string string_0 = "")
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if (card.StateInfo.CompanyType == 0)
            {
                string_0 = card.TaxCode;
                if ((string_0 != null) && (string_0.Length == 15))
                {
                    char ch = string_0[8];
                    if (string.Compare(ch.ToString(), "D", true) == 0)
                    {
                        char ch2 = string_0[9];
                        if (string.Compare(ch2.ToString(), "K", true) == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool ObjectToBool(object object_0)
        {
            if ((object_0 != null) && !(object_0.ToString() == ""))
            {
                bool result = true;
                bool.TryParse(object_0.ToString().Trim(), out result);
                return result;
            }
            return true;
        }

        public static DateTime ObjectToDateTime(object object_0)
        {
            if (object_0 == null)
            {
                return DateTime.Now;
            }
            DateTime now = DateTime.Now;
            DateTime.TryParse(object_0.ToString().Trim(), out now);
            return now;
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

        public static double ObjectToDouble(object object_0)
        {
            if (object_0 == null)
            {
                return 0.0;
            }
            double result = 0.0;
            double.TryParse(object_0.ToString().Trim(), out result);
            return result;
        }

        public static float ObjectToFloat(object object_0)
        {
            if (object_0 == null)
            {
                return 0f;
            }
            float result = 0f;
            float.TryParse(object_0.ToString().Trim(), out result);
            return result;
        }

        public static int ObjectToInt(object object_0)
        {
            if (object_0 == null)
            {
                return 0;
            }
            int result = 0;
            int.TryParse(object_0.ToString().Trim(), out result);
            return result;
        }

        private static void smethod_0(List<Dictionary<SPXX, string>> SpList)
        {
            for (int i = 0; i < 5; i++)
            {
                Dictionary<SPXX, string> item = new Dictionary<SPXX, string>();
                item.Add(SPXX.SPMC, "货物可乐");
                item.Add(SPXX.SPSM, "001");
                item.Add(SPXX.SLV, "0.03");
                item.Add(SPXX.FPHXZ, "1");
                item.Add(SPXX.JE, "12345.89");
                item.Add(SPXX.SE, "0.03");
                item.Add(SPXX.GGXH, "TESTISO");
                item.Add(SPXX.JLDW, "件");
                item.Add(SPXX.SL, "123.34");
                item.Add(SPXX.DJ, "12345.34");
                item.Add(SPXX.HSJBZ, "0");
                item.Add(SPXX.XH, i.ToString());
                SpList.Add(item);
            }
        }

        public static string smethod_1(string string_0)
        {
            try
            {
                if (string_0 == null)
                {
                    return new string('0', 8);
                }
                string_0 = string_0.Trim();
                int result = 0;
                int.TryParse(string_0, out result);
                return smethod_2(result);
            }
            catch (Exception)
            {
                return new string('0', 8);
            }
        }

        public static string smethod_2(int int_0)
        {
            try
            {
                if ((int_0 > 0x5f5e0ff) || (int_0 < 0))
                {
                    int_0 = 0;
                }
                return string.Format("{0:00000000}", int_0);
            }
            catch (Exception)
            {
                return new string('0', 8);
            }
        }

        private static int smethod_3(Array array_0)
        {
            int length = array_0.Length;
            byte[] array = new byte[array_0.Length + 2];
            array_0.CopyTo(array, 0);
            int num2 = 0;
            int num3 = 0;
            num2 = 1;
            byte num5 = array[0];
            num2 = 2;
            byte num6 = array[1];
            array[length] = 0;
            array[length + 1] = 0;
            while (--length >= 0)
            {
                byte num7 = array[num2++];
                for (num3 = 0; num3 < 8; num3++)
                {
                    if (((num5 & 0x80) >> 7) == 1)
                    {
                        num5 = (byte) (num5 << 1);
                        if (((num6 & 0x80) >> 7) == 1)
                        {
                            num5 = (byte) (num5 | 1);
                        }
                        num6 = (byte) (num6 << 1);
                        if (((num7 & 0x80) >> 7) == 1)
                        {
                            num6 = (byte) (num6 | 1);
                        }
                        num7 = (byte) (num7 << 1);
                        num5 = (byte) (num5 ^ 0x80);
                        num6 = (byte) (num6 ^ 5);
                    }
                    else
                    {
                        num5 = (byte) (num5 << 1);
                        if (((num6 & 0x80) >> 7) == 1)
                        {
                            num5 = (byte) (num5 | 1);
                        }
                        num6 = (byte) (num6 << 1);
                        if (((num7 & 0x80) >> 7) == 1)
                        {
                            num6 = (byte) (num6 | 1);
                        }
                        num7 = (byte) (num7 << 1);
                    }
                }
            }
            return ((num5 << 8) + num6);
        }

        public static string smethod_4(string string_0)
        {
            if (string_0 != "")
            {
                switch (ObjectToInt(string_0))
                {
                    case 0:
                    case 3:
                        return "0%";

                    case 1:
                        return "免税";

                    case 2:
                        return "不征税";
                }
            }
            return "";
        }

        public static string[] ZheHang(string string_0, string string_1)
        {
            Aisino.Fwkp.Print.ReadXml xml = Aisino.Fwkp.Print.ReadXml.Get();
            if (xml.ZheHang.ContainsKey(string_1))
            {
                Aisino.Fwkp.Print.PrintZheHangModel model = xml.ZheHang[string_1];
                string configId = model.ConfigId;
                string tempId = model.TempId;
                return ZheHang(string_0, configId, tempId);
            }
            return null;
        }

        public static string[] ZheHang(string string_0, string string_1, string string_2)
        {
            Canvas canvas = new Canvas(Aisino.Fwkp.Print.ReadXml.Get()[string_1].CanvasPath);
            if (canvas != null)
            {
                AisinoPrintLabel label = canvas[string_2] as AisinoPrintLabel;
                if (label != null)
                {
                    return label.ZheHang(string_0);
                }
            }
            return null;
        }
    }
}

