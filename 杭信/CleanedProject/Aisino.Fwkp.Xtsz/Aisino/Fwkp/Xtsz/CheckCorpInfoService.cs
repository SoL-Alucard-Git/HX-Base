namespace Aisino.Fwkp.Xtsz
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Xtsz.BLL;
    using System;

    internal sealed class CheckCorpInfoService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            ParaSetBLL tbll = new ParaSetBLL();
            if (!tbll.CheckCorpInfo())
            {
                return new object[] { false };
            }
            return new object[] { true };
        }
    }
}

