namespace Aisino.Fwkp.Fpzpz.IBLL
{
    using Aisino.Fwkp.Fpzpz.Model;
    using System;
    using System.Collections.Generic;

    internal interface IKHBM
    {
        bool AddInfoToCusrKmTempTbl(KHBMModal khbmModal);
        List<KHBMModal> SelectKHBM_BM(string strBM);
        List<KHBMModal> SelectKHBM_DQFL();
        List<KHBMModal> SelectKHBM_KH();
        List<KHBMModal> SelectKHBM_KHFL();
        List<KHBMModal> SelectKHBM_MC_BM(string strMc, string strBm);
        List<KHBMModal> SelectKHBM_WJ(int iWJ);
        List<KHBMModal> SelectKHKMB();
        List<KHBMModal> SelectKHKMB_KHBH(string strKHBH);
        bool UpdateKHBM_DQBM(string strBM, string strDQBM);
        bool UpdateKHBM_Dqkm(string strDqbm, string strDqmc, string strDqkm);
        bool UpdateKHBM_DQMC(string strBM, string strDQMC);
        bool UpdateKHBM_Yskm(string strBm, string strYskm);
        bool UpdateKHKMB_Yskm(string strKhbm, string strYskm);
    }
}

