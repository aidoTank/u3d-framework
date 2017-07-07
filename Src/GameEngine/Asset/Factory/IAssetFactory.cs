// ========================================
// Copyright (c) 2017 KingSoft, All rights reserved.
// http://www.kingsoft.com
// 
// Framwork
// 
// Filename: IAssetFactory.cs
// Date:     2017/06/09
// Author:   xiangjinbao
// Email:    xiangjinbao@kingsoft.com
// ========================================

namespace xsj.framework
{
    interface IAssetFactory
    {
        ILoader Loader
        {
            get;
        }

        void LoadAsset(AssetDataProxy proxy);

        AssetData GetAsset(string assetPath);

        void RecycleAsset(AssetData data);
    }
}
