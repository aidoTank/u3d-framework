/***
 * MenuConfig.cs
 * 
 * @author administrator
 */
namespace GameEditor
{
    public static class MenuConfig
    {
        // Build
        public const string TOOLS_BUILD_IOS = "Tools/Build/PacketBuild/iOS";
        public const string TOOLS_BUILD_ANDROID = "Tools/Build/PacketBuild/Android";
        public const string TOOLS_BUILD_StandaloneWindows = "Tools/Build/PacketBuild/Windows";
        public const string TOOLS_BUILD_StandaloneWindows64 = "Tools/Build/PacketBuild/Windows64";
        public const string TOOLS_BUILD_ACTIVEPLATFORM = "Tools/Build/PacketBuild/ActivePlatform";
        public const string TOOLS_BUILD_CLEARBUILD = "Tools/Build/ClearAllBuild";

        // AssetBundle
        public const string TOOLS_BUNDLE_BUILD_ANDROID = "Tools/Bundle/BundleBuild/Android";
        public const string TOOLS_BUNDLE_BUILD_IOS = "Tools/Bundle/BundleBuild/iOS";
        public const string TOOLS_BUNDLE_BUILD_WINDOW = "Tools/Bundle/BundleBuild/Window";
        public const string TOOLS_BUNDLE_BUILD_WINDOW64 = "Tools/Bundle/BundleBuild/Window64";
        public const string TOOLS_BUNDLE_BUILD_ACTIVEPLATFORM = "Tools/Bundle/BundleBuild/ActivePlatform";
        public const string TOOLS_BUNDLE_UPDATE = "Tools/Bundle/BundleName/Update";
        public const string TOOLS_BUNDLE_CLEAR = "Tools/Bundle/BundleName/Clear";
        public const string TOOLS_BUNDLE_CLEARBUILD = "Tools/Bundle/ClearAllBuild";

        // Helper
        public const string TOOLS_HELPER_MEMORY = "Tools/Helper/ProfilerMemory";
        public const string TOOLS_HELPER_HELPER = "Tools/Helper/ProcessTexture";
        public const string TOOLS_HELPER_OPEN = "Tools/Helper/Open";
        public const string TOOLS_HELPER_CLOSE = "Tools/Helper/Close";
        public const string TOOLS_HELPER_SCRIPTABLE = "Tools/Helper/CreateScriptable";

        // Interface
        public const string TOOLS_INTERFACE_PROJECTBUILD = "Tools/Interface/ProjectBuildWindow";


        // UGUI
        public const string TOOLS_UGUI_CREATEATLAS = "Assets/Tools/CreateUGUIAtlas";
        public const string TOOLS_UGUI_ATLASPREFAB = "Assets/Tools/CreateAtlaPrefab";
    }
}
