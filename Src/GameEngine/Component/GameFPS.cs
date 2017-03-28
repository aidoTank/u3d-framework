using UnityEngine;

/***
 * GameFPS.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public class GameFPS : SingletonComponent<GameFPS>
    {
        private static string FPSFormat = "fps:{0} ms: {1}";
        private static Rect FPSRect = new Rect(0, 200, 200, 100);
        private static GUIStyle GIStyle = new GUIStyle();

        public enum Ancher
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
        }

        public Ancher Poivt = Ancher.BottomRight;
        public float Interval = 1f;

        private float lastInterval;
        private int frame = 0;
        private float fps;

        void Start()
        {
            frame = 0;
            lastInterval = Time.realtimeSinceStartup;
        }

        void Update()
        {
            ++frame;
            if (Time.realtimeSinceStartup > lastInterval + Interval) {
                fps = frame / (Time.realtimeSinceStartup - lastInterval);
                frame = 0;
                lastInterval = Time.realtimeSinceStartup;
            }
        }

        void OnGUI()
        {
            switch (Poivt) {
                case Ancher.BottomLeft:
                    FPSRect.Set(0, Screen.height - 20, 200, 100);
                    break;
                case Ancher.BottomRight:
                    FPSRect.Set(Screen.width - 160, Screen.height - 20, 400, 200);
                    break;
                case Ancher.TopLeft:
                    FPSRect.Set(0, 0, 200, 100);
                    break;
                case Ancher.TopRight:
                    FPSRect.Set(Screen.width - 160, 0, 200, 100);
                    break;
            }

            GIStyle.fontSize = 18;
            GIStyle.normal.textColor = Color.green;

            GUI.Label(FPSRect, 
                string.Format(FPSFormat, fps.ToString("f2"), 
                (1000.0f / Mathf.Max(fps, 0.001f)).ToString("f1")),
                GIStyle
            );
        }
    }

}
