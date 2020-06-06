// ========================================
// Copyright (c) 2017 KingSoft, All rights reserved.
// http://www.kingsoft.com
// 
// Framwork
// 
// Filename: AssetFactory.cs
// Date:     2017/06/09
// Author:   xiangjinbao
// Email:    xiangjinbao@kingsoft.com
// ========================================

using System.Collections.Generic;
using UnityEngine;

namespace xsj.framework
{
    internal class AssetFactory : Singleton<AssetFactory>, IAssetFactory
    {
        // 缓存
        private Dictionary<string, AssetData> m_assetInMemory = new Dictionary<string, AssetData>();
        private Dictionary<string, AssetAssociate> m_assetAssociate = new Dictionary<string, AssetAssociate>();

        // 回收
        private List<AssetData> m_recycleAsset = new List<AssetData>();
        private Dictionary<string, AssetData> m_recycleLookUp = new Dictionary<string, AssetData>();

        // 加载器-需要指定策略
        private ILoader m_loader = new WWWLoader(new LoadStrategy(ThreadPriority.Low, 6));

        public ILoader Loader
        {
            get {
                return m_loader;
            }
        }

        /// <summary>
        /// 进入加载
        /// </summary>
        /// <param name="proxy"></param>
        public static void EnterLoad(AssetDataProxy proxy)
        {
            Instance.LoadAsset(proxy);
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="proxy"></param>
        public void LoadAsset(AssetDataProxy proxy)
        {
            if (proxy == null) {
                return;
            }

            proxy.Data = GetAsset(proxy.Url);
            if (proxy.Data.IsLoaded) {
                proxy.AssetComplete();
                return;
            }

            Loader.Load(proxy);
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        public AssetData GetAsset(string assetPath)
        {
            AssetData assetData = null;
            if(!m_assetInMemory.TryGetValue(assetPath, out assetData)) {
                assetData = new AssetData();
                assetData.AssetUrl = assetPath;
                m_assetInMemory.Add(assetPath, assetData);
            }

            assetData.AddRef();
            return assetData;
        }

        /// <summary>
        /// 回收资源
        /// </summary>
        /// <param name="asset"></param>
        public void RecycleAsset(AssetData asset)
        {
            if (asset == null) {
                return;
            }
            if (!m_recycleLookUp.ContainsKey(asset.AssetUrl) && m_assetInMemory.ContainsKey(asset.AssetUrl)) {
                m_recycleLookUp[asset.AssetUrl] = asset;
                m_recycleAsset.Add(asset);
            }
        }

        /// <summary>
        /// 更改资源状态-方法需要优化 TODO
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="inMemory"></param>
        public void ChangeAssetState(AssetData asset, bool inMemory)
        {
            foreach (KeyValuePair<string, AssetAssociate> pair in m_assetAssociate) {
                if (pair.Value.HasAA(asset.AssetUrl)) {
                    pair.Value.SetAssociate(asset.AssetUrl, inMemory);
                }
            }
        }

        private AssetAssociate GetAssociate(string path)
        {
            AssetAssociate associate = null;
            if (m_assetAssociate.TryGetValue(path, out associate)) {
                return associate;
            }
            return null;
        }

        public void UnloadAssets(bool unloadAll)
        {
            Dictionary<string, int> safeCount = new Dictionary<string, int>();
            while (m_recycleAsset.Count > 0) {
                AssetData asset = m_recycleAsset[0];
                m_recycleAsset.RemoveAt(0);
                if (asset == null) {
                    continue;
                }
                AssetAssociate aa = GetAssociate(asset.AssetUrl);
                if (aa != null && aa.HasAssociateInMemory) {
                    int nCount = 0;
                    safeCount.TryGetValue(asset.AssetUrl, out nCount);
                    if (nCount < 5) {
                        //确保资源删除时，被访问不要超过5次，5次遍历列表失败，此资源强制删除
                        m_recycleAsset.Add(asset);
                        if (safeCount.ContainsKey(asset.AssetUrl)) {
                            safeCount[asset.AssetUrl]++;
                        } else {
                            safeCount.Add(asset.AssetUrl, 1);
                        }
                        continue;
                    }
                }
                if (asset != null && !asset.IsUnLoaded) {
                    ChangeAssetState(asset, false);
                    asset.UnLoad(true);
                    m_assetInMemory.Remove(asset.AssetUrl);
                    m_recycleLookUp.Remove(asset.AssetUrl);
                }
            }
        }

        public void RecordAssociate(string url, string[] depends)
        {
            AssetAssociate aa = null;
            url = AssetUtility.GetAssetRealPath(url);

            if (depends != null) {
                for (int i = 0; i < depends.Length; ++i) {
                    string tdp = AssetUtility.GetAssetRealPath(depends[i]);
                    if (!m_assetAssociate.TryGetValue(tdp, out aa)) {
                        aa = new AssetAssociate();
                        aa.Url = tdp;
                        m_assetAssociate.Add(tdp, aa);
                    }
                    aa.SetAssociate(url, aa.GetAAState(url));
                }
            }
        }
    }
}
