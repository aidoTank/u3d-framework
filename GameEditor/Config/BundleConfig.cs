namespace GameEditor
{
    public class BundleInfo
    {
        public string SrcPath;
        public string DstPath;
        public string BundleName;
        public string[] FileExts;
    }

    public class BundleConfig
    {
        public static readonly string BUNDLE_SUFFIX = ".unity3d";

        public static readonly BundleInfo[] BUNDLE_CONFIG = new BundleInfo[]
        {
            NewInfo(
                "Assets/Arts/Config",
                "Config",
                "Config",
                NewString(".txt", ".ini")
            ),

            NewInfo(
                "Assets/Arts/Level",
                "Level",
                string.Empty,
                NewString(".Prefab")
            )
        };

        public static readonly string[] BUNDLE_CONFIG_IGNORE = new string[] 
        {
            "ignore file path"
        };

        public static BundleInfo NewInfo(string srcPath, string dstPath, string bundleName, string[] fileExts)
        {
            return new BundleInfo {
                SrcPath = srcPath,
                DstPath = dstPath,
                BundleName = bundleName,
                FileExts = fileExts
            };
        }

        public static string[] NewString(params string[] fileExts)
        {
            return fileExts;
        }
    }
}
