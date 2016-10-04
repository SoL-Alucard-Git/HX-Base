namespace BSDC
{
    using System;

    public class QueryPrintEntity
    {
        public bool m_bIsLocked;
        public bool m_bPrint;
        public bool m_bShowDialog;
        public INV_TYPE m_invType;
        public ITEM_ACTION m_itemAction;
        public int m_nMonth;
        public int m_nTaxPeriod;
        public int m_nYear;
        public string m_strItemDetail;
        public string m_strSubItem;
        public string m_strTitle;

        public QueryPrintEntity()
        {
            
            this.m_bPrint = false;
            this.m_bShowDialog = false;
            this.m_invType = INV_TYPE.INV_SPECIAL;
            this.m_nYear = 0;
            this.m_nMonth = 0;
            this.m_nTaxPeriod = 0;
            this.m_itemAction = ITEM_ACTION.ITEM_TOTAL;
            this.m_strTitle = "";
            this.m_strSubItem = "";
            this.m_strItemDetail = "";
            this.m_bIsLocked = false;
        }
    }
}

