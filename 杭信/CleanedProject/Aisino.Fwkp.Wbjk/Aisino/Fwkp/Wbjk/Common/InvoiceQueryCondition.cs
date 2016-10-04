namespace Aisino.Fwkp.Wbjk.Common
{
    using System;
    using System.Collections.Generic;

    public class InvoiceQueryCondition : CommFun
    {
        public DateTime m_dtEnd = Convert.ToDateTime("0001-01-01");
        public DateTime m_dtStart = Convert.ToDateTime("0001-01-01");
        public List<object> m_strBillCodeList = new List<object>();
        public List<object> m_strBuyerCodeList = new List<object>();
        public List<object> m_strBuyerNameList = new List<object>();
        public string m_strCondition = "";
        public List<object> m_strGoodsNameList = new List<object>();
        public List<object> m_strInvCodeList = new List<object>();
        public List<object> m_strInvNumList = new List<object>();
        public string m_strInvType = "";
        public List<object> m_strStateList = new List<object>();
        public string m_strWasteFlag = "";
    }
}

