namespace Aisino.Fwkp.Xtsz.BLL
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Xtsz.Common;
    using Aisino.Fwkp.Xtsz.DAL;
    using Aisino.Fwkp.Xtsz.Model;
    using log4net;
    using System;

    public class ParaSetBLL
    {
        private ILog loger = LogUtil.GetLogger<ParaSetBLL>();
        private ParaSetDAL paraSetDAL = new ParaSetDAL();
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();

        public bool AddSysTaxInfo(CommFun.CorporationInfo corporationInfo, ref SysTaxInfoModel sysTaxInfoModel)
        {
            this.paraSetDAL.MakeModelByCorporation(corporationInfo, ref sysTaxInfoModel);
            return this.paraSetDAL.AddSysTaxInfo(sysTaxInfoModel);
        }

        public bool CheckCorpInfo()
        {
            try
            {
                if (this.taxCard.get_Address().Trim() == "")
                {
                    MessageManager.ShowMsgBox("TCD_9101_");
                    this.loger.Error("未设置企业税务信息");
                    return false;
                }
            }
            catch (Exception exception)
            {
                this.loger.Debug("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            return true;
        }

        public CommFun.CorporationInfo GetCorporationInfo()
        {
            CommFun.CorporationInfo info = new CommFun.CorporationInfo();
            try
            {
                info.m_strCorpCode = this.taxCard.get_CorpCode();
                info.m_strCorpName = this.taxCard.get_Corporation();
                info.m_strSignCode = this.taxCard.get_TaxCode();
                info.m_nMachineCode = this.taxCard.get_Machine();
                if (this.taxCard.get_TaxMode() == 2)
                {
                    TaxStateInfo info2 = this.taxCard.get_StateInfo();
                    info.m_lUpperLimit = info2.InvLimit;
                    info.m_nBranchCount = info2.MachineNumber;
                }
                else
                {
                    info.m_lUpperLimit = 0L;
                    info.m_nBranchCount = 0;
                }
                info.m_strAddress = this.taxCard.get_Address();
                info.m_strTelephone = this.taxCard.get_Telephone();
                info.m_strBankAccount = this.taxCard.get_BankAccount();
                info.m_strAgenter = this.taxCard.get_CorpAgent();
                info.m_bEasyLevy = this.taxCard.get_EasyLevy();
                info.m_strRegType = this.taxCard.get_RegType();
                info.m_strAccounter = "";
                info.m_dtReportTime = this.taxCard.get_RepDate();
                info.m_nSoftPanDiv = 0;
            }
            catch (Exception exception)
            {
                this.loger.Debug(exception.Message);
                throw exception;
            }
            return info;
        }

        public bool GetCorporationInfoFromDB(string strCorpCode, ref SysTaxInfoModel sysTaxInfoModel)
        {
            return this.paraSetDAL.GetCorporationInfoByCode(strCorpCode, ref sysTaxInfoModel);
        }

        public bool GetDZDZInfoFromDB(ref DZDZInfoModel dzdzInfoModel)
        {
            return this.paraSetDAL.GetDZDZInfoFromDB(ref dzdzInfoModel);
        }

        public bool UpdateDZDZInfo(DZDZInfoModel dzdzInfoModel)
        {
            return this.paraSetDAL.UpdateDZDZInfo(dzdzInfoModel);
        }

        public bool UpdateSysTaxInfo(SysTaxInfoModel sysTaxInfoModel)
        {
            return this.paraSetDAL.AddSysTaxInfo(sysTaxInfoModel);
        }

        public bool UpdateXTSWXX_QYBH(string qybh)
        {
            return this.paraSetDAL.UpdateXTSWXX_QYBH(qybh);
        }

        public bool WriteTaxCardInfo()
        {
            try
            {
                SysTaxInfoModel sysTaxInfoModel = new SysTaxInfoModel();
                if (this.GetCorporationInfoFromDB(this.taxCard.get_TaxCode(), ref sysTaxInfoModel))
                {
                    this.taxCard.set_BankAccount(sysTaxInfoModel.YHZH);
                    this.taxCard.set_Address(sysTaxInfoModel.YYDZ);
                    this.taxCard.set_Telephone(sysTaxInfoModel.DHHM);
                }
                else
                {
                    this.loger.Debug("更新内存TaxCard信息失败!");
                    return false;
                }
            }
            catch (Exception exception)
            {
                this.loger.Debug("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
            return true;
        }
    }
}

