namespace Update.Model
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class DownloadInfo
    {
        private DateTime dateTime_0;
        private IList<SoftFileInfo> ilist_0;
        private int int_0;
        private int int_1;
        private long long_0;
        private SoftVersionInfo softVersionInfo_0;
        private string string_0;
        private string string_1;
        private string string_2;

        public DownloadInfo()
        {
            
            this.SoftVer = new SoftVersionInfo();
            this.FileList = new List<SoftFileInfo>();
        }

        public string Comment
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        public DateTime DownloadDate
        {
            get
            {
                return this.dateTime_0;
            }
            set
            {
                this.dateTime_0 = value;
            }
        }

        public int DownloadId
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        public IList<SoftFileInfo> FileList
        {
            get
            {
                return this.ilist_0;
            }
            set
            {
                this.ilist_0 = value;
            }
        }

        public string PreVersion
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public SoftVersionInfo SoftVer
        {
            get
            {
                return this.softVersionInfo_0;
            }
            set
            {
                this.softVersionInfo_0 = value;
            }
        }

        public string TaxCode
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public long TotalSize
        {
            get
            {
                return this.long_0;
            }
            set
            {
                this.long_0 = value;
            }
        }

        public int Xzbz
        {
            get
            {
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
            }
        }
    }
}

