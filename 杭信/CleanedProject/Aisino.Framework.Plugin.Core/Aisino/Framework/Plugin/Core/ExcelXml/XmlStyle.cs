namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using Aisino.Framework.Plugin.Core.ExcelXml.Extensions;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class XmlStyle : IStyle
    {
        [CompilerGenerated]
        private AlignmentOptions alignmentOptions_0;
        [CompilerGenerated]
        private BorderOptions borderOptions_0;
        private DisplayFormatType displayFormatType_0;
        [CompilerGenerated]
        private FontOptions fontOptions_0;
        [CompilerGenerated]
        private InteriorOptions interiorOptions_0;
        private string string_0;
        [CompilerGenerated]
        private string string_1;

        public XmlStyle()
        {
            
            this.method_8();
            this.method_9();
        }

        public XmlStyle(XmlStyle xmlStyle_0)
        {
            
            if (xmlStyle_0 == null)
            {
                this.method_8();
                this.method_9();
            }
            else
            {
                this.ID = "";
                this.method_1(new FontOptions(xmlStyle_0.method_0()));
                this.method_5(new InteriorOptions(xmlStyle_0.method_4()));
                this.method_3(new AlignmentOptions(xmlStyle_0.method_2()));
                this.method_7(new BorderOptions(xmlStyle_0.method_6()));
                this.DisplayFormat = xmlStyle_0.DisplayFormat;
            }
        }

        public override bool Equals(object value)
        {
            return ((value is XmlStyle) && this.method_10((XmlStyle) value));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        [CompilerGenerated]
        private FontOptions method_0()
        {
            return this.fontOptions_0;
        }

        [CompilerGenerated]
        private void method_1(FontOptions fontOptions_1)
        {
            this.fontOptions_0 = fontOptions_1;
        }

        internal bool method_10(XmlStyle xmlStyle_0)
        {
            if (xmlStyle_0 == null)
            {
                return false;
            }
            return (((this.method_0().method_0(xmlStyle_0.method_0()) && this.method_2().method_0(xmlStyle_0.method_2())) && (this.method_4().method_0(xmlStyle_0.method_4()) && this.method_6().method_0(xmlStyle_0.method_6()))) && (this.DisplayFormat == xmlStyle_0.DisplayFormat));
        }

        internal void method_11(XmlWriter xmlWriter_0)
        {
            xmlWriter_0.WriteStartElement("Style");
            xmlWriter_0.WriteAttributeString("ss", "ID", null, this.ID);
            this.method_0().method_2(xmlWriter_0);
            this.method_2().method_2(xmlWriter_0);
            this.method_6().method_3(xmlWriter_0);
            this.method_4().method_2(xmlWriter_0);
            if (this.DisplayFormat != DisplayFormatType.None)
            {
                string longDatePattern = "";
                switch (this.DisplayFormat)
                {
                    case DisplayFormatType.Text:
                        longDatePattern = "@";
                        break;

                    case DisplayFormatType.Fixed:
                    case DisplayFormatType.Standard:
                    case DisplayFormatType.Percent:
                    case DisplayFormatType.Scientific:
                        longDatePattern = this.DisplayFormat.ToString();
                        break;

                    case DisplayFormatType.GeneralDate:
                        longDatePattern = "General Date";
                        break;

                    case DisplayFormatType.ShortDate:
                        longDatePattern = "Short Date";
                        break;

                    case DisplayFormatType.LongDate:
                        longDatePattern = DateTimeFormatInfo.CurrentInfo.LongDatePattern;
                        break;

                    case DisplayFormatType.Time:
                        longDatePattern = DateTimeFormatInfo.CurrentInfo.LongTimePattern;
                        if (longDatePattern.Contains("t"))
                        {
                            longDatePattern = longDatePattern.Replace("t", "AM/PM");
                        }
                        if (longDatePattern.Contains("tt"))
                        {
                            longDatePattern = longDatePattern.Replace("tt", "AM/PM");
                        }
                        break;

                    case DisplayFormatType.Custom:
                        longDatePattern = this.CustomFormatString;
                        break;
                }
                xmlWriter_0.WriteStartElement("NumberFormat");
                xmlWriter_0.WriteAttributeString("ss", "Format", null, longDatePattern);
                xmlWriter_0.WriteEndElement();
            }
            xmlWriter_0.WriteEndElement();
        }

        [CompilerGenerated]
        private AlignmentOptions method_2()
        {
            return this.alignmentOptions_0;
        }

        [CompilerGenerated]
        private void method_3(AlignmentOptions alignmentOptions_1)
        {
            this.alignmentOptions_0 = alignmentOptions_1;
        }

        [CompilerGenerated]
        private InteriorOptions method_4()
        {
            return this.interiorOptions_0;
        }

        [CompilerGenerated]
        private void method_5(InteriorOptions interiorOptions_1)
        {
            this.interiorOptions_0 = interiorOptions_1;
        }

        [CompilerGenerated]
        private BorderOptions method_6()
        {
            return this.borderOptions_0;
        }

        [CompilerGenerated]
        private void method_7(BorderOptions borderOptions_1)
        {
            this.borderOptions_0 = borderOptions_1;
        }

        private void method_8()
        {
            this.method_1(new FontOptions());
            this.method_5(new InteriorOptions());
            this.method_3(new AlignmentOptions());
            this.method_7(new BorderOptions());
        }

        private void method_9()
        {
            this.ID = "";
            this.DisplayFormat = DisplayFormatType.None;
        }

        public static bool operator ==(XmlStyle xmlStyle_0, XmlStyle xmlStyle_1)
        {
            if (xmlStyle_0 == null)
            {
                return (xmlStyle_1 == null);
            }
            return xmlStyle_0.Equals(xmlStyle_1);
        }

        public static bool operator !=(XmlStyle xmlStyle_0, XmlStyle xmlStyle_1)
        {
            if (xmlStyle_0 == null)
            {
                return (xmlStyle_1 != null);
            }
            return !xmlStyle_0.Equals(xmlStyle_1);
        }

        internal static string smethod_0(Color color_0)
        {
            return string.Format(CultureInfo.InvariantCulture, "#{0:X2}{1:X2}{2:X2}", new object[] { color_0.R, color_0.G, color_0.B });
        }

        internal static Color smethod_1(string string_2)
        {
            int num;
            int num2;
            int num3;
            if ((int.TryParse(string_2.Substring(1, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num) && int.TryParse(string_2.Substring(3, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num2)) && int.TryParse(string_2.Substring(5, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num3))
            {
                return Color.FromArgb(num, num2, num3);
            }
            return Color.Black;
        }

        internal static XmlStyle smethod_2(XmlReader xmlReader_0)
        {
            string str;
            XmlStyle style = new XmlStyle();
            bool isEmptyElement = xmlReader_0.IsEmptyElement;
            XmlReaderAttributeItem singleAttribute = xmlReader_0.GetSingleAttribute("ID");
            if (singleAttribute != null)
            {
                style.ID = singleAttribute.Value;
            }
            if (!isEmptyElement)
            {
                goto Label_0215;
            }
            if (singleAttribute != null)
            {
                return style;
            }
            return null;
        Label_0135:
            if (str == DateTimeFormatInfo.CurrentInfo.LongDatePattern)
            {
                style.DisplayFormat = DisplayFormatType.LongDate;
            }
            string longTimePattern = DateTimeFormatInfo.CurrentInfo.LongTimePattern;
            if (longTimePattern.Contains("t"))
            {
                longTimePattern = longTimePattern.Replace("t", "AM/PM");
            }
            if (longTimePattern.Contains("tt"))
            {
                longTimePattern = longTimePattern.Replace("tt", "AM/PM");
            }
            if (str == longTimePattern)
            {
                style.DisplayFormat = DisplayFormatType.Time;
            }
            try
            {
                style.DisplayFormat = ObjectExtensions.ParseEnum<DisplayFormatType>(str);
            }
            catch (ArgumentException)
            {
                if (str.IsNullOrEmpty())
                {
                    style.DisplayFormat = DisplayFormatType.None;
                }
                else
                {
                    style.DisplayFormat = DisplayFormatType.Custom;
                    style.CustomFormatString = str;
                }
            }
        Label_0215:
            if (xmlReader_0.Read())
            {
                string str3;
                if ((xmlReader_0.Name == "Style") && (xmlReader_0.NodeType == XmlNodeType.EndElement))
                {
                    return style;
                }
                if ((xmlReader_0.NodeType != XmlNodeType.Element) || ((str3 = xmlReader_0.Name) == null))
                {
                    goto Label_0215;
                }
                if (str3 == "Font")
                {
                    style.method_0().method_1(xmlReader_0);
                    goto Label_0215;
                }
                if (str3 == "Alignment")
                {
                    style.method_2().method_1(xmlReader_0);
                    goto Label_0215;
                }
                if (str3 == "Interior")
                {
                    style.method_4().method_1(xmlReader_0);
                    goto Label_0215;
                }
                if (str3 == "Borders")
                {
                    style.method_6().method_1(xmlReader_0);
                    goto Label_0215;
                }
                if (!(str3 == "NumberFormat"))
                {
                    goto Label_0215;
                }
                XmlReaderAttributeItem item2 = xmlReader_0.GetSingleAttribute("Format");
                if (item2 == null)
                {
                    goto Label_0215;
                }
                str = item2.Value;
                string str4 = str;
                if (str4 != null)
                {
                    if (str4 == "Short Date")
                    {
                        style.DisplayFormat = DisplayFormatType.ShortDate;
                    }
                    else if (!(str4 == "General Date"))
                    {
                        if (!(str4 == "@"))
                        {
                            goto Label_0135;
                        }
                        style.DisplayFormat = DisplayFormatType.Text;
                    }
                    else
                    {
                        style.DisplayFormat = DisplayFormatType.GeneralDate;
                    }
                    goto Label_0215;
                }
                goto Label_0135;
            }
            return style;
        }

        public IAlignmentOptions Alignment
        {
            get
            {
                return this.method_2();
            }
            set
            {
                this.method_3((AlignmentOptions) value);
            }
        }

        public IBorderOptions Border
        {
            get
            {
                return this.method_6();
            }
            set
            {
                this.method_7((BorderOptions) value);
            }
        }

        public string CustomFormatString
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
                this.DisplayFormat = this.string_0.IsNullOrEmpty() ? DisplayFormatType.None : DisplayFormatType.Custom;
            }
        }

        public DisplayFormatType DisplayFormat
        {
            get
            {
                return this.displayFormatType_0;
            }
            set
            {
                this.displayFormatType_0 = value;
                if (!this.displayFormatType_0.IsValid())
                {
                    throw new ArgumentException("Invalid display format value encountered");
                }
                if ((this.displayFormatType_0 == DisplayFormatType.Custom) && this.CustomFormatString.IsNullOrEmpty())
                {
                    this.displayFormatType_0 = DisplayFormatType.None;
                }
            }
        }

        public IFontOptions Font
        {
            get
            {
                return this.method_0();
            }
            set
            {
                this.method_1((FontOptions) value);
            }
        }

        internal string ID
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

        public IInteriorOptions Interior
        {
            get
            {
                return this.method_4();
            }
            set
            {
                this.method_5((InteriorOptions) value);
            }
        }
    }
}

