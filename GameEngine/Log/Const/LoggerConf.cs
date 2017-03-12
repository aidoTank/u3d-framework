/***
 * LoggerConf.cs
 *
 * @author administrator
 */
namespace GameEngine
{
    public static class LoggerConf
    {
        public static string RomateLogURL = "http://10.230.72.21:8888/put/";


        public static bool LogFuncInfoSwitch = true;
        public static bool LogFileInfoSwitch = false;

        public static bool LogTimeSwitch = true;
        public static bool LogLevelSwitch = true;

        public static bool RomateLogSwitch = true;
        public static bool GUILogSwitch = true;
        public static bool ConsolelLogSwitch = true;
        public static bool FileLogSwitch = false;

        public static string FileLogName = "log.txt";
        public static int FileLogMaxSize = 8 * 1024 * 1024;
        public static bool FileLogIsNewFile = true;
    }
}