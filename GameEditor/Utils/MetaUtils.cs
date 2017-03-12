using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/***
 * MetaUtils.cs
 * 
 * @author administrator
 */
namespace GameEditor
{
    public static class MetaUtils
    {
        public static void UpdateMetaLicense()
        {
            string[] extends = new string[] { ".meta" };

            EditorUtils.ProcessDir("Assets", extends, (path, param) => {
                bool hasFound = false;
                List<string> content = new List<string>();

                using (StreamReader stream = new StreamReader(path)) {
                    string line;
                    while ((line = stream.ReadLine()) != null) {
                        if (line == "licenseType: Free") {
                            line = "licenseType: Pro";
                            hasFound = true;
                        }
                        content.Add(line);
                    }
                }

                if (hasFound) {
                    File.Delete(path);

                    using (StreamWriter writer = new StreamWriter(path)) {
                        foreach (var s in content) {
                            writer.WriteLine(s);
                        }
                    }
                    Debug.Log(string.Format("完成了\"{0}\"文件的 LicenseType 更新.", path));
                }
            });

            AssetDatabase.Refresh();

            Debug.Log("<color=#00FF00>所有Meta文件检查完毕！</color>");
        }

        public static void ProcessImageFormat(string path, int maxSize, TextureFormat iosFormat, TextureFormat androidFormat, bool isMinMap)
        {
            string[] extends = new string[] { ".png.meta", ".tga.meta", ".jpg.meta" };

            EditorUtils.ProcessDir(path, extends, (file, param) => {
                bool hasFound = false;
                List<string> content = new List<string>();
                StreamReader stream = new StreamReader(file);

                string line;
                while ((line = stream.ReadLine()) != null) {
                    if (line.IndexOf("enableMipMap:") != -1) {
                        line = "    enableMipMap: " + Convert.ToInt32(isMinMap);
                        hasFound = true;
                    }
                    string targetLine = string.Empty;
                    if (line.IndexOf("buildTargetSettings:") != -1) {
                        line = "  buildTargetSettings: []";
                        while ((targetLine = stream.ReadLine()) != null && (targetLine.IndexOf("- buildTarget:") != -1)) {
                            stream.ReadLine();
                            stream.ReadLine();
                            stream.ReadLine();
                        }
                        hasFound = true;
                    }

                    if (line.IndexOf("buildTargetSettings: []") != -1) {
                        line = "  buildTargetSettings:";
                        content.Add(line);
                        line = "  - buildTarget: iPhone";
                        content.Add(line);
                        line = "    maxTextureSize: " + maxSize;
                        content.Add(line);
                        line = "    textureFormat: " + (int)iosFormat;
                        content.Add(line);
                        line = "    compressionQuality: 50";
                        content.Add(line);
                        line = "  - buildTarget: Android";
                        content.Add(line);
                        line = "    maxTextureSize: " + maxSize;
                        content.Add(line);
                        line = "    textureFormat: " + (int)androidFormat;
                        content.Add(line);
                        line = "    compressionQuality: 50";
                        content.Add(line);
                        line = targetLine;
                        hasFound = true;
                    }
                    content.Add(line);
                }
                stream.Close();

                if (hasFound) {
                    File.Delete(file);
                    StreamWriter writer = new StreamWriter(file);
                    foreach (string s in content) {
                        writer.WriteLine(s);
                    }
                    writer.Close();
                }
            });

            AssetDatabase.Refresh();

            Debug.Log(string.Format("<color=#00FF00>[{0}]资源压缩格式更新完成！</color>", path));
        }
    }
}
