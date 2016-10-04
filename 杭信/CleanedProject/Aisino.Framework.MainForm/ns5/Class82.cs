namespace ns5
{
    using Aisino.Framework.Plugin.Core.Menu;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    internal class Class82
    {
        public Class82()
        {
            
        }

        internal static void smethod_0(ToolStrip toolStrip_0, List<string> list_0, List<string> menuList)
        {
            menuList.Clear();
            foreach (ToolStripItem item in toolStrip_0.Items)
            {
                if (item is ToolStripMenuItem)
                {
                    if (((ToolStripMenuItem) item).DropDownItems.Count > 0)
                    {
                        smethod_1((ToolStripMenuItem) item, list_0, menuList);
                    }
                    else
                    {
                        MenuCommand command = item as MenuCommand;
                        if ((command != null) && list_0.Contains(command.FunctionID))
                        {
                            menuList.Add(command.FunctionID);
                        }
                    }
                }
            }
        }

        private static void smethod_1(ToolStripMenuItem toolStripMenuItem_0, List<string> list_0, List<string> menuList)
        {
            foreach (ToolStripItem item in toolStripMenuItem_0.DropDownItems)
            {
                if (item is ToolStripMenuItem)
                {
                    if (((ToolStripMenuItem) item).DropDownItems.Count > 0)
                    {
                        smethod_1((ToolStripMenuItem) item, list_0, menuList);
                    }
                    else
                    {
                        MenuCommand command = item as MenuCommand;
                        if ((command != null) && list_0.Contains(command.FunctionID))
                        {
                            menuList.Add(command.FunctionID);
                        }
                    }
                }
            }
        }
    }
}

