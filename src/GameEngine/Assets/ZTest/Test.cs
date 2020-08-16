using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public bool IsTest1 = false;
    public bool IsTest2 = false;

    public string Path;

    public AssetBundle mainBundle;
    public Object[] assetObj;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (IsTest1) {
            IsTest1 = false;
            mainBundle = AssetBundle.LoadFromFile(GetABPath(Path));
        }

        if (IsTest2) {
            IsTest2 = false;
            assetObj = mainBundle.LoadAllAssets();
            foreach (Object obj in assetObj) {
                if (obj is GameObject) {
                    GameObject.Instantiate(obj);
                }
            }
        }
    }

    private string GetABPath(string name)
    {
        return string.Format("{0}/{1}", Application.streamingAssetsPath, name);
    }
}
