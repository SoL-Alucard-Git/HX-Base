namespace Aisino.Fwkp.Wbjk
{
    using log4net;
    using System;

    internal class HandleException
    {
        internal static ILog Log = LogUtil.GetLogger<HandleException>();

        internal static void HandleError(Exception ex)
        {
            string message = ex.ToString();
            if (ex.InnerException != null)
            {
                message = message + ex.InnerException.ToString();
            }
            Log.Error(message);
        }
    }
}

