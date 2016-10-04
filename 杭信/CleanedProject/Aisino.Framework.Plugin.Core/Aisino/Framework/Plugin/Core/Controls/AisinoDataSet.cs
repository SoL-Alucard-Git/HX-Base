namespace Aisino.Framework.Plugin.Core.Controls
{
    using System;
    using System.Data;

    public class AisinoDataSet
    {
        private DataTable dataTable_0;
        private int int_0;
        private int int_1;
        private int int_2;
        private int int_3;

        public AisinoDataSet()
        {
            
            this.dataTable_0 = new DataTable();
        }

        public int AllPageNum
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

        public int AllRows
        {
            get
            {
                return this.int_3;
            }
            set
            {
                this.int_3 = value;
            }
        }

        public int CurrentPage
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

        public DataTable Data
        {
            get
            {
                return this.dataTable_0;
            }
            set
            {
                this.dataTable_0 = value;
            }
        }

        public int PageSize
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
    }
}

