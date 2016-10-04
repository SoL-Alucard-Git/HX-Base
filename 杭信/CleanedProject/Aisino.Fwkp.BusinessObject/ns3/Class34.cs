namespace ns3
{
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Text.RegularExpressions;

    internal class Class34
    {
        public Class34()
        {
            
        }

        public static string smethod_0(string string_0)
        {
            if (string_0.Contains("差额征税"))
            {
                int index = string_0.IndexOf("差额征税");
                int num2 = string_0.IndexOf("。", index);
                if ((num2 > index) && (((num2 - index) - 5) > 0))
                {
                    return string_0.Substring(index + 5, (num2 - index) - 5);
                }
            }
            return null;
        }

        internal static string smethod_1(ZYFP_LX zyfp_LX_0, string string_0, int int_0, Spxx spxx_0 = null)
        {
            if (zyfp_LX_0 == ZYFP_LX.JZ_50_15)
            {
                return smethod_15(string_0, "69", int_0);
            }
            if (zyfp_LX_0 == ZYFP_LX.CEZS)
            {
                string kce = spxx_0.Kce;
                return smethod_12(smethod_18(string_0, kce), spxx_0.SLv, int_0);
            }
            return "";
        }

        internal static bool smethod_10(string string_0, string string_1)
        {
            decimal num = decimal.Parse(string_0);
            decimal num2 = decimal.Parse(string_1);
            return (decimal.Compare(num, num2) == 0);
        }

        internal static string smethod_11(string string_0, string string_1, int int_0)
        {
            decimal num = decimal.Parse(string_0);
            decimal num2 = decimal.Parse(string_1);
            return decimal.Round(decimal.Multiply(num, num2), int_0, MidpointRounding.AwayFromZero).ToString();
        }

        internal static string smethod_12(string string_0, string string_1, int int_0)
        {
            decimal num = decimal.Parse(string_0);
            decimal num2 = decimal.Parse(string_1);
            return decimal.Round(decimal.Multiply(num, num2), int_0, MidpointRounding.AwayFromZero).ToString("f" + int_0);
        }

        public static string smethod_13(string string_0, string string_1)
        {
            decimal num = decimal.Parse(string_0);
            decimal num2 = decimal.Parse(string_1);
            return decimal.Multiply(num, num2).ToString();
        }

        internal static string smethod_14(string string_0, string string_1, int int_0)
        {
            decimal num = decimal.Parse(string_0);
            decimal num2 = decimal.Parse(string_1);
            return decimal.Round(decimal.Divide(num, num2), int_0, MidpointRounding.AwayFromZero).ToString();
        }

        internal static string smethod_15(string string_0, string string_1, int int_0)
        {
            decimal num = decimal.Parse(string_0);
            decimal num2 = decimal.Parse(string_1);
            return decimal.Round(decimal.Divide(num, num2), int_0, MidpointRounding.AwayFromZero).ToString("f" + int_0);
        }

        internal static string smethod_16(string string_0, string string_1)
        {
            decimal num = decimal.Parse(string_0);
            decimal num2 = decimal.Parse(string_1);
            return decimal.Divide(num, num2).ToString();
        }

        internal static string smethod_17(string string_0, string string_1)
        {
            decimal num = decimal.Parse(string_0);
            decimal num2 = decimal.Parse(string_1);
            return decimal.Add(num, num2).ToString();
        }

        internal static string smethod_18(string string_0, string string_1)
        {
            decimal num = decimal.Parse(string_0);
            decimal num2 = decimal.Parse(string_1);
            return decimal.Subtract(num, num2).ToString();
        }

        internal static string smethod_19(string string_0, int int_0)
        {
            return decimal.Round(decimal.Parse(string_0), int_0, MidpointRounding.AwayFromZero).ToString("f" + int_0);
        }

        internal static string smethod_2(ZYFP_LX zyfp_LX_0, string string_0, int int_0, Dictionary<SPXX, string> spxx = null, Spxx spxx_0 = null)
        {
            if (zyfp_LX_0 == ZYFP_LX.JZ_50_15)
            {
                return smethod_14(smethod_13(string_0, "69"), "70", int_0);
            }
            if (zyfp_LX_0 == ZYFP_LX.CEZS)
            {
                string kce = "";
                string sLv = "";
                if (spxx != null)
                {
                    kce = spxx[SPXX.KCE];
                    sLv = spxx[SPXX.SLV];
                }
                else if (spxx_0 != null)
                {
                    kce = spxx_0.Kce;
                    sLv = spxx_0.SLv;
                }
                if (!string.IsNullOrEmpty(kce) && !string.IsNullOrEmpty(sLv))
                {
                    return smethod_15(smethod_17(smethod_13(kce, sLv), string_0), smethod_17("1", sLv), int_0);
                }
            }
            return "";
        }

        internal static string smethod_20(string string_0, int int_0)
        {
            if (string_0.IndexOf('.') >= 0)
            {
                for (int i = string_0.Length - 1; i >= 0; i--)
                {
                    if (string_0[i] == '.')
                    {
                        string_0 = string_0.Remove(i);
                        break;
                    }
                    if (string_0[i] != '0')
                    {
                        break;
                    }
                    string_0 = string_0.Remove(i);
                }
            }
            return decimal.Round(decimal.Parse(string_0), int_0, MidpointRounding.AwayFromZero).ToString();
        }

        internal static string smethod_21(string string_0)
        {
            if (string_0.IndexOf("-") == 0)
            {
                return string_0.Substring(1, string_0.Length - 1);
            }
            if (smethod_10(string_0, Struct40.string_0))
            {
                return string_0;
            }
            return new StringBuilder("-").Append(string_0).ToString();
        }

        internal static bool smethod_22(string string_0, string string_1, string string_2, int int_0, string string_3)
        {
            decimal num = decimal.Parse(string_0);
            decimal num2 = decimal.Parse(string_1);
            decimal num3 = decimal.Round(decimal.Parse(string_2), int_0, MidpointRounding.AwayFromZero);
            decimal num4 = Math.Abs(decimal.Parse(string_3));
            return (decimal.Compare(Math.Abs(decimal.Subtract(decimal.Round(decimal.Divide(num, num2), int_0, MidpointRounding.AwayFromZero), num3)), num4) <= 0);
        }

        internal static string smethod_23(string string_0, int int_0, bool bool_0, bool bool_1 = true)
        {
            string str = string_0;
            if (bool_1 && (string_0 != null))
            {
                string[] strArray2 = string_0.Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
                str = (strArray2.Length == 0) ? "" : strArray2[0];
            }
            if ((str != null) && (str.Length != 0))
            {
                int byteCount = ToolUtil.GetByteCount(str);
                if (byteCount <= int_0)
                {
                    if (bool_0)
                    {
                        return new StringBuilder(str).Append(new string(' ', int_0 - byteCount)).ToString();
                    }
                    return str;
                }
                for (int i = str.Length; i >= 0; i--)
                {
                    str = str.Substring(0, i);
                    if (ToolUtil.GetByteCount(str) <= int_0)
                    {
                        return str;
                    }
                }
                return "";
            }
            if (!bool_0)
            {
                return string.Empty;
            }
            return new string(' ', int_0);
        }

        internal static string smethod_24(string string_0, int int_0, bool bool_0)
        {
            try
            {
                if ((string_0 != null) && (string_0.Trim().Length != 0))
                {
                    decimal d = decimal.Parse(string_0);
                    string str2 = d.ToString();
                    int length = str2.Length;
                    if (length > int_0)
                    {
                        int index = str2.IndexOf('.');
                        if (index >= 0)
                        {
                            int num3 = (length - (index + 1)) - (length - int_0);
                            if (num3 >= -1)
                            {
                                return decimal.Round(d, (num3 < 0) ? 0 : num3, MidpointRounding.AwayFromZero).ToString();
                            }
                            return null;
                        }
                        if (length > int_0)
                        {
                            return null;
                        }
                        if (bool_0)
                        {
                            return new StringBuilder().Append(str2).Append(' ', int_0 - length).ToString();
                        }
                        return str2;
                    }
                    if (bool_0)
                    {
                        return new StringBuilder().Append(str2).Append(' ', int_0 - length).ToString();
                    }
                    return str2;
                }
                if (!bool_0)
                {
                    return string.Empty;
                }
                return new string(' ', int_0);
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal static byte[] smethod_25(object object_0)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream serializationStream = new MemoryStream(0x2800);
            formatter.Serialize(serializationStream, object_0);
            serializationStream.Seek(0L, SeekOrigin.Begin);
            byte[] buffer = new byte[(int) serializationStream.Length];
            serializationStream.Read(buffer, 0, buffer.Length);
            serializationStream.Close();
            return buffer;
        }

        internal static object smethod_26(byte[] byte_0)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream serializationStream = new MemoryStream(byte_0, 0, byte_0.Length, false);
            object obj2 = formatter.Deserialize(serializationStream);
            serializationStream.Close();
            return obj2;
        }

        internal static string smethod_3(ZYFP_LX zyfp_LX_0, string string_0, int int_0, Dictionary<SPXX, string> spxx = null, Spxx spxx_0 = null)
        {
            if (zyfp_LX_0 == ZYFP_LX.JZ_50_15)
            {
                return smethod_14(smethod_13(string_0, "69"), "70", int_0);
            }
            if (zyfp_LX_0 == ZYFP_LX.CEZS)
            {
                string sL = "";
                string sLv = "";
                string kce = "";
                string je = "";
                string se = "";
                if (spxx != null)
                {
                    sL = spxx[SPXX.SL];
                    sLv = spxx[SPXX.SLV];
                    kce = spxx[SPXX.KCE];
                    je = spxx[SPXX.JE];
                    se = spxx[SPXX.SE];
                }
                else if (spxx_0 != null)
                {
                    sL = spxx_0.SL;
                    sLv = spxx_0.SLv;
                    kce = spxx_0.Kce;
                    je = spxx_0.Je;
                    se = spxx_0.Se;
                }
                if ((!string.IsNullOrEmpty(kce) && !string.IsNullOrEmpty(sLv)) && (!string.IsNullOrEmpty(je) && !string.IsNullOrEmpty(se)))
                {
                    return smethod_14(smethod_13(smethod_17(smethod_17(je, se), smethod_13(kce, sLv)), string_0), smethod_13(smethod_17(je, se), smethod_17("1", sLv)), Struct40.int_46);
                }
                if ((!string.IsNullOrEmpty(sLv) && !string.IsNullOrEmpty(sL)) && !string.IsNullOrEmpty(kce))
                {
                    return smethod_14(smethod_17(smethod_16(smethod_13(kce, sLv), sL), string_0), smethod_17("1", sLv), int_0);
                }
            }
            return "";
        }

        internal static string smethod_4(ZYFP_LX zyfp_LX_0, string string_0, int int_0, Dictionary<SPXX, string> spxx = null, Spxx spxx_0 = null)
        {
            if (zyfp_LX_0 == ZYFP_LX.JZ_50_15)
            {
                return smethod_14(smethod_13(string_0, "70"), "69", int_0);
            }
            if (zyfp_LX_0 == ZYFP_LX.CEZS)
            {
                string sL = "";
                string sLv = "";
                string kce = "";
                string je = "";
                string se = "";
                if (spxx != null)
                {
                    sL = spxx[SPXX.SL];
                    sLv = spxx[SPXX.SLV];
                    kce = spxx[SPXX.KCE];
                    je = spxx[SPXX.JE];
                    se = spxx[SPXX.SE];
                }
                else if (spxx_0 != null)
                {
                    sL = spxx_0.SL;
                    sLv = spxx_0.SLv;
                    kce = spxx_0.Kce;
                    je = spxx_0.Je;
                    se = spxx_0.Se;
                }
                if ((!string.IsNullOrEmpty(kce) && !string.IsNullOrEmpty(sLv)) && (!string.IsNullOrEmpty(je) && !string.IsNullOrEmpty(se)))
                {
                    return smethod_14(smethod_13(smethod_13(string_0, smethod_17("1", sLv)), smethod_17(je, se)), smethod_17(smethod_17(je, se), smethod_13(kce, sLv)), Struct40.int_46);
                }
                if ((!string.IsNullOrEmpty(sLv) && !string.IsNullOrEmpty(sL)) && !string.IsNullOrEmpty(kce))
                {
                    return smethod_18(smethod_11(smethod_17("1", sLv), string_0, int_0), smethod_14(smethod_13(kce, sLv), sL, int_0));
                }
            }
            return "";
        }

        internal static bool smethod_5(string string_0)
        {
            Regex regex = new Regex("^[A-Z0-9]*$");
            return regex.IsMatch(string_0);
        }

        internal static bool smethod_6(string string_0)
        {
            Regex regex = new Regex("^[0-9]*$");
            return regex.IsMatch(string_0);
        }

        internal static bool smethod_7(string string_0, string string_1, string string_2, int int_0, string string_3)
        {
            decimal num = decimal.Parse(string_0);
            decimal num2 = decimal.Parse(string_1);
            decimal num3 = decimal.Round(decimal.Parse(string_2), int_0, MidpointRounding.AwayFromZero);
            decimal num4 = Math.Abs(decimal.Parse(string_3));
            return (decimal.Compare(Math.Abs(decimal.Subtract(decimal.Round(decimal.Multiply(num, num2), int_0, MidpointRounding.AwayFromZero), num3)), num4) <= 0);
        }

        internal static bool smethod_8(string string_0, string string_1, int int_0, string string_2)
        {
            string str = smethod_19(string_0, int_0);
            string str2 = smethod_19(string_1, int_0);
            decimal num = Math.Abs(decimal.Parse(smethod_18(str, str2)));
            decimal num2 = decimal.Parse(string_2);
            return (decimal.Compare(num, num2) <= 0);
        }

        internal static bool smethod_9(string string_0, string string_1, bool bool_0)
        {
            decimal num = decimal.Parse(string_0);
            decimal num2 = decimal.Parse(string_1);
            if (bool_0)
            {
                decimal.Compare(num, num2);
                return (decimal.Compare(num, num2) >= 0);
            }
            return (decimal.Compare(num, num2) > 0);
        }
    }
}

