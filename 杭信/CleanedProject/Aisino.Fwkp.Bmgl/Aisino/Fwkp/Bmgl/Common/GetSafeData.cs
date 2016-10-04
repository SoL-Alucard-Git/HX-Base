namespace Aisino.Fwkp.Bmgl.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal static class GetSafeData
    {
        public static string ExportItem(string Item, string Separator)
        {
            string str = Item;
            if (Item.Contains(" "))
            {
                return ('"' + Item + '"');
            }
            if ((Separator == " ") && (Item == ""))
            {
                str = "\"\"";
            }
            return str;
        }

        public static string[] Split(string LineText, string Separator)
        {
            List<string> list = new List<string>();
            string str = "";
            bool flag = false;
            string[] strArray = LineText.Split(new string[] { Separator }, StringSplitOptions.None);
            int length = strArray.Length;
            int index = 0;
            while (index < length)
            {
                string item = strArray[index];
                if (flag)
                {
                    str = str + Separator;
                    if (!item.StartsWith("\"") && item.EndsWith("\""))
                    {
                        str = str + item;
                        flag = false;
                        item = str.Trim(new char[] { '"' });
                    }
                }
                if (item.StartsWith("\"") && !item.EndsWith("\""))
                {
                    str = str + item;
                    flag = true;
                }
                index++;
                if (!flag)
                {
                    if (item.Contains(" "))
                    {
                        item = item.Trim(new char[] { '"' });
                    }
                    if (((Separator == " ") || (Separator == ",")) && (item == "\"\""))
                    {
                        item = "";
                    }
                    list.Add(item);
                    str = "";
                }
            }
            return list.ToArray();
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

