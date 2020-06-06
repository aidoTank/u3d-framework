// ========================================
// Copyright (c) 2017 KingSoft, All rights reserved.
// http://www.kingsoft.com
// 
// Framwork
// 
// Filename: AssetData.cs
// Date:     2017/06/09
// Author:   xiangjinbao
// Email:    xiangjinbao@kingsoft.com
// ========================================

using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace xsj.framework
{
    /// <summary>
    /// 资源对象
    /// </summary>
    public class AssetData : IDisposable
    {
        protected string m_assetUrl;
        protected AssetType m_assetType;

        protected ThreadPriority m_threadPriority = ThreadPriority.Normal;

        protected bool m_init;
        protected bool m_isDone = false;
        protected bool m_loaded;
        protected bool m_unloadCompress;
        protected AssetResult m_result = AssetResult.Result_Succeed;
        protected bool m_bAllowDelete;

        protected WWW m_curWWW;
        protected AssetBundle m_assetBundle;
        protected UnityEngine.Object m_mainObject;
        protected UnityEngine.Object[] m_loadObjects;

        protected List<object> m_params = new List<object>();
        protected int m_refCount;
        protected bool m_unLoaded;

        private AssetStatus m_status = AssetStatus.Status_Declared;

        public string AssetUrl
        {
            get {
                return m_assetUrl;
            }
            set {
                if (m_assetUrl != value) {
                    m_assetUrl = value;
                }
            }
        }

        internal AssetStatus Status
        {
            get {
                return m_status;
            }
            set {
                m_status = value;
            }
        }

        public bool IsLoaded
        {
            get {
                return m_status == AssetStatus.Status_Loaded;
            }
        }
        public bool IsLoading
        {
            get {
                return m_status == AssetStatus.Status_Loading;
            }
        }

        public bool Useable
        {
            get {
                return IsLoaded && Result == AssetResult.Result_Succeed;
            }
        }

        public bool IsUnLoaded
        {
            get {
                return m_unLoaded;
            }
        }

        public AudioClip GetAudioClip(bool threeD)
        {
            if (m_curWWW == null) {
                return null;
            }
            return m_curWWW.GetAudioClip(threeD);
        }

        public AudioClip GetAudioClip(bool threeD, bool stream)
        {
            if (m_curWWW == null) {
                return null;
            }
            return m_curWWW.GetAudioClip(threeD, stream);
        }

        public AudioClip GetAudioClip(bool threeD, bool stream, AudioType audioType)
        {
            if (m_curWWW == null) {
                return null;
            }
            return m_curWWW.GetAudioClip(threeD, stream, audioType);
        }

        public UnityEngine.Object MainObject
        {
            get {
                if(m_mainObject != null) {
                    return m_mainObject;
                }
                if (AssetBundle == null) {
                    return null;
                }
                if(AssetBundle.mainAsset != null) {
                    m_mainObject = AssetBundle.mainAsset;
                } else {
                    LoadAllAsset();
                    if(m_loadObjects.Length > 0) {
                        m_mainObject = m_loadObjects[0];
                    }
                }
                return m_mainObject;
            }
        }

        public UnityEngine.Object Instantiate()
        {
            try {
                Object mainObj = MainObject;
                if (mainObj == null) {
                    return null;
                }
                UnityEngine.Object newMainObj = UnityEngine.Object.Instantiate(mainObj);
                newMainObj.name = mainObj.name;
                return newMainObj;
            } catch (Exception exception) {
                Debug.LogError("AssetData Instantiate Exception: " + exception.ToString());
                return null;
            }
        }

        public T Instantiate<T>() where T : Object
        {
            try {
                Object mainObj = MainObject;
                if (mainObj == null) {
                    return default(T);
                }
                T newMainObj = UnityEngine.Object.Instantiate<T>((T)mainObj);
                newMainObj.name = mainObj.name;
                return newMainObj;
            } catch (Exception exception) {
                Debug.LogError("AssetData Instantiate Exception: " + exception.ToString());
            }
            return null;
        }

        public UnityEngine.Object LoadAsset(string name)
        {
            try {
                if (AssetBundle == null) {
                    return null;
                }
                return AssetBundle.LoadAsset(name);
            } catch (OutOfMemoryException exception) {
                Debug.LogError(exception.ToString());
                Resources.UnloadUnusedAssets();
                GC.Collect();
            } catch (Exception exception2) {
                Debug.LogError("AssetData Load Exception: " + exception2.ToString());
            }
            return null;
        }

        public T LoadAsset<T>(string name) where T : Object
        {
            try {
                if (AssetBundle == null) {
                    return null;
                }
                return AssetBundle.LoadAsset<T>(name);
            } catch (OutOfMemoryException exception) {
                Debug.LogError(exception.ToString());
                Resources.UnloadUnusedAssets();
                GC.Collect();
            } catch (Exception exception2) {
                Debug.LogError("AssetData Load Exception: " + exception2.ToString());
            }
            return null;
        }

        public UnityEngine.Object[] LoadAllAsset()
        {
            try {
                if (m_loaded) {
                    if (m_loadObjects == null) {
                        return null;
                    }
                    return m_loadObjects;
                }
                if (AssetBundle == null) {
                    return null;
                }
                m_loadObjects = AssetBundle.LoadAllAssets();
                m_loaded = true;
                m_unLoaded = false;

                return m_loadObjects;
            } catch (OutOfMemoryException exception) {
                Debug.LogError(exception.ToString());
                Resources.UnloadUnusedAssets();
                GC.Collect();
            } catch (Exception exception2) {
                Debug.LogError("AssetData LoadAll Exception: " + exception2.ToString());
            }

            return null;
        }

        public void LoadImageToTexture(Texture2D texture)
        {
            if (m_curWWW != null) {
                m_curWWW.LoadImageIntoTexture(texture);
            }
        }

        public void OnDownloadStart(WWW www)
        {
            if (m_curWWW != null) {
                UnLoad(false);
                m_curWWW = null;
            }
            m_curWWW = www;
        }

        public void OnDonwloadFinish()
        {
            m_isDone = true;
            m_bAllowDelete = true;
            if (AssetBundle != null) {
                m_status = AssetStatus.Status_Loaded;
                AssetFactory.Instance.ChangeAssetState(this, true);
            }
        }

        public void Reset()
        {
            m_init = false;
            m_curWWW = null;
            m_loaded = false;
            m_unloadCompress = false;
            m_unLoaded = false;
            m_isDone = false;

            m_assetBundle = null;
            m_assetUrl = null;
            m_result = AssetResult.Result_Succeed;
            m_refCount = 0;
            m_loadObjects = null;
            m_mainObject = null;
            m_threadPriority = ThreadPriority.Normal;
            m_assetType = AssetType.Asset_AssetBundle;
            m_bAllowDelete = false;
            m_params.Clear();
        }

        private void Start()
        {
            m_init = true;
        }

        public void UnLoad(bool unloadAllLoadedObjects)
        {
            if (!m_unLoaded && (AssetBundle != null)) {
                m_status = AssetStatus.Status_Unloaded;
                AssetBundle.Unload(unloadAllLoadedObjects);
                m_unLoaded = true;
                m_loaded = false;
                if (m_curWWW != null) {
                    m_curWWW.Dispose();
                    m_curWWW = null;
                }
            } else if (!m_unLoaded && m_unloadCompress) {
                m_loadObjects = null;
                m_mainObject = null;
            }
        }

        public void UnloadWebStream()
        {
            if (!m_unloadCompress && AssetBundle != null) {
                AssetBundle.Unload(false);
                m_unloadCompress = true;
                m_curWWW = null;
            }

        }

        public string Name
        {
            get {
                if (((Error == null) && (MainObject != null))) {
                    return MainObject.name;
                }
                return string.Empty;
            }
        }

        public bool AllowDelete
        {
            get {
                return m_bAllowDelete;
            }
        }

        public List<object> AssetParams
        {
            get {
                return m_params;
            }
            set {
                if (m_params != value) {
                    m_params = value;
                }
            }
        }

        public void AddRef()
        {
            m_refCount++;
        }

        public void Release()
        {
            if (--m_refCount <= 0) {
                AssetFactory.Instance.RecycleAsset(this);
            }
        }

        public int RefCount
        {
            get {
                return m_refCount;
            }
        }

        public AssetResult Result
        {
            get {
                return m_result;
            }
            set {
                m_result = value;
            }
        }

        public ThreadPriority ThreadPriority
        {
            get {
                return m_threadPriority;
            }
            set {
                m_threadPriority = value;
            }
        }

        public AssetType AssetType
        {
            get {
                return m_assetType;
            }
            set {
                if (m_assetType != value) {
                    m_assetType = value;
                }
            }
        }

        public AssetBundle AssetBundle
        {
            get {
                if (m_assetBundle != null) {
                    return m_assetBundle;
                }
                if (m_curWWW == null) {
                    return null;
                }
                if ((m_assetType != AssetType.Asset_AssetBundle) || !string.IsNullOrEmpty(m_curWWW.error)) {
                    return null;
                }
                try {
                    m_assetBundle = m_curWWW.assetBundle;
                    return m_assetBundle;
                } catch (Exception exception) {
                    Debug.Log(exception.Message);
                }
                return null;
            }
        }

        public byte[] Bytes
        {
            get {
                if (m_curWWW == null) {
                    return null;
                }
                return m_curWWW.bytes;
            }
        }

        public string Error
        {
            get {
                if (m_curWWW == null) {
                    return "Need Create WWW !!!";
                }
                return m_curWWW.error;
            }
        }

        public bool IsDone
        {
            get {
                return m_isDone;
            }
        }

        public float Progress
        {
            get {
                if (m_curWWW == null) {
                    return -1f;
                }
                return m_curWWW.progress;
            }
        }

        public int Size
        {
            get {
                if (m_curWWW == null) {
                    return 0;
                }
                return ((!m_curWWW.isDone || !string.IsNullOrEmpty(m_curWWW.error)) ? 0 : m_curWWW.size);
            }
            set {
            }
        }

        public string Text
        {
            get {
                if (m_curWWW == null) {
                    return null;
                }
                return m_curWWW.text;
            }
        }

        public Texture2D Texture
        {
            get {
                if (m_curWWW == null) {
                    return null;
                }
                return m_curWWW.texture;
            }
        }

        public float UploadProgress
        {
            get {
                if (m_curWWW == null) {
                    return 0f;
                }
                return m_curWWW.uploadProgress;
            }
        }

        public void Dispose()
        {

        }
    }

}
