namespace Aisino.Framework.PubData.Message_C2S
{
    using Aisino.Framework.PubData.DataType;
    using System;

    [Serializable]
    public abstract class CSMessage : MessageBase
    {
        private string userID;
        private string userName;

        protected CSMessage(string anUserName, string anUserID)
        {
            this.userName = anUserName;
            this.userID = anUserID;
        }

        public string UserID
        {
            get
            {
                return this.userID;
            }
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }
        }
    }
}

