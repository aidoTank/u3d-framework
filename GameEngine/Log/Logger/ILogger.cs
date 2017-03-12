/***
 * ILogger.cs
 *
 * @author administrator
 */
namespace GameEngine
{
    /// <summary>
    /// 管理日志Appender接口
    /// </summary>
    public interface ILogger
    {
        void Init();

        ILogger AddAppender(ILogAppender appender);

        ILogger RemAppender(ILogAppender appender);

        void Assert(bool condition, string assertString, bool pauseOnFail);

        void Info(string msg, params object[] args);

        void Warn(string msg, params object[] args);

        void Error(string msg, params object[] args);

        void Exception(System.Exception ex);

        void Dispose();

        void Log(LoggerType type, string condition, string stackTrace);
    }
}