namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using log4net;
    using System;

    public class Tool
    {
        private static ZYFP_LX _zyfplx;
        public static string DZFP = "电子增值税普通发票";
        public static string JSFP = "增值税普通发票(卷票)";
        private static ILog loger = LogUtil.GetLogger<Tool>();
        private static string strFpzl = "s";

        public static int CharNumInString(string str, char ch)
        {
            int num = 0;
            if ((str != null) && (str.Length != 0))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == ch)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        public static string FromBase64(string Base64String)
        {
            try
            {
                return ToolUtil.GetString(Convert.FromBase64String(Base64String));
            }
            catch (Exception)
            {
                return Base64String;
            }
        }

        public static string GetDBBSZT(string bszt)
        {
            switch (bszt)
            {
                case "未报送":
                    return "0";

                case "已报送":
                    return "1";

                case "报送失败":
                    return "2";

                case "报送中":
                    return "3";

                case "验签失败":
                    return "4";
            }
            return "-1";
        }

        public static string GetDBPZZT(string pzzt)
        {
            switch (pzzt)
            {
                case "未审核":
                    return "0";

                case "已审核":
                    return "1";

                case "已记账":
                    return "2";

                case "暂存":
                    return "3";
            }
            return "";
        }

        public static string GetDBSlv(string slv)
        {
            if ((slv != null) && (slv != "多税率"))
            {
                if (slv == "免税")
                {
                    return "0.0";
                }
                if (slv == "")
                {
                    return "0.05";
                }
                if (slv == "减按1.5%计算")
                {
                    return "0.015";
                }
                if (slv.IndexOf("%") != -1)
                {
                    slv = slv.Substring(0, slv.Length - 1);
                    float num = float.Parse(slv) / 100f;
                    return num.ToString();
                }
            }
            return "";
        }

        public static string getDymb(string yysbz)
        {
            int num = Convert.ToInt32(yysbz.Substring(6, 2), 0x10) - 1;
            switch (((PrintTemplate) num))
            {
                case (PrintTemplate)0:
                    return "NEW76mmX177mm";

                case (PrintTemplate)1:
                    return "NEW76mmX152mm";

                case (PrintTemplate)2:
                    return "NEW76mmX127mm";

                case (PrintTemplate)3:
                    return "NEW57mmX177mm";

                case (PrintTemplate)4:
                    return "NEW57mmX152mm";

                case (PrintTemplate)5:
                    return "NEW57mmX127mm";

                case (PrintTemplate)6:
                    return "SH76mmX177mm";

                case (PrintTemplate)7:
                    return "HLJ76mmX177mm";

                case (PrintTemplate)8:
                    return "BJ76mmX177mm";

                case (PrintTemplate)9:
                    return "YN76mmX127mm";

                case (PrintTemplate)10:
                    return "YN76mmX177mm";
            }
            return "NEW76mmX177mm";
        }

        public static string GetFPDBType(string fplb)
        {
            switch (fplb)
            {
                case "普通发票":
                case "收购发票":
                    return "c";

                case "专用发票":
                    return "s";

                case "货物运输业增值税专用发票":
                    return "f";

                case "机动车销售统一发票":
                    return "j";

                case "电子增值税普通发票":
                    return "p";

                case "增值税普通发票(卷票)":
                    return "q";
            }
            return "";
        }

        public static string GetFPType(string fplb)
        {
            switch (fplb)
            {
                case "c":
                    return "普通发票";

                case "s":
                    return "专用发票";

                case "f":
                    return "货物运输业增值税专用发票";

                case "j":
                    return "机动车销售统一发票";

                case "p":
                    return DZFP;

                case "q":
                    return JSFP;
            }
            return "全部发票";
        }

        public static int GetReturnErrCode(string errcode)
        {
            int num = -1;
            if (!string.IsNullOrWhiteSpace(errcode) && errcode.Contains("_"))
            {
                num = int.Parse(errcode.Split(new char[] { '_' })[1]);
            }
            return num;
        }

        public static bool IsShuiWuDKSQ()
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if (TaxCardFactory.CreateTaxCard().StateInfo.CompanyType == 0)
            {
                string str = card.TaxCode;
                if ((str != null) && (str.Length == 15))
                {
                    char ch = str[8];
                    if (string.Compare(ch.ToString(), "D", true) == 0)
                    {
                        char ch2 = str[9];
                        if (string.Compare(ch2.ToString(), "K", true) == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static int ObjectIsNULL(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            if (obj.ToString() == "")
            {
                return 2;
            }
            return 0;
        }

        public static bool ObjectToBool(object obj)
        {
            if ((obj == null) || (obj is DBNull))
            {
                return false;
            }
            string str = obj.ToString().Trim();
            return (((str == "1") || (str.ToLower() == "true")) || str.Equals("是"));
        }

        public static DateTime ObjectToDateTime(object data)
        {
            if ((data == null) || (data is DBNull))
            {
                return DateTime.Now;
            }
            DateTime now = DateTime.Now;
            DateTime.TryParse(data.ToString().Trim(), out now);
            return now;
        }

        public static decimal ObjectToDecimal(object data)
        {
            if ((data == null) || (data is DBNull))
            {
                return 0M;
            }
            decimal result = 0M;
            decimal.TryParse(data.ToString().Trim(), out result);
            return result;
        }

        public static double ObjectToDouble(object data)
        {
            if ((data == null) || (data is DBNull))
            {
                return 0.0;
            }
            double result = 0.0;
            double.TryParse(data.ToString().Trim(), out result);
            return result;
        }

        public static int ObjectToInt(object data)
        {
            if ((data == null) || (data is DBNull))
            {
                return 0;
            }
            int result = 0;
            int.TryParse(data.ToString().Trim(), out result);
            return result;
        }

        public static string PareFpType(FPLX fplx)
        {
            FPLX fplx2 = fplx;
            if ((int)fplx2 <= 12)
            {
                switch (fplx2)
                {
                    case 0:
                        return "s";

                    case (FPLX)2:
                        return "c";

                    case (FPLX)11:
                        return "f";

                    case (FPLX)12:
                        return "j";
                }
            }
            else
            {
                if (fplx2 == (FPLX)0x29)
                {
                    return "q";
                }
                if (fplx2 == (FPLX)0x33)
                {
                    return "p";
                }
            }
            return "s";
        }

        public static string ToBase64(string text)
        {
            return Convert.ToBase64String(ToolUtil.GetBytes(text));
        }

        public static bool YiDaoShuoShiQi_JinShuiCard(TaxCard taxcard, TaxStateInfo taxStateInfo)
        {
            try
            {
                if (taxStateInfo.IsLockReached != 0)
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                loger.Error("YiDaoShuoShiQi_JinShuiCard函数异常" + exception.Message);
                return false;
            }
            return true;
        }

        public static string FPZL
        {
            get
            {
                return strFpzl.Trim();
            }
            set
            {
                strFpzl = value.Trim();
            }
        }

        public static ZYFP_LX ZYFPZL
        {
            get
            {
                return _zyfplx;
            }
            set
            {
                _zyfplx = value;
            }
        }
    }
}

