namespace Aisino.Fwkp.Fpzpz.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Fpzpz.Common;
    using Aisino.Fwkp.Fpzpz.IDAL;
    using Aisino.Fwkp.Fpzpz.Model;
    using log4net;
    using System;
    using System.Collections.Generic;

    public class PZFLB : IPZFLB
    {
        private IBaseDAO baseDao;
        private ILog loger = LogUtil.GetLogger<PZFLB>();

        public PZFLB()
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

        public bool AddInfoToPZTempTbl(TPZEntry_InfoModal pz_InfoModal)
        {
            try
            {
                if (pz_InfoModal != null)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary.Add("iZH", pz_InfoModal.PZEntry_Group);
                    dictionary.Add("strDJXX", pz_InfoModal.PZEntry_KindcodeNo);
                    dictionary.Add("strKHBH", pz_InfoModal.PZEntry_CusrID);
                    string str = Convert.ToString(pz_InfoModal.PZEntry_JE);
                    if ((str.Length - str.IndexOf(".")) > 7)
                    {
                        object[] objArray = new object[] { str, str.IndexOf(".") + 3 };
                        object[] objArray2 = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FPPrecisionShareMethod", objArray);
                        if ((objArray2 != null) && (objArray2.Length > 0))
                        {
                            str = objArray2[0].ToString();
                        }
                    }
                    dictionary.Add("decJE", str);
                    dictionary.Add("strKM", pz_InfoModal.PZEntry_SkKm);
                    dictionary.Add("iJDBZ", pz_InfoModal.PZEntry_JDSFlag);
                    dictionary.Add("strSPBH", pz_InfoModal.PZEntry_GooDsID);
                    dictionary.Add("dSL", pz_InfoModal.PZEntry_SPNum);
                    dictionary.Add("dDJ", pz_InfoModal.PZEntry_SPPrice);
                    dictionary.Add("dateKPRQ", pz_InfoModal.PZEntry_Date);
                    dictionary.Add("strJLDW", pz_InfoModal.PZEntry_Jldw);
                    dictionary.Add("iHSBZ", pz_InfoModal.PZEntry_NumCheck);
                    string str2 = "aisino.fwkp.Fpzpz.InsertTempTable_PZFLB";
                    int num = this.baseDao.updateSQL(str2, dictionary);
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

        public List<TPZEntry_InfoModal> SelectPZFLB_ZH()
        {
            try
            {
                Dictionary<string, object> dictionary = null;
                string str = "aisino.fwkp.Fpzpz.SelectPZFLB_ZH";
                return Tool.ArrayListToListPZFLB(this.baseDao.querySQL(str, dictionary), this.loger);
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

        public List<TPZEntry_InfoModal> SelectPZFLB_ZH_DJXX(string strZH)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                int result = 0;
                int.TryParse(strZH, out result);
                dictionary.Add("iZH", result);
                string str = "aisino.fwkp.Fpzpz.SelectPZFLB_ZH_DJXX";
                return Tool.ArrayListToListPZFLB(this.baseDao.querySQL(str, dictionary), this.loger);
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

