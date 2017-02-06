using UnityEditor;

/***
 * BuildCommandEditor.cs
 * 
 * @author abaojin
 */
namespace GameEditor
{
    public class BuildMenuEditor : Editor
    {
        [MenuItem(MenuConfig.TOOLS_BUILD_ANDROID)]
        private static void BuildAndroid()
        {
            BuildSetting.BulidPacket(BuildTarget.Android, PathConfig.APP_OUTPUT);
        }

        [MenuItem(MenuConfig.TOOLS_BUILD_IOS)]
        private static void BuildiOS()
        {
            BuildSetting.BulidPacket(BuildTarget.iOS, PathConfig.APP_OUTPUT);
        }

        [MenuItem(MenuConfig.TOOLS_BUILD_StandaloneWindows)]
        private static void BuildStandaloneWindow()
        {
            BuildSetting.BulidPacket(BuildTarget.StandaloneWindows, PathConfig.APP_OUTPUT);
        }

        [MenuItem(MenuConfig.TOOLS_BUILD_StandaloneWindows64)]
        private static void BuildStandaloneWindow64()
        {
            BuildSetting.BulidPacket(BuildTarget.StandaloneWindows64, PathConfig.APP_OUTPUT);
        }

        [MenuItem(MenuConfig.TOOLS_BUILD_ACTIVEPLATFORM)]
        private static void BuildActivePlatform()
        {
            BuildSetting.BulidPacket(EditorUserBuildSettings.activeBuildTarget, PathConfig.APP_OUTPUT);
        }

        [MenuItem(MenuConfig.TOOLS_BUILD_CLEARBUILD)]
        private static void BuildClearAll()
        {
            BuildSetting.ClearAllBuild(PathConfig.APP_OUTPUT);
        }

    }
}
