namespace Aisino.Fwkp.Wbjk.DAL
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Fwkp.Wbjk.Common;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class FPZFdal
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();

        public InvoiceDataDetail GetInvInfo(string strInvType, string strCode, string strNum)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPZL", strInvType);
            dictionary.Add("FPDM", strCode);
            dictionary.Add("FPHM", strNum);
            InvoiceDataDetail detail = new InvoiceDataDetail();
            ArrayList list = this.baseDAO.querySQL("aisino.Fwkp.Wbjk.GetInfoXXFP", dictionary);
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            for (int i = 0; i < list.Count; i++)
            {
                dictionary2 = list[i] as Dictionary<string, object>;
                detail.m_strInvType = strInvType;
                detail.m_strInvCode = strCode;
                detail.m_strInvNum = strNum;
                detail.m_dtInvDate = Convert.ToDateTime(dictionary2["KPRQ"].ToString());
                detail.m_strBuyerCode = dictionary2["GFSH"].ToString();
                detail.m_strBuyerName = dictionary2["GFMC"].ToString();
                detail.m_dbAmount = dictionary2["HJJE"].ToString();
                detail.m_dbTaxRate = dictionary2["SLV"].ToString();
                detail.m_dbTax = dictionary2["HJSE"].ToString();
                detail.XFMC = dictionary2["XFMC"].ToString();
                detail.XFSH = dictionary2["XFSH"].ToString();
                detail.XSBM = dictionary2["XSBM"].ToString();
                detail.GFDZDH = dictionary2["GFDZDH"].ToString();
                detail.XFDZ = dictionary2["XFDZ"].ToString();
                detail.KHYHMC = dictionary2["KHYHMC"].ToString();
                detail.ZHD = dictionary2["ZHD"].ToString();
                detail.XHD = dictionary2["XHD"].ToString();
                detail.SCCJMC = dictionary2["SCCJMC"].ToString();
                detail.CM = dictionary2["CM"].ToString();
                detail.TYDH = dictionary2["TYDH"].ToString();
                detail.MDD = dictionary2["MDD"].ToString();
                detail.YYZZH = dictionary2["YYZZH"].ToString();
                detail.XFDZDH = dictionary2["XFDZDH"].ToString();
            }
            return detail;
        }

        public AisinoDataSet GetZuoFeiXSDJ(int pagesize, int pageno, bool IsAdmin, string mc)
        {
            DateTime cardClock = TaxCardFactory.CreateTaxCard().GetCardClock();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (IsAdmin)
            {
                dictionary.Add("ISADMIN", 1);
                dictionary.Add("KPRMC", mc);
                dictionary.Add("DATE", cardClock);
            }
            else
            {
                dictionary.Add("ISADMIN", 0);
                dictionary.Add("KPRMC", mc);
                dictionary.Add("DATE", cardClock);
            }
            return this.baseDAO.querySQLDataSet("aisino.Fwkp.Wbjk.FPZFGetXSDJ", dictionary, pagesize, pageno);
        }

        public int RollBackRecord(string strInvType, string strInvCode, string strInvNum)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPZL", strInvType);
            dictionary.Add("FPDM", strInvCode);
            dictionary.Add("FPHM", strInvNum);
            return this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.RollBackXXFP", dictionary);
        }

        public int ZuoFei(string FPZL, string FPDM, string FPHM)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("FPZL", FPZL);
            dictionary.Add("FPDM", FPDM);
            dictionary.Add("FPHM", FPHM);
            return this.baseDAO.updateSQL("aisino.Fwkp.Wbjk.FPZFZuoFeiFP", dictionary);
        }
    }
}

