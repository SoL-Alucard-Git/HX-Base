namespace Aisino.Fwkp.Bmgl.Common
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Microsoft.International.Converters.PinYinConverter;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Forms;

    public static class StringUtils
    {
        private static char[] charChinese = new char[] { (char)0xff08, (char)0xff09 };

        public static int CompareBM(TreeNodeTemp x, TreeNodeTemp y)
        {
            return string.Compare(x.BM, y.BM);
        }

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
                if (ChineseChar.IsValidChar(singleChinese))
                {
                    ch = new ChineseChar(singleChinese);
                }
                else
                {
                    return list.ToArray();
                }
            }
            catch
            {
                throw new ArgumentException("找不到此汉字 : " + singleChinese);
            }
            if (ch.PinyinCount < 1)
            {
                throw new ArgumentException("没有找到此汉字的拼音 : " + singleChinese);
            }
            ReadOnlyCollection<string> onlys = ch.Pinyins;
            for (int i = 0; i < ch.PinyinCount; i++)
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
            List<string> list = new List<string>();
            foreach (string str in strArray)
            {
                if (!(str == string.Empty))
                {
                    list.Add(str);
                    num2 *= str.Length;
                }
            }
            if (list.Count == 0)
            {
                num2 = 0;
            }
            string[] strArray2 = new string[num2];
            for (int j = 0; j < num2; j++)
            {
                string str2 = "";
                int num4 = 1;
                for (int k = 0; k < list.Count; k++)
                {
                    num4 *= list[k].Length;
                    str2 = str2 + list[k][(j / (num2 / num4)) % list[k].Length];
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

        public static string GetSubString(string s, int len)
        {
            if ((s == null) || (s.Length == 0))
            {
                return string.Empty;
            }
            if (ToolUtil.GetByteCount(s) > len)
            {
                for (int i = s.Length; i >= 0; i--)
                {
                    s = s.Substring(0, i);
                    if (ToolUtil.GetByteCount(s) <= len)
                    {
                        return s;
                    }
                }
            }
            return s;
        }

        public static void textBoxYHZH_DZDH_TextChanged(object sender, EventArgs e)
        {
            string[] lines = (sender as AisinoTXT).Lines;
            int selectionStart = (sender as AisinoTXT).SelectionStart;
            string str = string.Empty;
            bool flag = false;
            List<int> list = new List<int>();
            for (int i = 0; i < (sender as AisinoTXT).Lines.Length; i++)
            {
                if (ToolUtil.GetBytes((sender as AisinoTXT).Lines[i]).Length > 100)
                {
                    list.Add(i);
                    flag = true;
                    lines[i] = GetSubString((sender as AisinoTXT).Lines[i], 100).Trim();
                }
            }
            (sender as AisinoTXT).Lines = lines;
            if (flag)
            {
                (sender as AisinoTXT).SelectionStart = selectionStart - 1;
            }
            else
            {
                (sender as AisinoTXT).SelectionStart = selectionStart;
            }
            (sender as AisinoTXT).ScrollToCaret();
            if (list.Count == 1)
            {
                string name = (sender as AisinoTXT).Name;
                string str3 = string.Empty;
                if (name == "textBoxYHZH")
                {
                    str3 = "单条银行账号不能超过100个字符！";
                }
                else if (name == "textBoxDZDH")
                {
                    str3 = "单条地址电话不能超过100个字符！";
                }
                else
                {
                    str3 = "未知控件单条不能超过100个字符！";
                }
                MessageBoxHelper.Show(str3, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (list.Count > 1)
            {
                foreach (int num4 in list)
                {
                    str = str + "第" + ((num4 + 1)).ToString() + "行,";
                }
                MessageBoxHelper.Show(str.Substring(0, str.Length - 1) + "超过100个字符，已自动截取!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}

