// ========================================
// Copyright (c) 2017 KingSoft, All rights reserved.
// http://www.kingsoft.com
// 
// Framwork
// 
// Filename: AssetApi.cs
// Date:     2017/06/09
// Author:   xiangjinbao
// Email:    xiangjinbao@kingsoft.com
// ========================================

namespace xsj.framework
{
    /// <summary>
    /// 资源对外接口
    /// </summary>
    public static class AssetApi
    {
        /// <summary>
        /// 创建指定代理对象
        /// </summary>
        /// <param name="url"></param>
        /// <param name="aDelegate"></param>
        /// <param name="auto"></param>
        /// <returns></returns>
        public static AssetDataProxy CreateProxy(string url, AssetEventDelegate aDelegate = null, bool auto = false)
        {
            return new AssetDataProxy(url, aDelegate, auto);
        }

        /// <summary>
        /// 创建自动加载携程对象
        /// </summary>
        /// <param name="url"></param>
        /// <param name="aDelegate"></param>
        /// <returns></returns>
        public static AssetDataProxy CreateAutoProxy(string url, AssetEventDelegate aDelegate = null)
        {
            return CreateProxy(url, aDelegate, true);
        }

        /// <summary>
        /// 创建携程加载代理对象
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static AssetDataProxy CreateYieldProxy(string url)
        {
            return CreateProxy(url, null, true);
        }
    }
}
