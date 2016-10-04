namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.Startup.Login;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class User
    {
        private string mCreator;
        private readonly List<Role> mRoleList = new List<Role>();

        public string Code { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreatorName
        {
            get
            {
                if (string.IsNullOrEmpty(this.mCreator))
                {
                    this.mCreator = UserInfo.Yhdm;
                }
                return this.mCreator;
            }
            set
            {
                this.mCreator = value;
            }
        }

        public string Description { get; set; }

        public bool IsAdmin { get; set; }

        public string Password { get; set; }

        public string RealName { get; set; }

        public List<Role> RoleList
        {
            get
            {
                return this.mRoleList;
            }
        }

        public string Telephone { get; set; }
    }
}

