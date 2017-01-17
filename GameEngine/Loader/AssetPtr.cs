
using System;
namespace GameEngine
{
    /// <summary>
    /// 资源的智能对象
    /// </summary>
    public class AssetPtr
    {
        private Guid mGuid;
        public AssetPtr(Guid guid)
        {
            mGuid = guid;
        }

        public AssetData Data
        {
            get
            {
                //return AssetManager.Instance.GetAssetByGUID(mGuid);
                return null;
            }
        }
        ~AssetPtr()
        {
            //AssetManager.Instance.RecycleAsset(mGuid);
            mGuid = Guid.Empty;
        }
    }
}
