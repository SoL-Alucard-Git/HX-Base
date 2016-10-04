namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.DAL;
    using System;

    internal sealed class CanUseThisSPFLBM : AbstractService
    {
        private BLL.BMSPFLManager spflManager = new BLL.BMSPFLManager();

        protected override object[] doService(object[] param)
        {
            if (param.Length == 2)
            {
                string bm = (string) param[0];
                bool isSPBMSel = (bool) param[1];
                DAL.BMSPFLManager manager = new DAL.BMSPFLManager();
                return new object[] { manager.CanUseThisSPFLBM(bm, isSPBMSel, false) };
            }
            if (param.Length >= 3)
            {
                string str2 = (string) param[0];
                bool flag2 = (bool) param[1];
                bool isXTSP = (bool) param[2];
                DAL.BMSPFLManager manager2 = new DAL.BMSPFLManager();
                return new object[] { manager2.CanUseThisSPFLBM(str2, flag2, isXTSP) };
            }
            return null;
        }
    }
}

