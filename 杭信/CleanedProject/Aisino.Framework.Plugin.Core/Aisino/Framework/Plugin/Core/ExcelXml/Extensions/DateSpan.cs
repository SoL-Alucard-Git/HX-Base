namespace Aisino.Framework.Plugin.Core.ExcelXml.Extensions
{
    using System;
    using System.Runtime.CompilerServices;

    public class DateSpan
    {
        [CompilerGenerated]
        private int int_0;
        [CompilerGenerated]
        private int int_1;
        [CompilerGenerated]
        private int int_2;

        public DateSpan()
        {
            
        }

        public int Days
        {
            [CompilerGenerated]
            get
            {
                return this.int_0;
            }
            [CompilerGenerated]
            set
            {
                this.int_0 = value;
            }
        }

        public int Months
        {
            [CompilerGenerated]
            get
            {
                return this.int_1;
            }
            [CompilerGenerated]
            set
            {
                this.int_1 = value;
            }
        }

        public int Years
        {
            [CompilerGenerated]
            get
            {
                return this.int_2;
            }
            [CompilerGenerated]
            set
            {
                this.int_2 = value;
            }
        }
    }
}

