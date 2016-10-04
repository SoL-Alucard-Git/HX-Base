namespace Aisino.Fwkp.Bmgl.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Bmgl.IDAL;
    using System;
    using System.Collections.Generic;

    internal class BMSCCJManager : IBMSCCJManager
    {
        private int _currentPage = 1;
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private Dictionary<string, object> condition = new Dictionary<string, object>();
        private string SQLID;

        public AisinoDataSet QueryByKey(string KeyWord, int pagesize, int pageno)
        {
            this.condition.Clear();
            this.condition.Add("key", "%" + KeyWord + "%");
            this.SQLID = "aisino.Fwkp.Bmgl.BMSCCJ.SCCJQueryByKey";
            return this.baseDAO.querySQLDataSet(this.SQLID, this.condition, pagesize, pageno);
        }

        public AisinoDataSet QuerySCCJ(int pagesize, int pageno)
        {
            this.condition.Clear();
            this.condition.Add("key", "%");
            this.SQLID = "aisino.Fwkp.Bmgl.BMSCCJ.SCCJbmlike";
            return this.baseDAO.querySQLDataSet(this.SQLID, this.condition, pagesize, pageno);
        }

        public AisinoDataSet QueryTable(int pagesize, int pageno)
        {
            this.condition.Clear();
            this.SQLID = "aisino.Fwkp.Bmgl.BMSCCJ.SCCJQueryTable";
            return this.baseDAO.querySQLDataSet(this.SQLID, this.condition, pagesize, pageno);
        }

        public int CurrentPage
        {
            get
            {
                return this._currentPage;
            }
            set
            {
                this._currentPage = value;
            }
        }

        public int Pagesize
        {
            get
            {
                int num;
                string s = PropertyUtil.GetValue("pagesize");
                if (((s == null) || (s.Length == 0)) || !int.TryParse(s, out num))
                {
                    num = 10;
                    PropertyUtil.SetValue("pagesize", s);
                }
                return num;
            }
            set
            {
                PropertyUtil.SetValue("pagesize", value.ToString());
            }
        }
    }
}

