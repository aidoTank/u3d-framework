using System.Collections.Generic;
using UnityEngine;

/***
 * ObjectPool.cs
 * 
 * @author administrator
 */
namespace GameLogic
{
    /// <summary>
    /// 简单高效GameObject对象池
    /// </summary>
    public class ObjectPool
    {
        private Queue<GameObject> pool;

        private GameObject cachePrefab;
        private Transform poolParent;
        private string poolName;

        public ObjectPool(GameObject obj, int initialCapacity)
        {
            cachePrefab = obj;
            poolName = string.Format("{0}_poolParent", obj.name);

            pool = new Queue<GameObject>(initialCapacity);
            poolParent = new GameObject(poolName).transform;

            for (int i = 0; i < initialCapacity; i++) {
                GameObject objClone = GameObject.Instantiate(cachePrefab) as GameObject;
                objClone.transform.parent = poolParent;
                objClone.name = poolName + i.ToString();
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
                obj.transform.parent = poolParent;
            }
            obj.SetActive(true);

            return obj.GetComponent<T>();
        }

        public void Recycle(GameObject obj)
        {
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}
