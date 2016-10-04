namespace Aisino.Framework.PubData.Message_S2C
{
    using System;

    [Serializable]
    public class HtmlMessage : SCMessage
    {
        private string bk1;
        private string bk2;
        private string bk3;
        private string bk4;
        private string id;
        private string keyWords;
        private string message;
        private int tipShowSec;
        private string title;
        private string type;

        public HtmlMessage(string id, string title, string msg, string type, int tipShowSec)
        {
            this.title = title;
            this.message = msg;
            this.id = id;
            this.type = type;
            this.tipShowSec = tipShowSec;
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

        public string Bk3
        {
            get
            {
                return this.bk3;
            }
            set
            {
                this.bk3 = value;
            }
        }

        public string Bk4
        {
            get
            {
                return this.bk4;
            }
            set
            {
                this.bk4 = value;
            }
        }

        public string Id
        {
            get
            {
                return this.id;
            }
        }

        public string KeyWords
        {
            get
            {
                return this.keyWords;
            }
            set
            {
                this.keyWords = value;
            }
        }

        public string Message
        {
            get
            {
                return this.message;
            }
        }

        public int TipShowSec
        {
            get
            {
                return this.tipShowSec;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
        }

        public string Type
        {
            get
            {
                return this.type;
            }
        }
    }
}

