// ========================================
// Copyright (c) 2017 KingSoft, All rights reserved.
// http://www.kingsoft.com
// 
// Framwork
// 
// Filename: HTTPLoader.cs
// Date:     2017/06/09
// Author:   xiangjinbao
// Email:    xiangjinbao@kingsoft.com
// ========================================

using System;
using System.Collections;

namespace xsj.framework
{
    /// <summary>
    /// Http加载器
    /// </summary>
    public class HTTPLoader : LoaderBase
    {
        public HTTPLoader(LoadStrategy strategy) : base(strategy)
        {

        }

        protected override IEnumerator OnLoading()
        {
            throw new NotImplementedException();
        }

        protected override IEnumerator OnDownload(AssetDataProxy proxy)
        {
            throw new NotImplementedException();
        }
    }
}
