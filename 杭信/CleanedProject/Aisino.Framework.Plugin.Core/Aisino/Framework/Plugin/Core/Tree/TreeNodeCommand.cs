namespace Aisino.Framework.Plugin.Core.Tree
{
    using Aisino.Framework.Plugin.Core.Plugin;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using ns10;
    using System;
    using System.Collections;
    using System.Windows.Forms;

    public sealed class TreeNodeCommand : TreeNode
    {
        private ArrayList arrayList_0;
        internal Function function_0;
        private ILog ilog_0;
        private Interface2 interface2_0;
        private object object_0;

        internal TreeNodeCommand(Function function_1, object object_1, ArrayList arrayList_1)
        {
            
            this.ilog_0 = LogUtil.GetLogger<TreeNodeCommand>();
            if (arrayList_1 == null)
            {
                arrayList_1 = new ArrayList();
            }
            this.function_0 = function_1;
            this.object_0 = object_1;
            this.arrayList_0 = arrayList_1;
            string str = string.Empty;
            if (function_1.Properties.TryGetValue("icon", out str))
            {
                base.ImageKey = str;
            }
            base.Name = function_1.Id;
            ToolUtil.SetText(this, function_1);
            foreach (object obj2 in arrayList_1)
            {
                if (obj2 is TreeNodeCommand)
                {
                    base.Nodes.Add((TreeNodeCommand) obj2);
                }
            }
        }

        public void onClick()
        {
            string str;
            if ((this.interface2_0 == null) && this.function_0.Properties.TryGetValue("class", out str))
            {
                this.interface2_0 = (Interface2) this.function_0.PlugIn.method_9(str);
                this.interface2_0.imethod_2(this.object_0);
            }
            if (this.interface2_0 != null)
            {
                this.ilog_0.Info("执行【" + this.function_0.Properties["label"] + "】");
                this.interface2_0.imethod_0();
            }
        }
    }
}

