namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.Plugin.Core.Service;
    using System;
    using System.Collections.Generic;
    using BusinessObject;
    internal sealed class RegisitFileService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            List<VersionInfo> list = new RegistInfoDAL().SelectRegistFileName();
            return new object[] { list };
        }
    }
}

