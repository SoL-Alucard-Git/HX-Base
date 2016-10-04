namespace Aisino.Fwkp.Bsgl
{
    using System;

    public class IdTextPair : IComparable<IdTextPair>
    {
        private int mId;
        private string mText;

        public IdTextPair(int id, string text)
        {
            this.mId = id;
            this.mText = text;
        }

        public int CompareTo(IdTextPair other)
        {
            if (other == null)
            {
                return 1;
            }
            return this.mId.CompareTo(other.Id);
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.mText))
            {
                return this.mId.ToString();
            }
            return this.mText;
        }

        public int Id
        {
            get
            {
                return this.mId;
            }
            set
            {
                this.mId = value;
            }
        }

        public string Text
        {
            get
            {
                return this.mText;
            }
            set
            {
                this.mText = value;
            }
        }
    }
}

