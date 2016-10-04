namespace Aisino.Framework.PubData.Message_S2C
{
    using System;

    [Serializable]
    public class CommonMessage : SCMessage
    {
        private string code;
        private string mess;

        public CommonMessage(string code)
        {
            this.code = code;
        }

        public string Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code = value;
            }
        }

        public string Mess
        {
            get
            {
                return this.mess;
            }
            set
            {
                this.mess = value;
            }
        }
    }
}

