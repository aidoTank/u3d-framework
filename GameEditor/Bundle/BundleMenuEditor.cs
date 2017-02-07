using UnityEditor;

/***
 * BundleMenuEditor.cs
 * 
 * @author abaojin
 */
namespace GameEditor
{
    /// <summary>
    /// 菜单编辑器
    /// </summary>
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

        [MenuItem(MenuConfig.TOOLS_BUNDLE_BUILD_ANDROID)]
        private static void BuildAssetBundleAndroid()
        {
            if (IsOperation()) {
                BundleSetting.BuildAssetBundle(BuildTarget.Android, PathConfig.RES_OUTPUT);
            }
        }

        [MenuItem(MenuConfig.TOOLS_BUNDLE_BUILD_IOS)]
        private static void BuildAssetBundleiOS()
        {
            if (IsOperation()) {
                BundleSetting.BuildAssetBundle(BuildTarget.iOS, PathConfig.RES_OUTPUT);
            }
        }

        [MenuItem(MenuConfig.TOOLS_BUNDLE_BUILD_WINDOW)]
        private static void BuildAssetBundleStandaloneWindow()
        {
            if (IsOperation()) {
                BundleSetting.BuildAssetBundle(BuildTarget.StandaloneWindows, PathConfig.RES_OUTPUT);
            }
        }

        [MenuItem(MenuConfig.TOOLS_BUNDLE_BUILD_WINDOW64)]
        private static void BuildAssetBundleStandaloneWindow64()
        {
            if (IsOperation()) {
                BundleSetting.BuildAssetBundle(BuildTarget.StandaloneWindows64, PathConfig.RES_OUTPUT);
            }
        }

        [MenuItem(MenuConfig.TOOLS_BUNDLE_BUILD_ACTIVEPLATFORM)]
        private static void BuildAssetBundleActivePlatform()
        {
            if (IsOperation()) {
                BundleSetting.BuildAssetBundle(EditorUserBuildSettings.activeBuildTarget, PathConfig.RES_OUTPUT);
            }
        }

        [MenuItem(MenuConfig.TOOLS_BUNDLE_CLEARBUILD)]
        private static void ClearAllBuild()
        {
            if (IsOperation("确认要清理所有资源")) {
                BundleSetting.ClearAllBuild(PathConfig.RES_OUTPUT);
            }
        }

        private static bool IsOperation(string message = "确认导出资源包")
        {
            return EditorUtility.DisplayDialog("提示", message, "确认", "取消");
        }
    }
}
