using UnityEditor;
using UnityEngine;

public class TestEditor : Editor
{
    [MenuItem("Assets/Test")]
    public static void TestExecute()
    {
        Object target = Selection.activeObject;
        if (target == null) {
            return;
        }
        TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(target));
    }
}
