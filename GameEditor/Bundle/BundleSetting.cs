using System.IO;
using UnityEditor;
using UnityEngine;

/***
 * BundleSetting.cs
 * 
 * @author administrator
 */
namespace GameEditor
{
    /// <summary>
    /// Bundle操作接口
    /// </summary>
    public class BundleSetting
    {
        public static void UpdateAllBundleName(bool isClean = true)
        {
            if (isClean) {
                ClearAllBundleName();
            }

            int fileCount = 0;
            int dirCount = 0;

            foreach (BundleInfo info in BundleConfig.BUNDLE_CONFIG) {
                if (info.FileExts != null) {
                    EditorUtils.ProcessDir(info.SrcPath, info.FileExts, (filePath, data) => {
                        if (UpdateBundleName(filePath, (BundleInfo)data)) {
                            ++fileCount;
                        }
                    }, info);
                } else {
                    UpdateBundleName(info.SrcPath, info);
                    ++dirCount;
                }
            }

            AssetDatabase.Refresh();

            Debug.Log(string.Format("设置完毕！共有：{0}个文件和{1}个目录更新。", fileCount, dirCount));
        }

        public static void ClearAllBundleName()
        {
            AssetDatabase.RemoveUnusedAssetBundleNames();
            string[] bundleNames = AssetDatabase.GetAllAssetBundleNames();
            int len = bundleNames.Length;
            for (int i = 0; i < len; ++i) {
                AssetDatabase.RemoveAssetBundleName(bundleNames[i], true);
            }
        }

        public static void BuildAssetBundle(BuildTarget target, string path)
        {
            EditorUtils.SwitchBuildTarget(target);

            string output = Path.Combine(path, target.ToString());
            if (!Directory.Exists(output)) {
                Directory.CreateDirectory(output);
            }

            BuildPipeline.BuildAssetBundles(
                output,
                BuildAssetBundleOptions.ForceRebuildAssetBundle,
                EditorUserBuildSettings.activeBuildTarget
            );
        }

        public static void CopyAssetBundle(BuildTarget target)
        {

        }

        public static void ClearAllBuild(string outputPath)
        {
            if (!Directory.Exists(outputPath)) {
                return;
            }

            FileUtil.DeleteFileOrDirectory(outputPath);
        }

        private static bool UpdateBundleName(string filePath, BundleInfo info)
        {
            foreach (string path in BundleConfig.BUNDLE_CONFIG_IGNORE) {
                if (filePath.IndexOf(path) != -1) {
                    return false;
                }
            }

            string name = info.BundleName;

            if (string.IsNullOrEmpty(name)) {
                string[] strArray = null;
                strArray = filePath.Split('/');
                name = strArray[strArray.Length - 1];
                strArray = name.Split('.');
                name = strArray[0];
            }

            name = Path.Combine(info.DstPath, name);

            return SetBundleName(filePath, name);
        }

        private static bool SetBundleName(string filePath, string name)
        {
            int index = filePath.IndexOf("Assets");
            if (index != -1) {
                string path = filePath.Substring(index);
                AssetImporter assetImporter = AssetImporter.GetAtPath(path);
                if (assetImporter != null) {
                    assetImporter.assetBundleName = name.ToLower() + BundleConfig.BUNDLE_SUFFIX;
                    return true;
                }
            }

            return false;
        }
    }
}
