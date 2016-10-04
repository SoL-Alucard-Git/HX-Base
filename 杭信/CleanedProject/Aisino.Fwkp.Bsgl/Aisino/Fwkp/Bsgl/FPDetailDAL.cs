namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class FPDetailDAL
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private ILog loger = LogUtil.GetLogger<FPDetailDAL>();

        private bool ConvertToBool(object value)
        {
            if ((value == null) || (value.ToString() == ""))
            {
                return false;
            }
            try
            {
                return Convert.ToBoolean(value.ToString());
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message, exception);
                return false;
            }
        }

        private DateTime ConvertToDateTime(object value)
        {
            if ((value == null) || (value.ToString() == ""))
            {
                return DateTime.MinValue;
            }
            try
            {
                return Convert.ToDateTime(value.ToString());
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message, exception);
                return DateTime.MinValue;
            }
        }

        private decimal ConvertToDecimal(object value)
        {
            if ((value != null) && !(value.ToString() == ""))
            {
                return Convert.ToDecimal(value.ToString());
            }
            return 0M;
        }

        private float ConvertToFloat(object value)
        {
            if ((!(value is DBNull) && (value != null)) && !(value.ToString() == ""))
            {
                return Convert.ToSingle(value.ToString());
            }
            return 0f;
        }

        private long ConvertToLong(object value)
        {
            if ((value != null) && !(value.ToString() == ""))
            {
                return Convert.ToInt64(value.ToString());
            }
            return 0L;
        }

        private FPDetail CreateFPMainInfo(object item)
        {
            FPDetail detail = new FPDetail();
            Dictionary<string, object> dictionary = item as Dictionary<string, object>;
            if (dictionary != null)
            {
                try
                {
                    detail.BMBBBH = dictionary["BMBBBH"].ToString();
                    detail.FPType = (FPType) Enum.Parse(typeof(FPType), dictionary["FPZL"].ToString(), true);
                    detail.FPDM = dictionary["FPDM"].ToString();
                    detail.FPHM = this.ConvertToLong(dictionary["FPHM"]);
                    detail.KPRQ = this.ConvertToDateTime(dictionary["KPRQ"]);
                    detail.GFSH = dictionary["GFSH"].ToString();
                    detail.GFMC = dictionary["GFMC"].ToString();
                    detail.GFDZDH = dictionary["GFDZDH"].ToString();
                    detail.GFYHZH = dictionary["GFYHZH"].ToString();
                    detail.XFSH = dictionary["XFSH"].ToString();
                    detail.XFMC = dictionary["XFMC"].ToString();
                    detail.XFDZDH = dictionary["XFDZDH"].ToString();
                    detail.XFYHZH = dictionary["XFYHZH"].ToString();
                    detail.HJJE = this.ConvertToDecimal(dictionary["HJJE"]);
                    if (string.IsNullOrEmpty(dictionary["SLV"].ToString()))
                    {
                        detail.SLV = 99.01f;
                    }
                    else
                    {
                        detail.SLV = this.ConvertToFloat(dictionary["SLV"]);
                    }
                    detail.HJSE = this.ConvertToDecimal(dictionary["HJSE"]);
                    detail.HXM = dictionary["HXM"].ToString();
                    detail.ZFBZ = this.ConvertToBool(dictionary["ZFBZ"]);
                    detail.KPR = dictionary["KPR"].ToString();
                    detail.BZ = dictionary["BZ"].ToString();
                    detail.YYSBZ = dictionary["YYSBZ"].ToString();
                    detail.QDBZ = this.ConvertToBool(dictionary["QDBZ"]);
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
                    detail.ZFRQ = this.ConvertToDateTime(dictionary["ZFRQ"]);
                    detail.WSPZHM = dictionary["WSPZHM"].ToString();
                    detail.HZTZDH = dictionary["HZTZDH"].ToString();
                    detail.LZDMHM = dictionary["LZDMHM"].ToString();
                    detail.XSDJBH = dictionary["XSDJBH"].ToString();
                    detail.SPBM = dictionary["ZYSPMC"].ToString();
                    string str2 = dictionary["SPSM_BM"].ToString();
                    if (str2.IndexOf("#%") > -1)
                    {
                        detail.QYZBM = str2.Substring(0, str2.IndexOf("#%"));
                        detail.ZZSTSGL = str2.Substring(str2.IndexOf("#%") + 2);
                    }
                    else
                    {
                        detail.QYZBM = detail.ZZSTSGL = "";
                    }
                    str2 = dictionary["SKR"].ToString();
                    if (str2.IndexOf("#%") > -1)
                    {
                        detail.SFYH = str2.Substring(0, str2.IndexOf("#%"));
                        detail.LSLBS = str2.Substring(str2.IndexOf("#%") + 2);
                        return detail;
                    }
                    detail.SFYH = detail.LSLBS = "";
                }
                catch (Exception exception)
                {
                    this.loger.Error(exception.Message, exception);
                    throw exception;
                }
            }
            return detail;
        }

        private GoodsInfo CreateGoodsInfo(object item)
        {
            GoodsInfo info = new GoodsInfo();
            Dictionary<string, object> dictionary = item as Dictionary<string, object>;
            if (dictionary != null)
            {
                try
                {
                    info.Name = dictionary["SPMC"].ToString();
                    info.SpecMark = dictionary["GGXH"].ToString();
                    info.Unit = dictionary["JLDW"].ToString();
                    info.Num = dictionary["SL"].ToString();
                    if (((this.ConvertToBool(dictionary["HSJBZ"]) && (dictionary["SLV"] != null)) && ((dictionary["SLV"].ToString().Length > 0) && (dictionary["DJ"] != null))) && (dictionary["DJ"].ToString().Length > 0))
                    {
                        decimal d = decimal.Divide(decimal.Parse(dictionary["DJ"].ToString()), decimal.Add(decimal.Parse("1.00"), decimal.Parse(dictionary["SLV"].ToString())));
                        info.Price = decimal.Round(d, 15, MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        info.Price = dictionary["DJ"].ToString();
                    }
                    info.Amount = this.ConvertToDecimal(dictionary["JE"]);
                    info.Tax = this.ConvertToDecimal(dictionary["SE"]);
                    if (string.IsNullOrEmpty(Convert.ToString(dictionary["SLV"])))
                    {
                        info.SLV = -1f;
                    }
                    else
                    {
                        info.SLV = this.ConvertToFloat(dictionary["SLV"]);
                    }
                    info.TaxSign = this.ConvertToBool(dictionary["HSJBZ"]);
                    info.FPMXXH = dictionary["FPMXXH"].ToString();
                    info.SPBM = dictionary["FLBM"].ToString();
                    info.SFYH = dictionary["XSYH"].ToString();
                    info.QYZBM = dictionary["SPBH"].ToString();
                    info.LSLBS = dictionary["LSLVBS"].ToString();
                    info.ZZSTSGL = dictionary["YHSM"].ToString();
                }
                catch (Exception exception)
                {
                    this.loger.Error(exception.Message, exception);
                    throw exception;
                }
            }
            return info;
        }

        private GoodsInfo CreateGoodsInfo_(object item)
        {
            GoodsInfo info = new GoodsInfo();
            Dictionary<string, object> dictionary = item as Dictionary<string, object>;
            if (dictionary != null)
            {
                try
                {
                    info.Name = dictionary["SPMC"].ToString();
                    info.SpecMark = dictionary["GGXH"].ToString();
                    info.Unit = dictionary["JLDW"].ToString();
                    info.Num = dictionary["SL"].ToString();
                    if (((this.ConvertToBool(dictionary["HSJBZ"]) && (dictionary["SLV"] != null)) && ((dictionary["SLV"].ToString().Length > 0) && (dictionary["DJ"] != null))) && (dictionary["DJ"].ToString().Length > 0))
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
                    info.Amount = this.ConvertToDecimal(dictionary["JE"]);
                    info.Tax = this.ConvertToDecimal(dictionary["SE"]);
                    if (string.IsNullOrEmpty(Convert.ToString(dictionary["SLV"])))
                    {
                        info.SLV = -1f;
                    }
                    else
                    {
                        info.SLV = this.ConvertToFloat(dictionary["SLV"]);
                    }
                    info.TaxSign = this.ConvertToBool(dictionary["HSJBZ"]);
                    info.FPMXXH = dictionary["FPMXXH"].ToString();
                    info.SPBM = dictionary["FLBM"].ToString();
                    info.SFYH = dictionary["XSYH"].ToString();
                    info.QYZBM = dictionary["SPBH"].ToString();
                    info.LSLBS = dictionary["LSLVBS"].ToString();
                    info.ZZSTSGL = dictionary["YHSM"].ToString();
                }
                catch (Exception exception)
                {
                    this.loger.Error(exception.Message, exception);
                    throw exception;
                }
            }
            return info;
        }

        private GoodsInfo CreateGoodsInfoFPExport(object item)
        {
            GoodsInfo info = new GoodsInfo();
            Dictionary<string, object> dictionary = item as Dictionary<string, object>;
            if (dictionary != null)
            {
                try
                {
                    info.Name = dictionary["SPMC"].ToString();
                    info.SpecMark = dictionary["GGXH"].ToString();
                    info.Unit = dictionary["JLDW"].ToString();
                    info.Num = dictionary["SL"].ToString();
                    if (((this.ConvertToBool(dictionary["HSJBZ"]) && (dictionary["SLV"] != null)) && ((dictionary["SLV"].ToString().Length > 0) && (dictionary["DJ"] != null))) && (dictionary["DJ"].ToString().Length > 0))
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
                    info.Amount = this.ConvertToDecimal(dictionary["JE"]);
                    info.Tax = this.ConvertToDecimal(dictionary["SE"]);
                    if (string.IsNullOrEmpty(Convert.ToString(dictionary["SLV"])))
                    {
                        info.SLV = -1f;
                    }
                    else
                    {
                        info.SLV = this.ConvertToFloat(dictionary["SLV"]);
                    }
                    info.TaxSign = this.ConvertToBool(dictionary["HSJBZ"]);
                    info.FPMXXH = dictionary["FPMXXH"].ToString();
                    info.SPBM = dictionary["FLBM"].ToString();
                    info.SFYH = dictionary["XSYH"].ToString();
                    info.QYZBM = dictionary["SPBH"].ToString();
                    info.LSLBS = dictionary["LSLVBS"].ToString();
                    info.ZZSTSGL = dictionary["YHSM"].ToString();
                }
                catch (Exception exception)
                {
                    this.loger.Error(exception.Message, exception);
                    throw exception;
                }
            }
            return info;
        }

        public List<FPDetail> GetFPDetailList(DateTime startDate, DateTime endDate)
        {
            List<FPDetail> list = new List<FPDetail>();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                DateTime time = Convert.ToDateTime(endDate.AddDays(1.0).ToShortDateString());
                dictionary.Add("QSRQ", startDate);
                dictionary.Add("ZZRQ", time);
                foreach (object obj2 in this.baseDAO.querySQL("aisino.fwkp.bsgl.selectXxfpByDate", dictionary))
                {
                    FPDetail item = this.CreateFPMainInfo(obj2);
                    list.Add(item);
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
                this.loger.Error(exception.get_UserMessage() + exception.Message, exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message, exception2);
            }
            return list;
        }

        public List<FPDetail> GetFPDetailList(int ssyf, string taxCode)
        {
            List<FPDetail> list = new List<FPDetail>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("XFSH", taxCode);
            dictionary.Add("SSYF", ssyf);
            try
            {
                foreach (object obj2 in this.baseDAO.querySQL("aisino.fwkp.bsgl.selectXxfpByMonth", dictionary))
                {
                    FPDetail item = this.CreateFPMainInfo(obj2);
                    list.Add(item);
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
                this.loger.Error(exception.get_UserMessage() + exception.Message, exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message, exception2);
            }
            return list;
        }

        public List<FPDetail> GetFPDetailListByFPZL(int ssyf, string taxCode, string fpzl)
        {
            List<FPDetail> list = new List<FPDetail>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("XFSH", taxCode);
            dictionary.Add("SSYF", ssyf);
            dictionary.Add("FPZL", fpzl);
            try
            {
                foreach (object obj2 in this.baseDAO.querySQL("aisino.fwkp.bsgl.selectXxfpByMonthFPZL", dictionary))
                {
                    FPDetail item = this.CreateFPMainInfo(obj2);
                    if (!string.IsNullOrEmpty(item.SIGN) && !string.IsNullOrEmpty(item.YYSBZ))
                    {
                        list.Add(item);
                    }
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
                this.loger.Error(exception.get_UserMessage() + exception.Message, exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message, exception2);
            }
            return list;
        }

        public List<FPDetail> GetFPDetailListByFPZL(DateTime startDate, DateTime endDate, string FPZL, bool QDBZ = false)
        {
            List<FPDetail> list = new List<FPDetail>();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                DateTime time = Convert.ToDateTime(endDate.AddDays(1.0).ToShortDateString());
                dictionary.Add("QSRQ", startDate);
                dictionary.Add("ZZRQ", time);
                dictionary.Add("FPZL", FPZL);
                if (!QDBZ)
                {
                    dictionary.Add("QDBZ", 0);
                }
                else
                {
                    dictionary.Add("QDBZ", 1);
                }
                foreach (object obj2 in this.baseDAO.querySQL("aisino.fwkp.bsgl.selectXxfpByDateFPZL", dictionary))
                {
                    FPDetail item = this.CreateFPMainInfo(obj2);
                    list.Add(item);
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
                this.loger.Error(exception.get_UserMessage() + exception.Message, exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message, exception2);
            }
            return list;
        }

        public List<GoodsInfo> GetGoodsList(string fpzl, string fpdm, long fphm)
        {
            List<GoodsInfo> list = new List<GoodsInfo>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPZL", fpzl);
            dictionary.Add("FPDM", fpdm);
            dictionary.Add("FPHM", fphm);
            try
            {
                ArrayList list2 = this.baseDAO.querySQL("aisino.fwkp.bsgl.selectXxfpMc", dictionary);
                int num = 0;
                foreach (object obj2 in list2)
                {
                    GoodsInfo item = null;
                    if (fpzl.Equals("s"))
                    {
                        item = this.CreateGoodsInfo_(obj2);
                    }
                    else
                    {
                        item = this.CreateGoodsInfo(obj2);
                    }
                    item.RowNo = ++num;
                    list.Add(item);
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
                this.loger.Error(exception.get_UserMessage() + exception.Message, exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message, exception2);
            }
            return list;
        }

        public List<GoodsInfo> GetGoodsListFPExport(string fpzl, string fpdm, long fphm)
        {
            List<GoodsInfo> list = new List<GoodsInfo>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPZL", fpzl);
            dictionary.Add("FPDM", fpdm);
            dictionary.Add("FPHM", fphm);
            try
            {
                ArrayList list2 = this.baseDAO.querySQL("aisino.fwkp.bsgl.selectXxfpMc", dictionary);
                int num = 0;
                foreach (object obj2 in list2)
                {
                    GoodsInfo item = this.CreateGoodsInfoFPExport(obj2);
                    item.RowNo = ++num;
                    list.Add(item);
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
                this.loger.Error(exception.get_UserMessage() + exception.Message, exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message, exception2);
            }
            return list;
        }

        public List<GoodsInfo> GetGoodsQDList(string fpzl, string fpdm, long fphm)
        {
            List<GoodsInfo> list = new List<GoodsInfo>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPZL", fpzl);
            dictionary.Add("FPDM", fpdm);
            dictionary.Add("FPHM", fphm);
            try
            {
                ArrayList list2 = this.baseDAO.querySQL("aisino.fwkp.bsgl.selectXxfpMcQD", dictionary);
                int num = 0;
                foreach (object obj2 in list2)
                {
                    GoodsInfo info;
                    if (fpzl.Equals("s"))
                    {
                        info = this.CreateGoodsInfo_(obj2);
                    }
                    else
                    {
                        info = this.CreateGoodsInfo(obj2);
                    }
                    info.RowNo = ++num;
                    list.Add(info);
                }
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
                this.loger.Error(exception.get_UserMessage() + exception.Message, exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message, exception2);
            }
            return list;
        }

        public DateTime SelectLastKPMaxDate()
        {
            DateTime minValue = DateTime.MinValue;
            try
            {
                string str = this.baseDAO.queryValueSQL<string>("aisino.fwkp.bsgl.selectLastKPMaxDate", null);
                minValue = this.ConvertToDateTime(str);
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
                this.loger.Error(exception.get_UserMessage() + exception.Message, exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message, exception2);
            }
            return minValue;
        }

        public ArrayList UpDateBSBZ(bool bsbza, bool bsbzb, DateTime kprq, string FPZL, string bsq)
        {
            ArrayList list = new ArrayList();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("BSBZA", bsbza);
                dictionary.Add("BSBZB", bsbzb);
                dictionary.Add("KPRQ", kprq);
                dictionary.Add("FPZL", FPZL);
                dictionary.Add("BSQ", bsq);
                list = this.baseDAO.querySQL("aisino.fwkp.bsgl.updateBSBZ", dictionary);
            }
            catch (BaseException exception)
            {
                ExceptionHandler.HandleError(exception);
                this.loger.Error(exception.get_UserMessage() + exception.Message, exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message, exception2);
            }
            return list;
        }
    }
}

