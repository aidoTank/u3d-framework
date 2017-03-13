using UnityEngine;

/***
 * GameLog.cs
 *
 * @author administrator
 */
namespace GameEngine
{
    public static class GameLog
    {
        private static int stackFrameIndex = 7;

        private static LoggerType[] logTMapping = {
            LoggerType.Error, // LogType.Error
            LoggerType.Error, // LogType.Assert
            LoggerType.Warn,  // LogType.Warning
            LoggerType.Info,  // LogType.Log
            LoggerType.Error, // LogType.Exception
        };

        private static ILogger logger = null;

        static GameLog()
        {
            if (logger == null) {
                logger = new Logger(stackFrameIndex);
                IniLogCallback();
            }
        }

        private static void IniLogCallback()
        {
            Application.logMessageReceived += OnLogHandle;
        }

        private static void OnLogHandle(string condition, string stackTrace, LogType type)
        {
            if (logger == null) {
                return;
            }
            logger.Log(logTMapping[(int)type], condition, stackTrace);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Assert(bool condition)
        {
            if (logger != null) {
                logger.Assert(condition, string.Empty, true);
            }
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Assert(bool condition, string assertString)
        {
            if (logger != null) {
                logger.Assert(condition, assertString, true);
            }
        }

        public static void Info(string msg, params object[] args)
        {
            if (logger != null) {
                logger.Info(msg, args);
            }
        }

        public static void Warning(string msg, params object[] args)
        {
            if (logger != null) {
                logger.Warning(msg, args);
            }
        }

        public static void Error(string msg, params object[] args)
        {
            if (logger != null) {
                logger.Error(msg, args);
            }
        }

        public static void Exception(System.Exception ex)
        {
            if (logger != null) {
                logger.Exception(ex);
            }
        }

        public static void Dispose()
        {
            if (logger != null) {
                logger.Dispose();
                logger = null;
            }
#if UNITY_5
            Application.logMessageReceived -= OnLogHandle;
#endif
        }
    }
}