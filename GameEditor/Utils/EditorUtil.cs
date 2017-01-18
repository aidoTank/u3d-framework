using System.Collections.Generic;
using UnityEditor;

/***
 * EditorUtil.cs
 * 
 * @anthor abaojin
 */
namespace GameEditor
{
    public static class EditorUtil
    {
        public static string GetProductName()
        {
            return PlayerSettings.productName;
        }

        public static string GetProductVersion()
        {
            string bundleVersion = PlayerSettings.bundleVersion;
            string appVersion = null;
#if UNITY_ANDROID
        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android) {
            appVersion = PlayerSettings.Android.bundleVersionCode.ToString();
        }
#elif UNITY_IPHONE
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS) {
                appVersion = PlayerSettings.iOS.buildNumber.ToString();
            } 
#else
            appVersion = EditorUserBuildSettings.activeBuildTarget.ToString();
#endif
            return string.Format("{0}.{1}", appVersion, bundleVersion);
        }

        public static string[] GetEnabledScenes()
        {
            List<string> list = new List<string>();
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
                if (!scene.enabled) {
                    continue;
                }
                list.Add(scene.path);
            }

            return list.ToArray();
        }

    }

}
