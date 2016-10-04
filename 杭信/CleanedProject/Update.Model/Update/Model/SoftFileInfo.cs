namespace Update.Model
{
    using System;

    [Serializable]
    public class SoftFileInfo
    {
        private DateTime dateTime_0;
        private DateTime dateTime_1;
        private DateTime dateTime_2;
        private long long_0;
        private string string_0;
        private string string_1;

        public SoftFileInfo()
        {
            
        }

        public string CRCValue
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

        public DateTime CreationTime
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

        public DateTime LastAccessTime
        {
            get
            {
                return this.dateTime_1;
            }
            set
            {
                this.dateTime_1 = value;
            }
        }

        public DateTime LastWriteTime
        {
            get
            {
                return this.dateTime_2;
            }
            set
            {
                this.dateTime_2 = value;
            }
        }

        public long Length
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

        public string RelativePath
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
    }
}

