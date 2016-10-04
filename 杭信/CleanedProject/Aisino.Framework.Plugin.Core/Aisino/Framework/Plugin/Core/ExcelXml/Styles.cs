namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class Styles : CellSettingsApplier, IStyle
    {
        private AlignmentOptionsBase alignmentOptionsBase_0;
        [CompilerGenerated]
        private BorderOptionsBase borderOptionsBase_0;
        [CompilerGenerated]
        private static CellSettingsApplier.Delegate35 delegate35_0;
        [CompilerGenerated]
        private static CellSettingsApplier.Delegate35 delegate35_1;
        private FontOptionsBase fontOptionsBase_0;
        private InteriorOptionsBase interiorOptionsBase_0;
        [CompilerGenerated]
        private string string_0;

        internal Styles()
        {
            
            this.StyleID = "";
            this.fontOptionsBase_0 = new FontOptionsBase(this);
            this.alignmentOptionsBase_0 = new AlignmentOptionsBase(this);
            this.interiorOptionsBase_0 = new InteriorOptionsBase(this);
            this.method_4(new BorderOptionsBase(this));
            base.styles_0 = this;
        }

        internal bool method_2()
        {
            return (this.StyleID == "Default");
        }

        [CompilerGenerated]
        private BorderOptionsBase method_3()
        {
            return this.borderOptionsBase_0;
        }

        [CompilerGenerated]
        private void method_4(BorderOptionsBase borderOptionsBase_1)
        {
            this.borderOptionsBase_0 = borderOptionsBase_1;
        }

        [CompilerGenerated]
        private static object smethod_0(IStyle istyle_0)
        {
            return istyle_0.DisplayFormat;
        }

        [CompilerGenerated]
        private static object smethod_1(IStyle istyle_0)
        {
            return istyle_0.CustomFormatString;
        }

        internal abstract void vmethod_1(Delegate36 delegate36_0);
        internal abstract Cell vmethod_2();

        public IAlignmentOptions Alignment
        {
            get
            {
                return this.alignmentOptionsBase_0;
            }
            set
            {
                this.alignmentOptionsBase_0 = (AlignmentOptionsBase) value;
            }
        }

        public IBorderOptions Border
        {
            get
            {
                return this.method_3();
            }
            set
            {
                this.method_4((BorderOptionsBase) value);
            }
        }

        public string CustomFormatString
        {
            get
            {
                if (delegate35_1 == null)
                {
                    delegate35_1 = new CellSettingsApplier.Delegate35(Styles.smethod_1);
                }
                return (string) base.method_0(delegate35_1);
            }
            set
            {
                base.method_1(style => style.CustomFormatString = value);
            }
        }

        public DisplayFormatType DisplayFormat
        {
            get
            {
                if (delegate35_0 == null)
                {
                    delegate35_0 = new CellSettingsApplier.Delegate35(Styles.smethod_0);
                }
                return (DisplayFormatType) base.method_0(delegate35_0);
            }
            set
            {
                base.method_1(style => style.DisplayFormat = value);
            }
        }

        public IFontOptions Font
        {
            get
            {
                return this.fontOptionsBase_0;
            }
            set
            {
                this.fontOptionsBase_0 = (FontOptionsBase) value;
            }
        }

        public IInteriorOptions Interior
        {
            get
            {
                return this.interiorOptionsBase_0;
            }
            set
            {
                this.interiorOptionsBase_0 = (InteriorOptionsBase) value;
            }
        }

        public XmlStyle Style
        {
            get
            {
                if (this.vmethod_0() == null)
                {
                    return this.vmethod_2().vmethod_0().method_6(this.StyleID);
                }
                return this.vmethod_0().method_6(this.StyleID);
            }
            set
            {
                Delegate36 delegate2 = null;
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (this.vmethod_0() == null)
                {
                    if (delegate2 == null)
                    {
                        delegate2 = cell => cell.Style = value;
                    }
                    this.vmethod_1(delegate2);
                }
                else
                {
                    this.StyleID = this.vmethod_0().method_8(value);
                }
            }
        }

        internal string StyleID
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

        internal delegate void Delegate36(Cell cell);
    }
}

