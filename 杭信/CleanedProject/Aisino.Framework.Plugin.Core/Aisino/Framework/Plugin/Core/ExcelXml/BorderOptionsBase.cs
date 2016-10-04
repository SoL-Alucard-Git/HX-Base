namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class BorderOptionsBase : CellSettingsApplier, IBorderOptions
    {
        [CompilerGenerated]
        private static CellSettingsApplier.Delegate35 delegate35_0;
        [CompilerGenerated]
        private static CellSettingsApplier.Delegate35 delegate35_1;
        [CompilerGenerated]
        private static CellSettingsApplier.Delegate35 delegate35_2;
        [CompilerGenerated]
        private static CellSettingsApplier.Delegate35 delegate35_3;

        internal BorderOptionsBase(Styles styles_1) : base(styles_1)
        {
            
        }

        [CompilerGenerated]
        private static object smethod_0(IStyle istyle_0)
        {
            return istyle_0.Border.Sides;
        }

        [CompilerGenerated]
        private static object smethod_1(IStyle istyle_0)
        {
            return istyle_0.Border.Color;
        }

        [CompilerGenerated]
        private static object smethod_2(IStyle istyle_0)
        {
            return istyle_0.Border.Weight;
        }

        [CompilerGenerated]
        private static object smethod_3(IStyle istyle_0)
        {
            return istyle_0.Border.LineStyle;
        }

        public System.Drawing.Color Color
        {
            get
            {
                if (delegate35_1 == null)
                {
                    delegate35_1 = new CellSettingsApplier.Delegate35(BorderOptionsBase.smethod_1);
                }
                return (System.Drawing.Color) base.method_0(delegate35_1);
            }
            set
            {
                base.method_1(style => style.Border.Color = value);
            }
        }

        public Borderline LineStyle
        {
            get
            {
                if (delegate35_3 == null)
                {
                    delegate35_3 = new CellSettingsApplier.Delegate35(BorderOptionsBase.smethod_3);
                }
                return (Borderline) base.method_0(delegate35_3);
            }
            set
            {
                base.method_1(style => style.Border.LineStyle = value);
            }
        }

        public BorderSides Sides
        {
            get
            {
                if (delegate35_0 == null)
                {
                    delegate35_0 = new CellSettingsApplier.Delegate35(BorderOptionsBase.smethod_0);
                }
                return (BorderSides) base.method_0(delegate35_0);
            }
            set
            {
                base.method_1(style => style.Border.Sides = value);
            }
        }

        public int Weight
        {
            get
            {
                if (delegate35_2 == null)
                {
                    delegate35_2 = new CellSettingsApplier.Delegate35(BorderOptionsBase.smethod_2);
                }
                return (int) base.method_0(delegate35_2);
            }
            set
            {
                base.method_1(style => style.Border.Weight = value);
            }
        }
    }
}

