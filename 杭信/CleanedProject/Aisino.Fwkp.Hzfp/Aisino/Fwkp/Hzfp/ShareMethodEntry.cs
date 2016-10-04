namespace Aisino.Fwkp.Hzfp
{
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Service;
    using System;

    public class ShareMethodEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            ServiceFactory.RegPubService("Aisino.Fwkp.Hzfp.QuerySQD", "Aisino.Fwkp.Hzfp.dll", "Aisino.Fwkp.Hzfp.QuerySQDService", null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Hzfp.HZFPGetXXBInfo", "Aisino.Fwkp.Hzfp.dll", "Aisino.Fwkp.Hzfp.SelectSQDService", null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Hzfp.HzfpService", "Aisino.Fwkp.Hzfp.dll", "Aisino.Fwkp.Hzfp.BLL.HzfpService", null);
        }
    }
}

