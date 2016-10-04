namespace Aisino.Framework.Plugin.Core.Toolbar
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Plugin;
    using Aisino.Framework.Plugin.Core.Util;
    using log4net;
    using ns10;
    using ns12;
    using System;
    using System.Threading;
    using System.Windows.Forms;

    public sealed class ToolBarCommand : ToolStripMenuItem
    {
        private Function function_0;
        private ILog ilog_0;
        private Interface2 interface2_0;
        private object object_0;

        public static  event CoreStartup.SetStatusTextDelegate SetStatusTextEvent;

        internal ToolBarCommand(Function function_1, object object_1)
        {
            
            this.ilog_0 = LogUtil.GetLogger<ToolBarCommand>();
            this.RightToLeft = RightToLeft.Inherit;
            this.object_0 = object_1;
            this.function_0 = function_1;
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

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (this.interface2_0 == null)
            {
                this.interface2_0 = (Interface2) this.function_0.PlugIn.method_9(this.function_0.Properties["class"]);
            }
            if (((this.function_0 != null) && this.function_0.HasPermit) && (this.interface2_0 != null))
            {
                this.ilog_0.Info("执行【" + this.function_0.Properties["label"] + "】");
                this.interface2_0.imethod_0();
            }
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
    }
}

