namespace BSDC
{
    using System;

    public class IdTextPair : IComparable<IdTextPair>
    {
        private int int_0;
        private string string_0;

        public IdTextPair(int int_1, string string_1)
        {
            
            this.int_0 = int_1;
            this.string_0 = string_1;
        }

        public int CompareTo(IdTextPair row)
        {
            if (row == null)
            {
                return 1;
            }
            return this.int_0.CompareTo(row.Id);
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.string_0))
            {
                return this.int_0.ToString();
            }
            return this.string_0;
        }

        public int Id
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        public string Text
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

