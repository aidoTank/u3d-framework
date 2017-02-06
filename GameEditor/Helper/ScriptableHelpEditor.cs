using UnityEditor;
using UnityEngine;

namespace GameEditor
{
    public class ScriptableHelpEditor
    {
        [MenuItem(MenuConfig.TOOLS_HELPER_SCRIPTABLE)]
        public static void CreateAsset()
        {
            ScriptableData data = ScriptableObject.CreateInstance<ScriptableData>();
            data.nzme = "zhangsan";
            data.age = 100;

            string assetName = "Assets/GlobalConfig.asset";

            AssetDatabase.CreateAsset(data, assetName);
            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();
        }
    }

    /// <summary>
    /// 特别注意数据对象必须单独定义，避免数据丢失情况
    /// </summary>
    public class ScriptableData : ScriptableObject
    {
        public string nzme;
        public int age;
    }
}
