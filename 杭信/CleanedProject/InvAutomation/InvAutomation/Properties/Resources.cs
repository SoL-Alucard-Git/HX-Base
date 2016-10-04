namespace InvAutomation.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode]
    internal class Resources
    {
        private static object object_0;
        private static ResourceManager resourceManager_0;

        internal Resources()
        {
            
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo CultureInfo_0
        {
            set
            {
                object_0 = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager_0
        {
            get
            {
                if (object.ReferenceEquals(resourceManager_0, null))
                {
                    ResourceManager manager = new ResourceManager("InvAutomation.Properties.Resources", typeof(Resources).Assembly);
                    resourceManager_0 = manager;
                }
                return resourceManager_0;
            }
        }
    }
}

