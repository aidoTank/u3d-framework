using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameEngine
{
	/// <summary>
	/// 资源加载类
	/// </summary>
	public class AssetData
	{
	    //资源地址
	    protected string mAssetUrl;
	    //资源大小
	    protected int mAssetSize;

        protected ThreadPriority mThreadPriority = ThreadPriority.Normal;
	
	    protected bool mInit;
	    protected bool mIsDone = false;
	    protected bool mLoaded;
        protected bool mUnloadCompress;
	    protected AssetResult mResult = AssetResult.Result_Succeed;
	
	    //资源类型
	    protected AssetType mAssetType;
	    
	    protected bool mbAllowDelete;

	    protected WWW mCurWWW;
        protected AssetBundle mAssetBundle;
	
	    protected List<object> mParams = new List<object>();

	    //从资源中加载的Objects
        protected string mText; 
        protected UnityEngine.Object mMainObject;
	    protected UnityEngine.Object[] mLoadObjects;

        protected int mRefCount;
	    protected bool mUnLoaded;

        private AssetStatus mStatus = AssetStatus.Status_Declared;

        public string url
        {
            get
            {
                return mAssetUrl;
            }
            set
            {
                if (mAssetUrl != value)
                {
                    mAssetUrl = value;
                }
            }
        }

        internal AssetStatus status
        {
            get
            {
                return mStatus;
            }
        }
        public bool isLoaded
        {
            get
            {
                return mStatus == AssetStatus.Status_Loaded;
            }
        }
        public bool isLoading
        {
            get
            {
                return mStatus == AssetStatus.Status_Loading;
            }
        }
        public bool useable
        {
            get
            {
                return isLoaded && result == AssetResult.Result_Succeed;
            }
        }
	    /// <summary>
	    /// 开始下载
	    /// </summary>
	    internal Coroutine BeginDownLoad()
	    {
	        return BehaviourUtil.StartCoroutine(DownLoad());
	    }

        internal IEnumerator DownLoad()
	    {
            mStatus = AssetStatus.Status_Loading;
	        if (mCurWWW != null)
	        {
	            UnLoad(false);
	            mCurWWW = null;
	        }
	        mCurWWW = new WWW(mAssetUrl);
            mCurWWW.threadPriority = mThreadPriority;
	
	        yield return mCurWWW;
	
	        OnDonwloaded();
	        mIsDone = true;
	
	        if (!string.IsNullOrEmpty(error))
	        {
	            mResult = AssetResult.Result_Error;
	        }
	        else
	        {
	            mResult = AssetResult.Result_Succeed;
	        }
	    }
	

        public bool IsUnLoaded
        {
            get
            {
                return mUnLoaded;
            }
        }
	    public AudioClip GetAudioClip(bool threeD)
	    {
	        if (mCurWWW == null)
	        {
	            return null;
	        }
	        return mCurWWW.GetAudioClip(threeD);
	    }
	
	    public AudioClip GetAudioClip(bool threeD, bool stream)
	    {
	        if (mCurWWW == null)
	        {
	            return null;
	        }
	        return mCurWWW.GetAudioClip(threeD, stream);
	    }
	
	    public AudioClip GetAudioClip(bool threeD, bool stream, AudioType audioType)
	    {
	        if (mCurWWW == null)
	        {
	            return null;
	        }
	        return mCurWWW.GetAudioClip(threeD, stream, audioType);
	    }

        public UnityEngine.Object mainObject
        {
            get
            {
                if (mMainObject == null)
                {
                    if (assetBundle == null)
                    {
                        return null;
                    }
                    if (assetBundle.mainAsset != null)
                    {
                        LoadAll();

                        for (int i = 0; i < mLoadObjects.Length; ++i)
                        {
                            UnityEngine.Object obj = mLoadObjects[i];

                            if (obj != null && obj.GetInstanceID() == assetBundle.mainAsset.GetInstanceID())
                            {
                                mMainObject = obj;
                                break;
                            }
                        }
                    }

                }
                return mMainObject;
            }
        }
	    /// <summary>
	    /// AssetBundle 主资源
	    /// </summary>
	    /// <returns></returns>
	    public UnityEngine.Object Instantiate()
	    {
	        try
	        {
                Object mainObj = mainObject;
                if (mainObj == null)
                    return null;

                UnityEngine.Object obj3 = UnityEngine.Object.Instantiate(mainObj);
                obj3.name = mainObj.name;
	            return obj3;
	        }
	        catch (Exception exception)
	        {
	            Debug.LogError("AssetData Instantiate Exception: " + exception.ToString());
	            return null;
	        }
	    }
	
	    public UnityEngine.Object Instantiate(Vector3 pos, Quaternion q)
	    {
	        try
            {
                Object mainObj = mainObject;
                if (mainObj == null)
                    return null;

                UnityEngine.Object obj3 = UnityEngine.Object.Instantiate(mainObj, pos, q);
                obj3.name = mainObj.name;
                return obj3;
            }
	        catch (Exception exception)
	        {
	            Debug.LogError("AssetData Instantiate Exception: " + exception.ToString());
	            return null;
	        }
	    }
	
	    /// <summary>
	    /// 加载指定资源
	    /// </summary>
	    /// <param name="name"></param>
	    /// <returns></returns>
	    public UnityEngine.Object Load(string name)
	    {
	        try
	        {
	            if (assetBundle == null)
	            {
	                return null;
	            }
	            return assetBundle.LoadAsset(name);
	        }
	        catch (OutOfMemoryException exception)
	        {
	            Debug.LogError(exception.ToString());
	            Resources.UnloadUnusedAssets();
	            GC.Collect();
	            return null;
	        }
	        catch (Exception exception2)
	        {
	            Debug.LogError("AssetData Load Exception: " + exception2.ToString());
	            return null;
	        }
	    }
	
	    public UnityEngine.Object Load(string name, System.Type type)
	    {
	        try
	        {
	            if (assetBundle == null)
	            {
	                return null;
	            }
	            return assetBundle.LoadAsset(name, type);
	        }
	        catch (OutOfMemoryException exception)
	        {
	            Debug.LogError(exception.ToString());
	            Resources.UnloadUnusedAssets();
	            GC.Collect();
	            return null;
	        }
	        catch (Exception exception2)
	        {
	            Debug.LogError("AssetData Load Exception: " + exception2.ToString());
	            return null;
	        }
	    }
	
	    /// <summary>
	    /// 加载全部资源
	    /// </summary>
	    /// <returns></returns>
	    public UnityEngine.Object[] LoadAll()
	    {
	        try
	        {
	            if (mLoaded)
	            {
	                if (mLoadObjects == null)
	                {
	                    return null;
	                }
	                return mLoadObjects;
	            }
	            if (assetBundle == null)
	            {
	                return null;
	            }
	            mLoadObjects = assetBundle.LoadAllAssets();
	            mLoaded = true;
	            mUnLoaded = false;

	            return mLoadObjects;
	        }
	        catch (OutOfMemoryException exception)
	        {
	            Debug.LogError(exception.ToString());
	            Resources.UnloadUnusedAssets();
	            GC.Collect();
	            return null;
	        }
	        catch (Exception exception2)
	        {
	            Debug.LogError("AssetData LoadAll Exception: " + exception2.ToString());
	            return null;
	        }
	    }
	
	    public void LoadImagetoTexture(Texture2D tex)
	    {
	        if (mCurWWW != null)
	        {
	            mCurWWW.LoadImageIntoTexture(tex);
	        }
	    }
	
	    private void OnDonwloaded()
	    {
	        mbAllowDelete = true;
            //对Assetbundle做一次引用
            if (assetBundle != null){ }

            mStatus = AssetStatus.Status_Loaded;
            AssetManager.Instance.ChangeAssetState(this, true);
	    }
	
	    public void Reset()
	    {
            mInit = false;
	        mCurWWW = null;
            mLoaded = false;
            mUnloadCompress = false;
            mUnLoaded = false;
            mIsDone = false;

            mAssetBundle = null;
	        mAssetUrl = null;
	        mResult = AssetResult.Result_Succeed;
	        mRefCount = 0;
	        mLoadObjects = null;
            mMainObject = null;
            mThreadPriority = ThreadPriority.Normal;
	        mAssetType = AssetType.AssetType_AssetBundle;
	        mAssetSize = 0;
	        mbAllowDelete = false;
	        mParams.Clear();
	    }
	
	    private void Start()
	    {
	        mInit = true;
	    }
	
	    public void UnLoad(bool unloadAllLoadedObjects)
	    {
            if (!mUnLoaded && (assetBundle != null))
            {
                mStatus = AssetStatus.Status_Unloaded;
                assetBundle.Unload(unloadAllLoadedObjects);
                mUnLoaded = true;
                mLoaded = false;
                if (mCurWWW != null)
                {
                    mCurWWW.Dispose();
                    mCurWWW = null;
                }
            }
            else if(!mUnLoaded && mUnloadCompress)
            {
                mLoadObjects = null;
                mMainObject = null;
            }
	    }

        public void UnloadWebStream()
        {
            if (!mUnloadCompress && assetBundle != null)
            {
                assetBundle.Unload(false);
                mUnloadCompress = true;
                mCurWWW = null;
            }

        }
	
	    public string _name
	    {
	        get
	        {
	            if (((error == null) && (assetBundle != null)) && (assetBundle.mainAsset != null))
	            {
	                return assetBundle.mainAsset.name;
	            }
	            return string.Empty;
	        }
	    }
	
	    public bool allowDelete
	    {
	        get
	        {
	            return mbAllowDelete;
	        }
	    }
	
	    public AssetBundle assetBundle
	    {
	        get
	        {
                if (mAssetBundle != null)
                    return mAssetBundle;
	            if (mCurWWW == null)
	            {
	                return null;
	            }
	            if ((mAssetType != AssetType.AssetType_AssetBundle) || !string.IsNullOrEmpty(mCurWWW.error))
	            {
	                return null;
	            }
	            try
	            {
	                mAssetBundle =  mCurWWW.assetBundle;
                    return mAssetBundle;
	            }
	            catch (Exception exception)
	            {
                    Debug.Log(exception.Message);
	                return null;
	            }
	        }
	    }
	    public List<object> assetParams
	    {
	        get
	        {
	            return mParams;
	        }
	        set
	        {
	            if (mParams != value)
	            {
	                mParams = value;
	            }
	        }
	    }

        public ThreadPriority threadPriority 
	    {
	        get
	        {
	            return mThreadPriority;
	        }
	        set
	        {
                mThreadPriority = value;
	        }
	    }
	
	    public AssetType assetType
	    {
	        get
	        {
	            return mAssetType;
	        }
	        set
	        {
	            if (mAssetType != value)
	            {
	                mAssetType = value;
	            }
	        }
	    }
	
	    public AudioClip audioClip
	    {
	        get
	        {
	            if (mCurWWW == null)
	            {
	                return null;
	            }
	            return mCurWWW.audioClip;
	        }
	    }
	
	    public byte[] bytes
	    {
	        get
	        {
	            if (mCurWWW == null)
	            {
	                return null;
	            }
	            return mCurWWW.bytes;
	        }
	    }
	
	    public string error
	    {
	        get
	        {
	            if (mCurWWW == null)
	            {
	                return "Need Create WWW !!!";
	            }
	            return mCurWWW.error;
	        }
	    }
	
	    public bool isDone
	    {
	        get
	        {
	            return mIsDone;
	        }
	    }
	    public AudioClip oggVorbis
	    {
	        get
	        {
	            if (mCurWWW == null)
	            {
	                return null;
	            }
	            return mCurWWW.audioClip;
	        }
	    }
	
	    public float progress
	    {
	        get
	        {
	            if (mCurWWW == null)
	            {
	                return -1f;
	            }
	            return mCurWWW.progress;
	        }
	    }
	
        internal void AddRef()
        {
            mRefCount++;
        }
        internal void Release()
        {
            if (--mRefCount <= 0)
            {
                AssetManager.Instance.RecycleAsset(this);
            }
        }
	    public int refCount
	    {
	        get
	        {
	            return mRefCount;
	        }
	    }
	    public AssetResult result
	    {
	        get
	        {
	            return mResult;
	        }
	    }
	
	    public int size
	    {
	        get
	        {
	            if (mCurWWW == null)
	            {
	                return 0;
	            }
	            return ((!mCurWWW.isDone || !string.IsNullOrEmpty(mCurWWW.error)) ? 0 : mCurWWW.size);
	        }
	        set
	        {
	        }
	    }
	
	    public string text
	    {
	        get
	        {
	            if (mCurWWW == null)
	            {
	                return null;
	            }
	            return mCurWWW.text;
	        }
	    }
	
	    public Texture2D texture
	    {
	        get
	        {
	            if (mCurWWW == null)
	            {
	                return null;
	            }
	            return mCurWWW.texture;
	        }
	    }
	
	    public float uploadProgress
	    {
	        get
	        {
	            if (mCurWWW == null)
	            {
	                return 0f;
	            }
	            return mCurWWW.uploadProgress;
	        }
	    }
	    /// <summary>
	    /// 资源类型
	    /// </summary>
	    public enum AssetType
	    {
	        AssetType_AssetBundle,
	        AssetType_Text,
	        AssetType_Texture,
	        AssetType_StreamAudio,
	        AssetType_Movie
	    }
	}
	
}
