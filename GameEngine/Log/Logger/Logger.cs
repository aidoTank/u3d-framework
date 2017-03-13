using LitJson;
using System;
using System.Collections.Generic;
using UnityEngine;

/***
 * Logger.cs
 *
 * @author administrator
 */
namespace GameEngine
{
    public class Logger : ILogger
    {
        private LoggerType logType = LoggerType.Info;
        private List<ILogAppender> appenders = new List<ILogAppender>();
        private int currentStackFrame = 0;

        public Logger()
        {
            this.InitAppender();
        }

        public Logger(int stackFrameIndex) : this()
        {
            this.currentStackFrame = stackFrameIndex;
        }

        public void InitAppender()
        {
            if (LoggerConf.RomateLogSwitch) {
                AddAppender(new RomateLogAppender());
            }
            if (LoggerConf.GUILogSwitch) {
                AddAppender(new GUILogAppender());
            }
            if (LoggerConf.FileLogSwitch) {
                AddAppender(new FileLogAppender(LoggerConf.FileLogName));
            }
        }

        public ILogger AddAppender(ILogAppender appender)
        {
            if (!appenders.Contains(appender)) {
                appenders.Add(appender);
            }
            return this;
        }

        public ILogger RemAppender(ILogAppender appender)
        {
            int count = appenders.Count;
            for (int i = 0; i < count; ++i) {
                if (appenders[i] == appender) {
                    appenders.RemoveAt(i);
                    break;
                }
            }
            return this;
        }

        public void Assert(bool condition, string assertString, bool pauseOnFail)
        {
            if (!condition && (logType <= LoggerType.Error)) {
                this.Error("assert failed, {0}", assertString);
                if (pauseOnFail) {
                    Debug.Break();
                }
            }
        }

        public void Info(string msg, params object[] args)
        {
            if (!LoggerConf.CmdlLogSwitch) {
                return;
            }

            if (logType <= LoggerType.Info) {
                if (args.Length == 0) {
                    Debug.Log(msg);
                } else {
                    Debug.Log(string.Format(msg, args));
                }
            }
        }

        public void Warning(string message, params object[] args)
        {
            if (!LoggerConf.CmdlLogSwitch) {
                return;
            }

            if (logType <= LoggerType.Warn) {
                if (args.Length == 0) {
                    Debug.LogWarning(message);
                } else {
                    Debug.LogWarning(string.Format(message, args));
                }
            }
        }

        public void Error(string message, params object[] args)
        {
            if (!LoggerConf.CmdlLogSwitch) {
                return;
            }

            if (logType <= LoggerType.Error) {
                if (args.Length == 0) {
                    Debug.LogError(message);
                } else {
                    Debug.LogError(string.Format(message, args));
                }
            }
        }

        public void Exception(System.Exception exception)
        {
            if (!LoggerConf.CmdlLogSwitch) {
                return;
            }

            if (logType <= LoggerType.Error) {
                Debug.LogException(exception);
            }
        }

        public void Dispose()
        {
            if (appenders == null) {
                return;
            }
            foreach (ILogAppender appender in appenders) {
                appender.Dispose();
            }
        }

        public void Log(LoggerType type, string condition, string stackTrace)
        {
            if (appenders.Count <= 0) {
                return;
            }
            string message = Format(type, condition, currentStackFrame);
            foreach (ILogAppender appender in appenders) {
                appender.Write(message, stackTrace, type);
            }
        }

        private string Format(LoggerType type, string message, int stackFrame)
        {
            string logTime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            string logType = type.ToString();

            JsonData logProperty = new JsonData();
            logProperty["Time"] = logTime;
            logProperty["Type"] = logType;

            JsonData logPacket = new JsonData();
            logPacket["Property"] = logProperty;
            logPacket["Content"] = message;
            string logJson = JsonMapper.ToJson(logPacket);
            logJson = logJson.Replace("\\", "");

            return logJson;
        }
    }
}