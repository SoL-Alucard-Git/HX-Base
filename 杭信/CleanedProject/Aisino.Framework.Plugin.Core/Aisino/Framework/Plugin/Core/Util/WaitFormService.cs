namespace Aisino.Framework.Plugin.Core.Util
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class WaitFormService
    {
        private static readonly object object_0;
        private Thread thread_0;
        private WaitForm waitForm_0;
        private static WaitFormService waitFormService_0;

        static WaitFormService()
        {
            
            object_0 = new object();
        }

        private WaitFormService()
        {
            
        }

        public void CloseForm()
        {
            if (this.thread_0 != null)
            {
                try
                {
                    this.thread_0.Abort();
                    this.thread_0.Join();
                    this.waitForm_0.Dispose();
                }
                catch (Exception)
                {
                }
            }
        }

        public static void CloseWaitForm()
        {
            Instance.CloseForm();
        }

        public void CreateForm()
        {
            if (this.thread_0 != null)
            {
                try
                {
                    this.thread_0.Abort();
                }
                catch (Exception)
                {
                }
            }
            this.thread_0 = new Thread(new ThreadStart(this.method_0));
            this.thread_0.Start();
        }

        public static void CreateWaitForm()
        {
            Instance.CreateForm();
        }

        [CompilerGenerated]
        private void method_0()
        {
            this.waitForm_0 = new WaitForm();
            Application.Run(this.waitForm_0);
        }

        public void SetFormCaption(string string_0)
        {
            if (this.waitForm_0 != null)
            {
                try
                {
                    this.waitForm_0.SetText(string_0);
                }
                catch (Exception)
                {
                }
            }
        }

        public static void SetWaitFormCaption(string string_0)
        {
            Instance.SetFormCaption(string_0);
        }

        public static WaitFormService Instance
        {
            get
            {
                if (waitFormService_0 == null)
                {
                    lock (object_0)
                    {
                        if (waitFormService_0 == null)
                        {
                            waitFormService_0 = new WaitFormService();
                        }
                    }
                }
                return waitFormService_0;
            }
        }
    }
}

