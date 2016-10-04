namespace Aisino.Fwkp.DataMigrationTool.Common
{
    using Microsoft.International.Converters.PinYinConverter;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public static class StringUtils
    {
        private static char[] charChinese = new char[] { 0xff08, 0xff09 };

        private static char[] GetFirstWordByCh(char singleChinese)
        {
            ChineseChar ch;
            List<char> list = new List<char>();
            if (singleChinese < '\x007f')
            {
                list.Add(char.ToUpper(singleChinese));
                return list.ToArray();
            }
            try
            {
                ch = new ChineseChar(singleChinese);
            }
            catch
            {
                return null;
            }
            if (ch.get_PinyinCount() < 1)
            {
                return null;
            }
            ReadOnlyCollection<string> onlys = ch.get_Pinyins();
            for (int i = 0; i < ch.get_PinyinCount(); i++)
            {
                if (!list.Contains(onlys[i][0]))
                {
                    list.Add(onlys[i][0]);
                }
            }
            return list.ToArray();
        }

        public static string[] GetSpellCode(string cnStr)
        {
            string[] strArray = new string[cnStr.Length];
            for (int i = 0; i <= (cnStr.Length - 1); i++)
            {
                strArray[i] = new string(GetFirstWordByCh(cnStr.Substring(i, 1)[0]));
            }
            int num2 = 1;
            foreach (string str in strArray)
            {
                num2 *= str.Length;
            }
            string[] strArray2 = new string[num2];
            for (int j = 0; j < num2; j++)
            {
                string str2 = "";
                int num4 = 1;
                for (int k = 0; k < strArray.Length; k++)
                {
                    num4 *= strArray[k].Length;
                    str2 = str2 + strArray[k][(j / (num2 / num4)) % strArray[k].Length];
                }
                strArray2[j] = str2;
            }
            return strArray2;
        }

        public static string[] GetSpellCodeNoException(string cnStr)
        {
            try
            {
                foreach (char ch in charChinese)
                {
                    int index = cnStr.IndexOf(ch);
                    if (index > -1)
                    {
                        cnStr = cnStr.Substring(0, index);
                        break;
                    }
                }
                return GetSpellCode(cnStr);
            }
            catch
            {
                return new string[] { "" };
            }
        }
    }
}

