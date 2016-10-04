namespace Update.Model
{
    using System;

    [Serializable]
    public class SoftVersionInfo
    {
        private bool bool_0;
        private bool bool_1;
        private DateTime dateTime_0;
        private DateTime dateTime_1;
        private DateTime dateTime_2;
        private int int_0;
        private int int_1;
        private string string_0;
        private string string_1;
        private string string_10;
        private string string_11;
        private string string_12;
        private string string_2;
        private string string_3;
        private string string_4;
        private string string_5;
        private string string_6;
        private string string_7;
        private string string_8;
        private string string_9;

        public SoftVersionInfo()
        {
            
        }

        public string BserverStr
        {
            get
            {
                return this.string_8;
            }
            set
            {
                this.string_8 = value;
            }
        }

        public string ClientIp
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

        public string ClientName
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

        public int ClientPort
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

        public string DisableStr
        {
            get
            {
                return this.string_10;
            }
            set
            {
                this.string_10 = value;
            }
        }

        public string EnableStr
        {
            get
            {
                return this.string_9;
            }
            set
            {
                this.string_9 = value;
            }
        }

        public DateTime ExpireTime
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

        public string FilePath
        {
            get
            {
                return this.string_7;
            }
            set
            {
                this.string_7 = value;
            }
        }

        public bool Force
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public bool IsUses
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                this.bool_1 = value;
            }
        }

        public string RelativeSofts
        {
            get
            {
                return this.string_11;
            }
            set
            {
                this.string_11 = value;
            }
        }

        public string RestrictVer
        {
            get
            {
                return this.string_12;
            }
            set
            {
                this.string_12 = value;
            }
        }

        public string SoftName
        {
            get
            {
                return this.string_4;
            }
            set
            {
                this.string_4 = value;
            }
        }

        public string SoftNameCaption
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = value;
            }
        }

        public int SoftVersionId
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

        public DateTime StartTime
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

        public string TaxCode
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

        public DateTime UploadTime
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

        public string VerDesc
        {
            get
            {
                return this.string_6;
            }
            set
            {
                this.string_6 = value;
            }
        }

        public string Version
        {
            get
            {
                return this.string_5;
            }
            set
            {
                this.string_5 = value;
            }
        }
    }
}

