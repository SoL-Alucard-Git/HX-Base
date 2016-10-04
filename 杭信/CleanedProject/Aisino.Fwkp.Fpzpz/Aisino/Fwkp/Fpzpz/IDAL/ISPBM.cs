namespace Aisino.Fwkp.Fpzpz.IDAL
{
    using Aisino.Fwkp.Fpzpz.Model;
    using System;
    using System.Collections.Generic;

    internal interface ISPBM
    {
        bool AddInfoToGoodsKmTempTbl(SPBMModal spbmModal);
        List<SPBMModal> SelectSPBM_BM(string strBM);
        List<SPBMModal> SelectSPBM_CH();
        List<SPBMModal> SelectSPBM_CHFL();
        List<SPBMModal> SelectSPBM_MC_BM(string strMc, string strBm);
        List<SPBMModal> SelectSPBM_SPMC(string strSPMC);
        List<SPBMModal> SelectSPKMB();
        List<SPBMModal> SelectSPKMB_SPBH(string strSPBH);
        bool UpdateSPBM_XSSRKM(string strBm, string strXssrkm);
        bool UpdateSPBM_XSTHKM(string strBm, string strXsthkm);
        bool UpdateSPBM_YJZZSKM(string strBm, string strYjzzskm);
        bool UpdateSPKMB_XSSRKM(string strSpbh, string strXssrkm);
        bool UpdateSPKMB_XSTHKM(string strSpbh, string strXsthkm);
        bool UpdateSPKMB_YJZZSKM(string strSpbh, string strYjzzskm);
    }
}

