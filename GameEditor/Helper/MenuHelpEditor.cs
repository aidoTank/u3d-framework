using UnityEditor;
using UnityEngine;

namespace GameEditor
{
    public class MenuHelpEditor
    {
        #region 菜单置灰验证

        [MenuItem(MenuConfig.TOOLS_HELPER_NAME)]
        private static void DoProcessTexture()
        {
        }

        [MenuItem(MenuConfig.TOOLS_HELPER_NAME, true)]
        private static bool ValidationProcess()
        {
            UnityEngine.Object target = Selection.activeObject;
            if(target != null) {
                return target.GetType() == typeof(Texture2D);
            }
            return false;
        }

        #endregion

        #region 按钮状态切换

        public static bool IsOpen = false;

        [MenuItem(MenuConfig.TOOLS_OPEN_NAME, true)]
        private static bool ValidateGuideEnable()
        {
            return !IsOpen;
        }

        [MenuItem(MenuConfig.TOOLS_OPEN_NAME)]
        private static void GuideEnable()
        {
            IsOpen = true;
        }

        [MenuItem(MenuConfig.TOOLS_CLOSE_NAME, true)]
        private static bool ValidateGuideDisable()
        {
            return IsOpen;
        }

        [MenuItem(MenuConfig.TOOLS_CLOSE_NAME)]
        private static void GuideDisable()
        {
            IsOpen = false;
        }

        #endregion
    }
}
