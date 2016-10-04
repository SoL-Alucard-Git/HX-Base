namespace Aisino.Fwkp.Fpzpz.BLL
{
    using Aisino.Fwkp.Fpzpz.IBLL;
    using Aisino.Fwkp.Fpzpz.IDAL;
    using System;
    using System.Collections.Generic;

    public class XZQYBM : Aisino.Fwkp.Fpzpz.IBLL.IXZQYBM
    {
        private Aisino.Fwkp.Fpzpz.IDAL.IXZQYBM dal = new XZQYBM();

        public List<XZQYBMModal> SelectXZQYBM_BM(string strBM)
        {
            return this.dal.SelectXZQYBM_BM(strBM);
        }

        public bool UpdateXZQYBM(string strSET, string strWHERE)
        {
            return this.dal.UpdateXZQYBM(strSET, strWHERE);
        }
    }
}

