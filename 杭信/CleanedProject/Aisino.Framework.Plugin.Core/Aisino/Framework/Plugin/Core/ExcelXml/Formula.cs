namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class Formula
    {
        [CompilerGenerated]
        private bool bool_0;
        internal List<Parameter> list_0;

        public Formula()
        {
            
            this.list_0 = new List<Parameter>();
        }

        public Formula Add(Cell cell_0)
        {
            Parameter item = new Parameter(new Range(cell_0));
            this.Parameters.Add(item);
            return this;
        }

        public Formula Add(Formula formula_0)
        {
            Parameter item = new Parameter(formula_0);
            this.Parameters.Add(item);
            return this;
        }

        public Formula Add(Range range_0)
        {
            Parameter item = new Parameter(range_0);
            this.Parameters.Add(item);
            return this;
        }

        public Formula Add(char char_0)
        {
            Parameter item = new Parameter(char_0);
            this.Parameters.Add(item);
            return this;
        }

        public Formula Add(string string_0)
        {
            Parameter item = new Parameter(string_0);
            this.Parameters.Add(item);
            return this;
        }

        public Formula Add(Range range_0, Predicate<Cell> cellCompare)
        {
            if (range_0.cell_0 != null)
            {
                if (range_0.cell_1 == null)
                {
                    if (cellCompare(range_0.cell_0))
                    {
                        this.Add(range_0);
                    }
                    return this;
                }
                Worksheet worksheet = range_0.cell_0.row_0.worksheet_0;
                int num = range_0.cell_0.row_0.int_0;
                int num2 = range_0.cell_1.row_0.int_0;
                int num3 = range_0.cell_0.int_0;
                int num4 = range_0.cell_1.int_0;
                for (int i = num3; i <= num4; i++)
                {
                    int num6 = num;
                    while (!cellCompare(worksheet[i, num6]))
                    {
                        int num7;
                        num6++;
                    Label_0086:
                        if (num6 <= num2)
                        {
                            continue;
                        }
                        continue;
                    Label_008D:
                        num7 = num6 + 1;
                        while (num7 <= num2)
                        {
                            if (!cellCompare(worksheet[i, num7]))
                            {
                                goto Label_00B4;
                            }
                            num7++;
                        }
                        goto Label_0086;
                    Label_00B4:
                        this.Add(new Range(worksheet[i, num6], worksheet[i, num7 - 1]));
                        num6 = num7;
                        goto Label_0086;
                    }
                }
            }
            return this;
        }

        public Formula EmptyGroup()
        {
            Parameter item = new Parameter("()");
            this.Parameters.Add(item);
            return this;
        }

        public Formula EndGroup()
        {
            Parameter item = new Parameter(')');
            this.Parameters.Add(item);
            return this;
        }

        internal string method_0(Cell cell_0)
        {
            StringBuilder builder = new StringBuilder();
            foreach (Parameter parameter in this.Parameters)
            {
                switch (parameter.ParameterType)
                {
                    case ParameterType.String:
                        builder.Append(parameter.Value.ToString());
                        break;

                    case ParameterType.Range:
                    {
                        Range range = parameter.Value as Range;
                        if (range != null)
                        {
                            builder.Append(range.method_8(cell_0));
                        }
                        break;
                    }
                    case ParameterType.Formula:
                    {
                        Formula formula = parameter.Value as Formula;
                        if (formula != null)
                        {
                            builder.Append(formula.method_0(cell_0));
                        }
                        break;
                    }
                }
            }
            if (!this.MustHaveParameters || ((this.Parameters.Count != 0) && (builder.Length != 0)))
            {
                return builder.ToString();
            }
            return "";
        }

        public Formula Operator(char char_0)
        {
            Parameter item = new Parameter(char_0);
            this.Parameters.Add(item);
            return this;
        }

        public Formula StartGroup()
        {
            Parameter item = new Parameter('(');
            this.Parameters.Add(item);
            return this;
        }

        public bool MustHaveParameters
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

        public IList<Parameter> Parameters
        {
            get
            {
                return this.list_0;
            }
        }
    }
}

