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

    public sealed class ToolBarLabel : ToolStripLabel
    {
        private Function function_0;
        private ILog ilog_0;
        private Interface6 interface6_0;
        private object object_0;

        public static  event CoreStartup.SetStatusTextDelegate SetStatusTextEvent;

        internal ToolBarLabel(Function function_1, object object_1)
        {
            
            this.ilog_0 = LogUtil.GetLogger<ToolBarLabel>();
            this.RightToLeft = RightToLeft.Inherit;
            this.object_0 = object_1;
            this.function_0 = function_1;
            try
            {
                this.interface6_0 = (Interface6) function_1.PlugIn.method_9(function_1.Properties["class"]);
                this.interface6_0.imethod_2(object_1);
                function_1.Event_0 += new Function.Delegate39(this.method_1);
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("FRM-000003", new string[] { function_1.Id, exception.ToString() });
            }
            ToolUtil.smethod_5(this, function_1);
            if (function_1 != null)
            {
                this.Enabled = function_1.method_2();
            }
        }

        internal object method_0()
        {
            return this.object_0;
        }

        private void method_1(Function function_1, EventArgs1 eventArgs1_0)
        {
            this.Enabled = function_1.method_2();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (((this.function_0 != null) && this.function_0.HasPermit) && (this.interface6_0 != null))
            {
                this.ilog_0.Info("执行【" + this.function_0.Properties["label"] + "】");
                this.interface6_0.imethod_0();
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

