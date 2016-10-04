namespace ns4
{
    using System;
    using System.ComponentModel;
    using System.Threading;

    internal static class Class98
    {
        internal static AutoResetEvent autoResetEvent_0;
        internal static BackgroundWorker backgroundWorker_0;
        internal static bool bool_0;
        internal static bool bool_1;
        internal static object object_0;
        internal static object object_1;

        static Class98()
        {
            
            autoResetEvent_0 = new AutoResetEvent(false);
            bool_0 = false;
            bool_1 = false;
            object_1 = new object();
            backgroundWorker_0 = new BackgroundWorker();
        }
    }
}

