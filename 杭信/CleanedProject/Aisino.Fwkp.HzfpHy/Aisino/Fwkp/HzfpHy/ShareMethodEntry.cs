namespace Aisino.Fwkp.HzfpHy
{
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Service;
    using System;

    public class ShareMethodEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            ServiceFactory.RegPubService("Aisino.Fwkp.HzfpHy.QuerySQD", "Aisino.Fwkp.HzfpHy.dll", "Aisino.Fwkp.HzfpHy.QueryHySQDService", null);
            ServiceFactory.RegPubService("Aisino.Fwkp.HzfpHy.HZFPGetXXBInfo", "Aisino.Fwkp.HzfpHy.dll", "Aisino.Fwkp.HzfpHy.SelectHySQDService", null);
            ServiceFactory.RegPubService("Aisino.Fwkp.HzfpHy.HzfpHyService", "Aisino.Fwkp.HzfpHy.dll", "Aisino.Fwkp.HzfpHy.HzfpHyService", null);
        }
    }
}

