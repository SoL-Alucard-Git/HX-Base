namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using Aisino.Framework.Plugin.Core.ExcelXml.Extensions;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class InteriorOptions : IInteriorOptions
    {
        [CompilerGenerated]
        private System.Drawing.Color color_0;
        [CompilerGenerated]
        private System.Drawing.Color color_1;
        [CompilerGenerated]
        private Aisino.Framework.Plugin.Core.ExcelXml.Pattern pattern_0;

        public InteriorOptions()
        {
            
            this.Color = System.Drawing.Color.Empty;
            this.PatternColor = System.Drawing.Color.Empty;
            this.Pattern = Aisino.Framework.Plugin.Core.ExcelXml.Pattern.Solid;
        }

        public InteriorOptions(IInteriorOptions iinteriorOptions_0)
        {
            
            this.Color = iinteriorOptions_0.Color;
            this.PatternColor = iinteriorOptions_0.PatternColor;
            this.Pattern = iinteriorOptions_0.Pattern;
        }

        internal bool method_0(InteriorOptions interiorOptions_0)
        {
            return (((this.Color == interiorOptions_0.Color) && (this.PatternColor == interiorOptions_0.PatternColor)) && (this.Pattern == interiorOptions_0.Pattern));
        }

        internal void method_1(XmlReader xmlReader_0)
        {
            foreach (XmlReaderAttributeItem item in xmlReader_0.GetAttributes())
            {
                string localName = item.LocalName;
                if (localName != null)
                {
                    if (localName != "Color")
                    {
                        if (!(localName == "PatternColor"))
                        {
                            if (localName == "Pattern")
                            {
                                this.Pattern = ObjectExtensions.ParseEnum<Aisino.Framework.Plugin.Core.ExcelXml.Pattern>(item.Value);
                            }
                        }
                        else
                        {
                            this.PatternColor = XmlStyle.smethod_1(item.Value);
                        }
                    }
                    else
                    {
                        this.Color = XmlStyle.smethod_1(item.Value);
                    }
                }
            }
        }

        internal void method_2(XmlWriter xmlWriter_0)
        {
            if ((this.Color != System.Drawing.Color.Empty) || (this.PatternColor != System.Drawing.Color.Empty))
            {
                xmlWriter_0.WriteStartElement("Interior");
                if (this.Color != System.Drawing.Color.Empty)
                {
                    xmlWriter_0.WriteAttributeString("ss", "Color", null, XmlStyle.smethod_0(this.Color));
                }
                if (this.PatternColor != System.Drawing.Color.Empty)
                {
                    xmlWriter_0.WriteAttributeString("ss", "PatternColor", null, XmlStyle.smethod_0(this.PatternColor));
                }
                xmlWriter_0.WriteAttributeString("ss", "Pattern", null, this.Pattern.ToString());
                xmlWriter_0.WriteEndElement();
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

        public Aisino.Framework.Plugin.Core.ExcelXml.Pattern Pattern
        {
            [CompilerGenerated]
            get
            {
                return this.pattern_0;
            }
            [CompilerGenerated]
            set
            {
                this.pattern_0 = value;
            }
        }

        public System.Drawing.Color PatternColor
        {
            [CompilerGenerated]
            get
            {
                return this.color_1;
            }
            [CompilerGenerated]
            set
            {
                this.color_1 = value;
            }
        }
    }
}

