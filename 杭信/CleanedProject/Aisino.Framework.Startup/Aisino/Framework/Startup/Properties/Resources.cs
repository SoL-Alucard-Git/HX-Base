namespace Aisino.Framework.Startup.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [DebuggerNonUserCode, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), CompilerGenerated]
    internal class Resources
    {
        private static CultureInfo cultureInfo_0;
        private static ResourceManager resourceManager_0;

        internal Resources()
        {
            
        }

        internal static Bitmap smethod_0()
        {
            return (Bitmap) ResourceManager_0.GetObject("certpasswrod", cultureInfo_0);
        }

        internal static Bitmap smethod_1()
        {
            return (Bitmap) ResourceManager_0.GetObject("login_bg", cultureInfo_0);
        }

        internal static Bitmap smethod_2()
        {
            return (Bitmap) ResourceManager_0.GetObject("login_bg_png", cultureInfo_0);
        }

        internal static Bitmap smethod_3()
        {
            return (Bitmap) ResourceManager_0.GetObject("login_btn", cultureInfo_0);
        }

        internal static Bitmap smethod_4()
        {
            return (Bitmap) ResourceManager_0.GetObject("login_btn_mouseon", cultureInfo_0);
        }

        internal static Bitmap smethod_5()
        {
            return (Bitmap) ResourceManager_0.GetObject("login_setup", cultureInfo_0);
        }

        internal static Bitmap smethod_6()
        {
            return (Bitmap) ResourceManager_0.GetObject("login_setup_mouseon", cultureInfo_0);
        }

        internal static Bitmap smethod_7()
        {
            return (Bitmap) ResourceManager_0.GetObject("setup", cultureInfo_0);
        }

        internal static Bitmap smethod_8()
        {
            return (Bitmap) ResourceManager_0.GetObject("username", cultureInfo_0);
        }

        internal static Bitmap smethod_9()
        {
            return (Bitmap) ResourceManager_0.GetObject("userpassword", cultureInfo_0);
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
                    ResourceManager manager = new ResourceManager("Aisino.Framework.Startup.Properties.Resources", typeof(Resources).Assembly);
                    resourceManager_0 = manager;
                }
                return resourceManager_0;
            }
        }
    }
}

