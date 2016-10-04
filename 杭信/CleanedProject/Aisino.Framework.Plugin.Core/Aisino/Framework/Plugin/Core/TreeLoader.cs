namespace Aisino.Framework.Plugin.Core
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Controls.OutlookBar;
    using Aisino.Framework.Plugin.Core.Plugin;
    using Aisino.Framework.Plugin.Core.Tree;
    using Aisino.Framework.Plugin.Core.Util;
    using ns12;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Windows.Forms;

    public sealed class TreeLoader
    {
        public TreeLoader()
        {
            
        }

        public static void Load(AisinoTVW aisinoTVW_0, object object_0, string string_0, bool bool_0)
        {
            if (aisinoTVW_0.ImageList != null)
            {
                foreach (string str in ResourceUtil.dictionary_0.Keys)
                {
                    if (ResourceUtil.GetBitmap(str) != null)
                    {
                        aisinoTVW_0.ImageList.Images.Add(str, ResourceUtil.GetBitmap(str));
                    }
                }
            }
            Class120 class2 = PlugInTree.smethod_5(string_0);
            if (class2 != null)
            {
                foreach (object obj2 in class2.method_2(object_0, bool_0))
                {
                    if (!bool_0 && (obj2 is TreeNodeCommand))
                    {
                        aisinoTVW_0.Nodes.Add((TreeNodeCommand) obj2);
                    }
                    else if (bool_0 && (obj2 is TreeNodeCommand_All))
                    {
                        aisinoTVW_0.Nodes.Add((TreeNodeCommand_All) obj2);
                    }
                }
            }
        }

        public static void Load(NavigatePage navigatePage_0, object object_0, string string_0, bool bool_0)
        {
            Class120 class2 = PlugInTree.smethod_5(string_0);
            if (class2 != null)
            {
                NavigateMenuNode node = smethod_0(class2.method_2(object_0, bool_0));
                if (((node != null) && (node.Node != null)) && (node.Node.Count > 0))
                {
                    navigatePage_0.Nodes = node;
                }
            }
        }

        public static void Load(Aisino.Framework.Plugin.Core.Controls.OutlookBar.OutlookBar outlookBar_0, object object_0, string string_0, bool bool_0)
        {
            Class120 class2 = PlugInTree.smethod_5(string_0);
            if (class2 != null)
            {
                OutlookBarNodeCollection nodes = smethod_2(class2.method_2(object_0, bool_0));
                for (int i = nodes.Count - 1; i >= 0; i--)
                {
                    outlookBar_0.Nodes.Add(nodes[i]);
                }
            }
        }

        private static NavigateMenuNode smethod_0(ArrayList arrayList_0)
        {
            NavigateMenuNode node = new NavigateMenuNode();
            foreach (object obj2 in arrayList_0)
            {
                if (obj2 is TreeNodeCommand)
                {
                    TreeNodeCommand command = obj2 as TreeNodeCommand;
                    if (command != null)
                    {
                        Bitmap bitmap = ResourceUtil.GetBitmap(command.ImageKey);
                        if (bitmap == null)
                        {
                            bitmap = Class131.smethod_38();
                        }
                        NavigateMenuNode node2 = new NavigateMenuNode(command.Text, command.function_0.Id, bitmap, command) {
                            ImageKey = command.ImageKey
                        };
                        if (command.Nodes.Count > 0)
                        {
                            smethod_1(node2, command);
                        }
                        node.Node.Add(node2);
                    }
                }
            }
            return node;
        }

        private static void smethod_1(NavigateMenuNode navigateMenuNode_0, TreeNode treeNode_0)
        {
            foreach (TreeNodeCommand command in treeNode_0.Nodes)
            {
                Bitmap bitmap = ResourceUtil.GetBitmap(command.ImageKey);
                if (bitmap == null)
                {
                    bitmap = Class131.smethod_38();
                }
                NavigateMenuNode node = new NavigateMenuNode(command.Text, command.function_0.Id, bitmap, command) {
                    ImageKey = command.ImageKey
                };
                if (command.Nodes.Count > 0)
                {
                    smethod_1(node, command);
                }
                navigateMenuNode_0.Node.Add(node);
            }
        }

        private static OutlookBarNodeCollection smethod_2(ArrayList arrayList_0)
        {
            OutlookBarNodeCollection nodes = new OutlookBarNodeCollection();
            foreach (object obj2 in arrayList_0)
            {
                TreeNodeCommand command = obj2 as TreeNodeCommand;
                if (command != null)
                {
                    OutlookBarNode node = new OutlookBarNode(command.Text, true, false, 0, new Font("微软雅黑", 16f), 50);
                    smethod_3(node, command);
                    nodes.Add(node);
                }
            }
            return nodes;
        }

        private static void smethod_3(OutlookBarNode outlookBarNode_0, TreeNode treeNode_0)
        {
            for (int i = treeNode_0.Nodes.Count; i > 0; i--)
            {
                if (treeNode_0.Nodes[i - 1].Nodes.Count == 0)
                {
                    string id = ((TreeNodeCommand) treeNode_0.Nodes[i - 1]).function_0.Id;
                    outlookBarNode_0.NodeAdd("", treeNode_0.Nodes[i - 1].Text, id, (TreeNodeCommand) treeNode_0.Nodes[i - 1]);
                }
                else
                {
                    OutlookBarNode node = new OutlookBarNode(treeNode_0.Nodes[i - 1].Text, false, true, outlookBarNode_0.IconIdent + 20, new Font("微软雅黑", 12f), 40);
                    smethod_3(node, (TreeNodeCommand) treeNode_0.Nodes[i - 1]);
                    outlookBarNode_0.SubNodeAdd(node);
                }
            }
        }
    }
}

