namespace Aisino.Fwkp.Fptk.Entry
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fptk;
    using System;

    internal sealed class _QueryFPXXService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            if (param.Length != 3)
            {
                return null;
            }
            FPLX fpzl = Invoice.ParseFPLX(param[0] as string);
            int fphm = (int) param[2];
            IFpManager manager = new FpManager();
            Fpxx fpxx = manager.GetXxfp(fpzl, param[1] as string, fphm);
            return new object[] { fpxx, manager.Code() };
        }
    }
}

