namespace Aisino.Fwkp.Fpzpz.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Fwkp.Fpzpz.Common;
    using Aisino.Fwkp.Fpzpz.IDAL;
    using Aisino.Fwkp.Fpzpz.Model;
    using log4net;
    using System;
    using System.Collections.Generic;

    public class KHBM : IKHBM
    {
        private IBaseDAO baseDao;
        private ILog loger = LogUtil.GetLogger<KHBM>();

        public KHBM()
        {
            try
            {
                this.baseDao = BaseDAOFactory.GetBaseDAOSQLite();
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
        }

        public bool AddInfoToCusrKmTempTbl(KHBMModal khbmModal)
        {
            try
            {
                if (khbmModal != null)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("strKHBH", khbmModal.BM);
                    dictionary.Add("strYSKM", khbmModal.YSKM);
                    dictionary.Add("strSJBM", khbmModal.SJBM);
                    string str = "aisino.fwkp.Fpzpz.InsertTempTable_KHKMB";
                    int num = this.baseDao.updateSQL(str, dictionary);
                    if (0 < num)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return false;
        }

        public List<KHBMModal> SelectKHBM_BM(string strBM)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                string str = "aisino.fwkp.Fpzpz.SelectKHBM_BM";
                dictionary.Add("strBM", strBM);
                return Tool.ArrayListToListKHBMModal(this.baseDao.querySQL(str, dictionary), this.loger);
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return null;
        }

        public List<KHBMModal> SelectKHBM_DQFL()
        {
            try
            {
                string str = "aisino.fwkp.Fpzpz.SelectKHBM_DQFL";
                return Tool.ArrayListToListKHBMModal(this.baseDao.querySQL(str, null), this.loger);
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return null;
        }

        public List<KHBMModal> SelectKHBM_KH()
        {
            try
            {
                string str = "aisino.fwkp.Fpzpz.SelectKHBM_KH";
                return Tool.ArrayListToListKHBMModal(this.baseDao.querySQL(str, null), this.loger);
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return null;
        }

        public List<KHBMModal> SelectKHBM_KHFL()
        {
            try
            {
                string str = "aisino.fwkp.Fpzpz.SelectKHBM_KHFL";
                return Tool.ArrayListToListKHBMModal(this.baseDao.querySQL(str, null), this.loger);
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return null;
        }

        public List<KHBMModal> SelectKHBM_MC_BM(string strMc, string strBm)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("strMC", strMc);
                dictionary.Add("strBM", strBm);
                string str = "aisino.fwkp.Fpzpz.SelectKHBM_MC_BM";
                return Tool.ArrayListToListKHBMModal(this.baseDao.querySQL(str, dictionary), this.loger);
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return null;
        }

        public List<KHBMModal> SelectKHBM_WJ(int iWJ)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                string str = "aisino.fwkp.Fpzpz.SelectKHBM_WJ";
                dictionary.Add("iWJ", iWJ);
                return Tool.ArrayListToListKHBMModal(this.baseDao.querySQL(str, dictionary), this.loger);
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return null;
        }

        public List<KHBMModal> SelectKHKMB()
        {
            try
            {
                Dictionary<string, object> dictionary = null;
                string str = "aisino.fwkp.Fpzpz.SelectKHKMB";
                return Tool.ArrayListToListKHKMBModal(this.baseDao.querySQL(str, dictionary), this.loger);
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return null;
        }

        public List<KHBMModal> SelectKHKMB_KHBH(string strKHBH)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("strKHBH", strKHBH);
                string str = "aisino.fwkp.Fpzpz.SelectKHKMB_KHBH";
                return Tool.ArrayListToListKHKMBModal(this.baseDao.querySQL(str, dictionary), this.loger);
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return null;
        }

        public bool UpdateKHBM_DQBM(string strBM, string strDQBM)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("strBM", strBM);
                dictionary.Add("strDQBM", strDQBM);
                string str = "aisino.fwkp.Fpzpz.UpdateKHBM_DQBM";
                int num = this.baseDao.updateSQL(str, dictionary);
                return (0 < num);
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return false;
        }

        public bool UpdateKHBM_Dqkm(string strDqbm, string strDqmc, string strDqkm)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("strDQBM", strDqbm);
                dictionary.Add("strDQMC", strDqmc);
                dictionary.Add("strDQKM", strDqkm);
                string str = "aisino.fwkp.Fpzpz.UpdateKHBM_Dqkm";
                int num = this.baseDao.updateSQL(str, dictionary);
                return (0 < num);
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return false;
        }

        public bool UpdateKHBM_DQMC(string strBM, string strDQMC)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("strBM", strBM);
                dictionary.Add("strDQMC", strDQMC);
                string str = "aisino.fwkp.Fpzpz.UpdateKHBM_DQMC";
                int num = this.baseDao.updateSQL(str, dictionary);
                return (0 < num);
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return false;
        }

        public bool UpdateKHBM_Yskm(string strBm, string strYskm)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("strYSKM", strYskm);
                dictionary.Add("strBM", strBm);
                string str = "aisino.fwkp.Fpzpz.UpdateKHBM_Yskm";
                int num = this.baseDao.updateSQL(str, dictionary);
                return (0 < num);
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return false;
        }

        public bool UpdateKHKMB_Yskm(string strKhbm, string strYskm)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("strYSKM", strYskm);
                dictionary.Add("strKHBH", strKhbm);
                string str = "aisino.fwkp.Fpzpz.UpdateKHKMB_Yskm";
                int num = this.baseDao.updateSQL(str, dictionary);
                return (0 < num);
            }
            catch (BaseException exception)
            {
                this.loger.Error(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            catch (Exception exception2)
            {
                this.loger.Error(exception2.Message);
                ExceptionHandler.HandleError(exception2);
            }
            return false;
        }
    }
}

