// ========================================
// Copyright (c) 2017 KingSoft, All rights reserved.
// http://www.kingsoft.com
// 
// Framwork
// 
// Filename: LoaderBase.cs
// Date:     2017/06/09
// Author:   xiangjinbao
// Email:    xiangjinbao@kingsoft.com
// ========================================

using System.Collections.Generic;
using System.Collections;

namespace xsj.framework
{
    /// <summary>
    /// 加载器基类
    /// </summary>
    public abstract class LoaderBase : ILoader
    {
        // 加载策略
        private LoadStrategy m_strategy;
        // 加载队列
        private Queue<AssetDataProxy> m_queueLoading;
        // 加载数量
        private int m_loadingWork;

        public LoaderBase(LoadStrategy strategy)
        {
            this.m_strategy = strategy;
            this.m_queueLoading = new Queue<AssetDataProxy>();
            this.m_loadingWork = 0;
        }

        /// <summary>
        /// 加载策略
        /// </summary>
        protected LoadStrategy Strategy
        {
            get {
                return m_strategy;
            }
        }

        /// <summary>
        /// 是否有加载权限
        /// </summary>
        protected bool HasAccess
        {
            get {
                return m_loadingWork < Strategy.MaxLoadCount;
            }
        }

        /// <summary>
        /// 增加加载数量
        /// </summary>
        protected void IncreaseLoadingWork()
        {
            m_loadingWork++;
        }

        /// <summary>
        /// 减少工作数量
        /// </summary>
        protected void DecreaseLoadingWork()
        {
            if (--m_loadingWork < 0) {
                m_loadingWork = 0;
            }
        }
        
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="proxy"></param>
        public virtual void Load(AssetDataProxy proxy)
        {
            if(m_queueLoading != null) {
                m_queueLoading.Enqueue(proxy);
            }
            CoroutineUtil.StartCoroutine(OnLoading());
        }

        /// <summary>
        /// 进入加载状态
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerator OnLoading();

        /// <summary>
        /// 开始下载状态
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerator OnDownload(AssetDataProxy proxy);

        /// <summary>
        /// 获取加载对象
        /// </summary>
        /// <returns></returns>
        protected AssetDataProxy GetLoadProxy()
        {
            if(m_queueLoading != null) {
                return m_queueLoading.Dequeue();
            }
            return null;
        }
    }
}
