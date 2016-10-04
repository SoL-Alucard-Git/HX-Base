namespace Aisino.Fwkp.Fpkj.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Fpkj.IDAL;
    using Aisino.Fwkp.Fpkj.Model;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal class DKFP : IDKFP
    {
        private int _currentPage = 1;
        private IBaseDAO baseDao;
        private Dictionary<string, object> dict = new Dictionary<string, object>();
        private ILog loger = LogUtil.GetLogger<Aisino.Fwkp.Fpkj.DAL.DKFP>();

        public DKFP()
        {
            try
            {
                this.baseDao = BaseDAOFactory.GetBaseDAOSQLite();
                this.dict = new Dictionary<string, object>();
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
        }

        public bool Add(Aisino.Fwkp.Fpkj.Model.DKFP model)
        {
            try
            {
                this.dict.Clear();
                this.dict.Add("RZLX", model.RZLX);
                this.dict.Add("FPLX", model.FPLX);
                this.dict.Add("FPDM", model.FPDM);
                this.dict.Add("FPHM", model.FPHM);
                this.dict.Add("KPRQ", model.KPRQ);
                this.dict.Add("XFSBH", model.XFSBH);
                this.dict.Add("JE", model.JE);
                this.dict.Add("SE", model.SE);
                this.dict.Add("RZRQ", model.RZRQ);
                this.dict.Add("JSPH", model.JSPH);
                this.dict.Add("KPJH", model.KPJH);
                this.dict.Add("XZSJ", model.XZSJ);
                if (this.baseDao.未确认DAO方法2_疑似updateSQL("aisino.fwkp.fpkj.AddDKFP", this.dict) <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                return false;
            }
        }

        public bool Delete(string FPDM, int FPHM)
        {
            try
            {
                this.dict.Clear();
                this.dict.Add("FPDM", FPDM);
                this.dict.Add("FPHM", FPHM);
                return (this.baseDao.未确认DAO方法2_疑似updateSQL("aisino.fwkp.fpkj.DeleteDKFP", this.dict) > 0);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                return false;
            }
        }

        public Aisino.Fwkp.Fpkj.Model.DKFP DictionaryToModel(out Aisino.Fwkp.Fpkj.Model.DKFP model, Dictionary<string, object> dit)
        {
            model = new Aisino.Fwkp.Fpkj.Model.DKFP();
            try
            {
                model.FPDM = Convert.ToString(this.dict["FPDM"]);
                int result = 0;
                int.TryParse(this.dict["FPHM"].ToString().Trim(), out result);
                model.FPHM = result;
                model.RZLX = Convert.ToString(this.dict["RZLX"]);
                string s = this.dict["KPRQ"].ToString().Trim();
                model.KPRQ = DateTime.Parse(s);
                model.XFSBH = Convert.ToString(this.dict["XFSBH"]);
                string str2 = this.dict["JE"].ToString().Trim();
                model.JE = double.Parse(str2);
                string str3 = this.dict["SE"].ToString().Trim();
                model.SE = double.Parse(str3);
                string str4 = this.dict["RZRQ"].ToString().Trim();
                model.RZRQ = DateTime.Parse(str4);
                model.FPLX = Convert.ToString(this.dict["FPLX"]);
                return model;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                return null;
            }
        }

        public void DKFP_RollBack()
        {
        }

        private Aisino.Fwkp.Fpkj.Model.DKFP GetDkfp(Dictionary<string, object> dict)
        {
            if (dict != null)
            {
                return new Aisino.Fwkp.Fpkj.Model.DKFP { FPDM = dict["FPDM"].ToString(), FPHM = int.Parse(dict["FPHM"].ToString()), FPLX = dict["FPLX"].ToString(), JE = double.Parse(dict["JE"].ToString()), SE = double.Parse(dict["SE"].ToString()), XZSJ = DateTime.Parse(dict["XZSJ"].ToString()), RZLX = dict["RZLX"].ToString(), RZRQ = DateTime.Parse(dict["RZRQ"].ToString()) };
            }
            this.loger.Info("读取抵扣发票信息错误");
            this.loger.Info("读取抵扣发票信息");
            return null;
        }

        public Aisino.Fwkp.Fpkj.Model.DKFP GetModel(string FPDM, int FPHM)
        {
            try
            {
                this.dict.Clear();
                this.dict.Add("FPDM", FPDM);
                this.dict.Add("FPHM", FPHM);
                ArrayList list = this.baseDao.querySQL("aisino.fwkp.fpkj.SelectDKFP", this.dict);
                Aisino.Fwkp.Fpkj.Model.DKFP model = new Aisino.Fwkp.Fpkj.Model.DKFP();
                if (list.Count > 0)
                {
                    this.dict.Clear();
                    this.dict = list[0] as Dictionary<string, object>;
                    this.DictionaryToModel(out model, this.dict);
                    list = null;
                    return model;
                }
                list = null;
                return null;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                return null;
            }
        }

        public AisinoDataSet SelectDkfplist(int page, int count, Dictionary<string, object> dictionary)
        {
            AisinoDataSet set = null;
            try
            {
                set = this.baseDao.querySQLDataSet("aisino.Fwkp.Fpkj.SelectDKFPList", dictionary, count, page);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            return set;
        }

        public AisinoDataSet SelectDkfplist_ChaXunTiaoJian(int page, int count, Dictionary<string, object> dictionary)
        {
            AisinoDataSet set = null;
            try
            {
                set = this.baseDao.querySQLDataSet("aisino.fwkp.fpkj.SelectDKFP_TiaoJian", dictionary, count, page);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            return set;
        }

        public List<Aisino.Fwkp.Fpkj.Model.DKFP> SelectDkpf_DKFPDaoChu(Dictionary<string, object> condition)
        {
            List<Aisino.Fwkp.Fpkj.Model.DKFP> list = new List<Aisino.Fwkp.Fpkj.Model.DKFP>();
            list.Clear();
            string str = "aisino.fwkp.fpkj.SelectDKFPDaoChu";
            ArrayList list2 = this.baseDao.querySQL(str, condition);
            if ((list2 != null) && (list2.Count > 0))
            {
                int num = 0;
                int count = list2.Count;
                for (num = 0; num < count; num++)
                {
                    Dictionary<string, object> dict = list2[num] as Dictionary<string, object>;
                    Aisino.Fwkp.Fpkj.Model.DKFP item = this.GetDkfp(dict);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            list2 = null;
            return list;
        }

        public AisinoDataSet SelectList(int page, int count)
        {
            AisinoDataSet set = null;
            try
            {
                set = this.baseDao.querySQLDataSet("aisino.Fwkp.Fpkj.SelectDKFPList", this.dict, count, page);
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
            }
            return set;
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
                string s = PropertyUtil.GetValue("Aisino.Fwkp.Dkfp.pagesize");
                if (((s == null) || (s.Length == 0)) || !int.TryParse(s, out num))
                {
                    num = 5;
                    PropertyUtil.SetValue("Aisino.Fwkp.Dkfp.pagesize", num.ToString());
                }
                return num;
            }
            set
            {
                PropertyUtil.SetValue("Aisino.Fwkp.Dkfp.pagesize", value.ToString());
            }
        }
    }
}

