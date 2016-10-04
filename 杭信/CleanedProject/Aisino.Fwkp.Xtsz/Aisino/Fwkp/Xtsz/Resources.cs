namespace Aisino.Fwkp.Xtsz
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
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Aisino.Fwkp.Xtsz.Resources", typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static Bitmap 后退
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("后退", resourceCulture);
            }
        }

        internal static Bitmap 前进
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("前进", resourceCulture);
            }
        }

        internal static Bitmap 取消
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("取消", resourceCulture);
            }
        }

        internal static Bitmap 确认
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("确认", resourceCulture);
            }
        }

        internal static Bitmap 无上传设置
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("无上传设置", resourceCulture);
            }
        }

        internal static Bitmap 有上传设置
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("有上传设置", resourceCulture);
            }
        }
    }
}

