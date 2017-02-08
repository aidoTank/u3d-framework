using UnityEditor;
using UnityEngine;

/***
 * ScriptableHelpEditor.cs
 * 
 * @author abaojin
 */
namespace GameEditor
{
    /// <summary>
    /// 创建序列化脚本
    /// </summary>
    public class ScriptableHelpEditor
    {
        [MenuItem(MenuConfig.TOOLS_HELPER_SCRIPTABLE)]
        public static void CreateAsset()
        {
            ConfigScriptable data = ScriptableObject.CreateInstance<ConfigScriptable>();
            data.ProductName = "testproject";
            data.ProductVersion = "1.0";

            string assetName = "Assets/_Config.asset";

            AssetDatabase.CreateAsset(data, assetName);
            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();
        }
    }

    /// <summary>
    /// 特别注意数据对象必须单独定义，避免数据丢失情况（不要像这里写在这里）
    /// </summary>
    public class ConfigScriptable : ScriptableObject
    {
        public string ProductName;
        public string ProductVersion;

        public enum DevelopMode
        {
            Debug,
            Release
        }

        public DevelopMode DepMode = DevelopMode.Debug; 
    }
}
