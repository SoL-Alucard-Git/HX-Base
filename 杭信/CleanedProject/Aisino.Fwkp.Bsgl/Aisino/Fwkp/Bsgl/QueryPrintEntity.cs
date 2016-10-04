namespace Aisino.Fwkp.Bsgl
{
    using System;

    public class QueryPrintEntity
    {
        public bool m_bIsLocked = false;
        public bool m_bPrint = false;
        public bool m_bShowDialog = false;
        public INV_TYPE m_invType = INV_TYPE.INV_SPECIAL;
        public ITEM_ACTION m_itemAction = ITEM_ACTION.ITEM_TOTAL;
        public int m_nMonth = 0;
        public int m_nTaxPeriod = 0;
        public int m_nYear = 0;
        public string m_strItemDetail = "";
        public string m_strSubItem = "";
        public string m_strTitle = "";
    }
}

