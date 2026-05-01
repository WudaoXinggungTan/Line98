using System;
using System.IO;
using UnityEngine;
using CompanyCoreScripts.Extensions;
using CompanyCoreScripts.Services.LoggerService.BaseClass;

namespace CompanyCoreScripts.Services.LoggerService
{
    public class CustomLogger1 : BaseCustomLogger
    {
        private const string DebugTopicSuffix = ":: ";
        private const string Dot = ".";
        private const string StampFormat = "[{0}] ";
        private const string TimeStampFormat = "HH:mm:ss:ff";

        public override void Log(string message)
        {
            Debug.Log(GetTimeStamp() + message);
        }

        public override void LogWarning(string message)
        {
            Debug.LogWarning(GetTimeStamp() + message);
        }

        public override void LogError(string message)
        {
            Debug.LogError(GetTimeStamp() + message);
        }

        public override void LogException(Exception exception)
        {
            Debug.LogException(exception);
        }

        public override void LogTopic(string message, LogTopicType debugLogTopic = LogTopicType.Temp, string callerFilePath = "", string callerMemberName = "")
        {
            Debug.Log(debugLogTopic + DebugTopicSuffix + GetTimeStamp() + StampFormat.Format(GetCallerName(callerFilePath) + Dot + callerMemberName) + " " + message);
        }

        private string GetTimeStamp()
        {
            var timeStamp = DateTime.Now.ToString(TimeStampFormat);
            return string.Format(StampFormat, timeStamp);
        }

        private string GetCallerName(string callerFilePath)
        {
            return Path.GetFileNameWithoutExtension(callerFilePath);
        }
    }
}
