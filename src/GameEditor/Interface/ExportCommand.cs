/***
 * ExportCommand.cs
 * 
 * @author administrator
 */
namespace GameEditor
{
    /// <summary>
    /// 整个对外接口，方便Jenkins等调用
    /// </summary>
    public class ExportCommand
    {
        /// <summary>
        /// 导出游戏资源
        /// </summary>
        public static void BuildResource()
        {
            //BundleCommand.BuildAssetBundle();
        }

        /// <summary>
        /// 导出游戏包
        /// </summary>
        public static void BuildPackage()
        {
            //BuildCommand.BuildApplication();
        }
    }
}
