namespace Aisino.Fwkp.Publish
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
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Resources()
        {
        }

        internal static Bitmap bianmazuguanli_03
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("bianmazuguanli_03", resourceCulture);
            }
        }

        internal static Bitmap chakanmingxi_03
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("chakanmingxi_03", resourceCulture);
            }
        }

        internal static Bitmap chazhao_03
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("chazhao_03", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Aisino.Fwkp.Publish.Resources", typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}

