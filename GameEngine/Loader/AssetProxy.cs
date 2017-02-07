using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameEngine
{
    
    /// <summary>
    /// 资源代理，支持对此代理进行协程控制
    /// </summary>
    public sealed class AssetProxy
    {
        private string mURL;
        private AssetData mData;
        private bool mAbort = false;

        public ThreadPriority threadPriority = ThreadPriority.Normal;
        /// <summary>
        /// 资源加载的回调
        /// </summary>
        public AssetEventDelegate assetDelegate;
        public AssetProxy(string url,AssetEventDelegate aDelegate = null,bool auto = false)
        {
            mURL = AssetUtility.GetRealPath(url);
            assetDelegate = aDelegate;

            if (auto)
                Work();
        }

        public static AssetProxy CreateProxy(string url, AssetEventDelegate aDelegate = null, bool auto = false)
        {
            return new AssetProxy(url, aDelegate, auto);
        }
        public static AssetProxy CreateAutoProxy(string url, AssetEventDelegate aDelegate = null)
        {
            return CreateProxy(url, aDelegate, true);
        }
        public static AssetProxy CreateYieldProxy(string url)
        {
            return CreateProxy(url, null, true);
        }

        public void Work()
        {
            //向AssetManager申请资源
            AssetManager.Instance.LoadResource(this);
        }

        public string url
        {
            get
            {
                return mURL;
            }
        }

        public AssetData data
        {
            get
            {
                return mData;
            }
            internal set
            {
                mData = value;
            }
        }
        public bool isDone
        {

            get
            {
                return mData == null || mData.isDone;
            }
        }

        public Coroutine coroutine
        {
            get
            {
                return CoroutineUtils.StartCoroutine(WaitforComplete());
            }
         
        }
        private IEnumerator WaitforComplete()
        {
            while (!isDone)
                yield return 1;
        }
        public void Abort()
        {
            if (mData != null)
                mData.Release();
            mAbort = true;
            mData = null;
            mURL = string.Empty;
        }
        public bool abort
        {
            get
            {

               return mAbort;
            }
        }

        internal void AssetComplete()
        {
            if (assetDelegate != null)
                assetDelegate(this);
        }
        public void Dispose()
        {
            if(mData != null)
                mData.Release();
            mAbort = true;
            mData = null;
            assetDelegate = null;
            mURL = string.Empty;
        }
    }
}
