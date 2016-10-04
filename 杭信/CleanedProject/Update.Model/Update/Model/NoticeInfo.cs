namespace Update.Model
{
    using System;

    [Serializable]
    public class NoticeInfo
    {
        private DateTime dateTime_0;
        private int int_0;
        private int int_1;
        private int int_2;
        private string string_0;
        private string string_1;

        public NoticeInfo()
        {
            
        }

        public string NoticeContent
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

        public int NoticeId
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

        public string NoticeTitle
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

        public int NoticeType
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

        public int PID
        {
            get
            {
                return this.int_2;
            }
            set
            {
                this.int_2 = value;
            }
        }

        public DateTime PublishDate
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
    }
}

