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
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<TKey> _keys;
        [SerializeField]
        private List<TValue> _values;

        public void OnBeforeSerialize()
        {
            _keys = new List<TKey>(this.Count);
            _values = new List<TValue>(this.Count);
            foreach (var kvp in this) {
                _keys.Add(kvp.Key);
                _values.Add(kvp.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            this.Clear();
            int count = Mathf.Min(_keys.Count, _values.Count);
            for (int i = 0; i < count; ++i) {
                this.Add(_keys[i], _values[i]);
            }
        }
    }
}
 
