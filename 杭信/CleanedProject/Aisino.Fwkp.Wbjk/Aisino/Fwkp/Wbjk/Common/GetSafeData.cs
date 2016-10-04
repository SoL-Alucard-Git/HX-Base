namespace Aisino.Fwkp.Wbjk.Common
{
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Data;
    using System.Text;

    public class GetSafeData
    {
        private const int subbyte = 0xa1;
        private const int supbyte = 0xfe;

        public static object GetParameter(object para)
        {
            if (para == null)
            {
                return DBNull.Value;
            }
            return para;
        }

        internal static string GetSafeString(string text, int length)
        {
            text = text.Trim();
            int byteCount = ToolUtil.GetByteCount(text);
            Encoding encoding = ToolUtil.GetEncoding();
            byte[] bytes = encoding.GetBytes(text);
            if (bytes.GetLength(0) <= length)
            {
                return text;
            }
            byte num3 = bytes[length - 1];
            int index = length - 1;
            while ((bytes[index] >= 0xa1) && (bytes[index] <= 0xfe))
            {
                index--;
                if (index < 0)
                {
                    break;
                }
            }
            if (((length - index) % 2) == 0)
            {
                length--;
            }
            return encoding.GetString(bytes, 0, length);
        }

        internal static string GetSafeStringWithoutTrim(string text, int length)
        {
            text = text;
            int byteCount = ToolUtil.GetByteCount(text);
            Encoding encoding = ToolUtil.GetEncoding();
            byte[] bytes = encoding.GetBytes(text);
            if (bytes.GetLength(0) <= length)
            {
                return text;
            }
            byte num3 = bytes[length - 1];
            int index = length - 1;
            while ((bytes[index] >= 0xa1) && (bytes[index] <= 0xfe))
            {
                index--;
                if (index < 0)
                {
                    break;
                }
            }
            if (((length - index) % 2) == 0)
            {
                length--;
            }
            return encoding.GetString(bytes, 0, length);
        }

        public static T ValidateValue<T>(object valueObj)
        {
            T local = default(T);
            try
            {
                if (valueObj != DBNull.Value)
                {
                    local = (T) valueObj;
                }
                return local;
            }
            catch
            {
                return local;
            }
        }

        public static T ValidateValue<T>(DataRow row, string colname)
        {
            T local = default(T);
            if (row[colname] != DBNull.Value)
            {
                try
                {
                    Type type = row[colname].GetType();
                    if (typeof(T).Equals(typeof(double)) && type.Equals(typeof(decimal)))
                    {
                        object obj2 = Convert.ToDouble(row[colname]);
                        return (T) obj2;
                    }
                    return (T) row[colname];
                }
                catch (InvalidCastException exception)
                {
                    throw new InvalidCastException(exception.ToString());
                }
                catch
                {
                    return local;
                }
            }
            return local;
        }
    }
}

