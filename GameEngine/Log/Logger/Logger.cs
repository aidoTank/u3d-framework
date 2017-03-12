using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/***
 * DefaultLogger.cs
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
            this.Init();
        }

        public Logger(int stackFrameIndex) : this()
        {
            this.currentStackFrame = stackFrameIndex;
            this.Init();
        }

        public bool IsShowCallInfo
        {
            get { return LoggerConf.LogFuncInfoSwitch || LoggerConf.LogFileInfoSwitch; }
        }

        #region 实现方法

        public void Init()
        {
            if (LoggerConf.RomateLogSwitch) {
                AddAppender(new RomateLogAppender());
            }
            if (LoggerConf.GUILogSwitch) {
                AddAppender(new GUILogAppender());
            }
            if (LoggerConf.FileLogSwitch) {
                AddAppender(new FileLogAppender(LoggerConf.FileLogName, LoggerConf.FileLogMaxSize, LoggerConf.FileLogIsNewFile));
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
                BeginLog();
                this.Error("assert failed, {0}", assertString);
                EndLog();
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
                BeginLog();
                if (args.Length == 0) {
                    Debug.Log(msg);
                } else {
                    Debug.Log(string.Format(msg, args));
                }
                EndLog();
            }
        }

        public void Warn(string message, params object[] args)
        {
            if (!LoggerConf.CmdlLogSwitch) {
                return;
            }

            if (logType <= LoggerType.Warn) {
                BeginLog();
                if (args.Length == 0) {
                    Debug.LogWarning(message);
                } else {
                    Debug.LogWarning(string.Format(message, args));
                }
                EndLog();
            }
        }

        public void Error(string message, params object[] args)
        {
            if (!LoggerConf.CmdlLogSwitch) {
                return;
            }

            if (logType <= LoggerType.Error) {
                BeginLog();
                if (args.Length == 0) {
                    Debug.LogError(message);
                } else {
                    Debug.LogError(string.Format(message, args));
                }
                EndLog();
            }
        }

        public void Exception(System.Exception exception)
        {
            if (!LoggerConf.CmdlLogSwitch) {
                return;
            }

            if (logType <= LoggerType.Error) {
                BeginLog();
                Debug.LogException(exception);
                EndLog();
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
                Debug.LogError(message);
                appender.Write(message, stackTrace, type);
            }
        }

        #endregion 实现方法

        public void BeginLog()
        {
            if (IsShowCallInfo) currentStackFrame++;
        }

        public void EndLog()
        {
            if (IsShowCallInfo) currentStackFrame--;
        }

        #region 日志格式

        private string Format(LoggerType type, string msg, int stackFrame)
        {
            StringBuilder sb = new StringBuilder(256);
            if (LoggerConf.LogTimeSwitch) {
                sb.Append(string.Format("[{0}]", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss ffff")));
            }
            if (LoggerConf.LogLevelSwitch) {
                sb.Append(string.Format(" [{0}] ", type));
            }

#if !UNITY_IPHONE || UNITY_EDITOR
            if (IsShowCallInfo && stackFrame >= 0) {
                System.Diagnostics.StackFrame sf = new System.Diagnostics.StackTrace(true).GetFrame(stackFrame);
                if (sf != null) {
                    int fileLine = sf.GetFileLineNumber();
                    string fileName = sf.GetFileName();
                    int startIndex = fileName.LastIndexOf('\\') + 1;
                    string className = fileName.Substring(startIndex, fileName.LastIndexOf('.') - startIndex);
                    string methodName = sf.GetMethod().Name;

                    if (LoggerConf.LogFuncInfoSwitch) {
                        sb.Append(string.Format("- {0}.{1} ", className, methodName));
                    }
                    if (LoggerConf.LogFileInfoSwitch) {
                        sb.Append(string.Format("({0}:{1}) ", fileName, fileLine));
                    }
                }
            }
#endif
            sb.Append("- ");
            sb.Append(msg);

            return sb.ToString();
        }

        #endregion 日志格式
    }
}