namespace Aisino.Fwkp.Fpzpz.BLL
{
    using Aisino.Fwkp.Fpzpz.IBLL;
    using Aisino.Fwkp.Fpzpz.IDAL;
    using Aisino.Fwkp.Fpzpz.Model;
    using System;
    using System.Collections.Generic;

    public class PZFLB : Aisino.Fwkp.Fpzpz.IBLL.IPZFLB
    {
        private Aisino.Fwkp.Fpzpz.IDAL.IPZFLB dal = new PZFLB();

        public bool AddInfoToPZTempTbl(TPZEntry_InfoModal pz_InfoModal)
        {
            return this.dal.AddInfoToPZTempTbl(pz_InfoModal);
        }

        public List<TPZEntry_InfoModal> SelectPZFLB_ZH()
        {
            return this.dal.SelectPZFLB_ZH();
        }

        public List<TPZEntry_InfoModal> SelectPZFLB_ZH_DJXX(string strZH)
        {
            return this.dal.SelectPZFLB_ZH_DJXX(strZH);
        }
    }
}

