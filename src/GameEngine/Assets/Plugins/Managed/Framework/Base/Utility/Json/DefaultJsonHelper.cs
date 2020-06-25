using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class DefaultJsonHelper : IJsonHelper
    {
        public string ToJson(object obj)
        {
            return UnityEngine.JsonUtility.ToJson(obj);
        }

        public T ToObject<T>(string json)
        {
            return UnityEngine.JsonUtility.FromJson<T>(json);
        }

        public object ToObject(Type objectType, string json)
        {
            return UnityEngine.JsonUtility.FromJson(json, objectType);
        }
    }
}
