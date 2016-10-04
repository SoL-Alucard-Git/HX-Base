namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.Plugin.Core.Command;
    using Aisino.Framework.Startup.Login;
    using System;
    using System.Windows.Forms;

    public class UserInfoManageCommand : AbstractCommand
    {
        private RoleUserDAL userDAL = new RoleUserDAL();

        protected override void RunCommand()
        {
            User user = this.userDAL.SelectUserByDM(UserInfo.Yhdm);
            new UpdUserInfoForm(user.Code, user.RealName, user.Password) { StartPosition = FormStartPosition.CenterScreen, ShowInTaskbar = true }.ShowDialog();
        }

        protected override bool SetValid()
        {
            if (UserInfo.IsAdmin)
            {
                return false;
            }
            return true;
        }
    }
}

