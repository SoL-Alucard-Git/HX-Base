namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class InvoiceReportDAL
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private ILog loger = LogUtil.GetLogger<InvoiceReportDAL>();

        public bool AddInvVolumeData(List<InvVolumeEntity> _InvVolumeEntityList)
        {
            try
            {
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                for (int i = 0; i < _InvVolumeEntityList.Count; i++)
                {
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    item.Add("JH", _InvVolumeEntityList[i].m_nVolumeCode);
                    if (_InvVolumeEntityList[i].m_nInvType == 0)
                    {
                        item.Add("FPZL", "s");
                    }
                    else
                    {
                        item.Add("FPZL", "c");
                    }
                    item.Add("LBDM", _InvVolumeEntityList[i].m_strInvCode);
                    item.Add("QSHM", _InvVolumeEntityList[i].m_strStartCode);
                    item.Add("FPZS", _InvVolumeEntityList[i].m_nInvCount);
                    item.Add("LGRQ", _InvVolumeEntityList[i].m_dtBuyDate);
                    item.Add("KPJH", _InvVolumeEntityList[i].m_nMachineCode);
                    item.Add("SYZS", _InvVolumeEntityList[i].m_nRemainCount);
                    item.Add("MSBZ", _InvVolumeEntityList[i].m_bTaxSign);
                    item.Add("DJBZ", _InvVolumeEntityList[i].m_bRegisterSign);
                    item.Add("YWBZ", _InvVolumeEntityList[i].m_bExhaustSign);
                    item.Add("SYBZ", _InvVolumeEntityList[i].m_bUseSign);
                    list.Add(item);
                }
                if (this.baseDAO.updateSQL("aisino.Fwkp.Bsgl.InsertFPJ", list) > 0)
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                this.loger.Debug("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return false;
        }

        public bool AddMonthReportTableData(List<InvVolume> _InvVolumeList, int _nMonth)
        {
            try
            {
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                for (int i = 0; i < _InvVolumeList.Count; i++)
                {
                    Dictionary<string, object> item = new Dictionary<string, object>();
                    item.Add("SSYF", _nMonth);
                    item.Add("XH", i);
                    if (_InvVolumeList[i].InvType == null)
                    {
                        item.Add("FPZL", "s");
                    }
                    else
                    {
                        item.Add("FPZL", "c");
                    }
                    item.Add("LBDM", _InvVolumeList[i].TypeCode);
                    item.Add("QCKCFS", _InvVolumeList[i].PrdEarlyStockNum);
                    item.Add("QCKCHM", _InvVolumeList[i].PrdEarlyStockNO);
                    item.Add("BQKCFS", _InvVolumeList[i].PrdThisBuyNum);
                    item.Add("BQKCHM", _InvVolumeList[i].PrdThisBuyNO);
                    item.Add("BQKJFS", _InvVolumeList[i].PrdThisIssueNum);
                    item.Add("BQKJHM", _InvVolumeList[i].PrdThisIssueNO);
                    item.Add("BQZFFS", _InvVolumeList[i].WasteNum);
                    item.Add("BQZFHM", _InvVolumeList[i].WasteNO);
                    item.Add("JHWKFS", _InvVolumeList[i].MistakeNum);
                    item.Add("JHWKHM", _InvVolumeList[i].MistakeNO);
                    item.Add("QMKCFS", _InvVolumeList[i].PrdEndStockNum);
                    item.Add("QMKCHM", _InvVolumeList[i].PrdEndStockNO);
                    list.Add(item);
                }
                if (this.baseDAO.updateSQL("aisino.Fwkp.Bsgl.InsertFPLYCYBB", list) > 0)
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                this.loger.Debug("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return false;
        }

        public bool DeleteInvVolumeData()
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bsgl.FPJCount", dictionary) == 0)
                {
                    return true;
                }
                if (this.baseDAO.updateSQL("aisino.Fwkp.Bsgl.DeleteFPJ", dictionary) >= 0)
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                this.loger.Debug("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return false;
        }

        public bool DeleteMonthReportTable()
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Bsgl.FPLYCYBBCount", dictionary) == 0)
                {
                    return true;
                }
                if (this.baseDAO.updateSQL("aisino.Fwkp.Bsgl.DeleteFPLYCYBB", dictionary) >= 0)
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                this.loger.Debug("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return false;
        }

        public ArrayList GetDetailInfo(int nMonth)
        {
            ArrayList list;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("SSYF", nMonth.ToString());
            try
            {
                list = this.baseDAO.querySQL("aisino.Fwkp.Bsgl.GetDetailFPLYCYBB", dictionary);
            }
            catch (Exception exception)
            {
                this.loger.Debug("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return list;
        }

        public ArrayList GetTaxStatData(DateTime dtStart, DateTime dtEnd)
        {
            ArrayList list;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("STARTDATE", dtStart);
            dictionary.Add("ENDDATE", dtEnd);
            try
            {
                list = this.baseDAO.querySQL("aisino.Fwkp.Bsgl.GetStatXXFP", dictionary);
            }
            catch (Exception exception)
            {
                this.loger.Debug("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
                return null;
            }
            return list;
        }
    }
}

