using System;
using System.Collections.Generic;


/// <summary>
/// 特效资源数据,建立这个数据目的
/// 是确保在特效加载之前其依赖的资源
/// 加载到内存中，因贴图不打包到特效里面
/// </summary>
public class ParticleResourceData
{
   public string prefabname = string.Empty;
   public List<string> dependres = new List<string>();
}

