using System;

namespace CompanyCoreScripts.Services.LoggerService.Interface
{
    public interface ICustomLogger
    {
        void Log(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogException(Exception exception);
        void LogTopic(string message, LogTopicType logTopicType = LogTopicType.Temp, string callerFilePath = "", string callerMemberName = "");
    }
}
