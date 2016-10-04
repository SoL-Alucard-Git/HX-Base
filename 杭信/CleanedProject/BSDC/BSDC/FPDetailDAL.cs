namespace BSDC
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using log4net;
    using Microsoft.Win32;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using Aisino.Framework.Dao;
    public class FPDetailDAL
    {
        private IBaseDAO ibaseDAO_0;
        private ILog ilog_0;
        private string string_0;

        public FPDetailDAL()
        {
            
            this.ibaseDAO_0 = BaseDAOFactory.GetBaseDAOSQLite();
            this.ilog_0 = LogUtil.GetLogger<BSDataOutput>();
            this.string_0 = "";
        }

        public List<FPDetail> GetFPDetailList(DateTime dateTime_0, DateTime dateTime_1)
        {
            List<FPDetail> list = new List<FPDetail>();
            try
            {
                Dictionary<string, object> parameter = new Dictionary<string, object>();
                DateTime time = Convert.ToDateTime(dateTime_1.AddDays(1.0).ToShortDateString());
                parameter.Add("QSRQ", dateTime_0);
                parameter.Add("ZZRQ", time);
                foreach (object obj2 in this.ibaseDAO_0.querySQL("aisino.fwkp.bsgl.selectXxfpByDate", parameter))
                {
                    FPDetail item = this.method_2(obj2);
                    list.Add(item);
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public List<FPDetail> GetFPDetailList(int int_0, string string_1)
        {
            List<FPDetail> list = new List<FPDetail>();
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("XFSH", string_1);
            parameter.Add("SSYF", int_0);
            try
            {
                foreach (object obj2 in this.ibaseDAO_0.querySQL("aisino.fwkp.bsgl.selectXxfpByMonth", parameter))
                {
                    FPDetail item = this.method_2(obj2);
                    list.Add(item);
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public List<FPDetail> GetFPDetailListByFPZL(DateTime dateTime_0, DateTime dateTime_1, string string_1)
        {
            List<FPDetail> list = new List<FPDetail>();
            try
            {
                Dictionary<string, object> parameter = new Dictionary<string, object>();
                DateTime time = Convert.ToDateTime(dateTime_1.AddDays(1.0).ToShortDateString());
                parameter.Add("QSRQ", dateTime_0);
                parameter.Add("ZZRQ", time);
                parameter.Add("FPZL", string_1);
                foreach (object obj2 in this.ibaseDAO_0.querySQL("aisino.fwkp.bsgl.selectXxfpByDateFPZL", parameter))
                {
                    FPDetail item = this.method_2(obj2);
                    list.Add(item);
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public List<FPDetail> GetFPDetailListByFPZL(int int_0, string string_1, string string_2)
        {
            List<FPDetail> list = new List<FPDetail>();
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("XFSH", string_1);
            parameter.Add("SSYF", int_0);
            parameter.Add("FPZL", string_2);
            try
            {
                foreach (object obj2 in this.ibaseDAO_0.querySQL("aisino.fwkp.bsgl.selectXxfpByMonthFPZL", parameter))
                {
                    FPDetail item = this.method_2(obj2);
                    if (!string.IsNullOrEmpty(item.SIGN))
                    {
                        list.Add(item);
                    }
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public List<FPDetail> GetFPDetailListByFPZL_(int int_0, string string_1, string string_2)
        {
            List<FPDetail> list = new List<FPDetail>();
            Class66.smethod_0(this.method_0());
            string str = string.Concat(new object[] { "SELECT FPZL,FPDM,FPHM,KPRQ,GFSH,GFMC,GFDZDH,GFYHZH,XFSH,XFMC,XFDZDH,XFYHZH,HJJE,SLV,HJSE,HXM,ZFBZ,KPR,BZ,YYSBZ,QDBZ,CM,TYDH,SCCJMC,XFDZ,KHYHMC,QYD,ZHD,XHD,JQBH,YYZZH,MW,YSHWXX,HYBM,SKR,FHR,XSBM,XFDH,KHYHZH,MDD,JYM,SIGN,ZFRQ,WSPZHM,HZTZDH,LZDMHM,XSDJBH,BMBBBH,ZYSPMC,SPSM,SPSM_BM FROM XXFP WHERE  XFSH ='", string_1, "' And SSYF='", int_0, "' And FPZL='", string_2, "' AND SIGN != '' AND YYSBZ != ''" });
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if ((card.OldTaxCode != null) && (card.OldTaxCode != ""))
            {
                str = str + " AND XFSH='" + card.TaxCode + "'";
            }
            DataTable table = Class66.smethod_12(str);
            list = this.method_3(table);
            Class66.smethod_14();
            return list;
        }

        public List<GoodsInfo> GetGoodsList(string string_1, string string_2, long long_0)
        {
            List<GoodsInfo> list = new List<GoodsInfo>();
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("FPZL", string_1);
            parameter.Add("FPDM", string_2);
            parameter.Add("FPHM", long_0);
            try
            {
                ArrayList list2 = this.ibaseDAO_0.querySQL("aisino.fwkp.bsgl.selectXxfpMc", parameter);
                int num = 0;
                foreach (object obj2 in list2)
                {
                    GoodsInfo item = null;
                    if (string_1.Equals("s"))
                    {
                        item = this.method_5(obj2);
                    }
                    else
                    {
                        item = this.method_4(obj2);
                    }
                    item.RowNo = ++num;
                    list.Add(item);
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public List<GoodsInfo> GetGoodsList_(string string_1, string string_2, long long_0)
        {
            List<GoodsInfo> list = new List<GoodsInfo>();
            try
            {
                Class66.smethod_0(this.method_0());
                DataTable table = Class66.smethod_12(string.Concat(new object[] { "SELECT XXFP_MX.SPMC,XXFP_MX.GGXH,XXFP_MX.JLDW,XXFP_MX.SL,XXFP_MX.DJ,XXFP_MX.JE,XXFP_MX.SE,XXFP_MX.SLV,XXFP_MX.HSJBZ,  XXFP_MX.FPMXXH,XXFP_MX.FLBM,XXFP_MX.XSYH,XXFP_MX.YHSM,XXFP_MX.LSLVBS,XXFP_MX.SPBH,XXFP.YYSBZ,XXFP.SLv as XXFPSLv FROM XXFP_MX LEFT JOIN XXFP ON XXFP_MX.FPDM=XXFP.FPDM AND XXFP_MX.FPHM=XXFP.FPHM WHERE XXFP_MX.FPZL ='", string_1, "' And XXFP_MX.FPDM='", string_2, "' And XXFP_MX.FPHM='", long_0, "'" }));
                list = this.method_6(table, string_1);
                Class66.smethod_14();
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public List<GoodsInfo> GetGoodsListFPExport(string string_1, string string_2, long long_0)
        {
            List<GoodsInfo> list = new List<GoodsInfo>();
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("FPZL", string_1);
            parameter.Add("FPDM", string_2);
            parameter.Add("FPHM", long_0);
            try
            {
                ArrayList list2 = this.ibaseDAO_0.querySQL("aisino.fwkp.bsgl.selectXxfpMc", parameter);
                int num = 0;
                foreach (object obj2 in list2)
                {
                    GoodsInfo item = this.method_7(obj2);
                    item.RowNo = ++num;
                    list.Add(item);
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public List<GoodsInfo> GetGoodsQDList(string string_1, string string_2, long long_0)
        {
            List<GoodsInfo> list = new List<GoodsInfo>();
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("FPZL", string_1);
            parameter.Add("FPDM", string_2);
            parameter.Add("FPHM", long_0);
            try
            {
                ArrayList list2 = this.ibaseDAO_0.querySQL("aisino.fwkp.bsgl.selectXxfpMcQD", parameter);
                int num = 0;
                foreach (object obj2 in list2)
                {
                    GoodsInfo item = this.method_4(obj2);
                    item.RowNo = ++num;
                    list.Add(item);
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public List<GoodsInfo> GetGoodsQDList_(string string_1, string string_2, long long_0)
        {
            List<GoodsInfo> list = new List<GoodsInfo>();
            try
            {
                Class66.smethod_0(this.method_0());
                DataTable table = Class66.smethod_12(string.Concat(new object[] { "SELECT XXFP_XHQD.SPMC,XXFP_XHQD.GGXH,XXFP_XHQD.JLDW,XXFP_XHQD.SL,XXFP_XHQD.DJ,XXFP_XHQD.JE,XXFP_XHQD.SE,XXFP_XHQD.SLV,XXFP_XHQD.HSJBZ, XXFP_XHQD.FPMXXH,XXFP_XHQD.FLBM,XXFP_XHQD.XSYH,XXFP_XHQD.YHSM,XXFP_XHQD.LSLVBS,XXFP_XHQD.SPBH,XXFP.YYSBZ,XXFP.SLv as XXFPSLv FROM XXFP_XHQD LEFT JOIN XXFP ON XXFP_XHQD.FPDM=XXFP.FPDM AND XXFP_XHQD.FPHM=XXFP.FPHM WHERE XXFP_XHQD.FPZL ='", string_1, "' And XXFP_XHQD.FPDM='", string_2, "' And XXFP_XHQD.FPHM='", long_0, "'" }));
                list = this.method_6(table, string_1);
                Class66.smethod_14();
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public object GetQiShiDate()
        {
            DateTime time = new DateTime();
            this.ilog_0.Debug("进入 GetQiShiDate");
            Class66.smethod_0(this.method_0());
            this.ilog_0.Debug("SQLiteHelper open succ");
            string str = "select min(KPRQ) from xxfp where sign != ''";
            DataTable table = Class66.smethod_12(str);
            time = this.method_12(table.Rows[0][0]);
            Class66.smethod_14();
            if (time.Year == 1)
            {
                return null;
            }
            return time;
        }

        private string method_0()
        {
            try
            {
                this.string_0 = this.method_1() + @"\Bin\cc3268.dll";
                return string.Format("Data Source={0};Pooling=true;Password=LoveR1314;", this.string_0);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private string method_1()
        {
            string str = "";
            try
            {
                RegistryKey key2 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default).OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe");
                if (key2 == null)
                {
                    return str;
                }
                string str2 = PropertyUtil.GetValue("MAIN_PATH", "");
                if (str2.Length == 0)
                {
                    str2 = key2.GetValue("Path").ToString();
                }
                if (str2 != null)
                {
                    str = str2;
                }
            }
            catch
            {
            }
            return str;
        }

        private float method_10(object object_0)
        {
            if ((!(object_0 is DBNull) && (object_0 != null)) && !(object_0.ToString() == ""))
            {
                return Convert.ToSingle(object_0.ToString());
            }
            return 0f;
        }

        private decimal method_11(object object_0)
        {
            if ((object_0 != null) && !(object_0.ToString() == ""))
            {
                return Convert.ToDecimal(object_0.ToString());
            }
            return 0M;
        }

        private DateTime method_12(object object_0)
        {
            if ((object_0 == null) || (object_0.ToString() == ""))
            {
                return DateTime.MinValue;
            }
            try
            {
                return Convert.ToDateTime(object_0.ToString());
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        public ArrayList method_13(bool bool_0, bool bool_1, DateTime dateTime_0, string string_1)
        {
            ArrayList list = new ArrayList();
            try
            {
                Dictionary<string, object> parameter = new Dictionary<string, object>();
                parameter.Add("BSBZA", bool_0);
                parameter.Add("BSBZB", bool_1);
                parameter.Add("KPRQ", dateTime_0);
                parameter.Add("FPZL", string_1);
                list = this.ibaseDAO_0.querySQL("aisino.fwkp.bsgl.updateBSBZ", parameter);
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception)
            {
            }
            return list;
        }

        private string method_14(string string_1, int int_0)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(string_1))
            {
                return string_1;
            }
            try
            {
                str = decimal.Round(decimal.Parse(string_1), int_0, MidpointRounding.AwayFromZero).ToString("f" + int_0);
            }
            catch (Exception)
            {
            }
            return str;
        }

        private FPDetail method_2(object object_0)
        {
            FPDetail detail = new FPDetail();
            Dictionary<string, object> dictionary = object_0 as Dictionary<string, object>;
            if (dictionary != null)
            {
                try
                {
                    detail.FPType = (FPType) Enum.Parse(typeof(FPType), dictionary["FPZL"].ToString(), true);
                    detail.FPDM = dictionary["FPDM"].ToString();
                    detail.FPHM = this.method_9(dictionary["FPHM"]);
                    detail.KPRQ = this.method_12(dictionary["KPRQ"]);
                    detail.GFSH = dictionary["GFSH"].ToString();
                    detail.GFMC = dictionary["GFMC"].ToString();
                    detail.GFDZDH = dictionary["GFDZDH"].ToString();
                    detail.GFYHZH = dictionary["GFYHZH"].ToString();
                    detail.XFSH = dictionary["XFSH"].ToString();
                    detail.XFMC = dictionary["XFMC"].ToString();
                    detail.XFDZDH = dictionary["XFDZDH"].ToString();
                    detail.XFYHZH = dictionary["XFYHZH"].ToString();
                    detail.HJJE = this.method_11(dictionary["HJJE"]);
                    if (string.IsNullOrEmpty(dictionary["SLV"].ToString()))
                    {
                        detail.SLV = 99.01f;
                    }
                    else
                    {
                        detail.SLV = this.method_10(dictionary["SLV"]);
                    }
                    detail.HJSE = this.method_11(dictionary["HJSE"]);
                    detail.HXM = dictionary["HXM"].ToString();
                    detail.ZFBZ = this.method_8(dictionary["ZFBZ"]);
                    detail.KPR = dictionary["KPR"].ToString();
                    detail.BZ = dictionary["BZ"].ToString();
                    detail.YYSBZ = dictionary["YYSBZ"].ToString();
                    detail.QDBZ = this.method_8(dictionary["QDBZ"]);
                    detail.CM = dictionary["CM"].ToString();
                    detail.TYDH = dictionary["TYDH"].ToString();
                    detail.SCCJMC = dictionary["SCCJMC"].ToString();
                    detail.XFDZ = dictionary["XFDZ"].ToString();
                    detail.KHYHMC = dictionary["KHYHMC"].ToString();
                    detail.QYD = dictionary["QYD"].ToString();
                    detail.ZHD = dictionary["ZHD"].ToString();
                    detail.XHD = dictionary["XHD"].ToString();
                    detail.JQBH = dictionary["JQBH"].ToString();
                    detail.YYZZH = dictionary["YYZZH"].ToString();
                    detail.MW = dictionary["MW"].ToString();
                    detail.YSHWXX = dictionary["YSHWXX"].ToString();
                    detail.HYBM = dictionary["HYBM"].ToString();
                    detail.SKR = dictionary["SKR"].ToString();
                    detail.FHR = dictionary["FHR"].ToString();
                    detail.XSBM = dictionary["XSBM"].ToString();
                    detail.XFDH = dictionary["XFDH"].ToString();
                    detail.KHYHZH = dictionary["KHYHZH"].ToString();
                    detail.MDD = dictionary["MDD"].ToString();
                    detail.JYM = dictionary["JYM"].ToString();
                    detail.SIGN = dictionary["SIGN"].ToString();
                    detail.ZFRQ = this.method_12(dictionary["ZFRQ"]);
                    detail.WSPZHM = dictionary["WSPZHM"].ToString();
                    detail.HZTZDH = dictionary["HZTZDH"].ToString();
                    detail.LZDMHM = dictionary["LZDMHM"].ToString();
                    detail.XSDJBH = dictionary["XSDJBH"].ToString();
                    detail.BMBBBH = dictionary["BMBBBH"].ToString();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            return detail;
        }

        private List<FPDetail> method_3(DataTable dataTable_0)
        {
            List<FPDetail> list = new List<FPDetail>();
            if (dataTable_0 != null)
            {
                try
                {
                    for (int i = 0; i < dataTable_0.Rows.Count; i++)
                    {
                        FPDetail item = new FPDetail {
                            FPType = (FPType) Enum.Parse(typeof(FPType), dataTable_0.Rows[i]["FPZL"].ToString(), true),
                            FPDM = dataTable_0.Rows[i]["FPDM"].ToString(),
                            FPHM = this.method_9(dataTable_0.Rows[i]["FPHM"]),
                            KPRQ = this.method_12(dataTable_0.Rows[i]["KPRQ"]),
                            GFSH = dataTable_0.Rows[i]["GFSH"].ToString(),
                            GFMC = dataTable_0.Rows[i]["GFMC"].ToString(),
                            GFDZDH = dataTable_0.Rows[i]["GFDZDH"].ToString(),
                            GFYHZH = dataTable_0.Rows[i]["GFYHZH"].ToString(),
                            XFSH = dataTable_0.Rows[i]["XFSH"].ToString(),
                            XFMC = dataTable_0.Rows[i]["XFMC"].ToString(),
                            XFDZDH = dataTable_0.Rows[i]["XFDZDH"].ToString(),
                            XFYHZH = dataTable_0.Rows[i]["XFYHZH"].ToString(),
                            HJJE = this.method_11(dataTable_0.Rows[i]["HJJE"])
                        };
                        if (string.IsNullOrEmpty(dataTable_0.Rows[i]["SLV"].ToString()))
                        {
                            item.SLV = 99.01f;
                        }
                        else
                        {
                            item.SLV = this.method_10(dataTable_0.Rows[i]["SLV"]);
                        }
                        item.HJSE = this.method_11(dataTable_0.Rows[i]["HJSE"]);
                        item.HXM = dataTable_0.Rows[i]["HXM"].ToString();
                        item.ZFBZ = this.method_8(dataTable_0.Rows[i]["ZFBZ"]);
                        item.KPR = dataTable_0.Rows[i]["KPR"].ToString();
                        item.BZ = dataTable_0.Rows[i]["BZ"].ToString();
                        item.YYSBZ = dataTable_0.Rows[i]["YYSBZ"].ToString();
                        item.QDBZ = this.method_8(dataTable_0.Rows[i]["QDBZ"]);
                        item.CM = dataTable_0.Rows[i]["CM"].ToString();
                        item.TYDH = dataTable_0.Rows[i]["TYDH"].ToString();
                        item.SCCJMC = dataTable_0.Rows[i]["SCCJMC"].ToString();
                        item.XFDZ = dataTable_0.Rows[i]["XFDZ"].ToString();
                        item.KHYHMC = dataTable_0.Rows[i]["KHYHMC"].ToString();
                        item.QYD = dataTable_0.Rows[i]["QYD"].ToString();
                        item.ZHD = dataTable_0.Rows[i]["ZHD"].ToString();
                        item.XHD = dataTable_0.Rows[i]["XHD"].ToString();
                        item.JQBH = dataTable_0.Rows[i]["JQBH"].ToString();
                        item.YYZZH = dataTable_0.Rows[i]["YYZZH"].ToString();
                        item.MW = dataTable_0.Rows[i]["MW"].ToString();
                        item.YSHWXX = dataTable_0.Rows[i]["YSHWXX"].ToString();
                        item.HYBM = dataTable_0.Rows[i]["HYBM"].ToString();
                        item.SKR = dataTable_0.Rows[i]["SKR"].ToString();
                        item.FHR = dataTable_0.Rows[i]["FHR"].ToString();
                        item.XSBM = dataTable_0.Rows[i]["XSBM"].ToString();
                        item.XFDH = dataTable_0.Rows[i]["XFDH"].ToString();
                        item.KHYHZH = dataTable_0.Rows[i]["KHYHZH"].ToString();
                        item.MDD = dataTable_0.Rows[i]["MDD"].ToString();
                        item.JYM = dataTable_0.Rows[i]["JYM"].ToString();
                        item.SIGN = dataTable_0.Rows[i]["SIGN"].ToString();
                        item.ZFRQ = this.method_12(dataTable_0.Rows[i]["ZFRQ"]);
                        item.WSPZHM = dataTable_0.Rows[i]["WSPZHM"].ToString();
                        item.HZTZDH = dataTable_0.Rows[i]["HZTZDH"].ToString();
                        item.LZDMHM = dataTable_0.Rows[i]["LZDMHM"].ToString();
                        item.XSDJBH = dataTable_0.Rows[i]["XSDJBH"].ToString();
                        item.BMBBBH = dataTable_0.Rows[i]["BMBBBH"].ToString();
                        if (dataTable_0.Rows[i]["FPZL"].ToString() == "j")
                        {
                            item.SPBM = dataTable_0.Rows[i]["ZYSPMC"].ToString();
                            string str2 = dataTable_0.Rows[i]["SPSM_BM"].ToString();
                            if (str2.IndexOf("#%") > -1)
                            {
                                item.QYZBM = str2.Substring(0, str2.IndexOf("#%"));
                                item.ZZSTSGL = str2.Substring(str2.IndexOf("#%") + 2);
                            }
                            else
                            {
                                item.QYZBM = "";
                                item.ZZSTSGL = "";
                            }
                            string str3 = dataTable_0.Rows[i]["SKR"].ToString();
                            if (str3.IndexOf("#%") > -1)
                            {
                                item.SFYH = str3.Substring(0, str3.IndexOf("#%"));
                                item.LSLBS = str3.Substring(str3.IndexOf("#%") + 2);
                            }
                            else
                            {
                                item.SFYH = "";
                                item.LSLBS = "";
                            }
                        }
                        list.Add(item);
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        private GoodsInfo method_4(object object_0)
        {
            GoodsInfo info = new GoodsInfo();
            Dictionary<string, object> dictionary = object_0 as Dictionary<string, object>;
            if (dictionary != null)
            {
                try
                {
                    info.Name = dictionary["SPMC"].ToString();
                    info.SpecMark = dictionary["GGXH"].ToString();
                    info.Unit = dictionary["JLDW"].ToString();
                    info.Num = dictionary["SL"].ToString();
                    if (((this.method_8(dictionary["HSJBZ"]) && (dictionary["SLV"] != null)) && ((dictionary["SLV"].ToString().Length > 0) && (dictionary["DJ"] != null))) && (dictionary["DJ"].ToString().Length > 0))
                    {
                        decimal d = decimal.Divide(decimal.Parse(dictionary["DJ"].ToString()), decimal.Add(decimal.Parse("1.00"), decimal.Parse(dictionary["SLV"].ToString())));
                        info.Price = decimal.Round(d, 15, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        info.Price = dictionary["DJ"].ToString();
                    }
                    info.Amount = this.method_11(dictionary["JE"]);
                    info.Tax = this.method_11(dictionary["SE"]);
                    if (string.IsNullOrEmpty(Convert.ToString(dictionary["SLV"])))
                    {
                        info.SLV = -1f;
                    }
                    else
                    {
                        info.SLV = this.method_10(dictionary["SLV"]);
                    }
                    info.TaxSign = this.method_8(dictionary["HSJBZ"]);
                    info.FPMXXH = dictionary["FPMXXH"].ToString();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            return info;
        }

        private GoodsInfo method_5(object object_0)
        {
            GoodsInfo info = new GoodsInfo();
            Dictionary<string, object> dictionary = object_0 as Dictionary<string, object>;
            if (dictionary != null)
            {
                try
                {
                    info.Name = dictionary["SPMC"].ToString();
                    info.SpecMark = dictionary["GGXH"].ToString();
                    info.Unit = dictionary["JLDW"].ToString();
                    info.Num = dictionary["SL"].ToString();
                    if (((this.method_8(dictionary["HSJBZ"]) && (dictionary["SLV"] != null)) && ((dictionary["SLV"].ToString().Length > 0) && (dictionary["DJ"] != null))) && (dictionary["DJ"].ToString().Length > 0))
                    {
                        decimal d = decimal.Divide(decimal.Parse(dictionary["DJ"].ToString()), decimal.Add(decimal.Parse("1.00"), decimal.Parse(dictionary["SLV"].ToString())));
                        info.Price = decimal.Round(d, 15, MidpointRounding.AwayFromZero).ToString();
                        if (dictionary["SLV"].ToString().Equals("0.05"))
                        {
                            info.Price = dictionary["DJ"].ToString();
                        }
                    }
                    else
                    {
                        info.Price = dictionary["DJ"].ToString();
                    }
                    info.Amount = this.method_11(dictionary["JE"]);
                    info.Tax = this.method_11(dictionary["SE"]);
                    if (string.IsNullOrEmpty(Convert.ToString(dictionary["SLV"])))
                    {
                        info.SLV = -1f;
                    }
                    else
                    {
                        info.SLV = this.method_10(dictionary["SLV"]);
                    }
                    info.TaxSign = this.method_8(dictionary["HSJBZ"]);
                    info.FPMXXH = dictionary["FPMXXH"].ToString();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            return info;
        }

        private List<GoodsInfo> method_6(DataTable dataTable_0, string string_1)
        {
            List<GoodsInfo> list = new List<GoodsInfo>();
            if (dataTable_0 != null)
            {
                try
                {
                    for (int i = 0; i < dataTable_0.Rows.Count; i++)
                    {
                        GoodsInfo item = new GoodsInfo {
                            Name = dataTable_0.Rows[i]["SPMC"].ToString(),
                            SpecMark = dataTable_0.Rows[i]["GGXH"].ToString(),
                            Unit = dataTable_0.Rows[i]["JLDW"].ToString()
                        };
                        if (string_1.Equals("p"))
                        {
                            item.Num = this.method_14(dataTable_0.Rows[i]["SL"].ToString(), 8);
                        }
                        else
                        {
                            item.Num = dataTable_0.Rows[i]["SL"].ToString();
                        }
                        if (((this.method_8(dataTable_0.Rows[i]["HSJBZ"]) && (dataTable_0.Rows[i]["SLV"] != null)) && ((dataTable_0.Rows[i]["SLV"].ToString().Length > 0) && (dataTable_0.Rows[i]["DJ"] != null))) && (dataTable_0.Rows[i]["DJ"].ToString().Length > 0))
                        {
                            decimal d = decimal.Divide(decimal.Parse(dataTable_0.Rows[i]["DJ"].ToString()), decimal.Add(decimal.Parse("1.00"), decimal.Parse(dataTable_0.Rows[i]["SLV"].ToString())));
                            if (string_1.Equals("p"))
                            {
                                item.Price = decimal.Round(d, 8, MidpointRounding.AwayFromZero).ToString();
                            }
                            else
                            {
                                item.Price = decimal.Round(d, 15, MidpointRounding.AwayFromZero).ToString();
                            }
                            if (((string_1.Equals("s") && (dataTable_0.Rows[i]["XXFPSLv"] != null)) && (dataTable_0.Rows[i]["XXFPSLv"].ToString().Equals("0.05") && (dataTable_0.Rows[i]["yysbz"].ToString().Substring(8, 1) == "0"))) || dataTable_0.Rows[i]["SLV"].ToString().Equals("0.015"))
                            {
                                item.Price = dataTable_0.Rows[i]["DJ"].ToString();
                            }
                        }
                        else if (string_1.Equals("p"))
                        {
                            item.Price = this.method_14(dataTable_0.Rows[i]["DJ"].ToString(), 8);
                        }
                        else
                        {
                            item.Price = dataTable_0.Rows[i]["DJ"].ToString();
                        }
                        item.Amount = this.method_11(dataTable_0.Rows[i]["JE"]);
                        item.Tax = this.method_11(dataTable_0.Rows[i]["SE"]);
                        if (string.IsNullOrEmpty(Convert.ToString(dataTable_0.Rows[i]["SLV"])))
                        {
                            item.SLV = -1f;
                        }
                        else
                        {
                            item.SLV = this.method_10(dataTable_0.Rows[i]["SLV"]);
                        }
                        if ((string_1.Equals("s") && (dataTable_0.Rows[i]["XXFPSLv"] != null)) && (dataTable_0.Rows[i]["XXFPSLv"].ToString().Equals("0.05") && (dataTable_0.Rows[i]["yysbz"].ToString().Substring(8, 1) == "0")))
                        {
                            item.TaxSign = true;
                        }
                        else
                        {
                            item.TaxSign = false;
                        }
                        item.FPMXXH = dataTable_0.Rows[i]["FPMXXH"].ToString();
                        item.SPBM = dataTable_0.Rows[i]["FLBM"].ToString();
                        item.SFYH = dataTable_0.Rows[i]["XSYH"].ToString();
                        item.QYZBM = dataTable_0.Rows[i]["SPBH"].ToString();
                        item.LSLBS = dataTable_0.Rows[i]["LSLVBS"].ToString();
                        item.ZZSTSGL = dataTable_0.Rows[i]["YHSM"].ToString();
                        item.RowNo = i + 1;
                        list.Add(item);
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            return list;
        }

        private GoodsInfo method_7(object object_0)
        {
            GoodsInfo info = new GoodsInfo();
            Dictionary<string, object> dictionary = object_0 as Dictionary<string, object>;
            if (dictionary != null)
            {
                try
                {
                    info.Name = dictionary["SPMC"].ToString();
                    info.SpecMark = dictionary["GGXH"].ToString();
                    info.Unit = dictionary["JLDW"].ToString();
                    info.Num = dictionary["SL"].ToString();
                    if (((this.method_8(dictionary["HSJBZ"]) && (dictionary["SLV"] != null)) && ((dictionary["SLV"].ToString().Length > 0) && (dictionary["DJ"] != null))) && (dictionary["DJ"].ToString().Length > 0))
                    {
                        decimal d = decimal.Divide(decimal.Parse(dictionary["DJ"].ToString()), decimal.Add(decimal.Parse("1.00"), decimal.Parse(dictionary["SLV"].ToString())));
                        info.Price = decimal.Round(d, 15, MidpointRounding.AwayFromZero).ToString();
                        if (dictionary["SLV"].ToString().Equals("0.05"))
                        {
                            info.Price = dictionary["DJ"].ToString();
                        }
                    }
                    else
                    {
                        info.Price = dictionary["DJ"].ToString();
                    }
                    info.Amount = this.method_11(dictionary["JE"]);
                    info.Tax = this.method_11(dictionary["SE"]);
                    if (string.IsNullOrEmpty(Convert.ToString(dictionary["SLV"])))
                    {
                        info.SLV = -1f;
                    }
                    else
                    {
                        info.SLV = this.method_10(dictionary["SLV"]);
                    }
                    info.TaxSign = this.method_8(dictionary["HSJBZ"]);
                    info.FPMXXH = dictionary["FPMXXH"].ToString();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            return info;
        }

        private bool method_8(object object_0)
        {
            if ((object_0 == null) || (object_0.ToString() == ""))
            {
                return false;
            }
            try
            {
                return Convert.ToBoolean(object_0.ToString());
            }
            catch (Exception)
            {
                return false;
            }
        }

        private long method_9(object object_0)
        {
            if ((object_0 != null) && !(object_0.ToString() == ""))
            {
                return Convert.ToInt64(object_0.ToString());
            }
            return 0L;
        }

        public DateTime SelectLastKPMaxDate()
        {
            DateTime minValue = DateTime.MinValue;
            try
            {
                string str = this.ibaseDAO_0.queryValueSQL<string>("aisino.fwkp.bsgl.selectLastKPMaxDate", null);
                minValue = this.method_12(str);
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception)
            {
            }
            return minValue;
        }
    }
}

