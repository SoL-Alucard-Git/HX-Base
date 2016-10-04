namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.Plugin.Core.Service;
    using System;

    internal sealed class UserPwdService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            bool flag = false;
            if ((param != null) && (param.Length == 2))
            {
                string userName = param[0].ToString();
                string str2 = param[1].ToString();
                RoleUserDAL rdal = new RoleUserDAL();
                flag = rdal.SelectUserPwd(userName).Equals(str2);
            }
            return new object[] { flag };
        }
    }
}

