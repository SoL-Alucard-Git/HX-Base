namespace Aisino.Framework.Plugin.Core.Registry
{
    using System;

    public class RegFileInfo
    {
        private DateTime dateTime_0;
        private qwe qwe_0;
        private string string_0;
        private string string_1;

        public RegFileInfo(string string_2, qwe qwe_1, DateTime dateTime_1)
        {
            
            this.string_0 = string_2;
            this.qwe_0 = qwe_1;
            this.dateTime_0 = dateTime_1;
        }

        public bool CheckedOk
        {
            get
            {
                return this.string_1.Equals("0000");
            }
        }

        public string ErrCode
        {
            get
            {
                return this.string_1;
            }
            internal set
            {
                this.string_1 = value;
            }
        }

        public qwe FileContent
        {
            get
            {
                return this.qwe_0;
            }
        }

        public DateTime FileModifyDate
        {
            get
            {
                return this.dateTime_0;
            }
        }

        public string FileName
        {
            get
            {
                return this.string_0;
            }
        }

        public string SoftFlag
        {
            get
            {
                if (this.string_1.Equals("910101"))
                {
                    return null;
                }
                string str = new string(this.qwe_0.SoftwareID);
                return (str.Substring(0, 2) + str.Substring(4, 2));
            }
        }

        public string VerFlag
        {
            get
            {
                if (this.string_1.Equals("910101"))
                {
                    return null;
                }
                return new string(this.qwe_0.SoftwareID).Substring(0, 2);
            }
        }
    }
}

