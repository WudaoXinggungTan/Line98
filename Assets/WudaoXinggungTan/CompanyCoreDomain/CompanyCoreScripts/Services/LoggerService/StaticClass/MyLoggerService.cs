using System;
using System.Runtime.CompilerServices;
using CompanyCoreScripts.Services.LoggerService.Interface;


namespace CompanyCoreScripts.Services.LoggerService.StaticClass
{
    public static class MyLoggerService 
    {
        private static ICustomLogger _customLogger;

        internal static void InjectLogger(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }

        public static void Log(string message)
        {
            _customLogger.Log(message);
        }

        public static void LogWarning(string message)
        {
            _customLogger.LogWarning(message);
        }

        public static void LogError(string message)
        {
            _customLogger.LogError(message);
        }

        public static void LogException(Exception exception)
        {
            _customLogger.LogException(exception);
        }

        public static void LogTopic(string message, LogTopicType logTopicType = LogTopicType.Temp, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
        {
            _customLogger.LogTopic(message, logTopicType, callerFilePath, callerMemberName);
        }
    }
}
