namespace Aisino.FTaxBase
{
    using ns2;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public static class MessageShowTT
    {
        private static MsgForm msgForm_0;
        private static Thread thread_0;

        static MessageShowTT()
        {
            
            msgForm_0 = new MsgForm();
            msgForm_0.CloseBtnVisible = false;
            thread_0 = new Thread(new ThreadStart(MessageShowTT.smethod_2));
            thread_0.Start();
        }

        public static bool ConfirmDlg(string string_0)
        {
            return ConfirmDlg(string_0, "金税设备确认");
        }

        public static bool ConfirmDlg(string string_0, string string_1)
        {
            Class20.smethod_3(string_0);
            return (MessageBoxSys.Show(string_0, string_1, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
        }

        public static void MsgWait()
        {
            try
            {
                if (((msgForm_0 != null) && (thread_0 != null)) && ((thread_0.ThreadState == ThreadState.Running) && msgForm_0.InvokeRequired))
                {
                    msgForm_0.Invoke(new Delegate5(MessageShowTT.smethod_1), new object[] { "" });
                }
            }
            catch (Exception exception)
            {
                Class20.smethod_1(exception.ToString());
            }
        }

        public static void MsgWait(string string_0)
        {
            try
            {
                Class20.smethod_3(string_0);
                if (msgForm_0 != null)
                {
                    if (thread_0.ThreadState != ThreadState.Running)
                    {
                        msgForm_0.MessageStr = string_0;
                        msgForm_0.ShowDialog();
                        if (msgForm_0.InvokeRequired)
                        {
                            msgForm_0.Invoke(new Delegate5(MessageShowTT.smethod_0), new object[] { string_0 });
                        }
                    }
                    else if ((thread_0.ThreadState == ThreadState.Running) && msgForm_0.InvokeRequired)
                    {
                        msgForm_0.Invoke(new Delegate5(MessageShowTT.smethod_0), new object[] { string_0 });
                    }
                }
            }
            catch (Exception exception)
            {
                Class20.smethod_1(exception.ToString());
            }
        }

        public static void PromptBox(string string_0, string string_1)
        {
            Class20.smethod_3(string_1);
            MessageBoxSys.Show(string_1, string_0);
        }

        public static void PromptDlg(string string_0)
        {
            PromptDlg(string_0, "金税设备提示信息");
        }

        public static void PromptDlg(string string_0, string string_1)
        {
            Class20.smethod_3(string_0);
            MessageBoxSys.Show(string_0, string_1);
        }

        private static void ShowMessage(string strMessage)
        {
            msgForm_0.MessageStr = strMessage;
        }

        private static void smethod_0(string string_0)
        {
            msgForm_0.MessageStr = string_0;
        }

        private static void smethod_1(object object_0)
        {
            msgForm_0.Close();
        }

        private static void smethod_2()
        {
        }

        private delegate void Delegate5(string strMsg);
    }
}

