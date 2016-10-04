namespace BSDC
{
    using System;
    using System.Data;

    public class CInvStatData
    {
        public DataTable m_DataTableGrid;
        public string[] m_strHeadValue;
        public string m_strInvTypeName;

        public CInvStatData()
        {
            
            this.m_strInvTypeName = "";
            this.m_strHeadValue = new string[10];
            this.m_DataTableGrid = new DataTable();
        }
    }
}

