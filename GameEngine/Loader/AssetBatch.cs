

using System.Collections.Generic;
namespace GameEngine
{
    public class AssetBatch
    {
        //private List<AssetCollector> mAssets = new List<AssetCollector>();
        //暂时先这么处理，将引用到的资源放到这里
        public List<AssetProxy> Assets = new List<AssetProxy>();
    }
}
