namespace Aisino.Fwkp.Fpzpz.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Fwkp.Fpzpz.Common;
    using Aisino.Fwkp.Fpzpz.IDAL;
    using log4net;
    using System;
    using System.Collections.Generic;

    public class XZQYBM : IXZQYBM
    {
        private IBaseDAO baseDao;
        private ILog loger = LogUtil.GetLogger<XZQYBM>();

        public XZQYBM()
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

        public List<XZQYBMModal> SelectXZQYBM_BM(string strBM)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                string str = "aisino.fwkp.Fpzpz.SelectXZQYBM_BM";
                dictionary.Add("strBM", strBM);
                return Tool.ArrayListToListXZQYBMModal(this.baseDao.querySQL(str, dictionary), this.loger);
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

        public List<XZQYBMModal> SelectXZQYBM_ZH(string strZH)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                string str = "aisino.fwkp.Fpzpz.SelectXZQYBM_ZH";
                dictionary.Add("strZH", strZH);
                return Tool.ArrayListToListXZQYBMModal(this.baseDao.querySQL(str, dictionary), this.loger);
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

        public bool UpdateXZQYBM(string strSET, string strWHERE)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("strSET", strSET);
                dictionary.Add("strWHERE", strWHERE);
                string str = "aisino.fwkp.Fpzpz.UpdateXZQYBM";
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

