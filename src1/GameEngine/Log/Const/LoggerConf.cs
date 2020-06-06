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

        public static bool RomateLogSwitch = false;
        public static bool GUILogSwitch = false;
        public static bool CmdlLogSwitch = true;
        public static bool FileLogSwitch = true;

        public static string FileLogName = string.Format("{0}.log", DateTime.Now.ToString("yyyyMMdd-HHmmss"));
    }
}