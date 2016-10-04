namespace Aisino.Framework.Plugin.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class FormSplashHelper
    {
        private static FormSplash formSplash_0;
        private static Thread thread_0;

        static FormSplashHelper()
        {
            
            formSplash_0 = new FormSplash();
        }

        public FormSplashHelper()
        {
            
        }

        public static void MsgWait()
        {
            try
            {
                if (((formSplash_0 != null) && (thread_0 != null)) && (thread_0.ThreadState == ThreadState.Running))
                {
                    if (formSplash_0.InvokeRequired)
                    {
                        formSplash_0.Invoke(new Delegate42(FormSplashHelper.smethod_2), new object[] { "" });
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
                if (formSplash_0 != null)
                {
                    if ((thread_0 != null) && (thread_0.ThreadState != ThreadState.Stopped))
                    {
                        if (thread_0.ThreadState == ThreadState.Running)
                        {
                            if (formSplash_0.InvokeRequired)
                            {
                                formSplash_0.Invoke(new Delegate42(FormSplashHelper.smethod_0), new object[] { string_0 });
                            }
                            else
                            {
                                smethod_0(string_0);
                            }
                        }
                    }
                    else
                    {
                        thread_0 = new Thread(new ParameterizedThreadStart(FormSplashHelper.smethod_1));
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
            formSplash_0.Message = string_0;
        }

        private static void smethod_1(object object_0)
        {
            formSplash_0.Message = object_0.ToString();
            formSplash_0.ShowDialog();
        }

        private static void smethod_2(object object_0)
        {
            Thread.Sleep(100);
            formSplash_0.Close();
        }

        private delegate void Delegate42(string strMsg);
    }
}

