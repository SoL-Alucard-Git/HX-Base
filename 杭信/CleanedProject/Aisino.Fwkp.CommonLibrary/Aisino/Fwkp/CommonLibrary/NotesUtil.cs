namespace Aisino.Fwkp.CommonLibrary
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;

    public class NotesUtil
    {
        public NotesUtil()
        {
            
        }

        public static bool CheckTzdh(string string_0)
        {
            return CheckTzdh(string_0, "s");
        }

        public static bool CheckTzdh(string string_0, string string_1)
        {
            if (string_0 == null)
            {
                return false;
            }
            if (string_0.Length > 0x10)
            {
                return false;
            }
            if (!Regex.IsMatch(string_0, @"\d{16}$"))
            {
                return false;
            }
            if (int.Parse(string_0.Substring(6, 2)) < 7)
            {
                return false;
            }
            int num4 = int.Parse(string_0.Substring(8, 2));
            if ((string_1 != "s") && (string_1 != "f"))
            {
                return false;
            }
            if ((string_1 == "s") && ((num4 < 1) || (num4 > 12)))
            {
                return false;
            }
            if ((string_1 == "f") && ((num4 < 13) || (num4 > 0x18)))
            {
                return false;
            }
            int num2 = 0;
            for (int i = 0; i < (string_0.Length - 1); i++)
            {
                num2 += int.Parse(new string(string_0[i], 1));
            }
            return ((num2 % 10) == int.Parse(new string(string_0[string_0.Length - 1], 1)));
        }

        public static string GetDKBZ(string string_0, string string_1)
        {
            if (!string.IsNullOrEmpty(string_0) && !string.IsNullOrEmpty(string_1))
            {
                return string.Format("代开企业税号:{0} 代开企业名称:{1}", string_0, string_1);
            }
            return "";
        }

        public static string GetDKInvNotes(string string_0, string string_1)
        {
            if (!string.IsNullOrEmpty(string_0) && !string.IsNullOrEmpty(string_1))
            {
                return ("代开企业税号:" + string_0 + " 代开企业名称:" + string_1);
            }
            return null;
        }

        public static string GetDKQYFromInvNotes(string string_0, out string string_1, out string string_2)
        {
            string_1 = string.Empty;
            string_2 = string.Empty;
            foreach (string str2 in Regex.Split(string_0, "\r\n"))
            {
                int index = str2.IndexOf("代开企业税号:");
                int num3 = str2.IndexOf("代开企业名称:");
                if (((index >= 0) && (num3 >= 0)) && (index < num3))
                {
                    string str3 = str2.Substring(index + "代开企业税号:".Length, (num3 - index) - "代开企业税号:".Length).Trim();
                    string str4 = str2.Substring(num3 + "代开企业名称:".Length).Trim();
                    if (!string.IsNullOrEmpty(str3) && !string.IsNullOrEmpty(str4))
                    {
                        string_1 = str3;
                        string_2 = str4;
                        return "0000";
                    }
                }
            }
            return "0001";
        }

        public static string GetInfo(string string_0, int int_0, string string_1 = "")
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(string_0))
            {
                string_0 = string_0.Trim();
                if ((int_0 == 1) || (int_0 == 2))
                {
                    string str4 = (int_0 == 1) ? "开具红字增值税专用发票信息表编号" : "开具红字货物运输业增值税专用发票信息表编号";
                    string str11 = string_0;
                    foreach (string str3 in Regex.Split(str11, "\r\n"))
                    {
                        int index = str3.IndexOf(str4);
                        if (index >= 0)
                        {
                            string str5 = str3.Substring(index + str4.Length).Trim();
                            if (!string.IsNullOrEmpty(str5))
                            {
                                if (str5.Length > 0x10)
                                {
                                    str5 = str5.Substring(0, 0x10);
                                }
                                return str5;
                            }
                        }
                    }
                    return str;
                }
                if (((int_0 != 0) && (int_0 != 3)) && (int_0 != 4))
                {
                    return str;
                }
                string input = string_0.Trim();
                string str6 = string.Empty;
                string str7 = string.Empty;
                foreach (string str10 in Regex.Split(input, "\r\n"))
                {
                    int num4 = str10.IndexOf("对应正数发票代码:");
                    int num = str10.IndexOf("号码:");
                    if (((num4 >= 0) && (num >= 0)) && (num4 < num))
                    {
                        string str9 = str10.Substring(num4 + "对应正数发票代码:".Length, (num - num4) - "对应正数发票代码:".Length).Trim();
                        string str2 = str10.Substring(num + "号码:".Length).Trim();
                        if (!string.IsNullOrEmpty(str9) && !string.IsNullOrEmpty(str2))
                        {
                            if (str2.Length > 8)
                            {
                                str2 = str2.Substring(0, 8);
                            }
                            str6 = str9;
                            str7 = str2.PadLeft(8, '0');
                            str = str6 + str7;
                            if (!string.IsNullOrEmpty(string_1))
                            {
                                str = smethod_2(str6) + string_1 + smethod_2(str7);
                            }
                            return str;
                        }
                    }
                }
            }
            return str;
        }

        public static string GetRedInvNotes(string string_0)
        {
            return GetRedZyInvNotes(string_0, "s");
        }

        public static string GetRedInvNotes(string string_0, string string_1)
        {
            if ((string_1 != null) && (string_1.Length < 8))
            {
                string_1 = new string('0', 8 - string_1.Length) + string_1;
            }
            if (smethod_0(string_0) && smethod_1(string_1))
            {
                return ("对应正数发票代码:" + string_0 + "号码:" + string_1);
            }
            return null;
        }

        public static string GetRedZyInvNotes(string string_0, string string_1)
        {
            if (CheckTzdh(string_0, string_1))
            {
                if (string_1 == "s")
                {
                    return ("开具红字增值税专用发票信息表编号" + string_0);
                }
                if (string_1 == "f")
                {
                    return ("开具红字货物运输业增值税专用发票信息表编号" + string_0);
                }
            }
            return null;
        }

        private static bool smethod_0(object object_0)
        {
            if (object_0 == null)
            {
                return false;
            }
            return true;
        }

        private static bool smethod_1(string string_0)
        {
            if (string_0 == null)
            {
                return false;
            }
            if (string_0.Length > 8)
            {
                return false;
            }
            return true;
        }

        private static string smethod_2(string string_0)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char ch in string_0)
            {
                if (!char.IsDigit(ch))
                {
                    break;
                }
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}

