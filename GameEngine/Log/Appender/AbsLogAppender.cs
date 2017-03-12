/***
 * AbsLogAppender.cs
 *
 * @author administrator
 */
namespace GameEngine
{
    public abstract class AbsLogAppender : ILogAppender
    {
        private bool isEnabled = true;
        private LoggerType logType = LoggerType.NoLog;

        public bool IsEnabled
        {
            get {
                return isEnabled;
            }
            set {
                isEnabled = value;
            }
        }

        public LoggerType LogType
        {
            get {
                return logType;
            }
        }

        protected abstract void OnWrite(string msg, string stackTrace);

        public void Write(string message, string stack, LoggerType level)
        {
            this.logType = level;
            if (IsEnabled) {
                OnWrite(message, stack);
            }
        }

        public virtual void Dispose()
        {

        }
    }
}