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

        public int[] DependIds = new int[0];
    }
}
