namespace Aisino.Framework.Plugin.Core.Util
{
    using System;

    public class ProgressData
    {
        private int int_0;
        private int int_1;
        private string string_0;

        public ProgressData(int int_2, int int_3)
        {
            
            this.int_0 = int_2;
            this.int_1 = int_3;
            this.string_0 = ((100 * this.int_0) / this.int_1) + "%";
        }

        public int Current
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

        public string TipText
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

        public int Total
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
    }
}

