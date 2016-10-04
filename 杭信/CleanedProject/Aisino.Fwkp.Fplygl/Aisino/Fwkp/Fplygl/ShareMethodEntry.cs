namespace Aisino.Fwkp.Fplygl
{
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Service;
    using System;

    public class ShareMethodEntry : AbstractCommand
    {
        protected override void RunCommand()
        {
            ServiceFactory.RegPubService("Aisino.Fwkp.Fplygl.GetStockInfo", "Aisino.Fwkp.Fplygl.dll", "Aisino.Fwkp.Fplygl.GetStockInfoService", null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Fplygl.QueryAllocateInfo", "Aisino.Fwkp.Fplygl.dll", "Aisino.Fwkp.Fplygl.QueryAllocateInfoService", null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Fplygl.AllocateExcute", "Aisino.Fwkp.Fplygl.dll", "Aisino.Fwkp.Fplygl.AllocateExcuteService", null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Fplygl.DownloadQuery", "Aisino.Fwkp.Fplygl.dll", "Aisino.Fwkp.Fplygl.DownloadQueryService", null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Fplygl.DownloadExcute", "Aisino.Fwkp.Fplygl.dll", "Aisino.Fwkp.Fplygl.DownloadExcuteService", null);
        }
    }
}

