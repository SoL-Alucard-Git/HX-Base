namespace Aisino.Fwkp.HzfpHy.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Fwkp.HzfpHy.IDAL;
    using Aisino.Fwkp.HzfpHy.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class HZFPHY_SQD_MX : IHZFPHY_SQD_MX
    {
        private IBaseDAO baseDao = BaseDAOFactory.GetBaseDAOSQLite();
        private Dictionary<string, object> dict = new Dictionary<string, object>();

        public bool Delete(string SQDH)
        {
            this.dict.Clear();
            this.dict.Add("SQDH", SQDH);
            if (this.baseDao.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.HzfpHy.DeleteSQDMXList", this.dict) <= 0)
            {
                return false;
            }
            return true;
        }

        public bool Insert(List<Aisino.Fwkp.HzfpHy.Model.HZFPHY_SQD_MX> models)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (Aisino.Fwkp.HzfpHy.Model.HZFPHY_SQD_MX hzfphy_sqd_mx in models)
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                item.Add("SQDH", hzfphy_sqd_mx.SQDH);
                item.Add("MXXH", hzfphy_sqd_mx.MXXH);
                item.Add("JE", hzfphy_sqd_mx.JE);
                item.Add("SE", hzfphy_sqd_mx.SE);
                item.Add("SPMC", hzfphy_sqd_mx.SPMC);
                item.Add("HSJBZ", hzfphy_sqd_mx.HSJBZ);
                item.Add("FPHXZ", hzfphy_sqd_mx.FPHXZ);
                item.Add("FLBM", hzfphy_sqd_mx.FLBM);
                item.Add("QYSPBM", hzfphy_sqd_mx.QYSPBM);
                item.Add("SFXSYHZC", hzfphy_sqd_mx.SFXSYHZC);
                item.Add("YHZCMC", hzfphy_sqd_mx.YHZCMC);
                item.Add("LSLBS", hzfphy_sqd_mx.LSLBS);
                list.Add(item);
            }
            if (this.baseDao.未确认DAO方法3("aisino.Fwkp.HzfpHy.InsertSQDMX", list) <= 0)
            {
                return false;
            }
            return true;
        }

        public DataTable SelectList(string SQDH)
        {
            this.dict.Clear();
            this.dict.Add("SQDH", SQDH);
            return this.baseDao.querySQLDataTable("aisino.Fwkp.HzfpHy.SelectSQDMXList", this.dict);
        }

        public bool Updata(Aisino.Fwkp.HzfpHy.Model.HZFPHY_SQD_MX model)
        {
            this.dict.Clear();
            this.dict.Add("SQDH", model.SQDH);
            this.dict.Add("MXXH", model.MXXH);
            this.dict.Add("JE", model.JE);
            this.dict.Add("SPMC", model.SPMC);
            this.dict.Add("HSJBZ", model.HSJBZ);
            this.dict.Add("FPHXZ", model.FPHXZ);
            this.dict.Add("FlBM", model.FLBM);
            this.dict.Add("QYSPBM", model.QYSPBM);
            this.dict.Add("SFXSYHZC", model.SFXSYHZC);
            this.dict.Add("LSLBS", model.LSLBS);
            this.dict.Add("FLBM", model.FLBM);
            this.dict.Add("QYSPBM", model.QYSPBM);
            this.dict.Add("SFXSYHZC", model.SFXSYHZC);
            this.dict.Add("YHZCMC", model.YHZCMC);
            this.dict.Add("LSLBS", model.LSLBS);
            if (this.baseDao.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.HzfpHy.UpdateSQDMX", this.dict) <= 0)
            {
                return false;
            }
            return true;
        }
    }
}

