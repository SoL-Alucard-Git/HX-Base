namespace Aisino.Fwkp.Sjbf
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
    internal class Resource
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Resource()
        {
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
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Aisino.Fwkp.Sjbf.Resource", typeof(Resource).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static Bitmap 目录
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("目录", resourceCulture);
            }
        }

        internal static Bitmap 退出
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("退出", resourceCulture);
            }
        }

        internal static Bitmap 文件
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("文件", resourceCulture);
            }
        }
    }
}

