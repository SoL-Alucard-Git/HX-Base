namespace BSDC
{
    using System;
    using System.Collections.Generic;

    public class InvVolumeStatEntity
    {
        public int m_nInvType;
        public int m_nIssueCount;
        public int m_nWasteCount;
        public List<string> m_strIssueList;
        public List<string> m_strWasteList;

        public InvVolumeStatEntity()
        {
            
            this.m_nInvType = 0;
            this.m_nIssueCount = 0;
            this.m_nWasteCount = 0;
            this.m_strIssueList = new List<string>();
            this.m_strWasteList = new List<string>();
        }
    }
}

