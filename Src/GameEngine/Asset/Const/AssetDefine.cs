// ========================================
// Copyright (c) 2017 KingSoft, All rights reserved.
// http://www.kingsoft.com
// 
// Framwork
// 
// Filename: AssetDefine.cs
// Date:     2017/06/09
// Author:   xiangjinbao
// Email:    xiangjinbao@kingsoft.com
// ========================================

namespace xsj.framework
{
    /// <summary>
    /// 资源委托
    /// </summary>
    /// <param name="assetPtr"></param>
    public delegate void AssetEventDelegate(AssetDataProxy assetPtr);

    /// <summary>
    /// 下载结果
    /// </summary>
    public enum AssetResult
    {
        Result_Succeed = 1,
        Result_Error,
    }

    /// <summary>
    /// 资源状态
    /// </summary>
    public enum AssetStatus
    {
        // 资源已经声明
        Status_Declared,
        // 资源正在加载
        Status_Loading,
        // 资源已经加载
        Status_Loaded,
        // 资源已经卸载
        Status_Unloaded,
    }

    /// <summary>
    /// 资源路径类型
    /// </summary>
    public enum AssetPathType
    {
        // 未指定
        Path_None,
        // 本地StreamingAssets目录
        Path_LocalStreaming,
        // 本地目录
        Path_Local,
        // StreamingAssets目录
        Path_Streaming,
    }

    /// <summary>
    /// 资源类型
    /// </summary>
    public enum AssetType
    {
        Asset_AssetBundle,
        Asset_Text,
        Asset_Texture,
        Asset_StreamAudio,
        Asset_Movie
    }
}
