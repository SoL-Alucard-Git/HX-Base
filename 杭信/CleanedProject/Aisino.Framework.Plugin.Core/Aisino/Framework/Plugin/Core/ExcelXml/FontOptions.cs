namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using Aisino.Framework.Plugin.Core.ExcelXml.Extensions;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class FontOptions : IFontOptions
    {
        [CompilerGenerated]
        private bool bool_0;
        [CompilerGenerated]
        private bool bool_1;
        [CompilerGenerated]
        private bool bool_2;
        [CompilerGenerated]
        private bool bool_3;
        [CompilerGenerated]
        private System.Drawing.Color color_0;
        [CompilerGenerated]
        private int int_0;
        [CompilerGenerated]
        private string string_0;

        public FontOptions()
        {
            
            this.Name = "Tahoma";
            this.Size = 0;
            this.Bold = false;
            this.Underline = false;
            this.Italic = false;
            this.Strikeout = false;
            this.Color = System.Drawing.Color.Black;
        }

        public FontOptions(IFontOptions ifontOptions_0)
        {
            
            this.Name = ifontOptions_0.Name;
            this.Size = ifontOptions_0.Size;
            this.Bold = ifontOptions_0.Bold;
            this.Underline = ifontOptions_0.Underline;
            this.Italic = ifontOptions_0.Italic;
            this.Strikeout = ifontOptions_0.Strikeout;
            this.Color = ifontOptions_0.Color;
        }

        internal bool method_0(FontOptions fontOptions_0)
        {
            return (((((this.Name == fontOptions_0.Name) && (this.Size == fontOptions_0.Size)) && ((this.Bold == fontOptions_0.Bold) && (this.Underline == fontOptions_0.Underline))) && ((this.Italic == fontOptions_0.Italic) && (this.Strikeout == fontOptions_0.Strikeout))) && (this.Color == fontOptions_0.Color));
        }

        internal void method_1(XmlReader xmlReader_0)
        {
            foreach (XmlReaderAttributeItem item in xmlReader_0.GetAttributes())
            {
                switch (item.LocalName)
                {
                    case "FontName":
                        this.Name = item.Value;
                        break;

                    case "Size":
                        int num;
                        if (item.Value.ParseToInt<int>(out num))
                        {
                            this.Size = num;
                        }
                        break;

                    case "Color":
                        this.Color = XmlStyle.smethod_1(item.Value);
                        break;

                    case "Bold":
                        this.Bold = item.Value == "1";
                        break;

                    case "Italic":
                        this.Italic = item.Value == "1";
                        break;

                    case "Underline":
                        this.Underline = item.Value == "Single";
                        break;

                    case "Strikeout":
                        this.Strikeout = item.Value == "1";
                        break;
                }
            }
        }

        internal void method_2(XmlWriter xmlWriter_0)
        {
            xmlWriter_0.WriteStartElement("Font");
            xmlWriter_0.WriteAttributeString("ss", "FontName", null, this.Name);
            if (this.Size != 0)
            {
                xmlWriter_0.WriteAttributeString("ss", "Size", null, this.Size.ToString(CultureInfo.InvariantCulture));
            }
            xmlWriter_0.WriteAttributeString("ss", "Color", null, XmlStyle.smethod_0(this.Color));
            if (this.Bold)
            {
                xmlWriter_0.WriteAttributeString("ss", "Bold", null, "1");
            }
            if (this.Italic)
            {
                xmlWriter_0.WriteAttributeString("ss", "Italic", null, "1");
            }
            if (this.Underline)
            {
                xmlWriter_0.WriteAttributeString("ss", "Underline", null, "Single");
            }
            if (this.Strikeout)
            {
                xmlWriter_0.WriteAttributeString("ss", "Strikeout", null, "1");
            }
            xmlWriter_0.WriteEndElement();
        }

        public bool Bold
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

        public System.Drawing.Color Color
        {
            [CompilerGenerated]
            get
            {
                return this.color_0;
            }
            [CompilerGenerated]
            set
            {
                this.color_0 = value;
            }
        }

        public bool Italic
        {
            [CompilerGenerated]
            get
            {
                return this.bool_2;
            }
            [CompilerGenerated]
            set
            {
                this.bool_2 = value;
            }
        }

        public string Name
        {
            [CompilerGenerated]
            get
            {
                return this.string_0;
            }
            [CompilerGenerated]
            set
            {
                this.string_0 = value;
            }
        }

        public int Size
        {
            [CompilerGenerated]
            get
            {
                return this.int_0;
            }
            [CompilerGenerated]
            set
            {
                this.int_0 = value;
            }
        }

        public bool Strikeout
        {
            [CompilerGenerated]
            get
            {
                return this.bool_3;
            }
            [CompilerGenerated]
            set
            {
                this.bool_3 = value;
            }
        }

        public bool Underline
        {
            [CompilerGenerated]
            get
            {
                return this.bool_1;
            }
            [CompilerGenerated]
            set
            {
                this.bool_1 = value;
            }
        }
    }
}

