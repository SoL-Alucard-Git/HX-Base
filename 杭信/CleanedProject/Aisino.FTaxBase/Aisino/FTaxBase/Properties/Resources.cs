namespace Aisino.FTaxBase.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), CompilerGenerated, DebuggerNonUserCode]
    internal class Resources
    {
        private static CultureInfo cultureInfo_0;
        private static ResourceManager resourceManager_0;

        internal Resources()
        {
            
        }

        internal static Bitmap smethod_0()
        {
            return (Bitmap) ResourceManager_0.GetObject("07", cultureInfo_0);
        }

        internal static Bitmap smethod_1()
        {
            return (Bitmap) ResourceManager_0.GetObject("BackImg", cultureInfo_0);
        }

        internal static Bitmap smethod_10()
        {
            return (Bitmap) ResourceManager_0.GetObject("Weit", cultureInfo_0);
        }

        internal static Bitmap smethod_11()
        {
            return (Bitmap) ResourceManager_0.GetObject("xgre", cultureInfo_0);
        }

        internal static Bitmap smethod_2()
        {
            return (Bitmap) ResourceManager_0.GetObject("bg", cultureInfo_0);
        }

        internal static Bitmap smethod_3()
        {
            return (Bitmap) ResourceManager_0.GetObject("blueG", cultureInfo_0);
        }

        internal static Bitmap smethod_4()
        {
            return (Bitmap) ResourceManager_0.GetObject("btn_close", cultureInfo_0);
        }

        internal static Bitmap smethod_5()
        {
            return (Bitmap) ResourceManager_0.GetObject("btn_max", cultureInfo_0);
        }

        internal static Bitmap smethod_6()
        {
            return (Bitmap) ResourceManager_0.GetObject("btn_mini", cultureInfo_0);
        }

        internal static Bitmap smethod_7()
        {
            return (Bitmap) ResourceManager_0.GetObject("btn_restore", cultureInfo_0);
        }

        internal static Icon smethod_8()
        {
            return (Icon) ResourceManager_0.GetObject("logo", cultureInfo_0);
        }

        internal static Bitmap smethod_9()
        {
            return (Bitmap) ResourceManager_0.GetObject("Wait", cultureInfo_0);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo CultureInfo_0
        {
            set
            {
                cultureInfo_0 = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager_0
        {
            get
            {
                if (object.ReferenceEquals(resourceManager_0, null))
                {
                    ResourceManager manager = new ResourceManager("Aisino.FTaxBase.Properties.Resources", typeof(Resources).Assembly);
                    resourceManager_0 = manager;
                }
                return resourceManager_0;
            }
        }
    }
}

