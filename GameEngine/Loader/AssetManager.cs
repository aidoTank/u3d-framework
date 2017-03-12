using System.Collections;
using System.Collections.Generic;

/***
 * AssetManager.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    /// <summary>
    /// 资源管理器
    /// V1.2管理器已不再对外开放
    /// 对外执行的借口只有AssetProxy类
    /// </summary>
    internal class AssetManager : Singleton<AssetManager>
    {
        //正在下载的AssetBundle
        private Dictionary<string, AssetData> mLoadingAssetBundle = new Dictionary<string, AssetData>();

        //关联列表
        private Dictionary<string, AssetAssociate> mAssociatelist = new Dictionary<string, AssetAssociate>();


        //内存中的资源
        private Dictionary<string, AssetData> mAssetInMemory = new Dictionary<string, AssetData>();

        //资源释放控制
        private List<AssetData> mRecyAssets = new List<AssetData>();
        private Dictionary<string, AssetData> mRecycleLookUP = new Dictionary<string, AssetData>();

        private int mLoadingWork = 0;


        /// <summary>
        /// 是否有权利加载
        /// </summary>
        private bool hasAccess {
            get {
                return mLoadingWork < QualitySetting.MaxLoadingAsset;
            }
        }

        private void IncreaseLoadingWork()
        {
            mLoadingWork++;
        }
        private void DecreaseLoadingWork()
        {
            if (--mLoadingWork < 0)
                mLoadingWork = 0;
        }

        public void LoadResource(AssetProxy proxy)
        {
            if (proxy == null)
                return;
            proxy.data = AutoGetAssetData(proxy.url);
            if (proxy.data.isLoaded) {
                proxy.AssetComplete();
                return;
            }

            CoroutineUtils.StartCoroutine(DoLoadAsset(proxy));
        }
        /// <summary>
        /// 函数实现的目的：
        /// 1 执行加载
        /// 2 派发通知
        /// 3 执行排队
        /// </summary>
        /// <param name="assetProxy"></param>
        /// <returns></returns>
        public IEnumerator DoLoadAsset(AssetProxy assetProxy)
        {
            ///在加载：
            if (assetProxy.data.isLoading) {
                while (!assetProxy.abort && assetProxy.data.isLoading) {
                    yield return 1;
                }
                if (assetProxy.abort) {
                    yield break;
                }

                assetProxy.AssetComplete();
                yield break;
            }

            ///排队等待加载:
            while (!hasAccess && !assetProxy.abort) {
                yield return 1;
            }
            if (assetProxy.abort) {
                yield break;
            }

            ///再检测一次防止，在这期间有多个同时出现的任务：
            if (assetProxy.data.isLoading) {
                while (!assetProxy.abort && assetProxy.data.isLoading) {
                    yield return 1;
                }
                if (assetProxy.abort) {
                    yield break;
                }

                assetProxy.AssetComplete();
                yield break;
            }

            //如果这期间加载完成了也要返回
            if (assetProxy.data.isLoaded) {
                assetProxy.AssetComplete();
                yield break;
            }

            IncreaseLoadingWork();

            //将权重刷到AssetData中
            assetProxy.data.threadPriority = assetProxy.threadPriority;
            //执行加载
            yield return assetProxy.data.BeginDownLoad();

            DecreaseLoadingWork();

            assetProxy.AssetComplete();
        }

        public AssetData AutoGetAssetData(string assetPath)
        {
            //查找AssetData
            AssetData assetData = GetAssetByPath(assetPath);
            if (assetData == null) {
                assetData = CreateAssetData(assetPath);
            }

            assetData.AddRef();
            return assetData;
        }

        private AssetData CreateAssetData(string assetPath)
        {
            AssetData aData = new AssetData();
            aData.url = assetPath;
            mAssetInMemory.Add(assetPath, aData);
            return aData;
        }
        internal AssetData GetAssetByPath(string assetPath)
        {
            AssetData aData = null;
            mAssetInMemory.TryGetValue(assetPath, out aData);
            return aData;
        }

        internal void ChangeAssetState(AssetData asset, bool inMemory)
        {
            foreach (KeyValuePair<string, AssetAssociate> pair in mAssociatelist) {
                if (pair.Value.HasAA(asset.url)) {
                    pair.Value.SetAssociate(asset.url, inMemory);
                }
            }
        }

        /// <summary>
        /// 回收资源
        /// </summary>
        /// <param name="asset"></param>
        internal void RecycleAsset(AssetData asset)
        {
            if (asset == null)
                return;
            if (!mRecycleLookUP.ContainsKey(asset.url) && mAssetInMemory.ContainsKey(asset.url)) {
                mRecycleLookUP[asset.url] = asset;
                mRecyAssets.Add(asset);
            }
        }

        internal void Update()
        {
            //long totoalMemory = Profiler.usedHeapSize;

            //if (!Application.isEditor&&totoalMemory >= 400000000)
            //{
            //    //系统内存大于400M的时候开始清理
            //    //if(totoalMemory > 420000000)
            //    //{
            //    //    Debug.Log("强制调用了GC");
            //    //    GC.Collect();
            //    //}
            //    //UnloadAssets(false);

            //}
        }

        private AssetAssociate GetAssociate(string path)
        {
            AssetAssociate pathassociate = null;
            if (mAssociatelist.TryGetValue(path, out pathassociate)) {
                return pathassociate;
            }
            return null;
        }

        public void UnloadAssets(bool unloadAll)
        {
            Dictionary<string, int> safeCount = new Dictionary<string, int>();
            while (mRecyAssets.Count > 0) {
                AssetData asset = mRecyAssets[0];
                mRecyAssets.RemoveAt(0);
                if (asset == null) {
                    continue;
                }
                AssetAssociate aa = GetAssociate(asset.url);
                if (aa != null && aa.HasAssociateInMemory) {
                    int nCount = 0;
                    safeCount.TryGetValue(asset.url, out nCount);
                    if (nCount < 5) {
                        //确保资源删除时，被访问不要超过5次，5次遍历列表失败，此资源强制删除
                        mRecyAssets.Add(asset);
                        if (safeCount.ContainsKey(asset.url)) {
                            safeCount[asset.url]++;
                        } else {
                            safeCount.Add(asset.url, 1);
                        }
                        continue;
                    }
                }
                if (asset != null && !asset.IsUnLoaded) {
                    ChangeAssetState(asset, false);
                    asset.UnLoad(true);
                    mAssetInMemory.Remove(asset.url);
                    mRecycleLookUP.Remove(asset.url);
                }
            }
        }

        //记录引用
        public void RecordAssociate(string url, string[] depends)
        {
            AssetAssociate aa = null;
            url = AssetUtility.GetRealPath(url);

            if (depends != null) {
                for (int i = 0; i < depends.Length; ++i) {
                    string tdp = AssetUtility.GetRealPath(depends[i]);
                    if (!mAssociatelist.TryGetValue(tdp, out aa)) {
                        aa = new AssetAssociate();
                        aa.url = tdp;
                        mAssociatelist.Add(tdp, aa);
                    }
                    aa.SetAssociate(url, aa.GetAAState(url));
                }
            }

        }

        internal void Reset()
        {
            mLoadingWork = 0;
        }
    }
}
