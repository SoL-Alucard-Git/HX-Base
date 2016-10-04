namespace Aisino.Framework.Plugin.Core.MessageDlg
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class MessageHelper
    {
        private static ProcessForm processForm_0;
        private static Thread thread_0;

        static MessageHelper()
        {
            
            processForm_0 = new ProcessForm();
        }

        public MessageHelper()
        {
            
        }

        public static void MsgWait()
        {
            try
            {
                if (((processForm_0 != null) && (thread_0 != null)) && (thread_0.ThreadState == ThreadState.Running))
                {
                    if (processForm_0.InvokeRequired)
                    {
                        processForm_0.Invoke(new Delegate43(MessageHelper.smethod_2), new object[] { "" });
                    }
                    else
                    {
                        smethod_2("");
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static void MsgWait(string string_0)
        {
            try
            {
                if (processForm_0 != null)
                {
                    if ((thread_0 != null) && (thread_0.ThreadState != ThreadState.Stopped))
                    {
                        if (thread_0.ThreadState == ThreadState.Running)
                        {
                            if (processForm_0.InvokeRequired)
                            {
                                processForm_0.Invoke(new Delegate43(MessageHelper.smethod_0), new object[] { string_0 });
                            }
                            else
                            {
                                smethod_0(string_0);
                            }
                        }
                    }
                    else
                    {
                        thread_0 = new Thread(new ParameterizedThreadStart(MessageHelper.smethod_1));
                        thread_0.SetApartmentState(ApartmentState.STA);
                        thread_0.Start(string_0);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private static void smethod_0(string string_0)
        {
            processForm_0.Message = string_0;
        }

        private static void smethod_1(object object_0)
        {
            processForm_0.Message = object_0.ToString();
            processForm_0.ShowDialog();
        }

        private static void smethod_2(object object_0)
        {
            Thread.Sleep(100);
            processForm_0.Close();
        }

        private delegate void Delegate43(string strMsg);
    }
}

