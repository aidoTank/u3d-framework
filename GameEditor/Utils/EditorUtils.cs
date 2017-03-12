using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/***
 * EditorUtil.cs
 * 
 * @anthor administrator
 */
namespace GameEditor
{
    public static class EditorUtils
    {
        public static string GetProductName()
        {
            return PlayerSettings.productName;
        }

        public static string GetProductVersion()
        {
            string bundleVersion = PlayerSettings.bundleVersion;
            string appVersion = null;

            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android) {
                appVersion = PlayerSettings.Android.bundleVersionCode.ToString();
            } else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS) {
                appVersion = PlayerSettings.iOS.buildNumber.ToString();
            } else {
                appVersion = EditorUserBuildSettings.activeBuildTarget.ToString();
            }

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

        public static void ProcessDir(string dirPath, string[] fileExts, Action<string, object> handler, object param = null)
        {
            if (null == handler) {
                return;
            }

            if (!Directory.Exists(dirPath)) {
                return;
            }

            if (null != fileExts) {
                for (int i = 0; i < fileExts.Length; ++i) {
                    fileExts[i] = fileExts[i].ToLower();
                }
            }

            string[] files = Directory.GetFiles(dirPath, "*.*", SearchOption.AllDirectories);
            foreach (string file in files) {
                bool isMatch = true;
                if (null != fileExts) {
                    isMatch = false;
                    for (int i = 0; i < fileExts.Length; ++i) {
                        if (file.EndsWith(fileExts[i]) || fileExts[i] == "*") {
                            isMatch = true;
                            break;
                        }
                    }
                }

                if (isMatch) {
                    handler(file.Replace('\\', '/'), param);
                }
            }
        }

        public static void SwitchBuildTarget(BuildTarget target, bool isDevelopment = true, bool isProfiler = true)
        {
            EditorUserBuildSettings.development = isDevelopment;
            EditorUserBuildSettings.connectProfiler = isProfiler;
            if (EditorUserBuildSettings.activeBuildTarget == target) {
                return;
            }
            EditorUserBuildSettings.SwitchActiveBuildTarget(target);
        }

        public static void ClearPersistentData()
        {
            string persistentPath = Application.persistentDataPath;
            if (!Directory.Exists(persistentPath)) {
                return;
            }

            FileUtil.DeleteFileOrDirectory(persistentPath);
        }
    }

}
