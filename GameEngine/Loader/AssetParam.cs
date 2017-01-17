using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
	/// <summary>
	/// 资源加载的参数
	/// </summary>
	public class AssetParam
	{
	    public delegate void ResourceListener(AssetPtr assetPtr);
         
	
	    public ResourceListener listener;
	    public AssetPtr asset;

        // public AssetManager.PathType ptype = AssetManager.PathType.Path_None;
	}
}
