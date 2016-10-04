namespace Aisino.Framework.Plugin.Core.ExcelXml
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class FontOptionsBase : CellSettingsApplier, IFontOptions
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
        [CompilerGenerated]
        private static CellSettingsApplier.Delegate35 delegate35_6;

        internal FontOptionsBase(Styles styles_1) : base(styles_1)
        {
            
        }

        [CompilerGenerated]
        private static object smethod_0(IStyle istyle_0)
        {
            return istyle_0.Font.Name;
        }

        [CompilerGenerated]
        private static object smethod_1(IStyle istyle_0)
        {
            return istyle_0.Font.Size;
        }

        [CompilerGenerated]
        private static object smethod_2(IStyle istyle_0)
        {
            return istyle_0.Font.Bold;
        }

        [CompilerGenerated]
        private static object smethod_3(IStyle istyle_0)
        {
            return istyle_0.Font.Underline;
        }

        [CompilerGenerated]
        private static object smethod_4(IStyle istyle_0)
        {
            return istyle_0.Font.Italic;
        }

        [CompilerGenerated]
        private static object smethod_5(IStyle istyle_0)
        {
            return istyle_0.Font.Strikeout;
        }

        [CompilerGenerated]
        private static object smethod_6(IStyle istyle_0)
        {
            return istyle_0.Font.Color;
        }

        public bool Bold
        {
            get
            {
                if (delegate35_2 == null)
                {
                    delegate35_2 = new CellSettingsApplier.Delegate35(FontOptionsBase.smethod_2);
                }
                return (bool) base.method_0(delegate35_2);
            }
            set
            {
                base.method_1(style => style.Font.Bold = value);
            }
        }

        public System.Drawing.Color Color
        {
            get
            {
                if (delegate35_6 == null)
                {
                    delegate35_6 = new CellSettingsApplier.Delegate35(FontOptionsBase.smethod_6);
                }
                return (System.Drawing.Color) base.method_0(delegate35_6);
            }
            set
            {
                base.method_1(style => style.Font.Color = value);
            }
        }

        public bool Italic
        {
            get
            {
                if (delegate35_4 == null)
                {
                    delegate35_4 = new CellSettingsApplier.Delegate35(FontOptionsBase.smethod_4);
                }
                return (bool) base.method_0(delegate35_4);
            }
            set
            {
                base.method_1(style => style.Font.Italic = value);
            }
        }

        public string Name
        {
            get
            {
                if (delegate35_0 == null)
                {
                    delegate35_0 = new CellSettingsApplier.Delegate35(FontOptionsBase.smethod_0);
                }
                return (string) base.method_0(delegate35_0);
            }
            set
            {
                base.method_1(style => style.Font.Name = value);
            }
        }

        public int Size
        {
            get
            {
                if (delegate35_1 == null)
                {
                    delegate35_1 = new CellSettingsApplier.Delegate35(FontOptionsBase.smethod_1);
                }
                return (int) base.method_0(delegate35_1);
            }
            set
            {
                base.method_1(style => style.Font.Size = value);
            }
        }

        public bool Strikeout
        {
            get
            {
                if (delegate35_5 == null)
                {
                    delegate35_5 = new CellSettingsApplier.Delegate35(FontOptionsBase.smethod_5);
                }
                return (bool) base.method_0(delegate35_5);
            }
            set
            {
                base.method_1(style => style.Font.Strikeout = value);
            }
        }

        public bool Underline
        {
            get
            {
                if (delegate35_3 == null)
                {
                    delegate35_3 = new CellSettingsApplier.Delegate35(FontOptionsBase.smethod_3);
                }
                return (bool) base.method_0(delegate35_3);
            }
            set
            {
                base.method_1(style => style.Font.Underline = value);
            }
        }
    }
}

