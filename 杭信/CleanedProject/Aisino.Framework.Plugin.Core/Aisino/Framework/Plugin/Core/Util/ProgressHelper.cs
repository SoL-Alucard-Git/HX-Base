namespace Aisino.Framework.Plugin.Core.Util
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class ProgressHelper
    {
        private bool bool_0;
        private static bool bool_1;
        private Point point_0;
        private Thread thread_0;
        private Thread thread_1;
        private WaitForm waitForm_0;

        public event EventHandler DoBackWork;

        static ProgressHelper()
        {
            
        }

        public ProgressHelper()
        {
            
            this.bool_0 = true;
        }

        public ProgressHelper(object object_0)
        {
            
            this.bool_0 = true;
            this.point_0 = this.method_0(object_0);
        }

        public void Close()
        {
            if (this.thread_0 != null)
            {
                this.bool_0 = false;
                try
                {
                    if (this.thread_1 != null)
                    {
                        this.thread_1.Abort();
                        this.thread_1.Join();
                    }
                    this.thread_0.Abort();
                    this.thread_0.Join();
                    this.waitForm_0.Dispose();
                }
                catch (Exception)
                {
                }
            }
        }

        public void CloseCycle()
        {
            this.Close();
        }

        private Point method_0(object object_0)
        {
            int width;
            int height;
            int num = 350;
            int num2 = 0x87;
            int x = 0;
            int y = 0;
            int num5 = 100;
            int num6 = 100;
            if (object_0 is Form)
            {
                Form form = object_0 as Form;
                width = form.Width;
                height = form.Height;
                num5 = form.Location.X;
                num6 = form.Location.Y;
            }
            else
            {
                Screen primaryScreen = Screen.PrimaryScreen;
                width = primaryScreen.Bounds.Width;
                height = primaryScreen.Bounds.Height;
            }
            if (width > num)
            {
                x = num5 + ((width - num) / 2);
            }
            else
            {
                x = num5;
            }
            if (height > num2)
            {
                y = num6 + ((height - num2) / 2);
            }
            else
            {
                y = num6;
            }
            return new Point(x, y);
        }

        [CompilerGenerated]
        private void method_1()
        {
            int num = 0;
            while (this.bool_0)
            {
                this.SetFormProgress(num);
                num++;
                Thread.Sleep(100);
                if (num == 100)
                {
                    num = 0;
                }
            }
        }

        protected virtual void OnDoBackWork(object sender, EventArgs e)
        {
            if (this.DoBackWork != null)
            {
                this.DoBackWork(sender, e);
            }
        }

        public void SetCycleWait()
        {
            if (this.thread_1 != null)
            {
                this.thread_1.Abort();
                this.thread_1.Join();
            }
            Thread.Sleep(100);
            this.thread_1 = new Thread(new ThreadStart(this.method_1));
            this.thread_1.Start();
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

        public void SetFormLocation(int int_0, int int_1)
        {
            this.waitForm_0.Location = new Point(int_0, int_1);
        }

        public void SetFormProgress(int int_0)
        {
            if (this.waitForm_0 != null)
            {
                try
                {
                    this.waitForm_0.SetProgressValue(int_0);
                }
                catch (Exception)
                {
                }
            }
        }

        public void SetFormProgressTotal(int int_0)
        {
            if (this.waitForm_0 != null)
            {
                try
                {
                    this.waitForm_0.SetProgressMaxValue(int_0);
                }
                catch (Exception)
                {
                }
            }
        }

        public void Show(string string_0)
        {
            string title = string_0;
            if (this.thread_0 != null)
            {
                this.thread_0.Abort();
            }
            //thread_0 = new Thread(delegate {
            //    this.bool_0 = true;
            //    this.waitForm_0 = new WaitForm();
            //    if (title != null)
            //    {
            //        this.waitForm_0.Text = title;
            //        this.waitForm_0.SetFormPos(this.point_0);
            //    }
            //    Application.Run(this.waitForm_0);
            //});
            this.thread_0.Start();
            Thread.Sleep(200);
        }

        public void Show(string string_0, string string_1)
        {
            this.Show(string_0);
            this.SetFormCaption(string_1);
        }

        public void StartCycle()
        {
            this.SetCycleWait();
        }

        public void StartProgress(bool bool_2)
        {
            WaitForm progressDialog;
            if (!bool_1)
            {
                progressDialog = new WaitForm();
                progressDialog.SetIndeterminate(bool_2);
                //new Thread(delegate {
                //    bool_1 = true;
                //    this.OnDoBackWork(progressDialog, EventArgs.Empty);
                //    progressDialog.CloseProgress();
                //    bool_1 = false;
                //}).Start();
                progressDialog.ShowDialog();
            }
        }

        public void UpdateCycleText(string string_0)
        {
            this.SetFormCaption(string_0);
        }
    }
}

