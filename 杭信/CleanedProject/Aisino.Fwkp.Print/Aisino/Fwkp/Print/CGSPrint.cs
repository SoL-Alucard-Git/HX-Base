namespace Aisino.Fwkp.Print
{
    using Aisino.Framework.Plugin.Core.Controls.PrintControl;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CGSPrint : IPrint
    {
        private char[] char_0;
        private string string_0;

        public CGSPrint(string string_1, string string_2, int int_0)
        {
            
            this.char_0 = new char[] { 
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'R', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 
                'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 
                'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 
                'w', 'x', 'y', 'z'
             };
            this.string_0 = "cgssbb";
            base.method_0(new object[] { string_1, string_2, int_0, "_CGS" });
        }

        public static IPrint Create(string string_1, string string_2, int int_0)
        {
            return new CGSPrint(string_1, string_2, int_0);
        }

        protected override DataDict DictCreate(params object[] args)
        {
            try
            {
                if (args == null)
                {
                    base._isPrint = "0003";
                    return null;
                }
                base._IsHZFW = false;
                if (args.Length >= 3)
                {
                    args[0].ToString();
                    args[1].ToString();
                    Convert.ToInt32(args[2]);
                    return this.method_7(args);
                }
                return null;
            }
            catch (Exception exception)
            {
                base.loger.Error("[创建数据字典]：" + exception.ToString());
                base._isPrint = "0003";
                return null;
            }
        }

        private string method_6(int int_0, int int_1, string string_1)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random(DateTime.Now.Second);
            string str = DateTime.Now.ToString("yyyyMMddHHMMSS");
            for (int i = 0; i < int_1; i++)
            {
                builder.Append(str);
                for (int j = 0; j < int_0; j++)
                {
                    builder.Append(this.char_0[random.Next(0, this.char_0.Length)]);
                }
                if (i < (int_1 - 1))
                {
                    builder.Append(string_1);
                }
            }
            return builder.ToString();
        }

        private DataDict method_7(params object[] args)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("date", DateTime.Now.ToString("yyyy年MM月dd日"));
            dict.Add("hydm", "110111");
            dict.Add("zclxdm", "免税的");
            dict.Add("nsrmc", "航天信息股份有限公司");
            dict.Add("nsrzjmc", "身份证");
            dict.Add("zjhm", "110101222666888");
            dict.Add("lxdh", "010-88891111");
            dict.Add("yzbm", "1000000");
            dict.Add("dz", "北京市海淀区");
            dict.Add("qc", true);
            dict.Add("mtc", false);
            dict.Add("dc", false);
            dict.Add("gc", false);
            dict.Add("nynsc", false);
            dict.Add("sjqymc", "航天信息企业");
            dict.Add("cpxh", "大车1100");
            dict.Add("clsbdh", "代号007");
            dict.Add("fdjhm", "NB007");
            dict.Add("fphm", "1231231231");
            dict.Add("je", "12313123");
            dict.Add("jwfy", "222");
            dict.Add("gswsjg", "3333");
            dict.Add("gs", "4444");
            dict.Add("xfs", "xfs");
            dict.Add("gzrq", DateTime.Now.ToString("yyyy-MM-dd"));
            dict.Add("mstj", "免税条件大大的有");
            dict.Add("sbjsjg", "555");
            dict.Add("jsjg", "666");
            dict.Add("ynse", "10000");
            dict.Add("jmse", "1000");
            dict.Add("snse", "9000");
            dict.Add("sqr", "老大");
            dict.Add("dlrmc", "狗蛋");
            dict.Add("jbr", "二毛");
            dict.Add("jbrzjmc", "身份证");
            dict.Add("jbrzjhm", "110101333434");
            dict.Add("jsr", "终结者");
            dict.Add("jsrq", DateTime.Now.ToString("yyyy-MM-dd"));
            dict.Add("bz", "备注：天天忙shi了");
            string str = this.method_6(200, 1, " ");
            dict.Add("pdf417", str);
            base.Id = this.string_0;
            return new DataDict(dict);
        }
    }
}

