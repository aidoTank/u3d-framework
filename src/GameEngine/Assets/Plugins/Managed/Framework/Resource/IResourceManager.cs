using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public interface IResourceManager
    {
        void LoadAsset(string assetName, Type type);
        void LoadAssetAsync(string assetName, Type type);
        void UnloadAsset();

        void LoadScene(string sceneAssetName, bool additive);
        void LoadSceneAsync(string sceneAssetName, bool additive);
        void UnLoadScene();

        void LoadAssetBundle(string assetBundleName);
        void LoadAssetBundleAsync(string assetBundleName);
        void UnloadAssetBundle();

        void LoadBinary(string binaryAssetName);
        void LoadBinaryAsync(string binaryAssetName);
        void UnLoadBinary();
    }
}
