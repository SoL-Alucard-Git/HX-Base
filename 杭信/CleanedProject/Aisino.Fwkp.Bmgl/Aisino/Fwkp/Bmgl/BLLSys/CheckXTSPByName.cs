namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using System;

    internal sealed class CheckXTSPByName : AbstractService
    {
        private BMSPManager spManager = new BMSPManager();

        protected override object[] doService(object[] param)
        {
            if (((param != null) && (param.Length > 0)) && (param[0] != null))
            {
                string str = this.spManager.CheckXTSP(param[0].ToString());
                return new object[] { str };
            }
            return new object[] { "0" };
        }
    }
}

