namespace Aisino.Framework.Plugin.Core.ExcelXml.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Xml;

    public static class ObjectExtensions
    {
        private static XmlDocument xmlDocument_0;

        public static DateSpan DateDifference(this DateTime dateTime_0, DateTime dateTime_1)
        {
            int num = (((dateTime_0.Year - dateTime_1.Year) * 12) + dateTime_0.Month) - dateTime_1.Month;
            int days = 0;
            if (dateTime_0.Day < dateTime_1.Day)
            {
                int num3;
                int year;
                int day = dateTime_1.Day;
                if (dateTime_0.Month == 1)
                {
                    num3 = 12;
                    year = dateTime_0.Year - 1;
                }
                else
                {
                    num3 = dateTime_0.Month - 1;
                    year = dateTime_0.Year;
                }
                DateTime time = new DateTime(year, num3, day);
                TimeSpan span2 = (TimeSpan) (dateTime_0 - time);
                days = span2.Days;
                num--;
            }
            else
            {
                days = dateTime_0.Day - dateTime_1.Day;
            }
            return new DateSpan { Years = num / 12, Months = num % 12, Days = days };
        }

        public static IEnumerable<XmlReaderAttributeItem> GetAttributes(this XmlReader xmlReader_0)
        {
            List<XmlReaderAttributeItem> list = new List<XmlReaderAttributeItem>();
            if (xmlReader_0.HasAttributes)
            {
                xmlReader_0.MoveToFirstAttribute();
                list.Add(smethod_0(xmlReader_0));
                while (xmlReader_0.MoveToNextAttribute())
                {
                    list.Add(smethod_0(xmlReader_0));
                }
            }
            return list;
        }

        public static string GetDescription(this Enum enum_0)
        {
            DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) enum_0.GetType().GetField(enum_0.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if ((customAttributes != null) && (customAttributes.Length > 0))
            {
                return customAttributes[0].Description;
            }
            return enum_0.ToString();
        }

        public static XmlReaderAttributeItem GetSingleAttribute(this XmlReader xmlReader_0, string string_0)
        {
            return xmlReader_0.GetSingleAttribute(string_0, false);
        }

        public static XmlReaderAttributeItem GetSingleAttribute(this XmlReader xmlReader_0, string string_0, bool bool_0)
        {
            string name = xmlReader_0.Name;
            if (bool_0 && xmlReader_0.IsEmptyElement)
            {
                bool_0 = false;
            }
            using (IEnumerator<XmlReaderAttributeItem> enumerator = xmlReader_0.GetAttributes().GetEnumerator())
            {
                XmlReaderAttributeItem current;
                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    if (current.LocalName == string_0)
                    {
                        goto Label_0043;
                    }
                }
                goto Label_0078;
            Label_0043:
                if (bool_0)
                {
                    while (xmlReader_0.Read())
                    {
                        if ((xmlReader_0.Name == name) && (xmlReader_0.NodeType == XmlNodeType.EndElement))
                        {
                            return current;
                        }
                    }
                }
                return current;
            }
        Label_0078:
            if (bool_0)
            {
                while (xmlReader_0.Read())
                {
                    if ((xmlReader_0.Name == name) && (xmlReader_0.NodeType == XmlNodeType.EndElement))
                    {
                        break;
                    }
                }
            }
            return null;
        }

        public static bool IsFlagSet(this Enum enum_0, Enum enum_1)
        {
            if (!enum_0.IsValid())
            {
                return false;
            }
            if (!enum_1.IsValid())
            {
                return false;
            }
            if (enum_0.GetType() != enum_1.GetType())
            {
                return false;
            }
            int num = (int) Enum.Parse(enum_0.GetType(), enum_0.ToString());
            int num2 = (int) Enum.Parse(enum_1.GetType(), enum_1.ToString());
            return ((num & num2) == num2);
        }

        public static bool IsNullOrEmpty(this string string_0)
        {
            if (string_0 != null)
            {
                return string.IsNullOrEmpty(string_0.Trim());
            }
            return true;
        }

        public static bool IsNumericType(Type type_0)
        {
            switch (type_0.FullName)
            {
                case "System.SByte":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.Byte":
                case "System.UInt16":
                case "System.UInt32":
                case "System.UInt64":
                case "System.Single":
                case "System.Double":
                case "System.Decimal":
                    return true;
            }
            return false;
        }

        public static bool IsValid(this Enum enum_0)
        {
            bool flag;
            if (!(flag = Enum.IsDefined(enum_0.GetType(), enum_0)))
            {
                FlagsAttribute[] customAttributes = (FlagsAttribute[]) enum_0.GetType().GetCustomAttributes(typeof(FlagsAttribute), false);
                if ((customAttributes != null) && (customAttributes.Length > 0))
                {
                    return enum_0.ToString().Contains(",");
                }
            }
            return flag;
        }

        public static bool IsValidEmail(this string string_0)
        {
            return string_0.IsValidEmail(false);
        }

        public static bool IsValidEmail(this string string_0, bool bool_0)
        {
            if (string_0.IsNullOrEmpty())
            {
                return bool_0;
            }
            string pattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(string_0);
        }

        public static string Left(this string string_0, int int_0)
        {
            return string_0.Substring(0, Math.Min(string_0.Length, int_0));
        }

        public static T ParseEnum<T>(string string_0)
        {
            return (T) Enum.Parse(typeof(T), string_0);
        }

        public static bool ParseToInt<T>(this string string_0, out T gparam_0)
        {
            decimal num;
            if (!typeof(T).IsPrimitive && (typeof(T).FullName != "System.Decimal"))
            {
                throw new ArgumentException("'variable' must only be a primitive type");
            }
            if (!decimal.TryParse(string_0, NumberStyles.Number, CultureInfo.InvariantCulture, out num))
            {
                gparam_0 = default(T);
                return false;
            }
            gparam_0 = (T) Convert.ChangeType(num, typeof(T), CultureInfo.InvariantCulture);
            return true;
        }

        public static string Right(this string string_0, int int_0)
        {
            if (string_0.Length <= int_0)
            {
                return string_0;
            }
            return string_0.Substring(string_0.Length - int_0, int_0);
        }

        private static XmlReaderAttributeItem smethod_0(XmlReader xmlReader_0)
        {
            XmlReaderAttributeItem item = new XmlReaderAttributeItem {
                Name = xmlReader_0.Name,
                LocalName = xmlReader_0.LocalName,
                Prefix = xmlReader_0.Prefix,
                HasValue = xmlReader_0.HasValue
            };
            if (item.HasValue)
            {
                item.Value = xmlReader_0.Value;
                return item;
            }
            item.Value = "";
            return item;
        }

        public static string XmlEncode(this string string_0)
        {
            if (string_0 == null)
            {
                return "";
            }
            if (xmlDocument_0 == null)
            {
                xmlDocument_0 = new XmlDocument();
                xmlDocument_0.LoadXml("<text></text>");
            }
            lock (xmlDocument_0)
            {
                xmlDocument_0.LastChild.InnerText = string_0;
                return xmlDocument_0.LastChild.InnerXml;
            }
        }
    }
}

