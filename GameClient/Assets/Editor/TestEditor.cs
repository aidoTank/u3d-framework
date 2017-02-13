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
        Assembly assembly = Assembly.Load("UnityEngine");
        Type[] types = assembly.GetExportedTypes();

        foreach(Type t in types) {
            Debug.LogError(t.FullName);
        }
    }
}
