namespace Aisino.Framework.Plugin.Core.Util
{
    using log4net.Core;
    using log4net.Layout.Pattern;
    using System;
    using System.IO;

    public class UserNamePatternConvert : PatternLayoutConverter
    {
        private static string string_0;

        static UserNamePatternConvert()
        {
            
            string_0 = "SYS";
        }

        public UserNamePatternConvert()
        {
            
        }

        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            if (loggingEvent.MessageObject != null)
            {
                writer.Write(string_0);
            }
        }

        public static string UserName
        {
            set
            {
                string_0 = value;
            }
        }
    }
}

