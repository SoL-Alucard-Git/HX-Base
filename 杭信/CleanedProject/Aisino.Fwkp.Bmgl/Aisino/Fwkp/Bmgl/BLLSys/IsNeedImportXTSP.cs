namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using System;

    internal sealed class IsNeedImportXTSP : AbstractService
    {
        private BMSPManager spManager = new BMSPManager();

        protected override object[] doService(object[] param)
        {
            bool flag = false;
            if (param != null)
            {
                flag = this.spManager.IsNeedImportXTSP(param[0].ToString());
            }
            else
            {
                flag = this.spManager.IsNeedImportXTSP("");
            }
            return new object[] { flag.ToString() };
        }
    }
}

