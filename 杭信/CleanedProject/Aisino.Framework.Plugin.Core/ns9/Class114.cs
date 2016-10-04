namespace ns9
{
    using Aisino.Framework.Plugin.Core.Plugin;
    using Aisino.Framework.Plugin.Core.Util;
    using ns12;
    using System;
    using System.Collections;
    using System.Windows.Forms;

    internal sealed class Class114 : ToolStripSplitButton
    {
        private ArrayList arrayList_0;
        private Function function_0;
        private object object_0;

        internal Class114(Function function_1, object object_1, ArrayList arrayList_1)
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
        }

        private void method_0(Function function_1, EventArgs1 eventArgs1_0)
        {
            this.Enabled = function_1.method_2();
        }

        private void method_1()
        {
            base.DropDownItems.Clear();
            foreach (object obj2 in this.arrayList_0)
            {
                if (obj2 is ToolStripItem)
                {
                    base.DropDownItems.Add((ToolStripItem) obj2);
                }
            }
        }

        protected override void OnDropDownShow(EventArgs e)
        {
            if (this.function_0 != null)
            {
                this.method_1();
            }
            base.OnDropDownShow(e);
        }
    }
}

