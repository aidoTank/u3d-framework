using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class ResourceManager : FrameworkModule, IResourceManager
    {
        public void LoadAsset(string assetName, Type type)
        {
            throw new NotImplementedException();
        }

        public void LoadAssetAsync(string assetName, Type type)
        {
            throw new NotImplementedException();
        }

        public void LoadAssetBundle(string assetBundleName)
        {
            throw new NotImplementedException();
        }

        public void LoadAssetBundleAsync(string assetBundleName)
        {
            throw new NotImplementedException();
        }

        public void LoadBinary(string binaryAssetName)
        {
            throw new NotImplementedException();
        }

        public void LoadBinaryAsync(string binaryAssetName)
        {
            throw new NotImplementedException();
        }

        public void LoadScene(string sceneAssetName, bool additive)
        {
            throw new NotImplementedException();
        }

        public void LoadSceneAsync(string sceneAssetName, bool additive)
        {
            throw new NotImplementedException();
        }

        public void UnloadAsset()
        {
            throw new NotImplementedException();
        }

        public void UnloadAssetBundle()
        {
            throw new NotImplementedException();
        }

        public void UnLoadBinary()
        {
            throw new NotImplementedException();
        }

        public void UnLoadScene()
        {
            throw new NotImplementedException();
        }

        internal override void Shutdown()
        {
            throw new NotImplementedException();
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            throw new NotImplementedException();
        }
    }
}
