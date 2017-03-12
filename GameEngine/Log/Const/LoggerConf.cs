/***
 * LoggerConf.cs
 *
 * @author administrator
 */
using System;

namespace GameEngine
{
    public static class LoggerConf
    {
        public static string RomateLogURL = "http://10.230.72.21:8888/put/";

        public static bool LogFuncInfoSwitch = true;
        public static bool LogFileInfoSwitch = false;

        public static bool LogTimeSwitch = true;
        public static bool LogLevelSwitch = true;

        public static bool RomateLogSwitch = false;
        public static bool GUILogSwitch = true;
        public static bool CmdlLogSwitch = true;
        public static bool FileLogSwitch = true;

        public static string FileLogName = string.Format("log-{0}.txt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        public static int FileLogMaxSize = 8 * 1024 * 1024;
    }
}