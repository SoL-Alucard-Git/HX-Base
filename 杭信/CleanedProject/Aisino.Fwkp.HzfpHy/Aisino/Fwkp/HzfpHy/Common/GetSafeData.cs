namespace Aisino.Fwkp.HzfpHy.Common
{
    using System;
    using System.Data;

    internal class GetSafeData
    {
        public static string ValidateMxseValue(DataRow row, string colname)
        {
            string str = "";
            if (row[colname] != DBNull.Value)
            {
                try
                {
                    return Convert.ToString(row[colname]);
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

