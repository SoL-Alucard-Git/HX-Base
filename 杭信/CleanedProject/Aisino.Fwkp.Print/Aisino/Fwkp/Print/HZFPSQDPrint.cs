namespace Aisino.Fwkp.Print
{
    using Aisino.Framework.Plugin.Core.Controls.PrintControl;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class HZFPSQDPrint : IPrint
    {
        private string string_0;
        private string string_1;

        public HZFPSQDPrint(string[] string_2)
        {
            
            this.string_0 = "zyhpxxb";
            this.string_1 = "hyhpxxb";
            base.method_0(string_2);
        }

        public HZFPSQDPrint(string string_2)
        {
            
            this.string_0 = "zyhpxxb";
            this.string_1 = "hyhpxxb";
            base.method_0(new object[] { string_2 });
        }

        protected override DataDict DictCreate(params object[] args)
        {
            DataDict dict;
            try
            {
                string str;
                Fpxx fpxx;
                base.ZYFPLX = "";
                if (args == null)
                {
                    base._isPrint = "0003";
                    return null;
                }
                if (args.Length > 0)
                {
                    base._isZYPT = false;
                    for (int i = 0; i < args.Length; i++)
                    {
                        str = args[i].ToString();
                        object[] objArray2 = null;
                        char ch = str[0];
                        str = str.Substring(1);
                        if (ch == '0')
                        {
                            objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Hzfp.HzfpService", new object[] { str });
                        }
                        else
                        {
                            if (ch != '1')
                            {
                                continue;
                            }
                            objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.HzfpHy.HzfpHyService", new object[] { str });
                        }
                        if (objArray2 != null)
                        {
                            fpxx = objArray2[0] as Fpxx;
                            if (fpxx != null)
                            {
                                goto Label_00B5;
                            }
                        }
                    }
                }
                goto Label_0102;
            Label_00B5:;
                object[] objArray = new object[] { str, fpxx };
                switch (fpxx.fplx)
                {
                    case FPLX.ZYFP:
                        return this.DictCreate_ZYHZFPXXB(objArray);

                    case FPLX.HYFP:
                        return this.DictCreate_HYHZFPXXB(objArray);

                    default:
                        base._isPrint = "0006";
                        return null;
                }
            Label_0102:
                dict = null;
            }
            catch (Exception exception)
            {
                base.loger.Error("[创建数据字典]：" + exception.Message);
                base._isPrint = "0003";
                dict = null;
            }
            return dict;
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
                    double num3 = Aisino.Fwkp.Print.Common.ObjectToDouble(fpxx.se);
                    if ((num3 == 0.0) && (num2 == 0.0))
                    {
                        item.Add("hjse", "***");
                        item.Add("sLv", "***");
                    }
                    else
                    {
                        item.Add("hjse", "￥" + num3.ToString("f2"));
                        item.Add("sLv", num2.ToString() + "%");
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
                            string str2 = fpxx.Get_Print_Dj(dictionary2, 0, null);
                            object[] objArray2 = new object[] { str2, 12 };
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
                                object[] objArray5 = new object[] { dictionary2[SPXX.SL], 9 };
                                object[] objArray6 = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPPrecisionShareMethod", objArray5);
                                if ((objArray6 != null) && (objArray6.Length > 0))
                                {
                                    row["sl"] = objArray6[0].ToString();
                                }
                                else
                                {
                                    row["sl"] = "";
                                    base.loger.Error("精度四舍五入错误");
                                }
                            }
                            row["je"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary2[SPXX.JE]).ToString("f2");
                            if (!(fpxx.sLv != ""))
                            {
                                goto Label_0413;
                            }
                            string str3 = dictionary2[SPXX.SLV];
                            if (str3 != null)
                            {
                                if (str3 == "0.05")
                                {
                                    bool flag = false;
                                    flag = fpxx.Zyfplx == ZYFP_LX.HYSY;
                                    if ((fpxx.fplx == FPLX.ZYFP) && flag)
                                    {
                                        row["slv"] = "";
                                        row["se"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary2[SPXX.SE]).ToString("f2");
                                    }
                                    else
                                    {
                                        row["slv"] = dictionary2[SPXX.SLV];
                                        row["se"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary2[SPXX.SE]).ToString("f2");
                                    }
                                }
                                else if (!(str3 == "0.015"))
                                {
                                    if (!(str3 == "0"))
                                    {
                                        goto Label_03D5;
                                    }
                                    row["slv"] = "***";
                                    row["se"] = "***";
                                }
                                else
                                {
                                    row["slv"] = "***";
                                    row["se"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary2[SPXX.SE]).ToString("f2");
                                }
                                goto Label_044F;
                            }
                        Label_03D5:
                            row["slv"] = dictionary2[SPXX.SLV];
                            row["se"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary2[SPXX.SE]).ToString("f2");
                            goto Label_044F;
                        Label_0413:
                            row["slv"] = dictionary2[SPXX.SLV];
                            row["se"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary2[SPXX.SE]).ToString("f2");
                        Label_044F:
                            if (fpxx.yysbz.Substring(8, 1) == "2")
                            {
                                row["slv"] = "***";
                                row["se"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary2[SPXX.SE]).ToString("f2");
                            }
                            table.Rows.Add(row);
                        }
                        item.Add("dt", table);
                    }
                    item.Add("hjje", Aisino.Fwkp.Print.Common.ObjectToDouble(fpxx.je).ToString("f2"));
                    double num = Aisino.Fwkp.Print.Common.ObjectToDouble(fpxx.se);
                    if (fpxx.sLv == "")
                    {
                        item.Add("hjse", ((char) 0xffe5) + num.ToString("f2"));
                    }
                    else if ((num == 0.0) && (Aisino.Fwkp.Print.Common.ObjectToDouble(fpxx.sLv) == 0.0))
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
                DataRow row = table.NewRow();
                row["fyxm1"] = "费用项目";
                row["je1"] = "金额";
                table.Rows.Add(row);
                DataRow row2 = table.NewRow();
                Dictionary<SPXX, string> dictionary = fpxx_0.Qdxx[0];
                row2["fyxm1"] = dictionary[SPXX.SPMC];
                row2["je1"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary[SPXX.JE]).ToString("f2");
                table.Rows.Add(row2);
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
                DataRow row3 = table.NewRow();
                Dictionary<SPXX, string> dictionary3 = fpxx_0.Qdxx[i];
                row3["fyxm1"] = dictionary3[SPXX.SPMC];
                row3["je1"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary3[SPXX.JE]).ToString("f2");
                if ((i + 1) < fpxx_0.Qdxx.Count)
                {
                    Dictionary<SPXX, string> dictionary2 = fpxx_0.Qdxx[i + 1];
                    row3["fyxm2"] = dictionary2[SPXX.SPMC];
                    row3["je2"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary2[SPXX.JE]).ToString("f2");
                }
                table.Rows.Add(row3);
            }
            return table;
        }
    }
}

