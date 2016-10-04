namespace Aisino.Fwkp.Wbjk.Common
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.BLL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    internal class CommonTool
    {
        public static bool AddBlankColumns(DataTable table, int count)
        {
            try
            {
                if (table == null)
                {
                    return false;
                }
                int num = table.Columns.Count;
                int num2 = table.Columns.Count + count;
                for (int i = num; i < num2; i++)
                {
                    DataColumn column = new DataColumn(i.ToString(), Type.GetType("System.String"));
                    table.Columns.Add(column);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static double GetDisCount(string ZKMC, ref int ZKH)
        {
            if (ZKMC.Contains("行数"))
            {
                string s = ZKMC.Remove(ZKMC.IndexOf('(')).Remove(0, 4);
                ZKH = int.Parse(s);
            }
            else
            {
                ZKH = 1;
            }
            string str3 = ZKMC.Substring(ZKMC.IndexOf('(') + 1);
            double num = Convert.ToDouble(str3.Remove(str3.IndexOf('%'))) / 100.0;
            return SaleBillCtrl.GetRound(num, 5);
        }

        internal static double GetDisCount(double zkje, double je, ref int ZKH)
        {
            ZKH = 1;
            double num = zkje / je;
            return SaleBillCtrl.GetRound(num, 5);
        }

        internal static string GetDisCountMC(double ZKL, int ZKH)
        {
            if (ZKH > 1)
            {
                return string.Format("折扣行数{0}({1})", ZKH, ZKL.ToString("0.000%"));
            }
            return string.Format("折扣({0})", ZKL.ToString("0.000%"));
        }

        public static string GetInfoFromNotes(string Notes, int type)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            if (!string.IsNullOrEmpty(Notes))
            {
                bool flag2;
                int index;
                int length;
                int num3;
                bool flag3;
                int num4;
                Notes = Notes.Trim();
                bool flag = false;
                string str4 = TaxCardFactory.CreateTaxCard().get_TaxCode();
                if ((!string.IsNullOrEmpty(str4) && (str4.Length == 15)) && (str4.Substring(8, 2) == "DK"))
                {
                    flag = true;
                }
                if (type == 1)
                {
                    str2 = "开具红字增值税专用发票信息表编号";
                    flag2 = false;
                    flag2 = Notes.Contains("差额征税");
                    if (flag || flag2)
                    {
                        if (Notes.Contains(str2))
                        {
                            index = Notes.IndexOf(str2);
                            Notes = Notes.Substring(index + 0x10, Notes.Length - (0x10 + index));
                            Notes = Notes.Trim();
                            if (Notes.Length < 0x10)
                            {
                                return str;
                            }
                            Notes = Notes.Substring(0, 0x10);
                            if (Regex.IsMatch(Notes, "^[0-9]{16,16}$"))
                            {
                                return Notes;
                            }
                        }
                        return str;
                    }
                    if (Notes.StartsWith(str2))
                    {
                        Notes = Notes.Substring(0x10, Notes.Length - 0x10);
                        Notes = Notes.Trim();
                        if (Notes.Length < 0x10)
                        {
                            return str;
                        }
                        Notes = Notes.Substring(0, 0x10);
                        if (Regex.IsMatch(Notes, "^[0-9]{16,16}$"))
                        {
                            return Notes;
                        }
                    }
                    return str;
                }
                if (type != 0)
                {
                    if (type == 2)
                    {
                        str2 = "开具红字货物运输业增值税专用发票信息表编号";
                        flag2 = false;
                        flag2 = Notes.Contains("差额征税");
                        if (flag || flag2)
                        {
                            if (Notes.Contains(str2))
                            {
                                index = Notes.IndexOf(str2);
                                Notes = Notes.Substring(0x15 + index, Notes.Length - (0x15 + index));
                                Notes = Notes.Trim();
                                if (Notes.Length < 0x10)
                                {
                                    return str;
                                }
                                Notes = Notes.Substring(0, 0x10);
                                if (Regex.IsMatch(Notes, "^[0-9]{16,16}$"))
                                {
                                    return Notes;
                                }
                            }
                            return str;
                        }
                        if (!Notes.StartsWith(str2))
                        {
                            return str;
                        }
                        Notes = Notes.Substring(0x15, Notes.Length - 0x15);
                        Notes = Notes.Trim();
                        if (Notes.Length < 0x10)
                        {
                            return str;
                        }
                        Notes = Notes.Substring(0, 0x10);
                        if (Regex.IsMatch(Notes, "^[0-9]{16,16}$"))
                        {
                            return Notes;
                        }
                    }
                    return str;
                }
                str2 = "对应正数发票代码:";
                flag2 = false;
                flag2 = Notes.Contains("差额征税");
                if (!flag && !flag2)
                {
                    if (Notes.StartsWith(str2))
                    {
                        length = str2.Length;
                        Notes = Notes.Substring(length, Notes.Length - length);
                        Notes = Notes.Trim();
                        if (Notes.Length <= 10)
                        {
                            return str;
                        }
                        str2 = "号码:";
                        num3 = Notes.IndexOf(str2);
                        if (num3 == -1)
                        {
                            return str;
                        }
                        str3 = Notes.Substring(0, num3);
                        flag3 = true;
                        for (num4 = 0; num4 < str3.Length; num4++)
                        {
                            if (!Regex.IsMatch(str3.Substring(num4, 1), "^[0-9]{1}$"))
                            {
                                flag3 = false;
                                break;
                            }
                        }
                        if (flag3)
                        {
                            Notes = Notes.Substring(num3 + 3, (Notes.Length - num3) - 3);
                            Notes = Notes.Trim();
                            if (Notes.Length < 8)
                            {
                                return str;
                            }
                            Notes = Notes.Substring(0, 8);
                            if (Regex.IsMatch(Notes, "^[0-9]{8,8}$"))
                            {
                                return (str3 + Notes);
                            }
                        }
                    }
                    return str;
                }
                if (!Notes.Contains(str2))
                {
                    return str;
                }
                length = str2.Length;
                index = Notes.IndexOf(str2);
                Notes = Notes.Substring(length + index, Notes.Length - (length + index));
                Notes = Notes.Trim();
                if (Notes.Length <= 10)
                {
                    return str;
                }
                str2 = "号码:";
                num3 = Notes.IndexOf(str2);
                if (num3 == -1)
                {
                    return str;
                }
                str3 = Notes.Substring(0, num3);
                flag3 = true;
                for (num4 = 0; num4 < str3.Length; num4++)
                {
                    if (!Regex.IsMatch(str3.Substring(num4, 1), "^[0-9]{1}$"))
                    {
                        flag3 = false;
                        break;
                    }
                }
                if (flag3)
                {
                    Notes = Notes.Substring(num3 + 3, (Notes.Length - num3) - 3);
                    Notes = Notes.Trim();
                    if (Notes.Length < 8)
                    {
                        return str;
                    }
                    Notes = Notes.Substring(0, 8);
                    if (Regex.IsMatch(Notes, "^[0-9]{8,8}$"))
                    {
                        return (str3 + Notes);
                    }
                }
            }
            return str;
        }

        internal static InvType GetInvType(string DJZL)
        {
            switch (DJZL)
            {
                case "s":
                    return InvType.Special;

                case "c":
                    return InvType.Common;

                case "f":
                    return InvType.transportation;

                case "j":
                    return InvType.vehiclesales;
            }
            return InvType.Special;
        }

        internal static string GetInvTypeStr(InvType FPType)
        {
            switch (FPType)
            {
                case InvType.Special:
                    return "s";

                case InvType.Common:
                    return "c";

                case InvType.transportation:
                    return "f";

                case InvType.vehiclesales:
                    return "j";
            }
            return "NaN";
        }

        public static bool isCEZS()
        {
            bool flag = false;
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if ((card.GetExtandParams("CEBTVisble") != null) && (card.GetExtandParams("CEBTVisble") == "1"))
            {
                flag = true;
            }
            return flag;
        }

        public static bool isSPBMVersion()
        {
            bool flag = false;
            if (TaxCardFactory.CreateTaxCard().GetExtandParams("FLBMFlag") == "FLBM")
            {
                flag = true;
            }
            return flag;
        }

        public static string JZ50_15_DJType()
        {
            return "0";
        }

        internal static bool RegexMatchNum(object Input)
        {
            bool flag = false;
            if (Input != null)
            {
                flag = Regex.IsMatch(Input.ToString(), @"^(\-|\+)?\d+(\,\d+)*(\.\d+)?$");
            }
            return flag;
        }

        public static string ReverseS(string text)
        {
            char[] array = text.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        public static bool ToBoolString(string value)
        {
            return (ToStringBool(value) == "1");
        }

        internal static double Todouble(string number)
        {
            double result = 0.0;
            double.TryParse(number, out result);
            return result;
        }

        internal static double TodoubleNew(string number)
        {
            double result = 0.0;
            double.TryParse(number, out result);
            return result;
        }

        internal static double? TodoubleNew_x(string number)
        {
            double result = 0.0;
            if (!double.TryParse(number, out result))
            {
                return null;
            }
            return new double?(result);
        }

        public static double? ToSlv(string Slv)
        {
            double result = 0.0;
            if (Slv.EndsWith("%"))
            {
                Slv = Slv.Trim(new char[] { '%' });
            }
            if (!double.TryParse(Slv, out result))
            {
                return null;
            }
            if (result >= 1.0)
            {
                result /= 100.0;
            }
            return new double?(result);
        }

        public static string ToStringBool(bool value)
        {
            return ToStringBool(value.ToString());
        }

        internal static string ToStringBool(string value)
        {
            value = value.ToLower();
            string str = "0";
            if (!string.IsNullOrEmpty(value))
            {
                if ((((value == "1") || (value == "y")) || ((value == "t") || (value == "是"))) || (value == "true"))
                {
                    str = "1";
                }
                else if ((((value == "0") || (value == "n")) || ((value == "f") || (value == "否"))) || (value == "false"))
                {
                    str = "0";
                }
                else
                {
                    str = "0";
                }
            }
            return str;
        }

        public static string ToValidate(string value)
        {
            string str = value;
            double result = 0.0;
            if (value.Trim().Length == 0)
            {
                return "ImportNull";
            }
            if (!double.TryParse(value, out result))
            {
                str = "ImportError";
            }
            return str;
        }

        public static double? ToZKL(string zkl)
        {
            double num = 0.0;
            if (zkl.Trim().Length == 0)
            {
                return new double?(num);
            }
            if (zkl.EndsWith("%"))
            {
                if (!double.TryParse(zkl.Trim(new char[] { '%' }), out num))
                {
                    return null;
                }
                num /= 100.0;
                if ((num <= 0.0) || (num > 1.0))
                {
                    return null;
                }
            }
            else
            {
                if (!double.TryParse(zkl, out num))
                {
                    return null;
                }
                if (num > 1.0)
                {
                    num /= 100.0;
                }
                else if ((num <= 0.0) || (num > 1.0))
                {
                    return null;
                }
            }
            if ((num > 100.0) || (num < 0.0))
            {
                return null;
            }
            return new double?(num);
        }

        public static string ToZKSE(string value)
        {
            string str = value;
            double result = 0.0;
            if (value.Trim().Length == 0)
            {
                return "ImportNull";
            }
            if (!double.TryParse(value, out result))
            {
                str = "ImportError";
            }
            return str;
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        internal struct PARAMS
        {
            internal static int NEW_MAX_LINE;
            internal static int NEW_MAX_LINE_FLBM;
            internal static int OLD_MAX_LINE;
            internal static int OLD_MAX_LINE_FLBM;
            static PARAMS()
            {
                NEW_MAX_LINE = 0x270f;
                NEW_MAX_LINE_FLBM = 0x176f;
                OLD_MAX_LINE = 0xfc;
                OLD_MAX_LINE_FLBM = 0x95;
            }
        }
    }
}

