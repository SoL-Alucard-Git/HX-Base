namespace Aisino.Framework.Plugin.Core.MessageDlg
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ProcessHelper
    {
        private static ProcessBarForm processBarForm_0;
        private static Thread thread_0;

        static ProcessHelper()
        {
            
            processBarForm_0 = new ProcessBarForm();
        }

        public ProcessHelper()
        {
            
        }

        public static void MsgWait()
        {
            try
            {
                if (((processBarForm_0 != null) && (thread_0 != null)) && (thread_0.ThreadState == ThreadState.Running))
                {
                    if (processBarForm_0.InvokeRequired)
                    {
                        processBarForm_0.Invoke(new Delegate41(ProcessHelper.smethod_2), new object[] { "", 100, 100 });
                    }
                    else
                    {
                        smethod_2("", 100, 100);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static void MsgWait(string string_0, int int_0, int int_1)
        {
            try
            {
                if (int_1 > int_0)
                {
                    int_1 = int_0;
                }
                if (processBarForm_0 != null)
                {
                    if ((thread_0 != null) && (thread_0.ThreadState != ThreadState.Stopped))
                    {
                        if (thread_0.ThreadState == ThreadState.Running)
                        {
                            if (processBarForm_0.InvokeRequired)
                            {
                                processBarForm_0.Invoke(new Delegate41(ProcessHelper.smethod_0), new object[] { string_0, int_1, int_0 });
                            }
                            else
                            {
                                smethod_0(string_0, int_1, int_0);
                            }
                        }
                    }
                    else
                    {
                        thread_0 = new Thread(new ParameterizedThreadStart(ProcessHelper.smethod_1));
                        thread_0.SetApartmentState(ApartmentState.STA);
                        thread_0.Start(string_0);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private static void smethod_0(string string_0, int int_0, int int_1)
        {
            processBarForm_0.Message = string_0;
            processBarForm_0.Pos = int_0;
            processBarForm_0.Count = int_1;
        }

        private static void smethod_1(object object_0)
        {
            processBarForm_0.Message = object_0.ToString();
            processBarForm_0.Pos = 0;
            processBarForm_0.ShowDialog();
        }

        private static void smethod_2(object object_0, int int_0, int int_1)
        {
            Thread.Sleep(100);
            processBarForm_0.Pos = processBarForm_0.Count;
            processBarForm_0.Close();
        }

        private delegate void Delegate41(string strMsg, int percent, int count);
    }
}

