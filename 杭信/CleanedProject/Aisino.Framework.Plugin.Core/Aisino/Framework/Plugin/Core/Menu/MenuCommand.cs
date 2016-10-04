namespace Aisino.Framework.Plugin.Core.Menu
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

    public sealed class MenuCommand : ToolStripMenuItem
    {
        private Function function_0;
        private ILog ilog_0;
        private Interface2 interface2_0;
        private object object_0;

        public static  event CoreStartup.SetStatusTextDelegate SetStatusTextEvent;

        internal MenuCommand(Function function_1, object object_1)
        {
            
            this.ilog_0 = LogUtil.GetLogger<MenuCommand>();
            this.RightToLeft = RightToLeft.Inherit;
            this.object_0 = object_1;
            this.function_0 = function_1;
            this.method_0();
            ToolUtil.smethod_5(this, function_1);
            if (function_1 != null)
            {
                this.Enabled = function_1.method_2();
            }
        }

        private void method_0()
        {
            try
            {
                this.interface2_0 = (Interface2) this.function_0.PlugIn.method_9(this.function_0.Properties["class"]);
                if (this.interface2_0 != null)
                {
                    this.interface2_0.imethod_2(this.object_0);
                }
                this.function_0.Event_0 += new Function.Delegate39(this.method_1);
            }
            catch (Exception exception)
            {
                MessageManager.ShowMsgBox("FRM-000002", new string[] { this.function_0.Id, exception.ToString() });
            }
        }

        private void method_1(Function function_1, EventArgs1 eventArgs1_0)
        {
            this.Enabled = function_1.method_2();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (((this.function_0 != null) && this.function_0.HasPermit) && (this.Command != null))
            {
                this.ilog_0.Info("执行【" + this.function_0.Properties["label"] + "】");
                this.Command.imethod_0();
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

        private Interface2 Command
        {
            get
            {
                if (this.interface2_0 == null)
                {
                    this.method_0();
                }
                return this.interface2_0;
            }
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

