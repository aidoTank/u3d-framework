using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using GameEngine;
using System.IO;
using System.Text;

/***
 * UGUIAtlasEditor.cs
 * 
 * @anthor administrator
 */
namespace GameEditor
{
    public class UGUIAtlasEditor : Editor
    {
        // 最大图集尺寸
        private static float AtlasMaxSize = 2048;
        private static float Padding = 1;
        private static List<SpriteInfo> SpriteList = new List<SpriteInfo>();

        [MenuItem(MenuConfig.TOOLS_UGUI_CREATEATLAS)]
        public static void CreateUGUIAtlas()
        {
            Object activeObj = Selection.activeObject;
            if (activeObj == null) {
                Debug.LogError("Please select sprite dir.");
                return;
            }
            string dirPath = AssetDatabase.GetAssetPath(activeObj);

            string assetPath;
            Object[] objs = Selection.GetFiltered(typeof(Texture), SelectionMode.DeepAssets);
            for (int i = 0; i < objs.Length; i++) {
                Object obj = objs[i];
                if (obj.name.StartsWith(" ") || obj.name.EndsWith(" ")) {
                    string newName = obj.name.TrimStart(' ').TrimEnd(' ');
                    Debug.LogError(string.Format("rename texture'name old name : {0}, new name {1}", obj.name, newName));
                    AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(obj), newName);
                }
            }
            Texture2D[] texs = new Texture2D[objs.Length];
            if (texs.Length <= 0) {
                Debug.LogError("Please select sprites Dir.");
                return;
            }

            for (int i = 0; i < objs.Length; i++) {
                texs[i] = objs[i] as Texture2D;
                assetPath = AssetDatabase.GetAssetPath(texs[i]);
                AssetDatabase.ImportAsset(assetPath);
            }

            // 得到图片的设置信息
            SpriteImporterSettings[] originalSets = GeterSettings(texs);
            for (int i = 0; i < texs.Length; i++) {
                SettingTexture(texs[i], true, TextureImporterFormat.RGBA32);
            }

            // 主要的打图集代码
            string outputPath = dirPath + ".png";
            PacketSprite4Atlas(texs, outputPath);

            EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath(outputPath, typeof(Texture)));
        }

        public static SpriteImporterSettings[] GeterSettings(Texture2D[] texs)
        {
            SpriteImporterSettings[] sets = new SpriteImporterSettings[texs.Length];
            for (var i = 0; i < texs.Length; i++) {
                Texture2D tex = texs[i];
                string assetPath = AssetDatabase.GetAssetPath(tex);
                TextureImporter imp = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                sets[i] = new SpriteImporterSettings(imp.isReadable, imp.textureFormat);
                if (imp.textureType == TextureImporterType.Sprite && imp.spriteBorder != Vector4.zero) {
                    SpriteInfo spriteInfo = new SpriteInfo(tex.name, imp.spriteBorder, imp.spritePivot, tex.width, tex.height);
                    SpriteList.Add(spriteInfo);
                }
            }
            return sets;
        }

        public static void SettingTexture(Texture2D tex, bool isReadable, TextureImporterFormat textureFormat)
        {
            string assetPath = AssetDatabase.GetAssetPath(tex);
            TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            importer.isReadable = isReadable;
            importer.textureFormat = textureFormat;
            importer.mipmapEnabled = false;
            importer.npotScale = TextureImporterNPOTScale.None;
            importer.SaveAndReimport();
        }

        public static void PacketSprite4Atlas(Texture2D[] texs, string outputPath)
        {
            
            Texture2D atlas = new Texture2D(1, 1);
            Rect[] rect = atlas.PackTextures(texs, (int)Padding, (int)AtlasMaxSize);
                                                                                 
            File.WriteAllBytes(outputPath, atlas.EncodeToPNG());
            RefreshAsset(outputPath);

            // 记录图片的名字，只是用于输出日志用;
            StringBuilder names = new StringBuilder();
            // SpriteMetaData结构可以让我们编辑图片的一些信息,想图片的name,包围盒border,在图集中的区域rect等
            SpriteMetaData[] sheet = new SpriteMetaData[rect.Length];
            for (int i = 0; i < sheet.Length; i++) {
                SpriteMetaData meta = new SpriteMetaData();
                meta.name = texs[i].name;
                meta.rect = rect[i];
                // 这里的rect记录的是单个图片在图集中的uv坐标值
                // 因为rect最终需要记录单个图片在大图片图集中所在的区域rect，所以我们做如下的处理
                meta.rect.Set(
                    meta.rect.x * atlas.width,
                    meta.rect.y * atlas.height,
                    meta.rect.width * atlas.width,
                    meta.rect.height * atlas.height
                );
                // 如果图片有包围盒信息的话
                SpriteInfo spriteInfo = GetSpriteMetaData(meta.name);
                if (spriteInfo != null) {
                    meta.border = spriteInfo.SpriteBorder;
                    meta.pivot = spriteInfo.SpritePivot;
                }
                sheet[i] = meta;
                // 打印日志用
                names.Append(meta.name);
                if (i < sheet.Length - 1) {
                    names.Append(",");
                }
            }

            // 设置图集的信息
            TextureImporter importer = TextureImporter.GetAtPath(outputPath) as TextureImporter;
            importer.textureType = TextureImporterType.Sprite;
            importer.textureFormat = TextureImporterFormat.AutomaticCompressed;
            // Multiple表示我们这个大图片(图集)中包含很多小图片
            importer.spriteImportMode = SpriteImportMode.Multiple;
            // 是否开启mipmap
            importer.mipmapEnabled = false;
            importer.spritesheet = sheet;
            // 设置图集中小图片的信息(每个图片所在的区域rect等)
            // 保存并刷新
            importer.SaveAndReimport();
            SpriteList.Clear();

            // 输出日志
            Debug.Log("Atlas create ok, " + names.ToString());
        }

        public static void RefreshAsset(string assetPath)
        {
            AssetDatabase.Refresh();
            AssetDatabase.ImportAsset(assetPath);
        }

        public static SpriteInfo GetSpriteMetaData(string texName)
        {
            for (int i = 0; i < SpriteList.Count; i++) {
                if (SpriteList[i].Name == texName) {
                    return SpriteList[i];
                }
            }
            return null;
        }

        /// <summary>
        /// 单张贴图信息
        /// </summary>
        public class SpriteInfo
        {
            // 图片的名字
            public string Name;
            // 图片的包围盒(如果有的话)
            public Vector4 SpriteBorder;
            //图片包围盒中的中心轴(如果有的话)
            public Vector2 SpritePivot;
            public float Width;
            public float Height;

            public SpriteInfo(string name, Vector4 border, Vector2 pivot, float w, float h)
            {
                this.Name = name;
                SpriteBorder = border;
                SpritePivot = pivot;
                Width = w;
                Height = h;
            }
        }

        /// <summary>
        /// 贴图导入设置
        /// </summary>
        public class SpriteImporterSettings
        {
            public bool IsReadable;
            public TextureImporterFormat TextureFormat;

            public SpriteImporterSettings(bool isReadable, TextureImporterFormat textureFormat)
            {
                this.IsReadable = isReadable;
                this.TextureFormat = textureFormat;
            }
        }

        [MenuItem(MenuConfig.TOOLS_UGUI_ATLASPREFAB)]
        public static void CreateAtlasPrefab()
        {
            Object activeObj = Selection.activeObject;
            if (activeObj == null) {
                Debug.LogError("Please select sprite atlas.");
                return;
            }
            string path = AssetDatabase.GetAssetPath(activeObj);
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer != null && importer.textureType == TextureImporterType.Sprite && importer.spriteImportMode == SpriteImportMode.Multiple) {
                UGUIAtlas atlas = ScriptableObject.CreateInstance<UGUIAtlas>();
                object[] objs = AssetDatabase.LoadAllAssetsAtPath(path);
                atlas.SpriteLists.Clear();
                foreach (object o in objs) {
                    if (o.GetType() == typeof(Texture2D)) {
                        atlas.MainTex = o as Texture2D;
                    } else if (o.GetType() == typeof(Sprite)) {
                        atlas.SpriteLists.Add(o as Sprite);
                    }
                }
                AssetDatabase.CreateAsset(atlas, path.Replace(".png", "_Atlas.asset"));
                AssetDatabase.Refresh();
            } else {
                Debug.LogError("Select not a atlas.");
            }
        }
    }
}
