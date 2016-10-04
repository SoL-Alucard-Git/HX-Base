namespace Aisino.Fwkp.Fptk.Entry
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Fptk;
    using System;

    internal sealed class _QueryTRedJEService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            if ((param == null) || (param.Length < 2))
            {
                return null;
            }
            string blueFpdm = param[0].ToString();
            string blueFphm = param[1].ToString();
            decimal totalRedJe = new FpManager().GetTotalRedJe(blueFpdm, blueFphm);
            return new object[] { totalRedJe };
        }
    }
}

