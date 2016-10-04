namespace Aisino.Framework.Plugin.Core.Menu
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Plugin;
    using Aisino.Framework.Plugin.Core.Util;
    using ns12;
    using System;
    using System.Collections;
    using System.Threading;
    using System.Windows.Forms;

    public sealed class MenuLabel : ToolStripMenuItem
    {
        private ArrayList arrayList_0;
        private Function function_0;
        private object object_0;

        public static  event CoreStartup.SetStatusTextDelegate SetStatusTextEvent;

        internal MenuLabel(Function function_1, object object_1, ArrayList arrayList_1)
        {
            
            if (arrayList_1 == null)
            {
                arrayList_1 = new ArrayList();
            }
            this.function_0 = function_1;
            this.object_0 = object_1;
            this.arrayList_0 = arrayList_1;
            this.RightToLeft = RightToLeft.Inherit;
            ToolUtil.smethod_5(this, function_1);
            if (function_1 != null)
            {
                this.Enabled = function_1.method_2();
            }
            function_1.Event_0 += new Function.Delegate39(this.method_0);
            foreach (object obj2 in arrayList_1)
            {
                if (obj2 is ToolStripItem)
                {
                    base.DropDownItems.Add((ToolStripItem) obj2);
                }
            }
        }

        private void method_0(Function function_1, EventArgs1 eventArgs1_0)
        {
            this.Enabled = function_1.method_2();
        }

        protected override void OnMouseHover(EventArgs e)
        {
            if (SetStatusTextEvent != null)
            {
                string str = "";
                if (!this.function_0.Properties.TryGetValue("commentary", out str))
                {
                    str = this.function_0.Properties["label"];
                }
                SetStatusTextEvent(str);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            SetStatusTextEvent("");
        }

        public string FunctionID
        {
            get
            {
                return this.function_0.Id;
            }
        }
    }
}

