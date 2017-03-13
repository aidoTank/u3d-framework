using System;
using System.Text;
using UnityEngine;

/***
 * GUILogAppender.cs
 *
 * @author administrator
 */
namespace GameEngine
{
    public class GUILogAppender : AbsLogAppender
    {
        private static GUILogWindow guiLog = GUILogWindow.Instance;

        protected override void OnWrite(string message, string stackTrace)
        {
            if(LogType == LoggerType.Error) {
                guiLog.Print(message);
                guiLog.Print(stackTrace);
            } else {
                guiLog.Print(message);
            }
        }
    }

    public class GUILogWindow : SingletonComponent<GUILogWindow>
    {
        private static int BUTTON_WIDTH = 160;
        private static int BUTTON_HEIGHT = 80;

        public int MaxLogSize = 10 * 1024;

        private bool isShowed = false;
        private StringBuilder logInfos = null;
        private Rect rect1 = new Rect(Screen.width / 2 - BUTTON_WIDTH / 2, 0, BUTTON_WIDTH, BUTTON_HEIGHT);
        private Rect rect2 = new Rect(Screen.width / 2 + BUTTON_WIDTH / 2 + 2, 0, BUTTON_WIDTH, BUTTON_HEIGHT);
        private Rect rect3 = new Rect(0, BUTTON_HEIGHT, Screen.width, Screen.height - BUTTON_HEIGHT);

        private void OnGUI()
        {
            if (GUI.Button(rect1, isShowed ? "Hide" : "Show")) {
                isShowed = !isShowed;
            }

            if (isShowed) {
                if (GUI.Button(rect2, "Clear")) {
                    logInfos.Remove(0, logInfos.Length);
                }
                GUI.TextArea(rect3, logInfos.ToString());
            }
        }

        public void Print(string msg)
        {
            if (logInfos == null) {
                logInfos = new StringBuilder();
            }

            logInfos.Append(msg).Append(Environment.NewLine);

            if (logInfos.Length > MaxLogSize) {
                logInfos.Remove(0, (int)(MaxLogSize * 0.6));
            }
        }
    }
}