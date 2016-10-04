namespace Aisino.Framework.Plugin.Core.MutiQuery
{
    using System;

    public class BaseCondition : Condition
    {
        private ArithOperator arithOperator_0;
        private Aisino.Framework.Plugin.Core.MutiQuery.DataField dataField_0;
        private string string_2;
        private string string_3;

        public BaseCondition()
        {
            
        }

        public Aisino.Framework.Plugin.Core.MutiQuery.DataField DataField
        {
            get
            {
                return this.dataField_0;
            }
            set
            {
                this.dataField_0 = value;
            }
        }

        public override string Expression
        {
            get
            {
                this.string_3 = "";
                string str = "";
                if (this.dataField_0 != null)
                {
                    if (this.dataField_0.Field == "FPZL")
                    {
                        if (this.string_2 == "s")
                        {
                            str = "'专用发票'";
                        }
                        else if (this.string_2 == "c")
                        {
                            str = "'普通发票'";
                        }
                        else if (this.string_2 == "p")
                        {
                            str = "'电子增值税普通发票'";
                        }
                        else if (this.string_2 == "q")
                        {
                            str = "'增值税普通发票(卷票)'";
                        }
                        else if (this.string_2 == "f")
                        {
                            str = "'货物运输业增值税专用发票'";
                        }
                        else if (this.string_2 == "j")
                        {
                            str = "'机动车销售统一发票'";
                        }
                        else
                        {
                            str = "'" + this.string_2 + "'";
                        }
                    }
                    else if (this.dataField_0.DataType == FieldType.String)
                    {
                        if (!this.string_2.StartsWith("'") && !this.string_2.EndsWith("'"))
                        {
                            str = "'" + this.string_2 + "'";
                        }
                    }
                    else if (this.dataField_0.DataType == FieldType.DateTime)
                    {
                        str = "'" + this.string_2 + "'";
                    }
                    else
                    {
                        str = this.string_2;
                    }
                    if (this.string_2 == "c")
                    {
                        string str2 = "'农产品收购发票'";
                        string str3 = "'农产品销售发票'";
                        this.string_3 = string.Format("({0} {1} {2} or {0} {1} {3} or  {0} {1} {4})", new object[] { this.dataField_0.Field, EnumHelper.GetDescription(this.arithOperator_0), str, str2, str3 });
                    }
                    else if (this.dataField_0.DataType == FieldType.DateTime)
                    {
                        DateTime time;
                        if (((this.arithOperator_0 == ArithOperator.等于) && DateTime.TryParse(this.string_2, out time)) && (time == time.Date))
                        {
                            this.string_3 = string.Format("({0} {1} '{2}' and {0} {3} '{4}')", new object[] { this.dataField_0.Field, EnumHelper.GetDescription(ArithOperator.大于等于), time.ToString("yyyy-MM-dd 00:00:00"), EnumHelper.GetDescription(ArithOperator.小于等于), time.ToString("yyyy-MM-dd 23:59:59") });
                        }
                    }
                    else
                    {
                        this.string_3 = string.Format("({0} {1} {2})", this.dataField_0.Field, EnumHelper.GetDescription(this.arithOperator_0), str);
                    }
                }
                return this.string_3;
            }
        }

        public ArithOperator Operator
        {
            get
            {
                return this.arithOperator_0;
            }
            set
            {
                this.arithOperator_0 = value;
            }
        }

        public string Value
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }
    }
}

