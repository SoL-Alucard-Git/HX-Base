namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using System;
    using System.Runtime.CompilerServices;

    public class CellIndexInfo
    {
        [CompilerGenerated]
        private int int_0;
        [CompilerGenerated]
        private int int_1;
        [CompilerGenerated]
        private string string_0;

        internal CellIndexInfo(Cell cell_0)
        {
            
            this.ColumnIndex = cell_0.int_0;
            this.RowIndex = cell_0.row_0.int_0;
            this.method_0();
        }

        private void method_0()
        {
            this.ExcelColumnIndex = "";
            int num = (this.ColumnIndex / 0x1a) - 1;
            int num2 = this.ColumnIndex % 0x1a;
            if (num >= 0)
            {
                char ch = (char) (0x41 + num);
                this.ExcelColumnIndex = this.ExcelColumnIndex + ch;
            }
            char ch2 = (char) (0x41 + num2);
            this.ExcelColumnIndex = this.ExcelColumnIndex + ch2;
        }

        public int ColumnIndex
        {
            [CompilerGenerated]
            get
            {
                return this.int_1;
            }
            [CompilerGenerated]
            private set
            {
                this.int_1 = value;
            }
        }

        public string ExcelColumnIndex
        {
            [CompilerGenerated]
            get
            {
                return this.string_0;
            }
            [CompilerGenerated]
            private set
            {
                this.string_0 = value;
            }
        }

        public int RowIndex
        {
            [CompilerGenerated]
            get
            {
                return this.int_0;
            }
            [CompilerGenerated]
            private set
            {
                this.int_0 = value;
            }
        }
    }
}

