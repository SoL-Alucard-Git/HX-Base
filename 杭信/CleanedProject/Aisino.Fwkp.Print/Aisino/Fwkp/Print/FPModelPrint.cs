namespace Aisino.Fwkp.Print
{
    using Aisino.Framework.Plugin.Core.Controls.PrintControl;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class FPModelPrint : IPrint
    {
        private string string_0;
        private string string_1;
        private string string_10;
        private string string_2;
        private string string_3;
        private string string_4;
        private string string_5;
        private string string_6;
        private string string_7;
        private string string_8;
        private string string_9;

        public FPModelPrint(string string_11, string string_12, int int_0)
        {
            
            this.string_0 = "ptfp84";
            this.string_1 = "ptfp108";
            this.string_2 = "ptfphxm";
            this.string_3 = "zyfp84";
            this.string_4 = "zyfp108";
            this.string_5 = "zyfphxm";
            this.string_6 = "xtzyfp84";
            this.string_7 = "xtzyfp108";
            this.string_8 = "hwysyfp";
            this.string_9 = "jdcfp_new";
            this.string_10 = "jdcfp_old";
            string str = string_11;
            if (str != null)
            {
                if ((str == "c") || (str == "s"))
                {
                    base.dyfp = Aisino.Fwkp.Print.Common.GetPTZYFpxxModel(string_11, string_12, int_0.ToString(), false);
                }
                else if (str == "f")
                {
                    base.dyfp = Aisino.Fwkp.Print.Common.GetHWYSFpxxModel(string_11, string_12, int_0.ToString(), false);
                }
                else if (!(str == "j"))
                {
                    if (!(str == "q"))
                    {
                        goto Label_0128;
                    }
                    base.dyfp = Aisino.Fwkp.Print.Common.GetJSFpxxModel(string_11, string_12, int_0.ToString(), false);
                }
                else
                {
                    base.dyfp = Aisino.Fwkp.Print.Common.GetJDCFpxxModel(string_11, string_12, int_0.ToString());
                }
                goto Label_012F;
            }
        Label_0128:
            base.dyfp = null;
        Label_012F:
            if (base.dyfp == null)
            {
                base._isPrint = "0006";
            }
            else if (string_11 == "q")
            {
                int num = Convert.ToInt32(base.dyfp.yysbz.Substring(6, 2), 0x10) - 1;
                base.method_0(new object[] { string_11, string_12, int_0, "_FP", num });
            }
            else
            {
                base.method_0(new object[] { string_11, string_12, int_0, "_FP" });
            }
        }

        public static IPrint Create(string string_11, string string_12, int int_0, bool bool_1 = true)
        {
            if (bool_1)
            {
                return new FPModelPrint(string_11, string_12, int_0);
            }
            return new QDPrint(string_11, string_12, int_0);
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
                    switch (args[0].ToString())
                    {
                        case "c":
                        case "s":
                            return this.DictCreate_ZYAndPT(args);

                        case "q":
                            return this.DictCreate_JP(args);

                        case "j":
                            return this.DictCreate_JDCFP(args);

                        case "f":
                            return this.DictCreate_HWYSYFP(args);
                    }
                    base._isPrint = "0006";
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

        protected DataDict DictCreate_HWYSYFP(params object[] args)
        {
            Fpxx dyfp = base.dyfp;
            dyfp = new InvoiceHandler().ConvertInvoiceToZH(dyfp);
            base._isZYPT = false;
            base.ZYFPLX = "f";
            if (dyfp != null)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("lbdm", dyfp.fpdm);
                dict.Add("fphm", dyfp.fphm);
                DateTime time = Aisino.Fwkp.Print.Common.ObjectToDateTime(dyfp.kprq);
                dict.Add("year", time.Year);
                dict.Add("month", time.Month.ToString("00"));
                dict.Add("day", time.Day.ToString("00"));
                DateTime time2 = new DateTime(0x7dd, 9, 10, 8, 0x22, 30);
                TimeSpan span = (TimeSpan) (DateTime.Now - time2);
                byte[] buffer = AES_Crypt.Encrypt(ToolUtil.GetBytes(span.TotalSeconds.ToString("F1")), new byte[] { 
                    0xff, 0x42, 0xae, 0x95, 11, 0x51, 0xca, 0x15, 0x21, 140, 0x4f, 170, 220, 0x92, 170, 0xed, 
                    0xfd, 0xeb, 0x4e, 13, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
                 }, new byte[] { 0xf2, 0x1f, 0xac, 0x5b, 0x2c, 0xc0, 0xa9, 0xd0, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 });
                if (!base.IsFirstCreate)
                {
                    dyfp.Get_Print_Dj(null, 0, buffer);
                    base.IsFirstCreate = true;
                }
                dict.Add("mw", dyfp.mw);
                dict.Add("cyrmc", dyfp.cyrmc);
                dict.Add("cyrnsrsbh", dyfp.cyrnsrsbh);
                dict.Add("spfmc", dyfp.spfmc);
                dict.Add("spfnsrsbh", dyfp.spfnsrsbh);
                dict.Add("shrmc", dyfp.shrmc);
                dict.Add("shrnsrsbh", dyfp.shrnsrsbh);
                dict.Add("fhrmc", dyfp.fhrmc);
                dict.Add("fhrnsrsbh", dyfp.fhrnsrsbh);
                dict.Add("qyd", dyfp.qyd);
                dict.Add("yshwxx", dyfp.yshwxx);
                DataTable table = this.method_6(dyfp);
                dict.Add("list", table);
                dict.Add("hjje", "￥" + dyfp.je);
                float result = 0f;
                float.TryParse(dyfp.sLv, out result);
                if (result == 0f)
                {
                    dict.Add("sLv", "***");
                    dict.Add("hjse", "***");
                }
                else
                {
                    float num6 = 0f;
                    float.TryParse(dyfp.sLv, out num6);
                    string str2 = string.Format("{0}%", num6 * 100f);
                    dict.Add("sLv", str2);
                    string str3 = "￥" + dyfp.se;
                    dict.Add("hjse", str3);
                }
                dict.Add("jqbh", dyfp.jqbh);
                decimal num5 = Convert.ToDecimal(dyfp.je) + Convert.ToDecimal(dyfp.se);
                dict.Add("jshjdx", ToolUtil.RMBToDaXie(num5));
                string str = "￥" + num5.ToString();
                dict.Add("jshjxx", str);
                dict.Add("czch", dyfp.czch);
                dict.Add("ccdw", dyfp.ccdw);
                dict.Add("zgswjgmc", dyfp.zgswjgmc);
                dict.Add("zgswjgdm", dyfp.zgswjgdm);
                dict.Add("bz", dyfp.bz);
                dict.Add("kpr", dyfp.kpr);
                dict.Add("fhr", dyfp.fhr);
                dict.Add("skr", dyfp.skr);
                if (dyfp.zfbz)
                {
                    dict.Add("zfbz", "作废");
                }
                base.Id = this.string_8;
                return new DataDict(dict);
            }
            base._isPrint = "0006";
            return null;
        }

        protected DataDict DictCreate_JDCFP(params object[] args)
        {
            Fpxx dyfp = base.dyfp;
            dyfp = new InvoiceHandler().ConvertInvoiceToZH(dyfp);
            base._isZYPT = false;
            Dictionary<string, object> dict = new Dictionary<string, object>();
            if (dyfp != null)
            {
                dict.Add("lbdm", dyfp.fpdm);
                dict.Add("fphm", dyfp.fphm);
                dict.Add("jqbh", dyfp.jqbh);
                string str5 = ToolUtil.FormatDateTimeEx(dyfp.kprq);
                dyfp.kprq = Aisino.Fwkp.Print.Common.ObjectToDateTime(str5).Date.ToString("yyyy-MM-dd");
                dict.Add("kprq", dyfp.kprq);
                DateTime time3 = new DateTime(0x7dd, 9, 10, 8, 0x22, 30);
                TimeSpan span = (TimeSpan) (DateTime.Now - time3);
                byte[] buffer = AES_Crypt.Encrypt(ToolUtil.GetBytes(span.TotalSeconds.ToString("F1")), new byte[] { 
                    0xff, 0x42, 0xae, 0x95, 11, 0x51, 0xca, 0x15, 0x21, 140, 0x4f, 170, 220, 0x92, 170, 0xed, 
                    0xfd, 0xeb, 0x4e, 13, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
                 }, new byte[] { 0xf2, 0x1f, 0xac, 0x5b, 0x2c, 0xc0, 0xa9, 0xd0, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 });
                if (!base.IsFirstCreate)
                {
                    dyfp.Get_Print_Dj(null, 0, buffer);
                    base.IsFirstCreate = true;
                }
                dict.Add("mw", dyfp.mw);
                dict.Add("gfmc", dyfp.gfmc);
                dict.Add("gfsh", dyfp.gfsh);
                dict.Add("sccjmc", dyfp.sccjmc);
                dict.Add("sfzhm", dyfp.sfzhm);
                dict.Add("cllx", dyfp.cllx);
                dict.Add("cpxh", dyfp.cpxh);
                dict.Add("cd", dyfp.cd);
                dict.Add("hgzh", dyfp.hgzh);
                dict.Add("jkzmsh", dyfp.jkzmsh);
                dict.Add("sjdh", dyfp.sjdh);
                dict.Add("fdjhm", dyfp.fdjhm);
                dict.Add("clsbdh", dyfp.clsbdh);
                decimal num = Convert.ToDecimal(dyfp.je) + Convert.ToDecimal(dyfp.se);
                dict.Add("jshjdx", ToolUtil.RMBToDaXie(num));
                string str = "￥" + num.ToString();
                dict.Add("jshjxx", str);
                dict.Add("xfmc", dyfp.xfmc);
                dict.Add("xfdh", dyfp.xfdh);
                dict.Add("xfsh", dyfp.xfsh);
                dict.Add("xfzh", dyfp.xfzh);
                dict.Add("xfdz", dyfp.xfdz);
                dict.Add("xfyh", dyfp.xfyh);
                float result = 0f;
                float.TryParse(dyfp.sLv, out result);
                string str2 = string.Format("{0}%", result * 100f);
                if (result != 0f)
                {
                    dict.Add("sLv", str2);
                    string str3 = "￥" + dyfp.se;
                    dict.Add("hjse", str3);
                }
                else
                {
                    dict.Add("sLv", "***");
                    dict.Add("hjse", "***");
                }
                dict.Add("zgswjgmc", dyfp.zgswjgmc);
                dict.Add("zgswjgdm", dyfp.zgswjgdm);
                dict.Add("je", "￥" + dyfp.je);
                dict.Add("dw", dyfp.dw);
                dict.Add("xcrs", dyfp.xcrs);
                dict.Add("kpr", dyfp.kpr);
                if (dyfp.zfbz)
                {
                    dict.Add("zfbz", "作废");
                }
                switch (dyfp.yysbz.Substring(4, 1))
                {
                    case "1":
                        base.Id = this.string_10;
                        base.ZYFPLX = "JO";
                        break;

                    case "2":
                        base.Id = this.string_9;
                        base.ZYFPLX = "JN";
                        break;
                }
                return new DataDict(dict);
            }
            base._isPrint = "0006";
            return new DataDict(dict);
        }

        protected DataDict DictCreate_JP(params object[] args)
        {
            Fpxx dyfp = base.dyfp;
            dyfp = new InvoiceHandler().ConvertInvoiceToZH(dyfp);
            base._isZYPT = true;
            if (dyfp != null)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("fpdm", dyfp.fpdm);
                dict.Add("fphm", dyfp.fphm);
                dict.Add("xfmc", dyfp.xfmc);
                dict.Add("xfsh", dyfp.xfsh);
                dict.Add("jqbh", dyfp.jqbh);
                string str3 = ToolUtil.FormatDateTimeEx(dyfp.kprq);
                dyfp.kprq = Aisino.Fwkp.Print.Common.ObjectToDateTime(str3).Date.ToString("yyyy-MM-dd");
                dict.Add("kprq", dyfp.kprq);
                dict.Add("skr", dyfp.skr);
                dict.Add("gfmc", dyfp.gfmc);
                dict.Add("gfsh", dyfp.gfsh);
                dict.Add("hjje", ((char) 0xffe5) + dyfp.je);
                dict.Add("hjse", ((char) 0xffe5) + dyfp.se);
                decimal num2 = Convert.ToDecimal(dyfp.je) + Convert.ToDecimal(dyfp.se);
                dict.Add("jshjxx", ((char) 0xffe5) + num2.ToString());
                dict.Add("jshjdx", ToolUtil.RMBToDaXie(num2));
                dict.Add("jym", dyfp.jym);
                base.Id = "";
                int num = Convert.ToInt32(dyfp.yysbz.Substring(6, 2), 0x10) - 1;
                string str2 = "NEW76mmX177mm";
                Dictionary<string, int> jsPrintTemplate = ToolUtil.GetJsPrintTemplate();
                if (jsPrintTemplate.Count > 0)
                {
                    foreach (string str in jsPrintTemplate.Keys)
                    {
                        if (jsPrintTemplate[str] == num)
                        {
                            str2 = str;
                        }
                    }
                    if ((num >= 0) && (num <= 10))
                    {
                        string str5 = this.method_9(dyfp);
                        DataTable table3 = this.method_8(dyfp, 0);
                        if (str5 == "H")
                        {
                            dict.Add("list", table3);
                            base.Id = str2 + "H";
                        }
                        else
                        {
                            dict.Add("hwmc", table3.Rows[0]["hwmc"]);
                            dict.Add("dj", table3.Rows[0]["dj"]);
                            dict.Add("sl", table3.Rows[0]["sl"]);
                            dict.Add("je", table3.Rows[0]["je"]);
                            base.Id = str2 + "V";
                        }
                    }
                    else
                    {
                        DataTable table2 = this.method_8(dyfp, 0);
                        dict.Add("list", table2);
                        base.Id = str2;
                    }
                }
                else
                {
                    string str6 = this.method_9(dyfp);
                    DataTable table = this.method_8(dyfp, 0);
                    if (str6 == "H")
                    {
                        dict.Add("list", table);
                        base.Id = PrintTemplate.NEW76mmX177mm.ToString() + "H";
                    }
                    else
                    {
                        dict.Add("hwmc", table.Rows[0]["hwmc"]);
                        dict.Add("dj", table.Rows[0]["dj"]);
                        dict.Add("sl", table.Rows[0]["sl"]);
                        dict.Add("je", table.Rows[0]["je"]);
                        base.Id = PrintTemplate.NEW76mmX177mm.ToString() + "V";
                    }
                }
                return new DataDict(dict);
            }
            base._isPrint = "0006";
            return null;
        }

        protected DataDict DictCreate_ZYAndPT(params object[] args)
        {
            Fpxx dyfp = base.dyfp;
            dyfp = new InvoiceHandler().ConvertInvoiceToZH(dyfp);
            base._isZYPT = true;
            if (dyfp == null)
            {
                base._isPrint = "0006";
                return null;
            }
            Dictionary<string, object> dict = new Dictionary<string, object>();
            if (dyfp.fplx == FPLX.PTFP)
            {
                base.ZYFPLX = "c";
            }
            else
            {
                base.ZYFPLX = "s";
            }
            dict.Add("lbdm", dyfp.fpdm);
            dict.Add("fphm", dyfp.fphm);
            string str3 = ToolUtil.FormatDateTimeEx(dyfp.kprq);
            dyfp.kprq = Aisino.Fwkp.Print.Common.ObjectToDateTime(str3).Date.ToString("yyyy年MM月dd日");
            dict.Add("kprq", dyfp.kprq);
            DateTime time3 = new DateTime(0x7dd, 9, 10, 8, 0x22, 30);
            TimeSpan span = (TimeSpan) (DateTime.Now - time3);
            byte[] buffer = AES_Crypt.Encrypt(ToolUtil.GetBytes(span.TotalSeconds.ToString("F1")), new byte[] { 
                0xff, 0x42, 0xae, 0x95, 11, 0x51, 0xca, 0x15, 0x21, 140, 0x4f, 170, 220, 0x92, 170, 0xed, 
                0xfd, 0xeb, 0x4e, 13, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
             }, new byte[] { 0xf2, 0x1f, 0xac, 0x5b, 0x2c, 0xc0, 0xa9, 0xd0, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 });
            if (!base.IsFirstCreate)
            {
                dyfp.Get_Print_Dj(null, 0, buffer);
                base.IsFirstCreate = true;
            }
            if (dyfp.hzfw)
            {
                dict.Add("hxm", dyfp.hxm);
                base._IsHZFW = true;
            }
            else
            {
                dict.Add("mw", dyfp.mw);
                base._IsHZFW = false;
            }
            dict.Add("bz", dyfp.bz);
            dict.Add("hjje", dyfp.je);
            Aisino.Fwkp.Print.Common.GetYYSBZ(ref dyfp);
            if (dyfp.sLv == "0")
            {
                dict.Add("hjse", "***");
            }
            else
            {
                string str2 = ((char) 0xffe5) + dyfp.se;
                dict.Add("hjse", str2);
            }
            decimal num2 = Convert.ToDecimal(dyfp.je) + Convert.ToDecimal(dyfp.se);
            dict.Add("jshjxx", num2);
            dict.Add("jshjdx", ToolUtil.RMBToDaXie(num2));
            dict.Add("kpr", dyfp.kpr);
            dict.Add("fhr", dyfp.fhr);
            dict.Add("skr", dyfp.skr);
            dict.Add("jmbb", "加密版本:");
            dict.Add("jmbbh", dyfp.jmbbh);
            dict.Add("list", this.method_7(dyfp));
            dict.Add("gfmc", dyfp.gfmc);
            dict.Add("gfsh", dyfp.gfsh);
            dict.Add("gfdzdh", dyfp.gfdzdh);
            dict.Add("gfyhzh", dyfp.gfyhzh);
            dict.Add("xfmc", dyfp.xfmc);
            dict.Add("xfsh", dyfp.xfsh);
            dict.Add("xfdzdh", dyfp.xfdzdh);
            dict.Add("xfyhzh", dyfp.xfyhzh);
            if (dyfp.isRed)
            {
                dict.Add("xxfs", "销项负数");
            }
            if (dyfp.zfbz)
            {
                dict.Add("zfbz", "作废");
            }
            string yysbz = dyfp.yysbz;
            bool flag = false;
            if (yysbz.Length > 2)
            {
                switch (yysbz[2])
                {
                    case '1':
                    case '2':
                        flag = true;
                        break;
                }
            }
            if (flag)
            {
                dict.Add("xtbz", "XT");
            }
            if (dyfp.fplx == FPLX.PTFP)
            {
                dict.Add("jym", dyfp.jym);
            }
            switch (dyfp.fplx)
            {
                case FPLX.ZYFP:
                    if (!dyfp.hzfw)
                    {
                        if (Aisino.Fwkp.Print.Common.IsShuiWuDKSQ(""))
                        {
                            dict.Add("xtbz", "代  开");
                        }
                        if (dyfp.mw.Length < 0x5b)
                        {
                            base.Id = this.string_3;
                        }
                        else
                        {
                            base.Id = this.string_4;
                        }
                    }
                    else
                    {
                        base.Id = this.string_5;
                    }
                    goto Label_04AA;

                case FPLX.PTFP:
                    if (dyfp.Zyfplx != ZYFP_LX.NCP_SG)
                    {
                        if (dyfp.Zyfplx == ZYFP_LX.NCP_XS)
                        {
                            dict.Add("ncpfp", "农产品销售");
                        }
                        else if (Aisino.Fwkp.Print.Common.IsShuiWuDKSQ(""))
                        {
                            dict.Add("dkbz", "代  开");
                        }
                        break;
                    }
                    dict.Add("ncpfp", "收  购");
                    break;

                default:
                    goto Label_04AA;
            }
            if (dyfp.hzfw)
            {
                base.Id = this.string_2;
            }
            else if (dyfp.mw.Length < 0x5b)
            {
                base.Id = this.string_0;
            }
            else
            {
                base.Id = this.string_1;
            }
        Label_04AA:
            return new DataDict(dict);
        }

        private DataTable method_6(Fpxx fpxx_0)
        {
            if (((fpxx_0 == null) || (fpxx_0.Mxxx == null)) || (fpxx_0.Mxxx.Count == 0))
            {
                return null;
            }
            DataTable table = new DataTable();
            if (fpxx_0.Mxxx.Count == 1)
            {
                table.Columns.Add("fyxm1");
                table.Columns.Add("je1");
                DataRow row5 = table.NewRow();
                row5["fyxm1"] = "费用项目";
                row5["je1"] = "金额";
                table.Rows.Add(row5);
                DataRow row6 = table.NewRow();
                Dictionary<SPXX, string> dictionary5 = fpxx_0.Mxxx[0];
                row6["fyxm1"] = dictionary5[SPXX.SPMC];
                row6["je1"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary5[SPXX.JE]).ToString("f2");
                table.Rows.Add(row6);
                return table;
            }
            if ((fpxx_0.Mxxx.Count >= 2) && (fpxx_0.Mxxx.Count <= 12))
            {
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
                for (int i = 0; i < fpxx_0.Mxxx.Count; i += 2)
                {
                    DataRow row2 = table.NewRow();
                    Dictionary<SPXX, string> dictionary3 = fpxx_0.Mxxx[i];
                    row2["fyxm1"] = dictionary3[SPXX.SPMC];
                    row2["je1"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary3[SPXX.JE]).ToString("f2");
                    if ((i + 1) < fpxx_0.Mxxx.Count)
                    {
                        Dictionary<SPXX, string> dictionary6 = fpxx_0.Mxxx[i + 1];
                        row2["fyxm2"] = dictionary6[SPXX.SPMC];
                        row2["je2"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary6[SPXX.JE]).ToString("f2");
                    }
                    table.Rows.Add(row2);
                }
                return table;
            }
            if (fpxx_0.Mxxx.Count > 12)
            {
                table.Columns.Add("fyxm1");
                table.Columns.Add("je1");
                table.Columns.Add("fyxm2");
                table.Columns.Add("je2");
                table.Columns.Add("fyxm3");
                table.Columns.Add("je3");
                DataRow row3 = table.NewRow();
                row3["fyxm1"] = "费用项目";
                row3["je1"] = "金额";
                row3["fyxm2"] = "费用项目";
                row3["je2"] = "金额";
                row3["fyxm3"] = "费用项目";
                row3["je3"] = "金额";
                table.Rows.Add(row3);
                for (int j = 0; j < fpxx_0.Mxxx.Count; j += 3)
                {
                    DataRow row = table.NewRow();
                    Dictionary<SPXX, string> dictionary4 = fpxx_0.Mxxx[j];
                    row["fyxm1"] = dictionary4[SPXX.SPMC];
                    row["je1"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary4[SPXX.JE]).ToString("f2");
                    if ((j + 1) < fpxx_0.Mxxx.Count)
                    {
                        Dictionary<SPXX, string> dictionary2 = fpxx_0.Mxxx[j + 1];
                        row["fyxm2"] = dictionary2[SPXX.SPMC];
                        row["je2"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary2[SPXX.JE]).ToString("f2");
                    }
                    if ((j + 2) < fpxx_0.Mxxx.Count)
                    {
                        Dictionary<SPXX, string> dictionary = fpxx_0.Mxxx[j + 2];
                        row["fyxm3"] = dictionary[SPXX.SPMC];
                        row["je3"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary[SPXX.JE]).ToString("f2");
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        private DataTable method_7(Fpxx fpxx_0)
        {
            if (((fpxx_0 != null) && (fpxx_0.Mxxx != null)) && (fpxx_0.Mxxx.Count != 0))
            {
                DataTable table = new DataTable();
                table.Columns.Add("hwmc");
                table.Columns.Add("ggxh");
                table.Columns.Add("dw");
                table.Columns.Add("sl");
                table.Columns.Add("dj");
                table.Columns.Add("je");
                table.Columns.Add("slv");
                table.Columns.Add("se");
                float result = 0f;
                float.TryParse(fpxx_0.sLv, out result);
                foreach (Dictionary<SPXX, string> dictionary in fpxx_0.Mxxx)
                {
                    DataRow row = table.NewRow();
                    string str = fpxx_0.Get_Print_Dj(dictionary, 0, null);
                    object[] objArray2 = new object[] { str, 12 };
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
                    row["hwmc"] = dictionary[SPXX.SPMC];
                    row["ggxh"] = dictionary[SPXX.GGXH];
                    row["dw"] = dictionary[SPXX.JLDW];
                    if (dictionary[SPXX.SL] == "0")
                    {
                        row["sl"] = string.Empty;
                    }
                    else
                    {
                        object[] objArray6 = new object[] { dictionary[SPXX.SL], 9 };
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
                    row["je"] = dictionary[SPXX.JE];
                    if (dictionary[SPXX.SLV] != "")
                    {
                        float num2 = 0f;
                        float.TryParse(dictionary[SPXX.SLV], out num2);
                        if (num2 == 0f)
                        {
                            row["slv"] = "***";
                            row["se"] = "***";
                        }
                        else if ((fpxx_0.fplx == FPLX.ZYFP) && (num2 == 0.05f))
                        {
                            row["slv"] = "";
                            row["se"] = dictionary[SPXX.SE];
                        }
                        else
                        {
                            row["slv"] = dictionary[SPXX.SLV];
                            row["se"] = dictionary[SPXX.SE];
                        }
                    }
                    else
                    {
                        row["slv"] = "";
                        row["se"] = dictionary[SPXX.SE];
                    }
                    table.Rows.Add(row);
                }
                return table;
            }
            base.loger.Error("发票明细信息为空");
            return null;
        }

        private DataTable method_8(Fpxx fpxx_0, int int_0 = 0)
        {
            if (((fpxx_0 != null) && (fpxx_0.Mxxx != null)) && (fpxx_0.Mxxx.Count != 0))
            {
                DataTable table = new DataTable();
                table.Columns.Add("hwmc");
                table.Columns.Add("dj");
                table.Columns.Add("sl");
                table.Columns.Add("je");
                int num = 10;
                int num2 = 7;
                if (int_0 == 0)
                {
                    num = 10;
                    num2 = 7;
                }
                else
                {
                    num = 8;
                    num2 = 6;
                }
                float result = 0f;
                float.TryParse(fpxx_0.sLv, out result);
                foreach (Dictionary<SPXX, string> dictionary in fpxx_0.Mxxx)
                {
                    DataRow row = table.NewRow();
                    row["hwmc"] = dictionary[SPXX.SPMC];
                    string str = fpxx_0.Get_Print_Dj(dictionary, 0, null);
                    object[] objArray2 = new object[] { str, num };
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
                    if (dictionary[SPXX.SL] == "0")
                    {
                        row["sl"] = string.Empty;
                    }
                    else
                    {
                        object[] objArray5 = new object[] { dictionary[SPXX.SL], num2 };
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
                    row["je"] = dictionary[SPXX.JE];
                    table.Rows.Add(row);
                }
                return table;
            }
            base.loger.Error("发票明细信息为空");
            return null;
        }

        private string method_9(Fpxx fpxx_0)
        {
            if (fpxx_0.Mxxx != null)
            {
                if (fpxx_0.Mxxx.Count > 1)
                {
                    return "H";
                }
                if (fpxx_0.Mxxx.Count == 1)
                {
                    return "V";
                }
            }
            return "H";
        }
    }
}

