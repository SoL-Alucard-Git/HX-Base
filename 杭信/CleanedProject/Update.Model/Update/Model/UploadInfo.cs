namespace Update.Model
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class UploadInfo
    {
        [CompilerGenerated]
        private bool bool_0;
        [CompilerGenerated]
        private long long_0;
        [CompilerGenerated]
        private long long_1;
        [CompilerGenerated]
        private string string_0;

        public UploadInfo()
        {
            
        }

        public long ContentLength
        {
            [CompilerGenerated]
            get
            {
                return this.long_0;
            }
            [CompilerGenerated]
            set
            {
                this.long_0 = value;
            }
        }

        public string FileName
        {
            [CompilerGenerated]
            get
            {
                return this.string_0;
            }
            [CompilerGenerated]
            set
            {
                this.string_0 = value;
            }
        }

        public bool IsReady
        {
            [CompilerGenerated]
            get
            {
                return this.bool_0;
            }
            [CompilerGenerated]
            set
            {
                this.bool_0 = value;
            }
        }

        public long UploadedLength
        {
            [CompilerGenerated]
            get
            {
                return this.long_1;
            }
            [CompilerGenerated]
            set
            {
                this.long_1 = value;
            }
        }
    }
}

