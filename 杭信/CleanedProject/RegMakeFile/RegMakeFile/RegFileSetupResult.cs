namespace RegMakeFile
{
    using System;
    using System.Collections.Generic;

    public class RegFileSetupResult
    {
        private List<RegFileInfo> invalidRegFiles;
        private List<RegFileInfo> normalRegFiles;
        private List<RegFileInfo> outOfDateRegFiles;

        internal RegFileSetupResult()
        {
        }

        internal RegFileSetupResult(RegFileSetupResult setupResult)
        {
            this.normalRegFiles = new List<RegFileInfo>();
            this.outOfDateRegFiles = new List<RegFileInfo>();
            this.invalidRegFiles = new List<RegFileInfo>();
            foreach (RegFileInfo info in setupResult.normalRegFiles)
            {
                this.normalRegFiles.Add(info);
            }
            foreach (RegFileInfo info in setupResult.outOfDateRegFiles)
            {
                this.outOfDateRegFiles.Add(info);
            }
            foreach (RegFileInfo info in setupResult.invalidRegFiles)
            {
                this.invalidRegFiles.Add(info);
            }
        }

        public int FileCount
        {
            get
            {
                return ((this.normalRegFiles.Count + this.outOfDateRegFiles.Count) + this.invalidRegFiles.Count);
            }
        }

        public List<RegFileInfo> InvalidRegFiles
        {
            get
            {
                return this.invalidRegFiles;
            }
            internal set
            {
                this.invalidRegFiles = value;
            }
        }

        public List<RegFileInfo> NormalRegFiles
        {
            get
            {
                return this.normalRegFiles;
            }
            internal set
            {
                this.normalRegFiles = value;
            }
        }

        public List<RegFileInfo> OutOfDateRegFiles
        {
            get
            {
                return this.outOfDateRegFiles;
            }
            internal set
            {
                this.outOfDateRegFiles = value;
            }
        }
    }
}

