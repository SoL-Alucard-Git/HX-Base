namespace Aisino.Fwkp.Fpzpz.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Fwkp.Fpzpz.IDAL;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal class KMProperty : IKMProperty
    {
        private IBaseDAO baseDao;
        private ILog loger = LogUtil.GetLogger<KMProperty>();

        public KMProperty()
        {
            try
            {
                this.baseDao = BaseDAOFactory.GetBaseDAOSQLite();
            }
            catch (Exception exception)
            {
                this.loger.Error("发票转凭证错误：" + exception.Message);
            }
        }

        public bool Delete()
        {
            bool flag = false;
            try
            {
                string str = "aisino.fwkp.Fpzpz.DeleteKMPrpperty";
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                this.baseDao.updateSQL(str, dictionary);
                flag = true;
            }
            catch (Exception exception)
            {
                this.loger.Error("发票转凭证清空科目属性表错误：" + exception.Message);
            }
            return flag;
        }

        public bool Delete(string BM)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(BM))
            {
                try
                {
                    string str = "aisino.fwkp.Fpzpz.DeleteOneRecordKMPrpperty";
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("BM", BM);
                    this.baseDao.updateSQL(str, dictionary);
                    flag = true;
                }
                catch (Exception exception)
                {
                    this.loger.Error("发票转凭证清空科目属性表错误：" + exception.Message);
                }
            }
            return flag;
        }

        public DataTable GetKMInfo()
        {
            DataTable table = new DataTable();
            try
            {
                string str = "aisino.fwkp.Fpzpz.SelectKMPrppertyInfo";
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                table = this.baseDao.querySQLDataTable(str, dictionary);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
            return table;
        }

        public List<string> GetKMPropertyBM()
        {
            List<string> list = new List<string>();
            try
            {
                string str = "aisino.fwkp.Fpzpz.SelectKMPrppertyBM";
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                DataTable table = this.baseDao.querySQLDataTable(str, dictionary);
                if ((table == null) || (table.Rows.Count <= 0))
                {
                    return list;
                }
                foreach (DataRow row in table.Rows)
                {
                    list.Add(row["BM"].ToString());
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("发票转凭证异常：" + exception.Message);
            }
            return list;
        }

        public bool ReplaceRecord(Dictionary<string, object> dict)
        {
            bool flag = false;
            if ((dict != null) && (dict.Count >= 1))
            {
                try
                {
                    string str = "aisino.fwkp.Fpzpz.ReplaceKMProperty";
                    this.baseDao.updateSQL(str, dict);
                    flag = true;
                }
                catch (Exception exception)
                {
                    this.loger.Error("发票转凭证异常：" + exception.Message);
                }
            }
            return flag;
        }

        public bool ReplaceRecords(List<Dictionary<string, object>> list)
        {
            bool flag = false;
            if ((list != null) && (list.Count >= 1))
            {
                try
                {
                    List<string> list2 = new List<string>();
                    for (int i = 0; i < list.Count; i++)
                    {
                        list2.Add("aisino.fwkp.Fpzpz.ReplaceKMProperty");
                    }
                    this.baseDao.updateSQL(list2.ToArray(), list);
                    flag = true;
                }
                catch (Exception exception)
                {
                    this.loger.Error("发票转凭证异常：" + exception.Message);
                }
            }
            return flag;
        }
    }
}

