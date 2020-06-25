using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class TestEditor
{
    [MenuItem("Assets/ABC")]
    private static void Test()
    {
        BuildPipeline.BuildAssetBundles("Assets/StreamingAssets/", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
        AssetDatabase.Refresh();
    }
}
