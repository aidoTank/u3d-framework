using GameEditor;
using UnityEditor;
using UnityEngine;

public class TestEditor : Editor
{
    public static void TestExecute()
    {
        UnityEngine.Object target = Selection.activeObject;
        if (target == null) {
            return;
        }
        string productName = EditorUtils.GetProductName();
        string productVersion = EditorUtils.GetProductVersion();

        Debug.LogError(productName);
        Debug.LogError(productVersion);
    }
}
