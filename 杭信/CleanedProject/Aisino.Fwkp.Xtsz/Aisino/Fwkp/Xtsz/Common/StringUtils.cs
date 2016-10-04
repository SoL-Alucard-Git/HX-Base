namespace Aisino.Fwkp.Xtsz.Common
{
    using Aisino.Framework.Plugin.Core.Util;
    using System;

    public static class StringUtils
    {
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
    }
}

