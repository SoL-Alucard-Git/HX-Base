namespace Aisino.Framework.Plugin.Core
{
    using System;

    public class BaseException : ApplicationException
    {
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;
        private string string_4;

        public BaseException() : base(string.Empty)
        {
            
            this.string_0 = DateTime.Now.ToString();
            this.string_1 = null;
            this.string_2 = null;
            this.string_3 = null;
            this.string_4 = null;
        }

        public BaseException(string string_5, string string_6, string string_7, Exception exception_0) : base(string.Empty, exception_0)
        {
            
            if ((this.string_1 == null) && (string_5 != null))
            {
                this.string_1 = string_5;
            }
            this.string_2 = this.method_0();
            this.string_0 = DateTime.Now.ToString();
            this.string_3 = string_6;
            this.string_4 = string_7;
        }

        private string method_0()
        {
            string str = string.Empty;
            string str2 = this.string_1;
            if (str2 != null)
            {
                bool flag1 = str2 == "123";
            }
            return str;
        }

        public override string ToString()
        {
            return (this.string_1 + " " + this.string_2 + " " + this.string_4);
        }

        public string ExceptionID
        {
            get
            {
                return this.string_1;
            }
        }

        public string ExceptionInfo
        {
            get
            {
                return this.string_2;
            }
        }

        public string GenerateDateTime
        {
            get
            {
                return this.string_0;
            }
        }

        public string MethodName
        {
            get
            {
                return this.string_3;
            }
        }

        public string UserMessage
        {
            get
            {
                return this.string_4;
            }
        }
    }
}

