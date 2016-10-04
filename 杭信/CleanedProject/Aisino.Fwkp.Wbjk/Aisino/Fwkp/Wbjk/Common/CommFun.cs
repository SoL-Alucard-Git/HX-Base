namespace Aisino.Fwkp.Wbjk.Common
{
    using System;
    using System.Collections.Generic;

    public class CommFun
    {
        public static int StrSplit(string strSrc, string[] strDest, char chSplitter, int nMaxSubStringCount)
        {
            string str = "";
            int num = 0;
            int num2 = 0;
            while ((num < strSrc.Length) && (num2 < nMaxSubStringCount))
            {
                str = str + strSrc[num];
                if (strSrc[num] == chSplitter)
                {
                    strDest[num2++] = str.TrimEnd(new char[] { chSplitter });
                    str = "";
                }
                num++;
            }
            if (((num2 < nMaxSubStringCount) && (strSrc.Length > 0)) && (strSrc[strSrc.Length - 1] != chSplitter))
            {
                strDest[num2++] = str;
            }
            return num2;
        }

        public int StrSplit(string strSrc, ref List<object> strList, char chSplitter, int nMaxSubStringCount)
        {
            int num2;
            strList = new List<object>();
            string[] strDest = new string[nMaxSubStringCount];
            int num = StrSplit(strSrc, strDest, chSplitter, nMaxSubStringCount);
            for (num2 = 0; num2 < num; num2++)
            {
                strDest[num2] = "%" + strDest[num2] + "%";
            }
            for (num2 = 0; num2 < num; num2++)
            {
                strList.Add(strDest[num2]);
            }
            return num;
        }
    }
}

