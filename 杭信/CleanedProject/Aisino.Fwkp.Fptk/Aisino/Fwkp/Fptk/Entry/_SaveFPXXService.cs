namespace Aisino.Fwkp.Fptk.Entry
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fptk;
    using System;

    internal sealed class _SaveFPXXService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            if (param.Length != 1)
            {
                return null;
            }
            Fpxx fp = param[0] as Fpxx;
            if (fp == null)
            {
                return null;
            }
            if (new FpManager().SaveXxfp(fp))
            {
                return new object[] { "0000" };
            }
            return new object[] { "INP-242117" };
        }
    }
}

