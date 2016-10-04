namespace Aisino.Fwkp.Bmgl
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
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Aisino.Fwkp.Bmgl.Resources", typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static Bitmap 保存
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("保存", resourceCulture);
            }
        }

        internal static Bitmap 打印
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("打印", resourceCulture);
            }
        }

        internal static Bitmap 导出
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("导出", resourceCulture);
            }
        }

        internal static Bitmap 导入
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("导入", resourceCulture);
            }
        }

        internal static Bitmap 格式
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("格式", resourceCulture);
            }
        }

        internal static Bitmap 取消
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("取消", resourceCulture);
            }
        }

        internal static Bitmap 删除
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("删除", resourceCulture);
            }
        }

        internal static Bitmap 搜索
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("搜索", resourceCulture);
            }
        }

        internal static Bitmap 统计
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("统计", resourceCulture);
            }
        }

        internal static Bitmap 退出
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("退出", resourceCulture);
            }
        }

        internal static Bitmap 修改
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("修改", resourceCulture);
            }
        }

        internal static Bitmap 增加
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("增加", resourceCulture);
            }
        }
    }
}

