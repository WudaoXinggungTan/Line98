using System;

namespace WudaoXinggungTan.CompanyCoreDomain.Scripts.Services.LoggerService.BaseClass
{
    public abstract class BaseLogger : Interface.ILogger
    {
        public void Enable()
        {
            MyLogService.InjectLogger(this);
        }
        public abstract void Log(string message);
        public abstract void LogWarning(string message);
        public abstract void LogError(string message);
        public abstract void LogException(Exception exception);
        public abstract void LogTopic(string message, LogTopicType logTopicType = LogTopicType.Temp, string callerFilePath = "", string callerFileName = "");
    }
}