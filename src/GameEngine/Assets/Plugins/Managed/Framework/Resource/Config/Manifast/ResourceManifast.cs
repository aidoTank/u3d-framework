using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Framework
{
    public class ResourceManifastConfig : ScriptableObject
    {
        public string[] Dirs = new string[0];
        public ResourceAssetRef[] Assets = new ResourceAssetRef[0];
        public ResourceBundleRef[] Bundles = new ResourceBundleRef[0];
    }
}
