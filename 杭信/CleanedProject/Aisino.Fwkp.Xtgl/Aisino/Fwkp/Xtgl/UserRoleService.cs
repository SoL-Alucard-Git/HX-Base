namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.Plugin.Core.Service;
    using System;
    using System.Collections.Generic;

    internal sealed class UserRoleService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            List<string> list = new RoleUserDAL().SelectAllUserNames();
            return new object[] { list };
        }
    }
}

