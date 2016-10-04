namespace Aisino.Fwkp.DataMigrationTool.Properties
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
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Aisino.Fwkp.DataMigrationTool.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static Bitmap 复制
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("复制", resourceCulture);
            }
        }

        internal static Bitmap 取消
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("取消", resourceCulture);
            }
        }

        internal static Bitmap 数据库选择
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("数据库选择", resourceCulture);
            }
        }

        internal static Bitmap 完成
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("完成", resourceCulture);
            }
        }
    }
}

