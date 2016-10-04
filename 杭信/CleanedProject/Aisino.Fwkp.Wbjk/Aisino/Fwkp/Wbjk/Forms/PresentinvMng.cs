namespace Aisino.Fwkp.Wbjk.Forms
{
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fptk.Common.Forms;
    using Aisino.Fwkp.Wbjk.Common;
    using System;
    using System.Collections.Generic;

    public sealed class PresentinvMng
    {
        private static int CompareSlvDesc(string x, string y)
        {
            double num;
            double num2;
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                return 1;
            }
            if (y == null)
            {
                return -1;
            }
            double.TryParse(x, out num);
            double.TryParse(y, out num2);
            return num2.CompareTo(num);
        }

        public static string GetLSLVBSByYHZCMC(string YHZCMC)
        {
            string str2 = "出口零税";
            string str3 = "免税";
            string str4 = "不征税";
            if (YHZCMC.Contains(str2))
            {
                return "0";
            }
            if (YHZCMC.Contains(str3))
            {
                return "1";
            }
            if (YHZCMC.Contains(str4))
            {
                return "2";
            }
            return "3";
        }

        public static string GetSLValue(string text)
        {
            string str = "出口零税";
            string str2 = "免税";
            string str3 = "不征税";
            if (text.EndsWith("%"))
            {
                decimal num;
                if (decimal.TryParse(text.Substring(0, text.Length - 1), out num))
                {
                    text = decimal.Divide(num, decimal.Parse("100")).ToString();
                }
                return text;
            }
            if (text == "免税")
            {
                return "0.00";
            }
            if (text == str)
            {
                return "0.00";
            }
            if (text == str2)
            {
                return "0.00";
            }
            if (text == str3)
            {
                return "0.00";
            }
            return text;
        }

        public static List<SLV> GetSlvList(FPLX fplx, string slvStr)
        {
            slvStr = slvStr + ",";
            List<SLV> list = new List<SLV>();
            if (!string.IsNullOrEmpty(slvStr))
            {
                List<string> list2 = new List<string>(slvStr.Split(new char[] { ',' }, StringSplitOptions.None));
                list2.Sort(new Comparison<string>(PresentinvMng.CompareSlvDesc));
                foreach (string str in list2)
                {
                    double num;
                    if (double.TryParse(str, out num))
                    {
                        if (num == 0.0)
                        {
                            if (CommonTool.isSPBMVersion())
                            {
                                list.Add(new SLV(fplx, 0, "0.00", "0%", "0%"));
                            }
                            else
                            {
                                list.Add(new SLV(fplx, 0, "0.00", "免税", "免税"));
                            }
                        }
                        else
                        {
                            string str2 = ((num * 100.0)).ToString() + "%";
                            list.Add(new SLV(fplx, 0, str, str2, str2));
                        }
                    }
                }
            }
            return list;
        }
    }
}

