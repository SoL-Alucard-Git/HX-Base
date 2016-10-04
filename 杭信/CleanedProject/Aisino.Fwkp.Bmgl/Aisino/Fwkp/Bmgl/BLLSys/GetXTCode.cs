namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using System;

    internal sealed class GetXTCode : AbstractService
    {
        private BMSPManager spManager = new BMSPManager();

        protected override object[] doService(object[] param)
        {
            if (param != null)
            {
                string xTCodeByName = this.spManager.GetXTCodeByName(param[0].ToString());
                return new object[] { xTCodeByName };
            }
            return null;
        }
    }
}

