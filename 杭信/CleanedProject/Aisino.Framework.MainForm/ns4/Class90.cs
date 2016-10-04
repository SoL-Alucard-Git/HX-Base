namespace ns4
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    internal class Class90
    {
        public Class90()
        {
            
        }

        public bool method_0()
        {
            try
            {
                Class83 class2 = new Class83();
                Class94.dictionary_0 = new Dictionary<string, object>();
                if ((Class94.dictionary_0 != null) && class2.method_0(ref Class94.dictionary_0))
                {
                    Class94.string_0 = Convert.ToString(Class94.dictionary_0["ACCEPT_WEB_SERVER"]);
                    Class94.bool_0 = Convert.ToBoolean(Class94.dictionary_0["UPLOADNOW"]);
                    Class94.bool_1 = Convert.ToBoolean(Class94.dictionary_0["INTERVALFLAG"]);
                    Class94.int_0 = Convert.ToInt32(Class94.dictionary_0["INTERVALTIME"]);
                    Class94.bool_2 = Convert.ToBoolean(Class94.dictionary_0["ACCUMULATEFLAG"]);
                    Class94.int_1 = Convert.ToInt32(Class94.dictionary_0["ACCUMULATENUM"]);
                    int num = Convert.ToInt32(Class94.dictionary_0["DATASIZE"]);
                    if (num > 0)
                    {
                        Class87.int_3 = num;
                        Class87.int_2 = num;
                    }
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                Class101.smethod_1("get Dzdzxx info failed!" + exception.ToString());
                return false;
            }
        }

        public void method_1()
        {
            if (!string.IsNullOrEmpty(Class87.string_4))
            {
                int num = Regex.Matches(Class87.string_4, ";").Count + 1;
                Class87.int_3 = num;
                Class87.int_2 = num;
            }
        }
    }
}

