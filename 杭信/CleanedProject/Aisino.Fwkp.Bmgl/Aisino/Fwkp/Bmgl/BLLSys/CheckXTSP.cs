namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using System;

    internal sealed class CheckXTSP : AbstractService
    {
        private BMSPManager spManager = new BMSPManager();

        protected override object[] doService(object[] param)
        {
            bool flag = this.spManager.CheckXTExist();
            return new object[] { flag };
        }
    }
}

