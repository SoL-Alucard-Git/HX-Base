namespace Aisino.FTaxBase
{
    using System;

    public class UnlockInvoice
    {
        public byte[] Buffer;
        private int int_0;
        private int int_1;
        private int int_2;
        private string string_0;
        private string string_1;

        public UnlockInvoice()
        {
            
            this.int_0 = -1;
            this.string_0 = string.Empty;
            this.string_1 = string.Empty;
            this.int_1 = -1;
        }

        public int Count
        {
            get
            {
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
            }
        }

        public int Kind
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

        public int Machine
        {
            get
            {
                return this.int_2;
            }
            set
            {
                this.int_2 = value;
            }
        }

        public string Number
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

        public string TypeCode
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }
    }
}

