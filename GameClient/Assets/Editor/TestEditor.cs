using GameEditor;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class TestEditor : Editor
{
    [MenuItem("Assets/Test")]
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
