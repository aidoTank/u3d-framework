using System;
using System.Collections.Generic;
using System.Text;

/***
 * ExportCommand.cs
 * 
 * @author abaojin
 */
namespace GameEditor
{
    /// <summary>
    /// 整个对外接口，方便Jenkins等调用
    /// </summary>
    public class ExportCommand
    {
        public static void BuildResource()
        {
            BundleCommand.BuildAssetBundle();
        }

        public static void BuildPackage()
        {
            BuildCommand.BuildApplication();
        }
    }
}
