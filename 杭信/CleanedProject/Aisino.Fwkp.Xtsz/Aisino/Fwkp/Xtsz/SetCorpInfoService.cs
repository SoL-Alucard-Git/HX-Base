namespace Aisino.Fwkp.Xtsz
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Xtsz.BLL;
    using System;

    internal sealed class SetCorpInfoService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            ParaSetBLL tbll = new ParaSetBLL();
            if (!tbll.WriteTaxCardInfo())
            {
                return new object[] { false };
            }
            return new object[] { true };
        }
    }
}

