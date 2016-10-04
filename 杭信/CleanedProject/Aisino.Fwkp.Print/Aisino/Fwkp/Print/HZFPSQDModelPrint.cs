namespace Aisino.Fwkp.Print
{
    using Aisino.Framework.Plugin.Core.Controls.PrintControl;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class HZFPSQDModelPrint : IPrint
    {
        private string string_0;
        private string string_1;

        public HZFPSQDModelPrint(string[] string_2)
        {
            
            this.string_0 = "zyhpxxb";
            this.string_1 = "hyhpxxb";
            base.method_0(string_2);
        }

        public HZFPSQDModelPrint(string string_2)
        {
            
            this.string_0 = "zyhpxxb";
            this.string_1 = "hyhpxxb";
            base.method_0(new object[] { string_2 });
        }

        protected override DataDict DictCreate(params object[] args)
        {
            try
            {
                base.ZYFPLX = "";
                if (args == null)
                {
                    base._isPrint = "0003";
                    return null;
                }
                if (args.Length > 0)
                {
                    base._isZYPT = false;
                    string str4 = args[0].ToString();
                    char ch = str4[0];
                    str4 = str4.Substring(1);
                    string str = "s";
                    string str2 = "9956107100";
                    string str3 = "593803";
                    Fpxx fpxx = null;
                    switch (ch)
                    {
                        case '0':
                        {
                            str = "s";
                            fpxx = Aisino.Fwkp.Print.Common.GetPTZYFpxxModel(str, str2, str3, true);
                            fpxx.fhr = "00000001100";
                            object[] objArray4 = new object[] { str4, fpxx };
                            return this.DictCreate_ZYHZFPXXB(objArray4);
                        }
                        case '1':
                        {
                            str = "f";
                            fpxx = Aisino.Fwkp.Print.Common.GetHWYSFpxxModel(str, str2, str3, true);
                            fpxx.yshwxx = "打印测试";
                            fpxx.fhr = "0000000110";
                            object[] objArray2 = new object[] { str4, fpxx };
                            return this.DictCreate_HYHZFPXXB(objArray2);
                        }
                    }
                    base._isPrint = "0006";
                    return null;
                }
                return null;
            }
            catch (Exception exception)
            {
                base.loger.Error("[创建数据字典]：" + exception.Message);
                base._isPrint = "0003";
                return null;
            }
        }

        protected DataDict DictCreate_HYHZFPXXB(params object[] args)
        {
            if (args.Length >= 2)
            {
                Fpxx fpxx = args[1] as Fpxx;
                List<Dictionary<string, object>> listDict = new List<Dictionary<string, object>>();
                if (fpxx != null)
                {
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    string fpdm = fpxx.fpdm;
                    string fphm = fpxx.fphm;
                    item.Add("kprq", fpxx.kprq);
                    item.Add("cyrmc", fpxx.cyrmc);
                    item.Add("cyrnsrsbh", fpxx.cyrnsrsbh);
                    item.Add("spfmc", fpxx.spfmc);
                    item.Add("spfnsrsbh", fpxx.spfnsrsbh);
                    item.Add("shrmc", fpxx.shrmc);
                    item.Add("shrnsrsbh", fpxx.shrnsrsbh);
                    item.Add("fhrmc", fpxx.fhrmc);
                    item.Add("fhrnsrsbh", fpxx.fhrnsrsbh);
                    DataTable table = new DataTable();
                    table.Columns.Add("fyxm");
                    table.Columns.Add("je");
                    if (fpxx.Qdxx != null)
                    {
                        item.Add("dt", this.method_6(fpxx));
                    }
                    item.Add("yshwxx", fpxx.yshwxx);
                    item.Add("hjje", Aisino.Fwkp.Print.Common.ObjectToDouble(fpxx.je).ToString("f2"));
                    double num2 = Aisino.Fwkp.Print.Common.ObjectToDouble(fpxx.sLv) * 100.0;
                    item.Add("sLv", num2.ToString() + "%");
                    double num = Aisino.Fwkp.Print.Common.ObjectToDouble(fpxx.se);
                    if ((num == 0.0) && (num2 == 0.0))
                    {
                        item.Add("hjse", "***");
                    }
                    else
                    {
                        item.Add("hjse", "￥" + num.ToString("f2"));
                    }
                    item.Add("jqbh", fpxx.jqbh);
                    item.Add("czch", fpxx.czch);
                    item.Add("ccdw", fpxx.ccdw);
                    string fhr = fpxx.fhr;
                    if (fhr.Length == 10)
                    {
                        item.Add("S", fhr[0] == '1');
                        item.Add("S1", fhr[1] == '1');
                        item.Add("S2", fhr[2] == '1');
                        item.Add("S21", fhr[3] == '1');
                        item.Add("S22", fhr[4] == '1');
                        item.Add("S23", fhr[5] == '1');
                        item.Add("S24", fhr[6] == '1');
                        item.Add("C", fhr[7] == '1');
                        item.Add("C1", fhr[8] == '1');
                        item.Add("C2", fhr[9] == '1');
                        if (fhr[2] == '1')
                        {
                            item.Add("fpdm1", fpdm);
                            item.Add("fphm1", fphm);
                        }
                        if (fhr[7] == '1')
                        {
                            item.Add("fpdm2", fpdm);
                            item.Add("fphm2", fphm);
                        }
                    }
                    if (fpxx.hxm == null)
                    {
                        item.Add("hpxxbbh", "");
                    }
                    else
                    {
                        item.Add("hpxxbbh", fpxx.hxm);
                    }
                    listDict.Add(item);
                    base.Id = this.string_1;
                    return new DataDict(listDict);
                }
                base._isPrint = "0006";
            }
            return null;
        }

        protected DataDict DictCreate_ZYHZFPXXB(params object[] args)
        {
            if (args.Length >= 2)
            {
                Fpxx fpxx = args[1] as Fpxx;
                List<Dictionary<string, object>> listDict = new List<Dictionary<string, object>>();
                if (fpxx != null)
                {
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    string fpdm = fpxx.fpdm;
                    string fphm = fpxx.fphm;
                    item.Add("kprq", fpxx.kprq);
                    item.Add("xfmc", fpxx.xfmc);
                    item.Add("xfsh", fpxx.xfsh);
                    item.Add("gfmc", fpxx.gfmc);
                    item.Add("gfsh", fpxx.gfsh);
                    DataTable table = new DataTable();
                    table.Columns.Add("hwmc");
                    table.Columns.Add("sl");
                    table.Columns.Add("dj");
                    table.Columns.Add("je");
                    table.Columns.Add("slv");
                    table.Columns.Add("se");
                    if (fpxx.Qdxx != null)
                    {
                        foreach (Dictionary<SPXX, string> dictionary2 in fpxx.Qdxx)
                        {
                            DataRow row = table.NewRow();
                            string str4 = fpxx.Get_Print_Dj(dictionary2, 0, null);
                            object[] objArray2 = new object[] { str4, 12 };
                            object[] objArray3 = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPPrecisionShareMethod", objArray2);
                            if ((objArray3 != null) && (objArray3.Length > 0))
                            {
                                row["dj"] = Aisino.Fwkp.Print.Common.FormatString(objArray3[0].ToString());
                            }
                            else
                            {
                                row["dj"] = "";
                                base.loger.Error("精度四舍五入错误");
                            }
                            row["hwmc"] = dictionary2[SPXX.SPMC];
                            if (dictionary2[SPXX.SL] == "0")
                            {
                                row["sl"] = string.Empty;
                            }
                            else
                            {
                                object[] objArray6 = new object[] { dictionary2[SPXX.SL], 9 };
                                object[] objArray4 = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPPrecisionShareMethod", objArray6);
                                if ((objArray4 != null) && (objArray4.Length > 0))
                                {
                                    row["sl"] = objArray4[0].ToString();
                                }
                                else
                                {
                                    row["sl"] = "";
                                    base.loger.Error("精度四舍五入错误");
                                }
                            }
                            row["je"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary2[SPXX.JE]).ToString("f2");
                            if (dictionary2[SPXX.SLV] == "")
                            {
                                row["slv"] = "";
                                row["se"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary2[SPXX.SE]).ToString("f2");
                            }
                            else if (Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary2[SPXX.SLV]) == 0.0)
                            {
                                row["slv"] = "***";
                                row["se"] = "***";
                            }
                            else if ((fpxx.fplx == FPLX.ZYFP) && (fpxx.sLv == "0.05"))
                            {
                                row["slv"] = string.Empty;
                                row["se"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary2[SPXX.SE]).ToString("f2");
                            }
                            else
                            {
                                row["slv"] = ((Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary2[SPXX.SLV]) * 100.0)).ToString() + "%";
                                row["se"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary2[SPXX.SE]).ToString("f2");
                            }
                            table.Rows.Add(row);
                        }
                        item.Add("dt", table);
                    }
                    item.Add("hjje", Aisino.Fwkp.Print.Common.ObjectToDouble(fpxx.je).ToString("f2"));
                    double num2 = Aisino.Fwkp.Print.Common.ObjectToDouble(fpxx.se);
                    if (fpxx.sLv == "")
                    {
                        item.Add("hjse", ((char) 0xffe5) + num2.ToString("f2"));
                    }
                    else if ((num2 == 0.0) && (Aisino.Fwkp.Print.Common.ObjectToDouble(fpxx.sLv) == 0.0))
                    {
                        item.Add("hjse", "***");
                    }
                    else
                    {
                        item.Add("hjse", ((char) 0xffe5) + Aisino.Fwkp.Print.Common.ObjectToDouble(fpxx.se).ToString("f2"));
                    }
                    string fhr = fpxx.fhr;
                    if (fhr.Length == 11)
                    {
                        item.Add("G", fhr[0] == '1');
                        item.Add("G1", fhr[1] == '1');
                        item.Add("G2", fhr[2] == '1');
                        item.Add("G21", fhr[3] == '1');
                        item.Add("G22", fhr[4] == '1');
                        item.Add("G23", fhr[5] == '1');
                        item.Add("G24", fhr[6] == '1');
                        item.Add("X", fhr[7] == '1');
                        item.Add("X1", fhr[8] == '1');
                        item.Add("X2", fhr[9] == '1');
                        item.Add("hzfw", fhr[10] == '1');
                        if (fhr[2] == '1')
                        {
                            item.Add("fpdm1", fpdm);
                            item.Add("fphm1", fphm);
                        }
                        if (fhr[7] == '1')
                        {
                            item.Add("fpdm2", fpdm);
                            item.Add("fphm2", fphm);
                        }
                    }
                    if (fpxx.hxm == null)
                    {
                        item.Add("hpxxbbh", "");
                    }
                    else
                    {
                        item.Add("hpxxbbh", fpxx.hxm);
                    }
                    listDict.Add(item);
                    base.Id = this.string_0;
                    return new DataDict(listDict);
                }
                base._isPrint = "0006";
            }
            return null;
        }

        private DataTable method_6(Fpxx fpxx_0)
        {
            if (((fpxx_0 == null) || (fpxx_0.Qdxx == null)) || (fpxx_0.Qdxx.Count == 0))
            {
                return null;
            }
            DataTable table = new DataTable();
            if (fpxx_0.Qdxx.Count == 1)
            {
                table.Columns.Add("fyxm1");
                table.Columns.Add("je1");
                DataRow row2 = table.NewRow();
                row2["fyxm1"] = "费用项目";
                row2["je1"] = "金额";
                table.Rows.Add(row2);
                DataRow row3 = table.NewRow();
                Dictionary<SPXX, string> dictionary2 = fpxx_0.Qdxx[0];
                row3["fyxm1"] = dictionary2[SPXX.SPMC];
                row3["je1"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary2[SPXX.JE]).ToString("f2");
                table.Rows.Add(row3);
                return table;
            }
            table.Columns.Add("fyxm1");
            table.Columns.Add("je1");
            table.Columns.Add("fyxm2");
            table.Columns.Add("je2");
            DataRow row4 = table.NewRow();
            row4["fyxm1"] = "费用项目";
            row4["je1"] = "金额";
            row4["fyxm2"] = "费用项目";
            row4["je2"] = "金额";
            table.Rows.Add(row4);
            for (int i = 0; i < fpxx_0.Qdxx.Count; i += 2)
            {
                DataRow row = table.NewRow();
                Dictionary<SPXX, string> dictionary = fpxx_0.Qdxx[i];
                row["fyxm1"] = dictionary[SPXX.SPMC];
                row["je1"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary[SPXX.JE]).ToString("f2");
                if ((i + 1) < fpxx_0.Qdxx.Count)
                {
                    Dictionary<SPXX, string> dictionary3 = fpxx_0.Qdxx[i + 1];
                    row["fyxm2"] = dictionary3[SPXX.SPMC];
                    row["je2"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary3[SPXX.JE]).ToString("f2");
                }
                table.Rows.Add(row);
            }
            return table;
        }
    }
}

