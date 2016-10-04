namespace ns12
{
    using Aisino.Framework.Plugin.Core.Plugin;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal sealed class Class120
    {
        private bool bool_0;
        private Dictionary<string, Class120> dictionary_0;
        private List<Function> list_0;

        public Class120()
        {
            
            this.dictionary_0 = new Dictionary<string, Class120>();
            this.list_0 = new List<Function>();
        }

        internal Dictionary<string, Class120> method_0()
        {
            return this.dictionary_0;
        }

        internal List<Function> method_1()
        {
            return this.list_0;
        }

        internal ArrayList method_2(object object_0, bool bool_1)
        {
            ArrayList list = new ArrayList(this.list_0.Count);
            if (!this.bool_0)
            {
                this.list_0 = new Class134(this.list_0).method_0();
                this.bool_0 = true;
            }
            foreach (Function function in this.list_0)
            {
                ArrayList list2;
                string str4;
                if (bool_1)
                {
                    string str5;
                    function.Properties.TryGetValue("type", out str5);
                    if ((str5 == null) || !str5.Equals("Separator"))
                    {
                        string str;
                        function.Properties.TryGetValue("alwaysPermit", out str);
                        if ((str == null) || !string.Equals(str, bool.TrueString, StringComparison.OrdinalIgnoreCase))
                        {
                            goto Label_0121;
                        }
                    }
                    continue;
                }
                if (function.Properties.TryGetValue("isShow", out str4) && string.Equals(str4, bool.FalseString, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                string str2 = "false";
                if (function.Properties.TryGetValue("runOnce", out str2))
                {
                    string str3 = PropertyUtil.GetValue(function.Id + "HasRunOnce", "0");
                    if (string.Equals(str2, bool.TrueString, StringComparison.OrdinalIgnoreCase) && str3.Equals("1"))
                    {
                        continue;
                    }
                }
            Label_0121:
                list2 = null;
                if (this.dictionary_0.ContainsKey(function.Id))
                {
                    list2 = this.dictionary_0[function.Id].method_2(object_0, bool_1);
                }
                object obj2 = null;
                if (bool_1)
                {
                    obj2 = function.method_9(object_0, list2);
                }
                else
                {
                    obj2 = function.method_8(object_0, list2);
                }
                if (obj2 != null)
                {
                    list.Add(obj2);
                }
            }
            return list;
        }
    }
}

