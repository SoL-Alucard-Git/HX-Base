namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using System;

    internal sealed class ImportXTSP : AbstractService
    {
        private BMSPManager spManager = new BMSPManager();

        protected override object[] doService(object[] param)
        {
            if (param != null)
            {
                this.spManager.ImportXTSP(param[0].ToString(), false);
            }
            else
            {
                this.spManager.ImportXTSP("", false);
            }
            return null;
        }
    }
}

