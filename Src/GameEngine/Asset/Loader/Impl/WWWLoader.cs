// ========================================
// Copyright (c) 2017 KingSoft, All rights reserved.
// http://www.kingsoft.com
// 
// Framwork
// 
// Filename: WWWLoader.cs
// Date:     2017/06/09
// Author:   xiangjinbao
// Email:    xiangjinbao@kingsoft.com
// ========================================

using System.Collections;
using UnityEngine;

namespace xsj.framework
{
    /// <summary>
    /// WWW加载器
    /// </summary>
    public class WWWLoader : LoaderBase
    {
        public WWWLoader(LoadStrategy strategy) : base(strategy)
        {

        }

        /// <summary>
        /// 进入下载状态
        /// </summary>
        /// <returns></returns>
        protected override IEnumerator OnLoading()
        {
            AssetDataProxy proxy = GetLoadProxy();
            if(proxy == null) {
                yield break;
            }

            // 正在加载
            if (proxy.Data.IsLoading) {
                while (!proxy.IsAbort && proxy.Data.IsLoading) {
                    yield return 1;
                }
                if (proxy.IsAbort) {
                    yield break;
                }

                proxy.AssetComplete();
                yield break;
            }

            // 排队等待
            while (!HasAccess && !proxy.IsAbort) {
                yield return 1;
            }
            if (proxy.IsAbort) {
                yield break;
            }

            // 再次检测
            if (proxy.Data.IsLoading) {
                while (!proxy.IsAbort && proxy.Data.IsLoading) {
                    yield return 1;
                }
                if (proxy.IsAbort) {
                    yield break;
                }

                proxy.AssetComplete();
                yield break;
            }

            // 加载完成
            if (proxy.Data.IsLoaded) {
                proxy.AssetComplete();
                yield break;
            }

            // 开始加载
            IncreaseLoadingWork();
            CoroutineUtil.StartCoroutine(OnDownload(proxy));
            DecreaseLoadingWork();

            proxy.AssetComplete();
        }

        /// <summary>
        /// 开始下载
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        protected override IEnumerator OnDownload(AssetDataProxy proxy)
        {
            if(proxy == null) {
                yield break;
            }

            proxy.Data.Status = AssetStatus.Status_Loading;
            WWW www = new WWW(proxy.Url);
            www.threadPriority = Strategy.ThreadPriority;

            proxy.Data.OnDownloadStart(www);

            yield return www;

            proxy.Data.OnDonwloadFinish();

            if (!string.IsNullOrEmpty(www.error)) {
                proxy.Data.Result = AssetResult.Result_Error;
            } else {
                proxy.Data.Result = AssetResult.Result_Succeed;
            }
        }
    }
}
