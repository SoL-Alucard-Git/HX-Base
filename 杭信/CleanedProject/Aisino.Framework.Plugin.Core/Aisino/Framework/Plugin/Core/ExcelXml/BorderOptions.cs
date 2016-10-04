namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using Aisino.Framework.Plugin.Core.ExcelXml.Extensions;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class BorderOptions : IBorderOptions
    {
        private Borderline borderline_0;
        private BorderSides borderSides_0;
        [CompilerGenerated]
        private System.Drawing.Color color_0;
        [CompilerGenerated]
        private int int_0;

        public BorderOptions()
        {
            
            this.borderSides_0 = BorderSides.None;
            this.borderline_0 = Borderline.Continuous;
            this.Weight = 1;
            this.Color = System.Drawing.Color.Black;
        }

        public BorderOptions(IBorderOptions iborderOptions_0)
        {
            
            this.borderSides_0 = iborderOptions_0.Sides;
            this.borderline_0 = iborderOptions_0.LineStyle;
            this.Weight = iborderOptions_0.Weight;
            this.Color = iborderOptions_0.Color;
        }

        internal bool method_0(BorderOptions borderOptions_0)
        {
            return ((((this.Sides == borderOptions_0.Sides) && (this.LineStyle == borderOptions_0.LineStyle)) && (this.Weight == borderOptions_0.Weight)) && (this.Color == borderOptions_0.Color));
        }

        internal void method_1(XmlReader xmlReader_0)
        {
            if (!xmlReader_0.IsEmptyElement)
            {
                while (xmlReader_0.Read())
                {
                    if ((xmlReader_0.Name == "Borders") && (xmlReader_0.NodeType == XmlNodeType.EndElement))
                    {
                        return;
                    }
                    if ((xmlReader_0.NodeType == XmlNodeType.Element) && (xmlReader_0.Name == "Border"))
                    {
                        foreach (XmlReaderAttributeItem item in xmlReader_0.GetAttributes())
                        {
                            string localName = item.LocalName;
                            if (localName != null)
                            {
                                if (localName != "Position")
                                {
                                    if (!(localName == "LineStyle"))
                                    {
                                        int num;
                                        if ((localName == "Weight") && item.Value.ParseToInt<int>(out num))
                                        {
                                            this.Weight = num;
                                        }
                                    }
                                    else
                                    {
                                        this.LineStyle = ObjectExtensions.ParseEnum<Borderline>(item.Value);
                                    }
                                }
                                else
                                {
                                    this.Sides |= (BorderSides) ObjectExtensions.ParseEnum<BorderSides>(item.Value);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void method_2(XmlWriter xmlWriter_0, string string_0)
        {
            xmlWriter_0.WriteStartElement("Border");
            xmlWriter_0.WriteAttributeString("ss", "Position", null, string_0);
            xmlWriter_0.WriteAttributeString("ss", "LineStyle", null, this.LineStyle.ToString());
            xmlWriter_0.WriteAttributeString("ss", "Weight", null, this.Weight.ToString(CultureInfo.InvariantCulture));
            if (this.Color != System.Drawing.Color.Black)
            {
                xmlWriter_0.WriteAttributeString("ss", "Color", null, XmlStyle.smethod_0(this.Color));
            }
            xmlWriter_0.WriteEndElement();
        }

        internal void method_3(XmlWriter xmlWriter_0)
        {
            if (this.Sides != BorderSides.None)
            {
                xmlWriter_0.WriteStartElement("Borders");
                if (this.Sides.IsFlagSet(BorderSides.Left))
                {
                    this.method_2(xmlWriter_0, "Left");
                }
                if (this.Sides.IsFlagSet(BorderSides.Top))
                {
                    this.method_2(xmlWriter_0, "Top");
                }
                if (this.Sides.IsFlagSet(BorderSides.Right))
                {
                    this.method_2(xmlWriter_0, "Right");
                }
                if (this.Sides.IsFlagSet(BorderSides.Bottom))
                {
                    this.method_2(xmlWriter_0, "Bottom");
                }
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

        public Borderline LineStyle
        {
            get
            {
                return this.borderline_0;
            }
            set
            {
                this.borderline_0 = value;
                if (!this.borderline_0.IsValid())
                {
                    throw new ArgumentException("Invalid line style value encountered");
                }
            }
        }

        public BorderSides Sides
        {
            get
            {
                return this.borderSides_0;
            }
            set
            {
                this.borderSides_0 = value;
                if (!this.borderSides_0.IsValid())
                {
                    throw new ArgumentException("Invalid Border side value encountered");
                }
            }
        }

        public int Weight
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
    }
}

