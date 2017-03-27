using System;
using System.IO;
using UnityEditor;
using UnityEngine;

/***
 * BuildSetting.cs
 * 
 * @author administrator
 */
namespace GameEditor
{
    /// <summary>
    /// Build操作结构
    /// </summary>
    public class BuildSetting
    {
        public static void BulidPacket(BuildTarget target, string path)
        {
            EditorUtils.SwitchBuildTarget(target);

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
                    Debug.Log(string.Format("{0} not implemented.", target.ToString()));
                    break;
            }

            string exportDir = GetOutputPath(target, path);
            if (!Directory.Exists(exportDir)) {
                Directory.CreateDirectory(exportDir);
            }

            packetName = packetName.ToLower();
            string outputTargetPath = Path.Combine(exportDir, packetName);

            string[] scenes = EditorUtils.GetEnabledScenes();
            if (scenes.Length <= 0) {
                Debug.LogError("Not levels to build!");
                return;
            }

            BuildGeneric(scenes, outputTargetPath, target);
        }

        public static void BuildGeneric(string[] scenes, string targetPath, BuildTarget buildTarget)
        {
            BuildOptions op = EditorUserBuildSettings.development ?
                BuildOptions.Development : 
                BuildOptions.None;

            string result = BuildPipeline.BuildPlayer(scenes, targetPath, buildTarget, op);
            if (result.Length > 0) {
                throw new Exception(string.Format("Build Player Error: {0}", result));
            }

            EditorLog.Info("包导出成功：" + targetPath.Replace("\\", "/"));
        }

        public static string GetOutputPath(BuildTarget target, string outputPath)
        {
            return Path.Combine(outputPath, target.ToString());
        }

        public static string GetPacketName()
        {
            string name = EditorUtils.GetProductName();
            string version = EditorUtils.GetProductVersion();
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
