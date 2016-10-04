namespace Aisino.Fwkp.Fpzpz.IDAL
{
    using Aisino.Fwkp.Fpzpz.Model;
    using System;
    using System.Collections.Generic;

    internal interface IPZFLB
    {
        bool AddInfoToPZTempTbl(TPZEntry_InfoModal pz_InfoModal);
        List<TPZEntry_InfoModal> SelectPZFLB_ZH();
        List<TPZEntry_InfoModal> SelectPZFLB_ZH_DJXX(string strZH);
    }
}

