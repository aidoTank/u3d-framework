using UnityEditor;

/***
 * BuildCommand.cs
 * 
 * @author administrator 
 */
namespace GameEditor
{
    public class BuildCommand
    {
        public static void BuildApplication(BuildTarget target, string output)
        {
            BuildSetting.BulidPacket(target, output);
        }

        public static void ClearAllBuild()
        {
            BuildSetting.ClearAllBuild(PathConfig.APP_OUTPUT);
        }
    }
}
