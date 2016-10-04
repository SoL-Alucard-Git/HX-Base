namespace Aisino.Fwkp.Wbjk.Entry
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Wbjk.BLL;
    using System;

    internal sealed class Wbjk_FPZFSuccess : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            if (param.Length == 4)
            {
                string fpdm = (string) param[0];
                string s = (string) param[1];
                string djbh = (string) param[2];
                int num = (int) param[3];
                if ((fpdm == null) || (fpdm.Length == 0))
                {
                    return null;
                }
                if ((s == null) || (s.Length == 0))
                {
                    return null;
                }
                if ((djbh == null) || (djbh.Length == 0))
                {
                    return null;
                }
                SaleBillCtrl instance = SaleBillCtrl.Instance;
                int result = 0;
                int.TryParse(s, out result);
                if (num == 0)
                {
                    instance.ZFRecover(fpdm, result.ToString(), djbh);
                }
            }
            return null;
        }
    }
}

