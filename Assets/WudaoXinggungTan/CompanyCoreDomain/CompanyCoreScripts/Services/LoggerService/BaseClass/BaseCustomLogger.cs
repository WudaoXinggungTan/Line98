using System;
using CompanyCoreScripts.Services.LoggerService.Interface;
using CompanyCoreScripts.Services.LoggerService.StaticClass;

namespace CompanyCoreScripts.Services.LoggerService.BaseClass
{
    public abstract class BaseCustomLogger : ICustomLogger
    {
        protected BaseCustomLogger()
        {
            MyLoggerService.InjectLogger(this);
        }
        public abstract void Log(string message);
        public abstract void LogWarning(string message);
        public abstract void LogError(string message);
        public abstract void LogException(Exception exception);
        public abstract void LogTopic(string message, LogTopicType logTopicType = LogTopicType.Temp, string callerFilePath = "", string callerFileName = "");
    }
}
