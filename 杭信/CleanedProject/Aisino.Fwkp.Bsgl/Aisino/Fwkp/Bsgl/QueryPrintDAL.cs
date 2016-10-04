namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class QueryPrintDAL
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private ILog loger = LogUtil.GetLogger<QueryPrintDAL>();

        public DataTable GetMinusTable(string strType, bool bWaste, DateTime dtStart, DateTime dtEnd, int taxPeriod)
        {
            return this.GetTable("aisino.Fwkp.Bsgl.GetMinusTotalXXFP", strType, bWaste, dtStart, dtEnd, taxPeriod);
        }

        public DataTable GetPlusTable(string strType, bool bWaste, DateTime dtStart, DateTime dtEnd, int taxPeriod)
        {
            return this.GetTable("aisino.Fwkp.Bsgl.GetPlusTotalXXFP", strType, bWaste, dtStart, dtEnd, taxPeriod);
        }

        private DataTable GetTable(string strSQL, string strType, bool bWaste, DateTime dtStart, DateTime dtEnd, int taxPeriod)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPZL", strType);
            dictionary.Add("ZFBZ", bWaste);
            dictionary.Add("STARTDATE", dtStart);
            dictionary.Add("ENDDATE", dtEnd);
            dictionary.Add("BSQ", taxPeriod);
            DataTable table = new DataTable();
            try
            {
                DataTable table2 = this.baseDAO.querySQLDataTable(strSQL, dictionary);
                foreach (DataColumn column in table2.Columns)
                {
                    table.Columns.Add(column.ColumnName, column.DataType);
                }
                table.Columns["合计金额"].DataType = typeof(string);
                table.Columns["合计税额"].DataType = typeof(string);
                table.Columns["发票号码"].DataType = typeof(string);
                foreach (DataRow row in table2.Rows)
                {
                    DataRow row2 = table.NewRow();
                    foreach (DataColumn column2 in table2.Columns)
                    {
                        row2[column2.ColumnName] = row[column2.ColumnName];
                    }
                    table.Rows.Add(row2);
                }
                table.AcceptChanges();
            }
            catch (Exception exception)
            {
                this.loger.Debug("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return table;
        }
    }
}

