namespace Aisino.Framework.PubData.Message_S2C
{
    using System;
    using System.Net;

    [Serializable]
    public class LoginOKMessage : CommonMessage
    {
        private IPEndPoint endPoint;

        public LoginOKMessage(string code, IPEndPoint endPoint) : base(code)
        {
            this.endPoint = endPoint;
        }

        public IPEndPoint EndPoint
        {
            get
            {
                return this.endPoint;
            }
        }
    }
}

