namespace Aisino.Framework.PubData.Message_C2S
{
    using System;

    [Serializable]
    public class LogoutMessage : CSMessage
    {
        public LogoutMessage(string userName, string userID) : base(userName, userID)
        {
        }
    }
}

