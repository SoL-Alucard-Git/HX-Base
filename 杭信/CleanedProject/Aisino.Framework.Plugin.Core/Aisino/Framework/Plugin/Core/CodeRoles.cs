namespace Aisino.Framework.Plugin.Core
{
    using System;

    public class CodeRoles
    {
        public static readonly string CORP_PERMIT_SNYQYDM;
        public static readonly string CORP_RATE_XTKCPSCQY;
        public static readonly string CORP_RATE_XTKCPSCQYYCX;
        public static readonly string CORP_RATE_XTSMQY;
        public static readonly string CORP_RATE_XTSMQYYCX;
        public static readonly string CORP_RATE_XTYLFLQY;
        public static readonly string CORP_RATE_XTYLFLQYYCX;

        static CodeRoles()
        {
            
            CORP_PERMIT_SNYQYDM = "02C2511011";
            CORP_RATE_XTKCPSCQY = "01B0932011";
            CORP_RATE_XTYLFLQY = "01C3332011";
            CORP_RATE_XTSMQY = "01Y0001011";
            CORP_RATE_XTKCPSCQYYCX = "01B0932010";
            CORP_RATE_XTYLFLQYYCX = "01C3332010";
            CORP_RATE_XTSMQYYCX = "01Y0001010";
        }

        public CodeRoles()
        {
            
        }

        public static string ChangeXTCodeToName(string string_0)
        {
            switch (string_0)
            {
                case "01B0932011":
                    return "CORP_RATE_XTKCPSCQY";

                case "01C3332011":
                    return "CORP_RATE_XTYLFLQY";

                case "01Y0001011":
                    return "CORP_RATE_XTSMQY";

                case "01B0932010":
                    return "CORP_RATE_XTKCPSCQYYCX";

                case "01C3332010":
                    return "CORP_RATE_XTYLFLQYYCX";

                case "01Y0001010":
                    return "CORP_RATE_XTSMQYYCX";
            }
            return string.Empty;
        }
    }
}

