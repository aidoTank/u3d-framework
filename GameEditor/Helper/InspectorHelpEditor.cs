using UnityEditor;
using UnityEngine;

/***
 * InspectorHelpEditor.cs
 * 
 * @author abaojin
 */
namespace GameEditor
{
    /// <summary>
    /// 默认资源编辑器修改
    /// </summary>
    [CustomEditor(typeof(DefaultAsset))]
    public class InspectorHelpEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            string targetPath = AssetDatabase.GetAssetPath(target);

            if (targetPath.EndsWith(".unity")) {
                GUILayout.Button("I'm scene!");
            }else if (targetPath.EndsWith("")) {
                //GUILayout.Button("I'm directory!");
            }
        }
    }
}
