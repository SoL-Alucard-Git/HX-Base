namespace Aisino.Fwkp.Hzfp.Common
{
    using System;
    using System.Data;

    internal class GetSafeData
    {
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

        public static string ValidateDoubleValue(DataRow row, string colname)
        {
            string str = "";
            if (row[colname] != DBNull.Value)
            {
                try
                {
                    return Convert.ToString((double) row[colname]);
                }
                catch
                {
                    return str;
                }
            }
            return str;
        }

        public static T ValidateValue<T>(DataRow row, string colname)
        {
            T local = default(T);
            if (row[colname] != DBNull.Value)
            {
                try
                {
                    return (T) row[colname];
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

