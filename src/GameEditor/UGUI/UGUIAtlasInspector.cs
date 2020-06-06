using UnityEditor;
using UnityEngine;
using GameEngine;

/***
 * UGUIAtlasInspector.cs
 * 
 * @anthor administrator
 */
namespace GameEditor
{
    [CustomEditor(typeof(UGUIAtlas))]
    public class UGUIAtlasInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            UGUIAtlas atlas = target as UGUIAtlas;
            atlas.MainTex = EditorGUILayout.ObjectField("MainTextture", atlas.MainTex, typeof(Texture2D), true) as Texture2D;

            if (GUILayout.Button("Refresh")) {
                if (atlas.MainTex == null) {
                    string path = EditorUtility.OpenFilePanel("Select a Sprite", Application.dataPath, "png");
                    if (!string.IsNullOrEmpty(path)) {
                        atlas.MainTex = AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D)) as Texture2D;
                    }
                }

                if (atlas.MainTex != null) {
                    string path = AssetDatabase.GetAssetPath(atlas.MainTex);
                    TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
                    if (importer != null && importer.textureType == TextureImporterType.Sprite && importer.spriteImportMode == SpriteImportMode.Multiple) {
                        Object[] objs = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(atlas.MainTex));
                        atlas.SpriteLists.Clear();
                        foreach (Object o in objs) {
                            if (o.GetType() == typeof(Texture2D)) {
                                atlas.MainTex = o as Texture2D;
                            } else if (o.GetType() == typeof(Sprite)) {
                                atlas.SpriteLists.Add(o as Sprite);
                            }
                        }
                    } else {
                        atlas.MainTex = null;
                    }
                }
            }

            if (atlas.SpriteLists.Count > 0) {
                foreach (Sprite s in atlas.SpriteLists) {
                    EditorGUILayout.ObjectField(s.name, s, typeof(Sprite), true);
                }
            }
        }
    }
}
