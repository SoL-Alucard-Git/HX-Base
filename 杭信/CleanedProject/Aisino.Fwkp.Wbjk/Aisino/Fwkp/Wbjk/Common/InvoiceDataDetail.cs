namespace Aisino.Fwkp.Wbjk.Common
{
    using System;

    public class InvoiceDataDetail : InvoiceData
    {
        public string CM;
        public string GFDZDH;
        public string KHYHMC;
        public string m_dbAmount;
        public string m_dbTax;
        public string m_dbTaxRate;
        public DateTime m_dtInvDate;
        public string m_strBuyerCode;
        public string m_strBuyerName;
        public string MDD;
        public string SCCJMC;
        public string TYDH;
        public string XFDZ;
        public string XFDZDH;
        public string XFMC;
        public string XFSH;
        public string XHD;
        public string XSBM;
        public string YYZZH;
        public string ZHD;

        public InvoiceDataDetail()
        {
            base.m_strInvType = "";
            base.m_strInvCode = "";
            base.m_strInvNum = "";
            this.m_dtInvDate = DateTime.Now.Date;
            this.m_strBuyerCode = "";
            this.m_strBuyerName = "";
            this.m_dbAmount = "";
            this.m_dbTaxRate = "";
            this.m_dbTax = "";
            this.XFMC = "";
            this.XFSH = "";
            this.XSBM = "";
            this.GFDZDH = "";
            this.XFDZ = "";
            this.KHYHMC = "";
            this.ZHD = "";
            this.XHD = "";
            this.SCCJMC = "";
            this.CM = "";
            this.TYDH = "";
            this.MDD = "";
            this.YYZZH = "";
            this.XFDZDH = "";
        }
    }
}

