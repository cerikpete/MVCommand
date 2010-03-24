using System;
using log4net;

namespace MVCommand.Logging
{
    public static class Log<T>
    {
        private static readonly ILog _log;

        static Log()
        {
            _log = LogManager.GetLogger(typeof(T));
        }

        public static void Info(string message)
        {
            _log.Info(message);
        }

        public static void Debug(string message)
        {
            _log.Debug(message);
        }

        public static void Error(string message, Exception exception)
        {
            _log.Error(message, exception);
        }
    }
}