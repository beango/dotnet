using System;
using Common.Logging;

namespace com.YiF1.Util
{
    public class LogHelper
    {
        public static ILog log = LogManager.GetLogger(typeof(LogHelper));

        public static void Error(Exception ex)
        {
            log.Error(ex);
        }

        public static void Error(string msg)
        {
            log.Error(msg);
        }

        public static void Error(string msg, Exception ex)
        {
            log.Error(msg, ex);
        }

        public static void Info(string msg)
        {
            log.Info(msg);
        }
    }
}
