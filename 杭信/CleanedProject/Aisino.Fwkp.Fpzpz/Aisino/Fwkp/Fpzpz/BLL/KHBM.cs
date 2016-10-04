namespace Aisino.Fwkp.Fpzpz.BLL
{
    using Aisino.Fwkp.Fpzpz.IBLL;
    using Aisino.Fwkp.Fpzpz.IDAL;
    using Aisino.Fwkp.Fpzpz.Model;
    using System;
    using System.Collections.Generic;

    public class KHBM : Aisino.Fwkp.Fpzpz.IBLL.IKHBM
    {
        private Aisino.Fwkp.Fpzpz.IDAL.IKHBM dal = new KHBM();

        public bool AddInfoToCusrKmTempTbl(KHBMModal khbmModal)
        {
            return this.dal.AddInfoToCusrKmTempTbl(khbmModal);
        }

        public List<KHBMModal> SelectKHBM_BM(string strBM)
        {
            return this.dal.SelectKHBM_BM(strBM);
        }

        public List<KHBMModal> SelectKHBM_DQFL()
        {
            return this.dal.SelectKHBM_DQFL();
        }

        public List<KHBMModal> SelectKHBM_KH()
        {
            return this.dal.SelectKHBM_KH();
        }

        public List<KHBMModal> SelectKHBM_KHFL()
        {
            return this.dal.SelectKHBM_KHFL();
        }

        public List<KHBMModal> SelectKHBM_MC_BM(string strMc, string strBm)
        {
            return this.dal.SelectKHBM_MC_BM(strMc, strBm);
        }

        public List<KHBMModal> SelectKHBM_WJ(int iWJ)
        {
            return this.dal.SelectKHBM_WJ(iWJ);
        }

        public List<KHBMModal> SelectKHKMB()
        {
            return this.dal.SelectKHKMB();
        }

        public List<KHBMModal> SelectKHKMB_KHBH(string strKHBH)
        {
            return this.dal.SelectKHKMB_KHBH(strKHBH);
        }

        public bool UpdateKHBM_DQBM(string strBM, string strDQBM)
        {
            return this.dal.UpdateKHBM_DQBM(strBM, strDQBM);
        }

        public bool UpdateKHBM_Dqkm(string strDqbm, string strDqmc, string strDqkm)
        {
            return this.dal.UpdateKHBM_Dqkm(strDqbm, strDqmc, strDqkm);
        }

        public bool UpdateKHBM_DQMC(string strBM, string strDQMC)
        {
            return this.dal.UpdateKHBM_DQMC(strBM, strDQMC);
        }

        public bool UpdateKHBM_Yskm(string strBm, string strYskm)
        {
            return this.dal.UpdateKHBM_Yskm(strBm, strYskm);
        }

        public bool UpdateKHKMB_Yskm(string strKhbm, string strYskm)
        {
            return this.dal.UpdateKHKMB_Yskm(strKhbm, strYskm);
        }
    }
}

