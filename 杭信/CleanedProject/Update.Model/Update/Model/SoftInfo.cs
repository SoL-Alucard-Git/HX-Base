namespace Update.Model
{
    using System;

    [Serializable]
    public class SoftInfo
    {
        private bool bool_0;
        private int int_0;
        private string string_0;
        private string string_1;

        public SoftInfo()
        {
            
        }

        public SoftInfo(int softId, string softName, string softDesc)
        {
            
            this.int_0 = softId;
            this.string_0 = softName;
            this.string_1 = softDesc;
        }

        public bool Restrict
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

        public string SoftDesc
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

        public int SoftId
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

        public string SoftName
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

