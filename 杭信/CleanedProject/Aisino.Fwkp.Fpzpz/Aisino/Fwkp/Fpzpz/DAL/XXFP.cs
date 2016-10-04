namespace Aisino.Fwkp.Fpzpz.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Fwkp.Fpzpz.Common;
    using Aisino.Fwkp.Fpzpz.IDAL;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class XXFP : IXXFP
    {
        private IBaseDAO baseDao = BaseDAOFactory.GetBaseDAOSQLite();
        private Dictionary<string, object> dict = new Dictionary<string, object>();
        private ILog loger = LogUtil.GetLogger<XXFP>();

        public bool bIfMakedPZ(string fpdm, string fphm, string fpzl)
        {
            bool flag = false;
            if ((!string.IsNullOrEmpty(fpdm) && !string.IsNullOrEmpty(fphm)) && !string.IsNullOrEmpty(fpzl))
            {
                try
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("FPDM", fpdm);
                    int result = 0;
                    int.TryParse(fphm, out result);
                    dictionary.Add("FPHM", result);
                    string str = "";
                    if (fpzl.Equals("普通发票") || fpzl.Equals("PTFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str = "c";
                    }
                    else if (fpzl.Equals("专用发票") || fpzl.Equals("ZYFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str = "s";
                    }
                    else if (fpzl.Equals("货运发票") || fpzl.Equals("HYFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str = "f";
                    }
                    else if (fpzl.Equals("机动车发票") || fpzl.Equals("JDCFP", StringComparison.CurrentCultureIgnoreCase))
                    {
                        str = "j";
                    }
                    dictionary.Add("FPZL", str);
                    if (string.IsNullOrEmpty(this.baseDao.queryValueSQL<string>("aisino.fwkp.Fpzpz.SelectPZYWH", dictionary)))
                    {
                        return false;
                    }
                    flag = true;
                }
                catch (Exception exception)
                {
                    this.loger.Error(exception.Message);
                }
            }
            return flag;
        }

        public bool CreateTempTable()
        {
            bool flag = false;
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Clear();
                dictionary.Add("strTableName", "DQKMB");
                if (this.baseDao.queryValueSQL<int>("aisino.fwkp.Fpzpz.ExistTempTable", dictionary) > 0)
                {
                    this.baseDao.updateSQL("aisino.fwkp.Fpzpz.EmptyTempTable_DQKMB", dictionary);
                }
                else
                {
                    this.baseDao.updateSQL("aisino.fwkp.Fpzpz.CreateTempTable_DQKMB", dictionary);
                }
                dictionary.Clear();
                dictionary.Add("strTableName", "KHKMB");
                if (this.baseDao.queryValueSQL<int>("aisino.fwkp.Fpzpz.ExistTempTable", dictionary) > 0)
                {
                    this.baseDao.updateSQL("aisino.fwkp.Fpzpz.EmptyTempTable_KHKMB", dictionary);
                }
                else
                {
                    this.baseDao.updateSQL("aisino.fwkp.Fpzpz.CreateTempTable_KHKMB", dictionary);
                }
                dictionary.Clear();
                dictionary.Add("strTableName", "PZFLB");
                if (this.baseDao.queryValueSQL<int>("aisino.fwkp.Fpzpz.ExistTempTable", dictionary) > 0)
                {
                    this.baseDao.updateSQL("aisino.fwkp.Fpzpz.EmptyTempTable_PZFLB", dictionary);
                }
                else
                {
                    this.baseDao.updateSQL("aisino.fwkp.Fpzpz.CreateTempTable_PZFLB", dictionary);
                }
                dictionary.Clear();
                dictionary.Add("strTableName", "SPKMB");
                if (this.baseDao.queryValueSQL<int>("aisino.fwkp.Fpzpz.ExistTempTable", dictionary) > 0)
                {
                    this.baseDao.updateSQL("aisino.fwkp.Fpzpz.EmptyTempTable_SPKMB", dictionary);
                }
                else
                {
                    this.baseDao.updateSQL("aisino.fwkp.Fpzpz.CreateTempTable_SPKMB", dictionary);
                }
                flag = true;
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
            return flag;
        }

        public bool DropTempTable()
        {
            return true;
        }

        public bool EmptyTempTable()
        {
            try
            {
                int length = DingYiZhiFuChuan.strTableNameTemp.Length;
                for (int i = 0; i < length; i++)
                {
                    string strTableName = DingYiZhiFuChuan.strTableNameTemp[i];
                    if (this.ExistTempTable(strTableName))
                    {
                        Dictionary<string, object> dictionary = null;
                        string str2 = DingYiZhiFuChuan.strSQLID[2];
                        str2 = str2 + "_" + strTableName;
                        this.baseDao.updateSQL(str2, dictionary);
                    }
                }
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
            return true;
        }

        public bool ExistTempTable(string strTableName)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("strTableName", strTableName);
                string str = DingYiZhiFuChuan.strSQLID[3];
                ArrayList list = this.baseDao.querySQL(str, dictionary);
                if (list.Count <= 0)
                {
                    return false;
                }
                Dictionary<string, object> dictionary2 = list[0] as Dictionary<string, object>;
                string s = dictionary2["CNT"].ToString();
                int result = 0;
                int.TryParse(s, out result);
                if (result <= 0)
                {
                    return false;
                }
                return true;
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
            return true;
        }

        public List<Fpxx> SelectPagePZXXFP(Dictionary<string, object> dict)
        {
            try
            {
                string str = "aisino.fwkp.Fpzpz.SelectFPXX_PingZheng_TiaoJian";
                return Tool.ArrayListToListFpxx(this.baseDao.querySQL(str, dict), this.loger);
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

        public List<Fpxx> SelectPageXXFP(Dictionary<string, object> dict)
        {
            try
            {
                string str = "aisino.fwkp.Fpzpz.SelectFPXX_TiaoJian";
                return Tool.ArrayListToListFpxx(this.baseDao.querySQL(str, dict), this.loger);
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
    }
}

