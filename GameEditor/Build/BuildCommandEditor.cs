using System;
using System.IO;
using UnityEditor;
using UnityEngine;

/***
 * BuildEditor.cs
 * 
 * @author abaojin
 */
namespace GameEditor
{
    public class BuildCommandEditor : Editor
    {
        [MenuItem("Tool/Build Android")]
        private static void PerformAndroidBuild()
        {
            BulidTarget(BuildTarget.Android);
        }

        [MenuItem("Tool/Build iPhone")]
        private static void PerformiPhoneBuild()
        {
            BulidTarget(BuildTarget.iOS);
        }

        private static void BulidTarget(BuildTarget target)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(target);

            string appName = GetAppName();
            string targetDir = string.Empty;
            string targetName = string.Empty;
            BuildTarget buildTarget = BuildTarget.Android;
            string applicationPath = Application.dataPath.Replace("/Assets", "");

            if (target == BuildTarget.Android) {
                targetDir = applicationPath + "/Bin";
                targetName = appName + ".apk";
            }
            if (target == BuildTarget.iOS) {
                targetDir = applicationPath + "/Bin";
                targetName = appName;
                buildTarget = BuildTarget.iOS;
            }

            if (Directory.Exists(targetDir)) {
                if (File.Exists(targetName)) {
                    File.Delete(targetName);
                }
            } else {
                Directory.CreateDirectory(targetDir);
            }

            string[] scenes = EditorUtil.GetEnabledScenes();
            GenericBuild(scenes, targetDir + "/" + targetName, buildTarget, BuildOptions.None);
        }

        private static void GenericBuild(string[] scenes, string targetDir, BuildTarget buildTarget, BuildOptions buildOption)
        {
            string result = BuildPipeline.BuildPlayer(scenes, targetDir, buildTarget, buildOption);
            if (result.Length > 0) {
                throw new Exception("BuildPlayer failure: " + result);
            }
        }

        private static string GetAppName()
        {
            string name = EditorUtil.GetPrjName();
            string version = EditorUtil.GetPrjVersion();
            return string.Format("{0}_{1}_{2}", name, version, DateTime.Now.ToString("yyyyMMddHHmm"));
        }
    }
}
