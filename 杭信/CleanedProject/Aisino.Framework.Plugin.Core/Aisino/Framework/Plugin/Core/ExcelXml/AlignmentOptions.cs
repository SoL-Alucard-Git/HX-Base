namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using Aisino.Framework.Plugin.Core.ExcelXml.Extensions;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class AlignmentOptions : IAlignmentOptions
    {
        [CompilerGenerated]
        private bool bool_0;
        [CompilerGenerated]
        private bool bool_1;
        private HorizontalAlignment horizontalAlignment_0;
        [CompilerGenerated]
        private int int_0;
        [CompilerGenerated]
        private int int_1;
        private VerticalAlignment verticalAlignment_0;

        public AlignmentOptions()
        {
            
            this.horizontalAlignment_0 = HorizontalAlignment.None;
            this.verticalAlignment_0 = VerticalAlignment.None;
            this.Indent = 0;
            this.Rotate = 0;
            this.WrapText = false;
            this.ShrinkToFit = false;
        }

        public AlignmentOptions(IAlignmentOptions ialignmentOptions_0)
        {
            
            this.horizontalAlignment_0 = ialignmentOptions_0.Horizontal;
            this.verticalAlignment_0 = ialignmentOptions_0.Vertical;
            this.Indent = ialignmentOptions_0.Indent;
            this.Rotate = ialignmentOptions_0.Rotate;
            this.WrapText = ialignmentOptions_0.WrapText;
            this.ShrinkToFit = ialignmentOptions_0.ShrinkToFit;
        }

        internal bool method_0(AlignmentOptions alignmentOptions_0)
        {
            return (((((this.Vertical == alignmentOptions_0.Vertical) && (this.Horizontal == alignmentOptions_0.Horizontal)) && ((this.Indent == alignmentOptions_0.Indent) && (this.Rotate == alignmentOptions_0.Rotate))) && (this.WrapText == alignmentOptions_0.WrapText)) && (this.ShrinkToFit == alignmentOptions_0.ShrinkToFit));
        }

        internal void method_1(XmlReader xmlReader_0)
        {
            foreach (XmlReaderAttributeItem item in xmlReader_0.GetAttributes())
            {
                string localName = item.LocalName;
                if (localName != null)
                {
                    if (localName == "Vertical")
                    {
                        this.Vertical = ObjectExtensions.ParseEnum<VerticalAlignment>(item.Value);
                    }
                    else if (localName == "Horizontal")
                    {
                        this.Horizontal = ObjectExtensions.ParseEnum<HorizontalAlignment>(item.Value);
                    }
                    else if (localName == "WrapText")
                    {
                        this.WrapText = item.Value == "1";
                    }
                    else if (localName == "ShrinkToFit")
                    {
                        this.ShrinkToFit = item.Value == "1";
                    }
                    else if (!(localName == "Indent"))
                    {
                        int num;
                        if ((localName == "Rotate") && item.Value.ParseToInt<int>(out num))
                        {
                            this.Rotate = num;
                        }
                    }
                    else
                    {
                        int num2;
                        if (item.Value.ParseToInt<int>(out num2))
                        {
                            this.Indent = num2;
                        }
                    }
                }
            }
        }

        internal void method_2(XmlWriter xmlWriter_0)
        {
            xmlWriter_0.WriteStartElement("Alignment");
            if (this.Vertical != VerticalAlignment.None)
            {
                xmlWriter_0.WriteAttributeString("ss", "Vertical", null, this.Vertical.ToString());
            }
            if (this.Horizontal != HorizontalAlignment.None)
            {
                xmlWriter_0.WriteAttributeString("ss", "Horizontal", null, this.Horizontal.ToString());
            }
            if (this.WrapText)
            {
                xmlWriter_0.WriteAttributeString("ss", "WrapText", null, "1");
            }
            if (this.ShrinkToFit)
            {
                xmlWriter_0.WriteAttributeString("ss", "ShrinkToFit", null, "1");
            }
            if (this.Indent > 0)
            {
                xmlWriter_0.WriteAttributeString("ss", "Indent", null, this.Indent.ToString(CultureInfo.InvariantCulture));
            }
            if (this.Rotate > 0)
            {
                xmlWriter_0.WriteAttributeString("ss", "Rotate", null, this.Rotate.ToString(CultureInfo.InvariantCulture));
            }
            xmlWriter_0.WriteEndElement();
        }

        public HorizontalAlignment Horizontal
        {
            get
            {
                return this.horizontalAlignment_0;
            }
            set
            {
                this.horizontalAlignment_0 = value;
                if (!this.horizontalAlignment_0.IsValid())
                {
                    throw new ArgumentException("Invalid horizontal alignment value encountered");
                }
            }
        }

        public int Indent
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

        public int Rotate
        {
            [CompilerGenerated]
            get
            {
                return this.int_1;
            }
            [CompilerGenerated]
            set
            {
                this.int_1 = value;
            }
        }

        public bool ShrinkToFit
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

        public VerticalAlignment Vertical
        {
            get
            {
                return this.verticalAlignment_0;
            }
            set
            {
                this.verticalAlignment_0 = value;
                if (!this.verticalAlignment_0.IsValid())
                {
                    throw new ArgumentException("Invalid vertical alignment value encountered");
                }
            }
        }

        public bool WrapText
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
    }
}

