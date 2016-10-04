namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.DAL;
    using System;

    internal sealed class UpdateDatabaseBySPFL : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            if (param.Length >= 1)
            {
                string sPFL = (string) param[0];
                BMSPFLManager manager = new BMSPFLManager();
                return new object[] { manager.UpdateDatabaseBySPFL(sPFL) };
            }
            return null;
        }
    }
}

