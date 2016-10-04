namespace Aisino.Framework.Plugin.Core.Mail
{
    using System;
    using System.Collections.Generic;

    public class MailData
    {
        internal int int_0;
        internal int int_1;
        internal List<Dictionary<string, byte[]>> list_0;
        internal string string_0;
        internal string string_1;
        internal string string_2;
        internal string string_3;
        internal string string_4;
        internal string string_5;
        internal string string_6;

        public MailData()
        {
            
            this.list_0 = new List<Dictionary<string, byte[]>>();
        }

        public List<Dictionary<string, byte[]>> Attachment
        {
            get
            {
                return this.list_0;
            }
        }

        public string Body
        {
            get
            {
                return this.string_6;
            }
        }

        public string Cc
        {
            get
            {
                return this.string_2;
            }
        }

        public string Date
        {
            get
            {
                return this.string_4;
            }
        }

        public string From
        {
            get
            {
                return this.string_0;
            }
        }

        public int Id
        {
            get
            {
                return this.int_0;
            }
        }

        public string Message_id
        {
            get
            {
                return this.string_5;
            }
        }

        public int Message_size
        {
            get
            {
                return this.int_1;
            }
        }

        public string Subject
        {
            get
            {
                return this.string_3;
            }
        }

        public string To
        {
            get
            {
                return this.string_1;
            }
        }
    }
}

