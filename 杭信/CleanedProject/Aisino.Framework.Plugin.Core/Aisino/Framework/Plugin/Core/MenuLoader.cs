namespace Aisino.Framework.Plugin.Core
{
    using Aisino.Framework.Plugin.Core.Plugin;
    using ns12;
    using System;
    using System.Windows.Forms;

    public sealed class MenuLoader
    {
        public MenuLoader()
        {
            
        }

        public static void Load(ToolStripItemCollection toolStripItemCollection_0, object object_0, string string_0)
        {
            Class120 class2 = PlugInTree.smethod_5(string_0);
            if (class2 != null)
            {
                foreach (object obj2 in class2.method_2(object_0, false))
                {
                    if (obj2 is ToolStripItem)
                    {
                        toolStripItemCollection_0.Add((ToolStripItem) obj2);
                    }
                }
            }
        }
    }
}

