using System;
using System.IO;
using UnityEditor;
using UnityEngine;

/***
 * BuildSetting.cs
 * 
 * @author abaojin
 */
namespace GameEditor
{
    public class BuildSetting
    {
        public static void BulidPacket(BuildTarget target, string path)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(target);

            string packetName = GetPacketName();

            switch (target) {
                case BuildTarget.Android:
                    packetName = string.Format("{0}.apk", packetName);
                    break;
                case BuildTarget.iOS:
                    packetName = string.Format("{0}.ipa", packetName);
                    break;
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    packetName = string.Format("{0}.exe", packetName);
                    break;
                default:
                    Debug.Log("Platform target not implemented.");
                    break;
            }

            string exportDir = GetOutputPath(target, path);

            if (!Directory.Exists(exportDir)) {
                Directory.CreateDirectory(exportDir);
            }

            string[] scenes = EditorUtil.GetEnabledScenes();
            if(scenes.Length <= 0) {
                Debug.LogError("Not levels to build!");
                return;
            }

            string targetPath = Path.Combine(exportDir, packetName);

            BuildGeneric(scenes, targetPath, target);
        }

        public static void BuildGeneric(string[] scenes, string targetPath, BuildTarget buildTarget)
        {
            BuildOptions option = EditorUserBuildSettings.development ?
                BuildOptions.Development : 
                BuildOptions.None;

            string result = BuildPipeline.BuildPlayer(scenes, targetPath, buildTarget, option);
            if (result.Length > 0) {
                throw new Exception("BuildPlayer Error: " + result);
            }
        }

        public static void SwitchBuildTarget(BuildTarget target)
        {
            EditorUserBuildSettings.development = true;
            EditorUserBuildSettings.connectProfiler = true;
            if (EditorUserBuildSettings.activeBuildTarget == target) {
                return;
            }
            EditorUserBuildSettings.SwitchActiveBuildTarget(target);
        }

        public static string GetOutputPath(BuildTarget target, string outputPath)
        {
            return Path.Combine(outputPath, target.ToString());
        }

        public static string GetPacketName()
        {
            string name = EditorUtil.GetProductName();
            string version = EditorUtil.GetProductVersion();
            string time = DateTime.Now.ToString("yyyyMMddHHmm");

            return string.Format("{0}_{1}_{2}", name, version, time);
        }

        public static void ClearAllBuild(string outputPath)
        {
            if (!Directory.Exists(outputPath)) {
                return;
            }

            FileUtil.DeleteFileOrDirectory(outputPath);
        }
    }
}
