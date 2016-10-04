namespace Aisino.Fwkp.Bmgl.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
    internal class Resources
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Resources()
        {
        }

        internal static Bitmap backgroud
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("backgroud", resourceCulture);
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

        internal static Bitmap Duplicate
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Duplicate", resourceCulture);
            }
        }

        internal static Bitmap Failed
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Failed", resourceCulture);
            }
        }

        internal static Bitmap Invalid
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Invalid", resourceCulture);
            }
        }

        internal static Bitmap NoAccess
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("NoAccess", resourceCulture);
            }
        }

        internal static Bitmap Progress
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Progress", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Aisino.Fwkp.Bmgl.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static Bitmap Success
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Success", resourceCulture);
            }
        }

        internal static Bitmap Wait
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Wait", resourceCulture);
            }
        }
    }
}

