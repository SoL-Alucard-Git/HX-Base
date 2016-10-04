namespace ns12
{
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using System;
    using System.Collections.Generic;
    using Aisino.Framework.Plugin.Core.Plugin;
    internal sealed class Class134
    {
        private bool[] bool_0;
        private Dictionary<string, int> dictionary_0;
        private static readonly ILog ilog_0;
        private List<Function> list_0;
        private List<Function> list_1;

        static Class134()
        {
            
            ilog_0 = LogUtil.GetLogger<Class134>();
        }

        internal Class134(List<Function> functions)
        {
            
            this.list_0 = functions;
            this.bool_0 = new bool[functions.Count];
            this.list_1 = new List<Function>(functions.Count);
            this.dictionary_0 = new Dictionary<string, int>(functions.Count);
            for (int i = 0; i < functions.Count; i++)
            {
                this.bool_0[i] = false;
                this.dictionary_0[functions[i].Id] = i;
            }
        }

        internal List<Function> method_0()
        {
            for (int i = 0; i < this.list_0.Count; i++)
            {
                string key = this.list_0[i].method_6();
                if ((key != null) && (key != ""))
                {
                    if (this.dictionary_0.ContainsKey(key))
                    {
                        string str2 = this.list_0[this.dictionary_0[key]].method_4();
                        if ((str2 != null) && !(str2 == ""))
                        {
                            this.list_0[this.dictionary_0[key]].method_5(str2 + ',' + this.list_0[i].Id);
                        }
                        else
                        {
                            this.list_0[this.dictionary_0[key]].method_5(this.list_0[i].Id);
                        }
                    }
                    else
                    {
                        ilog_0.WarnFormat("功能不存在：{0}", key);
                    }
                }
                if (this.list_0[i].Properties.ContainsKey("isFirst") && this.list_0[i].Properties["isFirst"].Equals("true"))
                {
                    this.list_1.Add(this.list_0[i]);
                    this.bool_0[i] = true;
                }
            }
            for (int j = 0; j < this.list_0.Count; j++)
            {
                this.method_1(j);
            }
            return this.list_1;
        }

        private void method_1(int int_0)
        {
            if (!this.bool_0[int_0])
            {
                foreach (string str in this.list_0[int_0].method_4().Split(new char[] { ',' }))
                {
                    if ((str != null) && (str.Length != 0))
                    {
                        if (this.dictionary_0.ContainsKey(str))
                        {
                            this.method_1(this.dictionary_0[str]);
                        }
                        else
                        {
                            ilog_0.WarnFormat("功能不存在：{0}", this.list_0[int_0].method_4());
                        }
                    }
                }
                this.list_1.Add(this.list_0[int_0]);
                this.bool_0[int_0] = true;
            }
        }
    }
}

