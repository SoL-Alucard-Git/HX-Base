namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    public class DataSumDAL
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private ILog loger = LogUtil.GetLogger<DataSumDAL>();

        public ArrayList GetDateSegment()
        {
            ArrayList list;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            try
            {
                list = this.baseDAO.querySQL("aisino.Fwkp.Bsgl.GetDateXXFP", dictionary);
            }
            catch (Exception exception)
            {
                this.loger.Debug("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return list;
        }

        public DataTable GetDetail(DateTime dtStart, DateTime dtEnd)
        {
            DataTable table;
            string str = "aisino.Fwkp.Bsgl.GetDetailXXFP";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("STARTDATE", dtStart);
            dictionary.Add("ENDDATE", dtEnd);
            try
            {
                table = this.baseDAO.querySQLDataTable(str, dictionary);
            }
            catch (Exception exception)
            {
                this.loger.Debug("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return table;
        }

        public int GetFPNum(string fpzl, string ssyf)
        {
            int num = 0;
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("FPZL", fpzl);
                dictionary.Add("SSYF", ssyf);
                string str = "aisino.fwkp.bsgl.GetFPNum";
                num = this.baseDAO.queryValueSQL<int>(str, dictionary);
            }
            catch (Exception exception)
            {
                this.loger.Error("获取发票张数异常：" + exception.ToString());
            }
            return num;
        }

        public ArrayList GetMonthSegment(int nYear)
        {
            ArrayList list;
            DateTime time = Convert.ToDateTime(nYear.ToString() + "-01-01");
            DateTime time2 = Convert.ToDateTime(((nYear + 1)).ToString() + "-01-01");
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("STARTDATE", time);
            dictionary.Add("ENDDATE", time2);
            try
            {
                list = this.baseDAO.querySQL("aisino.Fwkp.Bsgl.GetMonthXXFP", dictionary);
            }
            catch (Exception exception)
            {
                this.loger.Debug("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return list;
        }

        public ArrayList GetTaxPeriodCollections(int nYear, int nMonth)
        {
            ArrayList list;
            DateTime time = Convert.ToDateTime(nYear.ToString() + "-" + nMonth.ToString() + "-01");
            if (nMonth >= 12)
            {
                nYear++;
                nMonth /= 12;
            }
            DateTime time2 = Convert.ToDateTime(nYear.ToString() + "-" + ((nMonth + 1)).ToString() + "-01");
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("STARTDATE", time);
            dictionary.Add("ENDDATE", time2);
            try
            {
                list = this.baseDAO.querySQL("aisino.Fwkp.Bsgl.GetTaxPeriodXXFP", dictionary);
            }
            catch (Exception exception)
            {
                this.loger.Debug("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return list;
        }
    }
}

