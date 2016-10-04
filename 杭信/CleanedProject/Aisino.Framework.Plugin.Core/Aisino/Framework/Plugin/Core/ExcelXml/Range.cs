namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using Aisino.Framework.Plugin.Core.ExcelXml.Extensions;
    using ns14;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    public class Range : Styles, IEnumerable<Cell>, IEnumerable
    {
        [CompilerGenerated]
        private bool bool_0;
        internal Cell cell_0;
        internal Cell cell_1;
        internal string string_1;
        private string string_2;

        public Range(Cell cell_2)
        {
            
            this.cell_0 = cell_2;
            this.string_1 = "";
        }

        internal Range(string string_3)
        {
            
            if (string_3[0] == '=')
            {
                string_3 = string_3.Substring(1);
            }
            this.string_1 = string_3;
        }

        public Range(Cell cell_2, Cell cell_3)
        {
            
            this.string_1 = "";
            if (cell_3 == null)
            {
                this.cell_0 = cell_2;
            }
            else
            {
                if (cell_2.row_0.worksheet_0 != cell_3.row_0.worksheet_0)
                {
                    throw new ArgumentException("cellFrom and cellTo's parent worksheets should be same");
                }
                if (cell_2 == cell_3)
                {
                    this.cell_0 = cell_2;
                }
                else
                {
                    int num = cell_2.row_0.int_0;
                    int num2 = cell_3.row_0.int_0;
                    int num3 = cell_2.int_0;
                    int num4 = cell_3.int_0;
                    if ((num <= num2) && (num3 <= num4))
                    {
                        this.cell_0 = cell_2;
                        this.cell_1 = cell_3;
                    }
                    else
                    {
                        this.cell_0 = cell_3;
                        this.cell_1 = cell_2;
                    }
                }
            }
        }

        public void AutoFilter()
        {
            this.cell_0.row_0.worksheet_0.bool_0 = true;
            this.cell_0.vmethod_0().method_10(this, "_FilterDatabase", this.cell_0.row_0.worksheet_0);
        }

        public bool Contains(Cell cell_2)
        {
            if (this.cell_0 == null)
            {
                return false;
            }
            if (this.cell_0.row_0.worksheet_0 != cell_2.row_0.worksheet_0)
            {
                return false;
            }
            if (this.cell_1 == null)
            {
                return (this.cell_0 == cell_2);
            }
            int num4 = this.cell_0.row_0.int_0;
            int num3 = this.cell_1.row_0.int_0;
            int num = this.cell_0.int_0;
            int num2 = this.cell_1.int_0;
            return ((((cell_2.row_0.int_0 >= num4) && (cell_2.row_0.int_0 <= num3)) && (cell_2.int_0 >= num)) && (cell_2.int_0 <= num2));
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            if (this.cell_0 != null)
            {
                if (this.cell_1 != null)
                {
                    int iteratorVariable0 = this.cell_0.row_0.int_0;
                    int iteratorVariable1 = this.cell_1.row_0.int_0;
                    int iteratorVariable2 = this.cell_0.int_0;
                    int iteratorVariable3 = this.cell_1.int_0;
                    Worksheet iteratorVariable4 = this.cell_0.row_0.worksheet_0;
                    for (int i = iteratorVariable0; i <= iteratorVariable1; i++)
                    {
                        for (int j = iteratorVariable2; j <= iteratorVariable3; j++)
                        {
                            yield return iteratorVariable4[j, i];
                        }
                    }
                }
                else
                {
                    yield return this.cell_0;
                }
            }
        }

        public bool Merge()
        {
            bool rangeHasMergedCells;
            if (!this.cell_0.bool_0)
            {
                rangeHasMergedCells = false;
                this.vmethod_1(delegate (Cell cell) {
                    rangeHasMergedCells = cell.bool_0;
                });
                if (rangeHasMergedCells)
                {
                    return false;
                }
                this.cell_0.row_0.worksheet_0.list_2.Add(this);
                this.cell_0.bool_0 = true;
                this.cell_0.ColumnSpan = this.ColumnCount;
                this.cell_0.RowSpan = this.RowCount;
            }
            return true;
        }

        private string method_5()
        {
            string str = string.Format(CultureInfo.InvariantCulture, "R{0}C{1}", new object[] { this.cell_0.row_0.int_0 + 1, this.cell_0.int_0 + 1 });
            if (this.cell_0 != null)
            {
                str = str + string.Format(CultureInfo.InvariantCulture, ":R{0}C{1}", new object[] { this.cell_1.row_0.int_0 + 1, this.cell_1.int_0 + 1 });
            }
            return str;
        }

        internal bool method_6(Aisino.Framework.Plugin.Core.ExcelXml.Range range_0)
        {
            return ((range_0.cell_0 == this.cell_0) && (range_0.cell_1 == this.cell_1));
        }

        internal string method_7(bool bool_1)
        {
            if (this.cell_0 == null)
            {
                return this.string_1;
            }
            string str = "";
            if (bool_1)
            {
                str = "'" + this.cell_0.row_0.worksheet_0.Name + "'!";
            }
            return (str + this.method_5());
        }

        internal string method_8(Cell cell_2)
        {
            string str2;
            if (this.cell_0 == null)
            {
                return this.string_1;
            }
            if (this.cell_0.row_0 == null)
            {
                return "#N/A";
            }
            if ((this.cell_1 != null) && (this.cell_1.row_0 == null))
            {
                return "#N/A";
            }
            if (cell_2 == null)
            {
                throw new ArgumentNullException("cell");
            }
            if (this.Absolute)
            {
                str2 = this.method_5();
            }
            else if (this.cell_1 != null)
            {
                str2 = string.Format(CultureInfo.InvariantCulture, "R[{0}]C[{1}]:R[{2}]C[{3}]", new object[] { this.cell_0.row_0.int_0 - cell_2.row_0.int_0, this.cell_0.int_0 - cell_2.int_0, this.cell_1.row_0.int_0 - cell_2.row_0.int_0, this.cell_1.int_0 - cell_2.int_0 });
            }
            else
            {
                str2 = string.Format(CultureInfo.InvariantCulture, "R[{0}]C[{1}]", new object[] { this.cell_0.row_0.int_0 - cell_2.row_0.int_0, this.cell_0.int_0 - cell_2.int_0 });
            }
            string name = "";
            if (this.cell_0.row_0.worksheet_0 != cell_2.row_0.worksheet_0)
            {
                name = this.cell_0.row_0.worksheet_0.Name;
                if (this.cell_0.vmethod_0() != cell_2.vmethod_0())
                {
                    throw new ArgumentException("External workbook references are not supported");
                }
            }
            if (!name.IsNullOrEmpty())
            {
                str2 = "'" + name + "'!" + str2;
            }
            return str2;
        }

        internal void method_9(Cell cell_2)
        {
            if (!this.string_1.IsNullOrEmpty())
            {
                Match match;
                Aisino.Framework.Plugin.Core.ExcelXml.Range range;
                Enum16 enum2 = Class129.smethod_2(this.string_1, out match);
                if (cell_2 == null)
                {
                    throw new ArgumentNullException("cell");
                }
                if (Class129.smethod_4(cell_2, match, out range, enum2 == ((Enum16) 3)))
                {
                    this.string_1 = "";
                    this.cell_0 = range.cell_0;
                    this.cell_1 = range.cell_1;
                }
            }
        }

        public void SetAsPrintArea()
        {
            this.cell_0.row_0.worksheet_0.bool_1 = true;
            this.cell_0.vmethod_0().method_10(this, "Print_Area", this.cell_0.row_0.worksheet_0);
        }

        internal static bool smethod_2(string string_3)
        {
            if ((!(string_3 == "Print_Titles") && !(string_3 == "_FilterDatabase")) && !(string_3 == "Print_Area"))
            {
                return false;
            }
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Unmerge()
        {
            if (this.cell_0.bool_0)
            {
                this.cell_0.row_0.worksheet_0.list_2.Remove(this);
                this.cell_0.bool_0 = false;
                this.cell_0.ColumnSpan = 1;
                this.cell_0.RowSpan = 1;
            }
        }

        internal override ExcelXmlWorkbook vmethod_0()
        {
            return null;
        }

        internal override void vmethod_1(Styles.Delegate36 delegate36_0)
        {
            if (this.cell_0 != null)
            {
                if (this.cell_1 == null)
                {
                    delegate36_0(this.cell_0);
                }
                else
                {
                    int num6 = this.cell_0.row_0.int_0;
                    int num4 = this.cell_1.row_0.int_0;
                    int num5 = this.cell_0.int_0;
                    int num2 = this.cell_1.int_0;
                    Worksheet worksheet = this.cell_0.row_0.worksheet_0;
                    for (int i = num6; i <= num4; i++)
                    {
                        for (int j = num5; j <= num2; j++)
                        {
                            delegate36_0(worksheet[j, i]);
                        }
                    }
                }
            }
        }

        internal override Cell vmethod_2()
        {
            return this.cell_0;
        }

        public bool Absolute
        {
            [CompilerGenerated]
            get
            {
                return this.bool_0;
            }
            [CompilerGenerated]
            set
            {
                this.bool_0 = value;
            }
        }

        public int ColumnCount
        {
            get
            {
                if (this.cell_0 == null)
                {
                    return 0;
                }
                if (this.cell_1 == null)
                {
                    return 1;
                }
                int num = this.cell_0.int_0;
                return ((this.cell_1.int_0 - num) + 1);
            }
        }

        public string Name
        {
            get
            {
                return this.string_2;
            }
            set
            {
                if (this.string_2 != value)
                {
                    if (value.IsNullOrEmpty() || smethod_2(value))
                    {
                        throw new ArgumentException("name");
                    }
                    this.string_2 = value;
                    this.cell_0.vmethod_0().AddNamedRange(this, this.string_2);
                }
            }
        }

        public int RowCount
        {
            get
            {
                if (this.cell_0 == null)
                {
                    return 0;
                }
                if (this.cell_1 == null)
                {
                    return 1;
                }
                int num = this.cell_0.row_0.int_0;
                return ((this.cell_1.row_0.int_0 - num) + 1);
            }
        }

    }
}

