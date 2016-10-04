namespace Aisino.Framework.Plugin.Core.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [DebuggerNonUserCode, CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    internal class Resources
    {
        private static CultureInfo cultureInfo_0;
        private static ResourceManager resourceManager_0;

        internal Resources()
        {
            
        }

        internal static Bitmap smethod_0()
        {
            return (Bitmap) ResourceManager_0.GetObject("canshushezhi_03", cultureInfo_0);
        }

        internal static Bitmap smethod_1()
        {
            return (Bitmap) ResourceManager_0.GetObject("chazhao_03", cultureInfo_0);
        }

        internal static Bitmap smethod_2()
        {
            return (Bitmap) ResourceManager_0.GetObject("infotext", cultureInfo_0);
        }

        internal static Bitmap smethod_3()
        {
            return (Bitmap) ResourceManager_0.GetObject("jianhang_03", cultureInfo_0);
        }

        internal static Bitmap smethod_4()
        {
            return (Bitmap) ResourceManager_0.GetObject("tuichu_03", cultureInfo_0);
        }

        internal static Bitmap smethod_5()
        {
            return (Bitmap) ResourceManager_0.GetObject("zenghang_03", cultureInfo_0);
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
                    ResourceManager manager = new ResourceManager("Aisino.Framework.Plugin.Core.Properties.Resources", typeof(Resources).Assembly);
                    resourceManager_0 = manager;
                }
                return resourceManager_0;
            }
        }
    }
}

