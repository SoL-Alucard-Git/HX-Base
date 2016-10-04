namespace Aisino.Fwkp.HzfpHy.BLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.HzfpHy.IBLL;
    using Aisino.Fwkp.HzfpHy.IDAL;
    using Aisino.Fwkp.HzfpHy.Model;
    using System;
    using System.Collections.Generic;

    internal class HZFPHY_SQD : Aisino.Fwkp.HzfpHy.IBLL.IHZFPHY_SQD
    {
        private int _currentPage = 1;
        private Aisino.Fwkp.HzfpHy.IDAL.IHZFPHY_SQD dal = new Aisino.Fwkp.HzfpHy.DAL.HZFPHY_SQD();

        public bool Delete(string SQDH)
        {
            return this.dal.Delete(SQDH);
        }

        public bool Insert(Aisino.Fwkp.HzfpHy.Model.HZFPHY_SQD model)
        {
            return this.dal.Insert(model);
        }

        public Aisino.Fwkp.HzfpHy.Model.HZFPHY_SQD Select(string SQDH)
        {
            return this.dal.Select(SQDH);
        }

        public AisinoDataSet SelectList(int page, int count, Dictionary<string, object> dict)
        {
            return this.dal.SelectList(page, count, dict);
        }

        public bool Updata(Aisino.Fwkp.HzfpHy.Model.HZFPHY_SQD model)
        {
            return this.dal.Updata(model);
        }

        public bool Updatazt(Aisino.Fwkp.HzfpHy.Model.HZFPHY_SQD model)
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
                string s = PropertyUtil.GetValue("Aisino.Fwkp.HzfpHy.pagesize");
                if (((s == null) || (s.Length == 0)) || !int.TryParse(s, out num))
                {
                    num = 5;
                    PropertyUtil.SetValue("Aisino.Fwkp.HzfpHy.pagesize", s);
                }
                return num;
            }
            set
            {
                PropertyUtil.SetValue("Aisino.Fwkp.HzfpHy.pagesize", value.ToString());
            }
        }
    }
}

