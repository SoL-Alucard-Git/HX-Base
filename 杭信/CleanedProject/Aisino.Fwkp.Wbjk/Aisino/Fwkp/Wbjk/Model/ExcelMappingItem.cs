namespace Aisino.Fwkp.Wbjk.Model
{
    using System;

    public class ExcelMappingItem
    {
        public class Relation
        {
            private int column = 0;
            private string key = string.Empty;
            private int table = 0;

            public int ColumnName
            {
                get
                {
                    return this.column;
                }
                set
                {
                    this.column = value;
                }
            }

            public string Key
            {
                get
                {
                    return this.key;
                }
                set
                {
                    this.key = value;
                }
            }

            public int TableFlag
            {
                get
                {
                    return this.table;
                }
                set
                {
                    this.table = value;
                }
            }
        }
    }
}

