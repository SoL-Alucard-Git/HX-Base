namespace Update.Model
{
    using System;

    [Serializable]
    public class BserverInfo
    {
        private int int_0;
        private int int_1;
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;
        private string string_4;
        private string string_5;
        private string string_6;

        public BserverInfo()
        {
            
        }

        public BserverInfo(int bserverId, string districtCode, string name, string url, string contactName, string contactPhone, string contactMail, string bz, int vertify)
        {
            
            this.int_0 = bserverId;
            this.string_0 = districtCode;
            this.string_1 = name;
            this.string_2 = url;
            this.string_3 = contactName;
            this.string_4 = contactPhone;
            this.string_5 = contactMail;
            this.int_1 = vertify;
            this.string_6 = bz;
        }

        public int BserverId
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

        public string BZ
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

        public string ContactMail
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

        public string ContactName
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

        public string ContactPhone
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

        public string DistrictCode
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

        public string Name
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

        public string Url
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

        public int Vertify
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

