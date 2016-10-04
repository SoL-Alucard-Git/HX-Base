namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Plugin.Core.Service;
    using System;

    public class XtglShareMethods : AbstractCommand
    {
        public XtglShareMethods()
        {
            ServiceFactory.RegPubService("Aisino.Fwkp.Xtgl.UserRoleService", "Aisino.Fwkp.Xtgl.dll", typeof(UserRoleService).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Xtgl.UserPwdService", "Aisino.Fwkp.Xtgl.dll", typeof(UserPwdService).FullName, null);
            ServiceFactory.RegPubService("Aisino.Fwkp.Xtgl.RegisitFileService", "Aisino.Fwkp.Xtgl.dll", typeof(RegisitFileService).FullName, null);
        }
    }
}

