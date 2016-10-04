namespace Aisino.Framework.Pdf417.Code
{
    using System;
    using System.IO;

    public class SupportClass
    {
        public static void WriteStackTrace(Exception throwable, TextWriter stream)
        {
            stream.Write(throwable.StackTrace);
            stream.Flush();
        }
    }
}

