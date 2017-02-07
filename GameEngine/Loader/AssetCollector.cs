using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * AssetCollector.cs
 * 
 * @author abaojin
 */
namespace GameEngine
{
    public class AssetCollector
    {
        public delegate void CollectComplete(AssetCollector collector);

        private string mPath;
        private string[] mDepends;

        private AssetProxy mMainAsset;
        private List<AssetProxy> mDependAssets = new List<AssetProxy>();
        private CollectComplete mCollector;

        private bool isDone = false;
        private bool needDispose = false;

        public Coroutine CreateAC(string path, string[] depends, CollectComplete collector)
        {
            return CoroutineUtils.StartCoroutine(CreateACCoroutine(path, depends, collector));
        }

        public IEnumerator CreateACCoroutine(string path, string[] depends, CollectComplete collector = null)
        {
            mCollector = collector;
            mPath = path;
            mDepends = depends;


            AssetManager.Instance.RecordAssociate(path, depends);
            if (depends != null) {
                for (int i = 0; i < depends.Length; ++i) {
                    string dep = depends[i];
                    AssetProxy proxy = new AssetProxy(dep);
                    proxy.Work();
                    yield return proxy.coroutine;
                    mDependAssets.Add(proxy);
                }

            }

            if (path != null) {
                AssetProxy proxy = new AssetProxy(path);
                proxy.Work();
                yield return proxy.coroutine;
                mMainAsset = proxy;
            }
            isDone = true;
            if (needDispose) {
                Dispose();
            }
            if (collector != null) {
                collector(this);
            }
        }

        public List<AssetProxy> GetDepends()
        {
            return mDependAssets;
        }

        //public AssetProxy GetDepend(string path)
        //{
        //    AssetProxy ptr = null;
        //    if (!mAssetMap.TryGetValue(path, out ptr))
        //        return null;
        //    return ptr;

        //}

        public AssetProxy GetMainAsset()
        {
            return mMainAsset;
        }

        public void Dispose()
        {
            if (!isDone) {
                needDispose = true;
                return;
            }

            if (mMainAsset != null) {
                mMainAsset.Dispose();
            }
            for (int i = 0; i < mDependAssets.Count; ++i) {
                if (mDependAssets[i] != null) {
                    mDependAssets[i].Dispose();
                }
            }
            mMainAsset = null;
            mDependAssets.Clear();
        }
    }
}

