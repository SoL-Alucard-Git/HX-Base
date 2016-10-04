namespace Aisino.Framework.Plugin.Core.Registry
{
    using System;
    using System.Collections.Generic;

    public class RegFileSetupResult
    {
        private List<RegFileInfo> list_0;
        private List<RegFileInfo> list_1;
        private List<RegFileInfo> list_2;

        internal RegFileSetupResult()
        {
            
        }

        internal RegFileSetupResult(RegFileSetupResult regFileSetupResult_0)
        {
            
            this.list_0 = new List<RegFileInfo>();
            this.list_1 = new List<RegFileInfo>();
            this.list_2 = new List<RegFileInfo>();
            foreach (RegFileInfo info in regFileSetupResult_0.list_0)
            {
                this.list_0.Add(info);
            }
            foreach (RegFileInfo info2 in regFileSetupResult_0.list_1)
            {
                this.list_1.Add(info2);
            }
            foreach (RegFileInfo info3 in regFileSetupResult_0.list_2)
            {
                this.list_2.Add(info3);
            }
        }

        public int FileCount
        {
            get
            {
                return ((this.list_0.Count + this.list_1.Count) + this.list_2.Count);
            }
        }

        public List<RegFileInfo> InvalidRegFiles
        {
            get
            {
                return this.list_2;
            }
            internal set
            {
                this.list_2 = value;
            }
        }

        public List<RegFileInfo> NormalRegFiles
        {
            get
            {
                return this.list_0;
            }
            internal set
            {
                this.list_0 = value;
            }
        }

        public List<RegFileInfo> OutOfDateRegFiles
        {
            get
            {
                return this.list_1;
            }
            internal set
            {
                this.list_1 = value;
            }
        }
    }
}

