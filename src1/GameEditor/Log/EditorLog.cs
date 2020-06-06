using GameEngine;

/***
 * EditorUtil.cs
 * 
 * @anthor administrator
 */
namespace GameEditor
{
    public class EditorLog
    {
        static EditorLog()
        {
            LoggerConf.RomateLogSwitch = false;
            LoggerConf.FileLogSwitch = false;
            LoggerConf.GUILogSwitch = false;
            LoggerConf.CmdlLogSwitch = true;
        }

        public static void Info(string message, string color = "#00ff00ff")
        {
            GameLog.Info(string.Format("<color={0}>{1}</color>", color, message));
        }

        public static void Error(string message, string color = "#ff0000ff")
        {
            GameLog.Error(string.Format("<color={0}>{1}</color>", color, message));
        }
    }
}
