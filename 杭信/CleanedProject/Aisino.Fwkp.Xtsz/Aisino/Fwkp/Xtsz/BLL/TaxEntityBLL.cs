namespace Aisino.Fwkp.Xtsz.BLL
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Framework.Startup.Login;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Xtsz.DAL;
    using log4net;
    using System;

    public class TaxEntityBLL
    {
        private ILog loger = LogUtil.GetLogger<TaxEntityBLL>();
        private InitialDAL m_InitialDAL = new InitialDAL();
        private int nMonth;
        private int nYear;
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();

        public TaxEntityBLL()
        {
            this.nYear = this.taxCard.GetCardClock().Year;
            this.nMonth = this.taxCard.GetCardClock().Month;
        }

        public bool CleanTable()
        {
            try
            {
                return this.m_InitialDAL.CleanupData();
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            return false;
        }

        public bool DataBack()
        {
            object[] objArray = new object[] { true };
            if (ServiceFactory.InvokePubService("Aisino.Fwkp.Sjbf", objArray) == null)
            {
                return false;
            }
            return true;
        }

        public bool GetDateFromTaxcadInterface()
        {
            try
            {
                this.nYear = this.taxCard.GetCardClock().Year;
                this.nMonth = this.taxCard.GetCardClock().Month;
                if (((this.nYear != 0) && (this.nMonth >= 1)) && (this.nMonth <= 12))
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            this.loger.Debug("从金税卡获取日期失败");
            return false;
        }

        public bool GetLoginName(ref string strLoginName)
        {
            try
            {
                strLoginName = UserInfo.get_Yhmc();
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
                strLoginName = "";
                return false;
            }
            return true;
        }

        public bool InvoiceRepair(int _nMonth)
        {
            try
            {
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FaPiaoXiuFu", new object[] { _nMonth });
                if (objArray != null)
                {
                    return ((objArray.Length == 1) && Convert.ToBoolean(objArray[0]));
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            return false;
        }

        public bool IsAdmin()
        {
            try
            {
                return UserInfo.get_IsAdmin();
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
                return false;
            }
        }

        public bool IsMatch(string strName, string strPasswd)
        {
            try
            {
                string hashStr = MD5_Crypt.GetHashStr(strPasswd);
                object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Xtgl.UserPwdService", new object[] { strName, hashStr });
                if (objArray != null)
                {
                    return ((objArray.Length == 1) && Convert.ToBoolean(objArray[0]));
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
                return false;
            }
            return false;
        }

        public bool SetAdminAndPwd(string strAdminName, string strPwd)
        {
            try
            {
                return this.m_InitialDAL.SetAdminAndPwd(strAdminName, strPwd);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            return false;
        }

        public bool SetManagerName(string _strName)
        {
            try
            {
                UserInfo.set_Yhmc(_strName);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
                return false;
            }
            return true;
        }

        public bool UpdateManager(string _strOldName, string _strNewName)
        {
            try
            {
                return this.m_InitialDAL.UpdateManager(_strOldName, _strNewName);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            return false;
        }
    }
}

