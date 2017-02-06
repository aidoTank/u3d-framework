using System.IO;
using UnityEditor;

namespace GameEditor
{
    public class BundleMenuEditor : Editor
    {
        [MenuItem(MenuConfig.TOOLS_BUNDLE_UPDATE)]
        private static void UpdateBundleName()
        {
            BundleSetting.UpdateAllBundleName(false);
        }

        [MenuItem(MenuConfig.TOOLS_BUNDLE_CLEAR)]
        private static void ClearBundleName()
        {
            BundleSetting.ClearAllBundleName();
        }

        [MenuItem(MenuConfig.TOOLS_BUNDLE_EXPORT)]
        private static void ExportAssetBundle()
        {
            if (!Directory.Exists(PathConfig.RES_OUTPUT)) {
                Directory.CreateDirectory(PathConfig.RES_OUTPUT);
            }

            BuildPipeline.BuildAssetBundles(
                PathConfig.RES_OUTPUT, 
                BuildAssetBundleOptions.ForceRebuildAssetBundle, 
                EditorUserBuildSettings.activeBuildTarget
                );
        }

        [MenuItem(MenuConfig.TOOLS_BUNDLE_CLEARBUILD)]
        private static void ClearAllBuild()
        {
            BundleSetting.ClearAllBuild(PathConfig.RES_OUTPUT);
        }
    }
}
