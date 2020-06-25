using System;
using System.Text;

namespace Framework
{
    public static class TextUtility
    {
        [ThreadStatic]
        private static StringBuilder s_CachedStringBuilder;

        public static string Format(string format, object arg0)
        {
            if (format == null) {
                throw new FrameworkException("Format is invalid.");
            }

            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(format, arg0);
            return s_CachedStringBuilder.ToString();
        }

        public static string Format(string format, object arg0, object arg1)
        {
            if (format == null) {
                throw new FrameworkException("Format is invalid.");
            }

            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(format, arg0, arg1);
            return s_CachedStringBuilder.ToString();
        }

        public static string Format(string format, object arg0, object arg1, object arg2)
        {
            if (format == null) {
                throw new FrameworkException("Format is invalid.");
            }

            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(format, arg0, arg1, arg2);
            return s_CachedStringBuilder.ToString();
        }

        public static string Format(string format, params object[] args)
        {
            if (format == null) {
                throw new FrameworkException("Format is invalid.");
            }

            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(format, args);
            return s_CachedStringBuilder.ToString();
        }

        private static void CheckCachedStringBuilder()
        {
            if (s_CachedStringBuilder == null) {
                s_CachedStringBuilder = new StringBuilder(1024);
            }
        }
    }
}
