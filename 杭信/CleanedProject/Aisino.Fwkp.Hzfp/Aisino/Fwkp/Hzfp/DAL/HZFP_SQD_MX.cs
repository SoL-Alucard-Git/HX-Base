namespace Aisino.Fwkp.Hzfp.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Fwkp.Hzfp.IDAL;
    using Aisino.Fwkp.Hzfp.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class HZFP_SQD_MX : Aisino.Fwkp.Hzfp.IDAL.HZFP_SQD_MX
    {
        private IBaseDAO baseDao = BaseDAOFactory.GetBaseDAOSQLite();
        private Dictionary<string, object> dict = new Dictionary<string, object>();

        public bool Delete(string SQDH)
        {
            this.dict.Clear();
            this.dict.Add("SQDH", SQDH);
            if (this.baseDao.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Hzfp.DeleteSQDMXList", this.dict) <= 0)
            {
                return false;
            }
            return true;
        }

        public bool Insert(List<Aisino.Fwkp.Hzfp.Model.HZFP_SQD_MX> models)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (Aisino.Fwkp.Hzfp.Model.HZFP_SQD_MX hzfp_sqd_mx in models)
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                item.Add("SQDH", hzfp_sqd_mx.SQDH);
                item.Add("MXXH", hzfp_sqd_mx.MXXH);
                item.Add("JE", hzfp_sqd_mx.JE);
                if (hzfp_sqd_mx.SLV == -1.0)
                {
                    item.Add("SLV", null);
                    item.Add("SE", hzfp_sqd_mx.SE);
                }
                else
                {
                    item.Add("SLV", hzfp_sqd_mx.SLV);
                    item.Add("SE", hzfp_sqd_mx.SE);
                }
                item.Add("SPMC", hzfp_sqd_mx.SPMC);
                item.Add("SPSM", hzfp_sqd_mx.SPSM);
                item.Add("GGXH", hzfp_sqd_mx.GGXH);
                item.Add("JLDW", hzfp_sqd_mx.JLDW);
                if (hzfp_sqd_mx.SL == 0.0)
                {
                    item.Add("SL", null);
                }
                else
                {
                    item.Add("SL", hzfp_sqd_mx.SL);
                }
                if (hzfp_sqd_mx.DJ == 0M)
                {
                    item.Add("DJ", null);
                }
                else
                {
                    item.Add("DJ", hzfp_sqd_mx.DJ);
                }
                item.Add("HSJBZ", hzfp_sqd_mx.HSJBZ);
                item.Add("SPBH", hzfp_sqd_mx.SPBH);
                item.Add("FPHXZ", hzfp_sqd_mx.FPHXZ);
                item.Add("XTHASH", hzfp_sqd_mx.XTHASH);
                item.Add("FLBM", hzfp_sqd_mx.FLBM);
                item.Add("QYSPBM", hzfp_sqd_mx.QYSPBM);
                item.Add("SFXSYHZC", hzfp_sqd_mx.SFXSYHZC);
                item.Add("YHZCMC", hzfp_sqd_mx.YHZCMC);
                item.Add("LSLBS", hzfp_sqd_mx.LSLBS);
                list.Add(item);
            }
            if (this.baseDao.未确认DAO方法3("aisino.Fwkp.Hzfp.InsertSQDMX", list) <= 0)
            {
                return false;
            }
            return true;
        }

        public DataTable SelectList(string SQDH)
        {
            this.dict.Clear();
            this.dict.Add("SQDH", SQDH);
            return this.baseDao.querySQLDataTable("aisino.Fwkp.Hzfp.SelectSQDMXList", this.dict);
        }

        public bool Updata(Aisino.Fwkp.Hzfp.Model.HZFP_SQD_MX model)
        {
            this.dict.Clear();
            this.dict.Add("SQDH", model.SQDH);
            this.dict.Add("MXXH", model.MXXH);
            this.dict.Add("JE", model.JE);
            this.dict.Add("SLV", model.SLV);
            this.dict.Add("SE", model.SE);
            this.dict.Add("SPMC", model.SPMC);
            this.dict.Add("SPSM", model.SPSM);
            this.dict.Add("GGXH", model.GGXH);
            this.dict.Add("JLDW", model.JLDW);
            this.dict.Add("SL", model.SL);
            this.dict.Add("DJ", model.DJ);
            this.dict.Add("HSJBZ", model.HSJBZ);
            this.dict.Add("FPHXZ", model.FPHXZ);
            this.dict.Add("FLBM", model.FLBM);
            this.dict.Add("QYSPBM", model.QYSPBM);
            this.dict.Add("SFXSYHZC", model.SFXSYHZC);
            this.dict.Add("YHZCMC", model.YHZCMC);
            this.dict.Add("LSLBS", model.LSLBS);
            if (this.baseDao.未确认DAO方法2_疑似updateSQL("aisino.Fwkp.Hzfp.UpdateSQDMX", this.dict) <= 0)
            {
                return false;
            }
            return true;
        }
    }
}

