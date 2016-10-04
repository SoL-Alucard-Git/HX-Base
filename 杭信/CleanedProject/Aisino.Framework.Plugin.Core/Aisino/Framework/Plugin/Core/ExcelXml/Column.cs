namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using Aisino.Framework.Plugin.Core.ExcelXml.Extensions;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class Column
    {
        [CompilerGenerated]
        private bool bool_0;
        [CompilerGenerated]
        private double double_0;
        private ExcelXmlWorkbook excelXmlWorkbook_0;
        private string string_0;

        internal Column(Worksheet worksheet_0)
        {
            
            if (worksheet_0 == null)
            {
                throw new ArgumentNullException("parent");
            }
            this.excelXmlWorkbook_0 = worksheet_0.excelXmlWorkbook_0;
        }

        internal void method_0(XmlWriter xmlWriter_0)
        {
            xmlWriter_0.WriteStartElement("Column");
            if (this.Width > 0.0)
            {
                xmlWriter_0.WriteAttributeString("ss", "Width", null, this.Width.ToString(CultureInfo.InvariantCulture));
            }
            if (this.Hidden)
            {
                xmlWriter_0.WriteAttributeString("ss", "Hidden", null, "1");
                xmlWriter_0.WriteAttributeString("ss", "AutoFitWidth", null, "0");
            }
            if (!this.Style.ID.IsNullOrEmpty() && (this.Style.ID != "Default"))
            {
                xmlWriter_0.WriteAttributeString("ss", "StyleID", null, this.Style.ID);
            }
            xmlWriter_0.WriteEndElement();
        }

        public bool Hidden
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

        public XmlStyle Style
        {
            get
            {
                return this.excelXmlWorkbook_0.method_6(this.string_0);
            }
            set
            {
                this.string_0 = this.excelXmlWorkbook_0.method_8(value);
            }
        }

        public double Width
        {
            [CompilerGenerated]
            get
            {
                return this.double_0;
            }
            [CompilerGenerated]
            set
            {
                this.double_0 = value;
            }
        }
    }
}

