using UnityEditor;

/***
 * BundleCommand.cs
 * 
 * @author abaojin
 */
namespace GameEditor
{
    public class BundleCommand
    {
        public static void BuildAssetBundle(BuildTarget target, string output)
        {
            BundleSetting.BuildAssetBundle(target, output);
        }

        public static void BuildAssetBundleAndCopy(BuildTarget target, string output)
        {
            BundleSetting.BuildAssetBundle(target, output);
            BundleSetting.CopyAssetBundle(target);
        }

        public static void ClearAllBuild()
        {
            BundleSetting.ClearAllBuild(PathConfig.RES_OUTPUT);
        }

        public static void UpdateAllBundleName(bool isClear = false)
        {
            if (isClear) {
                BundleSetting.ClearAllBundleName();
            }
            BundleSetting.UpdateAllBundleName();
        }

        public static void ClearAllBundleName()
        {
            BundleSetting.ClearAllBundleName();
        }

    }
}
