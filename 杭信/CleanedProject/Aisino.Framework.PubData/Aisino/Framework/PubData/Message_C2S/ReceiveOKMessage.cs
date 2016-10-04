namespace Aisino.Framework.PubData.Message_C2S
{
    using System;

    [Serializable]
    public class ReceiveOKMessage : CSMessage
    {
        private string messID;

        public ReceiveOKMessage(string userName, string userID, string messID) : base(userName, userID)
        {
            this.messID = messID;
        }

        public string MessID
        {
            get
            {
                return this.messID;
            }
        }
    }
}

