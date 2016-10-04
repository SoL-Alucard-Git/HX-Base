namespace ns6
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class Class126
    {
        private static char[] char_0;
        [DecimalConstant(2, 0, (uint) 0x33b2e3c, (uint) 0x9fd0803c, (uint) 0xe7ffffff)]
        private static readonly decimal decimal_0;
        [DecimalConstant(2, 0x80, (uint) 0x33b2e3c, (uint) 0x9fd0803c, (uint) 0xe7ffffff)]
        private static readonly decimal decimal_1;
        private static string string_0;
        private static string string_1;

        static Class126()
        {
            
            string_0 = "零壹贰叁肆伍陆柒捌玖";
            string_1 = "圆拾佰仟万拾佰仟亿拾佰仟兆拾佰仟万拾佰仟亿拾佰仟兆";
            decimal_0 = 9999999999999999999999999.99M;
            decimal_1 = -9999999999999999999999999.99M;
            char_0 = new char[] { '.' };
        }

        public static string smethod_0(decimal decimal_2)
        {
            bool flag = false;
            smethod_12(decimal_2);
            decimal num = Math.Round(decimal_2, 2);
            if (num == 0M)
            {
                return "零圆整";
            }
            if (num < 0M)
            {
                flag = true;
                num = Math.Abs(num);
            }
            else
            {
                flag = false;
            }
            string str2 = "";
            string str = "";
            string[] strArray = null;
            strArray = num.ToString().Split(char_0, 2);
            if (num >= 1M)
            {
                str = smethod_8(strArray[0]);
            }
            if (strArray.Length > 1)
            {
                str2 = smethod_9(strArray[1]);
            }
            else
            {
                str2 = "整";
            }
            if (!flag)
            {
                return (str + str2);
            }
            return ("(负数)" + str + str2);
        }

        public static string smethod_1(double double_0)
        {
            decimal num;
            try
            {
                num = Convert.ToDecimal(double_0);
            }
            catch
            {
                throw new Exception0("不能转成标准的decimal类型:" + double_0.ToString());
            }
            return smethod_0(num);
        }

        private static string smethod_10(int int_0)
        {
            char ch = string_1[int_0];
            return ch.ToString();
        }

        private static string smethod_11(char char_1)
        {
            char ch = string_0[char_1 - '0'];
            return ch.ToString();
        }

        private static void smethod_12(decimal decimal_2)
        {
            if ((decimal_2 < -9999999999999999999999999.99M) || (decimal_2 > 9999999999999999999999999.99M))
            {
                throw new Exception0("超出可转换的范围");
            }
        }

        private static string smethod_13(string string_2)
        {
            if (string_2.Contains("兆亿万"))
            {
                return string_2.Replace("兆亿万", "兆");
            }
            if (string_2.Contains("亿万"))
            {
                return string_2.Replace("亿万", "亿");
            }
            if (string_2.Contains("兆亿"))
            {
                return string_2.Replace("兆亿", "兆");
            }
            return string_2;
        }

        public static string smethod_2(float float_0)
        {
            decimal num;
            try
            {
                num = Convert.ToDecimal(float_0);
            }
            catch
            {
                throw new Exception0("不能转成标准的decimal类型:" + float_0.ToString());
            }
            return smethod_0(num);
        }

        public static string smethod_3(int int_0)
        {
            return smethod_0(Convert.ToDecimal(int_0));
        }

        public static string smethod_4(long long_0)
        {
            return smethod_0(Convert.ToDecimal(long_0));
        }

        public static string smethod_5(string string_2)
        {
            decimal num;
            try
            {
                num = Convert.ToDecimal(string_2, null);
            }
            catch
            {
                throw new Exception0("不能转成标准的decimal类型:" + string_2);
            }
            return smethod_0(num);
        }

        public static decimal smethod_6()
        {
            return 9999999999999999999999999.99M;
        }

        public static decimal smethod_7()
        {
            return -9999999999999999999999999.99M;
        }

        private static string smethod_8(string string_2)
        {
            string str = "";
            int length = string_2.Length;
            int num2 = length;
            string str2 = "";
            string str3 = "";
            int num3 = 0;
            while (num3 < (length - 1))
            {
                if (string_2[num3] != '0')
                {
                    str2 = smethod_11(string_2[num3]);
                    str3 = smethod_10(num2 - 1);
                }
                else if (((num2 - 1) % 4) == 0)
                {
                    str2 = "";
                    str3 = smethod_10(num2 - 1);
                }
                else
                {
                    str3 = "";
                    if (string_2[num3 + 1] != '0')
                    {
                        str2 = "零";
                    }
                    else
                    {
                        str2 = "";
                    }
                }
                str = str + str2 + str3;
                num3++;
                num2--;
            }
            if (string_2[num3] != '0')
            {
                str = str + smethod_11(string_2[num3]);
            }
            return smethod_13(str + "圆");
        }

        private static string smethod_9(string string_2)
        {
            string str = "";
            int length = string_2.Length;
            if ((string_2 == "0") || (string_2 == "00"))
            {
                return "整";
            }
            if (string_2.Length > 1)
            {
                if (string_2[0] == '0')
                {
                    return (smethod_11(string_2[1]) + "分");
                }
                if (string_2[1] == '0')
                {
                    return (smethod_11(string_2[0]) + "角整");
                }
                return ((smethod_11(string_2[0]) + "角") + smethod_11(string_2[1]) + "分");
            }
            return (str + smethod_11(string_2[0]) + "角整");
        }
    }
}

