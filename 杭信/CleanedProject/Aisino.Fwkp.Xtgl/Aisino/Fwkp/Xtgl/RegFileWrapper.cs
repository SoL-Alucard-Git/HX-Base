namespace Aisino.Fwkp.Xtgl
{
    using Aisino.Framework.Plugin.Core.Registry;
    using System;
    using System.Runtime.CompilerServices;

    public class RegFileWrapper
    {
        private string displayName;
        private RegFileInfo regFile;

        public RegFileWrapper(RegFileInfo fileInfo)
        {
            this.regFile = fileInfo;
        }

        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(this.displayName))
                {
                    this.displayName = this.VerFlag.ToString();
                }
                return this.displayName;
            }
            set
            {
                this.displayName = value;
            }
        }

        public bool ExportFlag { get; set; }

        public qwe FileContent
        {
            get
            {
                return this.regFile.FileContent;
            }
        }

        public DateTime FileModifyDate
        {
            get
            {
                return this.regFile.FileModifyDate;
            }
        }

        public string FileName
        {
            get
            {
                return this.regFile.FileName;
            }
        }

        public RegFileType FileType { get; set; }

        public string SoftFlag
        {
            get
            {
                return this.regFile.SoftFlag;
            }
        }

        public Aisino.Fwkp.Xtgl.VerFlag VerFlag
        {
            get
            {
                if (this.regFile.VerFlag != null)
                {
                    return (Aisino.Fwkp.Xtgl.VerFlag) Enum.Parse(typeof(Aisino.Fwkp.Xtgl.VerFlag), this.regFile.VerFlag, true);
                }
                return Aisino.Fwkp.Xtgl.VerFlag.NONE;
            }
        }

        public string VersionDesc { get; set; }
    }
}

