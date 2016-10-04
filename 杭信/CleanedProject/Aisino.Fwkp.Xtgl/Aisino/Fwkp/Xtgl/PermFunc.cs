namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.Startup.Login;
    using System;
    using System.Runtime.CompilerServices;

    public class PermFunc
    {
        private string mCreator;
        private string mID;

        public PermFunc()
        {
        }

        public PermFunc(string id)
        {
            this.mID = id;
        }

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

        public string FuncID
        {
            get
            {
                return this.mID;
            }
            set
            {
                this.mID = value;
            }
        }
    }
}

