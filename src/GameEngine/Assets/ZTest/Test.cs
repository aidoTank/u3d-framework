using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public bool IsTest1 = false;
    public bool IsTest2 = false;
    public string Path;

    public AssetBundle ab;

    private void Update()
    {
        if (IsTest1) {
            IsTest1 = false;
            ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + Path);
        }

        if (IsTest2) {
            IsTest2 = false;
            ab.LoadAllAssets();
        }
    }
}
