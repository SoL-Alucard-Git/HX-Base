namespace Aisino.Fwkp.Fptk.Entry
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fptk;
    using System;

    internal sealed class CheckHZXXBBHPService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            if ((param != null) && (param.Length >= 2))
            {
                FPLX fplx;
                string redNum = param[0].ToString();
                if (Enum.TryParse<FPLX>(param[1].ToString(), out fplx))
                {
                    bool flag = new FpManager().CheckRedNum(redNum, fplx);
                    return new object[] { flag };
                }
            }
            return null;
        }
    }
}

