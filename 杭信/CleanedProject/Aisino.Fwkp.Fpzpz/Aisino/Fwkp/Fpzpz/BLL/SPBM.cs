namespace Aisino.Fwkp.Fpzpz.BLL
{
    using Aisino.Fwkp.Fpzpz.IBLL;
    using Aisino.Fwkp.Fpzpz.IDAL;
    using Aisino.Fwkp.Fpzpz.Model;
    using System;
    using System.Collections.Generic;

    public class SPBM : Aisino.Fwkp.Fpzpz.IBLL.ISPBM
    {
        private Aisino.Fwkp.Fpzpz.IDAL.ISPBM dal = new SPBM();

        public bool AddInfoToGoodsKmTempTbl(SPBMModal spbmModal)
        {
            return this.dal.AddInfoToGoodsKmTempTbl(spbmModal);
        }

        public List<SPBMModal> SelectSPBM_BM(string strBM)
        {
            return this.dal.SelectSPBM_BM(strBM);
        }

        public List<SPBMModal> SelectSPBM_CH()
        {
            return this.dal.SelectSPBM_CH();
        }

        public List<SPBMModal> SelectSPBM_CHFL()
        {
            return this.dal.SelectSPBM_CHFL();
        }

        public List<SPBMModal> SelectSPBM_MC_BM(string strMc, string strBm)
        {
            return this.dal.SelectSPBM_MC_BM(strMc, strBm);
        }

        public List<SPBMModal> SelectSPBM_SPMC(string strSPMC)
        {
            return this.dal.SelectSPBM_SPMC(strSPMC);
        }

        public List<SPBMModal> SelectSPKMB()
        {
            return this.dal.SelectSPKMB();
        }

        public List<SPBMModal> SelectSPKMB_SPBH(string strSPBH)
        {
            return this.dal.SelectSPKMB_SPBH(strSPBH);
        }

        public bool UpdateSPBM_XSSRKM(string strBm, string strXssrkm)
        {
            return this.dal.UpdateSPBM_XSSRKM(strBm, strXssrkm);
        }

        public bool UpdateSPBM_XSTHKM(string strBm, string strXsthkm)
        {
            return this.dal.UpdateSPBM_XSTHKM(strBm, strXsthkm);
        }

        public bool UpdateSPBM_YJZZSKM(string strBm, string strYjzzskm)
        {
            return this.dal.UpdateSPBM_YJZZSKM(strBm, strYjzzskm);
        }

        public bool UpdateSPKMB_XSSRKM(string strSpbh, string strXssrkm)
        {
            return this.dal.UpdateSPKMB_XSSRKM(strSpbh, strXssrkm);
        }

        public bool UpdateSPKMB_XSTHKM(string strSpbh, string strXsthkm)
        {
            return this.dal.UpdateSPKMB_XSTHKM(strSpbh, strXsthkm);
        }

        public bool UpdateSPKMB_YJZZSKM(string strSpbh, string strYjzzskm)
        {
            return this.dal.UpdateSPKMB_YJZZSKM(strSpbh, strYjzzskm);
        }
    }
}

