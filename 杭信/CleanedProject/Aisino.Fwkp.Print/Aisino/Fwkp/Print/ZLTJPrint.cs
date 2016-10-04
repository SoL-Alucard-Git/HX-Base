namespace Aisino.Fwkp.Print
{
    using Aisino.Framework.Plugin.Core.Controls.PrintControl;
    using System;
    using System.Collections.Generic;

    public class ZLTJPrint : IPrint
    {
        public ZLTJPrint(string string_0) : base("zltj")
        {
            
            base.method_0(new object[] { string_0 });
        }

        protected override DataDict DictCreate(params object[] args)
        {
            base.ZYFPLX = "";
            string str = args[0] as string;
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("ssrq", str);
            return new DataDict(dict);
        }
    }
}

