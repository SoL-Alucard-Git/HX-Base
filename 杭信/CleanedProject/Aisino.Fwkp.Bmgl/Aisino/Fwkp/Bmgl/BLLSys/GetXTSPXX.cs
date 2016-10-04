namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using System;

    internal sealed class GetXTSPXX : AbstractService
    {
        private BMSPManager spManager = new BMSPManager();

        protected override object[] doService(object[] param)
        {
            if (((param != null) && (param.Length > 0)) && (param[0] != null))
            {
                string[] sPXXByName = this.spManager.GetSPXXByName(param[0].ToString());
                return new object[] { sPXXByName };
            }
            return null;
        }
    }
}

