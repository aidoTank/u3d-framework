using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public bool IsTest1 = false;
    public bool IsTest2 = false;

    public bool IsTest3 = false;
    public bool IsTest4 = false;

    public string Path;

    private AssetBundleManifest manifest;

    private List<AssetBundle> depBundleList = new List<AssetBundle>();
    private AssetBundle mainBundle;
    private GameObject instObj;

    private void Start()
    {
        AssetBundle assetBundle = AssetBundle.LoadFromFile(GetABPath("StreamingAssets"));
        manifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
    }

    private void Update()
    {
        if (IsTest1) {
            IsTest1 = false;
            string[] depABNames = manifest.GetAllDependencies(Path);
            foreach (string abName in depABNames) {
                Debug.LogError(abName);
                AssetBundle depBundle = AssetBundle.LoadFromFile(GetABPath(abName));
                depBundleList.Add(depBundle);
            }
            mainBundle = AssetBundle.LoadFromFile(GetABPath(Path));
        }

        if (IsTest2) {
            IsTest2 = false;
            GameObject obj = mainBundle.LoadAsset<GameObject>("TestObject");
            instObj = Instantiate(obj);
        }

        if (IsTest3) {
            IsTest3 = false;
            GameObject.DestroyImmediate(instObj);
            Resources.UnloadUnusedAssets();
        }

        if (IsTest4) {
            IsTest4 = false;
            mainBundle.Unload(true);
            Resources.UnloadUnusedAssets();
        }
    }

    private string GetABPath(string name)
    {
        return string.Format("{0}/{1}", Application.streamingAssetsPath, name);
    }
}
