namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using System;
    using System.Runtime.CompilerServices;

    public class AlignmentOptionsBase : CellSettingsApplier, IAlignmentOptions
    {
        [CompilerGenerated]
        private static CellSettingsApplier.Delegate35 delegate35_0;
        [CompilerGenerated]
        private static CellSettingsApplier.Delegate35 delegate35_1;
        [CompilerGenerated]
        private static CellSettingsApplier.Delegate35 delegate35_2;
        [CompilerGenerated]
        private static CellSettingsApplier.Delegate35 delegate35_3;
        [CompilerGenerated]
        private static CellSettingsApplier.Delegate35 delegate35_4;
        [CompilerGenerated]
        private static CellSettingsApplier.Delegate35 delegate35_5;

        internal AlignmentOptionsBase(Styles styles_1) : base(styles_1)
        {
            
        }

        [CompilerGenerated]
        private static object smethod_0(IStyle istyle_0)
        {
            return istyle_0.Alignment.Vertical;
        }

        [CompilerGenerated]
        private static object smethod_1(IStyle istyle_0)
        {
            return istyle_0.Alignment.Horizontal;
        }

        [CompilerGenerated]
        private static object smethod_2(IStyle istyle_0)
        {
            return istyle_0.Alignment.WrapText;
        }

        [CompilerGenerated]
        private static object smethod_3(IStyle istyle_0)
        {
            return istyle_0.Alignment.Indent;
        }

        [CompilerGenerated]
        private static object smethod_4(IStyle istyle_0)
        {
            return istyle_0.Alignment.Rotate;
        }

        [CompilerGenerated]
        private static object smethod_5(IStyle istyle_0)
        {
            return istyle_0.Alignment.ShrinkToFit;
        }

        public HorizontalAlignment Horizontal
        {
            get
            {
                if (delegate35_1 == null)
                {
                    delegate35_1 = new CellSettingsApplier.Delegate35(AlignmentOptionsBase.smethod_1);
                }
                return (HorizontalAlignment) base.method_0(delegate35_1);
            }
            set
            {
                base.method_1(style => style.Alignment.Horizontal = value);
            }
        }

        public int Indent
        {
            get
            {
                if (delegate35_3 == null)
                {
                    delegate35_3 = new CellSettingsApplier.Delegate35(AlignmentOptionsBase.smethod_3);
                }
                return (int) base.method_0(delegate35_3);
            }
            set
            {
                base.method_1(style => style.Alignment.Indent = value);
            }
        }

        public int Rotate
        {
            get
            {
                if (delegate35_4 == null)
                {
                    delegate35_4 = new CellSettingsApplier.Delegate35(AlignmentOptionsBase.smethod_4);
                }
                return (int) base.method_0(delegate35_4);
            }
            set
            {
                base.method_1(style => style.Alignment.Rotate = value);
            }
        }

        public bool ShrinkToFit
        {
            get
            {
                if (delegate35_5 == null)
                {
                    delegate35_5 = new CellSettingsApplier.Delegate35(AlignmentOptionsBase.smethod_5);
                }
                return (bool) base.method_0(delegate35_5);
            }
            set
            {
                base.method_1(style => style.Alignment.ShrinkToFit = value);
            }
        }

        public VerticalAlignment Vertical
        {
            get
            {
                if (delegate35_0 == null)
                {
                    delegate35_0 = new CellSettingsApplier.Delegate35(AlignmentOptionsBase.smethod_0);
                }
                return (VerticalAlignment) base.method_0(delegate35_0);
            }
            set
            {
                base.method_1(style => style.Alignment.Vertical = value);
            }
        }

        public bool WrapText
        {
            get
            {
                if (delegate35_2 == null)
                {
                    delegate35_2 = new CellSettingsApplier.Delegate35(AlignmentOptionsBase.smethod_2);
                }
                return (bool) base.method_0(delegate35_2);
            }
            set
            {
                base.method_1(style => style.Alignment.WrapText = value);
            }
        }
    }
}

