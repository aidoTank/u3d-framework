using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public interface IJsonHelper
    {
        string ToJson(object obj);

        T ToObject<T>(string json);

        object ToObject(Type objectType, string json);
    }
}
