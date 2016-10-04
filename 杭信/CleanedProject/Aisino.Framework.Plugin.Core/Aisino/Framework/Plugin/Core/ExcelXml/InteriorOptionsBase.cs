namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class InteriorOptionsBase : CellSettingsApplier, IInteriorOptions
    {
        [CompilerGenerated]
        private static CellSettingsApplier.Delegate35 delegate35_0;
        [CompilerGenerated]
        private static CellSettingsApplier.Delegate35 delegate35_1;
        [CompilerGenerated]
        private static CellSettingsApplier.Delegate35 delegate35_2;

        internal InteriorOptionsBase(Styles styles_1) : base(styles_1)
        {
            
        }

        [CompilerGenerated]
        private static object smethod_0(IStyle istyle_0)
        {
            return istyle_0.Interior.Color;
        }

        [CompilerGenerated]
        private static object smethod_1(IStyle istyle_0)
        {
            return istyle_0.Interior.PatternColor;
        }

        [CompilerGenerated]
        private static object smethod_2(IStyle istyle_0)
        {
            return istyle_0.Interior.Pattern;
        }

        public System.Drawing.Color Color
        {
            get
            {
                if (delegate35_0 == null)
                {
                    delegate35_0 = new CellSettingsApplier.Delegate35(InteriorOptionsBase.smethod_0);
                }
                return (System.Drawing.Color) base.method_0(delegate35_0);
            }
            set
            {
                base.method_1(style => style.Interior.Color = value);
            }
        }

        public Aisino.Framework.Plugin.Core.ExcelXml.Pattern Pattern
        {
            get
            {
                if (delegate35_2 == null)
                {
                    delegate35_2 = new CellSettingsApplier.Delegate35(InteriorOptionsBase.smethod_2);
                }
                return (Aisino.Framework.Plugin.Core.ExcelXml.Pattern) base.method_0(delegate35_2);
            }
            set
            {
                base.method_1(style => style.Interior.Pattern = value);
            }
        }

        public System.Drawing.Color PatternColor
        {
            get
            {
                if (delegate35_1 == null)
                {
                    delegate35_1 = new CellSettingsApplier.Delegate35(InteriorOptionsBase.smethod_1);
                }
                return (System.Drawing.Color) base.method_0(delegate35_1);
            }
            set
            {
                base.method_1(style => style.Interior.PatternColor = value);
            }
        }
    }
}

