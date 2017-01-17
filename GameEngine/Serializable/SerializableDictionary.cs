using UnityEngine;
using System.Collections.Generic;
using System;

/***
 * SerializableDictionary.cs
 * 
 * @author abaojin
 */ 
namespace GameEngine
{
    /// <summary>
    /// 支持序列化字典
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<TKey> keys = new List<TKey>();
        [SerializeField]
        private List<TValue> values = new List<TValue>();

        private Dictionary<TKey, TValue> datas = new Dictionary<TKey, TValue>();
        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();

            foreach (var kvp in datas) {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }
        public void OnAfterDeserialize()
        {
            datas.Clear();
            int nCount = Math.Min(keys.Count, values.Count);
            for (int i = 0; i < nCount; i++) {
                datas.Add(keys[i], values[i]);
            }

            keys.Clear();
            values.Clear();
        }

        public int Count { get { return datas.Count; } }

        public TValue this[TKey key] {
            get {
                return datas[key];
            }
        }
        public void Add(TKey key, TValue value)
        {
            datas.Add(key, value);
        }
        public void Clear()
        {
            datas.Clear();
        }
        public bool ContainsKey(TKey key)
        {
            return datas.ContainsKey(key);
        }
        public bool ContainsValue(TValue value)
        {
            return datas.ContainsValue(value);
        }
        public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            return datas.GetEnumerator();
        }
        public bool Remove(TKey key)
        {
            return datas.Remove(key);
        }
        public bool TryGetValue(TKey key, out TValue value)
        {
            return datas.TryGetValue(key, out value);
        }
    }
}
 
