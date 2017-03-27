using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * ObjectPool.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    /// <summary>
    /// 对象池
    /// </summary>
    public class ObjectPool
    {
        public static bool KeepCacheWhenRent;
        private int mBlockLimitCount;
        private int mCacheBlockMaxCount;
        private float mLastUpdateTime;
        private CacheReleaser mReleaser;
        private List<CachedObjectInfo> pool = new List<CachedObjectInfo>();
        public const float UpdateFrequency = 5f;
        public const float WornTime = 60f;

        public ObjectPool(CacheReleaser releaser, int maxCacheCount = 64, int cacheLimitCount = 128)
        {
            mCacheBlockMaxCount = maxCacheCount;
            mBlockLimitCount = cacheLimitCount;
            mReleaser = releaser;
            mLastUpdateTime = Time.realtimeSinceStartup;
            InitPool();
        }

        public bool AddCache(object key, object value)
        {
            if ((key == null) || (value == null)) {
                return false;
            }
            CachedObjectInfo cacheInfo = GetCacheInfo(key);
            if (cacheInfo == null) {
                cacheInfo = AddCacheInfo(key);
                if (cacheInfo == null) {
                    return false;
                }
            }
            if (cacheInfo.CachedObj(value.GetHashCode())) {
                cacheInfo.AddToCache(value);
                return true;
            }
            if (GetCacheCount() >= mCacheBlockMaxCount) {
                TryFixPool();
            }
            if (GetCacheCount() > mCacheBlockMaxCount) {
                return false;
            }
            cacheInfo.AddToCache(value);
            return true;
        }

        private CachedObjectInfo AddCacheInfo(object key)
        {
            if (key == null) {
                return null;
            }
            if (IsContainCacheInfo(key)) {
                return GetCacheInfo(key);
            }
            CachedObjectInfo item = new CachedObjectInfo();
            item.InitCache(key, mReleaser);
            pool.Add(item);
            return item;
        }

        public void ClearAllCache()
        {
            for (int i = 0; i < pool.Count; i++) {
                if (pool[i] != null) {
                    pool[i].Shrink(pool[i].GetFreeCacheCount());
                }
            }
        }

        public void ClearCache(object key)
        {
            CachedObjectInfo cacheInfo = GetCacheInfo(key);
            if (cacheInfo != null) {
                cacheInfo.Shrink(cacheInfo.GetFreeCacheCount());
            }
        }

        public int GetCacheCount()
        {
            int num = 0;
            for (int i = 0; i < pool.Count; i++) {
                num += pool[i].GetTotalCacheCount();
            }
            return num;
        }

        public int GetCacheCount(object key)
        {
            CachedObjectInfo cacheInfo = GetCacheInfo(key);
            if (cacheInfo == null) {
                return 0;
            }
            return cacheInfo.GetTotalCacheCount();
        }

        private CachedObjectInfo GetCacheInfo(object key)
        {
            for (int i = 0; i < pool.Count; i++) {
                if (pool[i].EqualsKey(key)) {
                    return pool[i];
                }
            }
            return null;
        }

        public int GetFreeCacheCount()
        {
            int num = 0;
            for (int i = 0; i < pool.Count; i++) {
                num += pool[i].GetFreeCacheCount();
            }
            return num;
        }

        public int GetFreeCacheCount(object key)
        {
            CachedObjectInfo cacheInfo = GetCacheInfo(key);
            if (cacheInfo == null) {
                return 0;
            }
            return cacheInfo.GetFreeCacheCount();
        }

        public void InitPool()
        {
            if (pool == null) {
                pool = new List<CachedObjectInfo>();
            }
        }

        private bool IsContainCacheInfo(object key)
        {
            for (int i = 0; i < pool.Count; i++) {
                if (pool[i].EqualsKey(key)) {
                    return true;
                }
            }
            return false;
        }

        public int ShrinkCache(object key, int maxShrinkCount)
        {
            CachedObjectInfo cacheInfo = GetCacheInfo(key);
            if ((cacheInfo != null) && (maxShrinkCount > 0)) {
                return cacheInfo.Shrink(maxShrinkCount);
            }
            return 0;
        }

        private void TryFixPool()
        {
            int freeCacheCount = GetFreeCacheCount();
            int cacheCount = GetCacheCount();
            if ((((float)cacheCount) / ((float)mCacheBlockMaxCount)) >= 0.9f) {
                if ((((float)freeCacheCount) / ((float)cacheCount)) < 0.2f) {
                    mCacheBlockMaxCount += 0x20;
                    if (mCacheBlockMaxCount > mBlockLimitCount) {
                        mCacheBlockMaxCount = mBlockLimitCount;
                    }
                } else {
                    for (int i = 0; i < pool.Count; i++) {
                        CachedObjectInfo info = pool[i];
                        int num4 = info.GetFreeCacheCount();
                        info.Shrink(num4 / 2);
                    }
                }
            }
        }

        public object TryHitCache(object key)
        {
            if (key == null) {
                return null;
            }
            CachedObjectInfo cacheInfo = GetCacheInfo(key);
            if (cacheInfo == null) {
                return null;
            }
            object obj2 = cacheInfo.TryHitCache();
            if (obj2 != null) {
                cacheInfo.UpdateWeight(Time.realtimeSinceStartup);
            }
            return obj2;
        }

        public void Update()
        {
            float realtimeSinceStartup = Time.realtimeSinceStartup;
            if ((realtimeSinceStartup - mLastUpdateTime) >= 5f) {
                mLastUpdateTime = realtimeSinceStartup;
                if (pool != null) {
                    int index = UnityEngine.Random.Range(0, pool.Count - 1);
                    if ((index >= 0) && (index < pool.Count)) {
                        CachedObjectInfo info = pool[index];
                        if (info == null) {
                            pool.RemoveAt(index);
                        } else {
                            float num3 = Time.realtimeSinceStartup - info.GetWeight();
                            int maxShrinkCount = (int)(num3 / 60f);
                            if (maxShrinkCount > 0) {
                                int freeCacheCount = info.GetFreeCacheCount();
                                if (freeCacheCount <= 0) {
                                    pool.RemoveAt(index);
                                } else {
                                    maxShrinkCount = (maxShrinkCount <= freeCacheCount) ? maxShrinkCount : freeCacheCount;
                                    info.Shrink(maxShrinkCount);
                                }
                            }
                        }
                    }
                }
            }
        }

        public class CachedObjectInfo
        {
            private object Key;
            private Hashtable mHashTableObjects = new Hashtable();
            private Hashtable mHashTableStatus = new Hashtable();
            private ArrayList mKeyArray = new ArrayList();
            private ObjectPool.CacheReleaser mReleaser;
            private float Weight = 0f;

            public void AddToCache(object value)
            {
                int hashCode = value.GetHashCode();
                if (ObjectPool.KeepCacheWhenRent) {
                    if (mKeyArray.Contains(hashCode)) {
                        mHashTableStatus[hashCode] = true;
                    } else {
                        mKeyArray.Add(hashCode);
                        mHashTableObjects.Add(hashCode, value);
                        mHashTableStatus.Add(hashCode, true);
                    }
                } else if (!mKeyArray.Contains(hashCode)) {
                    mKeyArray.Add(hashCode);
                    mHashTableObjects.Add(hashCode, value);
                }
            }

            public bool CachedObj(int hashCode)
            {
                return mKeyArray.Contains(hashCode);
            }

            private void DestroyOneCache(object keyObject, bool ignoreStatus = false)
            {
                if (((keyObject != null) && mKeyArray.Contains(keyObject)) && (ignoreStatus || ((bool)mHashTableStatus[keyObject]))) {
                    object obj2 = mHashTableObjects[keyObject];
                    mKeyArray.Remove(keyObject);
                    mHashTableObjects.Remove(keyObject);
                    mHashTableStatus.Remove(keyObject);
                    if (mReleaser != null) {
                        mReleaser.Release(obj2);
                    }
                }
            }

            public bool EqualsKey(object key)
            {
                return (((Key != null) && (key != null)) && Key.Equals(key));
            }

            public int GetFreeCacheCount()
            {
                int num = 0;
                if (ObjectPool.KeepCacheWhenRent) {
                    for (int i = 0; i < mKeyArray.Count; i++) {
                        if ((bool)mHashTableStatus[mKeyArray[i]]) {
                            num++;
                        }
                    }
                    return num;
                }
                return mKeyArray.Count;
            }

            public int GetTotalCacheCount()
            {
                return mKeyArray.Count;
            }

            public float GetWeight()
            {
                return Weight;
            }

            public bool InitCache(object key, ObjectPool.CacheReleaser releaser)
            {
                if (key == null) {
                    Weight = -1f;
                    return false;
                }
                Key = key;
                mReleaser = releaser;
                return true;
            }

            public void Reset()
            {
                Key = null;
                mHashTableObjects.Clear();
                mHashTableStatus.Clear();
                mKeyArray.Clear();
                Weight = 0f;
            }

            public int Shrink(int maxShrinkCount)
            {
                List<object> list = new List<object>();
                for (int i = 0; i < mKeyArray.Count; i++) {
                    if (ObjectPool.KeepCacheWhenRent) {
                        if ((bool)mHashTableStatus[mKeyArray[i]]) {
                            list.Add(mKeyArray[i]);
                        }
                    } else {
                        list.Add(mKeyArray[i]);
                    }
                }
                for (int j = 0; (j < list.Count) && (j < maxShrinkCount); j++) {
                    DestroyOneCache(list[j], true);
                }
                return list.Count;
            }

            public object TryHitCache()
            {
                object obj2 = null;
                if (ObjectPool.KeepCacheWhenRent) {
                    for (int i = 0; i < mKeyArray.Count; i++) {
                        int num2 = (int)mKeyArray[i];
                        if ((bool)mHashTableStatus[num2]) {
                            mHashTableStatus[num2] = false;
                            obj2 = mHashTableObjects[num2];
                        }
                    }
                    return obj2;
                }
                if (mKeyArray.Count > 0) {
                    int key = (int)mKeyArray[0];
                    obj2 = mHashTableObjects[key];
                    mKeyArray.RemoveAt(0);
                    mHashTableObjects.Remove(key);
                }
                return obj2;
            }

            public void UpdateWeight(float weight)
            {
                Weight = weight;
            }
        }

        public class CacheReleaser
        {
            private ReleaseCacheHandle releaseHandle;

            public void RegisterRelease(ReleaseCacheHandle handle)
            {
                if (handle != null) {
                    releaseHandle = (ReleaseCacheHandle)Delegate.Combine(releaseHandle, handle);
                }
            }

            public void Release(List<object> objects)
            {
                for (int i = 0; i < objects.Count; i++) {
                    Release(objects[i]);
                }
            }

            public void Release(object obj)
            {
                if (releaseHandle != null) {
                    releaseHandle(obj);
                }
            }

            public void UnRegisterRelease(ReleaseCacheHandle handle)
            {
                if (handle != null) {
                    releaseHandle = (ReleaseCacheHandle)Delegate.Remove(releaseHandle, handle);
                }
            }

            public delegate void ReleaseCacheHandle(object obj);
        }
    }
}
