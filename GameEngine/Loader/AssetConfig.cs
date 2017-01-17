using System;

namespace GameEngine
{
	/// <summary>
	/// 资源相对路径配置
	/// </summary>
	public static class AssetConfig
	{
	    public static string AssetSuffix = ".ab";//for assetbundle


        public static string MeshVisualProperty = "m_r_vp";
	    public static string CharacterPath
	    {
	        get
	        {
	            return "Character/";
	        }
	    }
	    //----------------------------------------------
	    public static string ParticleConfig = "ParticleData.config";
        public static string ParticleShader = "particleshaders";
        public static string MeshShader = "meshshaders";
        public static string LevelShader = "levelshaders";
	    public static string ParticlePath
	    {
	        get
	        {
	            return "Particle/";
	        }
	    }
	    public static string ParticleRes
	    {
	        get
	        {
	            return ParticlePath + "res/";
	        }
	    }
	    public static string ParticlePrefab
	    {
	        get
	        {
	            return ParticlePath + "prefab/";
	        }
	    }

        public static string SceneConfig = "SceneData.config";
        public static string ScenePath
        {
            get
            {
                return "Scene/";
            }
        }
        public static string LevelPath
        {
            get
            {
                return ScenePath + "level/";
            }
        }
        public static string LevelResPath
        {
            get
            {
                return LevelPath + "Res/";
            }
        }
	
	    public static string CustomBonesConfig = "CustomBone.xml";
	    public static string ModelPath
	    {
	        get
	        {
	            return "Model/";
	        }
	    }
	
	    public static string ModelPrefabPath
	    {
	        get
	        {
	            return "Model/Prefab/";
	        }
	    }
	    public static string WeaponPath
	    {
	        get
	        {
	            return "Weapon/";
	        }
	    }
	    public static string RolightPath
	    {
	        get
	        {
	            return "Scene/Dynamiclight.ab";
	        }
	    }
	    public static string CustomMaterialPath
	    {
	        get
	        {
	            return "Model/materials.ab";
	        }
	    }

        public static string FileListPath
        {
            get
            {
                return "filelist.ab";
            }
        }
	}
	
}
