namespace Update.Model
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class AreaCodeAddressInfo
    {
        [CompilerGenerated]
        private string string_0;
        [CompilerGenerated]
        private string string_1;

        public AreaCodeAddressInfo()
        {
            
        }

        public string Address
        {
            [CompilerGenerated]
            get
            {
                return this.string_1;
            }
            [CompilerGenerated]
            set
            {
                this.string_1 = value;
            }
        }

        public string AreaCode
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
    }
}

