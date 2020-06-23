using System;
using System.Runtime.Serialization;

namespace Framework
{
    /// <summary>
    /// 框架异常类
    /// </summary>
    [Serializable]
    public class FrameworkException : Exception
    {
        public FrameworkException() : base()
        {

        }

        public FrameworkException(string message) : base(message)
        {

        }

        public FrameworkException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public FrameworkException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}

