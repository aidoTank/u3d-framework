
using LitJson;
using System.Collections.Generic;
namespace GameEngine
{
    /// <summary>
    /// 文件列表
    /// </summary>
    public class Filelist
    {
        public Dictionary<string, ResData> filelist = new Dictionary<string, ResData>();


        public void ParseData(AssetData asset)
        {
            if (asset == null)
                return;

          UnityEngine.Object data =  asset.Instantiate();
          if (data == null)
              return;
          //SingleStringHolder hodler = data as SingleStringHolder;
          //Filelist jsonObj =  JsonMapper.ToObject<Filelist>(hodler.content);
          Filelist jsonObj = JsonMapper.ToObject<Filelist>("");

            filelist = jsonObj.filelist;

        }

        public void AddResData(ResData data)
        {
            if (!filelist.ContainsKey(data.mDataPath))
                filelist.Add(data.mDataPath, data);

            filelist[data.mDataPath] = data;
        }
    }
}
