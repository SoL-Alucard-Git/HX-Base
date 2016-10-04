namespace Aisino.Framework.PubData.DataType
{
    using System;
    using System.Net;

    [Serializable]
    public class User
    {
        private string bk1;
        private string bk2;
        private string maxMessID;
        private IPEndPoint netPoint;
        private string userID;
        private string userName;

        public User(string userID, string userName, IPEndPoint netPoint, string maxMessID)
        {
            this.userID = userID;
            this.userName = userName;
            this.netPoint = netPoint;
            this.maxMessID = maxMessID;
        }

        public string Bk1
        {
            get
            {
                return this.bk1;
            }
            set
            {
                this.bk1 = value;
            }
        }

        public string Bk2
        {
            get
            {
                return this.bk2;
            }
            set
            {
                this.bk2 = value;
            }
        }

        public string MaxMessID
        {
            get
            {
                return this.maxMessID;
            }
        }

        public IPEndPoint NetPoint
        {
            get
            {
                return this.netPoint;
            }
            set
            {
                this.netPoint = value;
            }
        }

        public string UserID
        {
            get
            {
                return this.userID;
            }
            set
            {
                this.userID = value;
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

