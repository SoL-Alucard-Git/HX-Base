namespace Aisino.Fwkp.Xtsz.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using Aisino.Fwkp.Xtsz.Common;
    using Aisino.Fwkp.Xtsz.Model;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;

    public class ParaSetDAL
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();
        private ILog loger = LogUtil.GetLogger<ParaSetDAL>();
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();

        public bool AddSysTaxInfo(SysTaxInfoModel TaxInfoModel)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                if ((this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Xtsz.XTSWXXIsExist", dictionary) > 0) && (this.baseDAO.updateSQL("aisino.Fwkp.Xtsz.DeleteXTSWXX", dictionary) < 0))
                {
                    return false;
                }
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                dictionary2.Add("QYBH", TaxInfoModel.QYBH);
                dictionary2.Add("QYMC", TaxInfoModel.QYMC);
                dictionary2.Add("ZCLX", TaxInfoModel.ZCLX);
                dictionary2.Add("KPXE", TaxInfoModel.KPXE);
                dictionary2.Add("BSRQ", TaxInfoModel.BSRQ);
                dictionary2.Add("BSRPHF", TaxInfoModel.BSRPHF);
                dictionary2.Add("YHZH", TaxInfoModel.YHZH);
                dictionary2.Add("FRDB", TaxInfoModel.FRDB);
                dictionary2.Add("YYDZ", TaxInfoModel.YYDZ);
                dictionary2.Add("DHHM", TaxInfoModel.DHHM);
                dictionary2.Add("JYZS", TaxInfoModel.JYZS);
                dictionary2.Add("KJZG", TaxInfoModel.KJZG);
                if (this.baseDAO.updateSQL("aisino.Fwkp.Xtsz.AddXTSWXX", dictionary2) > 0)
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                this.loger.Error("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            this.loger.Error("AddSysTaxInfo failed");
            return false;
        }

        private string GetAQJRDZ()
        {
            string path = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), "Bin", "AQJRDZ.txt");
            if (!File.Exists(path))
            {
                return "";
            }
            try
            {
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream, ToolUtil.GetEncoding());
                reader.BaseStream.Seek(0L, SeekOrigin.Begin);
                string str2 = reader.ReadLine();
                reader.Close();
                stream.Close();
                return str2;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public bool GetCorporationInfoByCode(string strCorpCode, ref SysTaxInfoModel TaxAdminModel)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("QYBH", strCorpCode);
                DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Xtsz.SelectXTSWXX", dictionary);
                if (table.Rows.Count <= 0)
                {
                    return false;
                }
                TaxAdminModel.QYMC = table.Rows[0]["QYMC"].ToString();
                TaxAdminModel.ZCLX = table.Rows[0]["ZCLX"].ToString();
                TaxAdminModel.KPXE = Convert.ToInt32(table.Rows[0]["KPXE"]);
                TaxAdminModel.BSRQ = Convert.ToDateTime(table.Rows[0]["BSRQ"]);
                TaxAdminModel.BSRPHF = Convert.ToInt32(table.Rows[0]["BSRPHF"]);
                TaxAdminModel.YHZH = table.Rows[0]["YHZH"].ToString();
                TaxAdminModel.FRDB = table.Rows[0]["FRDB"].ToString();
                TaxAdminModel.YYDZ = table.Rows[0]["YYDZ"].ToString();
                TaxAdminModel.DHHM = table.Rows[0]["DHHM"].ToString();
                TaxAdminModel.JYZS = Convert.ToBoolean(table.Rows[0]["JYZS"]);
                TaxAdminModel.KJZG = table.Rows[0]["KJZG"].ToString();
            }
            catch (Exception exception)
            {
                this.loger.Error("get info failed");
                ExceptionHandler.HandleError(exception);
                return false;
            }
            return true;
        }

        public bool GetDZDZInfoFromDB(ref DZDZInfoModel dzdzInfoModel)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                DataTable table = this.baseDAO.querySQLDataTable("aisino.Fwkp.Xtsz.SelectDZDZXX", dictionary);
                if (table.Rows.Count <= 0)
                {
                    return false;
                }
                dzdzInfoModel.AcceptWebServer = table.Rows[0]["ACCEPT_WEB_SERVER"].ToString();
                dzdzInfoModel.UploadNowFlag = Convert.ToBoolean(table.Rows[0]["UPLOADNOW"]);
                dzdzInfoModel.IntervalFlag = Convert.ToBoolean(table.Rows[0]["INTERVALFLAG"]);
                dzdzInfoModel.IntervalTime = Convert.ToInt32(table.Rows[0]["INTERVALTIME"]);
                dzdzInfoModel.AccumulateFlag = Convert.ToBoolean(table.Rows[0]["ACCUMULATEFLAG"]);
                dzdzInfoModel.AccumulateNum = Convert.ToInt32(table.Rows[0]["ACCUMULATENUM"]);
                dzdzInfoModel.DataSize = Convert.ToInt32(table.Rows[0]["DATASIZE"]);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
                this.loger.Error("GetDZDZInfoFromDB failed");
                return false;
            }
            return true;
        }

        public void MakeModelByCorporation(CommFun.CorporationInfo corporationInfo, ref SysTaxInfoModel TaxInfoModel)
        {
            try
            {
                TaxInfoModel.QYBH = corporationInfo.m_strSignCode;
                TaxInfoModel.QYMC = corporationInfo.m_strCorpName;
                TaxInfoModel.ZCLX = corporationInfo.m_strRegType;
                TaxInfoModel.KPXE = Convert.ToInt32(corporationInfo.m_lUpperLimit);
                TaxInfoModel.BSRQ = corporationInfo.m_dtReportTime;
                TaxInfoModel.BSRPHF = corporationInfo.m_nSoftPanDiv;
                TaxInfoModel.YHZH = corporationInfo.m_strBankAccount;
                TaxInfoModel.FRDB = corporationInfo.m_strAgenter;
                TaxInfoModel.YYDZ = corporationInfo.m_strAddress;
                TaxInfoModel.DHHM = corporationInfo.m_strTelephone;
                TaxInfoModel.JYZS = corporationInfo.m_bEasyLevy;
                TaxInfoModel.KJZG = corporationInfo.m_strAccounter;
            }
            catch (Exception exception)
            {
                this.loger.Error("init Model failed:" + exception.Message);
                ExceptionHandler.HandleError(exception);
            }
        }

        private bool SetAQJRDZ(string addrStr)
        {
            string path = Path.Combine(PropertyUtil.GetValue("MAIN_PATH"), "Bin", "AQJRDZ.txt");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            try
            {
                FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(stream, ToolUtil.GetEncoding());
                writer.BaseStream.Seek(0L, SeekOrigin.Begin);
                writer.WriteLine(addrStr);
                writer.Close();
                stream.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateDZDZInfo(DZDZInfoModel dzdzInfoModel)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Xtsz.XTDZDZXXIsExist", dictionary) <= 0)
                {
                    return false;
                }
                dictionary.Add("ACCEPT_WEB_SERVER", dzdzInfoModel.AcceptWebServer);
                dictionary.Add("UPLOADNOW", dzdzInfoModel.UploadNowFlag);
                dictionary.Add("INTERVALFLAG", dzdzInfoModel.IntervalFlag);
                dictionary.Add("INTERVALTIME", dzdzInfoModel.IntervalTime);
                dictionary.Add("ACCUMULATEFLAG", dzdzInfoModel.AccumulateFlag);
                dictionary.Add("ACCUMULATENUM", dzdzInfoModel.AccumulateNum);
                dictionary.Add("DATASIZE", dzdzInfoModel.DataSize);
                if (this.baseDAO.updateSQL("aisino.Fwkp.Xtsz.UpdateXTDZDZXX", dictionary) > 0)
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
                this.loger.Error("UpdateDZDZInfo failed!");
                return false;
            }
            return false;
        }

        public bool UpdateSysTaxInfo(string strKey, SysTaxInfoModel TaxInfoModel)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                if (this.baseDAO.queryValueSQL<int>("aisino.Fwkp.Xtsz.XTSWXXIsExist", dictionary) <= 0)
                {
                    return false;
                }
                dictionary = new Dictionary<string, object>();
                dictionary.Add("QYBH", TaxInfoModel.QYBH);
                dictionary.Add("YHZH", TaxInfoModel.YHZH);
                dictionary.Add("YYDZ", TaxInfoModel.YYDZ);
                dictionary.Add("DHHM", TaxInfoModel.DHHM);
                if (this.baseDAO.updateSQL("aisino.Fwkp.Xtsz.UpdateXTSWXX", dictionary) > 0)
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            this.loger.Error("UpdateSysTaxInfo failed");
            return false;
        }

        public bool UpdateXTSWXX_QYBH(string qybh)
        {
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("QYBH", qybh);
                if (this.baseDAO.updateSQL("aisino.Fwkp.Xtsz.UpdateXTSWXX_QYBH", dictionary) > 0)
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
                this.loger.Error("UpdateDZDZInfo failed!");
                return false;
            }
            return false;
        }
    }
}

