namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using Aisino.Framework.Plugin.Core.ExcelXml.Extensions;
    using System;
    using System.Globalization;
    using System.Text;
    using System.Xml;

    public class PrintOptions
    {
        internal bool bool_0;
        internal bool bool_1;
        internal double double_0;
        internal double double_1;
        internal double double_2;
        internal double double_3;
        internal double double_4;
        internal double double_5;
        internal int int_0;
        internal int int_1;
        internal int int_2;
        internal int int_3;
        internal int int_4;
        internal int int_5;
        internal int int_6;
        private PageLayout pageLayout_0;
        private PageOrientation pageOrientation_0;

        public PrintOptions()
        {
            
        }

        internal string method_0(string string_0)
        {
            StringBuilder builder = new StringBuilder();
            if (this.bool_1)
            {
                if (this.int_5 != 0)
                {
                    builder.AppendFormat("'{0}'!C{1}", string_0, this.int_5);
                    if (this.int_6 != this.int_5)
                    {
                        builder.AppendFormat(":C{0}", this.int_6);
                    }
                }
                if (this.int_3 != 0)
                {
                    if (this.int_5 != 0)
                    {
                        builder.Append(',');
                    }
                    builder.AppendFormat("'{0}'!R{1}", string_0, this.int_3);
                    if (this.int_4 != this.int_3)
                    {
                        builder.AppendFormat(":R{0}", this.int_4);
                    }
                }
            }
            return builder.ToString();
        }

        internal void method_1(XmlWriter xmlWriter_0)
        {
            xmlWriter_0.WriteStartElement("PageSetup");
            if (this.Orientation != PageOrientation.None)
            {
                xmlWriter_0.WriteStartElement("Layout");
                xmlWriter_0.WriteAttributeString("", "Orientation", null, this.Orientation.ToString());
                xmlWriter_0.WriteEndElement();
            }
            xmlWriter_0.WriteStartElement("Header");
            xmlWriter_0.WriteAttributeString("", "Margin", null, this.double_4.ToString(CultureInfo.InvariantCulture));
            xmlWriter_0.WriteEndElement();
            xmlWriter_0.WriteStartElement("Footer");
            xmlWriter_0.WriteAttributeString("", "Margin", null, this.double_5.ToString(CultureInfo.InvariantCulture));
            xmlWriter_0.WriteEndElement();
            xmlWriter_0.WriteStartElement("PageMargins");
            xmlWriter_0.WriteAttributeString("", "Bottom", null, this.double_3.ToString(CultureInfo.InvariantCulture));
            xmlWriter_0.WriteAttributeString("", "Left", null, this.double_0.ToString(CultureInfo.InvariantCulture));
            xmlWriter_0.WriteAttributeString("", "Right", null, this.double_1.ToString(CultureInfo.InvariantCulture));
            xmlWriter_0.WriteAttributeString("", "Top", null, this.double_2.ToString(CultureInfo.InvariantCulture));
            xmlWriter_0.WriteEndElement();
            xmlWriter_0.WriteEndElement();
            if (this.bool_0)
            {
                xmlWriter_0.WriteStartElement("FitToPage");
                xmlWriter_0.WriteEndElement();
            }
            xmlWriter_0.WriteStartElement("Print");
            xmlWriter_0.WriteElementString("ValidPrinterInfo", "");
            xmlWriter_0.WriteElementString("FitHeight", this.int_1.ToString(CultureInfo.InvariantCulture));
            xmlWriter_0.WriteElementString("FitWidth", this.int_2.ToString(CultureInfo.InvariantCulture));
            xmlWriter_0.WriteElementString("Scale", this.int_0.ToString(CultureInfo.InvariantCulture));
            xmlWriter_0.WriteEndElement();
        }

        public void ResetHeaders()
        {
            this.int_3 = 0;
            this.int_4 = 0;
            this.int_5 = 0;
            this.int_6 = 0;
        }

        public void ResetMargins()
        {
            this.double_0 = 0.7;
            this.double_1 = 0.7;
            this.double_2 = 0.75;
            this.double_3 = 0.75;
            this.double_4 = 0.3;
            this.double_5 = 0.3;
        }

        public void SetFitToPage(int int_7, int int_8)
        {
            this.int_2 = int_7;
            this.int_1 = int_8;
            this.bool_0 = true;
        }

        public void SetHeaderFooterMargins(double double_6, double double_7)
        {
            this.double_4 = Math.Max(0.0, double_6);
            this.double_5 = Math.Max(0.0, double_7);
        }

        public void SetMargins(double double_6, double double_7, double double_8, double double_9)
        {
            this.double_0 = Math.Max(0.0, double_6);
            this.double_2 = Math.Max(0.0, double_7);
            this.double_1 = Math.Max(0.0, double_8);
            this.double_3 = Math.Max(0.0, double_9);
        }

        public void SetScaleToSize(int int_7)
        {
            this.int_0 = int_7;
            this.bool_0 = false;
        }

        public void SetTitleColumns(int int_7, int int_8)
        {
            this.int_5 = Math.Max(1, int_7);
            this.int_6 = Math.Max(1, Math.Max(int_7, int_8));
            this.bool_1 = true;
        }

        public void SetTitleRows(int int_7, int int_8)
        {
            this.int_3 = Math.Max(1, int_7);
            this.int_4 = Math.Max(1, Math.Max(int_7, int_8));
            this.bool_1 = true;
        }

        public PageLayout Layout
        {
            get
            {
                return this.pageLayout_0;
            }
            set
            {
                this.pageLayout_0 = value;
                if (!this.pageLayout_0.IsValid())
                {
                    throw new ArgumentException("Invalid page layout defined");
                }
            }
        }

        public PageOrientation Orientation
        {
            get
            {
                return this.pageOrientation_0;
            }
            set
            {
                this.pageOrientation_0 = value;
                if (!this.pageOrientation_0.IsValid())
                {
                    throw new ArgumentException("Invalid page layout defined");
                }
            }
        }
    }
}

