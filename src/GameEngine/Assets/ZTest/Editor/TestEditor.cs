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

    [MenuItem("Assets/ABC2")]
	private static void Test2()
	{
		Object[] UnityAssets = AssetDatabase.LoadAllAssetsAtPath("Resources/unity_builtin_extra1");
        Debug.LogError(UnityAssets.Length);
		foreach (var asset in UnityAssets) {
			Debug.Log(asset.name);
		}
	}
}
