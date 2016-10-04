namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using Aisino.Framework.Plugin.Core.ExcelXml.Extensions;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class Cell : Styles
    {
        internal bool bool_0;
        internal Aisino.Framework.Plugin.Core.ExcelXml.ContentType contentType_0;
        private Formula formula_0;
        internal int int_0;
        private int int_1;
        private int int_2;
        private object object_0;
        internal Row row_0;
        [CompilerGenerated]
        private string string_1;
        [CompilerGenerated]
        private string string_2;

        internal Cell(Row row_1, int int_3)
        {
            
            if (row_1 == null)
            {
                throw new ArgumentNullException("parent");
            }
            this.row_0 = row_1;
            this.contentType_0 = Aisino.Framework.Plugin.Core.ExcelXml.ContentType.None;
            this.int_0 = int_3;
            if (row_1.Style != null)
            {
                base.Style = row_1.Style;
            }
            else if (row_1.worksheet_0.Columns(this.int_0).Style != null)
            {
                base.Style = row_1.worksheet_0.Columns(this.int_0).Style;
            }
            else if (row_1.worksheet_0.Style != null)
            {
                base.Style = row_1.worksheet_0.Columns(this.int_0).Style;
            }
        }

        public void Delete()
        {
            this.row_0.DeleteCell(this);
        }

        public void Empty()
        {
            this.method_6(true);
        }

        public T GetValue<T>()
        {
            string fullName = typeof(T).FullName;
            if (fullName == "System.Object")
            {
                return (T) this.object_0;
            }
            if ((!typeof(T).IsPrimitive && (fullName != "System.DateTime")) && ((fullName != "System.String") && (fullName != "Aisino.Framwork.Plugin.Core.ExcelXml.Formula")))
            {
                throw new ArgumentException("T must be of a primitive or Formula type");
            }
            switch (this.contentType_0)
            {
                case Aisino.Framework.Plugin.Core.ExcelXml.ContentType.String:
                case Aisino.Framework.Plugin.Core.ExcelXml.ContentType.UnresolvedValue:
                    if (!(fullName == "System.String"))
                    {
                        return default(T);
                    }
                    return (T) Convert.ChangeType(this.object_0, typeof(T), CultureInfo.InvariantCulture);

                case Aisino.Framework.Plugin.Core.ExcelXml.ContentType.Number:
                    if (!ObjectExtensions.IsNumericType(typeof(T)))
                    {
                        return default(T);
                    }
                    return (T) Convert.ChangeType(this.object_0, typeof(T), CultureInfo.InvariantCulture);

                case Aisino.Framework.Plugin.Core.ExcelXml.ContentType.DateTime:
                    if (!(fullName == "System.DateTime"))
                    {
                        return default(T);
                    }
                    return (T) Convert.ChangeType(this.object_0, typeof(T), CultureInfo.InvariantCulture);

                case Aisino.Framework.Plugin.Core.ExcelXml.ContentType.Boolean:
                    if (!(fullName == "System.Boolean"))
                    {
                        return default(T);
                    }
                    return (T) Convert.ChangeType(this.object_0, typeof(T), CultureInfo.InvariantCulture);

                case Aisino.Framework.Plugin.Core.ExcelXml.ContentType.Formula:
                    if (!(fullName == "Aisino.Framwork.Plugin.Core.ExcelXml.Formula"))
                    {
                        return default(T);
                    }
                    return (T) this.object_0;
            }
            return default(T);
        }

        public bool IsEmpty()
        {
            return (((this.contentType_0 == Aisino.Framework.Plugin.Core.ExcelXml.ContentType.None) && this.Comment.IsNullOrEmpty()) && base.method_2());
        }

        [CompilerGenerated]
        private bool method_10(Range range_0)
        {
            return (range_0.cell_0 == this);
        }

        [CompilerGenerated]
        private bool method_11(Range range_0)
        {
            return (range_0.cell_0 == this);
        }

        internal void method_5()
        {
            if (this.contentType_0 == Aisino.Framework.Plugin.Core.ExcelXml.ContentType.Formula)
            {
                foreach (Parameter parameter in this.formula_0.Parameters)
                {
                    if (parameter.ParameterType == ParameterType.Range)
                    {
                        Range range = parameter.Value as Range;
                        if (range != null)
                        {
                            range.method_9(this);
                        }
                    }
                }
            }
        }

        internal void method_6(bool bool_1)
        {
            this.contentType_0 = Aisino.Framework.Plugin.Core.ExcelXml.ContentType.None;
            this.object_0 = null;
            this.formula_0 = null;
            if (!bool_1)
            {
                this.row_0 = null;
            }
        }

        internal void method_7(XmlWriter xmlWriter_0, bool bool_1)
        {
            Predicate<Range> match = null;
            if (!this.IsEmpty() && (this.bool_0 || !this.row_0.worksheet_0.method_15(this)))
            {
                xmlWriter_0.WriteStartElement("Cell");
                if ((!base.StyleID.IsNullOrEmpty() && (this.row_0.StyleID != base.StyleID)) && (base.StyleID != "Default"))
                {
                    xmlWriter_0.WriteAttributeString("ss", "StyleID", null, base.StyleID);
                }
                if (bool_1)
                {
                    xmlWriter_0.WriteAttributeString("ss", "Index", null, (this.int_0 + 1).ToString(CultureInfo.InvariantCulture));
                }
                if (!this.HRef.IsNullOrEmpty())
                {
                    xmlWriter_0.WriteAttributeString("ss", "HRef", null, this.HRef.XmlEncode());
                }
                if (this.bool_0)
                {
                    if (match == null)
                    {
                        match = new Predicate<Range>(this.method_11);
                    }
                    Range range = this.row_0.worksheet_0.list_2.Find(match);
                    if (range != null)
                    {
                        int num = range.ColumnCount - 1;
                        int num2 = range.RowCount - 1;
                        if (num > 0)
                        {
                            xmlWriter_0.WriteAttributeString("ss", "MergeAcross", null, num.ToString(CultureInfo.InvariantCulture));
                        }
                        if (num2 > 0)
                        {
                            xmlWriter_0.WriteAttributeString("ss", "MergeDown", null, num2.ToString(CultureInfo.InvariantCulture));
                        }
                    }
                }
                this.method_8(xmlWriter_0);
                this.method_9(xmlWriter_0);
                foreach (string str in this.vmethod_0().method_13(this))
                {
                    xmlWriter_0.WriteStartElement("NamedCell");
                    xmlWriter_0.WriteAttributeString("ss", "Name", null, str);
                    xmlWriter_0.WriteEndElement();
                }
                xmlWriter_0.WriteEndElement();
            }
        }

        private void method_8(XmlWriter xmlWriter_0)
        {
            if (this.contentType_0 == Aisino.Framework.Plugin.Core.ExcelXml.ContentType.Formula)
            {
                xmlWriter_0.WriteAttributeString("ss", "Formula", null, "=" + this.formula_0.method_0(this));
            }
            else if (this.contentType_0 == Aisino.Framework.Plugin.Core.ExcelXml.ContentType.UnresolvedValue)
            {
                xmlWriter_0.WriteAttributeString("ss", "Formula", null, (string) this.object_0);
            }
            else if (this.contentType_0 != Aisino.Framework.Plugin.Core.ExcelXml.ContentType.None)
            {
                xmlWriter_0.WriteStartElement("Data");
                xmlWriter_0.WriteAttributeString("ss", "Type", null, this.contentType_0.ToString());
                switch (this.contentType_0)
                {
                    case Aisino.Framework.Plugin.Core.ExcelXml.ContentType.String:
                        xmlWriter_0.WriteValue((string) this.object_0);
                        break;

                    case Aisino.Framework.Plugin.Core.ExcelXml.ContentType.Number:
                        xmlWriter_0.WriteValue(Convert.ToDecimal(this.object_0, CultureInfo.InvariantCulture).ToString(new CultureInfo("en-US")));
                        break;

                    case Aisino.Framework.Plugin.Core.ExcelXml.ContentType.DateTime:
                        xmlWriter_0.WriteValue(((DateTime) this.object_0).ToString(@"yyyy-MM-dd\Thh:mm:ss.fff", CultureInfo.InvariantCulture));
                        break;

                    case Aisino.Framework.Plugin.Core.ExcelXml.ContentType.Boolean:
                        if (!((bool) this.object_0))
                        {
                            xmlWriter_0.WriteValue("0");
                            break;
                        }
                        xmlWriter_0.WriteValue("1");
                        break;
                }
                xmlWriter_0.WriteEndElement();
            }
        }

        private void method_9(XmlWriter xmlWriter_0)
        {
            if (!this.Comment.IsNullOrEmpty())
            {
                string author = this.vmethod_0().Properties.Author;
                xmlWriter_0.WriteStartElement("Comment");
                if (!author.IsNullOrEmpty())
                {
                    xmlWriter_0.WriteAttributeString("ss", "Author", null, author);
                }
                xmlWriter_0.WriteStartElement("ss", "Data", null);
                xmlWriter_0.WriteAttributeString("xmlns", "http://www.w3.org/TR/REC-html40");
                xmlWriter_0.WriteRaw(this.Comment);
                xmlWriter_0.WriteEndElement();
                xmlWriter_0.WriteEndElement();
            }
        }

        public void Unmerge()
        {
            if (this.bool_0)
            {
                this.row_0.worksheet_0.list_2.RemoveAll(new Predicate<Range>(this.method_10));
                this.bool_0 = false;
            }
        }

        internal override ExcelXmlWorkbook vmethod_0()
        {
            return this.row_0.worksheet_0.excelXmlWorkbook_0;
        }

        internal override void vmethod_1(Styles.Delegate36 delegate36_0)
        {
        }

        internal override Cell vmethod_2()
        {
            return null;
        }

        public int ColumnSpan
        {
            get
            {
                if (this.bool_0)
                {
                    return this.int_1;
                }
                return 1;
            }
            internal set
            {
                this.int_1 = value;
            }
        }

        public string Comment
        {
            [CompilerGenerated]
            get
            {
                return this.string_1;
            }
            [CompilerGenerated]
            set
            {
                this.string_1 = value;
            }
        }

        public Aisino.Framework.Plugin.Core.ExcelXml.ContentType ContentType
        {
            get
            {
                return this.contentType_0;
            }
        }

        public string HRef
        {
            [CompilerGenerated]
            get
            {
                return this.string_2;
            }
            [CompilerGenerated]
            set
            {
                this.string_2 = value;
            }
        }

        public CellIndexInfo Index
        {
            get
            {
                return new CellIndexInfo(this);
            }
        }

        public int RowSpan
        {
            get
            {
                if (this.bool_0)
                {
                    return this.int_2;
                }
                return 1;
            }
            internal set
            {
                this.int_2 = value;
            }
        }

        public object Value
        {
            get
            {
                return this.object_0;
            }
            set
            {
                switch (value.GetType().FullName)
                {
                    case "System.DateTime":
                        this.object_0 = value;
                        this.contentType_0 = Aisino.Framework.Plugin.Core.ExcelXml.ContentType.DateTime;
                        return;

                    case "System.Byte":
                    case "System.SByte":
                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                    case "System.UInt16":
                    case "System.UInt32":
                    case "System.UInt64":
                    case "System.Single":
                    case "System.Double":
                    case "System.Decimal":
                        this.object_0 = value;
                        this.contentType_0 = Aisino.Framework.Plugin.Core.ExcelXml.ContentType.Number;
                        return;

                    case "System.Boolean":
                        this.object_0 = value;
                        this.contentType_0 = Aisino.Framework.Plugin.Core.ExcelXml.ContentType.Boolean;
                        return;

                    case "System.String":
                        this.object_0 = value;
                        this.contentType_0 = Aisino.Framework.Plugin.Core.ExcelXml.ContentType.String;
                        return;

                    case "Aisino.Framwork.Plugin.Core.ExcelXml.Cell":
                    {
                        Cell cell = value as Cell;
                        if (cell == null)
                        {
                            this.formula_0 = null;
                            this.object_0 = null;
                            this.contentType_0 = Aisino.Framework.Plugin.Core.ExcelXml.ContentType.None;
                            return;
                        }
                        if (this.formula_0 != null)
                        {
                            this.formula_0 = null;
                        }
                        this.formula_0 = new Formula();
                        this.object_0 = null;
                        this.formula_0.Add(new Range(cell));
                        this.contentType_0 = Aisino.Framework.Plugin.Core.ExcelXml.ContentType.Formula;
                        return;
                    }
                    case "Aisino.Framwork.Plugin.Core.ExcelXml.Formula":
                    {
                        Formula formula = value as Formula;
                        if (formula == null)
                        {
                            this.formula_0 = null;
                            this.object_0 = null;
                            this.contentType_0 = Aisino.Framework.Plugin.Core.ExcelXml.ContentType.None;
                            return;
                        }
                        this.formula_0 = formula;
                        this.object_0 = null;
                        this.contentType_0 = Aisino.Framework.Plugin.Core.ExcelXml.ContentType.Formula;
                        return;
                    }
                }
                throw new NotImplementedException();
            }
        }
    }
}

