namespace Aisino.Fwkp.Fpkj
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode]
    internal class Resource1
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Resource1()
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
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Aisino.Fwkp.Fpkj.Resource1", typeof(Resource1).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static Icon 发票管理_发票管理
        {
            get
            {
                return (Icon) ResourceManager.GetObject("发票管理_发票管理", resourceCulture);
            }
        }

        internal static Icon 发票管理_发票修复
        {
            get
            {
                return (Icon) ResourceManager.GetObject("发票管理_发票修复", resourceCulture);
            }
        }

        internal static Icon 发票管理_普通发票作废
        {
            get
            {
                return (Icon) ResourceManager.GetObject("发票管理_普通发票作废", resourceCulture);
            }
        }

        internal static Icon 发票管理_未开发票作废
        {
            get
            {
                return (Icon) ResourceManager.GetObject("发票管理_未开发票作废", resourceCulture);
            }
        }

        internal static Icon 发票管理_已开发票查询
        {
            get
            {
                return (Icon) ResourceManager.GetObject("发票管理_已开发票查询", resourceCulture);
            }
        }

        internal static Icon 发票管理_已开发票作废
        {
            get
            {
                return (Icon) ResourceManager.GetObject("发票管理_已开发票作废", resourceCulture);
            }
        }
    }
}

