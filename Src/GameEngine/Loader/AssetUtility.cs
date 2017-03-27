using UnityEngine;
using System.IO;

/***
 * AssetUtility.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public class AssetUtility
    {
        public static string editorAssetPath = string.Empty;
        public static string StreamingBase {
            get {
                AssetPathType t = AssetPathType.Path_Streaming;
                if (GameSetting.UseExternalFile)
                    t = AssetPathType.Path_LocalStreaming;
                return GetFileAssetBase(t);
            }
        }

        /// <summary>
        /// 获得资源文件的根路径
        /// </summary>
        /// <param name="ptype"></param>
        /// <returns></returns>
        public static string GetFileAssetBase(AssetPathType ptype)
        {
            if (ptype == AssetPathType.Path_Local) {
                switch (Application.platform) {
                    //编辑器模式
                    case RuntimePlatform.WindowsEditor:
                    case RuntimePlatform.OSXEditor:
                        return Application.dataPath + "/../../../Bin/client/StreamingAssets/";
                    case RuntimePlatform.WindowsPlayer:
                        return Application.dataPath + "/../StreamingAssets/";
                    case RuntimePlatform.Android:
                        return Application.persistentDataPath + "/Data/";
                    case RuntimePlatform.IPhonePlayer:
                        return Application.persistentDataPath + "/Raw/";
                    default:
                        return "";
                }
            } else if (ptype == AssetPathType.Path_Streaming) {
                switch (Application.platform) {

                    case RuntimePlatform.WindowsEditor:
                    case RuntimePlatform.OSXEditor:
                        if (!string.IsNullOrEmpty(editorAssetPath))
                            return "file://" + editorAssetPath;

                        return "file://" + Application.dataPath + "/../../../Bin/client/StreamingAssets/";
                    case RuntimePlatform.WindowsPlayer:
                        return "file://" + Application.dataPath + "/../StreamingAssets/";
                    case RuntimePlatform.Android:
                        return "jar:file://" + Application.dataPath + "!/assets/Data/";
                    case RuntimePlatform.IPhonePlayer:
                        return "file://" + Application.dataPath + "/Raw/";
                    default:
                        return "";
                }
            } else if (ptype == AssetPathType.Path_LocalStreaming) {
                switch (Application.platform) {

                    case RuntimePlatform.WindowsEditor:
                    case RuntimePlatform.OSXEditor:
                        return "file://" + Application.dataPath + "/../../../Bin/client/StreamingAssets/";
                    case RuntimePlatform.WindowsPlayer:
                        return "file://" + Application.dataPath + "/../StreamingAssets/";
                    case RuntimePlatform.Android:
                        return "file://" + Application.persistentDataPath + "/Data/";
                    case RuntimePlatform.IPhonePlayer:
                        return "file://" + Application.persistentDataPath + "/Data/";
                    default:
                        return "";
                }
            }

            return "";

        }

        /// <summary>
        /// 获得文件的实际路径
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string GetRealPath(string filepath)
        {
            string streaming = string.Empty;
            if (string.IsNullOrEmpty(filepath))
                return filepath;

            if (GameSetting.UseExternalFile) {
                string temp = GetFileAssetBase(AssetPathType.Path_Local) + filepath;
                if (File.Exists(temp))
                    return GetFileAssetBase(AssetPathType.Path_LocalStreaming) + filepath;
            }

            return GetFileAssetBase(AssetPathType.Path_Streaming) + filepath;
        }
    }
}
