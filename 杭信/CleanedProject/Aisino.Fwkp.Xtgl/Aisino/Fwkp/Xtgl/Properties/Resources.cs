namespace Aisino.Fwkp.Xtgl.Properties
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

        internal static Bitmap accept
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("accept", resourceCulture);
            }
        }

        internal static Bitmap cancel
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("cancel", resourceCulture);
            }
        }

        internal static Bitmap change_password
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("change_password", resourceCulture);
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

        internal static Bitmap group_add
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("group_add", resourceCulture);
            }
        }

        internal static Bitmap group_delete
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("group_delete", resourceCulture);
            }
        }

        internal static Bitmap group_edit
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("group_edit", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Aisino.Fwkp.Xtgl.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static Bitmap user_add
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("user_add", resourceCulture);
            }
        }

        internal static Bitmap user_delete
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("user_delete", resourceCulture);
            }
        }

        internal static Bitmap user_edit
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("user_edit", resourceCulture);
            }
        }

        internal static Bitmap 删除注册文件
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("删除注册文件", resourceCulture);
            }
        }
    }
}

