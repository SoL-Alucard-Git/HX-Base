namespace ns2
{
    using System;

    internal class Class22
    {
        public static readonly string string_0;
        public static readonly string string_1;
        public static readonly string string_2;
        public static readonly string string_3;
        public static readonly string string_4;
        public static readonly string string_5;
        public static readonly string string_6;

        static Class22()
        {
            
            string_0 = "02C2511011";
            string_1 = "01B0932011";
            string_2 = "01C3332011";
            string_3 = "01Y0001011";
            string_4 = "01B0932010";
            string_5 = "01C3332010";
            string_6 = "01Y0001010";
        }

        internal Class22()
        {
            
        }

        public static string smethod_0(string string_7)
        {
            switch (string_7)
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

