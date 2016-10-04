namespace Aisino.Fwkp.Hzfp.BLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Hzfp.IBLL;
    using Aisino.Fwkp.Hzfp.IDAL;
    using Aisino.Fwkp.Hzfp.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal class HZFP_SQD : IHZFP_SQD
    {
        private int _currentPage = 1;
        private Aisino.Fwkp.Hzfp.IDAL.HZFP_SQD dal = new Aisino.Fwkp.Hzfp.DAL.HZFP_SQD();

        public bool Delete(string SQDH)
        {
            return this.dal.Delete(SQDH);
        }

        public DataTable GetData(int Year)
        {
            return this.dal.GetData(Year);
        }

        public bool Insert(Aisino.Fwkp.Hzfp.Model.HZFP_SQD model)
        {
            return this.dal.Insert(model);
        }

        public Aisino.Fwkp.Hzfp.Model.HZFP_SQD Select(string SQDH)
        {
            return this.dal.Select(SQDH);
        }

        public AisinoDataSet SelectList(int page, int count, int month)
        {
            return this.dal.SelectList(page, count, month);
        }

        public AisinoDataSet SelectSelList(int page, int count, int month, string xfsh)
        {
            return this.dal.SelectSelList(page, count, month, xfsh);
        }

        public AisinoDataSet SelectSqdlist(int page, int count, Dictionary<string, object> dict)
        {
            return this.dal.SelectSqdlist(page, count, dict);
        }

        public AisinoDataSet SelectSqdradlist(int page, int count, Dictionary<string, object> dict)
        {
            return this.dal.SelectSqdradlist(page, count, dict);
        }

        public bool Updata(Aisino.Fwkp.Hzfp.Model.HZFP_SQD model)
        {
            return this.dal.Updata(model);
        }

        public bool Updatazt(Aisino.Fwkp.Hzfp.Model.HZFP_SQD model)
        {
            return this.dal.Updatazt(model);
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
                string s = PropertyUtil.GetValue("Aisino.Fwkp.Hzfp.pagesize");
                if (((s == null) || (s.Length == 0)) || (!int.TryParse(s, out num) || (s == "0")))
                {
                    PropertyUtil.SetValue("Aisino.Fwkp.Hzfp.pagesize", 30.ToString());
                }
                return Convert.ToInt32(PropertyUtil.GetValue("Aisino.Fwkp.Hzfp.pagesize"));
            }
            set
            {
                PropertyUtil.SetValue("Aisino.Fwkp.Hzfp.pagesize", value.ToString());
            }
        }
    }
}

