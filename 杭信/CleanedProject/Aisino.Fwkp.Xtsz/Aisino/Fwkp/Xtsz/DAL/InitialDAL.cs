namespace Aisino.Fwkp.Xtsz.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Crypto;
    using log4net;
    using System;
    using System.Collections.Generic;

    public class InitialDAL
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private ILog loger = LogUtil.GetLogger<InitialDAL>();

        public bool CleanupData()
        {
            bool flag = true;
            try
            {
                flag &= this.DeleteDeficitInvoiceRequisition();
                flag &= this.DeleteDeficitInvoiceRequisitionDetail();
                flag &= this.DeleteSellCheck();
                flag &= this.DeleteSellCheckRestore();
                flag &= this.DeleteSellCheckDetail();
                flag &= this.DeleteSellCheckDetailRestore();
                flag &= this.DeleteOutInvoice();
                flag &= this.DeleteOutInvoiceDetail();
                flag &= this.DeleteBillOfSell();
            }
            catch (Exception exception)
            {
                this.loger.Error("清空数据库失败");
                ExceptionHandler.HandleError(exception);
            }
            return flag;
        }

        private bool DeleteBillOfSell()
        {
            return ((this.GetDataCount("aisino.Fwkp.Xtsz.XXFP_XHQDCount") == 0) || this.DeleteData("aisino.Fwkp.Xtsz.DeleteXXFP_XHQD"));
        }

        private bool DeleteData(string sqlID)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            try
            {
                if (this.baseDAO.updateSQL(sqlID, dictionary) >= 0)
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            return false;
        }

        private bool DeleteDeficitInvoiceRequisition()
        {
            return ((this.GetDataCount("aisino.Fwkp.Xtsz.HZFP_SQDCount") == 0) || this.DeleteData("aisino.Fwkp.Xtsz.DeleteHZFP_SQD"));
        }

        private bool DeleteDeficitInvoiceRequisitionDetail()
        {
            return ((this.GetDataCount("aisino.Fwkp.Xtsz.HZFP_SQD_MXCount") == 0) || this.DeleteData("aisino.Fwkp.Xtsz.DeleteHZFP_SQD_MX"));
        }

        private bool DeleteInputInvoice()
        {
            return ((this.GetDataCount("aisino.Fwkp.Xtsz.JXFPCount") == 0) || this.DeleteData("aisino.Fwkp.Xtsz.DeleteJXFP"));
        }

        private bool DeleteInvoiceCollaUse()
        {
            return ((this.GetDataCount("aisino.Fwkp.Xtsz.FPLYCYBBFBCount") == 0) || this.DeleteData("aisino.Fwkp.Xtsz.DeleteFPLYCYBBFB"));
        }

        private bool DeleteInvoiceSeries()
        {
            return ((this.GetDataCount("aisino.Fwkp.Xtsz.FPJCount") == 0) || this.DeleteData("aisino.Fwkp.Xtsz.DeleteFPJ"));
        }

        private bool DeleteInvoiceSeriesDetail()
        {
            return ((this.GetDataCount("aisino.Fwkp.Xtsz.FPJMXCount") == 0) || this.DeleteData("aisino.Fwkp.Xtsz.DeleteFPJMX"));
        }

        private bool DeleteLogInfo()
        {
            return ((this.GetDataCount("aisino.Fwkp.Xtsz.RZXXCount") == 0) || this.DeleteData("aisino.Fwkp.Xtsz.DeleteRZXX"));
        }

        private bool DeleteOutInvoice()
        {
            return ((this.GetDataCount("aisino.Fwkp.Xtsz.XXFPCount") == 0) || this.DeleteData("aisino.Fwkp.Xtsz.DeleteXXFP"));
        }

        private bool DeleteOutInvoiceDetail()
        {
            return ((this.GetDataCount("aisino.Fwkp.Xtsz.XXFP_MXCount") == 0) || this.DeleteData("aisino.Fwkp.Xtsz.DeleteXXFP_MX"));
        }

        private bool DeleteSellCheck()
        {
            return ((this.GetDataCount("aisino.Fwkp.Xtsz.XSDJCount") == 0) || this.DeleteData("aisino.Fwkp.Xtsz.DeleteXSDJ"));
        }

        private bool DeleteSellCheckDetail()
        {
            return ((this.GetDataCount("aisino.Fwkp.Xtsz.XSDJ_MXCount") == 0) || this.DeleteData("aisino.Fwkp.Xtsz.DeleteXSDJ_MX"));
        }

        private bool DeleteSellCheckDetailRestore()
        {
            return ((this.GetDataCount("aisino.Fwkp.Xtsz.XSDJ_MX_HYCount") == 0) || this.DeleteData("aisino.Fwkp.Xtsz.DeleteXSDJ_MX_HY"));
        }

        private bool DeleteSellCheckRestore()
        {
            return ((this.GetDataCount("aisino.Fwkp.Xtsz.XSDJ_HYCount") == 0) || this.DeleteData("aisino.Fwkp.Xtsz.DeleteXSDJ_HY"));
        }

        private bool DeleteSystemStatus()
        {
            return ((this.GetDataCount("aisino.Fwkp.Xtsz.XTZTXXCount") == 0) || this.DeleteData("aisino.Fwkp.Xtsz.DeleteXTZTXX"));
        }

        private int GetDataCount(string sqlID)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                return this.baseDAO.queryValueSQL<int>(sqlID, dictionary);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            return -1;
        }

        internal bool SetAdminAndPwd(string strAdminName, string strPwd)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("DM", "admin");
            dictionary.Add("ZSXM", strAdminName);
            dictionary.Add("MM", MD5_Crypt.GetHashStr(strPwd));
            try
            {
                if (1 == this.baseDAO.updateSQL("Aisino.Fwkp.Xtsz.SetAdminAndPwd", dictionary))
                {
                    return true;
                }
            }
            catch
            {
                throw;
            }
            this.loger.Error("初始化-设置主管名称密码异常");
            return false;
        }

        public bool UpdateManager(string strOldName, string strNewName)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("OldName", strOldName);
                dictionary.Add("NewName", strNewName);
                if (this.baseDAO.updateSQL("aisino.Fwkp.Xtsz.QX_YHXXUpdate", dictionary) > 0)
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            this.loger.Error("更新操作员信息失败");
            return false;
        }
    }
}

