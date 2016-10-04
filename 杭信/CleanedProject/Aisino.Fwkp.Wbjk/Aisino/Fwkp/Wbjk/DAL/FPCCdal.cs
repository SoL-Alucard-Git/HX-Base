namespace Aisino.Fwkp.Wbjk.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.Controls;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal class FPCCdal
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private ILog loger = LogUtil.GetLogger<FPCCdal>();

        public DataTable GetFaPiao(DateTime StartDate, DateTime EndDate, string FpType)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("QSRQ", StartDate);
            dictionary.Add("JZRQ", EndDate);
            dictionary.Add("FPZL", FpType);
            return this.baseDAO.querySQLDataTable("aisino.Fwkp.Wbjk.FPCCGetFaPiao", dictionary);
        }

        public AisinoDataSet QueryFaPiao(DateTime StartDate, DateTime EndDate, string FpType, int pagesize, int pageno)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("QSRQ", StartDate);
            dictionary.Add("JZRQ", EndDate);
            dictionary.Add("FPZL", FpType);
            return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.FPCCSelect", dictionary, pagesize, pageno);
        }
    }
}

