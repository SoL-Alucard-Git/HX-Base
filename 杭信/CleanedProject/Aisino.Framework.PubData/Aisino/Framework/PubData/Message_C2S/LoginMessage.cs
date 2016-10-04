namespace Aisino.Framework.PubData.Message_C2S
{
    using System;

    [Serializable]
    public class LoginMessage : CSMessage
    {
        private string maxMessID;

        public LoginMessage(string userName, string userID, string maxMessID) : base(userName, userID)
        {
            this.maxMessID = maxMessID;
        }

        public string MaxMessID
        {
            get
            {
                return this.maxMessID;
            }
        }
    }
}

