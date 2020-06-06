// ========================================
// Copyright (c) 2017 KingSoft, All rights reserved.
// http://www.kingsoft.com
// 
// Framwork
// 
// Filename: AssetCollector.cs
// Date:     2017/06/09
// Author:   xiangjinbao
// Email:    xiangjinbao@kingsoft.com
// ========================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xsj.framework
{
    public class AssetCollector
    {
        public delegate void CollectComplete(AssetCollector collector);

        private string m_path;
        private string[] m_depends;

        private AssetDataProxy m_mainAsset;
        private List<AssetDataProxy> m_dependAssets = new List<AssetDataProxy>();
        private CollectComplete m_collector;

        private bool m_isDone = false;
        private bool m_needDispose = false;

        public Coroutine CreateAC(string path, string[] depends, CollectComplete collector)
        {
            return CoroutineUtil.StartCoroutine(CreateACCoroutine(path, depends, collector));
        }

        public IEnumerator CreateACCoroutine(string path, string[] depends, CollectComplete collector = null)
        {
            m_collector = collector;
            m_path = path;
            m_depends = depends;

            AssetFactory.Instance.RecordAssociate(path, depends);
            if (depends != null) {
                for (int i = 0; i < depends.Length; ++i) {
                    string dep = depends[i];
                    AssetDataProxy proxy = new AssetDataProxy(dep);
                    proxy.OnWork();
                    yield return proxy.Coroutine;
                    m_dependAssets.Add(proxy);
                }

            }

            if (path != null) {
                AssetDataProxy proxy = new AssetDataProxy(path);
                proxy.OnWork();
                yield return proxy.Coroutine;
                m_mainAsset = proxy;
            }
            m_isDone = true;
            if (m_needDispose) {
                Dispose();
            }
            if (collector != null) {
                collector(this);
            }
        }

        public List<AssetDataProxy> GetDepends()
        {
            return m_dependAssets;
        }

        public AssetDataProxy GetMainAsset()
        {
            return m_mainAsset;
        }

        public void Dispose()
        {
            if (!m_isDone) {
                m_needDispose = true;
                return;
            }

            if (m_mainAsset != null) {
                m_mainAsset.Dispose();
            }
            for (int i = 0; i < m_dependAssets.Count; ++i) {
                if (m_dependAssets[i] != null) {
                    m_dependAssets[i].Dispose();
                }
            }
            m_mainAsset = null;
            m_dependAssets.Clear();
        }
    }
}

