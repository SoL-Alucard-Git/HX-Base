namespace Aisino.Framework.Plugin.Core.Util
{
    using log4net;
    using log4net.Appender;
    using log4net.Layout;
    using System;

    public class LogUtil
    {
        public LogUtil()
        {
            
        }

        public static ILog GetLogger<T>()
        {
            ILog logger = LogManager.GetLogger(typeof(T).FullName);
            foreach (IAppender appender in logger.Logger.Repository.GetAppenders())
            {
                PatternLayout layout = (appender as AppenderSkeleton).Layout as PatternLayout;
                layout.AddConverter("SYS", typeof(UserNamePatternConvert));
                layout.ActivateOptions();
            }
            return logger;
        }
    }
}

