namespace Aisino.Fwkp.Bsgl
{
    using System;
    using System.Collections.Generic;

    public class InvVolumeStatEntity
    {
        public int m_nInvType = 0;
        public int m_nIssueCount = 0;
        public int m_nWasteCount = 0;
        public List<string> m_strIssueList = new List<string>();
        public List<string> m_strWasteList = new List<string>();
    }
}

