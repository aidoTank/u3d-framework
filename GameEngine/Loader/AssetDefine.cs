/***
 * @author abaojin
 */
namespace GameEngine
{
    /// <summary>
    /// 资源委托
    /// </summary>
    /// <param name="assetPtr"></param>
    public delegate void AssetEventDelegate(AssetProxy assetPtr);

    /// <summary>
    /// 下载结果
    /// </summary>
    public enum AssetResult
    {
        Result_Succeed = 1,
        Result_Error,
    }

    public enum AssetPathType
    {
        Path_None,//未指定
        Path_Local,//本地目录
        Path_Streaming,//AssetBundle目录
        Path_LocalStreaming,//
    }

    internal enum AssetStatus
    {
        Status_Declared,//资源已经声明
        Status_Loading,//资源正在加载
        Status_Loaded,//资源已经加载
        Status_Unloaded,//资源已经卸载
    }
}
