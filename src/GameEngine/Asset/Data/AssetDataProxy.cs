// ========================================
// Copyright (c) 2017 KingSoft, All rights reserved.
// http://www.kingsoft.com
// 
// Framwork
// 
// Filename: AssetProxy.cs
// Date:     2017/06/09
// Author:   xiangjinbao
// Email:    xiangjinbao@kingsoft.com
// ========================================

using System.Collections;
using UnityEngine;

namespace xsj.framework
{
    /// <summary>
    /// 资源加载代理对象，需要优化
    /// </summary>
    public class AssetDataProxy
    {
        private string m_url;
        private AssetData m_data;
        private bool m_abort = false;
        private AssetEventDelegate m_assetDelegate;

        /// <summary>
        /// 实例化代理对象
        /// </summary>
        /// <param name="url"></param>
        /// <param name="aDelegate"></param>
        /// <param name="auto"></param>
        public AssetDataProxy(string url, AssetEventDelegate aDelegate = null, bool auto = false)
        {
            m_url = AssetUtility.GetAssetRealPath(url);
            m_assetDelegate = aDelegate;

            if (auto) {
                OnWork();
            }
        }

        /// <summary>
        /// 进入工作状态
        /// </summary>
        public void OnWork()
        {
            AssetFactory.EnterLoad(this);
        }

        /// <summary>
        /// 资源Url
        /// </summary>
        public string Url
        {
            get {
                return m_url;
            }
        }

        /// <summary>
        /// 数据对象
        /// </summary>
        public AssetData Data
        {
            get {
                return m_data;
            }
            internal set {
                m_data = value;
            }
        }

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsDone
        {
            get {
                return m_data == null || m_data.IsDone;
            }
        }

        /// <summary>
        /// 是否退出
        /// </summary>
        public bool IsAbort
        {
            get {
                return m_abort;
            }
        }

        /// <summary>
        /// 携程等待
        /// </summary>
        public Coroutine Coroutine
        {
            get {
                return CoroutineUtil.StartCoroutine(WaitforComplete());
            }
        }

        /// <summary>
        /// 等待完成
        /// </summary>
        /// <returns></returns>
        private IEnumerator WaitforComplete()
        {
            while (!IsDone) {
                yield return 1;
            }
        }

        /// <summary>
        /// 退出加载
        /// </summary>
        public void Abort()
        {
            if (m_data != null) {
                m_data.Release();
            }
            m_url = string.Empty;
            m_abort = true;
            m_data = null;
        }

        /// <summary>
        /// 资源加载完成
        /// </summary>
        public void AssetComplete()
        {
            if (m_assetDelegate != null) {
                m_assetDelegate(this);
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (m_data != null) {
                m_data.Release();
            }

            m_url = string.Empty;
            m_abort = true;
            m_data = null;
            m_assetDelegate = null;
        }
    }
}
