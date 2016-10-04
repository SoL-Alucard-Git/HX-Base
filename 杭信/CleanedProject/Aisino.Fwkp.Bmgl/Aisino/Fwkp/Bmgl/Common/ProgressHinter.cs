namespace Aisino.Fwkp.Bmgl.Common
{
    using Aisino.Fwkp.Bmgl.Forms;
    using log4net;
    using System;
    using System.Threading;
    using Framework.Plugin.Core.Util;

    public sealed class ProgressHinter
    {
        private ILog _Loger = LogUtil.GetLogger<ProgressHinter>();
        private static readonly ProgressHinter instance = new ProgressHinter();
        private string MSG = string.Empty;
        private MsgForm msgForm;
        private Thread waitThread;

        private ProgressHinter()
        {
        }

        public void CloseCycle()
        {
            try
            {
                Thread.Sleep(500);
                if (this.msgForm != null)
                {
                    this.msgForm.SetFormClose();
                    this.msgForm = null;
                }
            }
            catch (Exception exception)
            {
                this._Loger.Error(exception.Message);
            }
        }

        private void DoShow()
        {
            try
            {
                this.msgForm = new MsgForm(this.MSG);
                this.msgForm.ShowDialog();
            }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
            }
            catch (Exception exception)
            {
                this._Loger.Error(exception.Message);
            }
        }

        public static ProgressHinter GetInstance()
        {
            return instance;
        }

        public void SetMsg(string msg)
        {
            this.MSG = msg;
        }

        public void StartCycle()
        {
            try
            {
                if (this.msgForm != null)
                {
                    this.CloseCycle();
                }
                this.waitThread = new Thread(new ThreadStart(this.DoShow));
                this.waitThread.IsBackground = true;
                this.waitThread.Start();
            }
            catch (Exception exception)
            {
                this._Loger.Error(exception.Message);
            }
        }
    }
}

