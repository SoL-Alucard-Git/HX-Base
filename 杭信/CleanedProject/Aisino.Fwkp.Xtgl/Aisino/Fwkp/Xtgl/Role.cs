namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.Startup.Login;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Role
    {
        private string mCreator;
        private readonly List<PermFunc> mPermFuncList;

        public Role()
        {
            this.mPermFuncList = new List<PermFunc>();
        }

        public Role(string id, string name)
        {
            this.mPermFuncList = new List<PermFunc>();
            this.ID = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return this.Name;
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

        public string Description { get; set; }

        public string ID { get; set; }

        public string Name { get; set; }

        public List<PermFunc> PermFuncList
        {
            get
            {
                return this.mPermFuncList;
            }
        }
    }
}

