namespace Aisino.Framework.PubData.DataType
{
    using System;

    [Serializable]
    public abstract class MessageBase
    {
        private string b1;
        private string b2;

        protected MessageBase()
        {
        }

        public string B1
        {
            get
            {
                return this.b1;
            }
            set
            {
                this.b1 = value;
            }
        }

        public string B2
        {
            get
            {
                return this.b2;
            }
            set
            {
                this.b2 = value;
            }
        }
    }
}

