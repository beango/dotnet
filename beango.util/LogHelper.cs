using System;
using Common.Logging;

namespace beango.util
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

        public static void Debug(string msg)
        {
            log.Debug(msg);
        }
    }
}
