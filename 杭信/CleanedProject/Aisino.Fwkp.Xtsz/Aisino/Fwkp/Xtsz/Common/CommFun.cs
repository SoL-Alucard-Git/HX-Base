namespace Aisino.Fwkp.Xtsz.Common
{
    using System;

    public class CommFun
    {
        public class CorporationInfo
        {
            public bool m_bEasyLevy = false;
            public DateTime m_dtReportTime = DateTime.Now.Date;
            public long m_lUpperLimit = 0L;
            public int m_nBranchCount = 0;
            public int m_nMachineCode = 0;
            public int m_nSoftPanDiv = 0;
            public string m_strAccounter = "";
            public string m_strAddress = "";
            public string m_strAgenter = "";
            public string m_strBankAccount = "";
            public string m_strCorpCode = "";
            public string m_strCorpName = "";
            public string m_strRegType = "";
            public string m_strSignCode = "";
            public string m_strTelephone = "";
        }
    }
}

