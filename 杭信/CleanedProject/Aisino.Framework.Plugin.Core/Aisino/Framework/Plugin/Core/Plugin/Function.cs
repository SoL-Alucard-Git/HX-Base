namespace Aisino.Framework.Plugin.Core.Plugin
{
    using ns11;
    using ns12;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public sealed class Function
    {
        private bool bool_0;
        private bool bool_1;
        private bool bool_2;
        private Dictionary<string, string> dictionary_0;
        private Aisino.Framework.Plugin.Core.Plugin.PlugIn plugIn_0;
        private string string_0;

        internal event Delegate39 Event_0;

        internal event Delegate40 Event_1;

        internal Function(Aisino.Framework.Plugin.Core.Plugin.PlugIn plugIn_1, string string_1, Dictionary<string, string> properties, bool bool_3)
        {
            
            this.plugIn_0 = plugIn_1;
            this.string_0 = string_1;
            this.dictionary_0 = properties;
            this.bool_2 = bool_3;
            this.bool_0 = bool_3;
        }

        internal bool method_0()
        {
            return this.bool_1;
        }

        internal void method_1(bool bool_3)
        {
            this.bool_1 = bool_3;
            this.Event_1(this, null);
        }

        internal bool method_2()
        {
            return this.bool_0;
        }

        internal void method_3(bool bool_3)
        {
            if (this.bool_2)
            {
                this.bool_0 = bool_3;
                this.Event_0(this, null);
            }
        }

        internal string method_4()
        {
            if (!this.Properties.ContainsKey("insertAfter"))
            {
                return "";
            }
            return this.Properties["insertAfter"];
        }

        internal void method_5(string string_1)
        {
            this.Properties["insertAfter"] = string_1;
        }

        internal string method_6()
        {
            if (!this.Properties.ContainsKey("insertBefore"))
            {
                return "";
            }
            return this.Properties["insertBefore"];
        }

        internal void method_7(string string_1)
        {
            this.Properties["insertBefore"] = string_1;
        }

        internal object method_8(object object_0, ArrayList arrayList_0)
        {
            Interface3 interface2;
            if (!PlugInTree.smethod_1().TryGetValue(this.string_0, out interface2))
            {
                throw new Exception("功能代理 " + this.string_0 + " 未定义!");
            }
            return interface2.imethod_0(object_0, this, arrayList_0);
        }

        internal object method_9(object object_0, ArrayList arrayList_0)
        {
            Interface3 interface2;
            if (!PlugInTree.smethod_1().TryGetValue("TreeNode_All", out interface2))
            {
                throw new Exception("功能代理 TreeNode_All 未定义!");
            }
            return interface2.imethod_0(object_0, this, arrayList_0);
        }

        public override string ToString()
        {
            return string.Format("[功能: id = {0}, plugIn={1}]", this.Id, this.plugIn_0.method_0());
        }

        public bool HasPermit
        {
            get
            {
                return this.bool_2;
            }
        }

        internal string Id
        {
            get
            {
                return this.Properties["id"];
            }
        }

        internal Aisino.Framework.Plugin.Core.Plugin.PlugIn PlugIn
        {
            get
            {
                return this.plugIn_0;
            }
        }

        internal Dictionary<string, string> Properties
        {
            get
            {
                return this.dictionary_0;
            }
            set
            {
                this.dictionary_0 = value;
            }
        }

        internal delegate void Delegate39(Function obj, EventArgs1 eve);

        internal delegate void Delegate40(Function obj, EventArgs2 eve);
    }
}

