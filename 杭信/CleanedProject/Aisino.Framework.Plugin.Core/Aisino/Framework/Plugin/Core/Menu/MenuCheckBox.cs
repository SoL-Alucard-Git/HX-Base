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

    public sealed class MenuCheckBox : ToolStripMenuItem
    {
        private Function function_0;
        private ILog ilog_0;
        private Interface5 interface5_0;
        private object object_0;

        public static  event CoreStartup.SetStatusTextDelegate SetStatusTextEvent;

        internal MenuCheckBox(Function function_1, object object_1)
        {
            
            this.ilog_0 = LogUtil.GetLogger<MenuCheckBox>();
            this.RightToLeft = RightToLeft.Inherit;
            this.object_0 = object_1;
            this.function_0 = function_1;
            this.method_0();
            ToolUtil.smethod_5(this, function_1);
            this.method_1();
        }

        private void method_0()
        {
            if (this.interface5_0 == null)
            {
                try
                {
                    this.interface5_0 = (Interface5) this.function_0.PlugIn.method_9(this.function_0.Properties["class"]);
                    this.interface5_0.imethod_2(this.object_0);
                    this.function_0.Event_0 += new Function.Delegate39(this.method_2);
                    this.function_0.Event_1 += new Function.Delegate40(this.method_3);
                }
                catch (Exception exception)
                {
                    MessageManager.ShowMsgBox("FRM-000002", new string[] { this.function_0.Id, exception.ToString() });
                }
            }
        }

        private void method_1()
        {
            if ((this.function_0 != null) && (this.interface5_0 != null))
            {
                if (this.function_0.Properties.ContainsKey("checked"))
                {
                    base.Checked = string.Equals(this.function_0.Properties["checked"], bool.TrueString, StringComparison.OrdinalIgnoreCase);
                    this.interface5_0.imethod_5(base.Checked);
                }
                if (this.function_0 != null)
                {
                    this.Enabled = this.function_0.method_2();
                }
            }
        }

        private void method_2(Function function_1, EventArgs1 eventArgs1_0)
        {
            this.Enabled = function_1.method_2();
        }

        private void method_3(Function function_1, EventArgs2 eventArgs2_0)
        {
            base.Checked = function_1.method_0();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if ((this.function_0 != null) && this.function_0.HasPermit)
            {
                this.ilog_0.Info("执行【" + this.function_0.Properties["label"] + "】");
                this.interface5_0.imethod_0();
                base.Checked = !base.Checked;
                this.interface5_0.imethod_5(base.Checked);
                ToolUtil.smethod_6(this.function_0.Id, base.Checked);
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

