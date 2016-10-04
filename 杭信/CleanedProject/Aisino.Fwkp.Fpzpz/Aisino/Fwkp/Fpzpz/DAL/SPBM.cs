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

    public class SPBM : ISPBM
    {
        private IBaseDAO baseDao;
        private ILog loger = LogUtil.GetLogger<SPBM>();

        public SPBM()
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

        public bool AddInfoToGoodsKmTempTbl(SPBMModal spbmModal)
        {
            try
            {
                if (spbmModal != null)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("strSPBH", spbmModal.BM);
                    dictionary.Add("strXSSRKM", spbmModal.XSSRKM);
                    dictionary.Add("strYJZZSKM", spbmModal.YJZZSKM);
                    dictionary.Add("strXSTHKM", spbmModal.XSTHKM);
                    dictionary.Add("strSJBM", spbmModal.SJBM);
                    string str = "aisino.fwkp.Fpzpz.InsertTempTable_SPKMB";
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

        public List<SPBMModal> SelectSPBM_BM(string strBM)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                string str = "aisino.fwkp.Fpzpz.SelectSPBM_BM";
                dictionary.Add("strBM", strBM);
                return Tool.ArrayListToListSPBMModal(this.baseDao.querySQL(str, dictionary), this.loger);
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

        public List<SPBMModal> SelectSPBM_CH()
        {
            try
            {
                string str = "aisino.fwkp.Fpzpz.SelectSPBM_CH";
                return Tool.ArrayListToListSPBMModal(this.baseDao.querySQL(str, null), this.loger);
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

        public List<SPBMModal> SelectSPBM_CHFL()
        {
            try
            {
                string str = "aisino.fwkp.Fpzpz.SelectSPBM_CHFL";
                return Tool.ArrayListToListSPBMModal(this.baseDao.querySQL(str, null), this.loger);
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

        public List<SPBMModal> SelectSPBM_MC_BM(string strMc, string strBm)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("strMC", strMc);
                dictionary.Add("strBM", strBm);
                string str = "aisino.fwkp.Fpzpz.SelectSPBM_MC_BM";
                return Tool.ArrayListToListSPBMModal(this.baseDao.querySQL(str, dictionary), this.loger);
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

        public List<SPBMModal> SelectSPBM_SPMC(string strSPMC)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("strSPMC", strSPMC);
                string str = "aisino.fwkp.Fpzpz.SelectSPBM_SPMC";
                return Tool.ArrayListToListSPKMBModal(this.baseDao.querySQL(str, dictionary), this.loger);
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

        public List<SPBMModal> SelectSPKMB()
        {
            try
            {
                Dictionary<string, object> dictionary = null;
                string str = "aisino.fwkp.Fpzpz.SelectSPKMB";
                return Tool.ArrayListToListSPKMBModal(this.baseDao.querySQL(str, dictionary), this.loger);
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

        public List<SPBMModal> SelectSPKMB_SPBH(string strSPBH)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("strSPBH", strSPBH);
                string str = "aisino.fwkp.Fpzpz.SelectSPKMB_SPBH";
                return Tool.ArrayListToListSPKMBModal(this.baseDao.querySQL(str, dictionary), this.loger);
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

        public bool UpdateSPBM_XSSRKM(string strBm, string strXssrkm)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("strXSSRKM", strXssrkm);
                dictionary.Add("strBM", strBm);
                string str = "aisino.fwkp.Fpzpz.UpdateSPBM_XSSRKM";
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

        public bool UpdateSPBM_XSTHKM(string strBm, string strXsthkm)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("strXSTHKM", strXsthkm);
                dictionary.Add("strBM", strBm);
                string str = "aisino.fwkp.Fpzpz.UpdateSPBM_XSTHKM";
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

        public bool UpdateSPBM_YJZZSKM(string strBm, string strYjzzskm)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("strYJZZSKM", strYjzzskm);
                dictionary.Add("strBM", strBm);
                string str = "aisino.fwkp.Fpzpz.UpdateSPBM_YJZZSKM";
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

        public bool UpdateSPKMB_XSSRKM(string strSpbh, string strXssrkm)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("strXSSRKM", strXssrkm);
                dictionary.Add("strSPBH", strSpbh);
                string str = "aisino.fwkp.Fpzpz.UpdateSPKMB_XSSRKM";
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

        public bool UpdateSPKMB_XSTHKM(string strSpbh, string strXsthkm)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("strXSTHKM", strXsthkm);
                dictionary.Add("strSPBH", strSpbh);
                string str = "aisino.fwkp.Fpzpz.UpdateSPKMB_XSTHKM";
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

        public bool UpdateSPKMB_YJZZSKM(string strSpbh, string strYjzzskm)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("strYJZZSKM", strYjzzskm);
                dictionary.Add("strSPBH", strSpbh);
                string str = "aisino.fwkp.Fpzpz.UpdateSPKMB_YJZZSKM";
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

