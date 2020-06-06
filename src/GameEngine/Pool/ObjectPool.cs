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
    /// 游戏显示对象池
    /// </summary>
    public static class ObjectPool
    {
        private static Dictionary<string, BlockPool> objPools;

        static ObjectPool()
        {
            objPools = new Dictionary<string, BlockPool>();
        }

        /// <summary>
        /// 初始化对象缓冲池
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="initialCapacity"></param>
        public static void InitPool(string key, GameObject obj, int initialCapacity = 10)
        {
            BlockPool bPool = null;
            if(!objPools.TryGetValue(key, out bPool)) {
                bPool = new BlockPool(obj, initialCapacity);
                objPools.Add(key, bPool);
            }
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static GameObject GetObject(string key)
        {
            GameObject cacheGo = null;
            BlockPool bPool = null;
            if (objPools.TryGetValue(key, out bPool)) {
                cacheGo = bPool.GetObject();
            }
            if(cacheGo == null) {
                GameLog.Error(string.Format("{0} cache get err!", key));
            }
            return cacheGo;
        }

        public static GameObject GetObject(string key, Vector3 pos)
        {
            GameObject cacheGo = null;
            BlockPool bPool = null;
            if (objPools.TryGetValue(key, out bPool)) {
                cacheGo = bPool.GetObject(pos);
            }
            if (cacheGo == null) {
                GameLog.Error(string.Format("{0} cache get err!", key));
            }
            return cacheGo;
        }

        public static T GetObject<T>(string key) where T : Component
        {
            T t = null;
            BlockPool bPool = null;
            if (objPools.TryGetValue(key, out bPool)) {
                t = bPool.GetObject<T>();
            }
            if (t == null) {
                GameLog.Error(string.Format("{0} cache get err!", key));
            }
            return t;
        }

        public static T GetObject<T>(string key, Vector3 pos) where T : Component
        {
            T t = null;
            BlockPool bPool = null;
            if (objPools.TryGetValue(key, out bPool)) {
                t = bPool.GetObject<T>(pos);
            }
            if (t == null) {
                GameLog.Error(string.Format("{0} cache get err!", key));
            }
            return t;
        }

        /// <summary>
        /// 回收游戏对象，不销毁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public static void Recycle(string key, GameObject obj)
        {
            BlockPool bPool = null;
            if (objPools.TryGetValue(key, out bPool)) {
                bPool.Recycle(obj);
            }
        }

        /// <summary>
        /// 销毁游戏对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public static void Destroy(string key, GameObject obj)
        {
            BlockPool bPool = null;
            if (objPools.TryGetValue(key, out bPool)) {
                bPool.Destroy(obj);
            }
        }
    }


    /// <summary>
    /// 简单高效GameObject对象池
    /// </summary>
    internal class BlockPool
    {
        private Queue<GameObject> pool;

        private GameObject cachePrefab;
        private Transform poolParent;
        private string poolName;
        private string objName;

        public BlockPool(GameObject obj, int initialCapacity)
        {
            cachePrefab = obj;
            
            objName = obj.name;
            poolName = string.Format("{0}_pool", objName);

            pool = new Queue<GameObject>(initialCapacity);
            poolParent = new GameObject(poolName).transform;

            for (int i = 0; i < initialCapacity; i++) {
                GameObject objClone = GameObject.Instantiate(cachePrefab) as GameObject;
                objClone.transform.parent = poolParent;
                objClone.name = string.Format("{0}_{1}", objName, i);
                objClone.SetActive(false);
                pool.Enqueue(objClone);
            }
        }

        public GameObject GetObject(Vector3 pos)
        {
            GameObject obj = null;

            if (pool.Count > 0) {
                obj = pool.Dequeue();
            } else {
                obj = GameObject.Instantiate(cachePrefab) as GameObject;
                obj.name = string.Format("{0}_{1}", objName ,pool.Count + 1);
                obj.transform.parent = poolParent;
            }

            obj.transform.position = pos;
            obj.SetActive(true);

            return obj;
        }

        public GameObject GetObject()
        {
            GameObject obj = null;

            if (pool.Count > 0) {
                obj = pool.Dequeue();
            } else {
                obj = GameObject.Instantiate(cachePrefab) as GameObject;
                obj.name = string.Format("{0}_{1}", objName, pool.Count + 1);
                obj.transform.parent = poolParent;
            }
            obj.SetActive(true);

            return obj;
        }

        public T GetObject<T>(Vector3 pos) where T : Component
        {
            GameObject obj = null;

            if (pool.Count > 0) {
                obj = pool.Dequeue();
            } else {
                obj = GameObject.Instantiate(cachePrefab) as GameObject;
                obj.name = string.Format("{0}_{1}", objName, pool.Count + 1);
                obj.transform.parent = poolParent;
            }

            obj.transform.position = pos;
            obj.SetActive(true);

            return obj.GetComponent<T>();
        }

        public T GetObject<T>() where T : Component
        {
            GameObject obj = null;

            if (pool.Count > 0) {
                obj = pool.Dequeue();
            } else {
                obj = GameObject.Instantiate(cachePrefab) as GameObject;
                obj.name = string.Format("{0}_{1}", objName, pool.Count + 1);
                obj.transform.parent = poolParent;
            }
            obj.SetActive(true);

            return obj.GetComponent<T>();
        }

        public void Recycle(GameObject obj)
        {
            if(obj != null) {
                obj.SetActive(false);
                pool.Enqueue(obj);
            }
        }

        public void Destroy(GameObject obj)
        {
            if(obj != null) {
                Object.DestroyImmediate(obj);
                Resources.UnloadUnusedAssets();
            }
        }
    }
}
