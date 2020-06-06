using UnityEditor;

/***
 * BuildMenuEditor.cs
 * 
 * @author administrator
 */
namespace GameEditor
{
    public class BuildMenuEditor : Editor
    {
        [MenuItem(MenuConfig.TOOLS_BUILD_ANDROID)]
        private static void BuildAndroid()
        {
            if (IsOperation()) {
                BuildSetting.BulidPacket(BuildTarget.Android, PathConfig.APP_OUTPUT);
            }
        }

        [MenuItem(MenuConfig.TOOLS_BUILD_IOS)]
        private static void BuildiOS()
        {
            if (IsOperation()) {
                BuildSetting.BulidPacket(BuildTarget.iOS, PathConfig.APP_OUTPUT);
            }
        }

        [MenuItem(MenuConfig.TOOLS_BUILD_StandaloneWindows)]
        private static void BuildStandaloneWindow()
        {
            if (IsOperation()) {
                BuildSetting.BulidPacket(BuildTarget.StandaloneWindows, PathConfig.APP_OUTPUT);
            }
        }

        [MenuItem(MenuConfig.TOOLS_BUILD_StandaloneWindows64)]
        private static void BuildStandaloneWindow64()
        {
            if (IsOperation()) {
                BuildSetting.BulidPacket(BuildTarget.StandaloneWindows64, PathConfig.APP_OUTPUT);
            }
        }

        [MenuItem(MenuConfig.TOOLS_BUILD_ACTIVEPLATFORM)]
        private static void BuildActivePlatform()
        {
            if (IsOperation()) {
                BuildSetting.BulidPacket(EditorUserBuildSettings.activeBuildTarget, PathConfig.APP_OUTPUT);
            }
        }

        [MenuItem(MenuConfig.TOOLS_BUILD_CLEARBUILD)]
        private static void BuildClearAll()
        {
            if (IsOperation("确认清理所有项目包")) {
                BuildSetting.ClearAllBuild(PathConfig.APP_OUTPUT);
            }
        }

        private static bool IsOperation(string message = "确认导出项目包")
        {
            return EditorUtility.DisplayDialog("提示", message, "确认", "取消");
        }
    }
}
