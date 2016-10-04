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
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class FPPrint : IPrint
    {
        private string string_0;
        private string string_1;
        private string string_10;
        private string string_11;
        private string string_12;
        private string string_13;
        private string string_14;
        private string string_2;
        private string string_3;
        private string string_4;
        private string string_5;
        private string string_6;
        private string string_7;
        private string string_8;
        private string string_9;

        public FPPrint(string string_15, string string_16, int int_0)
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
            this.string_11 = "dkptfp108";
            this.string_12 = "dkzyfp108";
            this.string_13 = "dkptfp84";
            this.string_14 = "dkzyfp84";
            base.loger.Debug("[发票打印]：原来弹出界面设置的调用方式");
            object[] objArray2 = new object[] { string_15, string_16, int_0 };
            object[] objArray3 = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPChanXunWenBenJieKouShareMethods", objArray2);
            base.dyfp = objArray3[0] as Fpxx;
            if (base.dyfp == null)
            {
                base._isPrint = "0006";
            }
            else
            {
                base.IsFirstCreate = false;
                if (string_15 == "q")
                {
                    int num = Convert.ToInt32(base.dyfp.yysbz.Substring(6, 2), 0x10) - 1;
                    base.method_0(new object[] { string_15, string_16, int_0, "_FP", num });
                    if ((base.dyfp.Mxxx != null) && (base.dyfp.Mxxx.Count == 1))
                    {
                        base.IsShowTaoDaGruoupButton = true;
                    }
                }
                else
                {
                    base.method_0(new object[] { string_15, string_16, int_0, "_FP" });
                }
            }
        }

        public FPPrint(string string_15, string string_16, int int_0, string string_17, int int_1, int int_2, bool bool_1)
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
            this.string_11 = "dkptfp108";
            this.string_12 = "dkzyfp108";
            this.string_13 = "dkptfp84";
            this.string_14 = "dkzyfp84";
            base.loger.Debug("[发票打印]：不弹出对话框的调用方式");
            object[] objArray2 = new object[] { string_15, string_16, int_0 };
            object[] objArray3 = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPChanXunWenBenJieKouShareMethods", objArray2);
            base.dyfp = objArray3[0] as Fpxx;
            if (base.dyfp == null)
            {
                base._isPrint = "0006";
            }
            else
            {
                base.IsFirstCreate = false;
                base.loger.Debug("[发票打印]：根据传入参数打印，top:" + int_1.ToString() + " left:" + int_2.ToString() + "isTaoda: " + bool_1.ToString());
                if (string_15 == "q")
                {
                    int num = Convert.ToInt32(base.dyfp.yysbz.Substring(6, 2), 0x10) - 1;
                    base.method_0(new object[] { string_15, string_16, int_0, "_FP", num, string_17, int_1, int_2, bool_1 });
                    if ((base.dyfp.Mxxx != null) && (base.dyfp.Mxxx.Count == 1))
                    {
                        base.IsShowTaoDaGruoupButton = true;
                    }
                }
                else
                {
                    base.method_0(new object[] { string_15, string_16, int_0, "_FP", string_17, int_1, int_2, bool_1 });
                }
            }
        }

        public static IPrint Create(string string_15, string string_16, int int_0, bool bool_1)
        {
            IPrint.IsZjFlag = false;
            if (bool_1)
            {
                return new FPPrint(string_15, string_16, int_0);
            }
            return new QDPrint(string_15, string_16, int_0);
        }

        public static IPrint Create(string string_15, string string_16, int int_0, bool bool_1, string string_17, int int_1, int int_2, bool bool_2)
        {
            IPrint.IsZjFlag = true;
            if (bool_1)
            {
                return new FPPrint(string_15, string_16, int_0, string_17, int_1, int_2, bool_2);
            }
            return new QDPrint(string_15, string_16, int_0, string_17, int_1, int_2, bool_2);
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

                        case "j":
                            return this.DictCreate_JDCFP(args);

                        case "f":
                            return this.DictCreate_HWYSYFP(args);

                        case "q":
                            return this.DictCreate_JP(args);
                    }
                    base._isPrint = "0006";
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
                    base.dyfp.bz = dyfp.bz;
                    base.dyfp.yshwxx = dyfp.yshwxx;
                    base.dyfp.mw = dyfp.mw;
                }
                if (dyfp.zfbz && (Aisino.Fwkp.Print.Common.ObjectToDouble(dyfp.je) == 0.0))
                {
                    dict.Add("mw", "");
                }
                else
                {
                    dict.Add("mw", dyfp.mw);
                }
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
                DataTable table = this.method_7(dyfp);
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
                    float num = 0f;
                    float.TryParse(dyfp.sLv, out num);
                    string str = string.Format("{0}%", num * 100f);
                    dict.Add("sLv", str);
                    string str2 = "￥" + dyfp.se;
                    dict.Add("hjse", str2);
                }
                dict.Add("jqbh", dyfp.jqbh);
                decimal num5 = Convert.ToDecimal(dyfp.je) + Convert.ToDecimal(dyfp.se);
                dict.Add("jshjdx", ToolUtil.RMBToDaXie(num5));
                string str4 = "￥" + num5.ToString();
                dict.Add("jshjxx", str4);
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
                if (Aisino.Fwkp.Print.ReadXml.int_0 == 1)
                {
                    string str3 = this.method_8(dyfp);
                    dict.Add("qrm", str3);
                }
                base.IsFirstCreate = true;
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
                string str4 = ToolUtil.FormatDateTimeEx(dyfp.kprq);
                dyfp.kprq = Aisino.Fwkp.Print.Common.ObjectToDateTime(str4).Date.ToString("yyyy-MM-dd");
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
                    base.dyfp.mw = dyfp.mw;
                }
                if (dyfp.zfbz && (Aisino.Fwkp.Print.Common.ObjectToDouble(dyfp.je) == 0.0))
                {
                    dict.Add("mw", "");
                }
                else
                {
                    dict.Add("mw", dyfp.mw);
                }
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
                decimal num5 = Convert.ToDecimal(dyfp.je) + Convert.ToDecimal(dyfp.se);
                dict.Add("jshjdx", ToolUtil.RMBToDaXie(num5));
                string str9 = "￥" + num5.ToString();
                dict.Add("jshjxx", str9);
                dict.Add("xfmc", dyfp.xfmc);
                dict.Add("xfdh", dyfp.xfdh);
                dict.Add("xfsh", dyfp.xfsh);
                dict.Add("xfzh", dyfp.xfzh);
                dict.Add("xfdz", dyfp.xfdz);
                dict.Add("xfyh", dyfp.xfyh);
                if (dyfp.skr.Contains("#%"))
                {
                    int index = dyfp.skr.IndexOf("#%");
                    string str2 = "";
                    if (index != -1)
                    {
                        str2 = dyfp.skr.Substring(index + 2);
                    }
                    str2 = Aisino.Fwkp.Print.Common.smethod_4(str2);
                    if (str2 != "")
                    {
                        dict.Add("sLv", str2);
                    }
                    else
                    {
                        float result = 0f;
                        float.TryParse(dyfp.sLv, out result);
                        string str6 = string.Format("{0}%", result * 100f);
                        dict.Add("sLv", str6);
                    }
                    string str8 = "￥" + dyfp.se;
                    dict.Add("hjse", str8);
                }
                else
                {
                    float num2 = 0f;
                    float.TryParse(dyfp.sLv, out num2);
                    string str3 = string.Format("{0}%", num2 * 100f);
                    if (num2 != 0f)
                    {
                        dict.Add("sLv", str3);
                        string str7 = "￥" + dyfp.se;
                        dict.Add("hjse", str7);
                    }
                    else
                    {
                        dict.Add("sLv", "***");
                        dict.Add("hjse", "***");
                    }
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
                if (Aisino.Fwkp.Print.ReadXml.int_0 == 1)
                {
                    string str5 = this.method_8(dyfp);
                    dict.Add("qrm", str5);
                }
                base.IsFirstCreate = true;
                return new DataDict(dict);
            }
            base._isPrint = "0006";
            return new DataDict(dict);
        }

        protected DataDict DictCreate_JP(params object[] args)
        {
            Fpxx dyfp = base.dyfp;
            dyfp = new InvoiceHandler().ConvertInvoiceToZH(dyfp);
            base._isZYPT = false;
            if (dyfp != null)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("fpdm", dyfp.fpdm);
                dict.Add("fphm", dyfp.fphm);
                dict.Add("xfmc", dyfp.xfmc);
                dict.Add("xfsh", dyfp.xfsh);
                dict.Add("jqbh", dyfp.jqbh);
                string str2 = ToolUtil.FormatDateTimeEx(dyfp.kprq);
                dyfp.kprq = Aisino.Fwkp.Print.Common.ObjectToDateTime(str2).Date.ToString("yyyy-MM-dd");
                dict.Add("kprq", dyfp.kprq);
                dict.Add("skr", dyfp.skr);
                dict.Add("gfmc", dyfp.gfmc);
                dict.Add("gfsh", dyfp.gfsh);
                dict.Add("hjje", ((char) 0xffe5) + dyfp.je);
                dict.Add("hjse", ((char) 0xffe5) + dyfp.se);
                decimal num = Convert.ToDecimal(dyfp.je) + Convert.ToDecimal(dyfp.se);
                dict.Add("jshjxx", ((char) 0xffe5) + num.ToString());
                dict.Add("jshjdx", ToolUtil.RMBToDaXie(num));
                dict.Add("jym", dyfp.jym);
                base.Id = "";
                int num2 = Convert.ToInt32(dyfp.yysbz.Substring(6, 2), 0x10) - 1;
                string str = "NEW76mmX177mm";
                Dictionary<string, int> jsPrintTemplate = ToolUtil.GetJsPrintTemplate();
                if (jsPrintTemplate.Count > 0)
                {
                    foreach (string str6 in jsPrintTemplate.Keys)
                    {
                        if (jsPrintTemplate[str6] == num2)
                        {
                            str = str6;
                        }
                    }
                    if ((num2 >= 0) && (num2 <= 10))
                    {
                        string str4 = this.method_13(dyfp);
                        DataTable table2 = this.method_12(dyfp, 0);
                        if (str4 == "H")
                        {
                            dict.Add("list", table2);
                            base.Id = str + "H";
                        }
                        else
                        {
                            dict.Add("hwmc", table2.Rows[0]["hwmc"]);
                            dict.Add("dj", table2.Rows[0]["dj"]);
                            dict.Add("sl", table2.Rows[0]["sl"]);
                            dict.Add("je", table2.Rows[0]["je"]);
                            base.Id = str + "V";
                        }
                    }
                    else
                    {
                        DataTable table3 = this.method_12(dyfp, 0);
                        dict.Add("list", table3);
                        base.Id = str;
                    }
                }
                else
                {
                    string str5 = this.method_13(dyfp);
                    DataTable table = this.method_12(dyfp, 0);
                    if (str5 == "H")
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
            string str2 = ToolUtil.FormatDateTimeEx(dyfp.kprq);
            dyfp.kprq = Aisino.Fwkp.Print.Common.ObjectToDateTime(str2).Date.ToString("yyyy年MM月dd日");
            dict.Add("kprq", dyfp.kprq);
            DateTime time3 = new DateTime(0x7dd, 9, 10, 8, 0x22, 30);
            TimeSpan span = (TimeSpan) (DateTime.Now - time3);
            byte[] buffer = AES_Crypt.Encrypt(ToolUtil.GetBytes(span.TotalSeconds.ToString("F1")), new byte[] { 
                0xff, 0x42, 0xae, 0x95, 11, 0x51, 0xca, 0x15, 0x21, 140, 0x4f, 170, 220, 0x92, 170, 0xed, 
                0xfd, 0xeb, 0x4e, 13, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
             }, new byte[] { 0xf2, 0x1f, 0xac, 0x5b, 0x2c, 0xc0, 0xa9, 0xd0, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 });
            if (!base.IsFirstCreate || (dyfp.Zyfplx == ZYFP_LX.NCP_SG))
            {
                dyfp.Get_Print_Dj(null, 0, buffer);
                if (dyfp.Zyfplx != ZYFP_LX.NCP_SG)
                {
                    base.dyfp.mw = dyfp.mw;
                    base.dyfp.bz = dyfp.bz;
                }
            }
            if (dyfp.zfbz && (Aisino.Fwkp.Print.Common.ObjectToDouble(dyfp.je) == 0.0))
            {
                dict.Add("mw", "");
                dict.Add("hxm", "");
            }
            else if (dyfp.hzfw)
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
            this.method_6(ref dyfp);
            if (dyfp.sLv == "0")
            {
                dict.Add("hjse", "***");
            }
            else
            {
                string str = ((char) 0xffe5) + dyfp.se;
                dict.Add("hjse", str);
            }
            decimal num2 = Convert.ToDecimal(dyfp.je) + Convert.ToDecimal(dyfp.se);
            dict.Add("jshjxx", num2);
            dict.Add("jshjdx", ToolUtil.RMBToDaXie(num2));
            dict.Add("kpr", dyfp.kpr);
            dict.Add("fhr", dyfp.fhr);
            dict.Add("skr", dyfp.skr);
            dict.Add("jmbb", "加密版本:");
            dict.Add("jmbbh", dyfp.jmbbh);
            dict.Add("list", this.method_11(dyfp));
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
            bool flag2 = false;
            if (yysbz.Length > 2)
            {
                switch (yysbz[2])
                {
                    case '1':
                    case '2':
                        flag2 = true;
                        break;
                }
            }
            if (flag2)
            {
                dict.Add("xtbz", "XT");
            }
            if (dyfp.fplx == FPLX.PTFP)
            {
                dict.Add("jym", dyfp.jym);
            }
            bool flag = Aisino.Fwkp.Print.Common.IsShuiWuDKSQ(dyfp.xfsh);
            switch (dyfp.fplx)
            {
                case FPLX.ZYFP:
                    if (!flag)
                    {
                        if (dyfp.hzfw)
                        {
                            base.Id = this.string_5;
                        }
                        else if (dyfp.mw.Length < 0x5b)
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
                        dict.Add("dkbz", "代开");
                        if (dyfp.mw.Length >= 0x5b)
                        {
                            base.Id = this.string_12;
                        }
                        else
                        {
                            base.Id = this.string_14;
                        }
                    }
                    goto Label_0577;

                case FPLX.PTFP:
                    if (dyfp.Zyfplx != ZYFP_LX.NCP_SG)
                    {
                        if (dyfp.Zyfplx == ZYFP_LX.NCP_XS)
                        {
                            dict.Add("ncpfp", "销售");
                        }
                        else if (flag)
                        {
                            dict.Add("dkbz", "代开");
                        }
                        break;
                    }
                    dict.Add("ncpfp", "收购");
                    break;

                default:
                    goto Label_0577;
            }
            if (flag)
            {
                if (dyfp.mw.Length < 0x5b)
                {
                    base.Id = this.string_13;
                }
                else
                {
                    base.Id = this.string_11;
                }
            }
            else if (dyfp.hzfw)
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
        Label_0577:
            if (Aisino.Fwkp.Print.ReadXml.int_0 == 1)
            {
                string str4 = this.method_8(dyfp);
                dict.Add("qrm", str4);
            }
            base.IsFirstCreate = true;
            return new DataDict(dict);
        }

        private DataTable method_10(Fpxx fpxx_0)
        {
            if (((fpxx_0 != null) && (fpxx_0.Mxxx != null)) && ((fpxx_0.Mxxx.Count != 0) && !fpxx_0.hzfw))
            {
                Graphics graphics = Graphics.FromImage(new Bitmap(10, 10));
                Font font = new Font("宋体", 8f);
                float num = 0f;
                int num2 = 0;
                string str = "";
                StringFormat genericTypographic = StringFormat.GenericTypographic;
                string str2 = "";
                bool flag = false;
                DataTable table = new DataTable();
                table.Columns.Add("hwmc");
                table.Columns.Add("ggxh");
                table.Columns.Add("dw");
                table.Columns.Add("sl");
                table.Columns.Add("dj");
                table.Columns.Add("je");
                table.Columns.Add("slv");
                table.Columns.Add("se");
                bool flag2 = false;
                if ((fpxx_0.Qdxx != null) && (fpxx_0.Qdxx.Count > 0))
                {
                    flag2 = true;
                }
                foreach (Dictionary<SPXX, string> dictionary in fpxx_0.Mxxx)
                {
                    DataRow row;
                    str2 = dictionary[SPXX.SPMC].Trim();
                    str = "";
                    flag = true;
                    foreach (char ch in str2)
                    {
                        SizeF ef = graphics.MeasureString(ch.ToString(), font, (PointF) Point.Empty, genericTypographic);
                        str = str + ch;
                        if (ef.Width < 7f)
                        {
                            num += 0.5f;
                        }
                        else
                        {
                            num++;
                        }
                        if (num >= 15f)
                        {
                            num2++;
                            if (num2 > 8)
                            {
                                return null;
                            }
                            if (flag)
                            {
                                string str4 = fpxx_0.Get_Print_Dj(dictionary, 0, null);
                                if (dictionary.ContainsKey(SPXX.LSLVBS))
                                {
                                    row = this.method_9(table, str, dictionary[SPXX.GGXH], dictionary[SPXX.JLDW], dictionary[SPXX.SL], str4, dictionary[SPXX.JE], dictionary[SPXX.SLV], dictionary[SPXX.SE], true, fpxx_0.fplx, fpxx_0.Zyfplx, dictionary[SPXX.LSLVBS], flag2);
                                }
                                else
                                {
                                    row = this.method_9(table, str, dictionary[SPXX.GGXH], dictionary[SPXX.JLDW], dictionary[SPXX.SL], str4, dictionary[SPXX.JE], dictionary[SPXX.SLV], dictionary[SPXX.SE], true, fpxx_0.fplx, fpxx_0.Zyfplx, "", flag2);
                                }
                                table.Rows.Add(row);
                                flag = false;
                            }
                            else
                            {
                                row = this.method_9(table, str, "", "", "", "", "", "", "", false, fpxx_0.fplx, fpxx_0.Zyfplx, "", flag2);
                                table.Rows.Add(row);
                            }
                            num = 0f;
                            str = "";
                        }
                    }
                    if (num > 0f)
                    {
                        num2++;
                        if (num2 > 8)
                        {
                            return null;
                        }
                        if (flag)
                        {
                            string str5 = fpxx_0.Get_Print_Dj(dictionary, 0, null);
                            if (dictionary.ContainsKey(SPXX.LSLVBS))
                            {
                                row = this.method_9(table, str, dictionary[SPXX.GGXH], dictionary[SPXX.JLDW], dictionary[SPXX.SL], str5, dictionary[SPXX.JE], dictionary[SPXX.SLV], dictionary[SPXX.SE], true, fpxx_0.fplx, fpxx_0.Zyfplx, dictionary[SPXX.LSLVBS], flag2);
                            }
                            else
                            {
                                row = this.method_9(table, str, dictionary[SPXX.GGXH], dictionary[SPXX.JLDW], dictionary[SPXX.SL], str5, dictionary[SPXX.JE], dictionary[SPXX.SLV], dictionary[SPXX.SE], true, fpxx_0.fplx, fpxx_0.Zyfplx, "", flag2);
                            }
                            table.Rows.Add(row);
                            flag = false;
                        }
                        else
                        {
                            if (num < 15f)
                            {
                                num *= 2f;
                                str = str + new string(' ', 30 - ((int) num));
                            }
                            row = this.method_9(table, str, "", "", "", "", "", "", "", false, fpxx_0.fplx, fpxx_0.Zyfplx, "", flag2);
                            table.Rows.Add(row);
                        }
                        num = 0f;
                        str = "";
                    }
                }
                if (num2 > 8)
                {
                    return null;
                }
                return table;
            }
            base.loger.Error("发票明细信息为空");
            return null;
        }

        private DataTable method_11(Fpxx fpxx_0)
        {
            if (((fpxx_0 != null) && (fpxx_0.Mxxx != null)) && (fpxx_0.Mxxx.Count != 0))
            {
                DataTable table2 = this.method_10(fpxx_0);
                if (table2 != null)
                {
                    return table2;
                }
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
                        object[] objArray5 = new object[] { dictionary[SPXX.SL], 9 };
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
                    bool flag = Aisino.Fwkp.Print.Common.IsFlbm();
                    if (!dictionary.ContainsKey(SPXX.LSLVBS) || !flag)
                    {
                        goto Label_05BC;
                    }
                    if (dictionary[SPXX.LSLVBS] != "")
                    {
                        string str4 = Aisino.Fwkp.Print.Common.smethod_4(dictionary[SPXX.LSLVBS]);
                        if (str4 != "")
                        {
                            row["slv"] = str4;
                            row["se"] = "***";
                            if (fpxx_0.yysbz.Substring(8, 1) == "2")
                            {
                                row["slv"] = "***";
                                row["se"] = "***";
                            }
                        }
                        else
                        {
                            float num2 = 0f;
                            float.TryParse(fpxx_0.sLv, out num2);
                            string str5 = string.Format("{0}%", num2 * 100f);
                            row["slv"] = str5;
                            row["se"] = dictionary[SPXX.SE];
                        }
                        goto Label_07BB;
                    }
                    if (((fpxx_0.Qdxx != null) && (fpxx_0.Qdxx.Count > 0)) && (Aisino.Fwkp.Print.Common.ObjectToDouble(fpxx_0.sLv) == 0.0))
                    {
                        row["slv"] = "0%";
                        row["se"] = "***";
                    }
                    if (!(fpxx_0.sLv != ""))
                    {
                        goto Label_04F0;
                    }
                    string str3 = dictionary[SPXX.SLV];
                    if (str3 != null)
                    {
                        if (str3 == "0.05")
                        {
                            bool flag3 = fpxx_0.Zyfplx == ZYFP_LX.HYSY;
                            if ((fpxx_0.fplx == FPLX.ZYFP) && flag3)
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
                        else if (!(str3 == "0.015"))
                        {
                            if (!(str3 == "0"))
                            {
                                goto Label_04C7;
                            }
                            row["slv"] = "0%";
                            row["se"] = "***";
                        }
                        else
                        {
                            row["slv"] = "***";
                            row["se"] = dictionary[SPXX.SE];
                        }
                        goto Label_04F0;
                    }
                Label_04C7:
                    row["slv"] = dictionary[SPXX.SLV];
                    row["se"] = dictionary[SPXX.SE];
                Label_04F0:
                    if (((fpxx_0.yysbz.Substring(8, 1) == "2") && (fpxx_0.sLv != "")) && (dictionary[SPXX.FPHXZ] != ""))
                    {
                        row["slv"] = "***";
                        row["se"] = dictionary[SPXX.SE];
                        if (fpxx_0.sLv == "0")
                        {
                            row["se"] = "***";
                        }
                    }
                    if (fpxx_0.sLv == "")
                    {
                        row["slv"] = dictionary[SPXX.SLV];
                        row["se"] = dictionary[SPXX.SE];
                    }
                    goto Label_07BB;
                Label_05BC:
                    if (!(fpxx_0.sLv != ""))
                    {
                        goto Label_06F7;
                    }
                    string str2 = dictionary[SPXX.SLV];
                    if (str2 != null)
                    {
                        if (str2 == "0.05")
                        {
                            bool flag2 = fpxx_0.Zyfplx == ZYFP_LX.HYSY;
                            if ((fpxx_0.fplx == FPLX.ZYFP) && flag2)
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
                        else if (!(str2 == "0.015"))
                        {
                            if (!(str2 == "0"))
                            {
                                goto Label_06CE;
                            }
                            row["slv"] = "***";
                            row["se"] = "***";
                        }
                        else
                        {
                            row["slv"] = "***";
                            row["se"] = dictionary[SPXX.SE];
                        }
                        goto Label_06F7;
                    }
                Label_06CE:
                    row["slv"] = dictionary[SPXX.SLV];
                    row["se"] = dictionary[SPXX.SE];
                Label_06F7:
                    if (((fpxx_0.yysbz.Substring(8, 1) == "2") && (fpxx_0.sLv != "")) && (dictionary[SPXX.FPHXZ] != ""))
                    {
                        row["slv"] = "***";
                        row["se"] = dictionary[SPXX.SE];
                        if (fpxx_0.sLv == "0")
                        {
                            row["se"] = "***";
                        }
                    }
                    if (fpxx_0.sLv == "")
                    {
                        row["slv"] = dictionary[SPXX.SLV];
                        row["se"] = dictionary[SPXX.SE];
                    }
                Label_07BB:
                    table.Rows.Add(row);
                }
                return table;
            }
            base.loger.Error("发票明细信息为空");
            return null;
        }

        private DataTable method_12(Fpxx fpxx_0, int int_0 = 0)
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
                        object[] objArray6 = new object[] { dictionary[SPXX.SL], num2 };
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
                    row["je"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary[SPXX.JE]) + Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary[SPXX.SE]);
                    table.Rows.Add(row);
                }
                return table;
            }
            base.loger.Error("发票明细信息为空");
            return null;
        }

        private string method_13(Fpxx fpxx_0)
        {
            if (fpxx_0.Mxxx != null)
            {
                if (fpxx_0.Mxxx.Count > 1)
                {
                    return "H";
                }
                if ((fpxx_0.Mxxx.Count == 1) && !base.IsTaoDa)
                {
                    return "V";
                }
            }
            return "H";
        }

        private void method_6(ref Fpxx fpxx_0)
        {
            if ((fpxx_0.yysbz != null) && (fpxx_0.yysbz.Length >= 10))
            {
                switch (fpxx_0.yysbz[5])
                {
                    case '1':
                        fpxx_0.Zyfplx = ZYFP_LX.NCP_XS;
                        return;

                    case '2':
                        fpxx_0.Zyfplx = ZYFP_LX.NCP_SG;
                        return;
                }
            }
        }

        private DataTable method_7(Fpxx fpxx_0)
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
                Dictionary<SPXX, string> dictionary6 = fpxx_0.Mxxx[0];
                row6["fyxm1"] = dictionary6[SPXX.SPMC];
                row6["je1"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary6[SPXX.JE]).ToString("f2");
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
                    Dictionary<SPXX, string> dictionary2 = fpxx_0.Mxxx[i];
                    row2["fyxm1"] = dictionary2[SPXX.SPMC];
                    row2["je1"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary2[SPXX.JE]).ToString("f2");
                    if ((i + 1) < fpxx_0.Mxxx.Count)
                    {
                        Dictionary<SPXX, string> dictionary5 = fpxx_0.Mxxx[i + 1];
                        row2["fyxm2"] = dictionary5[SPXX.SPMC];
                        row2["je2"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary5[SPXX.JE]).ToString("f2");
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
                    Dictionary<SPXX, string> dictionary = fpxx_0.Mxxx[j];
                    row["fyxm1"] = dictionary[SPXX.SPMC];
                    row["je1"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary[SPXX.JE]).ToString("f2");
                    if ((j + 1) < fpxx_0.Mxxx.Count)
                    {
                        Dictionary<SPXX, string> dictionary3 = fpxx_0.Mxxx[j + 1];
                        row["fyxm2"] = dictionary3[SPXX.SPMC];
                        row["je2"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary3[SPXX.JE]).ToString("f2");
                    }
                    if ((j + 2) < fpxx_0.Mxxx.Count)
                    {
                        Dictionary<SPXX, string> dictionary4 = fpxx_0.Mxxx[j + 2];
                        row["fyxm3"] = dictionary4[SPXX.SPMC];
                        row["je3"] = Aisino.Fwkp.Print.Common.ObjectToDouble(dictionary4[SPXX.JE]).ToString("f2");
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        private string method_8(Fpxx fpxx_0)
        {
            string str = "01,";
            switch (fpxx_0.fplx)
            {
                case FPLX.ZYFP:
                    str = str + "01,";
                    break;

                case FPLX.PTFP:
                    str = str + "04,";
                    break;

                case FPLX.HYFP:
                    str = str + "02,";
                    break;

                case FPLX.JDCFP:
                    str = str + "03,";
                    break;

                case FPLX.DZFP:
                    str = str + "10,";
                    break;

                default:
                    str = str + "11,";
                    break;
            }
            str = (str + fpxx_0.fpdm + ",") + Aisino.Fwkp.Print.Common.smethod_1(fpxx_0.fphm) + ",";
            str = (str + Aisino.Fwkp.Print.Common.ObjectToDouble(fpxx_0.je).ToString("f2") + ",") + Aisino.Fwkp.Print.Common.ObjectToDateTime(fpxx_0.kprq).ToString("yyyyMMdd") + ",";
            if (fpxx_0.fplx == FPLX.PTFP)
            {
                str = str + fpxx_0.jym + ",";
            }
            else
            {
                str = str + ",";
            }
            return (str + Aisino.Fwkp.Print.Common.GetCRC(str) + ",");
        }

        private DataRow method_9(DataTable dataTable_0, string string_15, string string_16, string string_17, string string_18, string string_19, string string_20, string string_21, string string_22, bool bool_1, FPLX fplx_0, ZYFP_LX zyfp_LX_0, string string_23 = "", bool bool_2 = false)
        {
            if (dataTable_0 == null)
            {
                return null;
            }
            Fpxx dyfp = base.dyfp;
            DataRow row = dataTable_0.NewRow();
            if (!bool_1)
            {
                row["dj"] = "";
                row["hwmc"] = string_15;
                row["ggxh"] = "";
                row["dw"] = "";
                row["sl"] = "";
                row["je"] = "";
                row["slv"] = "";
                row["se"] = "";
                return row;
            }
            row["hwmc"] = string_15;
            row["ggxh"] = string_16;
            row["dw"] = string_17;
            if (string_18 == "0")
            {
                row["sl"] = string.Empty;
            }
            else
            {
                object[] objArray4 = new object[] { string_18, 9 };
                object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPPrecisionShareMethod", objArray4);
                if ((objArray2 != null) && (objArray2.Length > 0))
                {
                    row["sl"] = objArray2[0].ToString();
                }
                else
                {
                    row["sl"] = "";
                    base.loger.Error("精度四舍五入错误");
                }
            }
            object[] objArray6 = new object[] { string_19, 12 };
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPPrecisionShareMethod", objArray6);
            if ((objArray != null) && (objArray.Length > 0))
            {
                row["dj"] = Aisino.Fwkp.Print.Common.FormatString(objArray[0].ToString());
            }
            else
            {
                row["dj"] = "";
                base.loger.Error("精度四舍五入错误");
            }
            row["je"] = string_20;
            if (!Aisino.Fwkp.Print.Common.IsFlbm())
            {
                if (!(string_21 != ""))
                {
                    row["slv"] = "";
                    row["se"] = string_22;
                    goto Label_0681;
                }
                string str4 = string_21;
                if (str4 != null)
                {
                    if (str4 == "0.05")
                    {
                        bool flag = zyfp_LX_0 == ZYFP_LX.HYSY;
                        if ((fplx_0 == FPLX.ZYFP) && flag)
                        {
                            row["slv"] = "";
                            row["se"] = string_22;
                        }
                        else
                        {
                            row["slv"] = string_21;
                            row["se"] = string_22;
                        }
                    }
                    else if (!(str4 == "0.015"))
                    {
                        if (!(str4 == "0"))
                        {
                            goto Label_0648;
                        }
                        row["slv"] = "***";
                        row["se"] = "***";
                    }
                    else
                    {
                        row["slv"] = "***";
                        row["se"] = string_22;
                    }
                    goto Label_0681;
                }
                goto Label_0648;
            }
            if ((string_23 != null) && (string_23 != ""))
            {
                string str5 = Aisino.Fwkp.Print.Common.smethod_4(string_23);
                if (str5 != "")
                {
                    row["slv"] = str5;
                    row["se"] = "***";
                    if (dyfp.yysbz.Substring(8, 1) == "2")
                    {
                        row["slv"] = "***";
                        row["se"] = "***";
                    }
                    return row;
                }
                float result = 0f;
                float.TryParse(string_21, out result);
                string str2 = string.Format("{0}%", result * 100f);
                row["slv"] = str2;
                row["se"] = string_22;
                return row;
            }
            if (bool_2 && (Aisino.Fwkp.Print.Common.ObjectToDouble(string_21) == 0.0))
            {
                row["slv"] = "0%";
                row["se"] = "***";
            }
            if (!bool_2 || (Aisino.Fwkp.Print.Common.ObjectToDouble(string_21) == 0.0))
            {
                if (bool_2 || !(string_21 != ""))
                {
                    goto Label_04E0;
                }
                string str = string_21;
                if (str != null)
                {
                    if (str == "0.05")
                    {
                        bool flag2 = zyfp_LX_0 == ZYFP_LX.HYSY;
                        if ((fplx_0 == FPLX.ZYFP) && flag2)
                        {
                            row["slv"] = "";
                            row["se"] = string_22;
                        }
                        else
                        {
                            row["slv"] = string_21;
                            row["se"] = string_22;
                        }
                    }
                    else if (!(str == "0.015"))
                    {
                        if (!(str == "0"))
                        {
                            goto Label_04C6;
                        }
                        row["slv"] = "0%";
                        row["se"] = "***";
                    }
                    else
                    {
                        row["slv"] = "***";
                        row["se"] = string_22;
                    }
                    goto Label_04E0;
                }
                goto Label_04C6;
            }
            string str3 = string_21;
            if (str3 != null)
            {
                if (str3 == "0.05")
                {
                    bool flag3 = zyfp_LX_0 == ZYFP_LX.HYSY;
                    if ((fplx_0 == FPLX.ZYFP) && flag3)
                    {
                        row["slv"] = "";
                        row["se"] = string_22;
                    }
                    else
                    {
                        row["slv"] = string_21;
                        row["se"] = string_22;
                    }
                }
                else if (!(str3 == "0.015"))
                {
                    if (!(str3 == "0"))
                    {
                        goto Label_03CA;
                    }
                    row["slv"] = "0%";
                    row["se"] = "***";
                }
                else
                {
                    row["slv"] = "***";
                    row["se"] = string_22;
                }
                goto Label_04E0;
            }
        Label_03CA:
            row["slv"] = string_21;
            row["se"] = string_22;
            goto Label_04E0;
        Label_04C6:
            row["slv"] = string_21;
            row["se"] = string_22;
        Label_04E0:
            if (string_21 == "")
            {
                row["slv"] = "";
                row["se"] = string_22;
            }
            if (dyfp.yysbz.Substring(8, 1) == "2")
            {
                row["slv"] = "***";
                row["se"] = string_22;
                if (string_21 == "0")
                {
                    row["se"] = "***";
                }
            }
            return row;
        Label_0648:
            row["slv"] = string_21;
            row["se"] = string_22;
        Label_0681:
            if (dyfp.yysbz.Substring(8, 1) == "2")
            {
                row["slv"] = "***";
                row["se"] = string_22;
                if (string_21 == "0")
                {
                    row["se"] = "***";
                }
            }
            return row;
        }

        public double ObjectToDouble(object object_0)
        {
            if (object_0 == null)
            {
                return 0.0;
            }
            double result = 0.0;
            double.TryParse(object_0.ToString().Trim(), out result);
            return result;
        }
    }
}

