using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public static class JsonUtility
    {
        private static IJsonHelper s_JsonHelper;

        public static void SetJsonHelper(IJsonHelper jsonHelper)
        {
            s_JsonHelper = jsonHelper;
        }
    }
}
