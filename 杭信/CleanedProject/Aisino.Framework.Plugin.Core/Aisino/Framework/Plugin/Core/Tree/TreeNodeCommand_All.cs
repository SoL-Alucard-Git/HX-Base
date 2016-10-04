namespace Aisino.Framework.Plugin.Core.Tree
{
    using Aisino.Framework.Plugin.Core.Plugin;
    using Aisino.Framework.Plugin.Core.Util;
    using System;
    using System.Collections;
    using System.Windows.Forms;

    public sealed class TreeNodeCommand_All : TreeNode
    {
        private ArrayList arrayList_0;
        private Function function_0;
        private object object_0;

        internal TreeNodeCommand_All(Function function_1, object object_1, ArrayList arrayList_1)
        {
            
            if (arrayList_1 == null)
            {
                arrayList_1 = new ArrayList();
            }
            this.function_0 = function_1;
            this.object_0 = object_1;
            this.arrayList_0 = arrayList_1;
            string str3 = "";
            function_1.Properties.TryGetValue("tip", out str3);
            base.ToolTipText = StringUtil.Parse((str3 != null) ? str3 : "");
            string str2 = "";
            function_1.Properties.TryGetValue("label", out str2);
            base.Text = StringUtil.Parse((str2 == null) ? str3 : str2);
            base.Name = function_1.Id;
            string str = string.Empty;
            if (function_1.Properties.TryGetValue("_icon", out str))
            {
                base.ImageKey = str;
            }
            else if (function_1.Properties.TryGetValue("icon", out str))
            {
                base.ImageKey = str;
            }
            foreach (object obj2 in arrayList_1)
            {
                if (obj2 is TreeNodeCommand_All)
                {
                    string str4;
                    TreeNodeCommand_All node = (TreeNodeCommand_All) obj2;
                    node.function_0.Properties.TryGetValue("type", out str4);
                    if ((str4 != null) && !str4.Equals("Separator"))
                    {
                        base.Nodes.Add(node);
                    }
                }
            }
        }
    }
}

