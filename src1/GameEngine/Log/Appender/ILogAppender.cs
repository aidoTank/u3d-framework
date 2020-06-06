/***
 * ILogAppender.cs
 *
 * @author administrator
 */
namespace GameEngine
{
    /// <summary>
    /// 日志Appender接口
    /// </summary>
    public interface ILogAppender
    {
        bool IsEnabled
        {
            get;
            set;
        }

        void Dispose();

        void Write(string msg, string stackTrace, LoggerType type);
    }
}