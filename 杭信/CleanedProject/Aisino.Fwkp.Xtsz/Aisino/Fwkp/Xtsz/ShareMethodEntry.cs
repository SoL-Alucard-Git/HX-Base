namespace Aisino.Fwkp.Xtsz
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Service;
    using System;

    public class ShareMethodEntry : AbstractCommand
    {
        public ShareMethodEntry()
        {
            ServiceFactory.RegPubService("Aisino.Fwkp.QueryCorporationInfo", "Aisino.Fwkp.Xtsz.dll", "Aisino.Fwkp.Xtsz.QueryCorporationInfoService", null);
            ServiceFactory.RegPubService("Aisino.Fwkp.RunInitial", "Aisino.Fwkp.Xtsz.dll", "Aisino.Fwkp.Xtsz.InitialService", null);
            ServiceFactory.RegPubService("Aisino.Fwkp.SetCorpInfo", "Aisino.Fwkp.Xtsz.dll", "Aisino.Fwkp.Xtsz.SetCorpInfoService", null);
            ServiceFactory.RegPubService("Aisino.Fwkp.CheckCorpInfo", "Aisino.Fwkp.Xtsz.dll", "Aisino.Fwkp.Xtsz.CheckCorpInfoService", null);
            ServiceFactory.RegPubService("Aisino.Fwkp.QueryDZDZInfo", "Aisino.Fwkp.Xtsz.dll", "Aisino.Fwkp.Xtsz.QueryDZDZInfoService", null);
            ServiceFactory.RegPubService("Aisino.Fwkp.UpdateDZDZInfo", "Aisino.Fwkp.Xtsz.dll", "Aisino.Fwkp.Xtsz.UpdateDZDZInfoService", null);
        }

        protected override void RunCommand()
        {
        }

        protected override bool SetValid()
        {
            return (TaxCardFactory.CreateTaxCard().get_TaxMode() == 2);
        }
    }
}

