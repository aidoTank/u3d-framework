using System.Collections.Generic;

namespace GameEngine
{
    /// <summary>
    /// 文件的关联
    /// </summary>
    public class AssetAssociate
    {
        public string url;

        private Dictionary<string, bool> mAssociates = new Dictionary<string, bool>();

        public void SetAssociate(string acPath,bool bMemory)
        {
            if (mAssociates.ContainsKey(acPath))
                mAssociates[acPath] = bMemory;
            else
                mAssociates.Add(acPath, bMemory);
        }

        public bool GetAAState(string acPath)
        {
            if(HasAA(acPath))
                return mAssociates[acPath];
            return false;
        }
        public bool HasAA(string acPath)
        {
            return mAssociates.ContainsKey(acPath);
        }

        public bool HasAssociateInMemory
        {
            get
            {
                foreach(KeyValuePair<string,bool> ac in mAssociates)
                {
                    if (ac.Value)
                        return true;
                }
                return false;
            }
        }
    }
}
