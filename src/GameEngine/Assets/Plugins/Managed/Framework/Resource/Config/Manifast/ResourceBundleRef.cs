using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    [Serializable]
    public class ResourceBundleRef
    {
        public int Id;
        public string Name;

        /// <summary>
        /// 依赖资源
        /// </summary>
        public int[] DependIds = new int[0];
    }
}
