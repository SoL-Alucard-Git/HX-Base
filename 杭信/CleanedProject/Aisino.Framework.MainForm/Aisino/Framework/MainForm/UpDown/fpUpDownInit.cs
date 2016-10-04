namespace Aisino.Framework.MainForm.UpDown
{
    using Aisino.Framework.Plugin.Core.Util;
    using ns4;
    using System;
    using System.Collections.Generic;

    public class fpUpDownInit
    {
        private Class100 class100_0;
        private Class91 class91_0;

        public fpUpDownInit()
        {
            
            this.class100_0 = new Class100();
            this.class91_0 = new Class91();
        }

        public void init()
        {
            if ((Class97.dataTable_0 == null) || (Class97.dataTable_0.Columns.Count < 10))
            {
                this.class100_0.method_0();
            }
            Class87.list_1 = new List<Dictionary<string, object>>();
            Class87.list_0 = new List<Dictionary<string, object>>();
            this.class91_0.method_0();
        }

        private void method_0()
        {
            try
            {
                string str = PropertyUtil.GetValue("Debug", "0").ToString();
                if (!string.IsNullOrEmpty(str) && str.Equals("1"))
                {
                    Class101.bool_0 = true;
                    int num2 = int.Parse(PropertyUtil.GetValue("iTotalFPNum", "10000"));
                    if (num2 > 0)
                    {
                        Class87.int_0 = num2;
                    }
                    int num = int.Parse(PropertyUtil.GetValue("overTimeToValid", "30"));
                    if (num > 0)
                    {
                        Class87.int_4 = num;
                    }
                    string str3 = PropertyUtil.GetValue("sSaveTest", "0").ToString();
                    if (!string.IsNullOrEmpty(str3) && str3.Equals("1"))
                    {
                        Class103.bool_0 = true;
                    }
                    string str2 = PropertyUtil.GetValue("ShowDebugUPDown", "0").ToString();
                    if (!string.IsNullOrEmpty(str2) && str2.Equals("1"))
                    {
                        Class101.bool_0 = true;
                    }
                }
            }
            catch (Exception exception)
            {
                Class101.smethod_1("获取用户设置失败！" + exception.ToString());
            }
        }
    }
}

