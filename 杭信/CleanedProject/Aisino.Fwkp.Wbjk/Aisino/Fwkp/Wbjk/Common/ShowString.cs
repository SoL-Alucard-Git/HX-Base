namespace Aisino.Fwkp.Wbjk.Common
{
    using System;

    internal class ShowString
    {
        internal static string ShowBool(object objBool)
        {
            string str = Convert.ToString(objBool).ToLower();
            string str4 = str;
            if (str4 == null)
            {
                return str;
            }
            if (!(str4 == "0") && !(str4 == "false"))
            {
                if ((str4 == "1") || (str4 == "true"))
                {
                    return "是";
                }
                return str;
            }
            return "否";
        }

        internal static string ShowDJZT(string DJZT)
        {
            string str3 = DJZT;
            if (str3 == null)
            {
                return DJZT;
            }
            if (!(str3 == "Y"))
            {
                if (str3 == "W")
                {
                    return "W:已作废";
                }
                if (str3 == "N")
                {
                    return "N:数据错误";
                }
                return DJZT;
            }
            return "";
        }

        internal static string ShowFPZL(string FPZL)
        {
            string str3 = FPZL;
            if (str3 == null)
            {
                return FPZL;
            }
            if (!(str3 == "s"))
            {
                if (str3 == "c")
                {
                    return "增值税普通发票";
                }
                if (str3 == "f")
                {
                    return "货物运输业增值税专用发票";
                }
                if (str3 == "j")
                {
                    return "机动车销售统一发票";
                }
                return FPZL;
            }
            return "增值税专用发票";
        }

        internal static string ShowKPZT(string KPZT)
        {
            string str3 = KPZT;
            if (str3 == null)
            {
                return KPZT;
            }
            if (!(str3 == "N"))
            {
                if (str3 == "A")
                {
                    return "A:已开票";
                }
                if (str3 == "P")
                {
                    return "P:部分开票";
                }
                return KPZT;
            }
            return "N:未开票";
        }
    }
}

