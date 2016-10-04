namespace Aisino.Fwkp.Print
{
    using Aisino.Framework.Plugin.Core.Controls.PrintControl;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.BusinessObject;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class QDPrint : IPrint
    {
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;

        public QDPrint(string string_4, string string_5, int int_0)
        {
            
            this.string_0 = "qdqd";
            this.string_1 = "qdtd";
            this.string_2 = "qdfwqd";
            this.string_3 = "qdfwtd";
            base.method_0(new object[] { string_4, string_5, int_0, "_QD" });
            base.IsShowTaoDaGruoupButton = true;
        }

        public QDPrint(string string_4, string string_5, int int_0, string string_6, int int_1, int int_2, bool bool_1)
        {
            
            this.string_0 = "qdqd";
            this.string_1 = "qdtd";
            this.string_2 = "qdfwqd";
            this.string_3 = "qdfwtd";
            base.method_0(new object[] { string_4, string_5, int_0, "_QD", string_6, int_1, int_2, bool_1 });
            base.IsShowTaoDaGruoupButton = true;
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
                base.ZYFPLX = "";
                if (args.Length >= 3)
                {
                    base._isZYPT = false;
                    string str3 = args[0].ToString();
                    string str4 = args[1].ToString();
                    int num7 = Convert.ToInt32(args[2]);
                    object[] objArray5 = new object[] { str3, str4, num7 };
                    Fpxx fpxx = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPChanXunWenBenJieKouShareMethods", objArray5)[0] as Fpxx;
                    if (fpxx.hzfw)
                    {
                        QdfwUtil.IsQDFW();
                    }
                    if (((fpxx != null) && (fpxx.Qdxx != null)) && (fpxx.Qdxx.Count > 0))
                    {
                        List<Dictionary<string, object>> listDict = new List<Dictionary<string, object>>();
                        List<Dictionary<SPXX, string>> qdxx = fpxx.Qdxx;
                        int num5 = qdxx.Count / 0x19;
                        if ((qdxx.Count % 0x19) != 0)
                        {
                            num5++;
                        }
                        DateTime time = new DateTime(0x7dd, 9, 10, 8, 0x22, 30);
                        TimeSpan span = (TimeSpan) (DateTime.Now - time);
                        byte[] buffer = AES_Crypt.Encrypt(ToolUtil.GetBytes(span.TotalSeconds.ToString("F1")), new byte[] { 
                            0xff, 0x42, 0xae, 0x95, 11, 0x51, 0xca, 0x15, 0x21, 140, 0x4f, 170, 220, 0x92, 170, 0xed, 
                            0xfd, 0xeb, 0x4e, 13, 0xac, 0x80, 0x52, 0xff, 0x45, 0x90, 0x85, 0xca, 0xcb, 0x9f, 0xaf, 0xbd
                         }, new byte[] { 0xf2, 0x1f, 0xac, 0x5b, 0x2c, 0xc0, 0xa9, 0xd0, 0xbc, 0xaf, 0x86, 0x99, 0xad, 170, 0xfb, 0x60 });
                        fpxx.Get_Print_Dj(null, 0, buffer);
                        double num14 = 0.0;
                        double num13 = 0.0;
                        for (int i = 0; i < num5; i++)
                        {
                            Dictionary<string, object> item = new Dictionary<string, object>();
                            item.Add("fpdm", fpxx.fpdm);
                            item.Add("fphm", fpxx.fphm);
                            item.Add("fpzl", "所属增值税" + Aisino.Fwkp.Print.Common.GetInvoiceType(fpxx));
                            item.Add("tkrq", fpxx.kprq);
                            if (fpxx.hzfw)
                            {
                                item.Add("hxm", fpxx.hxm);
                            }
                            DataTable table = new DataTable();
                            table.Columns.Add("xh");
                            table.Columns.Add("hwmc");
                            table.Columns.Add("ggxh");
                            table.Columns.Add("dw");
                            table.Columns.Add("sl");
                            table.Columns.Add("dj");
                            table.Columns.Add("je");
                            table.Columns.Add("slv");
                            table.Columns.Add("se");
                            double num2 = 0.0;
                            double num3 = 0.0;
                            for (int j = i * 0x19; j < ((i + 1) * 0x19); j++)
                            {
                                double num19;
                                if (j >= qdxx.Count)
                                {
                                    break;
                                }
                                DataRow row = table.NewRow();
                                string str2 = fpxx.Get_Print_Dj(qdxx[j], 0, null);
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
                                row["xh"] = j + 1;
                                if (base.IsTaoDa)
                                {
                                    row["hwmc"] = qdxx[j][SPXX.SPMC];
                                }
                                else
                                {
                                    row["hwmc"] = qdxx[j][SPXX.SPMC];
                                }
                                row["ggxh"] = qdxx[j][SPXX.GGXH];
                                row["dw"] = qdxx[j][SPXX.JLDW];
                                string str5 = qdxx[j][SPXX.SL];
                                object[] objArray8 = new object[] { str5, 9 };
                                object[] objArray9 = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPPrecisionShareMethod", objArray8);
                                if ((objArray9 != null) && (objArray9.Length > 0))
                                {
                                    row["sl"] = objArray9[0].ToString();
                                }
                                else
                                {
                                    row["sl"] = "";
                                    base.loger.Error("精度四舍五入错误");
                                }
                                row["je"] = Aisino.Fwkp.Print.Common.ObjectToDouble(qdxx[j][SPXX.JE]).ToString("f2");
                                if ((qdxx[j].ContainsKey(SPXX.LSLVBS) && (qdxx[j][SPXX.LSLVBS] != null)) && (qdxx[j][SPXX.LSLVBS] != ""))
                                {
                                    int num10 = Aisino.Fwkp.Print.Common.ObjectToInt(qdxx[j][SPXX.LSLVBS]);
                                    if ((num10 >= 0) && (num10 <= 3))
                                    {
                                        row["se"] = "***";
                                        row["slv"] = Aisino.Fwkp.Print.Common.smethod_4(qdxx[j][SPXX.LSLVBS]);
                                    }
                                    else
                                    {
                                        row["se"] = qdxx[j][SPXX.SE];
                                        row["slv"] = qdxx[j][SPXX.SLV];
                                    }
                                    goto Label_0845;
                                }
                                if (!(fpxx.sLv != ""))
                                {
                                    goto Label_0749;
                                }
                                string str = qdxx[j][SPXX.SLV];
                                if (str != null)
                                {
                                    if (str == "0.05")
                                    {
                                        bool flag = fpxx.Zyfplx == ZYFP_LX.HYSY;
                                        if ((fpxx.fplx == FPLX.ZYFP) && flag)
                                        {
                                            row["slv"] = "";
                                            row["se"] = Aisino.Fwkp.Print.Common.ObjectToDouble(qdxx[j][SPXX.SE]).ToString("f2");
                                        }
                                        else
                                        {
                                            row["slv"] = ((Aisino.Fwkp.Print.Common.ObjectToDouble(qdxx[j][SPXX.SLV]) * 100.0)).ToString() + "%";
                                            row["se"] = Aisino.Fwkp.Print.Common.ObjectToDouble(qdxx[j][SPXX.SE]).ToString("f2");
                                        }
                                    }
                                    else if (!(str == "0.015"))
                                    {
                                        if (!(str == "0"))
                                        {
                                            goto Label_06D8;
                                        }
                                        row["slv"] = "***";
                                        row["se"] = "***";
                                    }
                                    else
                                    {
                                        row["slv"] = "***";
                                        row["se"] = Aisino.Fwkp.Print.Common.ObjectToDouble(qdxx[j][SPXX.SE]).ToString("f2");
                                    }
                                    goto Label_0845;
                                }
                            Label_06D8:
                                num19 = Aisino.Fwkp.Print.Common.ObjectToDouble(qdxx[j][SPXX.SLV]) * 100.0;
                                row["slv"] = num19.ToString() + "%";
                                row["se"] = Aisino.Fwkp.Print.Common.ObjectToDouble(qdxx[j][SPXX.SE]).ToString("f2");
                                goto Label_0845;
                            Label_0749:
                                if (qdxx[j][SPXX.SLV] != "")
                                {
                                    double num8 = Aisino.Fwkp.Print.Common.ObjectToDouble(qdxx[j][SPXX.SLV]) * 100.0;
                                    if (num8 == 0.0)
                                    {
                                        row["slv"] = "***";
                                        row["se"] = "***";
                                    }
                                    else
                                    {
                                        row["slv"] = num8.ToString() + "%";
                                        row["se"] = Aisino.Fwkp.Print.Common.ObjectToDouble(qdxx[j][SPXX.SE]).ToString("f2");
                                    }
                                }
                                else
                                {
                                    row["slv"] = "";
                                    row["se"] = Aisino.Fwkp.Print.Common.ObjectToDouble(qdxx[j][SPXX.SE]).ToString("f2");
                                }
                            Label_0845:
                                if (qdxx[j][SPXX.FPHXZ] != "5")
                                {
                                    num3 += Aisino.Fwkp.Print.Common.ObjectToDouble(qdxx[j][SPXX.SE]);
                                    num2 += Aisino.Fwkp.Print.Common.ObjectToDouble(qdxx[j][SPXX.JE]);
                                }
                                table.Rows.Add(row);
                            }
                            item.Add("list", table);
                            item.Add("xjje", num2.ToString("f2"));
                            if (num3 == 0.0)
                            {
                                if (fpxx.sLv == "0")
                                {
                                    item.Add("xjse", "***");
                                }
                                if (fpxx.sLv != "0")
                                {
                                    item.Add("xjse", "0.00");
                                }
                            }
                            else
                            {
                                item.Add("xjse", num3.ToString("f2"));
                            }
                            num14 += num2;
                            num13 += num3;
                            item.Add("zjje", num14.ToString("f2"));
                            if (num13 == 0.0)
                            {
                                if (fpxx.sLv == "0")
                                {
                                    item.Add("zjse", "***");
                                }
                                if (fpxx.sLv != "0")
                                {
                                    item.Add("zjse", "0.00");
                                }
                            }
                            else
                            {
                                item.Add("zjse", num13.ToString("f2"));
                            }
                            item.Add("gfmc", fpxx.gfmc);
                            item.Add("xfmc", fpxx.xfmc);
                            item.Add("bz", fpxx.bz);
                            item.Add("page", i + 1);
                            item.Add("allpage", num5);
                            listDict.Add(item);
                        }
                        if (base.IsTaoDa)
                        {
                            base.Id = this.string_0;
                        }
                        else
                        {
                            base.Id = this.string_1;
                        }
                        return new DataDict(listDict);
                    }
                    base._isPrint = "0006";
                }
                base._isPrint = "0006";
                return null;
            }
            catch (Exception exception)
            {
                base._isPrint = "0003";
                base.loger.Error("[创建数据字典]：" + exception.Message);
                return null;
            }
        }
    }
}

