namespace Aisino.Fwkp.Fptk
{
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fptk.Common.Forms;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class InvoiceHelper
    {
        public bool CheckRedJe(string blueJe, Invoice _fpxx)
        {
            decimal num;
            if ((blueJe != "") && decimal.TryParse(blueJe, out num))
            {
                decimal num2;
                decimal.TryParse(_fpxx.GetHjJeNotHs(), out num2);
                decimal num3 = Math.Abs(num2);
                decimal totalRedJe = new FpManager().GetTotalRedJe(_fpxx.BlueFpdm, _fpxx.BlueFphm);
                decimal num5 = decimal.Add(num, totalRedJe);
                if ((num5 <= decimal.Zero) || (num3.CompareTo(Math.Abs(num5)) > 0))
                {
                    string[] textArray1 = new string[] { num5.ToString("F2"), num2.ToString() };
                    MessageManager.ShowMsgBox("INP-242118", textArray1);
                    return false;
                }
            }
            return true;
        }

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

        public SLV GetSLv(string value, int valueType, FPLX fplx, string slvStr)
        {
            decimal num4;
            List<SLV> slvList = this.GetSlvList(fplx, slvStr);
            for (int i = 0; i < slvList.Count; i++)
            {
                decimal num2;
                decimal num3;
                SLV slv = slvList[i];
                if (((valueType == 0) && decimal.TryParse(value, out num2)) && (decimal.TryParse(slv.DataValue, out num3) && (decimal.Compare(num3, num2) == 0)))
                {
                    return slv;
                }
                if ((valueType == 1) && slv.ShowValue.Equals(value))
                {
                    return slv;
                }
            }
            if (valueType != 0)
            {
                return null;
            }
            if (decimal.TryParse(value, out num4))
            {
                string str = (value.Length == 0) ? "" : (decimal.Round(decimal.Multiply(decimal.Parse("100.0"), num4)).ToString() + "%");
                return new SLV(fplx, 0, (value.Length == 0) ? "" : decimal.Round(num4, 2).ToString(), str, str);
            }
            return new SLV(fplx, 0, value, value, value);
        }

        public string GetSLValue(string text)
        {
            if (text.EndsWith("%") && (text != "0%"))
            {
                decimal num;
                if (decimal.TryParse(text.Substring(0, text.Length - 1), out num))
                {
                    text = decimal.Divide(num, decimal.Parse("100")).ToString();
                }
                return text;
            }
            if ((!(text == "0%") && !(text == "免税")) && !(text == "不征税"))
            {
                return text;
            }
            return "0.00";
        }

        public List<SLV> GetSlvList(FPLX fplx, string slvStr)
        {
            List<SLV> list = new List<SLV>();
            if (!string.IsNullOrEmpty(slvStr))
            {
                char[] separator = new char[] { ',' };
                List<string> list1 = new List<string>(slvStr.Split(separator, StringSplitOptions.None).GroupBy<string, string>((serializeClass.staticFunc_13)).Select<IGrouping<string, string>, string>((serializeClass.staticFunc_14)).ToArray<string>());
                list1.Sort(new Comparison<string>(InvoiceHelper.CompareSlvDesc));
                foreach (string str in list1)
                {
                    double num;
                    if (double.TryParse(str, out num))
                    {
                        if (num == 0.0)
                        {
                            list.Add(new SLV(fplx, 0, "0.00", "0%", "0%"));
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

        [Serializable, CompilerGenerated]
        private sealed class serializeClass
        {
            public static readonly InvoiceHelper.serializeClass instance = new InvoiceHelper.serializeClass();
            public static Func<string, string> staticFunc_13;
            public static Func<IGrouping<string, string>, string> staticFunc_14;

            internal string slvlistFunc_13(string p)
            {
                return p;
            }

            internal string slvlistFunc_14(IGrouping<string, string> p)
            {
                return p.Key;
            }
        }
    }
}

