namespace Aisino.Framework.Plugin.Core.Controls
{
    using Aisino.Framework.Plugin.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Timers;

    public class TextBoxWait : TextBoxWaterMark
    {
        private int int_0;
        private int int_1;
        public GetListItem myDelegate;
        private string string_1;
        private System.Timers.Timer timer_0;

        public event GetTextEventHandler TextChangedWaitGetText;

        public TextBoxWait()
        {
            
            this.int_1 = 100;
            this.timer_0 = new System.Timers.Timer();
            this.timer_0.Elapsed += new ElapsedEventHandler(this.timer_0_Elapsed);
            this.timer_0.Interval = this.int_1;
            this.timer_0.Enabled = true;
            this.timer_0.AutoReset = false;
            this.timer_0.Stop();
            this.myDelegate = new GetListItem(this.method_0);
        }

        private void method_0()
        {
            this.RaiseEvent(this.string_1);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            try
            {
                if (this.Focused)
                {
                    if (this.int_0 == 0)
                    {
                        this.timer_0.Start();
                    }
                    else
                    {
                        this.timer_0.Stop();
                        this.timer_0.Start();
                    }
                    this.int_0++;
                    this.string_1 = this.Text.Trim();
                }
            }
            catch (Exception exception)
            {
                MessageBoxHelper.Show(exception.ToString());
            }
            finally
            {
                base.OnTextChanged(e);
            }
        }

        protected virtual void OnTextChangedWaitGetText(GetTextEventArgs getTextEventArgs_0)
        {
            if (this.TextChangedWaitGetText != null)
            {
                this.int_0 = 0;
                this.TextChangedWaitGetText(this, getTextEventArgs_0);
            }
        }

        public void RaiseEvent(string string_2)
        {
            GetTextEventArgs args = new GetTextEventArgs(string_2);
            this.OnTextChangedWaitGetText(args);
        }

        private void timer_0_Elapsed(object sender, ElapsedEventArgs e)
        {
            base.Invoke(this.myDelegate);
            this.timer_0.Close();
        }

        public int WaitMilliSeconds
        {
            get
            {
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
            }
        }

        public delegate void GetListItem();
    }
}

