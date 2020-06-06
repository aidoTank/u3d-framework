// ========================================
// Copyright (c) 2017 KingSoft, All rights reserved.
// http://www.kingsoft.com
// 
// Framwork
// 
// Filename: AssetUtility.cs
// Date:     2017/06/09
// Author:   xiangjinbao
// Email:    xiangjinbao@kingsoft.com
// ========================================

using UnityEngine;
using System.IO;

namespace xsj.framework
{
    /// <summary>
    /// 资源加载辅助类
    /// </summary>
    public static class AssetUtility
    {
        /// <summary>
        /// 获得资源文件的根路径
        /// </summary>
        /// <param name="pathType"></param>
        /// <returns></returns>
        public static string GetAssetBasePath(AssetPathType pathType)
        {
            switch (pathType) {
                case AssetPathType.Path_Local:
                    return GetAssetLocalPath();

                case AssetPathType.Path_Streaming:
                    return GetAssetStreamingAssetsPath();

                case AssetPathType.Path_LocalStreaming:
                    return GetAssetLocalStreamingAssetsPath();

                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// 获得文件的实际路径
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetAssetRealPath(string filePath)
        {
            string streaming = string.Empty;
            if (string.IsNullOrEmpty(filePath)) {
                return filePath;
            }

            if (AssetSetting.UseExternalFile) {
                string tempPath = GetAssetBasePath(AssetPathType.Path_Local) + filePath;
                if (File.Exists(tempPath)) {
                    return GetAssetBasePath(AssetPathType.Path_LocalStreaming) + filePath;
                }
            }

            return GetAssetBasePath(AssetPathType.Path_Streaming) + filePath;
        }

        /// <summary>
        /// 获取Streaming根路径
        /// </summary>
        /// <returns></returns>
        public static string GetStreamingBasePath()
        {
            AssetPathType t = AssetPathType.Path_Streaming;
            if (AssetSetting.UseExternalFile) {
                t = AssetPathType.Path_LocalStreaming;
            }
            return GetAssetBasePath(t);
        }

        /// <summary>
        /// 获取本地资源路径
        /// </summary>
        /// <returns></returns>
        private static string GetAssetLocalPath()
        {
            switch (Application.platform) {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXEditor:
                    return Application.dataPath + "/../../../Build/AssetBundle/";

                case RuntimePlatform.WindowsPlayer:
                    return Application.dataPath + "/../AssetBundle/";

                case RuntimePlatform.Android:
                    return Application.persistentDataPath + "/Data/";

                case RuntimePlatform.IPhonePlayer:
                    return Application.persistentDataPath + "/Raw/";

                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// 获取StreamingAssets资源路径
        /// </summary>
        /// <returns></returns>
        private static string GetAssetStreamingAssetsPath()
        {
            switch (Application.platform) {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXEditor:
                    return "file://" + Application.dataPath + "/../../../Build/AssetBundle/";

                case RuntimePlatform.WindowsPlayer:
                    return "file://" + Application.dataPath + "/../AssetBundle/";

                case RuntimePlatform.Android:
                    return "jar:file://" + Application.dataPath + "!/assets/Data/";

                case RuntimePlatform.IPhonePlayer:
                    return "file://" + Application.dataPath + "/Raw/";

                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// 获取本地StreamingAssets资源路径
        /// </summary>
        /// <returns></returns>
        private static string GetAssetLocalStreamingAssetsPath()
        {
            switch (Application.platform) {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXEditor:
                    return "file://" + Application.dataPath + "/../../../Build/AssetBundle/";

                case RuntimePlatform.WindowsPlayer:
                    return "file://" + Application.dataPath + "/../AssetBundle/";

                case RuntimePlatform.Android:
                    return "file://" + Application.persistentDataPath + "/Data/";

                case RuntimePlatform.IPhonePlayer:
                    return "file://" + Application.persistentDataPath + "/Data/";

                default:
                    return string.Empty;
            }
        }
    }
}
